using System;
using System.Collections;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Defines size and enumerators for a collection of <see cref="T:System.Diagnostics.EventLogEntry" /> instances.</summary>
	// Token: 0x0200025B RID: 603
	public class EventLogEntryCollection : ICollection, IEnumerable
	{
		// Token: 0x060012D6 RID: 4822 RVA: 0x00050956 File Offset: 0x0004EB56
		internal EventLogEntryCollection(EventLogImpl impl)
		{
			this._impl = impl;
		}

		/// <summary>Gets the number of entries in the event log (that is, the number of elements in the <see cref="T:System.Diagnostics.EventLogEntry" /> collection).</summary>
		/// <returns>The number of entries currently in the event log.</returns>
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00050965 File Offset: 0x0004EB65
		public int Count
		{
			get
			{
				return this._impl.EntryCount;
			}
		}

		/// <summary>Gets an entry in the event log, based on an index that starts at 0 (zero).</summary>
		/// <param name="index">The zero-based index that is associated with the event log entry.</param>
		/// <returns>The event log entry at the location that is specified by the <paramref name="index" /> parameter.</returns>
		// Token: 0x17000369 RID: 873
		public virtual EventLogEntry this[int index]
		{
			get
			{
				return this._impl[index];
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Diagnostics.EventLogEntryCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>
		///   <see langword="false" /> if access to the collection is not synchronized (thread-safe).</returns>
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Diagnostics.EventLogEntryCollection" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x000075E1 File Offset: 0x000057E1
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Diagnostics.EventLogEntryCollection" /> to an array of <see cref="T:System.Diagnostics.EventLogEntry" /> instances, starting at a particular array index.</summary>
		/// <param name="entries">The one-dimensional array of <see cref="T:System.Diagnostics.EventLogEntry" /> instances that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		// Token: 0x060012DB RID: 4827 RVA: 0x00050980 File Offset: 0x0004EB80
		public void CopyTo(EventLogEntry[] entries, int index)
		{
			EventLogEntry[] entries2 = this._impl.GetEntries();
			Array.Copy(entries2, 0, entries, index, entries2.Length);
		}

		/// <summary>Supports a simple iteration over the <see cref="T:System.Diagnostics.EventLogEntryCollection" /> object.</summary>
		/// <returns>An object that can be used to iterate over the collection.</returns>
		// Token: 0x060012DC RID: 4828 RVA: 0x000509A5 File Offset: 0x0004EBA5
		public IEnumerator GetEnumerator()
		{
			return new EventLogEntryCollection.EventLogEntryEnumerator(this._impl);
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements that are copied from the collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060012DD RID: 4829 RVA: 0x000509B4 File Offset: 0x0004EBB4
		void ICollection.CopyTo(Array array, int index)
		{
			EventLogEntry[] entries = this._impl.GetEntries();
			Array.Copy(entries, 0, array, index, entries.Length);
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal EventLogEntryCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000AC4 RID: 2756
		private readonly EventLogImpl _impl;

		// Token: 0x0200025C RID: 604
		private class EventLogEntryEnumerator : IEnumerator
		{
			// Token: 0x060012DF RID: 4831 RVA: 0x000509D9 File Offset: 0x0004EBD9
			internal EventLogEntryEnumerator(EventLogImpl impl)
			{
				this._impl = impl;
			}

			// Token: 0x1700036C RID: 876
			// (get) Token: 0x060012E0 RID: 4832 RVA: 0x000509EF File Offset: 0x0004EBEF
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x1700036D RID: 877
			// (get) Token: 0x060012E1 RID: 4833 RVA: 0x000509F7 File Offset: 0x0004EBF7
			public EventLogEntry Current
			{
				get
				{
					if (this._currentEntry != null)
					{
						return this._currentEntry;
					}
					throw new InvalidOperationException("No current EventLog entry available, cursor is located before the first or after the last element of the enumeration.");
				}
			}

			// Token: 0x060012E2 RID: 4834 RVA: 0x00050A14 File Offset: 0x0004EC14
			public bool MoveNext()
			{
				this._currentIndex++;
				if (this._currentIndex >= this._impl.EntryCount)
				{
					this._currentEntry = null;
					return false;
				}
				this._currentEntry = this._impl[this._currentIndex];
				return true;
			}

			// Token: 0x060012E3 RID: 4835 RVA: 0x00050A63 File Offset: 0x0004EC63
			public void Reset()
			{
				this._currentIndex = -1;
			}

			// Token: 0x04000AC5 RID: 2757
			private readonly EventLogImpl _impl;

			// Token: 0x04000AC6 RID: 2758
			private int _currentIndex = -1;

			// Token: 0x04000AC7 RID: 2759
			private EventLogEntry _currentEntry;
		}
	}
}
