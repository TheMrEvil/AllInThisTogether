using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Data.Odbc
{
	/// <summary>Represents a parameter to an <see cref="T:System.Data.Odbc.OdbcCommand" /> and optionally, its mapping to a <see cref="T:System.Data.DataColumn" />. This class cannot be inherited.</summary>
	// Token: 0x020002F3 RID: 755
	public sealed class OdbcParameter : DbParameter, ICloneable, IDataParameter, IDbDataParameter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class.</summary>
		// Token: 0x06002148 RID: 8520 RVA: 0x0005ADFB File Offset: 0x00058FFB
		public OdbcParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name and an <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="value">An <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</param>
		// Token: 0x06002149 RID: 8521 RVA: 0x0009C025 File Offset: 0x0009A225
		public OdbcParameter(string name, object value) : this()
		{
			this.ParameterName = name;
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name and data type.</summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="type">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type.</exception>
		// Token: 0x0600214A RID: 8522 RVA: 0x0009C03B File Offset: 0x0009A23B
		public OdbcParameter(string name, OdbcType type) : this()
		{
			this.ParameterName = name;
			this.OdbcType = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name, data type, and length.</summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="type">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values.</param>
		/// <param name="size">The length of the parameter.</param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type.</exception>
		// Token: 0x0600214B RID: 8523 RVA: 0x0009C051 File Offset: 0x0009A251
		public OdbcParameter(string name, OdbcType type, int size) : this()
		{
			this.ParameterName = name;
			this.OdbcType = type;
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name, data type, length, and source column name.</summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="type">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values.</param>
		/// <param name="size">The length of the parameter.</param>
		/// <param name="sourcecolumn">The name of the source column.</param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type.</exception>
		// Token: 0x0600214C RID: 8524 RVA: 0x0009C06E File Offset: 0x0009A26E
		public OdbcParameter(string name, OdbcType type, int size, string sourcecolumn) : this()
		{
			this.ParameterName = name;
			this.OdbcType = type;
			this.Size = size;
			this.SourceColumn = sourcecolumn;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name, data type, length, source column name, parameter direction, numeric precision, and other properties.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="odbcType">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values.</param>
		/// <param name="size">The length of the parameter.</param>
		/// <param name="parameterDirection">One of the <see cref="T:System.Data.ParameterDirection" /> values.</param>
		/// <param name="isNullable">
		///   <see langword="true" /> if the value of the field can be null; otherwise <see langword="false" />.</param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved.</param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved.</param>
		/// <param name="srcColumn">The name of the source column.</param>
		/// <param name="srcVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.Odbc.OdbcParameter" />.</param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type.</exception>
		// Token: 0x0600214D RID: 8525 RVA: 0x0009C094 File Offset: 0x0009A294
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public OdbcParameter(string parameterName, OdbcType odbcType, int size, ParameterDirection parameterDirection, bool isNullable, byte precision, byte scale, string srcColumn, DataRowVersion srcVersion, object value) : this()
		{
			this.ParameterName = parameterName;
			this.OdbcType = odbcType;
			this.Size = size;
			this.Direction = parameterDirection;
			this.IsNullable = isNullable;
			this.PrecisionInternal = precision;
			this.ScaleInternal = scale;
			this.SourceColumn = srcColumn;
			this.SourceVersion = srcVersion;
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name, data type, length, source column name, parameter direction, numeric precision, and other properties.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="odbcType">One of the <see cref="P:System.Data.Odbc.OdbcParameter.OdbcType" /> values.</param>
		/// <param name="size">The length of the parameter.</param>
		/// <param name="parameterDirection">One of the <see cref="T:System.Data.ParameterDirection" /> values.</param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved.</param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		/// <param name="sourceVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values.</param>
		/// <param name="sourceColumnNullMapping">
		///   <see langword="true" /> if the corresponding source column is nullable; <see langword="false" /> if it is not.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.Odbc.OdbcParameter" />.</param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type.</exception>
		// Token: 0x0600214E RID: 8526 RVA: 0x0009C0F4 File Offset: 0x0009A2F4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public OdbcParameter(string parameterName, OdbcType odbcType, int size, ParameterDirection parameterDirection, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value) : this()
		{
			this.ParameterName = parameterName;
			this.OdbcType = odbcType;
			this.Size = size;
			this.Direction = parameterDirection;
			this.PrecisionInternal = precision;
			this.ScaleInternal = scale;
			this.SourceColumn = sourceColumn;
			this.SourceVersion = sourceVersion;
			this.SourceColumnNullMapping = sourceColumnNullMapping;
			this.Value = value;
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.DbType" /> values. The default is <see cref="T:System.String" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property was not set to a valid <see cref="T:System.Data.DbType" />.</exception>
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x0009C154 File Offset: 0x0009A354
		// (set) Token: 0x06002150 RID: 8528 RVA: 0x0009C174 File Offset: 0x0009A374
		public override DbType DbType
		{
			get
			{
				if (this._userSpecifiedType)
				{
					return this._typemap._dbType;
				}
				return TypeMap._NVarChar._dbType;
			}
			set
			{
				if (this._typemap == null || this._typemap._dbType != value)
				{
					this.PropertyTypeChanging();
					this._typemap = TypeMap.FromDbType(value);
					this._userSpecifiedType = true;
				}
			}
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.Odbc.OdbcParameter" />.</summary>
		// Token: 0x06002151 RID: 8529 RVA: 0x0009C1A5 File Offset: 0x0009A3A5
		public override void ResetDbType()
		{
			this.ResetOdbcType();
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Odbc.OdbcType" /> of the parameter.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcType" /> value that is the <see cref="T:System.Data.Odbc.OdbcType" /> of the parameter. The default is <see langword="Nchar" />.</returns>
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06002152 RID: 8530 RVA: 0x0009C1AD File Offset: 0x0009A3AD
		// (set) Token: 0x06002153 RID: 8531 RVA: 0x0009C1CD File Offset: 0x0009A3CD
		[DbProviderSpecificTypeProperty(true)]
		[DefaultValue(OdbcType.NChar)]
		public OdbcType OdbcType
		{
			get
			{
				if (this._userSpecifiedType)
				{
					return this._typemap._odbcType;
				}
				return TypeMap._NVarChar._odbcType;
			}
			set
			{
				if (this._typemap == null || this._typemap._odbcType != value)
				{
					this.PropertyTypeChanging();
					this._typemap = TypeMap.FromOdbcType(value);
					this._userSpecifiedType = true;
				}
			}
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.Odbc.OdbcParameter" />.</summary>
		// Token: 0x06002154 RID: 8532 RVA: 0x0009C1FE File Offset: 0x0009A3FE
		public void ResetOdbcType()
		{
			this.PropertyTypeChanging();
			this._typemap = null;
			this._userSpecifiedType = false;
		}

		// Token: 0x170005F3 RID: 1523
		// (set) Token: 0x06002155 RID: 8533 RVA: 0x0009C214 File Offset: 0x0009A414
		internal bool HasChanged
		{
			set
			{
				this._hasChanged = value;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x0009C21D File Offset: 0x0009A41D
		internal bool UserSpecifiedType
		{
			get
			{
				return this._userSpecifiedType;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.Odbc.OdbcParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.Odbc.OdbcParameter" />. The default is an empty string ("").</returns>
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06002157 RID: 8535 RVA: 0x0009C228 File Offset: 0x0009A428
		// (set) Token: 0x06002158 RID: 8536 RVA: 0x0009C246 File Offset: 0x0009A446
		public override string ParameterName
		{
			get
			{
				string parameterName = this._parameterName;
				if (parameterName == null)
				{
					return ADP.StrEmpty;
				}
				return parameterName;
			}
			set
			{
				if (this._parameterName != value)
				{
					this.PropertyChanging();
					this._parameterName = value;
				}
			}
		}

		/// <summary>Gets or sets the number of digits used to represent the <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> property.</summary>
		/// <returns>The maximum number of digits used to represent the <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> property. The default value is 0, which indicates that the data provider sets the precision for <see cref="P:System.Data.Odbc.OdbcParameter.Value" />.</returns>
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06002159 RID: 8537 RVA: 0x0009C263 File Offset: 0x0009A463
		// (set) Token: 0x0600215A RID: 8538 RVA: 0x0009C26B File Offset: 0x0009A46B
		public new byte Precision
		{
			get
			{
				return this.PrecisionInternal;
			}
			set
			{
				this.PrecisionInternal = value;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600215B RID: 8539 RVA: 0x0009C274 File Offset: 0x0009A474
		// (set) Token: 0x0600215C RID: 8540 RVA: 0x0009C299 File Offset: 0x0009A499
		internal byte PrecisionInternal
		{
			get
			{
				byte b = this._precision;
				if (b == 0)
				{
					b = this.ValuePrecision(this.Value);
				}
				return b;
			}
			set
			{
				if (this._precision != value)
				{
					this.PropertyChanging();
					this._precision = value;
				}
			}
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0009C2B1 File Offset: 0x0009A4B1
		private bool ShouldSerializePrecision()
		{
			return this._precision > 0;
		}

		/// <summary>Gets or sets the number of decimal places to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved.</summary>
		/// <returns>The number of decimal places to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved. The default is 0.</returns>
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600215E RID: 8542 RVA: 0x0009C2BC File Offset: 0x0009A4BC
		// (set) Token: 0x0600215F RID: 8543 RVA: 0x0009C2C4 File Offset: 0x0009A4C4
		public new byte Scale
		{
			get
			{
				return this.ScaleInternal;
			}
			set
			{
				this.ScaleInternal = value;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06002160 RID: 8544 RVA: 0x0009C2D0 File Offset: 0x0009A4D0
		// (set) Token: 0x06002161 RID: 8545 RVA: 0x0009C2FB File Offset: 0x0009A4FB
		internal byte ScaleInternal
		{
			get
			{
				byte b = this._scale;
				if (!this.ShouldSerializeScale(b))
				{
					b = this.ValueScale(this.Value);
				}
				return b;
			}
			set
			{
				if (this._scale != value || !this._hasScale)
				{
					this.PropertyChanging();
					this._scale = value;
					this._hasScale = true;
				}
			}
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x0009C322 File Offset: 0x0009A522
		private bool ShouldSerializeScale()
		{
			return this.ShouldSerializeScale(this._scale);
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x0009C330 File Offset: 0x0009A530
		private bool ShouldSerializeScale(byte scale)
		{
			return this._hasScale && (scale != 0 || this.ShouldSerializePrecision());
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x0009C348 File Offset: 0x0009A548
		private int GetColumnSize(object value, int offset, int ordinal)
		{
			if (ODBC32.SQL_C.NUMERIC == this._bindtype._sql_c && this._internalPrecision != 0)
			{
				return Math.Min((int)this._internalPrecision, 29);
			}
			int num = this._bindtype._columnSize;
			if (0 >= num)
			{
				if (ODBC32.SQL_C.NUMERIC == this._typemap._sql_c)
				{
					num = 62;
				}
				else
				{
					num = this._internalSize;
					if (!this._internalShouldSerializeSize || 1073741823 <= num || num < 0)
					{
						if (!this._internalShouldSerializeSize && (ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0)
						{
							throw ADP.UninitializedParameterSize(ordinal, this._bindtype._type);
						}
						if (value == null || Convert.IsDBNull(value))
						{
							num = 0;
						}
						else if (value is string)
						{
							num = ((string)value).Length - offset;
							if ((ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0 && 1073741823 <= this._internalSize)
							{
								num = Math.Max(num, 4096);
							}
							if (ODBC32.SQL_TYPE.CHAR == this._bindtype._sql_type || ODBC32.SQL_TYPE.VARCHAR == this._bindtype._sql_type || ODBC32.SQL_TYPE.LONGVARCHAR == this._bindtype._sql_type)
							{
								num = Encoding.Default.GetMaxByteCount(num);
							}
						}
						else if (value is char[])
						{
							num = ((char[])value).Length - offset;
							if ((ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0 && 1073741823 <= this._internalSize)
							{
								num = Math.Max(num, 4096);
							}
							if (ODBC32.SQL_TYPE.CHAR == this._bindtype._sql_type || ODBC32.SQL_TYPE.VARCHAR == this._bindtype._sql_type || ODBC32.SQL_TYPE.LONGVARCHAR == this._bindtype._sql_type)
							{
								num = Encoding.Default.GetMaxByteCount(num);
							}
						}
						else if (value is byte[])
						{
							num = ((byte[])value).Length - offset;
							if ((ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0 && 1073741823 <= this._internalSize)
							{
								num = Math.Max(num, 8192);
							}
						}
						num = Math.Max(2, num);
					}
				}
			}
			return num;
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x0009C51C File Offset: 0x0009A71C
		private int GetValueSize(object value, int offset)
		{
			if (ODBC32.SQL_C.NUMERIC == this._bindtype._sql_c && this._internalPrecision != 0)
			{
				return Math.Min((int)this._internalPrecision, 29);
			}
			int num = this._bindtype._columnSize;
			if (0 >= num)
			{
				bool flag = false;
				if (value is string)
				{
					num = ((string)value).Length - offset;
					flag = true;
				}
				else if (value is char[])
				{
					num = ((char[])value).Length - offset;
					flag = true;
				}
				else if (value is byte[])
				{
					num = ((byte[])value).Length - offset;
				}
				else
				{
					num = 0;
				}
				if (this._internalShouldSerializeSize && this._internalSize >= 0 && this._internalSize < num && this._bindtype == this._originalbindtype)
				{
					num = this._internalSize;
				}
				if (flag)
				{
					num *= 2;
				}
			}
			return num;
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x0009C5E4 File Offset: 0x0009A7E4
		private int GetParameterSize(object value, int offset, int ordinal)
		{
			int num = this._bindtype._bufferSize;
			if (0 >= num)
			{
				if (ODBC32.SQL_C.NUMERIC == this._typemap._sql_c)
				{
					num = 518;
				}
				else
				{
					num = this._internalSize;
					if (!this._internalShouldSerializeSize || 1073741823 <= num || num < 0)
					{
						if (num <= 0 && (ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0)
						{
							throw ADP.UninitializedParameterSize(ordinal, this._bindtype._type);
						}
						if (value == null || Convert.IsDBNull(value))
						{
							if (this._bindtype._sql_c == ODBC32.SQL_C.WCHAR)
							{
								num = 2;
							}
							else
							{
								num = 0;
							}
						}
						else if (value is string)
						{
							num = (((string)value).Length - offset) * 2 + 2;
						}
						else if (value is char[])
						{
							num = (((char[])value).Length - offset) * 2 + 2;
						}
						else if (value is byte[])
						{
							num = ((byte[])value).Length - offset;
						}
						if ((ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0 && 1073741823 <= this._internalSize)
						{
							num = Math.Max(num, 8192);
						}
					}
					else if (ODBC32.SQL_C.WCHAR == this._bindtype._sql_c)
					{
						if (value is string && num < ((string)value).Length && this._bindtype == this._originalbindtype)
						{
							num = ((string)value).Length;
						}
						num = num * 2 + 2;
					}
					else if (value is byte[] && num < ((byte[])value).Length && this._bindtype == this._originalbindtype)
					{
						num = ((byte[])value).Length;
					}
				}
			}
			return num;
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x0009C760 File Offset: 0x0009A960
		private byte GetParameterPrecision(object value)
		{
			if (this._internalPrecision != 0 && value is decimal)
			{
				if (this._internalPrecision < 29)
				{
					if (this._internalPrecision != 0)
					{
						byte precision = ((decimal)value).Precision;
						this._internalPrecision = Math.Max(this._internalPrecision, precision);
					}
					return this._internalPrecision;
				}
				return 29;
			}
			else
			{
				if (value == null || value is decimal || Convert.IsDBNull(value))
				{
					return 28;
				}
				return 0;
			}
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x0009C7D8 File Offset: 0x0009A9D8
		private byte GetParameterScale(object value)
		{
			if (!(value is decimal))
			{
				return this._internalScale;
			}
			byte b = (byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16);
			if (this._internalScale > 0 && this._internalScale < b)
			{
				return this._internalScale;
			}
			return b;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x06002169 RID: 8553 RVA: 0x0009C826 File Offset: 0x0009AA26
		object ICloneable.Clone()
		{
			return new OdbcParameter(this);
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x0009C830 File Offset: 0x0009AA30
		private void CopyParameterInternal()
		{
			this._internalValue = this.Value;
			this._internalPrecision = (this.ShouldSerializePrecision() ? this.PrecisionInternal : this.ValuePrecision(this._internalValue));
			this._internalShouldSerializeSize = this.ShouldSerializeSize();
			this._internalSize = (this._internalShouldSerializeSize ? this.Size : this.ValueSize(this._internalValue));
			this._internalDirection = this.Direction;
			this._internalScale = (this.ShouldSerializeScale() ? this.ScaleInternal : this.ValueScale(this._internalValue));
			this._internalOffset = this.Offset;
			this._internalUserSpecifiedType = this.UserSpecifiedType;
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0009C8E0 File Offset: 0x0009AAE0
		private void CloneHelper(OdbcParameter destination)
		{
			this.CloneHelperCore(destination);
			destination._userSpecifiedType = this._userSpecifiedType;
			destination._typemap = this._typemap;
			destination._parameterName = this._parameterName;
			destination._precision = this._precision;
			destination._scale = this._scale;
			destination._hasScale = this._hasScale;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0009C93C File Offset: 0x0009AB3C
		internal void ClearBinding()
		{
			if (!this._userSpecifiedType)
			{
				this._typemap = null;
			}
			this._bindtype = null;
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0009C954 File Offset: 0x0009AB54
		internal void PrepareForBind(OdbcCommand command, short ordinal, ref int parameterBufferSize)
		{
			this.CopyParameterInternal();
			object obj = this.ProcessAndGetParameterValue();
			int num = this._internalOffset;
			int num2 = this._internalSize;
			if (num > 0)
			{
				if (obj is string)
				{
					if (num > ((string)obj).Length)
					{
						throw ADP.OffsetOutOfRangeException();
					}
				}
				else if (obj is char[])
				{
					if (num > ((char[])obj).Length)
					{
						throw ADP.OffsetOutOfRangeException();
					}
				}
				else if (obj is byte[])
				{
					if (num > ((byte[])obj).Length)
					{
						throw ADP.OffsetOutOfRangeException();
					}
				}
				else
				{
					num = 0;
				}
			}
			ODBC32.SQL_TYPE sql_type = this._bindtype._sql_type;
			if (sql_type - ODBC32.SQL_TYPE.WLONGVARCHAR > 2)
			{
				if (sql_type != ODBC32.SQL_TYPE.BIGINT)
				{
					if (sql_type - ODBC32.SQL_TYPE.NUMERIC <= 1 && (!command.Connection.IsV3Driver || !command.Connection.TestTypeSupport(ODBC32.SQL_TYPE.NUMERIC) || command.Connection.TestRestrictedSqlBindType(this._bindtype._sql_type)))
					{
						this._bindtype = TypeMap._VarChar;
						if (obj != null && !Convert.IsDBNull(obj))
						{
							obj = ((decimal)obj).ToString(CultureInfo.CurrentCulture);
							num2 = ((string)obj).Length;
							num = 0;
						}
					}
				}
				else if (!command.Connection.IsV3Driver)
				{
					this._bindtype = TypeMap._VarChar;
					if (obj != null && !Convert.IsDBNull(obj))
					{
						obj = ((long)obj).ToString(CultureInfo.CurrentCulture);
						num2 = ((string)obj).Length;
						num = 0;
					}
				}
			}
			else
			{
				if (obj is char)
				{
					obj = obj.ToString();
					num2 = ((string)obj).Length;
					num = 0;
				}
				if (!command.Connection.TestTypeSupport(this._bindtype._sql_type))
				{
					if (ODBC32.SQL_TYPE.WCHAR == this._bindtype._sql_type)
					{
						this._bindtype = TypeMap._Char;
					}
					else if (ODBC32.SQL_TYPE.WVARCHAR == this._bindtype._sql_type)
					{
						this._bindtype = TypeMap._VarChar;
					}
					else if (ODBC32.SQL_TYPE.WLONGVARCHAR == this._bindtype._sql_type)
					{
						this._bindtype = TypeMap._Text;
					}
				}
			}
			ODBC32.SQL_C sql_C = this._bindtype._sql_c;
			if (!command.Connection.IsV3Driver && sql_C == ODBC32.SQL_C.WCHAR)
			{
				sql_C = ODBC32.SQL_C.CHAR;
				if (obj != null && !Convert.IsDBNull(obj) && obj is string)
				{
					obj = Encoding.GetEncoding(new CultureInfo(CultureInfo.CurrentCulture.LCID).TextInfo.ANSICodePage).GetBytes(obj.ToString());
					num2 = ((byte[])obj).Length;
				}
			}
			int parameterSize = this.GetParameterSize(obj, num, (int)ordinal);
			sql_type = this._bindtype._sql_type;
			if (sql_type != ODBC32.SQL_TYPE.WVARCHAR)
			{
				if (sql_type != ODBC32.SQL_TYPE.VARBINARY)
				{
					if (sql_type == ODBC32.SQL_TYPE.VARCHAR)
					{
						if (num2 > 8000)
						{
							this._bindtype = TypeMap._Text;
						}
					}
				}
				else if (num2 > 8000)
				{
					this._bindtype = TypeMap._Image;
				}
			}
			else if (num2 > 4000)
			{
				this._bindtype = TypeMap._NText;
			}
			this._prepared_Sql_C_Type = sql_C;
			this._preparedOffset = num;
			this._preparedSize = num2;
			this._preparedValue = obj;
			this._preparedBufferSize = parameterSize;
			this._preparedIntOffset = parameterBufferSize;
			this._preparedValueOffset = this._preparedIntOffset + IntPtr.Size;
			parameterBufferSize += parameterSize + IntPtr.Size;
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x0009CC6C File Offset: 0x0009AE6C
		internal void Bind(OdbcStatementHandle hstmt, OdbcCommand command, short ordinal, CNativeBuffer parameterBuffer, bool allowReentrance)
		{
			ODBC32.SQL_C prepared_Sql_C_Type = this._prepared_Sql_C_Type;
			ODBC32.SQL_PARAM sql_PARAM = this.SqlDirectionFromParameterDirection();
			int preparedOffset = this._preparedOffset;
			int preparedSize = this._preparedSize;
			object obj = this._preparedValue;
			int valueSize = this.GetValueSize(obj, preparedOffset);
			int columnSize = this.GetColumnSize(obj, preparedOffset, (int)ordinal);
			byte parameterPrecision = this.GetParameterPrecision(obj);
			byte b = this.GetParameterScale(obj);
			HandleRef handleRef = parameterBuffer.PtrOffset(this._preparedValueOffset, this._preparedBufferSize);
			HandleRef intbuffer = parameterBuffer.PtrOffset(this._preparedIntOffset, IntPtr.Size);
			if (ODBC32.SQL_C.NUMERIC == prepared_Sql_C_Type)
			{
				if (ODBC32.SQL_PARAM.INPUT_OUTPUT == sql_PARAM && obj is decimal && b < this._internalScale)
				{
					while (b < this._internalScale)
					{
						obj = (decimal)obj * 10m;
						b += 1;
					}
				}
				this.SetInputValue(obj, prepared_Sql_C_Type, valueSize, (int)parameterPrecision, 0, parameterBuffer);
				if (ODBC32.SQL_PARAM.INPUT != sql_PARAM)
				{
					parameterBuffer.WriteInt16(this._preparedValueOffset, (short)((int)b << 8 | (int)parameterPrecision));
				}
			}
			else
			{
				this.SetInputValue(obj, prepared_Sql_C_Type, valueSize, preparedSize, preparedOffset, parameterBuffer);
			}
			if (!this._hasChanged && this._boundSqlCType == prepared_Sql_C_Type && this._boundParameterType == this._bindtype._sql_type && this._boundSize == columnSize && this._boundScale == (int)b && this._boundBuffer == handleRef.Handle && this._boundIntbuffer == intbuffer.Handle)
			{
				return;
			}
			ODBC32.RetCode retCode = hstmt.BindParameter(ordinal, (short)sql_PARAM, prepared_Sql_C_Type, this._bindtype._sql_type, (IntPtr)columnSize, (IntPtr)((int)b), handleRef, (IntPtr)this._preparedBufferSize, intbuffer);
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				if ("07006" == command.GetDiagSqlState())
				{
					command.Connection.FlagRestrictedSqlBindType(this._bindtype._sql_type);
					if (allowReentrance)
					{
						this.Bind(hstmt, command, ordinal, parameterBuffer, false);
						return;
					}
				}
				command.Connection.HandleError(hstmt, retCode);
			}
			this._hasChanged = false;
			this._boundSqlCType = prepared_Sql_C_Type;
			this._boundParameterType = this._bindtype._sql_type;
			this._boundSize = columnSize;
			this._boundScale = (int)b;
			this._boundBuffer = handleRef.Handle;
			this._boundIntbuffer = intbuffer.Handle;
			if (ODBC32.SQL_C.NUMERIC == prepared_Sql_C_Type)
			{
				OdbcDescriptorHandle descriptorHandle = command.GetDescriptorHandle(ODBC32.SQL_ATTR.APP_PARAM_DESC);
				retCode = descriptorHandle.SetDescriptionField1(ordinal, ODBC32.SQL_DESC.TYPE, (IntPtr)2);
				if (retCode != ODBC32.RetCode.SUCCESS)
				{
					command.Connection.HandleError(hstmt, retCode);
				}
				int value = (int)parameterPrecision;
				retCode = descriptorHandle.SetDescriptionField1(ordinal, ODBC32.SQL_DESC.PRECISION, (IntPtr)value);
				if (retCode != ODBC32.RetCode.SUCCESS)
				{
					command.Connection.HandleError(hstmt, retCode);
				}
				value = (int)b;
				retCode = descriptorHandle.SetDescriptionField1(ordinal, ODBC32.SQL_DESC.SCALE, (IntPtr)value);
				if (retCode != ODBC32.RetCode.SUCCESS)
				{
					command.Connection.HandleError(hstmt, retCode);
				}
				retCode = descriptorHandle.SetDescriptionField2(ordinal, ODBC32.SQL_DESC.DATA_PTR, handleRef);
				if (retCode != ODBC32.RetCode.SUCCESS)
				{
					command.Connection.HandleError(hstmt, retCode);
				}
			}
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0009CF40 File Offset: 0x0009B140
		internal void GetOutputValue(CNativeBuffer parameterBuffer)
		{
			if (this._hasChanged)
			{
				return;
			}
			if (this._bindtype != null && this._internalDirection != ParameterDirection.Input)
			{
				TypeMap bindtype = this._bindtype;
				this._bindtype = null;
				int num = (int)parameterBuffer.ReadIntPtr(this._preparedIntOffset);
				if (-1 == num)
				{
					this.Value = DBNull.Value;
					return;
				}
				if (0 <= num || num == -3)
				{
					this.Value = parameterBuffer.MarshalToManaged(this._preparedValueOffset, this._boundSqlCType, num);
					if (this._boundSqlCType == ODBC32.SQL_C.CHAR && this.Value != null && !Convert.IsDBNull(this.Value))
					{
						Encoding encoding = Encoding.GetEncoding(new CultureInfo(CultureInfo.CurrentCulture.LCID).TextInfo.ANSICodePage);
						this.Value = encoding.GetString((byte[])this.Value);
					}
					if (bindtype != this._typemap && this.Value != null && !Convert.IsDBNull(this.Value) && this.Value.GetType() != this._typemap._type)
					{
						this.Value = decimal.Parse((string)this.Value, CultureInfo.CurrentCulture);
					}
				}
			}
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0009D074 File Offset: 0x0009B274
		private object ProcessAndGetParameterValue()
		{
			object obj = this._internalValue;
			if (this._internalUserSpecifiedType)
			{
				if (obj != null && !Convert.IsDBNull(obj))
				{
					Type type = obj.GetType();
					if (!type.IsArray)
					{
						if (!(type != this._typemap._type))
						{
							goto IL_CE;
						}
						try
						{
							obj = Convert.ChangeType(obj, this._typemap._type, null);
							goto IL_CE;
						}
						catch (Exception ex)
						{
							if (!ADP.IsCatchableExceptionType(ex))
							{
								throw;
							}
							throw ADP.ParameterConversionFailed(obj, this._typemap._type, ex);
						}
					}
					if (type == typeof(char[]))
					{
						obj = new string((char[])obj);
					}
				}
			}
			else if (this._typemap == null)
			{
				if (obj == null || Convert.IsDBNull(obj))
				{
					this._typemap = TypeMap._NVarChar;
				}
				else
				{
					Type type2 = obj.GetType();
					this._typemap = TypeMap.FromSystemType(type2);
				}
			}
			IL_CE:
			this._originalbindtype = (this._bindtype = this._typemap);
			return obj;
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0009D178 File Offset: 0x0009B378
		private void PropertyChanging()
		{
			this._hasChanged = true;
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x0009D181 File Offset: 0x0009B381
		private void PropertyTypeChanging()
		{
			this.PropertyChanging();
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x0009D18C File Offset: 0x0009B38C
		internal void SetInputValue(object value, ODBC32.SQL_C sql_c_type, int cbsize, int sizeorprecision, int offset, CNativeBuffer parameterBuffer)
		{
			if (ParameterDirection.Input != this._internalDirection && ParameterDirection.InputOutput != this._internalDirection)
			{
				this._internalValue = null;
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, (IntPtr)(-1));
				return;
			}
			if (value == null)
			{
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, (IntPtr)(-5));
				return;
			}
			if (Convert.IsDBNull(value))
			{
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, (IntPtr)(-1));
				return;
			}
			if (sql_c_type == ODBC32.SQL_C.WCHAR || sql_c_type == ODBC32.SQL_C.BINARY || sql_c_type == ODBC32.SQL_C.CHAR)
			{
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, (IntPtr)cbsize);
			}
			else
			{
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, IntPtr.Zero);
			}
			parameterBuffer.MarshalToNative(this._preparedValueOffset, value, sql_c_type, sizeorprecision, offset);
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0009D244 File Offset: 0x0009B444
		private ODBC32.SQL_PARAM SqlDirectionFromParameterDirection()
		{
			switch (this._internalDirection)
			{
			case ParameterDirection.Input:
				return ODBC32.SQL_PARAM.INPUT;
			case ParameterDirection.Output:
			case ParameterDirection.ReturnValue:
				return ODBC32.SQL_PARAM.OUTPUT;
			case ParameterDirection.InputOutput:
				return ODBC32.SQL_PARAM.INPUT_OUTPUT;
			}
			return ODBC32.SQL_PARAM.INPUT;
		}

		/// <summary>Gets or sets the value of the parameter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06002175 RID: 8565 RVA: 0x0009D281 File Offset: 0x0009B481
		// (set) Token: 0x06002176 RID: 8566 RVA: 0x0009D289 File Offset: 0x0009B489
		public override object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._coercedValue = null;
				this._value = value;
			}
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x0009D299 File Offset: 0x0009B499
		private byte ValuePrecision(object value)
		{
			return this.ValuePrecisionCore(value);
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x0009D2A2 File Offset: 0x0009B4A2
		private byte ValueScale(object value)
		{
			return this.ValueScaleCore(value);
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x0009D2AB File Offset: 0x0009B4AB
		private int ValueSize(object value)
		{
			return this.ValueSizeCore(value);
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0009D2B4 File Offset: 0x0009B4B4
		private OdbcParameter(OdbcParameter source) : this()
		{
			ADP.CheckArgumentNull(source, "source");
			source.CloneHelper(this);
			ICloneable cloneable = this._value as ICloneable;
			if (cloneable != null)
			{
				this._value = cloneable.Clone();
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x0009D2F4 File Offset: 0x0009B4F4
		// (set) Token: 0x0600217C RID: 8572 RVA: 0x0009D2FC File Offset: 0x0009B4FC
		private object CoercedValue
		{
			get
			{
				return this._coercedValue;
			}
			set
			{
				this._coercedValue = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a stored procedure return value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is <see langword="Input" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x0009D308 File Offset: 0x0009B508
		// (set) Token: 0x0600217E RID: 8574 RVA: 0x0009D322 File Offset: 0x0009B522
		public override ParameterDirection Direction
		{
			get
			{
				ParameterDirection direction = this._direction;
				if (direction == (ParameterDirection)0)
				{
					return ParameterDirection.Input;
				}
				return direction;
			}
			set
			{
				if (this._direction == value)
				{
					return;
				}
				if (value - ParameterDirection.Input <= 2 || value == ParameterDirection.ReturnValue)
				{
					this.PropertyChanging();
					this._direction = value;
					return;
				}
				throw ADP.InvalidParameterDirection(value);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter accepts null values.</summary>
		/// <returns>
		///   <see langword="true" /> if null values are accepted; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x0009D34C File Offset: 0x0009B54C
		// (set) Token: 0x06002180 RID: 8576 RVA: 0x0009D354 File Offset: 0x0009B554
		public override bool IsNullable
		{
			get
			{
				return this._isNullable;
			}
			set
			{
				this._isNullable = value;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x0009D35D File Offset: 0x0009B55D
		// (set) Token: 0x06002182 RID: 8578 RVA: 0x0009D365 File Offset: 0x0009B565
		public int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidOffsetValue(value);
				}
				this._offset = value;
			}
		}

		/// <summary>Gets or sets the maximum size of the data within the column.</summary>
		/// <returns>The maximum size of the data within the column. The default value is inferred from the parameter value.</returns>
		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x0009D37C File Offset: 0x0009B57C
		// (set) Token: 0x06002184 RID: 8580 RVA: 0x0009D3A1 File Offset: 0x0009B5A1
		public override int Size
		{
			get
			{
				int num = this._size;
				if (num == 0)
				{
					num = this.ValueSize(this.Value);
				}
				return num;
			}
			set
			{
				if (this._size != value)
				{
					if (value < -1)
					{
						throw ADP.InvalidSizeValue(value);
					}
					this.PropertyChanging();
					this._size = value;
				}
			}
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x0009D3C4 File Offset: 0x0009B5C4
		private bool ShouldSerializeSize()
		{
			return this._size != 0;
		}

		/// <summary>Gets or sets the name of the source column mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.Odbc.OdbcParameter.Value" />.</summary>
		/// <returns>The name of the source column that will be used to set the value of this parameter. The default is an empty string ("").</returns>
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x0009D3D0 File Offset: 0x0009B5D0
		// (set) Token: 0x06002187 RID: 8583 RVA: 0x0009D3EE File Offset: 0x0009B5EE
		public override string SourceColumn
		{
			get
			{
				string sourceColumn = this._sourceColumn;
				if (sourceColumn == null)
				{
					return ADP.StrEmpty;
				}
				return sourceColumn;
			}
			set
			{
				this._sourceColumn = value;
			}
		}

		/// <summary>Sets or gets a value which indicates whether the source column is nullable. This lets <see cref="T:System.Data.Common.DbCommandBuilder" /> correctly generate Update statements for nullable columns.</summary>
		/// <returns>
		///   <see langword="true" /> if the source column is nullable; <see langword="false" /> if it is not.</returns>
		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x0009D3F7 File Offset: 0x0009B5F7
		// (set) Token: 0x06002189 RID: 8585 RVA: 0x0009D3FF File Offset: 0x0009B5FF
		public override bool SourceColumnNullMapping
		{
			get
			{
				return this._sourceColumnNullMapping;
			}
			set
			{
				this._sourceColumnNullMapping = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when you load <see cref="P:System.Data.Odbc.OdbcParameter.Value" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is Current.</returns>
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x0009D408 File Offset: 0x0009B608
		// (set) Token: 0x0600218B RID: 8587 RVA: 0x0009D426 File Offset: 0x0009B626
		public override DataRowVersion SourceVersion
		{
			get
			{
				DataRowVersion sourceVersion = this._sourceVersion;
				if (sourceVersion == (DataRowVersion)0)
				{
					return DataRowVersion.Current;
				}
				return sourceVersion;
			}
			set
			{
				if (value <= DataRowVersion.Current)
				{
					if (value != DataRowVersion.Original && value != DataRowVersion.Current)
					{
						goto IL_32;
					}
				}
				else if (value != DataRowVersion.Proposed && value != DataRowVersion.Default)
				{
					goto IL_32;
				}
				this._sourceVersion = value;
				return;
				IL_32:
				throw ADP.InvalidDataRowVersion(value);
			}
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x0009D460 File Offset: 0x0009B660
		private void CloneHelperCore(OdbcParameter destination)
		{
			destination._value = this._value;
			destination._direction = this._direction;
			destination._size = this._size;
			destination._offset = this._offset;
			destination._sourceColumn = this._sourceColumn;
			destination._sourceVersion = this._sourceVersion;
			destination._sourceColumnNullMapping = this._sourceColumnNullMapping;
			destination._isNullable = this._isNullable;
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x0009D4D0 File Offset: 0x0009B6D0
		internal object CompareExchangeParent(object value, object comparand)
		{
			object parent = this._parent;
			if (comparand == parent)
			{
				this._parent = value;
			}
			return parent;
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x0009D4F0 File Offset: 0x0009B6F0
		internal void ResetParent()
		{
			this._parent = null;
		}

		/// <summary>Gets a string that contains the <see cref="P:System.Data.Odbc.OdbcParameter.ParameterName" />.</summary>
		/// <returns>A string that contains the <see cref="P:System.Data.Odbc.OdbcParameter.ParameterName" />.</returns>
		// Token: 0x0600218F RID: 8591 RVA: 0x00079529 File Offset: 0x00077729
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x0009D4FC File Offset: 0x0009B6FC
		private byte ValuePrecisionCore(object value)
		{
			if (value is decimal)
			{
				return ((decimal)value).Precision;
			}
			return 0;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x0007955E File Offset: 0x0007775E
		private byte ValueScaleCore(object value)
		{
			if (value is decimal)
			{
				return (byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16);
			}
			return 0;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x0009D528 File Offset: 0x0009B728
		private int ValueSizeCore(object value)
		{
			if (!ADP.IsNull(value))
			{
				string text = value as string;
				if (text != null)
				{
					return text.Length;
				}
				byte[] array = value as byte[];
				if (array != null)
				{
					return array.Length;
				}
				char[] array2 = value as char[];
				if (array2 != null)
				{
					return array2.Length;
				}
				if (value is byte || value is char)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x040017D1 RID: 6097
		private bool _hasChanged;

		// Token: 0x040017D2 RID: 6098
		private bool _userSpecifiedType;

		// Token: 0x040017D3 RID: 6099
		private TypeMap _typemap;

		// Token: 0x040017D4 RID: 6100
		private TypeMap _bindtype;

		// Token: 0x040017D5 RID: 6101
		private string _parameterName;

		// Token: 0x040017D6 RID: 6102
		private byte _precision;

		// Token: 0x040017D7 RID: 6103
		private byte _scale;

		// Token: 0x040017D8 RID: 6104
		private bool _hasScale;

		// Token: 0x040017D9 RID: 6105
		private ODBC32.SQL_C _boundSqlCType;

		// Token: 0x040017DA RID: 6106
		private ODBC32.SQL_TYPE _boundParameterType;

		// Token: 0x040017DB RID: 6107
		private int _boundSize;

		// Token: 0x040017DC RID: 6108
		private int _boundScale;

		// Token: 0x040017DD RID: 6109
		private IntPtr _boundBuffer;

		// Token: 0x040017DE RID: 6110
		private IntPtr _boundIntbuffer;

		// Token: 0x040017DF RID: 6111
		private TypeMap _originalbindtype;

		// Token: 0x040017E0 RID: 6112
		private byte _internalPrecision;

		// Token: 0x040017E1 RID: 6113
		private bool _internalShouldSerializeSize;

		// Token: 0x040017E2 RID: 6114
		private int _internalSize;

		// Token: 0x040017E3 RID: 6115
		private ParameterDirection _internalDirection;

		// Token: 0x040017E4 RID: 6116
		private byte _internalScale;

		// Token: 0x040017E5 RID: 6117
		private int _internalOffset;

		// Token: 0x040017E6 RID: 6118
		internal bool _internalUserSpecifiedType;

		// Token: 0x040017E7 RID: 6119
		private object _internalValue;

		// Token: 0x040017E8 RID: 6120
		private int _preparedOffset;

		// Token: 0x040017E9 RID: 6121
		private int _preparedSize;

		// Token: 0x040017EA RID: 6122
		private int _preparedBufferSize;

		// Token: 0x040017EB RID: 6123
		private object _preparedValue;

		// Token: 0x040017EC RID: 6124
		private int _preparedIntOffset;

		// Token: 0x040017ED RID: 6125
		private int _preparedValueOffset;

		// Token: 0x040017EE RID: 6126
		private ODBC32.SQL_C _prepared_Sql_C_Type;

		// Token: 0x040017EF RID: 6127
		private object _value;

		// Token: 0x040017F0 RID: 6128
		private object _parent;

		// Token: 0x040017F1 RID: 6129
		private ParameterDirection _direction;

		// Token: 0x040017F2 RID: 6130
		private int _size;

		// Token: 0x040017F3 RID: 6131
		private int _offset;

		// Token: 0x040017F4 RID: 6132
		private string _sourceColumn;

		// Token: 0x040017F5 RID: 6133
		private DataRowVersion _sourceVersion;

		// Token: 0x040017F6 RID: 6134
		private bool _sourceColumnNullMapping;

		// Token: 0x040017F7 RID: 6135
		private bool _isNullable;

		// Token: 0x040017F8 RID: 6136
		private object _coercedValue;
	}
}
