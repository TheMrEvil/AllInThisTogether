using System;

// Token: 0x020000B0 RID: 176
public class FountainIndicator : Indicatable
{
	// Token: 0x060007F8 RID: 2040 RVA: 0x0003877E File Offset: 0x0003697E
	private void Awake()
	{
		WorldIndicators.Indicate(this);
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x00038786 File Offset: 0x00036986
	private void OnDestroy()
	{
		WorldIndicators.ReleaseIndicator(this);
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x0003878E File Offset: 0x0003698E
	public override bool ShouldIndicate()
	{
		return false;
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x00038791 File Offset: 0x00036991
	public FountainIndicator()
	{
	}
}
