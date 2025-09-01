using System;

namespace System.Xml
{
	// Token: 0x02000235 RID: 565
	internal class Ucs4Encoding3412 : Ucs4Encoding
	{
		// Token: 0x0600153E RID: 5438 RVA: 0x0008357A File Offset: 0x0008177A
		public Ucs4Encoding3412()
		{
			this.ucs4Decoder = new Ucs4Decoder3412();
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x0008358D File Offset: 0x0008178D
		public override string EncodingName
		{
			get
			{
				return "ucs-4 (order 3412)";
			}
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00083594 File Offset: 0x00081794
		public override byte[] GetPreamble()
		{
			byte[] array = new byte[4];
			array[0] = 254;
			array[1] = byte.MaxValue;
			return array;
		}
	}
}
