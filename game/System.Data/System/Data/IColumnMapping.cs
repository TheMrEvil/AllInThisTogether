using System;

namespace System.Data
{
	/// <summary>Associates a data source column with a <see cref="T:System.Data.DataSet" /> column, and is implemented by the <see cref="T:System.Data.Common.DataColumnMapping" /> class, which is used in common by .NET Framework data providers.</summary>
	// Token: 0x020000FC RID: 252
	public interface IColumnMapping
	{
		/// <summary>Gets or sets the name of the column within the <see cref="T:System.Data.DataSet" /> to map to.</summary>
		/// <returns>The name of the column within the <see cref="T:System.Data.DataSet" /> to map to. The name is not case sensitive.</returns>
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000EFF RID: 3839
		// (set) Token: 0x06000F00 RID: 3840
		string DataSetColumn { get; set; }

		/// <summary>Gets or sets the name of the column within the data source to map from. The name is case-sensitive.</summary>
		/// <returns>The case-sensitive name of the column in the data source.</returns>
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000F01 RID: 3841
		// (set) Token: 0x06000F02 RID: 3842
		string SourceColumn { get; set; }
	}
}
