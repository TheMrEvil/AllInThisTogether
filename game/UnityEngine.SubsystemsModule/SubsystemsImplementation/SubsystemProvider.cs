using System;

namespace UnityEngine.SubsystemsImplementation
{
	// Token: 0x02000016 RID: 22
	public abstract class SubsystemProvider
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002CB9 File Offset: 0x00000EB9
		public bool running
		{
			get
			{
				return this.m_Running;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000020A8 File Offset: 0x000002A8
		protected SubsystemProvider()
		{
		}

		// Token: 0x04000014 RID: 20
		internal bool m_Running;
	}
}
