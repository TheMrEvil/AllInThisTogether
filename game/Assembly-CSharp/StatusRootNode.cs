using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

// Token: 0x0200036D RID: 877
public class StatusRootNode : RootNode
{
	// Token: 0x06001D1B RID: 7451 RVA: 0x000B0B35 File Offset: 0x000AED35
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Status Effect",
			ShowInputNode = false,
			MinInspectorSize = new Vector2(300f, 0f)
		};
	}

	// Token: 0x06001D1C RID: 7452 RVA: 0x000B0B64 File Offset: 0x000AED64
	public Augments GetModifiers(int stacks, List<StatusRootNode.StatusAugment> overrideExtras)
	{
		Augments augments = new Augments();
		foreach (Node node in this.Augments)
		{
			StatusModNode statusModNode = (StatusModNode)node;
			if (!(statusModNode == null))
			{
				int count = 1;
				if (statusModNode.Stacks)
				{
					count = stacks;
				}
				augments.Add(statusModNode, count);
			}
		}
		foreach (StatusRootNode.StatusAugment statusAugment in this.ExtraAugments)
		{
			augments.Add(statusAugment.Tree, statusAugment.Stacks ? stacks : 1);
		}
		foreach (StatusRootNode.StatusAugment statusAugment2 in overrideExtras)
		{
			augments.Add(statusAugment2.Tree, statusAugment2.Stacks ? stacks : 1);
		}
		return augments;
	}

	// Token: 0x06001D1D RID: 7453 RVA: 0x000B0C8C File Offset: 0x000AEE8C
	public void GetPassiveMods(Passive p, int stacks, ref Augments addTo, List<StatusRootNode.StatusAugment> overrideExtras)
	{
		foreach (Node node in this.Augments)
		{
			StatusModNode statusModNode = (StatusModNode)node;
			if (!(statusModNode == null) && statusModNode.HasPassive(p))
			{
				int count = 1;
				if (statusModNode.Stacks)
				{
					count = stacks;
				}
				addTo.Add(statusModNode, count);
			}
		}
		foreach (StatusRootNode.StatusAugment statusAugment in this.ExtraAugments)
		{
			if (statusAugment.Tree.Root.HasPassive(p))
			{
				addTo.Add(statusAugment.Tree, statusAugment.Stacks ? stacks : 1);
			}
		}
		foreach (StatusRootNode.StatusAugment statusAugment2 in overrideExtras)
		{
			if (statusAugment2.Tree.Root.HasPassive(p))
			{
				addTo.Add(statusAugment2.Tree, statusAugment2.Stacks ? stacks : 1);
			}
		}
	}

	// Token: 0x06001D1E RID: 7454 RVA: 0x000B0DE0 File Offset: 0x000AEFE0
	public Augments GetSnippetMods(EventTrigger t, int stacks, List<StatusRootNode.StatusAugment> overrideExtras)
	{
		Augments augments = new Augments();
		foreach (Node node in this.Augments)
		{
			StatusModNode statusModNode = (StatusModNode)node;
			if (statusModNode.SnippetMatches.Contains(t))
			{
				int count = 1;
				if (statusModNode.Stacks)
				{
					count = stacks;
				}
				augments.Add(statusModNode, count);
			}
		}
		foreach (StatusRootNode.StatusAugment statusAugment in this.ExtraAugments)
		{
			if (statusAugment.Tree.Root.SnippetMatches.Contains(t))
			{
				augments.Add(statusAugment.Tree, statusAugment.Stacks ? stacks : 1);
			}
		}
		foreach (StatusRootNode.StatusAugment statusAugment2 in overrideExtras)
		{
			if (statusAugment2.Tree.Root.SnippetMatches.Contains(t))
			{
				augments.Add(statusAugment2.Tree, statusAugment2.Stacks ? stacks : 1);
			}
		}
		return augments;
	}

	// Token: 0x06001D1F RID: 7455 RVA: 0x000B0F40 File Offset: 0x000AF140
	public bool HasSnippet(EventTrigger t, List<StatusRootNode.StatusAugment> overrideExtras)
	{
		foreach (Node node in this.Augments)
		{
			StatusModNode statusModNode = (StatusModNode)node;
			if (statusModNode != null && statusModNode.SnippetMatches.Contains(t))
			{
				return true;
			}
		}
		using (List<StatusRootNode.StatusAugment>.Enumerator enumerator2 = this.ExtraAugments.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				if (enumerator2.Current.Tree.Root.SnippetMatches.Contains(t))
				{
					return true;
				}
			}
		}
		using (List<StatusRootNode.StatusAugment>.Enumerator enumerator2 = overrideExtras.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				if (enumerator2.Current.Tree.Root.SnippetMatches.Contains(t))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001D20 RID: 7456 RVA: 0x000B1058 File Offset: 0x000AF258
	public bool HasPassive(Passive p, List<StatusRootNode.StatusAugment> overrideExtras)
	{
		foreach (Node node in this.Augments)
		{
			StatusModNode statusModNode = (StatusModNode)node;
			if (statusModNode != null && statusModNode.HasPassive(p))
			{
				return true;
			}
		}
		using (List<StatusRootNode.StatusAugment>.Enumerator enumerator2 = this.ExtraAugments.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				if (enumerator2.Current.Tree.Root.HasPassive(p))
				{
					return true;
				}
			}
		}
		using (List<StatusRootNode.StatusAugment>.Enumerator enumerator2 = overrideExtras.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				if (enumerator2.Current.Tree.Root.HasPassive(p))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001D21 RID: 7457 RVA: 0x000B1160 File Offset: 0x000AF360
	public void Apply(EffectProperties props)
	{
		foreach (Node node in this.OnApply)
		{
			EffectNode effectNode = (EffectNode)node;
			if (effectNode != null)
			{
				effectNode.Invoke(props);
			}
			else
			{
				Debug.LogError("Null Effect in Status " + this.EffectName);
			}
		}
	}

	// Token: 0x06001D22 RID: 7458 RVA: 0x000B11D8 File Offset: 0x000AF3D8
	public void StackChanged(EffectProperties props)
	{
		foreach (Node node in this.OnStackChanged)
		{
			((EffectNode)node).Invoke(props);
		}
	}

	// Token: 0x06001D23 RID: 7459 RVA: 0x000B1230 File Offset: 0x000AF430
	public void TickEvent(EffectProperties props)
	{
		foreach (Node node in this.OnTick)
		{
			((EffectNode)node).Invoke(props);
		}
	}

	// Token: 0x06001D24 RID: 7460 RVA: 0x000B1288 File Offset: 0x000AF488
	public void Expire(EffectProperties props, bool enemyDead)
	{
		if (enemyDead)
		{
			using (List<Node>.Enumerator enumerator = this.OnDiedWith.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Node node = enumerator.Current;
					((EffectNode)node).Invoke(props);
				}
				return;
			}
		}
		foreach (Node node2 in this.OnExpire)
		{
			((EffectNode)node2).Invoke(props);
		}
	}

	// Token: 0x06001D25 RID: 7461 RVA: 0x000B1328 File Offset: 0x000AF528
	public void DiedWith(EffectProperties props)
	{
	}

	// Token: 0x06001D26 RID: 7462 RVA: 0x000B132C File Offset: 0x000AF52C
	public StatusRootNode()
	{
	}

	// Token: 0x04001DBF RID: 7615
	public Sprite EffectIcon;

	// Token: 0x04001DC0 RID: 7616
	public string EffectName = "New Effect";

	// Token: 0x04001DC1 RID: 7617
	[Space(5f)]
	public bool IsNegative;

	// Token: 0x04001DC2 RID: 7618
	public bool ShowInUI;

	// Token: 0x04001DC3 RID: 7619
	public bool UniquePerSource;

	// Token: 0x04001DC4 RID: 7620
	[TextArea(3, 10)]
	public string Description;

	// Token: 0x04001DC5 RID: 7621
	public float tickRate = 1f;

	// Token: 0x04001DC6 RID: 7622
	public bool CanStack;

	// Token: 0x04001DC7 RID: 7623
	public int MaxStacks = 1;

	// Token: 0x04001DC8 RID: 7624
	public StatusRootNode.TimeoutBehaviour TimeBehaviour;

	// Token: 0x04001DC9 RID: 7625
	public bool UniqueStackDurations;

	// Token: 0x04001DCA RID: 7626
	public bool Batched;

	// Token: 0x04001DCB RID: 7627
	[Header("Persist")]
	public bool PersistThroughDeath;

	// Token: 0x04001DCC RID: 7628
	[Header("")]
	public bool PersistThroughWave;

	// Token: 0x04001DCD RID: 7629
	public List<StatusKeyword> Keywords;

	// Token: 0x04001DCE RID: 7630
	public Keyword PlayerKeyword = Keyword.None;

	// Token: 0x04001DCF RID: 7631
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Apply", PortLocation.Default)]
	public List<Node> OnApply = new List<Node>();

	// Token: 0x04001DD0 RID: 7632
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Stack Changed", PortLocation.Default)]
	public List<Node> OnStackChanged = new List<Node>();

	// Token: 0x04001DD1 RID: 7633
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Tick", PortLocation.Default)]
	public List<Node> OnTick = new List<Node>();

	// Token: 0x04001DD2 RID: 7634
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Expire", PortLocation.Default)]
	public List<Node> OnExpire = new List<Node>();

	// Token: 0x04001DD3 RID: 7635
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Died With", PortLocation.Default)]
	public List<Node> OnDiedWith = new List<Node>();

	// Token: 0x04001DD4 RID: 7636
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(StatusModNode), true, "Augments", PortLocation.Default)]
	[ItemCanBeNull]
	public List<Node> Augments = new List<Node>();

	// Token: 0x04001DD5 RID: 7637
	public List<StatusRootNode.StatusAugment> ExtraAugments = new List<StatusRootNode.StatusAugment>();

	// Token: 0x04001DD6 RID: 7638
	public EnemyLevel EnemyLevel = EnemyLevel.All;

	// Token: 0x04001DD7 RID: 7639
	public EnemyType EffectsTypes;

	// Token: 0x04001DD8 RID: 7640
	public bool AffectPlayer = true;

	// Token: 0x0200067B RID: 1659
	public enum TimeoutBehaviour
	{
		// Token: 0x04002BB7 RID: 11191
		Expire,
		// Token: 0x04002BB8 RID: 11192
		DecrementStack
	}

	// Token: 0x0200067C RID: 1660
	[Serializable]
	public class StatusAugment
	{
		// Token: 0x060027CE RID: 10190 RVA: 0x000D71FC File Offset: 0x000D53FC
		public StatusAugment()
		{
		}

		// Token: 0x04002BB9 RID: 11193
		public AugmentTree Tree;

		// Token: 0x04002BBA RID: 11194
		public bool Stacks;
	}
}
