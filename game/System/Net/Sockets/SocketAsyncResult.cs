using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x020007C2 RID: 1986
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class SocketAsyncResult : IOAsyncResult
	{
		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06003F99 RID: 16281 RVA: 0x000D9033 File Offset: 0x000D7233
		public IntPtr Handle
		{
			get
			{
				if (this.socket == null)
				{
					return IntPtr.Zero;
				}
				return this.socket.Handle;
			}
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x000D904E File Offset: 0x000D724E
		public SocketAsyncResult()
		{
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x000D9058 File Offset: 0x000D7258
		public void Init(Socket socket, AsyncCallback callback, object state, SocketOperation operation)
		{
			base.Init(callback, state);
			this.socket = socket;
			this.operation = operation;
			this.DelayedException = null;
			this.EndPoint = null;
			this.Buffer = null;
			this.Offset = 0;
			this.Size = 0;
			this.SockFlags = SocketFlags.None;
			this.AcceptSocket = null;
			this.Addresses = null;
			this.Port = 0;
			this.Buffers = null;
			this.ReuseSocket = false;
			this.CurrentAddress = 0;
			this.AcceptedSocket = null;
			this.Total = 0;
			this.error = 0;
			this.EndCalled = 0;
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x000D90F1 File Offset: 0x000D72F1
		public SocketAsyncResult(Socket socket, AsyncCallback callback, object state, SocketOperation operation) : base(callback, state)
		{
			this.socket = socket;
			this.operation = operation;
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06003F9D RID: 16285 RVA: 0x000D910C File Offset: 0x000D730C
		public SocketError ErrorCode
		{
			get
			{
				SocketException ex = this.DelayedException as SocketException;
				if (ex != null)
				{
					return ex.SocketErrorCode;
				}
				if (this.error != 0)
				{
					return (SocketError)this.error;
				}
				return SocketError.Success;
			}
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x000D913F File Offset: 0x000D733F
		public void CheckIfThrowDelayedException()
		{
			if (this.DelayedException != null)
			{
				this.socket.is_connected = false;
				throw this.DelayedException;
			}
			if (this.error != 0)
			{
				this.socket.is_connected = false;
				throw new SocketException(this.error);
			}
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x000D917C File Offset: 0x000D737C
		internal override void CompleteDisposed()
		{
			this.Complete();
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x000D9184 File Offset: 0x000D7384
		public void Complete()
		{
			if (this.operation != SocketOperation.Receive && this.socket.CleanedUp)
			{
				this.DelayedException = new ObjectDisposedException(this.socket.GetType().ToString());
			}
			base.IsCompleted = true;
			Socket socket = this.socket;
			SocketOperation socketOperation = this.operation;
			if (!base.CompletedSynchronously && base.AsyncCallback != null)
			{
				ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
				{
					((SocketAsyncResult)state).AsyncCallback((SocketAsyncResult)state);
				}, this);
			}
			switch (socketOperation)
			{
			case SocketOperation.Accept:
			case SocketOperation.Receive:
			case SocketOperation.ReceiveFrom:
			case SocketOperation.ReceiveGeneric:
				socket.ReadSem.Release();
				return;
			case SocketOperation.Connect:
			case SocketOperation.RecvJustCallback:
			case SocketOperation.SendJustCallback:
			case SocketOperation.Disconnect:
			case SocketOperation.AcceptReceive:
				break;
			case SocketOperation.Send:
			case SocketOperation.SendTo:
			case SocketOperation.SendGeneric:
				socket.WriteSem.Release();
				break;
			default:
				return;
			}
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x000D925D File Offset: 0x000D745D
		public void Complete(bool synch)
		{
			base.CompletedSynchronously = synch;
			this.Complete();
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x000D926C File Offset: 0x000D746C
		public void Complete(int total)
		{
			this.Total = total;
			this.Complete();
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x000D927B File Offset: 0x000D747B
		public void Complete(Exception e, bool synch)
		{
			this.DelayedException = e;
			base.CompletedSynchronously = synch;
			this.Complete();
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x000D9291 File Offset: 0x000D7491
		public void Complete(Exception e)
		{
			this.DelayedException = e;
			this.Complete();
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x000D92A0 File Offset: 0x000D74A0
		public void Complete(Socket s)
		{
			this.AcceptedSocket = s;
			this.Complete();
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x000D92AF File Offset: 0x000D74AF
		public void Complete(Socket s, int total)
		{
			this.AcceptedSocket = s;
			this.Total = total;
			this.Complete();
		}

		// Token: 0x040025F2 RID: 9714
		public Socket socket;

		// Token: 0x040025F3 RID: 9715
		public SocketOperation operation;

		// Token: 0x040025F4 RID: 9716
		private Exception DelayedException;

		// Token: 0x040025F5 RID: 9717
		public EndPoint EndPoint;

		// Token: 0x040025F6 RID: 9718
		public Memory<byte> Buffer;

		// Token: 0x040025F7 RID: 9719
		public int Offset;

		// Token: 0x040025F8 RID: 9720
		public int Size;

		// Token: 0x040025F9 RID: 9721
		public SocketFlags SockFlags;

		// Token: 0x040025FA RID: 9722
		public Socket AcceptSocket;

		// Token: 0x040025FB RID: 9723
		public IPAddress[] Addresses;

		// Token: 0x040025FC RID: 9724
		public int Port;

		// Token: 0x040025FD RID: 9725
		public IList<ArraySegment<byte>> Buffers;

		// Token: 0x040025FE RID: 9726
		public bool ReuseSocket;

		// Token: 0x040025FF RID: 9727
		public int CurrentAddress;

		// Token: 0x04002600 RID: 9728
		public Socket AcceptedSocket;

		// Token: 0x04002601 RID: 9729
		public int Total;

		// Token: 0x04002602 RID: 9730
		internal int error;

		// Token: 0x04002603 RID: 9731
		public int EndCalled;

		// Token: 0x020007C3 RID: 1987
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003FA7 RID: 16295 RVA: 0x000D92C5 File Offset: 0x000D74C5
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003FA8 RID: 16296 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c()
			{
			}

			// Token: 0x06003FA9 RID: 16297 RVA: 0x000D92D1 File Offset: 0x000D74D1
			internal void <Complete>b__27_0(object state)
			{
				((SocketAsyncResult)state).AsyncCallback((SocketAsyncResult)state);
			}

			// Token: 0x04002604 RID: 9732
			public static readonly SocketAsyncResult.<>c <>9 = new SocketAsyncResult.<>c();

			// Token: 0x04002605 RID: 9733
			public static WaitCallback <>9__27_0;
		}
	}
}
