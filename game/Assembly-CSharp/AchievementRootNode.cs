using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020002A7 RID: 679
public class AchievementRootNode : RootNode
{
	// Token: 0x06001997 RID: 6551 RVA: 0x0009F79D File Offset: 0x0009D99D
	public bool TryUnlock(EventTrigger trigger, EffectProperties props)
	{
		return trigger == this.Trigger && (!this.CanScope() || this.ScopeMatches(props.AbilityType)) && this.SpecificMatches(props) && this.RequirementsMet(props);
	}

	// Token: 0x06001998 RID: 6552 RVA: 0x0009F7DC File Offset: 0x0009D9DC
	private bool RequirementsMet(EffectProperties props)
	{
		if (!(this.Reqs == null))
		{
			LogicNode logicNode = this.Reqs as LogicNode;
			if (logicNode != null)
			{
				return logicNode.Evaluate(props);
			}
		}
		return false;
	}

	// Token: 0x06001999 RID: 6553 RVA: 0x0009F80F File Offset: 0x0009DA0F
	private bool CanScope()
	{
		return this.Trigger.CanScope();
	}

	// Token: 0x0600199A RID: 6554 RVA: 0x0009F81C File Offset: 0x0009DA1C
	public bool NeedsProgressDisplay()
	{
		return this.ProgressNum != null;
	}

	// Token: 0x0600199B RID: 6555 RVA: 0x0009F82C File Offset: 0x0009DA2C
	[return: TupleElementNames(new string[]
	{
		"current",
		"needed"
	})]
	public ValueTuple<int, int> GetProgressValues(EffectProperties props)
	{
		if (this.ProgressNum == null)
		{
			return new ValueTuple<int, int>(0, 1);
		}
		int num = 0;
		if (this.ProgressNum != null)
		{
			NumberNode progressNum = this.ProgressNum;
			if (progressNum != null)
			{
				num = (int)progressNum.Evaluate(props);
			}
		}
		num = Mathf.Min(num, this.ProgressRequired);
		return new ValueTuple<int, int>(num, this.ProgressRequired);
	}

	// Token: 0x0600199C RID: 6556 RVA: 0x0009F88B File Offset: 0x0009DA8B
	private bool ScopeMatches(PlayerAbilityType aType)
	{
		return this.EventScope == PlayerAbilityType.Any || aType == PlayerAbilityType.Any || this.EventScope == aType;
	}

	// Token: 0x0600199D RID: 6557 RVA: 0x0009F8A5 File Offset: 0x0009DAA5
	private bool SpecificMatches(EffectProperties props)
	{
		return this.Trigger != EventTrigger.KeywordTriggered || (props.Keyword != Keyword.None && props.Keyword == this.Keyword);
	}

	// Token: 0x0600199E RID: 6558 RVA: 0x0009F8D0 File Offset: 0x0009DAD0
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Achievement",
			ShowInspectorView = true,
			ShowInputNode = false,
			MinInspectorSize = new Vector2(320f, 150f),
			MaxInspectorSize = new Vector3(320f, 150f)
		};
	}

	// Token: 0x0600199F RID: 6559 RVA: 0x0009F92A File Offset: 0x0009DB2A
	public AchievementRootNode()
	{
	}

	// Token: 0x040019C6 RID: 6598
	public Sprite Icon;

	// Token: 0x040019C7 RID: 6599
	public string Name;

	// Token: 0x040019C8 RID: 6600
	public string ID;

	// Token: 0x040019C9 RID: 6601
	[TextArea(2, 3)]
	public string Detail;

	// Token: 0x040019CA RID: 6602
	[InputPort(typeof(LogicNode), false, "Conditions", PortLocation.Vertical)]
	[HideInInspector]
	[SerializeField]
	public Node Reqs;

	// Token: 0x040019CB RID: 6603
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Progress", PortLocation.Default)]
	public NumberNode ProgressNum;

	// Token: 0x040019CC RID: 6604
	public EventTrigger Trigger;

	// Token: 0x040019CD RID: 6605
	public PlayerAbilityType EventScope;

	// Token: 0x040019CE RID: 6606
	public Keyword Keyword = Keyword.None;

	// Token: 0x040019CF RID: 6607
	public int ProgressRequired;

	// Token: 0x040019D0 RID: 6608
	public bool ResetWithPrestige;

	// Token: 0x040019D1 RID: 6609
	public bool ShowToast;

	// Token: 0x040019D2 RID: 6610
	public bool RequiresClaim;

	// Token: 0x040019D3 RID: 6611
	public bool RewardsCurrency;

	// Token: 0x040019D4 RID: 6612
	public int Quillmarks;

	// Token: 0x040019D5 RID: 6613
	public int Gildings;
}
