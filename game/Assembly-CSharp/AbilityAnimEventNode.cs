using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000292 RID: 658
public class AbilityAnimEventNode : AbilityNode
{
	// Token: 0x06001934 RID: 6452 RVA: 0x0009D278 File Offset: 0x0009B478
	internal override AbilityState Run(EffectProperties props)
	{
		AbilityState result = AbilityState.Finished;
		using (List<Node>.Enumerator enumerator = this.OnEvent.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((AbilityNode)enumerator.Current).DoUpdate(props) == AbilityState.Running)
				{
					result = AbilityState.Running;
				}
			}
		}
		return result;
	}

	// Token: 0x06001935 RID: 6453 RVA: 0x0009D2D8 File Offset: 0x0009B4D8
	internal override void OnCancel(EffectProperties props)
	{
		foreach (Node node in this.OnEvent)
		{
			((AbilityNode)node).Cancel(props);
		}
	}

	// Token: 0x06001936 RID: 6454 RVA: 0x0009D330 File Offset: 0x0009B530
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Anim Event",
			MinInspectorSize = new Vector2(120f, 0f)
		};
	}

	// Token: 0x06001937 RID: 6455 RVA: 0x0009D357 File Offset: 0x0009B557
	public AbilityAnimEventNode()
	{
	}

	// Token: 0x04001957 RID: 6487
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AbilityNode), true, "", PortLocation.Header)]
	public List<Node> OnEvent = new List<Node>();

	// Token: 0x04001958 RID: 6488
	[Range(0f, 1f)]
	public float AtPoint = 0.5f;
}
