using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002CF RID: 719
public class ModifyPropsNode : EffectNode
{
	// Token: 0x06001A52 RID: 6738 RVA: 0x000A3CDC File Offset: 0x000A1EDC
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Modify Props"
		};
	}

	// Token: 0x06001A53 RID: 6739 RVA: 0x000A3CF0 File Offset: 0x000A1EF0
	internal override void Apply(EffectProperties properties)
	{
		EffectProperties effectProperties = properties.Copy(false);
		EntityControl entity = this.NeedsEntity() ? properties.GetApplicationEntity(this.From) : null;
		EntityControl entity2 = this.NeedsEntity() ? properties.GetApplicationEntity(this.To) : null;
		switch (this.Modification)
		{
		case ModifyPropsNode.PropModType.Set:
			effectProperties.SetEntity(this.From, entity2);
			break;
		case ModifyPropsNode.PropModType.Swap:
			effectProperties.SetEntity(this.From, entity2);
			effectProperties.SetEntity(this.To, entity);
			break;
		case ModifyPropsNode.PropModType.AbilitySource:
			effectProperties.AbilityType = this.AbilityType;
			break;
		case ModifyPropsNode.PropModType.AddAugment:
			effectProperties.AddAugment(this.Augment.ID, 1);
			break;
		case ModifyPropsNode.PropModType.AddMana:
			effectProperties.AddMana(this.ManaColor, 1);
			break;
		case ModifyPropsNode.PropModType.ClearManaData:
			effectProperties.ClearManaData();
			break;
		case ModifyPropsNode.PropModType.RemoveEntity:
			effectProperties.SetEntity(this.From, null);
			break;
		case ModifyPropsNode.PropModType.NewRandomSeed:
			effectProperties.OverrideSeed(properties.RandomInt(0, int.MaxValue), 0);
			break;
		case ModifyPropsNode.PropModType.SeparateCache:
			effectProperties = effectProperties.Copy(true);
			if (this.IncrementRandom)
			{
				effectProperties.OverrideSeed(properties.RandomInt(0, int.MaxValue), 0);
			}
			break;
		case ModifyPropsNode.PropModType.ChangeCause:
			effectProperties.CauseName = this.CauseName;
			effectProperties.CauseID = this.CauseID;
			break;
		}
		bool flag = this.Effects.Count > 1;
		foreach (Node node in this.Effects)
		{
			((EffectNode)node).Invoke(flag ? effectProperties.Copy(false) : effectProperties);
		}
	}

	// Token: 0x06001A54 RID: 6740 RVA: 0x000A3EA4 File Offset: 0x000A20A4
	public override void TryCancel(EffectProperties props)
	{
		EffectProperties props2 = props.Copy(false);
		foreach (Node node in this.Effects)
		{
			((EffectNode)node).TryCancel(props2);
		}
	}

	// Token: 0x06001A55 RID: 6741 RVA: 0x000A3F04 File Offset: 0x000A2104
	public bool NeedsEntity()
	{
		ModifyPropsNode.PropModType modification = this.Modification;
		return modification == ModifyPropsNode.PropModType.Set || modification == ModifyPropsNode.PropModType.Swap || modification == ModifyPropsNode.PropModType.RemoveEntity;
	}

	// Token: 0x06001A56 RID: 6742 RVA: 0x000A3F34 File Offset: 0x000A2134
	public bool NeedsEntity2()
	{
		ModifyPropsNode.PropModType modification = this.Modification;
		return modification == ModifyPropsNode.PropModType.Set || modification == ModifyPropsNode.PropModType.Swap;
	}

	// Token: 0x06001A57 RID: 6743 RVA: 0x000A3F5C File Offset: 0x000A215C
	public ModifyPropsNode()
	{
	}

	// Token: 0x04001AC2 RID: 6850
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "Then", PortLocation.Header)]
	public List<Node> Effects = new List<Node>();

	// Token: 0x04001AC3 RID: 6851
	public ModifyPropsNode.PropModType Modification;

	// Token: 0x04001AC4 RID: 6852
	public ApplyOn From;

	// Token: 0x04001AC5 RID: 6853
	public ApplyOn To;

	// Token: 0x04001AC6 RID: 6854
	public PlayerAbilityType AbilityType;

	// Token: 0x04001AC7 RID: 6855
	public AugmentTree Augment;

	// Token: 0x04001AC8 RID: 6856
	public MagicColor ManaColor;

	// Token: 0x04001AC9 RID: 6857
	public string CauseName;

	// Token: 0x04001ACA RID: 6858
	public string CauseID;

	// Token: 0x04001ACB RID: 6859
	public bool IncrementRandom;

	// Token: 0x0200064D RID: 1613
	public enum PropModType
	{
		// Token: 0x04002AEA RID: 10986
		Set,
		// Token: 0x04002AEB RID: 10987
		Swap,
		// Token: 0x04002AEC RID: 10988
		AbilitySource,
		// Token: 0x04002AED RID: 10989
		AddAugment,
		// Token: 0x04002AEE RID: 10990
		AddMana,
		// Token: 0x04002AEF RID: 10991
		ClearManaData,
		// Token: 0x04002AF0 RID: 10992
		RemoveEntity,
		// Token: 0x04002AF1 RID: 10993
		NewRandomSeed,
		// Token: 0x04002AF2 RID: 10994
		SeparateCache,
		// Token: 0x04002AF3 RID: 10995
		ChangeCause
	}
}
