using System;
using UnityEngine;

// Token: 0x020002BB RID: 699
public class ApplyStatusNode : EffectNode
{
	// Token: 0x060019FB RID: 6651 RVA: 0x000A1E84 File Offset: 0x000A0084
	internal override void Apply(EffectProperties properties)
	{
		EntityControl applicationEntity = properties.GetApplicationEntity(this.ApplyTo);
		if (this.Status == null)
		{
			return;
		}
		if (applicationEntity == null || (applicationEntity.IsDead && !(this.Status.RootNode as StatusRootNode).PersistThroughDeath))
		{
			return;
		}
		if (!this.ShouldApply(properties, applicationEntity))
		{
			return;
		}
		if ((float)properties.RandomInt(0, 10000) >= this.Chance * 10000f)
		{
			return;
		}
		float num = this.Duration;
		if (this.DynamicDur != null)
		{
			num = (this.DynamicDur as NumberNode).Evaluate(properties);
		}
		if (num > 0f)
		{
			num = properties.ModifyAbilityPassives(Passive.AbilityValue.Status_Duration, num);
		}
		float num2 = (float)this.Stacks;
		if (this.DynamicStacks != null)
		{
			num2 = (this.DynamicStacks as NumberNode).Evaluate(properties);
		}
		if (this.Status.Root.Batched)
		{
			EntityNetworked net = applicationEntity.net;
			int hashCode = this.Status.HashCode;
			EntityControl sourceControl = properties.SourceControl;
			net.ApplyStatusBatched(hashCode, (sourceControl != null) ? sourceControl.view.ViewID : -1, (num > 0f) ? num : -2f, (int)num2, this.UseFrameDelay, properties.Depth + 1);
		}
		else
		{
			EntityNetworked net2 = applicationEntity.net;
			int hashCode2 = this.Status.HashCode;
			EntityControl sourceControl2 = properties.SourceControl;
			net2.ApplyStatus(hashCode2, (sourceControl2 != null) ? sourceControl2.view.ViewID : -1, (num > 0f) ? num : -2f, (int)num2, this.UseFrameDelay, properties.Depth + 1);
		}
		base.Completed();
	}

	// Token: 0x060019FC RID: 6652 RVA: 0x000A2018 File Offset: 0x000A0218
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo)
	{
		if (applyTo is PlayerControl && !this.Status.Root.AffectPlayer)
		{
			return false;
		}
		AIControl aicontrol = applyTo as AIControl;
		if (aicontrol != null)
		{
			if (!aicontrol.EnemyIsType(this.Status.Root.EffectsTypes) && this.Status.Root.EffectsTypes != EnemyType.Any)
			{
				return false;
			}
			if (!this.Status.Root.EnemyLevel.AnyFlagsMatch(aicontrol.Level) && this.Status.Root.EnemyLevel != EnemyLevel.All)
			{
				return false;
			}
		}
		return (props.IsLocal && !(applyTo is PlayerControl)) || applyTo == PlayerControl.myInstance;
	}

	// Token: 0x060019FD RID: 6653 RVA: 0x000A20CC File Offset: 0x000A02CC
	private void NewStatusGraph()
	{
		GraphTree editorTreeRef = base.EditorTreeRef;
		this.Status = (StatusTree.CreateAndOpenTree(((editorTreeRef != null) ? editorTreeRef.name : null) ?? "") as StatusTree);
	}

	// Token: 0x060019FE RID: 6654 RVA: 0x000A20F9 File Offset: 0x000A02F9
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Apply Status",
			MinInspectorSize = new Vector2(280f, 0f)
		};
	}

	// Token: 0x060019FF RID: 6655 RVA: 0x000A2120 File Offset: 0x000A0320
	public ApplyStatusNode()
	{
	}

	// Token: 0x04001A72 RID: 6770
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Duration", PortLocation.Default)]
	public Node DynamicDur;

	// Token: 0x04001A73 RID: 6771
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Dynamic Stacks", PortLocation.Default)]
	public Node DynamicStacks;

	// Token: 0x04001A74 RID: 6772
	public ApplyOn ApplyTo = ApplyOn.Affected;

	// Token: 0x04001A75 RID: 6773
	public StatusTree Status;

	// Token: 0x04001A76 RID: 6774
	[Range(0f, 1f)]
	public float Chance = 1f;

	// Token: 0x04001A77 RID: 6775
	public float Duration = 5f;

	// Token: 0x04001A78 RID: 6776
	public int Stacks = 1;

	// Token: 0x04001A79 RID: 6777
	public bool UseFrameDelay;
}
