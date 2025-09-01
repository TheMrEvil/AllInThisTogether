using System;

namespace System.Linq.Expressions
{
	/// <summary>Provides an internal interface for accessing the arguments of DynamicExpression tree nodes as well as CallSite and Rewriting functionality.  You should not use this API.  It is only public due to DLL refactoring and exists only for internal performance optimizations.</summary>
	// Token: 0x0200025E RID: 606
	public interface IDynamicExpression : IArgumentProvider
	{
		/// <summary>Gets the delegate type used by the CallSite, which is the type of the rules used in the dynamic expression’s polymorhphic inline cache. </summary>
		/// <returns>The delegate type used by the CallSite.</returns>
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060011BA RID: 4538
		Type DelegateType { get; }

		/// <summary>Rewrites this node replacing the dynamic expression’s arguments with the provided values.  The number of args needs to match the number of the current expression.  You should not use this type.  It is only public due to assembly refactoring, and it is used internally for performance optimizations.  This helper method allows re-writing of nodes to be independent of the specific implementation class deriving from DynamicExpression that is being used at the call site.</summary>
		/// <param name="args">The arguments used to replace this node.</param>
		/// <returns>The rewritten node, but if no changes were made, then returns the same node.</returns>
		// Token: 0x060011BB RID: 4539
		Expression Rewrite(Expression[] args);

		/// <summary>Optionally creates the CallSite and returns the CallSite for the DynamicExpression’s polymorphic inline cache.  You should not use this type.  It is only public due to assembly refactoring, and it is used internally for performance optimizations.</summary>
		/// <returns>The CallSite for the DynamicExpression’s polymorphic inline cache.</returns>
		// Token: 0x060011BC RID: 4540
		object CreateCallSite();
	}
}
