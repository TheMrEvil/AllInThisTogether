using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Data
{
	/// <summary>Defines the extension methods to the <see cref="T:System.Data.DataTable" /> class. <see cref="T:System.Data.DataTableExtensions" /> is a static class.</summary>
	// Token: 0x02000009 RID: 9
	public static class DataTableExtensions
	{
		/// <summary>Returns an <see cref="T:System.Collections.Generic.IEnumerable`1" /> object, where the generic parameter <paramref name="T" /> is <see cref="T:System.Data.DataRow" />. This object can be used in a LINQ expression or method query.</summary>
		/// <param name="source">The source <see cref="T:System.Data.DataTable" /> to make enumerable.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> object, where the generic parameter <paramref name="T" /> is <see cref="T:System.Data.DataRow" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The source <see cref="T:System.Data.DataTable" /> is <see langword="null" />.</exception>
		// Token: 0x06000027 RID: 39 RVA: 0x0000270C File Offset: 0x0000090C
		public static EnumerableRowCollection<DataRow> AsEnumerable(this DataTable source)
		{
			DataSetUtil.CheckArgumentNull<DataTable>(source, "source");
			return new EnumerableRowCollection<DataRow>(source);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that contains copies of the <see cref="T:System.Data.DataRow" /> objects, given an input <see cref="T:System.Collections.Generic.IEnumerable`1" /> object where the generic parameter <paramref name="T" /> is <see cref="T:System.Data.DataRow" />.</summary>
		/// <param name="source">The source <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence.</param>
		/// <typeparam name="T">The type of objects in the source sequence, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains the input sequence as the type of <see cref="T:System.Data.DataRow" /> objects.</returns>
		/// <exception cref="T:System.ArgumentNullException">The source <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence is <see langword="null" /> and a new table cannot be created.</exception>
		/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Data.DataRow" /> in the source sequence has a state of <see cref="F:System.Data.DataRowState.Deleted" />.  
		///  The source sequence does not contain any <see cref="T:System.Data.DataRow" /> objects.  
		///  A <see cref="T:System.Data.DataRow" /> in the source sequence is <see langword="null" />.</exception>
		// Token: 0x06000028 RID: 40 RVA: 0x00002720 File Offset: 0x00000920
		public static DataTable CopyToDataTable<T>(this IEnumerable<T> source) where T : DataRow
		{
			DataSetUtil.CheckArgumentNull<IEnumerable<T>>(source, "source");
			return DataTableExtensions.LoadTableFromEnumerable<T>(source, null, null, null);
		}

		/// <summary>Copies <see cref="T:System.Data.DataRow" /> objects to the specified <see cref="T:System.Data.DataTable" />, given an input <see cref="T:System.Collections.Generic.IEnumerable`1" /> object where the generic parameter <paramref name="T" /> is <see cref="T:System.Data.DataRow" />.</summary>
		/// <param name="source">The source <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence.</param>
		/// <param name="table">The destination <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="options">A <see cref="T:System.Data.LoadOption" /> enumeration that specifies the <see cref="T:System.Data.DataTable" /> load options.</param>
		/// <typeparam name="T">The type of objects in the source sequence, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <exception cref="T:System.ArgumentException">The copied <see cref="T:System.Data.DataRow" /> objects do not fit the schema of the destination <see cref="T:System.Data.DataTable" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The source <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence is <see langword="null" /> or the destination <see cref="T:System.Data.DataTable" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Data.DataRow" /> in the source sequence has a state of <see cref="F:System.Data.DataRowState.Deleted" />.  
		///  The source sequence does not contain any <see cref="T:System.Data.DataRow" /> objects.  
		///  A <see cref="T:System.Data.DataRow" /> in the source sequence is <see langword="null" />.</exception>
		// Token: 0x06000029 RID: 41 RVA: 0x00002749 File Offset: 0x00000949
		public static void CopyToDataTable<T>(this IEnumerable<T> source, DataTable table, LoadOption options) where T : DataRow
		{
			DataSetUtil.CheckArgumentNull<IEnumerable<T>>(source, "source");
			DataSetUtil.CheckArgumentNull<DataTable>(table, "table");
			DataTableExtensions.LoadTableFromEnumerable<T>(source, table, new LoadOption?(options), null);
		}

		/// <summary>Copies <see cref="T:System.Data.DataRow" /> objects to the specified <see cref="T:System.Data.DataTable" />, given an input <see cref="T:System.Collections.Generic.IEnumerable`1" /> object where the generic parameter <paramref name="T" /> is <see cref="T:System.Data.DataRow" />.</summary>
		/// <param name="source">The source <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence.</param>
		/// <param name="table">The destination <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="options">A <see cref="T:System.Data.LoadOption" /> enumeration that specifies the <see cref="T:System.Data.DataTable" /> load options.</param>
		/// <param name="errorHandler">A <see cref="T:System.Data.FillErrorEventHandler" /> delegate that represents the method that will handle an error.</param>
		/// <typeparam name="T">The type of objects in the source sequence, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <exception cref="T:System.ArgumentException">The copied <see cref="T:System.Data.DataRow" /> objects do not fit the schema of the destination <see cref="T:System.Data.DataTable" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The source <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence is <see langword="null" /> or the destination <see cref="T:System.Data.DataTable" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Data.DataRow" /> in the source sequence has a state of <see cref="F:System.Data.DataRowState.Deleted" />.  
		///  -or-  
		///  The source sequence does not contain any <see cref="T:System.Data.DataRow" /> objects.  
		///  -or-  
		///  A <see cref="T:System.Data.DataRow" /> in the source sequence is <see langword="null" />.</exception>
		// Token: 0x0600002A RID: 42 RVA: 0x00002770 File Offset: 0x00000970
		public static void CopyToDataTable<T>(this IEnumerable<T> source, DataTable table, LoadOption options, FillErrorEventHandler errorHandler) where T : DataRow
		{
			DataSetUtil.CheckArgumentNull<IEnumerable<T>>(source, "source");
			DataSetUtil.CheckArgumentNull<DataTable>(table, "table");
			DataTableExtensions.LoadTableFromEnumerable<T>(source, table, new LoadOption?(options), errorHandler);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002798 File Offset: 0x00000998
		private static DataTable LoadTableFromEnumerable<T>(IEnumerable<T> source, DataTable table, LoadOption? options, FillErrorEventHandler errorHandler) where T : DataRow
		{
			if (options != null)
			{
				LoadOption value = options.Value;
				if (value - LoadOption.OverwriteChanges > 2)
				{
					throw DataSetUtil.InvalidLoadOption(options.Value);
				}
			}
			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					DataTable dataTable = table;
					if (dataTable == null)
					{
						throw DataSetUtil.InvalidOperation("The source contains no DataRows.");
					}
					return dataTable;
				}
				else
				{
					if (table == null)
					{
						DataRow dataRow = enumerator.Current;
						if (dataRow == null)
						{
							throw DataSetUtil.InvalidOperation("The source contains a DataRow reference that is null.");
						}
						table = new DataTable
						{
							Locale = CultureInfo.CurrentCulture
						};
						foreach (object obj in dataRow.Table.Columns)
						{
							DataColumn dataColumn = (DataColumn)obj;
							table.Columns.Add(dataColumn.ColumnName, dataColumn.DataType);
						}
					}
					table.BeginLoadData();
					try
					{
						do
						{
							DataRow dataRow = enumerator.Current;
							if (dataRow != null)
							{
								object[] values = null;
								try
								{
									DataRowState rowState = dataRow.RowState;
									switch (rowState)
									{
									case DataRowState.Detached:
										if (!dataRow.HasVersion(DataRowVersion.Proposed))
										{
											throw DataSetUtil.InvalidOperation("The source contains a detached DataRow that cannot be copied to the DataTable.");
										}
										break;
									case DataRowState.Unchanged:
									case DataRowState.Added:
										break;
									case DataRowState.Detached | DataRowState.Unchanged:
										goto IL_172;
									default:
										if (rowState == DataRowState.Deleted)
										{
											throw DataSetUtil.InvalidOperation("The source contains a deleted DataRow that cannot be copied to the DataTable.");
										}
										if (rowState != DataRowState.Modified)
										{
											goto IL_172;
										}
										break;
									}
									values = dataRow.ItemArray;
									if (options != null)
									{
										table.LoadDataRow(values, options.Value);
									}
									else
									{
										table.LoadDataRow(values, true);
									}
									goto IL_1DA;
									IL_172:
									throw DataSetUtil.InvalidDataRowState(dataRow.RowState);
								}
								catch (Exception ex)
								{
									if (!DataSetUtil.IsCatchableExceptionType(ex))
									{
										throw;
									}
									FillErrorEventArgs fillErrorEventArgs = null;
									if (errorHandler != null)
									{
										fillErrorEventArgs = new FillErrorEventArgs(table, values)
										{
											Errors = ex
										};
										errorHandler(enumerator, fillErrorEventArgs);
									}
									if (fillErrorEventArgs == null)
									{
										throw;
									}
									if (!fillErrorEventArgs.Continue)
									{
										if ((fillErrorEventArgs.Errors ?? ex) == ex)
										{
											throw;
										}
										throw fillErrorEventArgs.Errors;
									}
								}
							}
							IL_1DA:;
						}
						while (enumerator.MoveNext());
					}
					finally
					{
						table.EndLoadData();
					}
				}
			}
			return table;
		}

		/// <summary>Creates and returns a LINQ-enabled <see cref="T:System.Data.DataView" /> object.</summary>
		/// <param name="table">The source <see cref="T:System.Data.DataTable" /> from which the LINQ-enabled <see cref="T:System.Data.DataView" /> is created.</param>
		/// <returns>A LINQ-enabled <see cref="T:System.Data.DataView" /> object.</returns>
		// Token: 0x0600002C RID: 44 RVA: 0x00002A04 File Offset: 0x00000C04
		public static DataView AsDataView(this DataTable table)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Creates and returns a LINQ-enabled <see cref="T:System.Data.DataView" /> object representing the LINQ to DataSet query.</summary>
		/// <param name="source">The source LINQ to DataSet query from which the LINQ-enabled <see cref="T:System.Data.DataView" /> is created.</param>
		/// <typeparam name="T">The type of objects in the source sequence, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <returns>A LINQ-enabled <see cref="T:System.Data.DataView" /> object.</returns>
		// Token: 0x0600002D RID: 45 RVA: 0x00002A04 File Offset: 0x00000C04
		public static DataView AsDataView<T>(this EnumerableRowCollection<T> source) where T : DataRow
		{
			throw new PlatformNotSupportedException();
		}
	}
}
