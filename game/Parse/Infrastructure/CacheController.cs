using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Infrastructure.Utilities;

namespace Parse.Infrastructure
{
	// Token: 0x0200003E RID: 62
	public class CacheController : IDiskFileCacheController, ICacheController
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000AFCD File Offset: 0x000091CD
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000AFD5 File Offset: 0x000091D5
		private FileInfo File
		{
			[CompilerGenerated]
			get
			{
				return this.<File>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<File>k__BackingField = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000AFDE File Offset: 0x000091DE
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000AFE6 File Offset: 0x000091E6
		private CacheController.FileBackedCache Cache
		{
			[CompilerGenerated]
			get
			{
				return this.<Cache>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Cache>k__BackingField = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000AFEF File Offset: 0x000091EF
		private TaskQueue Queue
		{
			[CompilerGenerated]
			get
			{
				return this.<Queue>k__BackingField;
			}
		} = new TaskQueue();

		// Token: 0x060002E0 RID: 736 RVA: 0x0000AFF7 File Offset: 0x000091F7
		public CacheController()
		{
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000B00A File Offset: 0x0000920A
		public CacheController(FileInfo file)
		{
			this.EnsureCacheExists(file);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000B028 File Offset: 0x00009228
		private CacheController.FileBackedCache EnsureCacheExists(FileInfo file = null)
		{
			CacheController.FileBackedCache result;
			if ((result = this.Cache) == null)
			{
				FileInfo file2 = file;
				if (file == null && (file2 = this.File) == null)
				{
					file2 = (this.File = this.PersistentCacheFile);
				}
				result = (this.Cache = new CacheController.FileBackedCache(file2));
			}
			return result;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000B06B File Offset: 0x0000926B
		public Task<IDataCache<string, object>> LoadAsync()
		{
			this.EnsureCacheExists(null);
			return this.Queue.Enqueue<Task<IDataCache<string, object>>>((Task toAwait) => toAwait.ContinueWith<Task<IDataCache<string, object>>>((Task _) => this.Cache.LoadAsync().OnSuccess((Task __) => this.Cache)).Unwrap<IDataCache<string, object>>(), CancellationToken.None);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000B094 File Offset: 0x00009294
		public Task<IDataCache<string, object>> SaveAsync(IDictionary<string, object> contents)
		{
			Func<Task, IDataCache<string, object>> <>9__2;
			Func<Task, Task<IDataCache<string, object>>> <>9__1;
			return this.Queue.Enqueue<Task<IDataCache<string, object>>>(delegate(Task toAwait)
			{
				Func<Task, Task<IDataCache<string, object>>> continuationFunction;
				if ((continuationFunction = <>9__1) == null)
				{
					continuationFunction = (<>9__1 = delegate(Task _)
					{
						this.EnsureCacheExists(null).Update(contents);
						Task task = this.Cache.SaveAsync();
						Func<Task, IDataCache<string, object>> continuation;
						if ((continuation = <>9__2) == null)
						{
							continuation = (<>9__2 = ((Task __) => this.Cache));
						}
						return task.OnSuccess(continuation);
					});
				}
				return toAwait.ContinueWith<Task<IDataCache<string, object>>>(continuationFunction).Unwrap<IDataCache<string, object>>();
			}, default(CancellationToken));
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000B0D8 File Offset: 0x000092D8
		public void RefreshPaths()
		{
			this.Cache = new CacheController.FileBackedCache(this.File = this.PersistentCacheFile);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000B100 File Offset: 0x00009300
		public void Clear()
		{
			FileInfo fileInfo = new FileInfo(this.FallbackRelativeCacheFilePath);
			if (fileInfo != null && fileInfo.Exists)
			{
				fileInfo.Delete();
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000B12A File Offset: 0x0000932A
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x0000B132 File Offset: 0x00009332
		public string RelativeCacheFilePath
		{
			[CompilerGenerated]
			get
			{
				return this.<RelativeCacheFilePath>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RelativeCacheFilePath>k__BackingField = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000B13B File Offset: 0x0000933B
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000B168 File Offset: 0x00009368
		public string AbsoluteCacheFilePath
		{
			get
			{
				string result;
				if ((result = this.StoredAbsoluteCacheFilePath) == null)
				{
					result = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), this.RelativeCacheFilePath ?? this.FallbackRelativeCacheFilePath));
				}
				return result;
			}
			set
			{
				this.StoredAbsoluteCacheFilePath = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000B171 File Offset: 0x00009371
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000B179 File Offset: 0x00009379
		private string StoredAbsoluteCacheFilePath
		{
			[CompilerGenerated]
			get
			{
				return this.<StoredAbsoluteCacheFilePath>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<StoredAbsoluteCacheFilePath>k__BackingField = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000B184 File Offset: 0x00009384
		public string FallbackRelativeCacheFilePath
		{
			get
			{
				string result;
				if ((result = this.StoredFallbackRelativeCacheFilePath) == null)
				{
					result = (this.StoredFallbackRelativeCacheFilePath = IdentifierBasedRelativeCacheLocationGenerator.Fallback.GetRelativeCacheFilePath(new MutableServiceHub
					{
						CacheController = this
					}));
				}
				return result;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000B1BD File Offset: 0x000093BD
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000B1C5 File Offset: 0x000093C5
		private string StoredFallbackRelativeCacheFilePath
		{
			[CompilerGenerated]
			get
			{
				return this.<StoredFallbackRelativeCacheFilePath>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<StoredFallbackRelativeCacheFilePath>k__BackingField = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000B1D0 File Offset: 0x000093D0
		public FileInfo PersistentCacheFile
		{
			get
			{
				Directory.CreateDirectory(this.AbsoluteCacheFilePath.Substring(0, this.AbsoluteCacheFilePath.LastIndexOf(Path.DirectorySeparatorChar)));
				FileInfo fileInfo = new FileInfo(this.AbsoluteCacheFilePath);
				if (!fileInfo.Exists)
				{
					using (fileInfo.Create())
					{
					}
				}
				return fileInfo;
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000B238 File Offset: 0x00009438
		public FileInfo GetRelativeFile(string path)
		{
			Directory.CreateDirectory((path = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), path))).Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar)));
			return new FileInfo(path);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000B26C File Offset: 0x0000946C
		public Task TransferAsync(string originFilePath, string targetFilePath)
		{
			CacheController.<TransferAsync>d__39 <TransferAsync>d__;
			<TransferAsync>d__.originFilePath = originFilePath;
			<TransferAsync>d__.targetFilePath = targetFilePath;
			<TransferAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<TransferAsync>d__.<>1__state = -1;
			<TransferAsync>d__.<>t__builder.Start<CacheController.<TransferAsync>d__39>(ref <TransferAsync>d__);
			return <TransferAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B2B7 File Offset: 0x000094B7
		[CompilerGenerated]
		private Task<IDataCache<string, object>> <LoadAsync>b__15_0(Task toAwait)
		{
			return toAwait.ContinueWith<Task<IDataCache<string, object>>>((Task _) => this.Cache.LoadAsync().OnSuccess((Task __) => this.Cache)).Unwrap<IDataCache<string, object>>();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000B2D0 File Offset: 0x000094D0
		[CompilerGenerated]
		private Task<IDataCache<string, object>> <LoadAsync>b__15_1(Task _)
		{
			return this.Cache.LoadAsync().OnSuccess((Task __) => this.Cache);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000B2EE File Offset: 0x000094EE
		[CompilerGenerated]
		private IDataCache<string, object> <LoadAsync>b__15_2(Task __)
		{
			return this.Cache;
		}

		// Token: 0x0400008A RID: 138
		[CompilerGenerated]
		private FileInfo <File>k__BackingField;

		// Token: 0x0400008B RID: 139
		[CompilerGenerated]
		private CacheController.FileBackedCache <Cache>k__BackingField;

		// Token: 0x0400008C RID: 140
		[CompilerGenerated]
		private readonly TaskQueue <Queue>k__BackingField;

		// Token: 0x0400008D RID: 141
		[CompilerGenerated]
		private string <RelativeCacheFilePath>k__BackingField;

		// Token: 0x0400008E RID: 142
		[CompilerGenerated]
		private string <StoredAbsoluteCacheFilePath>k__BackingField;

		// Token: 0x0400008F RID: 143
		[CompilerGenerated]
		private string <StoredFallbackRelativeCacheFilePath>k__BackingField;

		// Token: 0x02000112 RID: 274
		private class FileBackedCache : IDataCache<string, object>, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
		{
			// Token: 0x06000711 RID: 1809 RVA: 0x00015AB7 File Offset: 0x00013CB7
			public FileBackedCache(FileInfo file)
			{
				this.File = file;
			}

			// Token: 0x06000712 RID: 1810 RVA: 0x00015ADC File Offset: 0x00013CDC
			internal Task SaveAsync()
			{
				return this.Lock<Task>(() => this.File.WriteContentAsync(JsonUtilities.Encode(this.Storage)));
			}

			// Token: 0x06000713 RID: 1811 RVA: 0x00015AF0 File Offset: 0x00013CF0
			internal Task LoadAsync()
			{
				return this.File.ReadAllTextAsync().ContinueWith(delegate(Task<string> task)
				{
					object mutex = this.Mutex;
					lock (mutex)
					{
						try
						{
							this.Storage = (JsonUtilities.Parse(task.Result) as Dictionary<string, object>);
						}
						catch
						{
							this.Storage = new Dictionary<string, object>();
						}
					}
				});
			}

			// Token: 0x06000714 RID: 1812 RVA: 0x00015B10 File Offset: 0x00013D10
			internal void Update(IDictionary<string, object> contents)
			{
				this.Lock<Dictionary<string, object>>(() => this.Storage = contents.ToDictionary((KeyValuePair<string, object> element) => element.Key, (KeyValuePair<string, object> element) => element.Value));
			}

			// Token: 0x06000715 RID: 1813 RVA: 0x00015B44 File Offset: 0x00013D44
			public Task AddAsync(string key, object value)
			{
				object mutex = this.Mutex;
				Task result;
				lock (mutex)
				{
					this.Storage[key] = value;
					result = this.SaveAsync();
				}
				return result;
			}

			// Token: 0x06000716 RID: 1814 RVA: 0x00015B94 File Offset: 0x00013D94
			public Task RemoveAsync(string key)
			{
				object mutex = this.Mutex;
				Task result;
				lock (mutex)
				{
					this.Storage.Remove(key);
					result = this.SaveAsync();
				}
				return result;
			}

			// Token: 0x06000717 RID: 1815 RVA: 0x00015BE4 File Offset: 0x00013DE4
			public void Add(string key, object value)
			{
				throw new NotSupportedException(Resources.FileBackedCacheSynchronousMutationNotSupportedMessage);
			}

			// Token: 0x06000718 RID: 1816 RVA: 0x00015BF0 File Offset: 0x00013DF0
			public bool Remove(string key)
			{
				throw new NotSupportedException(Resources.FileBackedCacheSynchronousMutationNotSupportedMessage);
			}

			// Token: 0x06000719 RID: 1817 RVA: 0x00015BFC File Offset: 0x00013DFC
			public void Add(KeyValuePair<string, object> item)
			{
				throw new NotSupportedException(Resources.FileBackedCacheSynchronousMutationNotSupportedMessage);
			}

			// Token: 0x0600071A RID: 1818 RVA: 0x00015C08 File Offset: 0x00013E08
			public bool Remove(KeyValuePair<string, object> item)
			{
				throw new NotSupportedException(Resources.FileBackedCacheSynchronousMutationNotSupportedMessage);
			}

			// Token: 0x0600071B RID: 1819 RVA: 0x00015C14 File Offset: 0x00013E14
			public bool ContainsKey(string key)
			{
				return this.Lock<bool>(() => this.Storage.ContainsKey(key));
			}

			// Token: 0x0600071C RID: 1820 RVA: 0x00015C48 File Offset: 0x00013E48
			public bool TryGetValue(string key, out object value)
			{
				object mutex = this.Mutex;
				bool item3;
				lock (mutex)
				{
					object obj;
					bool item = this.Storage.TryGetValue(key, out obj);
					object item2;
					value = (item2 = obj);
					item3 = new ValueTuple<bool, object>(item, item2).Item1;
				}
				return item3;
			}

			// Token: 0x0600071D RID: 1821 RVA: 0x00015CA4 File Offset: 0x00013EA4
			public void Clear()
			{
				this.Lock(delegate()
				{
					this.Storage.Clear();
				});
			}

			// Token: 0x0600071E RID: 1822 RVA: 0x00015CB8 File Offset: 0x00013EB8
			public bool Contains(KeyValuePair<string, object> item)
			{
				return this.Lock<bool>(() => this.Elements.Contains(item));
			}

			// Token: 0x0600071F RID: 1823 RVA: 0x00015CEC File Offset: 0x00013EEC
			public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
			{
				this.Lock(delegate()
				{
					this.Elements.CopyTo(array, arrayIndex);
				});
			}

			// Token: 0x06000720 RID: 1824 RVA: 0x00015D26 File Offset: 0x00013F26
			public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
			{
				return this.Storage.GetEnumerator();
			}

			// Token: 0x06000721 RID: 1825 RVA: 0x00015D38 File Offset: 0x00013F38
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.Storage.GetEnumerator();
			}

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x06000722 RID: 1826 RVA: 0x00015D4A File Offset: 0x00013F4A
			// (set) Token: 0x06000723 RID: 1827 RVA: 0x00015D52 File Offset: 0x00013F52
			public FileInfo File
			{
				[CompilerGenerated]
				get
				{
					return this.<File>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<File>k__BackingField = value;
				}
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x06000724 RID: 1828 RVA: 0x00015D5B File Offset: 0x00013F5B
			// (set) Token: 0x06000725 RID: 1829 RVA: 0x00015D63 File Offset: 0x00013F63
			public object Mutex
			{
				[CompilerGenerated]
				get
				{
					return this.<Mutex>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Mutex>k__BackingField = value;
				}
			} = new object();

			// Token: 0x06000726 RID: 1830 RVA: 0x00015D6C File Offset: 0x00013F6C
			private TResult Lock<TResult>(Func<TResult> operation)
			{
				object mutex = this.Mutex;
				TResult result;
				lock (mutex)
				{
					result = operation();
				}
				return result;
			}

			// Token: 0x06000727 RID: 1831 RVA: 0x00015DB0 File Offset: 0x00013FB0
			private void Lock(Action operation)
			{
				object mutex = this.Mutex;
				lock (mutex)
				{
					operation();
				}
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x06000728 RID: 1832 RVA: 0x00015DF0 File Offset: 0x00013FF0
			private ICollection<KeyValuePair<string, object>> Elements
			{
				get
				{
					return this.Storage;
				}
			}

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x06000729 RID: 1833 RVA: 0x00015DF8 File Offset: 0x00013FF8
			// (set) Token: 0x0600072A RID: 1834 RVA: 0x00015E00 File Offset: 0x00014000
			private Dictionary<string, object> Storage
			{
				[CompilerGenerated]
				get
				{
					return this.<Storage>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Storage>k__BackingField = value;
				}
			} = new Dictionary<string, object>();

			// Token: 0x170001F1 RID: 497
			// (get) Token: 0x0600072B RID: 1835 RVA: 0x00015E09 File Offset: 0x00014009
			public ICollection<string> Keys
			{
				get
				{
					return this.Storage.Keys;
				}
			}

			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x0600072C RID: 1836 RVA: 0x00015E16 File Offset: 0x00014016
			public ICollection<object> Values
			{
				get
				{
					return this.Storage.Values;
				}
			}

			// Token: 0x170001F3 RID: 499
			// (get) Token: 0x0600072D RID: 1837 RVA: 0x00015E23 File Offset: 0x00014023
			public int Count
			{
				get
				{
					return this.Storage.Count;
				}
			}

			// Token: 0x170001F4 RID: 500
			// (get) Token: 0x0600072E RID: 1838 RVA: 0x00015E30 File Offset: 0x00014030
			public bool IsReadOnly
			{
				get
				{
					return this.Elements.IsReadOnly;
				}
			}

			// Token: 0x170001F5 RID: 501
			public object this[string key]
			{
				get
				{
					return this.Storage[key];
				}
				set
				{
					throw new NotSupportedException(Resources.FileBackedCacheSynchronousMutationNotSupportedMessage);
				}
			}

			// Token: 0x06000731 RID: 1841 RVA: 0x00015E57 File Offset: 0x00014057
			[CompilerGenerated]
			private Task <SaveAsync>b__1_0()
			{
				return this.File.WriteContentAsync(JsonUtilities.Encode(this.Storage));
			}

			// Token: 0x06000732 RID: 1842 RVA: 0x00015E70 File Offset: 0x00014070
			[CompilerGenerated]
			private void <LoadAsync>b__2_0(Task<string> task)
			{
				object mutex = this.Mutex;
				lock (mutex)
				{
					try
					{
						this.Storage = (JsonUtilities.Parse(task.Result) as Dictionary<string, object>);
					}
					catch
					{
						this.Storage = new Dictionary<string, object>();
					}
				}
			}

			// Token: 0x06000733 RID: 1843 RVA: 0x00015EDC File Offset: 0x000140DC
			[CompilerGenerated]
			private void <Clear>b__12_0()
			{
				this.Storage.Clear();
			}

			// Token: 0x04000240 RID: 576
			[CompilerGenerated]
			private FileInfo <File>k__BackingField;

			// Token: 0x04000241 RID: 577
			[CompilerGenerated]
			private object <Mutex>k__BackingField;

			// Token: 0x04000242 RID: 578
			[CompilerGenerated]
			private Dictionary<string, object> <Storage>k__BackingField;

			// Token: 0x02000144 RID: 324
			[CompilerGenerated]
			private sealed class <>c__DisplayClass3_0
			{
				// Token: 0x0600081C RID: 2076 RVA: 0x0001872A File Offset: 0x0001692A
				public <>c__DisplayClass3_0()
				{
				}

				// Token: 0x0600081D RID: 2077 RVA: 0x00018734 File Offset: 0x00016934
				internal Dictionary<string, object> <Update>b__0()
				{
					return this.<>4__this.Storage = this.contents.ToDictionary(new Func<KeyValuePair<string, object>, string>(CacheController.FileBackedCache.<>c.<>9.<Update>b__3_1), new Func<KeyValuePair<string, object>, object>(CacheController.FileBackedCache.<>c.<>9.<Update>b__3_2));
				}

				// Token: 0x040002F0 RID: 752
				public CacheController.FileBackedCache <>4__this;

				// Token: 0x040002F1 RID: 753
				public IDictionary<string, object> contents;
			}

			// Token: 0x02000145 RID: 325
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x0600081E RID: 2078 RVA: 0x00018798 File Offset: 0x00016998
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x0600081F RID: 2079 RVA: 0x000187A4 File Offset: 0x000169A4
				public <>c()
				{
				}

				// Token: 0x06000820 RID: 2080 RVA: 0x000187AC File Offset: 0x000169AC
				internal string <Update>b__3_1(KeyValuePair<string, object> element)
				{
					return element.Key;
				}

				// Token: 0x06000821 RID: 2081 RVA: 0x000187B5 File Offset: 0x000169B5
				internal object <Update>b__3_2(KeyValuePair<string, object> element)
				{
					return element.Value;
				}

				// Token: 0x040002F2 RID: 754
				public static readonly CacheController.FileBackedCache.<>c <>9 = new CacheController.FileBackedCache.<>c();

				// Token: 0x040002F3 RID: 755
				public static Func<KeyValuePair<string, object>, string> <>9__3_1;

				// Token: 0x040002F4 RID: 756
				public static Func<KeyValuePair<string, object>, object> <>9__3_2;
			}

			// Token: 0x02000146 RID: 326
			[CompilerGenerated]
			private sealed class <>c__DisplayClass10_0
			{
				// Token: 0x06000822 RID: 2082 RVA: 0x000187BE File Offset: 0x000169BE
				public <>c__DisplayClass10_0()
				{
				}

				// Token: 0x06000823 RID: 2083 RVA: 0x000187C6 File Offset: 0x000169C6
				internal bool <ContainsKey>b__0()
				{
					return this.<>4__this.Storage.ContainsKey(this.key);
				}

				// Token: 0x040002F5 RID: 757
				public CacheController.FileBackedCache <>4__this;

				// Token: 0x040002F6 RID: 758
				public string key;
			}

			// Token: 0x02000147 RID: 327
			[CompilerGenerated]
			private sealed class <>c__DisplayClass13_0
			{
				// Token: 0x06000824 RID: 2084 RVA: 0x000187DE File Offset: 0x000169DE
				public <>c__DisplayClass13_0()
				{
				}

				// Token: 0x06000825 RID: 2085 RVA: 0x000187E6 File Offset: 0x000169E6
				internal bool <Contains>b__0()
				{
					return this.<>4__this.Elements.Contains(this.item);
				}

				// Token: 0x040002F7 RID: 759
				public CacheController.FileBackedCache <>4__this;

				// Token: 0x040002F8 RID: 760
				public KeyValuePair<string, object> item;
			}

			// Token: 0x02000148 RID: 328
			[CompilerGenerated]
			private sealed class <>c__DisplayClass14_0
			{
				// Token: 0x06000826 RID: 2086 RVA: 0x000187FE File Offset: 0x000169FE
				public <>c__DisplayClass14_0()
				{
				}

				// Token: 0x06000827 RID: 2087 RVA: 0x00018806 File Offset: 0x00016A06
				internal void <CopyTo>b__0()
				{
					this.<>4__this.Elements.CopyTo(this.array, this.arrayIndex);
				}

				// Token: 0x040002F9 RID: 761
				public CacheController.FileBackedCache <>4__this;

				// Token: 0x040002FA RID: 762
				public KeyValuePair<string, object>[] array;

				// Token: 0x040002FB RID: 763
				public int arrayIndex;
			}
		}

		// Token: 0x02000113 RID: 275
		[CompilerGenerated]
		private sealed class <>c__DisplayClass16_0
		{
			// Token: 0x06000734 RID: 1844 RVA: 0x00015EE9 File Offset: 0x000140E9
			public <>c__DisplayClass16_0()
			{
			}

			// Token: 0x06000735 RID: 1845 RVA: 0x00015EF4 File Offset: 0x000140F4
			internal Task<IDataCache<string, object>> <SaveAsync>b__0(Task toAwait)
			{
				Func<Task, Task<IDataCache<string, object>>> continuationFunction;
				if ((continuationFunction = this.<>9__1) == null)
				{
					continuationFunction = (this.<>9__1 = delegate(Task _)
					{
						this.<>4__this.EnsureCacheExists(null).Update(this.contents);
						Task task = this.<>4__this.Cache.SaveAsync();
						Func<Task, IDataCache<string, object>> continuation;
						if ((continuation = this.<>9__2) == null)
						{
							continuation = (this.<>9__2 = ((Task __) => this.<>4__this.Cache));
						}
						return task.OnSuccess(continuation);
					});
				}
				return toAwait.ContinueWith<Task<IDataCache<string, object>>>(continuationFunction).Unwrap<IDataCache<string, object>>();
			}

			// Token: 0x06000736 RID: 1846 RVA: 0x00015F2C File Offset: 0x0001412C
			internal Task<IDataCache<string, object>> <SaveAsync>b__1(Task _)
			{
				this.<>4__this.EnsureCacheExists(null).Update(this.contents);
				Task task = this.<>4__this.Cache.SaveAsync();
				Func<Task, IDataCache<string, object>> continuation;
				if ((continuation = this.<>9__2) == null)
				{
					continuation = (this.<>9__2 = ((Task __) => this.<>4__this.Cache));
				}
				return task.OnSuccess(continuation);
			}

			// Token: 0x06000737 RID: 1847 RVA: 0x00015F84 File Offset: 0x00014184
			internal IDataCache<string, object> <SaveAsync>b__2(Task __)
			{
				return this.<>4__this.Cache;
			}

			// Token: 0x04000243 RID: 579
			public CacheController <>4__this;

			// Token: 0x04000244 RID: 580
			public IDictionary<string, object> contents;

			// Token: 0x04000245 RID: 581
			public Func<Task, IDataCache<string, object>> <>9__2;

			// Token: 0x04000246 RID: 582
			public Func<Task, Task<IDataCache<string, object>>> <>9__1;
		}

		// Token: 0x02000114 RID: 276
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <TransferAsync>d__39 : IAsyncStateMachine
		{
			// Token: 0x06000738 RID: 1848 RVA: 0x00015F94 File Offset: 0x00014194
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					FileInfo fileInfo;
					if (num > 1)
					{
						if (string.IsNullOrWhiteSpace(this.originFilePath) || string.IsNullOrWhiteSpace(this.targetFilePath))
						{
							goto IL_1B3;
						}
						fileInfo = new FileInfo(this.originFilePath);
						if (fileInfo == null || !fileInfo.Exists)
						{
							goto IL_1B3;
						}
						FileInfo fileInfo2 = new FileInfo(this.targetFilePath);
						if (fileInfo2 == null)
						{
							goto IL_1B3;
						}
						this.<writer>5__2 = new StreamWriter(fileInfo2.OpenWrite(), Encoding.Unicode);
					}
					try
					{
						if (num > 1)
						{
							this.<reader>5__3 = new StreamReader(fileInfo.OpenRead(), Encoding.Unicode);
						}
						try
						{
							TaskAwaiter awaiter;
							TaskAwaiter<string> awaiter2;
							if (num != 0)
							{
								if (num == 1)
								{
									awaiter = this.<>u__2;
									this.<>u__2 = default(TaskAwaiter);
									num = (this.<>1__state = -1);
									goto IL_163;
								}
								this.<>7__wrap3 = this.<writer>5__2;
								awaiter2 = this.<reader>5__3.ReadToEndAsync().GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (this.<>1__state = 0);
									this.<>u__1 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, CacheController.<TransferAsync>d__39>(ref awaiter2, ref this);
									return;
								}
							}
							else
							{
								awaiter2 = this.<>u__1;
								this.<>u__1 = default(TaskAwaiter<string>);
								num = (this.<>1__state = -1);
							}
							string result = awaiter2.GetResult();
							awaiter = this.<>7__wrap3.WriteAsync(result).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 1);
								this.<>u__2 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, CacheController.<TransferAsync>d__39>(ref awaiter, ref this);
								return;
							}
							IL_163:
							awaiter.GetResult();
							this.<>7__wrap3 = null;
						}
						finally
						{
							if (num < 0 && this.<reader>5__3 != null)
							{
								((IDisposable)this.<reader>5__3).Dispose();
							}
						}
					}
					finally
					{
						if (num < 0 && this.<writer>5__2 != null)
						{
							((IDisposable)this.<writer>5__2).Dispose();
						}
					}
					this.<writer>5__2 = null;
					this.<reader>5__3 = null;
					IL_1B3:;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000739 RID: 1849 RVA: 0x000161D0 File Offset: 0x000143D0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000247 RID: 583
			public int <>1__state;

			// Token: 0x04000248 RID: 584
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000249 RID: 585
			public string originFilePath;

			// Token: 0x0400024A RID: 586
			public string targetFilePath;

			// Token: 0x0400024B RID: 587
			private StreamWriter <writer>5__2;

			// Token: 0x0400024C RID: 588
			private StreamReader <reader>5__3;

			// Token: 0x0400024D RID: 589
			private StreamWriter <>7__wrap3;

			// Token: 0x0400024E RID: 590
			private TaskAwaiter<string> <>u__1;

			// Token: 0x0400024F RID: 591
			private TaskAwaiter <>u__2;
		}
	}
}
