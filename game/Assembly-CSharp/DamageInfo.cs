using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x020002B6 RID: 694
[Serializable]
public class DamageInfo
{
	// Token: 0x1700018D RID: 397
	// (get) Token: 0x060019E1 RID: 6625 RVA: 0x000A1033 File Offset: 0x0009F233
	public bool AffectedShield
	{
		get
		{
			return this.ShieldAmount > 0f;
		}
	}

	// Token: 0x1700018E RID: 398
	// (get) Token: 0x060019E2 RID: 6626 RVA: 0x000A1042 File Offset: 0x0009F242
	public float TotalAmount
	{
		get
		{
			return this.Amount + this.ShieldAmount;
		}
	}

	// Token: 0x060019E3 RID: 6627 RVA: 0x000A1051 File Offset: 0x0009F251
	private DamageInfo()
	{
	}

	// Token: 0x060019E4 RID: 6628 RVA: 0x000A107C File Offset: 0x0009F27C
	public DamageInfo(int amount)
	{
		this.Amount = (float)amount;
		this.AtPoint = Vector3.zero;
		this.DamageType = DNumType.Default;
		this.AffectedID = -1;
		this.SnippetChance = 1f;
		this.Depth = 1;
		this.SourceID = -1;
		this.AbilityType = PlayerAbilityType.None;
		this.ActionSource = ActionSource._;
		this.EffectSource = EffectSource._;
	}

