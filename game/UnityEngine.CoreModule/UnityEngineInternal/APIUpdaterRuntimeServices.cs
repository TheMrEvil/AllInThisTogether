using System;
using UnityEngine;

namespace UnityEngineInternal
{
	// Token: 0x0200000F RID: 15
	public sealed class APIUpdaterRuntimeServices
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000020EB File Offset: 0x000002EB
		[Obsolete("Method is not meant to be used at runtime. Please, replace this call with GameObject.AddComponent<T>()/GameObject.AddComponent(Type).", true)]
		public static Component AddComponent(GameObject go, string sourceInfo, string name)
		{
			throw new Exception();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002072 File Offset: 0x00000272
		public APIUpdaterRuntimeServices()
		{
		}
	}
}
