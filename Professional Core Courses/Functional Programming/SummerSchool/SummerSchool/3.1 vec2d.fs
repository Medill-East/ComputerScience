//A 2 dimensional vector library.
//Vectors are represented as pairs of floats
module vec2d

//The length of a vector
//val len : float * float -> float
let len ((x,y):float*float) = sqrt (x*x + y*y)


//The angle of a vector
//val ang : float * float -> float

let ang ((x,y):float*float) : float = atan2 y x


//Multiplication of a float and a vector
//val scale : float -> float * float -> float * float
let scale (n:float) ((x,y):float*float) : (float*float) = (n*x,n*y)


//Addition of two vectors
//val add : float * float -> float * float -> float * float
let add (((x1,y1):float*float)) (((x2,y2):float*float)) : (float*float) = (x1+y1,x2+y2)


//Dot product of two vectors
//val dot : float * float -> float * float -> float
let dot (((x1,y1):float*float)) (((x2,y2):float*float)) : float = x1*x2+y1*y2
