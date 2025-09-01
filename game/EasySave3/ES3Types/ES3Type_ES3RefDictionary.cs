using System;
using System.Collections.Generic;

namespace ES3Types
{
	// Token: 0x02000042 RID: 66
	public class ES3Type_ES3RefDictionary : ES3DictionaryType
	{
		// Token: 0x0600029D RID: 669 RVA: 0x0000A401 File Offset: 0x00008601
		public ES3Type_ES3RefDictionary() : base(typeof(Dictionary<ES3Ref, ES3Ref>), ES3Type_ES3Ref.Instance, ES3Type_ES3Ref.Instance)
		{
			ES3Type_ES3RefDictionary.Instance = this;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000A423 File Offset: 0x00008623
		// Note: this type is marked as 'beforefieldinit'.
		static ES3Type_ES3RefDictionary()
		{
		}

		// Token: 0x04000097 RID: 151
		public static ES3Type Instance = new ES3Type_ES3RefDictionary();
	}
}
