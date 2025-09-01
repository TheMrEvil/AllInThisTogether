using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000362 RID: 866
public class GenreSpawnNode : Node
{
	// Token: 0x06001CE0 RID: 7392 RVA: 0x000AFC6C File Offset: 0x000ADE6C
	public List<AIData.AIDetails> GetGroup(int groupIndex, float pointsAvailable, AILayout layout)
	{
		if (this.SpawnGroups.Count == 0)
		{
			return null;
		}
		int num = 0;
		int num2 = 0;
		GenreSpawnGroupNode genreSpawnGroupNode = null;
		int num3 = 50;
		int num4 = 0;
		for (int i = 0; i <= groupIndex; i++)
		{
			if (num >= this.SpawnGroups.Count)
			{
				num = 0;
			}
			GenreSpawnGroupNode genreSpawnGroupNode2 = this.SpawnGroups[num] as GenreSpawnGroupNode;
			if (genreSpawnGroupNode2 != null && genreSpawnGroupNode2.Repeat > num2)
			{
				if (pointsAvailable >= genreSpawnGroupNode2.GroupValue)
				{
					genreSpawnGroupNode = genreSpawnGroupNode2;
				}
				else
				{
					i--;
				}
				num2++;
			}
			else
			{
				num2 = 0;
				num++;
				i--;
			}
			num4++;
			if (num4 > num3)
			{
				break;
			}
		}
		if (genreSpawnGroupNode == null)
		{
			return null;
		}
		return genreSpawnGroupNode.CreateGroup(layout);
	}

	// Token: 0x06001CE1 RID: 7393 RVA: 0x000AFD1F File Offset: 0x000ADF1F
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Chapter Spawns",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001CE2 RID: 7394 RVA: 0x000AFD48 File Offset: 0x000ADF48
	public bool HasEnemyType(EnemyType eType)
	{
		if (this.FillEnemies.HasEnemyType(eType))
		{
			return true;
		}
		foreach (Node node in this.SpawnGroups)
		{
			GenreSpawnGroupNode genreSpawnGroupNode = (GenreSpawnGroupNode)node;
			if (genreSpawnGroupNode.FodderEnemies.HasEnemyType(eType) || genreSpawnGroupNode.BaseEnemies.HasEnemyType(eType))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001CE3 RID: 7395 RVA: 0x000AFDCC File Offset: 0x000ADFCC
	public override void OnCloned()
	{
		this.FillEnemies = this.FillEnemies.Copy();
	}

	// Token: 0x06001CE4 RID: 7396 RVA: 0x000AFDDF File Offset: 0x000ADFDF
	public GenreSpawnNode()
	{
	}

	// Token: 0x04001D95 RID: 7573
	[Tooltip("Total Points in the Chapter")]
	public int PointTotal = 20;

	// Token: 0x04001D96 RID: 7574
	[Tooltip("Total Points in the Set")]
	public int GroupPoints = 8;

	// Token: 0x04001D97 RID: 7575
	[Tooltip("The next set will spawn when enemies adding to this point value are alive")]
	public int NextGrpAt = 3;

	// Token: 0x04001D98 RID: 7576
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreSpawnGroupNode), true, "Spawn Groups", PortLocation.Default)]
	public List<Node> SpawnGroups = new List<Node>();

	// Token: 0x04001D99 RID: 7577
	[Header("Fill Enemies")]
	[Tooltip("If no points are available for a full group, remaining points will be filled with enemies from this list")]
	public AILayout.EnemyTypeList FillEnemies = new AILayout.EnemyTypeList();

	// Token: 0x02000675 RID: 1653
	[Serializable]
	public class EliteSpawn
	{
		// Token: 0x060027C8 RID: 10184 RVA: 0x000D7022 File Offset: 0x000D5222
		public EliteSpawn()
		{
		}

		// Token: 0x04002BA0 RID: 11168
		[Range(0f, 1f)]
		public float At;

		// Token: 0x04002BA1 RID: 11169
		[Range(1f, 10f)]
		public int Index = 1;
	}
}
