using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Policy.ApplicationTrust" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000404 RID: 1028
	[ComVisible(true)]
	public sealed class ApplicationTrustCollection : ICollection, IEnumerable
	{
		// Token: 0x06002A01 RID: 10753 RVA: 0x000985AD File Offset: 0x000967AD
		internal ApplicationTrustCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>Gets the number of items contained in the collection.</summary>
		/// <returns>The number of items contained in the collection.</returns>
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x000985C0 File Offset: 0x000967C0
		public int Count
		{
			[SecuritySafeCritical]
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06002A03 RID: 10755 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsSynchronized
		{
			[SecuritySafeCritical]
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>The object to use to synchronize access to the collection.</returns>
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06002A04 RID: 10756 RVA: 0x0000270D File Offset: 0x0000090D
		public object SyncRoot
		{
			[SecuritySafeCritical]
			get
			{
				return this;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Policy.ApplicationTrust" /> object located at the specified index in the collection.</summary>
		/// <param name="index">The zero-based index of the object within the collection.</param>
		/// <returns>The <see cref="T:System.Security.Policy.ApplicationTrust" /> object at the specified index in the collection.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is greater than or equal to the count of objects in the collection.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="index" /> is negative.</exception>
		// Token: 0x1700052F RID: 1327
		public ApplicationTrust this[int index]
		{
			get
			{
				return (ApplicationTrust)this._list[index];
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Policy.ApplicationTrust" /> object for the specified application.</summary>
		/// <param name="appFullName">The full name of the application.</param>
		/// <returns>The <see cref="T:System.Security.Policy.ApplicationTrust" /> object for the specified application, or <see langword="null" /> if the object cannot be found.</returns>
		// Token: 0x17000530 RID: 1328
		public ApplicationTrust this[string appFullName]
		{
			get
			{
				for (int i = 0; i < this._list.Count; i++)
				{
					ApplicationTrust applicationTrust = this._list[i] as ApplicationTrust;
					if (applicationTrust.ApplicationIdentity.FullName == appFullName)
					{
						return applicationTrust;
					}
				}
				return null;
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <param name="trust">The <see cref="T:System.Security.Policy.ApplicationTrust" /> object to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="trust" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> property of the <see cref="T:System.Security.Policy.ApplicationTrust" /> specified in <paramref name="trust" /> is <see langword="null" />.</exception>
		// Token: 0x06002A07 RID: 10759 RVA: 0x0009862B File Offset: 0x0009682B
		public int Add(ApplicationTrust trust)
		{
			if (trust == null)
			{
				throw new ArgumentNullException("trust");
			}
			if (trust.ApplicationIdentity == null)
			{
				throw new ArgumentException(Locale.GetText("ApplicationTrust.ApplicationIdentity can't be null."), "trust");
			}
			return this._list.Add(trust);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Security.Policy.ApplicationTrust" /> array to the end of the collection.</summary>
		/// <param name="trusts">An array of type <see cref="T:System.Security.Policy.ApplicationTrust" /> containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="trusts" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> property of an <see cref="T:System.Security.Policy.ApplicationTrust" /> specified in <paramref name="trust" /> is <see langword="null" />.</exception>
		// Token: 0x06002A08 RID: 10760 RVA: 0x00098664 File Offset: 0x00096864
		public void AddRange(ApplicationTrust[] trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			foreach (ApplicationTrust applicationTrust in trusts)
			{
				if (applicationTrust.ApplicationIdentity == null)
				{
					throw new ArgumentException(Locale.GetText("ApplicationTrust.ApplicationIdentity can't be null."), "trust");
				}
				this._list.Add(applicationTrust);
			}
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> to the end of the collection.</summary>
		/// <param name="trusts">A <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="trusts" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> property of an <see cref="T:System.Security.Policy.ApplicationTrust" /> specified in <paramref name="trust" /> is <see langword="null" />.</exception>
		// Token: 0x06002A09 RID: 10761 RVA: 0x000986C0 File Offset: 0x000968C0
		public void AddRange(ApplicationTrustCollection trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			foreach (ApplicationTrust applicationTrust in trusts)
			{
				if (applicationTrust.ApplicationIdentity == null)
				{
					throw new ArgumentException(Locale.GetText("ApplicationTrust.ApplicationIdentity can't be null."), "trust");
				}
				this._list.Add(applicationTrust);
			}
		}

		/// <summary>Removes all the application trusts from the collection.</summary>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> property of an item in the collection is <see langword="null" />.</exception>
		// Token: 0x06002A0A RID: 10762 RVA: 0x0009871D File Offset: 0x0009691D
		public void Clear()
		{
			this._list.Clear();
		}

		/// <summary>Copies the entire collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional array of type <see cref="T:System.Security.Policy.ApplicationTrust" /> that is the destination of the elements copied from the current collection.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06002A0B RID: 10763 RVA: 0x0009872A File Offset: 0x0009692A
		public void CopyTo(ApplicationTrust[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to the specified <see cref="T:System.Array" />, starting at the specified <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06002A0C RID: 10764 RVA: 0x0009872A File Offset: 0x0009692A
		void ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Gets the application trusts in the collection that match the specified application identity.</summary>
		/// <param name="applicationIdentity">An <see cref="T:System.ApplicationIdentity" /> object describing the application to find.</param>
		/// <param name="versionMatch">One of the <see cref="T:System.Security.Policy.ApplicationVersionMatch" /> values.</param>
		/// <returns>An <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> containing all matching <see cref="T:System.Security.Policy.ApplicationTrust" /> objects.</returns>
		// Token: 0x06002A0D RID: 10765 RVA: 0x0009873C File Offset: 0x0009693C
		public ApplicationTrustCollection Find(ApplicationIdentity applicationIdentity, ApplicationVersionMatch versionMatch)
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			string text = applicationIdentity.FullName;
			if (versionMatch != ApplicationVersionMatch.MatchExactVersion)
			{
				if (versionMatch != ApplicationVersionMatch.MatchAllVersions)
				{
					throw new ArgumentException("versionMatch");
				}
				int num = text.IndexOf(", Version=");
				if (num >= 0)
				{
					text = text.Substring(0, num);
				}
			}
			ApplicationTrustCollection applicationTrustCollection = new ApplicationTrustCollection();
			foreach (object obj in this._list)
			{
				ApplicationTrust applicationTrust = (ApplicationTrust)obj;
				if (applicationTrust.ApplicationIdentity.FullName.StartsWith(text))
				{
					applicationTrustCollection.Add(applicationTrust);
				}
			}
			return applicationTrustCollection;
		}

		/// <summary>Returns an object that can be used to iterate over the collection.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.ApplicationTrustEnumerator" /> that can be used to iterate over the collection.</returns>
		// Token: 0x06002A0E RID: 10766 RVA: 0x000987F8 File Offset: 0x000969F8
		public ApplicationTrustEnumerator GetEnumerator()
		{
			return new ApplicationTrustEnumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06002A0F RID: 10767 RVA: 0x000987F8 File Offset: 0x000969F8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ApplicationTrustEnumerator(this);
		}

		/// <summary>Removes the specified application trust from the collection.</summary>
		/// <param name="trust">The <see cref="T:System.Security.Policy.ApplicationTrust" /> object to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="trust" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> property of the <see cref="T:System.Security.Policy.ApplicationTrust" /> object specified by <paramref name="trust" /> is <see langword="null" />.</exception>
		// Token: 0x06002A10 RID: 10768 RVA: 0x00098800 File Offset: 0x00096A00
		public void Remove(ApplicationTrust trust)
		{
			if (trust == null)
			{
				throw new ArgumentNullException("trust");
			}
			if (trust.ApplicationIdentity == null)
			{
				throw new ArgumentException(Locale.GetText("ApplicationTrust.ApplicationIdentity can't be null."), "trust");
			}
			this.RemoveAllInstances(trust);
		}

		/// <summary>Removes all application trust objects that match the specified criteria from the collection.</summary>
		/// <param name="applicationIdentity">The <see cref="T:System.ApplicationIdentity" /> of the <see cref="T:System.Security.Policy.ApplicationTrust" /> object to be removed.</param>
		/// <param name="versionMatch">One of the <see cref="T:System.Security.Policy.ApplicationVersionMatch" /> values.</param>
		// Token: 0x06002A11 RID: 10769 RVA: 0x00098834 File Offset: 0x00096A34
		public void Remove(ApplicationIdentity applicationIdentity, ApplicationVersionMatch versionMatch)
		{
			foreach (ApplicationTrust trust in this.Find(applicationIdentity, versionMatch))
			{
				this.RemoveAllInstances(trust);
			}
		}

		/// <summary>Removes the application trust objects in the specified array from the collection.</summary>
		/// <param name="trusts">A one-dimensional array of type <see cref="T:System.Security.Policy.ApplicationTrust" /> that contains items to be removed from the current collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="trusts" /> is <see langword="null" />.</exception>
		// Token: 0x06002A12 RID: 10770 RVA: 0x00098868 File Offset: 0x00096A68
		public void RemoveRange(ApplicationTrust[] trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			foreach (ApplicationTrust trust in trusts)
			{
				this.RemoveAllInstances(trust);
			}
		}

		/// <summary>Removes the application trust objects in the specified collection from the collection.</summary>
		/// <param name="trusts">An <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> that contains items to be removed from the currentcollection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="trusts" /> is <see langword="null" />.</exception>
		// Token: 0x06002A13 RID: 10771 RVA: 0x000988A0 File Offset: 0x00096AA0
		public void RemoveRange(ApplicationTrustCollection trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			foreach (ApplicationTrust trust in trusts)
			{
				this.RemoveAllInstances(trust);
			}
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x000988DC File Offset: 0x00096ADC
		internal void RemoveAllInstances(ApplicationTrust trust)
		{
			for (int i = this._list.Count - 1; i >= 0; i--)
			{
				if (trust.Equals(this._list[i]))
				{
					this._list.RemoveAt(i);
				}
			}
		}

		// Token: 0x04001F61 RID: 8033
		private ArrayList _list;
	}
}
