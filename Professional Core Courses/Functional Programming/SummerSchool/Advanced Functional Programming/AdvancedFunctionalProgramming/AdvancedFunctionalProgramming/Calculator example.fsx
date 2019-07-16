/// Calculator world
/// Numbers (float)
/// Operators
///     Add
///     Minus


type ValueType = float
type Expression = 
    | Val of ValueType
    | Add of Expression * Expression
    | Minus of Expression * Expression
    | Mult of Expression * Expression
    | Divid of Expression * Expression

let expr0 = Val(10.0)
let expr1 = Add(Val 10.0,Val 10.0)
let expr2 = Add(Val 10.0, Add(Val 20.0, Val 30.0))

let rec eval expr = 
    match expr with
    | Val(x) -> x
    | Add (x,y) -> eval x + eval y 
    | Minus (x,y) -> eval x - eval y 
    | Mult (x,y) -> eval x * eval y 
    | Divid (x,y) -> eval x / eval y 

let rec simplify expr =
    match expr with 
    | Val(x) -> Val(x)
    | Add(Val 0.0, y) -> y
    | Mult(Val 1.0, y) -> y
    | Divid(x, Val 1.0) -> x
    | _ -> expr

let expr3 = Add(Val 0.0,Val 10.0)


printfn "expr0: %A \n expr1:%A \n expr2:%A" (eval expr0) (eval expr1) (eval expr2)
printfn "expr3: %A \n" (simplify expr3)
