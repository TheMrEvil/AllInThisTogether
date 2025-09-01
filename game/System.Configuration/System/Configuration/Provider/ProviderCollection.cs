using System;
using System.Collections;

namespace System.Configuration.Provider
{
	/// <summary>Represents a collection of provider objects that inherit from <see cref="T:System.Configuration.Provider.ProviderBase" />.</summary>
	// Token: 0x02000078 RID: 120
	public class ProviderCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Provider.ProviderCollection" /> class.</summary>
		// Token: 0x060003E8 RID: 1000 RVA: 0x0000AEEC File Offset: 0x000090EC
		public ProviderCollection()
		{
			this.lookup = new Hashtable(10, StringComparer.InvariantCultureIgnoreCase);
			this.values = new ArrayList();
		}

		/// <summary>Adds a provider to the collection.</summary>
		/// <param name="provider">The provider to be added.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="provider" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> of <paramref name="provider" /> is <see langword="null" />.  
		/// -or-
		///  The length of the <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> of <paramref name="provider" /> is less than 1.</exception>
		// Token: 0x060003E9 RID: 1001 RVA: 0x0000AF14 File Offset: 0x00009114
		public virtual void Add(ProviderBase provider)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			if (provider == null || provider.Name == null)
			{
				throw new ArgumentNullException();
			}
			int num = this.values.Add(provider);
			try
			{
				this.lookup.Add(provider.Name, num);
			}
			catch
			{
				this.values.RemoveAt(num);
				throw;
			}
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is set to read-only.</exception>
		// Token: 0x060003EA RID: 1002 RVA: 0x0000AF88 File Offset: 0x00009188
		public void Clear()
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.values.Clear();
			this.lookup.Clear();
		}

		/// <summary>Copies the contents of the collection to the given array starting at the specified index.</summary>
		/// <param name="array">The array to copy the elements of the collection to.</param>
		/// <param name="index">The index of the collection item at which to start the copying process.</param>
		// Token: 0x060003EB RID: 1003 RVA: 0x0000AFAE File Offset: 0x000091AE
		public void CopyTo(ProviderBase[] array, int index)
		{
			this.values.CopyTo(array, index);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Configuration.Provider.ProviderCollection" /> to an array, starting at a particular array index.</summary>
		/// <param name="array">The array to copy the elements of the collection to.</param>
		/// <param name="index">The index of the array at which to start copying provider instances from the collection.</param>
		// Token: 0x060003EC RID: 1004 RVA: 0x0000AFAE File Offset: 0x000091AE
		void ICollection.CopyTo(Array array, int index)
		{
			this.values.CopyTo(array, index);
		}

		/// <summary>Returns an object that implements the <see cref="T:System.Collections.IEnumerator" /> interface to iterate through the collection.</summary>
		/// <returns>An object that implements <see cref="T:System.Collections.IEnumerator" /> to iterate through the collection.</returns>
		// Token: 0x060003ED RID: 1005 RVA: 0x0000AFBD File Offset: 0x000091BD
		public IEnumerator GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		/// <summary>Removes a provider from the collection.</summary>
		/// <param name="name">The name of the provider to be removed.</param>
		/// <exception cref="T:System.NotSupportedException">The collection has been set to read-only.</exception>
		// Token: 0x060003EE RID: 1006 RVA: 0x0000AFCC File Offset: 0x000091CC
		public void Remove(string name)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			object obj = this.lookup[name];
			if (obj == null || !(obj is int))
			{
				throw new ArgumentException();
			}
			int num = (int)obj;
			if (num >= this.values.Count)
			{
				throw new ArgumentException();
			}
			this.values.RemoveAt(num);
			this.lookup.Remove(name);
			ArrayList arrayList = new ArrayList();
			foreach (object obj2 in this.lookup)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
				if ((int)dictionaryEntry.Value > num)
				{
					arrayList.Add(dictionaryEntry.Key);
				}
			}
			foreach (object obj3 in arrayList)
			{
				string key = (string)obj3;
				this.lookup[key] = (int)this.lookup[key] - 1;
			}
		}

		/// <summary>Sets the collection to be read-only.</summary>
		// Token: 0x060003EF RID: 1007 RVA: 0x0000B110 File Offset: 0x00009310
		public void SetReadOnly()
		{
			this.readOnly = true;
		}

		/// <summary>Gets the number of providers in the collection.</summary>
		/// <returns>The number of providers in the collection.</returns>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000B119 File Offset: 0x00009319
		public int Count
		{
			get
			{
				return this.values.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x000023BB File Offset: 0x000005BB
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the current object.</summary>
		/// <returns>The current object.</returns>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the provider with the specified name.</summary>
		/// <param name="name">The key by which the provider is identified.</param>
		/// <returns>The provider with the specified name.</returns>
		// Token: 0x17000126 RID: 294
		public ProviderBase this[string name]
		{
			get
			{
				object obj = this.lookup[name];
				if (obj == null)
				{
					return null;
				}
				return this.values[(int)obj] as ProviderBase;
			}
		}

		// Token: 0x04000163 RID: 355
		private Hashtable lookup;

		// Token: 0x04000164 RID: 356
		private bool readOnly;

		// Token: 0x04000165 RID: 357
		private ArrayList values;
	}
}
