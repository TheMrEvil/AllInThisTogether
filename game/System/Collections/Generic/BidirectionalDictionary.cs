using System;

namespace System.Collections.Generic
{
	// Token: 0x020004C4 RID: 1220
	internal sealed class BidirectionalDictionary<T1, T2> : IEnumerable<KeyValuePair<T1, T2>>, IEnumerable
	{
		// Token: 0x0600278A RID: 10122 RVA: 0x00089040 File Offset: 0x00087240
		public BidirectionalDictionary(int capacity)
		{
			this._forward = new Dictionary<T1, T2>(capacity);
			this._backward = new Dictionary<T2, T1>(capacity);
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x00089060 File Offset: 0x00087260
		public int Count
		{
			get
			{
				return this._forward.Count;
			}
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x0008906D File Offset: 0x0008726D
		public void Add(T1 item1, T2 item2)
		{
			this._forward.Add(item1, item2);
			this._backward.Add(item2, item1);
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x00089089 File Offset: 0x00087289
		public bool TryGetForward(T1 item1, out T2 item2)
		{
			return this._forward.TryGetValue(item1, out item2);
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x00089098 File Offset: 0x00087298
		public bool TryGetBackward(T2 item2, out T1 item1)
		{
			return this._backward.TryGetValue(item2, out item1);
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000890A7 File Offset: 0x000872A7
		public Dictionary<T1, T2>.Enumerator GetEnumerator()
		{
			return this._forward.GetEnumerator();
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000890B4 File Offset: 0x000872B4
		IEnumerator<KeyValuePair<T1, T2>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000890B4 File Offset: 0x000872B4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04001550 RID: 5456
		private readonly Dictionary<T1, T2> _forward;

		// Token: 0x04001551 RID: 5457
		private readonly Dictionary<T2, T1> _backward;
	}
}
