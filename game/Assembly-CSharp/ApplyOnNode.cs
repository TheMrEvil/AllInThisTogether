using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002BA RID: 698
public class ApplyOnNode : EffectNode
{
	// Token: 0x060019F6 RID: 6646 RVA: 0x000A1A28 File Offset: 0x0009FC28
	internal override void Apply(EffectProperties properties)
	{
		EffectProperties effectProperties = properties.Copy(false);
		effectProperties.StartLoc = effectProperties.OutLoc.Copy();
		if (this.LocOverride != null)
		{
			effectProperties.StartLoc = (effectProperties.OutLoc = new global::Pose((this.LocOverride as LocationNode).GetLocation(properties), Location.WorldUp()));
		}
		List<EffectProperties> list = new List<EffectProperties>();
		foreach (EntityControl entityControl in EntityControl.AllEntities)
		{
			if ((!this.RequireTargetable || entityControl.Targetable) && (this.CheckAll || ((this.GameplayObjects || !(entityControl is AITrivialControl)) && effectProperties.CanInteractWith(entityControl, this.Affects) && entityControl.CanBeInteractedBy(effectProperties.SourceControl) && (!entityControl.IsDead || this.IncludeDead) && !(entityControl == null))) && (!this.ExcludeSelf || !(entityControl == effectProperties.SourceControl)) && (!this.ExcludeAffected || !(entityControl == effectProperties.AffectedControl)))
			{
				EffectProperties effectProperties2 = effectProperties.Copy(false);
				effectProperties2.Affected = entityControl.gameObject;
				list.Add(effectProperties2);
			}
		}
		foreach (Node node in this.Filters)
		{
			((LogicFilterNode)node).Filter(ref list, effectProperties);
		}
		Vector3 origin = effectProperties.GetOrigin();
		int num = 0;
		int num2 = (this.MaxAffected > 0) ? Mathf.Min(this.MaxAffected, list.Count) : list.Count;
		effectProperties.SetExtra(EProp.AoE_EnteredCount, (float)num2);
		int num3 = 0;
		foreach (EffectProperties effectProperties3 in list)
		{
			if (this.MaxAffected > 0 && num >= this.MaxAffected)
			{
				break;
			}
			num3++;
			bool flag = this.Effects.Count > 1;
			foreach (Node node2 in this.Effects)
			{
				EffectNode effectNode = (EffectNode)node2;
				EffectProperties effectProperties4 = flag ? effectProperties.Copy(false) : effectProperties3;
				if (this.UpdateOutputPoint)
				{
					Vector3 position = effectProperties3.AffectedControl.display.CenterOfMass.position;
					effectProperties4.OutLoc = global::Pose.WorldPoint(position, (position - origin).normalized);
				}
				effectProperties4.Affected = effectProperties3.AffectedControl.gameObject;
				effectProperties4.SetExtra(EProp.Node_Output, (float)num3);
				effectNode.Apply(effectProperties4);
			}
			num++;
		}
	}

	// Token: 0x060019F7 RID: 6647 RVA: 0x000A1D6C File Offset: 0x0009FF6C
	public override void TryCancel(EffectProperties props)
	{
		EffectProperties props2 = props.Copy(false);
		foreach (Node node in this.Effects)
		{
			((EffectNode)node).TryCancel(props2);
		}
	}

	// Token: 0x060019F8 RID: 6648 RVA: 0x000A1DCC File Offset: 0x0009FFCC
	private bool IsMultiTarget()
	{
		switch (this.Affects)
		{
		case EffectInteractsWith.Enemies:
			return true;
		case EffectInteractsWith.Allies:
			return true;
		case EffectInteractsWith.AllEntities:
			return true;
		case EffectInteractsWith.Environment:
			return true;
		case EffectInteractsWith.Self:
			return false;
		case EffectInteractsWith.LocalPlayer:
			return false;
		case EffectInteractsWith.Anything:
			return true;
		case EffectInteractsWith.Target:
			return false;
		case EffectInteractsWith.Players:
			return true;
		}
		return false;
	}

	// Token: 0x060019F9 RID: 6649 RVA: 0x000A1E37 File Offset: 0x000A0037
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Apply Effects",
			SortX = true,
			MinInspectorSize = new Vector2(280f, 0f)
		};
	}

	// Token: 0x060019FA RID: 6650 RVA: 0x000A1E65 File Offset: 0x000A0065
	public ApplyOnNode()
	{
	}

	// Token: 0x04001A66 RID: 6758
	public EffectInteractsWith Affects;

	// Token: 0x04001A67 RID: 6759
	public bool ExcludeSelf;

	// Token: 0x04001A68 RID: 6760
	public bool ExcludeAffected;

	// Token: 0x04001A69 RID: 6761
	public bool CheckAll;

	// Token: 0x04001A6A RID: 6762
	public bool IncludeDead;

	// Token: 0x04001A6B RID: 6763
	public bool GameplayObjects;

	// Token: 0x04001A6C RID: 6764
	public bool RequireTargetable;

	// Token: 0x04001A6D RID: 6765
	public int MaxAffected;

	// Token: 0x04001A6E RID: 6766
	public bool UpdateOutputPoint;

	// Token: 0x04001A6F RID: 6767
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(LogicFilterNode), true, "Filters", PortLocation.Vertical)]
	public List<Node> Filters = new List<Node>();

	// Token: 0x04001A70 RID: 6768
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Location Override", PortLocation.Default)]
	public Node LocOverride;

	// Token: 0x04001A71 RID: 6769
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "Effects", PortLocation.Default)]
	public List<Node> Effects = new List<Node>();
}
