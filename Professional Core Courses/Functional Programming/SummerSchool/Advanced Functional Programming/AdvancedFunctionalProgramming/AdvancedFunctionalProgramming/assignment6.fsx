open System
/// Parts of the code are created by psaxton on GitHub:
/// https://gist.github.com/psaxton/c6ec6841a63bf0092304787697dc3beb

/// Abstract data type of a generic trie with one type 
/// constraint of having a comparison function.
type Trie<'T when 'T : comparison> = {
    IsTerminal: bool;               // Is true if the node is a leaf in the trie, which equals to a full word. 
    Children: Map<'T, Trie<'T>>;    // Childrens are postfixes of the trie node.
}

/// Modifies the Map map by adding or replacing a key value pair by removing and adding.
let mapAddOrReplace (key: 'K) (value: 'V) (map: Map<'K,'V>) : Map<'K,'V> =
    map
    |> Map.remove key
    |> Map.add key value 
    
/// Represents the empty trie. 
let empty = {
    IsTerminal = false;
    Children = Map.empty;
}

/// Checks if the trie is empty and returns true if that is the case, false othrwise. 
let isEmpty trie =
    Map.isEmpty trie.Children && not trie.IsTerminal 

/// Adds an item as a list of 'a to the trie. 
let rec add<'a when 'a : comparison> (branch: 'a list) (trie: Trie<'a>) : Trie<'a> =
    match branch with
    | [] ->
        { trie with IsTerminal = true; }
    | el::els ->
        let child =
            trie.Children
            |> Map.tryFind el
            |> defaultArg <| empty
            |> add els
        let children =
            trie.Children
            |> mapAddOrReplace el child
        { trie with Children = children }
     
/// Takes a list of 'a and recursively descends the trie accordingly 
/// to the list elements and returns true if it finds the right full word. 
let rec contains<'a when 'a : comparison> (branch: 'a list) (trie: Trie<'a>) : bool =
    match branch with
    | [] -> trie.IsTerminal
    | el::els ->
        Map.tryFind el trie.Children
        |> Option.exists (contains els)

/// Takes a list of 'a and recursively descends the trie accordingly 
/// to the list elements and returns the option of a trie. 
let rec find<'a when 'a : comparison> (branch: 'a list) (trie: Trie<'a>) : Trie<'a> Option =
    match branch with
    | [] -> Some(trie)
    | el::els ->
        Map.tryFind el trie.Children
        |> function
            | None -> None
            | Some v -> find els v
            
