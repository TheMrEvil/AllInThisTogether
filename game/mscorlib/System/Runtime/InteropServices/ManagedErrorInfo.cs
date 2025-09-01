using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000747 RID: 1863
	internal class ManagedErrorInfo : IErrorInfo
	{
		// Token: 0x06004147 RID: 16711 RVA: 0x000E1F87 File Offset: 0x000E0187
		public ManagedErrorInfo(Exception e)
		{
			this.m_Exception = e;
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06004148 RID: 16712 RVA: 0x000E1F96 File Offset: 0x000E0196
		public Exception Exception
		{
			get
			{
				return this.m_Exception;
			}
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x000E1F9E File Offset: 0x000E019E
		public int GetGUID(out Guid guid)
		{
			guid = Guid.Empty;
			return 0;
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x000E1FAC File Offset: 0x000E01AC
		public int GetSource(out string source)
		{
			source = this.m_Exception.Source;
			return 0;
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x000E1FBC File Offset: 0x000E01BC
		public int GetDescription(out string description)
		{
			description = this.m_Exception.Message;
			return 0;
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x000E1FCC File Offset: 0x000E01CC
		public int GetHelpFile(out string helpFile)
		{
			helpFile = this.m_Exception.HelpLink;
			return 0;
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x000E1FDC File Offset: 0x000E01DC
		public int GetHelpContext(out uint helpContext)
		{
			helpContext = 0U;
			return 0;
		}

		// Token: 0x04002BCB RID: 11211
		private Exception m_Exception;
	}
}
