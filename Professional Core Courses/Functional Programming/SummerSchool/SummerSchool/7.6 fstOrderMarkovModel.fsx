
//let fstOrderMarkovModel (cooc : int list list) (len:int) : string = 


    /// /// /// 7.6 step1 get the occurrence of story
  

    /// /// /// 7.6 step2 generates random string of length len whose character pairs are distruibuted
    /// /// /// according to occurrence of story

    /// /// /// 7.6 compare the result of step1 and step2

/// Reverse the order of a list of any type
let rec reverse (lst : 'a list) : 'a list =
  match lst with
    elm :: rest -> (reverse rest) @ [elm]
    | [] -> []

/// Calculate the cumulative sum of a list of integers from the first to the last element. First element is the first number in the original list, last element is the sum of all integers in the original list.
let cumSum (lst : int list list) : int list =
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

/// Generate a random character pair according to a histogram.
let rnd = System.Random() // A global object, if included in randomChar then seed point only changes slowly giving strongly correlated results in time
let randomCharPair (hist : int list list) : (char*char) =
  let cumHist = cumSum hist
  let v = rnd.Next(cumHist.[cumHist.Length-1])
  let i = reverseLookup cumHist v
  'a' + char i    



/// Generate a string of random character pairs distributed according to a histogram.
let randomStringPair (hist : int list list) (len : int) : string = 
  /// Append random character pair to a string, each distributed according to a histogram.
  let rec appendRandomStringPair (hist : int list list) (len : int) (src : string) : string = 
    if len = 0 then
      src
    else
      src + string (randomCharPair hist) |> appendRandomStringPair hist (len-1)

  appendRandomStringPair hist len ""
  

    ///// /// /// 7.4 step2 readfile
    //let str = readText "littleClausAndBigClaus.txt"
    ///// /// /// 7.4 step3 convertfile
    //let convertRes = convertText str
    ///// /// /// 7.4 step4 get histogram
    //let hist = histogram convertRes
    //let alphabet = List.init hist.Length (fun i -> 'a' + char i)
    //printfn "A histogram:\n %A" (List.zip alphabet hist)
    ///// /// /// 7.4 step5 generages a new random string
    //let ranStr = randomString hist convertRes.Length
    //printfn "A random string: %s" ranStr
    //let newHist = histogram ranStr
    //printfn "Resulting histogram:\n %A" (List.zip alphabet newHist)   


//printfn "random string of occurrence %A are %A" (cooccurrence storyContent) (fstOrderMarkovModel (cooccurrence storyContent) (storyContent.Length))

printfn "charpair %A" ("""ab""")