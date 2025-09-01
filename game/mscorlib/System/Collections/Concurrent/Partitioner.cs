using System;
using System.Collections.Generic;

namespace System.Collections.Concurrent
{
	/// <summary>Represents a particular manner of splitting a data source into multiple partitions.</summary>
	/// <typeparam name="TSource">Type of the elements in the collection.</typeparam>
	// Token: 0x02000A66 RID: 2662
	public abstract class Partitioner<TSource>
	{
		/// <summary>Partitions the underlying collection into the given number of partitions.</summary>
		/// <param name="partitionCount">The number of partitions to create.</param>
		/// <returns>A list containing <paramref name="partitionCount" /> enumerators.</returns>
		// Token: 0x06005F90 RID: 24464
		public abstract IList<IEnumerator<TSource>> GetPartitions(int partitionCount);

		/// <summary>Gets whether additional partitions can be created dynamically.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Concurrent.Partitioner`1" /> can create partitions dynamically as they are requested; <see langword="false" /> if the <see cref="T:System.Collections.Concurrent.Partitioner`1" /> can only allocate partitions statically.</returns>
		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x06005F91 RID: 24465 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool SupportsDynamicPartitions
		{
			get
			{
				return false;
			}
		}

		/// <summary>Creates an object that can partition the underlying collection into a variable number of partitions.</summary>
		/// <returns>An object that can create partitions over the underlying data source.</returns>
		/// <exception cref="T:System.NotSupportedException">Dynamic partitioning is not supported by the base class. You must implement it in a derived class.</exception>
		// Token: 0x06005F92 RID: 24466 RVA: 0x00140FD9 File Offset: 0x0013F1D9
		public virtual IEnumerable<TSource> GetDynamicPartitions()
		{
			throw new NotSupportedException("Dynamic partitions are not supported by this partitioner.");
		}

		/// <summary>Creates a new partitioner instance.</summary>
		// Token: 0x06005F93 RID: 24467 RVA: 0x0000259F File Offset: 0x0000079F
		protected Partitioner()
		{
		}
	}
}
