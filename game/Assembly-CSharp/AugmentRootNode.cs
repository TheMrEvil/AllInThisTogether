using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Search;

// Token: 0x02000330 RID: 816
public class AugmentRootNode : RootNode
{
	// Token: 0x17000195 RID: 405
	// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x000AAB04 File Offset: 0x000A8D04
	public HashSet<EventTrigger> SnippetMatches
	{
		get
		{
			if (this._snippetMatches != null)
			{
				return this._snippetMatches;
			}
			this._snippetMatches = new HashSet<EventTrigger>();
			foreach (Node node in this.Snippets)
			{
				ModSnippetNode modSnippetNode = (ModSnippetNode)node;
				this._snippetMatches.Add(modSnippetNode.Trigger);
			}
			return this._snippetMatches;
		}
	}

	// Token: 0x06001BCA RID: 7114 RVA: 0x000AAB88 File Offset: 0x000A8D88
	public float ModifyPassive(EntityControl control, EffectProperties props, Passive passive, float startVal, int stackCount = 1, bool ignoreRequirements = false)
	{
		float num = 0f;
		foreach (Node node in this.Passives)
		{
			ModPassiveNode modPassiveNode = (ModPassiveNode)node;
			num += modPassiveNode.TryModifyValue(control, props, passive, startVal, this, stackCount, ignoreRequirements);
		}
		return num;
	}

	// Token: 0x06001BCB RID: 7115 RVA: 0x000AABF4 File Offset: 0x000A8DF4
	public float GetPassiveScalar(EntityControl control, Passive passive, EffectProperties props, int stackCount = 1, bool ignoreRequirements = false)
	{
		float num = 0f;
		foreach (Node node in this.Passives)
		{
			ModPassiveNode modPassiveNode = (ModPassiveNode)node;
			if (modPassiveNode.Multiplier)
			{
				num += modPassiveNode.TryModifyValue(control, props, passive, 1f, this, stackCount, ignoreRequirements);
			}
		}
		return num;
	}

