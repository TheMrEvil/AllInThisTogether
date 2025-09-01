using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x020000AF RID: 175
	public class InventoryDef : IEquatable<InventoryDef>
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x00011257 File Offset: 0x0000F457
		public InventoryDef(InventoryDefId defId)
		{
			this._id = defId;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x00011268 File Offset: 0x0000F468
		public int Id
		{
			get
			{
				return this._id.Value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00011275 File Offset: 0x0000F475
		public string Name
		{
			get
			{
				return this.GetProperty("name");
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00011282 File Offset: 0x0000F482
		public string Description
		{
			get
			{
				return this.GetProperty("description");
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0001128F File Offset: 0x0000F48F
		public string IconUrl
		{
			get
			{
				return this.GetProperty("icon_url");
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0001129C File Offset: 0x0000F49C
		public string IconUrlLarge
		{
			get
			{
				return this.GetProperty("icon_url_large");
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x000112A9 File Offset: 0x0000F4A9
		public string PriceCategory
		{
			get
			{
				return this.GetProperty("price_category");
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x000112B6 File Offset: 0x0000F4B6
		public string Type
		{
			get
			{
				return this.GetProperty("type");
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x000112C3 File Offset: 0x0000F4C3
		public bool IsGenerator
		{
			get
			{
				return this.Type == "generator";
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x000112D5 File Offset: 0x0000F4D5
		public string ExchangeSchema
		{
			get
			{
				return this.GetProperty("exchange");
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x000112E4 File Offset: 0x0000F4E4
		public InventoryRecipe[] GetRecipes()
		{
			bool flag = string.IsNullOrEmpty(this.ExchangeSchema);
			InventoryRecipe[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string[] source = this.ExchangeSchema.Split(new char[]
				{
					';'
				}, StringSplitOptions.RemoveEmptyEntries);
				result = (from x in source
				select InventoryRecipe.FromString(x, this)).ToArray<InventoryRecipe>();
			}
			return result;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x00011338 File Offset: 0x0000F538
		public bool Marketable
		{
			get
			{
				return this.GetBoolProperty("marketable");
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00011345 File Offset: 0x0000F545
		public bool Tradable
		{
			get
			{
				return this.GetBoolProperty("tradable");
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x00011352 File Offset: 0x0000F552
		public DateTime Created
		{
			get
			{
				return this.GetProperty<DateTime>("timestamp");
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0001135F File Offset: 0x0000F55F
		public DateTime Modified
		{
			get
			{
				return this.GetProperty<DateTime>("modified");
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001136C File Offset: 0x0000F56C
		public string GetProperty(string name)
		{
			string text;
			bool flag = this._properties != null && this._properties.TryGetValue(name, out text);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				uint num = 32768U;
				string text2;
				bool flag2 = !SteamInventory.Internal.GetItemDefinitionProperty(this.Id, name, out text2, ref num);
				if (flag2)
				{
					result = null;
				}
				else
				{
					bool flag3 = name == null;
					if (flag3)
					{
						result = text2;
					}
					else
					{
						bool flag4 = this._properties == null;
						if (flag4)
						{
							this._properties = new Dictionary<string, string>();
						}
						this._properties[name] = text2;
						result = text2;
					}
				}
			}
			return result;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00011408 File Offset: 0x0000F608
		public bool GetBoolProperty(string name)
		{
			string property = this.GetProperty(name);
			bool flag = property.Length == 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = property[0] == '0' || property[0] == 'F' || property[0] == 'f';
				result = !flag2;
			}
			return result;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00011460 File Offset: 0x0000F660
		public T GetProperty<T>(string name)
		{
			string property = this.GetProperty(name);
			bool flag = string.IsNullOrEmpty(property);
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				try
				{
					result = (T)((object)Convert.ChangeType(property, typeof(T)));
				}
				catch (Exception)
				{
					result = default(T);
				}
			}
			return result;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x000114C8 File Offset: 0x0000F6C8
		public IEnumerable<KeyValuePair<string, string>> Properties
		{
			get
			{
				string list = this.GetProperty(null);
				string[] keys = list.Split(new char[]
				{
					','
				});
				foreach (string key in keys)
				{
					yield return new KeyValuePair<string, string>(key, this.GetProperty(key));
					key = null;
				}
				string[] array = null;
				yield break;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x000114E8 File Offset: 0x0000F6E8
		public int LocalPrice
		{
			get
			{
				ulong num = 0UL;
				ulong num2 = 0UL;
				bool flag = !SteamInventory.Internal.GetItemPrice(this.Id, ref num, ref num2);
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

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00011526 File Offset: 0x0000F726
		public string LocalPriceFormatted
		{
			get
			{
				return Utility.FormatPrice(SteamInventory.Currency, (double)this.LocalPrice / 100.0);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00011544 File Offset: 0x0000F744
		public int LocalBasePrice
		{
			get
			{
				ulong num = 0UL;
				ulong num2 = 0UL;
				bool flag = !SteamInventory.Internal.GetItemPrice(this.Id, ref num, ref num2);
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = (int)num2;
				}
				return result;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x00011582 File Offset: 0x0000F782
		public string LocalBasePriceFormatted
		{
			get
			{
				return Utility.FormatPrice(SteamInventory.Currency, (double)this.LocalPrice / 100.0);
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x000115A0 File Offset: 0x0000F7A0
		public InventoryRecipe[] GetRecipesContainingThis()
		{
			bool flag = this._recContaining != null;
			InventoryRecipe[] recContaining;
			if (flag)
			{
				recContaining = this._recContaining;
			}
			else
			{
				IEnumerable<InventoryRecipe> source = (from x in SteamInventory.Definitions
				select x.GetRecipes() into x
				where x != null
				select x).SelectMany((InventoryRecipe[] x) => x);
				this._recContaining = (from x in source
				where x.ContainsIngredient(this)
				select x).ToArray<InventoryRecipe>();
				recContaining = this._recContaining;
			}
			return recContaining;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00011660 File Offset: 0x0000F860
		public static bool operator ==(InventoryDef a, InventoryDef b)
		{
			bool flag = a == null;
			bool result;
			if (flag)
			{
				result = (b == null);
			}
			else
			{
				result = a.Equals(b);
			}
			return result;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00011688 File Offset: 0x0000F888
		public static bool operator !=(InventoryDef a, InventoryDef b)
		{
			return !(a == b);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00011694 File Offset: 0x0000F894
		public override bool Equals(object p)
		{
			return this.Equals((InventoryDef)p);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000116A4 File Offset: 0x0000F8A4
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x000116C0 File Offset: 0x0000F8C0
		public bool Equals(InventoryDef p)
		{
			bool flag = p == null;
			return !flag && p.Id == this.Id;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000116EF File Offset: 0x0000F8EF
		[CompilerGenerated]
		private InventoryRecipe <GetRecipes>b__21_0(string x)
		{
			return InventoryRecipe.FromString(x, this);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000116F8 File Offset: 0x0000F8F8
		[CompilerGenerated]
		private bool <GetRecipesContainingThis>b__44_3(InventoryRecipe x)
		{
			return x.ContainsIngredient(this);
		}

		// Token: 0x04000755 RID: 1877
		internal InventoryDefId _id;

		// Token: 0x04000756 RID: 1878
		internal Dictionary<string, string> _properties;

		// Token: 0x04000757 RID: 1879
		private InventoryRecipe[] _recContaining;

		// Token: 0x02000261 RID: 609
		[CompilerGenerated]
		private sealed class <get_Properties>d__34 : IEnumerable<KeyValuePair<string, string>>, IEnumerable, IEnumerator<KeyValuePair<string, string>>, IDisposable, IEnumerator
		{
			// Token: 0x060011D5 RID: 4565 RVA: 0x00020D12 File Offset: 0x0001EF12
			[DebuggerHidden]
			public <get_Properties>d__34(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060011D6 RID: 4566 RVA: 0x00020D2D File Offset: 0x0001EF2D
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060011D7 RID: 4567 RVA: 0x00020D30 File Offset: 0x0001EF30
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					key = null;
					i++;
				}
				else
				{
					this.<>1__state = -1;
					list = base.GetProperty(null);
					keys = list.Split(new char[]
					{
						','
					});
					array = keys;
					i = 0;
				}
				if (i >= array.Length)
				{
					array = null;
					return false;
				}
				key = array[i];
				this.<>2__current = new KeyValuePair<string, string>(key, base.GetProperty(key));
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000305 RID: 773
			// (get) Token: 0x060011D8 RID: 4568 RVA: 0x00020E18 File Offset: 0x0001F018
			KeyValuePair<string, string> IEnumerator<KeyValuePair<string, string>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060011D9 RID: 4569 RVA: 0x00020E20 File Offset: 0x0001F020
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000306 RID: 774
			// (get) Token: 0x060011DA RID: 4570 RVA: 0x00020E27 File Offset: 0x0001F027
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060011DB RID: 4571 RVA: 0x00020E34 File Offset: 0x0001F034
			[DebuggerHidden]
			IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
			{
				InventoryDef.<get_Properties>d__34 <get_Properties>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_Properties>d__ = this;
				}
				else
				{
					<get_Properties>d__ = new InventoryDef.<get_Properties>d__34(0);
					<get_Properties>d__.<>4__this = this;
				}
				return <get_Properties>d__;
			}

			// Token: 0x060011DC RID: 4572 RVA: 0x00020E77 File Offset: 0x0001F077
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.String>>.GetEnumerator();
			}

			// Token: 0x04000E1C RID: 3612
			private int <>1__state;

			// Token: 0x04000E1D RID: 3613
			private KeyValuePair<string, string> <>2__current;

			// Token: 0x04000E1E RID: 3614
			private int <>l__initialThreadId;

			// Token: 0x04000E1F RID: 3615
			public InventoryDef <>4__this;

			// Token: 0x04000E20 RID: 3616
			private string <list>5__1;

			// Token: 0x04000E21 RID: 3617
			private string[] <keys>5__2;

			// Token: 0x04000E22 RID: 3618
			private string[] <>s__3;

			// Token: 0x04000E23 RID: 3619
			private int <>s__4;

			// Token: 0x04000E24 RID: 3620
			private string <key>5__5;
		}

		// Token: 0x02000262 RID: 610
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060011DD RID: 4573 RVA: 0x00020E7F File Offset: 0x0001F07F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060011DE RID: 4574 RVA: 0x00020E8B File Offset: 0x0001F08B
			public <>c()
			{
			}

			// Token: 0x060011DF RID: 4575 RVA: 0x00020E94 File Offset: 0x0001F094
			internal InventoryRecipe[] <GetRecipesContainingThis>b__44_0(InventoryDef x)
			{
				return x.GetRecipes();
			}

			// Token: 0x060011E0 RID: 4576 RVA: 0x00020E9C File Offset: 0x0001F09C
			internal bool <GetRecipesContainingThis>b__44_1(InventoryRecipe[] x)
			{
				return x != null;
			}

			// Token: 0x060011E1 RID: 4577 RVA: 0x00020EA2 File Offset: 0x0001F0A2
			internal IEnumerable<InventoryRecipe> <GetRecipesContainingThis>b__44_2(InventoryRecipe[] x)
			{
				return x;
			}

			// Token: 0x04000E25 RID: 3621
			public static readonly InventoryDef.<>c <>9 = new InventoryDef.<>c();

			// Token: 0x04000E26 RID: 3622
			public static Func<InventoryDef, InventoryRecipe[]> <>9__44_0;

			// Token: 0x04000E27 RID: 3623
			public static Func<InventoryRecipe[], bool> <>9__44_1;

			// Token: 0x04000E28 RID: 3624
			public static Func<InventoryRecipe[], IEnumerable<InventoryRecipe>> <>9__44_2;
		}
	}
}
