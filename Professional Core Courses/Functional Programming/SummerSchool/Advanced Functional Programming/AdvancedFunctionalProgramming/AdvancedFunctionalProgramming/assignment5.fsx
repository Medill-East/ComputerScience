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

let rec substitute (term : Term) (var : VarType) (sub : Term) = 
    match term with
    | Var x when x = var -> sub
    | Abs (x,y) -> Abs (x, substitute y var sub)
    | App (x,y) -> App (substitute x var sub, substitute y var sub)
    | _ -> term
    

/// The function for computing beta reduces. 
let rec reduce ((term,red) : (Term * bool)) = 
    //// Fill in the solution. 
    //if red = true then
        match term with
        //| Var x when red = true -> substitute term x (Var x)
        //| Abs (x,y) -> Abs(x, reduce(y,red))
        //| App (x,y) -> App((reduce (x,red)), (reduce (y,red)))
        //| _ -> term

        | Var x when red = true -> Var x
        | Abs (x,y) -> Abs(x,reduce(y,red))
        | App (left,right) -> 
            match left with
            | Var x -> App(left,reduce(right,red))
            | Abs(x,y) -> substitute y x right
            | App(x,y) -> App(reduce (x,red), reduce(y,red))
        | _ -> term
    //else
        //term
     
/// Tries to reduce a lambda term into its normalform. 
//let rec eval (term : Term) = false
    // Fill in the solution. 
let rec eval (term : Term) = 
    match term with
    //| Var x -> reduce(Var x,true)
    ////Var x
    //| Abs(x,y) -> eval y
    //| App (f, arg) -> 
        //match f with 
        //| Abs(x,e) -> eval (substitute e x arg)

    | Var x -> reduce(Var x,true)
        //Var x
    | Abs(x,y) -> Abs(x,(eval y))
    | App (Abs(left,right), arg) -> 
        let judg :bool = (Abs(left,right) < (substitute right left arg))
        eval (reduce (term,judg))
        //match f with 
        //| Abs(x,e) -> eval (substitute e x arg) 
            //| Abs(x,e) -> eval (reduce (f,false))    
    | _ -> term
    //| App(App(Abs(left1,right1),Abs(left2,right2)),Abs(left3,right3)) ->
        //eval(substitute (Abs(left1,right1)) right1 (Abs(left2,right2)) >> 
            //substitute (Abs(left1,right1)) right1 (Abs(left2,right2)) >> 


    
/// Test Programs to check if things work correctly. 
let term1 = App (Var "x", Var "y")
let term2 = Abs ("z", term1)
let term3 = App (Abs ("x", Var "x"), Var "y")
let term4 = App (Abs ("x", Var "x"), Var "z")
let term5 = App (Abs ("x", App(Var"x",Var"x")), Abs("x",App(Var"x",Var"x")))
let term6 = App (App (Abs ("xy", Var "x"), Abs("x",Var"x")),Abs("x",Var"xx"))
let term7 = Var "x"
let term8 = Abs ("x", Var"y")




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
printfn """Your result should be: Var "z"
Your result is: %A""" (eval term4)
printfn ""
printfn """Your result should be: Var "x"
Your result is: %A""" (eval term7)
printfn ""
printfn """Your result should be: Abs ("x", Var "y")
Your result is: %A""" (eval term8)




//printfn ""
//printfn """Your result should be: App (Abs ("x", App(Var"x",Var"x")), Abs("x",App(Var"x",Var"x")))
//Your result is: %A""" (eval term5)
//printfn ""
//printfn """Your result should be: Abs ("x", Var "x")
//Your result is: %A""" (eval term6)
