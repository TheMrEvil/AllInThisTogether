using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a document referenced by a symbol store.</summary>
	// Token: 0x020009D8 RID: 2520
	[ComVisible(true)]
	public interface ISymbolDocumentWriter
	{
		/// <summary>Sets checksum information.</summary>
		/// <param name="algorithmId">The GUID representing the algorithm ID.</param>
		/// <param name="checkSum">The checksum.</param>
		// Token: 0x06005A4F RID: 23119
		void SetCheckSum(Guid algorithmId, byte[] checkSum);

		/// <summary>Stores the raw source for a document in the symbol store.</summary>
		/// <param name="source">The document source represented as unsigned bytes.</param>
		// Token: 0x06005A50 RID: 23120
		void SetSource(byte[] source);
	}
}
