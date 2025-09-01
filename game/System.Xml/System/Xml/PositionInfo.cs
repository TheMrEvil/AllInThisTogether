using System;

namespace System.Xml
{
	// Token: 0x020001E3 RID: 483
	internal class PositionInfo : IXmlLineInfo
	{
		// Token: 0x0600131E RID: 4894 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual bool HasLineInfo()
		{
			return false;
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual int LineNumber
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual int LinePosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00070D00 File Offset: 0x0006EF00
		public static PositionInfo GetPositionInfo(object o)
		{
			IXmlLineInfo xmlLineInfo = o as IXmlLineInfo;
			if (xmlLineInfo != null)
			{
				return new ReaderPositionInfo(xmlLineInfo);
			}
			return new PositionInfo();
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0000216B File Offset: 0x0000036B
		public PositionInfo()
		{
		}
	}
}
