using System;
using System.Runtime.CompilerServices;

namespace System.Data.Common
{
	/// <summary>Represents a column within a data source.</summary>
	// Token: 0x0200038A RID: 906
	public abstract class DbColumn
	{
		/// <summary>Gets a nullable boolean value that indicates whether <see langword="DBNull" /> values are allowed in this column, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether <see langword="DBNull" /> values are allowed in this column, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
		/// <returns>Returns <see langword="true" /> if <see langword="DBNull" /> values are allowed in this column; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06002B20 RID: 11040 RVA: 0x000BB337 File Offset: 0x000B9537
		// (set) Token: 0x06002B21 RID: 11041 RVA: 0x000BB33F File Offset: 0x000B953F
		public bool? AllowDBNull
		{
			[CompilerGenerated]
			get
			{
				return this.<AllowDBNull>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<AllowDBNull>k__BackingField = value;
			}
		}

		/// <summary>Gets the catalog name associated with the data source; otherwise, <see langword="null" /> if no value is set. Can be set to either the catalog name or <see langword="null" /> when overridden in a derived class.</summary>
		/// <returns>The catalog name associated with the data source; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002B22 RID: 11042 RVA: 0x000BB348 File Offset: 0x000B9548
		// (set) Token: 0x06002B23 RID: 11043 RVA: 0x000BB350 File Offset: 0x000B9550
		public string BaseCatalogName
		{
			[CompilerGenerated]
			get
			{
				return this.<BaseCatalogName>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<BaseCatalogName>k__BackingField = value;
			}
		}

		/// <summary>Gets the base column name; otherwise, <see langword="null" /> if no value is set. Can be set to either the column name or <see langword="null" /> when overridden in a derived class.</summary>
		/// <returns>The base column name; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x000BB359 File Offset: 0x000B9559
		// (set) Token: 0x06002B25 RID: 11045 RVA: 0x000BB361 File Offset: 0x000B9561
		public string BaseColumnName
		{
			[CompilerGenerated]
			get
			{
				return this.<BaseColumnName>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<BaseColumnName>k__BackingField = value;
			}
		}

		/// <summary>Gets the schema name associated with the data source; otherwise, <see langword="null" /> if no value is set. Can be set to either the schema name or <see langword="null" /> when overridden in a derived class.</summary>
		/// <returns>The schema name associated with the data source; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x000BB36A File Offset: 0x000B956A
		// (set) Token: 0x06002B27 RID: 11047 RVA: 0x000BB372 File Offset: 0x000B9572
		public string BaseSchemaName
		{
			[CompilerGenerated]
			get
			{
				return this.<BaseSchemaName>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<BaseSchemaName>k__BackingField = value;
			}
		}

		/// <summary>Gets the server name associated with the column; otherwise, <see langword="null" /> if no value is set. Can be set to either the server name or <see langword="null" /> when overridden in a derived class.</summary>
		/// <returns>The server name associated with the column; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x000BB37B File Offset: 0x000B957B
		// (set) Token: 0x06002B29 RID: 11049 RVA: 0x000BB383 File Offset: 0x000B9583
		public string BaseServerName
		{
			[CompilerGenerated]
			get
			{
				return this.<BaseServerName>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<BaseServerName>k__BackingField = value;
			}
		}

		/// <summary>Gets the table name in the schema; otherwise, <see langword="null" /> if no value is set. Can be set to either the table name or <see langword="null" /> when overridden in a derived class.</summary>
		/// <returns>The table name in the schema; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002B2A RID: 11050 RVA: 0x000BB38C File Offset: 0x000B958C
		// (set) Token: 0x06002B2B RID: 11051 RVA: 0x000BB394 File Offset: 0x000B9594
		public string BaseTableName
		{
			[CompilerGenerated]
			get
			{
				return this.<BaseTableName>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<BaseTableName>k__BackingField = value;
			}
		}

		/// <summary>Gets the name of the column. Can be set to the column name when overridden in a derived class.</summary>
		/// <returns>The name of the column.</returns>
		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002B2C RID: 11052 RVA: 0x000BB39D File Offset: 0x000B959D
		// (set) Token: 0x06002B2D RID: 11053 RVA: 0x000BB3A5 File Offset: 0x000B95A5
		public string ColumnName
		{
			[CompilerGenerated]
			get
			{
				return this.<ColumnName>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ColumnName>k__BackingField = value;
			}
		}

