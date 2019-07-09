/// /// /// 5.1 
let target = 1
let list = [1;1;1;2;3;4]
let mutable (n:int) = 0;
let multiplicity (x:int) (xs:int list) : int = 
    for i = 0 to xs.Length - 1 do
        if x = xs.[i]   
            then n <- n + 1
        else printf ""
    n
printfn "list %A has %A target %A" list (multiplicity target list) target