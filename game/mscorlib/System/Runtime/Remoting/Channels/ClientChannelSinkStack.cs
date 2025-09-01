using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Holds the stack of client channel sinks that must be invoked during an asynchronous message response decoding.</summary>
	// Token: 0x020005AB RID: 1451
	[ComVisible(true)]
	public class ClientChannelSinkStack : IClientChannelSinkStack, IClientResponseChannelSinkStack
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.ClientChannelSinkStack" /> class with default values.</summary>
		// Token: 0x06003849 RID: 14409 RVA: 0x0000259F File Offset: 0x0000079F
		public ClientChannelSinkStack()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.ClientChannelSinkStack" /> class with the specified reply sink.</summary>
		/// <param name="replySink">The <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> that the current stack can use to reply to messages.</param>
		// Token: 0x0600384A RID: 14410 RVA: 0x000CA1B4 File Offset: 0x000C83B4
		public ClientChannelSinkStack(IMessageSink replySink)
		{
			this._replySink = replySink;
		}

		/// <summary>Requests asynchronous processing of a method call on the sinks that are in the current sink stack.</summary>
		/// <param name="headers">The headers that are retrieved from the server response stream.</param>
		/// <param name="stream">The stream that is returning from the transport sink.</param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The current sink stack is empty.</exception>
		// Token: 0x0600384B RID: 14411 RVA: 0x000CA1C4 File Offset: 0x000C83C4
		[SecurityCritical]
		public void AsyncProcessResponse(ITransportHeaders headers, Stream stream)
		{
			if (this._sinkStack == null)
			{
				throw new RemotingException("The current sink stack is empty");
			}
			ChanelSinkStackEntry sinkStack = this._sinkStack;
			this._sinkStack = this._sinkStack.Next;
			((IClientChannelSink)sinkStack.Sink).AsyncProcessResponse(this, sinkStack.State, headers, stream);
		}

		/// <summary>Dispatches the specified exception on the reply sink.</summary>
		/// <param name="e">The exception to dispatch to the server.</param>
		// Token: 0x0600384C RID: 14412 RVA: 0x000CA215 File Offset: 0x000C8415
		[SecurityCritical]
		public void DispatchException(Exception e)
		{
			this.DispatchReplyMessage(new ReturnMessage(e, null));
		}

		/// <summary>Dispatches the specified reply message on the reply sink.</summary>
		/// <param name="msg">The <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> to dispatch.</param>
		// Token: 0x0600384D RID: 14413 RVA: 0x000CA224 File Offset: 0x000C8424
		[SecurityCritical]
		public void DispatchReplyMessage(IMessage msg)
		{
			if (this._replySink != null)
			{
				this._replySink.SyncProcessMessage(msg);
			}
		}

		/// <summary>Pops the information that is associated with all the sinks from the sink stack up to and including the specified sink.</summary>
		/// <param name="sink">The sink to remove and return from the sink stack.</param>
		/// <returns>Information generated on the request side and associated with the specified sink.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The current sink stack is empty, or the specified sink was never pushed onto the current stack.</exception>
		// Token: 0x0600384E RID: 14414 RVA: 0x000CA23C File Offset: 0x000C843C
		[SecurityCritical]
		public object Pop(IClientChannelSink sink)
		{
			while (this._sinkStack != null)
			{
				ChanelSinkStackEntry sinkStack = this._sinkStack;
				this._sinkStack = this._sinkStack.Next;
				if (sinkStack.Sink == sink)
				{
					return sinkStack.State;
				}
			}
			throw new RemotingException("The current sink stack is empty, or the specified sink was never pushed onto the current stack");
		}

		/// <summary>Pushes the specified sink and information that is associated with it onto the sink stack.</summary>
		/// <param name="sink">The sink to push onto the sink stack.</param>
		/// <param name="state">Information generated on the request side that is needed on the response side.</param>
		// Token: 0x0600384F RID: 14415 RVA: 0x000CA285 File Offset: 0x000C8485
		[SecurityCritical]
		public void Push(IClientChannelSink sink, object state)
		{
			this._sinkStack = new ChanelSinkStackEntry(sink, state, this._sinkStack);
		}

		// Token: 0x040025DB RID: 9691
		private IMessageSink _replySink;

		// Token: 0x040025DC RID: 9692
		private ChanelSinkStackEntry _sinkStack;
	}
}
