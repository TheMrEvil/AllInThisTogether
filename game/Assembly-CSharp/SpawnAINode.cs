using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002DD RID: 733
public class SpawnAINode : EffectNode
{
	// Token: 0x06001A8A RID: 6794 RVA: 0x000A4CF5 File Offset: 0x000A2EF5
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Spawn AI",
			MinInspectorSize = new Vector2(200f, 0f)
		};
	}

	// Token: 0x06001A8B RID: 6795 RVA: 0x000A4D1C File Offset: 0x000A2F1C
	internal override void Apply(EffectProperties properties)
	{
		GameObject gameObject = this.SpawnRef;
		if (this.UseGenreSpawn)
		{
			AIData.AIDetails details = AIManager.instance.DB.GetDetails(this.EnemyType, AIManager.instance.Layout);
			if (details != null)
			{
				gameObject = details.Reference;
			}
		}
		if (gameObject == null || !properties.IsLocal)
		{
			return;
		}
		EffectProperties effectProperties = properties.Copy(false);
		EntityControl entityControl;
		if (this.WorldSpawn)
		{
			entityControl = AIManager.SpawnAIWorld(AIData.AIDetails.GetResourcePath(gameObject), EnemyLevel.Default, false);
		}
		else
		{
			if (this.Loc != null)
			{
				effectProperties.StartLoc = (effectProperties.OutLoc = (this.Loc as PoseNode).GetPose(effectProperties));
			}
			Vector3 forward = Vector3.up;
			Vector3 vector = Vector3.zero;
			if (this.UseOutputPoint)
			{
				ValueTuple<Vector3, Vector3> outputVectors = effectProperties.GetOutputVectors();
				vector = outputVectors.Item1;
				forward = outputVectors.Item2;
			}
			else
			{
				ValueTuple<Vector3, Vector3> originVectors = effectProperties.GetOriginVectors();
				vector = originVectors.Item1;
				forward = originVectors.Item2;
			}
			Debug.DrawLine(vector, vector + Vector3.up * 5f, Color.green, 2f);
			entityControl = AIManager.SpawnAIExplicit(AIData.AIDetails.GetResourcePath(gameObject), vector, forward);
		}
		if (!(entityControl == null))
		{
			AIControl aicontrol = entityControl as AIControl;
			if (aicontrol != null)
			{
				if (this.CreateAsAlly)
				{
					aicontrol.ChangeTeam((effectProperties.SourceControl != null) ? effectProperties.SourceControl.TeamID : 1);
				}
				if (this.IsPet && effectProperties.SourceControl != null)
				{
					aicontrol.Net.SetPetOwnership(effectProperties.SourceControl);
				}
				if (this.NewBrain != null)
				{
					aicontrol.Net.ChangeBrain(this.NewBrain);
				}
				foreach (AugmentTree augmentTree in this.AddMod)
				{
					if (augmentTree != null)
					{
						aicontrol.net.AddAugment(augmentTree.ID);
					}
				}
				if (this.CanCancel)
				{
					aicontrol.Net.SetOwnership(effectProperties.SourceControl, this.guid);
				}
				if (this.InheritAugments && effectProperties.SourceControl != null)
				{
					aicontrol.AddAugment(effectProperties.SourceControl.AllAugments(false, null));
				}
				if (this.Effects.Count > 0)
				{
					aicontrol.Setup();
					effectProperties.SeekTarget = aicontrol.gameObject;
					foreach (Node node in this.Effects)
					{
						((EffectNode)node).Invoke(effectProperties.Copy(false));
					}
				}
				return;
			}
		}
	}

	// Token: 0x06001A8C RID: 6796 RVA: 0x000A4FDC File Offset: 0x000A31DC
	public override void TryCancel(EffectProperties props)
	{
		if (this.CanCancel)
		{
			AIManager.instance.TryCancelAI(props, this.guid);
		}
		base.TryCancel(props);
	}

	// Token: 0x06001A8D RID: 6797 RVA: 0x000A5000 File Offset: 0x000A3200
	private List<AugmentTree> GetModifierIDs()
	{
		GraphDB graphDB = Resources.Load<GraphDB>("GraphDB");
		List<AugmentTree> list = new List<AugmentTree>();
		if (graphDB == null)
		{
			return list;
		}
		foreach (AugmentTree item in graphDB.EnemyMods)
		{
			list.Add(item);
		}
		return list;
	}

	// Token: 0x06001A8E RID: 6798 RVA: 0x000A5070 File Offset: 0x000A3270
	public SpawnAINode()
	{
	}

	// Token: 0x04001B02 RID: 6914
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(PoseNode), false, "Pose Override", PortLocation.Default)]
	public Node Loc;

	// Token: 0x04001B03 RID: 6915
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "Then", PortLocation.Header)]
	public List<Node> Effects = new List<Node>();

	// Token: 0x04001B04 RID: 6916
	public bool UseGenreSpawn;

	// Token: 0x04001B05 RID: 6917
	public GameObject SpawnRef;

	// Token: 0x04001B06 RID: 6918
	public AILayout.GenreEnemy EnemyType;

	// Token: 0x04001B07 RID: 6919
	public bool CreateAsAlly = true;

	// Token: 0x04001B08 RID: 6920
	public bool WorldSpawn;

	// Token: 0x04001B09 RID: 6921
	public bool UseOutputPoint;

	// Token: 0x04001B0A RID: 6922
	public bool InheritAugments;

	// Token: 0x04001B0B RID: 6923
	public bool CanCancel = true;

	// Token: 0x04001B0C RID: 6924
	public bool IsPet;

	// Token: 0x04001B0D RID: 6925
	public AITree NewBrain;

	// Token: 0x04001B0E RID: 6926
	public List<AugmentTree> AddMod;
}
