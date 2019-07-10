let testString = "a"

/// /// /// 7.1




/// /// /// 7.2 converts
let rec convertText (src:string) : string = 
    let lowerSRC = src.ToLower()
    match lowerSRC with
    | "" -> ""
    | elm -> 
        if ('a' <= System.Convert.ToChar(elm) && System.Convert.ToChar(elm) <= 'z')
        then 
            elm + (convertText (lowerSRC.Remove (lowerSRC.IndexOf elm)))
        else
            convertText (lowerSRC.Remove (lowerSRC.IndexOf elm))

    let processFun elm:string = 
        if ('a' <= System.Convert.ToChar(elm) && System.Convert.ToChar(elm) <= 'z')
        then 
            elm + (convertText (lowerSRC.Remove (lowerSRC.IndexOf elm)))
        else
            convertText (lowerSRC.Remove (lowerSRC.IndexOf elm))
        
printfn "convert %A to %A" testString (convertText testString)


let rec myFilter (p: 'a -> bool) (lst: 'a list) : 'a list =
  //List.filter p lst
  match lst with
  | [] -> []
  | elm::rest -> 
    if (p elm)
    then 
        [elm] @ (myFilter p rest)
    else 
        myFilter p rest

// Exercise: replace the library function here with a recursive implementation for fold!
let rec myFold (f: 'b -> 'a -> 'b) (acc: 'b) (lst: 'a list) : 'b =
  //List.fold f acc lst
  match lst with
    | [] -> acc
    | elm::rest -> 
        let result = myFold f (f acc elm) rest
        result