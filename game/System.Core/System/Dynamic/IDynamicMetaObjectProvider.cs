using System;
using System.Linq.Expressions;

namespace System.Dynamic
{
	/// <summary>Represents a dynamic object, that can have its operations bound at runtime.</summary>
	// Token: 0x0200031B RID: 795
	public interface IDynamicMetaObjectProvider
	{
		/// <summary>Returns the <see cref="T:System.Dynamic.DynamicMetaObject" /> responsible for binding operations performed on this object.</summary>
		/// <param name="parameter">The expression tree representation of the runtime value.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> to bind this object.</returns>
		// Token: 0x060017F7 RID: 6135
		DynamicMetaObject GetMetaObject(Expression parameter);
	}
}
