using System;
using System.Diagnostics;

namespace UnityEngine
{
	// Token: 0x020001E7 RID: 487
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class IconAttribute : Attribute
	{
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x00023775 File Offset: 0x00021975
		public string path
		{
			get
			{
				return this.m_IconPath;
			}
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00002059 File Offset: 0x00000259
		private IconAttribute()
		{
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0002377D File Offset: 0x0002197D
		public IconAttribute(string path)
		{
			this.m_IconPath = path;
		}

		// Token: 0x040007C8 RID: 1992
		private string m_IconPath;
	}
}
