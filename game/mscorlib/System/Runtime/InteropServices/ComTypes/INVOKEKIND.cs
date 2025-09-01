using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Specifies how to invoke a function by <see langword="IDispatch::Invoke" />.</summary>
	// Token: 0x020007C5 RID: 1989
	[Flags]
	[Serializable]
	public enum INVOKEKIND
	{
		/// <summary>The member is called using a normal function invocation syntax.</summary>
		// Token: 0x04002CD8 RID: 11480
		INVOKE_FUNC = 1,
		/// <summary>The function is invoked using a normal property access syntax.</summary>
		// Token: 0x04002CD9 RID: 11481
		INVOKE_PROPERTYGET = 2,
		/// <summary>The function is invoked using a property value assignment syntax.</summary>
		// Token: 0x04002CDA RID: 11482
		INVOKE_PROPERTYPUT = 4,
		/// <summary>The function is invoked using a property reference assignment syntax.</summary>
		// Token: 0x04002CDB RID: 11483
		INVOKE_PROPERTYPUTREF = 8
	}
}
