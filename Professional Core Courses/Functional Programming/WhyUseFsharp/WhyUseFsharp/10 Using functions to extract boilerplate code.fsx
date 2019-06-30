let product n = 
    let initialValue = 1
    let action productSoFar x = productSoFar * x
    [1..n] |> List.fold action initialValue

//test
product 10

let sumOfOdds n = 
    let initialValue = 0
    let action sumSoFar x = if x%2=0 then sumSoFar else sumSoFar+x 
    [1..n] |> List.fold action initialValue

//test
sumOfOdds 10

let alternatingSum n = 
    let initialValue = (true,0)
    let action (isNeg,sumSoFar) x = if isNeg then (false,sumSoFar-x)
                                             else (true ,sumSoFar+x)
    [1..n] |> List.fold action initialValue |> snd

//test
alternatingSum 100






let sumOfSquaresWithFold n = 
    let initialValue = 0
    let action sumSoFar x = sumSoFar + (x*x)
    [1..n] |> List.fold action initialValue 

//test
sumOfSquaresWithFold 100


///////////////////////////////// fold in c#
//public static int ProductWithAggregate(int n)
//{
//    var initialValue = 1;
//    Func<int, int, int> action = (productSoFar, x) => 
//        productSoFar * x;
//    return Enumerable.Range(1, n)
//            .Aggregate(initialValue, action);
//}

//public static int SumOfOddsWithAggregate(int n)
//{
//    var initialValue = 0;
//    Func<int, int, int> action = (sumSoFar, x) =>
//        (x % 2 == 0) ? sumSoFar : sumSoFar + x;
//    return Enumerable.Range(1, n)
//        .Aggregate(initialValue, action);
//}

//public static int AlternatingSumsWithAggregate(int n)
//{
//    var initialValue = Tuple.Create(true, 0);
//    Func<Tuple<bool, int>, int, Tuple<bool, int>> action =
//        (t, x) => t.Item1
//            ? Tuple.Create(false, t.Item2 - x)
//            : Tuple.Create(true, t.Item2 + x);
//    return Enumerable.Range(1, n)
//        .Aggregate(initialValue, action)
//        .Item2;
//}

/////////////////////////////////////////////// find max
//public class NameAndSize
//{
//    public string Name;
//    public int Size;
//}

//public static NameAndSize MaxNameAndSize(IList<NameAndSize> list)
//{
//    if (list.Count() == 0)
//    {
//        return default(NameAndSize);
//    }

//    var maxSoFar = list[0];
//    foreach (var item in list)
//    {
//        if (item.Size > maxSoFar.Size)
//        {
//            maxSoFar = item;
//        }
//    }
//    return maxSoFar;
//}

////////////////////// c# using aggreagte
//public class NameAndSize
//{
//    public string Name;
//    public int Size;
//}

//public static NameAndSize MaxNameAndSize(IList<NameAndSize> list)
//{
//    if (!list.Any())
//    {
//        return default(NameAndSize);
//    }

//    var initialValue = list[0];
//    Func<NameAndSize, NameAndSize, NameAndSize> action =
//        (maxSoFar, x) => x.Size > maxSoFar.Size ? x : maxSoFar;
//    return list.Aggregate(initialValue, action);
//}

type NameAndSize= {Name:string; Size:int}
 
let maxNameAndSize list = 
    
    let innerMaxNameAndSize initialValue rest = 
        let action maxSoFar x = if maxSoFar.Size < x.Size then x else maxSoFar
        rest |> List.fold action initialValue 

    // handle empty lists
    match list with
    | [] -> 
        None
    | first::rest -> 
        let max = innerMaxNameAndSize first rest
        Some max

//test
let list = [
    {Name="Alice"; Size=10}
    {Name="Bob"; Size=1}
    {Name="Carol"; Size=12}
    {Name="David"; Size=5}
    ]    
maxNameAndSize list
maxNameAndSize []

// use the built in function
list |> List.maxBy (fun item -> item.Size)
[] |> List.maxBy (fun item -> item.Size)

// handle emptylist
let maxNameAndSize2 list = 
    match list with
    | [] -> 
        None
    | _ -> 
        let max = list |> List.maxBy (fun item -> item.Size)
        Some max