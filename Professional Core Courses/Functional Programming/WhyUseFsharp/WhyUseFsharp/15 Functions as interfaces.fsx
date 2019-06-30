/// /// /// decorateor pattern in c#
//interface ICalculator 
//{
//   int Calculate(int input);
//}

//class AddingCalculator: ICalculator
//{
//   public int Calculate(int input) { return input + 1; }
//}

//class LoggingCalculator: ICalculator
//{
//   ICalculator _innerCalculator;

//   LoggingCalculator(ICalculator innerCalculator)
//   {
//      _innerCalculator = innerCalculator;
//   }

//   public int Calculate(int input) 
//   { 
//      Console.WriteLine("input is {0}", input);
//      var result  = _innerCalculator.Calculate(input);
//      Console.WriteLine("result is {0}", result);
//      return result; 
//   }
//}

let addingCalculator input = input + 1

let loggingCalculator innerCalculator input = 
    printfn "input is %A" input
    let result = innerCalculator input
    printfn "result is %A" result
    result
    // the signature of the function is the interface

/// /// /// Generic wrappers
let add1 input = 
    input + 1
let times2 input = input * 2

let genericLogger anyFunc input = 
   printfn "input is %A" input   //log the input
   let result = anyFunc input    //evaluate the function
   printfn "result is %A" result //log the result
   result                        //return the result

let add1WithLogging = genericLogger add1
let times2WithLogging = genericLogger times2

// test
add1WithLogging 3
times2WithLogging 3

[1..5] |> List.map add1WithLogging

let genericTimer anyFunc input = 
    let stopwatch = System.Diagnostics.Stopwatch()
    stopwatch.Start()
    let result = anyFunc input // evaluate the function
    printfn "elapsed ms is %A" stopwatch.ElapsedMilliseconds
    result

let add1WithTimer = genericTimer add1WithLogging

//test
add1WithTimer 3

/// /// /// The strategy pattern

type Animal(noiseMakingStrategy) = 
    member this.MakeNoise =
        noiseMakingStrategy() |> printfn "Making noise %s"

// create a cat
let meowing() = "Meow"
let cat = Animal(meowing)
cat.MakeNoise

// dog
let woofOrBark() = 
    if(System.DateTime.Now.Second % 2 = 0)
    then "Wolf"
    else "Bark"
let dog = Animal(woofOrBark)
dog.MakeNoise
dog.MakeNoise // try again a second later