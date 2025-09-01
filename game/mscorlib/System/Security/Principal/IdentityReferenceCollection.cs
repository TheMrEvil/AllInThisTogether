using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Principal.IdentityReference" /> objects and provides a means of converting sets of <see cref="T:System.Security.Principal.IdentityReference" />-derived objects to <see cref="T:System.Security.Principal.IdentityReference" />-derived types.</summary>
	// Token: 0x020004E5 RID: 1253
	[ComVisible(false)]
	public class IdentityReferenceCollection : IEnumerable, ICollection<IdentityReference>, IEnumerable<IdentityReference>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> class with zero items in the collection.</summary>
		// Token: 0x060031FB RID: 12795 RVA: 0x000B7BD8 File Offset: 0x000B5DD8
		public IdentityReferenceCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> class by using the specified initial size.</summary>
		/// <param name="capacity">The initial number of items in the collection. The value of <paramref name="capacity" /> is a hint only; it is not necessarily the maximum number of items created.</param>
		// Token: 0x060031FC RID: 12796 RVA: 0x000B7BEB File Offset: 0x000B5DEB
		public IdentityReferenceCollection(int capacity)
		{
			this._list = new ArrayList(capacity);
		}

		/// <summary>Gets the number of items in the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		/// <returns>The number of <see cref="T:System.Security.Principal.IdentityReference" /> objects in the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</returns>
		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060031FD RID: 12797 RVA: 0x000B7BFF File Offset: 0x000B5DFF
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection is read-only.</summary>
		/// <returns>Always returns <see langword="false" />.</returns>
		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060031FE RID: 12798 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Sets or gets the node at the specified index of the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		/// <param name="index">The zero-based index in the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</param>
		/// <returns>The <see cref="T:System.Security.Principal.IdentityReference" /> at the specified index in the collection. If <paramref name="index" /> is greater than or equal to the number of nodes in the collection, the return value is <see langword="null" />.</returns>
		// Token: 0x170006A9 RID: 1705
		public IdentityReference this[int index]
		{
			get
			{
				if (index >= this._list.Count)
				{
					return null;
				}
				return (IdentityReference)this._list[index];
			}
			set
			{
				this._list[index] = value;
			}
		}

		/// <summary>Adds an <see cref="T:System.Security.Principal.IdentityReference" /> object to the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> object to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is <see langword="null" />.</exception>
		// Token: 0x06003201 RID: 12801 RVA: 0x000B7C3E File Offset: 0x000B5E3E
		public void Add(IdentityReference identity)
		{
			this._list.Add(identity);
		}

		/// <summary>Clears all <see cref="T:System.Security.Principal.IdentityReference" /> objects from the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		// Token: 0x06003202 RID: 12802 RVA: 0x000B7C4D File Offset: 0x000B5E4D
		public void Clear()
		{
			this._list.Clear();
		}

		/// <summary>Indicates whether the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection contains the specified <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> object to check for.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the specified object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is <see langword="null" />.</exception>
		// Token: 0x06003203 RID: 12803 RVA: 0x000B7C5C File Offset: 0x000B5E5C
		public bool Contains(IdentityReference identity)
		{
			using (IEnumerator enumerator = this._list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((IdentityReference)enumerator.Current).Equals(identity))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection to an <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> array, starting at the specified index.</summary>
		/// <param name="array">An <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> array object to which the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection is to be copied.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection is to be copied.</param>
		// Token: 0x06003204 RID: 12804 RVA: 0x000479F8 File Offset: 0x00045BF8
		public void CopyTo(IdentityReference[] array, int offset)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an enumerator that can be used to iterate through the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</returns>
		// Token: 0x06003205 RID: 12805 RVA: 0x000479F8 File Offset: 0x00045BF8
		public IEnumerator<IdentityReference> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an enumerator that can be used to iterate through the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</returns>
		// Token: 0x06003206 RID: 12806 RVA: 0x000479F8 File Offset: 0x00045BF8
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the specified <see cref="T:System.Security.Principal.IdentityReference" /> object from the collection.</summary>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> object to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object was removed from the collection.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is <see langword="null" />.</exception>
		// Token: 0x06003207 RID: 12807 RVA: 0x000B7CBC File Offset: 0x000B5EBC
		public bool Remove(IdentityReference identity)
		{
			foreach (object obj in this._list)
			{
				IdentityReference identityReference = (IdentityReference)obj;
				if (identityReference.Equals(identity))
				{
					this._list.Remove(identityReference);
					return true;
				}
			}
			return false;
		}

		/// <summary>Converts the objects in the collection to the specified type. Calling this method is the same as calling <see cref="M:System.Security.Principal.IdentityReferenceCollection.Translate(System.Type,System.Boolean)" /> with the second parameter set to <see langword="false" />, which means that exceptions will not be thrown for items that fail conversion.</summary>
		/// <param name="targetType">The type to which items in the collection are being converted.</param>
		/// <returns>A <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection that represents the converted contents of the original collection.</returns>
		// Token: 0x06003208 RID: 12808 RVA: 0x000479F8 File Offset: 0x00045BF8
		public IdentityReferenceCollection Translate(Type targetType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts the objects in the collection to the specified type and uses the specified fault tolerance to handle or ignore errors associated with a type not having a conversion mapping.</summary>
		/// <param name="targetType">The type to which items in the collection are being converted.</param>
		/// <param name="forceSuccess">A Boolean value that determines how conversion errors are handled.  
		///  If <paramref name="forceSuccess" /> is <see langword="true" />, conversion errors due to a mapping not being found for the translation result in a failed conversion and exceptions being thrown.  
		///  If <paramref name="forceSuccess" /> is <see langword="false" />, types that failed to convert due to a mapping not being found for the translation are copied without being converted into the collection being returned.</param>
		/// <returns>A <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection that represents the converted contents of the original collection.</returns>
		// Token: 0x06003209 RID: 12809 RVA: 0x000479F8 File Offset: 0x00045BF8
		public IdentityReferenceCollection Translate(Type targetType, bool forceSuccess)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040022C0 RID: 8896
		private ArrayList _list;
	}
}
