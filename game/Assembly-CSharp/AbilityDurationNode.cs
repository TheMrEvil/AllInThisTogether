using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000298 RID: 664
public class AbilityDurationNode : AbilityNode
{
	// Token: 0x0600194E RID: 6478 RVA: 0x0009DBE8 File Offset: 0x0009BDE8
	internal override AbilityState Run(EffectProperties props)
	{
		if (!this.startedTimer)
		{
			this.startedTimer = true;
			this.startTime = props.Lifetime;
		}
		if (props.Lifetime - this.startTime >= this.Duration)
		{
			if (!this.finished)
			{
				this.finished = true;
				foreach (Node node in this.During)
				{
					((AbilityNode)node).Cancel(props);
				}
			}
			return AbilityState.Finished;
		}
		AbilityState result = AbilityState.Finished;
		using (List<Node>.Enumerator enumerator = this.During.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((AbilityNode)enumerator.Current).DoUpdate(props) == AbilityState.Running)
				{
					result = AbilityState.Running;
				}
			}
		}
		if (this.EndWithSubnodes)
		{
			return result;
		}
		return AbilityState.Running;
	}

	// Token: 0x0600194F RID: 6479 RVA: 0x0009DCD8 File Offset: 0x0009BED8
	internal override void OnCancel(EffectProperties props)
	{
		this.startedTimer = false;
		this.finished = false;
		foreach (Node node in this.During)
		{
			((AbilityNode)node).Cancel(props);
		}
	}

	// Token: 0x06001950 RID: 6480 RVA: 0x0009DD3C File Offset: 0x0009BF3C
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Duration",
			MinInspectorSize = new Vector2(120f, 0f)
		};
	}

	// Token: 0x06001951 RID: 6481 RVA: 0x0009DD63 File Offset: 0x0009BF63
	public AbilityDurationNode()
	{
	}

	// Token: 0x04001971 RID: 6513
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AbilityNode), true, "", PortLocation.Header)]
	public List<Node> During = new List<Node>();

	// Token: 0x04001972 RID: 6514
	[Tooltip("Executes Outputs over X Seconds")]
	public float Duration = 5f;

	// Token: 0x04001973 RID: 6515
	[Tooltip("Node will Finish if all subnodes are Finished")]
	public bool EndWithSubnodes = true;

	// Token: 0x04001974 RID: 6516
	private bool startedTimer;

	// Token: 0x04001975 RID: 6517
	private bool finished;

	// Token: 0x04001976 RID: 6518
	private float startTime;
}
