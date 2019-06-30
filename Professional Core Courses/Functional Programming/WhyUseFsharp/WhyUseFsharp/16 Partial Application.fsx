// define a adding function
let add x y =
    x + y

// normal use
let z = add 1 2

// one parameter
let add42 = add 42

///The result is a new function that has the “42” baked in, 
//and now takes only one parameter instead of two! 
//This technique is called “partial application”,
//and it means that, for any function, 
//you can “fix” some of the parameters and 
//leave other ones open to be filled in later.

add42 2
add42 3

//let genericLogger anyFunc input = 
   //printfn "input is %A" input   //log the input
   //let result = anyFunc input    //evaluate the function
   //printfn "result is %A" result //log the result
   //result                        //return the result

let genericLogger before after anyFunc input = 
    before input    // callback for custom behavior
    let result = anyFunc input  //evaluate the function
    after result                //callback for custom behavior
    result

let add1 input = 
    input + 1

// resue case 1
genericLogger
    (fun x -> printf "before = %i. " x) // function to call before
    (fun x -> printfn "after = %i. " x) // function to call after
    add1                                // main func
    2                                   // parameter

// resue case 2
genericLogger
    (fun x -> printf "started with = %i. " x) // different callback
    (fun x -> printfn "ended with = %i. " x) 
    add1                                      // main func
    2                                         // parameter


// leaves the final parameter open
// define a reusable function with the "callback" functions fixed
let add1WithConsoleLogging = 
    genericLogger
        (fun x -> printf "input=%i. " x) 
        (fun x -> printfn " result=%i" x)
        add1
        // last parameter NOT defined here yet!

add1WithConsoleLogging 2
add1WithConsoleLogging 3
add1WithConsoleLogging 4
[1..5] |> List.map add1WithConsoleLogging                     

// / /// /// // // The functional approach in C#
//public class GenericLoggerHelper<TInput, TResult>
//{
//    public TResult GenericLogger(
//        Action<TInput> before,
//        Action<TResult> after,
//        Func<TInput, TResult> aFunc,
//        TInput input)
//    {
//        before(input);             //callback for custom behavior         
//        var result = aFunc(input); //do the function         
//        after(result);             //callback for custom behavior         
//        return result;
//    }
//}
//And here it is in use:

//[NUnit.Framework.Test]
//public void TestGenericLogger()
//{
//    var sut = new GenericLoggerHelper<int, int>();
//    sut.GenericLogger(
//        x => Console.Write("input={0}. ", x),
//        x => Console.WriteLine(" result={0}", x),
//        x => x + 1,
//        3);
//}