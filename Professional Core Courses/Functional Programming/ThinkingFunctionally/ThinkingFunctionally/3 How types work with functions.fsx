// function signature
// val functionName : domain -> range

let intToString x = sprintf "x is %i" x
let stringToInt y = System.Int32.Parse(y)

/// /// Primitive types
let intToFloat x = float x // "float" fn. converts ints to floats
let intToBool x = (x = 2) // true if x equals 2
let stringToString x = x + " world"

/// /// Type annotations
//let stringLength x = x.Length
let stringLength (x:string) = x.Length
let stringLengthAsInt (x:string):int = x.Length

/// /// Function types as parameters
// a function that takes other functions as parameters, or returns a 
// function, is called a higer-order function(HOF)

let evalWith5ThenAdd2 fn = fn 5 + 2 // same as fn(5) + 2
let add1 x = x + 1  // define a function of type (int -> int)
evalWith5ThenAdd2 add1  // test it 

let times3 x = x * 3    // a function of type(int -> int)
evalWith5ThenAdd2 times3

/// /// Functions as output
let adderGenerator numberToAdd = (+) numberToAdd
let add1test = adderGenerator 1
let add2test = adderGenerator 2
add1test 5 // 
add2test 5 

/// /// Using type annotations to constrain function types
let evalWith5 fn = fn 5
// add type annotation for function parameters in the same way
// as for a primitive type
let evalWith5AsInt (fn:int -> int) = fn 5
let evalWith5AsFloat (fn:int -> float) = fn 5
// specify the return type instead
let evalWith5AsString fn:string = fn 5

/// /// The "unit" type
let printInt x = printf "x is %i" x // print to console
// return unit which has one value '()'
// unit and () as somewhat like 'void' type and 'null' value
let whatIsThis = ()

/// /// Parameterless functions
let printHello = printfn "hello world"   // print to console
// val aName: type = constant
// resuable function have a unit argument
let printHelloFn () = printfn "hello world"  // print to console
printHelloFn ()

/// /// Forcing unit types with the ignore function
//do 1+1
//let something = 
    //2+2
do (1 + 1 |> ignore)    // ok
let something =
    2 + 2 |> ignore     // ok

/// /// Generic types
let onAStick x = x.ToString() + " on a stick"
// 'a -> a generic type not konwn at compile time
onAStick 22
onAStick 3.14

let concatString x y = x.ToString() + y.ToString()

let isEqual x y = (x=y)

/// /// Other types

/// /// Test your understanding of types
let testA = float 2
let testB x = float 2
let testC x = float 2 + x
let testD x = x.ToString().Length
let testE (x:float) = x.ToString().Length
let testF x = printfn "%s" x
let testG x = printfn "%f" x
let testH = 2*2 |> ignore
let testI x = 2*2 |> ignore
let testJ (x:int) = 2*2 |> ignore
let testK = "hello"
let testL () = "hello"
let testM x = x=x
let testN x = x 1
let testO x:string = x 1


