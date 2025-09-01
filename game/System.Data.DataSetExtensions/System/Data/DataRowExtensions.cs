using System;

namespace System.Data
{
	/// <summary>Defines the extension methods to the <see cref="T:System.Data.DataRow" /> class. This is a static class.</summary>
	// Token: 0x02000007 RID: 7
	public static class DataRowExtensions
	{
		/// <summary>Provides strongly-typed access to each of the column values in the specified row. The <see cref="M:System.Data.DataRowExtensions.Field``1(System.Data.DataRow,System.String)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="columnName">The name of the column to return the value of.</param>
		/// <typeparam name="T">A generic parameter that specifies the return type of the column.</typeparam>
		/// <returns>The value, of type <paramref name="T" />, of the <see cref="T:System.Data.DataColumn" /> specified by <paramref name="columnName" />.</returns>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could not be cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The column specified by <paramref name="columnName" /> does not occur in the <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Data.DataRow" /> is a part of.</exception>
		/// <exception cref="T:System.NullReferenceException">A <see langword="null" /> value was assigned to a non-nullable type.</exception>
		// Token: 0x0600001A RID: 26 RVA: 0x00002553 File Offset: 0x00000753
		public static T Field<T>(this DataRow row, string columnName)
		{
			DataSetUtil.CheckArgumentNull<DataRow>(row, "row");
			return DataRowExtensions.UnboxT<T>.s_unbox(row[columnName]);
		}

		/// <summary>Provides strongly-typed access to each of the column values in the specified row. The <see cref="M:System.Data.DataRowExtensions.Field``1(System.Data.DataRow,System.Data.DataColumn)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="column">The input <see cref="T:System.Data.DataColumn" /> object that specifies the column to return the value of.</param>
		/// <typeparam name="T">A generic parameter that specifies the return type of the column.</typeparam>
		/// <returns>The value, of type <paramref name="T" />, of the <see cref="T:System.Data.DataColumn" /> specified by <paramref name="column" />.</returns>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could not be cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The column specified by <paramref name="column" /> does not occur in the <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Data.DataRow" /> is a part of.</exception>
		/// <exception cref="T:System.NullReferenceException">A null value was assigned to a non-nullable type.</exception>
		// Token: 0x0600001B RID: 27 RVA: 0x00002571 File Offset: 0x00000771
		public static T Field<T>(this DataRow row, DataColumn column)
		{
			DataSetUtil.CheckArgumentNull<DataRow>(row, "row");
			return DataRowExtensions.UnboxT<T>.s_unbox(row[column]);
		}

		/// <summary>Provides strongly-typed access to each of the column values in the specified row. The <see cref="M:System.Data.DataRowExtensions.Field``1(System.Data.DataRow,System.Int32)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="columnIndex">The column index.</param>
		/// <typeparam name="T">A generic parameter that specifies the return type of the column.</typeparam>
		/// <returns>The value, of type <paramref name="T" />, of the <see cref="T:System.Data.DataColumn" /> specified by <paramref name="columnIndex" />.</returns>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could not be cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The column specified by <paramref name="ordinal" /> does not exist in the <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Data.DataRow" /> is a part of.</exception>
		/// <exception cref="T:System.NullReferenceException">A null value was assigned to a non-nullable type.</exception>
		// Token: 0x0600001C RID: 28 RVA: 0x0000258F File Offset: 0x0000078F
		public static T Field<T>(this DataRow row, int columnIndex)
		{
			DataSetUtil.CheckArgumentNull<DataRow>(row, "row");
			return DataRowExtensions.UnboxT<T>.s_unbox(row[columnIndex]);
		}

