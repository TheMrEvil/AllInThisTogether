using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000096 RID: 150
	public class SteamInventory : SteamSharedClass<SteamInventory>
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0000BFF6 File Offset: 0x0000A1F6
		internal static ISteamInventory Internal
		{
			get
			{
				return SteamSharedClass<SteamInventory>.Interface as ISteamInventory;
			}
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0000C002 File Offset: 0x0000A202
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamInventory(server));
			SteamInventory.InstallEvents(server);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0000C01C File Offset: 0x0000A21C
		internal static void InstallEvents(bool server)
		{
			bool flag = !server;
			if (flag)
			{
				Dispatch.Install<SteamInventoryFullUpdate_t>(delegate(SteamInventoryFullUpdate_t x)
				{
					SteamInventory.InventoryUpdated(x);
				}, false);
			}
			Dispatch.Install<SteamInventoryDefinitionUpdate_t>(delegate(SteamInventoryDefinitionUpdate_t x)
			{
				SteamInventory.LoadDefinitions();
			}, server);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0000C080 File Offset: 0x0000A280
		private static void InventoryUpdated(SteamInventoryFullUpdate_t x)
		{
			InventoryResult obj = new InventoryResult(x.Handle, false);
			SteamInventory.Items = obj.GetItems(false);
			Action<InventoryResult> onInventoryUpdated = SteamInventory.OnInventoryUpdated;
			if (onInventoryUpdated != null)
			{
				onInventoryUpdated(obj);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060007A7 RID: 1959 RVA: 0x0000C0C4 File Offset: 0x0000A2C4
		// (remove) Token: 0x060007A8 RID: 1960 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		public static event Action<InventoryResult> OnInventoryUpdated
		{
			[CompilerGenerated]
			add
			{
				Action<InventoryResult> action = SteamInventory.OnInventoryUpdated;
				Action<InventoryResult> action2;
				do
				{
					action2 = action;
					Action<InventoryResult> value2 = (Action<InventoryResult>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<InventoryResult>>(ref SteamInventory.OnInventoryUpdated, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<InventoryResult> action = SteamInventory.OnInventoryUpdated;
				Action<InventoryResult> action2;
				do
				{
					action2 = action;
					Action<InventoryResult> value2 = (Action<InventoryResult>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<InventoryResult>>(ref SteamInventory.OnInventoryUpdated, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060007A9 RID: 1961 RVA: 0x0000C12C File Offset: 0x0000A32C
		// (remove) Token: 0x060007AA RID: 1962 RVA: 0x0000C160 File Offset: 0x0000A360
		public static event Action OnDefinitionsUpdated
		{
			[CompilerGenerated]
			add
			{
				Action action = SteamInventory.OnDefinitionsUpdated;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamInventory.OnDefinitionsUpdated, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SteamInventory.OnDefinitionsUpdated;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamInventory.OnDefinitionsUpdated, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0000C194 File Offset: 0x0000A394
		private static void LoadDefinitions()
		{
			SteamInventory.Definitions = SteamInventory.GetDefinitions();
			bool flag = SteamInventory.Definitions == null;
			if (!flag)
			{
				SteamInventory._defMap = new Dictionary<int, InventoryDef>();
				foreach (InventoryDef inventoryDef in SteamInventory.Definitions)
				{
					SteamInventory._defMap[inventoryDef.Id] = inventoryDef;
				}
				Action onDefinitionsUpdated = SteamInventory.OnDefinitionsUpdated;
				if (onDefinitionsUpdated != null)
				{
					onDefinitionsUpdated();
				}
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0000C204 File Offset: 0x0000A404
		public static void LoadItemDefinitions()
		{
			bool flag = SteamInventory.Definitions == null;
			if (flag)
			{
				SteamInventory.LoadDefinitions();
			}
			SteamInventory.Internal.LoadItemDefinitions();
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0000C234 File Offset: 0x0000A434
		public static async Task<bool> WaitForDefinitions(float timeoutSeconds = 30f)
		{
			bool flag = SteamInventory.Definitions != null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				SteamInventory.LoadDefinitions();
				SteamInventory.LoadItemDefinitions();
				bool flag2 = SteamInventory.Definitions != null;
				if (flag2)
				{
					result = true;
				}
				else
				{
					Stopwatch sw = Stopwatch.StartNew();
					while (SteamInventory.Definitions == null)
					{
						bool flag3 = sw.Elapsed.TotalSeconds > (double)timeoutSeconds;
						if (flag3)
						{
							return false;
						}
						await Task.Delay(10);
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0000C27C File Offset: 0x0000A47C
		public static InventoryDef FindDefinition(InventoryDefId defId)
		{
			bool flag = SteamInventory._defMap == null;
			InventoryDef result;
			if (flag)
			{
				result = null;
			}
			else
			{
				InventoryDef inventoryDef;
				bool flag2 = SteamInventory._defMap.TryGetValue(defId, out inventoryDef);
				if (flag2)
				{
					result = inventoryDef;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0000C2B9 File Offset: 0x0000A4B9
		// (set) Token: 0x060007B0 RID: 1968 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
		public static string Currency
		{
			[CompilerGenerated]
			get
			{
				return SteamInventory.<Currency>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				SteamInventory.<Currency>k__BackingField = value;
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0000C2C8 File Offset: 0x0000A4C8
		public static async Task<InventoryDef[]> GetDefinitionsWithPricesAsync()
		{
			SteamInventoryRequestPricesResult_t? steamInventoryRequestPricesResult_t = await SteamInventory.Internal.RequestPrices();
			SteamInventoryRequestPricesResult_t? priceRequest = steamInventoryRequestPricesResult_t;
			steamInventoryRequestPricesResult_t = null;
			InventoryDef[] result;
			if (priceRequest == null || priceRequest.Value.Result != Result.OK)
			{
				result = null;
			}
			else
			{
				SteamInventory.Currency = ((priceRequest != null) ? priceRequest.GetValueOrDefault().CurrencyUTF8() : null);
				uint num = SteamInventory.Internal.GetNumItemsWithPrices();
				if (num <= 0U)
				{
					result = null;
				}
				else
				{
					InventoryDefId[] defs = new InventoryDefId[num];
					ulong[] currentPrices = new ulong[num];
					ulong[] baseprices = new ulong[num];
					bool gotPrices = SteamInventory.Internal.GetItemsWithPrices(defs, currentPrices, baseprices, num);
					if (!gotPrices)
					{
						result = null;
					}
					else
					{
						result = (from x in defs
						select new InventoryDef(x)).ToArray<InventoryDef>();
					}
				}
			}
			return result;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0000C308 File Offset: 0x0000A508
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x0000C30F File Offset: 0x0000A50F
		public static InventoryItem[] Items
		{
			[CompilerGenerated]
			get
			{
				return SteamInventory.<Items>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				SteamInventory.<Items>k__BackingField = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0000C317 File Offset: 0x0000A517
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x0000C31E File Offset: 0x0000A51E
		public static InventoryDef[] Definitions
		{
			[CompilerGenerated]
			get
			{
				return SteamInventory.<Definitions>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				SteamInventory.<Definitions>k__BackingField = value;
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0000C328 File Offset: 0x0000A528
		internal static InventoryDef[] GetDefinitions()
		{
			uint num = 0U;
			bool flag = !SteamInventory.Internal.GetItemDefinitionIDs(null, ref num);
			InventoryDef[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				InventoryDefId[] array = new InventoryDefId[num];
				bool flag2 = !SteamInventory.Internal.GetItemDefinitionIDs(array, ref num);
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = (from x in array
					select new InventoryDef(x)).ToArray<InventoryDef>();
				}
			}
			return result;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0000C3A0 File Offset: 0x0000A5A0
		public static bool GetAllItems()
		{
			SteamInventoryResult_t k_SteamInventoryResultInvalid = Defines.k_SteamInventoryResultInvalid;
			return SteamInventory.Internal.GetAllItems(ref k_SteamInventoryResultInvalid);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		public static async Task<InventoryResult?> GetAllItemsAsync()
		{
			SteamInventoryResult_t sresult = Defines.k_SteamInventoryResultInvalid;
			bool flag = !SteamInventory.Internal.GetAllItems(ref sresult);
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

		// Token: 0x060007B9 RID: 1977 RVA: 0x0000C404 File Offset: 0x0000A604
		public static async Task<InventoryResult?> GenerateItemAsync(InventoryDef target, int amount)
		{
			SteamInventoryResult_t sresult = Defines.k_SteamInventoryResultInvalid;
			InventoryDefId[] defs = new InventoryDefId[]
			{
				target.Id
			};
			uint[] cnts = new uint[]
			{
				(uint)amount
			};
			bool flag = !SteamInventory.Internal.GenerateItems(ref sresult, defs, cnts, 1U);
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

		// Token: 0x060007BA RID: 1978 RVA: 0x0000C454 File Offset: 0x0000A654
		public static async Task<InventoryResult?> CraftItemAsync(InventoryItem[] list, InventoryDef target)
		{
			SteamInventoryResult_t sresult = Defines.k_SteamInventoryResultInvalid;
			InventoryDefId[] give = new InventoryDefId[]
			{
				target.Id
			};
			uint[] givec = new uint[]
			{
				1U
			};
			InventoryItemId[] sell = (from x in list
			select x.Id).ToArray<InventoryItemId>();
			uint[] sellc = (from x in list
			select 1U).ToArray<uint>();
			bool flag = !SteamInventory.Internal.ExchangeItems(ref sresult, give, givec, 1U, sell, sellc, (uint)sell.Length);
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

		// Token: 0x060007BB RID: 1979 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		public static async Task<InventoryResult?> CraftItemAsync(InventoryItem.Amount[] list, InventoryDef target)
		{
			SteamInventoryResult_t sresult = Defines.k_SteamInventoryResultInvalid;
			InventoryDefId[] give = new InventoryDefId[]
			{
				target.Id
			};
			uint[] givec = new uint[]
			{
				1U
			};
			InventoryItemId[] sell = (from x in list
			select x.Item.Id).ToArray<InventoryItemId>();
			uint[] sellc = (from x in list
			select (uint)x.Quantity).ToArray<uint>();
			bool flag = !SteamInventory.Internal.ExchangeItems(ref sresult, give, givec, 1U, sell, sellc, (uint)sell.Length);
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

		// Token: 0x060007BC RID: 1980 RVA: 0x0000C4F4 File Offset: 0x0000A6F4
		public static async Task<InventoryResult?> DeserializeAsync(byte[] data, int dataLength = -1)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentException("data should not be null");
			}
			bool flag2 = dataLength == -1;
			if (flag2)
			{
				dataLength = data.Length;
			}
			IntPtr ptr = Marshal.AllocHGlobal(dataLength);
			InventoryResult? result;
			try
			{
				Marshal.Copy(data, 0, ptr, dataLength);
				SteamInventoryResult_t sresult = Defines.k_SteamInventoryResultInvalid;
				bool flag3 = !SteamInventory.Internal.DeserializeResult(ref sresult, ptr, (uint)dataLength, false);
				if (flag3)
				{
					result = null;
				}
				else
				{
					InventoryResult? inventoryResult = await InventoryResult.GetAsync(sresult.Value);
					result = inventoryResult;
				}
			}
			finally
			{
				Marshal.FreeHGlobal(ptr);
			}
			return result;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0000C544 File Offset: 0x0000A744
		public static async Task<InventoryResult?> GrantPromoItemsAsync()
		{
			SteamInventoryResult_t sresult = Defines.k_SteamInventoryResultInvalid;
			bool flag = !SteamInventory.Internal.GrantPromoItems(ref sresult);
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

		// Token: 0x060007BE RID: 1982 RVA: 0x0000C584 File Offset: 0x0000A784
		public static async Task<InventoryResult?> TriggerItemDropAsync(InventoryDefId id)
		{
			SteamInventoryResult_t sresult = Defines.k_SteamInventoryResultInvalid;
			bool flag = !SteamInventory.Internal.TriggerItemDrop(ref sresult, id);
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

		// Token: 0x060007BF RID: 1983 RVA: 0x0000C5CC File Offset: 0x0000A7CC
		public static async Task<InventoryResult?> AddPromoItemAsync(InventoryDefId id)
		{
			SteamInventoryResult_t sresult = Defines.k_SteamInventoryResultInvalid;
			bool flag = !SteamInventory.Internal.AddPromoItem(ref sresult, id);
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

		// Token: 0x060007C0 RID: 1984 RVA: 0x0000C614 File Offset: 0x0000A814
		public static async Task<InventoryPurchaseResult?> StartPurchaseAsync(InventoryDef[] items)
		{
			InventoryDefId[] item_i = (from x in items
			select x._id).ToArray<InventoryDefId>();
			uint[] item_q = (from x in items
			select 1U).ToArray<uint>();
			SteamInventoryStartPurchaseResult_t? steamInventoryStartPurchaseResult_t = await SteamInventory.Internal.StartPurchase(item_i, item_q, (uint)item_i.Length);
			SteamInventoryStartPurchaseResult_t? r = steamInventoryStartPurchaseResult_t;
			steamInventoryStartPurchaseResult_t = null;
			InventoryPurchaseResult? result;
			if (r == null)
			{
				result = null;
			}
			else
			{
				result = new InventoryPurchaseResult?(new InventoryPurchaseResult
				{
					Result = r.Value.Result,
					OrderID = r.Value.OrderID,
					TransID = r.Value.TransID
				});
			}
			return result;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0000C65B File Offset: 0x0000A85B
		public SteamInventory()
		{
		}

		// Token: 0x040006F0 RID: 1776
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<InventoryResult> OnInventoryUpdated;

		// Token: 0x040006F1 RID: 1777
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnDefinitionsUpdated;

		// Token: 0x040006F2 RID: 1778
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static string <Currency>k__BackingField;

		// Token: 0x040006F3 RID: 1779
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static InventoryItem[] <Items>k__BackingField;

		// Token: 0x040006F4 RID: 1780
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static InventoryDef[] <Definitions>k__BackingField;

		// Token: 0x040006F5 RID: 1781
		private static Dictionary<int, InventoryDef> _defMap;

		// Token: 0x02000223 RID: 547
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060010DA RID: 4314 RVA: 0x0001C933 File Offset: 0x0001AB33
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060010DB RID: 4315 RVA: 0x0001C93F File Offset: 0x0001AB3F
			public <>c()
			{
			}

			// Token: 0x060010DC RID: 4316 RVA: 0x0001C948 File Offset: 0x0001AB48
			internal void <InstallEvents>b__3_0(SteamInventoryFullUpdate_t x)
			{
				SteamInventory.InventoryUpdated(x);
			}

			// Token: 0x060010DD RID: 4317 RVA: 0x0001C951 File Offset: 0x0001AB51
			internal void <InstallEvents>b__3_1(SteamInventoryDefinitionUpdate_t x)
			{
				SteamInventory.LoadDefinitions();
			}

			// Token: 0x060010DE RID: 4318 RVA: 0x0001C959 File Offset: 0x0001AB59
			internal InventoryDef <GetDefinitionsWithPricesAsync>b__19_0(InventoryDefId x)
			{
				return new InventoryDef(x);
			}

			// Token: 0x060010DF RID: 4319 RVA: 0x0001C961 File Offset: 0x0001AB61
			internal InventoryDef <GetDefinitions>b__29_0(InventoryDefId x)
			{
				return new InventoryDef(x);
			}

			// Token: 0x060010E0 RID: 4320 RVA: 0x0001C969 File Offset: 0x0001AB69
			internal InventoryItemId <CraftItemAsync>b__33_0(InventoryItem x)
			{
				return x.Id;
			}

			// Token: 0x060010E1 RID: 4321 RVA: 0x0001C972 File Offset: 0x0001AB72
			internal uint <CraftItemAsync>b__33_1(InventoryItem x)
			{
				return 1U;
			}

			// Token: 0x060010E2 RID: 4322 RVA: 0x0001C975 File Offset: 0x0001AB75
			internal InventoryItemId <CraftItemAsync>b__34_0(InventoryItem.Amount x)
			{
				return x.Item.Id;
			}

			// Token: 0x060010E3 RID: 4323 RVA: 0x0001C983 File Offset: 0x0001AB83
			internal uint <CraftItemAsync>b__34_1(InventoryItem.Amount x)
			{
				return (uint)x.Quantity;
			}

			// Token: 0x060010E4 RID: 4324 RVA: 0x0001C98B File Offset: 0x0001AB8B
			internal InventoryDefId <StartPurchaseAsync>b__39_0(InventoryDef x)
			{
				return x._id;
			}

			// Token: 0x060010E5 RID: 4325 RVA: 0x0001C993 File Offset: 0x0001AB93
			internal uint <StartPurchaseAsync>b__39_1(InventoryDef x)
			{
				return 1U;
			}

			// Token: 0x04000CAE RID: 3246
			public static readonly SteamInventory.<>c <>9 = new SteamInventory.<>c();

			// Token: 0x04000CAF RID: 3247
			public static Action<SteamInventoryFullUpdate_t> <>9__3_0;

			// Token: 0x04000CB0 RID: 3248
			public static Action<SteamInventoryDefinitionUpdate_t> <>9__3_1;

			// Token: 0x04000CB1 RID: 3249
			public static Func<InventoryDefId, InventoryDef> <>9__19_0;

			// Token: 0x04000CB2 RID: 3250
			public static Func<InventoryDefId, InventoryDef> <>9__29_0;

			// Token: 0x04000CB3 RID: 3251
			public static Func<InventoryItem, InventoryItemId> <>9__33_0;

			// Token: 0x04000CB4 RID: 3252
			public static Func<InventoryItem, uint> <>9__33_1;

			// Token: 0x04000CB5 RID: 3253
			public static Func<InventoryItem.Amount, InventoryItemId> <>9__34_0;

			// Token: 0x04000CB6 RID: 3254
			public static Func<InventoryItem.Amount, uint> <>9__34_1;

			// Token: 0x04000CB7 RID: 3255
			public static Func<InventoryDef, InventoryDefId> <>9__39_0;

			// Token: 0x04000CB8 RID: 3256
			public static Func<InventoryDef, uint> <>9__39_1;
		}

		// Token: 0x02000224 RID: 548
		[CompilerGenerated]
		private sealed class <WaitForDefinitions>d__13 : IAsyncStateMachine
		{
			// Token: 0x060010E6 RID: 4326 RVA: 0x0001C996 File Offset: 0x0001AB96
			public <WaitForDefinitions>d__13()
			{
			}

			// Token: 0x060010E7 RID: 4327 RVA: 0x0001C9A0 File Offset: 0x0001ABA0
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				bool result;
				try
				{
					TaskAwaiter taskAwaiter;
					if (num != 0)
					{
						bool flag = SteamInventory.Definitions != null;
						if (flag)
						{
							result = true;
							goto IL_111;
						}
						SteamInventory.LoadDefinitions();
						SteamInventory.LoadItemDefinitions();
						bool flag2 = SteamInventory.Definitions != null;
						if (flag2)
						{
							result = true;
							goto IL_111;
						}
						sw = Stopwatch.StartNew();
						goto IL_E2;
					}
					else
					{
						TaskAwaiter taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num2 = -1;
					}
					IL_D9:
					taskAwaiter.GetResult();
					IL_E2:
					if (SteamInventory.Definitions != null)
					{
						result = true;
					}
					else
					{
						bool flag3 = sw.Elapsed.TotalSeconds > (double)timeoutSeconds;
						if (flag3)
						{
							result = false;
						}
						else
						{
							taskAwaiter = Task.Delay(10).GetAwaiter();
							if (!taskAwaiter.IsCompleted)
							{
								num2 = 0;
								TaskAwaiter taskAwaiter2 = taskAwaiter;
								SteamInventory.<WaitForDefinitions>d__13 <WaitForDefinitions>d__ = this;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, SteamInventory.<WaitForDefinitions>d__13>(ref taskAwaiter, ref <WaitForDefinitions>d__);
								return;
							}
							goto IL_D9;
						}
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_111:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060010E8 RID: 4328 RVA: 0x0001CAE4 File Offset: 0x0001ACE4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000CB9 RID: 3257
			public int <>1__state;

			// Token: 0x04000CBA RID: 3258
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000CBB RID: 3259
			public float timeoutSeconds;

			// Token: 0x04000CBC RID: 3260
			private Stopwatch <sw>5__1;

			// Token: 0x04000CBD RID: 3261
			private TaskAwaiter <>u__1;
		}

		// Token: 0x02000225 RID: 549
		[CompilerGenerated]
		private sealed class <GetDefinitionsWithPricesAsync>d__19 : IAsyncStateMachine
		{
			// Token: 0x060010E9 RID: 4329 RVA: 0x0001CAE6 File Offset: 0x0001ACE6
			public <GetDefinitionsWithPricesAsync>d__19()
			{
			}

			// Token: 0x060010EA RID: 4330 RVA: 0x0001CAF0 File Offset: 0x0001ACF0
			void IAsyncStateMachine.MoveNext()
			{
				int num3;
				int num2 = num3;
				InventoryDef[] result;
				try
				{
					CallResult<SteamInventoryRequestPricesResult_t> callResult;
					if (num2 != 0)
					{
						callResult = SteamInventory.Internal.RequestPrices().GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num3 = 0;
							CallResult<SteamInventoryRequestPricesResult_t> callResult2 = callResult;
							SteamInventory.<GetDefinitionsWithPricesAsync>d__19 <GetDefinitionsWithPricesAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<SteamInventoryRequestPricesResult_t>, SteamInventory.<GetDefinitionsWithPricesAsync>d__19>(ref callResult, ref <GetDefinitionsWithPricesAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<SteamInventoryRequestPricesResult_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<SteamInventoryRequestPricesResult_t>);
						num3 = -1;
					}
					steamInventoryRequestPricesResult_t = callResult.GetResult();
					priceRequest = steamInventoryRequestPricesResult_t;
					steamInventoryRequestPricesResult_t = null;
					bool flag = priceRequest == null || priceRequest.Value.Result != Result.OK;
					if (flag)
					{
						result = null;
					}
					else
					{
						SteamInventory.Currency = ((priceRequest != null) ? priceRequest.GetValueOrDefault().CurrencyUTF8() : null);
						num = SteamInventory.Internal.GetNumItemsWithPrices();
						bool flag2 = num <= 0U;
						if (flag2)
						{
							result = null;
						}
						else
						{
							defs = new InventoryDefId[num];
							currentPrices = new ulong[num];
							baseprices = new ulong[num];
							gotPrices = SteamInventory.Internal.GetItemsWithPrices(defs, currentPrices, baseprices, num);
							bool flag3 = !gotPrices;
							if (flag3)
							{
								result = null;
							}
							else
							{
								result = defs.Select(new Func<InventoryDefId, InventoryDef>(SteamInventory.<>c.<>9.<GetDefinitionsWithPricesAsync>b__19_0)).ToArray<InventoryDef>();
							}
						}
					}
				}
				catch (Exception exception)
				{
					num3 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num3 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060010EB RID: 4331 RVA: 0x0001CD00 File Offset: 0x0001AF00
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000CBE RID: 3262
			public int <>1__state;

			// Token: 0x04000CBF RID: 3263
			public AsyncTaskMethodBuilder<InventoryDef[]> <>t__builder;

			// Token: 0x04000CC0 RID: 3264
			private SteamInventoryRequestPricesResult_t? <priceRequest>5__1;

			// Token: 0x04000CC1 RID: 3265
			private uint <num>5__2;

			// Token: 0x04000CC2 RID: 3266
			private InventoryDefId[] <defs>5__3;

			// Token: 0x04000CC3 RID: 3267
			private ulong[] <currentPrices>5__4;

			// Token: 0x04000CC4 RID: 3268
			private ulong[] <baseprices>5__5;

			// Token: 0x04000CC5 RID: 3269
			private bool <gotPrices>5__6;

			// Token: 0x04000CC6 RID: 3270
			private SteamInventoryRequestPricesResult_t? <>s__7;

			// Token: 0x04000CC7 RID: 3271
			private CallResult<SteamInventoryRequestPricesResult_t> <>u__1;
		}

		// Token: 0x02000226 RID: 550
		[CompilerGenerated]
		private sealed class <GetAllItemsAsync>d__31 : IAsyncStateMachine
		{
			// Token: 0x060010EC RID: 4332 RVA: 0x0001CD02 File Offset: 0x0001AF02
			public <GetAllItemsAsync>d__31()
			{
			}

			// Token: 0x060010ED RID: 4333 RVA: 0x0001CD0C File Offset: 0x0001AF0C
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
						bool flag = !SteamInventory.Internal.GetAllItems(ref sresult);
						if (flag)
						{
							result = null;
							goto IL_C9;
						}
						taskAwaiter = InventoryResult.GetAsync(sresult).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<InventoryResult?> taskAwaiter2 = taskAwaiter;
							SteamInventory.<GetAllItemsAsync>d__31 <GetAllItemsAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<InventoryResult?>, SteamInventory.<GetAllItemsAsync>d__31>(ref taskAwaiter, ref <GetAllItemsAsync>d__);
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
				IL_C9:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060010EE RID: 4334 RVA: 0x0001CE08 File Offset: 0x0001B008
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000CC8 RID: 3272
			public int <>1__state;

			// Token: 0x04000CC9 RID: 3273
			public AsyncTaskMethodBuilder<InventoryResult?> <>t__builder;

			// Token: 0x04000CCA RID: 3274
			private SteamInventoryResult_t <sresult>5__1;

			// Token: 0x04000CCB RID: 3275
			private InventoryResult? <>s__2;

			// Token: 0x04000CCC RID: 3276
			private TaskAwaiter<InventoryResult?> <>u__1;
		}

		// Token: 0x02000227 RID: 551
		[CompilerGenerated]
		private sealed class <GenerateItemAsync>d__32 : IAsyncStateMachine
		{
			// Token: 0x060010EF RID: 4335 RVA: 0x0001CE0A File Offset: 0x0001B00A
			public <GenerateItemAsync>d__32()
			{
			}

			// Token: 0x060010F0 RID: 4336 RVA: 0x0001CE14 File Offset: 0x0001B014
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
						defs = new InventoryDefId[]
						{
							target.Id
						};
						cnts = new uint[]
						{
							(uint)amount
						};
						bool flag = !SteamInventory.Internal.GenerateItems(ref sresult, defs, cnts, 1U);
						if (flag)
						{
							result = null;
							goto IL_111;
						}
						taskAwaiter = InventoryResult.GetAsync(sresult).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<InventoryResult?> taskAwaiter2 = taskAwaiter;
							SteamInventory.<GenerateItemAsync>d__32 <GenerateItemAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<InventoryResult?>, SteamInventory.<GenerateItemAsync>d__32>(ref taskAwaiter, ref <GenerateItemAsync>d__);
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
				IL_111:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060010F1 RID: 4337 RVA: 0x0001CF58 File Offset: 0x0001B158
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000CCD RID: 3277
			public int <>1__state;

			// Token: 0x04000CCE RID: 3278
			public AsyncTaskMethodBuilder<InventoryResult?> <>t__builder;

			// Token: 0x04000CCF RID: 3279
			public InventoryDef target;

			// Token: 0x04000CD0 RID: 3280
			public int amount;

			// Token: 0x04000CD1 RID: 3281
			private SteamInventoryResult_t <sresult>5__1;

			// Token: 0x04000CD2 RID: 3282
			private InventoryDefId[] <defs>5__2;

			// Token: 0x04000CD3 RID: 3283
			private uint[] <cnts>5__3;

			// Token: 0x04000CD4 RID: 3284
			private InventoryResult? <>s__4;

			// Token: 0x04000CD5 RID: 3285
			private TaskAwaiter<InventoryResult?> <>u__1;
		}

		// Token: 0x02000228 RID: 552
		[CompilerGenerated]
		private sealed class <CraftItemAsync>d__33 : IAsyncStateMachine
		{
			// Token: 0x060010F2 RID: 4338 RVA: 0x0001CF5A File Offset: 0x0001B15A
			public <CraftItemAsync>d__33()
			{
			}

			// Token: 0x060010F3 RID: 4339 RVA: 0x0001CF64 File Offset: 0x0001B164
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
						give = new InventoryDefId[]
						{
							target.Id
						};
						givec = new uint[]
						{
							1U
						};
						sell = list.Select(new Func<InventoryItem, InventoryItemId>(SteamInventory.<>c.<>9.<CraftItemAsync>b__33_0)).ToArray<InventoryItemId>();
						sellc = list.Select(new Func<InventoryItem, uint>(SteamInventory.<>c.<>9.<CraftItemAsync>b__33_1)).ToArray<uint>();
						bool flag = !SteamInventory.Internal.ExchangeItems(ref sresult, give, givec, 1U, sell, sellc, (uint)sell.Length);
						if (flag)
						{
							result = null;
							goto IL_18A;
						}
						taskAwaiter = InventoryResult.GetAsync(sresult).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<InventoryResult?> taskAwaiter2 = taskAwaiter;
							SteamInventory.<CraftItemAsync>d__33 <CraftItemAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<InventoryResult?>, SteamInventory.<CraftItemAsync>d__33>(ref taskAwaiter, ref <CraftItemAsync>d__);
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
				IL_18A:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060010F4 RID: 4340 RVA: 0x0001D12C File Offset: 0x0001B32C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000CD6 RID: 3286
			public int <>1__state;

			// Token: 0x04000CD7 RID: 3287
			public AsyncTaskMethodBuilder<InventoryResult?> <>t__builder;

			// Token: 0x04000CD8 RID: 3288
			public InventoryItem[] list;

			// Token: 0x04000CD9 RID: 3289
			public InventoryDef target;

			// Token: 0x04000CDA RID: 3290
			private SteamInventoryResult_t <sresult>5__1;

			// Token: 0x04000CDB RID: 3291
			private InventoryDefId[] <give>5__2;

			// Token: 0x04000CDC RID: 3292
			private uint[] <givec>5__3;

			// Token: 0x04000CDD RID: 3293
			private InventoryItemId[] <sell>5__4;

			// Token: 0x04000CDE RID: 3294
			private uint[] <sellc>5__5;

			// Token: 0x04000CDF RID: 3295
			private InventoryResult? <>s__6;

			// Token: 0x04000CE0 RID: 3296
			private TaskAwaiter<InventoryResult?> <>u__1;
		}

		// Token: 0x02000229 RID: 553
		[CompilerGenerated]
		private sealed class <CraftItemAsync>d__34 : IAsyncStateMachine
		{
			// Token: 0x060010F5 RID: 4341 RVA: 0x0001D12E File Offset: 0x0001B32E
			public <CraftItemAsync>d__34()
			{
			}

			// Token: 0x060010F6 RID: 4342 RVA: 0x0001D138 File Offset: 0x0001B338
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
						give = new InventoryDefId[]
						{
							target.Id
						};
						givec = new uint[]
						{
							1U
						};
						sell = list.Select(new Func<InventoryItem.Amount, InventoryItemId>(SteamInventory.<>c.<>9.<CraftItemAsync>b__34_0)).ToArray<InventoryItemId>();
						sellc = list.Select(new Func<InventoryItem.Amount, uint>(SteamInventory.<>c.<>9.<CraftItemAsync>b__34_1)).ToArray<uint>();
						bool flag = !SteamInventory.Internal.ExchangeItems(ref sresult, give, givec, 1U, sell, sellc, (uint)sell.Length);
						if (flag)
						{
							result = null;
							goto IL_18A;
						}
						taskAwaiter = InventoryResult.GetAsync(sresult).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<InventoryResult?> taskAwaiter2 = taskAwaiter;
							SteamInventory.<CraftItemAsync>d__34 <CraftItemAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<InventoryResult?>, SteamInventory.<CraftItemAsync>d__34>(ref taskAwaiter, ref <CraftItemAsync>d__);
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
				IL_18A:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060010F7 RID: 4343 RVA: 0x0001D300 File Offset: 0x0001B500
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000CE1 RID: 3297
			public int <>1__state;

			// Token: 0x04000CE2 RID: 3298
			public AsyncTaskMethodBuilder<InventoryResult?> <>t__builder;

			// Token: 0x04000CE3 RID: 3299
			public InventoryItem.Amount[] list;

			// Token: 0x04000CE4 RID: 3300
			public InventoryDef target;

			// Token: 0x04000CE5 RID: 3301
			private SteamInventoryResult_t <sresult>5__1;

			// Token: 0x04000CE6 RID: 3302
			private InventoryDefId[] <give>5__2;

			// Token: 0x04000CE7 RID: 3303
			private uint[] <givec>5__3;

			// Token: 0x04000CE8 RID: 3304
			private InventoryItemId[] <sell>5__4;

			// Token: 0x04000CE9 RID: 3305
			private uint[] <sellc>5__5;

			// Token: 0x04000CEA RID: 3306
			private InventoryResult? <>s__6;

			// Token: 0x04000CEB RID: 3307
			private TaskAwaiter<InventoryResult?> <>u__1;
		}

		// Token: 0x0200022A RID: 554
		[CompilerGenerated]
		private sealed class <DeserializeAsync>d__35 : IAsyncStateMachine
		{
			// Token: 0x060010F8 RID: 4344 RVA: 0x0001D302 File Offset: 0x0001B502
			public <DeserializeAsync>d__35()
			{
			}

			// Token: 0x060010F9 RID: 4345 RVA: 0x0001D30C File Offset: 0x0001B50C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				InventoryResult? result;
				try
				{
					if (num != 0)
					{
						bool flag = data == null;
						if (flag)
						{
							throw new ArgumentException("data should not be null");
						}
						bool flag2 = dataLength == -1;
						if (flag2)
						{
							dataLength = data.Length;
						}
						ptr = Marshal.AllocHGlobal(dataLength);
					}
					try
					{
						TaskAwaiter<InventoryResult?> taskAwaiter;
						if (num != 0)
						{
							Marshal.Copy(data, 0, ptr, dataLength);
							sresult = Defines.k_SteamInventoryResultInvalid;
							bool flag3 = !SteamInventory.Internal.DeserializeResult(ref sresult, ptr, (uint)dataLength, false);
							if (flag3)
							{
								result = null;
								goto IL_161;
							}
							taskAwaiter = InventoryResult.GetAsync(sresult.Value).GetAwaiter();
							if (!taskAwaiter.IsCompleted)
							{
								num = (num2 = 0);
								TaskAwaiter<InventoryResult?> taskAwaiter2 = taskAwaiter;
								SteamInventory.<DeserializeAsync>d__35 <DeserializeAsync>d__ = this;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<InventoryResult?>, SteamInventory.<DeserializeAsync>d__35>(ref taskAwaiter, ref <DeserializeAsync>d__);
								return;
							}
						}
						else
						{
							TaskAwaiter<InventoryResult?> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<InventoryResult?>);
							num = (num2 = -1);
						}
						inventoryResult = taskAwaiter.GetResult();
						result = inventoryResult;
					}
					finally
					{
						if (num < 0)
						{
							Marshal.FreeHGlobal(ptr);
						}
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_161:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060010FA RID: 4346 RVA: 0x0001D4C4 File Offset: 0x0001B6C4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000CEC RID: 3308
			public int <>1__state;

			// Token: 0x04000CED RID: 3309
			public AsyncTaskMethodBuilder<InventoryResult?> <>t__builder;

			// Token: 0x04000CEE RID: 3310
			public byte[] data;

			// Token: 0x04000CEF RID: 3311
			public int dataLength;

			// Token: 0x04000CF0 RID: 3312
			private IntPtr <ptr>5__1;

			// Token: 0x04000CF1 RID: 3313
			private SteamInventoryResult_t <sresult>5__2;

			// Token: 0x04000CF2 RID: 3314
			private InventoryResult? <>s__3;

			// Token: 0x04000CF3 RID: 3315
			private TaskAwaiter<InventoryResult?> <>u__1;
		}

		// Token: 0x0200022B RID: 555
		[CompilerGenerated]
		private sealed class <GrantPromoItemsAsync>d__36 : IAsyncStateMachine
		{
			// Token: 0x060010FB RID: 4347 RVA: 0x0001D4C6 File Offset: 0x0001B6C6
			public <GrantPromoItemsAsync>d__36()
			{
			}

			// Token: 0x060010FC RID: 4348 RVA: 0x0001D4D0 File Offset: 0x0001B6D0
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
						bool flag = !SteamInventory.Internal.GrantPromoItems(ref sresult);
						if (flag)
						{
							result = null;
							goto IL_C9;
						}
						taskAwaiter = InventoryResult.GetAsync(sresult).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<InventoryResult?> taskAwaiter2 = taskAwaiter;
							SteamInventory.<GrantPromoItemsAsync>d__36 <GrantPromoItemsAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<InventoryResult?>, SteamInventory.<GrantPromoItemsAsync>d__36>(ref taskAwaiter, ref <GrantPromoItemsAsync>d__);
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
				IL_C9:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060010FD RID: 4349 RVA: 0x0001D5CC File Offset: 0x0001B7CC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000CF4 RID: 3316
			public int <>1__state;

			// Token: 0x04000CF5 RID: 3317
			public AsyncTaskMethodBuilder<InventoryResult?> <>t__builder;

			// Token: 0x04000CF6 RID: 3318
			private SteamInventoryResult_t <sresult>5__1;

			// Token: 0x04000CF7 RID: 3319
			private InventoryResult? <>s__2;

			// Token: 0x04000CF8 RID: 3320
			private TaskAwaiter<InventoryResult?> <>u__1;
		}

		// Token: 0x0200022C RID: 556
		[CompilerGenerated]
		private sealed class <TriggerItemDropAsync>d__37 : IAsyncStateMachine
		{
			// Token: 0x060010FE RID: 4350 RVA: 0x0001D5CE File Offset: 0x0001B7CE
			public <TriggerItemDropAsync>d__37()
			{
			}

			// Token: 0x060010FF RID: 4351 RVA: 0x0001D5D8 File Offset: 0x0001B7D8
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
						bool flag = !SteamInventory.Internal.TriggerItemDrop(ref sresult, id);
						if (flag)
						{
							result = null;
							goto IL_CF;
						}
						taskAwaiter = InventoryResult.GetAsync(sresult).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<InventoryResult?> taskAwaiter2 = taskAwaiter;
							SteamInventory.<TriggerItemDropAsync>d__37 <TriggerItemDropAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<InventoryResult?>, SteamInventory.<TriggerItemDropAsync>d__37>(ref taskAwaiter, ref <TriggerItemDropAsync>d__);
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
				IL_CF:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001100 RID: 4352 RVA: 0x0001D6DC File Offset: 0x0001B8DC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000CF9 RID: 3321
			public int <>1__state;

			// Token: 0x04000CFA RID: 3322
			public AsyncTaskMethodBuilder<InventoryResult?> <>t__builder;

			// Token: 0x04000CFB RID: 3323
			public InventoryDefId id;

			// Token: 0x04000CFC RID: 3324
			private SteamInventoryResult_t <sresult>5__1;

			// Token: 0x04000CFD RID: 3325
			private InventoryResult? <>s__2;

			// Token: 0x04000CFE RID: 3326
			private TaskAwaiter<InventoryResult?> <>u__1;
		}

		// Token: 0x0200022D RID: 557
		[CompilerGenerated]
		private sealed class <AddPromoItemAsync>d__38 : IAsyncStateMachine
		{
			// Token: 0x06001101 RID: 4353 RVA: 0x0001D6DE File Offset: 0x0001B8DE
			public <AddPromoItemAsync>d__38()
			{
			}

			// Token: 0x06001102 RID: 4354 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
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
						bool flag = !SteamInventory.Internal.AddPromoItem(ref sresult, id);
						if (flag)
						{
							result = null;
							goto IL_CF;
						}
						taskAwaiter = InventoryResult.GetAsync(sresult).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<InventoryResult?> taskAwaiter2 = taskAwaiter;
							SteamInventory.<AddPromoItemAsync>d__38 <AddPromoItemAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<InventoryResult?>, SteamInventory.<AddPromoItemAsync>d__38>(ref taskAwaiter, ref <AddPromoItemAsync>d__);
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
				IL_CF:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001103 RID: 4355 RVA: 0x0001D7EC File Offset: 0x0001B9EC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000CFF RID: 3327
			public int <>1__state;

			// Token: 0x04000D00 RID: 3328
			public AsyncTaskMethodBuilder<InventoryResult?> <>t__builder;

			// Token: 0x04000D01 RID: 3329
			public InventoryDefId id;

			// Token: 0x04000D02 RID: 3330
			private SteamInventoryResult_t <sresult>5__1;

			// Token: 0x04000D03 RID: 3331
			private InventoryResult? <>s__2;

			// Token: 0x04000D04 RID: 3332
			private TaskAwaiter<InventoryResult?> <>u__1;
		}

		// Token: 0x0200022E RID: 558
		[CompilerGenerated]
		private sealed class <StartPurchaseAsync>d__39 : IAsyncStateMachine
		{
			// Token: 0x06001104 RID: 4356 RVA: 0x0001D7EE File Offset: 0x0001B9EE
			public <StartPurchaseAsync>d__39()
			{
			}

			// Token: 0x06001105 RID: 4357 RVA: 0x0001D7F8 File Offset: 0x0001B9F8
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				InventoryPurchaseResult? result;
				try
				{
					CallResult<SteamInventoryStartPurchaseResult_t> callResult;
					if (num != 0)
					{
						item_i = items.Select(new Func<InventoryDef, InventoryDefId>(SteamInventory.<>c.<>9.<StartPurchaseAsync>b__39_0)).ToArray<InventoryDefId>();
						item_q = items.Select(new Func<InventoryDef, uint>(SteamInventory.<>c.<>9.<StartPurchaseAsync>b__39_1)).ToArray<uint>();
						callResult = SteamInventory.Internal.StartPurchase(item_i, item_q, (uint)item_i.Length).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<SteamInventoryStartPurchaseResult_t> callResult2 = callResult;
							SteamInventory.<StartPurchaseAsync>d__39 <StartPurchaseAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<SteamInventoryStartPurchaseResult_t>, SteamInventory.<StartPurchaseAsync>d__39>(ref callResult, ref <StartPurchaseAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<SteamInventoryStartPurchaseResult_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<SteamInventoryStartPurchaseResult_t>);
						num2 = -1;
					}
					steamInventoryStartPurchaseResult_t = callResult.GetResult();
					r = steamInventoryStartPurchaseResult_t;
					steamInventoryStartPurchaseResult_t = null;
					bool flag = r == null;
					if (flag)
					{
						result = null;
					}
					else
					{
						result = new InventoryPurchaseResult?(new InventoryPurchaseResult
						{
							Result = r.Value.Result,
							OrderID = r.Value.OrderID,
							TransID = r.Value.TransID
						});
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

			// Token: 0x06001106 RID: 4358 RVA: 0x0001D9DC File Offset: 0x0001BBDC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000D05 RID: 3333
			public int <>1__state;

			// Token: 0x04000D06 RID: 3334
			public AsyncTaskMethodBuilder<InventoryPurchaseResult?> <>t__builder;

			// Token: 0x04000D07 RID: 3335
			public InventoryDef[] items;

			// Token: 0x04000D08 RID: 3336
			private InventoryDefId[] <item_i>5__1;

			// Token: 0x04000D09 RID: 3337
			private uint[] <item_q>5__2;

			// Token: 0x04000D0A RID: 3338
			private SteamInventoryStartPurchaseResult_t? <r>5__3;

			// Token: 0x04000D0B RID: 3339
			private SteamInventoryStartPurchaseResult_t? <>s__4;

			// Token: 0x04000D0C RID: 3340
			private CallResult<SteamInventoryStartPurchaseResult_t> <>u__1;
		}
	}
}
