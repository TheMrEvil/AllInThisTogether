using System;

namespace System.Xml
{
	// Token: 0x02000234 RID: 564
	internal class Ucs4Encoding2143 : Ucs4Encoding
	{
		// Token: 0x0600153B RID: 5435 RVA: 0x00083548 File Offset: 0x00081748
		public Ucs4Encoding2143()
		{
			this.ucs4Decoder = new Ucs4Decoder2143();
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x0008355B File Offset: 0x0008175B
		public override string EncodingName
		{
			get
			{
				return "ucs-4 (order 2143)";
			}
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x00083562 File Offset: 0x00081762
		public override byte[] GetPreamble()
		{
			return new byte[]
			{
				0,
				0,
				byte.MaxValue,
				254
			};
		}
	}
}
