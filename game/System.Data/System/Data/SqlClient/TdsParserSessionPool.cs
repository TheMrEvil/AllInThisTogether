using System;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x0200026F RID: 623
	internal class TdsParserSessionPool
	{
		// Token: 0x06001D01 RID: 7425 RVA: 0x00089C04 File Offset: 0x00087E04
		internal TdsParserSessionPool(TdsParser parser)
		{
			this._parser = parser;
			this._cache = new List<TdsParserStateObject>();
			this._freeStateObjects = new TdsParserStateObject[10];
			this._freeStateObjectCount = 0;
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x00089C32 File Offset: 0x00087E32
		private bool IsDisposed
		{
			get
			{
				return this._freeStateObjects == null;
			}
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x00089C40 File Offset: 0x00087E40
		internal void Deactivate()
		{
			List<TdsParserStateObject> cache = this._cache;
			lock (cache)
			{
				for (int i = this._cache.Count - 1; i >= 0; i--)
				{
					TdsParserStateObject tdsParserStateObject = this._cache[i];
					if (tdsParserStateObject != null && tdsParserStateObject.IsOrphaned)
					{
						this.PutSession(tdsParserStateObject);
					}
				}
			}
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x00089CB4 File Offset: 0x00087EB4
		internal void Dispose()
		{
			List<TdsParserStateObject> cache = this._cache;
			lock (cache)
			{
				for (int i = 0; i < this._freeStateObjectCount; i++)
				{
					if (this._freeStateObjects[i] != null)
					{
						this._freeStateObjects[i].Dispose();
					}
				}
				this._freeStateObjects = null;
				this._freeStateObjectCount = 0;
				for (int j = 0; j < this._cache.Count; j++)
				{
					if (this._cache[j] != null)
					{
						if (this._cache[j].IsOrphaned)
						{
							this._cache[j].Dispose();
						}
						else
						{
							this._cache[j].DecrementPendingCallbacks(false);
						}
					}
				}
				this._cache.Clear();
				this._cachedCount = 0;
			}
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x00089D94 File Offset: 0x00087F94
		internal TdsParserStateObject GetSession(object owner)
		{
			List<TdsParserStateObject> cache = this._cache;
			TdsParserStateObject tdsParserStateObject;
			lock (cache)
			{
				if (this.IsDisposed)
				{
					throw ADP.ClosedConnectionError();
				}
				if (this._freeStateObjectCount > 0)
				{
					this._freeStateObjectCount--;
					tdsParserStateObject = this._freeStateObjects[this._freeStateObjectCount];
					this._freeStateObjects[this._freeStateObjectCount] = null;
				}
				else
				{
					tdsParserStateObject = this._parser.CreateSession();
					this._cache.Add(tdsParserStateObject);
					this._cachedCount = this._cache.Count;
				}
				tdsParserStateObject.Activate(owner);
			}
			return tdsParserStateObject;
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x00089E44 File Offset: 0x00088044
		internal void PutSession(TdsParserStateObject session)
		{
			bool flag = session.Deactivate();
			List<TdsParserStateObject> cache = this._cache;
			lock (cache)
			{
				if (this.IsDisposed)
				{
					session.Dispose();
				}
				else if (flag && this._freeStateObjectCount < 10)
				{
					this._freeStateObjects[this._freeStateObjectCount] = session;
					this._freeStateObjectCount++;
				}
				else
				{
					this._cache.Remove(session);
					this._cachedCount = this._cache.Count;
					session.Dispose();
				}
				session.RemoveOwner();
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x00089EEC File Offset: 0x000880EC
		internal int ActiveSessionsCount
		{
			get
			{
				return this._cachedCount - this._freeStateObjectCount;
			}
		}

		// Token: 0x04001429 RID: 5161
		private const int MaxInactiveCount = 10;

		// Token: 0x0400142A RID: 5162
		private readonly TdsParser _parser;

		// Token: 0x0400142B RID: 5163
		private readonly List<TdsParserStateObject> _cache;

		// Token: 0x0400142C RID: 5164
		private int _cachedCount;

		// Token: 0x0400142D RID: 5165
		private TdsParserStateObject[] _freeStateObjects;

		// Token: 0x0400142E RID: 5166
		private int _freeStateObjectCount;
	}
}
