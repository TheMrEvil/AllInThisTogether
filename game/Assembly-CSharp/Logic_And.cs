using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000271 RID: 625
public class Logic_And : LogicNode
{
	// Token: 0x060018DC RID: 6364 RVA: 0x0009B274 File Offset: 0x00099474
	public override bool Evaluate(EffectProperties props)
	{
		using (List<Node>.Enumerator enumerator = this.Tests.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!((LogicNode)enumerator.Current).Evaluate(props))
				{
					this.EditorStateDisplay = NodeState.Fail;
					return false;
				}
			}
		}
		this.EditorStateDisplay = NodeState.Success;
		return true;
	}

	// Token: 0x060018DD RID: 6365 RVA: 0x0009B2E4 File Offset: 0x000994E4
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "AND";
		inspectorProps.SortX = true;
		inspectorProps.MinInspectorSize = new Vector2(100f, 0f);
		inspectorProps.ShowInspectorView = false;
		return inspectorProps;
	}

	// Token: 0x060018DE RID: 6366 RVA: 0x0009B31A File Offset: 0x0009951A
	public Logic_And()
	{
	}

	// Token: 0x040018B5 RID: 6325
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(LogicNode), true, "", PortLocation.Vertical)]
	public List<Node> Tests = new List<Node>();
}
