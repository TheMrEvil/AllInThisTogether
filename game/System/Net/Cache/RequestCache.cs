using System;
using System.Collections.Specialized;
using System.IO;

namespace System.Net.Cache
{
	// Token: 0x02000782 RID: 1922
	internal abstract class RequestCache
	{
		// Token: 0x06003C90 RID: 15504 RVA: 0x000CE5C7 File Offset: 0x000CC7C7
		protected RequestCache(bool isPrivateCache, bool canWrite)
		{
			this._IsPrivateCache = isPrivateCache;
			this._CanWrite = canWrite;
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06003C91 RID: 15505 RVA: 0x000CE5DD File Offset: 0x000CC7DD
		internal bool IsPrivateCache
		{
			get
			{
				return this._IsPrivateCache;
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06003C92 RID: 15506 RVA: 0x000CE5E5 File Offset: 0x000CC7E5
		internal bool CanWrite
		{
			get
			{
				return this._CanWrite;
			}
		}

		// Token: 0x06003C93 RID: 15507
		internal abstract Stream Retrieve(string key, out RequestCacheEntry cacheEntry);

		// Token: 0x06003C94 RID: 15508
		internal abstract Stream Store(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06003C95 RID: 15509
		internal abstract void Remove(string key);

		// Token: 0x06003C96 RID: 15510
		internal abstract void Update(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06003C97 RID: 15511
		internal abstract bool TryRetrieve(string key, out RequestCacheEntry cacheEntry, out Stream readStream);

		// Token: 0x06003C98 RID: 15512
		internal abstract bool TryStore(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata, out Stream writeStream);

		// Token: 0x06003C99 RID: 15513
		internal abstract bool TryRemove(string key);

		// Token: 0x06003C9A RID: 15514
		internal abstract bool TryUpdate(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06003C9B RID: 15515
		internal abstract void UnlockEntry(Stream retrieveStream);

		// Token: 0x06003C9C RID: 15516 RVA: 0x000CE5ED File Offset: 0x000CC7ED
		// Note: this type is marked as 'beforefieldinit'.
		static RequestCache()
		{
		}

		// Token: 0x040023D2 RID: 9170
		internal static readonly char[] LineSplits = new char[]
		{
			'\r',
			'\n'
		};

		// Token: 0x040023D3 RID: 9171
		private bool _IsPrivateCache;

		// Token: 0x040023D4 RID: 9172
		private bool _CanWrite;
	}
}
