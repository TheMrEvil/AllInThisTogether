using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.Odbc
{
	/// <summary>Represents a collection of parameters relevant to an <see cref="T:System.Data.Odbc.OdbcCommand" /> and their respective mappings to columns in a <see cref="T:System.Data.DataSet" />. This class cannot be inherited.</summary>
	// Token: 0x020002F4 RID: 756
	public sealed class OdbcParameterCollection : DbParameterCollection
	{
		// Token: 0x06002193 RID: 8595 RVA: 0x0005AE10 File Offset: 0x00059010
		internal OdbcParameterCollection()
		{
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x0009D57D File Offset: 0x0009B77D
		// (set) Token: 0x06002195 RID: 8597 RVA: 0x0009D585 File Offset: 0x0009B785
		internal bool RebindCollection
		{
			get
			{
				return this._rebindCollection;
			}
			set
			{
				this._rebindCollection = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Odbc.OdbcParameter" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the parameter to retrieve.</param>
		/// <returns>The <see cref="T:System.Data.Odbc.OdbcParameter" /> at the specified index.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index specified does not exist.</exception>
		// Token: 0x17000604 RID: 1540
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public OdbcParameter this[int index]
		{
			get
			{
				return (OdbcParameter)this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, value);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Odbc.OdbcParameter" /> with the specified name.</summary>
		/// <param name="parameterName">The name of the parameter to retrieve.</param>
		/// <returns>The <see cref="T:System.Data.Odbc.OdbcParameter" /> with the specified name.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The name specified does not exist.</exception>
		// Token: 0x17000605 RID: 1541
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public OdbcParameter this[string parameterName]
		{
			get
			{
				return (OdbcParameter)this.GetParameter(parameterName);
			}
			set
			{
				this.SetParameter(parameterName, value);
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.Odbc.OdbcParameter" /> to the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Data.Odbc.OdbcParameter" /> to add to the collection.</param>
		/// <returns>The index of the new <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.Odbc.OdbcParameter" /> specified in the <paramref name="value" /> parameter is already added to this or another <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		// Token: 0x0600219A RID: 8602 RVA: 0x00079C1A File Offset: 0x00077E1A
		public OdbcParameter Add(OdbcParameter value)
		{
			this.Add(value);
			return value;
		}

		/// <summary>Adds an <see cref="T:System.Data.Odbc.OdbcParameter" /> to the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> given the parameter name and value.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="value">The <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> of the <see cref="T:System.Data.Odbc.OdbcParameter" /> to add to the collection.</param>
		/// <returns>The index of the new <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</returns>
		/// <exception cref="T:System.InvalidCastException">The <paramref name="value" /> parameter is not an <see cref="T:System.Data.Odbc.OdbcParameter" />.</exception>
		// Token: 0x0600219B RID: 8603 RVA: 0x0009D5AA File Offset: 0x0009B7AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Add(String parameterName, Object value) has been deprecated.  Use AddWithValue(String parameterName, Object value).  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		public OdbcParameter Add(string parameterName, object value)
		{
			return this.Add(new OdbcParameter(parameterName, value));
		}

		/// <summary>Adds a value to the end of the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="value">The value to be added.</param>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</returns>
		// Token: 0x0600219C RID: 8604 RVA: 0x0009D5AA File Offset: 0x0009B7AA
		public OdbcParameter AddWithValue(string parameterName, object value)
		{
			return this.Add(new OdbcParameter(parameterName, value));
		}

		/// <summary>Adds an <see cref="T:System.Data.Odbc.OdbcParameter" /> to the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />, given the parameter name and data type.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="odbcType">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values.</param>
		/// <returns>The index of the new <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</returns>
		// Token: 0x0600219D RID: 8605 RVA: 0x0009D5B9 File Offset: 0x0009B7B9
		public OdbcParameter Add(string parameterName, OdbcType odbcType)
		{
			return this.Add(new OdbcParameter(parameterName, odbcType));
		}

		/// <summary>Adds an <see cref="T:System.Data.Odbc.OdbcParameter" /> to the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />, given the parameter name, data type, and column length.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="odbcType">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values.</param>
		/// <param name="size">The length of the column.</param>
		/// <returns>The index of the new <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</returns>
		// Token: 0x0600219E RID: 8606 RVA: 0x0009D5C8 File Offset: 0x0009B7C8
		public OdbcParameter Add(string parameterName, OdbcType odbcType, int size)
		{
			return this.Add(new OdbcParameter(parameterName, odbcType, size));
		}

		/// <summary>Adds an <see cref="T:System.Data.Odbc.OdbcParameter" /> to the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> given the parameter name, data type, column length, and source column name.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="odbcType">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values.</param>
		/// <param name="size">The length of the column.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		/// <returns>The index of the new <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</returns>
		// Token: 0x0600219F RID: 8607 RVA: 0x0009D5D8 File Offset: 0x0009B7D8
		public OdbcParameter Add(string parameterName, OdbcType odbcType, int size, string sourceColumn)
		{
			return this.Add(new OdbcParameter(parameterName, odbcType, size, sourceColumn));
		}

		/// <summary>Adds an array of <see cref="T:System.Data.Odbc.OdbcParameter" /> values to the end of the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <param name="values">An array of <see cref="T:System.Data.Odbc.OdbcParameter" /> objects to add to the collection.</param>
		// Token: 0x060021A0 RID: 8608 RVA: 0x00079C65 File Offset: 0x00077E65
		public void AddRange(OdbcParameter[] values)
		{
			this.AddRange(values);
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x0009D5EC File Offset: 0x0009B7EC
		internal void Bind(OdbcCommand command, CMDWrapper cmdWrapper, CNativeBuffer parameterBuffer)
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].Bind(cmdWrapper.StatementHandle, command, checked((short)(i + 1)), parameterBuffer, true);
			}
			this._rebindCollection = false;
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x0009D62C File Offset: 0x0009B82C
		internal int CalcParameterBufferSize(OdbcCommand command)
		{
			int num = 0;
			for (int i = 0; i < this.Count; i++)
			{
				if (this._rebindCollection)
				{
					this[i].HasChanged = true;
				}
				this[i].PrepareForBind(command, (short)(i + 1), ref num);
				num = (num + (IntPtr.Size - 1) & ~(IntPtr.Size - 1));
			}
			return num;
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x0009D688 File Offset: 0x0009B888
		internal void ClearBindings()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].ClearBinding();
			}
		}

		/// <summary>Gets a value indicating whether an <see cref="T:System.Data.Odbc.OdbcParameter" /> object with the specified parameter name exists in the collection.</summary>
		/// <param name="value">The name of the <see cref="T:System.Data.Odbc.OdbcParameter" /> object to find.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060021A4 RID: 8612 RVA: 0x00079C6E File Offset: 0x00077E6E
		public override bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Data.Odbc.OdbcParameter" /> is in this <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Data.Odbc.OdbcParameter" /> value.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.Odbc.OdbcParameter" /> is in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x060021A5 RID: 8613 RVA: 0x0009D6B2 File Offset: 0x0009B8B2
		public bool Contains(OdbcParameter value)
		{
			return -1 != this.IndexOf(value);
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> to the specified <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> starting at the specified destination index.</summary>
		/// <param name="array">The <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> that is the destination of the elements copied from the current <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</param>
		/// <param name="index">A 32-bit integer that represents the index in the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> at which copying starts.</param>
		// Token: 0x060021A6 RID: 8614 RVA: 0x00079C8C File Offset: 0x00077E8C
		public void CopyTo(OdbcParameter[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x0009D6C1 File Offset: 0x0009B8C1
		private void OnChange()
		{
			this._rebindCollection = true;
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x0009D6CC File Offset: 0x0009B8CC
		internal void GetOutputValues(CMDWrapper cmdWrapper)
		{
			if (!this._rebindCollection)
			{
				CNativeBuffer nativeParameterBuffer = cmdWrapper._nativeParameterBuffer;
				for (int i = 0; i < this.Count; i++)
				{
					this[i].GetOutputValue(nativeParameterBuffer);
				}
			}
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.Odbc.OdbcParameter" /> within the collection.</summary>
		/// <param name="value">The <see cref="T:System.Data.Odbc.OdbcParameter" /> object in the collection to find.</param>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.Odbc.OdbcParameter" /> within the collection.</returns>
		// Token: 0x060021A9 RID: 8617 RVA: 0x00079C96 File Offset: 0x00077E96
		public int IndexOf(OdbcParameter value)
		{
			return this.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.Data.Odbc.OdbcParameter" /> object into the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which the object should be inserted.</param>
		/// <param name="value">A <see cref="T:System.Data.Odbc.OdbcParameter" /> object to be inserted in the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</param>
		// Token: 0x060021AA RID: 8618 RVA: 0x00079C9F File Offset: 0x00077E9F
		public void Insert(int index, OdbcParameter value)
		{
			this.Insert(index, value);
		}

		/// <summary>Removes the <see cref="T:System.Data.Odbc.OdbcParameter" /> from the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <param name="value">A <see cref="T:System.Data.Odbc.OdbcParameter" /> object to remove from the collection.</param>
		/// <exception cref="T:System.InvalidCastException">The parameter is not a <see cref="T:System.Data.Odbc.OdbcParameter" />.</exception>
		/// <exception cref="T:System.SystemException">The parameter does not exist in the collection.</exception>
		// Token: 0x060021AB RID: 8619 RVA: 0x00079CB2 File Offset: 0x00077EB2
		public void Remove(OdbcParameter value)
		{
			this.Remove(value);
		}

		/// <summary>Returns an Integer that contains the number of elements in the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />. Read-only.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> as an Integer.</returns>
		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x0009D706 File Offset: 0x0009B906
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

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x0009D720 File Offset: 0x0009B920
		private List<OdbcParameter> InnerList
		{
			get
			{
				List<OdbcParameter> list = this._items;
				if (list == null)
				{
					list = new List<OdbcParameter>();
					this._items = list;
				}
				return list;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> has a fixed size. Read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x0009D745 File Offset: 0x0009B945
		public override bool IsFixedSize
		{
			get
			{
				return ((IList)this.InnerList).IsFixedSize;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> is read only, otherwise, <see langword="false" />.</returns>
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x0009D752 File Offset: 0x0009B952
		public override bool IsReadOnly
		{
			get
			{
				return ((IList)this.InnerList).IsReadOnly;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> is synchronized. Read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> is synchronized; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x0009D75F File Offset: 0x0009B95F
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.InnerList).IsSynchronized;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />. Read-only.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</returns>
		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x0009D76C File Offset: 0x0009B96C
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.InnerList).SyncRoot;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.Odbc.OdbcParameter" /> object to the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <param name="value">A <see cref="T:System.Object" />.</param>
		/// <returns>The index of the new <see cref="T:System.Data.Odbc.OdbcParameter" /> object in the collection.</returns>
		// Token: 0x060021B2 RID: 8626 RVA: 0x0009D779 File Offset: 0x0009B979
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, value);
			this.InnerList.Add((OdbcParameter)value);
			return this.Count - 1;
		}

		/// <summary>Adds an array of values to the end of the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <param name="values">The <see cref="T:System.Array" /> values to add.</param>
		// Token: 0x060021B3 RID: 8627 RVA: 0x0009D7AC File Offset: 0x0009B9AC
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
				OdbcParameter odbcParameter = (OdbcParameter)obj;
				this.Validate(-1, odbcParameter);
				this.InnerList.Add(odbcParameter);
			}
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x0009D860 File Offset: 0x0009BA60
		private int CheckName(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, OdbcParameterCollection.s_itemType);
			}
			return num;
		}

		/// <summary>Removes all <see cref="T:System.Data.Odbc.OdbcParameter" /> objects from the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		// Token: 0x060021B5 RID: 8629 RVA: 0x0009D87C File Offset: 0x0009BA7C
		public override void Clear()
		{
			this.OnChange();
			List<OdbcParameter> innerList = this.InnerList;
			if (innerList != null)
			{
				foreach (OdbcParameter odbcParameter in innerList)
				{
					odbcParameter.ResetParent();
				}
				innerList.Clear();
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is in this <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> value.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> contains the value; otherwise, <see langword="false" />.</returns>
		// Token: 0x060021B6 RID: 8630 RVA: 0x00079E6C File Offset: 0x0007806C
		public override bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> to the specified one-dimensional <see cref="T:System.Array" /> starting at the specified destination <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</param>
		/// <param name="index">A 32-bit integer that represents the index in the <see cref="T:System.Array" /> at which copying starts.</param>
		// Token: 0x060021B7 RID: 8631 RVA: 0x0009D8E0 File Offset: 0x0009BAE0
		public override void CopyTo(Array array, int index)
		{
			((ICollection)this.InnerList).CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</returns>
		// Token: 0x060021B8 RID: 8632 RVA: 0x0009D8EF File Offset: 0x0009BAEF
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this.InnerList).GetEnumerator();
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0009D8FC File Offset: 0x0009BAFC
		protected override DbParameter GetParameter(int index)
		{
			this.RangeCheck(index);
			return this.InnerList[index];
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x0009D914 File Offset: 0x0009BB14
		protected override DbParameter GetParameter(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, OdbcParameterCollection.s_itemType);
			}
			return this.InnerList[num];
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x0009D948 File Offset: 0x0009BB48
		private static int IndexOf(IEnumerable items, string parameterName)
		{
			if (items != null)
			{
				int num = 0;
				foreach (object obj in items)
				{
					OdbcParameter odbcParameter = (OdbcParameter)obj;
					if (parameterName == odbcParameter.ParameterName)
					{
						return num;
					}
					num++;
				}
				num = 0;
				foreach (object obj2 in items)
				{
					OdbcParameter odbcParameter2 = (OdbcParameter)obj2;
					if (ADP.DstCompare(parameterName, odbcParameter2.ParameterName) == 0)
					{
						return num;
					}
					num++;
				}
				return -1;
			}
			return -1;
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.Odbc.OdbcParameter" /> with the specified name.</summary>
		/// <param name="parameterName">The case-sensitive name of the <see cref="T:System.Data.Odbc.OdbcParameter" /> to find.</param>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.Odbc.OdbcParameter" /> with the specified case-sensitive name.</returns>
		// Token: 0x060021BC RID: 8636 RVA: 0x0009DA14 File Offset: 0x0009BC14
		public override int IndexOf(string parameterName)
		{
			return OdbcParameterCollection.IndexOf(this.InnerList, parameterName);
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Object" /> within the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to find.</param>
		/// <returns>The zero-based location of the specified <see cref="T:System.Object" /> that is a <see cref="T:System.Data.Odbc.OdbcParameter" /> within the collection.</returns>
		// Token: 0x060021BD RID: 8637 RVA: 0x0009DA24 File Offset: 0x0009BC24
		public override int IndexOf(object value)
		{
			if (value != null)
			{
				this.ValidateType(value);
				List<OdbcParameter> innerList = this.InnerList;
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

		/// <summary>Inserts a <see cref="T:System.Object" /> into the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which the object should be inserted.</param>
		/// <param name="value">A <see cref="T:System.Object" /> to be inserted in the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</param>
		// Token: 0x060021BE RID: 8638 RVA: 0x0009DA65 File Offset: 0x0009BC65
		public override void Insert(int index, object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, (OdbcParameter)value);
			this.InnerList.Insert(index, (OdbcParameter)value);
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x0007A02B File Offset: 0x0007822B
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.ParametersMappingIndex(index, this);
			}
		}

		/// <summary>Removes the <see cref="T:System.Object" /> object from the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <param name="value">A <see cref="T:System.Object" /> to be removed from the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</param>
		// Token: 0x060021C0 RID: 8640 RVA: 0x0009DA94 File Offset: 0x0009BC94
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
			if (this != ((OdbcParameter)value).CompareExchangeParent(null, this))
			{
				throw ADP.CollectionRemoveInvalidObject(OdbcParameterCollection.s_itemType, this);
			}
		}

		/// <summary>Removes the <see cref="T:System.Data.Odbc.OdbcParameter" /> from the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Odbc.OdbcParameter" /> object to remove.</param>
		// Token: 0x060021C1 RID: 8641 RVA: 0x0009DADE File Offset: 0x0009BCDE
		public override void RemoveAt(int index)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		/// <summary>Removes the <see cref="T:System.Data.Odbc.OdbcParameter" /> from the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> with the specified parameter name.</summary>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.Odbc.OdbcParameter" /> object to remove.</param>
		// Token: 0x060021C2 RID: 8642 RVA: 0x0009DAF4 File Offset: 0x0009BCF4
		public override void RemoveAt(string parameterName)
		{
			this.OnChange();
			int index = this.CheckName(parameterName);
			this.RemoveIndex(index);
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x0009DB18 File Offset: 0x0009BD18
		private void RemoveIndex(int index)
		{
			List<OdbcParameter> innerList = this.InnerList;
			OdbcParameter odbcParameter = innerList[index];
			innerList.RemoveAt(index);
			odbcParameter.ResetParent();
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x0009DB40 File Offset: 0x0009BD40
		private void Replace(int index, object newValue)
		{
			List<OdbcParameter> innerList = this.InnerList;
			this.ValidateType(newValue);
			this.Validate(index, newValue);
			OdbcParameter odbcParameter = innerList[index];
			innerList[index] = (OdbcParameter)newValue;
			odbcParameter.ResetParent();
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x0009DB7C File Offset: 0x0009BD7C
		protected override void SetParameter(int index, DbParameter value)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.Replace(index, value);
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x0009DB94 File Offset: 0x0009BD94
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			this.OnChange();
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, OdbcParameterCollection.s_itemType);
			}
			this.Replace(num, value);
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x0009DBC8 File Offset: 0x0009BDC8
		private void Validate(int index, object value)
		{
			if (value == null)
			{
				throw ADP.ParameterNull("value", this, OdbcParameterCollection.s_itemType);
			}
			object obj = ((OdbcParameter)value).CompareExchangeParent(this, null);
			if (obj != null)
			{
				if (this != obj)
				{
					throw ADP.ParametersIsNotParent(OdbcParameterCollection.s_itemType, this);
				}
				if (index != this.IndexOf(value))
				{
					throw ADP.ParametersIsParent(OdbcParameterCollection.s_itemType, this);
				}
			}
			string text = ((OdbcParameter)value).ParameterName;
			if (text.Length == 0)
			{
				index = 1;
				do
				{
					text = "Parameter" + index.ToString(CultureInfo.CurrentCulture);
					index++;
				}
				while (-1 != this.IndexOf(text));
				((OdbcParameter)value).ParameterName = text;
			}
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x0009DC69 File Offset: 0x0009BE69
		private void ValidateType(object value)
		{
			if (value == null)
			{
				throw ADP.ParameterNull("value", this, OdbcParameterCollection.s_itemType);
			}
			if (!OdbcParameterCollection.s_itemType.IsInstanceOfType(value))
			{
				throw ADP.InvalidParameterType(this, OdbcParameterCollection.s_itemType, value);
			}
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x0009DC99 File Offset: 0x0009BE99
		// Note: this type is marked as 'beforefieldinit'.
		static OdbcParameterCollection()
		{
		}

		// Token: 0x040017F9 RID: 6137
		private bool _rebindCollection;

		// Token: 0x040017FA RID: 6138
		private static Type s_itemType = typeof(OdbcParameter);

		// Token: 0x040017FB RID: 6139
		private List<OdbcParameter> _items;
	}
}
