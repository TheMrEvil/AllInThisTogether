using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000289 RID: 649
	internal class NullableMapping : TypeMapping
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x0008E9C4 File Offset: 0x0008CBC4
		// (set) Token: 0x06001878 RID: 6264 RVA: 0x0008E9CC File Offset: 0x0008CBCC
		internal TypeMapping BaseMapping
		{
			get
			{
				return this.baseMapping;
			}
			set
			{
				this.baseMapping = value;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x0008E9D5 File Offset: 0x0008CBD5
		internal override string DefaultElementName
		{
			get
			{
				return this.BaseMapping.DefaultElementName;
			}
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x0008E9BC File Offset: 0x0008CBBC
		public NullableMapping()
		{
		}

		// Token: 0x040018CA RID: 6346
		private TypeMapping baseMapping;
	}
}
