using System;
using System.Collections.Generic;

// Token: 0x0200035C RID: 860
[Serializable]
public class GameMapFilter
{
	// Token: 0x06001CB2 RID: 7346 RVA: 0x000AE9C4 File Offset: 0x000ACBC4
	public void FilterList(List<GameMap> options)
	{
		for (int i = options.Count - 1; i >= 0; i--)
		{
			GameMap gameMap = options[i];
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
			else if (gameMap.Rarity == Rarity.Explicit)
			{
				options.RemoveAt(i);
			}
			else
			{
				int size = (int)gameMap.Size;
				if (this.RestrictSize && (size < (int)this.MinSize || size > (int)this.MaxSize))
				{
					options.RemoveAt(i);
				}
				else if (gameMap.Type != this.MapType && (this.MapType != GameMap.MapType.BossArena || !gameMap.SupportsBoss))
				{
					options.RemoveAt(i);
				}
				else
				{
					int rarity = (int)gameMap.Rarity;
					if (this.RestrictRarity && (rarity < (int)this.MinRarity || rarity > (int)this.MaxRarity))
					{
						options.RemoveAt(i);
					}
				}
			}
		}
	}

	// Token: 0x06001CB3 RID: 7347 RVA: 0x000AEB00 File Offset: 0x000ACD00
	public GameMapFilter Copy()
	{
		return base.MemberwiseClone() as GameMapFilter;
	}

	// Token: 0x06001CB4 RID: 7348 RVA: 0x000AEB0D File Offset: 0x000ACD0D
	public GameMapFilter()
	{
	}

	// Token: 0x04001D65 RID: 7525
	public GameMap.MapType MapType;

	// Token: 0x04001D66 RID: 7526
	public bool RestrictSize;

	// Token: 0x04001D67 RID: 7527
	public GameMap.MapSize MinSize;

	// Token: 0x04001D68 RID: 7528
	public GameMap.MapSize MaxSize;

	// Token: 0x04001D69 RID: 7529
	public bool RestrictRarity;

	// Token: 0x04001D6A RID: 7530
	public Rarity MinRarity;

	// Token: 0x04001D6B RID: 7531
	public Rarity MaxRarity;

	// Token: 0x04001D6C RID: 7532
	public bool Explicit;

	// Token: 0x04001D6D RID: 7533
	public List<SceneField> OptionOverrides;
}
