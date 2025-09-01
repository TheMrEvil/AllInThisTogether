using System;
using System.Collections;

namespace System.Drawing.Design
{
	/// <summary>Represents a collection of toolbox items.</summary>
	// Token: 0x0200012D RID: 301
	public sealed class ToolboxItemCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> class using the specified collection.</summary>
		/// <param name="value">A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> to fill the new collection with.</param>
		// Token: 0x06000DB7 RID: 3511 RVA: 0x0001EF88 File Offset: 0x0001D188
		public ToolboxItemCollection(ToolboxItemCollection value)
		{
			base.InnerList.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> class using the specified array of toolbox items.</summary>
		/// <param name="value">An array of type <see cref="T:System.Drawing.Design.ToolboxItem" /> containing the toolbox items to fill the collection with.</param>
		// Token: 0x06000DB8 RID: 3512 RVA: 0x0001EF88 File Offset: 0x0001D188
		public ToolboxItemCollection(ToolboxItem[] value)
		{
			base.InnerList.AddRange(value);
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Design.ToolboxItem" /> at the specified index.</summary>
		/// <param name="index">The index of the object to get or set.</param>
		/// <returns>A <see cref="T:System.Drawing.Design.ToolboxItem" /> at each valid index in the collection.</returns>
		// Token: 0x170003AB RID: 939
		public ToolboxItem this[int index]
		{
			get
			{
				return (ToolboxItem)base.InnerList[index];
			}
		}

		/// <summary>Indicates whether the collection contains the specified <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <param name="value">A <see cref="T:System.Drawing.Design.ToolboxItem" /> to search the collection for.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DBA RID: 3514 RVA: 0x0001EFAF File Offset: 0x0001D1AF
		public bool Contains(ToolboxItem value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Copies the collection to the specified array beginning with the specified destination index.</summary>
		/// <param name="array">The array to copy to.</param>
		/// <param name="index">The index to begin copying to.</param>
		// Token: 0x06000DBB RID: 3515 RVA: 0x0001EFBD File Offset: 0x0001D1BD
		public void CopyTo(ToolboxItem[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Drawing.Design.ToolboxItem" />, if it exists in the collection.</summary>
		/// <param name="value">A <see cref="T:System.Drawing.Design.ToolboxItem" /> to get the index of in the collection.</param>
		/// <returns>The index of the specified <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x06000DBC RID: 3516 RVA: 0x0001EFCC File Offset: 0x0001D1CC
		public int IndexOf(ToolboxItem value)
		{
			return base.InnerList.IndexOf(value);
		}
	}
}
