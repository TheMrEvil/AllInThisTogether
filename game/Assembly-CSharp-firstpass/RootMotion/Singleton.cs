using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000C0 RID: 192
	public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00039CD8 File Offset: 0x00037ED8
		public static T instance
		{
			get
			{
				return Singleton<T>.sInstance;
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00039CDF File Offset: 0x00037EDF
		protected virtual void Awake()
		{
			if (Singleton<T>.sInstance != null)
			{
				Debug.LogError(base.name + "error: already initialized", this);
			}
			Singleton<T>.sInstance = (T)((object)this);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00039D14 File Offset: 0x00037F14
		protected Singleton()
		{
		}

		// Token: 0x040006C8 RID: 1736
		private static T sInstance;
	}
}
