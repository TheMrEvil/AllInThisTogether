using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Photon.Pun;
using SimpleJSON;
using UnityEngine;

// Token: 0x02000265 RID: 613
public class EffectProperties
{
	// Token: 0x17000179 RID: 377
	// (get) Token: 0x0600187B RID: 6267 RVA: 0x00098F74 File Offset: 0x00097174
	// (set) Token: 0x0600187C RID: 6268 RVA: 0x00098F7C File Offset: 0x0009717C
	public EntityControl AffectedControl
	{
		[CompilerGenerated]
		get
		{
			return this.<AffectedControl>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			this.<AffectedControl>k__BackingField = value;
		}
	}

	// Token: 0x1700017A RID: 378
	// (get) Token: 0x0600187D RID: 6269 RVA: 0x00098F85 File Offset: 0x00097185
	// (set) Token: 0x0600187E RID: 6270 RVA: 0x00098F8D File Offset: 0x0009718D
	public EntityControl SeekTargetControl
	{
		[CompilerGenerated]
		get
		{
			return this.<SeekTargetControl>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			this.<SeekTargetControl>k__BackingField = value;
		}
	}

	// Token: 0x1700017B RID: 379
	// (get) Token: 0x0600187F RID: 6271 RVA: 0x00098F96 File Offset: 0x00097196
	// (set) Token: 0x06001880 RID: 6272 RVA: 0x00098F9E File Offset: 0x0009719E
	public EntityControl AllyTargetControl
	{
		[CompilerGenerated]
		get
		{
			return this.<AllyTargetControl>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			this.<AllyTargetControl>k__BackingField = value;
		}
	}

	// Token: 0x1700017C RID: 380
	// (get) Token: 0x06001881 RID: 6273 RVA: 0x00098FA7 File Offset: 0x000971A7
	// (set) Token: 0x06001882 RID: 6274 RVA: 0x00098FB0 File Offset: 0x000971B0
	public GameObject Affected
	{
		get
		{
			return this.affected;
		}
		set
		{
			this.affected = value;
			this.AffectedControl = ((this.affected != null) ? this.affected.GetComponent<EntityControl>() : null);
			if (this.affected != null && this.AffectedControl == null)
			{
				this.AffectedControl = this.affected.GetComponentInParent<EntityControl>();
			}
		}
	}

	// Token: 0x1700017D RID: 381
	// (get) Token: 0x06001883 RID: 6275 RVA: 0x00099013 File Offset: 0x00097213
	// (set) Token: 0x06001884 RID: 6276 RVA: 0x0009901B File Offset: 0x0009721B
	public GameObject SeekTarget
	{
		get
		{
			return this.seekTarget;
		}
		set
		{
			if (value == null)
			{
				this.SeekTargetControl = null;
				this.seekTarget = null;
				return;
			}
			this.SeekTargetControl = value.GetComponent<EntityControl>();
			this.seekTarget = value;
		}
	}

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x06001885 RID: 6277 RVA: 0x00099048 File Offset: 0x00097248
	// (set) Token: 0x06001886 RID: 6278 RVA: 0x00099050 File Offset: 0x00097250
	public GameObject AllyTarget
	{
		get
		{
			return this.allyTarget;
		}
		set
		{
			if (value == null)
			{
				this.AllyTargetControl = null;
				this.allyTarget = null;
				return;
			}
			this.AllyTargetControl = value.GetComponent<EntityControl>();
			this.allyTarget = value;
		}
	}

	// Token: 0x1700017F RID: 383
	// (get) Token: 0x06001887 RID: 6279 RVA: 0x00099080 File Offset: 0x00097280
	public bool IsLocal
	{
		get
		{
			if (this.IsWorld)
			{
				return !PhotonNetwork.InRoom || PhotonNetwork.IsMasterClient;
			}
			return this.SourceControl == null || this.SourceControl.view.IsMine || !PhotonNetwork.InRoom;
		}
	}

	// Token: 0x06001888 RID: 6280 RVA: 0x000990D0 File Offset: 0x000972D0
	public EffectProperties()
	{
		this.CreateRandomSeed(0, 0);
	}

	// Token: 0x06001889 RID: 6281 RVA: 0x00099154 File Offset: 0x00097354
	public EffectProperties(EntityControl source)
	{
		this.CastID = UnityEngine.Random.Range(0, int.MaxValue);
		this.CreateRandomSeed(0, 0);
		if (source == null)
		{
			return;
		}
		this.SourceControl = source;
		this.SourceTeam = source.TeamID;
		this.Affected = source.gameObject;
		if (source != null)
		{
			if (source.currentTarget != null)
			{
				GameObject gameObject;
				if (source == null)
				{
					gameObject = null;
				}
				else
				{
					EntityControl currentTarget = source.currentTarget;
					gameObject = ((currentTarget != null) ? currentTarget.gameObject : null);
				}
				this.SeekTarget = gameObject;
			}
			if (source.allyTarget != null)
			{
				GameObject gameObject2;
				if (source == null)
				{
					gameObject2 = null;
				}
				else
				{
					EntityControl entityControl = source.allyTarget;
					gameObject2 = ((entityControl != null) ? entityControl.gameObject : null);
				}
				this.AllyTarget = gameObject2;
			}
			this.StartLoc = (this.OutLoc = global::Pose.WorldPoint(source.display.CenterOfMass.position, source.display.CenterOfMass.forward));
		}
		this.AddEntityAugments(source, true);
	}

