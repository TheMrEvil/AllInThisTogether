using System;
using UnityEngine;

// Token: 0x0200004B RID: 75
[Serializable]
public class Cosmetic_Book : Cosmetic
{
	// Token: 0x1700001D RID: 29
	// (get) Token: 0x0600023C RID: 572 RVA: 0x00013CCD File Offset: 0x00011ECD
	public override string UnlockName
	{
		get
		{
			return this.Name;
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x0600023D RID: 573 RVA: 0x00013CD5 File Offset: 0x00011ED5
	public override string CategoryName
	{
		get
		{
			return "Book";
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x0600023E RID: 574 RVA: 0x00013CDC File Offset: 0x00011EDC
	public override UnlockCategory Type
	{
		get
		{
			return UnlockCategory.Cosmetic_Book;
		}
	}

	// Token: 0x0600023F RID: 575 RVA: 0x00013CDF File Offset: 0x00011EDF
	public override CosmeticType CType()
	{
		return CosmeticType.Book;
	}

	// Token: 0x06000240 RID: 576 RVA: 0x00013CE2 File Offset: 0x00011EE2
	public Cosmetic_Book()
	{
	}

	// Token: 0x04000230 RID: 560
	public Material BookCover;

	// Token: 0x04000231 RID: 561
	public GameObject Emblem;

	// Token: 0x04000232 RID: 562
	public GameObject Protector;

	// Token: 0x04000233 RID: 563
	[Header("Binding")]
	public Mesh BindingMesh;

	// Token: 0x04000234 RID: 564
	[Header("")]
	public Material BindingMat;

	// Token: 0x04000235 RID: 565
	[Header("Bookmark")]
	public PlayerBookDisplay.MarkShape MarkShape;

	// Token: 0x04000236 RID: 566
	[Header("")]
	public Material MarkMaterail;
}
