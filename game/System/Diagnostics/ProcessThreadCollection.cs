using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Provides a strongly typed collection of <see cref="T:System.Diagnostics.ProcessThread" /> objects.</summary>
	// Token: 0x02000246 RID: 582
	public class ProcessThreadCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessThreadCollection" /> class, with no associated <see cref="T:System.Diagnostics.ProcessThread" /> instances.</summary>
		// Token: 0x06001205 RID: 4613 RVA: 0x0004DD52 File Offset: 0x0004BF52
		protected ProcessThreadCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessThreadCollection" /> class, using the specified array of <see cref="T:System.Diagnostics.ProcessThread" /> instances.</summary>
		/// <param name="processThreads">An array of <see cref="T:System.Diagnostics.ProcessThread" /> instances with which to initialize this <see cref="T:System.Diagnostics.ProcessThreadCollection" /> instance.</param>
		// Token: 0x06001206 RID: 4614 RVA: 0x0004DD5A File Offset: 0x0004BF5A
		public ProcessThreadCollection(ProcessThread[] processThreads)
		{
			base.InnerList.AddRange(processThreads);
		}

		/// <summary>Gets an index for iterating over the set of process threads.</summary>
		/// <param name="index">The zero-based index value of the thread in the collection.</param>
		/// <returns>A <see cref="T:System.Diagnostics.ProcessThread" /> that indexes the threads in the collection.</returns>
		// Token: 0x1700033A RID: 826
		public ProcessThread this[int index]
		{
			get
			{
				return (ProcessThread)base.InnerList[index];
			}
		}

		/// <summary>Appends a process thread to the collection.</summary>
		/// <param name="thread">The thread to add to the collection.</param>
		/// <returns>The zero-based index of the thread in the collection.</returns>
		// Token: 0x06001208 RID: 4616 RVA: 0x0004E204 File Offset: 0x0004C404
		public int Add(ProcessThread thread)
		{
			return base.InnerList.Add(thread);
		}

		/// <summary>Inserts a process thread at the specified location in the collection.</summary>
		/// <param name="index">The zero-based index indicating the location at which to insert the thread.</param>
		/// <param name="thread">The thread to insert into the collection.</param>
		// Token: 0x06001209 RID: 4617 RVA: 0x0004E212 File Offset: 0x0004C412
		public void Insert(int index, ProcessThread thread)
		{
			base.InnerList.Insert(index, thread);
		}

		/// <summary>Provides the location of a specified thread within the collection.</summary>
		/// <param name="thread">The <see cref="T:System.Diagnostics.ProcessThread" /> whose index is retrieved.</param>
		/// <returns>The zero-based index that defines the location of the thread within the <see cref="T:System.Diagnostics.ProcessThreadCollection" />.</returns>
		// Token: 0x0600120A RID: 4618 RVA: 0x0004DD81 File Offset: 0x0004BF81
		public int IndexOf(ProcessThread thread)
		{
			return base.InnerList.IndexOf(thread);
		}

		/// <summary>Determines whether the specified process thread exists in the collection.</summary>
		/// <param name="thread">A <see cref="T:System.Diagnostics.ProcessThread" /> instance that indicates the thread to find in this collection.</param>
		/// <returns>
		///   <see langword="true" /> if the thread exists in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600120B RID: 4619 RVA: 0x0004DD8F File Offset: 0x0004BF8F
		public bool Contains(ProcessThread thread)
		{
			return base.InnerList.Contains(thread);
		}

		/// <summary>Deletes a process thread from the collection.</summary>
		/// <param name="thread">The thread to remove from the collection.</param>
		// Token: 0x0600120C RID: 4620 RVA: 0x0004E221 File Offset: 0x0004C421
		public void Remove(ProcessThread thread)
		{
			base.InnerList.Remove(thread);
		}

		/// <summary>Copies an array of <see cref="T:System.Diagnostics.ProcessThread" /> instances to the collection, at the specified index.</summary>
		/// <param name="array">An array of <see cref="T:System.Diagnostics.ProcessThread" /> instances to add to the collection.</param>
		/// <param name="index">The location at which to add the new instances.</param>
		// Token: 0x0600120D RID: 4621 RVA: 0x0004DD9D File Offset: 0x0004BF9D
		public void CopyTo(ProcessThread[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}
	}
}
