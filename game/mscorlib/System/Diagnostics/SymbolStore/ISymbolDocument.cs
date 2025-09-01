using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a document referenced by a symbol store.</summary>
	// Token: 0x020009D7 RID: 2519
	[ComVisible(true)]
	public interface ISymbolDocument
	{
		/// <summary>Gets the checksum algorithm identifier.</summary>
		/// <returns>A GUID identifying the checksum algorithm. The value is all zeros, if there is no checksum.</returns>
		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06005A45 RID: 23109
		Guid CheckSumAlgorithmId { get; }

		/// <summary>Gets the type of the current document.</summary>
		/// <returns>The type of the current document.</returns>
		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06005A46 RID: 23110
		Guid DocumentType { get; }

		/// <summary>Checks whether the current document is stored in the symbol store.</summary>
		/// <returns>
		///   <see langword="true" /> if the current document is stored in the symbol store; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06005A47 RID: 23111
		bool HasEmbeddedSource { get; }

		/// <summary>Gets the language of the current document.</summary>
		/// <returns>The language of the current document.</returns>
		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06005A48 RID: 23112
		Guid Language { get; }

		/// <summary>Gets the language vendor of the current document.</summary>
		/// <returns>The language vendor of the current document.</returns>
		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06005A49 RID: 23113
		Guid LanguageVendor { get; }

		/// <summary>Gets the length, in bytes, of the embedded source.</summary>
		/// <returns>The source length of the current document.</returns>
		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06005A4A RID: 23114
		int SourceLength { get; }

		/// <summary>Gets the URL of the current document.</summary>
		/// <returns>The URL of the current document.</returns>
		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06005A4B RID: 23115
		string URL { get; }

		/// <summary>Returns the closest line that is a sequence point, given a line in the current document that might or might not be a sequence point.</summary>
		/// <param name="line">The specified line in the document.</param>
		/// <returns>The closest line that is a sequence point.</returns>
		// Token: 0x06005A4C RID: 23116
		int FindClosestLine(int line);

		/// <summary>Gets the checksum.</summary>
		/// <returns>The checksum.</returns>
		// Token: 0x06005A4D RID: 23117
		byte[] GetCheckSum();

		/// <summary>Gets the embedded document source for the specified range.</summary>
		/// <param name="startLine">The starting line in the current document.</param>
		/// <param name="startColumn">The starting column in the current document.</param>
		/// <param name="endLine">The ending line in the current document.</param>
		/// <param name="endColumn">The ending column in the current document.</param>
		/// <returns>The document source for the specified range.</returns>
		// Token: 0x06005A4E RID: 23118
		byte[] GetSourceRange(int startLine, int startColumn, int endLine, int endColumn);
	}
}
