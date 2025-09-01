using System;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Data
{
	/// <summary>Represents a restriction on a set of columns in which all values must be unique.</summary>
	// Token: 0x02000137 RID: 311
	[DefaultProperty("ConstraintName")]
	public class UniqueConstraint : Constraint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name and <see cref="T:System.Data.DataColumn" />.</summary>
		/// <param name="name">The name of the constraint.</param>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to constrain.</param>
		// Token: 0x060010B3 RID: 4275 RVA: 0x00045C54 File Offset: 0x00043E54
		public UniqueConstraint(string name, DataColumn column)
		{
			this.Create(name, new DataColumn[]
			{
				column
			});
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified <see cref="T:System.Data.DataColumn" />.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to constrain.</param>
		// Token: 0x060010B4 RID: 4276 RVA: 0x00045C7C File Offset: 0x00043E7C
		public UniqueConstraint(DataColumn column)
		{
			this.Create(null, new DataColumn[]
			{
				column
			});
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name and array of <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="name">The name of the constraint.</param>
		/// <param name="columns">The array of <see cref="T:System.Data.DataColumn" /> objects to constrain.</param>
		// Token: 0x060010B5 RID: 4277 RVA: 0x00045CA2 File Offset: 0x00043EA2
		public UniqueConstraint(string name, DataColumn[] columns)
		{
			this.Create(name, columns);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the given array of <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="columns">The array of <see cref="T:System.Data.DataColumn" /> objects to constrain.</param>
		// Token: 0x060010B6 RID: 4278 RVA: 0x00045CB2 File Offset: 0x00043EB2
		public UniqueConstraint(DataColumn[] columns)
		{
			this.Create(null, columns);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name, an array of <see cref="T:System.Data.DataColumn" /> objects to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="name">The name of the constraint.</param>
		/// <param name="columnNames">An array of <see cref="T:System.Data.DataColumn" /> objects to constrain.</param>
		/// <param name="isPrimaryKey">
		///   <see langword="true" /> to indicate that the constraint is a primary key; otherwise, <see langword="false" />.</param>
		// Token: 0x060010B7 RID: 4279 RVA: 0x00045CC2 File Offset: 0x00043EC2
		[Browsable(false)]
		public UniqueConstraint(string name, string[] columnNames, bool isPrimaryKey)
		{
			this._constraintName = name;
			this._columnNames = columnNames;
			this._bPrimaryKey = isPrimaryKey;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name, the <see cref="T:System.Data.DataColumn" /> to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="name">The name of the constraint.</param>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to constrain.</param>
		/// <param name="isPrimaryKey">
		///   <see langword="true" /> to indicate that the constraint is a primary key; otherwise, <see langword="false" />.</param>
		// Token: 0x060010B8 RID: 4280 RVA: 0x00045CE0 File Offset: 0x00043EE0
		public UniqueConstraint(string name, DataColumn column, bool isPrimaryKey)
		{
			DataColumn[] columns = new DataColumn[]
			{
				column
			};
			this._bPrimaryKey = isPrimaryKey;
			this.Create(name, columns);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the <see cref="T:System.Data.DataColumn" /> to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to constrain.</param>
		/// <param name="isPrimaryKey">
		///   <see langword="true" /> to indicate that the constraint is a primary key; otherwise, <see langword="false" />.</param>
		// Token: 0x060010B9 RID: 4281 RVA: 0x00045D10 File Offset: 0x00043F10
		public UniqueConstraint(DataColumn column, bool isPrimaryKey)
		{
			DataColumn[] columns = new DataColumn[]
			{
				column
			};
			this._bPrimaryKey = isPrimaryKey;
			this.Create(null, columns);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name, an array of <see cref="T:System.Data.DataColumn" /> objects to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="name">The name of the constraint.</param>
		/// <param name="columns">An array of <see cref="T:System.Data.DataColumn" /> objects to constrain.</param>
		/// <param name="isPrimaryKey">
		///   <see langword="true" /> to indicate that the constraint is a primary key; otherwise, <see langword="false" />.</param>
		// Token: 0x060010BA RID: 4282 RVA: 0x00045D3D File Offset: 0x00043F3D
		public UniqueConstraint(string name, DataColumn[] columns, bool isPrimaryKey)
		{
			this._bPrimaryKey = isPrimaryKey;
			this.Create(name, columns);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with an array of <see cref="T:System.Data.DataColumn" /> objects to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="columns">An array of <see cref="T:System.Data.DataColumn" /> objects to constrain.</param>
		/// <param name="isPrimaryKey">
		///   <see langword="true" /> to indicate that the constraint is a primary key; otherwise, <see langword="false" />.</param>
		// Token: 0x060010BB RID: 4283 RVA: 0x00045D54 File Offset: 0x00043F54
		public UniqueConstraint(DataColumn[] columns, bool isPrimaryKey)
		{
			this._bPrimaryKey = isPrimaryKey;
			this.Create(null, columns);
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x00045D6B File Offset: 0x00043F6B
		internal string[] ColumnNames
		{
			get
			{
				return this._key.GetColumnNames();
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x00045D78 File Offset: 0x00043F78
		internal Index ConstraintIndex
		{
			get
			{
				return this._constraintIndex;
			}
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00045D80 File Offset: 0x00043F80
		[Conditional("DEBUG")]
		private void AssertConstraintAndKeyIndexes()
		{
			DataColumn[] array = new DataColumn[this._constraintIndex._indexFields.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._constraintIndex._indexFields[i].Column;
			}
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00045DC7 File Offset: 0x00043FC7
		internal void ConstraintIndexClear()
		{
			if (this._constraintIndex != null)
			{
				this._constraintIndex.RemoveRef();
				this._constraintIndex = null;
			}
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00045DE4 File Offset: 0x00043FE4
		internal void ConstraintIndexInitialize()
		{
			if (this._constraintIndex == null)
			{
				this._constraintIndex = this._key.GetSortIndex();
				this._constraintIndex.AddRef();
			}
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00045E0A File Offset: 0x0004400A
		internal override void CheckState()
		{
			this.NonVirtualCheckState();
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00045E12 File Offset: 0x00044012
		private void NonVirtualCheckState()
		{
			this._key.CheckState();
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00007EED File Offset: 0x000060ED
		internal override void CheckCanAddToCollection(ConstraintCollection constraints)
		{
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00045E20 File Offset: 0x00044020
		internal override bool CanBeRemovedFromCollection(ConstraintCollection constraints, bool fThrowException)
		{
			if (!this.Equals(constraints.Table._primaryKey))
			{
				ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this.Table.DataSet, this.Table);
				while (parentForeignKeyConstraintEnumerator.GetNext())
				{
					ForeignKeyConstraint foreignKeyConstraint = parentForeignKeyConstraintEnumerator.GetForeignKeyConstraint();
					if (this._key.ColumnsEqual(foreignKeyConstraint.ParentKey))
					{
						if (!fThrowException)
						{
							return false;
						}
						throw ExceptionBuilder.NeededForForeignKeyConstraint(this, foreignKeyConstraint);
					}
				}
				return true;
			}
			if (!fThrowException)
			{
				return false;
			}
			throw ExceptionBuilder.RemovePrimaryKey(constraints.Table);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00045E9A File Offset: 0x0004409A
		internal override bool CanEnableConstraint()
		{
			return !this.Table.EnforceConstraints || this.ConstraintIndex.CheckUnique();
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00045EB8 File Offset: 0x000440B8
		internal override bool IsConstraintViolated()
		{
			bool result = false;
			Index constraintIndex = this.ConstraintIndex;
			if (constraintIndex.HasDuplicates)
			{
				object[] uniqueKeyValues = constraintIndex.GetUniqueKeyValues();
				for (int i = 0; i < uniqueKeyValues.Length; i++)
				{
					Range range = constraintIndex.FindRecords((object[])uniqueKeyValues[i]);
					if (1 < range.Count)
					{
						DataRow[] rows = constraintIndex.GetRows(range);
						string text = ExceptionBuilder.UniqueConstraintViolationText(this._key.ColumnsReference, (object[])uniqueKeyValues[i]);
						for (int j = 0; j < rows.Length; j++)
						{
							rows[j].RowError = text;
							foreach (DataColumn column in this._key.ColumnsReference)
							{
								rows[j].SetColumnError(column, text);
							}
						}
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00045F8C File Offset: 0x0004418C
		internal override void CheckConstraint(DataRow row, DataRowAction action)
		{
			if (this.Table.EnforceConstraints && (action == DataRowAction.Add || action == DataRowAction.Change || (action == DataRowAction.Rollback && row._tempRecord != -1)) && row.HaveValuesChanged(this.ColumnsReference) && this.ConstraintIndex.IsKeyRecordInIndex(row.GetDefaultRecord()))
			{
				object[] columnValues = row.GetColumnValues(this.ColumnsReference);
				throw ExceptionBuilder.ConstraintViolation(this.ColumnsReference, columnValues);
			}
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00045FF7 File Offset: 0x000441F7
		internal override bool ContainsColumn(DataColumn column)
		{
			return this._key.ContainsColumn(column);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0003E727 File Offset: 0x0003C927
		internal override Constraint Clone(DataSet destination)
		{
			return this.Clone(destination, false);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00046008 File Offset: 0x00044208
		internal override Constraint Clone(DataSet destination, bool ignorNSforTableLookup)
		{
			int num;
			if (ignorNSforTableLookup)
			{
				num = destination.Tables.IndexOf(this.Table.TableName);
			}
			else
			{
				num = destination.Tables.IndexOf(this.Table.TableName, this.Table.Namespace, false);
			}
			if (num < 0)
			{
				return null;
			}
			DataTable dataTable = destination.Tables[num];
			int num2 = this.ColumnsReference.Length;
			DataColumn[] array = new DataColumn[num2];
			for (int i = 0; i < num2; i++)
			{
				DataColumn dataColumn = this.ColumnsReference[i];
				num = dataTable.Columns.IndexOf(dataColumn.ColumnName);
				if (num < 0)
				{
					return null;
				}
				array[i] = dataTable.Columns[num];
			}
			UniqueConstraint uniqueConstraint = new UniqueConstraint(this.ConstraintName, array);
			foreach (object key in base.ExtendedProperties.Keys)
			{
				uniqueConstraint.ExtendedProperties[key] = base.ExtendedProperties[key];
			}
			return uniqueConstraint;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00046134 File Offset: 0x00044334
		internal UniqueConstraint Clone(DataTable table)
		{
			int num = this.ColumnsReference.Length;
			DataColumn[] array = new DataColumn[num];
			for (int i = 0; i < num; i++)
			{
				DataColumn dataColumn = this.ColumnsReference[i];
				int num2 = table.Columns.IndexOf(dataColumn.ColumnName);
				if (num2 < 0)
				{
					return null;
				}
				array[i] = table.Columns[num2];
			}
			UniqueConstraint uniqueConstraint = new UniqueConstraint(this.ConstraintName, array);
			foreach (object key in base.ExtendedProperties.Keys)
			{
				uniqueConstraint.ExtendedProperties[key] = base.ExtendedProperties[key];
			}
			return uniqueConstraint;
		}

		/// <summary>Gets the array of columns that this constraint affects.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects.</returns>
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x00046208 File Offset: 0x00044408
		[ReadOnly(true)]
		public virtual DataColumn[] Columns
		{
			get
			{
				return this._key.ToArray();
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x00046215 File Offset: 0x00044415
		internal DataColumn[] ColumnsReference
		{
			get
			{
				return this._key.ColumnsReference;
			}
		}

		/// <summary>Gets a value indicating whether or not the constraint is on a primary key.</summary>
		/// <returns>
		///   <see langword="true" />, if the constraint is on a primary key; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x00046222 File Offset: 0x00044422
		public bool IsPrimaryKey
		{
			get
			{
				return this.Table != null && this == this.Table._primaryKey;
			}
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0004623C File Offset: 0x0004443C
		private void Create(string constraintName, DataColumn[] columns)
		{
			for (int i = 0; i < columns.Length; i++)
			{
				if (columns[i].Computed)
				{
					throw ExceptionBuilder.ExpressionInConstraint(columns[i]);
				}
			}
			this._key = new DataKey(columns, true);
			this.ConstraintName = constraintName;
			this.NonVirtualCheckState();
		}

		/// <summary>Compares this constraint to a second to determine if both are identical.</summary>
		/// <param name="key2">The object to which this <see cref="T:System.Data.UniqueConstraint" /> is compared.</param>
		/// <returns>
		///   <see langword="true" />, if the contraints are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060010D0 RID: 4304 RVA: 0x00046284 File Offset: 0x00044484
		public override bool Equals(object key2)
		{
			return key2 is UniqueConstraint && this.Key.ColumnsEqual(((UniqueConstraint)key2).Key);
		}

		/// <summary>Gets the hash code of this instance of the <see cref="T:System.Data.UniqueConstraint" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060010D1 RID: 4305 RVA: 0x0003EB4E File Offset: 0x0003CD4E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x170002E1 RID: 737
		// (set) Token: 0x060010D2 RID: 4306 RVA: 0x000462B4 File Offset: 0x000444B4
		internal override bool InCollection
		{
			set
			{
				base.InCollection = value;
				if (this._key.ColumnsReference.Length == 1)
				{
					this._key.ColumnsReference[0].InternalUnique(value);
				}
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x000462E0 File Offset: 0x000444E0
		internal DataKey Key
		{
			get
			{
				return this._key;
			}
		}

		/// <summary>Gets the table to which this constraint belongs.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> to which the constraint belongs.</returns>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x000462E8 File Offset: 0x000444E8
		[ReadOnly(true)]
		public override DataTable Table
		{
			get
			{
				if (this._key.HasValue)
				{
					return this._key.Table;
				}
				return null;
			}
		}

		// Token: 0x04000A4B RID: 2635
		private DataKey _key;

		// Token: 0x04000A4C RID: 2636
		private Index _constraintIndex;

		// Token: 0x04000A4D RID: 2637
		internal bool _bPrimaryKey;

		// Token: 0x04000A4E RID: 2638
		internal string _constraintName;

		// Token: 0x04000A4F RID: 2639
		internal string[] _columnNames;
	}
}
