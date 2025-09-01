using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200029F RID: 671
public class AbilityRootNode : AbilityNode
{
	// Token: 0x06001970 RID: 6512 RVA: 0x0009EBA9 File Offset: 0x0009CDA9
	public global::Pose AtPoint(EffectProperties props)
	{
		if (this.FromLocation == null)
		{
			return global::Pose.WorldPoint(Vector3.zero, Vector3.up);
		}
		return (this.FromLocation as PoseNode).GetPose(props);
	}

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x06001971 RID: 6513 RVA: 0x0009EBDA File Offset: 0x0009CDDA
	public string Name
	{
		get
		{
			if (!this.Usage.IsPlayerAbility)
			{
				return this.Usage.AIAbilityID;
			}
			return this.Usage.AbilityMetadata.Name;
		}
	}

	// Token: 0x06001972 RID: 6514 RVA: 0x0009EC05 File Offset: 0x0009CE05
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Root",
			ShowInputNode = false,
			MinInspectorSize = new Vector2(350f, 0f)
		};
	}

	// Token: 0x06001973 RID: 6515 RVA: 0x0009EC33 File Offset: 0x0009CE33
	public void Activate(EffectProperties props)
	{
		this.isReleasing = false;
		this.actionInvokes.Clear();
	}

	// Token: 0x06001974 RID: 6516 RVA: 0x0009EC47 File Offset: 0x0009CE47
	public void Release(EffectProperties props)
	{
		this.isReleasing = true;
		base.Cancel(props);
		base.DoUpdate(props);
	}

	// Token: 0x06001975 RID: 6517 RVA: 0x0009EC60 File Offset: 0x0009CE60
	internal override AbilityState Run(EffectProperties props)
	{
		AbilityState abilityState = AbilityState.Finished;
		foreach (Node node in (this.isReleasing ? this.OnRelease : this.OnActivate))
		{
			AbilityNode abilityNode = (AbilityNode)node;
			EffectNode effectNode = abilityNode as EffectNode;
			if (effectNode != null)
			{
				if (!this.actionInvokes.Contains(effectNode))
				{
					this.actionInvokes.Add(effectNode);
					effectNode.Invoke(props);
				}
			}
			else if (abilityNode.DoUpdate(props) == AbilityState.Running)
			{
				abilityState = AbilityState.Running;
			}
		}
		if (abilityState == AbilityState.Finished && this.isReleasing)
		{
			foreach (Node node2 in this.OnRelease)
			{
				((AbilityNode)node2).Cancel(props);
			}
		}
		return abilityState;
	}

	// Token: 0x06001976 RID: 6518 RVA: 0x0009ED50 File Offset: 0x0009CF50
	public void AddTags(HashSet<ModTag> tags)
	{
		ModTag item = new ModTag(new ModTagDetail
		{
			AbilityFeature = AbilityFeature.CastTime,
			AbilityType = this.AbilityType
		}, this.PlrAbilityType);
		if (!tags.Contains(item))
		{
			tags.Add(item);
		}
		foreach (ModTagDetail detail in this.Usage.AbilityTags)
		{
			ModTag item2 = new ModTag(detail, this.PlrAbilityType);
			if (!tags.Contains(item2))
			{
				tags.Add(item2);
			}
		}
	}

	// Token: 0x06001977 RID: 6519 RVA: 0x0009EDF4 File Offset: 0x0009CFF4
	public bool IsChanneledAbility()
	{
		return this.AbilityType > AbilityType.Instant;
	}

	// Token: 0x06001978 RID: 6520 RVA: 0x0009EE00 File Offset: 0x0009D000
	internal override void OnCancel(EffectProperties props)
	{
		foreach (Node node in this.OnActivate)
		{
			((AbilityNode)node).Cancel(props);
		}
		this.actionInvokes.Clear();
	}

	// Token: 0x06001979 RID: 6521 RVA: 0x0009EE64 File Offset: 0x0009D064
	public AbilityRootNode()
	{
	}

	// Token: 0x040019A0 RID: 6560
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AbilityNode), true, "On Activate", PortLocation.Default)]
	public List<Node> OnActivate = new List<Node>();

	// Token: 0x040019A1 RID: 6561
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AbilityNode), true, "On Realease", PortLocation.Default)]
	[ShowPort("IsChanneledAbility")]
	public List<Node> OnRelease = new List<Node>();

	// Token: 0x040019A2 RID: 6562
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(PoseNode), false, "From Pose", PortLocation.Header)]
	public Node FromLocation;

	// Token: 0x040019A3 RID: 6563
	public AbilityType AbilityType;

	// Token: 0x040019A4 RID: 6564
	public PlayerAbilityType PlrAbilityType;

	// Token: 0x040019A5 RID: 6565
	public AbilityRootNode.UsageProps Usage = new AbilityRootNode.UsageProps();

	// Token: 0x040019A6 RID: 6566
	private List<EffectNode> actionInvokes = new List<EffectNode>();

	// Token: 0x040019A7 RID: 6567
	[NonSerialized]
	public bool isReleasing;

	// Token: 0x0200063E RID: 1598
	[Serializable]
	public class UsageProps
	{
		// Token: 0x060027A5 RID: 10149 RVA: 0x000D6BD0 File Offset: 0x000D4DD0
		public UsageProps()
		{
		}

		// Token: 0x04002A9D RID: 10909
		[Tooltip("Minimum time between ability activations, must be >= 0.1 seconds")]
		public float Cooldown = 0.1f;

		// Token: 0x04002A9E RID: 10910
		public bool StartOnCD;

		// Token: 0x04002A9F RID: 10911
		public float MinCooldown = 0.1f;

		// Token: 0x04002AA0 RID: 10912
		[Range(0f, 1f)]
		public float RandomStartCD;

		// Token: 0x04002AA1 RID: 10913
		[Range(0f, 1f)]
		public float StartMult = 1f;

		// Token: 0x04002AA2 RID: 10914
		public float SnippetICD;

		// Token: 0x04002AA3 RID: 10915
		public int Charges = 1;

		// Token: 0x04002AA4 RID: 10916
		public bool IsPlayerAbility;

		// Token: 0x04002AA5 RID: 10917
		public PlayerAbilityInfo AbilityMetadata;

		// Token: 0x04002AA6 RID: 10918
		public string AIAbilityID;

		// Token: 0x04002AA7 RID: 10919
		public float MinCancelTime;

		// Token: 0x04002AA8 RID: 10920
		[Range(0f, 1f)]
		public float PageProcChance = 1f;

		// Token: 0x04002AA9 RID: 10921
		public bool RequiresTarget;

		// Token: 0x04002AAA RID: 10922
		public bool CanAutoInstant;

		// Token: 0x04002AAB RID: 10923
		public DangerIndicator.DangerLevel Danger;

		// Token: 0x04002AAC RID: 10924
		public bool Targeted;

		// Token: 0x04002AAD RID: 10925
		public List<ModTagDetail> AbilityTags = new List<ModTagDetail>();
	}
}