		/// <summary>Gets the column position (ordinal) in the datasource row; otherwise, <see langword="null" /> if no value is set. Can be set to either an <see langword="int32" /> value to specify the column position or <see langword="null" /> when overridden in a derived class.</summary>
		/// <returns>An <see langword="int32" /> value for column ordinal; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06002B2E RID: 11054 RVA: 0x000BB3AE File Offset: 0x000B95AE
		// (set) Token: 0x06002B2F RID: 11055 RVA: 0x000BB3B6 File Offset: 0x000B95B6
		public int? ColumnOrdinal
		{
			[CompilerGenerated]
			get
			{
				return this.<ColumnOrdinal>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ColumnOrdinal>k__BackingField = value;
			}
		}

		/// <summary>Gets the column size; otherwise, <see langword="null" /> if no value is set. Can be set to either an <see langword="int32" /> value to specify the column size or <see langword="null" /> when overridden in a derived class.</summary>
		/// <returns>An <see langword="int32" /> value for column size; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06002B30 RID: 11056 RVA: 0x000BB3BF File Offset: 0x000B95BF
		// (set) Token: 0x06002B31 RID: 11057 RVA: 0x000BB3C7 File Offset: 0x000B95C7
		public int? ColumnSize
		{
			[CompilerGenerated]
			get
			{
				return this.<ColumnSize>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ColumnSize>k__BackingField = value;
			}
		}

		/// <summary>Gets a nullable boolean value that indicates whether this column is aliased, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether this column is aliased, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
		/// <returns>Returns <see langword="true" /> if this column is aliased; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002B32 RID: 11058 RVA: 0x000BB3D0 File Offset: 0x000B95D0
		// (set) Token: 0x06002B33 RID: 11059 RVA: 0x000BB3D8 File Offset: 0x000B95D8
		public bool? IsAliased
		{
			[CompilerGenerated]
			get
			{
				return this.<IsAliased>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<IsAliased>k__BackingField = value;
			}
		}

		/// <summary>Gets a nullable boolean value that indicates whether values in this column are automatically incremented, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether values in this column are automatically incremented, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
		/// <returns>Returns <see langword="true" /> if values in this column are automatically incremented; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002B34 RID: 11060 RVA: 0x000BB3E1 File Offset: 0x000B95E1
		// (set) Token: 0x06002B35 RID: 11061 RVA: 0x000BB3E9 File Offset: 0x000B95E9
		public bool? IsAutoIncrement
		{
			[CompilerGenerated]
			get
			{
				return this.<IsAutoIncrement>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<IsAutoIncrement>k__BackingField = value;
			}
		}

		/// <summary>Gets a nullable boolean value that indicates whether this column is an expression, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether this column is an expression, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
		/// <returns>Returns <see langword="true" /> if this column is an expression; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x000BB3F2 File Offset: 0x000B95F2
		// (set) Token: 0x06002B37 RID: 11063 RVA: 0x000BB3FA File Offset: 0x000B95FA
		public bool? IsExpression
		{
			[CompilerGenerated]
			get
			{
				return this.<IsExpression>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<IsExpression>k__BackingField = value;
			}
		}

		/// <summary>Gets a nullable boolean value that indicates whether this column is hidden, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether this column is hidden, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
		/// <returns>Returns <see langword="true" /> if this column is hidden; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x000BB403 File Offset: 0x000B9603
		// (set) Token: 0x06002B39 RID: 11065 RVA: 0x000BB40B File Offset: 0x000B960B
		public bool? IsHidden
		{
			[CompilerGenerated]
			get
			{
				return this.<IsHidden>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<IsHidden>k__BackingField = value;
			}
		}

		/// <summary>Gets a nullable boolean value that indicates whether this column is an identity, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether this column is an identity, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
		/// <returns>Returns <see langword="true" /> if this column is an identity; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x000BB414 File Offset: 0x000B9614
		// (set) Token: 0x06002B3B RID: 11067 RVA: 0x000BB41C File Offset: 0x000B961C
		public bool? IsIdentity
		{
			[CompilerGenerated]
			get
			{
				return this.<IsIdentity>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<IsIdentity>k__BackingField = value;
			}
		}

