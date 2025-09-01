using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	/// <summary>A base type for handlers which only do some small processing of request and/or response messages.</summary>
	// Token: 0x0200002C RID: 44
	public abstract class MessageProcessingHandler : DelegatingHandler
	{
		/// <summary>Creates an instance of a <see cref="T:System.Net.Http.MessageProcessingHandler" /> class.</summary>
		// Token: 0x06000172 RID: 370 RVA: 0x00005E9A File Offset: 0x0000409A
		protected MessageProcessingHandler()
		{
		}

		/// <summary>Creates an instance of a <see cref="T:System.Net.Http.MessageProcessingHandler" /> class with a specific inner handler.</summary>
		/// <param name="innerHandler">The inner handler which is responsible for processing the HTTP response messages.</param>
		// Token: 0x06000173 RID: 371 RVA: 0x00005EA2 File Offset: 0x000040A2
		protected MessageProcessingHandler(HttpMessageHandler innerHandler) : base(innerHandler)
		{
		}

		/// <summary>Performs processing on each request sent to the server.</summary>
		/// <param name="request">The HTTP request message to process.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The HTTP request message that was processed.</returns>
		// Token: 0x06000174 RID: 372
		protected abstract HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken);

		/// <summary>Perform processing on each response from the server.</summary>
		/// <param name="response">The HTTP response message to process.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The HTTP response message that was processed.</returns>
		// Token: 0x06000175 RID: 373
		protected abstract HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken);

		/// <summary>Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.</summary>
		/// <param name="request">The HTTP request message to send to the server.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was <see langword="null" />.</exception>
		// Token: 0x06000176 RID: 374 RVA: 0x00005EAC File Offset: 0x000040AC
		protected internal sealed override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			MessageProcessingHandler.<SendAsync>d__4 <SendAsync>d__;
			<SendAsync>d__.<>4__this = this;
			<SendAsync>d__.request = request;
			<SendAsync>d__.cancellationToken = cancellationToken;
			<SendAsync>d__.<>t__builder = AsyncTaskMethodBuilder<HttpResponseMessage>.Create();
			<SendAsync>d__.<>1__state = -1;
			<SendAsync>d__.<>t__builder.Start<MessageProcessingHandler.<SendAsync>d__4>(ref <SendAsync>d__);
			return <SendAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005EFF File Offset: 0x000040FF
		[DebuggerHidden]
		[CompilerGenerated]
		private Task<HttpResponseMessage> <>n__0(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return base.SendAsync(request, cancellationToken);
		}

		// Token: 0x0200002D RID: 45
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SendAsync>d__4 : IAsyncStateMachine
		{
			// Token: 0x06000178 RID: 376 RVA: 0x00005F0C File Offset: 0x0000410C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				MessageProcessingHandler messageProcessingHandler = this.<>4__this;
				HttpResponseMessage result2;
				try
				{
					ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						this.request = messageProcessingHandler.ProcessRequest(this.request, this.cancellationToken);
						awaiter = messageProcessingHandler.<>n__0(this.request, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter, MessageProcessingHandler.<SendAsync>d__4>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					HttpResponseMessage result = awaiter.GetResult();
					result2 = messageProcessingHandler.ProcessResponse(result, this.cancellationToken);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06000179 RID: 377 RVA: 0x00006004 File Offset: 0x00004204
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040000C3 RID: 195
			public int <>1__state;

			// Token: 0x040000C4 RID: 196
			public AsyncTaskMethodBuilder<HttpResponseMessage> <>t__builder;

			// Token: 0x040000C5 RID: 197
			public HttpRequestMessage request;

			// Token: 0x040000C6 RID: 198
			public MessageProcessingHandler <>4__this;

			// Token: 0x040000C7 RID: 199
			public CancellationToken cancellationToken;

			// Token: 0x040000C8 RID: 200
			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
