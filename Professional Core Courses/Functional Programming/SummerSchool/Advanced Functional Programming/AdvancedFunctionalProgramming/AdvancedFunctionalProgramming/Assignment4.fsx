/// Language specification (Grammar)
/// Definition of a base type, for clarity throughout the code. 
type ConstType = float

/// The Embedded Domain Specific Language. 
type Expr =
    | Const of ConstType        // Constant value in expressions. 
    | Add of Expr * Expr        // Addition of expressions.
    | Sub of Expr * Expr        // Subtraction of expressions.
    | Mul of Expr * Expr        // Multiplication of expressions.
    | Div of Expr * Expr        // Division of expressions.
    | Var of string         

/// Creating a Map for storing variables and their values. 
//let memory = false
    // Fill in the solution. 

let memory : Map<string,float> = Map.ofList [ ("pi", 3.14152976); ("x", 10.0) ]

/// A function to retrieve the value of key x from storage: stor. 
//let lookup (x : string) (stor : Map<string,ConstType>) = false
    // Fill in the solution. 

let findKeyFromValue findValue map =
    printfn "With value %A, found key %A." findValue (Map.findKey (fun key value -> value = findValue) map)

let lookup (x : string) (stor : Map<string,ConstType>) = 
    if (Map.containsKey x stor) then
        stor.[x]
    else
        printfn "error key"
        0.0
    
/// Interpreter of grammar.
/// Interprets an expression: expr. 
/// Storage: stor, contains keys and their values. 
let rec eval (expr : Expr) (stor : Map<string,ConstType>) = 
    match expr with
    | Const(x) -> x
    | Add(x,y) -> (eval x stor)+(eval y stor)
    | Sub(x,y) -> (eval x stor)-(eval y stor)
    | Mul(x,y) -> (eval x stor)*(eval y stor)
    | Div(x,y) -> (eval x stor)/(eval y stor)
    | Var x -> (lookup x stor)

/// Test Programs to check if things work correctly. 
let expr1 = Add (Const 5.0, Const 10.0)
let expr2 = Mul (expr1, Var("pi"))
printfn ""
printfn """Your result should be: Add (Const 5.0,Const 10.0). 
Your result is: %A""" (expr1)
printfn ""
printfn """Your result should be: Mul (Add (Const 5.0,Const 10.0),Var "pi"). 
Your result is: %A""" (expr2)
printfn ""
printfn """"Your result should be: 15.0.
Your result is: %A""" (eval expr1 memory)
printfn ""
printfn """"Your result should be: 47.1229464.
Your result is: %A""" (eval expr2 memory) 
