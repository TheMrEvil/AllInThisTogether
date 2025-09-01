using System;
using System.Linq.Expressions;

namespace System.Linq
{
	/// <summary>Represents an expression tree and provides functionality to execute the expression tree after rewriting it.</summary>
	// Token: 0x0200008C RID: 140
	public abstract class EnumerableExecutor
	{
		// Token: 0x0600040A RID: 1034
		internal abstract object ExecuteBoxed();

		// Token: 0x0600040B RID: 1035 RVA: 0x0000BDCF File Offset: 0x00009FCF
		internal static EnumerableExecutor Create(Expression expression)
		{
			return (EnumerableExecutor)Activator.CreateInstance(typeof(EnumerableExecutor<>).MakeGenericType(new Type[]
			{
				expression.Type
			}), new object[]
			{
				expression
			});
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Linq.EnumerableExecutor" /> class.</summary>
		// Token: 0x0600040C RID: 1036 RVA: 0x00002162 File Offset: 0x00000362
		protected EnumerableExecutor()
		{
		}
	}
}
