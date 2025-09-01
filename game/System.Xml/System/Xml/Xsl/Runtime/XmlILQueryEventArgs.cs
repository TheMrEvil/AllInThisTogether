using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000478 RID: 1144
	internal class XmlILQueryEventArgs : XsltMessageEncounteredEventArgs
	{
		// Token: 0x06002C3B RID: 11323 RVA: 0x00106673 File Offset: 0x00104873
		public XmlILQueryEventArgs(string message)
		{
			this.message = message;
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002C3C RID: 11324 RVA: 0x00106682 File Offset: 0x00104882
		public override string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x040022E2 RID: 8930
		private string message;
	}
}
