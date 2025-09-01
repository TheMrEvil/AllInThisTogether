using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Provides a strongly typed collection of <see cref="T:System.Diagnostics.InstanceData" /> objects.</summary>
	// Token: 0x0200026A RID: 618
	public class InstanceDataCollection : DictionaryBase
	{
		// Token: 0x06001370 RID: 4976 RVA: 0x000517FF File Offset: 0x0004F9FF
		private static void CheckNull(object value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.InstanceDataCollection" /> class, using the specified performance counter (which defines a performance instance).</summary>
		/// <param name="counterName">The name of the counter, which often describes the quantity that is being counted.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001371 RID: 4977 RVA: 0x0005180B File Offset: 0x0004FA0B
		[Obsolete("Use InstanceDataCollectionCollection indexer instead.")]
		public InstanceDataCollection(string counterName)
		{
			InstanceDataCollection.CheckNull(counterName, "counterName");
			this.counterName = counterName;
		}

		/// <summary>Gets the name of the performance counter whose instance data you want to get.</summary>
		/// <returns>The performance counter name.</returns>
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x00051825 File Offset: 0x0004FA25
		public string CounterName
		{
			get
			{
				return this.counterName;
			}
		}

		/// <summary>Gets the instance data associated with this counter. This is typically a set of raw counter values.</summary>
		/// <param name="instanceName">The name of the performance counter category instance, or an empty string ("") if the category contains a single instance.</param>
		/// <returns>An <see cref="T:System.Diagnostics.InstanceData" /> item, by which the <see cref="T:System.Diagnostics.InstanceDataCollection" /> object is indexed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x170003A3 RID: 931
		public InstanceData this[string instanceName]
		{
			get
			{
				InstanceDataCollection.CheckNull(instanceName, "instanceName");
				return (InstanceData)base.Dictionary[instanceName];
			}
		}

		/// <summary>Gets the object and counter registry keys for the objects associated with this instance data.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents a set of object-specific registry keys.</returns>
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x0005184B File Offset: 0x0004FA4B
		public ICollection Keys
		{
			get
			{
				return base.Dictionary.Keys;
			}
		}

		/// <summary>Gets the raw counter values that comprise the instance data for the counter.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents the counter's raw data values.</returns>
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x00051858 File Offset: 0x0004FA58
		public ICollection Values
		{
			get
			{
				return base.Dictionary.Values;
			}
		}

		/// <summary>Determines whether a performance instance with a specified name (identified by one of the indexed <see cref="T:System.Diagnostics.InstanceData" /> objects) exists in the collection.</summary>
		/// <param name="instanceName">The name of the instance to find in this collection.</param>
		/// <returns>
		///   <see langword="true" /> if the instance exists in the collection; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001376 RID: 4982 RVA: 0x00051865 File Offset: 0x0004FA65
		public bool Contains(string instanceName)
		{
			InstanceDataCollection.CheckNull(instanceName, "instanceName");
			return base.Dictionary.Contains(instanceName);
		}

		/// <summary>Copies the items in the collection to the specified one-dimensional array at the specified index.</summary>
		/// <param name="instances">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The zero-based index value at which to add the new instances.</param>
		// Token: 0x06001377 RID: 4983 RVA: 0x0005187E File Offset: 0x0004FA7E
		public void CopyTo(InstanceData[] instances, int index)
		{
			base.Dictionary.CopyTo(instances, index);
		}

		// Token: 0x04000B02 RID: 2818
		private string counterName;
	}
}
