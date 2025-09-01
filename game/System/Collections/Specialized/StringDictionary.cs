using System;
using System.Collections.Generic;

namespace System.Collections.Specialized
{
	/// <summary>Implements a hash table with the key and the value strongly typed to be strings rather than objects.</summary>
	// Token: 0x020004B2 RID: 1202
	[Serializable]
	public class StringDictionary : IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.StringDictionary" /> class.</summary>
		// Token: 0x060026D9 RID: 9945 RVA: 0x000873DB File Offset: 0x000855DB
		public StringDictionary()
		{
		}

		/// <summary>Gets the number of key/value pairs in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <returns>The number of key/value pairs in the <see cref="T:System.Collections.Specialized.StringDictionary" />.  
		///  Retrieving the value of this property is an O(1) operation.</returns>
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060026DA RID: 9946 RVA: 0x000873EE File Offset: 0x000855EE
		public virtual int Count
		{
			get
			{
				return this.contents.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.StringDictionary" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.Specialized.StringDictionary" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060026DB RID: 9947 RVA: 0x000873FB File Offset: 0x000855FB
		public virtual bool IsSynchronized
		{
			get
			{
				return this.contents.IsSynchronized;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <param name="key">The key whose value to get or set.</param>
		/// <returns>The value associated with the specified key. If the specified key is not found, Get returns <see langword="null" />, and Set creates a new entry with the specified key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x170007E6 RID: 2022
		public virtual string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return (string)this.contents[key.ToLowerInvariant()];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.contents[key.ToLowerInvariant()] = value;
			}
		}

		/// <summary>Gets a collection of keys in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that provides the keys in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</returns>
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x00087450 File Offset: 0x00085650
		public virtual ICollection Keys
		{
			get
			{
				return this.contents.Keys;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.StringDictionary" />.</returns>
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060026DF RID: 9951 RVA: 0x0008745D File Offset: 0x0008565D
		public virtual object SyncRoot
		{
			get
			{
				return this.contents.SyncRoot;
			}
		}

		/// <summary>Gets a collection of values in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that provides the values in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</returns>
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060026E0 RID: 9952 RVA: 0x0008746A File Offset: 0x0008566A
		public virtual ICollection Values
		{
			get
			{
				return this.contents.Values;
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <param name="key">The key of the entry to add.</param>
		/// <param name="value">The value of the entry to add. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An entry with the same key already exists in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringDictionary" /> is read-only.</exception>
		// Token: 0x060026E1 RID: 9953 RVA: 0x00087477 File Offset: 0x00085677
		public virtual void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Add(key.ToLowerInvariant(), value);
		}

		/// <summary>Removes all entries from the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringDictionary" /> is read-only.</exception>
		// Token: 0x060026E2 RID: 9954 RVA: 0x00087499 File Offset: 0x00085699
		public virtual void Clear()
		{
			this.contents.Clear();
		}

		/// <summary>Determines if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains a specific key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains an entry with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The key is <see langword="null" />.</exception>
		// Token: 0x060026E3 RID: 9955 RVA: 0x000874A6 File Offset: 0x000856A6
		public virtual bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.contents.ContainsKey(key.ToLowerInvariant());
		}

		/// <summary>Determines if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains a specific value.</summary>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Specialized.StringDictionary" />. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains an element with the specified value; otherwise, <see langword="false" />.</returns>
		// Token: 0x060026E4 RID: 9956 RVA: 0x000874C7 File Offset: 0x000856C7
		public virtual bool ContainsValue(string value)
		{
			return this.contents.ContainsValue(value);
		}

		/// <summary>Copies the string dictionary values to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the <see cref="T:System.Collections.Specialized.StringDictionary" />.</param>
		/// <param name="index">The index in the array where copying begins.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the <see cref="T:System.Collections.Specialized.StringDictionary" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.</exception>
		// Token: 0x060026E5 RID: 9957 RVA: 0x000874D5 File Offset: 0x000856D5
		public virtual void CopyTo(Array array, int index)
		{
			this.contents.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the string dictionary.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that iterates through the string dictionary.</returns>
		// Token: 0x060026E6 RID: 9958 RVA: 0x000874E4 File Offset: 0x000856E4
		public virtual IEnumerator GetEnumerator()
		{
			return this.contents.GetEnumerator();
		}

		/// <summary>Removes the entry with the specified key from the string dictionary.</summary>
		/// <param name="key">The key of the entry to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The key is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringDictionary" /> is read-only.</exception>
		// Token: 0x060026E7 RID: 9959 RVA: 0x000874F1 File Offset: 0x000856F1
		public virtual void Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Remove(key.ToLowerInvariant());
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x00087512 File Offset: 0x00085712
		internal void ReplaceHashtable(Hashtable useThisHashtableInstead)
		{
			this.contents = useThisHashtableInstead;
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x0008751B File Offset: 0x0008571B
		internal IDictionary<string, string> AsGenericDictionary()
		{
			return new GenericAdapter(this);
		}

		// Token: 0x04001514 RID: 5396
		internal Hashtable contents = new Hashtable();
	}
}
