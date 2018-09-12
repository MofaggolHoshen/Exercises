# Restriction Operators

### Where
The Where operator filters a sequence based on a predicate.

The Where operator allocates and returns an enumerable object that captures the arguments passed to the operator. An ArgumentNullException is thrown 
if either argument is null.

##### Remarks

This method is implemented by using deferred execution. The immediate return value is an object that stores all the information that is required to 
perform the action. The query represented by this method is not executed until the object is enumerated either by calling its GetEnumerator method 
directly or by using foreach in Visual C# or For Each in Visual Basic. 

More details : 
1. https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic3
2. https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/where-clause?f1url=https%3A%2F%2Fmsdn.microsoft.com%2Fquery%2Fdev15.query%3FappId%3DDev15IDEF1%26l%3DEN-US%26k%3Dk(whereclause_CSharpKeyword)%3Bk(DevLang-csharp)%26rd%3Dtrue