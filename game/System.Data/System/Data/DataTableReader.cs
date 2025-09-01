using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	/// <summary>The <see cref="T:System.Data.DataTableReader" /> obtains the contents of one or more <see cref="T:System.Data.DataTable" /> objects in the form of one or more read-only, forward-only result sets.</summary>
	// Token: 0x020000D2 RID: 210
	public sealed class DataTableReader : DbDataReader
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTableReader" /> class by using data from the supplied <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> from which the new <see cref="T:System.Data.DataTableReader" /> obtains its result set.</param>
		// Token: 0x06000C9F RID: 3231 RVA: 0x000337C0 File Offset: 0x000319C0
		public DataTableReader(DataTable dataTable)
		{
			if (dataTable == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTable");
			}
			this._tables = new DataTable[]
			{
				dataTable
			};
			this.Init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTableReader" /> class using the supplied array of <see cref="T:System.Data.DataTable" /> objects.</summary>
		/// <param name="dataTables">The array of <see cref="T:System.Data.DataTable" /> objects that supplies the results for the new <see cref="T:System.Data.DataTableReader" /> object.</param>
		// Token: 0x06000CA0 RID: 3232 RVA: 0x00033814 File Offset: 0x00031A14
		public DataTableReader(DataTable[] dataTables)
		{
			if (dataTables == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTable");
			}
			if (dataTables.Length == 0)
			{
				throw ExceptionBuilder.DataTableReaderArgumentIsEmpty();
			}
			this._tables = new DataTable[dataTables.Length];
			for (int i = 0; i < dataTables.Length; i++)
			{
				if (dataTables[i] == null)
				{
					throw ExceptionBuilder.ArgumentNull("DataTable");
				}
				this._tables[i] = dataTables[i];
			}
			this.Init();
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x00033898 File Offset: 0x00031A98
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x000338A0 File Offset: 0x00031AA0
		private bool ReaderIsInvalid
		{
			get
			{
				return this._readerIsInvalid;
			}
			set
			{
				if (this._readerIsInvalid == value)
				{
					return;
				}
				this._readerIsInvalid = value;
				if (this._readerIsInvalid && this._listener != null)
				{
					this._listener.CleanUp();
				}
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x000338CE File Offset: 0x00031ACE
		// (set) Token: 0x06000CA4 RID: 3236 RVA: 0x000338D6 File Offset: 0x00031AD6
		private bool IsSchemaChanged
		{
			get
			{
				return this._schemaIsChanged;
			}
			set
			{
				if (!value || this._schemaIsChanged == value)
				{
					return;
				}
				this._schemaIsChanged = value;
				if (this._listener != null)
				{
					this._listener.CleanUp();
				}
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x000338FF File Offset: 0x00031AFF
		internal DataTable CurrentDataTable
		{
			get
			{
				return this._currentDataTable;
			}
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00033908 File Offset: 0x00031B08
		private void Init()
		{
			this._tableCounter = 0;
			this._reachEORows = false;
			this._schemaIsChanged = false;
			this._currentDataTable = this._tables[this._tableCounter];
			this._hasRows = (this._currentDataTable.Rows.Count > 0);
			this.ReaderIsInvalid = false;
			this._listener = new DataTableReaderListener(this);
		}

		/// <summary>Closes the current <see cref="T:System.Data.DataTableReader" />.</summary>
		// Token: 0x06000CA7 RID: 3239 RVA: 0x00033969 File Offset: 0x00031B69
		public override void Close()
		{
			if (!this._isOpen)
			{
				return;
			}
			if (this._listener != null)
			{
				this._listener.CleanUp();
			}
			this._listener = null;
			this._schemaTable = null;
			this._isOpen = false;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.DataTableReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.DataTableReader" /> is closed.</exception>
		// Token: 0x06000CA8 RID: 3240 RVA: 0x0003399C File Offset: 0x00031B9C
		public override DataTable GetSchemaTable()
		{
			this.ValidateOpen("GetSchemaTable");
			this.ValidateReader();
			if (this._schemaTable == null)
			{
				this._schemaTable = DataTableReader.GetSchemaTableFromDataTable(this._currentDataTable);
			}
			return this._schemaTable;
		}

		/// <summary>Advances the <see cref="T:System.Data.DataTableReader" /> to the next result set, if any.</summary>
		/// <returns>
		///   <see langword="true" /> if there was another result set; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to navigate within a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		// Token: 0x06000CA9 RID: 3241 RVA: 0x000339D0 File Offset: 0x00031BD0
		public override bool NextResult()
		{
			this.ValidateOpen("NextResult");
			if (this._tableCounter == this._tables.Length - 1)
			{
				return false;
			}
			DataTable[] tables = this._tables;
			int num = this._tableCounter + 1;
			this._tableCounter = num;
			this._currentDataTable = tables[num];
			if (this._listener != null)
			{
				this._listener.UpdataTable(this._currentDataTable);
			}
			this._schemaTable = null;
			this._rowCounter = -1;
			this._currentRowRemoved = false;
			this._reachEORows = false;
			this._schemaIsChanged = false;
			this._started = false;
			this.ReaderIsInvalid = false;
			this._tableCleared = false;
			this._hasRows = (this._currentDataTable.Rows.Count > 0);
			return true;
		}

		/// <summary>Advances the <see cref="T:System.Data.DataTableReader" /> to the next record.</summary>
		/// <returns>
		///   <see langword="true" /> if there was another row to read; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		// Token: 0x06000CAA RID: 3242 RVA: 0x00033A88 File Offset: 0x00031C88
		public override bool Read()
		{
			if (!this._started)
			{
				this._started = true;
			}
			this.ValidateOpen("Read");
			this.ValidateReader();
			if (this._reachEORows)
			{
				return false;
			}
			if (this._rowCounter >= this._currentDataTable.Rows.Count - 1)
			{
				this._reachEORows = true;
				if (this._listener != null)
				{
					this._listener.CleanUp();
				}
				return false;
			}
			this._rowCounter++;
			this.ValidateRow(this._rowCounter);
			this._currentDataRow = this._currentDataTable.Rows[this._rowCounter];
			while (this._currentDataRow.RowState == DataRowState.Deleted)
			{
				this._rowCounter++;
				if (this._rowCounter == this._currentDataTable.Rows.Count)
				{
					this._reachEORows = true;
					if (this._listener != null)
					{
						this._listener.CleanUp();
					}
					return false;
				}
				this.ValidateRow(this._rowCounter);
				this._currentDataRow = this._currentDataTable.Rows[this._rowCounter];
			}
			if (this._currentRowRemoved)
			{
				this._currentRowRemoved = false;
			}
			return true;
		}

		/// <summary>The depth of nesting for the current row of the <see cref="T:System.Data.DataTableReader" />.</summary>
		/// <returns>The depth of nesting for the current row; always zero.</returns>
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00033BB5 File Offset: 0x00031DB5
		public override int Depth
		{
			get
			{
				this.ValidateOpen("Depth");
				this.ValidateReader();
				return 0;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataTableReader" /> is closed.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.DataTableReader" /> is closed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00033BC9 File Offset: 0x00031DC9
		public override bool IsClosed
		{
			get
			{
				return !this._isOpen;
			}
		}

		/// <summary>Gets the number of rows inserted, changed, or deleted by execution of the SQL statement.</summary>
		/// <returns>The <see cref="T:System.Data.DataTableReader" /> does not support this property and always returns 0.</returns>
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00033BD4 File Offset: 0x00031DD4
		public override int RecordsAffected
		{
			get
			{
				this.ValidateReader();
				return 0;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataTableReader" /> contains one or more rows.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.DataTableReader" /> contains one or more rows; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to retrieve information about a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00033BDD File Offset: 0x00031DDD
		public override bool HasRows
		{
			get
			{
				this.ValidateOpen("HasRows");
				this.ValidateReader();
				return this._hasRows;
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column ordinal.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		// Token: 0x1700022C RID: 556
		public override object this[int ordinal]
		{
			get
			{
				this.ValidateOpen("Item");
				this.ValidateReader();
				if (this._currentDataRow == null || this._currentDataRow.RowState == DataRowState.Deleted)
				{
					this.ReaderIsInvalid = true;
					throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
				}
				object result;
				try
				{
					result = this._currentDataRow[ordinal];
				}
				catch (IndexOutOfRangeException e)
				{
					ExceptionBuilder.TraceExceptionWithoutRethrow(e);
					throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
				}
				return result;
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column name.</summary>
		/// <param name="name">The name of the column.</param>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <exception cref="T:System.ArgumentException">The name specified is not a valid column name.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		// Token: 0x1700022D RID: 557
		public override object this[string name]
		{
			get
			{
				this.ValidateOpen("Item");
				this.ValidateReader();
				if (this._currentDataRow == null || this._currentDataRow.RowState == DataRowState.Deleted)
				{
					this.ReaderIsInvalid = true;
					throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
				}
				return this._currentDataRow[name];
			}
		}

		/// <summary>Returns the number of columns in the current row.</summary>
		/// <returns>When not positioned in a valid result set, 0; otherwise the number of columns in the current row.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to retrieve the field count in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00033CD0 File Offset: 0x00031ED0
		public override int FieldCount
		{
			get
			{
				this.ValidateOpen("FieldCount");
				this.ValidateReader();
				return this._currentDataTable.Columns.Count;
			}
		}

		/// <summary>Gets the type of the specified column in provider-specific format.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		// Token: 0x06000CB2 RID: 3250 RVA: 0x00033CF3 File Offset: 0x00031EF3
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			this.ValidateOpen("GetProviderSpecificFieldType");
			this.ValidateReader();
			return this.GetFieldType(ordinal);
		}

		/// <summary>Gets the value of the specified column in provider-specific format.</summary>
		/// <param name="ordinal">The zero-based number of the column whose value is retrieved.</param>
		/// <returns>The value of the specified column in provider-specific format.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /></exception>
		// Token: 0x06000CB3 RID: 3251 RVA: 0x00033D0D File Offset: 0x00031F0D
		public override object GetProviderSpecificValue(int ordinal)
		{
			this.ValidateOpen("GetProviderSpecificValue");
			this.ValidateReader();
			return this.GetValue(ordinal);
		}

		/// <summary>Fills the supplied array with provider-specific type information for all the columns in the <see cref="T:System.Data.DataTableReader" />.</summary>
		/// <param name="values">An array of objects to be filled in with type information for the columns in the <see cref="T:System.Data.DataTableReader" />.</param>
		/// <returns>The number of column values copied into the array.</returns>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		// Token: 0x06000CB4 RID: 3252 RVA: 0x00033D27 File Offset: 0x00031F27
		public override int GetProviderSpecificValues(object[] values)
		{
			this.ValidateOpen("GetProviderSpecificValues");
			this.ValidateReader();
			return this.GetValues(values);
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Boolean" />.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a <see langword="Boolean" />.</exception>
		// Token: 0x06000CB5 RID: 3253 RVA: 0x00033D44 File Offset: 0x00031F44
		public override bool GetBoolean(int ordinal)
		{
			this.ValidateState("GetBoolean");
			this.ValidateReader();
			bool result;
			try
			{
				result = (bool)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		/// <summary>Gets the value of the specified column as a byte.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see langword="DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a byte.</exception>
		// Token: 0x06000CB6 RID: 3254 RVA: 0x00033D98 File Offset: 0x00031F98
		public override byte GetByte(int ordinal)
		{
			this.ValidateState("GetByte");
			this.ValidateReader();
			byte result;
			try
			{
				result = (byte)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		/// <summary>Reads a stream of bytes starting at the specified column offset into the buffer as an array starting at the specified buffer offset.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <param name="dataIndex">The index within the field from which to start the read operation.</param>
		/// <param name="buffer">The buffer into which to read the stream of bytes.</param>
		/// <param name="bufferIndex">The index within the buffer at which to start placing the data.</param>
		/// <param name="length">The maximum length to copy into the buffer.</param>
		/// <returns>The actual number of bytes read.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see langword="DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a byte array.</exception>
		// Token: 0x06000CB7 RID: 3255 RVA: 0x00033DEC File Offset: 0x00031FEC
		public override long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			this.ValidateState("GetBytes");
			this.ValidateReader();
			byte[] array;
			try
			{
				array = (byte[])this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = (int)dataIndex;
			int num2 = Math.Min(array.Length - num, length);
			if (num < 0)
			{
				throw ADP.InvalidSourceBufferIndex(array.Length, (long)num, "dataIndex");
			}
			if (bufferIndex < 0 || (bufferIndex > 0 && bufferIndex >= buffer.Length))
			{
				throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
			}
			if (0 < num2)
			{
				Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num2);
			}
			else
			{
				if (length < 0)
				{
					throw ADP.InvalidDataLength((long)length);
				}
				num2 = 0;
			}
			return (long)num2;
		}

		/// <summary>Gets the value of the specified column as a character.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see langword="DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified field does not contain a character.</exception>
		// Token: 0x06000CB8 RID: 3256 RVA: 0x00033EB4 File Offset: 0x000320B4
		public override char GetChar(int ordinal)
		{
			this.ValidateState("GetChar");
			this.ValidateReader();
			char result;
			try
			{
				result = (char)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		/// <summary>Returns the value of the specified column as a character array.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <param name="dataIndex">The index within the field from which to start the read operation.</param>
		/// <param name="buffer">The buffer into which to read the stream of chars.</param>
		/// <param name="bufferIndex">The index within the buffer at which to start placing the data.</param>
		/// <param name="length">The maximum length to copy into the buffer.</param>
		/// <returns>The actual number of characters read.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see langword="DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a character array.</exception>
		// Token: 0x06000CB9 RID: 3257 RVA: 0x00033F08 File Offset: 0x00032108
		public override long GetChars(int ordinal, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			this.ValidateState("GetChars");
			this.ValidateReader();
			char[] array;
			try
			{
				array = (char[])this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = (int)dataIndex;
			int num2 = Math.Min(array.Length - num, length);
			if (num < 0)
			{
				throw ADP.InvalidSourceBufferIndex(array.Length, (long)num, "dataIndex");
			}
			if (bufferIndex < 0 || (bufferIndex > 0 && bufferIndex >= buffer.Length))
			{
				throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
			}
			if (0 < num2)
			{
				Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num2);
			}
			else
			{
				if (length < 0)
				{
					throw ADP.InvalidDataLength((long)length);
				}
				num2 = 0;
			}
			return (long)num2;
		}

		/// <summary>Gets a string representing the data type of the specified column.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>A string representing the column's data type.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		// Token: 0x06000CBA RID: 3258 RVA: 0x00033FD0 File Offset: 0x000321D0
		public override string GetDataTypeName(int ordinal)
		{
			this.ValidateOpen("GetDataTypeName");
			this.ValidateReader();
			return this.GetFieldType(ordinal).Name;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see langword="DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a DateTime value.</exception>
		// Token: 0x06000CBB RID: 3259 RVA: 0x00033FF0 File Offset: 0x000321F0
		public override DateTime GetDateTime(int ordinal)
		{
			this.ValidateState("GetDateTime");
			this.ValidateReader();
			DateTime result;
			try
			{
				result = (DateTime)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Decimal" />.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see langword="DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a <see langword="Decimal" /> value.</exception>
		// Token: 0x06000CBC RID: 3260 RVA: 0x00034044 File Offset: 0x00032244
		public override decimal GetDecimal(int ordinal)
		{
			this.ValidateState("GetDecimal");
			this.ValidateReader();
			decimal result;
			try
			{
				result = (decimal)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		/// <summary>Gets the value of the column as a double-precision floating point number.</summary>
		/// <param name="ordinal">The zero-based ordinal of the column.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see langword="DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a double-precision floating point number.</exception>
		// Token: 0x06000CBD RID: 3261 RVA: 0x00034098 File Offset: 0x00032298
		public override double GetDouble(int ordinal)
		{
			this.ValidateState("GetDouble");
			this.ValidateReader();
			double result;
			try
			{
				result = (double)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that is the data type of the object.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		// Token: 0x06000CBE RID: 3262 RVA: 0x000340EC File Offset: 0x000322EC
		public override Type GetFieldType(int ordinal)
		{
			this.ValidateOpen("GetFieldType");
			this.ValidateReader();
			Type dataType;
			try
			{
				dataType = this._currentDataTable.Columns[ordinal].DataType;
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return dataType;
		}

		/// <summary>Gets the value of the specified column as a single-precision floating point number.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a single-precision floating point number.</exception>
		// Token: 0x06000CBF RID: 3263 RVA: 0x00034148 File Offset: 0x00032348
		public override float GetFloat(int ordinal)
		{
			this.ValidateState("GetFloat");
			this.ValidateReader();
			float result;
			try
			{
				result = (float)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		/// <summary>Gets the value of the specified column as a globally-unique identifier (GUID).</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a GUID.</exception>
		// Token: 0x06000CC0 RID: 3264 RVA: 0x0003419C File Offset: 0x0003239C
		public override Guid GetGuid(int ordinal)
		{
			this.ValidateState("GetGuid");
			this.ValidateReader();
			Guid result;
			try
			{
				result = (Guid)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		/// <summary>Gets the value of the specified column as a 16-bit signed integer.</summary>
		/// <param name="ordinal">The zero-based column ordinal</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a 16-bit signed integer.</exception>
		// Token: 0x06000CC1 RID: 3265 RVA: 0x000341F0 File Offset: 0x000323F0
		public override short GetInt16(int ordinal)
		{
			this.ValidateState("GetInt16");
			this.ValidateReader();
			short result;
			try
			{
				result = (short)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
		/// <param name="ordinal">The zero-based column ordinal</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a 32-bit signed integer value.</exception>
		// Token: 0x06000CC2 RID: 3266 RVA: 0x00034244 File Offset: 0x00032444
		public override int GetInt32(int ordinal)
		{
			this.ValidateState("GetInt32");
			this.ValidateReader();
			int result;
			try
			{
				result = (int)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
		/// <param name="ordinal">The zero-based column ordinal</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a 64-bit signed integer value.</exception>
		// Token: 0x06000CC3 RID: 3267 RVA: 0x00034298 File Offset: 0x00032498
		public override long GetInt64(int ordinal)
		{
			this.ValidateState("GetInt64");
			this.ValidateReader();
			long result;
			try
			{
				result = (long)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.String" />.</summary>
		/// <param name="ordinal">The zero-based column ordinal</param>
		/// <returns>The name of the specified column.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		// Token: 0x06000CC4 RID: 3268 RVA: 0x000342EC File Offset: 0x000324EC
		public override string GetName(int ordinal)
		{
			this.ValidateOpen("GetName");
			this.ValidateReader();
			string columnName;
			try
			{
				columnName = this._currentDataTable.Columns[ordinal].ColumnName;
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return columnName;
		}

		/// <summary>Gets the column ordinal, given the name of the column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <returns>The zero-based column ordinal.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.ArgumentException">The name specified is not a valid column name.</exception>
		// Token: 0x06000CC5 RID: 3269 RVA: 0x00034348 File Offset: 0x00032548
		public override int GetOrdinal(string name)
		{
			this.ValidateOpen("GetOrdinal");
			this.ValidateReader();
			DataColumn dataColumn = this._currentDataTable.Columns[name];
			if (dataColumn != null)
			{
				return dataColumn.Ordinal;
			}
			throw ExceptionBuilder.ColumnNotInTheTable(name, this._currentDataTable.TableName);
		}

		/// <summary>Gets the value of the specified column as a string.</summary>
		/// <param name="ordinal">The zero-based column ordinal</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a string.</exception>
		// Token: 0x06000CC6 RID: 3270 RVA: 0x00034394 File Offset: 0x00032594
		public override string GetString(int ordinal)
		{
			this.ValidateState("GetString");
			this.ValidateReader();
			string result;
			try
			{
				result = (string)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		/// <summary>Gets the value of the specified column in its native format.</summary>
		/// <param name="ordinal">The zero-based column ordinal</param>
		/// <returns>The value of the specified column. This method returns <see langword="DBNull" /> for null columns.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access columns in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		// Token: 0x06000CC7 RID: 3271 RVA: 0x000343E8 File Offset: 0x000325E8
		public override object GetValue(int ordinal)
		{
			this.ValidateState("GetValue");
			this.ValidateReader();
			object result;
			try
			{
				result = this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		/// <summary>Populates an array of objects with the column values of the current row.</summary>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the column values from the <see cref="T:System.Data.DataTableReader" />.</param>
		/// <returns>The number of column values copied into the array.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		// Token: 0x06000CC8 RID: 3272 RVA: 0x00034438 File Offset: 0x00032638
		public override int GetValues(object[] values)
		{
			this.ValidateState("GetValues");
			this.ValidateReader();
			if (values == null)
			{
				throw ExceptionBuilder.ArgumentNull("values");
			}
			Array.Copy(this._currentDataRow.ItemArray, values, (this._currentDataRow.ItemArray.Length > values.Length) ? values.Length : this._currentDataRow.ItemArray.Length);
			if (this._currentDataRow.ItemArray.Length <= values.Length)
			{
				return this._currentDataRow.ItemArray.Length;
			}
			return values.Length;
		}

		/// <summary>Gets a value that indicates whether the column contains non-existent or missing values.</summary>
		/// <param name="ordinal">The zero-based column ordinal</param>
		/// <returns>
		///   <see langword="true" /> if the specified column value is equivalent to <see cref="T:System.DBNull" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		// Token: 0x06000CC9 RID: 3273 RVA: 0x000344BC File Offset: 0x000326BC
		public override bool IsDBNull(int ordinal)
		{
			this.ValidateState("IsDBNull");
			this.ValidateReader();
			bool result;
			try
			{
				result = this._currentDataRow.IsNull(ordinal);
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		/// <summary>Returns an enumerator that can be used to iterate through the item collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that represents the item collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		// Token: 0x06000CCA RID: 3274 RVA: 0x0003450C File Offset: 0x0003270C
		public override IEnumerator GetEnumerator()
		{
			this.ValidateOpen("GetEnumerator");
			return new DbEnumerator(this);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00034520 File Offset: 0x00032720
		internal static DataTable GetSchemaTableFromDataTable(DataTable table)
		{
			if (table == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTable");
			}
			DataTable dataTable = new DataTable("SchemaTable");
			dataTable.Locale = CultureInfo.InvariantCulture;
			DataColumn column = new DataColumn(SchemaTableColumn.ColumnName, typeof(string));
			DataColumn column2 = new DataColumn(SchemaTableColumn.ColumnOrdinal, typeof(int));
			DataColumn dataColumn = new DataColumn(SchemaTableColumn.ColumnSize, typeof(int));
			DataColumn column3 = new DataColumn(SchemaTableColumn.NumericPrecision, typeof(short));
			DataColumn column4 = new DataColumn(SchemaTableColumn.NumericScale, typeof(short));
			DataColumn column5 = new DataColumn(SchemaTableColumn.DataType, typeof(Type));
			DataColumn column6 = new DataColumn(SchemaTableColumn.ProviderType, typeof(int));
			DataColumn dataColumn2 = new DataColumn(SchemaTableColumn.IsLong, typeof(bool));
			DataColumn column7 = new DataColumn(SchemaTableColumn.AllowDBNull, typeof(bool));
			DataColumn dataColumn3 = new DataColumn(SchemaTableOptionalColumn.IsReadOnly, typeof(bool));
			DataColumn dataColumn4 = new DataColumn(SchemaTableOptionalColumn.IsRowVersion, typeof(bool));
			DataColumn column8 = new DataColumn(SchemaTableColumn.IsUnique, typeof(bool));
			DataColumn dataColumn5 = new DataColumn(SchemaTableColumn.IsKey, typeof(bool));
			DataColumn dataColumn6 = new DataColumn(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool));
			DataColumn column9 = new DataColumn(SchemaTableColumn.BaseSchemaName, typeof(string));
			DataColumn dataColumn7 = new DataColumn(SchemaTableOptionalColumn.BaseCatalogName, typeof(string));
			DataColumn dataColumn8 = new DataColumn(SchemaTableColumn.BaseTableName, typeof(string));
			DataColumn column10 = new DataColumn(SchemaTableColumn.BaseColumnName, typeof(string));
			DataColumn dataColumn9 = new DataColumn(SchemaTableOptionalColumn.AutoIncrementSeed, typeof(long));
			DataColumn dataColumn10 = new DataColumn(SchemaTableOptionalColumn.AutoIncrementStep, typeof(long));
			DataColumn column11 = new DataColumn(SchemaTableOptionalColumn.DefaultValue, typeof(object));
			DataColumn column12 = new DataColumn(SchemaTableOptionalColumn.Expression, typeof(string));
			DataColumn column13 = new DataColumn(SchemaTableOptionalColumn.ColumnMapping, typeof(MappingType));
			DataColumn dataColumn11 = new DataColumn(SchemaTableOptionalColumn.BaseTableNamespace, typeof(string));
			DataColumn column14 = new DataColumn(SchemaTableOptionalColumn.BaseColumnNamespace, typeof(string));
			dataColumn.DefaultValue = -1;
			if (table.DataSet != null)
			{
				dataColumn7.DefaultValue = table.DataSet.DataSetName;
			}
			dataColumn8.DefaultValue = table.TableName;
			dataColumn11.DefaultValue = table.Namespace;
			dataColumn4.DefaultValue = false;
			dataColumn2.DefaultValue = false;
			dataColumn3.DefaultValue = false;
			dataColumn5.DefaultValue = false;
			dataColumn6.DefaultValue = false;
			dataColumn9.DefaultValue = 0;
			dataColumn10.DefaultValue = 1;
			dataTable.Columns.Add(column);
			dataTable.Columns.Add(column2);
			dataTable.Columns.Add(dataColumn);
			dataTable.Columns.Add(column3);
			dataTable.Columns.Add(column4);
			dataTable.Columns.Add(column5);
			dataTable.Columns.Add(column6);
			dataTable.Columns.Add(dataColumn2);
			dataTable.Columns.Add(column7);
			dataTable.Columns.Add(dataColumn3);
			dataTable.Columns.Add(dataColumn4);
			dataTable.Columns.Add(column8);
			dataTable.Columns.Add(dataColumn5);
			dataTable.Columns.Add(dataColumn6);
			dataTable.Columns.Add(dataColumn7);
			dataTable.Columns.Add(column9);
			dataTable.Columns.Add(dataColumn8);
			dataTable.Columns.Add(column10);
			dataTable.Columns.Add(dataColumn9);
			dataTable.Columns.Add(dataColumn10);
			dataTable.Columns.Add(column11);
			dataTable.Columns.Add(column12);
			dataTable.Columns.Add(column13);
			dataTable.Columns.Add(dataColumn11);
			dataTable.Columns.Add(column14);
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn12 = (DataColumn)obj;
				DataRow dataRow = dataTable.NewRow();
				dataRow[column] = dataColumn12.ColumnName;
				dataRow[column2] = dataColumn12.Ordinal;
				dataRow[column5] = dataColumn12.DataType;
				if (dataColumn12.DataType == typeof(string))
				{
					dataRow[dataColumn] = dataColumn12.MaxLength;
				}
				dataRow[column7] = dataColumn12.AllowDBNull;
				dataRow[dataColumn3] = dataColumn12.ReadOnly;
				dataRow[column8] = dataColumn12.Unique;
				if (dataColumn12.AutoIncrement)
				{
					dataRow[dataColumn6] = true;
					dataRow[dataColumn9] = dataColumn12.AutoIncrementSeed;
					dataRow[dataColumn10] = dataColumn12.AutoIncrementStep;
				}
				if (dataColumn12.DefaultValue != DBNull.Value)
				{
					dataRow[column11] = dataColumn12.DefaultValue;
				}
				if (dataColumn12.Expression.Length != 0)
				{
					bool flag = false;
					DataColumn[] dependency = dataColumn12.DataExpression.GetDependency();
					for (int i = 0; i < dependency.Length; i++)
					{
						if (dependency[i].Table != table)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						dataRow[column12] = dataColumn12.Expression;
					}
				}
				dataRow[column13] = dataColumn12.ColumnMapping;
				dataRow[column10] = dataColumn12.ColumnName;
				dataRow[column14] = dataColumn12.Namespace;
				dataTable.Rows.Add(dataRow);
			}
			foreach (DataColumn dataColumn13 in table.PrimaryKey)
			{
				dataTable.Rows[dataColumn13.Ordinal][dataColumn5] = true;
			}
			dataTable.AcceptChanges();
			return dataTable;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00034B98 File Offset: 0x00032D98
		private void ValidateOpen(string caller)
		{
			if (!this._isOpen)
			{
				throw ADP.DataReaderClosed(caller);
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00034BA9 File Offset: 0x00032DA9
		private void ValidateReader()
		{
			if (this.ReaderIsInvalid)
			{
				throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
			}
			if (this.IsSchemaChanged)
			{
				throw ExceptionBuilder.DataTableReaderSchemaIsInvalid(this._currentDataTable.TableName);
			}
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00034BE0 File Offset: 0x00032DE0
		private void ValidateState(string caller)
		{
			this.ValidateOpen(caller);
			if (this._tableCleared)
			{
				throw ExceptionBuilder.EmptyDataTableReader(this._currentDataTable.TableName);
			}
			if (this._currentDataRow == null || this._currentDataTable == null)
			{
				this.ReaderIsInvalid = true;
				throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
			}
			if (this._currentDataRow.RowState == DataRowState.Deleted || this._currentDataRow.RowState == DataRowState.Detached || this._currentRowRemoved)
			{
				throw ExceptionBuilder.InvalidCurrentRowInDataTableReader();
			}
			if (0 > this._rowCounter || this._currentDataTable.Rows.Count <= this._rowCounter)
			{
				this.ReaderIsInvalid = true;
				throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
			}
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00034C98 File Offset: 0x00032E98
		private void ValidateRow(int rowPosition)
		{
			if (this.ReaderIsInvalid)
			{
				throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
			}
			if (0 > rowPosition || this._currentDataTable.Rows.Count <= rowPosition)
			{
				this.ReaderIsInvalid = true;
				throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
			}
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00034CED File Offset: 0x00032EED
		internal void SchemaChanged()
		{
			this.IsSchemaChanged = true;
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00034CF6 File Offset: 0x00032EF6
		internal void DataTableCleared()
		{
			if (!this._started)
			{
				return;
			}
			this._rowCounter = -1;
			if (!this._reachEORows)
			{
				this._currentRowRemoved = true;
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00034D18 File Offset: 0x00032F18
		internal void DataChanged(DataRowChangeEventArgs args)
		{
			if (!this._started || (this._rowCounter == -1 && !this._tableCleared))
			{
				return;
			}
			DataRowAction action = args.Action;
			if (action <= DataRowAction.Rollback)
			{
				if (action != DataRowAction.Delete && action != DataRowAction.Rollback)
				{
					return;
				}
			}
			else if (action != DataRowAction.Commit)
			{
				if (action != DataRowAction.Add)
				{
					return;
				}
				this.ValidateRow(this._rowCounter + 1);
				if (this._currentDataRow == this._currentDataTable.Rows[this._rowCounter + 1])
				{
					this._rowCounter++;
					return;
				}
				return;
			}
			if (args.Row.RowState == DataRowState.Detached)
			{
				if (args.Row != this._currentDataRow)
				{
					if (this._rowCounter != 0)
					{
						this.ValidateRow(this._rowCounter - 1);
						if (this._currentDataRow == this._currentDataTable.Rows[this._rowCounter - 1])
						{
							this._rowCounter--;
							return;
						}
					}
				}
				else
				{
					this._currentRowRemoved = true;
					if (this._rowCounter > 0)
					{
						this._rowCounter--;
						this._currentDataRow = this._currentDataTable.Rows[this._rowCounter];
						return;
					}
					this._rowCounter = -1;
					this._currentDataRow = null;
				}
			}
		}

		// Token: 0x0400080C RID: 2060
		private readonly DataTable[] _tables;

		// Token: 0x0400080D RID: 2061
		private bool _isOpen = true;

		// Token: 0x0400080E RID: 2062
		private DataTable _schemaTable;

		// Token: 0x0400080F RID: 2063
		private int _tableCounter = -1;

		// Token: 0x04000810 RID: 2064
		private int _rowCounter = -1;

		// Token: 0x04000811 RID: 2065
		private DataTable _currentDataTable;

		// Token: 0x04000812 RID: 2066
		private DataRow _currentDataRow;

		// Token: 0x04000813 RID: 2067
		private bool _hasRows = true;

		// Token: 0x04000814 RID: 2068
		private bool _reachEORows;

		// Token: 0x04000815 RID: 2069
		private bool _currentRowRemoved;

		// Token: 0x04000816 RID: 2070
		private bool _schemaIsChanged;

		// Token: 0x04000817 RID: 2071
		private bool _started;

		// Token: 0x04000818 RID: 2072
		private bool _readerIsInvalid;

		// Token: 0x04000819 RID: 2073
		private DataTableReaderListener _listener;

		// Token: 0x0400081A RID: 2074
		private bool _tableCleared;
	}
}
