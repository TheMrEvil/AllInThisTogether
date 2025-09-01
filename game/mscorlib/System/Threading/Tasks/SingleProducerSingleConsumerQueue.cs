using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200034F RID: 847
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SingleProducerSingleConsumerQueue<>.SingleProducerSingleConsumerQueue_DebugView))]
	internal sealed class SingleProducerSingleConsumerQueue<T> : IProducerConsumerQueue<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06002364 RID: 9060 RVA: 0x0007E938 File Offset: 0x0007CB38
		internal SingleProducerSingleConsumerQueue()
		{
			this.m_head = (this.m_tail = new SingleProducerSingleConsumerQueue<T>.Segment(32));
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x0007E968 File Offset: 0x0007CB68
		public void Enqueue(T item)
		{
			SingleProducerSingleConsumerQueue<T>.Segment tail = this.m_tail;
			T[] array = tail.m_array;
			int last = tail.m_state.m_last;
			int num = last + 1 & array.Length - 1;
			if (num != tail.m_state.m_firstCopy)
			{
				array[last] = item;
				tail.m_state.m_last = num;
				return;
			}
			this.EnqueueSlow(item, ref tail);
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x0007E9CC File Offset: 0x0007CBCC
		private void EnqueueSlow(T item, ref SingleProducerSingleConsumerQueue<T>.Segment segment)
		{
			if (segment.m_state.m_firstCopy != segment.m_state.m_first)
			{
				segment.m_state.m_firstCopy = segment.m_state.m_first;
				this.Enqueue(item);
				return;
			}
			int num = this.m_tail.m_array.Length << 1;
			if (num > 16777216)
			{
				num = 16777216;
			}
			SingleProducerSingleConsumerQueue<T>.Segment segment2 = new SingleProducerSingleConsumerQueue<T>.Segment(num);
			segment2.m_array[0] = item;
			segment2.m_state.m_last = 1;
			segment2.m_state.m_lastCopy = 1;
			try
			{
			}
			finally
			{
				Volatile.Write<SingleProducerSingleConsumerQueue<T>.Segment>(ref this.m_tail.m_next, segment2);
				this.m_tail = segment2;
			}
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x0007EA94 File Offset: 0x0007CC94
		public bool TryDequeue(out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first != head.m_state.m_lastCopy)
			{
				result = array[first];
				array[first] = default(T);
				head.m_state.m_first = (first + 1 & array.Length - 1);
				return true;
			}
			return this.TryDequeueSlow(ref head, ref array, out result);
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x0007EB10 File Offset: 0x0007CD10
		private bool TryDequeueSlow(ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryDequeue(out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			array[first] = default(T);
			segment.m_state.m_first = (first + 1 & segment.m_array.Length - 1);
			segment.m_state.m_lastCopy = segment.m_state.m_last;
			return true;
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x0007EC20 File Offset: 0x0007CE20
		public bool TryPeek(out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first != head.m_state.m_lastCopy)
			{
				result = array[first];
				return true;
			}
			return this.TryPeekSlow(ref head, ref array, out result);
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x0007EC74 File Offset: 0x0007CE74
		private bool TryPeekSlow(ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryPeek(out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			return true;
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x0007ED3C File Offset: 0x0007CF3C
		public bool TryDequeueIf(Predicate<T> predicate, out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first == head.m_state.m_lastCopy)
			{
				return this.TryDequeueIfSlow(predicate, ref head, ref array, out result);
			}
			result = array[first];
			if (predicate == null || predicate(result))
			{
				array[first] = default(T);
				head.m_state.m_first = (first + 1 & array.Length - 1);
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x0007EDD0 File Offset: 0x0007CFD0
		private bool TryDequeueIfSlow(Predicate<T> predicate, ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryDequeueIf(predicate, out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			if (predicate == null || predicate(result))
			{
				array[first] = default(T);
				segment.m_state.m_first = (first + 1 & segment.m_array.Length - 1);
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x0007EF00 File Offset: 0x0007D100
		public void Clear()
		{
			T t;
			while (this.TryDequeue(out t))
			{
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600236E RID: 9070 RVA: 0x0007EF18 File Offset: 0x0007D118
		public bool IsEmpty
		{
			get
			{
				SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
				return head.m_state.m_first == head.m_state.m_lastCopy && head.m_state.m_first == head.m_state.m_last && head.m_next == null;
			}
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x0007EF71 File Offset: 0x0007D171
		public IEnumerator<T> GetEnumerator()
		{
			SingleProducerSingleConsumerQueue<T>.Segment segment;
			for (segment = this.m_head; segment != null; segment = segment.m_next)
			{
				for (int pt = segment.m_state.m_first; pt != segment.m_state.m_last; pt = (pt + 1 & segment.m_array.Length - 1))
				{
					yield return segment.m_array[pt];
				}
			}
			segment = null;
			yield break;
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x0007EF80 File Offset: 0x0007D180
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06002371 RID: 9073 RVA: 0x0007EF88 File Offset: 0x0007D188
		public int Count
		{
			get
			{
				int num = 0;
				for (SingleProducerSingleConsumerQueue<T>.Segment segment = this.m_head; segment != null; segment = segment.m_next)
				{
					int num2 = segment.m_array.Length;
					int first;
					int last;
					do
					{
						first = segment.m_state.m_first;
						last = segment.m_state.m_last;
					}
					while (first != segment.m_state.m_first);
					num += (last - first & num2 - 1);
				}
				return num;
			}
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x0007EFF0 File Offset: 0x0007D1F0
		int IProducerConsumerQueue<!0>.GetCountSafe(object syncObj)
		{
			int count;
			lock (syncObj)
			{
				count = this.Count;
			}
			return count;
		}

		// Token: 0x04001CAB RID: 7339
		private const int INIT_SEGMENT_SIZE = 32;

		// Token: 0x04001CAC RID: 7340
		private const int MAX_SEGMENT_SIZE = 16777216;

		// Token: 0x04001CAD RID: 7341
		private volatile SingleProducerSingleConsumerQueue<T>.Segment m_head;

		// Token: 0x04001CAE RID: 7342
		private volatile SingleProducerSingleConsumerQueue<T>.Segment m_tail;

		// Token: 0x02000350 RID: 848
		[StructLayout(LayoutKind.Sequential)]
		private sealed class Segment
		{
			// Token: 0x06002373 RID: 9075 RVA: 0x0007F030 File Offset: 0x0007D230
			internal Segment(int size)
			{
				this.m_array = new T[size];
			}

			// Token: 0x04001CAF RID: 7343
			internal SingleProducerSingleConsumerQueue<T>.Segment m_next;

			// Token: 0x04001CB0 RID: 7344
			internal readonly T[] m_array;

			// Token: 0x04001CB1 RID: 7345
			internal SingleProducerSingleConsumerQueue<T>.SegmentState m_state;
		}

		// Token: 0x02000351 RID: 849
		private struct SegmentState
		{
			// Token: 0x04001CB2 RID: 7346
			internal PaddingFor32 m_pad0;

			// Token: 0x04001CB3 RID: 7347
			internal volatile int m_first;

			// Token: 0x04001CB4 RID: 7348
			internal int m_lastCopy;

			// Token: 0x04001CB5 RID: 7349
			internal PaddingFor32 m_pad1;

			// Token: 0x04001CB6 RID: 7350
			internal int m_firstCopy;

			// Token: 0x04001CB7 RID: 7351
			internal volatile int m_last;

			// Token: 0x04001CB8 RID: 7352
			internal PaddingFor32 m_pad2;
		}

		// Token: 0x02000352 RID: 850
		private sealed class SingleProducerSingleConsumerQueue_DebugView
		{
			// Token: 0x06002374 RID: 9076 RVA: 0x0007F044 File Offset: 0x0007D244
			public SingleProducerSingleConsumerQueue_DebugView(SingleProducerSingleConsumerQueue<T> queue)
			{
				this.m_queue = queue;
			}

			// Token: 0x1700043B RID: 1083
			// (get) Token: 0x06002375 RID: 9077 RVA: 0x0007F054 File Offset: 0x0007D254
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public T[] Items
			{
				get
				{
					LowLevelList<T> lowLevelList = new LowLevelList<T>();
					foreach (T item in this.m_queue)
					{
						lowLevelList.Add(item);
					}
					return lowLevelList.ToArray();
				}
			}

			// Token: 0x04001CB9 RID: 7353
			private readonly SingleProducerSingleConsumerQueue<T> m_queue;
		}

		// Token: 0x02000353 RID: 851
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__16 : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06002376 RID: 9078 RVA: 0x0007F0B0 File Offset: 0x0007D2B0
			[DebuggerHidden]
			public <GetEnumerator>d__16(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06002377 RID: 9079 RVA: 0x00004BF9 File Offset: 0x00002DF9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06002378 RID: 9080 RVA: 0x0007F0C0 File Offset: 0x0007D2C0
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				SingleProducerSingleConsumerQueue<T> singleProducerSingleConsumerQueue = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					segment = singleProducerSingleConsumerQueue.m_head;
					goto IL_C0;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				pt = (pt + 1 & segment.m_array.Length - 1);
				IL_95:
				if (pt != segment.m_state.m_last)
				{
					this.<>2__current = segment.m_array[pt];
					this.<>1__state = 1;
					return true;
				}
				segment = segment.m_next;
				IL_C0:
				if (segment == null)
				{
					segment = null;
					return false;
				}
				pt = segment.m_state.m_first;
				goto IL_95;
			}

			// Token: 0x1700043C RID: 1084
			// (get) Token: 0x06002379 RID: 9081 RVA: 0x0007F1A0 File Offset: 0x0007D3A0
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600237A RID: 9082 RVA: 0x000472C8 File Offset: 0x000454C8
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700043D RID: 1085
			// (get) Token: 0x0600237B RID: 9083 RVA: 0x0007F1A8 File Offset: 0x0007D3A8
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04001CBA RID: 7354
			private int <>1__state;

			// Token: 0x04001CBB RID: 7355
			private T <>2__current;

			// Token: 0x04001CBC RID: 7356
			public SingleProducerSingleConsumerQueue<T> <>4__this;

			// Token: 0x04001CBD RID: 7357
			private SingleProducerSingleConsumerQueue<T>.Segment <segment>5__2;

			// Token: 0x04001CBE RID: 7358
			private int <pt>5__3;
		}
	}
}
