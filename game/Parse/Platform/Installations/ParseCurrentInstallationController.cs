using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Platform.Installations;
using Parse.Abstractions.Platform.Objects;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Installations
{
	// Token: 0x02000032 RID: 50
	internal class ParseCurrentInstallationController : IParseCurrentInstallationController, IParseObjectCurrentController<ParseInstallation>
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000A283 File Offset: 0x00008483
		private static string ParseInstallationKey
		{
			[CompilerGenerated]
			get
			{
				return ParseCurrentInstallationController.<ParseInstallationKey>k__BackingField;
			}
		} = "CurrentInstallation";

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000A28A File Offset: 0x0000848A
		private object Mutex
		{
			[CompilerGenerated]
			get
			{
				return this.<Mutex>k__BackingField;
			}
		} = new object();

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000A292 File Offset: 0x00008492
		private TaskQueue TaskQueue
		{
			[CompilerGenerated]
			get
			{
				return this.<TaskQueue>k__BackingField;
			}
		} = new TaskQueue();

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000A29A File Offset: 0x0000849A
		private IParseInstallationController InstallationController
		{
			[CompilerGenerated]
			get
			{
				return this.<InstallationController>k__BackingField;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000A2A2 File Offset: 0x000084A2
		private ICacheController StorageController
		{
			[CompilerGenerated]
			get
			{
				return this.<StorageController>k__BackingField;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000A2AA File Offset: 0x000084AA
		private IParseInstallationCoder InstallationCoder
		{
			[CompilerGenerated]
			get
			{
				return this.<InstallationCoder>k__BackingField;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000A2B2 File Offset: 0x000084B2
		private IParseObjectClassController ClassController
		{
			[CompilerGenerated]
			get
			{
				return this.<ClassController>k__BackingField;
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A2BA File Offset: 0x000084BA
		public ParseCurrentInstallationController(IParseInstallationController installationIdController, ICacheController storageController, IParseInstallationCoder installationCoder, IParseObjectClassController classController)
		{
			this.InstallationController = installationIdController;
			this.StorageController = storageController;
			this.InstallationCoder = installationCoder;
			this.ClassController = classController;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000A2F5 File Offset: 0x000084F5
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000A2FD File Offset: 0x000084FD
		private ParseInstallation CurrentInstallationValue
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentInstallationValue>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CurrentInstallationValue>k__BackingField = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000A308 File Offset: 0x00008508
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000A34C File Offset: 0x0000854C
		internal ParseInstallation CurrentInstallation
		{
			get
			{
				object mutex = this.Mutex;
				ParseInstallation currentInstallationValue;
				lock (mutex)
				{
					currentInstallationValue = this.CurrentInstallationValue;
				}
				return currentInstallationValue;
			}
			set
			{
				object mutex = this.Mutex;
				lock (mutex)
				{
					this.CurrentInstallationValue = value;
				}
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000A390 File Offset: 0x00008590
		public Task SetAsync(ParseInstallation installation, CancellationToken cancellationToken)
		{
			Func<Task<IDataCache<string, object>>, Task> <>9__2;
			Func<Task, Task> <>9__1;
			return this.TaskQueue.Enqueue<Task>(delegate(Task toAwait)
			{
				Func<Task, Task> continuationFunction;
				if ((continuationFunction = <>9__1) == null)
				{
					continuationFunction = (<>9__1 = delegate(Task _)
					{
						Task<IDataCache<string, object>> task = this.StorageController.LoadAsync();
						Func<Task<IDataCache<string, object>>, Task> continuation;
						if ((continuation = <>9__2) == null)
						{
							continuation = (<>9__2 = delegate(Task<IDataCache<string, object>> storage)
							{
								if (installation == null)
								{
									return storage.Result.RemoveAsync(ParseCurrentInstallationController.ParseInstallationKey);
								}
								return storage.Result.AddAsync(ParseCurrentInstallationController.ParseInstallationKey, JsonUtilities.Encode(this.InstallationCoder.Encode(installation)));
							});
						}
						Task result = task.OnSuccess(continuation).Unwrap();
						this.CurrentInstallation = installation;
						return result;
					});
				}
				return toAwait.ContinueWith<Task>(continuationFunction).Unwrap();
			}, cancellationToken);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000A3CC File Offset: 0x000085CC
		public Task<ParseInstallation> GetAsync(IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			ParseInstallation currentInstallation = this.CurrentInstallation;
			if (currentInstallation == null)
			{
				Func<Task<IDataCache<string, object>>, Task<ParseInstallation>> <>9__2;
				Func<Task, Task<Task<ParseInstallation>>> <>9__1;
				return this.TaskQueue.Enqueue<Task<ParseInstallation>>(delegate(Task toAwait)
				{
					Func<Task, Task<Task<ParseInstallation>>> continuationFunction;
					if ((continuationFunction = <>9__1) == null)
					{
						continuationFunction = (<>9__1 = delegate(Task _)
						{
							Task<IDataCache<string, object>> task = this.StorageController.LoadAsync();
							Func<Task<IDataCache<string, object>>, Task<ParseInstallation>> continuation;
							if ((continuation = <>9__2) == null)
							{
								continuation = (<>9__2 = delegate(Task<IDataCache<string, object>> stroage)
								{
									object obj;
									stroage.Result.TryGetValue(ParseCurrentInstallationController.ParseInstallationKey, out obj);
									ParseInstallation installation = null;
									string text = obj as string;
									Task task2;
									if (text != null)
									{
										IDictionary<string, object> data = JsonUtilities.Parse(text) as IDictionary<string, object>;
										installation = this.InstallationCoder.Decode(data, serviceHub);
										task2 = Task.FromResult<object>(null);
									}
									else
									{
										installation = this.ClassController.CreateObject(serviceHub);
										task2 = this.InstallationController.GetAsync().ContinueWith(delegate(Task<Guid?> t)
										{
											installation.SetIfDifferent<string>("installationId", t.Result.ToString());
										});
									}
									this.CurrentInstallation = installation;
									return task2.ContinueWith<ParseInstallation>((Task task) => installation);
								});
							}
							return task.OnSuccess(continuation);
						});
					}
					return toAwait.ContinueWith<Task<Task<ParseInstallation>>>(continuationFunction).Unwrap<Task<ParseInstallation>>().Unwrap<ParseInstallation>();
				}, cancellationToken);
			}
			return Task.FromResult<ParseInstallation>(currentInstallation);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000A416 File Offset: 0x00008616
		public Task<bool> ExistsAsync(CancellationToken cancellationToken)
		{
			if (this.CurrentInstallation == null)
			{
				return this.TaskQueue.Enqueue<Task<bool>>((Task toAwait) => toAwait.ContinueWith<Task<bool>>((Task _) => this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> storageTask) => storageTask.Result.ContainsKey(ParseCurrentInstallationController.ParseInstallationKey))).Unwrap<bool>(), cancellationToken);
			}
			return Task.FromResult<bool>(true);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000A43F File Offset: 0x0000863F
		public bool IsCurrent(ParseInstallation installation)
		{
			return this.CurrentInstallation == installation;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000A44A File Offset: 0x0000864A
		public void ClearFromMemory()
		{
			this.CurrentInstallation = null;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000A453 File Offset: 0x00008653
		public void ClearFromDisk()
		{
			this.ClearFromMemory();
			this.TaskQueue.Enqueue<Task>((Task toAwait) => toAwait.ContinueWith<Task<Task>>((Task _) => this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> storage) => storage.Result.RemoveAsync(ParseCurrentInstallationController.ParseInstallationKey))).Unwrap<Task>().Unwrap(), CancellationToken.None);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000A478 File Offset: 0x00008678
		// Note: this type is marked as 'beforefieldinit'.
		static ParseCurrentInstallationController()
		{
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000A484 File Offset: 0x00008684
		[CompilerGenerated]
		private Task<bool> <ExistsAsync>b__31_0(Task toAwait)
		{
			return toAwait.ContinueWith<Task<bool>>((Task _) => this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> storageTask) => storageTask.Result.ContainsKey(ParseCurrentInstallationController.ParseInstallationKey))).Unwrap<bool>();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000A49D File Offset: 0x0000869D
		[CompilerGenerated]
		private Task<bool> <ExistsAsync>b__31_1(Task _)
		{
			return this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> storageTask) => storageTask.Result.ContainsKey(ParseCurrentInstallationController.ParseInstallationKey));
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000A4CE File Offset: 0x000086CE
		[CompilerGenerated]
		private Task <ClearFromDisk>b__34_0(Task toAwait)
		{
			return toAwait.ContinueWith<Task<Task>>((Task _) => this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> storage) => storage.Result.RemoveAsync(ParseCurrentInstallationController.ParseInstallationKey))).Unwrap<Task>().Unwrap();
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000A4EC File Offset: 0x000086EC
		[CompilerGenerated]
		private Task<Task> <ClearFromDisk>b__34_1(Task _)
		{
			return this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> storage) => storage.Result.RemoveAsync(ParseCurrentInstallationController.ParseInstallationKey));
		}

		// Token: 0x04000069 RID: 105
		[CompilerGenerated]
		private static readonly string <ParseInstallationKey>k__BackingField;

		// Token: 0x0400006A RID: 106
		[CompilerGenerated]
		private readonly object <Mutex>k__BackingField;

		// Token: 0x0400006B RID: 107
		[CompilerGenerated]
		private readonly TaskQueue <TaskQueue>k__BackingField;

		// Token: 0x0400006C RID: 108
		[CompilerGenerated]
		private readonly IParseInstallationController <InstallationController>k__BackingField;

		// Token: 0x0400006D RID: 109
		[CompilerGenerated]
		private readonly ICacheController <StorageController>k__BackingField;

		// Token: 0x0400006E RID: 110
		[CompilerGenerated]
		private readonly IParseInstallationCoder <InstallationCoder>k__BackingField;

		// Token: 0x0400006F RID: 111
		[CompilerGenerated]
		private readonly IParseObjectClassController <ClassController>k__BackingField;

		// Token: 0x04000070 RID: 112
		[CompilerGenerated]
		private ParseInstallation <CurrentInstallationValue>k__BackingField;

		// Token: 0x02000104 RID: 260
		[CompilerGenerated]
		private sealed class <>c__DisplayClass29_0
		{
			// Token: 0x060006E4 RID: 1764 RVA: 0x00015428 File Offset: 0x00013628
			public <>c__DisplayClass29_0()
			{
			}

			// Token: 0x060006E5 RID: 1765 RVA: 0x00015430 File Offset: 0x00013630
			internal Task <SetAsync>b__0(Task toAwait)
			{
				Func<Task, Task> continuationFunction;
				if ((continuationFunction = this.<>9__1) == null)
				{
					continuationFunction = (this.<>9__1 = delegate(Task _)
					{
						Task<IDataCache<string, object>> task = this.<>4__this.StorageController.LoadAsync();
						Func<Task<IDataCache<string, object>>, Task> continuation;
						if ((continuation = this.<>9__2) == null)
						{
							continuation = (this.<>9__2 = delegate(Task<IDataCache<string, object>> storage)
							{
								if (this.installation == null)
								{
									return storage.Result.RemoveAsync(ParseCurrentInstallationController.ParseInstallationKey);
								}
								return storage.Result.AddAsync(ParseCurrentInstallationController.ParseInstallationKey, JsonUtilities.Encode(this.<>4__this.InstallationCoder.Encode(this.installation)));
							});
						}
						Task result = task.OnSuccess(continuation).Unwrap();
						this.<>4__this.CurrentInstallation = this.installation;
						return result;
					});
				}
				return toAwait.ContinueWith<Task>(continuationFunction).Unwrap();
			}

			// Token: 0x060006E6 RID: 1766 RVA: 0x00015468 File Offset: 0x00013668
			internal Task <SetAsync>b__1(Task _)
			{
				Task<IDataCache<string, object>> task = this.<>4__this.StorageController.LoadAsync();
				Func<Task<IDataCache<string, object>>, Task> continuation;
				if ((continuation = this.<>9__2) == null)
				{
					continuation = (this.<>9__2 = delegate(Task<IDataCache<string, object>> storage)
					{
						if (this.installation == null)
						{
							return storage.Result.RemoveAsync(ParseCurrentInstallationController.ParseInstallationKey);
						}
						return storage.Result.AddAsync(ParseCurrentInstallationController.ParseInstallationKey, JsonUtilities.Encode(this.<>4__this.InstallationCoder.Encode(this.installation)));
					});
				}
				Task result = task.OnSuccess(continuation).Unwrap();
				this.<>4__this.CurrentInstallation = this.installation;
				return result;
			}

			// Token: 0x060006E7 RID: 1767 RVA: 0x000154C0 File Offset: 0x000136C0
			internal Task <SetAsync>b__2(Task<IDataCache<string, object>> storage)
			{
				if (this.installation == null)
				{
					return storage.Result.RemoveAsync(ParseCurrentInstallationController.ParseInstallationKey);
				}
				return storage.Result.AddAsync(ParseCurrentInstallationController.ParseInstallationKey, JsonUtilities.Encode(this.<>4__this.InstallationCoder.Encode(this.installation)));
			}

			// Token: 0x0400021A RID: 538
			public ParseCurrentInstallationController <>4__this;

			// Token: 0x0400021B RID: 539
			public ParseInstallation installation;

			// Token: 0x0400021C RID: 540
			public Func<Task<IDataCache<string, object>>, Task> <>9__2;

			// Token: 0x0400021D RID: 541
			public Func<Task, Task> <>9__1;
		}

		// Token: 0x02000105 RID: 261
		[CompilerGenerated]
		private sealed class <>c__DisplayClass30_0
		{
			// Token: 0x060006E8 RID: 1768 RVA: 0x00015511 File Offset: 0x00013711
			public <>c__DisplayClass30_0()
			{
			}

			// Token: 0x060006E9 RID: 1769 RVA: 0x0001551C File Offset: 0x0001371C
			internal Task<ParseInstallation> <GetAsync>b__0(Task toAwait)
			{
				Func<Task, Task<Task<ParseInstallation>>> continuationFunction;
				if ((continuationFunction = this.<>9__1) == null)
				{
					continuationFunction = (this.<>9__1 = delegate(Task _)
					{
						Task<IDataCache<string, object>> task = this.<>4__this.StorageController.LoadAsync();
						Func<Task<IDataCache<string, object>>, Task<ParseInstallation>> continuation;
						if ((continuation = this.<>9__2) == null)
						{
							continuation = (this.<>9__2 = delegate(Task<IDataCache<string, object>> stroage)
							{
								ParseCurrentInstallationController.<>c__DisplayClass30_1 CS$<>8__locals1 = new ParseCurrentInstallationController.<>c__DisplayClass30_1();
								object obj;
								stroage.Result.TryGetValue(ParseCurrentInstallationController.ParseInstallationKey, out obj);
								CS$<>8__locals1.installation = null;
								string text = obj as string;
								Task task2;
								if (text != null)
								{
									IDictionary<string, object> data = JsonUtilities.Parse(text) as IDictionary<string, object>;
									CS$<>8__locals1.installation = this.<>4__this.InstallationCoder.Decode(data, this.serviceHub);
									task2 = Task.FromResult<object>(null);
								}
								else
								{
									CS$<>8__locals1.installation = this.<>4__this.ClassController.CreateObject(this.serviceHub);
									task2 = this.<>4__this.InstallationController.GetAsync().ContinueWith(new Action<Task<Guid?>>(CS$<>8__locals1.<GetAsync>b__3));
								}
								this.<>4__this.CurrentInstallation = CS$<>8__locals1.installation;
								return task2.ContinueWith<ParseInstallation>(new Func<Task, ParseInstallation>(CS$<>8__locals1.<GetAsync>b__4));
							});
						}
						return task.OnSuccess(continuation);
					});
				}
				return toAwait.ContinueWith<Task<Task<ParseInstallation>>>(continuationFunction).Unwrap<Task<ParseInstallation>>().Unwrap<ParseInstallation>();
			}

			// Token: 0x060006EA RID: 1770 RVA: 0x00015558 File Offset: 0x00013758
			internal Task<Task<ParseInstallation>> <GetAsync>b__1(Task _)
			{
				Task<IDataCache<string, object>> task = this.<>4__this.StorageController.LoadAsync();
				Func<Task<IDataCache<string, object>>, Task<ParseInstallation>> continuation;
				if ((continuation = this.<>9__2) == null)
				{
					continuation = (this.<>9__2 = delegate(Task<IDataCache<string, object>> stroage)
					{
						ParseCurrentInstallationController.<>c__DisplayClass30_1 CS$<>8__locals1 = new ParseCurrentInstallationController.<>c__DisplayClass30_1();
						object obj;
						stroage.Result.TryGetValue(ParseCurrentInstallationController.ParseInstallationKey, out obj);
						CS$<>8__locals1.installation = null;
						string text = obj as string;
						Task task2;
						if (text != null)
						{
							IDictionary<string, object> data = JsonUtilities.Parse(text) as IDictionary<string, object>;
							CS$<>8__locals1.installation = this.<>4__this.InstallationCoder.Decode(data, this.serviceHub);
							task2 = Task.FromResult<object>(null);
						}
						else
						{
							CS$<>8__locals1.installation = this.<>4__this.ClassController.CreateObject(this.serviceHub);
							task2 = this.<>4__this.InstallationController.GetAsync().ContinueWith(new Action<Task<Guid?>>(CS$<>8__locals1.<GetAsync>b__3));
						}
						this.<>4__this.CurrentInstallation = CS$<>8__locals1.installation;
						return task2.ContinueWith<ParseInstallation>(new Func<Task, ParseInstallation>(CS$<>8__locals1.<GetAsync>b__4));
					});
				}
				return task.OnSuccess(continuation);
			}

			// Token: 0x060006EB RID: 1771 RVA: 0x0001559C File Offset: 0x0001379C
			internal Task<ParseInstallation> <GetAsync>b__2(Task<IDataCache<string, object>> stroage)
			{
				ParseCurrentInstallationController.<>c__DisplayClass30_1 CS$<>8__locals1 = new ParseCurrentInstallationController.<>c__DisplayClass30_1();
				object obj;
				stroage.Result.TryGetValue(ParseCurrentInstallationController.ParseInstallationKey, out obj);
				CS$<>8__locals1.installation = null;
				string text = obj as string;
				Task task;
				if (text != null)
				{
					IDictionary<string, object> data = JsonUtilities.Parse(text) as IDictionary<string, object>;
					CS$<>8__locals1.installation = this.<>4__this.InstallationCoder.Decode(data, this.serviceHub);
					task = Task.FromResult<object>(null);
				}
				else
				{
					CS$<>8__locals1.installation = this.<>4__this.ClassController.CreateObject(this.serviceHub);
					task = this.<>4__this.InstallationController.GetAsync().ContinueWith(new Action<Task<Guid?>>(CS$<>8__locals1.<GetAsync>b__3));
				}
				this.<>4__this.CurrentInstallation = CS$<>8__locals1.installation;
				return task.ContinueWith<ParseInstallation>(new Func<Task, ParseInstallation>(CS$<>8__locals1.<GetAsync>b__4));
			}

			// Token: 0x0400021E RID: 542
			public ParseCurrentInstallationController <>4__this;

			// Token: 0x0400021F RID: 543
			public IServiceHub serviceHub;

			// Token: 0x04000220 RID: 544
			public Func<Task<IDataCache<string, object>>, Task<ParseInstallation>> <>9__2;

			// Token: 0x04000221 RID: 545
			public Func<Task, Task<Task<ParseInstallation>>> <>9__1;
		}

		// Token: 0x02000106 RID: 262
		[CompilerGenerated]
		private sealed class <>c__DisplayClass30_1
		{
			// Token: 0x060006EC RID: 1772 RVA: 0x00015668 File Offset: 0x00013868
			public <>c__DisplayClass30_1()
			{
			}

			// Token: 0x060006ED RID: 1773 RVA: 0x00015670 File Offset: 0x00013870
			internal void <GetAsync>b__3(Task<Guid?> t)
			{
				this.installation.SetIfDifferent<string>("installationId", t.Result.ToString());
			}

			// Token: 0x060006EE RID: 1774 RVA: 0x000156A1 File Offset: 0x000138A1
			internal ParseInstallation <GetAsync>b__4(Task task)
			{
				return this.installation;
			}

			// Token: 0x04000222 RID: 546
			public ParseInstallation installation;
		}

		// Token: 0x02000107 RID: 263
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006EF RID: 1775 RVA: 0x000156A9 File Offset: 0x000138A9
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006F0 RID: 1776 RVA: 0x000156B5 File Offset: 0x000138B5
			public <>c()
			{
			}

			// Token: 0x060006F1 RID: 1777 RVA: 0x000156BD File Offset: 0x000138BD
			internal bool <ExistsAsync>b__31_2(Task<IDataCache<string, object>> storageTask)
			{
				return storageTask.Result.ContainsKey(ParseCurrentInstallationController.ParseInstallationKey);
			}

			// Token: 0x060006F2 RID: 1778 RVA: 0x000156CF File Offset: 0x000138CF
			internal Task <ClearFromDisk>b__34_2(Task<IDataCache<string, object>> storage)
			{
				return storage.Result.RemoveAsync(ParseCurrentInstallationController.ParseInstallationKey);
			}

			// Token: 0x04000223 RID: 547
			public static readonly ParseCurrentInstallationController.<>c <>9 = new ParseCurrentInstallationController.<>c();

			// Token: 0x04000224 RID: 548
			public static Func<Task<IDataCache<string, object>>, bool> <>9__31_2;

			// Token: 0x04000225 RID: 549
			public static Func<Task<IDataCache<string, object>>, Task> <>9__34_2;
		}
	}
}
