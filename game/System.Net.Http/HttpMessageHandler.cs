using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	/// <summary>A base type for HTTP message handlers.</summary>
	// Token: 0x02000026 RID: 38
	public abstract class HttpMessageHandler : IDisposable
	{
		/// <summary>Releases the unmanaged resources and disposes of the managed resources used by the <see cref="T:System.Net.Http.HttpMessageHandler" />.</summary>
		// Token: 0x06000130 RID: 304 RVA: 0x000057E6 File Offset: 0x000039E6
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpMessageHandler" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		// Token: 0x06000131 RID: 305 RVA: 0x000057EF File Offset: 0x000039EF
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Send an HTTP request as an asynchronous operation.</summary>
		/// <param name="request">The HTTP request message to send.</param>
		/// <param name="cancellationToken">The cancellation token to cancel operation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was <see langword="null" />.</exception>
		// Token: 0x06000132 RID: 306
		protected internal abstract Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpMessageHandler" /> class.</summary>
		// Token: 0x06000133 RID: 307 RVA: 0x000022B8 File Offset: 0x000004B8
		protected HttpMessageHandler()
		{
		}
	}
}
