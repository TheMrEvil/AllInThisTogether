using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a variable within a symbol store.</summary>
	// Token: 0x020009DD RID: 2525
	[ComVisible(true)]
	public interface ISymbolVariable
	{
		/// <summary>Gets the first address of a variable.</summary>
		/// <returns>The first address of the variable.</returns>
		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06005A6F RID: 23151
		int AddressField1 { get; }

		/// <summary>Gets the second address of a variable.</summary>
		/// <returns>The second address of the variable.</returns>
		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06005A70 RID: 23152
		int AddressField2 { get; }

		/// <summary>Gets the third address of a variable.</summary>
		/// <returns>The third address of the variable.</returns>
		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06005A71 RID: 23153
		int AddressField3 { get; }

		/// <summary>Gets the <see cref="T:System.Diagnostics.SymbolStore.SymAddressKind" /> value describing the type of the address.</summary>
		/// <returns>The type of the address. One of the <see cref="T:System.Diagnostics.SymbolStore.SymAddressKind" /> values.</returns>
		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06005A72 RID: 23154
		SymAddressKind AddressKind { get; }

		/// <summary>Gets the attributes of the variable.</summary>
		/// <returns>The variable attributes.</returns>
		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06005A73 RID: 23155
		object Attributes { get; }

		/// <summary>Gets the end offset of a variable within the scope of the variable.</summary>
		/// <returns>The end offset of the variable.</returns>
		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06005A74 RID: 23156
		int EndOffset { get; }

		/// <summary>Gets the name of the variable.</summary>
		/// <returns>The name of the variable.</returns>
		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06005A75 RID: 23157
		string Name { get; }

		/// <summary>Gets the start offset of the variable within the scope of the variable.</summary>
		/// <returns>The start offset of the variable.</returns>
		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06005A76 RID: 23158
		int StartOffset { get; }

		/// <summary>Gets the variable signature.</summary>
		/// <returns>The variable signature as an opaque blob.</returns>
		// Token: 0x06005A77 RID: 23159
		byte[] GetSignature();
	}
}
