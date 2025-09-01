using System;

// Token: 0x02000047 RID: 71
[Serializable]
public class CosmeticSet
{
	// Token: 0x06000229 RID: 553 RVA: 0x00013A48 File Offset: 0x00011C48
	public CosmeticSet()
	{
	}

	// Token: 0x0600022A RID: 554 RVA: 0x00013A50 File Offset: 0x00011C50
	public CosmeticSet(string input)
	{
		if (input == null)
		{
			input = "";
		}
		string[] array = input.Split('|', StringSplitOptions.None);
		this.Head = CosmeticDB.GetHead((array.Length != 0) ? array[0] : "");
		this.Skin = CosmeticDB.GetSkin((array.Length > 1) ? array[1] : "");
		this.Book = CosmeticDB.GetBook((array.Length > 2) ? array[2] : "");
		this.Back = CosmeticDB.GetSignature((array.Length > 3) ? array[3] : "");
	}

	// Token: 0x0600022B RID: 555 RVA: 0x00013AE4 File Offset: 0x00011CE4
	public override string ToString()
	{
		Cosmetic_Head head = this.Head;
		string text = ((head != null) ? head.GUID : null) ?? "";
		Cosmetic_Skin skin = this.Skin;
		string text2 = ((skin != null) ? skin.GUID : null) ?? "";
		Cosmetic_Book book = this.Book;
		string text3 = ((book != null) ? book.GUID : null) ?? "";
		Cosmetic_Signature back = this.Back;
		string text4 = ((back != null) ? back.GUID : null) ?? "";
		return string.Concat(new string[]
		{
			text,
			"|",
			text2,
			"|",
			text3,
			"|",
			text4
		});
	}

	// Token: 0x0600022C RID: 556 RVA: 0x00013B94 File Offset: 0x00011D94
	public Cosmetic GetCosmetic(CosmeticType cType)
	{
		Cosmetic result;
		switch (cType)
		{
		case CosmeticType.Skin:
			result = this.Skin;
			break;
		case CosmeticType.Head:
			result = this.Head;
			break;
		case CosmeticType.Book:
			result = this.Book;
			break;
		case CosmeticType.Signature:
			result = this.Back;
			break;
		default:
			result = null;
			break;
		}
		return result;
	}

	// Token: 0x0600022D RID: 557 RVA: 0x00013BE4 File Offset: 0x00011DE4
	public bool Matches(CosmeticSet set)
	{
		return !(this.Head.GUID != set.Head.GUID) && !(this.Skin.GUID != set.Skin.GUID) && !(this.Book.GUID != set.Book.GUID) && !(this.Back.GUID != set.Back.GUID);
	}

	// Token: 0x04000222 RID: 546
	public Cosmetic_Head Head;

	// Token: 0x04000223 RID: 547
	public Cosmetic_Skin Skin;

	// Token: 0x04000224 RID: 548
	public Cosmetic_Book Book;

	// Token: 0x04000225 RID: 549
	public Cosmetic_Signature Back;
}
