using System;

namespace System.Data
{
	/// <summary>Represents a parameter to a Command object, and optionally, its mapping to <see cref="T:System.Data.DataSet" /> columns; and is implemented by .NET Framework data providers that access data sources.</summary>
	// Token: 0x020000FF RID: 255
	public interface IDataParameter
	{
		/// <summary>Gets or sets the <see cref="T:System.Data.DbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.DbType" /> values. The default is <see cref="F:System.Data.DbType.String" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property was not set to a valid <see cref="T:System.Data.DbType" />.</exception>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000F13 RID: 3859
		// (set) Token: 0x06000F14 RID: 3860
		DbType DbType { get; set; }

		/// <summary>Gets or sets a value indicating whether the parameter is input-only, output-only, bidirectional, or a stored procedure return value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is <see langword="Input" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000F15 RID: 3861
		// (set) Token: 0x06000F16 RID: 3862
		ParameterDirection Direction { get; set; }

		/// <summary>Gets a value indicating whether the parameter accepts null values.</summary>
		/// <returns>
		///   <see langword="true" /> if null values are accepted; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000F17 RID: 3863
		bool IsNullable { get; }

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.IDataParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.IDataParameter" />. The default is an empty string.</returns>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000F18 RID: 3864
		// (set) Token: 0x06000F19 RID: 3865
		string ParameterName { get; set; }

		/// <summary>Gets or sets the name of the source column that is mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.IDataParameter.Value" />.</summary>
		/// <returns>The name of the source column that is mapped to the <see cref="T:System.Data.DataSet" />. The default is an empty string.</returns>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000F1A RID: 3866
		// (set) Token: 0x06000F1B RID: 3867
		string SourceColumn { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when loading <see cref="P:System.Data.IDataParameter.Value" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is <see langword="Current" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set one of the <see cref="T:System.Data.DataRowVersion" /> values.</exception>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000F1C RID: 3868
		// (set) Token: 0x06000F1D RID: 3869
		DataRowVersion SourceVersion { get; set; }

		/// <summary>Gets or sets the value of the parameter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000F1E RID: 3870
		// (set) Token: 0x06000F1F RID: 3871
		object Value { get; set; }
	}
}
