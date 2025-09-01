using System;
using System.Collections.Generic;

// Token: 0x020000A4 RID: 164
[Serializable]
public class Augments
{
	// Token: 0x060007A3 RID: 1955 RVA: 0x00036CA9 File Offset: 0x00034EA9
	public Augments()
	{
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x00036CE0 File Offset: 0x00034EE0
	public Augments(Augments aug)
	{
		if (aug == null)
		{
			return;
		}
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in aug.trees)
		{
			this.trees.Add(keyValuePair.Key, keyValuePair.Value);
			this.TreeIDs.Add(keyValuePair.Key.guid);
		}
		this.allTags.Clear();
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x00036DA0 File Offset: 0x00034FA0
	public void Add(Augments augment)
	{
		if (augment == null)
		{
			return;
		}
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in augment.trees)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}
		this.allTags.Clear();
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x00036E10 File Offset: 0x00035010
	public void Add(AugmentRootNode tree, int count = 1)
	{
		if (tree == null)
		{
			return;
		}
		if (this.trees.ContainsKey(tree))
		{
			Dictionary<AugmentRootNode, int> dictionary = this.trees;
			dictionary[tree] += count;
			return;
		}
		this.TreeIDs.Add(tree.guid);
		this.trees.Add(tree, count);
		this.allTags.Clear();
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x00036E7C File Offset: 0x0003507C
	public void Add(List<AugmentTree> augments)
	{
		if (augments == null || augments.Count == 0)
		{
			return;
		}
		foreach (AugmentTree augmentTree in augments)
		{
			if (this.trees.ContainsKey(augmentTree.Root))
			{
				Dictionary<AugmentRootNode, int> dictionary = this.trees;
				AugmentRootNode root = augmentTree.Root;
				dictionary[root]++;
			}
			else
			{
				this.TreeIDs.Add(augmentTree.Root.guid);
				this.trees.Add(augmentTree.Root, 1);
			}
		}
		this.allTags.Clear();
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x00036F38 File Offset: 0x00035138
	public void Remove(AugmentTree tree, int count = 1)
	{
		if (tree == null)
		{
			return;
		}
		if (!this.trees.ContainsKey(tree))
		{
			return;
		}
		Dictionary<AugmentRootNode, int> dictionary = this.trees;
		AugmentRootNode key = tree;
		dictionary[key] -= count;
		if (this.trees[tree] <= 0)
		{
			this.TreeIDs.Remove(tree.ID);
			this.trees.Remove(tree);
		}
		this.allTags.Clear();
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x00036FC4 File Offset: 0x000351C4
	public void Clear()
	{
		this.trees.Clear();
		this.TreeIDs.Clear();
		this.allTags.Clear();
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x00036FE7 File Offset: 0x000351E7
	public int GetCount(AugmentTree tree)
	{
		if (this.trees.ContainsKey(tree.Root))
		{
			return this.trees[tree.Root];
		}
		return 0;
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x00037010 File Offset: 0x00035210
	public float GetModifiedValue(EntityControl control, EffectProperties props, Passive passive, float startVal)
	{
		float num = startVal;
		this.sorted.Clear();
		foreach (KeyValuePair<AugmentRootNode, int> item in this.trees)
		{
			int priority = item.Key.Priority;
			if (!this.sorted.ContainsKey(priority))
			{
				this.sorted[priority] = new List<KeyValuePair<AugmentRootNode, int>>();
			}
			this.sorted[priority].Add(item);
		}
		foreach (KeyValuePair<int, List<KeyValuePair<AugmentRootNode, int>>> keyValuePair in this.sorted)
		{
			startVal = num;
			foreach (KeyValuePair<AugmentRootNode, int> keyValuePair2 in keyValuePair.Value)
			{
				num += keyValuePair2.Key.ModifyPassive(control, props, passive, startVal, keyValuePair2.Value, false);
			}
		}
		return num;
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x00037144 File Offset: 0x00035344
	public void GetPassiveAugments(Passive p, ref Augments addTo)
	{
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.trees)
		{
			if (keyValuePair.Key.HasPassive(p))
			{
				addTo.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x000371B4 File Offset: 0x000353B4
	public float GetScalarValue(EntityControl control, Passive passive, EffectProperties props)
	{
		float num = 1f;
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.trees)
		{
			if (keyValuePair.Key.HasPassive(passive))
			{
				num += keyValuePair.Key.GetPassiveScalar(control, passive, props, keyValuePair.Value, false);
			}
		}
		return num;
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x00037230 File Offset: 0x00035430
	private List<ModPassiveNode> GetPassives(Passive property)
	{
		List<ModPassiveNode> list = new List<ModPassiveNode>();
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.trees)
		{
			foreach (Node node in keyValuePair.Key.Passives)
			{
				ModPassiveNode modPassiveNode = (ModPassiveNode)node;
				if (modPassiveNode.MatchesPassive(property))
				{
					list.Add(modPassiveNode);
				}
			}
		}
		return list;
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x000372DC File Offset: 0x000354DC
	public bool HasPassive(Passive property)
	{
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.trees)
		{
			if (keyValuePair.Key.HasPassive(property))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x00037340 File Offset: 0x00035540
	public void ApplySnippets(EntityControl control, EventTrigger trigger, EffectProperties props = null, float chanceMult = 1f)
	{
		Dictionary<AugmentRootNode, int> dictionary = new Dictionary<AugmentRootNode, int>();
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.trees)
		{
			if (keyValuePair.Key.Snippets.Count != 0 && keyValuePair.Key.SnippetMatches.Contains(trigger))
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair2 in dictionary)
		{
			for (int i = 0; i < keyValuePair2.Value; i++)
			{
				keyValuePair2.Key.TryTrigger(control, trigger, props, chanceMult);
			}
		}
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x0003742C File Offset: 0x0003562C
	public void ApplySnippetsFromProps(EventTrigger trigger, EffectProperties props)
	{
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.trees)
		{
			if (keyValuePair.Key.Snippets.Count != 0 && keyValuePair.Key.SnippetMatches.Contains(trigger))
			{
				for (int i = 0; i < keyValuePair.Value; i++)
				{
					keyValuePair.Key.TryTriggerFromProps(trigger, props);
				}
			}
		}
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x000374C0 File Offset: 0x000356C0
	public Augments GetSnippetAugments(EventTrigger t)
	{
		if (this.snippetAug == null)
		{
			this.snippetAug = new Augments();
		}
		this.snippetAug.Clear();
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.trees)
		{
			if (keyValuePair.Key.SnippetMatches.Contains(t))
			{
				this.snippetAug.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
		return this.snippetAug;
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x00037560 File Offset: 0x00035760
	public bool HasSnippet(EventTrigger t)
	{
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.trees)
		{
			if (keyValuePair.Key.SnippetMatches.Contains(t))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x000375C8 File Offset: 0x000357C8
	public void OverrideNodeProperties(EffectProperties props, Node n, params object[] values)
	{
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.trees)
		{
			keyValuePair.Key.OverrideNodeProperties(props, n, values);
		}
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x00037624 File Offset: 0x00035824
	public void OverrideNodeEffects(EffectProperties props, Node n, ref List<ModOverrideNode> overrides)
	{
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.trees)
		{
			if (keyValuePair.Key.Overrides.Count != 0)
			{
				for (int i = 0; i < keyValuePair.Value; i++)
				{
					keyValuePair.Key.OverrideNodeEffects(props, n, ref overrides);
				}
			}
		}
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x000376A4 File Offset: 0x000358A4
	public List<ModOverrideNode> GetOverrides(EffectProperties props, Node n)
	{
		List<ModOverrideNode> list = new List<ModOverrideNode>();
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.trees)
		{
			if (keyValuePair.Key.Overrides.Count != 0)
			{
				for (int i = 0; i < keyValuePair.Value; i++)
				{
					list.AddRange(keyValuePair.Key.GetOverrides(props, n));
				}
			}
		}
		return list;
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x00037730 File Offset: 0x00035930
	public List<WorldOverrideNode> GetWorldOverrides()
	{
		List<WorldOverrideNode> list = new List<WorldOverrideNode>();
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.trees)
		{
			list.AddRange(keyValuePair.Key.GetWorldOverrides());
		}
		return list;
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x00037798 File Offset: 0x00035998
	internal HashSet<ModTag> GetTags()
	{
		if (this.allTags.Count > 0)
		{
			return this.allTags;
		}
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.trees)
		{
			foreach (ModTag item in keyValuePair.Key.AddedTags)
			{
				if (!this.allTags.Contains(item))
				{
					this.allTags.Add(item);
				}
			}
		}
		return this.allTags;
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x0003785C File Offset: 0x00035A5C
	internal HashSet<EnemyModTag> GetEnemyTags()
	{
		HashSet<EnemyModTag> hashSet = new HashSet<EnemyModTag>();
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.trees)
		{
			foreach (EnemyModTag item in keyValuePair.Key.EnemyTags)
			{
				if (!hashSet.Contains(item))
				{
					hashSet.Add(item);
				}
			}
		}
		return hashSet;
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x00037904 File Offset: 0x00035B04
	public Augments(string netStr)
	{
		string[] array = netStr.Split(',', StringSplitOptions.None);
		int num = array[0].TryParseInt();
		int num2 = 1;
		int num3 = num2;
		while (num3 < num + num2 && num3 < array.Length)
		{
			string[] array2 = array[num3].Split('|', StringSplitOptions.None);
			int num4 = 1;
			int.TryParse(array2[1], out num4);
			AugmentTree augment = GraphDB.GetAugment(array2[0]);
			if (augment != null && num4 > 0)
			{
				this.Add(augment, num4);
			}
			num2 = num3;
			num3++;
		}
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x000379B0 File Offset: 0x00035BB0
	public override string ToString()
	{
		string text = "";
		text += this.trees.Count.ToString();
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.trees)
		{
			text = string.Concat(new string[]
			{
				text,
				",",
				keyValuePair.Key.guid,
				"|",
				keyValuePair.Value.ToString()
			});
		}
		return text;
	}

	// Token: 0x0400065C RID: 1628
	[NonSerialized]
	public Dictionary<AugmentRootNode, int> trees = new Dictionary<AugmentRootNode, int>();

	// Token: 0x0400065D RID: 1629
	[NonSerialized]
	public HashSet<string> TreeIDs = new HashSet<string>();

	// Token: 0x0400065E RID: 1630
	[NonSerialized]
	internal HashSet<ModTag> allTags = new HashSet<ModTag>();

	// Token: 0x0400065F RID: 1631
	private SortedList<int, List<KeyValuePair<AugmentRootNode, int>>> sorted = new SortedList<int, List<KeyValuePair<AugmentRootNode, int>>>();

	// Token: 0x04000660 RID: 1632
	private Augments snippetAug;
}