	// Token: 0x060019E5 RID: 6629 RVA: 0x000A1100 File Offset: 0x0009F300
	public DamageInfo(float amount, DNumType damageType, int affectedID, float snippetChance, EffectProperties props)
	{
		this.Amount = amount;
		this.AtPoint = ((props != null) ? props.GetOutputPoint() : Vector3.zero);
		this.DamageType = damageType;
		this.AffectedID = affectedID;
		this.SnippetChance = snippetChance;
		this.Depth = ((props == null) ? 1 : props.Depth);
		this.SourceID = ((props == null) ? -1 : ((props.SourceControl == null) ? -1 : props.SourceControl.ViewID));
		this.AbilityType = ((props != null) ? props.AbilityType : PlayerAbilityType.None);
		this.ActionSource = ((props != null) ? props.SourceType : ActionSource._);
		this.EffectSource = ((props != null) ? props.EffectSource : EffectSource._);
		this.CauseName = (((props != null) ? props.CauseName : null) ?? "");
		this.CauseID = (((props != null) ? props.CauseID : null) ?? "");
		if (props != null && props.EffectAugments != null)
		{
			foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in props.EffectAugments.trees)
			{
				if (keyValuePair.Key.SendWithAbility && keyValuePair.Key.OnlyInProps)
				{
					this.SentWithAugments.Add(keyValuePair.Key.guid);
				}
			}
		}
	}

	// Token: 0x060019E6 RID: 6630 RVA: 0x000A12A4 File Offset: 0x0009F4A4
	public DamageInfo Copy()
	{
		return base.MemberwiseClone() as DamageInfo;
	}

	// Token: 0x060019E7 RID: 6631 RVA: 0x000A12B1 File Offset: 0x0009F4B1
	public void Add(DamageInfo toAdd)
	{
		this.Amount += toAdd.Amount;
		this.ShieldAmount += toAdd.ShieldAmount;
	}

	// Token: 0x060019E8 RID: 6632 RVA: 0x000A12DC File Offset: 0x0009F4DC
	public EffectProperties GenerateEffectProperties()
	{
		EntityControl entity = EntityControl.GetEntity(this.SourceID);
		EffectProperties effectProperties = new EffectProperties(entity);
		effectProperties.StartLoc = (effectProperties.OutLoc = ((entity == null) ? global::Pose.WorldPoint(Vector3.zero, Vector3.up) : global::Pose.WorldPoint(this.AtPoint, (this.AtPoint - entity.display.CenterOfMass.position).normalized)));
		EffectProperties effectProperties2 = effectProperties;
		EntityControl entity2 = EntityControl.GetEntity(this.AffectedID);
		effectProperties2.SeekTarget = ((entity2 != null) ? entity2.gameObject : null);
		effectProperties.Affected = effectProperties.SeekTarget;
		effectProperties.AbilityType = this.AbilityType;
		effectProperties.SourceType = this.ActionSource;
		effectProperties.EffectSource = this.EffectSource;
		effectProperties.Depth = this.Depth;
		effectProperties.CauseName = this.CauseName;
		effectProperties.CauseID = this.CauseID;
		effectProperties.SetExtra(EProp.Snip_DamageDone, this.TotalAmount);
		foreach (string key in this.SentWithAugments)
		{
			effectProperties.AddAugment(key, 1);
		}
		return effectProperties;
	}

	// Token: 0x060019E9 RID: 6633 RVA: 0x000A1420 File Offset: 0x0009F620
	public static short SerializeDamageInfo(StreamBuffer outStream, object customobject)
	{
		DamageInfo damageInfo = (DamageInfo)customobject;
		byte[] obj = DamageInfo.memDamage;
		lock (obj)
		{
			byte[] array = DamageInfo.memDamage;
			int num = 0;
			Protocol.Serialize(damageInfo.Amount, array, ref num);
			Protocol.Serialize(damageInfo.ShieldAmount, array, ref num);
			Protocol.Serialize(damageInfo.AtPoint.x, array, ref num);
			Protocol.Serialize(damageInfo.AtPoint.y, array, ref num);
			Protocol.Serialize(damageInfo.AtPoint.z, array, ref num);
			Protocol.Serialize(damageInfo.SourceID, array, ref num);
			Protocol.Serialize(damageInfo.AffectedID, array, ref num);
			Protocol.Serialize((int)damageInfo.DamageType, array, ref num);
			Protocol.Serialize((int)damageInfo.AbilityType, array, ref num);
			Protocol.Serialize((int)damageInfo.ActionSource, array, ref num);
			Protocol.Serialize((int)damageInfo.EffectSource, array, ref num);
			Protocol.Serialize(damageInfo.SnippetChance, array, ref num);
			Protocol.Serialize(damageInfo.Depth, array, ref num);
			outStream.Write(array, 0, 52);
		}
		return 52;
	}

	// Token: 0x060019EA RID: 6634 RVA: 0x000A153C File Offset: 0x0009F73C
	public static object DeserializeDamageInfo(StreamBuffer inStream, short length)
	{
		DamageInfo damageInfo = new DamageInfo();
		byte[] obj = DamageInfo.memDamage;
		lock (obj)
		{
			inStream.Read(DamageInfo.memDamage, 0, 52);
			int num = 0;
			Protocol.Deserialize(out damageInfo.Amount, DamageInfo.memDamage, ref num);
			Protocol.Deserialize(out damageInfo.ShieldAmount, DamageInfo.memDamage, ref num);
			float x;
			Protocol.Deserialize(out x, DamageInfo.memDamage, ref num);
			float y;
			Protocol.Deserialize(out y, DamageInfo.memDamage, ref num);
			float z;
			Protocol.Deserialize(out z, DamageInfo.memDamage, ref num);
			damageInfo.AtPoint = new Vector3(x, y, z);
			Protocol.Deserialize(out damageInfo.SourceID, DamageInfo.memDamage, ref num);
			Protocol.Deserialize(out damageInfo.SourceID, DamageInfo.memDamage, ref num);
			int num2;
			Protocol.Deserialize(out num2, DamageInfo.memDamage, ref num);
			damageInfo.DamageType = (DNumType)num2;
			Protocol.Deserialize(out num2, DamageInfo.memDamage, ref num);
			damageInfo.AbilityType = (PlayerAbilityType)num2;
			Protocol.Deserialize(out num2, DamageInfo.memDamage, ref num);
			damageInfo.ActionSource = (ActionSource)num2;
			Protocol.Deserialize(out num2, DamageInfo.memDamage, ref num);
			damageInfo.EffectSource = (EffectSource)num2;
			Protocol.Deserialize(out damageInfo.SnippetChance, DamageInfo.memDamage, ref num);
			Protocol.Deserialize(out damageInfo.Depth, DamageInfo.memDamage, ref num);
		}
		return damageInfo;
	}

	// Token: 0x060019EB RID: 6635 RVA: 0x000A1698 File Offset: 0x0009F898
	// Note: this type is marked as 'beforefieldinit'.
	static DamageInfo()
	{
	}

	// Token: 0x04001A3F RID: 6719
	public float Amount;

	// Token: 0x04001A40 RID: 6720
	public float ShieldAmount;

	// Token: 0x04001A41 RID: 6721
	public Vector3 AtPoint;

	// Token: 0x04001A42 RID: 6722
	public int SourceID;

	// Token: 0x04001A43 RID: 6723
	public int AffectedID;

	// Token: 0x04001A44 RID: 6724
	public int Depth;

	// Token: 0x04001A45 RID: 6725
	public DNumType DamageType;

	// Token: 0x04001A46 RID: 6726
	public ActionSource ActionSource;

	// Token: 0x04001A47 RID: 6727
	public PlayerAbilityType AbilityType;

	// Token: 0x04001A48 RID: 6728
	public EffectSource EffectSource;

	// Token: 0x04001A49 RID: 6729
	public float SnippetChance;

	// Token: 0x04001A4A RID: 6730
	public bool CanOneShot;

	// Token: 0x04001A4B RID: 6731
	public List<string> SentWithAugments = new List<string>();

	// Token: 0x04001A4C RID: 6732
	public string CauseName = "";

	// Token: 0x04001A4D RID: 6733
	public string CauseID = "";

	// Token: 0x04001A4E RID: 6734
	private const int SERIALIZE_COUNT = 13;

	// Token: 0x04001A4F RID: 6735
	public static readonly byte[] memDamage = new byte[52];
}
