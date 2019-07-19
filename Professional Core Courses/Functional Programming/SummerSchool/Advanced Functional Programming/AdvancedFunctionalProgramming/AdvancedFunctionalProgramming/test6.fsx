let rec stringToCharlist (input: string) : char list = 
    match input with
    | "" -> []
    | str -> str.[0]::(stringToCharlist str.[1..])

stringToCharlist " "