	// Token: 0x0600188A RID: 6282 RVA: 0x000992B0 File Offset: 0x000974B0
	public EffectProperties(string inpt)
	{
		inpt = inpt.NetworkDecompress();
		JSONNode jsonnode = JSON.Parse(inpt);
		int num = jsonnode.GetValueOrDefault("sid", -1);
		if (num >= 0)
		{
			this.SourceControl = EntityControl.GetEntity(num);
		}
		this.SourceTeam = jsonnode.GetValueOrDefault("stm", -1);
		this.IsWorld = jsonnode.GetValueOrDefault("ww", false);
		this.Depth = jsonnode.GetValueOrDefault("dpt", 0);
		this.StartLoc = new global::Pose(jsonnode.GetValueOrDefault("slc", "").ToString());
		this.OutLoc = new global::Pose(jsonnode.GetValueOrDefault("olc", "").ToString());
		this.InputID = jsonnode.GetValueOrDefault("iid", "");
		this.AbilityType = (PlayerAbilityType)jsonnode.GetValueOrDefault("atp", 5);
		this.SourceType = (ActionSource)jsonnode.GetValueOrDefault("stp", 0);
		this.Keyword = (Keyword)jsonnode.GetValueOrDefault("kw", 1024);
		this.Lifetime = jsonnode.GetValueOrDefault("lft", 0);
		this.GraphIDRef = jsonnode.GetValueOrDefault("gid", "");
		this.CastID = jsonnode.GetValueOrDefault("cst", 0);
		this.RandSeed = jsonnode.GetValueOrDefault("rng", 0);
		this.RandomIndex = jsonnode.GetValueOrDefault("rid", 0);
		this.CreateRandomSeed(this.RandSeed, 0);
		this.LocalIndex = jsonnode.GetValueOrDefault("lid", 0);
		if (jsonnode.HasKey("cs"))
		{
			string[] array = jsonnode.GetValueOrDefault("cs", "").Split('|', StringSplitOptions.None);
			if (array.Length == 2)
			{
				this.CauseName = array[0];
				this.CauseID = array[1];
			}
		}
		if (jsonnode.HasKey("ext"))
		{
			this.ExtraValues = jsonnode["ext"];
		}
		if (jsonnode.HasKey("aug"))
		{
			foreach (KeyValuePair<string, JSONNode> keyValuePair in (jsonnode["aug"] as JSONArray))
			{
				this.AddAugment(keyValuePair.Value.ToString().Replace("\"", ""), 1);
			}
		}
		if (jsonnode.HasKey("mn"))
		{
			foreach (KeyValuePair<string, JSONNode> keyValuePair2 in jsonnode["mn"])
			{
				int asInt = keyValuePair2.Value.AsInt;
				int num2;
				int.TryParse(keyValuePair2.Key, out num2);
				MagicColor magicColor = (MagicColor)num2;
				if (!this.ManaConsumed.ContainsKey(magicColor))
				{
					this.ManaConsumed.Add(magicColor, 0);
				}
				Dictionary<MagicColor, int> manaConsumed = this.ManaConsumed;
				MagicColor key = magicColor;
				manaConsumed[key] += asInt;
			}
		}
		if (jsonnode.HasKey("ls"))
		{
			foreach (KeyValuePair<string, JSONNode> keyValuePair3 in jsonnode["ls"])
			{
				Vector3 value = keyValuePair3.Value.ToString().ToVector3();
				string key2 = keyValuePair3.Key;
				this.CachedLocations.TryAdd(key2, value);
			}
		}
		if (jsonnode.HasKey("fs"))
		{
			foreach (KeyValuePair<string, JSONNode> keyValuePair4 in jsonnode["fs"])
			{
				float asFloat = keyValuePair4.Value.AsFloat;
				string key3 = keyValuePair4.Key;
				this.CachedFloats.TryAdd(key3, asFloat);
			}
		}
		if (jsonnode.HasKey("aid"))
		{
			EntityControl entity = EntityControl.GetEntity(jsonnode.GetValueOrDefault("aid", -1));
			this.Affected = ((entity != null) ? entity.gameObject : null);
		}
		if (jsonnode.HasKey("stid"))
		{
			EntityControl entity2 = EntityControl.GetEntity(jsonnode.GetValueOrDefault("stid", -1));
			this.SeekTarget = ((entity2 != null) ? entity2.gameObject : null);
		}
		if (jsonnode.HasKey("alid"))
		{
			EntityControl entity3 = EntityControl.GetEntity(jsonnode.GetValueOrDefault("alid", -1));
			this.AllyTarget = ((entity3 != null) ? entity3.gameObject : null);
		}
	}

