/// /// /// /// /// /// How F# implements a message-based approach

#nowarn "40"
let printerAgent = MailboxProcessor.Start(fun inbox ->

    // the massage processing function
    let rec messageLoop = async{
        // read a message
        let! msg = inbox.Receive()

        // process a message
        printfn "message is: %s" msg

        // loop to top
        return! messageLoop
    }
    // start the loop
    messageLoop
    )

printerAgent.Post "hello"
printerAgent.Post "hello again"
printerAgent.Post "hello a third time"

/// /// /// /// /// Managing shared state
// the locking approach to shared state
open System
open System.Threading
open System.Diagnostics

// a utility fucntion
type Utility() = 
    static let rand = new Random()

    static member RandomSleep() = 
        let ms = rand.Next(1,10)
        Thread.Sleep ms

// an implementation of a shared counter using locks
type LockedCounter() = 
    static let _lock = new Object()

    static let mutable count = 0
    static let mutable sum = 0
    static let updateState i =
        // increment the counters and ..
        sum <- sum + i
        count <- count + 1
        printfn "Count is: %i. Sum is %i" count sum

        // .. emulated a short delay
        Utility.RandomSleep()

    // public interface to hide the state
    static member Add i = 
        // see how long a client has to wait
        let stopwatch = new Stopwatch()
        stopwatch.Start()

        // start lock. Same as C# lock(...)
        lock _lock (fun() ->
            // see how long the wait was
            stopwatch.Stop()
            printfn "Client waited %i" stopwatch.ElapsedMilliseconds

            // do the core logic
            updateState i 
            )
    // release lock

// test in isolation    
LockedCounter.Add 4
LockedCounter.Add 5

let makeCountingTask addFunction taskId = async{
    let name = sprintf "Task%i" taskId
    for i in [1..3] do
        addFunction i
    }

// test in isolation
let task = makeCountingTask LockedCounter.Add 1
Async.RunSynchronously task

let messageExample5 = 
    [1..5]
        |> List.map(fun i -> makeCountingTask MessageBasedCounter.Add i)
        |> Async.Parallel
        |> Async.RunSynchronously
        |> ignore

//c///c//c// /// Shared IO
// IO without serialization
let slowConsoleWrite msg = 
    msg |> String.iter (fun ch ->
        System.Threading.Thread.Sleep(1)
        System.Console.Write ch
        )

// test in isolation
slowConsoleWrite "abc"

let makeTask logger taskId = async {
    let name = sprintf "Task%i" taskId
    for i in [1..3] do
        let msg = sprintf "-%s:Loop%i-" name i
        logger msg
    }

// test in isolation
let task = makeTask slowConsoleWrite 1
Async.RunSynchronously task

type UnserializedLogger() =
    // interface
    member this.Log msg = slowConsoleWrite msg

// test in isolation
let unserializedLogger = UnserializedLogger()
unserializedLogger.Log "hello"

let unserializedExample =
    let logger = new UnserializedLogger()
    [1..5]
        |> List.map (fun i -> makeTask logger.Log i)
        |> Async.Parallel
        |> Async.RunSynchronously
        |> ignore

// Serialized IO with messages

type SerializedLogger() =

    // create the mailbox processor
    let agent = MailboxProcessor.Start(fun inbox ->

        // the message processing function
        let rec messageLoop () = async{

            // read a message
            let! msg = inbox.Receive()

            // write it to the log
            slowConsoleWrite msg

            // loop to top
            return! messageLoop ()
            }

        // start the loop
        messageLoop ()
        )

    // public interface
    member this.Log msg = agent.Post msg

// test in isolation
let serializedLogger = SerializedLogger()
serializedLogger.Log "hello"

let serializedExample =
    let logger = new SerializedLogger()
    [1..5]
        |> List.map (fun i -> makeTask logger.Log i)
        |> Async.Parallel
        |> Async.RunSynchronously
        |> ignore
