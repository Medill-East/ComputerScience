let main (filename : string) : string = 
    let line = 
      try
        let reader = System.IO.File.OpenText filename
        reader.ReadToEnd ()
      with
        _ -> "" // The file cannot be read, so we return an empty string
    line