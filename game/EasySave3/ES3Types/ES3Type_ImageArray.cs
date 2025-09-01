using System;
using UnityEngine.UI;

namespace ES3Types
{
	// Token: 0x02000065 RID: 101
	public class ES3Type_ImageArray : ES3ArrayType
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x0000C44C File Offset: 0x0000A64C
		public ES3Type_ImageArray() : base(typeof(Image[]), ES3Type_Image.Instance)
		{
			ES3Type_ImageArray.Instance = this;
		}

		// Token: 0x040000B4 RID: 180
		public static ES3Type Instance;
	}
}
