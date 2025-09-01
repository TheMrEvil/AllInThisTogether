using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
[Serializable]
public class Cosmetic_Head : Cosmetic
{
	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000237 RID: 567 RVA: 0x00013CB0 File Offset: 0x00011EB0
	public override string UnlockName
	{
		get
		{
			return this.Name;
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000238 RID: 568 RVA: 0x00013CB8 File Offset: 0x00011EB8
	public override string CategoryName
	{
		get
		{
			return "Stylus";
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000239 RID: 569 RVA: 0x00013CBF File Offset: 0x00011EBF
	public override UnlockCategory Type
	{
		get
		{
			return UnlockCategory.Cosmetic_Head;
		}
	}

	// Token: 0x0600023A RID: 570 RVA: 0x00013CC2 File Offset: 0x00011EC2
	public override CosmeticType CType()
	{
		return CosmeticType.Head;
	}

	// Token: 0x0600023B RID: 571 RVA: 0x00013CC5 File Offset: 0x00011EC5
	public Cosmetic_Head()
	{
	}

	// Token: 0x0400022F RID: 559
	public GameObject Prefab;
}
