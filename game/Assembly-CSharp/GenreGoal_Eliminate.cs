using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

// Token: 0x02000357 RID: 855
public class GenreGoal_Eliminate : GenreGoalNode
{
	// Token: 0x06001C92 RID: 7314 RVA: 0x000AE0E4 File Offset: 0x000AC2E4
	public override void Setup()
	{
		if (this.Enemies.Count == 0 || !PhotonNetwork.IsMasterClient)
		{
			return;
		}
		List<GameObject> list = new List<GameObject>();
		switch (this.SpawnType)
		{
		case GenreGoal_Eliminate.SpawnMode.Explicit:
			list = this.Enemies;
			break;
		case GenreGoal_Eliminate.SpawnMode.RandomAny:
			for (int i = 0; i < this.NumToSpawn; i++)
			{
				list.Add(this.Enemies[UnityEngine.Random.Range(0, this.Enemies.Count)]);
			}
			break;
		case GenreGoal_Eliminate.SpawnMode.RandomSingle:
		{
			GameObject item = this.Enemies[UnityEngine.Random.Range(0, this.Enemies.Count)];
			for (int j = 0; j < this.NumToSpawn; j++)
			{
				list.Add(item);
			}
			break;
		}
		}
		if (list.Count == 0)
		{
			return;
		}
		GenreGoal_Eliminate.StatusID = GameplayManager.instance.GoalStatus.RootNode.guid;
		foreach (GameObject o in list)
		{
			AIData.AIDetails details = AIManager.instance.DB.GetDetails(o);
			if (details != null)
			{
				EntityControl entityControl = AIManager.SpawnEnemy(details, true);
				GameplayManager.AddIndicatorStatus(entityControl);
				foreach (string augmentID in this.AddedMods)
				{
					entityControl.net.AddAugment(augmentID);
				}
				if (this.EnemyType == EnemyLevel.Boss)
				{
					entityControl.net.AddAugment(AIManager.instance.BossMod.ID);
				}
				else if (this.EnemyType == EnemyLevel.Elite)
				{
					entityControl.net.AddAugment(AIManager.instance.EliteMod.ID);
				}
				AIManager.instance.PointsSpawned += details.PointValue;
			}
		}
	}

	// Token: 0x06001C93 RID: 7315 RVA: 0x000AE2DC File Offset: 0x000AC4DC
	public override void TickUpdate()
	{
		int num = this.NumToSpawn;
		if (this.SpawnType == GenreGoal_Eliminate.SpawnMode.Explicit)
		{
			num = Mathf.Max(1, this.Enemies.Count);
		}
		int num2 = 0;
		foreach (EntityControl entityControl in AIManager.Enemies)
		{
			if (!entityControl.IsDead && entityControl.HasStatusEffectGUID(GenreGoal_Eliminate.StatusID))
			{
				num2++;
			}
		}
		this.Progress = (float)(num - num2) / (float)num;
	}

	// Token: 0x06001C94 RID: 7316 RVA: 0x000AE370 File Offset: 0x000AC570
	public override bool IsFinished()
	{
		if (AIManager.instance.PointsSpawned == 0f)
		{
			return false;
		}
		foreach (EntityControl entityControl in AIManager.Enemies)
		{
			if (!entityControl.IsDead && entityControl.HasStatusEffectGUID(GenreGoal_Eliminate.StatusID))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001C95 RID: 7317 RVA: 0x000AE3EC File Offset: 0x000AC5EC
	public override string GetGoalInfo()
	{
		return "- Eliminate the Targets -";
	}

	// Token: 0x06001C96 RID: 7318 RVA: 0x000AE3F4 File Offset: 0x000AC5F4
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

	// Token: 0x06001C97 RID: 7319 RVA: 0x000AE46C File Offset: 0x000AC66C
	private List<string> GetModifierIDs()
	{
		GraphDB graphDB = Resources.Load<GraphDB>("GraphDB");
		List<string> list = new List<string>();
		if (graphDB == null)
		{
			return list;
		}
		foreach (AugmentTree augmentTree in graphDB.EnemyMods)
		{
			list.Add(augmentTree.ID);
		}
		return list;
	}

	// Token: 0x06001C98 RID: 7320 RVA: 0x000AE4E4 File Offset: 0x000AC6E4
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Eliminate",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001C99 RID: 7321 RVA: 0x000AE50B File Offset: 0x000AC70B
	public GenreGoal_Eliminate()
	{
	}

	// Token: 0x06001C9A RID: 7322 RVA: 0x000AE521 File Offset: 0x000AC721
	// Note: this type is marked as 'beforefieldinit'.
	static GenreGoal_Eliminate()
	{
	}

	// Token: 0x04001D59 RID: 7513
	private static string StatusID = "";

	// Token: 0x04001D5A RID: 7514
	public EnemyLevel EnemyType = EnemyLevel.Elite;

	// Token: 0x04001D5B RID: 7515
	public GenreGoal_Eliminate.SpawnMode SpawnType;

	// Token: 0x04001D5C RID: 7516
	public int NumToSpawn = 1;

	// Token: 0x04001D5D RID: 7517
	public List<GameObject> Enemies;

	// Token: 0x04001D5E RID: 7518
	public List<string> AddedMods;

	// Token: 0x02000670 RID: 1648
	public enum SpawnMode
	{
		// Token: 0x04002B8D RID: 11149
		Explicit,
		// Token: 0x04002B8E RID: 11150
		RandomAny,
		// Token: 0x04002B8F RID: 11151
		RandomSingle
	}
}
