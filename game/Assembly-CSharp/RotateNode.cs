using System;
using UnityEngine;

// Token: 0x020002DB RID: 731
public class RotateNode : EffectNode
{
	// Token: 0x06001A84 RID: 6788 RVA: 0x000A4A10 File Offset: 0x000A2C10
	internal override void Apply(EffectProperties properties)
	{
		if (properties.SourceControl == null)
		{
			return;
		}
		EntityControl sourceControl = properties.SourceControl;
		Vector3 forward = sourceControl.movement.GetForward();
		float num = this.RotationSpeed;
		if (this.Value != null)
		{
			NumberNode numberNode = this.Value as NumberNode;
			if (numberNode != null)
			{
				num = numberNode.Evaluate(properties);
			}
		}
		float num2 = 1f;
		if (Mathf.Abs(num) > 180f)
		{
			num /= 10f;
			num2 = 10f;
		}
		Vector3 vector;
		if (this.LocOverride != null)
		{
			LocationNode locationNode = this.LocOverride as LocationNode;
			if (locationNode != null)
			{
				vector = (locationNode.GetPoint(properties) - sourceControl.movement.GetPosition()).normalized;
				goto IL_15E;
			}
		}
		if (this.TowardsTarget)
		{
			if (properties.SeekTarget == null)
			{
				return;
			}
			vector = (((properties.SeekTargetControl == null) ? properties.SeekTarget.transform.position : properties.SeekTargetControl.display.CenterOfMass.position) + properties.SeekTargetControl.movement.GetVelocity() * this.VelocityOffset - sourceControl.movement.GetPosition()).normalized;
		}
		else
		{
			vector = Quaternion.Euler(0f, num, 0f) * forward;
		}
		IL_15E:
		if (this.OnPlane)
		{
			vector = Vector3.ProjectOnPlane(vector, Vector3.up);
		}
		if (this.OverTime)
		{
			num *= GameplayManager.deltaTime;
		}
		vector = Vector3.RotateTowards(forward, vector, num2 * Mathf.Abs(num) * 0.017453292f, 1f);
		Debug.DrawLine(sourceControl.display.CenterOfMass.position, sourceControl.display.CenterOfMass.position + forward * 5f, Color.red);
		Debug.DrawLine(sourceControl.display.CenterOfMass.position, sourceControl.display.CenterOfMass.position + vector * 5f, Color.green);
		sourceControl.movement.SetForward(vector, true);
		base.Completed();
	}

	// Token: 0x06001A85 RID: 6789 RVA: 0x000A4C42 File Offset: 0x000A2E42
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Rotate Caster",
			MinInspectorSize = new Vector2(120f, 0f)
		};
	}

	// Token: 0x06001A86 RID: 6790 RVA: 0x000A4C69 File Offset: 0x000A2E69
	public RotateNode()
	{
	}

	// Token: 0x04001AF9 RID: 6905
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Speed", PortLocation.Header)]
	public Node Value;

	// Token: 0x04001AFA RID: 6906
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Target", PortLocation.Header)]
	public Node LocOverride;

	// Token: 0x04001AFB RID: 6907
	public bool OnPlane = true;

	// Token: 0x04001AFC RID: 6908
	public bool OverTime = true;

	// Token: 0x04001AFD RID: 6909
	public bool TowardsTarget;

	// Token: 0x04001AFE RID: 6910
	[Range(0f, 2f)]
	public float VelocityOffset;

	// Token: 0x04001AFF RID: 6911
	public float RotationSpeed;
}
