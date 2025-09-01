using System;
using UnityEngine;

// Token: 0x0200019F RID: 415
public class Choice
{
	// Token: 0x1700013E RID: 318
	// (get) Token: 0x0600115B RID: 4443 RVA: 0x0006B9A1 File Offset: 0x00069BA1
	public bool IsAugment
	{
		get
		{
			return this.ChoiceType == ChoiceType.EnemyScroll || this.ChoiceType == ChoiceType.PlayerScroll;
		}
	}

	// Token: 0x1700013F RID: 319
	// (get) Token: 0x0600115C RID: 4444 RVA: 0x0006B9B7 File Offset: 0x00069BB7
	public bool NeedsVote
	{
		get
		{
			return this.ChoiceType == ChoiceType.EnemyScroll;
		}
	}

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x0600115D RID: 4445 RVA: 0x0006B9C2 File Offset: 0x00069BC2
	public AugmentTree Augment
	{
		get
		{
			return this.modifiers;
		}
	}

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x0600115E RID: 4446 RVA: 0x0006B9CA File Offset: 0x00069BCA
	public GenreTree Genre
	{
		get
		{
			return this.genreOption;
		}
	}

	// Token: 0x0600115F RID: 4447 RVA: 0x0006B9D4 File Offset: 0x00069BD4
	public Choice(ChoiceType cType, AugmentTree mods, int voteIndex = -1)
	{
		this.ChoiceType = cType;
		this.VoteIndex = voteIndex;
		this.modifiers = mods;
		this.Title = mods.Root.Name;
		this.Detail = TextParser.AugmentDetail(mods.Root.Detail, mods, PlayerControl.myInstance, false);
		this.Icon = mods.Root.Icon;
		this.Rarity = mods.Root.DisplayQuality;
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x0006BA54 File Offset: 0x00069C54
	public Choice(ChoiceType cType, GenreTree genre, int voteIndex = -1)
	{
		this.ChoiceType = cType;
		this.VoteIndex = voteIndex;
		this.genreOption = genre;
		GenreRootNode genreRootNode = genre.RootNode as GenreRootNode;
		this.Title = genreRootNode.Name;
		this.Detail = TextParser.AugmentDetail(genreRootNode.Detail, null, null, false);
		this.Icon = genreRootNode.Icon;
	}

	// Token: 0x06001161 RID: 4449 RVA: 0x0006BABC File Offset: 0x00069CBC
	public void Choose()
	{
		ChoiceType choiceType = this.ChoiceType;
		if (choiceType != ChoiceType.PlayerScroll)
		{
			return;
		}
		PlayerChoicePanel.instance.ConfirmPlayerScroll(this.modifiers, this.scrollUI);
	}

	// Token: 0x04000FD9 RID: 4057
	public string Title;

	// Token: 0x04000FDA RID: 4058
	public string Detail;

	// Token: 0x04000FDB RID: 4059
	public Sprite Icon;

	// Token: 0x04000FDC RID: 4060
	public ChoiceType ChoiceType;

	// Token: 0x04000FDD RID: 4061
	public int VoteIndex;

	// Token: 0x04000FDE RID: 4062
	public AugmentQuality Rarity = AugmentQuality.Normal;

	// Token: 0x04000FDF RID: 4063
	public ChoiceOption optionUI;

	// Token: 0x04000FE0 RID: 4064
	public UIPlayerScroll scrollUI;

	// Token: 0x04000FE1 RID: 4065
	private AugmentTree modifiers;

	// Token: 0x04000FE2 RID: 4066
	private GenreTree genreOption;
}
