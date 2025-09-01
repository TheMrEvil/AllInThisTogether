using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Represents a parameter to an <see cref="T:System.Data.OleDb.OleDbCommand" /> and optionally its mapping to a <see cref="T:System.Data.DataSet" /> column. This class cannot be inherited.</summary>
	// Token: 0x0200016D RID: 365
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbParameter : DbParameter, IDataParameter, IDbDataParameter, ICloneable
	{
		/// <summary>Gets or sets the <see cref="T:System.Data.DbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.DbType" /> values. The default is <see cref="F:System.Data.DbType.String" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property was not set to a valid <see cref="T:System.Data.DbType" />.</exception>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x0600137A RID: 4986 RVA: 0x00007EED File Offset: 0x000060ED
		public override DbType DbType
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a stored procedure return-value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is <see langword="Input" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x0600137C RID: 4988 RVA: 0x00007EED File Offset: 0x000060ED
		public override ParameterDirection Direction
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter accepts null values.</summary>
		/// <returns>
		///   <see langword="true" /> if null values are accepted; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x0600137E RID: 4990 RVA: 0x00007EED File Offset: 0x000060ED
		public override bool IsNullable
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x06001380 RID: 4992 RVA: 0x00007EED File Offset: 0x000060ED
		public int Offset
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbType" /> of the parameter.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbType" /> of the parameter. The default is <see cref="F:System.Data.OleDb.OleDbType.VarWChar" />.</returns>
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x06001382 RID: 4994 RVA: 0x00007EED File Offset: 0x000060ED
		public OleDbType OleDbType
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.OleDb.OleDbParameter" />. The default is an empty string ("").</returns>
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x06001384 RID: 4996 RVA: 0x00007EED File Offset: 0x000060ED
		public override string ParameterName
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the maximum number of digits used to represent the <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> property.</summary>
		/// <returns>The maximum number of digits used to represent the <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> property. The default value is 0, which indicates that the data provider sets the precision for <see cref="P:System.Data.OleDb.OleDbParameter.Value" />.</returns>
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x06001386 RID: 4998 RVA: 0x00007EED File Offset: 0x000060ED
		public new byte Precision
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved.</summary>
		/// <returns>The number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved. The default is 0.</returns>
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x06001388 RID: 5000 RVA: 0x00007EED File Offset: 0x000060ED
		public new byte Scale
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the maximum size, in bytes, of the data within the column.</summary>
		/// <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the parameter value.</returns>
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x0600138A RID: 5002 RVA: 0x00007EED File Offset: 0x000060ED
		public override int Size
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the name of the source column mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.OleDb.OleDbParameter.Value" />.</summary>
		/// <returns>The name of the source column mapped to the <see cref="T:System.Data.DataSet" />. The default is an empty string.</returns>
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x0600138C RID: 5004 RVA: 0x00007EED File Offset: 0x000060ED
		public override string SourceColumn
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Sets or gets a value which indicates whether the source column is nullable. This allows <see cref="T:System.Data.Common.DbCommandBuilder" /> to correctly generate Update statements for nullable columns.</summary>
		/// <returns>
		///   <see langword="true" /> if the source column is nullable; <see langword="false" /> if it is not.</returns>
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x0600138E RID: 5006 RVA: 0x00007EED File Offset: 0x000060ED
		public override bool SourceColumnNullMapping
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when you load <see cref="P:System.Data.OleDb.OleDbParameter.Value" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is <see langword="Current" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set to one of the <see cref="T:System.Data.DataRowVersion" /> values.</exception>
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x06001390 RID: 5008 RVA: 0x00007EED File Offset: 0x000060ED
		public override DataRowVersion SourceVersion
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the value of the parameter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x06001392 RID: 5010 RVA: 0x00007EED File Offset: 0x000060ED
		public override object Value
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class.</summary>
		// Token: 0x06001393 RID: 5011 RVA: 0x0005ADFB File Offset: 0x00058FFB
		public OleDbParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name and data type.</summary>
		/// <param name="name">The name of the parameter to map.</param>
		/// <param name="dataType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type.</exception>
		// Token: 0x06001394 RID: 5012 RVA: 0x0005AE03 File Offset: 0x00059003
		public OleDbParameter(string name, OleDbType dataType)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name, data type, and length.</summary>
		/// <param name="name">The name of the parameter to map.</param>
		/// <param name="dataType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values.</param>
		/// <param name="size">The length of the parameter.</param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type.</exception>
		// Token: 0x06001395 RID: 5013 RVA: 0x0005AE03 File Offset: 0x00059003
		public OleDbParameter(string name, OleDbType dataType, int size)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name, data type, length, source column name, parameter direction, numeric precision, and other properties.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="dbType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values.</param>
		/// <param name="size">The length of the parameter.</param>
		/// <param name="direction">One of the <see cref="T:System.Data.ParameterDirection" /> values.</param>
		/// <param name="isNullable">
		///   <see langword="true" /> if the value of the field can be null; otherwise <see langword="false" />.</param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved.</param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved.</param>
		/// <param name="srcColumn">The name of the source column.</param>
		/// <param name="srcVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.OleDb.OleDbParameter" />.</param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type.</exception>
		// Token: 0x06001396 RID: 5014 RVA: 0x0005AE03 File Offset: 0x00059003
		public OleDbParameter(string parameterName, OleDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string srcColumn, DataRowVersion srcVersion, object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name, data type, length, source column name, parameter direction, numeric precision, and other properties.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="dbType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values.</param>
		/// <param name="size">The length of the parameter.</param>
		/// <param name="direction">One of the <see cref="T:System.Data.ParameterDirection" /> values.</param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved.</param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		/// <param name="sourceVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values.</param>
		/// <param name="sourceColumnNullMapping">
		///   <see langword="true" /> if the source column is nullable; <see langword="false" /> if it is not.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.OleDb.OleDbParameter" />.</param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type.</exception>
		// Token: 0x06001397 RID: 5015 RVA: 0x0005AE03 File Offset: 0x00059003
		public OleDbParameter(string parameterName, OleDbType dbType, int size, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name, data type, length, and source column name.</summary>
		/// <param name="name">The name of the parameter to map.</param>
		/// <param name="dataType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values.</param>
		/// <param name="size">The length of the parameter.</param>
		/// <param name="srcColumn">The name of the source column.</param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type.</exception>
		// Token: 0x06001398 RID: 5016 RVA: 0x0005AE03 File Offset: 0x00059003
		public OleDbParameter(string name, OleDbType dataType, int size, string srcColumn)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name and the value of the new <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
		/// <param name="name">The name of the parameter to map.</param>
		/// <param name="value">The value of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</param>
		// Token: 0x06001399 RID: 5017 RVA: 0x0005AE03 File Offset: 0x00059003
		public OleDbParameter(string name, object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
		// Token: 0x0600139A RID: 5018 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override void ResetDbType()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets a string that contains the <see cref="P:System.Data.OleDb.OleDbParameter.ParameterName" />.</summary>
		/// <returns>A string that contains the <see cref="P:System.Data.OleDb.OleDbParameter.ParameterName" />.</returns>
		// Token: 0x0600139B RID: 5019 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override string ToString()
		{
			throw ADP.OleDb();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x0600139C RID: 5020 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		object ICloneable.Clone()
		{
			throw ADP.OleDb();
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
		// Token: 0x0600139D RID: 5021 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public void ResetOleDbType()
		{
			throw ADP.OleDb();
		}
	}
}
