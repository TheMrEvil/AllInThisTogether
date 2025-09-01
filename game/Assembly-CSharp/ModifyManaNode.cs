using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002CE RID: 718
public class ModifyManaNode : EffectNode
{
	// Token: 0x06001A4D RID: 6733 RVA: 0x000A36E4 File Offset: 0x000A18E4
	internal override void Apply(EffectProperties properties)
	{
		EntityControl applicationEntity = properties.GetApplicationEntity(this.ApplyTo);
		if (applicationEntity == null)
		{
			return;
		}
		PlayerControl playerControl = applicationEntity as PlayerControl;
		if (playerControl == null)
		{
			return;
		}
		bool flag = this.ShouldApply(properties, applicationEntity);
		float num = this.Amount;
		if (this.Value != null)
		{
			num = (this.Value as NumberNode).Evaluate(properties);
		}
		if (this.Type == ModifyManaNode.Modification.Consume)
		{
			PlayerAbilityType playerAbilityType = PlayerAbilityType.Any;
			AbilityRootNode abilityRootNode = this.RootNode as AbilityRootNode;
			if (abilityRootNode != null)
			{
				playerAbilityType = abilityRootNode.PlrAbilityType;
			}
			num = applicationEntity.ModifyManaCost(properties, num);
			if (this.MinimumMana > num)
			{
				num = this.MinimumMana;
			}
			if (num < 1f && num > 0f)
			{
				num = (float)((properties.RandomFloat(0f, 1f) > num) ? 0 : 1);
			}
			if (!playerControl.Mana.CanUseMana(num))
			{
				foreach (Node node in this.OnFail)
				{
					AbilityNode abilityNode = (AbilityNode)node;
					EffectNode effectNode = abilityNode as EffectNode;
					if (effectNode != null)
					{
						effectNode.Invoke(properties.Copy(false));
					}
					else
					{
						abilityNode.DoUpdate(properties);
					}
				}
				if (playerControl.IsMine)
				{
					Crosshair.instance.CastFailed(playerAbilityType, CastFailedReason.Mana, (int)num);
					goto IL_4A7;
				}
				goto IL_4A7;
			}
			else
			{
				if (flag)
				{
					Dictionary<MagicColor, int> dictionary = playerControl.Mana.ConsumeMana((float)((int)num), properties.SourceControl == applicationEntity);
					foreach (KeyValuePair<MagicColor, int> keyValuePair in dictionary)
					{
						properties.SetExtra(EProp.Node_Output, (float)keyValuePair.Key);
						properties.AddMana(keyValuePair.Key, keyValuePair.Value);
					}
					playerControl.actions.RefreshMana(properties.AbilityType, dictionary);
					if (playerControl.IsMine && playerAbilityType != PlayerAbilityType.Any)
					{
						Crosshair.instance.AbilityManaUsed(playerAbilityType, dictionary);
					}
				}
				using (List<Node>.Enumerator enumerator = this.OnSucceed.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Node node2 = enumerator.Current;
						AbilityNode abilityNode2 = (AbilityNode)node2;
						EffectNode effectNode2 = abilityNode2 as EffectNode;
						if (effectNode2 != null)
						{
							effectNode2.Invoke(properties.Copy(false));
						}
						else
						{
							abilityNode2.DoUpdate(properties);
						}
					}
					goto IL_4A7;
				}
			}
		}
		if (this.Type == ModifyManaNode.Modification.Regen)
		{
			if (flag)
			{
				num = properties.ModifyAbilityPassives(Passive.AbilityValue.ManaGenerated, num);
				playerControl.Mana.Recharge(num);
			}
			using (List<Node>.Enumerator enumerator = this.OnSucceed.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Node node3 = enumerator.Current;
					AbilityNode abilityNode3 = (AbilityNode)node3;
					EffectNode effectNode3 = abilityNode3 as EffectNode;
					if (effectNode3 != null)
					{
						effectNode3.Invoke(properties.Copy(false));
					}
					else
					{
						abilityNode3.DoUpdate(properties);
					}
				}
				goto IL_4A7;
			}
		}
		if (this.Type == ModifyManaNode.Modification.Change)
		{
			if (!flag)
			{
				return;
			}
			playerControl.Mana.ChangeManaElement(this.NeutralOnly, this.FromFront, this.magicColor);
		}
		else if (this.Type == ModifyManaNode.Modification.ChangeTemp)
		{
			if (!flag)
			{
				return;
			}
			playerControl.Mana.ChangeManaElementTemp(this.NeutralOnly, this.magicColor, (int)this.Amount);
		}
		else if (this.Type == ModifyManaNode.Modification.RemovePerm)
		{
			if (!flag)
			{
				return;
			}
			playerControl.Mana.RemoveMana(this.magicColor, this.FromFront);
		}
		else if (this.Type == ModifyManaNode.Modification.RemoveTemp)
		{
			if (!flag)
			{
				return;
			}
			playerControl.Mana.RemoveTempMana(this.magicColor);
		}
		else if (this.Type == ModifyManaNode.Modification.ColorizeNeutral)
		{
			if (!flag)
			{
				return;
			}
			playerControl.Mana.ColorizeNeutral((int)this.Amount, this.CoreOnly, this.ExplicitColor ? this.magicColor : PlayerControl.myInstance.SignatureColor);
		}
		else
		{
			if (flag)
			{
				int num2 = 0;
				while ((float)num2 < num)
				{
					playerControl.Mana.AddMana(this.magicColor, this.Type == ModifyManaNode.Modification.GainTemp, false);
					num2++;
				}
			}
			if (this.Type == ModifyManaNode.Modification.GainTemp)
			{
				EffectProperties effectProperties = properties.Copy(false);
				effectProperties.Affected = playerControl.gameObject;
				effectProperties.ClearManaData();
				effectProperties.AddMana(this.magicColor, Mathf.FloorToInt(num));
				effectProperties.SetExtra(EProp.Snip_Input, num);
				PlayerControl playerControl2 = properties.SourceControl as PlayerControl;
				if (playerControl2 != null)
				{
					playerControl2.Mana.ManaGenerated(effectProperties);
				}
			}
			foreach (Node node4 in this.OnSucceed)
			{
				AbilityNode abilityNode4 = (AbilityNode)node4;
				EffectNode effectNode4 = abilityNode4 as EffectNode;
				if (effectNode4 != null)
				{
					effectNode4.Invoke(properties.Copy(false));
				}
				else
				{
					abilityNode4.DoUpdate(properties);
				}
			}
		}
		IL_4A7:
		base.Completed();
	}

	// Token: 0x06001A4E RID: 6734 RVA: 0x000A3BE0 File Offset: 0x000A1DE0
	internal override void OnCancel(EffectProperties props)
	{
		foreach (Node node in this.OnSucceed)
		{
			((AbilityNode)node).Cancel(props);
		}
		foreach (Node node2 in this.OnFail)
		{
			((AbilityNode)node2).Cancel(props);
		}
	}

	// Token: 0x06001A4F RID: 6735 RVA: 0x000A3C7C File Offset: 0x000A1E7C
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo)
	{
		return applyTo == PlayerControl.myInstance;
	}

	// Token: 0x06001A50 RID: 6736 RVA: 0x000A3C89 File Offset: 0x000A1E89
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Modify Mana",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001A51 RID: 6737 RVA: 0x000A3CB0 File Offset: 0x000A1EB0
	public ModifyManaNode()
	{
	}

	// Token: 0x04001AB6 RID: 6838
	public float Amount;

	// Token: 0x04001AB7 RID: 6839
	public ModifyManaNode.Modification Type = ModifyManaNode.Modification.Consume;

	// Token: 0x04001AB8 RID: 6840
	public bool CoreOnly;

	// Token: 0x04001AB9 RID: 6841
	public bool ExplicitColor;

	// Token: 0x04001ABA RID: 6842
	public MagicColor magicColor = MagicColor.Neutral;

	// Token: 0x04001ABB RID: 6843
	public bool NeutralOnly;

	// Token: 0x04001ABC RID: 6844
	public bool FromFront;

	// Token: 0x04001ABD RID: 6845
	public float MinimumMana;

	// Token: 0x04001ABE RID: 6846
	public ApplyOn ApplyTo;

	// Token: 0x04001ABF RID: 6847
	[HideInInspector]
	[OutputPort(typeof(AbilityNode), true, "On Success", PortLocation.Default)]
	public List<Node> OnSucceed = new List<Node>();

	// Token: 0x04001AC0 RID: 6848
	[HideInInspector]
	[OutputPort(typeof(AbilityNode), true, "On Fail", PortLocation.Default)]
	public List<Node> OnFail = new List<Node>();

	// Token: 0x04001AC1 RID: 6849
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), true, "Scaling Value", PortLocation.Default)]
	public Node Value;

	// Token: 0x0200064C RID: 1612
	public enum Modification
	{
		// Token: 0x04002AE0 RID: 10976
		GainTemp,
		// Token: 0x04002AE1 RID: 10977
		GainPerm,
		// Token: 0x04002AE2 RID: 10978
		Consume,
		// Token: 0x04002AE3 RID: 10979
		Change,
		// Token: 0x04002AE4 RID: 10980
		RemovePerm,
		// Token: 0x04002AE5 RID: 10981
		Regen,
		// Token: 0x04002AE6 RID: 10982
		RemoveTemp,
		// Token: 0x04002AE7 RID: 10983
		ChangeTemp,
		// Token: 0x04002AE8 RID: 10984
		ColorizeNeutral
	}
}
