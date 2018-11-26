## Exercises

##### Aggregate Operators
1. Count
2. LongCount
3. Sum
4. Min
5. Max
6. Average
7. Aggregate : The Aggregate operator applies a function over a sequence.   
More details: https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic16

##### Concatenation Operator 
1. Concat  
Details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic7

Conversion Operators
1. AsEnumerable : The AsEnumerable operator returns its argument typed as IEnumerable<TSource>.
2. ToArray
3. ToList
4. ToDictionary
5. ToLookup : The ToLookup operator creates a Lookup<TKey, TElement> from a sequence
6. OfType : The OfType operator filters the elements of a sequence based on a type.
7. Cast : The Cast operator casts the elements of a sequence to a given type.  
Details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic11

##### Element Operators
1. First
2. FirstOrDefault
3. Last
4. LastOrDefault
5. Single
6. SingleOrDefault
7. ElementAt
8. ElementAtOrDefault
9. DefaultIfEmpty : The DefaultIfEmpty operator supplies a default element for an empty sequence.  
Details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic13

##### Equality Operator
1. SequenceEqual: The SequenceEqual operator checks whether two sequences are equal.  
Details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic12

Generation Operators
1. Range
2. Repeat
3. Empty  
More details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic14

##### Grouping Operators
1. GroupBy  
Details: https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic9

##### Join Operators
1. Join
2. GroupJoin  
Details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic6

##### Ordering Operators
1. OrderBy and ThenBy : Operators in the OrderBy/ThenBy family of operators order a sequence according to one or more keys.  
Details: https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic8

##### Partitioning Operators
1. Take : The Take operator yields a given number of elements from a sequence and then skips the remainder of the sequence.
2. Skip : The Skip operator skips a given number of elements from a sequence and then yields the remainder of the sequence.
3. TakeWhile : The TakeWhile operator yields elements from a sequence while a test is true and then skips the remainder of the sequence.
4. SkipWhile : The SkipWhile operator skips elements from a sequence while a test is true and then yields the remainder of the sequence.  
Details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic5

##### Projection Operators
1. Select
2. SelectMany : The SelectMany operator performs a one-to-many element projection over a sequence.  
Details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic4

##### Quantifiers
1. Any
2. All
3. Contains  
More details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic15

##### Restriction Operatiors
1. Where  
Details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic3

##### Set Operators
1. Distinct : The Distinct operator eliminates duplicate elements from a sequence.
2. Union : The Union operator produces the set union of two sequences.
3. Intersect : The Intersect operator produces the set intersection of two sequences.
4. Except : The Except operator produces the set difference between two sequences.  
Details : https://msdn.microsoft.com/en-us/library/bb394939.aspx#standardqueryops_topic10

More detail:
1.  https://msdn.microsoft.com/en-us/library/bb394939.aspx
2.  https://code.msdn.microsoft.com/101-LINQ-Samples-3fb9811b