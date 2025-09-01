using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Provides a strongly typed collection of <see cref="T:System.Diagnostics.InstanceDataCollection" /> objects.</summary>
	// Token: 0x0200026B RID: 619
	public class InstanceDataCollectionCollection : DictionaryBase
	{
		// Token: 0x06001378 RID: 4984 RVA: 0x000517FF File Offset: 0x0004F9FF
		private static void CheckNull(object value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.InstanceDataCollectionCollection" /> class.</summary>
		// Token: 0x06001379 RID: 4985 RVA: 0x0005188D File Offset: 0x0004FA8D
		[Obsolete("Use PerformanceCounterCategory.ReadCategory()")]
		public InstanceDataCollectionCollection()
		{
		}

		/// <summary>Gets the instance data for the specified counter.</summary>
		/// <param name="counterName">The name of the performance counter.</param>
		/// <returns>An <see cref="T:System.Diagnostics.InstanceDataCollection" /> item, by which the <see cref="T:System.Diagnostics.InstanceDataCollectionCollection" /> object is indexed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x170003A6 RID: 934
		public InstanceDataCollection this[string counterName]
		{
			get
			{
				InstanceDataCollectionCollection.CheckNull(counterName, "counterName");
				return (InstanceDataCollection)base.Dictionary[counterName];
			}
		}

		/// <summary>Gets the object and counter registry keys for the objects associated with this instance data collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents a set of object-specific registry keys.</returns>
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x0005184B File Offset: 0x0004FA4B
		public ICollection Keys
		{
			get
			{
				return base.Dictionary.Keys;
			}
		}

		/// <summary>Gets the instance data values that comprise the collection of instances for the counter.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents the counter's instances and their associated data values.</returns>
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x00051858 File Offset: 0x0004FA58
		public ICollection Values
		{
			get
			{
				return base.Dictionary.Values;
			}
		}

		/// <summary>Determines whether an instance data collection for the specified counter (identified by one of the indexed <see cref="T:System.Diagnostics.InstanceDataCollection" /> objects) exists in the collection.</summary>
		/// <param name="counterName">The name of the performance counter.</param>
		/// <returns>
		///   <see langword="true" /> if an instance data collection containing the specified counter exists in the collection; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600137D RID: 4989 RVA: 0x000518B3 File Offset: 0x0004FAB3
		public bool Contains(string counterName)
		{
			InstanceDataCollectionCollection.CheckNull(counterName, "counterName");
			return base.Dictionary.Contains(counterName);
		}

		/// <summary>Copies an array of <see cref="T:System.Diagnostics.InstanceDataCollection" /> instances to the collection, at the specified index.</summary>
		/// <param name="counters">An array of <see cref="T:System.Diagnostics.InstanceDataCollection" /> instances (identified by the counters they contain) to add to the collection.</param>
		/// <param name="index">The location at which to add the new instances.</param>
		// Token: 0x0600137E RID: 4990 RVA: 0x0005187E File Offset: 0x0004FA7E
		public void CopyTo(InstanceDataCollection[] counters, int index)
		{
			base.Dictionary.CopyTo(counters, index);
		}
	}
}