/// Converts a trie to a list of sequences of 'a. 
let rec trieToSeq<'a when 'a : comparison> (trie: Trie<'a>) : 'a list seq =
    seq {
        for KeyValue(label, trie) in trie.Children do
            if trie.IsTerminal then yield [label]
            let children = trieToSeq trie
            for child in children do
                yield label :: child
    }

/// A global object to create random values. 
let rnd = System.Random() 

/// Convert a string to a charlist
let rec stringToCharlist (input: string) : char list = 
    match input with
    | "" -> []
    | str -> str.[0]::(stringToCharlist str.[1..])    

let stringToList (input:string) : string list = 
    Array.toList(input.Split(""))

/// Convert charlist to a string
let rec charlistToString (input: char list): string = 
    match input with
    | [] -> ""
    | fst::rest -> string(fst) + charlistToString(rest)

/// Looks up a string prefix in the trie and returns an option of its (sub)trie. 
let lookup (prefix: string) (trie: Trie<char>) : Trie<char> Option = 
    // Fill in here.
    match prefix with
    | "" -> None
    | str -> find (stringToCharlist str) trie

/// Convert a Trie<char> to Seq<string>
let rec triecharToSeqString (input: Trie<char>) : string seq = 
    match input with
    //| empty -> Seq.empty
    | charList -> (charList.ToString()::(stringToList (charList.Children.ToString()))) |> Seq.ofList

/// Uses a string prefix to complete a string by using the string as a prefix in the trie to look up its children. 
let autoComplete (prefix: string) (trie: Trie<char>) : string seq = 
    // Fill in here.
    match prefix with
    | "" -> Seq.empty
    | str -> 
        try
            match (lookup str trie) with
            | Option.None -> Seq.empty
            | Some x -> Seq.map (fun x-> x.ToString()) (trieToSeq trie)
                //triecharToSeqString x
                //Seq.collect (fun a -> a ) (trieToSeq x)
        with
            | ex -> eprintf "An exception occurred with message %s" ex.Message
                    Seq.empty

/// Uses the trie to check if a word is correct by checking containment in the trie. 
let spellCheck (word: string) (trie: Trie<char>) : bool = 
    // Fill in here.
    contains (stringToCharlist word) trie

/// Picks a random word from the given trie. 
let randWord (trie: Trie<char>) : char list = 
    // Fill in here.
    let wordList = Seq.toList (trieToSeq trie)  //char list list
    //printfn "%A" wordList.[Random().Next(0,wordList.Length)]
    wordList.[Random().Next(1,wordList.Length)] // char list
    //match trie with
    ////| empty -> []
    //| x -> (stringToCharlist ((triecharToSeqString x).ToString()))

/// Generates recursively
let rec recursiveGen (length:int) (trie: Trie<char>) : string = 
    match length with
    //| 1 -> String.Empty
    | x when x > 1 -> ((randWord trie)::(stringToCharlist " ")::[(stringToCharlist (recursiveGen (length-1) trie))]).ToString()
    | 1 -> (randWord trie).ToString()
    | _ -> "Error"

/// Generates a text based on a length len and trie. The result is a string consisting of len words drawn out of the trie. 
let rec genText (len: int) (trie: Trie<char>) : string =
    // Fill in here.
    //recursiveGen len trie
    match len with
    //| 1 -> String.Empty
    //| x when x > 1 -> ((randWord trie)::(stringToCharlist " ")::[(stringToCharlist (genText (len-1) trie))]).ToString()

    //| x when x > 1 -> (randWord trie).ToString() + " " + (genText (len-1) trie)
    //| 1 -> (randWord trie).ToString()
    //| _ -> "Error"

    | x when x > 1 -> (charlistToString (randWord trie)) + " " + (genText (len-1) trie)
    | 1 -> (charlistToString (randWord trie))
    | _ -> "Error"

/// It returns the string in a try with, to take exceptions into account. 
let readText (filename : string) : string =
    try
        let reader = System.IO.File.OpenText filename
        reader.ReadToEnd ()
    with
        _ -> "" // The file cannot be read, so we return an empty string.

/// Reads the text files and returns it as a string. 
let hca = (readText "littleClausAndBigClaus.txt") 
//let hca = (readText "testtext.txt") 


/// Inserts a string txt into a Trie trie.
let insertIntoTrie (txt: char list list) (trie: Trie<char>) : Trie<char> =
    List.fold (fun acc x -> add (Seq.toList x) acc) empty txt

/// Preprocesses a text as a string into a lowered sequence of words that are transformed in a sequence of characters. 
let processTxtToSeq (txt : string) =
    txt.ToLower().Split [|' '; ','; '.'; '\010'; ';'; '?'; '!'; '"'; '\''|]
    |> Seq.toList
    |> List.map stringToCharlist
    //|> // Fill in here.
    //stringToCharlist ((txt.ToLower().Split [|' '; ','; '.'; '\010'; ';'; '?'; '!'; '"'; '\''|]).ToString())
    //|> // Fill in here.




/// Generates the trie based on HCA's text. 
let hcaTrie = insertIntoTrie (processTxtToSeq hca) empty

/// Error message in case of a failed or missed console parameter. 
let printErrorMessage () =
    printfn "Program Input should be either 'autocomplete', 'spellcheck', or 'generate text'."

/// test
//printfn "input %A in hectrie and we get: %A" "a" (lookup "a" hectrie)


/// Main entry point for execution which is triggering the tests 
/// provided as a console parameter, in the parameter list. 
[<EntryPoint>]
let main (paramList : string []) : int =
  if paramList.Length <> 1 then
    do printErrorMessage ()
    0
  else      
      let (is_ok, res) = 
        match paramList.[0] with
          | "autocomplete" ->
            let res_pos = ["st"; "ca"; "cu"]    |> List.forall (fun x->autoComplete x hcaTrie |> Seq.isEmpty |> not)
            let res_neg = ["gts"; "kns"; "iqw"] |> List.exists (fun x->autoComplete x hcaTrie |> Seq.isEmpty)
            (res_pos && res_neg, "Autocomplete.")
          | "spellcheck" ->
            let res_pos = ["standing"; "can"; "about"] |> List.forall (fun x->spellCheck x hcaTrie)
            let res_neg = ["gts"; "cfm"; "iqw"] |> List.exists (fun x->spellCheck x hcaTrie)
            (res_pos && not res_neg, "Spellcheck.")
          | "generatetext" ->
            let res_pos = (((genText 50 hcaTrie).Split [|' '|]).Length=50)
            let res_neg = (((genText 30 hcaTrie).Split [|' '|]).Length=10)
            //printf "first length:%A\n" ((genText 50 hcaTrie).Split [|' '|]).Length
            //printf "%A\n" ((genText 30 hcaTrie).Split [|' '|]).Length
            
            (res_pos && not res_neg, "Generate text.")
          | _ ->
            do printErrorMessage ()
            (false,"")
      if is_ok then
        printfn "Correct %A" res
      else
        printfn "Incorrect: %A" res
      1

