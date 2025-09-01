using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Common
{
	/// <summary>Reads a forward-only stream of rows from a data source.</summary>
	// Token: 0x02000392 RID: 914
	public abstract class DbDataReader : MarshalByRefObject, IDataReader, IDisposable, IDataRecord, IEnumerable, IAsyncDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbDataReader" /> class.</summary>
		// Token: 0x06002C3F RID: 11327 RVA: 0x00003DB9 File Offset: 0x00001FB9
		protected DbDataReader()
		{
		}

		/// <summary>Gets a value indicating the depth of nesting for the current row.</summary>
		/// <returns>The depth of nesting for the current row.</returns>
		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002C40 RID: 11328
		public abstract int Depth { get; }

		/// <summary>Gets the number of columns in the current row.</summary>
		/// <returns>The number of columns in the current row.</returns>
		/// <exception cref="T:System.NotSupportedException">There is no current connection to an instance of SQL Server.</exception>
		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002C41 RID: 11329
		public abstract int FieldCount { get; }

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Data.Common.DbDataReader" /> contains one or more rows.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.Common.DbDataReader" /> contains one or more rows; otherwise <see langword="false" />.</returns>
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002C42 RID: 11330
		public abstract bool HasRows { get; }

		/// <summary>Gets a value indicating whether the <see cref="T:System.Data.Common.DbDataReader" /> is closed.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.Common.DbDataReader" /> is closed; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed.</exception>
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002C43 RID: 11331
		public abstract bool IsClosed { get; }

		/// <summary>Gets the number of rows changed, inserted, or deleted by execution of the SQL statement.</summary>
		/// <returns>The number of rows changed, inserted, or deleted. -1 for SELECT statements; 0 if no rows were affected or the statement failed.</returns>
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002C44 RID: 11332
		public abstract int RecordsAffected { get; }

		/// <summary>Gets the number of fields in the <see cref="T:System.Data.Common.DbDataReader" /> that are not hidden.</summary>
		/// <returns>The number of fields that are not hidden.</returns>
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x000BE7B0 File Offset: 0x000BC9B0
		public virtual int VisibleFieldCount
		{
			get
			{
				return this.FieldCount;
			}
		}

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x1700078D RID: 1933
		public abstract object this[int ordinal]
		{
			get;
		}

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <param name="name">The name of the column.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">No column with the specified name was found.</exception>
		// Token: 0x1700078E RID: 1934
		public abstract object this[string name]
		{
			get;
		}

		/// <summary>Closes the <see cref="T:System.Data.Common.DbDataReader" /> object.</summary>
		// Token: 0x06002C48 RID: 11336 RVA: 0x00007EED File Offset: 0x000060ED
		public virtual void Close()
		{
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Data.Common.DbDataReader" /> class.</summary>
		// Token: 0x06002C49 RID: 11337 RVA: 0x000BE7B8 File Offset: 0x000BC9B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the managed resources used by the <see cref="T:System.Data.Common.DbDataReader" /> and optionally releases the unmanaged resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002C4A RID: 11338 RVA: 0x000BE7C1 File Offset: 0x000BC9C1
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		/// <summary>Gets name of the data type of the specified column.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>A string representing the name of the data type.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06002C4B RID: 11339
		public abstract string GetDataTypeName(int ordinal);

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</returns>
		// Token: 0x06002C4C RID: 11340
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract IEnumerator GetEnumerator();

		/// <summary>Gets the data type of the specified column.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The data type of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06002C4D RID: 11341
		public abstract Type GetFieldType(int ordinal);

		/// <summary>Gets the name of the column, given the zero-based column ordinal.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The name of the specified column.</returns>
		// Token: 0x06002C4E RID: 11342
		public abstract string GetName(int ordinal);

		/// <summary>Gets the column ordinal given the name of the column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <returns>The zero-based column ordinal.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The name specified is not a valid column name.</exception>
		// Token: 0x06002C4F RID: 11343
		public abstract int GetOrdinal(string name);

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.Common.DbDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed.</exception>
		// Token: 0x06002C50 RID: 11344 RVA: 0x00087F51 File Offset: 0x00086151
		public virtual DataTable GetSchemaTable()
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the value of the specified column as a Boolean.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06002C51 RID: 11345
		public abstract bool GetBoolean(int ordinal);

		/// <summary>Gets the value of the specified column as a byte.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06002C52 RID: 11346
		public abstract byte GetByte(int ordinal);

		/// <summary>Reads a stream of bytes from the specified column, starting at location indicated by <paramref name="dataOffset" />, into the buffer, starting at the location indicated by <paramref name="bufferOffset" />.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <param name="dataOffset">The index within the row from which to begin the read operation.</param>
		/// <param name="buffer">The buffer into which to copy the data.</param>
		/// <param name="bufferOffset">The index with the buffer to which the data will be copied.</param>
		/// <param name="length">The maximum number of characters to read.</param>
		/// <returns>The actual number of bytes read.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06002C53 RID: 11347
		public abstract long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length);

		/// <summary>Gets the value of the specified column as a single character.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06002C54 RID: 11348
		public abstract char GetChar(int ordinal);

		/// <summary>Reads a stream of characters from the specified column, starting at location indicated by <paramref name="dataOffset" />, into the buffer, starting at the location indicated by <paramref name="bufferOffset" />.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <param name="dataOffset">The index within the row from which to begin the read operation.</param>
		/// <param name="buffer">The buffer into which to copy the data.</param>
		/// <param name="bufferOffset">The index with the buffer to which the data will be copied.</param>
		/// <param name="length">The maximum number of characters to read.</param>
		/// <returns>The actual number of characters read.</returns>
		// Token: 0x06002C55 RID: 11349
		public abstract long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length);

		/// <summary>Returns a <see cref="T:System.Data.Common.DbDataReader" /> object for the requested column ordinal.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>A <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		// Token: 0x06002C56 RID: 11350 RVA: 0x000BE7CC File Offset: 0x000BC9CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DbDataReader GetData(int ordinal)
		{
			return this.GetDbDataReader(ordinal);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDataRecord.GetData(System.Int32)" />.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>An instance of <see cref="T:System.Data.IDataReader" /> to be used when the field points to more remote structured data.</returns>
		// Token: 0x06002C57 RID: 11351 RVA: 0x000BE7CC File Offset: 0x000BC9CC
		IDataReader IDataRecord.GetData(int ordinal)
		{
			return this.GetDbDataReader(ordinal);
		}

		/// <summary>Returns a <see cref="T:System.Data.Common.DbDataReader" /> object for the requested column ordinal that can be overridden with a provider-specific implementation.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>A <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		// Token: 0x06002C58 RID: 11352 RVA: 0x00008E4B File Offset: 0x0000704B
		protected virtual DbDataReader GetDbDataReader(int ordinal)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06002C59 RID: 11353
		public abstract DateTime GetDateTime(int ordinal);

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Decimal" /> object.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06002C5A RID: 11354
		public abstract decimal GetDecimal(int ordinal);

		/// <summary>Gets the value of the specified column as a double-precision floating point number.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06002C5B RID: 11355
		public abstract double GetDouble(int ordinal);

		/// <summary>Gets the value of the specified column as a single-precision floating point number.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06002C5C RID: 11356
		public abstract float GetFloat(int ordinal);

		/// <summary>Gets the value of the specified column as a globally-unique identifier (GUID).</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06002C5D RID: 11357
		public abstract Guid GetGuid(int ordinal);

		/// <summary>Gets the value of the specified column as a 16-bit signed integer.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06002C5E RID: 11358
		public abstract short GetInt16(int ordinal);

		/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06002C5F RID: 11359
		public abstract int GetInt32(int ordinal);

		/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06002C60 RID: 11360
		public abstract long GetInt64(int ordinal);

		/// <summary>Returns the provider-specific field type of the specified column.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The <see cref="T:System.Type" /> object that describes the data type of the specified column.</returns>
		// Token: 0x06002C61 RID: 11361 RVA: 0x000BE7D5 File Offset: 0x000BC9D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual Type GetProviderSpecificFieldType(int ordinal)
		{
			return this.GetFieldType(ordinal);
		}

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		// Token: 0x06002C62 RID: 11362 RVA: 0x0006AE3E File Offset: 0x0006903E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual object GetProviderSpecificValue(int ordinal)
		{
			return this.GetValue(ordinal);
		}

		/// <summary>Gets all provider-specific attribute columns in the collection for the current row.</summary>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the attribute columns.</param>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		// Token: 0x06002C63 RID: 11363 RVA: 0x000BE7DE File Offset: 0x000BC9DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual int GetProviderSpecificValues(object[] values)
		{
			return this.GetValues(values);
		}

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.String" />.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06002C64 RID: 11364
		public abstract string GetString(int ordinal);

		/// <summary>Retrieves data as a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="ordinal">Retrieves data as a <see cref="T:System.IO.Stream" />.</param>
		/// <returns>The returned object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
		/// -or-
		///  The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.  
		/// -or-
		///  There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).  
		/// -or-
		///  Tried to read a previously-read column in sequential mode.  
		/// -or-
		///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">The returned type was not one of the following types: binary, image, varbinary, or udt.</exception>
		// Token: 0x06002C65 RID: 11365 RVA: 0x000BE7E8 File Offset: 0x000BC9E8
		public virtual Stream GetStream(int ordinal)
		{
			Stream result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				long num = 0L;
				byte[] array = new byte[4096];
				long bytes;
				do
				{
					bytes = this.GetBytes(ordinal, num, array, 0, array.Length);
					memoryStream.Write(array, 0, (int)bytes);
					num += bytes;
				}
				while (bytes > 0L);
				result = new MemoryStream(memoryStream.ToArray(), false);
			}
			return result;
		}

		/// <summary>Retrieves data as a <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="ordinal">Retrieves data as a <see cref="T:System.IO.TextReader" />.</param>
		/// <returns>The returned object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
		/// -or-
		///  The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.  
		/// -or-
		///  There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).  
		/// -or-
		///  Tried to read a previously-read column in sequential mode.  
		/// -or-
		///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">The returned type was not one of the following types: char, nchar, ntext, nvarchar, text, or varchar.</exception>
		// Token: 0x06002C66 RID: 11366 RVA: 0x000BE85C File Offset: 0x000BCA5C
		public virtual TextReader GetTextReader(int ordinal)
		{
			if (this.IsDBNull(ordinal))
			{
				return new StringReader(string.Empty);
			}
			return new StringReader(this.GetString(ordinal));
		}

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		// Token: 0x06002C67 RID: 11367
		public abstract object GetValue(int ordinal);

		/// <summary>Synchronously gets the value of the specified column as a type.</summary>
		/// <param name="ordinal">The column to be retrieved.</param>
		/// <typeparam name="T">Synchronously gets the value of the specified column as a type.</typeparam>
		/// <returns>The column to be retrieved.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
		/// -or-
		///  The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.  
		/// -or-
		///  There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).  
		/// -or-
		///  Tried to read a previously-read column in sequential mode.  
		/// -or-
		///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="T" /> doesn't match the type returned by SQL Server or cannot be cast.</exception>
		// Token: 0x06002C68 RID: 11368 RVA: 0x000BE87E File Offset: 0x000BCA7E
		public virtual T GetFieldValue<T>(int ordinal)
		{
			return (T)((object)this.GetValue(ordinal));
		}

		/// <summary>Asynchronously gets the value of the specified column as a type.</summary>
		/// <param name="ordinal">The type of the value to be returned.</param>
		/// <typeparam name="T">The type of the value to be returned.</typeparam>
		/// <returns>The type of the value to be returned.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
		/// -or-
		///  The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.  
		/// -or-
		///  There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).  
		/// -or-
		///  Tried to read a previously-read column in sequential mode.  
		/// -or-
		///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="T" /> doesn't match the type returned by the data source  or cannot be cast.</exception>
		// Token: 0x06002C69 RID: 11369 RVA: 0x000BE88C File Offset: 0x000BCA8C
		public Task<T> GetFieldValueAsync<T>(int ordinal)
		{
			return this.GetFieldValueAsync<T>(ordinal, CancellationToken.None);
		}

		/// <summary>Asynchronously gets the value of the specified column as a type.</summary>
		/// <param name="ordinal">The type of the value to be returned.</param>
		/// <param name="cancellationToken">The cancellation instruction, which propagates a notification that operations should be canceled. This does not guarantee the cancellation. A setting of <see langword="CancellationToken.None" /> makes this method equivalent to <see cref="M:System.Data.Common.DbDataReader.GetFieldValueAsync``1(System.Int32)" />. The returned task must be marked as cancelled.</param>
		/// <typeparam name="T">The type of the value to be returned.</typeparam>
		/// <returns>The type of the value to be returned.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
		/// -or-
		///  The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.  
		/// -or-
		///  There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).  
		/// -or-
		///  Tried to read a previously-read column in sequential mode.  
		/// -or-
		///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="T" /> doesn't match the type returned by the data source or cannot be cast.</exception>
		// Token: 0x06002C6A RID: 11370 RVA: 0x000BE89C File Offset: 0x000BCA9C
		public virtual Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<T>();
			}
			Task<T> result;
			try
			{
				result = Task.FromResult<T>(this.GetFieldValue<T>(ordinal));
			}
			catch (Exception exception)
			{
				result = Task.FromException<T>(exception);
			}
			return result;
		}

		/// <summary>Populates an array of objects with the column values of the current row.</summary>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the attribute columns.</param>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		// Token: 0x06002C6B RID: 11371
		public abstract int GetValues(object[] values);

		/// <summary>Gets a value that indicates whether the column contains nonexistent or missing values.</summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>
		///   <see langword="true" /> if the specified column is equivalent to <see cref="T:System.DBNull" />; otherwise <see langword="false" />.</returns>
		// Token: 0x06002C6C RID: 11372
		public abstract bool IsDBNull(int ordinal);

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbDataReader.IsDBNull(System.Int32)" />, which gets a value that indicates whether the column contains non-existent or missing values.</summary>
		/// <param name="ordinal">The zero-based column to be retrieved.</param>
		/// <returns>
		///   <see langword="true" /> if the specified column value is equivalent to <see langword="DBNull" /> otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
		/// -or-
		///  The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.  
		/// -or-
		///  There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).  
		/// -or-
		///  Trying to read a previously read column in sequential mode.  
		/// -or-
		///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		// Token: 0x06002C6D RID: 11373 RVA: 0x000BE8E4 File Offset: 0x000BCAE4
		public Task<bool> IsDBNullAsync(int ordinal)
		{
			return this.IsDBNullAsync(ordinal, CancellationToken.None);
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbDataReader.IsDBNull(System.Int32)" />, which gets a value that indicates whether the column contains non-existent or missing values. Optionally, sends a notification that operations should be cancelled.</summary>
		/// <param name="ordinal">The zero-based column to be retrieved.</param>
		/// <param name="cancellationToken">The cancellation instruction, which propagates a notification that operations should be canceled. This does not guarantee the cancellation. A setting of <see langword="CancellationToken.None" /> makes this method equivalent to <see cref="M:System.Data.Common.DbDataReader.IsDBNullAsync(System.Int32)" />. The returned task must be marked as cancelled.</param>
		/// <returns>
		///   <see langword="true" /> if the specified column value is equivalent to <see langword="DBNull" /> otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
		/// -or-
		///  The <see cref="T:System.Data.Common.DbDataReader" /> is closed during the data retrieval.  
		/// -or-
		///  There is no data ready to be read (for example, the first <see cref="M:System.Data.Common.DbDataReader.Read" /> hasn't been called, or returned false).  
		/// -or-
		///  Trying to read a previously read column in sequential mode.  
		/// -or-
		///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		// Token: 0x06002C6E RID: 11374 RVA: 0x000BE8F4 File Offset: 0x000BCAF4
		public virtual Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<bool>();
			}
			Task<bool> result;
			try
			{
				result = (this.IsDBNull(ordinal) ? ADP.TrueTask : ADP.FalseTask);
			}
			catch (Exception exception)
			{
				result = Task.FromException<bool>(exception);
			}
			return result;
		}

		/// <summary>Advances the reader to the next result when reading the results of a batch of statements.</summary>
		/// <returns>
		///   <see langword="true" /> if there are more result sets; otherwise <see langword="false" />.</returns>
		// Token: 0x06002C6F RID: 11375
		public abstract bool NextResult();

		/// <summary>Advances the reader to the next record in a result set.</summary>
		/// <returns>
		///   <see langword="true" /> if there are more rows; otherwise <see langword="false" />.</returns>
		// Token: 0x06002C70 RID: 11376
		public abstract bool Read();

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbDataReader.Read" />, which advances the reader to the next record in a result set. This method invokes <see cref="M:System.Data.Common.DbDataReader.ReadAsync(System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x06002C71 RID: 11377 RVA: 0x000BE944 File Offset: 0x000BCB44
		public Task<bool> ReadAsync()
		{
			return this.ReadAsync(CancellationToken.None);
		}

		/// <summary>This is the asynchronous version of <see cref="M:System.Data.Common.DbDataReader.Read" />.  Providers should override with an appropriate implementation. The cancellationToken may optionally be ignored.  
		///  The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbDataReader.Read" /> method and returns a completed task, blocking the calling thread. The default implementation will return a cancelled task if passed an already cancelled cancellationToken.  Exceptions thrown by Read will be communicated via the returned Task Exception property.  
		///  Do not invoke other methods and properties of the <see langword="DbDataReader" /> object until the returned Task is complete.</summary>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x06002C72 RID: 11378 RVA: 0x000BE954 File Offset: 0x000BCB54
		public virtual Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<bool>();
			}
			Task<bool> result;
			try
			{
				result = (this.Read() ? ADP.TrueTask : ADP.FalseTask);
			}
			catch (Exception exception)
			{
				result = Task.FromException<bool>(exception);
			}
			return result;
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbDataReader.NextResult" />, which advances the reader to the next result when reading the results of a batch of statements.  
		///  Invokes <see cref="M:System.Data.Common.DbDataReader.NextResultAsync(System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x06002C73 RID: 11379 RVA: 0x000BE9A4 File Offset: 0x000BCBA4
		public Task<bool> NextResultAsync()
		{
			return this.NextResultAsync(CancellationToken.None);
		}

		/// <summary>This is the asynchronous version of <see cref="M:System.Data.Common.DbDataReader.NextResult" />. Providers should override with an appropriate implementation. The <paramref name="cancellationToken" /> may optionally be ignored.  
		///  The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbDataReader.NextResult" /> method and returns a completed task, blocking the calling thread. The default implementation will return a cancelled task if passed an already cancelled <paramref name="cancellationToken" />. Exceptions thrown by <see cref="M:System.Data.Common.DbDataReader.NextResult" /> will be communicated via the returned Task Exception property.  
		///  Other methods and properties of the DbDataReader object should not be invoked while the returned Task is not yet completed.</summary>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x06002C74 RID: 11380 RVA: 0x000BE9B4 File Offset: 0x000BCBB4
		public virtual Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<bool>();
			}
			Task<bool> result;
			try
			{
				result = (this.NextResult() ? ADP.TrueTask : ADP.FalseTask);
			}
			catch (Exception exception)
			{
				result = Task.FromException<bool>(exception);
			}
			return result;
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x000BEA04 File Offset: 0x000BCC04
		public virtual Task CloseAsync()
		{
			Task result;
			try
			{
				this.Close();
				result = Task.CompletedTask;
			}
			catch (Exception exception)
			{
				result = Task.FromException(exception);
			}
			return result;
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x000BEA38 File Offset: 0x000BCC38
		public virtual ValueTask DisposeAsync()
		{
			this.Dispose();
			return default(ValueTask);
		}
	}
}
