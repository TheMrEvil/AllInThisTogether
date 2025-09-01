using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x020006A0 RID: 1696
	internal class ListenerAsyncResult : IAsyncResult
	{
		// Token: 0x0600364B RID: 13899 RVA: 0x000BE44C File Offset: 0x000BC64C
		public ListenerAsyncResult(AsyncCallback cb, object state)
		{
			this.cb = cb;
			this.state = state;
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x000BE470 File Offset: 0x000BC670
		internal void Complete(Exception exc)
		{
			if (this.forward != null)
			{
				this.forward.Complete(exc);
				return;
			}
			this.exception = exc;
			if (this.InGet && exc is ObjectDisposedException)
			{
				this.exception = new HttpListenerException(500, "Listener closed");
			}
			object obj = this.locker;
			lock (obj)
			{
				this.completed = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
				if (this.cb != null)
				{
					ThreadPool.UnsafeQueueUserWorkItem(ListenerAsyncResult.InvokeCB, this);
				}
			}
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x000BE51C File Offset: 0x000BC71C
		private static void InvokeCallback(object o)
		{
			ListenerAsyncResult listenerAsyncResult = (ListenerAsyncResult)o;
			if (listenerAsyncResult.forward != null)
			{
				ListenerAsyncResult.InvokeCallback(listenerAsyncResult.forward);
				return;
			}
			try
			{
				listenerAsyncResult.cb(listenerAsyncResult);
			}
			catch
			{
			}
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000BE568 File Offset: 0x000BC768
		internal void Complete(HttpListenerContext context)
		{
			this.Complete(context, false);
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000BE574 File Offset: 0x000BC774
		internal void Complete(HttpListenerContext context, bool synch)
		{
			if (this.forward != null)
			{
				this.forward.Complete(context, synch);
				return;
			}
			this.synch = synch;
			this.context = context;
			object obj = this.locker;
			lock (obj)
			{
				AuthenticationSchemes authenticationSchemes = context.Listener.SelectAuthenticationScheme(context);
				if ((authenticationSchemes == AuthenticationSchemes.Basic || context.Listener.AuthenticationSchemes == AuthenticationSchemes.Negotiate) && context.Request.Headers["Authorization"] == null)
				{
					context.Response.StatusCode = 401;
					context.Response.Headers["WWW-Authenticate"] = authenticationSchemes.ToString() + " realm=\"" + context.Listener.Realm + "\"";
					context.Response.OutputStream.Close();
					IAsyncResult asyncResult = context.Listener.BeginGetContext(this.cb, this.state);
					this.forward = (ListenerAsyncResult)asyncResult;
					object obj2 = this.forward.locker;
					lock (obj2)
					{
						if (this.handle != null)
						{
							this.forward.handle = this.handle;
						}
					}
					ListenerAsyncResult listenerAsyncResult = this.forward;
					int num = 0;
					while (listenerAsyncResult.forward != null)
					{
						if (num > 20)
						{
							this.Complete(new HttpListenerException(400, "Too many authentication errors"));
						}
						listenerAsyncResult = listenerAsyncResult.forward;
						num++;
					}
				}
				else
				{
					this.completed = true;
					this.synch = false;
					if (this.handle != null)
					{
						this.handle.Set();
					}
					if (this.cb != null)
					{
						ThreadPool.UnsafeQueueUserWorkItem(ListenerAsyncResult.InvokeCB, this);
					}
				}
			}
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x000BE76C File Offset: 0x000BC96C
		internal HttpListenerContext GetContext()
		{
			if (this.forward != null)
			{
				return this.forward.GetContext();
			}
			if (this.exception != null)
			{
				throw this.exception;
			}
			return this.context;
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06003651 RID: 13905 RVA: 0x000BE797 File Offset: 0x000BC997
		public object AsyncState
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.AsyncState;
				}
				return this.state;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06003652 RID: 13906 RVA: 0x000BE7B4 File Offset: 0x000BC9B4
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.AsyncWaitHandle;
				}
				object obj = this.locker;
				lock (obj)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.completed);
					}
				}
				return this.handle;
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06003653 RID: 13907 RVA: 0x000BE824 File Offset: 0x000BCA24
		public bool CompletedSynchronously
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.CompletedSynchronously;
				}
				return this.synch;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06003654 RID: 13908 RVA: 0x000BE840 File Offset: 0x000BCA40
		public bool IsCompleted
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.IsCompleted;
				}
				object obj = this.locker;
				bool result;
				lock (obj)
				{
					result = this.completed;
				}
				return result;
			}
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x000BE898 File Offset: 0x000BCA98
		// Note: this type is marked as 'beforefieldinit'.
		static ListenerAsyncResult()
		{
		}

		// Token: 0x04001F99 RID: 8089
		private ManualResetEvent handle;

		// Token: 0x04001F9A RID: 8090
		private bool synch;

		// Token: 0x04001F9B RID: 8091
		private bool completed;

		// Token: 0x04001F9C RID: 8092
		private AsyncCallback cb;

		// Token: 0x04001F9D RID: 8093
		private object state;

		// Token: 0x04001F9E RID: 8094
		private Exception exception;

		// Token: 0x04001F9F RID: 8095
		private HttpListenerContext context;

		// Token: 0x04001FA0 RID: 8096
		private object locker = new object();

		// Token: 0x04001FA1 RID: 8097
		private ListenerAsyncResult forward;

		// Token: 0x04001FA2 RID: 8098
		internal bool EndCalled;

		// Token: 0x04001FA3 RID: 8099
		internal bool InGet;

		// Token: 0x04001FA4 RID: 8100
		private static WaitCallback InvokeCB = new WaitCallback(ListenerAsyncResult.InvokeCallback);
	}
}
