

let str = "abcz"
printfn "A string: %s" str

let str = myString.readfile.readText "littleClausAndBigClaus.txt"
let hist = myString.histogram.histogram str
let alphabet = List.init hist.Length (fun i -> 'a' + char i)
printfn "A histogram:\n %A" (List.zip alphabet hist)
    