using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
[Serializable]
public class Cosmetic_Emote : Cosmetic
{
	// Token: 0x17000023 RID: 35
	// (get) Token: 0x06000246 RID: 582 RVA: 0x00013D08 File Offset: 0x00011F08
	public override string UnlockName
	{
		get
		{
			return this.Name;
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x06000247 RID: 583 RVA: 0x00013D10 File Offset: 0x00011F10
	public override string CategoryName
	{
		get
		{
			return "Emote";
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x06000248 RID: 584 RVA: 0x00013D17 File Offset: 0x00011F17
	public override UnlockCategory Type
	{
		get
		{
			return UnlockCategory.Cosmetic_Emote;
		}
	}

	// Token: 0x06000249 RID: 585 RVA: 0x00013D1B File Offset: 0x00011F1B
	public override CosmeticType CType()
	{
		return CosmeticType.Emote;
	}

	// Token: 0x0600024A RID: 586 RVA: 0x00013D1E File Offset: 0x00011F1E
	public Cosmetic_Emote()
	{
	}

	// Token: 0x04000238 RID: 568
	public Sprite Icon;

	// Token: 0x04000239 RID: 569
	public string Animation;

	// Token: 0x0400023A RID: 570
	public bool UseLegs;

	// Token: 0x0400023B RID: 571
	public StatusTree Status;

	// Token: 0x0400023C RID: 572
	public float Duration;
}
