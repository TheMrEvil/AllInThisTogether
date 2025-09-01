using System;
using System.Linq.Expressions;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents the runtime state of a dynamically generated method.</summary>
	// Token: 0x020002EE RID: 750
	[Obsolete("do not use this type", true)]
	public class ExecutionScope
	{
		// Token: 0x060016B8 RID: 5816 RVA: 0x0004C847 File Offset: 0x0004AA47
		internal ExecutionScope()
		{
			this.Parent = null;
			this.Globals = null;
			this.Locals = null;
		}

		/// <summary>Creates an array to store the hoisted local variables.</summary>
		/// <returns>An array to store hoisted local variables.</returns>
		// Token: 0x060016B9 RID: 5817 RVA: 0x000080E3 File Offset: 0x000062E3
		public object[] CreateHoistedLocals()
		{
			throw new NotSupportedException();
		}

		/// <summary>Creates a delegate that can be used to execute a dynamically generated method.</summary>
		/// <param name="indexLambda">The index of the object that stores information about associated lambda expression of the dynamic method.</param>
		/// <param name="locals">An array that contains the hoisted local variables from the parent context.</param>
		/// <returns>A <see cref="T:System.Delegate" /> that can execute a dynamically generated method.</returns>
		// Token: 0x060016BA RID: 5818 RVA: 0x000080E3 File Offset: 0x000062E3
		public Delegate CreateDelegate(int indexLambda, object[] locals)
		{
			throw new NotSupportedException();
		}

		/// <summary>Frees a specified expression tree of external parameter references by replacing the parameter with its current value.</summary>
		/// <param name="expression">An expression tree to free of external parameter references.</param>
		/// <param name="locals">An array that contains the hoisted local variables.</param>
		/// <returns>An expression tree that does not contain external parameter references.</returns>
		// Token: 0x060016BB RID: 5819 RVA: 0x000080E3 File Offset: 0x000062E3
		public Expression IsolateExpression(Expression expression, object[] locals)
		{
			throw new NotSupportedException();
		}

		/// <summary>Represents the execution scope of the calling delegate.</summary>
		// Token: 0x04000B65 RID: 2917
		public ExecutionScope Parent;

		/// <summary>Represents the non-trivial constants and locally executable expressions that are referenced by a dynamically generated method.</summary>
		// Token: 0x04000B66 RID: 2918
		public object[] Globals;

		/// <summary>Represents the hoisted local variables from the parent context.</summary>
		// Token: 0x04000B67 RID: 2919
		public object[] Locals;
	}
}
