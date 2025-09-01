using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000355 RID: 853
public class GenreFountainNode : Node
{
	// Token: 0x06001C85 RID: 7301 RVA: 0x000ADDA3 File Offset: 0x000ABFA3
	public bool NeedLayer2()
	{
		return this.Layers > 1;
	}

	// Token: 0x06001C86 RID: 7302 RVA: 0x000ADDAE File Offset: 0x000ABFAE
	public bool NeedLayer3()
	{
		return this.Layers > 2;
	}

	// Token: 0x06001C87 RID: 7303 RVA: 0x000ADDB9 File Offset: 0x000ABFB9
	public bool NeedLayer4()
	{
		return this.Layers > 3;
	}

	// Token: 0x06001C88 RID: 7304 RVA: 0x000ADDC4 File Offset: 0x000ABFC4
	public int GetCost(int layer)
	{
		if (layer <= 0 || layer >= this.Thresholds.Count)
		{
			return 0;
		}
		return this.Thresholds[layer];
	}

	// Token: 0x06001C89 RID: 7305 RVA: 0x000ADDE8 File Offset: 0x000ABFE8
	public List<AugmentTree> GetModifiers(int layer, int sumValue)
	{
		List<AugmentTree> list = new List<AugmentTree>();
		List<Node> list2;
		switch (layer)
		{
		case 0:
			list2 = this.Layer1;
			break;
		case 1:
			list2 = this.Layer2;
			break;
		case 2:
			list2 = this.Layer3;
			break;
		case 3:
			list2 = this.Layer4;
			break;
		default:
			list2 = null;
			break;
		}
		List<Node> list3 = list2;
		if (list3 == null || list3.Count == 0)
		{
			return list;
		}
		int num = 3;
		int num2 = 5 + this.GetCost(layer + 1) - sumValue;
		if (list3.Count == 1)
		{
			GenreRewardOptionNode genreRewardOptionNode = list3[0] as GenreRewardOptionNode;
			List<AugmentTree> modifiers = genreRewardOptionNode.GetModifiers(ModType.Fountain, null);
			int num3 = Mathf.Min(num, modifiers.Count);
			for (int i = list.Count; i < num3; i++)
			{
				AugmentTree augmentTree = GraphDB.ChooseFountainModFromList(modifiers, (i == num3 - 1) ? num2 : 0);
				if (augmentTree != null)
				{
					modifiers.Remove(augmentTree);
					list.Add(augmentTree);
					num2 -= augmentTree.Root.InkCost;
				}
				else
				{
					Debug.Log("No valid mods found from option filter: " + genreRewardOptionNode.titleOverride);
				}
			}
		}
		else
		{
			for (int j = 0; j < num; j++)
			{
				int index = j % list3.Count;
				GenreRewardOptionNode genreRewardOptionNode2 = list3[index] as GenreRewardOptionNode;
				List<AugmentTree> modifiers2 = genreRewardOptionNode2.GetModifiers(ModType.Fountain, null);
				foreach (AugmentTree item in list)
				{
					modifiers2.Remove(item);
				}
				AugmentTree augmentTree2 = GraphDB.ChooseFountainModFromList(modifiers2, (j == num - 1) ? num2 : 0);
				if (augmentTree2 != null)
				{
					list.Add(augmentTree2);
					num2 -= augmentTree2.Root.InkCost;
				}
				else
				{
					Debug.Log("No valid mods found from option filter: " + genreRewardOptionNode2.titleOverride);
				}
			}
		}
		return list;
	}

	// Token: 0x06001C8A RID: 7306 RVA: 0x000ADFD4 File Offset: 0x000AC1D4
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Fountain",
			MinInspectorSize = new Vector2(200f, 0f)
		};
	}

	// Token: 0x06001C8B RID: 7307 RVA: 0x000ADFFC File Offset: 0x000AC1FC
	private bool ValidateList(List<int> list)
	{
		int layers = this.Layers;
		if (list.Count < layers)
		{
			int num = layers - list.Count;
			for (int i = 0; i < num; i++)
			{
				list.Add(0);
			}
		}
		else if (list.Count > layers)
		{
			int num2 = list.Count - layers;
			for (int j = 0; j < num2; j++)
			{
				list.RemoveAt(list.Count - 1);
			}
		}
		list[0] = 0;
		return true;
	}

	// Token: 0x06001C8C RID: 7308 RVA: 0x000AE070 File Offset: 0x000AC270
	public GenreFountainNode()
	{
	}

	// Token: 0x04001D4F RID: 7503
	public int Layers = 1;

	// Token: 0x04001D50 RID: 7504
	[OutputPort(typeof(GenreRewardOptionNode), true, "Layer 1", PortLocation.Default)]
	[HideInInspector]
	[SerializeField]
	public List<Node> Layer1 = new List<Node>();

	// Token: 0x04001D51 RID: 7505
	[OutputPort(typeof(GenreRewardOptionNode), true, "Layer 2", PortLocation.Default)]
	[ShowPort("NeedLayer2")]
	[HideInInspector]
	[SerializeField]
	public List<Node> Layer2 = new List<Node>();

	// Token: 0x04001D52 RID: 7506
	[OutputPort(typeof(GenreRewardOptionNode), true, "Layer 3", PortLocation.Default)]
	[ShowPort("NeedLayer3")]
	[HideInInspector]
	[SerializeField]
	public List<Node> Layer3 = new List<Node>();

	// Token: 0x04001D53 RID: 7507
	[OutputPort(typeof(GenreRewardOptionNode), true, "Layer 4", PortLocation.Default)]
	[ShowPort("NeedLayer4")]
	[HideInInspector]
	[SerializeField]
	public List<Node> Layer4 = new List<Node>();

	// Token: 0x04001D54 RID: 7508
	[Space(10f)]
	[Range(0f, 50f)]
	public List<int> Thresholds = new List<int>();

	// Token: 0x04001D55 RID: 7509
	public bool RewardAtStart;

	// Token: 0x0200066F RID: 1647
	[Serializable]
	public class FountainLayerData
	{
		// Token: 0x060027C3 RID: 10179 RVA: 0x000D6FE0 File Offset: 0x000D51E0
		public FountainLayerData()
		{
		}

		// Token: 0x04002B8A RID: 11146
		public int UnlockReq;

		// Token: 0x04002B8B RID: 11147
		public int Slots = 3;
	}
}
