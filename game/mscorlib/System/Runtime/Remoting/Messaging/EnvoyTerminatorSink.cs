using System;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000616 RID: 1558
	[Serializable]
	internal class EnvoyTerminatorSink : IMessageSink
	{
		// Token: 0x06003AD5 RID: 15061 RVA: 0x000CE2B1 File Offset: 0x000CC4B1
		public IMessage SyncProcessMessage(IMessage msg)
		{
			return Thread.CurrentContext.GetClientContextSinkChain().SyncProcessMessage(msg);
		}

		// Token: 0x06003AD6 RID: 15062 RVA: 0x000CE2C3 File Offset: 0x000CC4C3
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			return Thread.CurrentContext.GetClientContextSinkChain().AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06003AD7 RID: 15063 RVA: 0x0000AF5E File Offset: 0x0000915E
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x0000259F File Offset: 0x0000079F
		public EnvoyTerminatorSink()
		{
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x000CE2D6 File Offset: 0x000CC4D6
		// Note: this type is marked as 'beforefieldinit'.
		static EnvoyTerminatorSink()
		{
		}

		// Token: 0x0400268C RID: 9868
		public static EnvoyTerminatorSink Instance = new EnvoyTerminatorSink();
	}
}
