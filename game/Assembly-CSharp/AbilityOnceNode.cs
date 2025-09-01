using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200029D RID: 669
public class AbilityOnceNode : AbilityNode
{
	// Token: 0x06001968 RID: 6504 RVA: 0x0009E844 File Offset: 0x0009CA44
	internal override AbilityState Run(EffectProperties props)
	{
		if (this.didRun)
		{
			return AbilityState.Finished;
		}
		AbilityState abilityState = AbilityState.Finished;
		using (List<Node>.Enumerator enumerator = this.Effects.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((AbilityNode)enumerator.Current).DoUpdate(props) == AbilityState.Running)
				{
					abilityState = AbilityState.Running;
				}
			}
		}
		this.didRun = (abilityState == AbilityState.Finished);
		return abilityState;
	}

	// Token: 0x06001969 RID: 6505 RVA: 0x0009E8B8 File Offset: 0x0009CAB8
	internal override void OnCancel(EffectProperties props)
	{
		if (!this.didRun)
		{
			return;
		}
		foreach (Node node in this.Effects)
		{
			((AbilityNode)node).Cancel(props);
		}
	}

	// Token: 0x0600196A RID: 6506 RVA: 0x0009E918 File Offset: 0x0009CB18
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Run Once",
			MinInspectorSize = new Vector2(150f, 0f)
		};
	}

	// Token: 0x0600196B RID: 6507 RVA: 0x0009E93F File Offset: 0x0009CB3F
	public AbilityOnceNode()
	{
	}

	// Token: 0x04001996 RID: 6550
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AbilityNode), true, "", PortLocation.Header)]
	public List<Node> Effects = new List<Node>();

	// Token: 0x04001997 RID: 6551
	private bool didRun;
}
