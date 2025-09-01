using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x020000B2 RID: 178
	public struct InventoryResult : IDisposable
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00011C1A File Offset: 0x0000FE1A
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x00011C22 File Offset: 0x0000FE22
		public bool Expired
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Expired>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Expired>k__BackingField = value;
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00011C2B File Offset: 0x0000FE2B
		internal InventoryResult(SteamInventoryResult_t id, bool expired)
		{
			this._id = id;
			this.Expired = expired;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00011C40 File Offset: 0x0000FE40
		public int ItemCount
		{
			get
			{
				uint num = 0U;
				bool flag = !SteamInventory.Internal.GetResultItems(this._id, null, ref num);
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = (int)num;
				}
				return result;
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00011C74 File Offset: 0x0000FE74
		public bool BelongsTo(SteamId steamId)
		{
			return SteamInventory.Internal.CheckResultSteamID(this._id, steamId);
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00011C98 File Offset: 0x0000FE98
		public InventoryItem[] GetItems(bool includeProperties = false)
		{
			uint itemCount = (uint)this.ItemCount;
			bool flag = itemCount <= 0U;
			InventoryItem[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				SteamItemDetails_t[] array = new SteamItemDetails_t[itemCount];
				bool flag2 = !SteamInventory.Internal.GetResultItems(this._id, array, ref itemCount);
				if (flag2)
				{
					result = null;
				}
				else
				{
					InventoryItem[] array2 = new InventoryItem[itemCount];
					int num = 0;
					while ((long)num < (long)((ulong)itemCount))
					{
						InventoryItem inventoryItem = InventoryItem.From(array[num]);
						if (includeProperties)
						{
							inventoryItem._properties = InventoryItem.GetProperties(this._id, num);
						}
						array2[num] = inventoryItem;
						num++;
					}
					result = array2;
				}
			}
			return result;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00011D44 File Offset: 0x0000FF44
		public void Dispose()
		{
			bool flag = this._id.Value == -1;
			if (!flag)
			{
				SteamInventory.Internal.DestroyResult(this._id);
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00011D78 File Offset: 0x0000FF78
		internal static async Task<InventoryResult?> GetAsync(SteamInventoryResult_t sresult)
		{
			Result _result = Result.Pending;
			while (_result == Result.Pending)
			{
				_result = SteamInventory.Internal.GetResultStatus(sresult);
				await Task.Delay(10);
			}
			InventoryResult? result;
			if (_result != Result.OK && _result != Result.Expired)
			{
				result = null;
			}
			else
			{
				result = new InventoryResult?(new InventoryResult(sresult, _result == Result.Expired));
			}
			return result;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00011DC0 File Offset: 0x0000FFC0
		public unsafe byte[] Serialize()
		{
			uint num = 0U;
			bool flag = !SteamInventory.Internal.SerializeResult(this._id, IntPtr.Zero, ref num);
			byte[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				byte[] array = new byte[num];
				byte[] array2;
				byte* value;
				if ((array2 = array) == null || array2.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array2[0];
				}
				bool flag2 = !SteamInventory.Internal.SerializeResult(this._id, (IntPtr)((void*)value), ref num);
				if (flag2)
				{
					result = null;
				}
				else
				{
					array2 = null;
					result = array;
				}
			}
			return result;
		}

		// Token: 0x04000760 RID: 1888
		internal SteamInventoryResult_t _id;

		// Token: 0x04000761 RID: 1889
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <Expired>k__BackingField;

		// Token: 0x0200026A RID: 618
		[CompilerGenerated]
		private sealed class <GetAsync>d__11 : IAsyncStateMachine
		{
			// Token: 0x060011F2 RID: 4594 RVA: 0x000212FE File Offset: 0x0001F4FE
			public <GetAsync>d__11()
			{
			}

			// Token: 0x060011F3 RID: 4595 RVA: 0x00021308 File Offset: 0x0001F508
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				InventoryResult? result;
				try
				{
					if (num != 0)
					{
						_result = Result.Pending;
						goto IL_92;
					}
					TaskAwaiter taskAwaiter2;
					TaskAwaiter taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter);
					num2 = -1;
					IL_89:
					taskAwaiter.GetResult();
					IL_92:
					if (_result != Result.Pending)
					{
						bool flag = _result != Result.OK && _result != Result.Expired;
						if (flag)
						{
							result = null;
						}
						else
						{
							result = new InventoryResult?(new InventoryResult(sresult, _result == Result.Expired));
						}
					}
					else
					{
						_result = SteamInventory.Internal.GetResultStatus(sresult);
						taskAwaiter = Task.Delay(10).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							taskAwaiter2 = taskAwaiter;
							InventoryResult.<GetAsync>d__11 <GetAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, InventoryResult.<GetAsync>d__11>(ref taskAwaiter, ref <GetAsync>d__);
							return;
						}
						goto IL_89;
					}
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

			// Token: 0x060011F4 RID: 4596 RVA: 0x00021440 File Offset: 0x0001F640
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000E48 RID: 3656
			public int <>1__state;

			// Token: 0x04000E49 RID: 3657
			public AsyncTaskMethodBuilder<InventoryResult?> <>t__builder;

			// Token: 0x04000E4A RID: 3658
			public SteamInventoryResult_t sresult;

			// Token: 0x04000E4B RID: 3659
			private Result <_result>5__1;

			// Token: 0x04000E4C RID: 3660
			private TaskAwaiter <>u__1;
		}
	}
}
