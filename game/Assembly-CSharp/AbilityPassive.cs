using System;
using System.Runtime.CompilerServices;

// Token: 0x0200033A RID: 826
[Serializable]
public class AbilityPassive : Passive
{
	// Token: 0x06001C1D RID: 7197 RVA: 0x000AC065 File Offset: 0x000AA265
	public AbilityPassive(PlayerAbilityType pType, Passive.AbilityValue v)
	{
		this.AbilityType = pType;
		this.Value = v;
	}

	// Token: 0x06001C1E RID: 7198 RVA: 0x000AC084 File Offset: 0x000AA284
	public override bool Matches(Passive p)
	{
		if (!(p is AbilityPassive))
		{
			return false;
		}
		AbilityPassive abilityPassive = p as AbilityPassive;
		return (this.AbilityType == PlayerAbilityType.Any || abilityPassive.AbilityType == PlayerAbilityType.Any || this.AbilityType == abilityPassive.AbilityType) && abilityPassive.Value == this.Value;
	}

	// Token: 0x06001C1F RID: 7199 RVA: 0x000AC0D7 File Offset: 0x000AA2D7
	public static implicit operator AbilityPassive([TupleElementNames(new string[]
	{
		"pt",
		"v"
	})] ValueTuple<PlayerAbilityType, Passive.AbilityValue> x)
	{
		return new AbilityPassive(x.Item1, x.Item2);
	}

	// Token: 0x06001C20 RID: 7200 RVA: 0x000AC0EA File Offset: 0x000AA2EA
	public AbilityPassive Copy()
	{
		return base.MemberwiseClone() as AbilityPassive;
	}

	// Token: 0x06001C21 RID: 7201 RVA: 0x000AC0F7 File Offset: 0x000AA2F7
	public override string ToString()
	{
		return this.AbilityType.ToString() + "-" + this.Value.ToString();
	}

	// Token: 0x06001C22 RID: 7202 RVA: 0x000AC128 File Offset: 0x000AA328
	public override int GetHashCode()
	{
		int num = 4;
		int num2 = 16;
		return num << num2 | (int)this.Value;
	}

	// Token: 0x04001C71 RID: 7281
	public PlayerAbilityType AbilityType = PlayerAbilityType.Any;

	// Token: 0x04001C72 RID: 7282
	public Passive.AbilityValue Value;
}
