using System;

namespace System.Xml
{
	// Token: 0x02000233 RID: 563
	internal class Ucs4Encoding4321 : Ucs4Encoding
	{
		// Token: 0x06001538 RID: 5432 RVA: 0x00083516 File Offset: 0x00081716
		public Ucs4Encoding4321()
		{
			this.ucs4Decoder = new Ucs4Decoder4321();
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x00083529 File Offset: 0x00081729
		public override string EncodingName
		{
			get
			{
				return "ucs-4";
			}
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x00083530 File Offset: 0x00081730
		public override byte[] GetPreamble()
		{
			byte[] array = new byte[4];
			array[0] = byte.MaxValue;
			array[1] = 254;
			return array;
		}
	}
}
