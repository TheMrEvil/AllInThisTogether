using System;
using UnityEngine;

// Token: 0x020002B7 RID: 695
public class ApplyForceNode : EffectNode
{
	// Token: 0x1700018F RID: 399
	// (get) Token: 0x060019EC RID: 6636 RVA: 0x000A16A6 File Offset: 0x0009F8A6
	internal override bool CanSkipClone
	{
		get
		{
			return true;
		}
	}

	// Token: 0x060019ED RID: 6637 RVA: 0x000A16AC File Offset: 0x0009F8AC
	internal override void Apply(EffectProperties properties)
	{
		if (properties.AffectedControl == null && this.ForceTarget == ApplyOn.Affected)
		{
			return;
		}
		if (properties.SourceControl == null && this.ForceTarget == ApplyOn.Source)
		{
			return;
		}
		EffectProperties effectProperties = properties.Copy(false);
		EntityControl entityControl = (this.ForceTarget == ApplyOn.Source) ? effectProperties.SourceControl : effectProperties.AffectedControl;
		Location location = effectProperties.StartLoc.GetLocation();
		if (this.OverrideOrigin)
		{
			location = Location.AtWorldPoint(entityControl.display.CenterOfMass.position);
		}
		if (this.Dir != null && location != null)
		{
			LocationNode locationNode = this.Dir as LocationNode;
			if (locationNode != null)
			{
				Location location2 = locationNode.GetLocation(properties);
				effectProperties.StartLoc = new global::Pose(location, location2);
			}
		}
		bool flag = this.ShouldApply(effectProperties, entityControl);
		effectProperties.SourceLocation = null;
		effectProperties.Affected = entityControl.gameObject;
		if (!flag)
		{
			return;
		}
		Vector3 normalized = effectProperties.GetOriginForward().normalized;
		if (!normalized.IsValid() || normalized.magnitude == 0f)
		{
			return;
		}
		entityControl.movement.AddForce(this, normalized.normalized, effectProperties);
		base.Completed();
	}

	// Token: 0x060019EE RID: 6638 RVA: 0x000A17CC File Offset: 0x0009F9CC
	public float GetForceValue(EffectProperties properties)
	{
		float num = Mathf.Abs(this.ForceValue);
		if (this.Value != null)
		{
			num = (this.Value as NumberNode).Evaluate(properties);
		}
		num = Mathf.Max(properties.ModifyAbilityPassives(Passive.AbilityValue.ForceApplied, num), 0f);
		if (properties.AffectedControl.HasPassiveMod(new EntityPassive(Passive.EntityValue.ForceTaken), true))
		{
			num = Mathf.Max(properties.AffectedControl.GetPassiveMod(Passive.EntityValue.ForceTaken, properties, num), 0f);
		}
		return num * Mathf.Sign(this.ForceValue);
	}

	// Token: 0x060019EF RID: 6639 RVA: 0x000A185F File Offset: 0x0009FA5F
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo)
	{
		return applyTo.IsMine;
	}

	// Token: 0x060019F0 RID: 6640 RVA: 0x000A1867 File Offset: 0x0009FA67
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Apply Force",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x060019F1 RID: 6641 RVA: 0x000A188E File Offset: 0x0009FA8E
	public ApplyForceNode()
	{
	}

	// Token: 0x04001A50 RID: 6736
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), true, "Force Toward", PortLocation.Default)]
	public Node Dir;

	// Token: 0x04001A51 RID: 6737
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), true, "Scaling Value", PortLocation.Default)]
	public Node Value;

	// Token: 0x04001A52 RID: 6738
	public ApplyOn ForceTarget = ApplyOn.Affected;

	// Token: 0x04001A53 RID: 6739
	public float ForceValue = 10f;

	// Token: 0x04001A54 RID: 6740
	public float VerticalAdd;

	// Token: 0x04001A55 RID: 6741
	public float InAirMultiplier = 1f;

	// Token: 0x04001A56 RID: 6742
	public bool OverrideOrigin;

	// Token: 0x04001A57 RID: 6743
	public ForceMode ForceMode;
}
