using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.SqlClient
{
	/// <summary>Represents a collection of parameters associated with a <see cref="T:System.Data.SqlClient.SqlCommand" /> and their respective mappings to columns in a <see cref="T:System.Data.DataSet" />. This class cannot be inherited.</summary>
	// Token: 0x0200021C RID: 540
	public sealed class SqlParameterCollection : DbParameterCollection, ICollection, IEnumerable, IList, IDataParameterCollection
	{
		// Token: 0x06001A36 RID: 6710 RVA: 0x0005AE10 File Offset: 0x00059010
		internal SqlParameterCollection()
		{
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x00079BBF File Offset: 0x00077DBF
		// (set) Token: 0x06001A38 RID: 6712 RVA: 0x00079BC7 File Offset: 0x00077DC7
		internal bool IsDirty
		{
			get
			{
				return this._isDirty;
			}
			set
			{
				this._isDirty = value;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x00079BD0 File Offset: 0x00077DD0
		public override bool IsFixedSize
		{
			get
			{
				return ((IList)this.InnerList).IsFixedSize;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x00079BDD File Offset: 0x00077DDD
		public override bool IsReadOnly
		{
			get
			{
				return ((IList)this.InnerList).IsReadOnly;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlParameter" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the parameter to retrieve.</param>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlParameter" /> at the specified index.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The specified index does not exist.</exception>
		// Token: 0x170004EE RID: 1262
		public SqlParameter this[int index]
		{
			get
			{
				return (SqlParameter)this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, value);
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlParameter" /> with the specified name.</summary>
		/// <param name="parameterName">The name of the parameter to retrieve.</param>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlParameter" /> with the specified name.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The specified <paramref name="parameterName" /> is not valid.</exception>
		// Token: 0x170004EF RID: 1263
		public SqlParameter this[string parameterName]
		{
			get
			{
				return (SqlParameter)this.GetParameter(parameterName);
			}
			set
			{
				this.SetParameter(parameterName, value);
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> object to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Data.SqlClient.SqlParameter" /> to add to the collection.</param>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.SqlClient.SqlParameter" /> specified in the <paramref name="value" /> parameter is already added to this or another <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The parameter passed was not a <see cref="T:System.Data.SqlClient.SqlParameter" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		// Token: 0x06001A3F RID: 6719 RVA: 0x00079C1A File Offset: 0x00077E1A
		public SqlParameter Add(SqlParameter value)
		{
			this.Add(value);
			return value;
		}

		/// <summary>Adds a value to the end of the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="value">The value to be added. Use <see cref="F:System.DBNull.Value" /> instead of null, to indicate a null value.</param>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		// Token: 0x06001A40 RID: 6720 RVA: 0x00079C25 File Offset: 0x00077E25
		public SqlParameter AddWithValue(string parameterName, object value)
		{
			return this.Add(new SqlParameter(parameterName, value));
		}

		/// <summary>Adds a <see cref="T:System.Data.SqlClient.SqlParameter" /> to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> given the parameter name and the data type.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="sqlDbType">One of the <see cref="T:System.Data.SqlDbType" /> values.</param>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		// Token: 0x06001A41 RID: 6721 RVA: 0x00079C34 File Offset: 0x00077E34
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType));
		}

		/// <summary>Adds a <see cref="T:System.Data.SqlClient.SqlParameter" /> to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />, given the specified parameter name, <see cref="T:System.Data.SqlDbType" /> and size.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="sqlDbType">The <see cref="T:System.Data.SqlDbType" /> of the <see cref="T:System.Data.SqlClient.SqlParameter" /> to add to the collection.</param>
		/// <param name="size">The size as an <see cref="T:System.Int32" />.</param>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		// Token: 0x06001A42 RID: 6722 RVA: 0x00079C43 File Offset: 0x00077E43
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType, int size)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType, size));
		}

		/// <summary>Adds a <see cref="T:System.Data.SqlClient.SqlParameter" /> to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> with the parameter name, the data type, and the column length.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="sqlDbType">One of the <see cref="T:System.Data.SqlDbType" /> values.</param>
		/// <param name="size">The column length.</param>
		/// <param name="sourceColumn">The name of the source column (<see cref="P:System.Data.SqlClient.SqlParameter.SourceColumn" />) if this <see cref="T:System.Data.SqlClient.SqlParameter" /> is used in a call to <see cref="Overload:System.Data.Common.DbDataAdapter.Update" />.</param>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		// Token: 0x06001A43 RID: 6723 RVA: 0x00079C53 File Offset: 0x00077E53
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType, int size, string sourceColumn)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType, size, sourceColumn));
		}

		/// <summary>Adds an array of <see cref="T:System.Data.SqlClient.SqlParameter" /> values to the end of the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <param name="values">The <see cref="T:System.Data.SqlClient.SqlParameter" /> values to add.</param>
		// Token: 0x06001A44 RID: 6724 RVA: 0x00079C65 File Offset: 0x00077E65
		public void AddRange(SqlParameter[] values)
		{
			this.AddRange(values);
		}

		/// <summary>Determines whether the specified parameter name is in this <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.String" /> value.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> contains the value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001A45 RID: 6725 RVA: 0x00079C6E File Offset: 0x00077E6E
		public override bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> is in this <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Data.SqlClient.SqlParameter" /> value.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> contains the value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001A46 RID: 6726 RVA: 0x00079C7D File Offset: 0x00077E7D
		public bool Contains(SqlParameter value)
		{
			return -1 != this.IndexOf(value);
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> to the specified <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> starting at the specified destination index.</summary>
		/// <param name="array">The <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> that is the destination of the elements copied from the current <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</param>
		/// <param name="index">A 32-bit integer that represents the index in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> at which copying starts.</param>
		// Token: 0x06001A47 RID: 6727 RVA: 0x00079C8C File Offset: 0x00077E8C
		public void CopyTo(SqlParameter[] array, int index)
		{
			this.CopyTo(array, index);
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> within the collection.</summary>
		/// <param name="value">The <see cref="T:System.Data.SqlClient.SqlParameter" /> to find.</param>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> that is a <see cref="T:System.Data.SqlClient.SqlParameter" /> within the collection. Returns -1 when the object does not exist in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</returns>
		// Token: 0x06001A48 RID: 6728 RVA: 0x00079C96 File Offset: 0x00077E96
		public int IndexOf(SqlParameter value)
		{
			return this.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.Data.SqlClient.SqlParameter" /> object into the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">A <see cref="T:System.Data.SqlClient.SqlParameter" /> object to be inserted in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</param>
		// Token: 0x06001A49 RID: 6729 RVA: 0x00079C9F File Offset: 0x00077E9F
		public void Insert(int index, SqlParameter value)
		{
			this.Insert(index, value);
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00079CA9 File Offset: 0x00077EA9
		private void OnChange()
		{
			this.IsDirty = true;
		}

		/// <summary>Removes the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> from the collection.</summary>
		/// <param name="value">A <see cref="T:System.Data.SqlClient.SqlParameter" /> object to remove from the collection.</param>
		/// <exception cref="T:System.InvalidCastException">The parameter is not a <see cref="T:System.Data.SqlClient.SqlParameter" />.</exception>
		/// <exception cref="T:System.SystemException">The parameter does not exist in the collection.</exception>
		// Token: 0x06001A4B RID: 6731 RVA: 0x00079CB2 File Offset: 0x00077EB2
		public void Remove(SqlParameter value)
		{
			this.Remove(value);
		}

		/// <summary>Returns an Integer that contains the number of elements in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />. Read-only.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> as an Integer.</returns>
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x00079CBB File Offset: 0x00077EBB
		public override int Count
		{
			get
			{
				if (this._items == null)
				{
					return 0;
				}
				return this._items.Count;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x00079CD4 File Offset: 0x00077ED4
		private List<SqlParameter> InnerList
		{
			get
			{
				List<SqlParameter> list = this._items;
				if (list == null)
				{
					list = new List<SqlParameter>();
					this._items = list;
				}
				return list;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</returns>
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x00079CF9 File Offset: 0x00077EF9
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.InnerList).SyncRoot;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> object to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <param name="value">An <see cref="T:System.Object" />.</param>
		/// <returns>The index of the new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		// Token: 0x06001A4F RID: 6735 RVA: 0x00079D06 File Offset: 0x00077F06
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, value);
			this.InnerList.Add((SqlParameter)value);
			return this.Count - 1;
		}

		/// <summary>Adds an array of values to the end of the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <param name="values">The <see cref="T:System.Array" /> values to add.</param>
		// Token: 0x06001A50 RID: 6736 RVA: 0x00079D38 File Offset: 0x00077F38
		public override void AddRange(Array values)
		{
			this.OnChange();
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			foreach (object value in values)
			{
				this.ValidateType(value);
			}
			foreach (object obj in values)
			{
				SqlParameter sqlParameter = (SqlParameter)obj;
				this.Validate(-1, sqlParameter);
				this.InnerList.Add(sqlParameter);
			}
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00079DEC File Offset: 0x00077FEC
		private int CheckName(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, SqlParameterCollection.s_itemType);
			}
			return num;
		}

		/// <summary>Removes all the <see cref="T:System.Data.SqlClient.SqlParameter" /> objects from the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		// Token: 0x06001A52 RID: 6738 RVA: 0x00079E08 File Offset: 0x00078008
		public override void Clear()
		{
			this.OnChange();
			List<SqlParameter> innerList = this.InnerList;
			if (innerList != null)
			{
				foreach (SqlParameter sqlParameter in innerList)
				{
					sqlParameter.ResetParent();
				}
				innerList.Clear();
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is in this <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> value.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> contains the value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001A53 RID: 6739 RVA: 0x00079E6C File Offset: 0x0007806C
		public override bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> to the specified one-dimensional <see cref="T:System.Array" /> starting at the specified destination <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</param>
		/// <param name="index">A 32-bit integer that represents the index in the <see cref="T:System.Array" /> at which copying starts.</param>
		// Token: 0x06001A54 RID: 6740 RVA: 0x00079E7B File Offset: 0x0007807B
		public override void CopyTo(Array array, int index)
		{
			((ICollection)this.InnerList).CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</returns>
		// Token: 0x06001A55 RID: 6741 RVA: 0x00079E8A File Offset: 0x0007808A
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this.InnerList).GetEnumerator();
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x00079E97 File Offset: 0x00078097
		protected override DbParameter GetParameter(int index)
		{
			this.RangeCheck(index);
			return this.InnerList[index];
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x00079EAC File Offset: 0x000780AC
		protected override DbParameter GetParameter(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, SqlParameterCollection.s_itemType);
			}
			return this.InnerList[num];
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00079EE0 File Offset: 0x000780E0
		private static int IndexOf(IEnumerable items, string parameterName)
		{
			if (items != null)
			{
				int num = 0;
				foreach (object obj in items)
				{
					SqlParameter sqlParameter = (SqlParameter)obj;
					if (parameterName == sqlParameter.ParameterName)
					{
						return num;
					}
					num++;
				}
				num = 0;
				foreach (object obj2 in items)
				{
					SqlParameter sqlParameter2 = (SqlParameter)obj2;
					if (ADP.DstCompare(parameterName, sqlParameter2.ParameterName) == 0)
					{
						return num;
					}
					num++;
				}
				return -1;
			}
			return -1;
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> with the specified name.</summary>
		/// <param name="parameterName">The case-sensitive name of the <see cref="T:System.Data.SqlClient.SqlParameter" /> to find.</param>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> with the specified case-sensitive name. Returns -1 when the object does not exist in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</returns>
		// Token: 0x06001A59 RID: 6745 RVA: 0x00079FAC File Offset: 0x000781AC
		public override int IndexOf(string parameterName)
		{
			return SqlParameterCollection.IndexOf(this.InnerList, parameterName);
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Object" /> within the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to find.</param>
		/// <returns>The zero-based location of the specified <see cref="T:System.Object" /> that is a <see cref="T:System.Data.SqlClient.SqlParameter" /> within the collection. Returns -1 when the object does not exist in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</returns>
		// Token: 0x06001A5A RID: 6746 RVA: 0x00079FBC File Offset: 0x000781BC
		public override int IndexOf(object value)
		{
			if (value != null)
			{
				this.ValidateType(value);
				List<SqlParameter> innerList = this.InnerList;
				if (innerList != null)
				{
					int count = innerList.Count;
					for (int i = 0; i < count; i++)
					{
						if (value == innerList[i])
						{
							return i;
						}
					}
				}
			}
			return -1;
		}

		/// <summary>Inserts an <see cref="T:System.Object" /> into the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">An <see cref="T:System.Object" /> to be inserted in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</param>
		// Token: 0x06001A5B RID: 6747 RVA: 0x00079FFD File Offset: 0x000781FD
		public override void Insert(int index, object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, (SqlParameter)value);
			this.InnerList.Insert(index, (SqlParameter)value);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x0007A02B File Offset: 0x0007822B
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.ParametersMappingIndex(index, this);
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> from the collection.</summary>
		/// <param name="value">The object to remove from the collection.</param>
		// Token: 0x06001A5D RID: 6749 RVA: 0x0007A044 File Offset: 0x00078244
		public override void Remove(object value)
		{
			this.OnChange();
			this.ValidateType(value);
			int num = this.IndexOf(value);
			if (-1 != num)
			{
				this.RemoveIndex(num);
				return;
			}
			if (this != ((SqlParameter)value).CompareExchangeParent(null, this))
			{
				throw ADP.CollectionRemoveInvalidObject(SqlParameterCollection.s_itemType, this);
			}
		}

		/// <summary>Removes the <see cref="T:System.Data.SqlClient.SqlParameter" /> from the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.SqlClient.SqlParameter" /> object to remove.</param>
		// Token: 0x06001A5E RID: 6750 RVA: 0x0007A08E File Offset: 0x0007828E
		public override void RemoveAt(int index)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		/// <summary>Removes the <see cref="T:System.Data.SqlClient.SqlParameter" /> from the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> at the specified parameter name.</summary>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.SqlClient.SqlParameter" /> to remove.</param>
		// Token: 0x06001A5F RID: 6751 RVA: 0x0007A0A4 File Offset: 0x000782A4
		public override void RemoveAt(string parameterName)
		{
			this.OnChange();
			int index = this.CheckName(parameterName);
			this.RemoveIndex(index);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x0007A0C8 File Offset: 0x000782C8
		private void RemoveIndex(int index)
		{
			List<SqlParameter> innerList = this.InnerList;
			SqlParameter sqlParameter = innerList[index];
			innerList.RemoveAt(index);
			sqlParameter.ResetParent();
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x0007A0F0 File Offset: 0x000782F0
		private void Replace(int index, object newValue)
		{
			List<SqlParameter> innerList = this.InnerList;
			this.ValidateType(newValue);
			this.Validate(index, newValue);
			SqlParameter sqlParameter = innerList[index];
			innerList[index] = (SqlParameter)newValue;
			sqlParameter.ResetParent();
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x0007A12C File Offset: 0x0007832C
		protected override void SetParameter(int index, DbParameter value)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.Replace(index, value);
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0007A144 File Offset: 0x00078344
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			this.OnChange();
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, SqlParameterCollection.s_itemType);
			}
			this.Replace(num, value);
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0007A178 File Offset: 0x00078378
		private void Validate(int index, object value)
		{
			if (value == null)
			{
				throw ADP.ParameterNull("value", this, SqlParameterCollection.s_itemType);
			}
			object obj = ((SqlParameter)value).CompareExchangeParent(this, null);
			if (obj != null)
			{
				if (this != obj)
				{
					throw ADP.ParametersIsNotParent(SqlParameterCollection.s_itemType, this);
				}
				if (index != this.IndexOf(value))
				{
					throw ADP.ParametersIsParent(SqlParameterCollection.s_itemType, this);
				}
			}
			string text = ((SqlParameter)value).ParameterName;
			if (text.Length == 0)
			{
				index = 1;
				do
				{
					text = "Parameter" + index.ToString(CultureInfo.CurrentCulture);
					index++;
				}
				while (-1 != this.IndexOf(text));
				((SqlParameter)value).ParameterName = text;
			}
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x0007A219 File Offset: 0x00078419
		private void ValidateType(object value)
		{
			if (value == null)
			{
				throw ADP.ParameterNull("value", this, SqlParameterCollection.s_itemType);
			}
			if (!SqlParameterCollection.s_itemType.IsInstanceOfType(value))
			{
				throw ADP.InvalidParameterType(this, SqlParameterCollection.s_itemType, value);
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> object to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.SqlClient.SqlParameter" /> to add to the collection.</param>
		/// <param name="value">A <see cref="T:System.Object" />.</param>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.  
		///  Use caution when you are using this overload of the <see langword="SqlParameterCollection.Add" /> method to specify integer parameter values. Because this overload takes a <paramref name="value" /> of type <see cref="T:System.Object" />, you must convert the integral value to an <see cref="T:System.Object" /> type when the value is zero, as the following C# example demonstrates.  
		/// parameters.Add("@pname", Convert.ToInt32(0));  
		///  If you do not perform this conversion, the compiler assumes that you are trying to call the <see langword="SqlParameterCollection.Add" /> (<see langword="string" />, <see langword="SqlDbType" />) overload.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.SqlClient.SqlParameter" /> specified in the <paramref name="value" /> parameter is already added to this or another <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		// Token: 0x06001A66 RID: 6758 RVA: 0x00079C25 File Offset: 0x00077E25
		public SqlParameter Add(string parameterName, object value)
		{
			return this.Add(new SqlParameter(parameterName, value));
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x0007A249 File Offset: 0x00078449
		// Note: this type is marked as 'beforefieldinit'.
		static SqlParameterCollection()
		{
		}

		// Token: 0x040010EF RID: 4335
		private bool _isDirty;

		// Token: 0x040010F0 RID: 4336
		private static Type s_itemType = typeof(SqlParameter);

		// Token: 0x040010F1 RID: 4337
		private List<SqlParameter> _items;
	}
}
