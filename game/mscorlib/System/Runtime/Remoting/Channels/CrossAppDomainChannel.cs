using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020005AD RID: 1453
	[Serializable]
	internal class CrossAppDomainChannel : IChannel, IChannelSender, IChannelReceiver
	{
		// Token: 0x06003853 RID: 14419 RVA: 0x000CA2D0 File Offset: 0x000C84D0
		internal static void RegisterCrossAppDomainChannel()
		{
			object obj = CrossAppDomainChannel.s_lock;
			lock (obj)
			{
				ChannelServices.RegisterChannel(new CrossAppDomainChannel());
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06003854 RID: 14420 RVA: 0x000CA314 File Offset: 0x000C8514
		public virtual string ChannelName
		{
			get
			{
				return "MONOCAD";
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06003855 RID: 14421 RVA: 0x000CA31B File Offset: 0x000C851B
		public virtual int ChannelPriority
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x000CA31F File Offset: 0x000C851F
		public string Parse(string url, out string objectURI)
		{
			objectURI = url;
			return null;
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06003857 RID: 14423 RVA: 0x000CA325 File Offset: 0x000C8525
		public virtual object ChannelData
		{
			get
			{
				return new CrossAppDomainData(Thread.GetDomainID());
			}
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x000CA331 File Offset: 0x000C8531
		public virtual string[] GetUrlsForUri(string objectURI)
		{
			throw new NotSupportedException("CrossAppdomain channel dont support UrlsForUri");
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public virtual void StartListening(object data)
		{
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public virtual void StopListening(object data)
		{
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x000CA340 File Offset: 0x000C8540
		public virtual IMessageSink CreateMessageSink(string url, object data, out string uri)
		{
			uri = null;
			if (data != null)
			{
				CrossAppDomainData crossAppDomainData = data as CrossAppDomainData;
				if (crossAppDomainData != null && crossAppDomainData.ProcessID == RemotingConfiguration.ProcessId)
				{
					return CrossAppDomainSink.GetSink(crossAppDomainData.DomainID);
				}
			}
			if (url != null && url.StartsWith("MONOCAD"))
			{
				throw new NotSupportedException("Can't create a named channel via crossappdomain");
			}
			return null;
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x0000259F File Offset: 0x0000079F
		public CrossAppDomainChannel()
		{
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x000CA397 File Offset: 0x000C8597
		// Note: this type is marked as 'beforefieldinit'.
		static CrossAppDomainChannel()
		{
		}

		// Token: 0x040025E0 RID: 9696
		private const string _strName = "MONOCAD";

		// Token: 0x040025E1 RID: 9697
		private static object s_lock = new object();
	}
}
