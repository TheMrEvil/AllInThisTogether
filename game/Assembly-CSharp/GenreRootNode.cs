using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000360 RID: 864
public class GenreRootNode : RootNode
{
	// Token: 0x1700019B RID: 411
	// (get) Token: 0x06001CCD RID: 7373 RVA: 0x000AF72B File Offset: 0x000AD92B
	public int TotalWaves
	{
		get
		{
			return this.Waves.Count;
		}
	}

	// Token: 0x06001CCE RID: 7374 RVA: 0x000AF738 File Offset: 0x000AD938
	public bool FinishedGame(int waveIndex, int AppendixLevel)
	{
		if (AppendixLevel > 0)
		{
			return this.Appendix.Count <= this.TransformWaveIndex(waveIndex, AppendixLevel);
		}
		return this.Waves.Count <= waveIndex;
	}

	// Token: 0x06001CCF RID: 7375 RVA: 0x000AF768 File Offset: 0x000AD968
	private int TransformWaveIndex(int index, int appendixLevel)
	{
		index -= this.Waves.Count;
		index -= appendixLevel;
		for (int i = 0; i < appendixLevel - 1; i++)
		{
			index -= this.Appendix.Count;
		}
		return index;
	}

	// Token: 0x06001CD0 RID: 7376 RVA: 0x000AF7A8 File Offset: 0x000AD9A8
	public List<AugmentTree> GetAugments(int waveIndex, ModType modType)
	{
		this.augments.Clear();
		if (this.WorldOptions != null)
		{
			GenreWorldNode genreWorldNode = this.WorldOptions as GenreWorldNode;
			if (genreWorldNode != null)
			{
				genreWorldNode.AddAugments(ref this.augments, modType);
			}
		}
		if (this.Waves.ValidIndex(waveIndex) && this.Waves[waveIndex] != null)
		{
			GenreWaveNode genreWaveNode = this.Waves[waveIndex] as GenreWaveNode;
			if (genreWaveNode != null && genreWaveNode != null && genreWaveNode.WaveOptions != null)
			{
				GenreWorldNode genreWorldNode2 = genreWaveNode.WaveOptions as GenreWorldNode;
				if (genreWorldNode2 != null)
				{
					genreWorldNode2.AddAugments(ref this.augments, modType);
				}
			}
		}
		return this.augments;
	}

	// Token: 0x06001CD1 RID: 7377 RVA: 0x000AF85C File Offset: 0x000ADA5C
	public GenreWaveNode GetWave(int waveIndex, int appendix)
	{
		if (appendix > 0)
		{
			waveIndex = this.TransformWaveIndex(waveIndex, appendix);
			if (!this.Appendix.ValidIndex(waveIndex))
			{
				return null;
			}
			return this.Appendix[waveIndex] as GenreWaveNode;
		}
		else
		{
			if (!this.Waves.ValidIndex(waveIndex))
			{
				return null;
			}
			return this.Waves[waveIndex] as GenreWaveNode;
		}
	}

	// Token: 0x06001CD2 RID: 7378 RVA: 0x000AF8BC File Offset: 0x000ADABC
	public GenreRewardNode GetReward(int wave, int appendix)
	{
		if (wave < 0)
		{
			if (this.StartRewards == null)
			{
				return null;
			}
			return this.StartRewards as GenreRewardNode;
		}
		else
		{
			GenreWaveNode wave2 = this.GetWave(wave, appendix);
			if (appendix > 0 && wave2 == null && this.AppendixRewards != null)
			{
				return this.AppendixRewards as GenreRewardNode;
			}
			if (wave2 == null)
			{
				return null;
			}
			return wave2.Reward as GenreRewardNode;
		}
	}

	// Token: 0x06001CD3 RID: 7379 RVA: 0x000AF92F File Offset: 0x000ADB2F
	public GameMap GetFirstMap()
	{
		return GenreMapNode.GetMap(this.FirstMap, false, this.NextMapNode);
	}

	// Token: 0x06001CD4 RID: 7380 RVA: 0x000AF943 File Offset: 0x000ADB43
	public GameMap GetFirstAppendixMap()
	{
		return GenreMapNode.GetMap(this.AppendixMap, false, this.FirstAppendixMap);
	}

