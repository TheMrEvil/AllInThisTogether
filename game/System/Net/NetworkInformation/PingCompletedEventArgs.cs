using System;
using System.ComponentModel;
using Unity;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides data for the <see cref="E:System.Net.NetworkInformation.Ping.PingCompleted" /> event.</summary>
	// Token: 0x02000721 RID: 1825
	public class PingCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06003A4C RID: 14924 RVA: 0x000CA699 File Offset: 0x000C8899
		internal PingCompletedEventArgs(Exception ex, bool cancelled, object userState, PingReply reply) : base(ex, cancelled, userState)
		{
			this.reply = reply;
		}

		/// <summary>Gets an object that contains data that describes an attempt to send an Internet Control Message Protocol (ICMP) echo request message and receive a corresponding ICMP echo reply message.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that describes the results of the ICMP echo request.</returns>
		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06003A4D RID: 14925 RVA: 0x000CA6AC File Offset: 0x000C88AC
		public PingReply Reply
		{
			get
			{
				return this.reply;
			}
		}

		// Token: 0x06003A4E RID: 14926 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal PingCompletedEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002245 RID: 8773
		private PingReply reply;
	}
}
