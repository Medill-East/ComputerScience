/// /// /// 2.2 - listing 3
let a = 3
let b = 4
let x = 5
printfn "%A * %A + %A = %A" a x b ( a * x + b )

let y = a*x + b
printfn "%A" y

/// /// /// 2.3 - listing 4
let firstName = "Jon"
let lastName = "Sporring"
let name = firstName + " " + lastName
printfn "Hello %A!" name

printfn "Hello %A!" name

/// /// /// 2.4
let f (a:int) (b:int) (x:int) : int = a * x + b
let result = f a b x
printfn "result: %A" result

/// /// /// 2.5
// for loop
let aLop = 3
let bLop = 4
let N = 5
for i = 0 to N do
printfn "resultLop: %A" (f aLop bLop i)

// while loop
let aWhile = 3
let bWhile = 4
let mutable NWhile = 0
while NWhile < 5 do
    printfn "resultWhile: %A" (f aWhile bWhile NWhile)
    NWhile <- NWhile + 1

/// /// /// 2.6
printf "   "
let oneToTen = [1..10]
for i = 0 to 9 do
    printf "%3d " oneToTen.[i]
printfn ""    
for i = 0 to 9 do 
    printf "%3d" oneToTen.[i]
    for j = 0 to 9 do
        printf "%3d " (oneToTen.[i] * oneToTen.[j])

    printfn ""    