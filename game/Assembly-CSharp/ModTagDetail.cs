using System;

// Token: 0x02000277 RID: 631
[Serializable]
public class ModTagDetail
{
	// Token: 0x060018F0 RID: 6384 RVA: 0x0009B9A4 File Offset: 0x00099BA4
	public ModTagDetail Copy()
	{
		return base.MemberwiseClone() as ModTagDetail;
	}

	// Token: 0x060018F1 RID: 6385 RVA: 0x0009B9B1 File Offset: 0x00099BB1
	public ModTagDetail()
	{
	}

	// Token: 0x040018D4 RID: 6356
	public AbilityFeature AbilityFeature;

	// Token: 0x040018D5 RID: 6357
	public StatusFeature StatusFeature;

	// Token: 0x040018D6 RID: 6358
	public ProjectileFeature ProjectileFeature;

	// Token: 0x040018D7 RID: 6359
	public AreaFeature AreaFeature;

	// Token: 0x040018D8 RID: 6360
	public BeamFeature BeamFeature;

	// Token: 0x040018D9 RID: 6361
	public ForceFeature ForceFeature;

	// Token: 0x040018DA RID: 6362
	public PositionFeature PositionFeature;

	// Token: 0x040018DB RID: 6363
	public AbilityType AbilityType;

	// Token: 0x040018DC RID: 6364
	public Keyword Keyword;

	// Token: 0x040018DD RID: 6365
	public ManaFeature Mana;
}
