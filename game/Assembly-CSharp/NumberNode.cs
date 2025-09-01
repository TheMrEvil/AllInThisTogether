using System;

// Token: 0x02000383 RID: 899
public class NumberNode : Node
{
	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x06001D85 RID: 7557 RVA: 0x000B38FC File Offset: 0x000B1AFC
	internal override bool CanSkipClone
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06001D86 RID: 7558 RVA: 0x000B38FF File Offset: 0x000B1AFF
	public virtual float Evaluate(EffectProperties props)
	{
		return 0f;
	}

	// Token: 0x06001D87 RID: 7559 RVA: 0x000B3906 File Offset: 0x000B1B06
	public NumberNode()
	{
	}
}
