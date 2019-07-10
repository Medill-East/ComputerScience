/// /// /// 5.1 
let target = 1
let list = [1;1;1;2;3;4]
let fulllist = [1;1;1;2;3;4]
let mutable (n:int) = 0;
// for loop
//let multiplicity (x:int) (xs:int list) : int = 
    //for i = 0 to xs.Length - 1 do
    //    if x = xs.[i]   
    //        then n <- n + 1
    //    else printf ""
    //n

// match with
let rec multiplicity (x:int) (xs:int list) : int = 
    match xs with
    | elm::rest -> 
        if elm = x
        then 
            n <- n+1
            multiplicity target rest
        else
        n
    | [] -> 0     
printfn "list %A has %A target %A" list (multiplicity target list) target

// match with better
//let rec multiplicity (x:int) (xs:int list) : int = 
//    match xs with
//    | elm::rest -> 
//        let v = if (x = elm) then 1 else 0
//        v + multiplicity x rest
//    | [] -> 0     
//printfn "list %A has %A target %A" list (multiplicity target list) target

// fold
//let rec multiplicity x xs = 
    //List.fold ( fun acc elm -> acc + (if elm = x then 1 else 0)) 0 xs

/// /// /// 5.2 filter
let evens (fullList:int list) : int list = 
    List.filter(fun x -> x % 2 = 0) fullList
printfn "evenlist of %A is %A" list (evens list)

/// /// /// 5.3 reverse
//let rev = List.rev
//let reverseApply (fullList:_) (func): _ = 
//    func fullList
//printfn "reverseList of %A is %A" list (reverseApply list rev)

// solusion
//let reverseApply x f = f x 

/// /// /// 5.4 map
//let funcList = List.init list.Length (fun i -> rev)
//let mutable resultList = []
//let applylist (listFunc: ('a -> 'b) list) (testList:'a) :'b list = 
//    //List.iter(List.map listFunc elm) funcList
//    for i = 0 to funcList.Length - 1 do 
//        let mutable tempList = List.map funcList.[i] testList
//        resultList <- resultList @ testList
//    resultList
////let applylist (listFunc: (List.map (fun x -> []) fulllist ) ) (fulllist)
//printfn "applyList of %A is %A" list (applylist (funcList) ())

// curring
let reverseApply x f = f x
let applyList lst x =
    (List.map (reverseApply x) lst)
let res = applyList [cos;sin] 3.5
printfn "applyList [cos;sin] 3.5 = %A" res

/// /// /// 5.5 concat
let aList = [[2];[6;4];[1]]
let rec concat lst = 
    match lst with
    | elm::rest -> elm @ (concat rest)
    | [] -> []
printfn "concat %A = %A" aList (concat aList)     

// fold
let resFold = List.fold (fun acc elm -> acc @ elm) [] aList

// collect
let resCollect = List.collect id aList