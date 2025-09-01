using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020005C6 RID: 1478
	internal class ServerDispatchSink : IServerChannelSink, IChannelSinkBase
	{
		// Token: 0x060038A8 RID: 14504 RVA: 0x0000259F File Offset: 0x0000079F
		public ServerDispatchSink()
		{
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060038A9 RID: 14505 RVA: 0x0000AF5E File Offset: 0x0000915E
		public IServerChannelSink NextChannelSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060038AA RID: 14506 RVA: 0x0000AF5E File Offset: 0x0000915E
		public IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x000472C8 File Offset: 0x000454C8
		public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream stream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x0000AF5E File Offset: 0x0000915E
		public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers)
		{
			return null;
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000CA822 File Offset: 0x000C8A22
		public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			responseHeaders = null;
			responseStream = null;
			return ChannelServices.DispatchMessage(sinkStack, requestMsg, out responseMsg);
		}
	}
}
