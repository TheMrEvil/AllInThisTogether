using System;

namespace System.Data
{
	// Token: 0x020000F5 RID: 245
	internal interface IFilter
	{
		// Token: 0x06000E9C RID: 3740
		bool Invoke(DataRow row, DataRowVersion version);
	}
}
