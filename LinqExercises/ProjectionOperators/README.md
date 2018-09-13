# Projection Operators

### Select
The Select operator performs a projection over a sequence.

The Any operator enumerates the source sequence and returns true if any element satisfies the test given by the predicate. If no predicate
 function is specified, the Any operator simply returns true if the source sequence contains any elements.

### SelectMany
The SelectMany operator performs a one-to-many element projection over a sequence.

Projects each element of a sequence to an System.Collections.Generic.IEnumerable`1 flattens the resulting sequences into one sequence

##### Remarks

This methods are implemented by using deferred execution. The immediate return value is an object that stores all the information that is 
required to perform the action. The query represented by this method is not executed until the object is enumerated either by calling its 
GetEnumerator method directly or by using foreach in Visual C# or For Each in Visual Basic.  

More details : 
1. https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic4