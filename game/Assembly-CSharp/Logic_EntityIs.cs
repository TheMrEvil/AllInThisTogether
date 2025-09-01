using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000280 RID: 640
public class Logic_EntityIs : LogicNode
{
	// Token: 0x060018F2 RID: 6386 RVA: 0x0009B9BC File Offset: 0x00099BBC
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
		bool isResult = this.GetIsResult(applicationEntity, props);
		this.EditorStateDisplay = (isResult ? NodeState.Success : NodeState.Fail);
		return isResult;
	}

	// Token: 0x060018F3 RID: 6387 RVA: 0x0009BA10 File Offset: 0x00099C10
	private bool GetIsResult(EntityControl entity, EffectProperties props)
	{
		EntityControl entityControl = null;
		if (this.NeedSecondEntity())
		{
			entityControl = props.GetApplicationEntity(this.EntityCompare);
		}
		switch (this.Test)
		{
		case Logic_EntityIs.EntityIsTest.Exists:
			return true;
		case Logic_EntityIs.EntityIsTest.Alive:
			return !entity.IsDead;
		case Logic_EntityIs.EntityIsTest.InDanger:
			using (List<AreaOfEffect>.Enumerator enumerator = AreaOfEffect.AllAreas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AreaOfEffect areaOfEffect = enumerator.Current;
					if (areaOfEffect.IsNegative && areaOfEffect.HasEntityInside(entity))
					{
						return true;
					}
				}
				return false;
			}
			break;
		case Logic_EntityIs.EntityIsTest.Moving:
			return entity.movement.IsMoving();
		case Logic_EntityIs.EntityIsTest.ShieldCharging:
			break;
		case Logic_EntityIs.EntityIsTest.ShieldOvercharged:
			return entity.health.HasOvershield;
		case Logic_EntityIs.EntityIsTest.AbilityOnCooldown:
		{
			PlayerControl playerControl = entity as PlayerControl;
			return playerControl != null && playerControl.actions.IsOnCooldown(this.AbilityT);
		}
		case Logic_EntityIs.EntityIsTest.EnemyOf:
			return !(entityControl == null) && !(entityControl == entity) && entityControl.TeamID != entity.TeamID;
		case Logic_EntityIs.EntityIsTest.TargetOf:
			return entityControl != null && entityControl.currentTarget == entity;
		case Logic_EntityIs.EntityIsTest.AbleToSeeLOS:
			if (!(entityControl == null) && !(entityControl == entity))
			{
				AIControl aicontrol = entity as AIControl;
				if (aicontrol != null)
				{
					return aicontrol.CanSeeEntity(entityControl);
				}
			}
			return false;
		case Logic_EntityIs.EntityIsTest.LocalPlayer:
			return entity == PlayerControl.myInstance;
		case Logic_EntityIs.EntityIsTest.EnemyType:
		{
			AIControl aicontrol2 = entity as AIControl;
			return aicontrol2 != null && (this.EType == EnemyType.Any || aicontrol2.EnemyIsType(this.EType)) && (this.Level == EnemyLevel.All || (this.Level.HasFlagUnsafe(aicontrol2.Level) && aicontrol2.Level != EnemyLevel.None));
		}
		case Logic_EntityIs.EntityIsTest.OnTeam:
			if (this.Team == Logic_EntityIs.TeamType.Players)
			{
				return entity.TeamID == 1;
			}
			return entity.TeamID == 2;
		case Logic_EntityIs.EntityIsTest.InFrontOf:
			return !(entityControl == null) && !(entityControl == entity) && Logic_EntityIs.InFrontOf(entity, entityControl);
		case Logic_EntityIs.EntityIsTest.PetOf:
			if (!(entityControl == null) && !(entityControl == entity))
			{
				AIControl aicontrol3 = entity as AIControl;
				if (aicontrol3 != null)
				{
					return aicontrol3.PetOwnerID == entityControl.ViewID;
				}
			}
			return false;
		case Logic_EntityIs.EntityIsTest.CastingAbility:
			return entity.IsUsingActiveAbility();
		case Logic_EntityIs.EntityIsTest.LocallyOwned:
			return entity.IsMine;
		case Logic_EntityIs.EntityIsTest.InForcedMove:
			return entity.movement.IsOnForcedMove;
		case Logic_EntityIs.EntityIsTest.SameAs:
			return entityControl == entity;
		case Logic_EntityIs.EntityIsTest.IsUsingPlrAbility:
		{
			PlayerControl playerControl2 = entity as PlayerControl;
			if (playerControl2 == null)
			{
				return false;
			}
			Ability ability = playerControl2.actions.GetAbility(this.AbilityT);
			return ability != null && ability.IsActive(true);
		}
		case Logic_EntityIs.EntityIsTest.ScribeColor:
		{
			PlayerControl playerControl3 = entity as PlayerControl;
			return playerControl3 != null && (this.magicColor == MagicColor.Any || playerControl3.SignatureColor == this.magicColor);
		}
		case Logic_EntityIs.EntityIsTest.PlayingAnim:
			return entity.display.IsPlayingAbilityAnim(this.ID);
		case Logic_EntityIs.EntityIsTest.Targetable:
			return entity.Targetable;
		case Logic_EntityIs.EntityIsTest.Affectable:
			return entity.Affectable;
		default:
			return false;
		}
		return entity.GetStatTrigger(EStat.ShieldCharging);
	}

	// Token: 0x060018F4 RID: 6388 RVA: 0x0009BD14 File Offset: 0x00099F14
	private bool NeedAbilityScope()
	{
		return this.Test == Logic_EntityIs.EntityIsTest.AbilityOnCooldown || this.Test == Logic_EntityIs.EntityIsTest.IsUsingPlrAbility;
	}

	// Token: 0x060018F5 RID: 6389 RVA: 0x0009BD2C File Offset: 0x00099F2C
	private bool NeedSecondEntity()
	{
		Logic_EntityIs.EntityIsTest test = this.Test;
		switch (test)
		{
		case Logic_EntityIs.EntityIsTest.EnemyOf:
			return true;
		case Logic_EntityIs.EntityIsTest.TargetOf:
			return true;
		case Logic_EntityIs.EntityIsTest.AbleToSeeLOS:
			return true;
		case Logic_EntityIs.EntityIsTest.LocalPlayer:
		case Logic_EntityIs.EntityIsTest.EnemyType:
		case Logic_EntityIs.EntityIsTest.OnTeam:
			break;
		case Logic_EntityIs.EntityIsTest.InFrontOf:
			return true;
		case Logic_EntityIs.EntityIsTest.PetOf:
			return true;
		default:
			if (test == Logic_EntityIs.EntityIsTest.SameAs)
			{
				return true;
			}
			break;
		}
		return false;
	}

	// Token: 0x060018F6 RID: 6390 RVA: 0x0009BD8C File Offset: 0x00099F8C
	public static bool InFrontOf(EntityControl target, EntityControl ofEntity)
	{
		Vector3 position = target.movement.GetPosition();
		Vector3 position2 = ofEntity.movement.GetPosition();
		position.y = 0f;
		position2.y = 0f;
		Vector3 normalized = (position - position2).normalized;
		return Vector3.Dot(ofEntity.movement.GetForward(), normalized) > 0.3f;
	}

	// Token: 0x060018F7 RID: 6391 RVA: 0x0009BDF1 File Offset: 0x00099FF1
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Entity Is";
		inspectorProps.MinInspectorSize = new Vector2(160f, 0f);
		inspectorProps.MaxInspectorSize = new Vector2(160f, 0f);
		return inspectorProps;
	}

	// Token: 0x060018F8 RID: 6392 RVA: 0x0009BE2E File Offset: 0x0009A02E
	public Logic_EntityIs()
	{
	}

	// Token: 0x04001908 RID: 6408
	public ApplyOn Entity;

	// Token: 0x04001909 RID: 6409
	[Space(-10f)]
	public Logic_EntityIs.EntityIsTest Test;

	// Token: 0x0400190A RID: 6410
	public PlayerAbilityType AbilityT;

	// Token: 0x0400190B RID: 6411
	public ApplyOn EntityCompare;

	// Token: 0x0400190C RID: 6412
	public EnemyLevel Level;

	// Token: 0x0400190D RID: 6413
	public EnemyType EType;

	// Token: 0x0400190E RID: 6414
	public Logic_EntityIs.TeamType Team;

	// Token: 0x0400190F RID: 6415
	public MagicColor magicColor;

	// Token: 0x04001910 RID: 6416
	public string ID;

	// Token: 0x02000632 RID: 1586
	public enum EntityIsTest
	{
		// Token: 0x04002A47 RID: 10823
		Exists,
		// Token: 0x04002A48 RID: 10824
		Alive,
		// Token: 0x04002A49 RID: 10825
		InDanger,
		// Token: 0x04002A4A RID: 10826
		Moving,
		// Token: 0x04002A4B RID: 10827
		ShieldCharging,
		// Token: 0x04002A4C RID: 10828
		ShieldOvercharged,
		// Token: 0x04002A4D RID: 10829
		AbilityOnCooldown,
		// Token: 0x04002A4E RID: 10830
		EnemyOf,
		// Token: 0x04002A4F RID: 10831
		TargetOf,
		// Token: 0x04002A50 RID: 10832
		AbleToSeeLOS,
		// Token: 0x04002A51 RID: 10833
		LocalPlayer,
		// Token: 0x04002A52 RID: 10834
		EnemyType,
		// Token: 0x04002A53 RID: 10835
		OnTeam,
		// Token: 0x04002A54 RID: 10836
		InFrontOf,
		// Token: 0x04002A55 RID: 10837
		PetOf,
		// Token: 0x04002A56 RID: 10838
		CastingAbility,
		// Token: 0x04002A57 RID: 10839
		LocallyOwned,
		// Token: 0x04002A58 RID: 10840
		InForcedMove,
		// Token: 0x04002A59 RID: 10841
		SameAs,
		// Token: 0x04002A5A RID: 10842
		IsUsingPlrAbility,
		// Token: 0x04002A5B RID: 10843
		ScribeColor,
		// Token: 0x04002A5C RID: 10844
		PlayingAnim,
		// Token: 0x04002A5D RID: 10845
		Targetable,
		// Token: 0x04002A5E RID: 10846
		Affectable
	}

	// Token: 0x02000633 RID: 1587
	public enum TeamType
	{
		// Token: 0x04002A60 RID: 10848
		Players,
		// Token: 0x04002A61 RID: 10849
		AIEnemy
	}
}
