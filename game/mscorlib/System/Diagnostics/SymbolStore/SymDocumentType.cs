using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Holds the public GUIDs for document types to be used with the symbol store.</summary>
	// Token: 0x020009E0 RID: 2528
	[ComVisible(true)]
	public class SymDocumentType
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SymbolStore.SymDocumentType" /> class.</summary>
		// Token: 0x06005A8C RID: 23180 RVA: 0x0000259F File Offset: 0x0000079F
		public SymDocumentType()
		{
		}

		/// <summary>Specifies the GUID of the document type to be used with the symbol store.</summary>
		// Token: 0x040037C9 RID: 14281
		public static readonly Guid Text;
	}
}
