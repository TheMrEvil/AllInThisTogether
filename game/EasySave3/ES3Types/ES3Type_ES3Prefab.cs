using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000018 RID: 24
	[Preserve]
	public class ES3Type_ES3Prefab : ES3Type
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x00006FDB File Offset: 0x000051DB
		public ES3Type_ES3Prefab() : base(typeof(ES3Prefab))
		{
			ES3Type_ES3Prefab.Instance = this;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006FF3 File Offset: 0x000051F3
		public override void Write(object obj, ES3Writer writer)
		{
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006FF5 File Offset: 0x000051F5
		public override object Read<T>(ES3Reader reader)
		{
			return null;
		}

		// Token: 0x0400005A RID: 90
		public static ES3Type Instance;
	}
}
