// sum of 1 to 100
[1..100]
    |> List.sum

// sum of 1^2 to 100^2
[1..100]
    |> List.map (fun x -> x * x)
    |> List.sum

// sum of just the even numbers
[1..100]
    |> List.filter(fun x -> x%2 = 0)
    |> List.sum

// takes any list of floats and a function as an input
let myfunc (lst:float list) (f: float list -> float list) : (float list) = f lst

let addAndDivide = 
    List.map (fun x -> x + 10.25)
    >> List.map (fun x -> x/4.0)

printfn "The result of %A are %A" ([1.00..100.00]) (myfunc [1.0..100.00] addAndDivide)