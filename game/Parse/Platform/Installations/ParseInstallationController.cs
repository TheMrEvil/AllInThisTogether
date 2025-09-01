using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Platform.Installations;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Installations
{
	// Token: 0x02000034 RID: 52
	public class ParseInstallationController : IParseInstallationController
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000A66D File Offset: 0x0000886D
		private static string InstallationIdKey
		{
			[CompilerGenerated]
			get
			{
				return ParseInstallationController.<InstallationIdKey>k__BackingField;
			}
		} = "InstallationId";

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000A674 File Offset: 0x00008874
		private object Mutex
		{
			[CompilerGenerated]
			get
			{
				return this.<Mutex>k__BackingField;
			}
		} = new object();

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000A67C File Offset: 0x0000887C
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000A684 File Offset: 0x00008884
		private Guid? InstallationId
		{
			[CompilerGenerated]
			get
			{
				return this.<InstallationId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<InstallationId>k__BackingField = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000A68D File Offset: 0x0000888D
		private ICacheController StorageController
		{
			[CompilerGenerated]
			get
			{
				return this.<StorageController>k__BackingField;
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000A695 File Offset: 0x00008895
		public ParseInstallationController(ICacheController storageController)
		{
			this.StorageController = storageController;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000A6B0 File Offset: 0x000088B0
		public Task SetAsync(Guid? installationId)
		{
			object mutex = this.Mutex;
			Task result;
			lock (mutex)
			{
				Task task;
				if (installationId == null)
				{
					task = this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> storage) => storage.Result.RemoveAsync(ParseInstallationController.InstallationIdKey)).Unwrap();
				}
				else
				{
					task = this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> storage) => storage.Result.AddAsync(ParseInstallationController.InstallationIdKey, installationId.ToString())).Unwrap();
				}
				this.InstallationId = installationId;
				result = task;
			}
			return result;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000A76C File Offset: 0x0000896C
		public Task<Guid?> GetAsync()
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				if (this.InstallationId != null)
				{
					return Task.FromResult<Guid?>(this.InstallationId);
				}
			}
			return this.StorageController.LoadAsync().OnSuccess(delegate(Task<IDataCache<string, object>> storageTask)
			{
				object obj;
				storageTask.Result.TryGetValue(ParseInstallationController.InstallationIdKey, out obj);
				Task<Guid?> result;
				try
				{
					object mutex2 = this.Mutex;
					lock (mutex2)
					{
						result = Task.FromResult<Guid?>(this.InstallationId = new Guid?(new Guid(obj as string)));
					}
				}
				catch (Exception)
				{
					Guid newInstallationId = Guid.NewGuid();
					result = this.SetAsync(new Guid?(newInstallationId)).OnSuccess((Task _) => new Guid?(newInstallationId));
				}
				return result;
			}).Unwrap<Guid?>();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000A7E8 File Offset: 0x000089E8
		public Task ClearAsync()
		{
			return this.SetAsync(null);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000A804 File Offset: 0x00008A04
		// Note: this type is marked as 'beforefieldinit'.
		static ParseInstallationController()
		{
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000A810 File Offset: 0x00008A10
		[CompilerGenerated]
		private Task<Guid?> <GetAsync>b__15_0(Task<IDataCache<string, object>> storageTask)
		{
			object obj;
			storageTask.Result.TryGetValue(ParseInstallationController.InstallationIdKey, out obj);
			Task<Guid?> result;
			try
			{
				object mutex = this.Mutex;
				lock (mutex)
				{
					result = Task.FromResult<Guid?>(this.InstallationId = new Guid?(new Guid(obj as string)));
				}
			}
			catch (Exception)
			{
				Guid newInstallationId = Guid.NewGuid();
				result = this.SetAsync(new Guid?(newInstallationId)).OnSuccess((Task _) => new Guid?(newInstallationId));
			}
			return result;
		}

		// Token: 0x04000073 RID: 115
		[CompilerGenerated]
		private static readonly string <InstallationIdKey>k__BackingField;

		// Token: 0x04000074 RID: 116
		[CompilerGenerated]
		private readonly object <Mutex>k__BackingField;

		// Token: 0x04000075 RID: 117
		[CompilerGenerated]
		private Guid? <InstallationId>k__BackingField;

		// Token: 0x04000076 RID: 118
		[CompilerGenerated]
		private readonly ICacheController <StorageController>k__BackingField;

		// Token: 0x02000109 RID: 265
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x060006F7 RID: 1783 RVA: 0x00015707 File Offset: 0x00013907
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x060006F8 RID: 1784 RVA: 0x0001570F File Offset: 0x0001390F
			internal Task <SetAsync>b__0(Task<IDataCache<string, object>> storage)
			{
				return storage.Result.AddAsync(ParseInstallationController.InstallationIdKey, this.installationId.ToString());
			}

			// Token: 0x04000229 RID: 553
			public Guid? installationId;
		}

		// Token: 0x0200010A RID: 266
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006F9 RID: 1785 RVA: 0x00015732 File Offset: 0x00013932
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006FA RID: 1786 RVA: 0x0001573E File Offset: 0x0001393E
			public <>c()
			{
			}

			// Token: 0x060006FB RID: 1787 RVA: 0x00015746 File Offset: 0x00013946
			internal Task <SetAsync>b__14_1(Task<IDataCache<string, object>> storage)
			{
				return storage.Result.RemoveAsync(ParseInstallationController.InstallationIdKey);
			}

			// Token: 0x0400022A RID: 554
			public static readonly ParseInstallationController.<>c <>9 = new ParseInstallationController.<>c();

			// Token: 0x0400022B RID: 555
			public static Func<Task<IDataCache<string, object>>, Task> <>9__14_1;
		}

		// Token: 0x0200010B RID: 267
		[CompilerGenerated]
		private sealed class <>c__DisplayClass15_0
		{
			// Token: 0x060006FC RID: 1788 RVA: 0x00015758 File Offset: 0x00013958
			public <>c__DisplayClass15_0()
			{
			}

			// Token: 0x060006FD RID: 1789 RVA: 0x00015760 File Offset: 0x00013960
			internal Guid? <GetAsync>b__1(Task _)
			{
				return new Guid?(this.newInstallationId);
			}

			// Token: 0x0400022C RID: 556
			public Guid newInstallationId;
		}
	}
}
