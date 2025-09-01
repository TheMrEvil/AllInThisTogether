using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Encapsulates the results of an asynchronous operation on a delegate.</summary>
	// Token: 0x0200060A RID: 1546
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncResult : IAsyncResult, IMessageSink, IThreadPoolWorkItem
	{
		// Token: 0x06003A7D RID: 14973 RVA: 0x0000259F File Offset: 0x0000079F
		internal AsyncResult()
		{
		}

		/// <summary>Gets the object provided as the last parameter of a <see langword="BeginInvoke" /> method call.</summary>
		/// <returns>The object provided as the last parameter of a <see langword="BeginInvoke" /> method call.</returns>
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06003A7E RID: 14974 RVA: 0x000CCFF4 File Offset: 0x000CB1F4
		public virtual object AsyncState
		{
			get
			{
				return this.async_state;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that encapsulates Win32 synchronization handles, and allows the implementation of various synchronization schemes.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that encapsulates Win32 synchronization handles, and allows the implementation of various synchronization schemes.</returns>
		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06003A7F RID: 14975 RVA: 0x000CCFFC File Offset: 0x000CB1FC
		public virtual WaitHandle AsyncWaitHandle
		{
			get
			{
				WaitHandle result;
				lock (this)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.completed);
					}
					result = this.handle;
				}
				return result;
			}
		}

		/// <summary>Gets a value indicating whether the <see langword="BeginInvoke" /> call completed synchronously.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="BeginInvoke" /> call completed synchronously; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06003A80 RID: 14976 RVA: 0x000CD054 File Offset: 0x000CB254
		public virtual bool CompletedSynchronously
		{
			get
			{
				return this.sync_completed;
			}
		}

		/// <summary>Gets a value indicating whether the server has completed the call.</summary>
		/// <returns>
		///   <see langword="true" /> after the server has completed the call; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06003A81 RID: 14977 RVA: 0x000CD05C File Offset: 0x000CB25C
		public virtual bool IsCompleted
		{
			get
			{
				return this.completed;
			}
		}

		/// <summary>Gets or sets a value indicating whether <see langword="EndInvoke" /> has been called on the current <see cref="T:System.Runtime.Remoting.Messaging.AsyncResult" />.</summary>
		/// <returns>
		///   <see langword="true" /> if <see langword="EndInvoke" /> has been called on the current <see cref="T:System.Runtime.Remoting.Messaging.AsyncResult" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06003A82 RID: 14978 RVA: 0x000CD064 File Offset: 0x000CB264
		// (set) Token: 0x06003A83 RID: 14979 RVA: 0x000CD06C File Offset: 0x000CB26C
		public bool EndInvokeCalled
		{
			get
			{
				return this.endinvoke_called;
			}
			set
			{
				this.endinvoke_called = value;
			}
		}

		/// <summary>Gets the delegate object on which the asynchronous call was invoked.</summary>
		/// <returns>The delegate object on which the asynchronous call was invoked.</returns>
		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06003A84 RID: 14980 RVA: 0x000CD075 File Offset: 0x000CB275
		public virtual object AsyncDelegate
		{
			get
			{
				return this.async_delegate;
			}
		}

		/// <summary>Gets the next message sink in the sink chain.</summary>
		/// <returns>An <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> interface that represents the next message sink in the sink chain.</returns>
		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06003A85 RID: 14981 RVA: 0x0000AF5E File Offset: 0x0000915E
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> interface.</summary>
		/// <param name="msg">The request <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> interface.</param>
		/// <param name="replySink">The response <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> interface.</param>
		/// <returns>No value is returned.</returns>
		// Token: 0x06003A86 RID: 14982 RVA: 0x000472C8 File Offset: 0x000454C8
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the response message for the asynchronous call.</summary>
		/// <returns>A remoting message that should represent a response to a method call on a remote object.</returns>
		// Token: 0x06003A87 RID: 14983 RVA: 0x000CD07D File Offset: 0x000CB27D
		public virtual IMessage GetReplyMessage()
		{
			return this.reply_message;
		}

		/// <summary>Sets an <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> for the current remote method call, which provides a way to control asynchronous messages after they have been dispatched.</summary>
		/// <param name="mc">The <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> for the current remote method call.</param>
		// Token: 0x06003A88 RID: 14984 RVA: 0x000CD085 File Offset: 0x000CB285
		public virtual void SetMessageCtrl(IMessageCtrl mc)
		{
			this.message_ctrl = mc;
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x000CD08E File Offset: 0x000CB28E
		internal void SetCompletedSynchronously(bool completed)
		{
			this.sync_completed = completed;
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x000CD098 File Offset: 0x000CB298
		internal IMessage EndInvoke()
		{
			lock (this)
			{
				if (this.completed)
				{
					return this.reply_message;
				}
			}
			this.AsyncWaitHandle.WaitOne();
			return this.reply_message;
		}

		/// <summary>Synchronously processes a response message returned by a method call on a remote object.</summary>
		/// <param name="msg">A response message to a method call on a remote object.</param>
		/// <returns>Returns <see langword="null" />.</returns>
		// Token: 0x06003A8B RID: 14987 RVA: 0x000CD0F4 File Offset: 0x000CB2F4
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			this.reply_message = msg;
			lock (this)
			{
				this.completed = true;
				if (this.handle != null)
				{
					((ManualResetEvent)this.AsyncWaitHandle).Set();
				}
			}
			if (this.async_callback != null)
			{
				((AsyncCallback)this.async_callback)(this);
			}
			return null;
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06003A8C RID: 14988 RVA: 0x000CD16C File Offset: 0x000CB36C
		// (set) Token: 0x06003A8D RID: 14989 RVA: 0x000CD174 File Offset: 0x000CB374
		internal MonoMethodMessage CallMessage
		{
			get
			{
				return this.call_message;
			}
			set
			{
				this.call_message = value;
			}
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x000CD17D File Offset: 0x000CB37D
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.Invoke();
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x00004BF9 File Offset: 0x00002DF9
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x06003A90 RID: 14992
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object Invoke();

		// Token: 0x0400265F RID: 9823
		private object async_state;

		// Token: 0x04002660 RID: 9824
		private WaitHandle handle;

		// Token: 0x04002661 RID: 9825
		private object async_delegate;

		// Token: 0x04002662 RID: 9826
		private IntPtr data;

		// Token: 0x04002663 RID: 9827
		private object object_data;

		// Token: 0x04002664 RID: 9828
		private bool sync_completed;

		// Token: 0x04002665 RID: 9829
		private bool completed;

		// Token: 0x04002666 RID: 9830
		private bool endinvoke_called;

		// Token: 0x04002667 RID: 9831
		private object async_callback;

		// Token: 0x04002668 RID: 9832
		private ExecutionContext current;

		// Token: 0x04002669 RID: 9833
		private ExecutionContext original;

		// Token: 0x0400266A RID: 9834
		private long add_time;

		// Token: 0x0400266B RID: 9835
		private MonoMethodMessage call_message;

		// Token: 0x0400266C RID: 9836
		private IMessageCtrl message_ctrl;

		// Token: 0x0400266D RID: 9837
		private IMessage reply_message;

		// Token: 0x0400266E RID: 9838
		private WaitCallback orig_cb;
	}
}
