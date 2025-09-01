using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a collection of designers.</summary>
	// Token: 0x02000450 RID: 1104
	public class DesignerCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerCollection" /> class that contains the specified designers.</summary>
		/// <param name="designers">An array of <see cref="T:System.ComponentModel.Design.IDesignerHost" /> objects to store.</param>
		// Token: 0x060023E4 RID: 9188 RVA: 0x00081790 File Offset: 0x0007F990
		public DesignerCollection(IDesignerHost[] designers)
		{
			if (designers != null)
			{
				this._designers = new ArrayList(designers);
				return;
			}
			this._designers = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerCollection" /> class that contains the specified set of designers.</summary>
		/// <param name="designers">A list that contains the collection of designers to add.</param>
		// Token: 0x060023E5 RID: 9189 RVA: 0x000817B3 File Offset: 0x0007F9B3
		public DesignerCollection(IList designers)
		{
			this._designers = designers;
		}

		/// <summary>Gets the number of designers in the collection.</summary>
		/// <returns>The number of designers in the collection.</returns>
		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060023E6 RID: 9190 RVA: 0x000817C2 File Offset: 0x0007F9C2
		public int Count
		{
			get
			{
				return this._designers.Count;
			}
		}

		/// <summary>Gets the designer at the specified index.</summary>
		/// <param name="index">The index of the designer to return.</param>
		/// <returns>The designer at the specified index.</returns>
		// Token: 0x17000744 RID: 1860
		public virtual IDesignerHost this[int index]
		{
			get
			{
				return (IDesignerHost)this._designers[index];
			}
		}

		/// <summary>Gets a new enumerator for this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that enumerates the collection.</returns>
		// Token: 0x060023E8 RID: 9192 RVA: 0x000817E2 File Offset: 0x0007F9E2
		public IEnumerator GetEnumerator()
		{
			return this._designers.GetEnumerator();
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x000817EF File Offset: 0x0007F9EF
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060023EA RID: 9194 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060023EB RID: 9195 RVA: 0x00002F6A File Offset: 0x0000116A
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060023EC RID: 9196 RVA: 0x000817F7 File Offset: 0x0007F9F7
		void ICollection.CopyTo(Array array, int index)
		{
			this._designers.CopyTo(array, index);
		}

		/// <summary>Gets a new enumerator for this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that enumerates the collection.</returns>
		// Token: 0x060023ED RID: 9197 RVA: 0x00081806 File Offset: 0x0007FA06
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040010C4 RID: 4292
		private IList _designers;
	}
}
