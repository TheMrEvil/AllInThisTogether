using System;

// Token: 0x02000276 RID: 630
[Serializable]
public class ModTag
{
	// Token: 0x060018EC RID: 6380 RVA: 0x0009B7D3 File Offset: 0x000999D3
	public ModTag(ModTagDetail detail, PlayerAbilityType source = PlayerAbilityType.Any)
	{
		this.TagSource = source;
		this.Detail = detail;
	}

	// Token: 0x060018ED RID: 6381 RVA: 0x0009B7EC File Offset: 0x000999EC
	public override bool Equals(object obj)
	{
		ModTag modTag = obj as ModTag;
		if (modTag == null)
		{
			return false;
		}
		if (this.Detail.AbilityFeature != modTag.Detail.AbilityFeature)
		{
			return false;
		}
		if (this.TagSource != modTag.TagSource && this.TagSource != PlayerAbilityType.Any && modTag.TagSource != PlayerAbilityType.Any)
		{
			return false;
		}
		switch (this.Detail.AbilityFeature)
		{
		case AbilityFeature.Status:
			return this.Detail.StatusFeature == modTag.Detail.StatusFeature;
		case AbilityFeature.Projectile:
			return this.Detail.ProjectileFeature == modTag.Detail.ProjectileFeature;
		case AbilityFeature.AreaOfEffect:
			return this.Detail.AreaFeature == modTag.Detail.AreaFeature;
		case AbilityFeature.Beam:
			return this.Detail.BeamFeature == modTag.Detail.BeamFeature;
		case AbilityFeature.Force:
			return this.Detail.ForceFeature == modTag.Detail.ForceFeature;
		case AbilityFeature.PositionChange:
			return this.Detail.PositionFeature == modTag.Detail.PositionFeature;
		case AbilityFeature.CastTime:
			return this.Detail.AbilityType == modTag.Detail.AbilityType;
		case AbilityFeature.Player_Keyword:
			return this.Detail.Keyword == modTag.Detail.Keyword;
		case AbilityFeature.Mana:
			return this.Detail.Mana == modTag.Detail.Mana;
		}
		return this.Detail.AbilityFeature == modTag.Detail.AbilityFeature;
	}

	// Token: 0x060018EE RID: 6382 RVA: 0x0009B979 File Offset: 0x00099B79
	public override int GetHashCode()
	{
		return (int)this.Detail.AbilityFeature;
	}

	// Token: 0x060018EF RID: 6383 RVA: 0x0009B986 File Offset: 0x00099B86
	public ModTag Copy()
	{
		ModTag modTag = base.MemberwiseClone() as ModTag;
		modTag.Detail = this.Detail.Copy();
		return modTag;
	}

	// Token: 0x040018D2 RID: 6354
	public PlayerAbilityType TagSource;

	// Token: 0x040018D3 RID: 6355
	public ModTagDetail Detail;
}
