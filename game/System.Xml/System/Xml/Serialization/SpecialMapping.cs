using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000293 RID: 659
	internal class SpecialMapping : TypeMapping
	{
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x0008F729 File Offset: 0x0008D929
		// (set) Token: 0x060018ED RID: 6381 RVA: 0x0008F731 File Offset: 0x0008D931
		internal bool NamedAny
		{
			get
			{
				return this.namedAny;
			}
			set
			{
				this.namedAny = value;
			}
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x0008E9BC File Offset: 0x0008CBBC
		public SpecialMapping()
		{
		}

		// Token: 0x040018F5 RID: 6389
		private bool namedAny;
	}
}
