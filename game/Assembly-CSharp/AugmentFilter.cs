using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000343 RID: 835
[Serializable]
public class AugmentFilter
{
	// Token: 0x06001C49 RID: 7241 RVA: 0x000ACD18 File Offset: 0x000AAF18
	public List<AugmentTree> GetModifiers(ModType modType, List<string> exclude = null)
	{
		if (this.Explicit && this.OptionOverrides.Count == 3)
		{
			return this.OptionOverrides.Clone<AugmentTree>();
		}
		List<AugmentTree> validMods = GraphDB.GetValidMods(modType);
		this.FilterList(validMods, PlayerControl.myInstance);
		if (exclude != null && exclude.Count > 0)
		{
			validMods.RemoveAll((AugmentTree x) => exclude.Any((string y) => y == x.ID));
		}
		return validMods;
	}

	// Token: 0x06001C4A RID: 7242 RVA: 0x000ACD94 File Offset: 0x000AAF94
	public void FilterList(List<AugmentTree> options, PlayerControl player)
	{
		if (this.Explicit)
		{
			options.Clear();
			foreach (AugmentTree augmentTree in this.OptionOverrides)
			{
				if (!(player != null) || this.AllowDuplicates || !player.HasAugment(augmentTree.ID, true))
				{
					options.Add(augmentTree);
				}
			}
			return;
		}
		for (int i = options.Count - 1; i >= 0; i--)
		{
			AugmentRootNode root = options[i].Root;
			int displayQuality = (int)root.DisplayQuality;
			if (this.RestrictRarity && (displayQuality < (int)this.MinRarity || displayQuality > (int)this.MaxRarity))
			{
				options.RemoveAt(i);
			}
			else
			{
				if (this.RestrictEnemy && root.modType == ModType.Enemy)
				{
					bool flag = false;
					if (this.InvertEnemy)
					{
						flag = true;
						if (this.GenericOnly)
						{
							flag &= (root.ApplyType > EnemyType.Any);
						}
						else if (this.EnemyType == EnemyType.Any)
						{
							flag &= (root.ApplyType == EnemyType.Any);
						}
						else
						{
							flag &= this.EnemyType.Matches(root.ApplyType);
						}
					}
					else if (root.ApplyType != this.EnemyType)
					{
						if (!this.AllowGenericEnemy)
						{
							flag |= (root.ApplyType == EnemyType.Any);
						}
						flag |= !this.EnemyType.Matches(root.ApplyType);
					}
					if (!this.AllowBoss && (root.Filters.HasFilter(ModFilter.Enemy_Boss) || root.ApplyTo == EnemyLevel.Boss))
					{
						flag = true;
					}
					if (!this.AllowElite && root.ApplyTo == EnemyLevel.Elite)
					{
						flag = true;
					}
					if (flag)
					{
						options.RemoveAt(i);
						goto IL_25F;
					}
				}
				if (root.modType == ModType.Fountain && this.RestrictFountain && (root.InkCost < this.MinCost || (this.MaxCost > 0 && root.InkCost > this.MaxCost)))
				{
					options.RemoveAt(i);
				}
				else if (this.RestrictTags && !(this.IncludeModFilter.Matches(root.Filters, this.IncludeAnyTag) & !this.ExcludeModFilter.Matches(root.Filters, true)))
				{
					options.RemoveAt(i);
				}
				else if (!this.RestrictEnemy && root.Filters.HasFilter(ModFilter.Enemy_Boss) && (!this.RestrictTags || !this.IncludeModFilter.HasFilter(ModFilter.Enemy_Boss)))
				{
					options.RemoveAt(i);
				}
			}
			IL_25F:;
		}
	}

	// Token: 0x06001C4B RID: 7243 RVA: 0x000AD01C File Offset: 0x000AB21C
	public AugmentFilter Copy()
	{
		AugmentFilter augmentFilter = base.MemberwiseClone() as AugmentFilter;
		augmentFilter.IncludeModFilter = this.IncludeModFilter.Copy();
		augmentFilter.ExcludeModFilter = this.ExcludeModFilter.Copy();
		augmentFilter.OptionOverrides = new List<AugmentTree>();
		augmentFilter.OptionOverrides.AddRange(this.OptionOverrides);
		return augmentFilter;
	}