		/// <summary>Provides strongly-typed access to each of the column values in the specified row. The <see cref="M:System.Data.DataRowExtensions.Field``1(System.Data.DataRow,System.Int32,System.Data.DataRowVersion)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="columnIndex">The zero-based ordinal of the column to return the value of.</param>
		/// <param name="version">A <see cref="T:System.Data.DataRowVersion" /> enumeration that specifies the version of the column value to return, such as <see langword="Current" /> or <see langword="Original" /> version.</param>
		/// <typeparam name="T">A generic parameter that specifies the return type of the column.</typeparam>
		/// <returns>The value, of type <paramref name="T" />, of the <see cref="T:System.Data.DataColumn" /> specified by <paramref name="ordinal" /> and <paramref name="version" />.</returns>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could not be cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The column specified by <paramref name="ordinal" /> does not exist in the <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Data.DataRow" /> is a part of.</exception>
		/// <exception cref="T:System.NullReferenceException">A null value was assigned to a non-nullable type.</exception>
		// Token: 0x0600001D RID: 29 RVA: 0x000025AD File Offset: 0x000007AD
		public static T Field<T>(this DataRow row, int columnIndex, DataRowVersion version)
		{
			DataSetUtil.CheckArgumentNull<DataRow>(row, "row");
			return DataRowExtensions.UnboxT<T>.s_unbox(row[columnIndex, version]);
		}

		/// <summary>Provides strongly-typed access to each of the column values in the specified row. The <see cref="M:System.Data.DataRowExtensions.Field``1(System.Data.DataRow,System.String,System.Data.DataRowVersion)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="columnName">The name of the column to return the value of.</param>
		/// <param name="version">A <see cref="T:System.Data.DataRowVersion" /> enumeration that specifies the version of the column value to return, such as <see langword="Current" /> or <see langword="Original" /> version.</param>
		/// <typeparam name="T">A generic parameter that specifies the return type of the column.</typeparam>
		/// <returns>The value, of type <paramref name="T" />, of the <see cref="T:System.Data.DataColumn" /> specified by <paramref name="columnName" /> and <paramref name="version" />.</returns>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could not be cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The column specified by <paramref name="columnName" /> does not exist in the <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Data.DataRow" /> is a part of.</exception>
		/// <exception cref="T:System.NullReferenceException">A null value was assigned to a non-nullable type.</exception>
		// Token: 0x0600001E RID: 30 RVA: 0x000025CC File Offset: 0x000007CC
		public static T Field<T>(this DataRow row, string columnName, DataRowVersion version)
		{
			DataSetUtil.CheckArgumentNull<DataRow>(row, "row");
			return DataRowExtensions.UnboxT<T>.s_unbox(row[columnName, version]);
		}

		/// <summary>Provides strongly-typed access to each of the column values in the specified row. The <see cref="M:System.Data.DataRowExtensions.Field``1(System.Data.DataRow,System.Data.DataColumn,System.Data.DataRowVersion)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="column">The input <see cref="T:System.Data.DataColumn" /> object that specifies the column to return the value of.</param>
		/// <param name="version">A <see cref="T:System.Data.DataRowVersion" /> enumeration that specifies the version of the column value to return, such as <see langword="Current" /> or <see langword="Original" /> version.</param>
		/// <typeparam name="T">A generic parameter that specifies the return type of the column.</typeparam>
		/// <returns>The value, of type <paramref name="T" />, of the <see cref="T:System.Data.DataColumn" /> specified by <paramref name="column" /> and <paramref name="version" />.</returns>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could not be cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The column specified by <paramref name="column" /> does not exist in the <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Data.DataRow" /> is a part of.</exception>
		/// <exception cref="T:System.NullReferenceException">A null value was assigned to a non-nullable type.</exception>
		// Token: 0x0600001F RID: 31 RVA: 0x000025EB File Offset: 0x000007EB
		public static T Field<T>(this DataRow row, DataColumn column, DataRowVersion version)
		{
			DataSetUtil.CheckArgumentNull<DataRow>(row, "row");
			return DataRowExtensions.UnboxT<T>.s_unbox(row[column, version]);
		}

