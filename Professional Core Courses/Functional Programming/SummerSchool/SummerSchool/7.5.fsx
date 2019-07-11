//7.5
let rec countnum (src:string) (charpair:string):int =
  if src.Length >=2 then
    if src.[0..1] = charpair then
      1+(countnum src.[1..] charpair)
    else
      (countnum src.[1..] charpair)
  else
    0
let rec countcharpair (src:string) (letter1:char) (letter2:char): int list =
  if letter2 <= 'z' then
    let charpair = string letter1 + string letter2
    (countnum src charpair) :: (countcharpair src letter1 (letter2 + char 1))
  else
    []

let rec countchar (src:string) (letter1:char) : int list list = 
  if letter1 <= 'z' then
    (countcharpair src letter1 'a')::(countchar src (letter1 + char 1))
  else
    []
let cooccurrence (src:string):int list list =
  countchar src 'a'