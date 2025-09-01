using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Contains a collection of settings property values that map <see cref="T:System.Configuration.SettingsProperty" /> objects to <see cref="T:System.Configuration.SettingsPropertyValue" /> objects.</summary>
	// Token: 0x020001D1 RID: 465
	public class SettingsPropertyValueCollection : ICloneable, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> class.</summary>
		// Token: 0x06000C36 RID: 3126 RVA: 0x00032588 File Offset: 0x00030788
		public SettingsPropertyValueCollection()
		{
			this.items = new Hashtable();
		}

		/// <summary>Adds a <see cref="T:System.Configuration.SettingsPropertyValue" /> object to the collection.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsPropertyValue" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to add an item to the collection, but the collection was marked as read-only.</exception>
		// Token: 0x06000C37 RID: 3127 RVA: 0x0003259B File Offset: 0x0003079B
		public void Add(SettingsPropertyValue property)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			this.items.Add(property.Name, property);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x000325C0 File Offset: 0x000307C0
		internal void Add(SettingsPropertyValueCollection vals)
		{
			foreach (object obj in vals)
			{
				SettingsPropertyValue property = (SettingsPropertyValue)obj;
				this.Add(property);
			}
		}

		/// <summary>Removes all <see cref="T:System.Configuration.SettingsPropertyValue" /> objects from the collection.</summary>
		// Token: 0x06000C39 RID: 3129 RVA: 0x00032614 File Offset: 0x00030814
		public void Clear()
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			this.items.Clear();
		}

		/// <summary>Creates a copy of the existing collection.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> class.</returns>
		// Token: 0x06000C3A RID: 3130 RVA: 0x0003262F File Offset: 0x0003082F
		public object Clone()
		{
			return new SettingsPropertyValueCollection
			{
				items = (Hashtable)this.items.Clone()
			};
		}

		/// <summary>Copies this <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> collection to an array.</summary>
		/// <param name="array">The array to copy the collection to.</param>
		/// <param name="index">The index at which to begin copying.</param>
		// Token: 0x06000C3B RID: 3131 RVA: 0x0003264C File Offset: 0x0003084C
		public void CopyTo(Array array, int index)
		{
			this.items.Values.CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</returns>
		// Token: 0x06000C3C RID: 3132 RVA: 0x00032660 File Offset: 0x00030860
		public IEnumerator GetEnumerator()
		{
			return this.items.Values.GetEnumerator();
		}

		/// <summary>Removes a <see cref="T:System.Configuration.SettingsPropertyValue" /> object from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsPropertyValue" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to remove an item from the collection, but the collection was marked as read-only.</exception>
		// Token: 0x06000C3D RID: 3133 RVA: 0x00032672 File Offset: 0x00030872
		public void Remove(string name)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			this.items.Remove(name);
		}

		/// <summary>Sets the collection to be read-only.</summary>
		// Token: 0x06000C3E RID: 3134 RVA: 0x0003268E File Offset: 0x0003088E
		public void SetReadOnly()
		{
			this.isReadOnly = true;
		}

		/// <summary>Gets a value that specifies the number of <see cref="T:System.Configuration.SettingsPropertyValue" /> objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Configuration.SettingsPropertyValue" /> objects in the collection.</returns>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00032697 File Offset: 0x00030897
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> collection is synchronized; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x0000829A File Offset: 0x0000649A
		public bool IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an item from the collection.</summary>
		/// <param name="name">A <see cref="T:System.Configuration.SettingsPropertyValue" /> object.</param>
		/// <returns>The <see cref="T:System.Configuration.SettingsPropertyValue" /> object with the specified <paramref name="name" />.</returns>
		// Token: 0x1700021B RID: 539
		public SettingsPropertyValue this[string name]
		{
			get
			{
				return (SettingsPropertyValue)this.items[name];
			}
		}

		/// <summary>Gets the object to synchronize access to the collection.</summary>
		/// <returns>The object to synchronize access to the collection.</returns>
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0000829A File Offset: 0x0000649A
		public object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x040007B0 RID: 1968
		private Hashtable items;

		// Token: 0x040007B1 RID: 1969
		private bool isReadOnly;
	}
}
