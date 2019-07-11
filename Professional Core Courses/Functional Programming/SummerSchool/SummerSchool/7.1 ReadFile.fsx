namespace myString
module readfile = 

    let printErrorMessage () =
      printfn "Program Input should be either 'map n', 'fold n', or 'filter n', where 'n' is a positive integer"


    //[<EntryPoint>]
    //let main (paramList : string[]) : string = 
        //if paramList.Length <> 2 then
        //   printErrorMessage
        //   0
        //else
            ////let n = int paramList.[1]
            //match paramList.[0] with
                //| "readfile" ->
                //    let line = 
                //      try
                //        let reader = System.IO.File.OpenText paramList.[1]
                //        reader.ReadToEnd ()
                //      with
                //        _ -> "" // The file cannot be read, so we return an empty string
                //    printfn "the content of the file %A is %A" paramList.[1] line
                //    1
                //| _ ->
                    //printErrorMessage
                    //0





            //let n = int paramList.[1]
            //if n < 0 then
            //    printErrorMessage
            //    0
            //else
            //    let (is_ok, res) = 
            //      match paramList.[0] with
            //        | "readfile" ->
            //            let filename = "readFile.fsx"
            //            let line = 
            //              try
            //                let reader = System.IO.File.OpenText paramList.[1]
            //                reader.ReadToEnd ()
            //              with
            //                _ -> "" // The file cannot be read, so we return an empty string

            //            printfn "%s" line
            //          // printfn "Result of 'map (*2) %A': %A\n" lst map_res
            //        | _ ->
            //          printErrorMessage ()
            //          (false,[])
            //if is_ok then
            //      printfn "Result is CORRECT  : %A" res
            //else
            //      printfn "Result is INCORRECT: %A" res
            //1






    //let main (filename:string) : string = 
    ////let filename = "readFile.fsx"
        //let line = 
        //  try
        //    let reader = System.IO.File.OpenText filename
        //    reader.ReadToEnd ()
        //  with
        //    _ -> "" // The file cannot be read, so we return an empty string

        //printfn "%A" line

    //let filename = "readFile.txt"
    //let line = 
    //  try
    //    let reader = System.IO.File.OpenText filename
    //    reader.ReadToEnd ()
    //  with
    //    _ -> "" // The file cannot be read, so we return an empty string

    //printfn "%s" line

    let readText (filename:string) : string = 
        let line = 
            try
              let reader = System.IO.File.OpenText filename
              reader.ReadToEnd ()
            with
              _ -> "" // The file cannot be read, so we return an empty string
        printfn "%s" line
        line


    [<EntryPoint>]
    let main (paramList : string[]) : int = 
        if paramList.Length <> 2 then
            printErrorMessage ()
            0
        else
            match paramList.[0] with
                | "readfile" ->
                    readText paramList.[1]
                | _ ->
                    (printErrorMessage ()).ToString()
                    //(false,[])
                |> ignore
            ////let (is_ok, res) = 
            //    match paramList.[0] with
            //        | "readfile" ->
            //            readText paramList.[1]
            //        | _ ->
            //            printErrorMessage ()
            //            (false,[])
            ////if is_ok then
            ////    printfn ""
            ////else
            1