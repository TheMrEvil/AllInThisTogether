using System;
using UnityEngine;

// Token: 0x02000307 RID: 775
public class AITestNode : AILogicNode
{
	// Token: 0x06001B2F RID: 6959 RVA: 0x000A8654 File Offset: 0x000A6854
	public virtual bool Evaluate(EntityControl entity)
	{
		return false;
	}

	// Token: 0x06001B30 RID: 6960 RVA: 0x000A8658 File Offset: 0x000A6858
	public virtual bool Evaluate(EffectProperties props, ApplyOn toCheck)
	{
		if (props == null)
		{
			return false;
		}
		EntityControl applicationEntity = props.GetApplicationEntity(toCheck);
		return !(applicationEntity == null) && this.Evaluate(applicationEntity);
	}

	// Token: 0x06001B31 RID: 6961 RVA: 0x000A8684 File Offset: 0x000A6884
	protected static EntityControl GetEntity(EntityControl AI, AITestNode.TestTarget Entity)
	{
		EntityControl result;
		if (Entity != AITestNode.TestTarget.Self)
		{
			if (Entity != AITestNode.TestTarget.CurrentTarget)
			{
				result = AI;
			}
			else
			{
				result = AI.currentTarget;
			}
		}
		else
		{
			result = AI;
		}
		return result;
	}

	// Token: 0x06001B32 RID: 6962 RVA: 0x000A86AA File Offset: 0x000A68AA
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			ShowInputNode = false
		};
	}

	// Token: 0x06001B33 RID: 6963 RVA: 0x000A86B8 File Offset: 0x000A68B8
	public AITestNode()
	{
	}

	// Token: 0x04001BBD RID: 7101
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(Node), true, "", PortLocation.Vertical)]
	public Node TestNode;

	// Token: 0x02000654 RID: 1620
	public enum TestTarget
	{
		// Token: 0x04002B0E RID: 11022
		Self,
		// Token: 0x04002B0F RID: 11023
		CurrentTarget,
		// Token: 0x04002B10 RID: 11024
		Affected
	}
}
