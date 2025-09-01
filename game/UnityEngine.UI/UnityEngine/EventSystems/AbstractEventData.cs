using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200004D RID: 77
	public abstract class AbstractEventData
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x00017D64 File Offset: 0x00015F64
		public virtual void Reset()
		{
			this.m_Used = false;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00017D6D File Offset: 0x00015F6D
		public virtual void Use()
		{
			this.m_Used = true;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00017D76 File Offset: 0x00015F76
		public virtual bool used
		{
			get
			{
				return this.m_Used;
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00017D7E File Offset: 0x00015F7E
		protected AbstractEventData()
		{
		}

		// Token: 0x040001AD RID: 429
		protected bool m_Used;
	}
}
