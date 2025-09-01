using System;
using System.Buffers;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	/// <summary>The WebSocket class allows applications to send and receive data after the WebSocket upgrade has completed.</summary>
	// Token: 0x020007EB RID: 2027
	public abstract class WebSocket : IDisposable
	{
		/// <summary>Indicates the reason why the remote endpoint initiated the close handshake.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketCloseStatus" />.</returns>
		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x0600409D RID: 16541
		public abstract WebSocketCloseStatus? CloseStatus { get; }

		/// <summary>Allows the remote endpoint to describe the reason why the connection was closed.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x0600409E RID: 16542
		public abstract string CloseStatusDescription { get; }

		/// <summary>Gets the subprotocol that was negotiated during the opening handshake.</summary>
		/// <returns>The subprotocol that was negotiated during the opening handshake.</returns>
		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x0600409F RID: 16543
		public abstract string SubProtocol { get; }

		/// <summary>Returns the current state of the WebSocket connection.</summary>
		/// <returns>The current state of the WebSocket connection.</returns>
		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x060040A0 RID: 16544
		public abstract WebSocketState State { get; }

		/// <summary>Aborts the WebSocket connection and cancels any pending IO operations.</summary>
		// Token: 0x060040A1 RID: 16545
		public abstract void Abort();

		/// <summary>Closes the WebSocket connection as an asynchronous operation using the close handshake defined in the WebSocket protocol specification section 7.</summary>
		/// <param name="closeStatus">Indicates the reason for closing the WebSocket connection.</param>
		/// <param name="statusDescription">Specifies a human readable explanation as to why the connection is closed.</param>
		/// <param name="cancellationToken">The token that can be used to propagate notification that operations should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x060040A2 RID: 16546
		public abstract Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken);

		/// <summary>Initiates or completes the close handshake defined in the WebSocket protocol specification section 7.</summary>
		/// <param name="closeStatus">Indicates the reason for closing the WebSocket connection.</param>
		/// <param name="statusDescription">Allows applications to specify a human readable explanation as to why the connection is closed.</param>
		/// <param name="cancellationToken">The token that can be used to propagate notification that operations should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x060040A3 RID: 16547
		public abstract Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken);

		/// <summary>Used to clean up unmanaged resources for ASP.NET and self-hosted implementations.</summary>
		// Token: 0x060040A4 RID: 16548
		public abstract void Dispose();

		/// <summary>Receives data from the <see cref="T:System.Net.WebSockets.WebSocket" /> connection asynchronously.</summary>
		/// <param name="buffer">References the application buffer that is the storage location for the received data.</param>
		/// <param name="cancellationToken">Propagates the notification that operations should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the received data.</returns>
		// Token: 0x060040A5 RID: 16549
		public abstract Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken);

		/// <summary>Sends data over the <see cref="T:System.Net.WebSockets.WebSocket" /> connection asynchronously.</summary>
		/// <param name="buffer">The buffer to be sent over the connection.</param>
		/// <param name="messageType">Indicates whether the application is sending a binary or text message.</param>
		/// <param name="endOfMessage">Indicates whether the data in "buffer" is the last part of a message.</param>
		/// <param name="cancellationToken">The token that propagates the notification that operations should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x060040A6 RID: 16550
		public abstract Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken);

		// Token: 0x060040A7 RID: 16551 RVA: 0x000DE8C0 File Offset: 0x000DCAC0
		public virtual ValueTask<ValueWebSocketReceiveResult> ReceiveAsync(Memory<byte> buffer, CancellationToken cancellationToken)
		{
			WebSocket.<ReceiveAsync>d__14 <ReceiveAsync>d__;
			<ReceiveAsync>d__.<>4__this = this;
			<ReceiveAsync>d__.buffer = buffer;
			<ReceiveAsync>d__.cancellationToken = cancellationToken;
			<ReceiveAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<ValueWebSocketReceiveResult>.Create();
			<ReceiveAsync>d__.<>1__state = -1;
			<ReceiveAsync>d__.<>t__builder.Start<WebSocket.<ReceiveAsync>d__14>(ref <ReceiveAsync>d__);
			return <ReceiveAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x000DE914 File Offset: 0x000DCB14
		public virtual ValueTask SendAsync(ReadOnlyMemory<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			ArraySegment<byte> buffer2;
			return new ValueTask(MemoryMarshal.TryGetArray<byte>(buffer, out buffer2) ? this.SendAsync(buffer2, messageType, endOfMessage, cancellationToken) : this.SendWithArrayPoolAsync(buffer, messageType, endOfMessage, cancellationToken));
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x000DE948 File Offset: 0x000DCB48
		private Task SendWithArrayPoolAsync(ReadOnlyMemory<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			WebSocket.<SendWithArrayPoolAsync>d__16 <SendWithArrayPoolAsync>d__;
			<SendWithArrayPoolAsync>d__.<>4__this = this;
			<SendWithArrayPoolAsync>d__.buffer = buffer;
			<SendWithArrayPoolAsync>d__.messageType = messageType;
			<SendWithArrayPoolAsync>d__.endOfMessage = endOfMessage;
			<SendWithArrayPoolAsync>d__.cancellationToken = cancellationToken;
			<SendWithArrayPoolAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SendWithArrayPoolAsync>d__.<>1__state = -1;
			<SendWithArrayPoolAsync>d__.<>t__builder.Start<WebSocket.<SendWithArrayPoolAsync>d__16>(ref <SendWithArrayPoolAsync>d__);
			return <SendWithArrayPoolAsync>d__.<>t__builder.Task;
		}

		/// <summary>Gets the default WebSocket protocol keep-alive interval.</summary>
		/// <returns>The default WebSocket protocol keep-alive interval. The typical value for this interval is 30 seconds (as defined by the OS or the .NET platform). It is used to initialize <see cref="P:System.Net.WebSockets.ClientWebSocketOptions.KeepAliveInterval" /> value.</returns>
		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x060040AA RID: 16554 RVA: 0x000DE9AC File Offset: 0x000DCBAC
		public static TimeSpan DefaultKeepAliveInterval
		{
			get
			{
				return TimeSpan.FromSeconds(30.0);
			}
		}

		/// <summary>Verifies that the connection is in an expected state.</summary>
		/// <param name="state">The current state of the WebSocket to be tested against the list of valid states.</param>
		/// <param name="validStates">List of valid connection states.</param>
		// Token: 0x060040AB RID: 16555 RVA: 0x000DE9BC File Offset: 0x000DCBBC
		protected static void ThrowOnInvalidState(WebSocketState state, params WebSocketState[] validStates)
		{
			string p = string.Empty;
			if (validStates != null && validStates.Length != 0)
			{
				foreach (WebSocketState webSocketState in validStates)
				{
					if (state == webSocketState)
					{
						return;
					}
				}
				p = string.Join<WebSocketState>(", ", validStates);
			}
			throw new WebSocketException(SR.Format("The WebSocket is in an invalid state ('{0}') for this operation. Valid states are: '{1}'", state, p));
		}

		/// <summary>Returns a value that indicates if the state of the WebSocket instance is closed or aborted.</summary>
		/// <param name="state">The current state of the WebSocket.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.WebSockets.WebSocket" /> is closed or aborted; otherwise, <see langword="false" />.</returns>
		// Token: 0x060040AC RID: 16556 RVA: 0x000DEA11 File Offset: 0x000DCC11
		protected static bool IsStateTerminal(WebSocketState state)
		{
			return state == WebSocketState.Closed || state == WebSocketState.Aborted;
		}

		/// <summary>Create client buffers to use with this <see cref="T:System.Net.WebSockets.WebSocket" /> instance.</summary>
		/// <param name="receiveBufferSize">The size, in bytes, of the client receive buffer.</param>
		/// <param name="sendBufferSize">The size, in bytes, of the send buffer.</param>
		/// <returns>An array with the client buffers.</returns>
		// Token: 0x060040AD RID: 16557 RVA: 0x000DEA20 File Offset: 0x000DCC20
		public static ArraySegment<byte> CreateClientBuffer(int receiveBufferSize, int sendBufferSize)
		{
			if (receiveBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			if (sendBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("sendBufferSize", sendBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			return new ArraySegment<byte>(new byte[Math.Max(receiveBufferSize, sendBufferSize)]);
		}

		/// <summary>Creates a WebSocket server buffer.</summary>
		/// <param name="receiveBufferSize">The size, in bytes, of the desired buffer.</param>
		/// <returns>Returns <see cref="T:System.ArraySegment`1" />.</returns>
		// Token: 0x060040AE RID: 16558 RVA: 0x000DEA88 File Offset: 0x000DCC88
		public static ArraySegment<byte> CreateServerBuffer(int receiveBufferSize)
		{
			if (receiveBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			return new ArraySegment<byte>(new byte[receiveBufferSize]);
		}

		// Token: 0x060040AF RID: 16559 RVA: 0x000DEABC File Offset: 0x000DCCBC
		public static WebSocket CreateFromStream(Stream stream, bool isServer, string subProtocol, TimeSpan keepAliveInterval)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead || !stream.CanWrite)
			{
				throw new ArgumentException((!stream.CanRead) ? "The base stream is not readable." : "The base stream is not writeable.", "stream");
			}
			if (subProtocol != null)
			{
				WebSocketValidate.ValidateSubprotocol(subProtocol);
			}
			if (keepAliveInterval != Timeout.InfiniteTimeSpan && keepAliveInterval < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("keepAliveInterval", keepAliveInterval, SR.Format("The argument must be a value greater than {0}.", 0));
			}
			return ManagedWebSocket.CreateFromConnectedStream(stream, isServer, subProtocol, keepAliveInterval);
		}

		/// <summary>Returns a value that indicates if the WebSocket instance is targeting .NET Framework 4.5.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.WebSockets.WebSocket" /> is targeting .NET Framework 4.5; otherwise, <see langword="false" />.</returns>
		// Token: 0x060040B0 RID: 16560 RVA: 0x0000390E File Offset: 0x00001B0E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.")]
		public static bool IsApplicationTargeting45()
		{
			return true;
		}

		/// <summary>Allows callers to register prefixes for WebSocket requests (ws and wss).</summary>
		// Token: 0x060040B1 RID: 16561 RVA: 0x00011F54 File Offset: 0x00010154
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void RegisterPrefixes()
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Allows callers to create a client side WebSocket class which will use the WSPC for framing purposes.</summary>
		/// <param name="innerStream">The connection to be used for IO operations.</param>
		/// <param name="subProtocol">The subprotocol accepted by the client.</param>
		/// <param name="receiveBufferSize">The size in bytes of the client WebSocket receive buffer.</param>
		/// <param name="sendBufferSize">The size in bytes of the client WebSocket send buffer.</param>
		/// <param name="keepAliveInterval">Determines how regularly a frame is sent over the connection as a keep-alive. Applies only when the connection is idle.</param>
		/// <param name="useZeroMaskingKey">Indicates whether a random key or a static key (just zeros) should be used for the WebSocket masking.</param>
		/// <param name="internalBuffer">Will be used as the internal buffer in the WPC. The size has to be at least <c>2 * ReceiveBufferSize + SendBufferSize + 256 + 20 (16 on 32-bit)</c>.</param>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocket" />.</returns>
		// Token: 0x060040B2 RID: 16562 RVA: 0x000DEB54 File Offset: 0x000DCD54
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static WebSocket CreateClientWebSocket(Stream innerStream, string subProtocol, int receiveBufferSize, int sendBufferSize, TimeSpan keepAliveInterval, bool useZeroMaskingKey, ArraySegment<byte> internalBuffer)
		{
			if (innerStream == null)
			{
				throw new ArgumentNullException("innerStream");
			}
			if (!innerStream.CanRead || !innerStream.CanWrite)
			{
				throw new ArgumentException((!innerStream.CanRead) ? "The base stream is not readable." : "The base stream is not writeable.", "innerStream");
			}
			if (subProtocol != null)
			{
				WebSocketValidate.ValidateSubprotocol(subProtocol);
			}
			if (keepAliveInterval != Timeout.InfiniteTimeSpan && keepAliveInterval < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("keepAliveInterval", keepAliveInterval, SR.Format("The argument must be a value greater than {0}.", 0));
			}
			if (receiveBufferSize <= 0 || sendBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException((receiveBufferSize <= 0) ? "receiveBufferSize" : "sendBufferSize", (receiveBufferSize <= 0) ? receiveBufferSize : sendBufferSize, SR.Format("The argument must be a value greater than {0}.", 0));
			}
			return ManagedWebSocket.CreateFromConnectedStream(innerStream, false, subProtocol, keepAliveInterval);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocket" /> class.</summary>
		// Token: 0x060040B3 RID: 16563 RVA: 0x0000219B File Offset: 0x0000039B
		protected WebSocket()
		{
		}

		// Token: 0x020007EC RID: 2028
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReceiveAsync>d__14 : IAsyncStateMachine
		{
			// Token: 0x060040B4 RID: 16564 RVA: 0x000DEC2C File Offset: 0x000DCE2C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebSocket webSocket = this.<>4__this;
				ValueWebSocketReceiveResult result2;
				try
				{
					ConfiguredTaskAwaitable<WebSocketReceiveResult>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num != 1)
						{
							ArraySegment<byte> arraySegment;
							if (MemoryMarshal.TryGetArray<byte>(this.buffer, out arraySegment))
							{
								awaiter = webSocket.ReceiveAsync(arraySegment, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 0);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<WebSocketReceiveResult>.ConfiguredTaskAwaiter, WebSocket.<ReceiveAsync>d__14>(ref awaiter, ref this);
									return;
								}
								goto IL_97;
							}
							else
							{
								this.<array>5__2 = ArrayPool<byte>.Shared.Rent(this.buffer.Length);
							}
						}
						try
						{
							if (num != 1)
							{
								awaiter = webSocket.ReceiveAsync(new ArraySegment<byte>(this.<array>5__2, 0, this.buffer.Length), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 1);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<WebSocketReceiveResult>.ConfiguredTaskAwaiter, WebSocket.<ReceiveAsync>d__14>(ref awaiter, ref this);
									return;
								}
							}
							else
							{
								awaiter = this.<>u__1;
								this.<>u__1 = default(ConfiguredTaskAwaitable<WebSocketReceiveResult>.ConfiguredTaskAwaiter);
								num = (this.<>1__state = -1);
							}
							WebSocketReceiveResult result = awaiter.GetResult();
							new Span<byte>(this.<array>5__2, 0, result.Count).CopyTo(this.buffer.Span);
							result2 = new ValueWebSocketReceiveResult(result.Count, result.MessageType, result.EndOfMessage);
							goto IL_1DA;
						}
						finally
						{
							if (num < 0)
							{
								ArrayPool<byte>.Shared.Return(this.<array>5__2, false);
							}
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<WebSocketReceiveResult>.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
					}
					IL_97:
					WebSocketReceiveResult result3 = awaiter.GetResult();
					result2 = new ValueWebSocketReceiveResult(result3.Count, result3.MessageType, result3.EndOfMessage);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1DA:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x060040B5 RID: 16565 RVA: 0x000DEE5C File Offset: 0x000DD05C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002725 RID: 10021
			public int <>1__state;

			// Token: 0x04002726 RID: 10022
			public AsyncValueTaskMethodBuilder<ValueWebSocketReceiveResult> <>t__builder;

			// Token: 0x04002727 RID: 10023
			public Memory<byte> buffer;

			// Token: 0x04002728 RID: 10024
			public WebSocket <>4__this;

			// Token: 0x04002729 RID: 10025
			public CancellationToken cancellationToken;

			// Token: 0x0400272A RID: 10026
			private ConfiguredTaskAwaitable<WebSocketReceiveResult>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x0400272B RID: 10027
			private byte[] <array>5__2;
		}

		// Token: 0x020007ED RID: 2029
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SendWithArrayPoolAsync>d__16 : IAsyncStateMachine
		{
			// Token: 0x060040B6 RID: 16566 RVA: 0x000DEE6C File Offset: 0x000DD06C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebSocket webSocket = this.<>4__this;
				try
				{
					if (num != 0)
					{
						this.<array>5__2 = ArrayPool<byte>.Shared.Rent(this.buffer.Length);
					}
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							this.buffer.Span.CopyTo(this.<array>5__2);
							awaiter = webSocket.SendAsync(new ArraySegment<byte>(this.<array>5__2, 0, this.buffer.Length), this.messageType, this.endOfMessage, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebSocket.<SendWithArrayPoolAsync>d__16>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						awaiter.GetResult();
					}
					finally
					{
						if (num < 0)
						{
							ArrayPool<byte>.Shared.Return(this.<array>5__2, false);
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<array>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<array>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060040B7 RID: 16567 RVA: 0x000DEFC8 File Offset: 0x000DD1C8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400272C RID: 10028
			public int <>1__state;

			// Token: 0x0400272D RID: 10029
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400272E RID: 10030
			public ReadOnlyMemory<byte> buffer;

			// Token: 0x0400272F RID: 10031
			public WebSocket <>4__this;

			// Token: 0x04002730 RID: 10032
			public WebSocketMessageType messageType;

			// Token: 0x04002731 RID: 10033
			public bool endOfMessage;

			// Token: 0x04002732 RID: 10034
			public CancellationToken cancellationToken;

			// Token: 0x04002733 RID: 10035
			private byte[] <array>5__2;

			// Token: 0x04002734 RID: 10036
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