	// Token: 0x06001C4C RID: 7244 RVA: 0x000AD072 File Offset: 0x000AB272
	public AugmentFilter()
	{
	}

	// Token: 0x04001CD5 RID: 7381
	public bool RestrictRarity;

	// Token: 0x04001CD6 RID: 7382
	public AugmentQuality MinRarity;

	// Token: 0x04001CD7 RID: 7383
	public AugmentQuality MaxRarity;

	// Token: 0x04001CD8 RID: 7384
	public bool RestrictFountain;

	// Token: 0x04001CD9 RID: 7385
	public int MinCost;

	// Token: 0x04001CDA RID: 7386
	public int MaxCost;

	// Token: 0x04001CDB RID: 7387
	public bool RestrictEnemy;

	// Token: 0x04001CDC RID: 7388
	public EnemyType EnemyType;

	// Token: 0x04001CDD RID: 7389
	public bool InvertEnemy;

	// Token: 0x04001CDE RID: 7390
	public bool AllowGenericEnemy = true;

	// Token: 0x04001CDF RID: 7391
	public bool GenericOnly;

	// Token: 0x04001CE0 RID: 7392
	public bool AllowBoss;

	// Token: 0x04001CE1 RID: 7393
	public bool AllowElite;

	// Token: 0x04001CE2 RID: 7394
	public bool RestrictTags;

	// Token: 0x04001CE3 RID: 7395
	public bool IncludeAnyTag;

	// Token: 0x04001CE4 RID: 7396
	public ModFilterList IncludeModFilter = new ModFilterList();

	// Token: 0x04001CE5 RID: 7397
	public ModFilterList ExcludeModFilter = new ModFilterList();

	// Token: 0x04001CE6 RID: 7398
	public bool Explicit;

	// Token: 0x04001CE7 RID: 7399
	public List<AugmentTree> OptionOverrides;

	// Token: 0x04001CE8 RID: 7400
	public bool AllowDuplicates;

	// Token: 0x04001CE9 RID: 7401
	[HideInInspector]
	[SerializeField]
	public bool CanUseEnemy;

	// Token: 0x04001CEA RID: 7402
	[HideInInspector]
	[SerializeField]
	public bool CanUsePlayer;

	// Token: 0x04001CEB RID: 7403
	[HideInInspector]
	[SerializeField]
	public bool CanUseFountain;

	// Token: 0x02000669 RID: 1641
	[CompilerGenerated]
	private sealed class <>c__DisplayClass23_0
	{
		// Token: 0x060027BF RID: 10175 RVA: 0x000D6F8C File Offset: 0x000D518C
		public <>c__DisplayClass23_0()
		{
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x000D6F94 File Offset: 0x000D5194
		internal bool <GetModifiers>b__0(AugmentTree x)
		{
			AugmentFilter.<>c__DisplayClass23_1 CS$<>8__locals1 = new AugmentFilter.<>c__DisplayClass23_1();
			CS$<>8__locals1.x = x;
			return this.exclude.Any(new Func<string, bool>(CS$<>8__locals1.<GetModifiers>b__1));
		}

		// Token: 0x04002B7E RID: 11134
		public List<string> exclude;
	}

	// Token: 0x0200066A RID: 1642
	[CompilerGenerated]
	private sealed class <>c__DisplayClass23_1
	{
		// Token: 0x060027C1 RID: 10177 RVA: 0x000D6FC5 File Offset: 0x000D51C5
		public <>c__DisplayClass23_1()
		{
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x000D6FCD File Offset: 0x000D51CD
		internal bool <GetModifiers>b__1(string y)
		{
			return y == this.x.ID;
		}

		// Token: 0x04002B7F RID: 11135
		public AugmentTree x;
	}
}
