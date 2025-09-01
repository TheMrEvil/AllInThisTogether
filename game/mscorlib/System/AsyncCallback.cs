using System;

namespace System
{
	/// <summary>References a method to be called when a corresponding asynchronous operation completes.</summary>
	/// <param name="ar">The result of the asynchronous operation.</param>
	// Token: 0x020000FC RID: 252
	// (Invoke) Token: 0x0600074E RID: 1870
	[Serializable]
	public delegate void AsyncCallback(IAsyncResult ar);
}
