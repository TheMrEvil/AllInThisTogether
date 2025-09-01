using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.PropertyInformation" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000059 RID: 89
	[Serializable]
	public sealed class PropertyInformationCollection : NameObjectCollectionBase
	{
		// Token: 0x060002FE RID: 766 RVA: 0x00008A12 File Offset: 0x00006C12
		internal PropertyInformationCollection() : base(StringComparer.Ordinal)
		{
		}

		/// <summary>Copies the entire <see cref="T:System.Configuration.PropertyInformationCollection" /> collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Configuration.PropertyInformationCollection" /> collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Array.Length" /> property of <paramref name="array" /> is less than <see cref="P:System.Collections.Specialized.NameObjectCollectionBase.Count" /> + <paramref name="index" />.</exception>
		// Token: 0x060002FF RID: 767 RVA: 0x00008A1F File Offset: 0x00006C1F
		public void CopyTo(PropertyInformation[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Configuration.PropertyInformation" /> object in the collection, based on the specified property name.</summary>
		/// <param name="propertyName">The name of the configuration attribute contained in the <see cref="T:System.Configuration.PropertyInformationCollection" /> object.</param>
		/// <returns>A <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		// Token: 0x170000E0 RID: 224
		public PropertyInformation this[string propertyName]
		{
			get
			{
				return (PropertyInformation)base.BaseGet(propertyName);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> object, which is used to iterate through this <see cref="T:System.Configuration.PropertyInformationCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object, which is used to iterate through this <see cref="T:System.Configuration.PropertyInformationCollection" />.</returns>
		// Token: 0x06000301 RID: 769 RVA: 0x00008A37 File Offset: 0x00006C37
		public override IEnumerator GetEnumerator()
		{
			return new PropertyInformationCollection.PropertyInformationEnumerator(this);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00008A3F File Offset: 0x00006C3F
		internal void Add(PropertyInformation pi)
		{
			base.BaseAdd(pi.Name, pi);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the <see cref="T:System.Configuration.PropertyInformationCollection" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Configuration.PropertyInformationCollection" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Configuration.PropertyInformationCollection" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06000303 RID: 771 RVA: 0x0000371B File Offset: 0x0000191B
		[MonoTODO]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0200005A RID: 90
		private class PropertyInformationEnumerator : IEnumerator
		{
			// Token: 0x06000304 RID: 772 RVA: 0x00008A4E File Offset: 0x00006C4E
			public PropertyInformationEnumerator(PropertyInformationCollection collection)
			{
				this.collection = collection;
				this.position = -1;
			}

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x06000305 RID: 773 RVA: 0x00008A64 File Offset: 0x00006C64
			public object Current
			{
				get
				{
					if (this.position < this.collection.Count && this.position >= 0)
					{
						return this.collection.BaseGet(this.position);
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x06000306 RID: 774 RVA: 0x00008A9C File Offset: 0x00006C9C
			public bool MoveNext()
			{
				int num = this.position + 1;
				this.position = num;
				return num < this.collection.Count;
			}

			// Token: 0x06000307 RID: 775 RVA: 0x00008ACA File Offset: 0x00006CCA
			public void Reset()
			{
				this.position = -1;
			}

			// Token: 0x04000118 RID: 280
			private PropertyInformationCollection collection;

			// Token: 0x04000119 RID: 281
			private int position;
		}
	}
}
