using System;
using System.Runtime.CompilerServices;

// Token: 0x0200033B RID: 827
[Serializable]
public class KeywordPassive : Passive
{
	// Token: 0x06001C23 RID: 7203 RVA: 0x000AC145 File Offset: 0x000AA345
	public KeywordPassive(Keyword keyword, Passive.AbilityValue v)
	{
		this.Keyword = keyword;
		this.Value = v;
	}

	// Token: 0x06001C24 RID: 7204 RVA: 0x000AC168 File Offset: 0x000AA368
	public override bool Matches(Passive p)
	{
		if (!(p is KeywordPassive))
		{
			return false;
		}
		KeywordPassive keywordPassive = p as KeywordPassive;
		return this.Keyword != Keyword.None && this.Keyword == keywordPassive.Keyword && keywordPassive.Value == this.Value;
	}

	// Token: 0x06001C25 RID: 7205 RVA: 0x000AC1B6 File Offset: 0x000AA3B6
	public static implicit operator KeywordPassive([TupleElementNames(new string[]
	{
		"k",
		"v"
	})] ValueTuple<Keyword, Passive.AbilityValue> x)
	{
		return new KeywordPassive(x.Item1, x.Item2);
	}

	// Token: 0x06001C26 RID: 7206 RVA: 0x000AC1C9 File Offset: 0x000AA3C9
	public KeywordPassive Copy()
	{
		return base.MemberwiseClone() as KeywordPassive;
	}

	// Token: 0x06001C27 RID: 7207 RVA: 0x000AC1D8 File Offset: 0x000AA3D8
	public override int GetHashCode()
	{
		int num = 8;
		int num2 = 16;
		return num << num2 | (int)this.Value;
	}

	// Token: 0x04001C73 RID: 7283
	public Keyword Keyword = Keyword.None;

	// Token: 0x04001C74 RID: 7284
	public Passive.AbilityValue Value;
}
