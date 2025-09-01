using System;
using UnityEngine;

// Token: 0x020002E6 RID: 742
public class SubActionNode : EffectNode
{
	// Token: 0x06001AB1 RID: 6833 RVA: 0x000A60D4 File Offset: 0x000A42D4
	internal override void Apply(EffectProperties properties)
	{
		if (this.Action == null)
		{
			return;
		}
		float num = properties.GetFloat("sa_inv_c");
		if (float.IsNaN(num))
		{
			num = 0f;
		}
		properties.SaveFloat("sa_inv_c", num + 1f);
		EffectProperties effectProperties = properties.Copy(this.SeparateCache);
		effectProperties.OverrideSeed(effectProperties.RandSeed + this.guid.GetHashCode(), (int)properties.GetFloat("sa_inv_c"));
		if (this.Loc != null)
		{
			effectProperties.StartLoc = (effectProperties.OutLoc = (this.Loc as PoseNode).GetPose(properties));
		}
		if (this.InputValue != null)
		{
			effectProperties.SetExtra(EProp.DynamicInput, (this.InputValue as NumberNode).Evaluate(properties));
		}
		if (this.RootNode is AbilityNode)
		{
			effectProperties.SetExtra(EProp.Override_Depth, 0f);
			effectProperties.Depth = 0;
		}
		this.Action.Root.Apply(effectProperties);
	}

	// Token: 0x06001AB2 RID: 6834 RVA: 0x000A61D6 File Offset: 0x000A43D6
	public override void TryCancel(EffectProperties props)
	{
		this.CancelAction(props);
	}

	// Token: 0x06001AB3 RID: 6835 RVA: 0x000A61E0 File Offset: 0x000A43E0
	public void CancelAction(EffectProperties props)
	{
		if (this.CanCancel)
		{
			EffectProperties effectProperties = props.Copy(false);
			effectProperties.OverrideSeed(effectProperties.RandSeed + this.guid.GetHashCode(), 0);
			this.Action.Root.TryCancel(effectProperties);
		}
	}

	// Token: 0x06001AB4 RID: 6836 RVA: 0x000A6227 File Offset: 0x000A4427
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Fire Sub-Action",
			MinInspectorSize = new Vector2(280f, 0f)
		};
	}

	// Token: 0x06001AB5 RID: 6837 RVA: 0x000A624E File Offset: 0x000A444E
	private void NewActionGraph()
	{
		GraphTree editorTreeRef = base.EditorTreeRef;
		this.Action = (ActionTree.CreateAndOpenTree(((editorTreeRef != null) ? editorTreeRef.name : null) ?? "") as ActionTree);
	}

	// Token: 0x06001AB6 RID: 6838 RVA: 0x000A627B File Offset: 0x000A447B
	public SubActionNode()
	{
	}

	// Token: 0x04001B42 RID: 6978
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(PoseNode), false, "Pose Override", PortLocation.Default)]
	public Node Loc;

	// Token: 0x04001B43 RID: 6979
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Dynamic Input", PortLocation.Default)]
	public Node InputValue;

	// Token: 0x04001B44 RID: 6980
	public ActionTree Action;

	// Token: 0x04001B45 RID: 6981
	public bool CanCancel = true;

	// Token: 0x04001B46 RID: 6982
	public bool SeparateCache;
}
