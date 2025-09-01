using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000195 RID: 405
	internal sealed class Row
	{
		// Token: 0x06001470 RID: 5232 RVA: 0x0005C95C File Offset: 0x0005AB5C
		internal Row(int rowCount)
		{
			this._dataFields = new object[rowCount];
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0005C970 File Offset: 0x0005AB70
		internal object[] DataFields
		{
			get
			{
				return this._dataFields;
			}
		}

		// Token: 0x17000387 RID: 903
		internal object this[int index]
		{
			get
			{
				return this._dataFields[index];
			}
		}

		// Token: 0x04000CE6 RID: 3302
		private object[] _dataFields;
	}
}
