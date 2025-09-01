using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x020000B0 RID: 176
	public struct InventoryItem : IEquatable<InventoryItem>
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00011702 File Offset: 0x0000F902
		public InventoryItemId Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0001170A File Offset: 0x0000F90A
		public InventoryDefId DefId
		{
			get
			{
				return this._def;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00011712 File Offset: 0x0000F912
		public int Quantity
		{
			get
			{
				return (int)this._quantity;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x0001171A File Offset: 0x0000F91A
		public InventoryDef Def
		{
			get
			{
				return SteamInventory.FindDefinition(this.DefId);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00011727 File Offset: 0x0000F927
		public Dictionary<string, string> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0001172F File Offset: 0x0000F92F
		public bool IsNoTrade
		{
			get
			{
				return this._flags.HasFlag(SteamItemFlags.NoTrade);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00011747 File Offset: 0x0000F947
		public bool IsRemoved
		{
			get
			{
				return this._flags.HasFlag(SteamItemFlags.Removed);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00011763 File Offset: 0x0000F963
		public bool IsConsumed
		{
			get
			{
				return this._flags.HasFlag(SteamItemFlags.Consumed);
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00011780 File Offset: 0x0000F980
		public async Task<InventoryResult?> ConsumeAsync(int amount = 1)
		{
			SteamInventoryResult_t sresult = Defines.k_SteamInventoryResultInvalid;
			bool flag = !SteamInventory.Internal.ConsumeItem(ref sresult, this.Id, (uint)amount);
			InventoryResult? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				InventoryResult? inventoryResult = await InventoryResult.GetAsync(sresult);
				result = inventoryResult;
			}
			return result;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000117D4 File Offset: 0x0000F9D4
		public async Task<InventoryResult?> SplitStackAsync(int quantity = 1)
		{
			SteamInventoryResult_t sresult = Defines.k_SteamInventoryResultInvalid;
			bool flag = !SteamInventory.Internal.TransferItemQuantity(ref sresult, this.Id, (uint)quantity, ulong.MaxValue);
			InventoryResult? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				InventoryResult? inventoryResult = await InventoryResult.GetAsync(sresult);
				result = inventoryResult;
			}
			return result;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00011828 File Offset: 0x0000FA28
		public async Task<InventoryResult?> AddAsync(InventoryItem add, int quantity = 1)
		{
			SteamInventoryResult_t sresult = Defines.k_SteamInventoryResultInvalid;
			bool flag = !SteamInventory.Internal.TransferItemQuantity(ref sresult, add.Id, (uint)quantity, this.Id);
			InventoryResult? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				InventoryResult? inventoryResult = await InventoryResult.GetAsync(sresult);
				result = inventoryResult;
			}
			return result;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00011884 File Offset: 0x0000FA84
		internal static InventoryItem From(SteamItemDetails_t details)
		{
			return new InventoryItem
			{
				_id = details.ItemId,
				_def = details.Definition,
				_flags = (SteamItemFlags)details.Flags,
				_quantity = details.Quantity
			};
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x000118D8 File Offset: 0x0000FAD8
		internal static Dictionary<string, string> GetProperties(SteamInventoryResult_t result, int index)
		{
			uint num = 32768U;
			string text;
			bool flag = !SteamInventory.Internal.GetResultItemProperty(result, (uint)index, null, out text, ref num);
			Dictionary<string, string> result2;
			if (flag)
			{
				result2 = null;
			}
			else
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				foreach (string text2 in text.Split(new char[]
				{
					','
				}))
				{
					num = 32768U;
					string value;
					bool resultItemProperty = SteamInventory.Internal.GetResultItemProperty(result, (uint)index, text2, out value, ref num);
					if (resultItemProperty)
					{
						dictionary.Add(text2, value);
					}
				}
				result2 = dictionary;
			}
			return result2;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00011974 File Offset: 0x0000FB74
		public DateTime Acquired
		{
			get
			{
				bool flag = this.Properties == null;
				DateTime result;
				if (flag)
				{
					result = DateTime.UtcNow;
				}
				else
				{
					string text;
					bool flag2 = this.Properties.TryGetValue("acquired", out text);
					if (flag2)
					{
						int year = int.Parse(text.Substring(0, 4));
						int month = int.Parse(text.Substring(4, 2));
						int day = int.Parse(text.Substring(6, 2));
						int hour = int.Parse(text.Substring(9, 2));
						int minute = int.Parse(text.Substring(11, 2));
						int second = int.Parse(text.Substring(13, 2));
						result = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
					}
					else
					{
						result = DateTime.UtcNow;
					}
				}
				return result;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00011A2C File Offset: 0x0000FC2C
		public string Origin
		{
			get
			{
				bool flag = this.Properties == null;
				string result;
				if (flag)
				{
					result = null;
				}
				else
				{
					string text;
					bool flag2 = this.Properties.TryGetValue("origin", out text);
					if (flag2)
					{
						result = text;
					}
					else
					{
						result = null;
					}
				}
				return result;
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00011A6A File Offset: 0x0000FC6A
		public static bool operator ==(InventoryItem a, InventoryItem b)
		{
			return a._id == b._id;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00011A7D File Offset: 0x0000FC7D
		public static bool operator !=(InventoryItem a, InventoryItem b)
		{
			return a._id != b._id;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00011A90 File Offset: 0x0000FC90
		public override bool Equals(object p)
		{
			return this.Equals((InventoryItem)p);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00011A9E File Offset: 0x0000FC9E
		public override int GetHashCode()
		{
			return this._id.GetHashCode();
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00011AB1 File Offset: 0x0000FCB1
		public bool Equals(InventoryItem p)
		{
			return p._id == this._id;
		}

		// Token: 0x04000758 RID: 1880
		internal InventoryItemId _id;

		// Token: 0x04000759 RID: 1881
		internal InventoryDefId _def;

		// Token: 0x0400075A RID: 1882
		internal SteamItemFlags _flags;

		// Token: 0x0400075B RID: 1883
		internal ushort _quantity;

		// Token: 0x0400075C RID: 1884
		internal Dictionary<string, string> _properties;

		// Token: 0x02000263 RID: 611
		public struct Amount
		{
			// Token: 0x04000E29 RID: 3625
			public InventoryItem Item;

			// Token: 0x04000E2A RID: 3626
			public int Quantity;
		}

		// Token: 0x02000264 RID: 612
		[CompilerGenerated]
		private sealed class <ConsumeAsync>d__21 : IAsyncStateMachine
		{
			// Token: 0x060011E2 RID: 4578 RVA: 0x00020EA5 File Offset: 0x0001F0A5
			public <ConsumeAsync>d__21()
			{
			}

			// Token: 0x060011E3 RID: 4579 RVA: 0x00020EB0 File Offset: 0x0001F0B0
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				InventoryResult? result;
				try
				{
					TaskAwaiter<InventoryResult?> taskAwaiter;
					if (num != 0)
					{
						sresult = Defines.k_SteamInventoryResultInvalid;
						bool flag = !SteamInventory.Internal.ConsumeItem(ref sresult, base.Id, (uint)amount);
						if (flag)
						{
							result = null;
							goto IL_DD;
						}
						taskAwaiter = InventoryResult.GetAsync(sresult).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<InventoryResult?> taskAwaiter2 = taskAwaiter;
							InventoryItem.<ConsumeAsync>d__21 <ConsumeAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<InventoryResult?>, InventoryItem.<ConsumeAsync>d__21>(ref taskAwaiter, ref <ConsumeAsync>d__);
							return;
						}
					}
					else
					{
						TaskAwaiter<InventoryResult?> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<InventoryResult?>);
						num2 = -1;
					}
					inventoryResult = taskAwaiter.GetResult();
					result = inventoryResult;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_DD:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060011E4 RID: 4580 RVA: 0x00020FC0 File Offset: 0x0001F1C0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000E2B RID: 3627
			public int <>1__state;

			// Token: 0x04000E2C RID: 3628
			public AsyncTaskMethodBuilder<InventoryResult?> <>t__builder;

			// Token: 0x04000E2D RID: 3629
			public int amount;

			// Token: 0x04000E2E RID: 3630
			public InventoryItem <>4__this;

			// Token: 0x04000E2F RID: 3631
			private SteamInventoryResult_t <sresult>5__1;

			// Token: 0x04000E30 RID: 3632
			private InventoryResult? <>s__2;

			// Token: 0x04000E31 RID: 3633
			private TaskAwaiter<InventoryResult?> <>u__1;
		}

		// Token: 0x02000265 RID: 613
		[CompilerGenerated]
		private sealed class <SplitStackAsync>d__22 : IAsyncStateMachine
		{
			// Token: 0x060011E5 RID: 4581 RVA: 0x00020FC2 File Offset: 0x0001F1C2
			public <SplitStackAsync>d__22()
			{
			}

			// Token: 0x060011E6 RID: 4582 RVA: 0x00020FCC File Offset: 0x0001F1CC
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				InventoryResult? result;
				try
				{
					TaskAwaiter<InventoryResult?> taskAwaiter;
					if (num != 0)
					{
						sresult = Defines.k_SteamInventoryResultInvalid;
						bool flag = !SteamInventory.Internal.TransferItemQuantity(ref sresult, base.Id, (uint)quantity, ulong.MaxValue);
						if (flag)
						{
							result = null;
							goto IL_E4;
						}
						taskAwaiter = InventoryResult.GetAsync(sresult).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<InventoryResult?> taskAwaiter2 = taskAwaiter;
							InventoryItem.<SplitStackAsync>d__22 <SplitStackAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<InventoryResult?>, InventoryItem.<SplitStackAsync>d__22>(ref taskAwaiter, ref <SplitStackAsync>d__);
							return;
						}
					}
					else
					{
						TaskAwaiter<InventoryResult?> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<InventoryResult?>);
						num2 = -1;
					}
					inventoryResult = taskAwaiter.GetResult();
					result = inventoryResult;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_E4:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060011E7 RID: 4583 RVA: 0x000210E4 File Offset: 0x0001F2E4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000E32 RID: 3634
			public int <>1__state;

			// Token: 0x04000E33 RID: 3635
			public AsyncTaskMethodBuilder<InventoryResult?> <>t__builder;

			// Token: 0x04000E34 RID: 3636
			public int quantity;

			// Token: 0x04000E35 RID: 3637
			public InventoryItem <>4__this;

			// Token: 0x04000E36 RID: 3638
			private SteamInventoryResult_t <sresult>5__1;

			// Token: 0x04000E37 RID: 3639
			private InventoryResult? <>s__2;

			// Token: 0x04000E38 RID: 3640
			private TaskAwaiter<InventoryResult?> <>u__1;
		}

		// Token: 0x02000266 RID: 614
		[CompilerGenerated]
		private sealed class <AddAsync>d__23 : IAsyncStateMachine
		{
			// Token: 0x060011E8 RID: 4584 RVA: 0x000210E6 File Offset: 0x0001F2E6
			public <AddAsync>d__23()
			{
			}

			// Token: 0x060011E9 RID: 4585 RVA: 0x000210F0 File Offset: 0x0001F2F0
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				InventoryResult? result;
				try
				{
					TaskAwaiter<InventoryResult?> taskAwaiter;
					if (num != 0)
					{
						sresult = Defines.k_SteamInventoryResultInvalid;
						bool flag = !SteamInventory.Internal.TransferItemQuantity(ref sresult, add.Id, (uint)quantity, base.Id);
						if (flag)
						{
							result = null;
							goto IL_E8;
						}
						taskAwaiter = InventoryResult.GetAsync(sresult).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<InventoryResult?> taskAwaiter2 = taskAwaiter;
							InventoryItem.<AddAsync>d__23 <AddAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<InventoryResult?>, InventoryItem.<AddAsync>d__23>(ref taskAwaiter, ref <AddAsync>d__);
							return;
						}
					}
					else
					{
						TaskAwaiter<InventoryResult?> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<InventoryResult?>);
						num2 = -1;
					}
					inventoryResult = taskAwaiter.GetResult();
					result = inventoryResult;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_E8:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060011EA RID: 4586 RVA: 0x0002120C File Offset: 0x0001F40C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000E39 RID: 3641
			public int <>1__state;

			// Token: 0x04000E3A RID: 3642
			public AsyncTaskMethodBuilder<InventoryResult?> <>t__builder;

			// Token: 0x04000E3B RID: 3643
			public InventoryItem add;

			// Token: 0x04000E3C RID: 3644
			public int quantity;

			// Token: 0x04000E3D RID: 3645
			public InventoryItem <>4__this;

			// Token: 0x04000E3E RID: 3646
			private SteamInventoryResult_t <sresult>5__1;

			// Token: 0x04000E3F RID: 3647
			private InventoryResult? <>s__2;

			// Token: 0x04000E40 RID: 3648
			private TaskAwaiter<InventoryResult?> <>u__1;
		}
	}
}
