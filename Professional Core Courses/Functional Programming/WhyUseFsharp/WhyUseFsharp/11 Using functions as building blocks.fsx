// building blocks
let add2 x = x + 2
let mult3 x = x * 3
let square x = x * x

// test
[1..10] |> List.map add2 |> printfn "%A"
[1..10] |> List.map mult3 |> printfn "%A"
[1..10] |> List.map square |> printfn "%A" 

// new composed functions
let add2ThenMult3 = add2 >> mult3
let mult3ThenSquare = mult3 >> square 

// parameter x dosen't mean anything
let add2ThenMult3_2 x = mult3 (add2 x)
let mult3ThenSquare_2 x = square (mult3 x)

// test
add2ThenMult3 5
mult3ThenSquare 9
[1..10] |> List.map add2ThenMult3 |> printfn "%A"
[1..10] |> List.map mult3ThenSquare |> printfn "%A" 


///

// helper functions;
let logMsg msg x = printf "%s%i" msg x; x     //without linefeed 
let logMsgN msg x = printfn "%s%i" msg x; x   //with linefeed

// new composed function with new improved logging!
let mult3ThenSquareLogged = 
   logMsg "before=" 
   >> mult3 
   >> logMsg " after mult3=" 
   >> square
   >> logMsgN " result=" 

// test
mult3ThenSquareLogged 9
[1..10] |> List.map mult3ThenSquareLogged //apply to a whole list


///

let listOfFunctions = [
   mult3; 
   square;
   add2;
   logMsgN "result=";
   ]

// compose all functions in the list into a single one
let allFunctions = List.reduce (>>) listOfFunctions 

//test
allFunctions 5

////////////////////////////////////////// Mini languages
// set up the vocabulary
type DateScale = Hour | Hours | Day | Days | Week | Weeks
type DateDirection = Ago | Hence

// define a function that matches on the vocabulary
let getDate interval scale direction =
    let absHours = match scale with
                   | Hour | Hours -> 1 * interval
                   | Day | Days -> 24 * interval
                   | Week | Weeks -> 24 * 7 * interval
    let signedHours = match direction with
                      | Ago -> -1 * absHours 
                      | Hence ->  absHours 
    System.DateTime.Now.AddHours(float signedHours)

// test some examples
let example1 = getDate 5 Days Ago
let example2 = getDate 1 Hour Hence

// the C# equivalent would probably be more like this:
// getDate().Interval(5).Days().Ago()
// getDate().Interval(1).Hour().Hence()

//////////////////////////////////// drawing program
//FluentShape.Default
   //.SetColor("red")
   //.SetLabel("box")
   //.OnClick( s => Console.Write("clicked") );

// create an underlying type
type FluentShape = {
    label : string; 
    color : string; 
    onClick : FluentShape -> FluentShape // a function type
    }  

let defaultShape = {label = ""; color = ""; onClick = fun shape -> shape}

let click shape = shape.onClick shape

let display shape = 
    printfn "My label = %s and my color = %s" shape.label shape.color
    shape

let setLabel label shape = {shape with FluentShape.label = label}
let setColor color shape = {shape with FluentShape.color = color}
let appendClickAction action shape = 
    {shape with FluentShape.onClick = shape.onClick >> action}

// Compose two "base" functions to make a compound function.
let setRedBox = setColor "red" >> setLabel "box" 

// Create another function by composing with previous function.
// It overrides the color value but leaves the label alone.
let setBlueBox = setRedBox >> setColor "blue"  

// Make a special case of appendClickAction
let changeColorOnClick color = appendClickAction (setColor color)   

//setup some test values
let redBox = defaultShape |> setRedBox
let blueBox = defaultShape |> setBlueBox 

// create a shape that changes color when clicked
redBox 
    |> display
    |> changeColorOnClick "green"
    |> click
    |> display  // new version after the click

// create a shape that changes label and color when clicked
blueBox 
    |> display
    |> appendClickAction (setLabel "box2" >> setColor "green")  
    |> click
    |> display  // new version after the click

let rainbow = 
    ["red";"orange";"yellow";"green";"blue";"indigo";"violet"]

let showRainbow = 
    let setColorAndDisplay color = setColor color >> display
    rainbow
    |> List.map setColorAndDisplay
    |> List.reduce (>>)

// test the showRainbow function
defaultShape |> showRainbow