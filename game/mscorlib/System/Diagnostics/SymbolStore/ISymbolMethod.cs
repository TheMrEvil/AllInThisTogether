using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a method within a symbol store.</summary>
	// Token: 0x020009D9 RID: 2521
	[ComVisible(true)]
	public interface ISymbolMethod
	{
		/// <summary>Gets the root lexical scope for the current method. This scope encloses the entire method.</summary>
		/// <returns>The root lexical scope that encloses the entire method.</returns>
		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06005A51 RID: 23121
		ISymbolScope RootScope { get; }

		/// <summary>Gets a count of the sequence points in the method.</summary>
		/// <returns>The count of the sequence points in the method.</returns>
		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06005A52 RID: 23122
		int SequencePointCount { get; }

		/// <summary>Gets the <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> containing the metadata for the current method.</summary>
		/// <returns>The metadata token for the current method.</returns>
		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06005A53 RID: 23123
		SymbolToken Token { get; }

		/// <summary>Gets the namespace that the current method is defined within.</summary>
		/// <returns>The namespace that the current method is defined within.</returns>
		// Token: 0x06005A54 RID: 23124
		ISymbolNamespace GetNamespace();

		/// <summary>Gets the Microsoft intermediate language (MSIL) offset within the method that corresponds to the specified position.</summary>
		/// <param name="document">The document for which the offset is requested.</param>
		/// <param name="line">The document line corresponding to the offset.</param>
		/// <param name="column">The document column corresponding to the offset.</param>
		/// <returns>The offset within the specified document.</returns>
		// Token: 0x06005A55 RID: 23125
		int GetOffset(ISymbolDocument document, int line, int column);

		/// <summary>Gets the parameters for the current method.</summary>
		/// <returns>The array of parameters for the current method.</returns>
		// Token: 0x06005A56 RID: 23126
		ISymbolVariable[] GetParameters();

		/// <summary>Gets an array of start and end offset pairs that correspond to the ranges of Microsoft intermediate language (MSIL) that a given position covers within this method.</summary>
		/// <param name="document">The document for which the offset is requested.</param>
		/// <param name="line">The document line corresponding to the ranges.</param>
		/// <param name="column">The document column corresponding to the ranges.</param>
		/// <returns>An array of start and end offset pairs.</returns>
		// Token: 0x06005A57 RID: 23127
		int[] GetRanges(ISymbolDocument document, int line, int column);

		/// <summary>Returns the most enclosing lexical scope when given an offset within a method.</summary>
		/// <param name="offset">The byte offset within the method of the lexical scope.</param>
		/// <returns>The most enclosing lexical scope for the given byte offset within the method.</returns>
		// Token: 0x06005A58 RID: 23128
		ISymbolScope GetScope(int offset);

		/// <summary>Gets the sequence points for the current method.</summary>
		/// <param name="offsets">The array of byte offsets from the beginning of the method for the sequence points.</param>
		/// <param name="documents">The array of documents in which the sequence points are located.</param>
		/// <param name="lines">The array of lines in the documents at which the sequence points are located.</param>
		/// <param name="columns">The array of columns in the documents at which the sequence points are located.</param>
		/// <param name="endLines">The array of lines in the documents at which the sequence points end.</param>
		/// <param name="endColumns">The array of columns in the documents at which the sequence points end.</param>
		// Token: 0x06005A59 RID: 23129
		void GetSequencePoints(int[] offsets, ISymbolDocument[] documents, int[] lines, int[] columns, int[] endLines, int[] endColumns);

		/// <summary>Gets the start and end positions for the source of the current method.</summary>
		/// <param name="docs">The starting and ending source documents.</param>
		/// <param name="lines">The starting and ending lines in the corresponding source documents.</param>
		/// <param name="columns">The starting and ending columns in the corresponding source documents.</param>
		/// <returns>
		///   <see langword="true" /> if the positions were defined; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005A5A RID: 23130
		bool GetSourceStartEnd(ISymbolDocument[] docs, int[] lines, int[] columns);
	}
}
