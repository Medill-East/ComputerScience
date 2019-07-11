namespace myString
module convertText = 

    let testString = "aB1cdEFg09-"
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