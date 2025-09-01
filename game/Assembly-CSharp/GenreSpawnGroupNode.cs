using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000361 RID: 865
public class GenreSpawnGroupNode : Node
{
	// Token: 0x1700019C RID: 412
	// (get) Token: 0x06001CDA RID: 7386 RVA: 0x000AFA39 File Offset: 0x000ADC39
	public float GroupValue
	{
		get
		{
			return this.BaseBudget + this.FodderBudget;
		}
	}

	// Token: 0x06001CDB RID: 7387 RVA: 0x000AFA48 File Offset: 0x000ADC48
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Spawn Group",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001CDC RID: 7388 RVA: 0x000AFA70 File Offset: 0x000ADC70
	public List<AIData.AIDetails> CreateGroup(AILayout layout)
	{
		List<AIData.AIDetails> list = new List<AIData.AIDetails>();
		float num = this.BaseBudget;
		float num2 = this.FodderBudget;
		List<AIData.AIDetails> details = AIManager.instance.DB.GetDetails(this.BaseEnemies.Enemies, layout);
		bool flag = false;
		while (num > 0f)
		{
			AIData.AIDetails aidetails = AIManager.instance.DB.ChooseEnemy(details, num, !flag);
			if (aidetails == null)
			{
				num2 += Mathf.Max(num, 0f);
				break;
			}
			flag = true;
			num -= Mathf.Max(aidetails.PointValue, 0.05f);
			list.Add(aidetails);
		}
		List<AIData.AIDetails> options = AIManager.instance.DB.GetDetails(this.FodderEnemies.Enemies, layout);
		if (GameplayManager.HasGameOverride("No_Fodder"))
		{
			options = ((details.Count > 0) ? details : AIManager.instance.DB.GetNormalEnemies(layout));
			num2 *= 4f;
		}
		while (num2 > 0f)
		{
			AIData.AIDetails aidetails2 = AIManager.instance.DB.ChooseEnemy(options, num2, true);
			if (aidetails2 == null)
			{
				break;
			}
			num2 -= Mathf.Max(aidetails2.PointValue, 0.05f);
			list.Add(aidetails2);
		}
		return list;
	}

	// Token: 0x06001CDD RID: 7389 RVA: 0x000AFB94 File Offset: 0x000ADD94
	private List<GameObject> GetAllAI()
	{
		AIData aidata = Resources.Load<AIData>("AI Database");
		List<GameObject> list = new List<GameObject>();
		if (aidata == null)
		{
			return list;
		}
		foreach (AIData.AIDetails aidetails in aidata.Enemies)
		{
			list.Add(aidetails.Reference);
		}
		return list;
	}

	// Token: 0x06001CDE RID: 7390 RVA: 0x000AFC0C File Offset: 0x000ADE0C
	public override void OnCloned()
	{
		this.BaseEnemies = this.BaseEnemies.Copy();
		this.FodderEnemies = this.FodderEnemies.Copy();
	}

	// Token: 0x06001CDF RID: 7391 RVA: 0x000AFC30 File Offset: 0x000ADE30
	public GenreSpawnGroupNode()
	{
	}

	// Token: 0x04001D90 RID: 7568
	[Header("Base Enemies")]
	[Range(0f, 4f)]
	public float BaseBudget = 1.5f;

	// Token: 0x04001D91 RID: 7569
	public AILayout.EnemyBaseTypeList BaseEnemies = new AILayout.EnemyBaseTypeList();

	// Token: 0x04001D92 RID: 7570
	[Header("Fodder Enemies")]
	[Range(0f, 4f)]
	public float FodderBudget = 1f;

	// Token: 0x04001D93 RID: 7571
	public AILayout.FodderTypeList FodderEnemies = new AILayout.FodderTypeList();

	// Token: 0x04001D94 RID: 7572
	[Header("Core")]
	[Range(1f, 5f)]
	[Tooltip("This will spawn multiple times before the next group is considered")]
	public int Repeat = 1;
}
