using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Provides a strongly typed collection of <see cref="T:System.Diagnostics.CounterCreationData" /> objects.</summary>
	// Token: 0x0200024D RID: 589
	[Serializable]
	public class CounterCreationDataCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> class, with no associated <see cref="T:System.Diagnostics.CounterCreationData" /> instances.</summary>
		// Token: 0x0600121D RID: 4637 RVA: 0x0004E358 File Offset: 0x0004C558
		public CounterCreationDataCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> class by using the specified array of <see cref="T:System.Diagnostics.CounterCreationData" /> instances.</summary>
		/// <param name="value">An array of <see cref="T:System.Diagnostics.CounterCreationData" /> instances with which to initialize this <see cref="T:System.Diagnostics.CounterCreationDataCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600121E RID: 4638 RVA: 0x0004E360 File Offset: 0x0004C560
		public CounterCreationDataCollection(CounterCreationData[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> class by using the specified collection of <see cref="T:System.Diagnostics.CounterCreationData" /> instances.</summary>
		/// <param name="value">A <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> that holds <see cref="T:System.Diagnostics.CounterCreationData" /> instances with which to initialize this <see cref="T:System.Diagnostics.CounterCreationDataCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600121F RID: 4639 RVA: 0x0004E36F File Offset: 0x0004C56F
		public CounterCreationDataCollection(CounterCreationDataCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Indexes the <see cref="T:System.Diagnostics.CounterCreationData" /> collection.</summary>
		/// <param name="index">An index into the <see cref="T:System.Diagnostics.CounterCreationDataCollection" />.</param>
		/// <returns>The collection index, which is used to access individual elements of the collection.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than the number of items in the collection.</exception>
		// Token: 0x1700033E RID: 830
		public CounterCreationData this[int index]
		{
			get
			{
				return (CounterCreationData)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		/// <summary>Adds an instance of the <see cref="T:System.Diagnostics.CounterCreationData" /> class to the collection.</summary>
		/// <param name="value">A <see cref="T:System.Diagnostics.CounterCreationData" /> object to append to the existing collection.</param>
		/// <returns>The index of the new <see cref="T:System.Diagnostics.CounterCreationData" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Diagnostics.CounterCreationData" /> object.</exception>
		// Token: 0x06001222 RID: 4642 RVA: 0x0004E3A0 File Offset: 0x0004C5A0
		public int Add(CounterCreationData value)
		{
			return base.InnerList.Add(value);
		}

		/// <summary>Adds the specified array of <see cref="T:System.Diagnostics.CounterCreationData" /> instances to the collection.</summary>
		/// <param name="value">An array of <see cref="T:System.Diagnostics.CounterCreationData" /> instances to append to the existing collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001223 RID: 4643 RVA: 0x0004E3B0 File Offset: 0x0004C5B0
		public void AddRange(CounterCreationData[] value)
		{
			foreach (CounterCreationData value2 in value)
			{
				this.Add(value2);
			}
		}

		/// <summary>Adds the specified collection of <see cref="T:System.Diagnostics.CounterCreationData" /> instances to the collection.</summary>
		/// <param name="value">A collection of <see cref="T:System.Diagnostics.CounterCreationData" /> instances to append to the existing collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001224 RID: 4644 RVA: 0x0004E3DC File Offset: 0x0004C5DC
		public void AddRange(CounterCreationDataCollection value)
		{
			foreach (object obj in value)
			{
				CounterCreationData value2 = (CounterCreationData)obj;
				this.Add(value2);
			}
		}

		/// <summary>Determines whether a <see cref="T:System.Diagnostics.CounterCreationData" /> instance exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.Diagnostics.CounterCreationData" /> object to find in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Diagnostics.CounterCreationData" /> object exists in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001225 RID: 4645 RVA: 0x0004E434 File Offset: 0x0004C634
		public bool Contains(CounterCreationData value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Diagnostics.CounterCreationData" /> to an array, starting at the specified index of the array.</summary>
		/// <param name="array">An array of <see cref="T:System.Diagnostics.CounterCreationData" /> instances to add to the collection.</param>
		/// <param name="index">The location at which to add the new instances.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		// Token: 0x06001226 RID: 4646 RVA: 0x0004E442 File Offset: 0x0004C642
		public void CopyTo(CounterCreationData[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		/// <summary>Returns the index of a <see cref="T:System.Diagnostics.CounterCreationData" /> object in the collection.</summary>
		/// <param name="value">The <see cref="T:System.Diagnostics.CounterCreationData" /> object to locate in the collection.</param>
		/// <returns>The zero-based index of the specified <see cref="T:System.Diagnostics.CounterCreationData" />, if it is found, in the collection; otherwise, -1.</returns>
		// Token: 0x06001227 RID: 4647 RVA: 0x0004E451 File Offset: 0x0004C651
		public int IndexOf(CounterCreationData value)
		{
			return base.InnerList.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.Diagnostics.CounterCreationData" /> object into the collection, at the specified index.</summary>
		/// <param name="index">The zero-based index of the location at which the <see cref="T:System.Diagnostics.CounterCreationData" /> is to be inserted.</param>
		/// <param name="value">The <see cref="T:System.Diagnostics.CounterCreationData" /> to insert into the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Diagnostics.CounterCreationData" /> object.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is greater than the number of items in the collection.</exception>
		// Token: 0x06001228 RID: 4648 RVA: 0x0004E45F File Offset: 0x0004C65F
		public void Insert(int index, CounterCreationData value)
		{
			base.InnerList.Insert(index, value);
		}

		/// <summary>Checks the specified object to determine whether it is a valid <see cref="T:System.Diagnostics.CounterCreationData" /> type.</summary>
		/// <param name="value">The object that will be validated.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Diagnostics.CounterCreationData" /> object.</exception>
		// Token: 0x06001229 RID: 4649 RVA: 0x0004E46E File Offset: 0x0004C66E
		protected override void OnValidate(object value)
		{
			if (!(value is CounterCreationData))
			{
				throw new NotSupportedException(Locale.GetText("You can only insert CounterCreationData objects into the collection"));
			}
		}

		/// <summary>Removes a <see cref="T:System.Diagnostics.CounterCreationData" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Diagnostics.CounterCreationData" /> to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Diagnostics.CounterCreationData" /> object.  
		/// -or-  
		/// <paramref name="value" /> does not exist in the collection.</exception>
		// Token: 0x0600122A RID: 4650 RVA: 0x0004E488 File Offset: 0x0004C688
		public virtual void Remove(CounterCreationData value)
		{
			base.InnerList.Remove(value);
		}
	}
}
