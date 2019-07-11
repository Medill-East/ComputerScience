namespace myString
module histogram =
    let storyContent = "“LITTLE CLAUS AND BIG CLAUS a translation of hans christian andersen’s ’lille claus og store claus’ by jean hersholt.

    In a village there lived two men who had the self-same name. Both were named Claus. But one of them owned four horses, and the other owned only one horse; so to distinguish between them people called the man who had four horses Big Claus, and the man who had only one horse Little Claus. Now I’ll tell you what happened to these two, for this is a true story.”
    "
    let rec countchar (src:string)(letter:char): int list = 
      if letter >= 'a' && letter <= 'z' then 
        ((String.filter (fun char -> char=letter) src).Length):: (countchar src (char (int letter + 1)))
      else
        []
    let histogram (src:string): int list = (countchar src 'a')
    printfn "histogram of %A is %A" storyContent (histogram (storyContent.ToLower()))