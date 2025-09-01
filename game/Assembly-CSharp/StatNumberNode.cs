using System;
using UnityEngine;

// Token: 0x02000388 RID: 904
public class StatNumberNode : NumberNode
{
	// Token: 0x06001D98 RID: 7576 RVA: 0x000B3C34 File Offset: 0x000B1E34
	public override float Evaluate(EffectProperties props)
	{
		float result;
		switch (this.StatCategory)
		{
		case StatNumberNode.StatType.Global:
			result = (float)GameStats.GetGlobalStat(this.GlobalStat, 0);
			break;
		case StatNumberNode.StatType.Tome:
			result = (float)GameStats.GetTomeStat(this.Tome, this.GlobalStat, 0);
			break;
		case StatNumberNode.StatType.Composite:
			result = (float)GameStats.GetCompositStat(this.CompositeStat);
			break;
		case StatNumberNode.StatType.CurrentGame:
			result = (float)PlayerControl.myInstance.PStats.GetStat(this.GameStat);
			break;
		case StatNumberNode.StatType.GameplayBest:
			result = GameStats.GetPlayerStat(this.GameStat, 0U);
			break;
		case StatNumberNode.StatType.Meta:
			result = (float)GameStats.GetMetaStat(this.MetaStat);
			break;
		case StatNumberNode.StatType.InkColor:
			result = GameStats.GetColorStat(this.Color, this.ColorStat, 0U);
			break;
		case StatNumberNode.StatType.Ephemeral:
			result = (this.UseInputID ? GameStats.GetEphemeralStat(props.InputID) : GameStats.GetEphemeralStat(this.StatID));
			break;
		case StatNumberNode.StatType.Unique:
			result = (float)GameStats.GetSpecialStat(this.SpecialStat);
			break;
		case StatNumberNode.StatType.Raid:
			result = (float)GameStats.GetRaidStat(this.EncounterID, this.Stat, 0);
			break;
		case StatNumberNode.StatType.RaidColor:
			result = (float)(GameStats.HasCoreRaidSticker(this.RaidColor, this.Color) ? 1 : 0);
			break;
		default:
			result = 0f;
			break;
		}
		return result;
	}

	// Token: 0x06001D99 RID: 7577 RVA: 0x000B3D80 File Offset: 0x000B1F80
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Stat Value",
			MinInspectorSize = new Vector2(150f, 0f),
			MaxInspectorSize = new Vector2(150f, 0f),
			AllowMultipleInputs = true
		};
	}

	// Token: 0x06001D9A RID: 7578 RVA: 0x000B3DCE File Offset: 0x000B1FCE
	public StatNumberNode()
	{
	}

	// Token: 0x04001E41 RID: 7745
	public StatNumberNode.StatType StatCategory;

	// Token: 0x04001E42 RID: 7746
	public GenreTree Tome;

	// Token: 0x04001E43 RID: 7747
	public MagicColor Color;

	// Token: 0x04001E44 RID: 7748
	public GameStats.Stat GlobalStat;

	// Token: 0x04001E45 RID: 7749
	public PlayerStat GameStat;

	// Token: 0x04001E46 RID: 7750
	public GameStats.CompositeStat CompositeStat;

	// Token: 0x04001E47 RID: 7751
	public GameStats.MetaStat MetaStat;

	// Token: 0x04001E48 RID: 7752
	public GameStats.SignatureStat ColorStat;

	// Token: 0x04001E49 RID: 7753
	public GameStats.SpecialStat SpecialStat;

	// Token: 0x04001E4A RID: 7754
	public string EncounterID;

	// Token: 0x04001E4B RID: 7755
	public GameStats.RaidStat Stat;

	// Token: 0x04001E4C RID: 7756
	public GameStats.RaidStickerType RaidColor;

	// Token: 0x04001E4D RID: 7757
	public bool UseInputID;

	// Token: 0x04001E4E RID: 7758
	public string StatID;

	// Token: 0x02000686 RID: 1670
	public enum StatType
	{
		// Token: 0x04002BE8 RID: 11240
		Global,
		// Token: 0x04002BE9 RID: 11241
		Tome,
		// Token: 0x04002BEA RID: 11242
		Composite,
		// Token: 0x04002BEB RID: 11243
		CurrentGame,
		// Token: 0x04002BEC RID: 11244
		GameplayBest,
		// Token: 0x04002BED RID: 11245
		Meta,
		// Token: 0x04002BEE RID: 11246
		InkColor,
		// Token: 0x04002BEF RID: 11247
		Ephemeral,
		// Token: 0x04002BF0 RID: 11248
		Unique,
		// Token: 0x04002BF1 RID: 11249
		Raid,
		// Token: 0x04002BF2 RID: 11250
		RaidColor
	}
}
