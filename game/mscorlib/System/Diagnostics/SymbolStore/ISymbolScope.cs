using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a lexical scope within <see cref="T:System.Diagnostics.SymbolStore.ISymbolMethod" />, providing access to the start and end offsets of the scope, as well as its child and parent scopes.</summary>
	// Token: 0x020009DC RID: 2524
	[ComVisible(true)]
	public interface ISymbolScope
	{
		/// <summary>Gets the end offset of the current lexical scope.</summary>
		/// <returns>The end offset of the current lexical scope.</returns>
		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06005A68 RID: 23144
		int EndOffset { get; }

		/// <summary>Gets the method that contains the current lexical scope.</summary>
		/// <returns>The method that contains the current lexical scope.</returns>
		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06005A69 RID: 23145
		ISymbolMethod Method { get; }

		/// <summary>Gets the parent lexical scope of the current scope.</summary>
		/// <returns>The parent lexical scope of the current scope.</returns>
		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06005A6A RID: 23146
		ISymbolScope Parent { get; }

		/// <summary>Gets the start offset of the current lexical scope.</summary>
		/// <returns>The start offset of the current lexical scope.</returns>
		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06005A6B RID: 23147
		int StartOffset { get; }

		/// <summary>Gets the child lexical scopes of the current lexical scope.</summary>
		/// <returns>The child lexical scopes that of the current lexical scope.</returns>
		// Token: 0x06005A6C RID: 23148
		ISymbolScope[] GetChildren();

		/// <summary>Gets the local variables within the current lexical scope.</summary>
		/// <returns>The local variables within the current lexical scope.</returns>
		// Token: 0x06005A6D RID: 23149
		ISymbolVariable[] GetLocals();

		/// <summary>Gets the namespaces that are used within the current scope.</summary>
		/// <returns>The namespaces that are used within the current scope.</returns>
		// Token: 0x06005A6E RID: 23150
		ISymbolNamespace[] GetNamespaces();
	}
}
