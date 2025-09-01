using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Provides a thread-safe list of <see cref="T:System.Diagnostics.TraceListener" /> objects.</summary>
	// Token: 0x02000232 RID: 562
	public class TraceListenerCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x060010BF RID: 4287 RVA: 0x00049171 File Offset: 0x00047371
		internal TraceListenerCollection()
		{
			this.list = new ArrayList(1);
		}

		/// <summary>Gets or sets the <see cref="T:System.Diagnostics.TraceListener" /> at the specified index.</summary>
		/// <param name="i">The zero-based index of the <see cref="T:System.Diagnostics.TraceListener" /> to get from the list.</param>
		/// <returns>A <see cref="T:System.Diagnostics.TraceListener" /> with the specified index.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is <see langword="null" />.</exception>
		// Token: 0x170002CB RID: 715
		public TraceListener this[int i]
		{
			get
			{
				return (TraceListener)this.list[i];
			}
			set
			{
				this.InitializeListener(value);
				this.list[i] = value;
			}
		}

		/// <summary>Gets the first <see cref="T:System.Diagnostics.TraceListener" /> in the list with the specified name.</summary>
		/// <param name="name">The name of the <see cref="T:System.Diagnostics.TraceListener" /> to get from the list.</param>
		/// <returns>The first <see cref="T:System.Diagnostics.TraceListener" /> in the list with the given <see cref="P:System.Diagnostics.TraceListener.Name" />. This item returns <see langword="null" /> if no <see cref="T:System.Diagnostics.TraceListener" /> with the given name can be found.</returns>
		// Token: 0x170002CC RID: 716
		public TraceListener this[string name]
		{
			get
			{
				foreach (object obj in this)
				{
					TraceListener traceListener = (TraceListener)obj;
					if (traceListener.Name == name)
					{
						return traceListener;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the number of listeners in the list.</summary>
		/// <returns>The number of listeners in the list.</returns>
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x00049214 File Offset: 0x00047414
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Adds a <see cref="T:System.Diagnostics.TraceListener" /> to the list.</summary>
		/// <param name="listener">A <see cref="T:System.Diagnostics.TraceListener" /> to add to the list.</param>
		/// <returns>The position at which the new listener was inserted.</returns>
		// Token: 0x060010C4 RID: 4292 RVA: 0x00049224 File Offset: 0x00047424
		public int Add(TraceListener listener)
		{
			this.InitializeListener(listener);
			object critSec = TraceInternal.critSec;
			int result;
			lock (critSec)
			{
				result = this.list.Add(listener);
			}
			return result;
		}

		/// <summary>Adds an array of <see cref="T:System.Diagnostics.TraceListener" /> objects to the list.</summary>
		/// <param name="value">An array of <see cref="T:System.Diagnostics.TraceListener" /> objects to add to the list.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x060010C5 RID: 4293 RVA: 0x00049274 File Offset: 0x00047474
		public void AddRange(TraceListener[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Adds the contents of another <see cref="T:System.Diagnostics.TraceListenerCollection" /> to the list.</summary>
		/// <param name="value">Another <see cref="T:System.Diagnostics.TraceListenerCollection" /> whose contents are added to the list.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x060010C6 RID: 4294 RVA: 0x000492A8 File Offset: 0x000474A8
		public void AddRange(TraceListenerCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Clears all the listeners from the list.</summary>
		// Token: 0x060010C7 RID: 4295 RVA: 0x000492E4 File Offset: 0x000474E4
		public void Clear()
		{
			this.list = new ArrayList();
		}

		/// <summary>Checks whether the list contains the specified listener.</summary>
		/// <param name="listener">A <see cref="T:System.Diagnostics.TraceListener" /> to find in the list.</param>
		/// <returns>
		///   <see langword="true" /> if the listener is in the list; otherwise, <see langword="false" />.</returns>
		// Token: 0x060010C8 RID: 4296 RVA: 0x000492F1 File Offset: 0x000474F1
		public bool Contains(TraceListener listener)
		{
			return ((IList)this).Contains(listener);
		}

		/// <summary>Copies a section of the current <see cref="T:System.Diagnostics.TraceListenerCollection" /> list to the specified array at the specified index.</summary>
		/// <param name="listeners">An array of type <see cref="T:System.Array" /> to copy the elements into.</param>
		/// <param name="index">The starting index number in the current list to copy from.</param>
		// Token: 0x060010C9 RID: 4297 RVA: 0x000492FA File Offset: 0x000474FA
		public void CopyTo(TraceListener[] listeners, int index)
		{
			((ICollection)this).CopyTo(listeners, index);
		}

		/// <summary>Gets an enumerator for this list.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x060010CA RID: 4298 RVA: 0x00049304 File Offset: 0x00047504
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00049311 File Offset: 0x00047511
		internal void InitializeListener(TraceListener listener)
		{
			if (listener == null)
			{
				throw new ArgumentNullException("listener");
			}
			listener.IndentSize = TraceInternal.IndentSize;
			listener.IndentLevel = TraceInternal.IndentLevel;
		}

		/// <summary>Gets the index of the specified listener.</summary>
		/// <param name="listener">A <see cref="T:System.Diagnostics.TraceListener" /> to find in the list.</param>
		/// <returns>The index of the listener, if it can be found in the list; otherwise, -1.</returns>
		// Token: 0x060010CC RID: 4300 RVA: 0x00049337 File Offset: 0x00047537
		public int IndexOf(TraceListener listener)
		{
			return ((IList)this).IndexOf(listener);
		}

		/// <summary>Inserts the listener at the specified index.</summary>
		/// <param name="index">The position in the list to insert the new <see cref="T:System.Diagnostics.TraceListener" />.</param>
		/// <param name="listener">A <see cref="T:System.Diagnostics.TraceListener" /> to insert in the list.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> is not a valid index in the list.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="listener" /> is <see langword="null" />.</exception>
		// Token: 0x060010CD RID: 4301 RVA: 0x00049340 File Offset: 0x00047540
		public void Insert(int index, TraceListener listener)
		{
			this.InitializeListener(listener);
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				this.list.Insert(index, listener);
			}
		}

		/// <summary>Removes from the collection the specified <see cref="T:System.Diagnostics.TraceListener" />.</summary>
		/// <param name="listener">A <see cref="T:System.Diagnostics.TraceListener" /> to remove from the list.</param>
		// Token: 0x060010CE RID: 4302 RVA: 0x00049390 File Offset: 0x00047590
		public void Remove(TraceListener listener)
		{
			((IList)this).Remove(listener);
		}

		/// <summary>Removes from the collection the first <see cref="T:System.Diagnostics.TraceListener" /> with the specified name.</summary>
		/// <param name="name">The name of the <see cref="T:System.Diagnostics.TraceListener" /> to remove from the list.</param>
		// Token: 0x060010CF RID: 4303 RVA: 0x0004939C File Offset: 0x0004759C
		public void Remove(string name)
		{
			TraceListener traceListener = this[name];
			if (traceListener != null)
			{
				((IList)this).Remove(traceListener);
			}
		}

		/// <summary>Removes from the collection the <see cref="T:System.Diagnostics.TraceListener" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Diagnostics.TraceListener" /> to remove from the list.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> is not a valid index in the list.</exception>
		// Token: 0x060010D0 RID: 4304 RVA: 0x000493BC File Offset: 0x000475BC
		public void RemoveAt(int index)
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				this.list.RemoveAt(index);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Diagnostics.TraceListener" /> at the specified index in the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</summary>
		/// <param name="index">The zero-based index of the <paramref name="value" /> to get.</param>
		/// <returns>The <see cref="T:System.Diagnostics.TraceListener" /> at the specified index.</returns>
		// Token: 0x170002CE RID: 718
		object IList.this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				TraceListener traceListener = value as TraceListener;
				if (traceListener == null)
				{
					throw new ArgumentException(SR.GetString("Only TraceListeners can be added to a TraceListenerCollection."), "value");
				}
				this.InitializeListener(traceListener);
				this.list[index] = traceListener;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Diagnostics.TraceListenerCollection" /> is read-only</summary>
		/// <returns>Always <see langword="false" />.</returns>
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x00003062 File Offset: 0x00001262
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Diagnostics.TraceListenerCollection" /> has a fixed size.</summary>
		/// <returns>Always <see langword="false" />.</returns>
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x00003062 File Offset: 0x00001262
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds a trace listener to the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</summary>
		/// <param name="value">The object to add to the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</param>
		/// <returns>The position into which the new trace listener was inserted.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is not a <see cref="T:System.Diagnostics.TraceListener" />.</exception>
		// Token: 0x060010D5 RID: 4309 RVA: 0x00049454 File Offset: 0x00047654
		int IList.Add(object value)
		{
			TraceListener traceListener = value as TraceListener;
			if (traceListener == null)
			{
				throw new ArgumentException(SR.GetString("Only TraceListeners can be added to a TraceListenerCollection."), "value");
			}
			this.InitializeListener(traceListener);
			object critSec = TraceInternal.critSec;
			int result;
			lock (critSec)
			{
				result = this.list.Add(value);
			}
			return result;
		}

		/// <summary>Determines whether the <see cref="T:System.Diagnostics.TraceListenerCollection" /> contains a specific object.</summary>
		/// <param name="value">The object to locate in the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Diagnostics.TraceListenerCollection" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060010D6 RID: 4310 RVA: 0x000494C4 File Offset: 0x000476C4
		bool IList.Contains(object value)
		{
			return this.list.Contains(value);
		}

		/// <summary>Determines the index of a specific object in the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</summary>
		/// <param name="value">The object to locate in the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</param>
		/// <returns>The index of <paramref name="value" /> if found in the <see cref="T:System.Diagnostics.TraceListenerCollection" />; otherwise, -1.</returns>
		// Token: 0x060010D7 RID: 4311 RVA: 0x000494D2 File Offset: 0x000476D2
		int IList.IndexOf(object value)
		{
			return this.list.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.Diagnostics.TraceListener" /> object at the specified position in the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Diagnostics.TraceListener" /> object to insert into the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Diagnostics.TraceListener" /> object.</exception>
		// Token: 0x060010D8 RID: 4312 RVA: 0x000494E0 File Offset: 0x000476E0
		void IList.Insert(int index, object value)
		{
			TraceListener traceListener = value as TraceListener;
			if (traceListener == null)
			{
				throw new ArgumentException(SR.GetString("Only TraceListeners can be added to a TraceListenerCollection."), "value");
			}
			this.InitializeListener(traceListener);
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				this.list.Insert(index, value);
			}
		}

		/// <summary>Removes an object from the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</summary>
		/// <param name="value">The object to remove from the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</param>
		// Token: 0x060010D9 RID: 4313 RVA: 0x0004954C File Offset: 0x0004774C
		void IList.Remove(object value)
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				this.list.Remove(value);
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Diagnostics.TraceListenerCollection" />.</summary>
		/// <returns>The current <see cref="T:System.Diagnostics.TraceListenerCollection" /> object.</returns>
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x000075E1 File Offset: 0x000057E1
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Diagnostics.TraceListenerCollection" /> is synchronized (thread safe).</summary>
		/// <returns>Always <see langword="true" />.</returns>
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x0000390E File Offset: 0x00001B0E
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		/// <summary>Copies a section of the current <see cref="T:System.Diagnostics.TraceListenerCollection" /> to the specified array of <see cref="T:System.Diagnostics.TraceListener" /> objects.</summary>
		/// <param name="array">The one-dimensional array of <see cref="T:System.Diagnostics.TraceListener" /> objects that is the destination of the elements copied from the <see cref="T:System.Diagnostics.TraceListenerCollection" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060010DC RID: 4316 RVA: 0x00049594 File Offset: 0x00047794
		void ICollection.CopyTo(Array array, int index)
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				this.list.CopyTo(array, index);
			}
		}

		// Token: 0x040009FB RID: 2555
		private ArrayList list;
	}
}
