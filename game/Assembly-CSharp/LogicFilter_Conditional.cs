using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200026B RID: 619
public class LogicFilter_Conditional : LogicFilterNode
{
	// Token: 0x060018C7 RID: 6343 RVA: 0x0009AE60 File Offset: 0x00099060
	public override void Filter(ref List<EffectProperties> propList, EffectProperties props)
	{
		if (!(this.Test == null))
		{
			LogicNode logicNode = this.Test as LogicNode;
			if (logicNode != null)
			{
				for (int i = propList.Count - 1; i >= 0; i--)
				{
					if (!logicNode.Evaluate(propList[i]))
					{
						propList.RemoveAt(i);
					}
				}
				return;
			}
		}
	}

	// Token: 0x060018C8 RID: 6344 RVA: 0x0009AEB7 File Offset: 0x000990B7
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Keep";
		inspectorProps.MinInspectorSize = new Vector2(100f, 0f);
		inspectorProps.AllowMultipleInputs = true;
		return inspectorProps;
	}

	// Token: 0x060018C9 RID: 6345 RVA: 0x0009AEE6 File Offset: 0x000990E6
	public LogicFilter_Conditional()
	{
	}

	// Token: 0x040018AB RID: 6315
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(LogicNode), false, "If", PortLocation.Vertical)]
	public Node Test;
}
