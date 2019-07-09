/// /// /// 5.1 
let target = 1
let list = [1;1;1;2;3;4]
let fulllist = [1;1;1;2;3;4]
let mutable (n:int) = 0;
//let multiplicity (x:int) (xs:int list) : int = 
    //for i = 0 to xs.Length - 1 do
    //    if x = xs.[i]   
    //        then n <- n + 1
    //    else printf ""
    //n
let rec multiplicity (x:int) (xs:int list) : int = 
    match xs with
    | elm::rest -> 
        if elm = x
        then 
            n <- n+1
            multiplicity target rest
        else
        n
    | [] -> n     
printfn "list %A has %A target %A" list (multiplicity target list) target

/// /// /// 5.2 filter
let evens (fullList:int list) : int list = 
    List.filter(fun x -> x % 2 = 0) fullList
printfn "evenlist of %A is %A" list (evens list)

/// /// /// 5.3 reverse
let rev = List.rev
let reverseApply (fullList:_) (func): _ = 
    func fullList
printfn "reverseList of %A is %A" list (reverseApply list rev)

/// /// /// 5.4 map
let funcList = List.init list.Length (fun i -> rev)
let mutable resultList = []
let applylist (listFunc: (_ -> _) list) (testList:_) :_ = 
    //List.iter(List.map listFunc elm) funcList
    for i = 0 to funcList.Length - 1 do 
        let mutable tempList = List.map funcList.[i] testList
        resultList <- resultList @ testList
    resultList
//let applylist (listFunc: (List.map (fun x -> []) fulllist ) ) (fulllist)
printfn "applyList of %A is %A" list (applylist (funcList) ())

/// /// /// 5.5 concat
let aList = [[2];[6;4];[1]]
let bList = []
let rec concat lst = 
    match lst with
    | elm::rest -> elm @ (concat rest)
    | [] -> []
printfn "concat %A = %A" aList (concat aList)     