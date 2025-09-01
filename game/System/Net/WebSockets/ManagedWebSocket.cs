using System;
using System.Buffers;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	// Token: 0x020007CA RID: 1994
	internal sealed class ManagedWebSocket : WebSocket
	{
		// Token: 0x06003FD7 RID: 16343 RVA: 0x000D9B38 File Offset: 0x000D7D38
		public static ManagedWebSocket CreateFromConnectedStream(Stream stream, bool isServer, string subprotocol, TimeSpan keepAliveInterval)
		{
			return new ManagedWebSocket(stream, isServer, subprotocol, keepAliveInterval);
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06003FD8 RID: 16344 RVA: 0x000D9B43 File Offset: 0x000D7D43
		private object StateUpdateLock
		{
			get
			{
				return this._abortSource;
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06003FD9 RID: 16345 RVA: 0x000D9B4B File Offset: 0x000D7D4B
		private object ReceiveAsyncLock
		{
			get
			{
				return this._utf8TextState;
			}
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x000D9B54 File Offset: 0x000D7D54
		private ManagedWebSocket(Stream stream, bool isServer, string subprotocol, TimeSpan keepAliveInterval)
		{
			this._stream = stream;
			this._isServer = isServer;
			this._subprotocol = subprotocol;
			this._receiveBuffer = new byte[125];
			this._abortSource.Token.Register(delegate(object s)
			{
				ManagedWebSocket managedWebSocket = (ManagedWebSocket)s;
				object stateUpdateLock = managedWebSocket.StateUpdateLock;
				lock (stateUpdateLock)
				{
					WebSocketState state = managedWebSocket._state;
					if (state != WebSocketState.Closed && state != WebSocketState.Aborted)
					{
						managedWebSocket._state = ((state != WebSocketState.None && state != WebSocketState.Connecting) ? WebSocketState.Aborted : WebSocketState.Closed);
					}
				}
			}, this);
			if (keepAliveInterval > TimeSpan.Zero)
			{
				this._keepAliveTimer = new Timer(delegate(object s)
				{
					((ManagedWebSocket)s).SendKeepAliveFrameAsync();
				}, this, keepAliveInterval, keepAliveInterval);
			}
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x000D9C54 File Offset: 0x000D7E54
		public override void Dispose()
		{
			object stateUpdateLock = this.StateUpdateLock;
			lock (stateUpdateLock)
			{
				this.DisposeCore();
			}
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x000D9C94 File Offset: 0x000D7E94
		private void DisposeCore()
		{
			if (!this._disposed)
			{
				this._disposed = true;
				Timer keepAliveTimer = this._keepAliveTimer;
				if (keepAliveTimer != null)
				{
					keepAliveTimer.Dispose();
				}
				Stream stream = this._stream;
				if (stream != null)
				{
					stream.Dispose();
				}
				if (this._state < WebSocketState.Aborted)
				{
					this._state = WebSocketState.Closed;
				}
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06003FDD RID: 16349 RVA: 0x000D9CE2 File Offset: 0x000D7EE2
		public override WebSocketCloseStatus? CloseStatus
		{
			get
			{
				return this._closeStatus;
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06003FDE RID: 16350 RVA: 0x000D9CEA File Offset: 0x000D7EEA
		public override string CloseStatusDescription
		{
			get
			{
				return this._closeStatusDescription;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06003FDF RID: 16351 RVA: 0x000D9CF2 File Offset: 0x000D7EF2
		public override WebSocketState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06003FE0 RID: 16352 RVA: 0x000D9CFA File Offset: 0x000D7EFA
		public override string SubProtocol
		{
			get
			{
				return this._subprotocol;
			}
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x000D9D04 File Offset: 0x000D7F04
		public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			if (messageType != WebSocketMessageType.Text && messageType != WebSocketMessageType.Binary)
			{
				throw new ArgumentException(SR.Format("The message type '{0}' is not allowed for the '{1}' operation. Valid message types are: '{2}, {3}'. To close the WebSocket, use the '{4}' operation instead. ", new object[]
				{
					"Close",
					"SendAsync",
					"Binary",
					"Text",
					"CloseOutputAsync"
				}), "messageType");
			}
			WebSocketValidate.ValidateArraySegment(buffer, "buffer");
			return this.SendPrivateAsync(buffer, messageType, endOfMessage, cancellationToken).AsTask();
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x000D9D80 File Offset: 0x000D7F80
		private ValueTask SendPrivateAsync(ReadOnlyMemory<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			if (messageType != WebSocketMessageType.Text && messageType != WebSocketMessageType.Binary)
			{
				throw new ArgumentException(SR.Format("The message type '{0}' is not allowed for the '{1}' operation. Valid message types are: '{2}, {3}'. To close the WebSocket, use the '{4}' operation instead. ", new object[]
				{
					"Close",
					"SendAsync",
					"Binary",
					"Text",
					"CloseOutputAsync"
				}), "messageType");
			}
			try
			{
				WebSocketValidate.ThrowIfInvalidState(this._state, this._disposed, ManagedWebSocket.s_validSendStates);
			}
			catch (Exception exception)
			{
				return new ValueTask(Task.FromException(exception));
			}
			ManagedWebSocket.MessageOpcode opcode = this._lastSendWasFragment ? ManagedWebSocket.MessageOpcode.Continuation : ((messageType == WebSocketMessageType.Binary) ? ManagedWebSocket.MessageOpcode.Binary : ManagedWebSocket.MessageOpcode.Text);
			ValueTask result = this.SendFrameAsync(opcode, endOfMessage, buffer, cancellationToken);
			this._lastSendWasFragment = !endOfMessage;
			return result;
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x000D9E38 File Offset: 0x000D8038
		public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			WebSocketValidate.ValidateArraySegment(buffer, "buffer");
			Task<WebSocketReceiveResult> result;
			try
			{
				WebSocketValidate.ThrowIfInvalidState(this._state, this._disposed, ManagedWebSocket.s_validReceiveStates);
				object receiveAsyncLock = this.ReceiveAsyncLock;
				lock (receiveAsyncLock)
				{
					this.ThrowIfOperationInProgress(this._lastReceiveAsync.IsCompleted, "ReceiveAsync");
					Task<WebSocketReceiveResult> task = this.ReceiveAsyncPrivate<ManagedWebSocket.WebSocketReceiveResultGetter, WebSocketReceiveResult>(buffer, cancellationToken, default(ManagedWebSocket.WebSocketReceiveResultGetter)).AsTask();
					this._lastReceiveAsync = task;
					result = task;
				}
			}
			catch (Exception exception)
			{
				result = Task.FromException<WebSocketReceiveResult>(exception);
			}
			return result;
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x000D9EEC File Offset: 0x000D80EC
		public override Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			WebSocketValidate.ValidateCloseStatus(closeStatus, statusDescription);
			try
			{
				WebSocketValidate.ThrowIfInvalidState(this._state, this._disposed, ManagedWebSocket.s_validCloseStates);
			}
			catch (Exception exception)
			{
				return Task.FromException(exception);
			}
			return this.CloseAsyncPrivate(closeStatus, statusDescription, cancellationToken);
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x000D9F3C File Offset: 0x000D813C
		public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			WebSocketValidate.ValidateCloseStatus(closeStatus, statusDescription);
			try
			{
				WebSocketValidate.ThrowIfInvalidState(this._state, this._disposed, ManagedWebSocket.s_validCloseOutputStates);
			}
			catch (Exception exception)
			{
				return Task.FromException(exception);
			}
			return this.SendCloseFrameAsync(closeStatus, statusDescription, cancellationToken);
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x000D9F8C File Offset: 0x000D818C
		public override void Abort()
		{
			this._abortSource.Cancel();
			this.Dispose();
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x000D9F9F File Offset: 0x000D819F
		private ValueTask SendFrameAsync(ManagedWebSocket.MessageOpcode opcode, bool endOfMessage, ReadOnlyMemory<byte> payloadBuffer, CancellationToken cancellationToken)
		{
			if (!cancellationToken.CanBeCanceled && this._sendFrameAsyncLock.Wait(0))
			{
				return this.SendFrameLockAcquiredNonCancelableAsync(opcode, endOfMessage, payloadBuffer);
			}
			return new ValueTask(this.SendFrameFallbackAsync(opcode, endOfMessage, payloadBuffer, cancellationToken));
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x000D9FD4 File Offset: 0x000D81D4
		private ValueTask SendFrameLockAcquiredNonCancelableAsync(ManagedWebSocket.MessageOpcode opcode, bool endOfMessage, ReadOnlyMemory<byte> payloadBuffer)
		{
			ValueTask valueTask = default(ValueTask);
			bool flag = true;
			try
			{
				int length = this.WriteFrameToSendBuffer(opcode, endOfMessage, payloadBuffer.Span);
				valueTask = this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._sendBuffer, 0, length), default(CancellationToken));
				if (valueTask.IsCompleted)
				{
					return valueTask;
				}
				flag = false;
			}
			catch (Exception ex)
			{
				return new ValueTask(Task.FromException((ex is OperationCanceledException) ? ex : ((this._state == WebSocketState.Aborted) ? ManagedWebSocket.CreateOperationCanceledException(ex, default(CancellationToken)) : new WebSocketException(WebSocketError.ConnectionClosedPrematurely, ex))));
			}
			finally
			{
				if (flag)
				{
					this._sendFrameAsyncLock.Release();
					this.ReleaseSendBuffer();
				}
			}
			return new ValueTask(this.WaitForWriteTaskAsync(valueTask));
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x000DA0B0 File Offset: 0x000D82B0
		private Task WaitForWriteTaskAsync(ValueTask writeTask)
		{
			ManagedWebSocket.<WaitForWriteTaskAsync>d__55 <WaitForWriteTaskAsync>d__;
			<WaitForWriteTaskAsync>d__.<>4__this = this;
			<WaitForWriteTaskAsync>d__.writeTask = writeTask;
			<WaitForWriteTaskAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WaitForWriteTaskAsync>d__.<>1__state = -1;
			<WaitForWriteTaskAsync>d__.<>t__builder.Start<ManagedWebSocket.<WaitForWriteTaskAsync>d__55>(ref <WaitForWriteTaskAsync>d__);
			return <WaitForWriteTaskAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x000DA0FC File Offset: 0x000D82FC
		private Task SendFrameFallbackAsync(ManagedWebSocket.MessageOpcode opcode, bool endOfMessage, ReadOnlyMemory<byte> payloadBuffer, CancellationToken cancellationToken)
		{
			ManagedWebSocket.<SendFrameFallbackAsync>d__56 <SendFrameFallbackAsync>d__;
			<SendFrameFallbackAsync>d__.<>4__this = this;
			<SendFrameFallbackAsync>d__.opcode = opcode;
			<SendFrameFallbackAsync>d__.endOfMessage = endOfMessage;
			<SendFrameFallbackAsync>d__.payloadBuffer = payloadBuffer;
			<SendFrameFallbackAsync>d__.cancellationToken = cancellationToken;
			<SendFrameFallbackAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SendFrameFallbackAsync>d__.<>1__state = -1;
			<SendFrameFallbackAsync>d__.<>t__builder.Start<ManagedWebSocket.<SendFrameFallbackAsync>d__56>(ref <SendFrameFallbackAsync>d__);
			return <SendFrameFallbackAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x000DA160 File Offset: 0x000D8360
		private int WriteFrameToSendBuffer(ManagedWebSocket.MessageOpcode opcode, bool endOfMessage, ReadOnlySpan<byte> payloadBuffer)
		{
			this.AllocateSendBuffer(payloadBuffer.Length + 14);
			int? num = null;
			int num2;
			if (this._isServer)
			{
				num2 = ManagedWebSocket.WriteHeader(opcode, this._sendBuffer, payloadBuffer, endOfMessage, false);
			}
			else
			{
				num = new int?(ManagedWebSocket.WriteHeader(opcode, this._sendBuffer, payloadBuffer, endOfMessage, true));
				num2 = num.GetValueOrDefault() + 4;
			}
			if (payloadBuffer.Length > 0)
			{
				payloadBuffer.CopyTo(new Span<byte>(this._sendBuffer, num2, payloadBuffer.Length));
				if (num != null)
				{
					ManagedWebSocket.ApplyMask(new Span<byte>(this._sendBuffer, num2, payloadBuffer.Length), this._sendBuffer, num.Value, 0);
				}
			}
			return num2 + payloadBuffer.Length;
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x000DA21C File Offset: 0x000D841C
		private void SendKeepAliveFrameAsync()
		{
			if (this._sendFrameAsyncLock.Wait(0))
			{
				ValueTask valueTask = this.SendFrameLockAcquiredNonCancelableAsync(ManagedWebSocket.MessageOpcode.Ping, true, Memory<byte>.Empty);
				if (valueTask.IsCompletedSuccessfully)
				{
					valueTask.GetAwaiter().GetResult();
					return;
				}
				valueTask.AsTask().ContinueWith(delegate(Task p)
				{
					AggregateException exception = p.Exception;
				}, CancellationToken.None, TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
			}
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x000DA2A0 File Offset: 0x000D84A0
		private static int WriteHeader(ManagedWebSocket.MessageOpcode opcode, byte[] sendBuffer, ReadOnlySpan<byte> payload, bool endOfMessage, bool useMask)
		{
			sendBuffer[0] = (byte)opcode;
			if (endOfMessage)
			{
				int num = 0;
				sendBuffer[num] |= 128;
			}
			int num2;
			if (payload.Length <= 125)
			{
				sendBuffer[1] = (byte)payload.Length;
				num2 = 2;
			}
			else if (payload.Length <= 65535)
			{
				sendBuffer[1] = 126;
				sendBuffer[2] = (byte)(payload.Length / 256);
				sendBuffer[3] = (byte)payload.Length;
				num2 = 4;
			}
			else
			{
				sendBuffer[1] = 127;
				int num3 = payload.Length;
				for (int i = 9; i >= 2; i--)
				{
					sendBuffer[i] = (byte)num3;
					num3 /= 256;
				}
				num2 = 10;
			}
			if (useMask)
			{
				int num4 = 1;
				sendBuffer[num4] |= 128;
				ManagedWebSocket.WriteRandomMask(sendBuffer, num2);
			}
			return num2;
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x000DA359 File Offset: 0x000D8559
		private static void WriteRandomMask(byte[] buffer, int offset)
		{
			ManagedWebSocket.s_random.GetBytes(buffer, offset, 4);
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x000DA368 File Offset: 0x000D8568
		private ValueTask<TWebSocketReceiveResult> ReceiveAsyncPrivate<TWebSocketReceiveResultGetter, TWebSocketReceiveResult>(Memory<byte> payloadBuffer, CancellationToken cancellationToken, TWebSocketReceiveResultGetter resultGetter = default(TWebSocketReceiveResultGetter)) where TWebSocketReceiveResultGetter : struct, ManagedWebSocket.IWebSocketReceiveResultGetter<TWebSocketReceiveResult>
		{
			ManagedWebSocket.<ReceiveAsyncPrivate>d__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult> <ReceiveAsyncPrivate>d__;
			<ReceiveAsyncPrivate>d__.<>4__this = this;
			<ReceiveAsyncPrivate>d__.payloadBuffer = payloadBuffer;
			<ReceiveAsyncPrivate>d__.cancellationToken = cancellationToken;
			<ReceiveAsyncPrivate>d__.resultGetter = resultGetter;
			<ReceiveAsyncPrivate>d__.<>t__builder = AsyncValueTaskMethodBuilder<TWebSocketReceiveResult>.Create();
			<ReceiveAsyncPrivate>d__.<>1__state = -1;
			<ReceiveAsyncPrivate>d__.<>t__builder.Start<ManagedWebSocket.<ReceiveAsyncPrivate>d__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult>>(ref <ReceiveAsyncPrivate>d__);
			return <ReceiveAsyncPrivate>d__.<>t__builder.Task;
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x000DA3C4 File Offset: 0x000D85C4
		private Task HandleReceivedCloseAsync(ManagedWebSocket.MessageHeader header, CancellationToken cancellationToken)
		{
			ManagedWebSocket.<HandleReceivedCloseAsync>d__62 <HandleReceivedCloseAsync>d__;
			<HandleReceivedCloseAsync>d__.<>4__this = this;
			<HandleReceivedCloseAsync>d__.header = header;
			<HandleReceivedCloseAsync>d__.cancellationToken = cancellationToken;
			<HandleReceivedCloseAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<HandleReceivedCloseAsync>d__.<>1__state = -1;
			<HandleReceivedCloseAsync>d__.<>t__builder.Start<ManagedWebSocket.<HandleReceivedCloseAsync>d__62>(ref <HandleReceivedCloseAsync>d__);
			return <HandleReceivedCloseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x000DA418 File Offset: 0x000D8618
		private Task WaitForServerToCloseConnectionAsync(CancellationToken cancellationToken)
		{
			ManagedWebSocket.<WaitForServerToCloseConnectionAsync>d__63 <WaitForServerToCloseConnectionAsync>d__;
			<WaitForServerToCloseConnectionAsync>d__.<>4__this = this;
			<WaitForServerToCloseConnectionAsync>d__.cancellationToken = cancellationToken;
			<WaitForServerToCloseConnectionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WaitForServerToCloseConnectionAsync>d__.<>1__state = -1;
			<WaitForServerToCloseConnectionAsync>d__.<>t__builder.Start<ManagedWebSocket.<WaitForServerToCloseConnectionAsync>d__63>(ref <WaitForServerToCloseConnectionAsync>d__);
			return <WaitForServerToCloseConnectionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x000DA464 File Offset: 0x000D8664
		private Task HandleReceivedPingPongAsync(ManagedWebSocket.MessageHeader header, CancellationToken cancellationToken)
		{
			ManagedWebSocket.<HandleReceivedPingPongAsync>d__64 <HandleReceivedPingPongAsync>d__;
			<HandleReceivedPingPongAsync>d__.<>4__this = this;
			<HandleReceivedPingPongAsync>d__.header = header;
			<HandleReceivedPingPongAsync>d__.cancellationToken = cancellationToken;
			<HandleReceivedPingPongAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<HandleReceivedPingPongAsync>d__.<>1__state = -1;
			<HandleReceivedPingPongAsync>d__.<>t__builder.Start<ManagedWebSocket.<HandleReceivedPingPongAsync>d__64>(ref <HandleReceivedPingPongAsync>d__);
			return <HandleReceivedPingPongAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x000DA4B7 File Offset: 0x000D86B7
		private static bool IsValidCloseStatus(WebSocketCloseStatus closeStatus)
		{
			return closeStatus >= WebSocketCloseStatus.NormalClosure && closeStatus < (WebSocketCloseStatus)5000 && (closeStatus >= (WebSocketCloseStatus)3000 || (closeStatus - WebSocketCloseStatus.NormalClosure <= 3 || closeStatus - WebSocketCloseStatus.InvalidPayloadData <= 4));
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x000DA4EC File Offset: 0x000D86EC
		private Task CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus closeStatus, WebSocketError error, Exception innerException = null)
		{
			ManagedWebSocket.<CloseWithReceiveErrorAndThrowAsync>d__66 <CloseWithReceiveErrorAndThrowAsync>d__;
			<CloseWithReceiveErrorAndThrowAsync>d__.<>4__this = this;
			<CloseWithReceiveErrorAndThrowAsync>d__.closeStatus = closeStatus;
			<CloseWithReceiveErrorAndThrowAsync>d__.error = error;
			<CloseWithReceiveErrorAndThrowAsync>d__.innerException = innerException;
			<CloseWithReceiveErrorAndThrowAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CloseWithReceiveErrorAndThrowAsync>d__.<>1__state = -1;
			<CloseWithReceiveErrorAndThrowAsync>d__.<>t__builder.Start<ManagedWebSocket.<CloseWithReceiveErrorAndThrowAsync>d__66>(ref <CloseWithReceiveErrorAndThrowAsync>d__);
			return <CloseWithReceiveErrorAndThrowAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x000DA548 File Offset: 0x000D8748
		private unsafe bool TryParseMessageHeaderFromReceiveBuffer(out ManagedWebSocket.MessageHeader resultHeader)
		{
			ManagedWebSocket.MessageHeader messageHeader = default(ManagedWebSocket.MessageHeader);
			Span<byte> span = this._receiveBuffer.Span;
			messageHeader.Fin = ((*span[this._receiveBufferOffset] & 128) > 0);
			bool flag = (*span[this._receiveBufferOffset] & 112) > 0;
			messageHeader.Opcode = (ManagedWebSocket.MessageOpcode)(*span[this._receiveBufferOffset] & 15);
			bool flag2 = (*span[this._receiveBufferOffset + 1] & 128) > 0;
			messageHeader.PayloadLength = (long)(*span[this._receiveBufferOffset + 1] & 127);
			this.ConsumeFromBuffer(2);
			if (messageHeader.PayloadLength == 126L)
			{
				messageHeader.PayloadLength = (long)((int)(*span[this._receiveBufferOffset]) << 8 | (int)(*span[this._receiveBufferOffset + 1]));
				this.ConsumeFromBuffer(2);
			}
			else if (messageHeader.PayloadLength == 127L)
			{
				messageHeader.PayloadLength = 0L;
				for (int i = 0; i < 8; i++)
				{
					messageHeader.PayloadLength = (messageHeader.PayloadLength << 8 | (long)((ulong)(*span[this._receiveBufferOffset + i])));
				}
				this.ConsumeFromBuffer(8);
			}
			bool flag3 = flag;
			if (flag2)
			{
				if (!this._isServer)
				{
					flag3 = true;
				}
				messageHeader.Mask = ManagedWebSocket.CombineMaskBytes(span, this._receiveBufferOffset);
				this.ConsumeFromBuffer(4);
			}
			switch (messageHeader.Opcode)
			{
			case ManagedWebSocket.MessageOpcode.Continuation:
				if (this._lastReceiveHeader.Fin)
				{
					flag3 = true;
					goto IL_1CD;
				}
				goto IL_1CD;
			case ManagedWebSocket.MessageOpcode.Text:
			case ManagedWebSocket.MessageOpcode.Binary:
				if (!this._lastReceiveHeader.Fin)
				{
					flag3 = true;
					goto IL_1CD;
				}
				goto IL_1CD;
			case ManagedWebSocket.MessageOpcode.Close:
			case ManagedWebSocket.MessageOpcode.Ping:
			case ManagedWebSocket.MessageOpcode.Pong:
				if (messageHeader.PayloadLength > 125L || !messageHeader.Fin)
				{
					flag3 = true;
					goto IL_1CD;
				}
				goto IL_1CD;
			}
			flag3 = true;
			IL_1CD:
			resultHeader = messageHeader;
			return !flag3;
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x000DA730 File Offset: 0x000D8930
		private Task CloseAsyncPrivate(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			ManagedWebSocket.<CloseAsyncPrivate>d__68 <CloseAsyncPrivate>d__;
			<CloseAsyncPrivate>d__.<>4__this = this;
			<CloseAsyncPrivate>d__.closeStatus = closeStatus;
			<CloseAsyncPrivate>d__.statusDescription = statusDescription;
			<CloseAsyncPrivate>d__.cancellationToken = cancellationToken;
			<CloseAsyncPrivate>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CloseAsyncPrivate>d__.<>1__state = -1;
			<CloseAsyncPrivate>d__.<>t__builder.Start<ManagedWebSocket.<CloseAsyncPrivate>d__68>(ref <CloseAsyncPrivate>d__);
			return <CloseAsyncPrivate>d__.<>t__builder.Task;
		}

		// Token: 0x06003FF7 RID: 16375 RVA: 0x000DA78C File Offset: 0x000D898C
		private Task SendCloseFrameAsync(WebSocketCloseStatus closeStatus, string closeStatusDescription, CancellationToken cancellationToken)
		{
			ManagedWebSocket.<SendCloseFrameAsync>d__69 <SendCloseFrameAsync>d__;
			<SendCloseFrameAsync>d__.<>4__this = this;
			<SendCloseFrameAsync>d__.closeStatus = closeStatus;
			<SendCloseFrameAsync>d__.closeStatusDescription = closeStatusDescription;
			<SendCloseFrameAsync>d__.cancellationToken = cancellationToken;
			<SendCloseFrameAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SendCloseFrameAsync>d__.<>1__state = -1;
			<SendCloseFrameAsync>d__.<>t__builder.Start<ManagedWebSocket.<SendCloseFrameAsync>d__69>(ref <SendCloseFrameAsync>d__);
			return <SendCloseFrameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x000DA7E7 File Offset: 0x000D89E7
		private void ConsumeFromBuffer(int count)
		{
			this._receiveBufferCount -= count;
			this._receiveBufferOffset += count;
		}

		// Token: 0x06003FF9 RID: 16377 RVA: 0x000DA808 File Offset: 0x000D8A08
		private Task EnsureBufferContainsAsync(int minimumRequiredBytes, CancellationToken cancellationToken, bool throwOnPrematureClosure = true)
		{
			ManagedWebSocket.<EnsureBufferContainsAsync>d__71 <EnsureBufferContainsAsync>d__;
			<EnsureBufferContainsAsync>d__.<>4__this = this;
			<EnsureBufferContainsAsync>d__.minimumRequiredBytes = minimumRequiredBytes;
			<EnsureBufferContainsAsync>d__.cancellationToken = cancellationToken;
			<EnsureBufferContainsAsync>d__.throwOnPrematureClosure = throwOnPrematureClosure;
			<EnsureBufferContainsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EnsureBufferContainsAsync>d__.<>1__state = -1;
			<EnsureBufferContainsAsync>d__.<>t__builder.Start<ManagedWebSocket.<EnsureBufferContainsAsync>d__71>(ref <EnsureBufferContainsAsync>d__);
			return <EnsureBufferContainsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06003FFA RID: 16378 RVA: 0x000DA863 File Offset: 0x000D8A63
		private void ThrowIfEOFUnexpected(bool throwOnPrematureClosure)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("WebSocket");
			}
			if (throwOnPrematureClosure)
			{
				throw new WebSocketException(WebSocketError.ConnectionClosedPrematurely);
			}
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x000DA882 File Offset: 0x000D8A82
		private void AllocateSendBuffer(int minLength)
		{
			this._sendBuffer = ArrayPool<byte>.Shared.Rent(minLength);
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x000DA898 File Offset: 0x000D8A98
		private void ReleaseSendBuffer()
		{
			byte[] sendBuffer = this._sendBuffer;
			if (sendBuffer != null)
			{
				this._sendBuffer = null;
				ArrayPool<byte>.Shared.Return(sendBuffer, false);
			}
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x000DA8C2 File Offset: 0x000D8AC2
		private static int CombineMaskBytes(Span<byte> buffer, int maskOffset)
		{
			return BitConverter.ToInt32(buffer.Slice(maskOffset));
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x000DA8D6 File Offset: 0x000D8AD6
		private static int ApplyMask(Span<byte> toMask, byte[] mask, int maskOffset, int maskOffsetIndex)
		{
			return ManagedWebSocket.ApplyMask(toMask, ManagedWebSocket.CombineMaskBytes(mask, maskOffset), maskOffsetIndex);
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x000DA8EC File Offset: 0x000D8AEC
		private unsafe static int ApplyMask(Span<byte> toMask, int mask, int maskIndex)
		{
			int num = maskIndex * 8;
			int num2 = (int)((uint)mask >> num | (uint)((uint)mask << 32 - num));
			int i = toMask.Length;
			if (i > 0)
			{
				fixed (byte* reference = MemoryMarshal.GetReference<byte>(toMask))
				{
					byte* ptr = reference;
					if (ptr % 4L == null)
					{
						while (i >= 4)
						{
							i -= 4;
							*(int*)ptr ^= num2;
							ptr += 4;
						}
					}
					if (i > 0)
					{
						byte* ptr2 = (byte*)(&mask);
						byte* ptr3 = ptr + i;
						while (ptr < ptr3)
						{
							byte* ptr4 = ptr++;
							*ptr4 ^= ptr2[maskIndex];
							maskIndex = (maskIndex + 1 & 3);
						}
					}
				}
			}
			return maskIndex;
		}

		// Token: 0x06004000 RID: 16384 RVA: 0x000DA977 File Offset: 0x000D8B77
		private void ThrowIfOperationInProgress(bool operationCompleted, [CallerMemberName] string methodName = null)
		{
			if (!operationCompleted)
			{
				this.Abort();
				this.ThrowOperationInProgress(methodName);
			}
		}

		// Token: 0x06004001 RID: 16385 RVA: 0x000DA989 File Offset: 0x000D8B89
		private void ThrowOperationInProgress(string methodName)
		{
			throw new InvalidOperationException(SR.Format("There is already one outstanding '{0}' call for this WebSocket instance. ReceiveAsync and SendAsync can be called simultaneously, but at most one outstanding operation for each of them is allowed at the same time.", methodName));
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x000DA99B File Offset: 0x000D8B9B
		private static Exception CreateOperationCanceledException(Exception innerException, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new OperationCanceledException(new OperationCanceledException().Message, innerException, cancellationToken);
		}

		// Token: 0x06004003 RID: 16387 RVA: 0x000DA9B0 File Offset: 0x000D8BB0
		private unsafe static bool TryValidateUtf8(Span<byte> span, bool endOfMessage, ManagedWebSocket.Utf8MessageState state)
		{
			int i = 0;
			while (i < span.Length)
			{
				if (!state.SequenceInProgress)
				{
					state.SequenceInProgress = true;
					byte b = *span[i];
					i++;
					if ((b & 128) == 0)
					{
						state.AdditionalBytesExpected = 0;
						state.CurrentDecodeBits = (int)(b & 127);
						state.ExpectedValueMin = 0;
					}
					else
					{
						if ((b & 192) == 128)
						{
							return false;
						}
						if ((b & 224) == 192)
						{
							state.AdditionalBytesExpected = 1;
							state.CurrentDecodeBits = (int)(b & 31);
							state.ExpectedValueMin = 128;
						}
						else if ((b & 240) == 224)
						{
							state.AdditionalBytesExpected = 2;
							state.CurrentDecodeBits = (int)(b & 15);
							state.ExpectedValueMin = 2048;
						}
						else
						{
							if ((b & 248) != 240)
							{
								return false;
							}
							state.AdditionalBytesExpected = 3;
							state.CurrentDecodeBits = (int)(b & 7);
							state.ExpectedValueMin = 65536;
						}
					}
				}
				while (state.AdditionalBytesExpected > 0 && i < span.Length)
				{
					byte b2 = *span[i];
					if ((b2 & 192) != 128)
					{
						return false;
					}
					i++;
					state.AdditionalBytesExpected--;
					state.CurrentDecodeBits = (state.CurrentDecodeBits << 6 | (int)(b2 & 63));
					if (state.AdditionalBytesExpected == 1 && state.CurrentDecodeBits >= 864 && state.CurrentDecodeBits <= 895)
					{
						return false;
					}
					if (state.AdditionalBytesExpected == 2 && state.CurrentDecodeBits >= 272)
					{
						return false;
					}
				}
				if (state.AdditionalBytesExpected == 0)
				{
					state.SequenceInProgress = false;
					if (state.CurrentDecodeBits < state.ExpectedValueMin)
					{
						return false;
					}
				}
			}
			return !endOfMessage || !state.SequenceInProgress;
		}

		// Token: 0x06004004 RID: 16388 RVA: 0x000DAB74 File Offset: 0x000D8D74
		private Task ValidateAndReceiveAsync(Task receiveTask, byte[] buffer, CancellationToken cancellationToken)
		{
			if (receiveTask != null)
			{
				if (receiveTask.Status != TaskStatus.RanToCompletion)
				{
					return receiveTask;
				}
				Task<WebSocketReceiveResult> task = receiveTask as Task<WebSocketReceiveResult>;
				if (task != null && task.Result.MessageType == WebSocketMessageType.Close)
				{
					return receiveTask;
				}
			}
			receiveTask = this.ReceiveAsyncPrivate<ManagedWebSocket.WebSocketReceiveResultGetter, WebSocketReceiveResult>(new ArraySegment<byte>(buffer), cancellationToken, default(ManagedWebSocket.WebSocketReceiveResultGetter)).AsTask();
			return receiveTask;
		}

		// Token: 0x06004005 RID: 16389 RVA: 0x000DABCC File Offset: 0x000D8DCC
		// Note: this type is marked as 'beforefieldinit'.
		static ManagedWebSocket()
		{
		}

		// Token: 0x04002637 RID: 9783
		private static readonly RandomNumberGenerator s_random = RandomNumberGenerator.Create();

		// Token: 0x04002638 RID: 9784
		private static readonly UTF8Encoding s_textEncoding = new UTF8Encoding(false, true);

		// Token: 0x04002639 RID: 9785
		private static readonly WebSocketState[] s_validSendStates = new WebSocketState[]
		{
			WebSocketState.Open,
			WebSocketState.CloseReceived
		};

		// Token: 0x0400263A RID: 9786
		private static readonly WebSocketState[] s_validReceiveStates = new WebSocketState[]
		{
			WebSocketState.Open,
			WebSocketState.CloseSent
		};

		// Token: 0x0400263B RID: 9787
		private static readonly WebSocketState[] s_validCloseOutputStates = new WebSocketState[]
		{
			WebSocketState.Open,
			WebSocketState.CloseReceived
		};

		// Token: 0x0400263C RID: 9788
		private static readonly WebSocketState[] s_validCloseStates = new WebSocketState[]
		{
			WebSocketState.Open,
			WebSocketState.CloseReceived,
			WebSocketState.CloseSent
		};

		// Token: 0x0400263D RID: 9789
		private static readonly Task<WebSocketReceiveResult> s_cachedCloseTask = Task.FromResult<WebSocketReceiveResult>(new WebSocketReceiveResult(0, WebSocketMessageType.Close, true));

		// Token: 0x0400263E RID: 9790
		internal const int MaxMessageHeaderLength = 14;

		// Token: 0x0400263F RID: 9791
		private const int MaxControlPayloadLength = 125;

		// Token: 0x04002640 RID: 9792
		private const int MaskLength = 4;

		// Token: 0x04002641 RID: 9793
		private readonly Stream _stream;

		// Token: 0x04002642 RID: 9794
		private readonly bool _isServer;

		// Token: 0x04002643 RID: 9795
		private readonly string _subprotocol;

		// Token: 0x04002644 RID: 9796
		private readonly Timer _keepAliveTimer;

		// Token: 0x04002645 RID: 9797
		private readonly CancellationTokenSource _abortSource = new CancellationTokenSource();

		// Token: 0x04002646 RID: 9798
		private Memory<byte> _receiveBuffer;

		// Token: 0x04002647 RID: 9799
		private readonly ManagedWebSocket.Utf8MessageState _utf8TextState = new ManagedWebSocket.Utf8MessageState();

		// Token: 0x04002648 RID: 9800
		private readonly SemaphoreSlim _sendFrameAsyncLock = new SemaphoreSlim(1, 1);

		// Token: 0x04002649 RID: 9801
		private WebSocketState _state = WebSocketState.Open;

		// Token: 0x0400264A RID: 9802
		private bool _disposed;

		// Token: 0x0400264B RID: 9803
		private bool _sentCloseFrame;

		// Token: 0x0400264C RID: 9804
		private bool _receivedCloseFrame;

		// Token: 0x0400264D RID: 9805
		private WebSocketCloseStatus? _closeStatus;

		// Token: 0x0400264E RID: 9806
		private string _closeStatusDescription;

		// Token: 0x0400264F RID: 9807
		private ManagedWebSocket.MessageHeader _lastReceiveHeader = new ManagedWebSocket.MessageHeader
		{
			Opcode = ManagedWebSocket.MessageOpcode.Text,
			Fin = true
		};

		// Token: 0x04002650 RID: 9808
		private int _receiveBufferOffset;

		// Token: 0x04002651 RID: 9809
		private int _receiveBufferCount;

		// Token: 0x04002652 RID: 9810
		private int _receivedMaskOffsetOffset;

		// Token: 0x04002653 RID: 9811
		private byte[] _sendBuffer;

		// Token: 0x04002654 RID: 9812
		private bool _lastSendWasFragment;

		// Token: 0x04002655 RID: 9813
		private Task _lastReceiveAsync = Task.CompletedTask;

		// Token: 0x020007CB RID: 1995
		private sealed class Utf8MessageState
		{
			// Token: 0x06004006 RID: 16390 RVA: 0x0000219B File Offset: 0x0000039B
			public Utf8MessageState()
			{
			}

			// Token: 0x04002656 RID: 9814
			internal bool SequenceInProgress;

			// Token: 0x04002657 RID: 9815
			internal int AdditionalBytesExpected;

			// Token: 0x04002658 RID: 9816
			internal int ExpectedValueMin;

			// Token: 0x04002659 RID: 9817
			internal int CurrentDecodeBits;
		}

		// Token: 0x020007CC RID: 1996
		private enum MessageOpcode : byte
		{
			// Token: 0x0400265B RID: 9819
			Continuation,
			// Token: 0x0400265C RID: 9820
			Text,
			// Token: 0x0400265D RID: 9821
			Binary,
			// Token: 0x0400265E RID: 9822
			Close = 8,
			// Token: 0x0400265F RID: 9823
			Ping,
			// Token: 0x04002660 RID: 9824
			Pong
		}

		// Token: 0x020007CD RID: 1997
		[StructLayout(LayoutKind.Auto)]
		private struct MessageHeader
		{
			// Token: 0x04002661 RID: 9825
			internal ManagedWebSocket.MessageOpcode Opcode;

			// Token: 0x04002662 RID: 9826
			internal bool Fin;

			// Token: 0x04002663 RID: 9827
			internal long PayloadLength;

			// Token: 0x04002664 RID: 9828
			internal int Mask;
		}

		// Token: 0x020007CE RID: 1998
		private interface IWebSocketReceiveResultGetter<TResult>
		{
			// Token: 0x06004007 RID: 16391
			TResult GetResult(int count, WebSocketMessageType messageType, bool endOfMessage, WebSocketCloseStatus? closeStatus, string closeDescription);
		}

		// Token: 0x020007CF RID: 1999
		private readonly struct WebSocketReceiveResultGetter : ManagedWebSocket.IWebSocketReceiveResultGetter<WebSocketReceiveResult>
		{
			// Token: 0x06004008 RID: 16392 RVA: 0x000DAC50 File Offset: 0x000D8E50
			public WebSocketReceiveResult GetResult(int count, WebSocketMessageType messageType, bool endOfMessage, WebSocketCloseStatus? closeStatus, string closeDescription)
			{
				return new WebSocketReceiveResult(count, messageType, endOfMessage, closeStatus, closeDescription);
			}
		}

		// Token: 0x020007D0 RID: 2000
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004009 RID: 16393 RVA: 0x000DAC5E File Offset: 0x000D8E5E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600400A RID: 16394 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c()
			{
			}

			// Token: 0x0600400B RID: 16395 RVA: 0x000DAC6C File Offset: 0x000D8E6C
			internal void <.ctor>b__36_0(object s)
			{
				ManagedWebSocket managedWebSocket = (ManagedWebSocket)s;
				object stateUpdateLock = managedWebSocket.StateUpdateLock;
				lock (stateUpdateLock)
				{
					WebSocketState state = managedWebSocket._state;
					if (state != WebSocketState.Closed && state != WebSocketState.Aborted)
					{
						managedWebSocket._state = ((state != WebSocketState.None && state != WebSocketState.Connecting) ? WebSocketState.Aborted : WebSocketState.Closed);
					}
				}
			}

			// Token: 0x0600400C RID: 16396 RVA: 0x000DACD0 File Offset: 0x000D8ED0
			internal void <.ctor>b__36_1(object s)
			{
				((ManagedWebSocket)s).SendKeepAliveFrameAsync();
			}

			// Token: 0x0600400D RID: 16397 RVA: 0x000DACDD File Offset: 0x000D8EDD
			internal void <SendFrameFallbackAsync>b__56_0(object s)
			{
				((ManagedWebSocket)s).Abort();
			}

			// Token: 0x0600400E RID: 16398 RVA: 0x000DACEA File Offset: 0x000D8EEA
			internal void <SendKeepAliveFrameAsync>b__58_0(Task p)
			{
				AggregateException exception = p.Exception;
			}

			// Token: 0x0600400F RID: 16399 RVA: 0x000DACDD File Offset: 0x000D8EDD
			internal void <WaitForServerToCloseConnectionAsync>b__63_0(object s)
			{
				((ManagedWebSocket)s).Abort();
			}

			// Token: 0x04002665 RID: 9829
			public static readonly ManagedWebSocket.<>c <>9 = new ManagedWebSocket.<>c();

			// Token: 0x04002666 RID: 9830
			public static Action<object> <>9__36_0;

			// Token: 0x04002667 RID: 9831
			public static TimerCallback <>9__36_1;

			// Token: 0x04002668 RID: 9832
			public static Action<object> <>9__56_0;

			// Token: 0x04002669 RID: 9833
			public static Action<Task> <>9__58_0;

			// Token: 0x0400266A RID: 9834
			public static Action<object> <>9__63_0;
		}

		// Token: 0x020007D1 RID: 2001
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WaitForWriteTaskAsync>d__55 : IAsyncStateMachine
		{
			// Token: 0x06004010 RID: 16400 RVA: 0x000DACF4 File Offset: 0x000D8EF4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ManagedWebSocket managedWebSocket = this.<>4__this;
				try
				{
					try
					{
						ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = this.writeTask.ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, ManagedWebSocket.<WaitForWriteTaskAsync>d__55>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						awaiter.GetResult();
					}
					catch (Exception ex) when (!(ex is OperationCanceledException))
					{
						throw (managedWebSocket._state == WebSocketState.Aborted) ? ManagedWebSocket.CreateOperationCanceledException(ex, default(CancellationToken)) : new WebSocketException(WebSocketError.ConnectionClosedPrematurely, ex);
					}
					finally
					{
						if (num < 0)
						{
							managedWebSocket._sendFrameAsyncLock.Release();
							managedWebSocket.ReleaseSendBuffer();
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06004011 RID: 16401 RVA: 0x000DAE30 File Offset: 0x000D9030
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400266B RID: 9835
			public int <>1__state;

			// Token: 0x0400266C RID: 9836
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400266D RID: 9837
			public ValueTask writeTask;

			// Token: 0x0400266E RID: 9838
			public ManagedWebSocket <>4__this;

			// Token: 0x0400266F RID: 9839
			private ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__1;
		}

		// Token: 0x020007D2 RID: 2002
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SendFrameFallbackAsync>d__56 : IAsyncStateMachine
		{
			// Token: 0x06004012 RID: 16402 RVA: 0x000DAE40 File Offset: 0x000D9040
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ManagedWebSocket managedWebSocket = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							goto IL_7E;
						}
						awaiter = managedWebSocket._sendFrameAsyncLock.WaitAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ManagedWebSocket.<SendFrameFallbackAsync>d__56>(ref awaiter, ref this);
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
					IL_7E:
					try
					{
						int length;
						if (num != 1)
						{
							length = managedWebSocket.WriteFrameToSendBuffer(this.opcode, this.endOfMessage, this.payloadBuffer.Span);
							this.<>7__wrap1 = this.cancellationToken.Register(new Action<object>(ManagedWebSocket.<>c.<>9.<SendFrameFallbackAsync>b__56_0), managedWebSocket);
						}
						try
						{
							ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter awaiter2;
							if (num != 1)
							{
								awaiter2 = managedWebSocket._stream.WriteAsync(new ReadOnlyMemory<byte>(managedWebSocket._sendBuffer, 0, length), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (this.<>1__state = 1);
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, ManagedWebSocket.<SendFrameFallbackAsync>d__56>(ref awaiter2, ref this);
									return;
								}
							}
							else
							{
								awaiter2 = this.<>u__2;
								this.<>u__2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
								num = (this.<>1__state = -1);
							}
							awaiter2.GetResult();
						}
						finally
						{
							if (num < 0)
							{
								((IDisposable)this.<>7__wrap1).Dispose();
							}
						}
						this.<>7__wrap1 = default(CancellationTokenRegistration);
					}
					catch (Exception ex) when (!(ex is OperationCanceledException))
					{
						throw (managedWebSocket._state == WebSocketState.Aborted) ? ManagedWebSocket.CreateOperationCanceledException(ex, this.cancellationToken) : new WebSocketException(WebSocketError.ConnectionClosedPrematurely, ex);
					}
					finally
					{
						if (num < 0)
						{
							managedWebSocket._sendFrameAsyncLock.Release();
							managedWebSocket.ReleaseSendBuffer();
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06004013 RID: 16403 RVA: 0x000DB0C0 File Offset: 0x000D92C0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002670 RID: 9840
			public int <>1__state;

			// Token: 0x04002671 RID: 9841
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04002672 RID: 9842
			public ManagedWebSocket <>4__this;

			// Token: 0x04002673 RID: 9843
			public ManagedWebSocket.MessageOpcode opcode;

			// Token: 0x04002674 RID: 9844
			public bool endOfMessage;

			// Token: 0x04002675 RID: 9845
			public ReadOnlyMemory<byte> payloadBuffer;

			// Token: 0x04002676 RID: 9846
			public CancellationToken cancellationToken;

			// Token: 0x04002677 RID: 9847
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04002678 RID: 9848
			private CancellationTokenRegistration <>7__wrap1;

			// Token: 0x04002679 RID: 9849
			private ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__2;
		}

		// Token: 0x020007D3 RID: 2003
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult> where TWebSocketReceiveResultGetter : struct, ManagedWebSocket.IWebSocketReceiveResultGetter<TWebSocketReceiveResult>
		{
			// Token: 0x06004014 RID: 16404 RVA: 0x000DB0CE File Offset: 0x000D92CE
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__61()
			{
			}

			// Token: 0x06004015 RID: 16405 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__61()
			{
			}

			// Token: 0x06004016 RID: 16406 RVA: 0x000DACDD File Offset: 0x000D8EDD
			internal void <ReceiveAsyncPrivate>b__61_0(object s)
			{
				((ManagedWebSocket)s).Abort();
			}

			// Token: 0x0400267A RID: 9850
			public static readonly ManagedWebSocket.<>c__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult> <>9 = new ManagedWebSocket.<>c__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult>();

			// Token: 0x0400267B RID: 9851
			public static Action<object> <>9__61_0;
		}

		// Token: 0x020007D4 RID: 2004
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReceiveAsyncPrivate>d__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult> : IAsyncStateMachine where TWebSocketReceiveResultGetter : struct, ManagedWebSocket.IWebSocketReceiveResultGetter<TWebSocketReceiveResult>
		{
			// Token: 0x06004017 RID: 16407 RVA: 0x000DB0DC File Offset: 0x000D92DC
			unsafe void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ManagedWebSocket managedWebSocket = this.<>4__this;
				TWebSocketReceiveResult result;
				try
				{
					if (num > 6)
					{
						this.<registration>5__2 = this.cancellationToken.Register(new Action<object>(ManagedWebSocket.<>c__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult>.<>9.<ReceiveAsyncPrivate>b__61_0), managedWebSocket);
					}
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter awaiter2;
						switch (num)
						{
						case 0:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							break;
						case 1:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_1D5;
						case 2:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_252;
						case 3:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_2EB;
						case 4:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_375;
						case 5:
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_56D;
						case 6:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_6D2;
						default:
							IL_66:
							this.<header>5__3 = managedWebSocket._lastReceiveHeader;
							if (this.<header>5__3.PayloadLength != 0L)
							{
								goto IL_260;
							}
							if (managedWebSocket._receiveBufferCount >= (managedWebSocket._isServer ? 14 : 10))
							{
								goto IL_1DC;
							}
							if (managedWebSocket._receiveBufferCount >= 2)
							{
								goto IL_114;
							}
							awaiter = managedWebSocket.EnsureBufferContainsAsync(2, this.cancellationToken, true).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ManagedWebSocket.<ReceiveAsyncPrivate>d__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult>>(ref awaiter, ref this);
								return;
							}
							break;
						}
						awaiter.GetResult();
						IL_114:
						long num2 = (long)(*managedWebSocket._receiveBuffer.Span[managedWebSocket._receiveBufferOffset + 1] & 127);
						if (!managedWebSocket._isServer && num2 <= 125L)
						{
							goto IL_1DC;
						}
						int minimumRequiredBytes = 2 + (managedWebSocket._isServer ? 4 : 0) + ((num2 <= 125L) ? 0 : ((num2 == 126L) ? 2 : 8));
						awaiter = managedWebSocket.EnsureBufferContainsAsync(minimumRequiredBytes, this.cancellationToken, true).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 1);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ManagedWebSocket.<ReceiveAsyncPrivate>d__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult>>(ref awaiter, ref this);
							return;
						}
						IL_1D5:
						awaiter.GetResult();
						IL_1DC:
						if (managedWebSocket.TryParseMessageHeaderFromReceiveBuffer(out this.<header>5__3))
						{
							goto IL_259;
						}
						awaiter = managedWebSocket.CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus.ProtocolError, WebSocketError.Faulted, null).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 2);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ManagedWebSocket.<ReceiveAsyncPrivate>d__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult>>(ref awaiter, ref this);
							return;
						}
						IL_252:
						awaiter.GetResult();
						IL_259:
						managedWebSocket._receivedMaskOffsetOffset = 0;
						IL_260:
						if (this.<header>5__3.Opcode == ManagedWebSocket.MessageOpcode.Ping || this.<header>5__3.Opcode == ManagedWebSocket.MessageOpcode.Pong)
						{
							awaiter = managedWebSocket.HandleReceivedPingPongAsync(this.<header>5__3, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 3);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ManagedWebSocket.<ReceiveAsyncPrivate>d__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult>>(ref awaiter, ref this);
								return;
							}
						}
						else if (this.<header>5__3.Opcode == ManagedWebSocket.MessageOpcode.Close)
						{
							awaiter = managedWebSocket.HandleReceivedCloseAsync(this.<header>5__3, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 4);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ManagedWebSocket.<ReceiveAsyncPrivate>d__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult>>(ref awaiter, ref this);
								return;
							}
							goto IL_375;
						}
						else
						{
							if (this.<header>5__3.Opcode == ManagedWebSocket.MessageOpcode.Continuation)
							{
								this.<header>5__3.Opcode = managedWebSocket._lastReceiveHeader.Opcode;
							}
							if (this.<header>5__3.PayloadLength == 0L || this.payloadBuffer.Length == 0)
							{
								managedWebSocket._lastReceiveHeader = this.<header>5__3;
								result = this.resultGetter.GetResult(0, (this.<header>5__3.Opcode == ManagedWebSocket.MessageOpcode.Text) ? WebSocketMessageType.Text : WebSocketMessageType.Binary, this.<header>5__3.Fin && this.<header>5__3.PayloadLength == 0L, null, null);
								goto IL_7BE;
							}
							this.<totalBytesReceived>5__4 = 0;
							if (managedWebSocket._receiveBufferCount > 0)
							{
								int num3 = Math.Min(this.payloadBuffer.Length, (int)Math.Min(this.<header>5__3.PayloadLength, (long)managedWebSocket._receiveBufferCount));
								managedWebSocket._receiveBuffer.Span.Slice(managedWebSocket._receiveBufferOffset, num3).CopyTo(this.payloadBuffer.Span);
								managedWebSocket.ConsumeFromBuffer(num3);
								this.<totalBytesReceived>5__4 += num3;
								goto IL_593;
							}
							goto IL_593;
						}
						IL_2EB:
						awaiter.GetResult();
						goto IL_66;
						IL_375:
						awaiter.GetResult();
						result = this.resultGetter.GetResult(0, WebSocketMessageType.Close, true, managedWebSocket._closeStatus, managedWebSocket._closeStatusDescription);
						goto IL_7BE;
						IL_56D:
						int result2 = awaiter2.GetResult();
						if (result2 <= 0)
						{
							managedWebSocket.ThrowIfEOFUnexpected(true);
							goto IL_5BD;
						}
						this.<totalBytesReceived>5__4 += result2;
						IL_593:
						if (this.<totalBytesReceived>5__4 < this.payloadBuffer.Length && (long)this.<totalBytesReceived>5__4 < this.<header>5__3.PayloadLength)
						{
							awaiter2 = managedWebSocket._stream.ReadAsync(this.payloadBuffer.Slice(this.<totalBytesReceived>5__4, (int)Math.Min((long)this.payloadBuffer.Length, this.<header>5__3.PayloadLength) - this.<totalBytesReceived>5__4), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 5);
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, ManagedWebSocket.<ReceiveAsyncPrivate>d__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult>>(ref awaiter2, ref this);
								return;
							}
							goto IL_56D;
						}
						IL_5BD:
						if (managedWebSocket._isServer)
						{
							managedWebSocket._receivedMaskOffsetOffset = ManagedWebSocket.ApplyMask(this.payloadBuffer.Span.Slice(0, this.<totalBytesReceived>5__4), this.<header>5__3.Mask, managedWebSocket._receivedMaskOffsetOffset);
						}
						this.<header>5__3.PayloadLength = this.<header>5__3.PayloadLength - (long)this.<totalBytesReceived>5__4;
						if (this.<header>5__3.Opcode != ManagedWebSocket.MessageOpcode.Text || ManagedWebSocket.TryValidateUtf8(this.payloadBuffer.Span.Slice(0, this.<totalBytesReceived>5__4), this.<header>5__3.Fin && this.<header>5__3.PayloadLength == 0L, managedWebSocket._utf8TextState))
						{
							goto IL_6D9;
						}
						awaiter = managedWebSocket.CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus.InvalidPayloadData, WebSocketError.Faulted, null).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 6);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ManagedWebSocket.<ReceiveAsyncPrivate>d__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult>>(ref awaiter, ref this);
							return;
						}
						IL_6D2:
						awaiter.GetResult();
						IL_6D9:
						managedWebSocket._lastReceiveHeader = this.<header>5__3;
						result = this.resultGetter.GetResult(this.<totalBytesReceived>5__4, (this.<header>5__3.Opcode == ManagedWebSocket.MessageOpcode.Text) ? WebSocketMessageType.Text : WebSocketMessageType.Binary, this.<header>5__3.Fin && this.<header>5__3.PayloadLength == 0L, null, null);
					}
					catch (Exception ex) when (!(ex is OperationCanceledException))
					{
						if (managedWebSocket._state == WebSocketState.Aborted)
						{
							throw new OperationCanceledException("Aborted", ex);
						}
						managedWebSocket._abortSource.Cancel();
						throw new WebSocketException(WebSocketError.ConnectionClosedPrematurely, ex);
					}
					finally
					{
						if (num < 0)
						{
							this.<registration>5__2.Dispose();
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<registration>5__2 = default(CancellationTokenRegistration);
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_7BE:
				this.<>1__state = -2;
				this.<registration>5__2 = default(CancellationTokenRegistration);
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06004018 RID: 16408 RVA: 0x000DB914 File Offset: 0x000D9B14
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400267C RID: 9852
			public int <>1__state;

			// Token: 0x0400267D RID: 9853
			public AsyncValueTaskMethodBuilder<TWebSocketReceiveResult> <>t__builder;

			// Token: 0x0400267E RID: 9854
			public CancellationToken cancellationToken;

			// Token: 0x0400267F RID: 9855
			public ManagedWebSocket <>4__this;

			// Token: 0x04002680 RID: 9856
			public TWebSocketReceiveResultGetter resultGetter;

			// Token: 0x04002681 RID: 9857
			public Memory<byte> payloadBuffer;

			// Token: 0x04002682 RID: 9858
			private CancellationTokenRegistration <registration>5__2;

			// Token: 0x04002683 RID: 9859
			private ManagedWebSocket.MessageHeader <header>5__3;

			// Token: 0x04002684 RID: 9860
			private int <totalBytesReceived>5__4;

			// Token: 0x04002685 RID: 9861
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04002686 RID: 9862
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__2;
		}

		// Token: 0x020007D5 RID: 2005
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <HandleReceivedCloseAsync>d__62 : IAsyncStateMachine
		{
			// Token: 0x06004019 RID: 16409 RVA: 0x000DB924 File Offset: 0x000D9B24
			unsafe void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ManagedWebSocket managedWebSocket = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					object stateUpdateLock;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
						goto IL_193;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
						goto IL_290;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
						goto IL_369;
					case 4:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
						goto IL_413;
					default:
					{
						stateUpdateLock = managedWebSocket.StateUpdateLock;
						bool flag = false;
						try
						{
							Monitor.Enter(stateUpdateLock, ref flag);
							managedWebSocket._receivedCloseFrame = true;
							if (managedWebSocket._state < WebSocketState.CloseReceived)
							{
								managedWebSocket._state = WebSocketState.CloseReceived;
							}
						}
						finally
						{
							if (num < 0 && flag)
							{
								Monitor.Exit(stateUpdateLock);
							}
						}
						this.<closeStatus>5__2 = WebSocketCloseStatus.NormalClosure;
						this.<closeStatusDescription>5__3 = string.Empty;
						if (this.header.PayloadLength == 1L)
						{
							awaiter = managedWebSocket.CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus.ProtocolError, WebSocketError.Faulted, null).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ManagedWebSocket.<HandleReceivedCloseAsync>d__62>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							if (this.header.PayloadLength < 2L)
							{
								goto IL_382;
							}
							if ((long)managedWebSocket._receiveBufferCount >= this.header.PayloadLength)
							{
								goto IL_19A;
							}
							awaiter = managedWebSocket.EnsureBufferContainsAsync((int)this.header.PayloadLength, this.cancellationToken, true).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 1);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ManagedWebSocket.<HandleReceivedCloseAsync>d__62>(ref awaiter, ref this);
								return;
							}
							goto IL_193;
						}
						break;
					}
					}
					awaiter.GetResult();
					goto IL_382;
					IL_193:
					awaiter.GetResult();
					IL_19A:
					if (managedWebSocket._isServer)
					{
						ManagedWebSocket.ApplyMask(managedWebSocket._receiveBuffer.Span.Slice(managedWebSocket._receiveBufferOffset, (int)this.header.PayloadLength), this.header.Mask, 0);
					}
					this.<closeStatus>5__2 = (WebSocketCloseStatus)((int)(*managedWebSocket._receiveBuffer.Span[managedWebSocket._receiveBufferOffset]) << 8 | (int)(*managedWebSocket._receiveBuffer.Span[managedWebSocket._receiveBufferOffset + 1]));
					if (ManagedWebSocket.IsValidCloseStatus(this.<closeStatus>5__2))
					{
						goto IL_297;
					}
					awaiter = managedWebSocket.CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus.ProtocolError, WebSocketError.Faulted, null).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (this.<>1__state = 2);
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ManagedWebSocket.<HandleReceivedCloseAsync>d__62>(ref awaiter, ref this);
						return;
					}
					IL_290:
					awaiter.GetResult();
					IL_297:
					if (this.header.PayloadLength <= 2L)
					{
						goto IL_370;
					}
					int num2 = 0;
					try
					{
						this.<closeStatusDescription>5__3 = ManagedWebSocket.s_textEncoding.GetString(managedWebSocket._receiveBuffer.Span.Slice(managedWebSocket._receiveBufferOffset + 2, (int)this.header.PayloadLength - 2));
					}
					catch (DecoderFallbackException stateUpdateLock)
					{
						num2 = 1;
					}
					if (num2 != 1)
					{
						goto IL_370;
					}
					DecoderFallbackException innerException = (DecoderFallbackException)stateUpdateLock;
					awaiter = managedWebSocket.CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus.ProtocolError, WebSocketError.Faulted, innerException).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (this.<>1__state = 3);
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ManagedWebSocket.<HandleReceivedCloseAsync>d__62>(ref awaiter, ref this);
						return;
					}
					IL_369:
					awaiter.GetResult();
					IL_370:
					managedWebSocket.ConsumeFromBuffer((int)this.header.PayloadLength);
					IL_382:
					managedWebSocket._closeStatus = new WebSocketCloseStatus?(this.<closeStatus>5__2);
					managedWebSocket._closeStatusDescription = this.<closeStatusDescription>5__3;
					if (managedWebSocket._isServer || !managedWebSocket._sentCloseFrame)
					{
						goto IL_41A;
					}
					awaiter = managedWebSocket.WaitForServerToCloseConnectionAsync(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (this.<>1__state = 4);
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ManagedWebSocket.<HandleReceivedCloseAsync>d__62>(ref awaiter, ref this);
						return;
					}
					IL_413:
					awaiter.GetResult();
					IL_41A:;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<closeStatusDescription>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<closeStatusDescription>5__3 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600401A RID: 16410 RVA: 0x000DBDD4 File Offset: 0x000D9FD4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002687 RID: 9863
			public int <>1__state;

			// Token: 0x04002688 RID: 9864
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04002689 RID: 9865
			public ManagedWebSocket <>4__this;

			// Token: 0x0400268A RID: 9866
			public ManagedWebSocket.MessageHeader header;

			// Token: 0x0400268B RID: 9867
			public CancellationToken cancellationToken;

			// Token: 0x0400268C RID: 9868
			private WebSocketCloseStatus <closeStatus>5__2;

			// Token: 0x0400268D RID: 9869
			private string <closeStatusDescription>5__3;

			// Token: 0x0400268E RID: 9870
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020007D6 RID: 2006
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WaitForServerToCloseConnectionAsync>d__63 : IAsyncStateMachine
		{
			// Token: 0x0600401B RID: 16411 RVA: 0x000DBDE4 File Offset: 0x000D9FE4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ManagedWebSocket managedWebSocket = this.<>4__this;
				try
				{
					ValueTask<int> valueTask;
					if (num != 0)
					{
						valueTask = managedWebSocket._stream.ReadAsync(managedWebSocket._receiveBuffer, this.cancellationToken);
						if (valueTask.IsCompletedSuccessfully)
						{
							goto IL_138;
						}
						this.<finalCts>5__2 = new CancellationTokenSource(1000);
					}
					try
					{
						if (num != 0)
						{
							this.<>7__wrap2 = this.<finalCts>5__2.Token.Register(new Action<object>(ManagedWebSocket.<>c.<>9.<WaitForServerToCloseConnectionAsync>b__63_0), managedWebSocket);
						}
						try
						{
							ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter awaiter;
							if (num != 0)
							{
								awaiter = valueTask.ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 0);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, ManagedWebSocket.<WaitForServerToCloseConnectionAsync>d__63>(ref awaiter, ref this);
									return;
								}
							}
							else
							{
								awaiter = this.<>u__1;
								this.<>u__1 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
								num = (this.<>1__state = -1);
							}
							awaiter.GetResult();
						}
						catch
						{
						}
						finally
						{
							if (num < 0)
							{
								((IDisposable)this.<>7__wrap2).Dispose();
							}
						}
						this.<>7__wrap2 = default(CancellationTokenRegistration);
					}
					finally
					{
						if (num < 0 && this.<finalCts>5__2 != null)
						{
							((IDisposable)this.<finalCts>5__2).Dispose();
						}
					}
					this.<finalCts>5__2 = null;
					IL_138:;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600401C RID: 16412 RVA: 0x000DBFBC File Offset: 0x000DA1BC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400268F RID: 9871
			public int <>1__state;

			// Token: 0x04002690 RID: 9872
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04002691 RID: 9873
			public ManagedWebSocket <>4__this;

			// Token: 0x04002692 RID: 9874
			public CancellationToken cancellationToken;

			// Token: 0x04002693 RID: 9875
			private CancellationTokenSource <finalCts>5__2;

			// Token: 0x04002694 RID: 9876
			private CancellationTokenRegistration <>7__wrap2;

			// Token: 0x04002695 RID: 9877
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__1;
		}

		// Token: 0x020007D7 RID: 2007
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <HandleReceivedPingPongAsync>d__64 : IAsyncStateMachine
		{
			// Token: 0x0600401D RID: 16413 RVA: 0x000DBFCC File Offset: 0x000DA1CC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ManagedWebSocket managedWebSocket = this.<>4__this;
				try
				{
					ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
							this.<>1__state = -1;
							goto IL_19B;
						}
						if (this.header.PayloadLength <= 0L || (long)managedWebSocket._receiveBufferCount >= this.header.PayloadLength)
						{
							goto IL_B8;
						}
						awaiter2 = managedWebSocket.EnsureBufferContainsAsync((int)this.header.PayloadLength, this.cancellationToken, true).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ManagedWebSocket.<HandleReceivedPingPongAsync>d__64>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter2.GetResult();
					IL_B8:
					if (this.header.Opcode != ManagedWebSocket.MessageOpcode.Ping)
					{
						goto IL_1A2;
					}
					if (managedWebSocket._isServer)
					{
						ManagedWebSocket.ApplyMask(managedWebSocket._receiveBuffer.Span.Slice(managedWebSocket._receiveBufferOffset, (int)this.header.PayloadLength), this.header.Mask, 0);
					}
					awaiter = managedWebSocket.SendFrameAsync(ManagedWebSocket.MessageOpcode.Pong, true, managedWebSocket._receiveBuffer.Slice(managedWebSocket._receiveBufferOffset, (int)this.header.PayloadLength), default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, ManagedWebSocket.<HandleReceivedPingPongAsync>d__64>(ref awaiter, ref this);
						return;
					}
					IL_19B:
					awaiter.GetResult();
					IL_1A2:
					if (this.header.PayloadLength > 0L)
					{
						managedWebSocket.ConsumeFromBuffer((int)this.header.PayloadLength);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600401E RID: 16414 RVA: 0x000DC1E8 File Offset: 0x000DA3E8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002696 RID: 9878
			public int <>1__state;

			// Token: 0x04002697 RID: 9879
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04002698 RID: 9880
			public ManagedWebSocket.MessageHeader header;

			// Token: 0x04002699 RID: 9881
			public ManagedWebSocket <>4__this;

			// Token: 0x0400269A RID: 9882
			public CancellationToken cancellationToken;

			// Token: 0x0400269B RID: 9883
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x0400269C RID: 9884
			private ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__2;
		}

		// Token: 0x020007D8 RID: 2008
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <CloseWithReceiveErrorAndThrowAsync>d__66 : IAsyncStateMachine
		{
			// Token: 0x0600401F RID: 16415 RVA: 0x000DC1F8 File Offset: 0x000DA3F8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ManagedWebSocket managedWebSocket = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (managedWebSocket._sentCloseFrame)
						{
							goto IL_8F;
						}
						awaiter = managedWebSocket.CloseOutputAsync(this.closeStatus, string.Empty, default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ManagedWebSocket.<CloseWithReceiveErrorAndThrowAsync>d__66>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					IL_8F:
					managedWebSocket._receiveBufferCount = 0;
					throw new WebSocketException(this.error, this.innerException);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
				}
			}

			// Token: 0x06004020 RID: 16416 RVA: 0x000DC2D8 File Offset: 0x000DA4D8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400269D RID: 9885
			public int <>1__state;

			// Token: 0x0400269E RID: 9886
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400269F RID: 9887
			public ManagedWebSocket <>4__this;

			// Token: 0x040026A0 RID: 9888
			public WebSocketCloseStatus closeStatus;

			// Token: 0x040026A1 RID: 9889
			public WebSocketError error;

			// Token: 0x040026A2 RID: 9890
			public Exception innerException;

			// Token: 0x040026A3 RID: 9891
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020007D9 RID: 2009
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <CloseAsyncPrivate>d__68 : IAsyncStateMachine
		{
			// Token: 0x06004021 RID: 16417 RVA: 0x000DC2E8 File Offset: 0x000DA4E8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ManagedWebSocket managedWebSocket = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							goto IL_AB;
						}
						if (managedWebSocket._sentCloseFrame)
						{
							goto IL_96;
						}
						awaiter = managedWebSocket.SendCloseFrameAsync(this.closeStatus, this.statusDescription, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ManagedWebSocket.<CloseAsyncPrivate>d__68>(ref awaiter, ref this);
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
					IL_96:
					this.<closeBuffer>5__2 = ArrayPool<byte>.Shared.Rent(139);
					IL_AB:
					object obj;
					bool flag;
					try
					{
						if (num != 1)
						{
							goto IL_170;
						}
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
						IL_169:
						awaiter.GetResult();
						IL_170:
						if (!managedWebSocket._receivedCloseFrame)
						{
							obj = managedWebSocket.ReceiveAsyncLock;
							flag = false;
							Task task;
							try
							{
								Monitor.Enter(obj, ref flag);
								if (managedWebSocket._receivedCloseFrame)
								{
									goto IL_17B;
								}
								task = managedWebSocket._lastReceiveAsync;
								task = (managedWebSocket._lastReceiveAsync = managedWebSocket.ValidateAndReceiveAsync(task, this.<closeBuffer>5__2, this.cancellationToken));
							}
							finally
							{
								if (num < 0 && flag)
								{
									Monitor.Exit(obj);
								}
							}
							awaiter = task.ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 1);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ManagedWebSocket.<CloseAsyncPrivate>d__68>(ref awaiter, ref this);
								return;
							}
							goto IL_169;
						}
						IL_17B:;
					}
					finally
					{
						if (num < 0)
						{
							ArrayPool<byte>.Shared.Return(this.<closeBuffer>5__2, false);
						}
					}
					obj = managedWebSocket.StateUpdateLock;
					flag = false;
					try
					{
						Monitor.Enter(obj, ref flag);
						managedWebSocket.DisposeCore();
						if (managedWebSocket._state < WebSocketState.Closed)
						{
							managedWebSocket._state = WebSocketState.Closed;
						}
					}
					finally
					{
						if (num < 0 && flag)
						{
							Monitor.Exit(obj);
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<closeBuffer>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<closeBuffer>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06004022 RID: 16418 RVA: 0x000DC564 File Offset: 0x000DA764
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040026A4 RID: 9892
			public int <>1__state;

			// Token: 0x040026A5 RID: 9893
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040026A6 RID: 9894
			public ManagedWebSocket <>4__this;

			// Token: 0x040026A7 RID: 9895
			public WebSocketCloseStatus closeStatus;

			// Token: 0x040026A8 RID: 9896
			public string statusDescription;

			// Token: 0x040026A9 RID: 9897
			public CancellationToken cancellationToken;

			// Token: 0x040026AA RID: 9898
			private byte[] <closeBuffer>5__2;

			// Token: 0x040026AB RID: 9899
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020007DA RID: 2010
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SendCloseFrameAsync>d__69 : IAsyncStateMachine
		{
			// Token: 0x06004023 RID: 16419 RVA: 0x000DC574 File Offset: 0x000DA774
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ManagedWebSocket managedWebSocket = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_20B;
						}
						this.<buffer>5__2 = null;
					}
					try
					{
						ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter awaiter2;
						if (num != 0)
						{
							int num2 = 2;
							if (string.IsNullOrEmpty(this.closeStatusDescription))
							{
								this.<buffer>5__2 = ArrayPool<byte>.Shared.Rent(num2);
							}
							else
							{
								num2 += ManagedWebSocket.s_textEncoding.GetByteCount(this.closeStatusDescription);
								this.<buffer>5__2 = ArrayPool<byte>.Shared.Rent(num2);
								ManagedWebSocket.s_textEncoding.GetBytes(this.closeStatusDescription, 0, this.closeStatusDescription.Length, this.<buffer>5__2, 2);
							}
							ushort num3 = (ushort)this.closeStatus;
							this.<buffer>5__2[0] = (byte)(num3 >> 8);
							this.<buffer>5__2[1] = (byte)(num3 & 255);
							awaiter2 = managedWebSocket.SendFrameAsync(ManagedWebSocket.MessageOpcode.Close, true, new Memory<byte>(this.<buffer>5__2, 0, num2), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, ManagedWebSocket.<SendCloseFrameAsync>d__69>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = this.<>u__1;
							this.<>u__1 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						awaiter2.GetResult();
					}
					finally
					{
						if (num < 0 && this.<buffer>5__2 != null)
						{
							ArrayPool<byte>.Shared.Return(this.<buffer>5__2, false);
						}
					}
					object stateUpdateLock = managedWebSocket.StateUpdateLock;
					bool flag = false;
					try
					{
						Monitor.Enter(stateUpdateLock, ref flag);
						managedWebSocket._sentCloseFrame = true;
						if (managedWebSocket._state <= WebSocketState.CloseReceived)
						{
							managedWebSocket._state = WebSocketState.CloseSent;
						}
					}
					finally
					{
						if (num < 0 && flag)
						{
							Monitor.Exit(stateUpdateLock);
						}
					}
					if (managedWebSocket._isServer || !managedWebSocket._receivedCloseFrame)
					{
						goto IL_212;
					}
					awaiter = managedWebSocket.WaitForServerToCloseConnectionAsync(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (this.<>1__state = 1);
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ManagedWebSocket.<SendCloseFrameAsync>d__69>(ref awaiter, ref this);
						return;
					}
					IL_20B:
					awaiter.GetResult();
					IL_212:;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<buffer>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<buffer>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06004024 RID: 16420 RVA: 0x000DC81C File Offset: 0x000DAA1C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040026AC RID: 9900
			public int <>1__state;

			// Token: 0x040026AD RID: 9901
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040026AE RID: 9902
			public string closeStatusDescription;

			// Token: 0x040026AF RID: 9903
			public WebSocketCloseStatus closeStatus;

			// Token: 0x040026B0 RID: 9904
			public ManagedWebSocket <>4__this;

			// Token: 0x040026B1 RID: 9905
			public CancellationToken cancellationToken;

			// Token: 0x040026B2 RID: 9906
			private byte[] <buffer>5__2;

			// Token: 0x040026B3 RID: 9907
			private ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__1;

			// Token: 0x040026B4 RID: 9908
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020007DB RID: 2011
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <EnsureBufferContainsAsync>d__71 : IAsyncStateMachine
		{
			// Token: 0x06004025 RID: 16421 RVA: 0x000DC82C File Offset: 0x000DAA2C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ManagedWebSocket managedWebSocket = this.<>4__this;
				try
				{
					ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter awaiter;
					if (num != 0)
					{
						if (managedWebSocket._receiveBufferCount < this.minimumRequiredBytes)
						{
							if (managedWebSocket._receiveBufferCount > 0)
							{
								managedWebSocket._receiveBuffer.Span.Slice(managedWebSocket._receiveBufferOffset, managedWebSocket._receiveBufferCount).CopyTo(managedWebSocket._receiveBuffer.Span);
							}
							managedWebSocket._receiveBufferOffset = 0;
							goto IL_127;
						}
						goto IL_138;
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
						this.<>1__state = -1;
					}
					IL_FF:
					int result = awaiter.GetResult();
					if (result <= 0)
					{
						managedWebSocket.ThrowIfEOFUnexpected(this.throwOnPrematureClosure);
						goto IL_138;
					}
					managedWebSocket._receiveBufferCount += result;
					IL_127:
					if (managedWebSocket._receiveBufferCount < this.minimumRequiredBytes)
					{
						awaiter = managedWebSocket._stream.ReadAsync(managedWebSocket._receiveBuffer.Slice(managedWebSocket._receiveBufferCount, managedWebSocket._receiveBuffer.Length - managedWebSocket._receiveBufferCount), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, ManagedWebSocket.<EnsureBufferContainsAsync>d__71>(ref awaiter, ref this);
							return;
						}
						goto IL_FF;
					}
					IL_138:;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06004026 RID: 16422 RVA: 0x000DC9BC File Offset: 0x000DABBC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040026B5 RID: 9909
			public int <>1__state;

			// Token: 0x040026B6 RID: 9910
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040026B7 RID: 9911
			public ManagedWebSocket <>4__this;

			// Token: 0x040026B8 RID: 9912
			public int minimumRequiredBytes;

			// Token: 0x040026B9 RID: 9913
			public CancellationToken cancellationToken;

			// Token: 0x040026BA RID: 9914
			public bool throwOnPrematureClosure;

			// Token: 0x040026BB RID: 9915
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__1;
		}
	}
}
