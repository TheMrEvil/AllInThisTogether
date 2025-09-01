using System;
using ES3Types;

namespace ES3Internal
{
	// Token: 0x020000D4 RID: 212
	public struct ES3Data
	{
		// Token: 0x0600042A RID: 1066 RVA: 0x0001B14D File Offset: 0x0001934D
		public ES3Data(Type type, byte[] bytes)
		{
			this.type = ((type == null) ? null : ES3TypeMgr.GetOrCreateES3Type(type, true));
			this.bytes = bytes;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001B16F File Offset: 0x0001936F
		public ES3Data(ES3Type type, byte[] bytes)
		{
			this.type = type;
			this.bytes = bytes;
		}

		// Token: 0x0400012F RID: 303
		public ES3Type type;

		// Token: 0x04000130 RID: 304
		public byte[] bytes;
	}
}
