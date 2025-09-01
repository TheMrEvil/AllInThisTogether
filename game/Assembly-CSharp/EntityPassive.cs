using System;

// Token: 0x02000339 RID: 825
[Serializable]
public class EntityPassive : Passive
{
	// Token: 0x06001C16 RID: 7190 RVA: 0x000ABFDF File Offset: 0x000AA1DF
	public EntityPassive(Passive.EntityValue v)
	{
		this.Value = v;
	}

	// Token: 0x06001C17 RID: 7191 RVA: 0x000ABFF0 File Offset: 0x000AA1F0
	public override bool Matches(Passive p)
	{
		EntityPassive entityPassive = p as EntityPassive;
		return entityPassive != null && entityPassive.Value == this.Value;
	}

	// Token: 0x06001C18 RID: 7192 RVA: 0x000AC017 File Offset: 0x000AA217
	public static implicit operator Passive.EntityValue(EntityPassive p)
	{
		return p.Value;
	}

	// Token: 0x06001C19 RID: 7193 RVA: 0x000AC01F File Offset: 0x000AA21F
	public static implicit operator EntityPassive(Passive.EntityValue p)
	{
		return new EntityPassive(p);
	}

	// Token: 0x06001C1A RID: 7194 RVA: 0x000AC027 File Offset: 0x000AA227
	public EntityPassive Copy()
	{
		return base.MemberwiseClone() as EntityPassive;
	}

	// Token: 0x06001C1B RID: 7195 RVA: 0x000AC034 File Offset: 0x000AA234
	public override string ToString()
	{
		return this.Value.ToString();
	}

	// Token: 0x06001C1C RID: 7196 RVA: 0x000AC048 File Offset: 0x000AA248
	public override int GetHashCode()
	{
		int num = 2;
		int num2 = 16;
		return num << num2 | (int)this.Value;
	}

	// Token: 0x04001C70 RID: 7280
	public Passive.EntityValue Value;
}
