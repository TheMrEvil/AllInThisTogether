using System;

namespace System.Collections.Specialized
{
	/// <summary>Creates collections that ignore the case in strings.</summary>
	// Token: 0x020004A2 RID: 1186
	public class CollectionsUtil
	{
		/// <summary>Creates a new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class with the default initial capacity.</summary>
		/// <returns>A new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class with the default initial capacity.</returns>
		// Token: 0x0600260C RID: 9740 RVA: 0x000857E4 File Offset: 0x000839E4
		public static Hashtable CreateCaseInsensitiveHashtable()
		{
			return new Hashtable(StringComparer.CurrentCultureIgnoreCase);
		}

		/// <summary>Creates a new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class with the specified initial capacity.</summary>
		/// <param name="capacity">The approximate number of entries that the <see cref="T:System.Collections.Hashtable" /> can initially contain.</param>
		/// <returns>A new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class with the specified initial capacity.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x0600260D RID: 9741 RVA: 0x000857F0 File Offset: 0x000839F0
		public static Hashtable CreateCaseInsensitiveHashtable(int capacity)
		{
			return new Hashtable(capacity, StringComparer.CurrentCultureIgnoreCase);
		}

		/// <summary>Copies the entries from the specified dictionary to a new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class with the same initial capacity as the number of entries copied.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> to copy to a new case-insensitive <see cref="T:System.Collections.Hashtable" />.</param>
		/// <returns>A new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class containing the entries from the specified <see cref="T:System.Collections.IDictionary" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is <see langword="null" />.</exception>
		// Token: 0x0600260E RID: 9742 RVA: 0x000857FD File Offset: 0x000839FD
		public static Hashtable CreateCaseInsensitiveHashtable(IDictionary d)
		{
			return new Hashtable(d, StringComparer.CurrentCultureIgnoreCase);
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Collections.SortedList" /> class that ignores the case of strings.</summary>
		/// <returns>A new instance of the <see cref="T:System.Collections.SortedList" /> class that ignores the case of strings.</returns>
		// Token: 0x0600260F RID: 9743 RVA: 0x0008580A File Offset: 0x00083A0A
		public static SortedList CreateCaseInsensitiveSortedList()
		{
			return new SortedList(CaseInsensitiveComparer.Default);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.CollectionsUtil" /> class.</summary>
		// Token: 0x06002610 RID: 9744 RVA: 0x0000219B File Offset: 0x0000039B
		public CollectionsUtil()
		{
		}
	}
}