		/// <summary>Gets a nullable boolean value that indicates whether this column is a key, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether this column is a key, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
		/// <returns>Returns <see langword="true" /> if this column is a key; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x000BB425 File Offset: 0x000B9625
		// (set) Token: 0x06002B3D RID: 11069 RVA: 0x000BB42D File Offset: 0x000B962D
		public bool? IsKey
		{
			[CompilerGenerated]
			get
			{
				return this.<IsKey>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<IsKey>k__BackingField = value;
			}
		}

		/// <summary>Gets a nullable boolean value that indicates whether this column contains long data, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether this column contains long data, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
		/// <returns>Returns <see langword="true" /> if this column contains long data; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x000BB436 File Offset: 0x000B9636
		// (set) Token: 0x06002B3F RID: 11071 RVA: 0x000BB43E File Offset: 0x000B963E
		public bool? IsLong
		{
			[CompilerGenerated]
			get
			{
				return this.<IsLong>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<IsLong>k__BackingField = value;
			}
		}

		/// <summary>Gets a nullable boolean value that indicates whether this column is read-only, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether this column is read-only, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
		/// <returns>Returns <see langword="true" /> if this column is read-only; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x000BB447 File Offset: 0x000B9647
		// (set) Token: 0x06002B41 RID: 11073 RVA: 0x000BB44F File Offset: 0x000B964F
		public bool? IsReadOnly
		{
			[CompilerGenerated]
			get
			{
				return this.<IsReadOnly>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<IsReadOnly>k__BackingField = value;
			}
		}

		/// <summary>Gets a nullable boolean value that indicates whether a unique constraint applies to this column, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether a unique constraint applies to this column, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
		/// <returns>Returns <see langword="true" /> if a unique constraint applies to this column; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002B42 RID: 11074 RVA: 0x000BB458 File Offset: 0x000B9658
		// (set) Token: 0x06002B43 RID: 11075 RVA: 0x000BB460 File Offset: 0x000B9660
		public bool? IsUnique
		{
			[CompilerGenerated]
			get
			{
				return this.<IsUnique>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<IsUnique>k__BackingField = value;
			}
		}

		/// <summary>Gets the numeric precision of the column data; otherwise, <see langword="null" /> if no value is set. Can be set to either an <see langword="int32" /> value to specify the numeric precision of the column data or <see langword="null" /> when overridden in a derived class.</summary>
		/// <returns>An <see langword="int32" /> value that specifies the precision of the column data, if the data is numeric; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002B44 RID: 11076 RVA: 0x000BB469 File Offset: 0x000B9669
		// (set) Token: 0x06002B45 RID: 11077 RVA: 0x000BB471 File Offset: 0x000B9671
		public int? NumericPrecision
		{
			[CompilerGenerated]
			get
			{
				return this.<NumericPrecision>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<NumericPrecision>k__BackingField = value;
			}
		}

		/// <summary>Gets a nullable <see langword="int32" /> value that either returns <see langword="null" /> or the numeric scale of the column data. Can be set to either <see langword="null" /> or an <see langword="int32" /> value for the numeric scale of the column data when overridden in a derived class.</summary>
		/// <returns>A null reference (<see langword="Nothing" /> in Visual Basic) if no value is set; otherwise, a <see langword="int32" /> value that specifies the scale of the column data, if the data is numeric.</returns>
		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002B46 RID: 11078 RVA: 0x000BB47A File Offset: 0x000B967A
		// (set) Token: 0x06002B47 RID: 11079 RVA: 0x000BB482 File Offset: 0x000B9682
		public int? NumericScale
		{
			[CompilerGenerated]
			get
			{
				return this.<NumericScale>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<NumericScale>k__BackingField = value;
			}
		}

		/// <summary>Gets the assembly-qualified name of the <see cref="T:System.Type" /> object that represents the type of data in the column; otherwise, <see langword="null" /> if no value is set. Can be set to either the assembly-qualified name or <see langword="null" /> when overridden in a derived class.</summary>
		/// <returns>The assembly-qualified name of the <see cref="T:System.Type" /> object that represents the type of data in the column; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002B48 RID: 11080 RVA: 0x000BB48B File Offset: 0x000B968B
		// (set) Token: 0x06002B49 RID: 11081 RVA: 0x000BB493 File Offset: 0x000B9693
		public string UdtAssemblyQualifiedName
		{
			[CompilerGenerated]
			get
			{
				return this.<UdtAssemblyQualifiedName>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<UdtAssemblyQualifiedName>k__BackingField = value;
			}
		}

