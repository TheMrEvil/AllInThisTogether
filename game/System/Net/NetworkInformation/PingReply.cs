using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about the status and data resulting from a <see cref="Overload:System.Net.NetworkInformation.Ping.Send" /> or <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> operation.</summary>
	// Token: 0x020006FD RID: 1789
	public class PingReply
	{
		// Token: 0x06003977 RID: 14711 RVA: 0x0000219B File Offset: 0x0000039B
		internal PingReply()
		{
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x000C8B60 File Offset: 0x000C6D60
		internal PingReply(IPStatus ipStatus)
		{
			this.ipStatus = ipStatus;
			this.buffer = new byte[0];
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x000C8B7C File Offset: 0x000C6D7C
		internal PingReply(byte[] data, int dataLength, IPAddress address, int time)
		{
			this.address = address;
			this.rtt = (long)time;
			this.ipStatus = this.GetIPStatus((IcmpV4Type)data[20], (IcmpV4Code)data[21]);
			if (this.ipStatus == IPStatus.Success)
			{
				this.buffer = new byte[dataLength - 28];
				Array.Copy(data, 28, this.buffer, 0, dataLength - 28);
				return;
			}
			this.buffer = new byte[0];
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x000C8BEA File Offset: 0x000C6DEA
		internal PingReply(IPAddress address, byte[] buffer, PingOptions options, long roundtripTime, IPStatus status)
		{
			this.address = address;
			this.buffer = buffer;
			this.options = options;
			this.rtt = roundtripTime;
			this.ipStatus = status;
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x000C8C18 File Offset: 0x000C6E18
		private IPStatus GetIPStatus(IcmpV4Type type, IcmpV4Code code)
		{
			switch (type)
			{
			case IcmpV4Type.ICMP4_ECHO_REPLY:
				return IPStatus.Success;
			case (IcmpV4Type)1:
			case (IcmpV4Type)2:
				break;
			case IcmpV4Type.ICMP4_DST_UNREACH:
				switch (code)
				{
				case IcmpV4Code.ICMP4_UNREACH_NET:
					return IPStatus.DestinationNetworkUnreachable;
				case IcmpV4Code.ICMP4_UNREACH_HOST:
					return IPStatus.DestinationHostUnreachable;
				case IcmpV4Code.ICMP4_UNREACH_PROTOCOL:
					return IPStatus.DestinationProtocolUnreachable;
				case IcmpV4Code.ICMP4_UNREACH_PORT:
					return IPStatus.DestinationPortUnreachable;
				case IcmpV4Code.ICMP4_UNREACH_FRAG_NEEDED:
					return IPStatus.PacketTooBig;
				default:
					return IPStatus.DestinationUnreachable;
				}
				break;
			case IcmpV4Type.ICMP4_SOURCE_QUENCH:
				return IPStatus.SourceQuench;
			default:
				if (type == IcmpV4Type.ICMP4_TIME_EXCEEDED)
				{
					return IPStatus.TtlExpired;
				}
				if (type == IcmpV4Type.ICMP4_PARAM_PROB)
				{
					return IPStatus.ParameterProblem;
				}
				break;
			}
			return IPStatus.Unknown;
		}

		/// <summary>Gets the status of an attempt to send an Internet Control Message Protocol (ICMP) echo request and receive the corresponding ICMP echo reply message.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPStatus" /> value indicating the result of the request.</returns>
		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x0600397C RID: 14716 RVA: 0x000C8CA0 File Offset: 0x000C6EA0
		public IPStatus Status
		{
			get
			{
				return this.ipStatus;
			}
		}

		/// <summary>Gets the address of the host that sends the Internet Control Message Protocol (ICMP) echo reply.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> containing the destination for the ICMP echo message.</returns>
		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x0600397D RID: 14717 RVA: 0x000C8CA8 File Offset: 0x000C6EA8
		public IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		/// <summary>Gets the number of milliseconds taken to send an Internet Control Message Protocol (ICMP) echo request and receive the corresponding ICMP echo reply message.</summary>
		/// <returns>An <see cref="T:System.Int64" /> that specifies the round trip time, in milliseconds.</returns>
		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x0600397E RID: 14718 RVA: 0x000C8CB0 File Offset: 0x000C6EB0
		public long RoundtripTime
		{
			get
			{
				return this.rtt;
			}
		}

		/// <summary>Gets the options used to transmit the reply to an Internet Control Message Protocol (ICMP) echo request.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingOptions" /> object that contains the Time to Live (TTL) and the fragmentation directive used for transmitting the reply if <see cref="P:System.Net.NetworkInformation.PingReply.Status" /> is <see cref="F:System.Net.NetworkInformation.IPStatus.Success" />; otherwise, <see langword="null" />.</returns>
		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x0600397F RID: 14719 RVA: 0x000C8CB8 File Offset: 0x000C6EB8
		public PingOptions Options
		{
			get
			{
				return this.options;
			}
		}

		/// <summary>Gets the buffer of data received in an Internet Control Message Protocol (ICMP) echo reply message.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the data received in an ICMP echo reply message, or an empty array, if no reply was received.</returns>
		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06003980 RID: 14720 RVA: 0x000C8CC0 File Offset: 0x000C6EC0
		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x040021AB RID: 8619
		private IPAddress address;

		// Token: 0x040021AC RID: 8620
		private PingOptions options;

		// Token: 0x040021AD RID: 8621
		private IPStatus ipStatus;

		// Token: 0x040021AE RID: 8622
		private long rtt;

		// Token: 0x040021AF RID: 8623
		private byte[] buffer;
	}
}
