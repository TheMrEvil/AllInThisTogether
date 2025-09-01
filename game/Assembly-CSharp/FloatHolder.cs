using System;

// Token: 0x02000336 RID: 822
public class FloatHolder
{
	// Token: 0x06001C06 RID: 7174 RVA: 0x000ABCA7 File Offset: 0x000A9EA7
	public FloatHolder(float v)
	{
		this.value = v;
	}

	// Token: 0x06001C07 RID: 7175 RVA: 0x000ABCB6 File Offset: 0x000A9EB6
	public static implicit operator float(FloatHolder x)
	{
		return x.value;
	}

	// Token: 0x06001C08 RID: 7176 RVA: 0x000ABCBE File Offset: 0x000A9EBE
	public static implicit operator FloatHolder(float x)
	{
		return new FloatHolder(x);
	}

	// Token: 0x04001C67 RID: 7271
	public float value;
}
