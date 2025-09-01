using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000261 RID: 609
	internal sealed class SqlEnvChange
	{
		// Token: 0x06001CC6 RID: 7366 RVA: 0x00003D93 File Offset: 0x00001F93
		public SqlEnvChange()
		{
		}

		// Token: 0x040013BC RID: 5052
		internal byte type;

		// Token: 0x040013BD RID: 5053
		internal byte oldLength;

		// Token: 0x040013BE RID: 5054
		internal int newLength;

		// Token: 0x040013BF RID: 5055
		internal int length;

		// Token: 0x040013C0 RID: 5056
		internal string newValue;

		// Token: 0x040013C1 RID: 5057
		internal string oldValue;

		// Token: 0x040013C2 RID: 5058
		internal byte[] newBinValue;

		// Token: 0x040013C3 RID: 5059
		internal byte[] oldBinValue;

		// Token: 0x040013C4 RID: 5060
		internal long newLongValue;

		// Token: 0x040013C5 RID: 5061
		internal long oldLongValue;

		// Token: 0x040013C6 RID: 5062
		internal SqlCollation newCollation;

		// Token: 0x040013C7 RID: 5063
		internal SqlCollation oldCollation;

		// Token: 0x040013C8 RID: 5064
		internal RoutingInfo newRoutingInfo;
	}
}
