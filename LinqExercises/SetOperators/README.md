# Set Operators
### Distinct
 The Distinct operator eliminates duplicate elements from a sequence.

The Distinct operator allocates and returns an enumerable object that captures the source argument. An ArgumentNullException is thrown if the source argument is null.
When the object returned by Distinct is enumerated, it enumerates the source sequence, yielding each element that has not previously been yielded. If a non-null comparer 
argument is supplied, it is used to compare the elements. Otherwise the default equality comparer, EqualityComparer<TSource>.Default, is used.
The following example produces a sequence of all product categories:


### Union
The Union operator produces the set union of two sequences.

The Union operator allocates and returns an enumerable object that captures the arguments passed to the operator. An ArgumentNullException is thrown if any argument is null.
When the object returned by Union is enumerated, it enumerates the first and second sequences, in that order, yielding each element that has not previously been yielded. 
If a non-null comparer argument is supplied, it is used to compare the elements. Otherwise the default equality comparer, EqualityComparer<TSource>.Default, is used.

### Intersect
The Intersect operator produces the set intersection of two sequences.

The Intersect operator allocates and returns an enumerable object that captures the arguments passed to the operator. An ArgumentNullException is thrown if any argument is null.
When the object returned by Intersect is enumerated, it enumerates the first sequence, collecting all distinct elements of that sequence. It then enumerates the second sequence, marking those elements that occur in both sequences. It finally yields the marked elements in the order in which they were collected. If a non-null comparer argument is supplied, it is used to compare the elements. Otherwise the default equality comparer, EqualityComparer<TSource>.Default, is used.

### Except
The Except operator produces the set difference between two sequences.

The Except operator allocates and returns an enumerable object that captures the arguments passed to the operator. An ArgumentNullException is thrown if any argument is null.
When the object returned by Except is enumerated, it enumerates the first sequence, collecting all distinct elements of that sequence. It then enumerates the second sequence, removing those elements that were also contained in the first sequence. It finally yields the remaining elements in the order in which they were collected. If a non-null comparer argument is supplied, it is used to compare the elements. Otherwise the default equality comparer, EqualityComparer<TSource>.Default, is used.

##### Remarks

This methods are implemented by using deferred execution. The immediate return value is an object that stores all the information that is required to perform the action.
 The query represented by this method is not executed until the object is enumerated either by calling its GetEnumerator method directly or by using foreach in Visual
 C# or For Each in Visual Basic. 

More details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic10