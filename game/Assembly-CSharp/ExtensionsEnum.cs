using System;

// Token: 0x02000229 RID: 553
public static class ExtensionsEnum
{
	// Token: 0x06001720 RID: 5920 RVA: 0x00092878 File Offset: 0x00090A78
	public static bool AffectsCaster(this EffectInteractsWith interact)
	{
		return interact == EffectInteractsWith.AllEntities || interact == EffectInteractsWith.Allies;
	}
}
