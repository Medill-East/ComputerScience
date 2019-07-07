/// /// /// /// A simple evnet stream
open System
open System.Threading

/// create a timer and register an event handler
/// then run the timer for 5s
let createTimer timerInterval eventHandler = 
    // setup a timer
    let timer = new System.Timers.Timer(float timerInterval)
    timer.AutoReset <- true

    // add an event handler
    timer.Elapsed.Add eventHandler

    // return an async task
    async {
        // start timer..
        timer.Start()
        // run for 5s
        do! Async.Sleep 5000
        // and stop
        timer.Stop()
        }

// test
// create a handler,. The event args are ignored
let basicHandler _ = printfn "tick %A" DateTime.Now

// register the handler
let basicTimer1 = createTimer 1000 basicHandler

// run the task now
Async.RunSynchronously basicTimer1

//stream of events
let createTimerAndObservable timerInterval = 
    // setup a timer
    let timer = new System.Timers.Timer(float timerInterval)
    timer.AutoReset <- true

    // evnets are automatically IObservable
    let observable = timer.Elapsed

    // return an async task
    let task = async {
        timer.Start()
        do! Async.Sleep 5000
        timer.Stop
        }
    // return a async task and the observable
    (task, observable)

// test
// create the timer and the correspondng observable
let basicTimer2, timerEventStream = createTimerAndObservable 1000

// register that everytime something happens on the
// event stream, print the time
timerEventStream
|> Observable.subscribe (fun _ -> printfn "tick %A" DateTime.Now)

// run the task now
Async.RunSynchronously basicTimer2

/// Counting events
//Create a timer that ticks every 500ms.
//At each tick, print the number of ticks so far and the current time.

// classic imperative way
type ImperativeTimerCount() = 
    let mutable count = 0

    // the event handler. The evnet args are ignored
    member this.handlerEvent _ = 
        count <- count + 1
        printfn "timer ticked with count %i" count

// create a handler class
let handler = new ImperativeTimerCount()

// register the hndler method
let timerCount1 = createTimer 500 handler.handlerEvent

// run the task now
Async.RunSynchronously timerCount1




/// do in a functional way
let timerCount2, timerEventStream2 = createTimerAndObservable 500

// setup the transformations on the evnet stream
timerEventStream2
|> Observable.scan (fun count _ -> count + 1) 0
|> Observable.subscribe (fun count -> printfn "timer ticked with count %i" count)

//run the task now
Async.RunSynchronously timerCount2

/// /// /// //// //// Merging multiple evnet streams
//// FizzBuzz
//Create two timers, called '3' and '5'. The '3' timer ticks every 300ms and the '5' tim
//er ticks every 500ms.

//Handle the events as follows:

//a) for all events, print the id of the time and the time

//b) when a tick is simultaneous with a previous tick, print 'FizzBuzz'
//otherwise:

//c) when the '3' timer ticks on its own, print 'Fizz'

//d) when the '5' timer ticks on its own, print 'Buzz'

type FizzBuzzEvent = {label:int; time:DateTime}

let areSimultaneous (earlierEvent, laterEvnet) = 
    let {label = _; time = t1 } = earlierEvent
    let {label = _; time = t2 } = laterEvnet
    t2.Subtract(t1).Milliseconds < 50    

type ImperativeFizzBuzzHandler() =

    let mutable previousEvent: FizzBuzzEvent option = None

    let printEvent thisEvent =
        let {label=id; time=t} = thisEvent
        printf "[%i] %i.%03i " id t.Second t.Millisecond
        let simultaneous = previousEvent.IsSome && areSimultaneous (previousEvent.Value,thisEvent)
        if simultaneous 
        then printfn "FizzBuzz"
        elif id = 3 then printfn "Fizz"
        elif id = 5 then printfn "Buzz"

    member this.handleEvent3 eventArgs =
        let event = {label=3; time=DateTime.Now}
        printEvent event
        previousEvent <- Some event

    member this.handleEvent5 eventArgs =
        let event = {label=5; time=DateTime.Now}
        printEvent event
        previousEvent <- Some event

// create the class

let handler2 = new ImperativeFizzBuzzHandler()

// create the two timers and register the two handlers
let timer3 = createTimer 300 handler2.handleEvent3
let timer5 = createTimer 500 handler2.handleEvent5

// run the two timers at the same time
[timer3; timer5]
|> Async.Parallel
|> Async.RunSynchronously

//////// functional version
let timer3_2, timerEventStream3 = createTimerAndObservable 300
let timer5_2, timerEventStream5 = createTimerAndObservable 300

// convert the time evnets into FizzBuzz events with the appropriate id
let eventStream3 = 
    timerEventStream3
    |> Observable.map (fun _ -> {label=3;time = DateTime.Now})

let eventStream5 = 
    timerEventStream5
    |> Observable.map (fun _ -> {label=5;time = DateTime.Now})

// combine the two streams
let combinedStream =
    Observable.merge eventStream3 eventStream5

// make pairs of events
let pairwiseStream =
    combinedStream |> Observable.pairwise

// split the stream based on whether the pairs are simultaneous
let simultaneousStream, nonSimultaneousStream =
    pairwiseStream |> Observable.partition areSimultaneous

// split the non-simultaneous stream based on the id
let fizzStream, buzzStream =
    nonSimultaneousStream
    // convert pair of events to the first event
    |> Observable.map (fun (ev1,_) -> ev1)
    // split on whether the event id is three
    |> Observable.partition (fun {label=id} -> id=3)

//print events from the
combinedStream
    |> Observable.subscribe (fun {label=id;time=t} ->
                                    printf "[%i] %i.%03i " id t.Second t.Millisecond)

//print events from the simultaneous stream
simultaneousStream
    |> Observable.subscribe (fun _ -> printfn "FizzBuzz")

//print events from the nonSimultaneous streams
fizzStream
|> Observable. subscribe (fun _ -> printfn "Fizz")

buzzStream
|> Observable. subscribe (fun _ -> printfn "Buzz")

// run the two timers at the same time
[timer3;timer5]
|> Async.Parallel
|> Async.RunSynchronously
