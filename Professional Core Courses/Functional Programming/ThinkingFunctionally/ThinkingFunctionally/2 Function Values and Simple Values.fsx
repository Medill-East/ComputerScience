/// Function Values and Simple Values
// binding - using a name to represent a value
// no variables, only values

/// /// Funtion values
let add1 x = x + 1
// add1 is called a "function value"
let plus1 = add1
add1 5
plus1 5
// val functionName : domain -> range

/// /// Simple values
// only return - constant
let c = 5
// no mapping, just single int
// val aName : type = constant // no arrow

/// /// Simple values vs. funtion values
// functions are values that can be passed aroud as inputs to other functions
// subtle difference 
//  funtion - always has domain and range / must be applied to an argument to get a result
//  simple value - not need to be evaluated after being bound

// constant function
let c2 = fun() -> 5
let c3() = 5

/// /// "Values" vs. "Objects"
// f# - values - member of a domain/ immutable // not have attached behavior
// c# - objects - encapsulation of a data structure with associated behavior(method)
//                  have state(mutable)

/// /// Naming Values
A'b'c   begin'  //valid names

let f = x
let f' = derivative f
let f'' = derivative f'

let if' b t f = 
    if b
    then tan
    else f

let ``begin`` = "begin"

// use nature language for business rules 
let ``is first time customer?`` = true
let ``add gift to order`` = ()
if ``is first time customer?`` then ``add gift to order``

// Unit test
let [<Test>] ``When input is 2 then expect square is 4``=
// code here

// BDD clause
let [<Given>] ``I have (.*) N products in my cart`` (n:int) =
// code here











