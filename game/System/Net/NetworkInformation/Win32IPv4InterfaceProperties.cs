using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200072E RID: 1838
	internal sealed class Win32IPv4InterfaceProperties : IPv4InterfaceProperties
	{
		// Token: 0x06003AAD RID: 15021
		[DllImport("iphlpapi.dll")]
		private static extern int GetPerAdapterInfo(int IfIndex, Win32_IP_PER_ADAPTER_INFO pPerAdapterInfo, ref int pOutBufLen);

		// Token: 0x06003AAE RID: 15022 RVA: 0x000CB3AC File Offset: 0x000C95AC
		public Win32IPv4InterfaceProperties(Win32_IP_ADAPTER_ADDRESSES addr, Win32_MIB_IFROW mib)
		{
			this.addr = addr;
			this.mib = mib;
			int num = 0;
			Win32IPv4InterfaceProperties.GetPerAdapterInfo(mib.Index, null, ref num);
			this.painfo = new Win32_IP_PER_ADAPTER_INFO();
			int perAdapterInfo = Win32IPv4InterfaceProperties.GetPerAdapterInfo(mib.Index, this.painfo, ref num);
			if (perAdapterInfo != 0)
			{
				throw new NetworkInformationException(perAdapterInfo);
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06003AAF RID: 15023 RVA: 0x000CB407 File Offset: 0x000C9607
		public override int Index
		{
			get
			{
				return this.mib.Index;
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06003AB0 RID: 15024 RVA: 0x000CB414 File Offset: 0x000C9614
		public override bool IsAutomaticPrivateAddressingActive
		{
			get
			{
				return this.painfo.AutoconfigActive > 0U;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06003AB1 RID: 15025 RVA: 0x000CB424 File Offset: 0x000C9624
		public override bool IsAutomaticPrivateAddressingEnabled
		{
			get
			{
				return this.painfo.AutoconfigEnabled > 0U;
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06003AB2 RID: 15026 RVA: 0x000CB434 File Offset: 0x000C9634
		public override bool IsDhcpEnabled
		{
			get
			{
				return this.addr.DhcpEnabled;
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06003AB3 RID: 15027 RVA: 0x000CB441 File Offset: 0x000C9641
		public override bool IsForwardingEnabled
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.EnableRouting > 0U;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06003AB4 RID: 15028 RVA: 0x000CB450 File Offset: 0x000C9650
		public override int Mtu
		{
			get
			{
				return this.mib.Mtu;
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06003AB5 RID: 15029 RVA: 0x000CB45D File Offset: 0x000C965D
		public override bool UsesWins
		{
			get
			{
				return this.addr.FirstWinsServerAddress != IntPtr.Zero;
			}
		}

		// Token: 0x04002276 RID: 8822
		private Win32_IP_ADAPTER_ADDRESSES addr;

		// Token: 0x04002277 RID: 8823
		private Win32_IP_PER_ADAPTER_INFO painfo;

		// Token: 0x04002278 RID: 8824
		private Win32_MIB_IFROW mib;
	}
}
