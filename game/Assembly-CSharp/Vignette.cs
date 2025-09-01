using System;
using System.Collections.Generic;

// Token: 0x0200005D RID: 93
[Serializable]
public class Vignette
{
	// Token: 0x17000037 RID: 55
	// (get) Token: 0x060002EA RID: 746 RVA: 0x00018D78 File Offset: 0x00016F78
	private string HeaderText
	{
		get
		{
			if (this.Scene != null)
			{
				return this.Scene.SceneName;
			}
			return "UNDEFINED";
		}
	}

	// Token: 0x060002EB RID: 747 RVA: 0x00018D93 File Offset: 0x00016F93
	public Vignette()
	{
	}

	// Token: 0x040002D0 RID: 720
	public SceneField Scene;

	// Token: 0x040002D1 RID: 721
	public string Name;

	// Token: 0x040002D2 RID: 722
	public Rarity Rarity;

	// Token: 0x040002D3 RID: 723
	public GameMap.Biome Biome;

	// Token: 0x040002D4 RID: 724
	public bool Repeatable = true;

	// Token: 0x040002D5 RID: 725
	public int MinWave;

	// Token: 0x040002D6 RID: 726
	public int MinBinding;

	// Token: 0x040002D7 RID: 727
	public int MinAppendix;

	// Token: 0x040002D8 RID: 728
	public List<Vignette.Tag> Tags;

	// Token: 0x0200047A RID: 1146
	public enum Tag
	{
		// Token: 0x040022CA RID: 8906
		_,
		// Token: 0x040022CB RID: 8907
		__,
		// Token: 0x040022CC RID: 8908
		___,
		// Token: 0x040022CD RID: 8909
		TomeSpecific,
		// Token: 0x040022CE RID: 8910
		JumpPuzzle,
		// Token: 0x040022CF RID: 8911
		Randomness,
		// Token: 0x040022D0 RID: 8912
		Tradeoff
	}
}
