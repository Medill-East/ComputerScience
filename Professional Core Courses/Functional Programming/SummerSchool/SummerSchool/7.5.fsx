//7.5
let rec countcharpair2 (src:string) (charpair:string):int =
  if src.Length >=2 then
    if src.[0..1] = charpair then
      1+(countcharpair2 src.[2..] charpair)
    else
      (countcharpair2 src.[2..] charpair)
  else
    0
let rec countcharpair1 (src:string) (letter1:char) (letter2:char): int list =
  if letter2 <= 'z' then
    let charpair = string letter1 + string letter2
    (countcharpair2 src charpair) :: (countcharpair1 src letter1 (letter2 + char 1))
  else
    []
let countchar2 (src:string) (letter1:char) : int list = 
  countcharpair1 src letter1 'a'
let rec countchar1 (src:string) (letter1:char) : int list list = 
  if letter1 <= 'z' then
    (countchar2 src letter1)::(countchar1 src (letter1 + char 1))
  else
    []
let cooccurrence (src:string):int list list =
  countchar1 src 'a'