using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Represents a collection of parameters relevant to an <see cref="T:System.Data.OleDb.OleDbCommand" /> as well as their respective mappings to columns in a <see cref="T:System.Data.DataSet" />.</summary>
	// Token: 0x0200016E RID: 366
	[MonoTODO("OleDb is not implemented.")]
	public class OleDbParameterCollection : DbParameterCollection
	{
		// Token: 0x0600139E RID: 5022 RVA: 0x0005AE10 File Offset: 0x00059010
		internal OleDbParameterCollection()
		{
		}

		/// <summary>Adds the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Data.OleDb.OleDbParameter" /> to add to the collection.</param>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.OleDb.OleDbParameter" /> specified in the <paramref name="value" /> parameter is already added to this or another <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		// Token: 0x0600139F RID: 5023 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public OleDbParameter Add(OleDbParameter value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> object to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="value">A <see cref="T:System.Object" />.</param>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object in the collection.</returns>
		// Token: 0x060013A0 RID: 5024 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds an <see cref="T:System.Data.OleDb.OleDbParameter" /> to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />, given the parameter name and data type.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="oleDbType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values.</param>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		// Token: 0x060013A1 RID: 5025 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public OleDbParameter Add(string parameterName, OleDbType oleDbType)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds an <see cref="T:System.Data.OleDb.OleDbParameter" /> to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> given the parameter name, data type, and column length.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="oleDbType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values.</param>
		/// <param name="size">The length of the column.</param>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		// Token: 0x060013A2 RID: 5026 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public OleDbParameter Add(string parameterName, OleDbType oleDbType, int size)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds an <see cref="T:System.Data.OleDb.OleDbParameter" /> to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> given the parameter name, data type, column length, and source column name.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="oleDbType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values.</param>
		/// <param name="size">The length of the column.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		// Token: 0x060013A3 RID: 5027 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public OleDbParameter Add(string parameterName, OleDbType oleDbType, int size, string sourceColumn)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds an <see cref="T:System.Data.OleDb.OleDbParameter" /> to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> given the parameter name and value.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="value">The <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> of the <see cref="T:System.Data.OleDb.OleDbParameter" /> to add to the collection.</param>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		/// <exception cref="T:System.InvalidCastException">The <paramref name="value" /> parameter is not an <see cref="T:System.Data.OleDb.OleDbParameter" />.</exception>
		// Token: 0x060013A4 RID: 5028 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public OleDbParameter Add(string parameterName, object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds an array of values to the end of the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="values">The <see cref="T:System.Array" /> values to add.</param>
		// Token: 0x060013A5 RID: 5029 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override void AddRange(Array values)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds an array of <see cref="T:System.Data.OleDb.OleDbParameter" /> values to the end of the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="values">The <see cref="T:System.Data.OleDb.OleDbParameter" /> values to add.</param>
		// Token: 0x060013A6 RID: 5030 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public void AddRange(OleDbParameter[] values)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds a value to the end of the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="value">The value to be added.</param>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		// Token: 0x060013A7 RID: 5031 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public OleDbParameter AddWithValue(string parameterName, object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Removes all <see cref="T:System.Data.OleDb.OleDbParameter" /> objects from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		// Token: 0x060013A8 RID: 5032 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override void Clear()
		{
			throw ADP.OleDb();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> is in this <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Data.OleDb.OleDbParameter" /> value.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.OleDb.OleDbParameter" /> is in the collection; otherwise <see langword="false" />.</returns>
		// Token: 0x060013A9 RID: 5033 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public bool Contains(OleDbParameter value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is in this <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> value.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> contains <paramref name="value" />; otherwise <see langword="false" />.</returns>
		// Token: 0x060013AA RID: 5034 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override bool Contains(object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Determines whether the specified <see cref="T:System.String" /> is in this <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.String" /> value.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> contains the value; otherwise <see langword="false" />.</returns>
		// Token: 0x060013AB RID: 5035 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override bool Contains(string value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> to the specified one-dimensional <see cref="T:System.Array" /> starting at the specified destination <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</param>
		/// <param name="index">A 32-bit integer that represents the index in the <see cref="T:System.Array" /> at which copying starts.</param>
		// Token: 0x060013AC RID: 5036 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override void CopyTo(Array array, int index)
		{
			throw ADP.OleDb();
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> to the specified <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> starting at the specified destination index.</summary>
		/// <param name="array">The <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> that is the destination of the elements copied from the current <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</param>
		/// <param name="index">A 32-bit integer that represents the index in the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> at which copying starts.</param>
		// Token: 0x060013AD RID: 5037 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public void CopyTo(OleDbParameter[] array, int index)
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</returns>
		// Token: 0x060013AE RID: 5038 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override IEnumerator GetEnumerator()
		{
			throw ADP.OleDb();
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override DbParameter GetParameter(int index)
		{
			throw ADP.OleDb();
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override DbParameter GetParameter(string parameterName)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> within the collection.</summary>
		/// <param name="value">The <see cref="T:System.Data.OleDb.OleDbParameter" /> object in the collection to find.</param>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> that is a <see cref="T:System.Data.OleDb.OleDbParameter" /> within the collection.</returns>
		// Token: 0x060013B1 RID: 5041 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public int IndexOf(OleDbParameter value)
		{
			throw ADP.OleDb();
		}

		/// <summary>The location of the specified <see cref="T:System.Object" /> within the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to find.</param>
		/// <returns>The zero-based location of the specified <see cref="T:System.Object" /> that is a <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> within the collection.</returns>
		// Token: 0x060013B2 RID: 5042 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override int IndexOf(object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> with the specified name.</summary>
		/// <param name="parameterName">The case-sensitive name of the <see cref="T:System.Data.OleDb.OleDbParameter" /> to find.</param>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> with the specified case-sensitive name.</returns>
		// Token: 0x060013B3 RID: 5043 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override int IndexOf(string parameterName)
		{
			throw ADP.OleDb();
		}

		/// <summary>Inserts a <see cref="T:System.Data.OleDb.OleDbParameter" /> object into the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">An <see cref="T:System.Data.OleDb.OleDbParameter" /> object to be inserted in the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</param>
		// Token: 0x060013B4 RID: 5044 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public void Insert(int index, OleDbParameter value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Inserts a <see cref="T:System.Object" /> into the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">A <see cref="T:System.Object" /> to be inserted in the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</param>
		// Token: 0x060013B5 RID: 5045 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override void Insert(int index, object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Removes the <see cref="T:System.Data.OleDb.OleDbParameter" /> from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="value">An <see cref="T:System.Data.OleDb.OleDbParameter" /> object to remove from the collection.</param>
		/// <exception cref="T:System.InvalidCastException">The parameter is not a <see cref="T:System.Data.OleDb.OleDbParameter" />.</exception>
		/// <exception cref="T:System.SystemException">The parameter does not exist in the collection.</exception>
		// Token: 0x060013B6 RID: 5046 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public void Remove(OleDbParameter value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Removes the <see cref="T:System.Object" /> object from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> to be removed from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</param>
		// Token: 0x060013B7 RID: 5047 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override void Remove(object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Removes the <see cref="T:System.Data.OleDb.OleDbParameter" /> from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.OleDb.OleDbParameter" /> object to remove.</param>
		// Token: 0x060013B8 RID: 5048 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override void RemoveAt(int index)
		{
			throw ADP.OleDb();
		}

		/// <summary>Removes the <see cref="T:System.Data.OleDb.OleDbParameter" /> from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> at the specified parameter name.</summary>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.OleDb.OleDbParameter" /> object to remove.</param>
		// Token: 0x060013B9 RID: 5049 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override void RemoveAt(string parameterName)
		{
			throw ADP.OleDb();
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override void SetParameter(int index, DbParameter value)
		{
			throw ADP.OleDb();
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns an integer that contains the number of elements in the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />. Read-only.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> as an integer.</returns>
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override int Count
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> has a fixed size. Read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> has a fixed size; otherwise <see langword="false" />.</returns>
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override bool IsFixedSize
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> is read only; otherwise <see langword="false" />.</returns>
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override bool IsReadOnly
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> is synchronized. Read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> is synchronized; otherwise <see langword="false" />.</returns>
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override bool IsSynchronized
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbParameter" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the parameter to retrieve.</param>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbParameter" /> at the specified index.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index specified does not exist.</exception>
		// Token: 0x17000352 RID: 850
		public OleDbParameter this[int index]
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbParameter" /> with the specified name.</summary>
		/// <param name="parameterName">The name of the parameter to retrieve.</param>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbParameter" /> with the specified name.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The name specified does not exist.</exception>
		// Token: 0x17000353 RID: 851
		public OleDbParameter this[string parameterName]
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />. Read-only.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</returns>
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override object SyncRoot
		{
			get
			{
				throw ADP.OleDb();
			}
		}
	}
}