		/// <summary>Sets a new value for the specified column in the <see cref="T:System.Data.DataRow" /> the method is called on. The <see cref="M:System.Data.DataRowExtensions.SetField``1(System.Data.DataRow,System.Int32,``0)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="columnIndex">The zero-based ordinal of the column to set the value of.</param>
		/// <param name="value">The new row value for the specified column, of type <paramref name="T" />.</param>
		/// <typeparam name="T">A generic parameter that specifies the value type of the column.</typeparam>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">Occurs when attempting to set a value on a deleted row.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> argument is out of range.</exception>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could be not cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		// Token: 0x06000020 RID: 32 RVA: 0x0000260A File Offset: 0x0000080A
		public static void SetField<T>(this DataRow row, int columnIndex, T value)
		{
			DataSetUtil.CheckArgumentNull<DataRow>(row, "row");
			row[columnIndex] = (value ?? DBNull.Value);
		}

		/// <summary>Sets a new value for the specified column in the <see cref="T:System.Data.DataRow" />. The <see cref="M:System.Data.DataRowExtensions.SetField``1(System.Data.DataRow,System.String,``0)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="columnName">The name of the column to set the value of.</param>
		/// <param name="value">The new row value for the specified column, of type <paramref name="T" />.</param>
		/// <typeparam name="T">A generic parameter that specifies the value type of the column.</typeparam>
		/// <exception cref="T:System.ArgumentException">The column specified by <paramref name="columnName" /> cannot be found.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">Occurs when attempting to set a value on a deleted row.</exception>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could not be cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		// Token: 0x06000021 RID: 33 RVA: 0x0000262D File Offset: 0x0000082D
		public static void SetField<T>(this DataRow row, string columnName, T value)
		{
			DataSetUtil.CheckArgumentNull<DataRow>(row, "row");
			row[columnName] = (value ?? DBNull.Value);
		}

		/// <summary>Sets a new value for the specified column in the <see cref="T:System.Data.DataRow" />. The <see cref="M:System.Data.DataRowExtensions.SetField``1(System.Data.DataRow,System.Data.DataColumn,``0)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="column">The input <see cref="T:System.Data.DataColumn" /> specifies which row value to retrieve.</param>
		/// <param name="value">The new row value for the specified column, of type <paramref name="T" />.</param>
		/// <typeparam name="T">A generic parameter that specifies the value type of the column.</typeparam>
		/// <exception cref="T:System.ArgumentException">The column specified by <paramref name="column" /> cannot be found.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> is null.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">Occurs when attempting to set a value on a deleted row.</exception>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could not be cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		// Token: 0x06000022 RID: 34 RVA: 0x00002650 File Offset: 0x00000850
		public static void SetField<T>(this DataRow row, DataColumn column, T value)
		{
			DataSetUtil.CheckArgumentNull<DataRow>(row, "row");
			row[column] = (value ?? DBNull.Value);
		}

		// Token: 0x02000008 RID: 8
		private static class UnboxT<T>
		{
			// Token: 0x06000023 RID: 35 RVA: 0x00002674 File Offset: 0x00000874
			private static Converter<object, T> Create()
			{
				if (default(T) == null)
				{
					return new Converter<object, T>(DataRowExtensions.UnboxT<T>.ReferenceOrNullableField);
				}
				return new Converter<object, T>(DataRowExtensions.UnboxT<T>.ValueField);
			}

			// Token: 0x06000024 RID: 36 RVA: 0x000026AC File Offset: 0x000008AC
			private static T ReferenceOrNullableField(object value)
			{
				if (DBNull.Value != value)
				{
					return (T)((object)value);
				}
				return default(T);
			}

			// Token: 0x06000025 RID: 37 RVA: 0x000026D1 File Offset: 0x000008D1
			private static T ValueField(object value)
			{
				if (DBNull.Value == value)
				{
					throw DataSetUtil.InvalidCast(string.Format("Cannot cast DBNull. Value to type '{0}'. Please use a nullable type.", typeof(T).ToString()));
				}
				return (T)((object)value);
			}

			// Token: 0x06000026 RID: 38 RVA: 0x00002700 File Offset: 0x00000900
			// Note: this type is marked as 'beforefieldinit'.
			static UnboxT()
			{
			}

			// Token: 0x04000038 RID: 56
			internal static readonly Converter<object, T> s_unbox = DataRowExtensions.UnboxT<T>.Create();
		}
	}
}
