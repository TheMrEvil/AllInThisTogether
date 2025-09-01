using System;

namespace ES3Types
{
	// Token: 0x02000041 RID: 65
	public class ES3Type_ES3RefArray : ES3ArrayType
	{
		// Token: 0x0600029B RID: 667 RVA: 0x0000A3D8 File Offset: 0x000085D8
		public ES3Type_ES3RefArray() : base(typeof(ES3Ref[]), ES3Type_ES3Ref.Instance)
		{
			ES3Type_ES3RefArray.Instance = this;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000A3F5 File Offset: 0x000085F5
		// Note: this type is marked as 'beforefieldinit'.
		static ES3Type_ES3RefArray()
		{
		}

		// Token: 0x04000096 RID: 150
		public static ES3Type Instance = new ES3Type_ES3RefArray();
	}
}
