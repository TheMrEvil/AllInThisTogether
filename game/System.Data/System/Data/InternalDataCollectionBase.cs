using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	/// <summary>Provides the base functionality for creating collections.</summary>
	// Token: 0x020000A2 RID: 162
	public class InternalDataCollectionBase : ICollection, IEnumerable
	{
		/// <summary>Gets the total number of elements in a collection.</summary>
		/// <returns>The total number of elements in a collection.</returns>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0002AF4C File Offset: 0x0002914C
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				return this.List.Count;
			}
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.InternalDataCollectionBase" /> to a one-dimensional <see cref="T:System.Array" />, starting at the specified <see cref="T:System.Data.InternalDataCollectionBase" /> index.</summary>
		/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> to copy the current <see cref="T:System.Data.InternalDataCollectionBase" /> object's elements into.</param>
		/// <param name="index">The destination <see cref="T:System.Array" /> index to start copying into.</param>
		// Token: 0x06000A38 RID: 2616 RVA: 0x0002AF59 File Offset: 0x00029159
		public virtual void CopyTo(Array ar, int index)
		{
			this.List.CopyTo(ar, index);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
		// Token: 0x06000A39 RID: 2617 RVA: 0x0002AF68 File Offset: 0x00029168
		public virtual IEnumerator GetEnumerator()
		{
			return this.List.GetEnumerator();
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.InternalDataCollectionBase" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00006D64 File Offset: 0x00004F64
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.InternalDataCollectionBase" /> is synchonized.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is synchronized; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00006D64 File Offset: 0x00004F64
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002AF75 File Offset: 0x00029175
		internal int NamesEqual(string s1, string s2, bool fCaseSensitive, CultureInfo locale)
		{
			if (fCaseSensitive)
			{
				if (string.Compare(s1, s2, false, locale) != 0)
				{
					return 0;
				}
				return 1;
			}
			else
			{
				if (locale.CompareInfo.Compare(s1, s2, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) != 0)
				{
					return 0;
				}
				if (string.Compare(s1, s2, false, locale) != 0)
				{
					return -1;
				}
				return 1;
			}
		}

		/// <summary>Gets an object that can be used to synchronize the collection.</summary>
		/// <returns>The <see cref="T:System.Object" /> used to synchronize the collection.</returns>
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00005696 File Offset: 0x00003896
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the items of the collection as a list.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains the collection.</returns>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00003E32 File Offset: 0x00002032
		protected virtual ArrayList List
		{
			get
			{
				return null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InternalDataCollectionBase" /> class.</summary>
		// Token: 0x06000A3F RID: 2623 RVA: 0x00003D93 File Offset: 0x00001F93
		public InternalDataCollectionBase()
		{
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002AFAD File Offset: 0x000291AD
		// Note: this type is marked as 'beforefieldinit'.
		static InternalDataCollectionBase()
		{
		}

		// Token: 0x0400075E RID: 1886
		internal static readonly CollectionChangeEventArgs s_refreshEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null);
	}
}
