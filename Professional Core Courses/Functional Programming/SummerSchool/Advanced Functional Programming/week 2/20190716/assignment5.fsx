/// Definition of a base type, for clarity throughout the code. 
type VarType = string

/// The Embedded Domain Specific Language. 
type Term = 
    // Fill in the solution. 

/// Substitution. 
let rec substitute (term : Term) (var : VarType) (sub : Term) = false
    // Fill in the solution. 

/// The function for computing beta reduces. 
let rec reduce ((term,red) : (Term * bool)) = false
    // Fill in the solution. 

/// Tries to reduce a lambda term into its normalform. 
let rec eval (term : Term) = false
    // Fill in the solution. 

/// Test Programs to check if things work correctly. 
let term1 = App (Var "x", Var "y")
let term2 = Abs ("z", term1)
let term3 = App (Abs ("x", Var "x"), Var "y")
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
