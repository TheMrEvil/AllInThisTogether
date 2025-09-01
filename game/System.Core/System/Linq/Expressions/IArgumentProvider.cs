using System;

namespace System.Linq.Expressions
{
	/// <summary>Provides an internal interface for accessing the arguments of multiple tree nodes (DynamicExpression, ElementInit, MethodCallExpression, InvocationExpression, NewExpression, and InexExpression).  You should not use this API.  It is only public due to DLL refactoring and exists only for internal performance optimizations.This enables two optimizations which reduce the size of expression trees.  The first enables the tree nodes to hold onto an IList&lt;T&gt; instead of a ReadOnlyCollection.  IList&lt;T&gt; saves the cost of allocating the ReadOnlyCollection for each node.  The second enables creating specialized subclasses that hold onto a specific number of arguments (for example, Block2, Block3, Block4).  Therefore, these nodes avoid allocating both a ReadOnlyCollection and an array for storing their elements, saving 32 bytes per node.The expression tree nodes continue to expose the original LINQ properties of ReadOnlyCollections.  The nodes do this by re-using a field for storing both the array or an element that would normally be stored in the array.For the array case, the collection is typed to IList&lt;T&gt; instead of ReadOnlyCollection&lt;T&gt;.  When the node is initially constructed it is an array.  When the compiler accesses the members it uses this interface.  Accessing array elements promotes the array to a ReadOnlyCollection.For the object case we store the first argument in a field typed to object.  When the node is initially constructed, the field holds the Expression.  The compiler accesses arguments through this interface, and the accessor for the first argument uses Expression.ReturnObject to return the object that handles the Expression or ReadOnlyCollection case.  When the user accesses the ReadOnlyCollection, then the object field is updated to hold directly onto the ReadOnlyCollection.It is important that the Expressions consistently return the same ReadOnlyCollection.  Otherwise, the re-writer tree walker will break.  It is a breaking change from LINQ v1 to return different ReadOnlyCollections form the same Expression node.  Currently users can rely on object identity to tell if the node has changed.  Storing the ReadOnlyCollection in an overloaded field both reduces memory usage and maintains compatibility for the public API.</summary>
	// Token: 0x0200025D RID: 605
	public interface IArgumentProvider
	{
		/// <summary>Returns the argument at index, throwing if index is out of bounds.  You should not use this type.  It is only public due to assembly refactoring, and it is used internally for performance optimizations.</summary>
		/// <param name="index">The index of the argument.</param>
		/// <returns>The argument at index, throwing if index is out of bounds as <see cref="T:System.Linq.Expressions.Expression" />.</returns>
		// Token: 0x060011B8 RID: 4536
		Expression GetArgument(int index);

		/// <summary>Returns the number of arguments to the expression tree node.  You should not use this type.  It is only public due to assembly refactoring, and it is used internally for performance optimizations.</summary>
		/// <returns>The number of arguments to the expression tree node as <see cref="T:System.Int32" />.</returns>
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060011B9 RID: 4537
		int ArgumentCount { get; }
	}
}
