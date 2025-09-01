using System;

namespace System.Xml
{
	// Token: 0x02000232 RID: 562
	internal class Ucs4Encoding1234 : Ucs4Encoding
	{
		// Token: 0x06001535 RID: 5429 RVA: 0x000834E4 File Offset: 0x000816E4
		public Ucs4Encoding1234()
		{
			this.ucs4Decoder = new Ucs4Decoder1234();
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x000834F7 File Offset: 0x000816F7
		public override string EncodingName
		{
			get
			{
				return "ucs-4 (Bigendian)";
			}
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x000834FE File Offset: 0x000816FE
		public override byte[] GetPreamble()
		{
			return new byte[]
			{
				0,
				0,
				254,
				byte.MaxValue
			};
		}
	}
}
