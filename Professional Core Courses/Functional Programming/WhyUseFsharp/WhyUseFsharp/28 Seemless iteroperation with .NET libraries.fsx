/// //// /// /// TryParse and TryGetValue
// using an Int32
let (i1sucess, i1) = System.Int32.TryParse("123");
if i1sucess
then printfn "parsed as %i" i1
else printfn "parse failed"

let (i2success, i2) = System.Int32.TryParse("hello");
if i2success
then printfn "parsed as %i" i2
else printfn "parse failed"

//using a DateTime
let (d1sucess, d1) = System.DateTime.TryParse("1/1/1980")
let (d2sucess, d2) = System.DateTime.TryParse("hello")

//using a dictionary
let dict = new System.Collections.Generic.Dictionary<string,string>()
dict.Add("a","hello")
let (e1sucess,e1) = dict.TryGetValue("a")
let (e2sucess,e2) = dict.TryGetValue("b")

/// /// /// /// Named arguments to help type inference

//In C# (and .NET in general), you can have overloaded methods with many different
//parameters. F# can have trouble with this. For example, here is an attempt to create a

//StreamReader

//let createReader fileName = new System.IO.StreamReader (fileName)

//error FS0041: A unique overload for method 'StreamReader

//could not be determined

//The problem is that F# does not know if the argument is supposed to be a string or a stream.
//You could explicitly specify the type of the argument, but that is not the F# way!

//Instead, a nice workaround is enabled by the fact that in F#, when calling methods in .NET
//libraries, you can specify named arguments.

let createReader2 fileName = new System.IO.StreamReader(path=fileName)

//In many cases, such as the one above, just using the argument name is enough to resolve
//the type issue. And using explicit argument names can often help to make the code more
//legible anyway.
//

/// /// /// /// /// Active patterns for .NET functions
