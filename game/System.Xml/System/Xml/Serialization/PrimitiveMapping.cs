using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000288 RID: 648
	internal class PrimitiveMapping : TypeMapping
	{
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x0008E9AB File Offset: 0x0008CBAB
		// (set) Token: 0x06001875 RID: 6261 RVA: 0x0008E9B3 File Offset: 0x0008CBB3
		internal override bool IsList
		{
			get
			{
				return this.isList;
			}
			set
			{
				this.isList = value;
			}
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x0008E9BC File Offset: 0x0008CBBC
		public PrimitiveMapping()
		{
		}

		// Token: 0x040018C9 RID: 6345
		private bool isList;
	}
}
