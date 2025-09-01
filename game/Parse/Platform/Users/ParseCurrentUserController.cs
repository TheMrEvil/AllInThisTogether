using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Data;
using Parse.Abstractions.Platform.Objects;
using Parse.Abstractions.Platform.Users;
using Parse.Infrastructure.Data;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Users
{
	// Token: 0x02000025 RID: 37
	public class ParseCurrentUserController : IParseCurrentUserController, IParseObjectCurrentController<ParseUser>
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00008933 File Offset: 0x00006B33
		private object Mutex
		{
			[CompilerGenerated]
			get
			{
				return this.<Mutex>k__BackingField;
			}
		} = new object();

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000893B File Offset: 0x00006B3B
		private TaskQueue TaskQueue
		{
			[CompilerGenerated]
			get
			{
				return this.<TaskQueue>k__BackingField;
			}
		} = new TaskQueue();

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00008943 File Offset: 0x00006B43
		private ICacheController StorageController
		{
			[CompilerGenerated]
			get
			{
				return this.<StorageController>k__BackingField;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000894B File Offset: 0x00006B4B
		private IParseObjectClassController ClassController
		{
			[CompilerGenerated]
			get
			{
				return this.<ClassController>k__BackingField;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00008953 File Offset: 0x00006B53
		private IParseDataDecoder Decoder
		{
			[CompilerGenerated]
			get
			{
				return this.<Decoder>k__BackingField;
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000895C File Offset: 0x00006B5C
		public ParseCurrentUserController(ICacheController storageController, IParseObjectClassController classController, IParseDataDecoder decoder)
		{
			this.StorageController = storageController;
			this.ClassController = classController;
			this.Decoder = decoder;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000089A0 File Offset: 0x00006BA0
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x000089E4 File Offset: 0x00006BE4
		public ParseUser CurrentUser
		{
			get
			{
				object mutex = this.Mutex;
				ParseUser result;
				lock (mutex)
				{
					result = this.currentUser;
				}
				return result;
			}
			set
			{
				object mutex = this.Mutex;
				lock (mutex)
				{
					this.currentUser = value;
				}
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008A28 File Offset: 0x00006C28
		public Task SetAsync(ParseUser user, CancellationToken cancellationToken)
		{
			Func<Task, Task> <>9__1;
			return this.TaskQueue.Enqueue<Task>(delegate(Task toAwait)
			{
				Func<Task, Task> continuationFunction;
				if ((continuationFunction = <>9__1) == null)
				{
					continuationFunction = (<>9__1 = delegate(Task _)
					{
						Task result;
						if (user == null)
						{
							result = this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> task) => task.Result.RemoveAsync("CurrentUser")).Unwrap();
						}
						else
						{
							IDictionary<string, object> data = user.ServerDataToJSONObjectForSerialization();
							data["objectId"] = user.ObjectId;
							if (user.CreatedAt != null)
							{
								data["createdAt"] = user.CreatedAt.Value.ToString(ParseClient.DateFormatStrings.First<string>(), CultureInfo.InvariantCulture);
							}
							if (user.UpdatedAt != null)
							{
								data["updatedAt"] = user.UpdatedAt.Value.ToString(ParseClient.DateFormatStrings.First<string>(), CultureInfo.InvariantCulture);
							}
							result = this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> task) => task.Result.AddAsync("CurrentUser", JsonUtilities.Encode(data))).Unwrap();
						}
						this.CurrentUser = user;
						return result;
					});
				}
				return toAwait.ContinueWith<Task>(continuationFunction).Unwrap();
			}, cancellationToken);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008A64 File Offset: 0x00006C64
		public Task<ParseUser> GetAsync(IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			object mutex = this.Mutex;
			ParseUser parseUser;
			lock (mutex)
			{
				parseUser = this.CurrentUser;
			}
			if (parseUser == null)
			{
				Func<Task<IDataCache<string, object>>, ParseUser> <>9__2;
				Func<Task, Task<ParseUser>> <>9__1;
				return this.TaskQueue.Enqueue<Task<ParseUser>>(delegate(Task toAwait)
				{
					Func<Task, Task<ParseUser>> continuationFunction;
					if ((continuationFunction = <>9__1) == null)
					{
						continuationFunction = (<>9__1 = delegate(Task _)
						{
							Task<IDataCache<string, object>> task2 = this.StorageController.LoadAsync();
							Func<Task<IDataCache<string, object>>, ParseUser> continuation;
							if ((continuation = <>9__2) == null)
							{
								continuation = (<>9__2 = delegate(Task<IDataCache<string, object>> task)
								{
									object obj;
									task.Result.TryGetValue("CurrentUser", out obj);
									ParseUser parseUser2 = null;
									string text = obj as string;
									if (text != null)
									{
										parseUser2 = this.ClassController.GenerateObjectFromState(ParseObjectCoder.Instance.Decode(JsonUtilities.Parse(text) as IDictionary<string, object>, this.Decoder, serviceHub), "_User", serviceHub);
									}
									return this.CurrentUser = parseUser2;
								});
							}
							return task2.OnSuccess(continuation);
						});
					}
					return toAwait.ContinueWith<Task<ParseUser>>(continuationFunction).Unwrap<ParseUser>();
				}, cancellationToken);
			}
			return Task.FromResult<ParseUser>(parseUser);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008ADC File Offset: 0x00006CDC
		public Task<bool> ExistsAsync(CancellationToken cancellationToken)
		{
			if (this.CurrentUser == null)
			{
				return this.TaskQueue.Enqueue<Task<bool>>((Task toAwait) => toAwait.ContinueWith<Task<bool>>((Task _) => this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> t) => t.Result.ContainsKey("CurrentUser"))).Unwrap<bool>(), cancellationToken);
			}
			return Task.FromResult<bool>(true);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008B08 File Offset: 0x00006D08
		public bool IsCurrent(ParseUser user)
		{
			object mutex = this.Mutex;
			bool result;
			lock (mutex)
			{
				result = (this.CurrentUser == user);
			}
			return result;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008B50 File Offset: 0x00006D50
		public void ClearFromMemory()
		{
			this.CurrentUser = null;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008B5C File Offset: 0x00006D5C
		public void ClearFromDisk()
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				this.ClearFromMemory();
				this.TaskQueue.Enqueue<Task>((Task toAwait) => toAwait.ContinueWith<Task<Task>>((Task _) => this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> t) => t.Result.RemoveAsync("CurrentUser"))).Unwrap<Task>().Unwrap(), CancellationToken.None);
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008BBC File Offset: 0x00006DBC
		public Task<string> GetCurrentSessionTokenAsync(IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetAsync(serviceHub, cancellationToken).OnSuccess(delegate(Task<ParseUser> task)
			{
				ParseUser result = task.Result;
				if (result == null)
				{
					return null;
				}
				return result.SessionToken;
			});
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008BEC File Offset: 0x00006DEC
		public Task LogOutAsync(IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			Func<Task, Task<ParseUser>> <>9__1;
			Action<Task<ParseUser>> <>9__2;
			return this.TaskQueue.Enqueue<Task>(delegate(Task toAwait)
			{
				Func<Task, Task<ParseUser>> continuationFunction;
				if ((continuationFunction = <>9__1) == null)
				{
					continuationFunction = (<>9__1 = ((Task _) => this.GetAsync(serviceHub, cancellationToken)));
				}
				Task<ParseUser> task2 = toAwait.ContinueWith<Task<ParseUser>>(continuationFunction).Unwrap<ParseUser>();
				Action<Task<ParseUser>> continuation;
				if ((continuation = <>9__2) == null)
				{
					continuation = (<>9__2 = delegate(Task<ParseUser> task)
					{
						this.ClearFromDisk();
					});
				}
				return task2.OnSuccess(continuation);
			}, cancellationToken);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008C31 File Offset: 0x00006E31
		[CompilerGenerated]
		private Task<bool> <ExistsAsync>b__22_0(Task toAwait)
		{
			return toAwait.ContinueWith<Task<bool>>((Task _) => this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> t) => t.Result.ContainsKey("CurrentUser"))).Unwrap<bool>();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008C4A File Offset: 0x00006E4A
		[CompilerGenerated]
		private Task<bool> <ExistsAsync>b__22_1(Task _)
		{
			return this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> t) => t.Result.ContainsKey("CurrentUser"));
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008C7B File Offset: 0x00006E7B
		[CompilerGenerated]
		private Task <ClearFromDisk>b__25_0(Task toAwait)
		{
			return toAwait.ContinueWith<Task<Task>>((Task _) => this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> t) => t.Result.RemoveAsync("CurrentUser"))).Unwrap<Task>().Unwrap();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008C99 File Offset: 0x00006E99
		[CompilerGenerated]
		private Task<Task> <ClearFromDisk>b__25_1(Task _)
		{
			return this.StorageController.LoadAsync().OnSuccess((Task<IDataCache<string, object>> t) => t.Result.RemoveAsync("CurrentUser"));
		}

		// Token: 0x0400003B RID: 59
		[CompilerGenerated]
		private readonly object <Mutex>k__BackingField;

		// Token: 0x0400003C RID: 60
		[CompilerGenerated]
		private readonly TaskQueue <TaskQueue>k__BackingField;

		// Token: 0x0400003D RID: 61
		[CompilerGenerated]
		private readonly ICacheController <StorageController>k__BackingField;

		// Token: 0x0400003E RID: 62
		[CompilerGenerated]
		private readonly IParseObjectClassController <ClassController>k__BackingField;

		// Token: 0x0400003F RID: 63
		[CompilerGenerated]
		private readonly IParseDataDecoder <Decoder>k__BackingField;

		// Token: 0x04000040 RID: 64
		private ParseUser currentUser;

		// Token: 0x020000E7 RID: 231
		[CompilerGenerated]
		private sealed class <>c__DisplayClass20_0
		{
			// Token: 0x06000692 RID: 1682 RVA: 0x000147C7 File Offset: 0x000129C7
			public <>c__DisplayClass20_0()
			{
			}

			// Token: 0x06000693 RID: 1683 RVA: 0x000147D0 File Offset: 0x000129D0
			internal Task <SetAsync>b__0(Task toAwait)
			{
				Func<Task, Task> continuationFunction;
				if ((continuationFunction = this.<>9__1) == null)
				{
					continuationFunction = (this.<>9__1 = delegate(Task _)
					{
						Task result;
						if (this.user == null)
						{
							result = this.<>4__this.StorageController.LoadAsync().OnSuccess(new Func<Task<IDataCache<string, object>>, Task>(ParseCurrentUserController.<>c.<>9.<SetAsync>b__20_2)).Unwrap();
						}
						else
						{
							ParseCurrentUserController.<>c__DisplayClass20_1 CS$<>8__locals1 = new ParseCurrentUserController.<>c__DisplayClass20_1();
							CS$<>8__locals1.data = this.user.ServerDataToJSONObjectForSerialization();
							CS$<>8__locals1.data["objectId"] = this.user.ObjectId;
							if (this.user.CreatedAt != null)
							{
								CS$<>8__locals1.data["createdAt"] = this.user.CreatedAt.Value.ToString(ParseClient.DateFormatStrings.First<string>(), CultureInfo.InvariantCulture);
							}
							if (this.user.UpdatedAt != null)
							{
								CS$<>8__locals1.data["updatedAt"] = this.user.UpdatedAt.Value.ToString(ParseClient.DateFormatStrings.First<string>(), CultureInfo.InvariantCulture);
							}
							result = this.<>4__this.StorageController.LoadAsync().OnSuccess(new Func<Task<IDataCache<string, object>>, Task>(CS$<>8__locals1.<SetAsync>b__3)).Unwrap();
						}
						this.<>4__this.CurrentUser = this.user;
						return result;
					});
				}
				return toAwait.ContinueWith<Task>(continuationFunction).Unwrap();
			}

			// Token: 0x06000694 RID: 1684 RVA: 0x00014808 File Offset: 0x00012A08
			internal Task <SetAsync>b__1(Task _)
			{
				Task result;
				if (this.user == null)
				{
					result = this.<>4__this.StorageController.LoadAsync().OnSuccess(new Func<Task<IDataCache<string, object>>, Task>(ParseCurrentUserController.<>c.<>9.<SetAsync>b__20_2)).Unwrap();
				}
				else
				{
					ParseCurrentUserController.<>c__DisplayClass20_1 CS$<>8__locals1 = new ParseCurrentUserController.<>c__DisplayClass20_1();
					CS$<>8__locals1.data = this.user.ServerDataToJSONObjectForSerialization();
					CS$<>8__locals1.data["objectId"] = this.user.ObjectId;
					if (this.user.CreatedAt != null)
					{
						CS$<>8__locals1.data["createdAt"] = this.user.CreatedAt.Value.ToString(ParseClient.DateFormatStrings.First<string>(), CultureInfo.InvariantCulture);
					}
					if (this.user.UpdatedAt != null)
					{
						CS$<>8__locals1.data["updatedAt"] = this.user.UpdatedAt.Value.ToString(ParseClient.DateFormatStrings.First<string>(), CultureInfo.InvariantCulture);
					}
					result = this.<>4__this.StorageController.LoadAsync().OnSuccess(new Func<Task<IDataCache<string, object>>, Task>(CS$<>8__locals1.<SetAsync>b__3)).Unwrap();
				}
				this.<>4__this.CurrentUser = this.user;
				return result;
			}

			// Token: 0x040001D2 RID: 466
			public ParseUser user;

			// Token: 0x040001D3 RID: 467
			public ParseCurrentUserController <>4__this;

			// Token: 0x040001D4 RID: 468
			public Func<Task, Task> <>9__1;
		}

		// Token: 0x020000E8 RID: 232
		[CompilerGenerated]
		private sealed class <>c__DisplayClass20_1
		{
			// Token: 0x06000695 RID: 1685 RVA: 0x00014967 File Offset: 0x00012B67
			public <>c__DisplayClass20_1()
			{
			}

			// Token: 0x06000696 RID: 1686 RVA: 0x0001496F File Offset: 0x00012B6F
			internal Task <SetAsync>b__3(Task<IDataCache<string, object>> task)
			{
				return task.Result.AddAsync("CurrentUser", JsonUtilities.Encode(this.data));
			}

			// Token: 0x040001D5 RID: 469
			public IDictionary<string, object> data;
		}

		// Token: 0x020000E9 RID: 233
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000697 RID: 1687 RVA: 0x0001498C File Offset: 0x00012B8C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000698 RID: 1688 RVA: 0x00014998 File Offset: 0x00012B98
			public <>c()
			{
			}

			// Token: 0x06000699 RID: 1689 RVA: 0x000149A0 File Offset: 0x00012BA0
			internal Task <SetAsync>b__20_2(Task<IDataCache<string, object>> task)
			{
				return task.Result.RemoveAsync("CurrentUser");
			}

			// Token: 0x0600069A RID: 1690 RVA: 0x000149B2 File Offset: 0x00012BB2
			internal bool <ExistsAsync>b__22_2(Task<IDataCache<string, object>> t)
			{
				return t.Result.ContainsKey("CurrentUser");
			}

			// Token: 0x0600069B RID: 1691 RVA: 0x000149C4 File Offset: 0x00012BC4
			internal Task <ClearFromDisk>b__25_2(Task<IDataCache<string, object>> t)
			{
				return t.Result.RemoveAsync("CurrentUser");
			}

			// Token: 0x0600069C RID: 1692 RVA: 0x000149D6 File Offset: 0x00012BD6
			internal string <GetCurrentSessionTokenAsync>b__26_0(Task<ParseUser> task)
			{
				ParseUser result = task.Result;
				if (result == null)
				{
					return null;
				}
				return result.SessionToken;
			}

			// Token: 0x040001D6 RID: 470
			public static readonly ParseCurrentUserController.<>c <>9 = new ParseCurrentUserController.<>c();

			// Token: 0x040001D7 RID: 471
			public static Func<Task<IDataCache<string, object>>, Task> <>9__20_2;

			// Token: 0x040001D8 RID: 472
			public static Func<Task<IDataCache<string, object>>, bool> <>9__22_2;

			// Token: 0x040001D9 RID: 473
			public static Func<Task<IDataCache<string, object>>, Task> <>9__25_2;

			// Token: 0x040001DA RID: 474
			public static Func<Task<ParseUser>, string> <>9__26_0;
		}

		// Token: 0x020000EA RID: 234
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_0
		{
			// Token: 0x0600069D RID: 1693 RVA: 0x000149E9 File Offset: 0x00012BE9
			public <>c__DisplayClass21_0()
			{
			}

			// Token: 0x0600069E RID: 1694 RVA: 0x000149F4 File Offset: 0x00012BF4
			internal Task<ParseUser> <GetAsync>b__0(Task toAwait)
			{
				Func<Task, Task<ParseUser>> continuationFunction;
				if ((continuationFunction = this.<>9__1) == null)
				{
					continuationFunction = (this.<>9__1 = delegate(Task _)
					{
						Task<IDataCache<string, object>> task2 = this.<>4__this.StorageController.LoadAsync();
						Func<Task<IDataCache<string, object>>, ParseUser> continuation;
						if ((continuation = this.<>9__2) == null)
						{
							continuation = (this.<>9__2 = delegate(Task<IDataCache<string, object>> task)
							{
								object obj;
								task.Result.TryGetValue("CurrentUser", out obj);
								ParseUser currentUser = null;
								string text = obj as string;
								if (text != null)
								{
									currentUser = this.<>4__this.ClassController.GenerateObjectFromState(ParseObjectCoder.Instance.Decode(JsonUtilities.Parse(text) as IDictionary<string, object>, this.<>4__this.Decoder, this.serviceHub), "_User", this.serviceHub);
								}
								return this.<>4__this.CurrentUser = currentUser;
							});
						}
						return task2.OnSuccess(continuation);
					});
				}
				return toAwait.ContinueWith<Task<ParseUser>>(continuationFunction).Unwrap<ParseUser>();
			}

			// Token: 0x0600069F RID: 1695 RVA: 0x00014A2C File Offset: 0x00012C2C
			internal Task<ParseUser> <GetAsync>b__1(Task _)
			{
				Task<IDataCache<string, object>> task2 = this.<>4__this.StorageController.LoadAsync();
				Func<Task<IDataCache<string, object>>, ParseUser> continuation;
				if ((continuation = this.<>9__2) == null)
				{
					continuation = (this.<>9__2 = delegate(Task<IDataCache<string, object>> task)
					{
						object obj;
						task.Result.TryGetValue("CurrentUser", out obj);
						ParseUser currentUser = null;
						string text = obj as string;
						if (text != null)
						{
							currentUser = this.<>4__this.ClassController.GenerateObjectFromState(ParseObjectCoder.Instance.Decode(JsonUtilities.Parse(text) as IDictionary<string, object>, this.<>4__this.Decoder, this.serviceHub), "_User", this.serviceHub);
						}
						return this.<>4__this.CurrentUser = currentUser;
					});
				}
				return task2.OnSuccess(continuation);
			}

			// Token: 0x060006A0 RID: 1696 RVA: 0x00014A70 File Offset: 0x00012C70
			internal ParseUser <GetAsync>b__2(Task<IDataCache<string, object>> task)
			{
				object obj;
				task.Result.TryGetValue("CurrentUser", out obj);
				ParseUser currentUser = null;
				string text = obj as string;
				if (text != null)
				{
					currentUser = this.<>4__this.ClassController.GenerateObjectFromState(ParseObjectCoder.Instance.Decode(JsonUtilities.Parse(text) as IDictionary<string, object>, this.<>4__this.Decoder, this.serviceHub), "_User", this.serviceHub);
				}
				return this.<>4__this.CurrentUser = currentUser;
			}

			// Token: 0x040001DB RID: 475
			public ParseCurrentUserController <>4__this;

			// Token: 0x040001DC RID: 476
			public IServiceHub serviceHub;

			// Token: 0x040001DD RID: 477
			public Func<Task<IDataCache<string, object>>, ParseUser> <>9__2;

			// Token: 0x040001DE RID: 478
			public Func<Task, Task<ParseUser>> <>9__1;
		}

		// Token: 0x020000EB RID: 235
		[CompilerGenerated]
		private sealed class <>c__DisplayClass27_0
		{
			// Token: 0x060006A1 RID: 1697 RVA: 0x00014AED File Offset: 0x00012CED
			public <>c__DisplayClass27_0()
			{
			}

			// Token: 0x060006A2 RID: 1698 RVA: 0x00014AF8 File Offset: 0x00012CF8
			internal Task <LogOutAsync>b__0(Task toAwait)
			{
				Func<Task, Task<ParseUser>> continuationFunction;
				if ((continuationFunction = this.<>9__1) == null)
				{
					continuationFunction = (this.<>9__1 = ((Task _) => this.<>4__this.GetAsync(this.serviceHub, this.cancellationToken)));
				}
				Task<ParseUser> task2 = toAwait.ContinueWith<Task<ParseUser>>(continuationFunction).Unwrap<ParseUser>();
				Action<Task<ParseUser>> continuation;
				if ((continuation = this.<>9__2) == null)
				{
					continuation = (this.<>9__2 = delegate(Task<ParseUser> task)
					{
						this.<>4__this.ClearFromDisk();
					});
				}
				return task2.OnSuccess(continuation);
			}

			// Token: 0x060006A3 RID: 1699 RVA: 0x00014B53 File Offset: 0x00012D53
			internal Task<ParseUser> <LogOutAsync>b__1(Task _)
			{
				return this.<>4__this.GetAsync(this.serviceHub, this.cancellationToken);
			}

			// Token: 0x060006A4 RID: 1700 RVA: 0x00014B6C File Offset: 0x00012D6C
			internal void <LogOutAsync>b__2(Task<ParseUser> task)
			{
				this.<>4__this.ClearFromDisk();
			}

			// Token: 0x040001DF RID: 479
			public ParseCurrentUserController <>4__this;

			// Token: 0x040001E0 RID: 480
			public IServiceHub serviceHub;

			// Token: 0x040001E1 RID: 481
			public CancellationToken cancellationToken;

			// Token: 0x040001E2 RID: 482
			public Func<Task, Task<ParseUser>> <>9__1;

			// Token: 0x040001E3 RID: 483
			public Action<Task<ParseUser>> <>9__2;
		}
	}
}
