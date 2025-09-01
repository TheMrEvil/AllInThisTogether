using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	/// <summary>A type for HTTP handlers that delegate the processing of HTTP response messages to another handler, called the inner handler.</summary>
	// Token: 0x02000014 RID: 20
	public abstract class DelegatingHandler : HttpMessageHandler
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.DelegatingHandler" /> class.</summary>
		// Token: 0x060000C6 RID: 198 RVA: 0x00003BC4 File Offset: 0x00001DC4
		protected DelegatingHandler()
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.DelegatingHandler" /> class with a specific inner handler.</summary>
		/// <param name="innerHandler">The inner handler which is responsible for processing the HTTP response messages.</param>
		// Token: 0x060000C7 RID: 199 RVA: 0x00003BCC File Offset: 0x00001DCC
		protected DelegatingHandler(HttpMessageHandler innerHandler)
		{
			if (innerHandler == null)
			{
				throw new ArgumentNullException("innerHandler");
			}
			this.InnerHandler = innerHandler;
		}

		/// <summary>Gets or sets the inner handler which processes the HTTP response messages.</summary>
		/// <returns>The inner handler for HTTP response messages.</returns>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003BE9 File Offset: 0x00001DE9
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00003BF1 File Offset: 0x00001DF1
		public HttpMessageHandler InnerHandler
		{
			get
			{
				return this.handler;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("InnerHandler");
				}
				this.handler = value;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.DelegatingHandler" />, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		// Token: 0x060000CA RID: 202 RVA: 0x00003C08 File Offset: 0x00001E08
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				if (this.InnerHandler != null)
				{
					this.InnerHandler.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.</summary>
		/// <param name="request">The HTTP request message to send to the server.</param>
		/// <param name="cancellationToken">A cancellation token to cancel operation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was <see langword="null" />.</exception>
		// Token: 0x060000CB RID: 203 RVA: 0x00003C36 File Offset: 0x00001E36
		protected internal override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (this.InnerHandler == null)
			{
				throw new InvalidOperationException("The inner handler has not been assigned.");
			}
			return this.InnerHandler.SendAsync(request, cancellationToken);
		}

		// Token: 0x04000051 RID: 81
		private bool disposed;

		// Token: 0x04000052 RID: 82
		private HttpMessageHandler handler;
	}
}
