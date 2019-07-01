/// /// /// /// /// /// Using standard type checking

// define a "safe" email address type

type EmailAddress = EmailAddress of string

// define a function that uses it
let sendEmail(EmailAddress email) = 
    printfn "sent an email to %s" email

// try to send one
let aliceEmail = EmailAddress "alice@example.com"
sendEmail aliceEmail

// try to send a plain string
//sendEmail "bob@example.com"  // error

/// /// /// /// /// /// /// Additional type safety features in F#
/// Type-safe formatting with printf
//let printingExample = 
//   printf "an int %i" 2                        // ok
//   printf "an int %i" 2.0                      // wrong type
//   printf "an int %i" "hello"                  // wrong type
//   printf "an int %i"                          // missing param

//   printf "a string %s" "hello"                // ok
//   printf "a string %s" 2                      // wrong type
//   printf "a string %s"                        // missing param
//   printf "a string %s" "he" "lo"              // too many params

//   printf "an int %i and string %s" 2 "hello"  // ok
//   printf "an int %i and string %s" "hello" 2  // wrong type
//   printf "an int %i and string %s" 2          // missing param

let printAString x = printf "%s" x
let printAnInt x = printf "%i" x

// the result is:
// val printAString : string -> unit //takes a string parameter
// val printAnInt : int -> unit //takes an int parameter

/// /// // /// /// Units of measure
// define some measures
[<Measure>]
type cm

[<Measure>]
type inches

[<Measure>]
type feet = 
    // add a conversion function
    static member toInches(feet : float<feet>) : float<inches> = 
        feet * 12.0<inches/feet>

// define some values
let meter = 100.0<cm>
let yard = 3.0<feet>

// convert to different measure
let yardInInches = feet.toInches(yard)

// can't mix and match!
//yard  + meter

// now define some currencies
[<Measure>]
type GBP

[<Measure>]
type USD

let gbp10 = 10.0<GBP>
let usd10 = 10.0<USD>
gbp10 + gbp10             // allowed: same currency
//gbp10 + usd10             // not allowed: different currency
//gbp10 + 1.0               // not allowed: didn't specify a currency
gbp10 + 1.0<_>            // allowed using wildcard

/// /// /// /// /// /// /// Type-safe equality
//open System
//let obj = new Object()
//let ex = new Exception()
//let b = (obj = ex)

// deny comparison
[<NoEquality; NoComparison>]
type CustomerAccount = {CustomerAccountId:int}

let x = {CustomerAccountId = 1}

//x = x
//x.CustomerAccountId = x.CustomerAccountId

//x = x       // error!
//x.CustomerAccountId = x.CustomerAccountId // no error