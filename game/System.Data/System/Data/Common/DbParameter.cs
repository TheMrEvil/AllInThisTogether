using System;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Represents a parameter to a <see cref="T:System.Data.Common.DbCommand" /> and optionally, its mapping to a <see cref="T:System.Data.DataSet" /> column. For more information on parameters, see Configuring Parameters and Parameter Data Types.</summary>
	// Token: 0x0200039B RID: 923
	public abstract class DbParameter : MarshalByRefObject, IDbDataParameter, IDataParameter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbParameter" /> class.</summary>
		// Token: 0x06002CBA RID: 11450 RVA: 0x00003DB9 File Offset: 0x00001FB9
		protected DbParameter()
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.DbType" /> values. The default is <see cref="F:System.Data.DbType.String" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property is not set to a valid <see cref="T:System.Data.DbType" />.</exception>
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002CBB RID: 11451
		// (set) Token: 0x06002CBC RID: 11452
		[RefreshProperties(RefreshProperties.All)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public abstract DbType DbType { get; set; }

		/// <summary>Resets the DbType property to its original settings.</summary>
		// Token: 0x06002CBD RID: 11453
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public abstract void ResetDbType();

		/// <summary>Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a stored procedure return value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is <see langword="Input" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property is not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06002CBE RID: 11454
		// (set) Token: 0x06002CBF RID: 11455
		[DefaultValue(ParameterDirection.Input)]
		[RefreshProperties(RefreshProperties.All)]
		public abstract ParameterDirection Direction { get; set; }

		/// <summary>Gets or sets a value that indicates whether the parameter accepts null values.</summary>
		/// <returns>
		///   <see langword="true" /> if null values are accepted; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06002CC0 RID: 11456
		// (set) Token: 0x06002CC1 RID: 11457
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignOnly(true)]
		[Browsable(false)]
		public abstract bool IsNullable { get; set; }

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.Common.DbParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.Common.DbParameter" />. The default is an empty string ("").</returns>
		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002CC2 RID: 11458
		// (set) Token: 0x06002CC3 RID: 11459
		[DefaultValue("")]
		public abstract string ParameterName { get; set; }

		/// <summary>Indicates the precision of numeric parameters.</summary>
		/// <returns>The maximum number of digits used to represent the <see langword="Value" /> property of a data provider <see langword="Parameter" /> object. The default value is 0, which indicates that a data provider sets the precision for <see langword="Value" />.</returns>
		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x00006D64 File Offset: 0x00004F64
		// (set) Token: 0x06002CC5 RID: 11461 RVA: 0x00007EED File Offset: 0x000060ED
		byte IDbDataParameter.Precision
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataParameter.Scale" />.</summary>
		/// <returns>The number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved. The default is 0.</returns>
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002CC6 RID: 11462 RVA: 0x00006D64 File Offset: 0x00004F64
		// (set) Token: 0x06002CC7 RID: 11463 RVA: 0x00007EED File Offset: 0x000060ED
		byte IDbDataParameter.Scale
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the maximum number of digits used to represent the <see cref="P:System.Data.Common.DbParameter.Value" /> property.</summary>
		/// <returns>The maximum number of digits used to represent the <see cref="P:System.Data.Common.DbParameter.Value" /> property.</returns>
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002CC8 RID: 11464 RVA: 0x000BEEBF File Offset: 0x000BD0BF
		// (set) Token: 0x06002CC9 RID: 11465 RVA: 0x000BEEC7 File Offset: 0x000BD0C7
		public virtual byte Precision
		{
			get
			{
				return ((IDbDataParameter)this).Precision;
			}
			set
			{
				((IDbDataParameter)this).Precision = value;
			}
		}

		/// <summary>Gets or sets the number of decimal places to which <see cref="P:System.Data.Common.DbParameter.Value" /> is resolved.</summary>
		/// <returns>The number of decimal places to which <see cref="P:System.Data.Common.DbParameter.Value" /> is resolved.</returns>
		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x000BEED0 File Offset: 0x000BD0D0
		// (set) Token: 0x06002CCB RID: 11467 RVA: 0x000BEED8 File Offset: 0x000BD0D8
		public virtual byte Scale
		{
			get
			{
				return ((IDbDataParameter)this).Scale;
			}
			set
			{
				((IDbDataParameter)this).Scale = value;
			}
		}

		/// <summary>Gets or sets the maximum size, in bytes, of the data within the column.</summary>
		/// <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the parameter value.</returns>
		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002CCC RID: 11468
		// (set) Token: 0x06002CCD RID: 11469
		public abstract int Size { get; set; }

		/// <summary>Gets or sets the name of the source column mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.Common.DbParameter.Value" />.</summary>
		/// <returns>The name of the source column mapped to the <see cref="T:System.Data.DataSet" />. The default is an empty string.</returns>
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06002CCE RID: 11470
		// (set) Token: 0x06002CCF RID: 11471
		[DefaultValue("")]
		public abstract string SourceColumn { get; set; }

		/// <summary>Sets or gets a value which indicates whether the source column is nullable. This allows <see cref="T:System.Data.Common.DbCommandBuilder" /> to correctly generate Update statements for nullable columns.</summary>
		/// <returns>
		///   <see langword="true" /> if the source column is nullable; <see langword="false" /> if it is not.</returns>
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06002CD0 RID: 11472
		// (set) Token: 0x06002CD1 RID: 11473
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public abstract bool SourceColumnNullMapping { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when you load <see cref="P:System.Data.Common.DbParameter.Value" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is <see langword="Current" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property is not set to one of the <see cref="T:System.Data.DataRowVersion" /> values.</exception>
		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x000BEEE1 File Offset: 0x000BD0E1
		// (set) Token: 0x06002CD3 RID: 11475 RVA: 0x00007EED File Offset: 0x000060ED
		[DefaultValue(DataRowVersion.Current)]
		public virtual DataRowVersion SourceVersion
		{
			get
			{
				return DataRowVersion.Default;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the value of the parameter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06002CD4 RID: 11476
		// (set) Token: 0x06002CD5 RID: 11477
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(null)]
		public abstract object Value { get; set; }
	}
}
