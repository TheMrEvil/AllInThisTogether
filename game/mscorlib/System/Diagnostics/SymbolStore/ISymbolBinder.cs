using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a symbol binder for managed code.</summary>
	// Token: 0x020009D5 RID: 2517
	[ComVisible(true)]
	public interface ISymbolBinder
	{
		/// <summary>Gets the interface of the symbol reader for the current file.</summary>
		/// <param name="importer">The metadata import interface.</param>
		/// <param name="filename">The name of the file for which the reader interface is required.</param>
		/// <param name="searchPath">The search path used to locate the symbol file.</param>
		/// <returns>The <see cref="T:System.Diagnostics.SymbolStore.ISymbolReader" /> interface that reads the debugging symbols.</returns>
		// Token: 0x06005A43 RID: 23107
		[Obsolete("This interface is not 64-bit clean.  Use ISymbolBinder1 instead")]
		ISymbolReader GetReader(int importer, string filename, string searchPath);
	}
}
