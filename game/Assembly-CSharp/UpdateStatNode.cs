using System;
using UnityEngine;

// Token: 0x020002EB RID: 747
public class UpdateStatNode : EffectNode
{
	// Token: 0x06001AC3 RID: 6851 RVA: 0x000A670C File Offset: 0x000A490C
	internal override void Apply(EffectProperties properties)
	{
		if (MapManager.InLobbyScene)
		{
			return;
		}
		if (properties.SourceControl != PlayerControl.myInstance && properties.AffectedControl != PlayerControl.myInstance)
		{
			return;
		}
		float num = (float)this.Value;
		if (this.Num != null)
		{
			NumberNode num2 = this.Num;
			if (num2 != null)
			{
				num = num2.Evaluate(properties);
			}
		}
		if (num <= 0f)
		{
			return;
		}
		uint num3 = (uint)num;
		if (num3 == 0U)
		{
			return;
		}
		switch (this.Category)
		{
		case UpdateStatNode.StatCategory.SignatureInk:
			GameStats.IncrementStat(this.Color, this.ColorStat, num3, false);
			return;
		case UpdateStatNode.StatCategory.Unique:
			GameStats.IncrementStat(this.SpecialStat, num3, false);
			return;
		case UpdateStatNode.StatCategory.Ephemeral:
		{
			string id = this.StatID;
			if (this.UseInputID)
			{
				id = properties.InputID;
			}
			GameStats.IncrementEphemeral(id, num3);
			return;
		}
		default:
			return;
		}
	}

	// Token: 0x06001AC4 RID: 6852 RVA: 0x000A67D9 File Offset: 0x000A49D9
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Add Stat",
			MinInspectorSize = new Vector2(180f, 0f),
			MaxInspectorSize = new Vector2(180f, 0f)
		};
	}

	// Token: 0x06001AC5 RID: 6853 RVA: 0x000A6815 File Offset: 0x000A4A15
	public UpdateStatNode()
	{
	}

	// Token: 0x04001B51 RID: 6993
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "", PortLocation.Header)]
	public NumberNode Num;

	// Token: 0x04001B52 RID: 6994
	public UpdateStatNode.StatCategory Category;

	// Token: 0x04001B53 RID: 6995
	public MagicColor Color;

	// Token: 0x04001B54 RID: 6996
	public GameStats.SignatureStat ColorStat;

	// Token: 0x04001B55 RID: 6997
	public GameStats.SpecialStat SpecialStat;

	// Token: 0x04001B56 RID: 6998
	public bool UseInputID;

	// Token: 0x04001B57 RID: 6999
	public string StatID;

	// Token: 0x04001B58 RID: 7000
	public int Value = 1;

	// Token: 0x02000650 RID: 1616
	public enum StatCategory
	{
		// Token: 0x04002AFE RID: 11006
		SignatureInk,
		// Token: 0x04002AFF RID: 11007
		Unique,
		// Token: 0x04002B00 RID: 11008
		Ephemeral
	}
}
