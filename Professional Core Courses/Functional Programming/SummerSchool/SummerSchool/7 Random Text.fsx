﻿let testString = "aB1cdEFg09-"
let storyFileName = "littleCalusAndBigClaus.txt"
let storyContent = "“LITTLE CLAUS AND BIG CLAUS a translation of hans christian andersen’s ’lille claus og store claus’ by jean hersholt.

In a village there lived two men who had the self-same name. Both were named Claus. But one of them owned four horses, and the other owned only one horse; so to distinguish between them people called the man who had four horses Big Claus, and the man who had only one horse Little Claus. Now I’ll tell you what happened to these two, for this is a true story.”
"

/// /// /// 7.1 readfile
let printErrorMessage () =
    printfn "Program Input should be either 'map n', 'fold n', or 'filter n', where 'n' is a positive integer"

let readText (filename:string) : string = 
    let line = 
        try
          let reader = System.IO.File.OpenText filename
          reader.ReadToEnd ()
        with
          _ -> "" // The file cannot be read, so we return an empty string
    printfn "%s" line
    line

//[<EntryPoint>]
    //let main (paramList : string[]) : int = 
        //if paramList.Length <> 2 then
        //    printErrorMessage ()
        //    0
        //else
            //match paramList.[0] with
            //    | "readfile" ->
            //        readText paramList.[1]
            //    | _ ->
            //        (printErrorMessage ()).ToString()
            //        //(false,[])
            //    |> ignore
            //1

/// /// /// 7.2 converts
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

printfn "convert %A to %A" testString (convertText testString)

/// /// /// /// /// /// 7.3
let rec countchar (src:string)(letter:char): int list = 
  if letter >= 'a' && letter <= 'z' then 
    ((String.filter (fun char -> char=letter) src).Length):: (countchar src (char (int letter + 1)))
  else
    []
let histogram (src:string): int list = (countchar src 'a')
//printfn "histogram of %A is %A" testString (histogram (testString.ToLower()))

/// /// /// /// 7.4 

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

/// /// /// 7.4 step2 readfile
let str = readText "littleClausAndBigClaus.txt"
/// /// /// 7.4 step3 convertfile
let convertRes = convertText str
/// /// /// 7.4 step4 get histogram
let hist = histogram convertRes
let alphabet = List.init hist.Length (fun i -> 'a' + char i)
printfn "A histogram:\n %A" (List.zip alphabet hist)
/// /// /// 7.4 step5 generages a new random string
let ranStr = randomString hist convertRes.Length
printfn "A random string: %s" ranStr
let newHist = histogram ranStr
printfn "Resulting histogram:\n %A" (List.zip alphabet newHist)


/// /// /// /// /// 7.5
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

let rec countchar (src:string) (letter1:char) : int list list = 
  if letter1 <= 'z' then
    (countcharpair src letter1 'a')::(countchar src (letter1 + char 1))
  else
    []
let cooccurrence (src:string):int list list =
  countchar src 'a'

let storyContent = "“LITTLE CLAUS AND BIG CLAUS a translation of hans christian andersen’s ’lille claus og store claus’ by jean hersholt.

In a village there lived two men who had the self-same name. Both were named Claus. But one of them owned four horses, and the other owned only one horse; so to distinguish between them people called the man who had four horses Big Claus, and the man who had only one horse Little Claus. Now I’ll tell you what happened to these two, for this is a true story.”
"
printfn "occurence of %A is %A" storyContent (cooccurrence storyContent)


/// /// /// /// /// 7.6 fstOrderMarkovModel

/// /// /// /// /// 1 convert cooccurrence from int list list to int list = cooccurrenceList

/// /// /// /// /// 2 make cooccurrence as argument of randomList histogram


    /// /// /// 7.6 step1 get the occurrence of story
  

    /// /// /// 7.6 step2 generates random string of length len whose character pairs are distruibuted
    /// /// /// according to occurrence of story

    /// /// /// 7.6 compare the result of step1 and step2


let storyContent = "“LITTLE CLAUS AND BIG CLAUS a translation of hans christian andersen’s ’lille claus og store claus’ by jean hersholt.

In a village there lived two men who had the self-same name. Both were named Claus. But one of them owned four horses, and the other owned only one horse; so to distinguish between them people called the man who had four horses Big Claus, and the man who had only one horse Little Claus. Now I’ll tell you what happened to these two, for this is a true story.”
"

printfn "former cooc : \n %A" (cooccurrence storyContent)

let strLengthFunc (charSum:int) (cooccurSum:int) (targetLength:int) = (charSum/cooccurSum) * targetLength
let rec strLengthList (lst:int list list) : (int list) = List.map (List.sum) (lst)
let strLengthRatio (lst:int list) (lengthSum:int) : (int list) = List.map (fun x -> x/lengthSum) lst

printfn "strLength: %A" (strLengthList (cooccurrence storyContent))
printfn "strLengthSum: %A" (List.sum (strLengthList (cooccurrence storyContent)))
printfn "strLengthRatio: %A" (strLengthRatio (strLengthList (cooccurrence storyContent)) (List.sum (strLengthList (cooccurrence storyContent))))



let randomStringAlt (hist: int list):string = randomString hist (storyContent.Length/26)
let strlist = List.map (randomStringAlt) (cooccurrence storyContent)
//printfn "%A" strlist
let composestr (acc:string) (elm:string):string = acc + elm
let randomstr = List.fold composestr "" strlist
printfn "present cooc : \n%A" (cooccurrence randomstr)
printfn "%A" randomstr

let fstOrderMarkovModel (cooc:int list list) (len:int) : string =
    let randomStringAlt (hist: int list):string = randomString hist len
    List.fold composestr "" (List.map (randomStringAlt) cooc)

printfn "test: \n %A" (fstOrderMarkovModel (cooccurrence randomstr) (randomstr.Length/26))