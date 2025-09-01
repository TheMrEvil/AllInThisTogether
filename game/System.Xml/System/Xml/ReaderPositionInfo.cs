using System;

namespace System.Xml
{
	// Token: 0x020001E4 RID: 484
	internal class ReaderPositionInfo : PositionInfo
	{
		// Token: 0x06001323 RID: 4899 RVA: 0x00070D23 File Offset: 0x0006EF23
		public ReaderPositionInfo(IXmlLineInfo lineInfo)
		{
			this.lineInfo = lineInfo;
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00070D32 File Offset: 0x0006EF32
		public override bool HasLineInfo()
		{
			return this.lineInfo.HasLineInfo();
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x00070D3F File Offset: 0x0006EF3F
		public override int LineNumber
		{
			get
			{
				return this.lineInfo.LineNumber;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x00070D4C File Offset: 0x0006EF4C
		public override int LinePosition
		{
			get
			{
				return this.lineInfo.LinePosition;
			}
		}

		// Token: 0x040010EB RID: 4331
		private IXmlLineInfo lineInfo;
	}
}
