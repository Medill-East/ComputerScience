/// Definition of a base type, for clarity throughout the code.
type VarType = string

/// The Embedded Domain Specific Language.
type Term =
    // Fill in the solution.
    | Var of VarType
    | App of Term * Term
    | Abs of VarType * Term


/// Substitution.
//let rec substitute (term : Term) (var : VarType) (sub : Term) = false
    // Fill in the solution.

let rec substitute (term: Term) (var: VarType) (sub: Term) =
    match term with
    | Var x when x = var -> sub
    | Abs(x, y) -> Abs(x, substitute y var sub)
    | App(x, y) -> App(substitute x var sub, substitute y var sub)
    | _ -> term

let mutable cnt = 0

/// The function for computing beta reduces.
let rec reduce ((term, red, cnt): Term * bool * int) =
    //// Fill in the solution.
    match term with
    //| Var x when red = true -> substitute term x (Var x)
    //| Abs (x,y) -> Abs(x, reduce(y,red))
    //| App (x,y) -> App((reduce (x,red)), (reduce (y,red)))
    //| _ -> term
    // | App (left, right) when cnt >= 100 -> App(left, right)
    // | Abs (x,y) -> Abs(x,reduce(y,red, cnt+1))
    | Var x when red = true -> Var x
    | Abs(x, y) -> 
        if cnt >= 100 then Abs(x, y) else Abs(x, reduce (y, red, cnt + 1))
    | App(left, right) -> 
        if cnt >= 100 then App(left, right) else
        match reduce (left, true, cnt + 1) with
        | Var _ -> App(left, reduce (right, red, cnt + 1))
        // | Abs(x,y) -> substitute left x y
        | App(x, y) -> App(reduce (x, red, cnt + 1), reduce (y, red, cnt + 1))
        | Abs(x, y) -> reduce ((substitute y x right), red, cnt + 1)
        // | Abs(x,y) -> reduce((substitute left x y), red, cnt+1)
    | _ -> term

/// Tries to reduce a lambda term into its normalform.
//let rec eval (term : Term) = false
    // Fill in the solution.
let rec eval (term: Term) =
    match term with
    | Var x -> Var x
    | Abs(x, y) -> Abs(x, y)
    | App(t1, t2) ->
        match eval t1 with
            //| Abs (v, t) -> eval (substitute t v t2)
        | Var _ -> t1
            // match reduce (Abs(v, t), false) with
            // | Var x' -> Var x'
            // | Abs(v', t') when v'=v && t'=t -> Abs(v, t)
            // | App (t5, t6) -> App(t5, t6)
            // | _ -> reduce (Abs(v, t), false)
        | App(t3, t4) -> App(t3, t4)
        | Abs(v, t) -> reduce (term, false, 0)

        // | t' ->
        //     match t' with
        //     | t1 ->
        //         match eval t' with
        //         | t' -> t'
        //         | Abs (v, t) -> eval (substitute t v t2)
        // | Abs (v, t) -> eval (substitute t v t2)



    // | App (f, arg) ->
    // //eval (reduce (f,true))
    //     match reduce(f, false) with
    //     | App (t3, t4) when App (t3, t4) = f -> f
    //     | Var _ -> f
    //     | App (t3, t4) -> App (t3, t4)
    //     | Abs(x,e) -> eval (substitute e x arg)
    //     | _ -> f





/// Test Programs to check if things work correctly.
let term1 = App(Var "x", Var "y")
let term2 = Abs("z", term1)
let term3 = App(Abs("x", Var "x"), Var "y")
let term4 = Abs("x", Var("x"))
let term5 = App(Abs("x", Var "x"), Var "z")
let omega = Abs("x", App(Var "x", Var "x"))
let Omega = App(omega, omega)
let term6 = App(Abs("x", Var("x")), App(Abs("x", Var "x"), Var "z"))
let term8 = App(Abs("x", App(Var "z", App(Var "x", Var "x"))), Abs("y", App(Var "z", App(Var "y", Var "y"))))

printfn ""
printfn """Your result should be: App (Var "x",Var "y")
Your result is: %A""" term1

printfn ""
printfn """Your result should be: Abs ("z",App (Var "x",Var "y"))
Your result is: %A""" term2

printfn ""
printfn """Your result should be: App (Var "x",Var "z")
Your result is: %A""" (substitute term1 "y" (Var "z"))

printfn ""
printfn """Your result should be: Var "y"
Your result is: %A""" (eval term3)

printfn ""
printfn """Your result should be: Var "y"
Your result is: %A""" (reduce (term3, false, 0))

printfn ""
printfn """Your result should be: Abs (Var "x",Var "x")
Your result is: %A""" (eval term4)

printfn ""
printfn """Your result should be: Var "z"
Your result is: %A""" (reduce (term5, false, 0))

printfn ""
printfn """Your result should be: "z"
Your result is: %A""" (eval term6)

printfn ""
printfn """Your result should be: Abs("x", App(Var "x", Var "x"))
Your result is: %A""" (reduce (omega, false, 0))

printfn ""
printfn """Your result should be: App(omega, omega)
Your result is: %A""" (reduce (Omega, false, 0))

printfn ""
printfn """Your result should be: omega
Your result is: %A""" (eval omega) //infinite loop

printfn ""
printfn """Your result should be: App(omega, omega)
Your result is: %A""" (eval Omega) //infinite loop

printfn ""
printfn """Your result should be: App(omega, omega)
Your result is: %A""" (eval term8) //infinite loop