	// Token: 0x06001BCC RID: 7116 RVA: 0x000AAC6C File Offset: 0x000A8E6C
	public bool HasRequirements(EffectProperties props)
	{
		using (List<Node>.Enumerator enumerator = this.Passives.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!((ModPassiveNode)enumerator.Current).RequirementsMet(props))
				{
					return false;
				}
			}
		}
		using (List<Node>.Enumerator enumerator = this.Snippets.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!((ModSnippetNode)enumerator.Current).RequirementsMet(props))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06001BCD RID: 7117 RVA: 0x000AAD18 File Offset: 0x000A8F18
	public bool HasPassive(Passive p)
	{
		if (this._passiveCache == null)
		{
			this._passiveCache = new HashSet<int>();
			foreach (Node node in this.Passives)
			{
				ModPassiveNode modPassiveNode = (ModPassiveNode)node;
				if (modPassiveNode.category == ModPassiveNode.Category.Entity)
				{
					this._passiveCache.Add(modPassiveNode.entityPassive.GetHashCode());
				}
				if (modPassiveNode.category == ModPassiveNode.Category.Ability)
				{
					this._passiveCache.Add(modPassiveNode.abilityPassive.GetHashCode());
				}
				if (modPassiveNode.category == ModPassiveNode.Category.Keyword)
				{
					this._passiveCache.Add(modPassiveNode.keywordPassive.GetHashCode());
				}
			}
		}
		return this._passiveCache.Contains(p.GetHashCode());
	}

	// Token: 0x06001BCE RID: 7118 RVA: 0x000AADF0 File Offset: 0x000A8FF0
	private bool InStatus()
	{
		return this.RootNode != null && this.RootNode is StatusRootNode;
	}

	// Token: 0x06001BCF RID: 7119 RVA: 0x000AAE10 File Offset: 0x000A9010
	public void TryTrigger(EntityControl control, EventTrigger trigger, EffectProperties props = null, float chanceMult = 1f)
	{
		if (props == null)
		{
			props = new EffectProperties(control);
			props.AbilityType = PlayerAbilityType.None;
			props.StartLoc = (props.OutLoc = global::Pose.WorldPoint(control.display.CenterOfMass.position, control.display.CenterOfMass.forward));
		}
		foreach (Node node in this.Snippets)
		{
			ModSnippetNode modSnippetNode = (ModSnippetNode)node;
			if (modSnippetNode != null && modSnippetNode.Trigger == trigger)
			{
				modSnippetNode.TryTrigger(control, props, chanceMult, this);
			}
		}
	}

	// Token: 0x06001BD0 RID: 7120 RVA: 0x000AAEC4 File Offset: 0x000A90C4
	public void TryTriggerFromProps(EventTrigger trigger, EffectProperties props)
	{
		foreach (Node node in this.Snippets)
		{
			ModSnippetNode modSnippetNode = (ModSnippetNode)node;
			if (modSnippetNode != null && modSnippetNode.Trigger == trigger)
			{
				modSnippetNode.TryTriggerFromProps(props, this);
			}
		}
	}

	// Token: 0x06001BD1 RID: 7121 RVA: 0x000AAF30 File Offset: 0x000A9130
	public void CollectSnippets(ref List<ModSnippetNode> mods, EventTrigger trigger)
	{
		foreach (Node node in this.Snippets)
		{
			ModSnippetNode modSnippetNode = (ModSnippetNode)node;
			if (!(modSnippetNode == null) && modSnippetNode.Trigger == trigger)
			{
				mods.Add(modSnippetNode);
			}
		}
	}

	// Token: 0x06001BD2 RID: 7122 RVA: 0x000AAF9C File Offset: 0x000A919C
	public void OverrideNodeProperties(EffectProperties props, Node n, object[] values)
	{
		int num = 0;
		if (props != null)
		{
			num = (int)props.GetExtra(EProp.Override_Depth, 0f);
		}
		foreach (Node node in this.Overrides)
		{
			ModOverrideNode modOverrideNode = (ModOverrideNode)node;
			if (num <= modOverrideNode.MaxDepth)
			{
				modOverrideNode.OverrideNodeProperties(props, n, values);
			}
		}
	}

	// Token: 0x06001BD3 RID: 7123 RVA: 0x000AB018 File Offset: 0x000A9218
	public void OverrideNodeEffects(EffectProperties props, Node n, ref List<ModOverrideNode> overrides)
	{
		int num = 0;
		if (props != null)
		{
			num = (int)props.GetExtra(EProp.Override_Depth, 0f);
		}
		foreach (Node node in this.Overrides)
		{
			ModOverrideNode modOverrideNode = (ModOverrideNode)node;
			if (modOverrideNode != null && num <= modOverrideNode.MaxDepth)
			{
				modOverrideNode.OverrideNodeEffects(props, n, ref overrides);
			}
		}
	}

	// Token: 0x06001BD4 RID: 7124 RVA: 0x000AB09C File Offset: 0x000A929C
	public List<ModOverrideNode> GetOverrides(EffectProperties props, Node n)
	{
		List<ModOverrideNode> list = new List<ModOverrideNode>();
		foreach (Node node in this.Overrides)
		{
			ModOverrideNode modOverrideNode = (ModOverrideNode)node;
			if (modOverrideNode.ShouldOverride(props, n))
			{
				list.Add(modOverrideNode);
			}
		}
		return list;
	}

	// Token: 0x06001BD5 RID: 7125 RVA: 0x000AB108 File Offset: 0x000A9308
	public List<WorldOverrideNode> GetWorldOverrides()
	{
		List<WorldOverrideNode> list = new List<WorldOverrideNode>();
		foreach (Node node in this.Overrides)
		{
			WorldOverrideNode worldOverrideNode = ((ModOverrideNode)node) as WorldOverrideNode;
			if (worldOverrideNode != null)
			{
				list.Add(worldOverrideNode);
			}
		}
		return list;
	}

	// Token: 0x06001BD6 RID: 7126 RVA: 0x000AB170 File Offset: 0x000A9370
	public bool ShouldShowAsStatus(EntityControl control)
	{
		if (!this.ShowAsStatus)
		{
			return false;
		}
		if (this.Passives.Count == 0)
		{
			return false;
		}
		foreach (Node node in this.Passives)
		{
			ModPassiveNode modPassiveNode = (ModPassiveNode)node;
			if (!(modPassiveNode.Reqs == null) && modPassiveNode.RequirementsMet(control))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001BD7 RID: 7127 RVA: 0x000AB1F8 File Offset: 0x000A93F8
	public void WarmPoolObjects()
	{
		if (this.PoolObjects == null || this.PoolObjects.Count == 0)
		{
			return;
		}
		foreach (AugmentRootNode.PoolObject poolObject in this.PoolObjects)
		{
			ActionPool.Warm(poolObject.Obj, poolObject.Count);
		}
	}

	// Token: 0x06001BD8 RID: 7128 RVA: 0x000AB26C File Offset: 0x000A946C
	public bool Validate(EffectProperties props)
	{
		return this.Requirements == null || (this.Requirements as LogicNode).Evaluate(props);
	}

	// Token: 0x06001BD9 RID: 7129 RVA: 0x000AB290 File Offset: 0x000A9490
	public int GetDifficulty(GenreTree genre, int wave, List<AugmentRootNode> EnemyAugments, List<ModDifficultyNode.PlayerInfo> Players)
	{
		ModDifficultyNode dMod = null;
		if (this.DifficultyMod != null)
		{
			dMod = (this.DifficultyMod as ModDifficultyNode);
		}
		return ModDifficultyNode.ModifyDifficulty(this, dMod, this.Difficulty, genre, wave, EnemyAugments, Players);
	}

	// Token: 0x06001BDA RID: 7130 RVA: 0x000AB2CB File Offset: 0x000A94CB
	public string GetCauseName()
	{
		if (!(this.DmgCreditOverride == null))
		{
			return this.DmgCreditOverride.Root.Name;
		}
		return this.Name;
	}

	// Token: 0x06001BDB RID: 7131 RVA: 0x000AB2F2 File Offset: 0x000A94F2
	public string GetCauseID()
	{
		if (!(this.DmgCreditOverride == null))
		{
			return this.DmgCreditOverride.Root.guid;
		}
		return this.guid;
	}

	// Token: 0x06001BDC RID: 7132 RVA: 0x000AB31C File Offset: 0x000A951C
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Augment",
			ShowInspectorView = true,
			ShowInputNode = false,
			MinInspectorSize = new Vector2(320f, 150f),
			MaxInspectorSize = new Vector3(320f, 150f)
		};
	}

	// Token: 0x06001BDD RID: 7133 RVA: 0x000AB376 File Offset: 0x000A9576
	private IEnumerable GetSprites()
	{
		return new List<Sprite>();
	}

	// Token: 0x06001BDE RID: 7134 RVA: 0x000AB37D File Offset: 0x000A957D
	public bool ShowDifficultyModPort()
	{
		return !this.InStatus() && this.modType == ModType.Enemy;
	}

	// Token: 0x06001BDF RID: 7135 RVA: 0x000AB392 File Offset: 0x000A9592
	public void SuggestTitleName()
	{
		if (string.IsNullOrEmpty(this.Detail))
		{
			return;
		}
		AugmentRootNode.OpenAIReq("SuggestAugmentTitle", this.Detail, new Action<string>(this.UpdateTitle));
		this.fetchingTitle = true;
	}

	// Token: 0x06001BE0 RID: 7136 RVA: 0x000AB3C5 File Offset: 0x000A95C5
	private void UpdateTitle(string val)
	{
		this.Name = val.Replace("Title:", "").Trim();
		this.fetchingTitle = false;
	}

	// Token: 0x06001BE1 RID: 7137 RVA: 0x000AB3E9 File Offset: 0x000A95E9
	private bool NoTitleSuggesting()
	{
		return !string.IsNullOrEmpty(this.Detail) && this.fetchingTitle;
	}

	// Token: 0x06001BE2 RID: 7138 RVA: 0x000AB400 File Offset: 0x000A9600
	public void EncodeDescription()
	{
		if (string.IsNullOrEmpty(this.Detail))
		{
			return;
		}
		AugmentRootNode.OpenAIReq("ReformatCardText", this.Detail, new Action<string>(this.UpdateDetailText));
		this.fetchingDetail = true;
	}

	// Token: 0x06001BE3 RID: 7139 RVA: 0x000AB433 File Offset: 0x000A9633
	private void UpdateDetailText(string val)
	{
		this.Detail = val;
		this.fetchingDetail = false;
	}

	// Token: 0x06001BE4 RID: 7140 RVA: 0x000AB443 File Offset: 0x000A9643
	private string DetailButtonText()
	{
		if (!this.fetchingDetail)
		{
			return "Encode Description";
		}
		return "Encoding...";
	}

	// Token: 0x06001BE5 RID: 7141 RVA: 0x000AB458 File Offset: 0x000A9658
	private bool AlreadyEncoded()
	{
		return string.IsNullOrEmpty(this.Detail) || this.Detail.Contains("$");
	}

	// Token: 0x06001BE6 RID: 7142 RVA: 0x000AB479 File Offset: 0x000A9679
	public static void OpenAIReq(string command, string input, Action<string> onComplete)
	{
	}

	// Token: 0x06001BE7 RID: 7143 RVA: 0x000AB47B File Offset: 0x000A967B
	private void ClearCache()
	{
		this._passiveCache = null;
		this._snippetMatches = null;
	}

	// Token: 0x06001BE8 RID: 7144 RVA: 0x000AB48C File Offset: 0x000A968C
	public AugmentRootNode()
	{
	}

	// Token: 0x04001C2F RID: 7215
	[InputPort(typeof(LogicNode), false, "Requirements", PortLocation.Vertical)]
	[HideInInspector]
	[SerializeField]
	public Node Requirements;

	// Token: 0x04001C30 RID: 7216
	[SearchContext("t:Sprite -l:Editor", "assets", SearchViewFlags.GridView)]
	public Sprite Icon;

	// Token: 0x04001C31 RID: 7217
	public string Name;

	// Token: 0x04001C32 RID: 7218
	[Space]
	[TextArea(3, 5)]
	public string Detail;

	// Token: 0x04001C33 RID: 7219
	public ModType modType;

	// Token: 0x04001C34 RID: 7220
	public Rarity Rarity;

	// Token: 0x04001C35 RID: 7221
	public AugmentQuality DisplayQuality;

	// Token: 0x04001C36 RID: 7222
	public bool SendWithAbility;

	// Token: 0x04001C37 RID: 7223
	[Tooltip("Visual range to be indicated around player")]
	public float DisplayRadius;

	// Token: 0x04001C38 RID: 7224
	[Tooltip("When checked, this will only apply to abilities that contain this augment in their effect properties")]
	public bool OnlyInProps;

	// Token: 0x04001C39 RID: 7225
	public AugmentTree DmgCreditOverride;

	// Token: 0x04001C3A RID: 7226
	public MagicColor magicColor;

	// Token: 0x04001C3B RID: 7227
	[Tooltip("Determines when this passive will be calculated, later levels apply on previous values")]
	public int Priority;

	// Token: 0x04001C3C RID: 7228
	public bool Raid = true;

	// Token: 0x04001C3D RID: 7229
	public bool ShowAsStatus;

	// Token: 0x04001C3E RID: 7230
	public bool StartUnlocked = true;

	// Token: 0x04001C3F RID: 7231
	public bool PriorityUnlock;

	// Token: 0x04001C40 RID: 7232
	public bool NoOmit;

	// Token: 0x04001C41 RID: 7233
	public EnemyLevel ApplyTo = EnemyLevel.All;

	// Token: 0x04001C42 RID: 7234
	public EnemyType ApplyType;

	// Token: 0x04001C43 RID: 7235
	public bool ApplyPlayerTeam;

	// Token: 0x04001C44 RID: 7236
	public bool ApplyToWorld;

	// Token: 0x04001C45 RID: 7237
	[Range(0f, 100f)]
	[SerializeField]
	private int Difficulty = 50;

	// Token: 0x04001C46 RID: 7238
	[SerializeField]
	public int EMinHeat;

	// Token: 0x04001C47 RID: 7239
	public List<EnemyModTag> EnemyTags = new List<EnemyModTag>();

	// Token: 0x04001C48 RID: 7240
	public int InkCost = 3;

	// Token: 0x04001C49 RID: 7241
	public SpawnType SpawnType = SpawnType.None;

	// Token: 0x04001C4A RID: 7242
	public bool ApplyToEnemies;

	// Token: 0x04001C4B RID: 7243
	public bool ApplyToPlayers;

	// Token: 0x04001C4C RID: 7244
	public bool RunLocally;

	// Token: 0x04001C4D RID: 7245
	public int HeatLevel = 1;

	// Token: 0x04001C4E RID: 7246
	public GenreTree Tome;

	// Token: 0x04001C4F RID: 7247
	public float MinHeat;

	// Token: 0x04001C50 RID: 7248
	public AugmentFilter Filter;

	// Token: 0x04001C51 RID: 7249
	public List<ModTag> AddedTags = new List<ModTag>();

	// Token: 0x04001C52 RID: 7250
	public ModFilterList Filters = new ModFilterList();

	// Token: 0x04001C53 RID: 7251
	public List<AugmentRootNode.PoolObject> PoolObjects = new List<AugmentRootNode.PoolObject>();

	// Token: 0x04001C54 RID: 7252
	[OutputPort(typeof(ModPassiveNode), true, "Passives", PortLocation.Default)]
	[HideInInspector]
	[SerializeField]
	public List<Node> Passives = new List<Node>();

	// Token: 0x04001C55 RID: 7253
	[OutputPort(typeof(ModSnippetNode), true, "Snippets", PortLocation.Default)]
	[HideInInspector]
	[SerializeField]
	public List<Node> Snippets = new List<Node>();

	// Token: 0x04001C56 RID: 7254
	[OutputPort(typeof(ModOverrideNode), true, "Overrides", PortLocation.Default)]
	[HideInInspector]
	[SerializeField]
	public List<Node> Overrides = new List<Node>();

	// Token: 0x04001C57 RID: 7255
	[ShowPort("ShowDifficultyModPort")]
	[OutputPort(typeof(ModDifficultyNode), false, "Difficulty Modifiers", PortLocation.Default)]
	[HideInInspector]
	[SerializeField]
	public Node DifficultyMod;

	// Token: 0x04001C58 RID: 7256
	private HashSet<int> _passiveCache;

	// Token: 0x04001C59 RID: 7257
	private HashSet<EventTrigger> _snippetMatches;

	// Token: 0x04001C5A RID: 7258
	private bool fetchingTitle;

	// Token: 0x04001C5B RID: 7259
	private bool fetchingDetail;

	// Token: 0x02000663 RID: 1635
	[Serializable]
	public class PoolObject
	{
		// Token: 0x060027BC RID: 10172 RVA: 0x000D6EC9 File Offset: 0x000D50C9
		public PoolObject()
		{
		}

		// Token: 0x04002B42 RID: 11074
		public GameObject Obj;

		// Token: 0x04002B43 RID: 11075
		public int Count = 1;
	}
}
