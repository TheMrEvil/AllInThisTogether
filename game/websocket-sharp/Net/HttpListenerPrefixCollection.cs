using System;
using System.Collections;
using System.Collections.Generic;

namespace WebSocketSharp.Net
{
	// Token: 0x02000023 RID: 35
	public class HttpListenerPrefixCollection : ICollection<string>, IEnumerable<string>, IEnumerable
	{
		// Token: 0x0600027A RID: 634 RVA: 0x000100D8 File Offset: 0x0000E2D8
		internal HttpListenerPrefixCollection(HttpListener listener)
		{
			this._listener = listener;
			this._prefixes = new List<string>();
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600027B RID: 635 RVA: 0x000100F4 File Offset: 0x0000E2F4
		public int Count
		{
			get
			{
				return this._prefixes.Count;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00010114 File Offset: 0x0000E314
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00010128 File Offset: 0x0000E328
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0001013C File Offset: 0x0000E33C
		public void Add(string uriPrefix)
		{
			this._listener.CheckDisposed();
			HttpListenerPrefix.CheckPrefix(uriPrefix);
			bool flag = this._prefixes.Contains(uriPrefix);
			if (!flag)
			{
				bool isListening = this._listener.IsListening;
				if (isListening)
				{
					EndPointManager.AddPrefix(uriPrefix, this._listener);
				}
				this._prefixes.Add(uriPrefix);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00010198 File Offset: 0x0000E398
		public void Clear()
		{
			this._listener.CheckDisposed();
			bool isListening = this._listener.IsListening;
			if (isListening)
			{
				EndPointManager.RemoveListener(this._listener);
			}
			this._prefixes.Clear();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000101DC File Offset: 0x0000E3DC
		public bool Contains(string uriPrefix)
		{
			this._listener.CheckDisposed();
			bool flag = uriPrefix == null;
			if (flag)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			return this._prefixes.Contains(uriPrefix);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00010219 File Offset: 0x0000E419
		public void CopyTo(string[] array, int offset)
		{
			this._listener.CheckDisposed();
			this._prefixes.CopyTo(array, offset);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00010238 File Offset: 0x0000E438
		public IEnumerator<string> GetEnumerator()
		{
			return this._prefixes.GetEnumerator();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0001025C File Offset: 0x0000E45C
		public bool Remove(string uriPrefix)
		{
			this._listener.CheckDisposed();
			bool flag = uriPrefix == null;
			if (flag)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			bool flag2 = !this._prefixes.Contains(uriPrefix);
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool isListening = this._listener.IsListening;
				if (isListening)
				{
					EndPointManager.RemovePrefix(uriPrefix, this._listener);
				}
				result = this._prefixes.Remove(uriPrefix);
			}
			return result;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000102CC File Offset: 0x0000E4CC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._prefixes.GetEnumerator();
		}

		// Token: 0x040000F5 RID: 245
		private HttpListener _listener;

		// Token: 0x040000F6 RID: 246
		private List<string> _prefixes;
	}
}
