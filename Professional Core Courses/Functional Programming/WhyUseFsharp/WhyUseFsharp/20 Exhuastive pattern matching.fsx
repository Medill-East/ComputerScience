open System.Net.WebRequestMethods
open System.Net.WebRequestMethods

//Let’s compare some C# to F# again. Here’s some C# code that uses a switch statement to handle different types of state.
//enum State { New, Draft, Published, Inactive, Discontinued }
//void HandleState(State state)
//{
//    switch (state)
//    {
//    case State.Inactive: 
//        ... 
//    case State.Draft:  
//        ... 
//    case State.New: 
//        ... 
//    case State.Discontinued:  
//        ... 
//    } 
//}

/// /// /// /// exhaustive matching
//type State = New|Draft|Published|Inactive|Discontinued
//let handelState state = 
//    match state with    
//    | Inactive ->
//        ...
//    | Draft ->
//        ...
//    | New ->
//        ...
//    | Discontinued ->
//        ...


/// /// /// /// /// Avoiding nulls with the Option type

/// c#
//if (myObject != null)
//{
//  // do something }

let getFileInfo filePath = 
    let fi = new System.IO.FileInfo(filePath)
    if fi.Exists
    then Some(fi)
    else None

let goodFileName = "good.txt"
let badFileName = "bad.txt"

let goodFileInfo = getFileInfo goodFileName // Some(fileinfo)
let badFileInfo = getFileInfo badFileName // None

match goodFileInfo with
    | Some fileinfo ->
        printfn " the file %s exists" fileinfo.FullName
    | None ->
        printfn "the file doesn't exist"

match badFileInfo with
    | Some fileinfo ->
        printfn " the file %s exists" fileinfo.FullName
    | None ->
        printfn "the file doesn't exist"

/// /// /// /// // Exhaustive pattern matching for edge cases
// c#
//public IList<float> MovingAverages(IList<int> list)
//{
//    var averages = new List<float>();
//    for (int i = 0; i < list.Count; i++)
//    {
//        var avg = (list[i] + list[i+1]) / 2;
//        averages.Add(avg);
//    }
//    return averages;
//}

let rec movingAverages list = 
    match list with
    // if input is empty, return an empty list
    | [] -> []
    // otherwise process pairs of items from the input
    | x::y::rest ->
        let avg = (x+y)/2.0
        // build the result by recursing the rest of the list
        avg :: movingAverages(y::rest)
    // for one item, return an empty list
    | [_] -> []

//test
movingAverages [1.0]
movingAverages [1.0;2.0]
movingAverages [1.0;2.0;3.0]

/// /// /// /// /// Exhaustive pattern matching as an error handling technique
// define a "union" of two differen alternatives
type Result<'a,'b> = 
    | Success of 'a // 'a means generic type. The actual type will be determined when it is used
    | Failure of 'b // generic failure type as well

// define all possible errors
type FileErrorReason = 
    | FileNotFound of string
    | UnauthorizedAccess of string * System.Exception

// define a low level function in the bottom layer
let performActionOnFile action filePath = 
    try
        // open file, do the action and return the result
        use sr = new System.IO.StreamReader(filePath:string)
        let result = action sr // do the action to the reader
        sr.Close()
        Success (result)    // return a success
    with    // catch some exceptions and convert them to errors
        | :? System.IO.FileNotFoundException as ex
            -> Failure (FileNotFound filePath)
        | :? System.Security.SecurityException as ex
            -> Failure (UnauthorizedAccess (filePath,ex))
        // other exception are unhandled

// a function in the middle layer
let middleLayerDo action filePath =
    let fileResult = performActionOnFile action filePath
    // do some stuff
    fileResult // ruturn

// a function in the top layer
let topLayerDo action filePath = 
    let fileResult = middleLayerDo action filePath
    // do some stuff
    fileResult // return

/// Here is an example of a client function that accesses the top layer:

// get teh first line of the file
let printFirstLineOfFile filePath =
    let fileResult = topLayerDo (fun fs -> fs.ReadLine()) filePath

    match fileResult with
    | Success result ->
        // note type-safe string printing with %s
        printfn "first line is: '%s'" result
    | Failure reason ->
        match reason with   // must match EVERY reason
        | FileNotFound file->
            printfn "File not found: %s" file
        | UnauthorizedAccess (file,_) ->
            printfn "You do not have access to the file: %s" file

// In the example below, the function uses the underscore wildcard 
//to treat all the failure reasons as one. 
//This can be considered bad practice
//if we want to get the benefits of the strictness, 
//but at least it is clearly done.

/// get the length of the text in the file
let printLengthOfFile filePath = 
   let fileResult = 
     topLayerDo (fun fs->fs.ReadToEnd().Length) filePath

   match fileResult with
   | Success result -> 
      // note type-safe int printing with %i
      printfn "length is: %i" result       
   | Failure _ -> 
      printfn "An error happened but I don't want to be specific"

/// write some text to a file
let writeSomeText filePath someText = 
    use writer = new System.IO.StreamWriter(filePath:string)
    writer.WriteLine(someText:string)
    writer.Close()

let goodFileName = "good.txt"
let badFileName = "bad.txt"

writeSomeText goodFileName "hello"

printFirstLineOfFile goodFileName 
printLengthOfFile goodFileName 

printFirstLineOfFile badFileName 
printLengthOfFile badFileName 

/// /// /// /// /// Exhaustive pattern matching as a change management tool
