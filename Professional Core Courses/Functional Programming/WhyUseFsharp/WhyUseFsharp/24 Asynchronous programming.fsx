/// /// /// /// /// /// Traditional asynchronous programming
open System

let userTimerWithCallback = 
    // create an event to wait on
    let event = new System.Threading.AutoResetEvent(false)

    // create a timer and add an event handler that will signal the event
    let timer = new System.Timers.Timer(2000.0)
    timer.Elapsed.Add()