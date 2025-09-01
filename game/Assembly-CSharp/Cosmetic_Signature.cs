using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
[Serializable]
public class Cosmetic_Signature : Cosmetic
{
	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000241 RID: 577 RVA: 0x00013CEA File Offset: 0x00011EEA
	public override string UnlockName
	{
		get
		{
			return this.Name;
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000242 RID: 578 RVA: 0x00013CF2 File Offset: 0x00011EF2
	public override string CategoryName
	{
		get
		{
			return "Curiosity";
		}
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000243 RID: 579 RVA: 0x00013CF9 File Offset: 0x00011EF9
	public override UnlockCategory Type
	{
		get
		{
			return UnlockCategory.Cosmetic_Signature;
		}
	}

	// Token: 0x06000244 RID: 580 RVA: 0x00013CFD File Offset: 0x00011EFD
	public override CosmeticType CType()
	{
		return CosmeticType.Signature;
	}

	// Token: 0x06000245 RID: 581 RVA: 0x00013D00 File Offset: 0x00011F00
	public Cosmetic_Signature()
	{
	}

	// Token: 0x04000237 RID: 567
	public GameObject Prefab;
}
