using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200001E RID: 30
	[Serializable]
	public abstract class TMP_Asset : ScriptableObject
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00017361 File Offset: 0x00015561
		public int instanceID
		{
			get
			{
				if (this.m_InstanceID == 0)
				{
					this.m_InstanceID = base.GetInstanceID();
				}
				return this.m_InstanceID;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0001737D File Offset: 0x0001557D
		protected TMP_Asset()
		{
		}

		// Token: 0x04000107 RID: 263
		private int m_InstanceID;

		// Token: 0x04000108 RID: 264
		public int hashCode;

		// Token: 0x04000109 RID: 265
		public Material material;

		// Token: 0x0400010A RID: 266
		public int materialHashCode;
	}
}
