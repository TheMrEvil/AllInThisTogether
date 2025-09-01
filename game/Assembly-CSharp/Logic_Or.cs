using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000286 RID: 646
public class Logic_Or : LogicNode
{
	// Token: 0x0600190F RID: 6415 RVA: 0x0009C410 File Offset: 0x0009A610
	public override bool Evaluate(EffectProperties props)
	{
		using (List<Node>.Enumerator enumerator = this.Tests.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((LogicNode)enumerator.Current).Evaluate(props))
				{
					this.EditorStateDisplay = NodeState.Success;
					return true;
				}
			}
		}
		this.EditorStateDisplay = NodeState.Fail;
		return false;
	}

	// Token: 0x06001910 RID: 6416 RVA: 0x0009C480 File Offset: 0x0009A680
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "OR";
		inspectorProps.MinInspectorSize = new Vector2(100f, 0f);
		inspectorProps.ShowInspectorView = false;
		return inspectorProps;
	}

	// Token: 0x06001911 RID: 6417 RVA: 0x0009C4AF File Offset: 0x0009A6AF
	public Logic_Or()
	{
	}

	// Token: 0x04001925 RID: 6437
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(LogicNode), true, "", PortLocation.Vertical)]
	public List<Node> Tests = new List<Node>();
}
