using System;
using System.Collections.Generic;

namespace System.Data
{
	/// <summary>Compares two <see cref="T:System.Data.DataRow" /> objects for equivalence by using value-based comparison.</summary>
	/// <typeparam name="TRow">The type of objects to be compared, typically <see cref="T:System.Data.DataRow" />.</typeparam>
	// Token: 0x02000006 RID: 6
	public sealed class DataRowComparer<TRow> : IEqualityComparer<TRow> where TRow : DataRow
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000021DF File Offset: 0x000003DF
		private DataRowComparer()
		{
		}

		/// <summary>Gets a singleton instance of <see cref="T:System.Data.DataRowComparer`1" />. This property is read-only.</summary>
		/// <returns>An instance of a <see cref="T:System.Data.DataRowComparer`1" />.</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000023D3 File Offset: 0x000005D3
		public static DataRowComparer<TRow> Default
		{
			get
			{
				return DataRowComparer<TRow>.s_instance;
			}
		}

		/// <summary>Compares two <see cref="T:System.Data.DataRow" /> objects by using a column-by-column, value-based comparison.</summary>
		/// <param name="leftRow">The first <see cref="T:System.Data.DataRow" /> object to compare.</param>
		/// <param name="rightRow">The second <see cref="T:System.Data.DataRow" /> object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Data.DataRow" /> objects have ordered sets of column values that are equal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">One or both of the source <see cref="T:System.Data.DataRow" /> objects are <see langword="null" />.</exception>
		// Token: 0x06000017 RID: 23 RVA: 0x000023DC File Offset: 0x000005DC
		public bool Equals(TRow leftRow, TRow rightRow)
		{
			if (leftRow == rightRow)
			{
				return true;
			}
			if (leftRow == null || rightRow == null)
			{
				return false;
			}
			if (leftRow.RowState == DataRowState.Deleted || rightRow.RowState == DataRowState.Deleted)
			{
				throw DataSetUtil.InvalidOperation("The DataRowComparer does not work with DataRows that have been deleted since it only compares current values.");
			}
			int count = leftRow.Table.Columns.Count;
			if (count != rightRow.Table.Columns.Count)
			{
				return false;
			}
			for (int i = 0; i < count; i++)
			{
				if (!DataRowComparer.AreEqual(leftRow[i], rightRow[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns a hash code for the specified <see cref="T:System.Data.DataRow" /> object.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to compute the hash code from.</param>
		/// <returns>An <see cref="T:System.Int32" /> value representing the hash code of the row.</returns>
		/// <exception cref="T:System.ArgumentException">The source <see cref="T:System.Data.DataRow" /> objects does not belong to a <see cref="T:System.Data.DataTable" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The source <see cref="T:System.Data.DataRow" /> objects is <see langword="null" />.</exception>
		// Token: 0x06000018 RID: 24 RVA: 0x00002490 File Offset: 0x00000690
		public int GetHashCode(TRow row)
		{
			DataSetUtil.CheckArgumentNull<TRow>(row, "row");
			if (row.RowState == DataRowState.Deleted)
			{
				throw DataSetUtil.InvalidOperation("The DataRowComparer does not work with DataRows that have been deleted since it only compares current values.");
			}
			int result = 0;
			if (row.Table.Columns.Count > 0)
			{
				object obj = row[0];
				if (obj.GetType().IsArray)
				{
					Array array = obj as Array;
					if (array.Rank > 1)
					{
						result = obj.GetHashCode();
					}
					else if (array.Length > 0)
					{
						result = array.GetValue(array.GetLowerBound(0)).GetHashCode();
					}
				}
				else
				{
					ValueType valueType = obj as ValueType;
					if (valueType != null)
					{
						result = valueType.GetHashCode();
					}
					else
					{
						result = obj.GetHashCode();
					}
				}
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002547 File Offset: 0x00000747
		// Note: this type is marked as 'beforefieldinit'.
		static DataRowComparer()
		{
		}

		// Token: 0x04000037 RID: 55
		private static DataRowComparer<TRow> s_instance = new DataRowComparer<TRow>();
	}
}
