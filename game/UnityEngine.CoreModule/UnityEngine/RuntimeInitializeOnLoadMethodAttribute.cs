using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000213 RID: 531
	[RequiredByNativeCode]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class RuntimeInitializeOnLoadMethodAttribute : PreserveAttribute
	{
		// Token: 0x06001755 RID: 5973 RVA: 0x00025894 File Offset: 0x00023A94
		public RuntimeInitializeOnLoadMethodAttribute()
		{
			this.loadType = RuntimeInitializeLoadType.AfterSceneLoad;
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x000258A6 File Offset: 0x00023AA6
		public RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType loadType)
		{
			this.loadType = loadType;
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x000258B8 File Offset: 0x00023AB8
		// (set) Token: 0x06001758 RID: 5976 RVA: 0x000258D0 File Offset: 0x00023AD0
		public RuntimeInitializeLoadType loadType
		{
			get
			{
				return this.m_LoadType;
			}
			private set
			{
				this.m_LoadType = value;
			}
		}

		// Token: 0x04000804 RID: 2052
		private RuntimeInitializeLoadType m_LoadType;
	}
}
