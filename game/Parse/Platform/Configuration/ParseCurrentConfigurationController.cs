using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Data;
using Parse.Abstractions.Platform.Configuration;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Configuration
{
	// Token: 0x0200003A RID: 58
	internal class ParseCurrentConfigurationController : IParseCurrentConfigurationController
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000AC28 File Offset: 0x00008E28
		private static string CurrentConfigurationKey
		{
			[CompilerGenerated]
			get
			{
				return ParseCurrentConfigurationController.<CurrentConfigurationKey>k__BackingField;
			}
		} = "CurrentConfig";

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000AC2F File Offset: 0x00008E2F
		private TaskQueue TaskQueue
		{
			[CompilerGenerated]
			get
			{
				return this.<TaskQueue>k__BackingField;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000AC37 File Offset: 0x00008E37
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000AC3F File Offset: 0x00008E3F
		private ParseConfiguration CurrentConfiguration
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentConfiguration>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CurrentConfiguration>k__BackingField = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000AC48 File Offset: 0x00008E48
		private ICacheController StorageController
		{
			[CompilerGenerated]
			get
			{
				return this.<StorageController>k__BackingField;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000AC50 File Offset: 0x00008E50
		private IParseDataDecoder Decoder
		{
			[CompilerGenerated]
			get
			{
				return this.<Decoder>k__BackingField;
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000AC58 File Offset: 0x00008E58
		public ParseCurrentConfigurationController(ICacheController storageController, IParseDataDecoder decoder)
		{
			this.StorageController = storageController;
			this.Decoder = decoder;
			this.TaskQueue = new TaskQueue();
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000AC7C File Offset: 0x00008E7C
		public Task<ParseConfiguration> GetCurrentConfigAsync(IServiceHub serviceHub)
		{
			Func<Task<IDataCache<string, object>>, ParseConfiguration> <>9__2;
			Func<Task, Task<ParseConfiguration>> <>9__1;
			return this.TaskQueue.Enqueue<Task<Task<ParseConfiguration>>>(delegate(Task toAwait)
			{
				Func<Task, Task<ParseConfiguration>> continuationFunction;
				if ((continuationFunction = <>9__1) == null)
				{
					continuationFunction = (<>9__1 = delegate(Task _)
					{
						if (this.CurrentConfiguration == null)
						{
							Task<IDataCache<string, object>> task2 = this.StorageController.LoadAsync();
							Func<Task<IDataCache<string, object>>, ParseConfiguration> continuation;
							if ((continuation = <>9__2) == null)
							{
								continuation = (<>9__2 = delegate(Task<IDataCache<string, object>> task)
								{
									object obj;
									task.Result.TryGetValue(ParseCurrentConfigurationController.CurrentConfigurationKey, out obj);
									ParseCurrentConfigurationController <>4__this = this;
									string text = obj as string;
									return <>4__this.CurrentConfiguration = ((text != null) ? this.Decoder.BuildConfiguration(ParseClient.DeserializeJsonString(text), serviceHub) : new ParseConfiguration(serviceHub));
								});
							}
							return task2.OnSuccess(continuation);
						}
						return Task.FromResult<ParseConfiguration>(this.CurrentConfiguration);
					});
				}
				return toAwait.ContinueWith<Task<ParseConfiguration>>(continuationFunction);
			}, CancellationToken.None).Unwrap<ParseConfiguration>();
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000ACC0 File Offset: 0x00008EC0
		public Task SetCurrentConfigAsync(ParseConfiguration target)
		{
			Func<Task<IDataCache<string, object>>, Task> <>9__2;
			Func<Task, Task<Task>> <>9__1;
			return this.TaskQueue.Enqueue<Task>(delegate(Task toAwait)
			{
				Func<Task, Task<Task>> continuationFunction;
				if ((continuationFunction = <>9__1) == null)
				{
					continuationFunction = (<>9__1 = delegate(Task _)
					{
						this.CurrentConfiguration = target;
						Task<IDataCache<string, object>> task2 = this.StorageController.LoadAsync();
						Func<Task<IDataCache<string, object>>, Task> continuation;
						if ((continuation = <>9__2) == null)
						{
							continuation = (<>9__2 = ((Task<IDataCache<string, object>> task) => task.Result.AddAsync(ParseCurrentConfigurationController.CurrentConfigurationKey, ParseClient.SerializeJsonString(((IJsonConvertible)target).ConvertToJSON()))));
						}
						return task2.OnSuccess(continuation);
					});
				}
				return toAwait.ContinueWith<Task<Task>>(continuationFunction).Unwrap<Task>().Unwrap();
			}, CancellationToken.None);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000ACFD File Offset: 0x00008EFD
		public Task ClearCurrentConfigAsync()
		{
			return this.TaskQueue.Enqueue<Task>((Task toAwait) => toAwait.ContinueWith<Task<Task>>(delegate(Task _)
			{
				this.CurrentConfiguration = null;
				return this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> task) => task.Result.RemoveAsync(ParseCurrentConfigurationController.CurrentConfigurationKey));
			}).Unwrap<Task>().Unwrap(), CancellationToken.None);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000AD1B File Offset: 0x00008F1B
		public Task ClearCurrentConfigInMemoryAsync()
		{
			return this.TaskQueue.Enqueue<Task<ParseConfiguration>>((Task toAwait) => toAwait.ContinueWith<ParseConfiguration>((Task _) => this.CurrentConfiguration = null), CancellationToken.None);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000AD39 File Offset: 0x00008F39
		// Note: this type is marked as 'beforefieldinit'.
		static ParseCurrentConfigurationController()
		{
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000AD45 File Offset: 0x00008F45
		[CompilerGenerated]
		private Task <ClearCurrentConfigAsync>b__19_0(Task toAwait)
		{
			return toAwait.ContinueWith<Task<Task>>(delegate(Task _)
			{
				this.CurrentConfiguration = null;
				return this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> task) => task.Result.RemoveAsync(ParseCurrentConfigurationController.CurrentConfigurationKey));
			}).Unwrap<Task>().Unwrap();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000AD63 File Offset: 0x00008F63
		[CompilerGenerated]
		private Task<Task> <ClearCurrentConfigAsync>b__19_1(Task _)
		{
			this.CurrentConfiguration = null;
			return this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> task) => task.Result.RemoveAsync(ParseCurrentConfigurationController.CurrentConfigurationKey));
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000AD9B File Offset: 0x00008F9B
		[CompilerGenerated]
		private Task<ParseConfiguration> <ClearCurrentConfigInMemoryAsync>b__20_0(Task toAwait)
		{
			return toAwait.ContinueWith<ParseConfiguration>((Task _) => this.CurrentConfiguration = null);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		[CompilerGenerated]
		private ParseConfiguration <ClearCurrentConfigInMemoryAsync>b__20_1(Task _)
		{
			return this.CurrentConfiguration = null;
		}

		// Token: 0x04000081 RID: 129
		[CompilerGenerated]
		private static readonly string <CurrentConfigurationKey>k__BackingField;

		// Token: 0x04000082 RID: 130
		[CompilerGenerated]
		private readonly TaskQueue <TaskQueue>k__BackingField;

		// Token: 0x04000083 RID: 131
		[CompilerGenerated]
		private ParseConfiguration <CurrentConfiguration>k__BackingField;

		// Token: 0x04000084 RID: 132
		[CompilerGenerated]
		private readonly ICacheController <StorageController>k__BackingField;

		// Token: 0x04000085 RID: 133
		[CompilerGenerated]
		private readonly IParseDataDecoder <Decoder>k__BackingField;

		// Token: 0x0200010E RID: 270
		[CompilerGenerated]
		private sealed class <>c__DisplayClass17_0
		{
			// Token: 0x06000704 RID: 1796 RVA: 0x00015872 File Offset: 0x00013A72
			public <>c__DisplayClass17_0()
			{
			}

			// Token: 0x06000705 RID: 1797 RVA: 0x0001587C File Offset: 0x00013A7C
			internal Task<Task<ParseConfiguration>> <GetCurrentConfigAsync>b__0(Task toAwait)
			{
				Func<Task, Task<ParseConfiguration>> continuationFunction;
				if ((continuationFunction = this.<>9__1) == null)
				{
					continuationFunction = (this.<>9__1 = delegate(Task _)
					{
						if (this.<>4__this.CurrentConfiguration == null)
						{
							Task<IDataCache<string, object>> task2 = this.<>4__this.StorageController.LoadAsync();
							Func<Task<IDataCache<string, object>>, ParseConfiguration> continuation;
							if ((continuation = this.<>9__2) == null)
							{
								continuation = (this.<>9__2 = delegate(Task<IDataCache<string, object>> task)
								{
									object obj;
									task.Result.TryGetValue(ParseCurrentConfigurationController.CurrentConfigurationKey, out obj);
									ParseCurrentConfigurationController parseCurrentConfigurationController = this.<>4__this;
									string text = obj as string;
									return parseCurrentConfigurationController.CurrentConfiguration = ((text != null) ? this.<>4__this.Decoder.BuildConfiguration(ParseClient.DeserializeJsonString(text), this.serviceHub) : new ParseConfiguration(this.serviceHub));
								});
							}
							return task2.OnSuccess(continuation);
						}
						return Task.FromResult<ParseConfiguration>(this.<>4__this.CurrentConfiguration);
					});
				}
				return toAwait.ContinueWith<Task<ParseConfiguration>>(continuationFunction);
			}

			// Token: 0x06000706 RID: 1798 RVA: 0x000158B0 File Offset: 0x00013AB0
			internal Task<ParseConfiguration> <GetCurrentConfigAsync>b__1(Task _)
			{
				if (this.<>4__this.CurrentConfiguration == null)
				{
					Task<IDataCache<string, object>> task2 = this.<>4__this.StorageController.LoadAsync();
					Func<Task<IDataCache<string, object>>, ParseConfiguration> continuation;
					if ((continuation = this.<>9__2) == null)
					{
						continuation = (this.<>9__2 = delegate(Task<IDataCache<string, object>> task)
						{
							object obj;
							task.Result.TryGetValue(ParseCurrentConfigurationController.CurrentConfigurationKey, out obj);
							ParseCurrentConfigurationController parseCurrentConfigurationController = this.<>4__this;
							string text = obj as string;
							return parseCurrentConfigurationController.CurrentConfiguration = ((text != null) ? this.<>4__this.Decoder.BuildConfiguration(ParseClient.DeserializeJsonString(text), this.serviceHub) : new ParseConfiguration(this.serviceHub));
						});
					}
					return task2.OnSuccess(continuation);
				}
				return Task.FromResult<ParseConfiguration>(this.<>4__this.CurrentConfiguration);
			}

			// Token: 0x06000707 RID: 1799 RVA: 0x00015910 File Offset: 0x00013B10
			internal ParseConfiguration <GetCurrentConfigAsync>b__2(Task<IDataCache<string, object>> task)
			{
				object obj;
				task.Result.TryGetValue(ParseCurrentConfigurationController.CurrentConfigurationKey, out obj);
				ParseCurrentConfigurationController parseCurrentConfigurationController = this.<>4__this;
				string text = obj as string;
				return parseCurrentConfigurationController.CurrentConfiguration = ((text != null) ? this.<>4__this.Decoder.BuildConfiguration(ParseClient.DeserializeJsonString(text), this.serviceHub) : new ParseConfiguration(this.serviceHub));
			}

			// Token: 0x04000234 RID: 564
			public ParseCurrentConfigurationController <>4__this;

			// Token: 0x04000235 RID: 565
			public IServiceHub serviceHub;

			// Token: 0x04000236 RID: 566
			public Func<Task<IDataCache<string, object>>, ParseConfiguration> <>9__2;

			// Token: 0x04000237 RID: 567
			public Func<Task, Task<ParseConfiguration>> <>9__1;
		}

		// Token: 0x0200010F RID: 271
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_0
		{
			// Token: 0x06000708 RID: 1800 RVA: 0x00015971 File Offset: 0x00013B71
			public <>c__DisplayClass18_0()
			{
			}

			// Token: 0x06000709 RID: 1801 RVA: 0x0001597C File Offset: 0x00013B7C
			internal Task <SetCurrentConfigAsync>b__0(Task toAwait)
			{
				Func<Task, Task<Task>> continuationFunction;
				if ((continuationFunction = this.<>9__1) == null)
				{
					continuationFunction = (this.<>9__1 = delegate(Task _)
					{
						this.<>4__this.CurrentConfiguration = this.target;
						Task<IDataCache<string, object>> task2 = this.<>4__this.StorageController.LoadAsync();
						Func<Task<IDataCache<string, object>>, Task> continuation;
						if ((continuation = this.<>9__2) == null)
						{
							continuation = (this.<>9__2 = ((Task<IDataCache<string, object>> task) => task.Result.AddAsync(ParseCurrentConfigurationController.CurrentConfigurationKey, ParseClient.SerializeJsonString(((IJsonConvertible)this.target).ConvertToJSON()))));
						}
						return task2.OnSuccess(continuation);
					});
				}
				return toAwait.ContinueWith<Task<Task>>(continuationFunction).Unwrap<Task>().Unwrap();
			}

			// Token: 0x0600070A RID: 1802 RVA: 0x000159B8 File Offset: 0x00013BB8
			internal Task<Task> <SetCurrentConfigAsync>b__1(Task _)
			{
				this.<>4__this.CurrentConfiguration = this.target;
				Task<IDataCache<string, object>> task2 = this.<>4__this.StorageController.LoadAsync();
				Func<Task<IDataCache<string, object>>, Task> continuation;
				if ((continuation = this.<>9__2) == null)
				{
					continuation = (this.<>9__2 = ((Task<IDataCache<string, object>> task) => task.Result.AddAsync(ParseCurrentConfigurationController.CurrentConfigurationKey, ParseClient.SerializeJsonString(((IJsonConvertible)this.target).ConvertToJSON()))));
				}
				return task2.OnSuccess(continuation);
			}

			// Token: 0x0600070B RID: 1803 RVA: 0x00015A0A File Offset: 0x00013C0A
			internal Task <SetCurrentConfigAsync>b__2(Task<IDataCache<string, object>> task)
			{
				return task.Result.AddAsync(ParseCurrentConfigurationController.CurrentConfigurationKey, ParseClient.SerializeJsonString(((IJsonConvertible)this.target).ConvertToJSON()));
			}

			// Token: 0x04000238 RID: 568
			public ParseCurrentConfigurationController <>4__this;

			// Token: 0x04000239 RID: 569
			public ParseConfiguration target;

			// Token: 0x0400023A RID: 570
			public Func<Task<IDataCache<string, object>>, Task> <>9__2;

			// Token: 0x0400023B RID: 571
			public Func<Task, Task<Task>> <>9__1;
		}

		// Token: 0x02000110 RID: 272
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600070C RID: 1804 RVA: 0x00015A2C File Offset: 0x00013C2C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600070D RID: 1805 RVA: 0x00015A38 File Offset: 0x00013C38
			public <>c()
			{
			}

			// Token: 0x0600070E RID: 1806 RVA: 0x00015A40 File Offset: 0x00013C40
			internal Task <ClearCurrentConfigAsync>b__19_2(Task<IDataCache<string, object>> task)
			{
				return task.Result.RemoveAsync(ParseCurrentConfigurationController.CurrentConfigurationKey);
			}

			// Token: 0x0400023C RID: 572
			public static readonly ParseCurrentConfigurationController.<>c <>9 = new ParseCurrentConfigurationController.<>c();

			// Token: 0x0400023D RID: 573
			public static Func<Task<IDataCache<string, object>>, Task> <>9__19_2;
		}
	}
}
