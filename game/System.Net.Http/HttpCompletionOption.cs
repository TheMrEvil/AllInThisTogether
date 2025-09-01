using System;

namespace System.Net.Http
{
	/// <summary>Indicates if <see cref="T:System.Net.Http.HttpClient" /> operations should be considered completed either as soon as a response is available, or after reading the entire response message including the content.</summary>
	// Token: 0x0200001E RID: 30
	public enum HttpCompletionOption
	{
		/// <summary>The operation should complete after reading the entire response including the content.</summary>
		// Token: 0x0400008D RID: 141
		ResponseContentRead,
		/// <summary>The operation should complete as soon as a response is available and headers are read. The content is not read yet.</summary>
		// Token: 0x0400008E RID: 142
		ResponseHeadersRead
	}
}