	// Token: 0x0600188B RID: 6283 RVA: 0x000997F0 File Offset: 0x000979F0
	public override string ToString()
	{
		JSONNode jsonnode = new JSONObject();
		if (this.SourceControl != null)
		{
			JSONNode jsonnode2 = jsonnode;
			string aKey = "sid";
			int num = this.SourceControl.net.view.ViewID;
			jsonnode2.Add(aKey, num.ToString());
		}
		if (this.SourceTeam != -1)
		{
			jsonnode.Add("stm", this.SourceTeam);
		}
		jsonnode.Add("slc", this.StartLoc.ToJSON());
		jsonnode.Add("olc", this.OutLoc.ToJSON());
		if (this.AbilityType != PlayerAbilityType.Any)
		{
			jsonnode.Add("atp", (int)this.AbilityType);
		}
		if (this.SourceType != ActionSource._)
		{
			jsonnode.Add("stp", (int)this.SourceType);
		}
		if (this.Keyword != Keyword.None)
		{
			jsonnode.Add("kw", (int)this.Keyword);
		}
		if (this.Lifetime != 0f)
		{
			jsonnode.Add("lft", this.Lifetime);
		}
		if (this.CastID != 0)
		{
			jsonnode.Add("cst", this.CastID);
		}
		jsonnode.Add("rng", this.RandSeed);
		if (this.RandomIndex != 0)
		{
			jsonnode.Add("rid", this.RandomIndex);
		}
		if (this.LocalIndex != 0)
		{
			jsonnode.Add("lid", this.LocalIndex);
		}
		if (!string.IsNullOrEmpty(this.InputID))
		{
			jsonnode.Add("iid", this.InputID);
		}
		if (this.IsWorld)
		{
			jsonnode.Add("ww", this.IsWorld);
		}
		if (this.Depth != 0)
		{
			jsonnode.Add("dpt", this.Depth);
		}
		if (!string.IsNullOrEmpty(this.CauseName) || !string.IsNullOrEmpty(this.CauseID))
		{
			jsonnode.Add("cs", this.CauseName + "|" + this.CauseID);
		}
		if (this.EffectAugments != null && this.EffectAugments.trees.Count > 0)
		{
			JSONArray jsonarray = new JSONArray();
			foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.EffectAugments.trees)
			{
				int num;
				AugmentRootNode augmentRootNode;
				keyValuePair.Deconstruct(out augmentRootNode, out num);
				AugmentRootNode augmentRootNode2 = augmentRootNode;
				jsonarray.Add(augmentRootNode2.guid);
			}
			jsonnode.Add("aug", jsonarray);
		}
		if (this.AffectedControl != null)
		{
			jsonnode.Add("aid", this.AffectedControl.net.view.ViewID);
		}
		if (this.SeekTargetControl != null)
		{
			jsonnode.Add("stid", this.SeekTargetControl.net.view.ViewID);
		}
		if (this.AllyTargetControl != null)
		{
			jsonnode.Add("alid", this.AllyTargetControl.net.view.ViewID);
		}
		if (this.ExtraValues.Count > 0)
		{
			jsonnode.Add("ext", this.ExtraValues);
		}
		if (!string.IsNullOrEmpty(this.GraphIDRef))
		{
			jsonnode.Add("gid", this.GraphIDRef);
		}
		if (this.ManaConsumed.Count > 0)
		{
			JSONNode jsonnode3 = new JSONObject();
			foreach (KeyValuePair<MagicColor, int> keyValuePair2 in this.ManaConsumed)
			{
				JSONNode jsonnode4 = jsonnode3;
				int num = (int)keyValuePair2.Key;
				jsonnode4.Add(num.ToString(), keyValuePair2.Value);
			}
			jsonnode.Add("mn", jsonnode3);
		}
		if (this.CachedLocations.Count > 0)
		{
			JSONNode jsonnode5 = new JSONObject();
			foreach (KeyValuePair<string, Vector3> keyValuePair3 in this.CachedLocations)
			{
				jsonnode5.Add(keyValuePair3.Key, keyValuePair3.Value.ToDetailedString());
			}
			jsonnode.Add("ls", jsonnode5);
		}
		if (this.CachedFloats.Count > 0)
		{
			JSONNode jsonnode6 = new JSONObject();
			foreach (KeyValuePair<string, float> keyValuePair4 in this.CachedFloats)
			{
				jsonnode6.Add(keyValuePair4.Key, keyValuePair4.Value);
			}
			jsonnode.Add("fs", jsonnode6);
		}
		return jsonnode.ToString().NetworkCompress();
	}

	// Token: 0x0600188C RID: 6284 RVA: 0x00099D0C File Offset: 0x00097F0C
	public void OverrideSeed(int seed, int randIndex = 0)
	{
		this.CreateRandomSeed(seed, randIndex);
	}

	// Token: 0x0600188D RID: 6285 RVA: 0x00099D18 File Offset: 0x00097F18
	private void CreateRandomSeed(int seedOverride = 0, int indexAt = 0)
	{
		if (seedOverride == 0)
		{
			seedOverride = UnityEngine.Random.Range(0, int.MaxValue);
		}
		this.RandSeed = seedOverride;
		this.random = new System.Random(this.RandSeed);
		if (indexAt > 0)
		{
			indexAt %= 5000;
			for (int i = 0; i < indexAt; i++)
			{
				this.random.Next();
			}
		}
	}

	// Token: 0x0600188E RID: 6286 RVA: 0x00099D74 File Offset: 0x00097F74
	public void SetEntity(ApplyOn eType, EntityControl entity)
	{
		GameObject gameObject = null;
		if (entity != null)
		{
			gameObject = entity.gameObject;
		}
		switch (eType)
		{
		case ApplyOn.Source:
			this.SourceControl = entity;
			return;
		case ApplyOn.Affected:
			this.Affected = gameObject;
			return;
		case ApplyOn.SeekTarget:
			this.SeekTarget = gameObject;
			return;
		case ApplyOn.AllyTarget:
			this.AllyTarget = gameObject;
			return;
		default:
			return;
		}
	}

	// Token: 0x0600188F RID: 6287 RVA: 0x00099DCC File Offset: 0x00097FCC
	public void AddEntityAugments(EntityControl control, bool fresh)
	{
		if (fresh || this.EffectAugments == null)
		{
			this.EffectAugments = new Augments();
		}
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in control.AllAugments(true, null).trees)
		{
			if (keyValuePair.Key.SendWithAbility && keyValuePair.Key.HasRequirements(this))
			{
				this.AddAugment(keyValuePair.Key.guid, keyValuePair.Value);
			}
		}
	}

	// Token: 0x06001890 RID: 6288 RVA: 0x00099E6C File Offset: 0x0009806C
	public void ClearManaData()
	{
		foreach (KeyValuePair<MagicColor, int> keyValuePair in this.ManaConsumed)
		{
			MagicColor magicColor;
			int num;
			keyValuePair.Deconstruct(out magicColor, out num);
			MagicColor magicColor2 = magicColor;
			int value = num;
			GameDB.ElementInfo element = GameDB.GetElement(magicColor2);
			if (!(element.AbilityAugment == null))
			{
				this.RemoveAugment(element.AbilityAugment.Root.guid, value);
			}
		}
		this.ManaConsumed.Clear();
	}

	// Token: 0x06001891 RID: 6289 RVA: 0x00099F00 File Offset: 0x00098100
	public void AddMana(MagicColor magicColor, int count)
	{
		this.ManaConsumed.TryAdd(magicColor, 0);
		Dictionary<MagicColor, int> manaConsumed = this.ManaConsumed;
		manaConsumed[magicColor] += count;
	}

	// Token: 0x06001892 RID: 6290 RVA: 0x00099F34 File Offset: 0x00098134
	public void AddAugment(string key, int value)
	{
		if (key == null || key.Length <= 1)
		{
			return;
		}
		AugmentTree augment = GraphDB.GetAugment(key);
		if (augment == null)
		{
			Debug.LogError("No augment found with GUID: " + key);
			return;
		}
		if (this.EffectAugments == null)
		{
			this.EffectAugments = new Augments();
		}
		this.EffectAugments.Add(augment, value);
	}

	// Token: 0x06001893 RID: 6291 RVA: 0x00099F94 File Offset: 0x00098194
	public void RemoveAugment(string key, int value)
	{
		if (key == null || key.Length <= 1)
		{
			return;
		}
		AugmentTree augment = GraphDB.GetAugment(key);
		if (augment == null)
		{
			Debug.LogError("No augment found with GUID: " + key);
			return;
		}
		if (this.EffectAugments == null)
		{
			this.EffectAugments = new Augments();
		}
		this.EffectAugments.Remove(augment, value);
	}

	// Token: 0x06001894 RID: 6292 RVA: 0x00099FEF File Offset: 0x000981EF
	public float ModifyEntityPassive(Passive.EntityValue valueType, float startValue)
	{
		return this.ModifyValue(new EntityPassive(valueType), startValue, false);
	}

	// Token: 0x06001895 RID: 6293 RVA: 0x0009A000 File Offset: 0x00098200
	public float ModifyAbilityPassives(Passive.AbilityValue valueType, float startVal)
	{
		this.SetExtra(EProp.Passive_Input, startVal);
		float num = this.ModifyValue(new AbilityPassive(this.AbilityType, valueType), startVal, false);
		if (this.Keyword != Keyword.None)
		{
			num = this.ModifyValue(new KeywordPassive(this.Keyword, valueType), num, false);
		}
		return num;
	}

	// Token: 0x06001896 RID: 6294 RVA: 0x0009A050 File Offset: 0x00098250
	public float ModifyValue(Passive passive, float startVal, bool difference = false)
	{
		EffectProperties.checkedTrees.Clear();
		float num = startVal;
		if (this.EffectAugments != null)
		{
			foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.EffectAugments.trees)
			{
				EffectProperties.checkedTrees.Add(keyValuePair.Key);
				if (keyValuePair.Key.HasPassive(passive))
				{
					num += keyValuePair.Key.ModifyPassive(this.SourceControl, this, passive, startVal, keyValuePair.Value, true);
				}
			}
		}
		foreach (KeyValuePair<MagicColor, int> keyValuePair2 in this.ManaConsumed)
		{
			AugmentTree abilityAugment = GameDB.GetElement(keyValuePair2.Key).AbilityAugment;
			if (!(abilityAugment == null))
			{
				if (keyValuePair2.Value > 1)
				{
					this.SetExtra(EProp.DynamicInput, (float)keyValuePair2.Value);
				}
				num += abilityAugment.Root.ModifyPassive(this.SourceControl, this, passive, startVal, 1, true);
			}
		}
		if (this.SourceControl != null && this.SourceControl.HasPassiveMod(passive, true))
		{
			this.SourceControl.GetPassiveAugments(passive, false, EffectProperties.augmentCache);
			if (this.sorted != null)
			{
				this.sorted.Clear();
			}
			else
			{
				this.sorted = new SortedList<int, List<KeyValuePair<AugmentRootNode, int>>>();
			}
			foreach (KeyValuePair<AugmentRootNode, int> item in EffectProperties.augmentCache.trees)
			{
				if (!EffectProperties.checkedTrees.Contains(item.Key) && (!item.Key.SendWithAbility || !item.Key.OnlyInProps))
				{
					int priority = item.Key.Priority;
					if (!this.sorted.ContainsKey(priority))
					{
						this.sorted[priority] = new List<KeyValuePair<AugmentRootNode, int>>();
					}
					this.sorted[priority].Add(item);
				}
			}
			foreach (KeyValuePair<int, List<KeyValuePair<AugmentRootNode, int>>> keyValuePair3 in this.sorted)
			{
				startVal = num;
				foreach (KeyValuePair<AugmentRootNode, int> keyValuePair4 in keyValuePair3.Value)
				{
					num += keyValuePair4.Key.ModifyPassive(this.SourceControl, this, passive, startVal, keyValuePair4.Value, false);
				}
			}
		}
		if (difference)
		{
			return num - startVal;
		}
		return num;
	}

	// Token: 0x06001897 RID: 6295 RVA: 0x0009A330 File Offset: 0x00098530
	public float GetScalarValue(Passive APassive)
	{
		float num = 1f;
		if (this.EffectAugments != null)
		{
			foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.EffectAugments.trees)
			{
				if (keyValuePair.Key.HasPassive(APassive))
				{
					num += keyValuePair.Key.GetPassiveScalar(this.SourceControl, APassive, this, keyValuePair.Value, false);
				}
			}
		}
		return num;
	}

	// Token: 0x06001898 RID: 6296 RVA: 0x0009A3C0 File Offset: 0x000985C0
	public void TriggerDamageDone(ApplyOn SnippetTarget, DamageInfo dmg)
	{
		EntityControl applicationEntity = this.GetApplicationEntity(SnippetTarget);
		if (applicationEntity == null || dmg == null)
		{
			return;
		}
		EffectProperties effectProperties = applicationEntity.DamageDone(dmg);
		if (effectProperties != null)
		{
			effectProperties.Depth = this.Depth;
		}
		float snippetChance = dmg.SnippetChance;
		if (!this.HasExtra(EProp.Snip_DidHit))
		{
			this.SetExtra(EProp.Snip_DidHit, 1f);
			this.ApplyEffectSnippets(applicationEntity, EventTrigger.AbilityHit, ((effectProperties != null) ? effectProperties.Copy(false) : null) ?? null, snippetChance);
			this.TryAbilityFirstHit();
		}
		if (dmg.DamageType == DNumType.Crit)
		{
			this.ApplyEffectSnippets(applicationEntity, EventTrigger.CriticalDone, ((effectProperties != null) ? effectProperties.Copy(false) : null) ?? null, snippetChance);
		}
		this.ApplyEffectSnippets(applicationEntity, EventTrigger.DamageDone, effectProperties, snippetChance);
	}

	// Token: 0x06001899 RID: 6297 RVA: 0x0009A46D File Offset: 0x0009866D
	public void TryAbilityFirstHit()
	{
		if (this.SourceControl == null)
		{
			return;
		}
		if (this.SourceControl.TryConsumeFirstHitEvent(this))
		{
			this.ApplyEffectSnippets(this.SourceControl, EventTrigger.PlayerAbilityFirstHit, this.Copy(false), 1f);
		}
	}

	// Token: 0x0600189A RID: 6298 RVA: 0x0009A4A8 File Offset: 0x000986A8
	private void ApplyEffectSnippets(EntityControl control, EventTrigger trigger, EffectProperties properties = null, float chance = 1f)
	{
		if (!control.IsMine && !trigger.IsLocalTrigger())
		{
			return;
		}
		if (this.EffectAugments != null)
		{
			foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.EffectAugments.trees)
			{
				AugmentRootNode augmentRootNode;
				int num;
				keyValuePair.Deconstruct(out augmentRootNode, out num);
				AugmentRootNode augmentRootNode2 = augmentRootNode;
				int num2 = num;
				if (augmentRootNode2.SnippetMatches.Contains(trigger))
				{
					for (int i = 0; i < num2; i++)
					{
						augmentRootNode2.TryTrigger(control, trigger, properties, chance);
					}
				}
			}
		}
		foreach (KeyValuePair<MagicColor, int> keyValuePair2 in this.ManaConsumed)
		{
			int num;
			MagicColor magicColor;
			keyValuePair2.Deconstruct(out magicColor, out num);
			MagicColor magicColor2 = magicColor;
			int num3 = num;
			AugmentTree abilityAugment = GameDB.GetElement(magicColor2).AbilityAugment;
			if (!(abilityAugment == null))
			{
				EffectProperties effectProperties = this;
				if (num3 > 1)
				{
					effectProperties = this.Copy(false);
					effectProperties.SetExtra(EProp.DynamicInput, (float)num3);
				}
				abilityAugment.Root.TryTrigger(control, trigger, effectProperties, chance);
			}
		}
	}

	// Token: 0x0600189B RID: 6299 RVA: 0x0009A5E0 File Offset: 0x000987E0
	public bool HasModTag(ModTag tag)
	{
		Augments effectAugments = this.EffectAugments;
		if (effectAugments != null && effectAugments.GetTags().Contains(tag))
		{
			return true;
		}
		foreach (KeyValuePair<MagicColor, int> keyValuePair in this.ManaConsumed)
		{
			AugmentTree abilityAugment = GameDB.GetElement(keyValuePair.Key).AbilityAugment;
			if (!(abilityAugment == null))
			{
				using (List<ModTag>.Enumerator enumerator2 = abilityAugment.Root.AddedTags.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.Equals(tag))
						{
							return true;
						}
					}
				}
			}
		}
		if (this.SourceControl != null)
		{
			foreach (KeyValuePair<AugmentRootNode, int> keyValuePair2 in this.SourceControl.AllAugments(true, null).trees)
			{
				if ((!keyValuePair2.Key.SendWithAbility || !keyValuePair2.Key.OnlyInProps) && keyValuePair2.Key.AddedTags.Contains(tag))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600189C RID: 6300 RVA: 0x0009A744 File Offset: 0x00098944
	public void SaveLocation(string id, Vector3 loc)
	{
		this.CachedLocations.TryAdd(id, loc);
		this.CachedLocations[id] = loc;
	}

	// Token: 0x0600189D RID: 6301 RVA: 0x0009A764 File Offset: 0x00098964
	public Vector3 GetCachedLocation(string id)
	{
		Vector3 result;
		if (this.CachedLocations.TryGetValue(id, out result))
		{
			return result;
		}
		return Vector3.one.INVALID();
	}

	// Token: 0x0600189E RID: 6302 RVA: 0x0009A78D File Offset: 0x0009898D
	public void SaveFloat(string id, float v)
	{
		this.CachedFloats.TryAdd(id, v);
		this.CachedFloats[id] = v;
	}

	// Token: 0x0600189F RID: 6303 RVA: 0x0009A7AC File Offset: 0x000989AC
	public bool HasEffectProp(EProp prop)
	{
		JSONNode extraValues = this.ExtraValues;
		int num = (int)prop;
		return extraValues.HasKey(num.ToString());
	}

	// Token: 0x060018A0 RID: 6304 RVA: 0x0009A7D0 File Offset: 0x000989D0
	public float GetFloat(string id)
	{
		float result;
		if (this.CachedFloats.TryGetValue(id, out result))
		{
			return result;
		}
		return float.NaN;
	}

	// Token: 0x060018A1 RID: 6305 RVA: 0x0009A7F4 File Offset: 0x000989F4
	public void SetExtra(EProp key, float val)
	{
		int num = (int)key;
		string aKey = num.ToString();
		if (this.ExtraValues.HasKey(aKey))
		{
			this.ExtraValues[aKey] = val;
			return;
		}
		this.ExtraValues.Add(aKey, val);
	}

	// Token: 0x060018A2 RID: 6306 RVA: 0x0009A83E File Offset: 0x00098A3E
	public void Increment(EProp key, int value = 1)
	{
		if (this.HasExtra(key))
		{
			this.SetExtra(key, this.GetExtra(key, 0f) + (float)value);
			return;
		}
		this.SetExtra(key, (float)value);
	}

	// Token: 0x060018A3 RID: 6307 RVA: 0x0009A86C File Offset: 0x00098A6C
	public bool HasExtra(EProp key)
	{
		JSONNode extraValues = this.ExtraValues;
		int num = (int)key;
		return extraValues.HasKey(num.ToString());
	}

	// Token: 0x060018A4 RID: 6308 RVA: 0x0009A890 File Offset: 0x00098A90
	public float GetExtra(EProp key, float defaultVal)
	{
		JSONNode extraValues = this.ExtraValues;
		int num = (int)key;
		return extraValues.GetValueOrDefault(num.ToString(), defaultVal);
	}

	// Token: 0x060018A5 RID: 6309 RVA: 0x0009A8BC File Offset: 0x00098ABC
	public float GetExtra(EProp key, string ID, float defaultVal)
	{
		if (key != EProp.ExplicitStacks)
		{
			return defaultVal;
		}
		EntityControl sourceControl = this.SourceControl;
		EntityControl.AppliedStatus appliedStatus = (sourceControl != null) ? sourceControl.GetStatusInfo(ID) : null;
		if (appliedStatus == null)
		{
			return defaultVal;
		}
		return (float)appliedStatus.Stacks;
	}

	// Token: 0x060018A6 RID: 6310 RVA: 0x0009A8EF File Offset: 0x00098AEF
	public bool CanInteractWith(EntityControl entity, EffectInteractsWith mask)
	{
		if (this.IsWorld)
		{
			return EntityControl.CanInteractWith(this.SourceType, entity, mask);
		}
		if (this.SourceControl != null)
		{
			return EntityControl.CanInteractWith(this, entity, mask);
		}
		return EntityControl.CanInteractWith(entity, this.SourceTeam, mask);
	}

	// Token: 0x060018A7 RID: 6311 RVA: 0x0009A92C File Offset: 0x00098B2C
	public float RandomFloat(float min, float max)
	{
		int num = 10000;
		float num2 = (float)this.random.Next(0, num) / (float)num;
		return min + num2 * (max - min);
	}

	// Token: 0x060018A8 RID: 6312 RVA: 0x0009A958 File Offset: 0x00098B58
	public int RandomInt(int min, int max)
	{
		return this.random.Next(min, max);
	}

	// Token: 0x060018A9 RID: 6313 RVA: 0x0009A968 File Offset: 0x00098B68
	public Vector2 RandomInsideUnitCircle(float minRadius = 0f)
	{
		int num = this.random.Next();
		int num2 = num & 32767;
		float num3 = (float)(num >> 15);
		float num4 = (float)num2 * 3.0517578E-05f;
		float num5 = num3 * 1.5258789E-05f;
		float f = num4 * 3.1415927f * 2f;
		float num6 = minRadius * minRadius;
		float num7 = Mathf.Sqrt(num6 + (1f - num6) * num5);
		float num8 = Mathf.Cos(f);
		float num9 = Mathf.Sin(f);
		return new Vector2(num8 * num7, num9 * num7);
	}

	// Token: 0x060018AA RID: 6314 RVA: 0x0009A9E0 File Offset: 0x00098BE0
	public Vector3 RandomInsideUnitSphere(float minRadius = 0f)
	{
		int num = this.random.Next();
		int num2 = num & 1023;
		int num3 = num >> 10 & 1023;
		float num4 = (float)(num >> 20 & 2047);
		float num5 = (float)num2 * 0.0009765625f;
		float num6 = (float)num3 * 0.0009765625f;
		float num7 = num4 * 0.00048828125f;
		float f = num5 * 6.2831855f;
		float f2 = Mathf.Acos(2f * num6 - 1f);
		float num8 = Mathf.Pow(minRadius + (1f - minRadius) * num7, 0.3333f);
		float num9 = Mathf.Sin(f2);
		float x = num8 * num9 * Mathf.Cos(f);
		float y = num8 * num9 * Mathf.Sin(f);
		float z = num8 * Mathf.Cos(f2);
		return new Vector3(x, y, z);
	}

	// Token: 0x060018AB RID: 6315 RVA: 0x0009AA9C File Offset: 0x00098C9C
	public EntityControl GetApplicationEntity(ApplyOn applyOn)
	{
		switch (applyOn)
		{
		case ApplyOn.Source:
			return this.SourceControl;
		case ApplyOn.Affected:
			return this.AffectedControl;
		case ApplyOn.SeekTarget:
			return this.SeekTargetControl;
		case ApplyOn.AllyTarget:
			return this.AllyTargetControl;
		default:
			return null;
		}
	}

	// Token: 0x060018AC RID: 6316 RVA: 0x0009AAD4 File Offset: 0x00098CD4
	public EffectProperties Copy(bool breakCache = false)
	{
		EffectProperties effectProperties = base.MemberwiseClone() as EffectProperties;
		effectProperties.EffectAugments = new Augments(this.EffectAugments);
		effectProperties.StartLoc = this.StartLoc;
		effectProperties.OutLoc = this.OutLoc;
		effectProperties.random = this.random;
		effectProperties.ExtraValues = this.ExtraValues.Clone();
		if (breakCache)
		{
			effectProperties.CachedFloats = this.CachedFloats.ToDictionary((KeyValuePair<string, float> entry) => entry.Key, (KeyValuePair<string, float> entry) => entry.Value);
			effectProperties.CachedLocations = this.CachedLocations.ToDictionary((KeyValuePair<string, Vector3> entry) => entry.Key, (KeyValuePair<string, Vector3> entry) => entry.Value);
			effectProperties.ManaConsumed = this.ManaConsumed.ToDictionary((KeyValuePair<MagicColor, int> entry) => entry.Key, (KeyValuePair<MagicColor, int> entry) => entry.Value);
		}
		return effectProperties;
	}

	// Token: 0x060018AD RID: 6317 RVA: 0x0009AC27 File Offset: 0x00098E27
	public Vector3 GetOrigin()
	{
		if (this.StartLoc == null)
		{
			Debug.LogError("StartLoc does not exist!");
			return Vector3.one.INVALID();
		}
		return this.StartLoc.GetPosition(this);
	}

	// Token: 0x060018AE RID: 6318 RVA: 0x0009AC52 File Offset: 0x00098E52
	public Vector3 GetOriginForward()
	{
		if (this.StartLoc == null)
		{
			Debug.LogError("StartLoc does not exist!");
			return Vector3.one.INVALID();
		}
		return this.StartLoc.GetLookDirection(this);
	}

	// Token: 0x060018AF RID: 6319 RVA: 0x0009AC7D File Offset: 0x00098E7D
	[return: TupleElementNames(new string[]
	{
		"origin",
		"forward"
	})]
	public ValueTuple<Vector3, Vector3> GetOriginVectors()
	{
		if (this.StartLoc == null)
		{
			Debug.LogError("StartLoc does not exist!");
			return new ValueTuple<Vector3, Vector3>(Vector3.one.INVALID(), Vector3.one.INVALID());
		}
		return this.StartLoc.GetData(this);
	}

	// Token: 0x060018B0 RID: 6320 RVA: 0x0009ACB7 File Offset: 0x00098EB7
	public Vector3 GetOutputPoint()
	{
		return this.OutLoc.GetPosition(this);
	}

	// Token: 0x060018B1 RID: 6321 RVA: 0x0009ACC5 File Offset: 0x00098EC5
	public Vector3 GetOutputForward()
	{
		return this.OutLoc.GetLookDirection(this);
	}

	// Token: 0x060018B2 RID: 6322 RVA: 0x0009ACD3 File Offset: 0x00098ED3
	[return: TupleElementNames(new string[]
	{
		"origin",
		"forward"
	})]
	public ValueTuple<Vector3, Vector3> GetOutputVectors()
	{
		if (this.StartLoc == null)
		{
			Debug.LogError("StartLoc does not exist!");
			return new ValueTuple<Vector3, Vector3>(Vector3.one.INVALID(), Vector3.one.INVALID());
		}
		return this.StartLoc.GetData(this);
	}

	// Token: 0x060018B3 RID: 6323 RVA: 0x0009AD0D File Offset: 0x00098F0D
	// Note: this type is marked as 'beforefieldinit'.
	static EffectProperties()
	{
	}

	// Token: 0x0400185C RID: 6236
	public EntityControl SourceControl;

	// Token: 0x0400185D RID: 6237
	public int SourceTeam = -1;

	// Token: 0x0400185E RID: 6238
	[CompilerGenerated]
	private EntityControl <AffectedControl>k__BackingField;

	// Token: 0x0400185F RID: 6239
	[CompilerGenerated]
	private EntityControl <SeekTargetControl>k__BackingField;

	// Token: 0x04001860 RID: 6240
	[CompilerGenerated]
	private EntityControl <AllyTargetControl>k__BackingField;

	// Token: 0x04001861 RID: 6241
	private GameObject affected;

	// Token: 0x04001862 RID: 6242
	private GameObject seekTarget;

	// Token: 0x04001863 RID: 6243
	private GameObject allyTarget;

	// Token: 0x04001864 RID: 6244
	public global::Pose StartLoc = new global::Pose();

	// Token: 0x04001865 RID: 6245
	public Transform SourceLocation;

	// Token: 0x04001866 RID: 6246
	public float Lifetime;

	// Token: 0x04001867 RID: 6247
	public string CauseName;

	// Token: 0x04001868 RID: 6248
	public string CauseID;

	// Token: 0x04001869 RID: 6249
	public global::Pose OutLoc = new global::Pose();

	// Token: 0x0400186A RID: 6250
	public ActionSource SourceType;

	// Token: 0x0400186B RID: 6251
	public PlayerAbilityType AbilityType = PlayerAbilityType.None;

	// Token: 0x0400186C RID: 6252
	public EffectSource EffectSource;

	// Token: 0x0400186D RID: 6253
	public Keyword Keyword = Keyword.None;

	// Token: 0x0400186E RID: 6254
	public int Depth;

	// Token: 0x0400186F RID: 6255
	public string GraphIDRef;

	// Token: 0x04001870 RID: 6256
	public Augments EffectAugments;

	// Token: 0x04001871 RID: 6257
	public JSONNode ExtraValues = new JSONObject();

	// Token: 0x04001872 RID: 6258
	public Dictionary<MagicColor, int> ManaConsumed = new Dictionary<MagicColor, int>();

	// Token: 0x04001873 RID: 6259
	public Dictionary<string, Vector3> CachedLocations = new Dictionary<string, Vector3>();

	// Token: 0x04001874 RID: 6260
	public Dictionary<string, float> CachedFloats = new Dictionary<string, float>();

	// Token: 0x04001875 RID: 6261
	[NonSerialized]
	public int CastID;

	// Token: 0x04001876 RID: 6262
	[NonSerialized]
	public int RandSeed;

	// Token: 0x04001877 RID: 6263
	[NonSerialized]
	private int RandomIndex;

	// Token: 0x04001878 RID: 6264
	[NonSerialized]
	public int LocalIndex;

	// Token: 0x04001879 RID: 6265
	private System.Random random;

	// Token: 0x0400187A RID: 6266
	public string InputID = "";

	// Token: 0x0400187B RID: 6267
	public bool IsWorld;

	// Token: 0x0400187C RID: 6268
	private static List<AugmentRootNode> checkedTrees = new List<AugmentRootNode>();

	// Token: 0x0400187D RID: 6269
	private static Augments augmentCache = new Augments();

	// Token: 0x0400187E RID: 6270
	private SortedList<int, List<KeyValuePair<AugmentRootNode, int>>> sorted;

	// Token: 0x0200062A RID: 1578
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x06002791 RID: 10129 RVA: 0x000D6764 File Offset: 0x000D4964
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000D6770 File Offset: 0x000D4970
		public <>c()
		{
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000D6778 File Offset: 0x000D4978
		internal string <Copy>b__91_0(KeyValuePair<string, float> entry)
		{
			return entry.Key;
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x000D6781 File Offset: 0x000D4981
		internal float <Copy>b__91_1(KeyValuePair<string, float> entry)
		{
			return entry.Value;
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x000D678A File Offset: 0x000D498A
		internal string <Copy>b__91_2(KeyValuePair<string, Vector3> entry)
		{
			return entry.Key;
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000D6793 File Offset: 0x000D4993
		internal Vector3 <Copy>b__91_3(KeyValuePair<string, Vector3> entry)
		{
			return entry.Value;
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x000D679C File Offset: 0x000D499C
		internal MagicColor <Copy>b__91_4(KeyValuePair<MagicColor, int> entry)
		{
			return entry.Key;
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x000D67A5 File Offset: 0x000D49A5
		internal int <Copy>b__91_5(KeyValuePair<MagicColor, int> entry)
		{
			return entry.Value;
		}

		// Token: 0x04002A1A RID: 10778
		public static readonly EffectProperties.<>c <>9 = new EffectProperties.<>c();

		// Token: 0x04002A1B RID: 10779
		public static Func<KeyValuePair<string, float>, string> <>9__91_0;

		// Token: 0x04002A1C RID: 10780
		public static Func<KeyValuePair<string, float>, float> <>9__91_1;

		// Token: 0x04002A1D RID: 10781
		public static Func<KeyValuePair<string, Vector3>, string> <>9__91_2;

		// Token: 0x04002A1E RID: 10782
		public static Func<KeyValuePair<string, Vector3>, Vector3> <>9__91_3;

		// Token: 0x04002A1F RID: 10783
		public static Func<KeyValuePair<MagicColor, int>, MagicColor> <>9__91_4;

		// Token: 0x04002A20 RID: 10784
		public static Func<KeyValuePair<MagicColor, int>, int> <>9__91_5;
	}
}
