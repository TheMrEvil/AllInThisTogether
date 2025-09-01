using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;

namespace Parse.Infrastructure
{
	// Token: 0x0200004E RID: 78
	public class TransientCacheController : ICacheController
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000C996 File Offset: 0x0000AB96
		private TransientCacheController.VirtualCache Cache
		{
			[CompilerGenerated]
			get
			{
				return this.<Cache>k__BackingField;
			}
		} = new TransientCacheController.VirtualCache();

		// Token: 0x060003FA RID: 1018 RVA: 0x0000C99E File Offset: 0x0000AB9E
		public void Clear()
		{
			this.Cache.Clear();
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000C9AB File Offset: 0x0000ABAB
		public FileInfo GetRelativeFile(string path)
		{
			throw new NotSupportedException(Resources.TransientCacheControllerDiskFileOperationNotSupportedMessage);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000C9B7 File Offset: 0x0000ABB7
		public Task<IDataCache<string, object>> LoadAsync()
		{
			return Task.FromResult<IDataCache<string, object>>(this.Cache);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000C9C4 File Offset: 0x0000ABC4
		public Task<IDataCache<string, object>> SaveAsync(IDictionary<string, object> contents)
		{
			foreach (KeyValuePair<string, object> item in contents)
			{
				((ICollection<KeyValuePair<string, object>>)this.Cache).Add(item);
			}
			return Task.FromResult<IDataCache<string, object>>(this.Cache);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		public Task TransferAsync(string originFilePath, string targetFilePath)
		{
			return Task.FromException(new NotSupportedException(Resources.TransientCacheControllerDiskFileOperationNotSupportedMessage));
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000CA2D File Offset: 0x0000AC2D
		public TransientCacheController()
		{
		}

		// Token: 0x040000C6 RID: 198
		[CompilerGenerated]
		private readonly TransientCacheController.VirtualCache <Cache>k__BackingField;

		// Token: 0x02000118 RID: 280
		private class VirtualCache : Dictionary<string, object>, IDataCache<string, object>, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
		{
			// Token: 0x06000749 RID: 1865 RVA: 0x00016284 File Offset: 0x00014484
			public Task AddAsync(string key, object value)
			{
				base.Add(key, value);
				return Task.CompletedTask;
			}

			// Token: 0x0600074A RID: 1866 RVA: 0x00016293 File Offset: 0x00014493
			public Task RemoveAsync(string key)
			{
				base.Remove(key);
				return Task.CompletedTask;
			}

			// Token: 0x0600074B RID: 1867 RVA: 0x000162A2 File Offset: 0x000144A2
			public VirtualCache()
			{
			}
		}
	}
}
