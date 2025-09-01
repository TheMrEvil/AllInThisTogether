using System;

// Token: 0x02000335 RID: 821
public class IntHolder
{
	// Token: 0x06001C03 RID: 7171 RVA: 0x000ABC88 File Offset: 0x000A9E88
	public IntHolder(int v)
	{
		this.value = v;
	}

	// Token: 0x06001C04 RID: 7172 RVA: 0x000ABC97 File Offset: 0x000A9E97
	public static implicit operator int(IntHolder x)
	{
		return x.value;
	}

	// Token: 0x06001C05 RID: 7173 RVA: 0x000ABC9F File Offset: 0x000A9E9F
	public static implicit operator IntHolder(int x)
	{
		return new IntHolder(x);
	}

	// Token: 0x04001C66 RID: 7270
	public int value;
}
