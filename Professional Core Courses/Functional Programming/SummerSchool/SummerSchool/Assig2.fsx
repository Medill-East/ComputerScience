open System.Diagnostics

/// 7.1 Readfile
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

/// 7.2 Converts a string, such that all letters are converted to lower case, and removes all characters except a. . . z
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

/// 7.3 Counts occurrences of each lower-case letter of the English alphabet in a string and returns a list
let rec countchar (src:string)(letter:char): int list = 
  if letter >= 'a' && letter <= 'z' then 
    ((String.filter (fun char -> char=letter) src).Length):: (countchar src (char (int letter + 1)))
  else
    []
let histogram (src:string): int list = (countchar src 'a')

/// 7.5 Counts occurrences of each pairs of lower-case letter of the English alphabet in a string and returns a list of lists
let rec countnum (src:string) (charpair:string):int =
  if src.Length >=2 then
    if src.[0..1] = charpair then
      1+(countnum src.[1..] charpair)
    else
      (countnum src.[1..] charpair)
  else
    0
let rec countcharpair (src:string) (letter1:char) (letter2:char): int list =
  if letter2 <= 'z' then
    let charpair = string letter1 + string letter2
    (countnum src charpair) :: (countcharpair src letter1 (letter2 + char 1))
  else
    []
let rec countchar1 (src:string) (letter1:char) : int list list = 
  if letter1 <= 'z' then
    (countcharpair src letter1 'a')::(countchar1 src (letter1 + char 1))
  else
    []
let cooccurrence (src:string):int list list =
  countchar1 src 'a'

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

/// 7.6 generates a random string of length len, whose character pairs are distributed according to a user specified cooccurrence histogram cooc
let rec nextrandomchar (p:int)(len:int)(cooc:int list list)(str:string):string = 
  let charprobe = 'a'+char p
  if charprobe <= 'z' then
    if str.Length < len then
      if str.[str.Length-1] = charprobe then 
          let str1 = (str+(randomString (cooc.[p]) 1))
          (str1+(nextrandomchar 0 len cooc str1))
      else
          (str+(nextrandomchar (p+1) len cooc str))
    else
      str
  else
    let str1 = str + (randomString (List.map (fun (x:int list) -> x.[0]) cooc) 1)
    (str1 + (nextrandomchar 0 len cooc str1))

let fstOrderMarkovModel (cooc:int list list) (len:int) : string =
    //let strLengthlist = List.map (fun x -> (float x) ) (List.map (List.sum) cooc)
    //let strLengthSum = List.sum strLengthlist
    //let ratiolist = List.map (fun x -> x/strLengthSum) strLengthlist
    //let randomStringAlt (hist: int list):string = randomString hist (int ((float (List.sum (hist))/strLengthSum)*(float len)))
    //let strlist = List.map (randomStringAlt) cooc
    //let composestr (acc:string) (elm:string):string = acc + elm
    //List.fold composestr "" strlist
    let str = randomString (List.map (fun (x:int list) -> x.[0]) cooc) 1
    (nextrandomchar 0 len cooc str)


[<EntryPoint>]
let main (paramList : string[]) : int = 
    if paramList.Length <> 2 then
        printErrorMessage ()
        0
    else
        match paramList.[0] with
            | "readfile" ->
                printfn "str : "
                let str = readText paramList.[1]
                let convertedstr = convertText str
                printfn "convertedstr : \n%A" convertedstr
                printfn "histogram : \n%A" (histogram convertedstr)
                //let newstring1 = randomString (histogram convertedstr) (convertedstr.Length)
                //printfn "new random string1 : \n%A" newstring1
                //printfn "new histogram : \n%A" (histogram newstring1)
                let strcooc = (cooccurrence convertedstr)
                printfn "str's cooc: \n%A" strcooc
                //let newcooc = (cooccurrence newstring1)
                //printfn "new random string1's cooc : \n%A" (cooccurrence newstring1)
                let newstring2 = (fstOrderMarkovModel strcooc convertedstr.Length)
                printfn "new random string2 :\n%A" newstring2
                printfn "new random string2's cooc : \n%A" (cooccurrence newstring2)
            | _ ->
                printErrorMessage ()
        1