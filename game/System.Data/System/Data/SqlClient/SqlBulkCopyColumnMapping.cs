using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	/// <summary>Defines the mapping between a column in a <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> instance's data source and a column in the instance's destination table.</summary>
	// Token: 0x020001A9 RID: 425
	public sealed class SqlBulkCopyColumnMapping
	{
		/// <summary>Name of the column being mapped in the destination database table.</summary>
		/// <returns>The string value of the <see cref="P:System.Data.SqlClient.SqlBulkCopyColumnMapping.DestinationColumn" /> property.</returns>
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0006036E File Offset: 0x0005E56E
		// (set) Token: 0x060014F7 RID: 5367 RVA: 0x00060384 File Offset: 0x0005E584
		public string DestinationColumn
		{
			get
			{
				if (this._destinationColumnName != null)
				{
					return this._destinationColumnName;
				}
				return string.Empty;
			}
			set
			{
				this._destinationColumnOrdinal = (this._internalDestinationColumnOrdinal = -1);
				this._destinationColumnName = value;
			}
		}

		/// <summary>Ordinal value of the destination column within the destination table.</summary>
		/// <returns>The integer value of the <see cref="P:System.Data.SqlClient.SqlBulkCopyColumnMapping.DestinationOrdinal" /> property, or -1 if the property has not been set.</returns>
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x000603A8 File Offset: 0x0005E5A8
		// (set) Token: 0x060014F9 RID: 5369 RVA: 0x000603B0 File Offset: 0x0005E5B0
		public int DestinationOrdinal
		{
			get
			{
				return this._destinationColumnOrdinal;
			}
			set
			{
				if (value >= 0)
				{
					this._destinationColumnName = null;
					this._internalDestinationColumnOrdinal = value;
					this._destinationColumnOrdinal = value;
					return;
				}
				throw ADP.IndexOutOfRange(value);
			}
		}

		/// <summary>Name of the column being mapped in the data source.</summary>
		/// <returns>The string value of the <see cref="P:System.Data.SqlClient.SqlBulkCopyColumnMapping.SourceColumn" /> property.</returns>
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x000603DF File Offset: 0x0005E5DF
		// (set) Token: 0x060014FB RID: 5371 RVA: 0x000603F8 File Offset: 0x0005E5F8
		public string SourceColumn
		{
			get
			{
				if (this._sourceColumnName != null)
				{
					return this._sourceColumnName;
				}
				return string.Empty;
			}
			set
			{
				this._sourceColumnOrdinal = (this._internalSourceColumnOrdinal = -1);
				this._sourceColumnName = value;
			}
		}

		/// <summary>The ordinal position of the source column within the data source.</summary>
		/// <returns>The integer value of the <see cref="P:System.Data.SqlClient.SqlBulkCopyColumnMapping.SourceOrdinal" /> property.</returns>
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x0006041C File Offset: 0x0005E61C
		// (set) Token: 0x060014FD RID: 5373 RVA: 0x00060424 File Offset: 0x0005E624
		public int SourceOrdinal
		{
			get
			{
				return this._sourceColumnOrdinal;
			}
			set
			{
				if (value >= 0)
				{
					this._sourceColumnName = null;
					this._internalSourceColumnOrdinal = value;
					this._sourceColumnOrdinal = value;
					return;
				}
				throw ADP.IndexOutOfRange(value);
			}
		}

		/// <summary>Default constructor that initializes a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object.</summary>
		// Token: 0x060014FE RID: 5374 RVA: 0x00060453 File Offset: 0x0005E653
		public SqlBulkCopyColumnMapping()
		{
			this._internalSourceColumnOrdinal = -1;
		}

		/// <summary>Creates a new column mapping, using column names to refer to source and destination columns.</summary>
		/// <param name="sourceColumn">The name of the source column within the data source.</param>
		/// <param name="destinationColumn">The name of the destination column within the destination table.</param>
		// Token: 0x060014FF RID: 5375 RVA: 0x00060462 File Offset: 0x0005E662
		public SqlBulkCopyColumnMapping(string sourceColumn, string destinationColumn)
		{
			this.SourceColumn = sourceColumn;
			this.DestinationColumn = destinationColumn;
		}

		/// <summary>Creates a new column mapping, using a column ordinal to refer to the source column and a column name for the target column.</summary>
		/// <param name="sourceColumnOrdinal">The ordinal position of the source column within the data source.</param>
		/// <param name="destinationColumn">The name of the destination column within the destination table.</param>
		// Token: 0x06001500 RID: 5376 RVA: 0x00060478 File Offset: 0x0005E678
		public SqlBulkCopyColumnMapping(int sourceColumnOrdinal, string destinationColumn)
		{
			this.SourceOrdinal = sourceColumnOrdinal;
			this.DestinationColumn = destinationColumn;
		}

		/// <summary>Creates a new column mapping, using a column name to refer to the source column and a column ordinal for the target column.</summary>
		/// <param name="sourceColumn">The name of the source column within the data source.</param>
		/// <param name="destinationOrdinal">The ordinal position of the destination column within the destination table.</param>
		// Token: 0x06001501 RID: 5377 RVA: 0x0006048E File Offset: 0x0005E68E
		public SqlBulkCopyColumnMapping(string sourceColumn, int destinationOrdinal)
		{
			this.SourceColumn = sourceColumn;
			this.DestinationOrdinal = destinationOrdinal;
		}

		/// <summary>Creates a new column mapping, using column ordinals to refer to source and destination columns.</summary>
		/// <param name="sourceColumnOrdinal">The ordinal position of the source column within the data source.</param>
		/// <param name="destinationOrdinal">The ordinal position of the destination column within the destination table.</param>
		// Token: 0x06001502 RID: 5378 RVA: 0x000604A4 File Offset: 0x0005E6A4
		public SqlBulkCopyColumnMapping(int sourceColumnOrdinal, int destinationOrdinal)
		{
			this.SourceOrdinal = sourceColumnOrdinal;
			this.DestinationOrdinal = destinationOrdinal;
		}

		// Token: 0x04000D57 RID: 3415
		internal string _destinationColumnName;

		// Token: 0x04000D58 RID: 3416
		internal int _destinationColumnOrdinal;

		// Token: 0x04000D59 RID: 3417
		internal string _sourceColumnName;

		// Token: 0x04000D5A RID: 3418
		internal int _sourceColumnOrdinal;

		// Token: 0x04000D5B RID: 3419
		internal int _internalDestinationColumnOrdinal;

		// Token: 0x04000D5C RID: 3420
		internal int _internalSourceColumnOrdinal;
	}
}
