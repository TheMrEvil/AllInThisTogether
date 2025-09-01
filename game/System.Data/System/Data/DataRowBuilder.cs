using System;
using Unity;

namespace System.Data
{
	/// <summary>The <see langword="DataRowBuilder" /> type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
	// Token: 0x020000BE RID: 190
	public sealed class DataRowBuilder
	{
		// Token: 0x06000BFA RID: 3066 RVA: 0x00031FC0 File Offset: 0x000301C0
		internal DataRowBuilder(DataTable table, int record)
		{
			this._table = table;
			this._record = record;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x000108A6 File Offset: 0x0000EAA6
		internal DataRowBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040007D8 RID: 2008
		internal readonly DataTable _table;

		// Token: 0x040007D9 RID: 2009
		internal int _record;
	}
}
