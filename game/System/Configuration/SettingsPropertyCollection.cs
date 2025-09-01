using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.SettingsProperty" /> objects.</summary>
	// Token: 0x020001CD RID: 461
	public class SettingsPropertyCollection : ICloneable, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> class.</summary>
		// Token: 0x06000C0D RID: 3085 RVA: 0x00031FC8 File Offset: 0x000301C8
		public SettingsPropertyCollection()
		{
			this.items = new Hashtable();
		}

		/// <summary>Adds a <see cref="T:System.Configuration.SettingsProperty" /> object to the collection.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000C0E RID: 3086 RVA: 0x00031FDB File Offset: 0x000301DB
		public void Add(SettingsProperty property)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			this.OnAdd(property);
			this.items.Add(property.Name, property);
			this.OnAddComplete(property);
		}

		/// <summary>Removes all <see cref="T:System.Configuration.SettingsProperty" /> objects from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000C0F RID: 3087 RVA: 0x0003200B File Offset: 0x0003020B
		public void Clear()
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			this.OnClear();
			this.items.Clear();
			this.OnClearComplete();
		}

		/// <summary>Creates a copy of the existing collection.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyCollection" /> class.</returns>
		// Token: 0x06000C10 RID: 3088 RVA: 0x00032032 File Offset: 0x00030232
		public object Clone()
		{
			return new SettingsPropertyCollection
			{
				items = (Hashtable)this.items.Clone()
			};
		}

		/// <summary>Copies this <see cref="T:System.Configuration.SettingsPropertyCollection" /> object to an array.</summary>
		/// <param name="array">The array to copy the object to.</param>
		/// <param name="index">The index at which to begin copying.</param>
		// Token: 0x06000C11 RID: 3089 RVA: 0x0003204F File Offset: 0x0003024F
		public void CopyTo(Array array, int index)
		{
			this.items.Values.CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</returns>
		// Token: 0x06000C12 RID: 3090 RVA: 0x00032063 File Offset: 0x00030263
		public IEnumerator GetEnumerator()
		{
			return this.items.Values.GetEnumerator();
		}

		/// <summary>Removes a <see cref="T:System.Configuration.SettingsProperty" /> object from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000C13 RID: 3091 RVA: 0x00032078 File Offset: 0x00030278
		public void Remove(string name)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			SettingsProperty property = (SettingsProperty)this.items[name];
			this.OnRemove(property);
			this.items.Remove(name);
			this.OnRemoveComplete(property);
		}

		/// <summary>Sets the collection to be read-only.</summary>
		// Token: 0x06000C14 RID: 3092 RVA: 0x000320BF File Offset: 0x000302BF
		public void SetReadOnly()
		{
			this.isReadOnly = true;
		}

		/// <summary>Performs additional, custom processing when adding to the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06000C15 RID: 3093 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnAdd(SettingsProperty property)
		{
		}

		/// <summary>Performs additional, custom processing after adding to the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06000C16 RID: 3094 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnAddComplete(SettingsProperty property)
		{
		}

		/// <summary>Performs additional, custom processing when clearing the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		// Token: 0x06000C17 RID: 3095 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnClear()
		{
		}

		/// <summary>Performs additional, custom processing after clearing the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		// Token: 0x06000C18 RID: 3096 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnClearComplete()
		{
		}

		/// <summary>Performs additional, custom processing when removing the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06000C19 RID: 3097 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnRemove(SettingsProperty property)
		{
		}

		/// <summary>Performs additional, custom processing after removing the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06000C1A RID: 3098 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnRemoveComplete(SettingsProperty property)
		{
		}

		/// <summary>Gets a value that specifies the number of <see cref="T:System.Configuration.SettingsProperty" /> objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Configuration.SettingsProperty" /> objects in the collection.</returns>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x000320C8 File Offset: 0x000302C8
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Configuration.SettingsPropertyCollection" /> is synchronized; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the collection item with the specified name.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <returns>The <see cref="T:System.Configuration.SettingsProperty" /> object with the specified <paramref name="name" />.</returns>
		// Token: 0x17000210 RID: 528
		public SettingsProperty this[string name]
		{
			get
			{
				return (SettingsProperty)this.items[name];
			}
		}

		/// <summary>Gets the object to synchronize access to the collection.</summary>
		/// <returns>The object to synchronize access to the collection.</returns>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x000075E1 File Offset: 0x000057E1
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x040007A6 RID: 1958
		private Hashtable items;

		// Token: 0x040007A7 RID: 1959
		private bool isReadOnly;
	}
}
