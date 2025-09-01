using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000275 RID: 629
public class Logic_EntityHas : LogicNode
{
	// Token: 0x060018E5 RID: 6373 RVA: 0x0009B5C8 File Offset: 0x000997C8
	public override bool Evaluate(EffectProperties props)
	{
		if (props == null)
		{
			this.EditorStateDisplay = NodeState.Fail;
			return false;
		}
		EntityControl applicationEntity = props.GetApplicationEntity(this.Entity);
		if (applicationEntity == null)
		{
			this.EditorStateDisplay = NodeState.Fail;
			return false;
		}
		bool hasResult = this.GetHasResult(applicationEntity, props);
		this.EditorStateDisplay = (hasResult ? NodeState.Success : NodeState.Fail);
		return hasResult;
	}

	// Token: 0x060018E6 RID: 6374 RVA: 0x0009B61C File Offset: 0x0009981C
	private bool GetHasResult(EntityControl entity, EffectProperties props)
	{
		switch (this.Test)
		{
		case Logic_EntityHas.EntityHasTest.Target:
			return entity.currentTarget != null && (this.AllowDead || !entity.currentTarget.IsDead);
		case Logic_EntityHas.EntityHasTest.ManaElement:
		{
			PlayerControl playerControl = entity as PlayerControl;
			return playerControl != null && playerControl.Mana.HasManaElement(this.manaMagicColor);
		}
		case Logic_EntityHas.EntityHasTest.Tag:
		{
			AIControl aicontrol = entity as AIControl;
			return aicontrol != null && aicontrol.HasTag(this.TagID);
		}
		case Logic_EntityHas.EntityHasTest.EverHadTarget:
		{
			AIControl aicontrol2 = entity as AIControl;
			return aicontrol2 != null && aicontrol2.HasTag("HadTarget");
		}
		case Logic_EntityHas.EntityHasTest.AugmentType:
			return this.DoAugmentTest(entity);
		case Logic_EntityHas.EntityHasTest.StatusKeyword:
			return entity.HasStatusKeyword(this.StatusKey);
		default:
			return false;
		}
	}

	// Token: 0x060018E7 RID: 6375 RVA: 0x0009B6E4 File Offset: 0x000998E4
	private bool DoAugmentTest(EntityControl entity)
	{
		bool result;
		switch (this.AugmentT)
		{
		case Logic_EntityHas.AugmentFlagTest.ModTag:
			result = entity.HasModTag(this.ModTag);
			break;
		case Logic_EntityHas.AugmentFlagTest.EntityPassive:
			result = entity.Augment.HasPassive(this.EPassive);
			break;
		case Logic_EntityHas.AugmentFlagTest.AbilityPassive:
			result = entity.Augment.HasPassive(new ValueTuple<PlayerAbilityType, Passive.AbilityValue>(this.AbilityT, this.APassive));
			break;
		default:
			result = false;
			break;
		}
		return result;
	}

	// Token: 0x060018E8 RID: 6376 RVA: 0x0009B75B File Offset: 0x0009995B
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Entity Has";
		inspectorProps.MinInspectorSize = new Vector2(160f, 0f);
		inspectorProps.MaxInspectorSize = new Vector2(160f, 0f);
		return inspectorProps;
	}

	// Token: 0x060018E9 RID: 6377 RVA: 0x0009B798 File Offset: 0x00099998
	public override Node Clone(Dictionary<string, Node> alreadyCloned = null, bool fullClone = false)
	{
		Logic_EntityHas logic_EntityHas = base.Clone(alreadyCloned, fullClone) as Logic_EntityHas;
		logic_EntityHas.ModTag = this.ModTag.Copy();
		return logic_EntityHas;
	}

	// Token: 0x060018EA RID: 6378 RVA: 0x0009B7B8 File Offset: 0x000999B8
	public override void OnCloned()
	{
		this.ModTag = this.ModTag.Copy();
	}

	// Token: 0x060018EB RID: 6379 RVA: 0x0009B7CB File Offset: 0x000999CB
	public Logic_EntityHas()
	{
	}

	// Token: 0x040018C7 RID: 6343
	public ApplyOn Entity;

	// Token: 0x040018C8 RID: 6344
	[Space(-10f)]
	public Logic_EntityHas.EntityHasTest Test;

	// Token: 0x040018C9 RID: 6345
	public string TagID;

	// Token: 0x040018CA RID: 6346
	public bool AllowDead;

	// Token: 0x040018CB RID: 6347
	public Logic_EntityHas.AugmentFlagTest AugmentT;

	// Token: 0x040018CC RID: 6348
	[Space(10f)]
	public ModTag ModTag;

	// Token: 0x040018CD RID: 6349
	[Space(10f)]
	public Passive.EntityValue EPassive;

	// Token: 0x040018CE RID: 6350
	[Space(10f)]
	public PlayerAbilityType AbilityT;

	// Token: 0x040018CF RID: 6351
	public Passive.AbilityValue APassive;

	// Token: 0x040018D0 RID: 6352
	public StatusKeyword StatusKey;

	// Token: 0x040018D1 RID: 6353
	public MagicColor manaMagicColor;

	// Token: 0x02000630 RID: 1584
	public enum EntityHasTest
	{
		// Token: 0x04002A3C RID: 10812
		Target,
		// Token: 0x04002A3D RID: 10813
		ManaElement,
		// Token: 0x04002A3E RID: 10814
		Tag,
		// Token: 0x04002A3F RID: 10815
		EverHadTarget,
		// Token: 0x04002A40 RID: 10816
		AugmentType,
		// Token: 0x04002A41 RID: 10817
		StatusKeyword
	}

	// Token: 0x02000631 RID: 1585
	public enum AugmentFlagTest
	{
		// Token: 0x04002A43 RID: 10819
		ModTag,
		// Token: 0x04002A44 RID: 10820
		EntityPassive,
		// Token: 0x04002A45 RID: 10821
		AbilityPassive
	}
}
