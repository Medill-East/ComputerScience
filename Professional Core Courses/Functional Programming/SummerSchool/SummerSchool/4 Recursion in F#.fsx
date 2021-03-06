﻿/// /// /// 4.1
let rec fac (n:int) =
    if n > 1
    then n * fac(n-1)
    else
        1
let n = 5
printfn "fac %d = %d" n (fac n)                 

/// /// /// 4.2
// recursion
let rec sum (n:int) =
    if n > 1
    then n + sum(n-1)
    else
        1
let sumN = 10
printfn "sum %d = %d" sumN (sum sumN)   

//while
let mutable sumWhile = 0
let mutable nWhile = 10
while nWhile > 0 do
    sumWhile <- sumWhile + nWhile
    nWhile <- nWhile - 1
printfn "sumWhile %d = %d" sumN sumWhile

/// /// /// 4.3 Euclid's algorithm

// match with
//let rec gcd a b =
//    match b with
//        | 0 -> a
//        | n -> gcd n (a%b)

//printfn "gcd %d %d = %d" 8 2 (gcd 8 2)


// normal loop
let rec gcd a b =
    if b <> 0
    then gcd b (a%b)
    else
        a
printfn "gcd %d %d = %d" 8 2 (gcd 8 2)
printfn "gcd %d %d = %d" 2 8 (gcd 2 8)
printfn "gcd %d %d = %d" 3 7 (gcd 3 7)
printfn "gcd %d %d = %d" 21 3 (gcd 21 3)



  