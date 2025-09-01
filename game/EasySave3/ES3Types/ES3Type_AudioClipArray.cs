using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000078 RID: 120
	public class ES3Type_AudioClipArray : ES3ArrayType
	{
		// Token: 0x06000321 RID: 801 RVA: 0x0000F618 File Offset: 0x0000D818
		public ES3Type_AudioClipArray() : base(typeof(AudioClip[]), ES3Type_AudioClip.Instance)
		{
			ES3Type_AudioClipArray.Instance = this;
		}

		// Token: 0x040000C8 RID: 200
		public static ES3Type Instance;
	}
}
