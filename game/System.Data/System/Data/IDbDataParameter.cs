using System;

namespace System.Data
{
	/// <summary>Used by the Visual Basic .NET Data Designers to represent a parameter to a Command object, and optionally, its mapping to <see cref="T:System.Data.DataSet" /> columns.</summary>
	// Token: 0x02000106 RID: 262
	public interface IDbDataParameter : IDataParameter
	{
		/// <summary>Indicates the precision of numeric parameters.</summary>
		/// <returns>The maximum number of digits used to represent the Value property of a data provider Parameter object. The default value is 0, which indicates that a data provider sets the precision for Value.</returns>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000F6C RID: 3948
		// (set) Token: 0x06000F6D RID: 3949
		byte Precision { get; set; }

		/// <summary>Indicates the scale of numeric parameters.</summary>
		/// <returns>The number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved. The default is 0.</returns>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000F6E RID: 3950
		// (set) Token: 0x06000F6F RID: 3951
		byte Scale { get; set; }

		/// <summary>The size of the parameter.</summary>
		/// <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the parameter value.</returns>
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000F70 RID: 3952
		// (set) Token: 0x06000F71 RID: 3953
		int Size { get; set; }
	}
}
