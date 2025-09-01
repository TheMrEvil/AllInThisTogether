using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000098 RID: 152
	public class ES3Type_KeyframeArray : ES3ArrayType
	{
		// Token: 0x0600037B RID: 891 RVA: 0x00011B30 File Offset: 0x0000FD30
		public ES3Type_KeyframeArray() : base(typeof(Keyframe[]), ES3Type_Keyframe.Instance)
		{
			ES3Type_KeyframeArray.Instance = this;
		}

		// Token: 0x040000EB RID: 235
		public static ES3Type Instance;
	}
}
