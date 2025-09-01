using System;

namespace System.Collections.Specialized
{
	/// <summary>Represents a collection of strings.</summary>
	// Token: 0x020004B0 RID: 1200
	[Serializable]
	public class StringCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the entry to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Specialized.StringCollection.Count" />.</exception>
		// Token: 0x170007DB RID: 2011
		public string this[int index]
		{
			get
			{
				return (string)this.data[index];
			}
			set
			{
				this.data[index] = value;
			}
		}

		/// <summary>Gets the number of strings contained in the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>The number of strings contained in the <see cref="T:System.Collections.Specialized.StringCollection" />.</returns>
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x00087261 File Offset: 0x00085461
		public int Count
		{
			get
			{
				return this.data.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.StringCollection" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.StringCollection" /> object is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060026BB RID: 9915 RVA: 0x00003062 File Offset: 0x00001262
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.StringCollection" /> object has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.StringCollection" /> object has a fixed size; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x00003062 File Offset: 0x00001262
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds a string to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The string to add to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index at which the new element is inserted.</returns>
		// Token: 0x060026BD RID: 9917 RVA: 0x0008726E File Offset: 0x0008546E
		public int Add(string value)
		{
			return this.data.Add(value);
		}

		/// <summary>Copies the elements of a string array to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">An array of strings to add to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />. The array itself can not be <see langword="null" /> but it can contain elements that are <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x060026BE RID: 9918 RVA: 0x0008727C File Offset: 0x0008547C
		public void AddRange(string[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.data.AddRange(value);
		}

		/// <summary>Removes all the strings from the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		// Token: 0x060026BF RID: 9919 RVA: 0x00087298 File Offset: 0x00085498
		public void Clear()
		{
			this.data.Clear();
		}

		/// <summary>Determines whether the specified string is in the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The string to locate in the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is found in the <see cref="T:System.Collections.Specialized.StringCollection" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060026C0 RID: 9920 RVA: 0x000872A5 File Offset: 0x000854A5
		public bool Contains(string value)
		{
			return this.data.Contains(value);
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.Specialized.StringCollection" /> values to a one-dimensional array of strings, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional array of strings that is the destination of the elements copied from <see cref="T:System.Collections.Specialized.StringCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.Specialized.StringCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.StringCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060026C1 RID: 9921 RVA: 0x000872B3 File Offset: 0x000854B3
		public void CopyTo(string[] array, int index)
		{
			this.data.CopyTo(array, index);
		}

		/// <summary>Returns a <see cref="T:System.Collections.Specialized.StringEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringEnumerator" /> for the <see cref="T:System.Collections.Specialized.StringCollection" />.</returns>
		// Token: 0x060026C2 RID: 9922 RVA: 0x000872C2 File Offset: 0x000854C2
		public StringEnumerator GetEnumerator()
		{
			return new StringEnumerator(this);
		}

		/// <summary>Searches for the specified string and returns the zero-based index of the first occurrence within the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The string to locate. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> in the <see cref="T:System.Collections.Specialized.StringCollection" />, if found; otherwise, -1.</returns>
		// Token: 0x060026C3 RID: 9923 RVA: 0x000872CA File Offset: 0x000854CA
		public int IndexOf(string value)
		{
			return this.data.IndexOf(value);
		}

		/// <summary>Inserts a string into the <see cref="T:System.Collections.Specialized.StringCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> is inserted.</param>
		/// <param name="value">The string to insert. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> greater than <see cref="P:System.Collections.Specialized.StringCollection.Count" />.</exception>
		// Token: 0x060026C4 RID: 9924 RVA: 0x000872D8 File Offset: 0x000854D8
		public void Insert(int index, string value)
		{
			this.data.Insert(index, value);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.StringCollection" /> is read-only.</summary>
		/// <returns>This property always returns <see langword="false" />.</returns>
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.StringCollection" /> is synchronized (thread safe).</summary>
		/// <returns>This property always returns <see langword="false" />.</returns>
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060026C6 RID: 9926 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Removes the first occurrence of a specific string from the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The string to remove from the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be <see langword="null" />.</param>
		// Token: 0x060026C7 RID: 9927 RVA: 0x000872E7 File Offset: 0x000854E7
		public void Remove(string value)
		{
			this.data.Remove(value);
		}

		/// <summary>Removes the string at the specified index of the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="index">The zero-based index of the string to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Specialized.StringCollection.Count" />.</exception>
		// Token: 0x060026C8 RID: 9928 RVA: 0x000872F5 File Offset: 0x000854F5
		public void RemoveAt(int index)
		{
			this.data.RemoveAt(index);
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.StringCollection" />.</returns>
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060026C9 RID: 9929 RVA: 0x00087303 File Offset: 0x00085503
		public object SyncRoot
		{
			get
			{
				return this.data.SyncRoot;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Specialized.StringCollection.Count" />.</exception>
		// Token: 0x170007E2 RID: 2018
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (string)value;
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to be added to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be <see langword="null" />.</param>
		/// <returns>The <see cref="T:System.Collections.Specialized.StringCollection" /> index at which the <paramref name="value" /> has been added.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringCollection" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.Specialized.StringCollection" /> has a fixed size.</exception>
		// Token: 0x060026CC RID: 9932 RVA: 0x00087328 File Offset: 0x00085528
		int IList.Add(object value)
		{
			return this.Add((string)value);
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is found in the <see cref="T:System.Collections.Specialized.StringCollection" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060026CD RID: 9933 RVA: 0x00087336 File Offset: 0x00085536
		bool IList.Contains(object value)
		{
			return this.Contains((string)value);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Collections.Specialized.StringCollection" />, if found; otherwise, -1.</returns>
		// Token: 0x060026CE RID: 9934 RVA: 0x00087344 File Offset: 0x00085544
		int IList.IndexOf(object value)
		{
			return this.IndexOf((string)value);
		}

		/// <summary>Inserts an element into the <see cref="T:System.Collections.Specialized.StringCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than <see cref="P:System.Collections.Specialized.StringCollection.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringCollection" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.Specialized.StringCollection" /> has a fixed size.</exception>
		// Token: 0x060026CF RID: 9935 RVA: 0x00087352 File Offset: 0x00085552
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (string)value);
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringCollection" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.Specialized.StringCollection" /> has a fixed size.</exception>
		// Token: 0x060026D0 RID: 9936 RVA: 0x00087361 File Offset: 0x00085561
		void IList.Remove(object value)
		{
			this.Remove((string)value);
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.Specialized.StringCollection" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Specialized.StringCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.Specialized.StringCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.StringCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060026D1 RID: 9937 RVA: 0x000872B3 File Offset: 0x000854B3
		void ICollection.CopyTo(Array array, int index)
		{
			this.data.CopyTo(array, index);
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.StringCollection" />.</returns>
		// Token: 0x060026D2 RID: 9938 RVA: 0x0008736F File Offset: 0x0008556F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.StringCollection" /> class.</summary>
		// Token: 0x060026D3 RID: 9939 RVA: 0x0008737C File Offset: 0x0008557C
		public StringCollection()
		{
		}

		// Token: 0x04001511 RID: 5393
		private readonly ArrayList data = new ArrayList();
	}
}
