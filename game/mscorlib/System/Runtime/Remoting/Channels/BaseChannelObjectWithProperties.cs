using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides a base implementation of a channel object that exposes a dictionary interface to its properties.</summary>
	// Token: 0x020005A4 RID: 1444
	[ComVisible(true)]
	public abstract class BaseChannelObjectWithProperties : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.BaseChannelObjectWithProperties" /> class.</summary>
		// Token: 0x06003814 RID: 14356 RVA: 0x000C9469 File Offset: 0x000C7669
		protected BaseChannelObjectWithProperties()
		{
			this.table = new Hashtable();
		}

		/// <summary>Gets the number of properties associated with the channel object.</summary>
		/// <returns>The number of properties associated with the channel object.</returns>
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06003815 RID: 14357 RVA: 0x000C947C File Offset: 0x000C767C
		public virtual int Count
		{
			[SecuritySafeCritical]
			get
			{
				return this.table.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the number of properties that can be entered into the channel object is fixed.</summary>
		/// <returns>
		///   <see langword="true" /> if the number of properties that can be entered into the channel object is fixed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06003816 RID: 14358 RVA: 0x000040F7 File Offset: 0x000022F7
		public virtual bool IsFixedSize
		{
			[SecuritySafeCritical]
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether the collection of properties in the channel object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection of properties in the channel object is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06003817 RID: 14359 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsReadOnly
		{
			[SecuritySafeCritical]
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the dictionary of channel object properties is synchronized.</summary>
		/// <returns>
		///   <see langword="true" /> if the dictionary of channel object properties is synchronized; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsSynchronized
		{
			[SecuritySafeCritical]
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the property that is associated with the specified key.</summary>
		/// <param name="key">The key of the property to get or set.</param>
		/// <returns>The property that is associated with the specified key.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is accessed.</exception>
		// Token: 0x170007E9 RID: 2025
		public virtual object this[object key]
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
			[SecuritySafeCritical]
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a <see cref="T:System.Collections.ICollection" /> of keys that the channel object properties are associated with.</summary>
		/// <returns>A <see cref="T:System.Collections.ICollection" /> of keys that the channel object properties are associated with.</returns>
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x0600381B RID: 14363 RVA: 0x000C9489 File Offset: 0x000C7689
		public virtual ICollection Keys
		{
			[SecuritySafeCritical]
			get
			{
				return this.table.Keys;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.IDictionary" /> of the channel properties associated with the channel object.</summary>
		/// <returns>A <see cref="T:System.Collections.IDictionary" /> of the channel properties associated with the channel object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x0600381C RID: 14364 RVA: 0x0000270D File Offset: 0x0000090D
		public virtual IDictionary Properties
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets an object that is used to synchronize access to the <see cref="T:System.Runtime.Remoting.Channels.BaseChannelObjectWithProperties" />.</summary>
		/// <returns>An object that is used to synchronize access to the <see cref="T:System.Runtime.Remoting.Channels.BaseChannelObjectWithProperties" />.</returns>
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600381D RID: 14365 RVA: 0x0000270D File Offset: 0x0000090D
		public virtual object SyncRoot
		{
			[SecuritySafeCritical]
			get
			{
				return this;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.ICollection" /> of the values of the properties associated with the channel object.</summary>
		/// <returns>A <see cref="T:System.Collections.ICollection" /> of the values of the properties associated with the channel object.</returns>
		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x0600381E RID: 14366 RVA: 0x000C9496 File Offset: 0x000C7696
		public virtual ICollection Values
		{
			[SecuritySafeCritical]
			get
			{
				return this.table.Values;
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="key">The key that is associated with the object in the <paramref name="value" /> parameter.</param>
		/// <param name="value">The value to add.</param>
		/// <exception cref="T:System.NotSupportedException">The method is called.</exception>
		// Token: 0x0600381F RID: 14367 RVA: 0x000472C8 File Offset: 0x000454C8
		[SecuritySafeCritical]
		public virtual void Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The method is called.</exception>
		// Token: 0x06003820 RID: 14368 RVA: 0x000472C8 File Offset: 0x000454C8
		[SecuritySafeCritical]
		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns a value that indicates whether the channel object contains a property that is associated with the specified key.</summary>
		/// <param name="key">The key of the property to look for.</param>
		/// <returns>
		///   <see langword="true" /> if the channel object contains a property associated with the specified key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003821 RID: 14369 RVA: 0x000C94A3 File Offset: 0x000C76A3
		[SecuritySafeCritical]
		public virtual bool Contains(object key)
		{
			return this.table.Contains(key);
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="array">The array to copy the properties to.</param>
		/// <param name="index">The index at which to begin copying.</param>
		/// <exception cref="T:System.NotSupportedException">The method is called.</exception>
		// Token: 0x06003822 RID: 14370 RVA: 0x000472C8 File Offset: 0x000454C8
		[SecuritySafeCritical]
		public virtual void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns a <see cref="T:System.Collections.IDictionaryEnumerator" /> that enumerates over all the properties associated with the channel object.</summary>
		/// <returns>A <see cref="T:System.Collections.IDictionaryEnumerator" /> that enumerates over all the properties associated with the channel object.</returns>
		// Token: 0x06003823 RID: 14371 RVA: 0x000C94B1 File Offset: 0x000C76B1
		[SecuritySafeCritical]
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return this.table.GetEnumerator();
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> that enumerates over all the properties that are associated with the channel object.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> that enumerates over all the properties that are associated with the channel object.</returns>
		// Token: 0x06003824 RID: 14372 RVA: 0x000C94B1 File Offset: 0x000C76B1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.table.GetEnumerator();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="key">The key of the object to be removed.</param>
		/// <exception cref="T:System.NotSupportedException">The method is called.</exception>
		// Token: 0x06003825 RID: 14373 RVA: 0x000472C8 File Offset: 0x000454C8
		[SecuritySafeCritical]
		public virtual void Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040025CD RID: 9677
		private Hashtable table;
	}
}
