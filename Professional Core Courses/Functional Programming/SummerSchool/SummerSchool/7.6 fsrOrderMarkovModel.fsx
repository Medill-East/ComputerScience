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
let a = "LITTLECLAUSANDBIGCLAUSatranslationofhanschristianandersen’s’lilleclaus og store claus’ by jean hersholt. In a village there lived two men who had the self-same name. Both were named Claus. Butoneofthemownedfourhorses,andtheotherownedonlyonehorse;sotodistinguish between them people called the man who had four horses Big Claus, and the man who had only one horse Little Claus. Now I’ll tell you what happened to these two, for this is a true story."
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
printfn "former cooc : \n %A" (cooccurrence a)

let strLengthFunc (charSum:int) (cooccurSum:int) (targetLength:int) = (charSum/cooccurSum) * targetLength
let rec strLengthList (lst:int list list) : (int list) = List.map (List.sum) (lst)
let strLengthRatio (lst:int list) (lengthSum:int) : (int list) = List.map (fun x -> x/lengthSum) lst

printfn "strLength: %A" (strLengthList (cooccurrence a))
printfn "strLengthSum: %A" (List.sum (strLengthList (cooccurrence a)))

printfn "strLengthRatio: %A" (strLengthRatio (strLengthList (cooccurrence a)) (List.sum (strLengthList (cooccurrence a))))



let randomStringAlt (hist: int list):string = randomString hist (a.Length/26)
let strlist = List.map (randomStringAlt) (cooccurrence a)
//printfn "%A" strlist
let composestr (acc:string) (elm:string):string = acc + elm
let randomstr = List.fold composestr "" strlist
printfn "present cooc : \n%A" (cooccurrence randomstr)
printfn "%A" randomstr

let fstOrderMarkovModel (cooc:int list list) (len:int) : string =
    let randomStringAlt (hist: int list):string = randomString hist len
    List.fold composestr "" (List.map (randomStringAlt) cooc)

printfn "test: \n %A" (fstOrderMarkovModel (cooccurrence randomstr) (randomstr.Length/26))