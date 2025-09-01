using System;

namespace System.Xml.Schema
{
	// Token: 0x020004F4 RID: 1268
	internal class UpaException : Exception
	{
		// Token: 0x060033F4 RID: 13300 RVA: 0x001271C8 File Offset: 0x001253C8
		public UpaException(object particle1, object particle2)
		{
			this.particle1 = particle1;
			this.particle2 = particle2;
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060033F5 RID: 13301 RVA: 0x001271DE File Offset: 0x001253DE
		public object Particle1
		{
			get
			{
				return this.particle1;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060033F6 RID: 13302 RVA: 0x001271E6 File Offset: 0x001253E6
		public object Particle2
		{
			get
			{
				return this.particle2;
			}
		}

		// Token: 0x040026C9 RID: 9929
		private object particle1;

		// Token: 0x040026CA RID: 9930
		private object particle2;
	}
}
