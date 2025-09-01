using System;

namespace System.Data
{
	/// <summary>Provides access to the column values within each row for a <see langword="DataReader" />, and is implemented by .NET Framework data providers that access relational databases.</summary>
	// Token: 0x02000102 RID: 258
	public interface IDataRecord
	{
		/// <summary>Gets the number of columns in the current row.</summary>
		/// <returns>When not positioned in a valid recordset, 0; otherwise, the number of columns in the current record. The default is -1.</returns>
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000F2C RID: 3884
		int FieldCount { get; }

		/// <summary>Gets the column located at the specified index.</summary>
		/// <param name="i">The zero-based index of the column to get.</param>
		/// <returns>The column located at the specified index as an <see cref="T:System.Object" />.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x1700029B RID: 667
		object this[int i]
		{
			get;
		}

		/// <summary>Gets the column with the specified name.</summary>
		/// <param name="name">The name of the column to find.</param>
		/// <returns>The column with the specified name as an <see cref="T:System.Object" />.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">No column with the specified name was found.</exception>
		// Token: 0x1700029C RID: 668
		object this[string name]
		{
			get;
		}

		/// <summary>Gets the name for the field to find.</summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>The name of the field or the empty string (""), if there is no value to return.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F2F RID: 3887
		string GetName(int i);

		/// <summary>Gets the data type information for the specified field.</summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>The data type information for the specified field.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F30 RID: 3888
		string GetDataTypeName(int i);

		/// <summary>Gets the <see cref="T:System.Type" /> information corresponding to the type of <see cref="T:System.Object" /> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)" />.</summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>The <see cref="T:System.Type" /> information corresponding to the type of <see cref="T:System.Object" /> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)" />.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F31 RID: 3889
		Type GetFieldType(int i);

		/// <summary>Return the value of the specified field.</summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>The <see cref="T:System.Object" /> which will contain the field value upon return.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F32 RID: 3890
		object GetValue(int i);

		/// <summary>Populates an array of objects with the column values of the current record.</summary>
		/// <param name="values">An array of <see cref="T:System.Object" /> to copy the attribute fields into.</param>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		// Token: 0x06000F33 RID: 3891
		int GetValues(object[] values);

		/// <summary>Return the index of the named field.</summary>
		/// <param name="name">The name of the field to find.</param>
		/// <returns>The index of the named field.</returns>
		// Token: 0x06000F34 RID: 3892
		int GetOrdinal(string name);

		/// <summary>Gets the value of the specified column as a Boolean.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F35 RID: 3893
		bool GetBoolean(int i);

		/// <summary>Gets the 8-bit unsigned integer value of the specified column.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The 8-bit unsigned integer value of the specified column.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F36 RID: 3894
		byte GetByte(int i);

		/// <summary>Reads a stream of bytes from the specified column offset into the buffer as an array, starting at the given buffer offset.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <param name="fieldOffset">The index within the field from which to start the read operation.</param>
		/// <param name="buffer">The buffer into which to read the stream of bytes.</param>
		/// <param name="bufferoffset">The index for <paramref name="buffer" /> to start the read operation.</param>
		/// <param name="length">The number of bytes to read.</param>
		/// <returns>The actual number of bytes read.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F37 RID: 3895
		long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length);

		/// <summary>Gets the character value of the specified column.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The character value of the specified column.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F38 RID: 3896
		char GetChar(int i);

		/// <summary>Reads a stream of characters from the specified column offset into the buffer as an array, starting at the given buffer offset.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <param name="fieldoffset">The index within the row from which to start the read operation.</param>
		/// <param name="buffer">The buffer into which to read the stream of bytes.</param>
		/// <param name="bufferoffset">The index for <paramref name="buffer" /> to start the read operation.</param>
		/// <param name="length">The number of bytes to read.</param>
		/// <returns>The actual number of characters read.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F39 RID: 3897
		long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length);

		/// <summary>Returns the GUID value of the specified field.</summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>The GUID value of the specified field.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F3A RID: 3898
		Guid GetGuid(int i);

		/// <summary>Gets the 16-bit signed integer value of the specified field.</summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>The 16-bit signed integer value of the specified field.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F3B RID: 3899
		short GetInt16(int i);

		/// <summary>Gets the 32-bit signed integer value of the specified field.</summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>The 32-bit signed integer value of the specified field.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F3C RID: 3900
		int GetInt32(int i);

		/// <summary>Gets the 64-bit signed integer value of the specified field.</summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>The 64-bit signed integer value of the specified field.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F3D RID: 3901
		long GetInt64(int i);

		/// <summary>Gets the single-precision floating point number of the specified field.</summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>The single-precision floating point number of the specified field.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F3E RID: 3902
		float GetFloat(int i);

		/// <summary>Gets the double-precision floating point number of the specified field.</summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>The double-precision floating point number of the specified field.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F3F RID: 3903
		double GetDouble(int i);

		/// <summary>Gets the string value of the specified field.</summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>The string value of the specified field.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F40 RID: 3904
		string GetString(int i);

		/// <summary>Gets the fixed-position numeric value of the specified field.</summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>The fixed-position numeric value of the specified field.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F41 RID: 3905
		decimal GetDecimal(int i);

		/// <summary>Gets the date and time data value of the specified field.</summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>The date and time data value of the specified field.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F42 RID: 3906
		DateTime GetDateTime(int i);

		/// <summary>Returns an <see cref="T:System.Data.IDataReader" /> for the specified column ordinal.</summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>The <see cref="T:System.Data.IDataReader" /> for the specified column ordinal.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F43 RID: 3907
		IDataReader GetData(int i);

		/// <summary>Return whether the specified field is set to null.</summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>
		///   <see langword="true" /> if the specified field is set to null; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x06000F44 RID: 3908
		bool IsDBNull(int i);
	}
}
