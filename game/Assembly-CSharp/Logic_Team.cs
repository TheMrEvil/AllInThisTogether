using System;
using UnityEngine;

// Token: 0x02000288 RID: 648
public class Logic_Team : LogicNode
{
	// Token: 0x06001917 RID: 6423 RVA: 0x0009C708 File Offset: 0x0009A908
	public override bool Evaluate(EffectProperties props)
	{
		if (this.Test == null)
		{
			return true;
		}
		LogicNode logicNode = this.Test as LogicNode;
		bool flag = this.TestType == Logic_Team.TeamTestType.All;
		int checkTeamID = this.GetCheckTeamID(props);
		foreach (EntityControl entityControl in EntityControl.AllEntities)
		{
			if (!(entityControl == null) && (this.AllowDead || !entityControl.IsDead) && entityControl.TeamID == checkTeamID && (this.Team != Logic_Team.TeamType.Players || entityControl is PlayerControl))
			{
				bool flag2 = logicNode.Evaluate(new EffectProperties(entityControl));
				if (flag2 && this.TestType == Logic_Team.TeamTestType.Any)
				{
					this.EditorStateDisplay = NodeState.Success;
					return true;
				}
				flag = (flag && flag2);
			}
		}
		this.EditorStateDisplay = (flag ? NodeState.Success : NodeState.Fail);
		return flag;
	}

	// Token: 0x06001918 RID: 6424 RVA: 0x0009C7F8 File Offset: 0x0009A9F8
	private int GetCheckTeamID(EffectProperties props)
	{
		int result;
		switch (this.Team)
		{
		case Logic_Team.TeamType.Allies:
			result = props.SourceTeam;
			break;
		case Logic_Team.TeamType.Enemies:
			result = ((props.SourceTeam == 1) ? 2 : 1);
			break;
		case Logic_Team.TeamType.Players:
			result = 1;
			break;
		case Logic_Team.TeamType.AIEnemy:
			result = 2;
			break;
		default:
			result = 0;
			break;
		}
		return result;
	}

	// Token: 0x06001919 RID: 6425 RVA: 0x0009C848 File Offset: 0x0009AA48
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Team Check";
		inspectorProps.MinInspectorSize = new Vector2(150f, 0f);
		inspectorProps.MaxInspectorSize = new Vector2(150f, 0f);
		return inspectorProps;
	}

	// Token: 0x0600191A RID: 6426 RVA: 0x0009C885 File Offset: 0x0009AA85
	public Logic_Team()
	{
	}

	// Token: 0x0400192E RID: 6446
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(LogicNode), false, "", PortLocation.Vertical)]
	public Node Test;

	// Token: 0x0400192F RID: 6447
	public Logic_Team.TeamTestType TestType;

	// Token: 0x04001930 RID: 6448
	public Logic_Team.TeamType Team;

	// Token: 0x04001931 RID: 6449
	public bool AllowDead;

	// Token: 0x02000637 RID: 1591
	public enum TeamTestType
	{
		// Token: 0x04002A71 RID: 10865
		Any,
		// Token: 0x04002A72 RID: 10866
		All
	}

	// Token: 0x02000638 RID: 1592
	public enum TeamType
	{
		// Token: 0x04002A74 RID: 10868
		Allies,
		// Token: 0x04002A75 RID: 10869
		Enemies,
		// Token: 0x04002A76 RID: 10870
		Players,
		// Token: 0x04002A77 RID: 10871
		AIEnemy
	}
}
