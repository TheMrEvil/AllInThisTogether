using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Holds the public GUIDs for language vendors to be used with the symbol store.</summary>
	// Token: 0x020009E2 RID: 2530
	[ComVisible(true)]
	public class SymLanguageVendor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SymbolStore.SymLanguageVendor" /> class.</summary>
		// Token: 0x06005A8E RID: 23182 RVA: 0x0000259F File Offset: 0x0000079F
		public SymLanguageVendor()
		{
		}

		/// <summary>Specifies the GUID of the Microsoft language vendor.</summary>
		// Token: 0x040037D5 RID: 14293
		public static readonly Guid Microsoft;
	}
}
