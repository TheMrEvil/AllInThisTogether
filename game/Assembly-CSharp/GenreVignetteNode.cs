using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000364 RID: 868
public class GenreVignetteNode : Node
{
	// Token: 0x06001CEE RID: 7406 RVA: 0x000AFF5C File Offset: 0x000AE15C
	public static Vignette GetVignette(GenreVignetteNode.GameVignetteFilter filter)
	{
		List<Vignette> list = new List<Vignette>();
		int bindingLevel = GameplayManager.BindingLevel;
		int appendixLevel = WaveManager.instance.AppendixLevel;
		int currentWave = WaveManager.CurrentWave;
		foreach (Vignette vignette in AIManager.instance.Waves.Vignettes)
		{
			if (filter != null && filter.Explicit)
			{
				using (List<SceneField>.Enumerator enumerator2 = filter.OptionOverrides.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.SceneName == vignette.Scene.SceneName)
						{
							list.Add(vignette);
							break;
						}
					}
					continue;
				}
			}
			if ((vignette.Biome.AnyFlagsMatch(MapManager.TomeBiome) || vignette.Biome == GameMap.Biome.Any || !AIManager.instance.Waves.UseMapBiomes) && (!MapManager.UsedVignettes.Contains(vignette.Scene.SceneName) || vignette.Repeatable) && currentWave >= vignette.MinWave && bindingLevel >= vignette.MinBinding && appendixLevel >= vignette.MinAppendix && vignette.Rarity != Rarity.Explicit)
			{
				list.Add(vignette);
			}
		}
		int num = list.Count - 1;
		while (num >= 0 && list.Count > 1)
		{
			if (MapManager.LastVignettes.Contains(list[num].Scene.SceneName))
			{
				list.RemoveAt(num);
			}
			num--;
		}
		if (filter != null)
		{
			filter.FilterList(list);
		}
		if (list.Count == 0)
		{
			return null;
		}
		return GenreVignetteNode.ChooseVignetteFromList(list);
	}

	// Token: 0x06001CEF RID: 7407 RVA: 0x000B0124 File Offset: 0x000AE324
	public Vignette GetVignette()
	{
		return GenreVignetteNode.GetVignette(this.Filter);
	}

	// Token: 0x06001CF0 RID: 7408 RVA: 0x000B0134 File Offset: 0x000AE334
	private static Vignette ChooseVignetteFromList(List<Vignette> options)
	{
		if (options.Count == 0)
		{
			return null;
		}
		List<Vignette> list = new List<Vignette>();
		foreach (Vignette vignette in options)
		{
			int num = (int)(100f / (float)GameDB.Rarity(vignette.Rarity).RelativeChance);
			for (int i = 0; i < num; i++)
			{
				list.Add(vignette);
			}
		}
		return list[MapManager.GetRandom(0, list.Count)];
	}

	// Token: 0x06001CF1 RID: 7409 RVA: 0x000B01D0 File Offset: 0x000AE3D0
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Vignette",
			AllowMultipleInputs = true,
			MinInspectorSize = new Vector2(150f, 0f)
		};
	}

	// Token: 0x06001CF2 RID: 7410 RVA: 0x000B01FE File Offset: 0x000AE3FE
	public override void OnCloned()
	{
		this.Filter = this.Filter.Copy();
	}

	// Token: 0x06001CF3 RID: 7411 RVA: 0x000B0211 File Offset: 0x000AE411
	public GenreVignetteNode()
	{
	}

	// Token: 0x04001D9A RID: 7578
	public GenreVignetteNode.GameVignetteFilter Filter;

	// Token: 0x02000676 RID: 1654
	[Serializable]
	public class GameVignetteFilter
	{
		// Token: 0x060027C9 RID: 10185 RVA: 0x000D7034 File Offset: 0x000D5234
		public void FilterList(List<Vignette> options)
		{
			for (int i = options.Count - 1; i >= 0; i--)
			{
				Vignette vignette = options[i];
				if (this.Explicit)
				{
					bool flag = false;
					foreach (SceneField sceneField in this.OptionOverrides)
					{
						flag |= (sceneField.SceneName == options[i].Scene.SceneName);
					}
					if (!flag)
					{
						options.RemoveAt(i);
					}
				}
				else if (vignette.Rarity == Rarity.Explicit)
				{
					options.RemoveAt(i);
				}
				else
				{
					if (this.RequireTags && this.Tags.Count > 0)
					{
						bool flag2 = false;
						foreach (Vignette.Tag item in this.Tags)
						{
							if (!options[i].Tags.Contains(item))
							{
								flag2 = true;
								break;
							}
						}
						if (flag2)
						{
							options.RemoveAt(i);
							goto IL_131;
						}
					}
					int rarity = (int)vignette.Rarity;
					if (this.RestrictRarity && (rarity < (int)this.MinRarity || rarity > (int)this.MaxRarity))
					{
						options.RemoveAt(i);
					}
				}
				IL_131:;
			}
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x000D719C File Offset: 0x000D539C
		public GenreVignetteNode.GameVignetteFilter Copy()
		{
			GenreVignetteNode.GameVignetteFilter gameVignetteFilter = base.MemberwiseClone() as GenreVignetteNode.GameVignetteFilter;
			gameVignetteFilter.Tags = this.Tags.Clone<Vignette.Tag>();
			gameVignetteFilter.OptionOverrides = this.OptionOverrides.Clone<SceneField>();
			return gameVignetteFilter;
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x000D71CB File Offset: 0x000D53CB
		public GameVignetteFilter()
		{
		}

		// Token: 0x04002BA2 RID: 11170
		public bool RequireTags;

		// Token: 0x04002BA3 RID: 11171
		public List<Vignette.Tag> Tags = new List<Vignette.Tag>();

		// Token: 0x04002BA4 RID: 11172
		public bool RestrictRarity;

		// Token: 0x04002BA5 RID: 11173
		public Rarity MinRarity;

		// Token: 0x04002BA6 RID: 11174
		public Rarity MaxRarity;

		// Token: 0x04002BA7 RID: 11175
		public bool Explicit;

		// Token: 0x04002BA8 RID: 11176
		public List<SceneField> OptionOverrides;
	}
}
