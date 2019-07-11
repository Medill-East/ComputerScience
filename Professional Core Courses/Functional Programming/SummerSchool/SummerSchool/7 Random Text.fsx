let testString = "aB1cdEFg09-"

/// /// /// 7.1 readfile

let filename = "readFile.fsx"
let line = 
  try
    let reader = System.IO.File.OpenText filename
    reader.ReadToEnd ()
  with
    _ -> "" // The file cannot be read, so we return an empty string
    
printfn "%s" line


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

