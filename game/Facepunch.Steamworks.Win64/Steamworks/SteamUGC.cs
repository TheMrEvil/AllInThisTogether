using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Steamworks.Data;
using Steamworks.Ugc;

namespace Steamworks
{
	// Token: 0x020000A4 RID: 164
	public class SteamUGC : SteamSharedClass<SteamUGC>
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0000EFF4 File Offset: 0x0000D1F4
		internal static ISteamUGC Internal
		{
			get
			{
				return SteamSharedClass<SteamUGC>.Interface as ISteamUGC;
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0000F000 File Offset: 0x0000D200
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamUGC(server));
			SteamUGC.InstallEvents(server);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0000F018 File Offset: 0x0000D218
		internal static void InstallEvents(bool server)
		{
			Dispatch.Install<DownloadItemResult_t>(delegate(DownloadItemResult_t x)
			{
				Action<Result> onDownloadItemResult = SteamUGC.OnDownloadItemResult;
				if (onDownloadItemResult != null)
				{
					onDownloadItemResult(x.Result);
				}
			}, server);
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060008C7 RID: 2247 RVA: 0x0000F044 File Offset: 0x0000D244
		// (remove) Token: 0x060008C8 RID: 2248 RVA: 0x0000F078 File Offset: 0x0000D278
		public static event Action<Result> OnDownloadItemResult
		{
			[CompilerGenerated]
			add
			{
				Action<Result> action = SteamUGC.OnDownloadItemResult;
				Action<Result> action2;
				do
				{
					action2 = action;
					Action<Result> value2 = (Action<Result>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Result>>(ref SteamUGC.OnDownloadItemResult, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Result> action = SteamUGC.OnDownloadItemResult;
				Action<Result> action2;
				do
				{
					action2 = action;
					Action<Result> value2 = (Action<Result>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Result>>(ref SteamUGC.OnDownloadItemResult, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0000F0AC File Offset: 0x0000D2AC
		public static async Task<bool> DeleteFileAsync(PublishedFileId fileId)
		{
			DeleteItemResult_t? deleteItemResult_t = await SteamUGC.Internal.DeleteItem(fileId);
			DeleteItemResult_t? r = deleteItemResult_t;
			deleteItemResult_t = null;
			return r != null && r.GetValueOrDefault().Result == Result.OK;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0000F0F4 File Offset: 0x0000D2F4
		public static bool Download(PublishedFileId fileId, bool highPriority = false)
		{
			return SteamUGC.Internal.DownloadItem(fileId, highPriority);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0000F114 File Offset: 0x0000D314
		public static async Task<bool> DownloadAsync(PublishedFileId fileId, Action<float> progress = null, int milisecondsUpdateDelay = 60, CancellationToken ct = default(CancellationToken))
		{
			Item item = new Item(fileId);
			bool flag = ct == default(CancellationToken);
			if (flag)
			{
				ct = new CancellationTokenSource(TimeSpan.FromSeconds(60.0)).Token;
			}
			if (progress != null)
			{
				progress(0f);
			}
			bool flag2 = !SteamUGC.Download(fileId, true);
			if (!flag2)
			{
				Action<Result> onDownloadStarted = null;
				try
				{
					SteamUGC.<>c__DisplayClass9_0 CS$<>8__locals1 = new SteamUGC.<>c__DisplayClass9_0();
					CS$<>8__locals1.downloadStarted = false;
					onDownloadStarted = delegate(Result r)
					{
						CS$<>8__locals1.downloadStarted = true;
					};
					SteamUGC.OnDownloadItemResult += onDownloadStarted;
					while (!CS$<>8__locals1.downloadStarted)
					{
						bool isCancellationRequested = ct.IsCancellationRequested;
						if (isCancellationRequested)
						{
							break;
						}
						await Task.Delay(milisecondsUpdateDelay);
					}
					CS$<>8__locals1 = null;
				}
				finally
				{
					SteamUGC.OnDownloadItemResult -= onDownloadStarted;
				}
				onDownloadStarted = null;
				if (progress != null)
				{
					progress(0.2f);
				}
				await Task.Delay(milisecondsUpdateDelay);
				while (!ct.IsCancellationRequested)
				{
					if (progress != null)
					{
						progress(0.2f + item.DownloadAmount * 0.8f);
					}
					if (!item.IsDownloading && item.IsInstalled)
					{
						IL_31A:
						if (progress != null)
						{
							progress(1f);
						}
						return item.IsInstalled;
					}
					await Task.Delay(milisecondsUpdateDelay);
				}
				goto IL_31A;
			}
			return item.IsInstalled;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0000F170 File Offset: 0x0000D370
		public static async Task<Item?> QueryFileAsync(PublishedFileId fileId)
		{
			ResultPage? resultPage = await Query.All.WithFileId(new PublishedFileId[]
			{
				fileId
			}).GetPageAsync(1);
			ResultPage? result = resultPage;
			resultPage = null;
			Item? result2;
			if (result == null || result.Value.ResultCount != 1)
			{
				result2 = null;
			}
			else
			{
				Item item = result.Value.Entries.First<Item>();
				result.Value.Dispose();
				result2 = new Item?(item);
			}
			return result2;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0000F1B8 File Offset: 0x0000D3B8
		public static async Task<bool> StartPlaytimeTracking(PublishedFileId fileId)
		{
			StartPlaytimeTrackingResult_t? startPlaytimeTrackingResult_t = await SteamUGC.Internal.StartPlaytimeTracking(new PublishedFileId[]
			{
				fileId
			}, 1U);
			StartPlaytimeTrackingResult_t? result = startPlaytimeTrackingResult_t;
			startPlaytimeTrackingResult_t = null;
			return result.Value.Result == Result.OK;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0000F200 File Offset: 0x0000D400
		public static async Task<bool> StopPlaytimeTracking(PublishedFileId fileId)
		{
			StopPlaytimeTrackingResult_t? stopPlaytimeTrackingResult_t = await SteamUGC.Internal.StopPlaytimeTracking(new PublishedFileId[]
			{
				fileId
			}, 1U);
			StopPlaytimeTrackingResult_t? result = stopPlaytimeTrackingResult_t;
			stopPlaytimeTrackingResult_t = null;
			return result.Value.Result == Result.OK;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0000F248 File Offset: 0x0000D448
		public static async Task<bool> StopPlaytimeTrackingForAllItems()
		{
			StopPlaytimeTrackingResult_t? stopPlaytimeTrackingResult_t = await SteamUGC.Internal.StopPlaytimeTrackingForAllItems();
			StopPlaytimeTrackingResult_t? result = stopPlaytimeTrackingResult_t;
			stopPlaytimeTrackingResult_t = null;
			return result.Value.Result == Result.OK;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0000F288 File Offset: 0x0000D488
		public SteamUGC()
		{
		}

		// Token: 0x04000726 RID: 1830
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Result> OnDownloadItemResult;

		// Token: 0x02000242 RID: 578
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600115E RID: 4446 RVA: 0x0001EA6E File Offset: 0x0001CC6E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600115F RID: 4447 RVA: 0x0001EA7A File Offset: 0x0001CC7A
			public <>c()
			{
			}

			// Token: 0x06001160 RID: 4448 RVA: 0x0001EA83 File Offset: 0x0001CC83
			internal void <InstallEvents>b__3_0(DownloadItemResult_t x)
			{
				Action<Result> onDownloadItemResult = SteamUGC.OnDownloadItemResult;
				if (onDownloadItemResult != null)
				{
					onDownloadItemResult(x.Result);
				}
			}

			// Token: 0x04000D6B RID: 3435
			public static readonly SteamUGC.<>c <>9 = new SteamUGC.<>c();

			// Token: 0x04000D6C RID: 3436
			public static Action<DownloadItemResult_t> <>9__3_0;
		}

		// Token: 0x02000243 RID: 579
		[CompilerGenerated]
		private sealed class <DeleteFileAsync>d__7 : IAsyncStateMachine
		{
			// Token: 0x06001161 RID: 4449 RVA: 0x0001EA9C File Offset: 0x0001CC9C
			public <DeleteFileAsync>d__7()
			{
			}

			// Token: 0x06001162 RID: 4450 RVA: 0x0001EAA8 File Offset: 0x0001CCA8
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				bool result;
				try
				{
					CallResult<DeleteItemResult_t> callResult;
					if (num != 0)
					{
						callResult = SteamUGC.Internal.DeleteItem(fileId).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<DeleteItemResult_t> callResult2 = callResult;
							SteamUGC.<DeleteFileAsync>d__7 <DeleteFileAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<DeleteItemResult_t>, SteamUGC.<DeleteFileAsync>d__7>(ref callResult, ref <DeleteFileAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<DeleteItemResult_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<DeleteItemResult_t>);
						num2 = -1;
					}
					deleteItemResult_t = callResult.GetResult();
					r = deleteItemResult_t;
					deleteItemResult_t = null;
					result = (r != null && r.GetValueOrDefault().Result == Result.OK);
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001163 RID: 4451 RVA: 0x0001EBB0 File Offset: 0x0001CDB0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000D6D RID: 3437
			public int <>1__state;

			// Token: 0x04000D6E RID: 3438
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000D6F RID: 3439
			public PublishedFileId fileId;

			// Token: 0x04000D70 RID: 3440
			private DeleteItemResult_t? <r>5__1;

			// Token: 0x04000D71 RID: 3441
			private DeleteItemResult_t? <>s__2;

			// Token: 0x04000D72 RID: 3442
			private CallResult<DeleteItemResult_t> <>u__1;
		}

		// Token: 0x02000244 RID: 580
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0
		{
			// Token: 0x06001164 RID: 4452 RVA: 0x0001EBB2 File Offset: 0x0001CDB2
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x06001165 RID: 4453 RVA: 0x0001EBBB File Offset: 0x0001CDBB
			internal void <DownloadAsync>b__0(Result r)
			{
				this.downloadStarted = true;
			}

			// Token: 0x04000D73 RID: 3443
			public bool downloadStarted;
		}

		// Token: 0x02000245 RID: 581
		[CompilerGenerated]
		private sealed class <DownloadAsync>d__9 : IAsyncStateMachine
		{
			// Token: 0x06001166 RID: 4454 RVA: 0x0001EBC4 File Offset: 0x0001CDC4
			public <DownloadAsync>d__9()
			{
			}

			// Token: 0x06001167 RID: 4455 RVA: 0x0001EBD0 File Offset: 0x0001CDD0
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				bool isInstalled;
				try
				{
					TaskAwaiter taskAwaiter2;
					TaskAwaiter taskAwaiter;
					TaskAwaiter taskAwaiter3;
					switch (num)
					{
					case 0:
						break;
					case 1:
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num = (num2 = -1);
						goto IL_237;
					case 2:
						taskAwaiter3 = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num = (num2 = -1);
						goto IL_309;
					default:
					{
						item = new Item(fileId);
						bool flag = ct == default(CancellationToken);
						if (flag)
						{
							ct = new CancellationTokenSource(TimeSpan.FromSeconds(60.0)).Token;
						}
						Action<float> action = progress;
						if (action != null)
						{
							action(0f);
						}
						bool flag2 = !SteamUGC.Download(fileId, true);
						if (flag2)
						{
							isInstalled = item.IsInstalled;
							goto IL_35A;
						}
						onDownloadStarted = null;
						break;
					}
					}
					try
					{
						if (num != 0)
						{
							CS$<>8__locals1 = new SteamUGC.<>c__DisplayClass9_0();
							CS$<>8__locals1.downloadStarted = false;
							onDownloadStarted = new Action<Result>(CS$<>8__locals1.<DownloadAsync>b__0);
							SteamUGC.OnDownloadItemResult += onDownloadStarted;
							goto IL_183;
						}
						TaskAwaiter taskAwaiter4 = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num = (num2 = -1);
						IL_17A:
						taskAwaiter4.GetResult();
						IL_183:
						if (!CS$<>8__locals1.downloadStarted)
						{
							bool isCancellationRequested = ct.IsCancellationRequested;
							if (!isCancellationRequested)
							{
								taskAwaiter4 = Task.Delay(milisecondsUpdateDelay).GetAwaiter();
								if (!taskAwaiter4.IsCompleted)
								{
									num = (num2 = 0);
									taskAwaiter2 = taskAwaiter4;
									SteamUGC.<DownloadAsync>d__9 <DownloadAsync>d__ = this;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, SteamUGC.<DownloadAsync>d__9>(ref taskAwaiter4, ref <DownloadAsync>d__);
									return;
								}
								goto IL_17A;
							}
						}
						CS$<>8__locals1 = null;
					}
					finally
					{
						if (num < 0)
						{
							SteamUGC.OnDownloadItemResult -= onDownloadStarted;
						}
					}
					onDownloadStarted = null;
					Action<float> action2 = progress;
					if (action2 != null)
					{
						action2(0.2f);
					}
					taskAwaiter = Task.Delay(milisecondsUpdateDelay).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						num = (num2 = 1);
						taskAwaiter2 = taskAwaiter;
						SteamUGC.<DownloadAsync>d__9 <DownloadAsync>d__ = this;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, SteamUGC.<DownloadAsync>d__9>(ref taskAwaiter, ref <DownloadAsync>d__);
						return;
					}
					IL_237:
					taskAwaiter.GetResult();
					goto IL_312;
					IL_309:
					taskAwaiter3.GetResult();
					IL_312:
					bool isCancellationRequested2 = ct.IsCancellationRequested;
					if (!isCancellationRequested2)
					{
						Action<float> action3 = progress;
						if (action3 != null)
						{
							action3(0.2f + item.DownloadAmount * 0.8f);
						}
						bool flag3 = !item.IsDownloading && item.IsInstalled;
						if (!flag3)
						{
							taskAwaiter3 = Task.Delay(milisecondsUpdateDelay).GetAwaiter();
							if (!taskAwaiter3.IsCompleted)
							{
								num = (num2 = 2);
								taskAwaiter2 = taskAwaiter3;
								SteamUGC.<DownloadAsync>d__9 <DownloadAsync>d__ = this;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, SteamUGC.<DownloadAsync>d__9>(ref taskAwaiter3, ref <DownloadAsync>d__);
								return;
							}
							goto IL_309;
						}
					}
					Action<float> action4 = progress;
					if (action4 != null)
					{
						action4(1f);
					}
					isInstalled = item.IsInstalled;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_35A:
				num2 = -2;
				this.<>t__builder.SetResult(isInstalled);
			}

			// Token: 0x06001168 RID: 4456 RVA: 0x0001EF80 File Offset: 0x0001D180
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000D74 RID: 3444
			public int <>1__state;

			// Token: 0x04000D75 RID: 3445
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000D76 RID: 3446
			public PublishedFileId fileId;

			// Token: 0x04000D77 RID: 3447
			public Action<float> progress;

			// Token: 0x04000D78 RID: 3448
			public int milisecondsUpdateDelay;

			// Token: 0x04000D79 RID: 3449
			public CancellationToken ct;

			// Token: 0x04000D7A RID: 3450
			private Item <item>5__1;

			// Token: 0x04000D7B RID: 3451
			private Action<Result> <onDownloadStarted>5__2;

			// Token: 0x04000D7C RID: 3452
			private SteamUGC.<>c__DisplayClass9_0 <>8__3;

			// Token: 0x04000D7D RID: 3453
			private TaskAwaiter <>u__1;
		}

		// Token: 0x02000246 RID: 582
		[CompilerGenerated]
		private sealed class <QueryFileAsync>d__10 : IAsyncStateMachine
		{
			// Token: 0x06001169 RID: 4457 RVA: 0x0001EF82 File Offset: 0x0001D182
			public <QueryFileAsync>d__10()
			{
			}

			// Token: 0x0600116A RID: 4458 RVA: 0x0001EF8C File Offset: 0x0001D18C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Item? result2;
				try
				{
					TaskAwaiter<ResultPage?> taskAwaiter;
					if (num != 0)
					{
						taskAwaiter = Query.All.WithFileId(new PublishedFileId[]
						{
							fileId
						}).GetPageAsync(1).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<ResultPage?> taskAwaiter2 = taskAwaiter;
							SteamUGC.<QueryFileAsync>d__10 <QueryFileAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<ResultPage?>, SteamUGC.<QueryFileAsync>d__10>(ref taskAwaiter, ref <QueryFileAsync>d__);
							return;
						}
					}
					else
					{
						TaskAwaiter<ResultPage?> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<ResultPage?>);
						num2 = -1;
					}
					resultPage = taskAwaiter.GetResult();
					result = resultPage;
					resultPage = null;
					bool flag = result == null || result.Value.ResultCount != 1;
					if (flag)
					{
						result2 = null;
					}
					else
					{
						item = result.Value.Entries.First<Item>();
						result.Value.Dispose();
						result2 = new Item?(item);
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x0600116B RID: 4459 RVA: 0x0001F10C File Offset: 0x0001D30C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000D7E RID: 3454
			public int <>1__state;

			// Token: 0x04000D7F RID: 3455
			public AsyncTaskMethodBuilder<Item?> <>t__builder;

			// Token: 0x04000D80 RID: 3456
			public PublishedFileId fileId;

			// Token: 0x04000D81 RID: 3457
			private ResultPage? <result>5__1;

			// Token: 0x04000D82 RID: 3458
			private Item <item>5__2;

			// Token: 0x04000D83 RID: 3459
			private ResultPage? <>s__3;

			// Token: 0x04000D84 RID: 3460
			private TaskAwaiter<ResultPage?> <>u__1;
		}

		// Token: 0x02000247 RID: 583
		[CompilerGenerated]
		private sealed class <StartPlaytimeTracking>d__11 : IAsyncStateMachine
		{
			// Token: 0x0600116C RID: 4460 RVA: 0x0001F10E File Offset: 0x0001D30E
			public <StartPlaytimeTracking>d__11()
			{
			}

			// Token: 0x0600116D RID: 4461 RVA: 0x0001F118 File Offset: 0x0001D318
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				bool result2;
				try
				{
					CallResult<StartPlaytimeTrackingResult_t> callResult;
					if (num != 0)
					{
						callResult = SteamUGC.Internal.StartPlaytimeTracking(new PublishedFileId[]
						{
							fileId
						}, 1U).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<StartPlaytimeTrackingResult_t> callResult2 = callResult;
							SteamUGC.<StartPlaytimeTracking>d__11 <StartPlaytimeTracking>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<StartPlaytimeTrackingResult_t>, SteamUGC.<StartPlaytimeTracking>d__11>(ref callResult, ref <StartPlaytimeTracking>d__);
							return;
						}
					}
					else
					{
						CallResult<StartPlaytimeTrackingResult_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<StartPlaytimeTrackingResult_t>);
						num2 = -1;
					}
					startPlaytimeTrackingResult_t = callResult.GetResult();
					result = startPlaytimeTrackingResult_t;
					startPlaytimeTrackingResult_t = null;
					result2 = (result.Value.Result == Result.OK);
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x0600116E RID: 4462 RVA: 0x0001F224 File Offset: 0x0001D424
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000D85 RID: 3461
			public int <>1__state;

			// Token: 0x04000D86 RID: 3462
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000D87 RID: 3463
			public PublishedFileId fileId;

			// Token: 0x04000D88 RID: 3464
			private StartPlaytimeTrackingResult_t? <result>5__1;

			// Token: 0x04000D89 RID: 3465
			private StartPlaytimeTrackingResult_t? <>s__2;

			// Token: 0x04000D8A RID: 3466
			private CallResult<StartPlaytimeTrackingResult_t> <>u__1;
		}

		// Token: 0x02000248 RID: 584
		[CompilerGenerated]
		private sealed class <StopPlaytimeTracking>d__12 : IAsyncStateMachine
		{
			// Token: 0x0600116F RID: 4463 RVA: 0x0001F226 File Offset: 0x0001D426
			public <StopPlaytimeTracking>d__12()
			{
			}

			// Token: 0x06001170 RID: 4464 RVA: 0x0001F230 File Offset: 0x0001D430
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				bool result2;
				try
				{
					CallResult<StopPlaytimeTrackingResult_t> callResult;
					if (num != 0)
					{
						callResult = SteamUGC.Internal.StopPlaytimeTracking(new PublishedFileId[]
						{
							fileId
						}, 1U).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<StopPlaytimeTrackingResult_t> callResult2 = callResult;
							SteamUGC.<StopPlaytimeTracking>d__12 <StopPlaytimeTracking>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<StopPlaytimeTrackingResult_t>, SteamUGC.<StopPlaytimeTracking>d__12>(ref callResult, ref <StopPlaytimeTracking>d__);
							return;
						}
					}
					else
					{
						CallResult<StopPlaytimeTrackingResult_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<StopPlaytimeTrackingResult_t>);
						num2 = -1;
					}
					stopPlaytimeTrackingResult_t = callResult.GetResult();
					result = stopPlaytimeTrackingResult_t;
					stopPlaytimeTrackingResult_t = null;
					result2 = (result.Value.Result == Result.OK);
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06001171 RID: 4465 RVA: 0x0001F33C File Offset: 0x0001D53C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000D8B RID: 3467
			public int <>1__state;

			// Token: 0x04000D8C RID: 3468
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000D8D RID: 3469
			public PublishedFileId fileId;

			// Token: 0x04000D8E RID: 3470
			private StopPlaytimeTrackingResult_t? <result>5__1;

			// Token: 0x04000D8F RID: 3471
			private StopPlaytimeTrackingResult_t? <>s__2;

			// Token: 0x04000D90 RID: 3472
			private CallResult<StopPlaytimeTrackingResult_t> <>u__1;
		}

		// Token: 0x02000249 RID: 585
		[CompilerGenerated]
		private sealed class <StopPlaytimeTrackingForAllItems>d__13 : IAsyncStateMachine
		{
			// Token: 0x06001172 RID: 4466 RVA: 0x0001F33E File Offset: 0x0001D53E
			public <StopPlaytimeTrackingForAllItems>d__13()
			{
			}

			// Token: 0x06001173 RID: 4467 RVA: 0x0001F348 File Offset: 0x0001D548
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				bool result2;
				try
				{
					CallResult<StopPlaytimeTrackingResult_t> callResult;
					if (num != 0)
					{
						callResult = SteamUGC.Internal.StopPlaytimeTrackingForAllItems().GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<StopPlaytimeTrackingResult_t> callResult2 = callResult;
							SteamUGC.<StopPlaytimeTrackingForAllItems>d__13 <StopPlaytimeTrackingForAllItems>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<StopPlaytimeTrackingResult_t>, SteamUGC.<StopPlaytimeTrackingForAllItems>d__13>(ref callResult, ref <StopPlaytimeTrackingForAllItems>d__);
							return;
						}
					}
					else
					{
						CallResult<StopPlaytimeTrackingResult_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<StopPlaytimeTrackingResult_t>);
						num2 = -1;
					}
					stopPlaytimeTrackingResult_t = callResult.GetResult();
					result = stopPlaytimeTrackingResult_t;
					stopPlaytimeTrackingResult_t = null;
					result2 = (result.Value.Result == Result.OK);
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06001174 RID: 4468 RVA: 0x0001F440 File Offset: 0x0001D640
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000D91 RID: 3473
			public int <>1__state;

			// Token: 0x04000D92 RID: 3474
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000D93 RID: 3475
			private StopPlaytimeTrackingResult_t? <result>5__1;

			// Token: 0x04000D94 RID: 3476
			private StopPlaytimeTrackingResult_t? <>s__2;

			// Token: 0x04000D95 RID: 3477
			private CallResult<StopPlaytimeTrackingResult_t> <>u__1;
		}
	}
}
