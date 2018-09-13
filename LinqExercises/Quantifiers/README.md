# Quantifiers Operators

### Any
The Any operator checks whether any element of a sequence satisfies a condition.

The Any operator enumerates the source sequence and returns true if any element satisfies the test given by the predicate. If no predicate function 
is specified, the Any operator simply returns true if the source sequence contains any elements.

##### Remarks

This method does not return any one element of a collection. Instead, it determines whether the collection contains any elements.
The enumeration of source is stopped as soon as the result can be determined.  

### All
The All operator checks whether all elements of a sequence satisfy a condition.

##### Remarks

This method does not return all the elements of a collection. Instead, it determines whether all the elements of a collection satisfy a condition.
The enumeration of source is stopped as soon as the result can be determined

### Contains
The Contains operator checks whether a sequence contains a given element.

##### Remarks

If the type of source implements ICollection<T>, the Contains method in that implementation is invoked to obtain the result. Otherwise, 
this method determines whether source contains the specified element.
Enumeration is terminated as soon as a matching element is found. 

More details : 
1. https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic15