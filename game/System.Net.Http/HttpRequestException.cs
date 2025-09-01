using System;

namespace System.Net.Http
{
	/// <summary>A base class for exceptions thrown by the <see cref="T:System.Net.Http.HttpClient" /> and <see cref="T:System.Net.Http.HttpMessageHandler" /> classes.</summary>
	// Token: 0x02000029 RID: 41
	[Serializable]
	public class HttpRequestException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestException" /> class.</summary>
		// Token: 0x0600014A RID: 330 RVA: 0x0000599A File Offset: 0x00003B9A
		public HttpRequestException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestException" /> class with a specific message that describes the current exception.</summary>
		/// <param name="message">A message that describes the current exception.</param>
		// Token: 0x0600014B RID: 331 RVA: 0x000059A2 File Offset: 0x00003BA2
		public HttpRequestException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestException" /> class with a specific message that describes the current exception and an inner exception.</summary>
		/// <param name="message">A message that describes the current exception.</param>
		/// <param name="inner">The inner exception.</param>
		// Token: 0x0600014C RID: 332 RVA: 0x000059AB File Offset: 0x00003BAB
		public HttpRequestException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
