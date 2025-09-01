using System;
using System.Collections;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>Provides a collection container for instances of the <see cref="T:System.Net.Cookie" /> class.</summary>
	// Token: 0x0200064F RID: 1615
	[Serializable]
	public class CookieCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.CookieCollection" /> class.</summary>
		// Token: 0x060032D8 RID: 13016 RVA: 0x000B0B14 File Offset: 0x000AED14
		public CookieCollection()
		{
			this.m_IsReadOnly = true;
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000B0B39 File Offset: 0x000AED39
		internal CookieCollection(bool IsReadOnly)
		{
			this.m_IsReadOnly = IsReadOnly;
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Net.CookieCollection" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if this is a read-only <see cref="T:System.Net.CookieCollection" />; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x060032DA RID: 13018 RVA: 0x000B0B5E File Offset: 0x000AED5E
		public bool IsReadOnly
		{
			get
			{
				return this.m_IsReadOnly;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.Cookie" /> with a specific index from a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Net.Cookie" /> to be found.</param>
		/// <returns>A <see cref="T:System.Net.Cookie" /> with a specific index from a <see cref="T:System.Net.CookieCollection" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or <paramref name="index" /> is greater than or equal to <see cref="P:System.Net.CookieCollection.Count" />.</exception>
		// Token: 0x17000A40 RID: 2624
		public Cookie this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_list.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return (Cookie)this.m_list[index];
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.Cookie" /> with a specific name from a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <param name="name">The name of the <see cref="T:System.Net.Cookie" /> to be found.</param>
		/// <returns>The <see cref="T:System.Net.Cookie" /> with a specific name from a <see cref="T:System.Net.CookieCollection" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x17000A41 RID: 2625
		public Cookie this[string name]
		{
			get
			{
				foreach (object obj in this.m_list)
				{
					Cookie cookie = (Cookie)obj;
					if (string.Compare(cookie.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return cookie;
					}
				}
				return null;
			}
		}

		/// <summary>Adds a <see cref="T:System.Net.Cookie" /> to a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <param name="cookie">The <see cref="T:System.Net.Cookie" /> to be added to a <see cref="T:System.Net.CookieCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookie" /> is <see langword="null" />.</exception>
		// Token: 0x060032DD RID: 13021 RVA: 0x000B0C00 File Offset: 0x000AEE00
		public void Add(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			this.m_version++;
			int num = this.IndexOf(cookie);
			if (num == -1)
			{
				this.m_list.Add(cookie);
				return;
			}
			this.m_list[num] = cookie;
		}

		/// <summary>Adds the contents of a <see cref="T:System.Net.CookieCollection" /> to the current instance.</summary>
		/// <param name="cookies">The <see cref="T:System.Net.CookieCollection" /> to be added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookies" /> is <see langword="null" />.</exception>
		// Token: 0x060032DE RID: 13022 RVA: 0x000B0C50 File Offset: 0x000AEE50
		public void Add(CookieCollection cookies)
		{
			if (cookies == null)
			{
				throw new ArgumentNullException("cookies");
			}
			foreach (object obj in cookies)
			{
				Cookie cookie = (Cookie)obj;
				this.Add(cookie);
			}
		}

		/// <summary>Gets the number of cookies contained in a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>The number of cookies contained in a <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x060032DF RID: 13023 RVA: 0x000B0CB4 File Offset: 0x000AEEB4
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to a <see cref="T:System.Net.CookieCollection" /> is thread safe.</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Net.CookieCollection" /> is thread safe; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060032E0 RID: 13024 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object to synchronize access to the <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>An object to synchronize access to the <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060032E1 RID: 13025 RVA: 0x000075E1 File Offset: 0x000057E1
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the elements of a <see cref="T:System.Net.CookieCollection" /> to an instance of the <see cref="T:System.Array" /> class, starting at a particular index.</summary>
		/// <param name="array">The target <see cref="T:System.Array" /> to which the <see cref="T:System.Net.CookieCollection" /> will be copied.</param>
		/// <param name="index">The zero-based index in the target <see cref="T:System.Array" /> where copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in this <see cref="T:System.Net.CookieCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.CookieCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060032E2 RID: 13026 RVA: 0x000B0CC1 File Offset: 0x000AEEC1
		public void CopyTo(Array array, int index)
		{
			this.m_list.CopyTo(array, index);
		}

		/// <summary>Copies the elements of this <see cref="T:System.Net.CookieCollection" /> to a <see cref="T:System.Net.Cookie" /> array starting at the specified index of the target array.</summary>
		/// <param name="array">The target <see cref="T:System.Net.Cookie" /> array to which the <see cref="T:System.Net.CookieCollection" /> will be copied.</param>
		/// <param name="index">The zero-based index in the target <see cref="T:System.Array" /> where copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in this <see cref="T:System.Net.CookieCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.CookieCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060032E3 RID: 13027 RVA: 0x000B0CC1 File Offset: 0x000AEEC1
		public void CopyTo(Cookie[] array, int index)
		{
			this.m_list.CopyTo(array, index);
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x000B0CD0 File Offset: 0x000AEED0
		internal DateTime TimeStamp(CookieCollection.Stamp how)
		{
			switch (how)
			{
			case CookieCollection.Stamp.Set:
				this.m_TimeStamp = DateTime.Now;
				break;
			case CookieCollection.Stamp.SetToUnused:
				this.m_TimeStamp = DateTime.MinValue;
				break;
			case CookieCollection.Stamp.SetToMaxUsed:
				this.m_TimeStamp = DateTime.MaxValue;
				break;
			}
			return this.m_TimeStamp;
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060032E5 RID: 13029 RVA: 0x000B0D20 File Offset: 0x000AEF20
		internal bool IsOtherVersionSeen
		{
			get
			{
				return this.m_has_other_versions;
			}
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000B0D28 File Offset: 0x000AEF28
		internal int InternalAdd(Cookie cookie, bool isStrict)
		{
			int result = 1;
			if (isStrict)
			{
				IComparer comparer = Cookie.GetComparer();
				int num = 0;
				foreach (object obj in this.m_list)
				{
					Cookie cookie2 = (Cookie)obj;
					if (comparer.Compare(cookie, cookie2) == 0)
					{
						result = 0;
						if (cookie2.Variant <= cookie.Variant)
						{
							this.m_list[num] = cookie;
							break;
						}
						break;
					}
					else
					{
						num++;
					}
				}
				if (num == this.m_list.Count)
				{
					this.m_list.Add(cookie);
				}
			}
			else
			{
				this.m_list.Add(cookie);
			}
			if (cookie.Version != 1)
			{
				this.m_has_other_versions = true;
			}
			return result;
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x000B0DF8 File Offset: 0x000AEFF8
		internal int IndexOf(Cookie cookie)
		{
			IComparer comparer = Cookie.GetComparer();
			int num = 0;
			foreach (object obj in this.m_list)
			{
				Cookie y = (Cookie)obj;
				if (comparer.Compare(cookie, y) == 0)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000B0E6C File Offset: 0x000AF06C
		internal void RemoveAt(int idx)
		{
			this.m_list.RemoveAt(idx);
		}

		/// <summary>Gets an enumerator that can iterate through a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>An instance of an implementation of an <see cref="T:System.Collections.IEnumerator" /> interface that can iterate through a <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x060032E9 RID: 13033 RVA: 0x000B0E7A File Offset: 0x000AF07A
		public IEnumerator GetEnumerator()
		{
			return new CookieCollection.CookieCollectionEnumerator(this);
		}

		// Token: 0x04001DDF RID: 7647
		internal int m_version;

		// Token: 0x04001DE0 RID: 7648
		private ArrayList m_list = new ArrayList();

		// Token: 0x04001DE1 RID: 7649
		private DateTime m_TimeStamp = DateTime.MinValue;

		// Token: 0x04001DE2 RID: 7650
		private bool m_has_other_versions;

		// Token: 0x04001DE3 RID: 7651
		[OptionalField]
		private bool m_IsReadOnly;

		// Token: 0x02000650 RID: 1616
		internal enum Stamp
		{
			// Token: 0x04001DE5 RID: 7653
			Check,
			// Token: 0x04001DE6 RID: 7654
			Set,
			// Token: 0x04001DE7 RID: 7655
			SetToUnused,
			// Token: 0x04001DE8 RID: 7656
			SetToMaxUsed
		}

		// Token: 0x02000651 RID: 1617
		private class CookieCollectionEnumerator : IEnumerator
		{
			// Token: 0x060032EA RID: 13034 RVA: 0x000B0E82 File Offset: 0x000AF082
			internal CookieCollectionEnumerator(CookieCollection cookies)
			{
				this.m_cookies = cookies;
				this.m_count = cookies.Count;
				this.m_version = cookies.m_version;
			}

			// Token: 0x17000A46 RID: 2630
			// (get) Token: 0x060032EB RID: 13035 RVA: 0x000B0EB0 File Offset: 0x000AF0B0
			object IEnumerator.Current
			{
				get
				{
					if (this.m_index < 0 || this.m_index >= this.m_count)
					{
						throw new InvalidOperationException(SR.GetString("Enumeration has either not started or has already finished."));
					}
					if (this.m_version != this.m_cookies.m_version)
					{
						throw new InvalidOperationException(SR.GetString("Collection was modified; enumeration operation may not execute."));
					}
					return this.m_cookies[this.m_index];
				}
			}

			// Token: 0x060032EC RID: 13036 RVA: 0x000B0F18 File Offset: 0x000AF118
			bool IEnumerator.MoveNext()
			{
				if (this.m_version != this.m_cookies.m_version)
				{
					throw new InvalidOperationException(SR.GetString("Collection was modified; enumeration operation may not execute."));
				}
				int num = this.m_index + 1;
				this.m_index = num;
				if (num < this.m_count)
				{
					return true;
				}
				this.m_index = this.m_count;
				return false;
			}

			// Token: 0x060032ED RID: 13037 RVA: 0x000B0F70 File Offset: 0x000AF170
			void IEnumerator.Reset()
			{
				this.m_index = -1;
			}

			// Token: 0x04001DE9 RID: 7657
			private CookieCollection m_cookies;

			// Token: 0x04001DEA RID: 7658
			private int m_count;

			// Token: 0x04001DEB RID: 7659
			private int m_index = -1;

			// Token: 0x04001DEC RID: 7660
			private int m_version;
		}
	}
}
