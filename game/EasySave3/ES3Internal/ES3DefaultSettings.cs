using System;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000DE RID: 222
	public class ES3DefaultSettings : MonoBehaviour
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x0001E13E File Offset: 0x0001C33E
		public ES3DefaultSettings()
		{
		}

		// Token: 0x04000151 RID: 337
		[SerializeField]
		public ES3SerializableSettings settings;

		// Token: 0x04000152 RID: 338
		public bool autoUpdateReferences = true;
	}
}
