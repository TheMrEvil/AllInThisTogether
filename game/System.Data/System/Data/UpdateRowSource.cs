using System;

namespace System.Data
{
	/// <summary>Specifies how query command results are applied to the row being updated.</summary>
	// Token: 0x02000138 RID: 312
	public enum UpdateRowSource
	{
		/// <summary>Any returned parameters or rows are ignored.</summary>
		// Token: 0x04000A51 RID: 2641
		None,
		/// <summary>Output parameters are mapped to the changed row in the <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x04000A52 RID: 2642
		OutputParameters,
		/// <summary>The data in the first returned row is mapped to the changed row in the <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x04000A53 RID: 2643
		FirstReturnedRecord,
		/// <summary>Both the output parameters and the first returned row are mapped to the changed row in the <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x04000A54 RID: 2644
		Both
	}
}
