using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
[Serializable]
public class Cosmetic_Skin : Cosmetic
{
	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000232 RID: 562 RVA: 0x00013C93 File Offset: 0x00011E93
	public override string UnlockName
	{
		get
		{
			return this.Name;
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000233 RID: 563 RVA: 0x00013C9B File Offset: 0x00011E9B
	public override string CategoryName
	{
		get
		{
			return "Raiment";
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000234 RID: 564 RVA: 0x00013CA2 File Offset: 0x00011EA2
	public override UnlockCategory Type
	{
		get
		{
			return UnlockCategory.Cosmetic_Skin;
		}
	}

	// Token: 0x06000235 RID: 565 RVA: 0x00013CA5 File Offset: 0x00011EA5
	public override CosmeticType CType()
	{
		return CosmeticType.Skin;
	}

	// Token: 0x06000236 RID: 566 RVA: 0x00013CA8 File Offset: 0x00011EA8
	public Cosmetic_Skin()
	{
	}

	// Token: 0x0400022B RID: 555
	public Material Torso;

	// Token: 0x0400022C RID: 556
	public Material Arm_L;

	// Token: 0x0400022D RID: 557
	public Material Arm_R;

	// Token: 0x0400022E RID: 558
	public Material Legs;
}
