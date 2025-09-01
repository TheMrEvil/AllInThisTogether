using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Steamworks
{
	// Token: 0x020000B1 RID: 177
	public struct InventoryRecipe : IEquatable<InventoryRecipe>
	{
		// Token: 0x060009B8 RID: 2488 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		internal static InventoryRecipe FromString(string part, InventoryDef Result)
		{
			InventoryRecipe result = new InventoryRecipe
			{
				Result = Result,
				Source = part
			};
			string[] source = part.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			result.Ingredients = (from x in source
			select InventoryRecipe.Ingredient.FromString(x) into x
			where x.DefinitionId != 0
			select x).ToArray<InventoryRecipe.Ingredient>();
			return result;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00011B5C File Offset: 0x0000FD5C
		internal bool ContainsIngredient(InventoryDef inventoryDef)
		{
			return this.Ingredients.Any((InventoryRecipe.Ingredient x) => x.DefinitionId == inventoryDef.Id);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00011B92 File Offset: 0x0000FD92
		public static bool operator ==(InventoryRecipe a, InventoryRecipe b)
		{
			return a.GetHashCode() == b.GetHashCode();
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00011BB0 File Offset: 0x0000FDB0
		public static bool operator !=(InventoryRecipe a, InventoryRecipe b)
		{
			return a.GetHashCode() != b.GetHashCode();
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00011BD1 File Offset: 0x0000FDD1
		public override bool Equals(object p)
		{
			return this.Equals((InventoryRecipe)p);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00011BE0 File Offset: 0x0000FDE0
		public override int GetHashCode()
		{
			return this.Source.GetHashCode();
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00011BFD File Offset: 0x0000FDFD
		public bool Equals(InventoryRecipe p)
		{
			return p.GetHashCode() == this.GetHashCode();
		}

		// Token: 0x0400075D RID: 1885
		public InventoryDef Result;

		// Token: 0x0400075E RID: 1886
		public InventoryRecipe.Ingredient[] Ingredients;

		// Token: 0x0400075F RID: 1887
		public string Source;

		// Token: 0x02000267 RID: 615
		public struct Ingredient
		{
			// Token: 0x060011EB RID: 4587 RVA: 0x00021210 File Offset: 0x0001F410
			internal static InventoryRecipe.Ingredient FromString(string part)
			{
				InventoryRecipe.Ingredient ingredient = default(InventoryRecipe.Ingredient);
				ingredient.Count = 1;
				try
				{
					bool flag = part.Contains("x");
					if (flag)
					{
						int num = part.IndexOf('x');
						int count = 0;
						bool flag2 = int.TryParse(part.Substring(num + 1), out count);
						if (flag2)
						{
							ingredient.Count = count;
						}
						part = part.Substring(0, num);
					}
					ingredient.DefinitionId = int.Parse(part);
					ingredient.Definition = SteamInventory.FindDefinition(ingredient.DefinitionId);
				}
				catch (Exception)
				{
					return ingredient;
				}
				return ingredient;
			}

			// Token: 0x04000E41 RID: 3649
			public int DefinitionId;

			// Token: 0x04000E42 RID: 3650
			public InventoryDef Definition;

			// Token: 0x04000E43 RID: 3651
			public int Count;
		}

		// Token: 0x02000268 RID: 616
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060011EC RID: 4588 RVA: 0x000212B8 File Offset: 0x0001F4B8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060011ED RID: 4589 RVA: 0x000212C4 File Offset: 0x0001F4C4
			public <>c()
			{
			}

			// Token: 0x060011EE RID: 4590 RVA: 0x000212CD File Offset: 0x0001F4CD
			internal InventoryRecipe.Ingredient <FromString>b__4_0(string x)
			{
				return InventoryRecipe.Ingredient.FromString(x);
			}

			// Token: 0x060011EF RID: 4591 RVA: 0x000212D5 File Offset: 0x0001F4D5
			internal bool <FromString>b__4_1(InventoryRecipe.Ingredient x)
			{
				return x.DefinitionId != 0;
			}

			// Token: 0x04000E44 RID: 3652
			public static readonly InventoryRecipe.<>c <>9 = new InventoryRecipe.<>c();

			// Token: 0x04000E45 RID: 3653
			public static Func<string, InventoryRecipe.Ingredient> <>9__4_0;

			// Token: 0x04000E46 RID: 3654
			public static Func<InventoryRecipe.Ingredient, bool> <>9__4_1;
		}

		// Token: 0x02000269 RID: 617
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x060011F0 RID: 4592 RVA: 0x000212E0 File Offset: 0x0001F4E0
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x060011F1 RID: 4593 RVA: 0x000212E9 File Offset: 0x0001F4E9
			internal bool <ContainsIngredient>b__0(InventoryRecipe.Ingredient x)
			{
				return x.DefinitionId == this.inventoryDef.Id;
			}

			// Token: 0x04000E47 RID: 3655
			public InventoryDef inventoryDef;
		}
	}
}
