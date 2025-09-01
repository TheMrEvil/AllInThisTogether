using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Data.SqlClient
{
	/// <summary>Collection of <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> objects that inherits from <see cref="T:System.Collections.CollectionBase" />.</summary>
	// Token: 0x020001AA RID: 426
	public sealed class SqlBulkCopyColumnMappingCollection : CollectionBase
	{
		// Token: 0x06001503 RID: 5379 RVA: 0x000604BA File Offset: 0x0005E6BA
		internal SqlBulkCopyColumnMappingCollection()
		{
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x000604C2 File Offset: 0x0005E6C2
		// (set) Token: 0x06001505 RID: 5381 RVA: 0x000604CA File Offset: 0x0005E6CA
		internal bool ReadOnly
		{
			[CompilerGenerated]
			get
			{
				return this.<ReadOnly>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ReadOnly>k__BackingField = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> to find.</param>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object.</returns>
		// Token: 0x17000398 RID: 920
		public SqlBulkCopyColumnMapping this[int index]
		{
			get
			{
				return (SqlBulkCopyColumnMapping)base.List[index];
			}
		}

		/// <summary>Adds the specified mapping to the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" />.</summary>
		/// <param name="bulkCopyColumnMapping">The <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object that describes the mapping to be added to the collection.</param>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object.</returns>
		// Token: 0x06001507 RID: 5383 RVA: 0x000604E8 File Offset: 0x0005E6E8
		public SqlBulkCopyColumnMapping Add(SqlBulkCopyColumnMapping bulkCopyColumnMapping)
		{
			this.AssertWriteAccess();
			if ((string.IsNullOrEmpty(bulkCopyColumnMapping.SourceColumn) && bulkCopyColumnMapping.SourceOrdinal == -1) || (string.IsNullOrEmpty(bulkCopyColumnMapping.DestinationColumn) && bulkCopyColumnMapping.DestinationOrdinal == -1))
			{
				throw SQL.BulkLoadNonMatchingColumnMapping();
			}
			base.InnerList.Add(bulkCopyColumnMapping);
			return bulkCopyColumnMapping;
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> and adds it to the collection, using column names to specify both source and destination columns.</summary>
		/// <param name="sourceColumn">The name of the source column within the data source.</param>
		/// <param name="destinationColumn">The name of the destination column within the destination table.</param>
		/// <returns>A column mapping.</returns>
		// Token: 0x06001508 RID: 5384 RVA: 0x0006053B File Offset: 0x0005E73B
		public SqlBulkCopyColumnMapping Add(string sourceColumn, string destinationColumn)
		{
			this.AssertWriteAccess();
			return this.Add(new SqlBulkCopyColumnMapping(sourceColumn, destinationColumn));
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> and adds it to the collection, using an ordinal for the source column and a string for the destination column.</summary>
		/// <param name="sourceColumnIndex">The ordinal position of the source column within the data source.</param>
		/// <param name="destinationColumn">The name of the destination column within the destination table.</param>
		/// <returns>A column mapping.</returns>
		// Token: 0x06001509 RID: 5385 RVA: 0x00060550 File Offset: 0x0005E750
		public SqlBulkCopyColumnMapping Add(int sourceColumnIndex, string destinationColumn)
		{
			this.AssertWriteAccess();
			return this.Add(new SqlBulkCopyColumnMapping(sourceColumnIndex, destinationColumn));
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> and adds it to the collection, using a column name to describe the source column and an ordinal to specify the destination column.</summary>
		/// <param name="sourceColumn">The name of the source column within the data source.</param>
		/// <param name="destinationColumnIndex">The ordinal position of the destination column within the destination table.</param>
		/// <returns>A column mapping.</returns>
		// Token: 0x0600150A RID: 5386 RVA: 0x00060565 File Offset: 0x0005E765
		public SqlBulkCopyColumnMapping Add(string sourceColumn, int destinationColumnIndex)
		{
			this.AssertWriteAccess();
			return this.Add(new SqlBulkCopyColumnMapping(sourceColumn, destinationColumnIndex));
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> and adds it to the collection, using ordinals to specify both source and destination columns.</summary>
		/// <param name="sourceColumnIndex">The ordinal position of the source column within the data source.</param>
		/// <param name="destinationColumnIndex">The ordinal position of the destination column within the destination table.</param>
		/// <returns>A column mapping.</returns>
		// Token: 0x0600150B RID: 5387 RVA: 0x0006057A File Offset: 0x0005E77A
		public SqlBulkCopyColumnMapping Add(int sourceColumnIndex, int destinationColumnIndex)
		{
			this.AssertWriteAccess();
			return this.Add(new SqlBulkCopyColumnMapping(sourceColumnIndex, destinationColumnIndex));
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0006058F File Offset: 0x0005E78F
		private void AssertWriteAccess()
		{
			if (this.ReadOnly)
			{
				throw SQL.BulkLoadMappingInaccessible();
			}
		}

		/// <summary>Clears the contents of the collection.</summary>
		// Token: 0x0600150D RID: 5389 RVA: 0x0006059F File Offset: 0x0005E79F
		public new void Clear()
		{
			this.AssertWriteAccess();
			base.Clear();
		}

		/// <summary>Gets a value indicating whether a specified <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object exists in the collection.</summary>
		/// <param name="value">A valid <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified mapping exists in the collection; otherwise <see langword="false" />.</returns>
		// Token: 0x0600150E RID: 5390 RVA: 0x000605AD File Offset: 0x0005E7AD
		public bool Contains(SqlBulkCopyColumnMapping value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" /> to an array of <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> items, starting at a particular index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> array that is the destination of the elements copied from <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x0600150F RID: 5391 RVA: 0x000605BB File Offset: 0x0005E7BB
		public void CopyTo(SqlBulkCopyColumnMapping[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x000605CC File Offset: 0x0005E7CC
		internal void CreateDefaultMapping(int columnCount)
		{
			for (int i = 0; i < columnCount; i++)
			{
				base.InnerList.Add(new SqlBulkCopyColumnMapping(i, i));
			}
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object for which to search.</param>
		/// <returns>The zero-based index of the column mapping, or -1 if the column mapping is not found in the collection.</returns>
		// Token: 0x06001511 RID: 5393 RVA: 0x000605F8 File Offset: 0x0005E7F8
		public int IndexOf(SqlBulkCopyColumnMapping value)
		{
			return base.InnerList.IndexOf(value);
		}

		/// <summary>Insert a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> at the index specified.</summary>
		/// <param name="index">Integer value of the location within the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" /> at which to insert the new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" />.</param>
		/// <param name="value">
		///   <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object to be inserted in the collection.</param>
		// Token: 0x06001512 RID: 5394 RVA: 0x00060606 File Offset: 0x0005E806
		public void Insert(int index, SqlBulkCopyColumnMapping value)
		{
			this.AssertWriteAccess();
			base.InnerList.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> element from the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" />.</summary>
		/// <param name="value">
		///   <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object to be removed from the collection.</param>
		// Token: 0x06001513 RID: 5395 RVA: 0x0006061B File Offset: 0x0005E81B
		public void Remove(SqlBulkCopyColumnMapping value)
		{
			this.AssertWriteAccess();
			base.InnerList.Remove(value);
		}

		/// <summary>Removes the mapping at the specified index from the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object to be removed from the collection.</param>
		// Token: 0x06001514 RID: 5396 RVA: 0x0006062F File Offset: 0x0005E82F
		public new void RemoveAt(int index)
		{
			this.AssertWriteAccess();
			base.RemoveAt(index);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00060640 File Offset: 0x0005E840
		internal void ValidateCollection()
		{
			foreach (object obj in base.InnerList)
			{
				SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping = (SqlBulkCopyColumnMapping)obj;
				SqlBulkCopyColumnMappingCollection.MappingSchema mappingSchema = (sqlBulkCopyColumnMapping.SourceOrdinal != -1) ? ((sqlBulkCopyColumnMapping.DestinationOrdinal != -1) ? SqlBulkCopyColumnMappingCollection.MappingSchema.OrdinalsOrdinals : SqlBulkCopyColumnMappingCollection.MappingSchema.OrdinalsNames) : ((sqlBulkCopyColumnMapping.DestinationOrdinal != -1) ? SqlBulkCopyColumnMappingCollection.MappingSchema.NemesOrdinals : SqlBulkCopyColumnMappingCollection.MappingSchema.NamesNames);
				if (this._mappingSchema == SqlBulkCopyColumnMappingCollection.MappingSchema.Undefined)
				{
					this._mappingSchema = mappingSchema;
				}
				else if (this._mappingSchema != mappingSchema)
				{
					throw SQL.BulkLoadMappingsNamesOrOrdinalsOnly();
				}
			}
		}

		// Token: 0x04000D5D RID: 3421
		private SqlBulkCopyColumnMappingCollection.MappingSchema _mappingSchema;

		// Token: 0x04000D5E RID: 3422
		[CompilerGenerated]
		private bool <ReadOnly>k__BackingField;

		// Token: 0x020001AB RID: 427
		private enum MappingSchema
		{
			// Token: 0x04000D60 RID: 3424
			Undefined,
			// Token: 0x04000D61 RID: 3425
			NamesNames,
			// Token: 0x04000D62 RID: 3426
			NemesOrdinals,
			// Token: 0x04000D63 RID: 3427
			OrdinalsNames,
			// Token: 0x04000D64 RID: 3428
			OrdinalsOrdinals
		}
	}
}
