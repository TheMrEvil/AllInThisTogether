using System;
using System.Collections;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020005C7 RID: 1479
	internal class ServerDispatchSinkProvider : IServerFormatterSinkProvider, IServerChannelSinkProvider
	{
		// Token: 0x060038AE RID: 14510 RVA: 0x0000259F File Offset: 0x0000079F
		public ServerDispatchSinkProvider()
		{
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x0000259F File Offset: 0x0000079F
		public ServerDispatchSinkProvider(IDictionary properties, ICollection providerData)
		{
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060038B0 RID: 14512 RVA: 0x0000AF5E File Offset: 0x0000915E
		// (set) Token: 0x060038B1 RID: 14513 RVA: 0x000472C8 File Offset: 0x000454C8
		public IServerChannelSinkProvider Next
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000CA835 File Offset: 0x000C8A35
		public IServerChannelSink CreateSink(IChannelReceiver channel)
		{
			return new ServerDispatchSink();
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void GetChannelData(IChannelDataStore channelData)
		{
		}
	}
}
