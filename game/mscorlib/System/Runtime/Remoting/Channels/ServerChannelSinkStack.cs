using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Holds the stack of server channel sinks.</summary>
	// Token: 0x020005C5 RID: 1477
	[ComVisible(true)]
	public class ServerChannelSinkStack : IServerChannelSinkStack, IServerResponseChannelSinkStack
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.ServerChannelSinkStack" /> class.</summary>
		// Token: 0x060038A0 RID: 14496 RVA: 0x0000259F File Offset: 0x0000079F
		public ServerChannelSinkStack()
		{
		}

		/// <summary>Returns the <see cref="T:System.IO.Stream" /> onto which the specified message is to be serialized.</summary>
		/// <param name="msg">The message to be serialized onto the requested stream.</param>
		/// <param name="headers">The headers retrieved from the server response stream.</param>
		/// <returns>The <see cref="T:System.IO.Stream" /> onto which the specified message is to be serialized.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The sink stack is empty.</exception>
		// Token: 0x060038A1 RID: 14497 RVA: 0x000CA735 File Offset: 0x000C8935
		[SecurityCritical]
		public Stream GetResponseStream(IMessage msg, ITransportHeaders headers)
		{
			if (this._sinkStack == null)
			{
				throw new RemotingException("The sink stack is empty");
			}
			return ((IServerChannelSink)this._sinkStack.Sink).GetResponseStream(this, this._sinkStack.State, msg, headers);
		}

		/// <summary>Pops the information associated with all the sinks from the sink stack up to and including the specified sink.</summary>
		/// <param name="sink">The sink to remove and return from the sink stack.</param>
		/// <returns>Information generated on the request side and associated with the specified sink.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The current sink stack is empty, or the specified sink was never pushed onto the current stack.</exception>
		// Token: 0x060038A2 RID: 14498 RVA: 0x000CA770 File Offset: 0x000C8970
		[SecurityCritical]
		public object Pop(IServerChannelSink sink)
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

		/// <summary>Pushes the specified sink and information associated with it onto the sink stack.</summary>
		/// <param name="sink">The sink to push onto the sink stack.</param>
		/// <param name="state">Information generated on the request side that is needed on the response side.</param>
		// Token: 0x060038A3 RID: 14499 RVA: 0x000CA7B9 File Offset: 0x000C89B9
		[SecurityCritical]
		public void Push(IServerChannelSink sink, object state)
		{
			this._sinkStack = new ChanelSinkStackEntry(sink, state, this._sinkStack);
		}

		/// <summary>Provides a <see cref="T:System.AsyncCallback" /> delegate to handle a callback after a message has been dispatched asynchronously.</summary>
		/// <param name="ar">The status and state of an asynchronous operation on a remote object.</param>
		// Token: 0x060038A4 RID: 14500 RVA: 0x000479F8 File Offset: 0x00045BF8
		[SecurityCritical]
		[MonoTODO]
		public void ServerCallback(IAsyncResult ar)
		{
			throw new NotImplementedException();
		}

		/// <summary>Stores a message sink and its associated state for later asynchronous processing.</summary>
		/// <param name="sink">A server channel sink.</param>
		/// <param name="state">The state associated with <paramref name="sink" />.</param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The current sink stack is empty.  
		///  -or-  
		///  The specified sink was never pushed onto the current stack.</exception>
		// Token: 0x060038A5 RID: 14501 RVA: 0x000479F8 File Offset: 0x00045BF8
		[SecurityCritical]
		[MonoTODO]
		public void Store(IServerChannelSink sink, object state)
		{
			throw new NotImplementedException();
		}

		/// <summary>Stores a message sink and its associated state, and then dispatches a message asynchronously, using the sink just stored and any other stored sinks.</summary>
		/// <param name="sink">A server channel sink.</param>
		/// <param name="state">The state associated with <paramref name="sink" />.</param>
		// Token: 0x060038A6 RID: 14502 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		[SecurityCritical]
		public void StoreAndDispatch(IServerChannelSink sink, object state)
		{
			throw new NotImplementedException();
		}

		/// <summary>Requests asynchronous processing of a method call on the sinks in the current sink stack.</summary>
		/// <param name="msg">The message to be serialized onto the requested stream.</param>
		/// <param name="headers">The headers retrieved from the server response stream.</param>
		/// <param name="stream">The stream coming back from the transport sink.</param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The current sink stack is empty.</exception>
		// Token: 0x060038A7 RID: 14503 RVA: 0x000CA7D0 File Offset: 0x000C89D0
		[SecurityCritical]
		public void AsyncProcessResponse(IMessage msg, ITransportHeaders headers, Stream stream)
		{
			if (this._sinkStack == null)
			{
				throw new RemotingException("The current sink stack is empty");
			}
			ChanelSinkStackEntry sinkStack = this._sinkStack;
			this._sinkStack = this._sinkStack.Next;
			((IServerChannelSink)sinkStack.Sink).AsyncProcessResponse(this, sinkStack.State, msg, headers, stream);
		}

		// Token: 0x040025E9 RID: 9705
		private ChanelSinkStackEntry _sinkStack;
	}
}
