//immutable list
let list = [1,2,3,4]

type PersonalName = {FirstName:string; LastName:string}

// immutable person
let john = {FirstName = "John"; LastName = "Doe"}

let alice = {john with FirstName = "Alice"}

// create an immutable list
let list1 = [1;2;3;4]

// prepend to make a new list
let list2 = 0::list1

// get the last 4 of the second list
let list3 = list2.Tail

// the two lists are the identical object in memory
System.Object.ReferenceEquals(list1,list3)


