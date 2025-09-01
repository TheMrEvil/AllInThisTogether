using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x0200012C RID: 300
	internal sealed class Listeners<TElem> where TElem : class
	{
		// Token: 0x0600107C RID: 4220 RVA: 0x000451D5 File Offset: 0x000433D5
		internal Listeners(int ObjectID, Listeners<TElem>.Func<TElem, bool> notifyFilter)
		{
			this._listeners = new List<TElem>();
			this._filter = notifyFilter;
			this._objectID = ObjectID;
			this._listenerReaderCount = 0;
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x000451FD File Offset: 0x000433FD
		internal bool HasListeners
		{
			get
			{
				return 0 < this._listeners.Count;
			}
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0004520D File Offset: 0x0004340D
		internal void Add(TElem listener)
		{
			this._listeners.Add(listener);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0004521B File Offset: 0x0004341B
		internal int IndexOfReference(TElem listener)
		{
			return Index.IndexOfReference<TElem>(this._listeners, listener);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0004522C File Offset: 0x0004342C
		internal void Remove(TElem listener)
		{
			int index = this.IndexOfReference(listener);
			this._listeners[index] = default(TElem);
			if (this._listenerReaderCount == 0)
			{
				this._listeners.RemoveAt(index);
				this._listeners.TrimExcess();
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00045278 File Offset: 0x00043478
		internal void Notify<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3, Listeners<TElem>.Action<TElem, T1, T2, T3> action)
		{
			int count = this._listeners.Count;
			if (0 < count)
			{
				int nullIndex = -1;
				this._listenerReaderCount++;
				try
				{
					for (int i = 0; i < count; i++)
					{
						TElem arg4 = this._listeners[i];
						if (this._filter(arg4))
						{
							action(arg4, arg1, arg2, arg3);
						}
						else
						{
							this._listeners[i] = default(TElem);
							nullIndex = i;
						}
					}
				}
				finally
				{
					this._listenerReaderCount--;
				}
				if (this._listenerReaderCount == 0)
				{
					this.RemoveNullListeners(nullIndex);
				}
			}
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00045324 File Offset: 0x00043524
		private void RemoveNullListeners(int nullIndex)
		{
			int num = nullIndex;
			while (0 <= num)
			{
				if (this._listeners[num] == null)
				{
					this._listeners.RemoveAt(num);
				}
				num--;
			}
		}

		// Token: 0x04000A10 RID: 2576
		private readonly List<TElem> _listeners;

		// Token: 0x04000A11 RID: 2577
		private readonly Listeners<TElem>.Func<TElem, bool> _filter;

		// Token: 0x04000A12 RID: 2578
		private readonly int _objectID;

		// Token: 0x04000A13 RID: 2579
		private int _listenerReaderCount;

		// Token: 0x0200012D RID: 301
		// (Invoke) Token: 0x06001084 RID: 4228
		internal delegate void Action<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

		// Token: 0x0200012E RID: 302
		// (Invoke) Token: 0x06001088 RID: 4232
		internal delegate TResult Func<T1, TResult>(T1 arg1);
	}
}