		/// <summary>Gets the type of data stored in the column. Can be set to a <see cref="T:System.Type" /> object that represents the type of data in the column when overridden in a derived class.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type of data the column contains.</returns>
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002B4A RID: 11082 RVA: 0x000BB49C File Offset: 0x000B969C
		// (set) Token: 0x06002B4B RID: 11083 RVA: 0x000BB4A4 File Offset: 0x000B96A4
		public Type DataType
		{
			[CompilerGenerated]
			get
			{
				return this.<DataType>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<DataType>k__BackingField = value;
			}
		}

		/// <summary>Gets the name of the data type; otherwise, <see langword="null" /> if no value is set. Can be set to either the data type name or <see langword="null" /> when overridden in a derived class.</summary>
		/// <returns>The name of the data type; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002B4C RID: 11084 RVA: 0x000BB4AD File Offset: 0x000B96AD
		// (set) Token: 0x06002B4D RID: 11085 RVA: 0x000BB4B5 File Offset: 0x000B96B5
		public string DataTypeName
		{
			[CompilerGenerated]
			get
			{
				return this.<DataTypeName>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<DataTypeName>k__BackingField = value;
			}
		}

		/// <summary>Gets the object based on the column property name.</summary>
		/// <param name="property">The column property name.</param>
		/// <returns>The object based on the column property name.</returns>
		// Token: 0x17000750 RID: 1872
		public virtual object this[string property]
		{
			get
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(property);
				if (num <= 2477638934U)
				{
					if (num <= 1067318116U)
					{
						if (num <= 687909556U)
						{
							if (num != 405521230U)
							{
								if (num == 687909556U)
								{
									if (property == "ColumnOrdinal")
									{
										return this.ColumnOrdinal;
									}
								}
							}
							else if (property == "DataTypeName")
							{
								return this.DataTypeName;
							}
						}
						else if (num != 720006947U)
						{
							if (num != 1005639113U)
							{
								if (num == 1067318116U)
								{
									if (property == "ColumnName")
									{
										return this.ColumnName;
									}
								}
							}
							else if (property == "IsHidden")
							{
								return this.IsHidden;
							}
						}
						else if (property == "IsLong")
						{
							return this.IsLong;
						}
					}
					else if (num <= 2215472237U)
					{
						if (num != 1154057342U)
						{
							if (num != 1309233724U)
							{
								if (num == 2215472237U)
								{
									if (property == "DataType")
									{
										return this.DataType;
									}
								}
							}
							else if (property == "IsKey")
							{
								return this.IsKey;
							}
						}
						else if (property == "ColumnSize")
						{
							return this.ColumnSize;
						}
					}
					else if (num != 2239129947U)
					{
						if (num != 2380251540U)
						{
							if (num == 2477638934U)
							{
								if (property == "IsUnique")
								{
									return this.IsUnique;
								}
							}
						}
						else if (property == "NumericPrecision")
						{
							return this.NumericPrecision;
						}
					}
					else if (property == "IsExpression")
					{
						return this.IsExpression;
					}
				}
				else if (num <= 3042527364U)
				{
					if (num <= 2711511624U)
					{
						if (num != 2504653387U)
						{
							if (num != 2586490225U)
							{
								if (num == 2711511624U)
								{
									if (property == "BaseServerName")
									{
										return this.BaseServerName;
									}
								}
							}
							else if (property == "UdtAssemblyQualifiedName")
							{
								return this.UdtAssemblyQualifiedName;
							}
						}
						else if (property == "IsIdentity")
						{
							return this.IsIdentity;
						}
					}
					else if (num != 2741140585U)
					{
						if (num != 2757192823U)
						{
							if (num == 3042527364U)
							{
								if (property == "BaseCatalogName")
								{
									return this.BaseCatalogName;
								}
							}
						}
						else if (property == "BaseTableName")
						{
							return this.BaseTableName;
						}
					}
					else if (property == "BaseColumnName")
					{
						return this.BaseColumnName;
					}
				}
				else if (num <= 3656290791U)
				{
					if (num != 3115085976U)
					{
						if (num != 3173893005U)
						{
							if (num == 3656290791U)
							{
								if (property == "IsReadOnly")
								{
									return this.IsReadOnly;
								}
							}
						}
						else if (property == "AllowDBNull")
						{
							return this.AllowDBNull;
						}
					}
					else if (property == "BaseSchemaName")
					{
						return this.BaseSchemaName;
					}
				}
				else if (num != 3912158903U)
				{
					if (num != 3938522122U)
					{
						if (num == 4233439846U)
						{
							if (property == "IsAliased")
							{
								return this.IsAliased;
							}
						}
					}
					else if (property == "NumericScale")
					{
						return this.NumericScale;
					}
				}
				else if (property == "IsAutoIncrement")
				{
					return this.IsAutoIncrement;
				}
				return null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbColumn" /> class.</summary>
		// Token: 0x06002B4F RID: 11087 RVA: 0x00003D93 File Offset: 0x00001F93
		protected DbColumn()
		{
		}

		// Token: 0x04001B1A RID: 6938
		[CompilerGenerated]
		private bool? <AllowDBNull>k__BackingField;

		// Token: 0x04001B1B RID: 6939
		[CompilerGenerated]
		private string <BaseCatalogName>k__BackingField;

		// Token: 0x04001B1C RID: 6940
		[CompilerGenerated]
		private string <BaseColumnName>k__BackingField;

		// Token: 0x04001B1D RID: 6941
		[CompilerGenerated]
		private string <BaseSchemaName>k__BackingField;

		// Token: 0x04001B1E RID: 6942
		[CompilerGenerated]
		private string <BaseServerName>k__BackingField;

		// Token: 0x04001B1F RID: 6943
		[CompilerGenerated]
		private string <BaseTableName>k__BackingField;

		// Token: 0x04001B20 RID: 6944
		[CompilerGenerated]
		private string <ColumnName>k__BackingField;

		// Token: 0x04001B21 RID: 6945
		[CompilerGenerated]
		private int? <ColumnOrdinal>k__BackingField;

		// Token: 0x04001B22 RID: 6946
		[CompilerGenerated]
		private int? <ColumnSize>k__BackingField;

		// Token: 0x04001B23 RID: 6947
		[CompilerGenerated]
		private bool? <IsAliased>k__BackingField;

		// Token: 0x04001B24 RID: 6948
		[CompilerGenerated]
		private bool? <IsAutoIncrement>k__BackingField;

		// Token: 0x04001B25 RID: 6949
		[CompilerGenerated]
		private bool? <IsExpression>k__BackingField;

		// Token: 0x04001B26 RID: 6950
		[CompilerGenerated]
		private bool? <IsHidden>k__BackingField;

		// Token: 0x04001B27 RID: 6951
		[CompilerGenerated]
		private bool? <IsIdentity>k__BackingField;

		// Token: 0x04001B28 RID: 6952
		[CompilerGenerated]
		private bool? <IsKey>k__BackingField;

		// Token: 0x04001B29 RID: 6953
		[CompilerGenerated]
		private bool? <IsLong>k__BackingField;

		// Token: 0x04001B2A RID: 6954
		[CompilerGenerated]
		private bool? <IsReadOnly>k__BackingField;

		// Token: 0x04001B2B RID: 6955
		[CompilerGenerated]
		private bool? <IsUnique>k__BackingField;

		// Token: 0x04001B2C RID: 6956
		[CompilerGenerated]
		private int? <NumericPrecision>k__BackingField;

		// Token: 0x04001B2D RID: 6957
		[CompilerGenerated]
		private int? <NumericScale>k__BackingField;

		// Token: 0x04001B2E RID: 6958
		[CompilerGenerated]
		private string <UdtAssemblyQualifiedName>k__BackingField;

		// Token: 0x04001B2F RID: 6959
		[CompilerGenerated]
		private Type <DataType>k__BackingField;

		// Token: 0x04001B30 RID: 6960
		[CompilerGenerated]
		private string <DataTypeName>k__BackingField;
	}
}
