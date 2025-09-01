using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000731 RID: 1841
	internal class Win32IPv6InterfaceProperties : IPv6InterfaceProperties
	{
		// Token: 0x06003AC4 RID: 15044 RVA: 0x000CB52B File Offset: 0x000C972B
		public Win32IPv6InterfaceProperties(Win32_MIB_IFROW mib)
		{
			this.mib = mib;
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06003AC5 RID: 15045 RVA: 0x000CB53A File Offset: 0x000C973A
		public override int Index
		{
			get
			{
				return this.mib.Index;
			}
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06003AC6 RID: 15046 RVA: 0x000CB547 File Offset: 0x000C9747
		public override int Mtu
		{
			get
			{
				return this.mib.Mtu;
			}
		}

		// Token: 0x0400227E RID: 8830
		private Win32_MIB_IFROW mib;
	}
}
