
let printErrorMessage () =
    printfn "Program Input should be 'readfile filename'"

let readText (filename:string) : string = 
    let line = 
        try
          let reader = System.IO.File.OpenText filename
          reader.ReadToEnd ()
        with
          _ -> "" // The file cannot be read, so we return an empty string
    printfn "%s" line
    line

let rec convertText (src:string) : string = 
    let lowerSRC = src.ToLower()
    match lowerSRC with
    | "" -> ""
    | elm -> 
        if ('a' <= elm.[0] && elm.[0] <= 'z')
        then 
            elm.[0].ToString() + (convertText elm.[1..])
        else
            convertText elm.[1..]
 
let rec countchar (src:string)(letter:char): int list = 
  if letter >= 'a' && letter <= 'z' then 
    ((String.filter (fun char -> char=letter) src).Length):: (countchar src (char (int letter + 1)))
  else
    []
let histogram (src:string): int list = (countchar src 'a')

/// Reverse the order of a list of any type
let rec reverse (lst : 'a list) : 'a list =
  match lst with
    elm :: rest -> (reverse rest) @ [elm]
    | [] -> []

/// Calculate the cumulative sum of a list of integers from the first to the last element. First element is the first number in the original list, last element is the sum of all integers in the original list.
let cumSum (lst : int list) : int list =
  /// Prepend the sum of the first element in acc and elm to acc.
  let updateCumSum (acc : int list) (elm : int) : int list =
    match acc with
      [] -> [elm]
      | a -> a.Head + elm :: a

  List.fold updateCumSum [] lst |> reverse

/// Given a monotonic function and an index into its value set, find the corresponding value on its definition set.
let reverseLookup (monotonic : 'a list) (v : 'a) : int =
    try
      List.findIndex (fun w -> w > v) monotonic
    with
      _ -> monotonic.Length - 1

/// Generate a random character according to a histogram.
let rnd = System.Random() // A global object, if included in randomChar then seed point only changes slowly giving strongly correlated results in time
let randomChar (hist : int list) : char =
  let cumHist = cumSum hist
  let v = rnd.Next(cumHist.[cumHist.Length-1])
  let i = reverseLookup cumHist v
  'a' + char i

/// Generate a string of random characters each distributed according to a histogram.
let randomString (hist : int list) (len : int) : string = 
  /// Append random characters to a string, each distributed according to a histogram.
  let rec appendRandomString (hist : int list) (len : int) (src : string) : string = 
    if len = 0 then
      src
    else
      src + string (randomChar hist) |> appendRandomString hist (len-1)

  appendRandomString hist len ""


[<EntryPoint>]
let main (paramList : string[]) : int = 
    if paramList.Length <> 2 then
        printErrorMessage ()
        0
    else
        match paramList.[0] with
            | "readfile" ->
                readText paramList.[1]
                let str = readText paramList.[1]
                printfn "str : %A" str
                let convertedstr = convertText str
                printfn "convertedstr : %A" convertedstr
                printfn "histogram : \n%A" (histogram convertedstr)
                let newstring = randomString (histogram convertedstr) (convertedstr.Length)
                printfn "new random string : \n%A" newstring
                printfn "new histogram : \n%A" (histogram newstring)
            | _ ->
                printErrorMessage ()
        1