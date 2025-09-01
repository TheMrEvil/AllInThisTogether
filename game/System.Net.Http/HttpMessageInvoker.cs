using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	/// <summary>A specialty class that allows applications to call the <see cref="M:System.Net.Http.HttpMessageInvoker.SendAsync(System.Net.Http.HttpRequestMessage,System.Threading.CancellationToken)" /> method on an HTTP handler chain.</summary>
	// Token: 0x02000027 RID: 39
	public class HttpMessageInvoker : IDisposable
	{
		/// <summary>Initializes an instance of a <see cref="T:System.Net.Http.HttpMessageInvoker" /> class with a specific <see cref="T:System.Net.Http.HttpMessageHandler" />.</summary>
		/// <param name="handler">The <see cref="T:System.Net.Http.HttpMessageHandler" /> responsible for processing the HTTP response messages.</param>
		// Token: 0x06000134 RID: 308 RVA: 0x000057F1 File Offset: 0x000039F1
		public HttpMessageInvoker(HttpMessageHandler handler) : this(handler, true)
		{
		}

		/// <summary>Initializes an instance of a <see cref="T:System.Net.Http.HttpMessageInvoker" /> class with a specific <see cref="T:System.Net.Http.HttpMessageHandler" />.</summary>
		/// <param name="handler">The <see cref="T:System.Net.Http.HttpMessageHandler" /> responsible for processing the HTTP response messages.</param>
		/// <param name="disposeHandler">
		///   <see langword="true" /> if the inner handler should be disposed of by Dispose(), <see langword="false" /> if you intend to reuse the inner handler.</param>
		// Token: 0x06000135 RID: 309 RVA: 0x000057FB File Offset: 0x000039FB
		public HttpMessageInvoker(HttpMessageHandler handler, bool disposeHandler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.handler = handler;
			this.disposeHandler = disposeHandler;
		}

		/// <summary>Releases the unmanaged resources and disposes of the managed resources used by the <see cref="T:System.Net.Http.HttpMessageInvoker" />.</summary>
		// Token: 0x06000136 RID: 310 RVA: 0x0000581F File Offset: 0x00003A1F
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpMessageInvoker" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		// Token: 0x06000137 RID: 311 RVA: 0x00005828 File Offset: 0x00003A28
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.disposeHandler && this.handler != null)
			{
				this.handler.Dispose();
				this.handler = null;
			}
		}

		/// <summary>Send an HTTP request as an asynchronous operation.</summary>
		/// <param name="request">The HTTP request message to send.</param>
		/// <param name="cancellationToken">The cancellation token to cancel operation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was <see langword="null" />.</exception>
		// Token: 0x06000138 RID: 312 RVA: 0x0000584F File Offset: 0x00003A4F
		public virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return this.handler.SendAsync(request, cancellationToken);
		}

		// Token: 0x040000A9 RID: 169
		private protected HttpMessageHandler handler;

		// Token: 0x040000AA RID: 170
		private readonly bool disposeHandler;
	}
}
