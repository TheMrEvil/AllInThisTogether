using System;
using UnityEngine;

// Token: 0x02000048 RID: 72
[Serializable]
public class Cosmetic : Unlockable
{
	// Token: 0x17000016 RID: 22
	// (get) Token: 0x0600022E RID: 558 RVA: 0x00013C6E File Offset: 0x00011E6E
	public override string GUID
	{
		get
		{
			return this.cosmeticid;
		}
	}

	// Token: 0x0600022F RID: 559 RVA: 0x00013C76 File Offset: 0x00011E76
	public Cosmetic()
	{
	}

	// Token: 0x06000230 RID: 560 RVA: 0x00013C7E File Offset: 0x00011E7E
	private string Metadata()
	{
		return "Metadata - " + this.Name;
	}

	// Token: 0x06000231 RID: 561 RVA: 0x00013C90 File Offset: 0x00011E90
	public virtual CosmeticType CType()
	{
		return CosmeticType._;
	}

	// Token: 0x04000226 RID: 550
	public string Name;

	// Token: 0x04000227 RID: 551
	public string cosmeticid;

	// Token: 0x04000228 RID: 552
	[TextArea(3, 5)]
	public string Detail;

	// Token: 0x04000229 RID: 553
	public AugmentQuality Rarity;

	// Token: 0x0400022A RID: 554
	public bool Hidden;
}