	// Token: 0x06001CD5 RID: 7381 RVA: 0x000AF958 File Offset: 0x000ADB58
	public GenreVignetteNode GetVignette(int wave, int appendix)
	{
		GenreWaveNode wave2 = this.GetWave(wave, appendix);
		if (wave2 != null)
		{
			if (wave2.NextVignette != null)
			{
				GenreVignetteNode genreVignetteNode = wave2.NextVignette as GenreVignetteNode;
				if (genreVignetteNode != null)
				{
					return genreVignetteNode;
				}
			}
			return null;
		}
		if (appendix > 0 && this.FirstVignetteNode != null)
		{
			return this.FirstVignetteNode as GenreVignetteNode;
		}
		return null;
	}

	// Token: 0x06001CD6 RID: 7382 RVA: 0x000AF9B7 File Offset: 0x000ADBB7
	public bool NextMapExplicit()
	{
		return this.FirstMap == GenreWaveNode.NextMapType.Explicit;
	}

	// Token: 0x06001CD7 RID: 7383 RVA: 0x000AF9C2 File Offset: 0x000ADBC2
	public bool AppendixMapExplicit()
	{
		return this.AppendixMap == GenreWaveNode.NextMapType.Explicit;
	}

	// Token: 0x06001CD8 RID: 7384 RVA: 0x000AF9CD File Offset: 0x000ADBCD
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Root",
			ShowInputNode = false,
			MinInspectorSize = new Vector2(320f, 0f)
		};
	}

	// Token: 0x06001CD9 RID: 7385 RVA: 0x000AF9FB File Offset: 0x000ADBFB
	public GenreRootNode()
	{
	}

	// Token: 0x04001D7A RID: 7546
	public Sprite Icon;

	// Token: 0x04001D7B RID: 7547
	public string Name;

	// Token: 0x04001D7C RID: 7548
	[Space]
	[TextArea(3, 4)]
	public string Detail;

	// Token: 0x04001D7D RID: 7549
	public Sprite VFXIcon;

	// Token: 0x04001D7E RID: 7550
	public string ShortName;

	// Token: 0x04001D7F RID: 7551
	public bool HasTomePower;

	// Token: 0x04001D80 RID: 7552
	public AugmentTree TomePowerAugment;

	// Token: 0x04001D81 RID: 7553
	public bool IsNegativePower;

	// Token: 0x04001D82 RID: 7554
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreWaveNode), true, "Waves", PortLocation.Header)]
	public List<Node> Waves = new List<Node>();

	// Token: 0x04001D83 RID: 7555
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreWorldNode), false, "World Options", PortLocation.Default)]
	public Node WorldOptions;

	// Token: 0x04001D84 RID: 7556
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreRewardNode), true, "Starting Rewards", PortLocation.Default)]
	public Node StartRewards;

	// Token: 0x04001D85 RID: 7557
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreFountainNode), true, "Fountain", PortLocation.Default)]
	public Node FountainNode;

	// Token: 0x04001D86 RID: 7558
	[ShowPort("NextMapExplicit")]
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreMapNode), false, "First Map", PortLocation.Default)]
	public Node NextMapNode;

	// Token: 0x04001D87 RID: 7559
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreMapNode), false, "First Vignette", PortLocation.Default)]
	public Node FirstVignetteNode;

	// Token: 0x04001D88 RID: 7560
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreWaveNode), true, "Appendix", PortLocation.Default)]
	public List<Node> Appendix = new List<Node>();

	// Token: 0x04001D89 RID: 7561
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreRewardNode), true, "Appendix Rewards", PortLocation.Default)]
	public Node AppendixRewards;

	// Token: 0x04001D8A RID: 7562
	[ShowPort("AppendixMapExplicit")]
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreMapNode), false, "Appendix Map", PortLocation.Default)]
	public Node FirstAppendixMap;

	// Token: 0x04001D8B RID: 7563
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreMapNode), false, "First Appendix Vignette", PortLocation.Default)]
	public Node AppendixVignetteNode;

	// Token: 0x04001D8C RID: 7564
	public GenreWaveNode.NextMapType FirstMap = GenreWaveNode.NextMapType.Random;

	// Token: 0x04001D8D RID: 7565
	public GenreWaveNode.NextMapType AppendixMap = GenreWaveNode.NextMapType.Random;

	// Token: 0x04001D8E RID: 7566
	public bool IsAvailable = true;

	// Token: 0x04001D8F RID: 7567
	private List<AugmentTree> augments = new List<AugmentTree>();
}
