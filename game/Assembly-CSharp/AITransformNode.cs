using System;
using UnityEngine;

// Token: 0x02000313 RID: 787
public class AITransformNode : AIActionNode
{
	// Token: 0x06001B5D RID: 7005 RVA: 0x000A90DC File Offset: 0x000A72DC
	internal override AILogicState Run(AIControl entity)
	{
		string str = "Transforming into ";
		GameObject newEntity = this.NewEntity;
		Debug.Log(str + ((newEntity != null) ? newEntity.ToString() : null));
		if (this.NewEntity == null)
		{
			return AILogicState.Fail;
		}
		AIData.AIDetails enemy = AIManager.instance.DB.GetEnemy(this.NewEntity);
		if (enemy == null)
		{
			Debug.LogError("Selected AI not in AIDatabase, can't transform over Network");
			return AILogicState.Fail;
		}
		entity.Net.TransformInto(enemy);
		return AILogicState.Success;
	}

	// Token: 0x06001B5E RID: 7006 RVA: 0x000A914C File Offset: 0x000A734C
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Transform Creature",
			MinInspectorSize = new Vector2(100f, 0f)
		};
	}

	// Token: 0x06001B5F RID: 7007 RVA: 0x000A9173 File Offset: 0x000A7373
	public AITransformNode()
	{
	}

	// Token: 0x04001BD4 RID: 7124
	public GameObject NewEntity;
}
