//public IEnumerable<TSource> Where<TSource>(
//    IEnumerable<TSource> source,
//    Func<TSource, bool> predicate
//    )
//{
//    //use the standard LINQ implementation     return source.Where(predicate);
//}

//public IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(
//    IEnumerable<TSource> source,
//    Func<TSource, TKey> keySelector
//    )
//{
//    //use the standard LINQ implementation     return source.GroupBy(keySelector);
//}



let Where source predicate = 
    //use the standard F# implementation
    Seq.filter predicate source

let GroupBy source keySelector = 
    //use the standard F# implementation
    Seq.groupBy keySelector source


let i  = 1
let s = "hello"
let tuple  = s,i      // pack into tuple  
let s2,i2  = tuple    // unpack
let list = [s2]       // type is string list


let sumLengths strList = 
    strList |> List.map String.length |> List.sum

// function type is: string list -> int