using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000046 RID: 70
public class CosmeticDB : ScriptableObject
{
	// Token: 0x17000013 RID: 19
	// (get) Token: 0x0600021B RID: 539 RVA: 0x000131A0 File Offset: 0x000113A0
	public Cosmetic_Signature DefaultSignature
	{
		get
		{
			return this.Signatures[0];
		}
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x0600021C RID: 540 RVA: 0x000131AE File Offset: 0x000113AE
	public static CosmeticDB DB
	{
		get
		{
			if (CosmeticDB._db == null)
			{
				CosmeticDB._db = Resources.Load<CosmeticDB>("CosmeticDB");
			}
			return CosmeticDB._db;
		}
	}

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x0600021D RID: 541 RVA: 0x000131D4 File Offset: 0x000113D4
	public static List<Cosmetic> AllCosmetics
	{
		get
		{
			List<Cosmetic> list = new List<Cosmetic>();
			list.Add(CosmeticDB.DB.DefaultHead);
			list.AddRange(CosmeticDB.DB.Heads);
			list.Add(CosmeticDB.DB.DefaultSkin);
			list.AddRange(CosmeticDB.DB.Skins);
			list.Add(CosmeticDB.DB.DefaultBook);
			list.AddRange(CosmeticDB.DB.Books);
			list.AddRange(CosmeticDB.DB.Signatures);
			list.AddRange(CosmeticDB.DB.Emotes);
			return list;
		}
	}

	// Token: 0x0600021E RID: 542 RVA: 0x00013268 File Offset: 0x00011468
	public static List<Cosmetic> GetCosmetics(CosmeticType ctype)
	{
		List<Cosmetic> list = new List<Cosmetic>();
		switch (ctype)
		{
		case CosmeticType.Skin:
			list.Add(CosmeticDB.DB.DefaultSkin);
			list.AddRange(CosmeticDB.DB.Skins);
			break;
		case CosmeticType.Head:
			list.Add(CosmeticDB.DB.DefaultHead);
			list.AddRange(CosmeticDB.DB.Heads);
			break;
		case CosmeticType.Book:
			list.Add(CosmeticDB.DB.DefaultBook);
			list.AddRange(CosmeticDB.DB.Books);
			break;
		case CosmeticType.Signature:
			list.AddRange(CosmeticDB.DB.Signatures);
			break;
		case CosmeticType.Emote:
			list.AddRange(CosmeticDB.DB.Emotes);
			break;
		}
		return list;
	}

	// Token: 0x0600021F RID: 543 RVA: 0x00013328 File Offset: 0x00011528
	public static Cosmetic GetCosmetic(string cID)
	{
		if (CosmeticDB.DB.DefaultHead.cosmeticid == cID)
		{
			return CosmeticDB.DB.DefaultHead;
		}
		if (CosmeticDB.DB.DefaultSkin.cosmeticid == cID)
		{
			return CosmeticDB.DB.DefaultSkin;
		}
		if (CosmeticDB.DB.DefaultBook.cosmeticid == cID)
		{
			return CosmeticDB.DB.DefaultBook;
		}
		IEnumerable<Cosmetic_Head> heads = CosmeticDB.DB.Heads;
		Func<Cosmetic_Head, bool> <>9__0;
		Func<Cosmetic_Head, bool> predicate;
		if ((predicate = <>9__0) == null)
		{
			predicate = (<>9__0 = ((Cosmetic_Head v) => v.cosmeticid == cID));
		}
		using (IEnumerator<Cosmetic_Head> enumerator = heads.Where(predicate).GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				return enumerator.Current;
			}
		}
		IEnumerable<Cosmetic_Skin> skins = CosmeticDB.DB.Skins;
		Func<Cosmetic_Skin, bool> <>9__1;
		Func<Cosmetic_Skin, bool> predicate2;
		if ((predicate2 = <>9__1) == null)
		{
			predicate2 = (<>9__1 = ((Cosmetic_Skin v) => v.cosmeticid == cID));
		}
		using (IEnumerator<Cosmetic_Skin> enumerator2 = skins.Where(predicate2).GetEnumerator())
		{
			if (enumerator2.MoveNext())
			{
				return enumerator2.Current;
			}
		}
		IEnumerable<Cosmetic_Book> books = CosmeticDB.DB.Books;
		Func<Cosmetic_Book, bool> <>9__2;
		Func<Cosmetic_Book, bool> predicate3;
		if ((predicate3 = <>9__2) == null)
		{
			predicate3 = (<>9__2 = ((Cosmetic_Book v) => v.cosmeticid == cID));
		}
		using (IEnumerator<Cosmetic_Book> enumerator3 = books.Where(predicate3).GetEnumerator())
		{
			if (enumerator3.MoveNext())
			{
				return enumerator3.Current;
			}
		}
		IEnumerable<Cosmetic_Emote> emotes = CosmeticDB.DB.Emotes;
		Func<Cosmetic_Emote, bool> <>9__3;
		Func<Cosmetic_Emote, bool> predicate4;
		if ((predicate4 = <>9__3) == null)
		{
			predicate4 = (<>9__3 = ((Cosmetic_Emote v) => v.cosmeticid == cID));
		}
		using (IEnumerator<Cosmetic_Emote> enumerator4 = emotes.Where(predicate4).GetEnumerator())
		{
			if (enumerator4.MoveNext())
			{
				return enumerator4.Current;
			}
		}
		IEnumerable<Cosmetic_Signature> signatures = CosmeticDB.DB.Signatures;
		Func<Cosmetic_Signature, bool> <>9__4;
		Func<Cosmetic_Signature, bool> predicate5;
		if ((predicate5 = <>9__4) == null)
		{
			predicate5 = (<>9__4 = ((Cosmetic_Signature v) => v.cosmeticid == cID));
		}
		using (IEnumerator<Cosmetic_Signature> enumerator5 = signatures.Where(predicate5).GetEnumerator())
		{
			if (enumerator5.MoveNext())
			{
				return enumerator5.Current;
			}
		}
		return null;
	}

	// Token: 0x06000220 RID: 544 RVA: 0x000135C0 File Offset: 0x000117C0
	public static Cosmetic_Head GetHead(string id)
	{
		IEnumerable<Cosmetic_Head> heads = CosmeticDB.DB.Heads;
		Func<Cosmetic_Head, bool> <>9__0;
		Func<Cosmetic_Head, bool> predicate;
		if ((predicate = <>9__0) == null)
		{
			predicate = (<>9__0 = ((Cosmetic_Head v) => v.GUID == id));
		}
		using (IEnumerator<Cosmetic_Head> enumerator = heads.Where(predicate).GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				return enumerator.Current;
			}
		}
		return CosmeticDB.DB.DefaultHead;
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0001364C File Offset: 0x0001184C
	public static Cosmetic_Skin GetSkin(string id)
	{
		IEnumerable<Cosmetic_Skin> skins = CosmeticDB.DB.Skins;
		Func<Cosmetic_Skin, bool> <>9__0;
		Func<Cosmetic_Skin, bool> predicate;
		if ((predicate = <>9__0) == null)
		{
			predicate = (<>9__0 = ((Cosmetic_Skin v) => v.GUID == id));
		}
		using (IEnumerator<Cosmetic_Skin> enumerator = skins.Where(predicate).GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				return enumerator.Current;
			}
		}
		return CosmeticDB.DB.DefaultSkin;
	}

	// Token: 0x06000222 RID: 546 RVA: 0x000136D8 File Offset: 0x000118D8
	public static Cosmetic_Signature GetSignature(string id)
	{
		IEnumerable<Cosmetic_Signature> signatures = CosmeticDB.DB.Signatures;
		Func<Cosmetic_Signature, bool> <>9__0;
		Func<Cosmetic_Signature, bool> predicate;
		if ((predicate = <>9__0) == null)
		{
			predicate = (<>9__0 = ((Cosmetic_Signature v) => v.GUID == id));
		}
		using (IEnumerator<Cosmetic_Signature> enumerator = signatures.Where(predicate).GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				return enumerator.Current;
			}
		}
		return CosmeticDB.DB.Signatures[0];
	}

	// Token: 0x06000223 RID: 547 RVA: 0x00013768 File Offset: 0x00011968
	public static Cosmetic_Book GetBook(string id)
	{
		IEnumerable<Cosmetic_Book> books = CosmeticDB.DB.Books;
		Func<Cosmetic_Book, bool> <>9__0;
		Func<Cosmetic_Book, bool> predicate;
		if ((predicate = <>9__0) == null)
		{
			predicate = (<>9__0 = ((Cosmetic_Book v) => v.GUID == id));
		}
		using (IEnumerator<Cosmetic_Book> enumerator = books.Where(predicate).GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				return enumerator.Current;
			}
		}
		return CosmeticDB.DB.DefaultBook;
	}

	// Token: 0x06000224 RID: 548 RVA: 0x000137F4 File Offset: 0x000119F4
	public static Cosmetic_Emote GetEmote(string id)
	{
		IEnumerable<Cosmetic_Emote> emotes = CosmeticDB.DB.Emotes;
		Func<Cosmetic_Emote, bool> <>9__0;
		Func<Cosmetic_Emote, bool> predicate;
		if ((predicate = <>9__0) == null)
		{
			predicate = (<>9__0 = ((Cosmetic_Emote v) => v.GUID == id));
		}
		using (IEnumerator<Cosmetic_Emote> enumerator = emotes.Where(predicate).GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				return enumerator.Current;
			}
		}
		return CosmeticDB.DB.Emotes[0];
	}

	// Token: 0x06000225 RID: 549 RVA: 0x00013884 File Offset: 0x00011A84
	public static bool EventIsActive(DateEvent e)
	{
		foreach (CosmeticDB.DateEventInfo dateEventInfo in CosmeticDB.DB.YearlyEvents)
		{
			if (e == dateEventInfo.Event)
			{
				return dateEventInfo.IsActive;
			}
		}
		return false;
	}

	// Token: 0x06000226 RID: 550 RVA: 0x000138EC File Offset: 0x00011AEC
	public static string CosmeticName(CosmeticType cType)
	{
		string result;
		switch (cType)
		{
		case CosmeticType.Skin:
			result = "Raiment";
			break;
		case CosmeticType.Head:
			result = "Stylus";
			break;
		case CosmeticType.Book:
			result = "Book";
			break;
		case CosmeticType.Signature:
			result = "Back";
			break;
		case CosmeticType.Emote:
			result = "Emote";
			break;
		default:
			result = "";
			break;
		}
		return result;
	}

	// Token: 0x06000227 RID: 551 RVA: 0x00013948 File Offset: 0x00011B48
	private void CheckGUIDs()
	{
		string guid = this.DefaultHead.GUID;
		foreach (Cosmetic_Head cosmetic_Head in this.Heads)
		{
			string guid2 = cosmetic_Head.GUID;
		}
		string guid3 = this.DefaultSkin.GUID;
		foreach (Cosmetic_Skin cosmetic_Skin in this.Skins)
		{
			string guid4 = cosmetic_Skin.GUID;
		}
		string guid5 = this.DefaultBook.GUID;
		foreach (Cosmetic_Book cosmetic_Book in this.Books)
		{
			string guid6 = cosmetic_Book.GUID;
		}
	}

	// Token: 0x06000228 RID: 552 RVA: 0x00013A40 File Offset: 0x00011C40
	public CosmeticDB()
	{
	}

	// Token: 0x04000218 RID: 536
	public Cosmetic_Head DefaultHead;

	// Token: 0x04000219 RID: 537
	public List<Cosmetic_Head> Heads;

	// Token: 0x0400021A RID: 538
	public Cosmetic_Skin DefaultSkin;

	// Token: 0x0400021B RID: 539
	public List<Cosmetic_Skin> Skins;

	// Token: 0x0400021C RID: 540
	public Cosmetic_Book DefaultBook;

	// Token: 0x0400021D RID: 541
	public List<Cosmetic_Book> Books;

	// Token: 0x0400021E RID: 542
	public List<Cosmetic_Signature> Signatures;

	// Token: 0x0400021F RID: 543
	public List<Cosmetic_Emote> Emotes;

	// Token: 0x04000220 RID: 544
	[Header("Data Bindings")]
	public List<CosmeticDB.DateEventInfo> YearlyEvents;

	// Token: 0x04000221 RID: 545
	private static CosmeticDB _db;

	// Token: 0x02000435 RID: 1077
	[Serializable]
	public class DateEventInfo
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x000C21F0 File Offset: 0x000C03F0
		public bool IsActive
		{
			get
			{
				DateTime now = DateTime.Now;
				int year = now.Year;
				DateTime t = new DateTime(year, (int)this.startMonth, this.startDay);
				DateTime dateTime = new DateTime(year, (int)this.endMonth, this.endDay).AddDays(1.0).AddTicks(-1L);
				if (dateTime < t)
				{
					dateTime = dateTime.AddYears(1);
				}
				return now >= t && now <= dateTime;
			}
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000C2273 File Offset: 0x000C0473
		public DateEventInfo()
		{
		}

		// Token: 0x04002177 RID: 8567
		public DateEvent Event;

		// Token: 0x04002178 RID: 8568
		public CosmeticDB.DateEventInfo.Month startMonth = CosmeticDB.DateEventInfo.Month.January;

		// Token: 0x04002179 RID: 8569
		public int startDay;

		// Token: 0x0400217A RID: 8570
		[Header("Through")]
		public CosmeticDB.DateEventInfo.Month endMonth = CosmeticDB.DateEventInfo.Month.December;

		// Token: 0x0400217B RID: 8571
		[Header("")]
		public int endDay;

		// Token: 0x020006B1 RID: 1713
		public enum Month
		{
			// Token: 0x04002C99 RID: 11417
			_,
			// Token: 0x04002C9A RID: 11418
			January,
			// Token: 0x04002C9B RID: 11419
			February,
			// Token: 0x04002C9C RID: 11420
			March,
			// Token: 0x04002C9D RID: 11421
			April,
			// Token: 0x04002C9E RID: 11422
			May,
			// Token: 0x04002C9F RID: 11423
			June,
			// Token: 0x04002CA0 RID: 11424
			July,
			// Token: 0x04002CA1 RID: 11425
			August,
			// Token: 0x04002CA2 RID: 11426
			Sepetember,
			// Token: 0x04002CA3 RID: 11427
			October,
			// Token: 0x04002CA4 RID: 11428
			November,
			// Token: 0x04002CA5 RID: 11429
			December
		}
	}

	// Token: 0x02000436 RID: 1078
	[CompilerGenerated]
	private sealed class <>c__DisplayClass17_0
	{
		// Token: 0x0600210F RID: 8463 RVA: 0x000C228A File Offset: 0x000C048A
		public <>c__DisplayClass17_0()
		{
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000C2292 File Offset: 0x000C0492
		internal bool <GetCosmetic>b__0(Cosmetic_Head v)
		{
			return v.cosmeticid == this.cID;
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x000C22A5 File Offset: 0x000C04A5
		internal bool <GetCosmetic>b__1(Cosmetic_Skin v)
		{
			return v.cosmeticid == this.cID;
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x000C22B8 File Offset: 0x000C04B8
		internal bool <GetCosmetic>b__2(Cosmetic_Book v)
		{
			return v.cosmeticid == this.cID;
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x000C22CB File Offset: 0x000C04CB
		internal bool <GetCosmetic>b__3(Cosmetic_Emote v)
		{
			return v.cosmeticid == this.cID;
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x000C22DE File Offset: 0x000C04DE
		internal bool <GetCosmetic>b__4(Cosmetic_Signature v)
		{
			return v.cosmeticid == this.cID;
		}

		// Token: 0x0400217C RID: 8572
		public string cID;

		// Token: 0x0400217D RID: 8573
		public Func<Cosmetic_Head, bool> <>9__0;

		// Token: 0x0400217E RID: 8574
		public Func<Cosmetic_Skin, bool> <>9__1;

		// Token: 0x0400217F RID: 8575
		public Func<Cosmetic_Book, bool> <>9__2;

		// Token: 0x04002180 RID: 8576
		public Func<Cosmetic_Emote, bool> <>9__3;

		// Token: 0x04002181 RID: 8577
		public Func<Cosmetic_Signature, bool> <>9__4;
	}

	// Token: 0x02000437 RID: 1079
	[CompilerGenerated]
	private sealed class <>c__DisplayClass18_0
	{
		// Token: 0x06002115 RID: 8469 RVA: 0x000C22F1 File Offset: 0x000C04F1
		public <>c__DisplayClass18_0()
		{
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x000C22F9 File Offset: 0x000C04F9
		internal bool <GetHead>b__0(Cosmetic_Head v)
		{
			return v.GUID == this.id;
		}

		// Token: 0x04002182 RID: 8578
		public string id;

		// Token: 0x04002183 RID: 8579
		public Func<Cosmetic_Head, bool> <>9__0;
	}

	// Token: 0x02000438 RID: 1080
	[CompilerGenerated]
	private sealed class <>c__DisplayClass19_0
	{
		// Token: 0x06002117 RID: 8471 RVA: 0x000C230C File Offset: 0x000C050C
		public <>c__DisplayClass19_0()
		{
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x000C2314 File Offset: 0x000C0514
		internal bool <GetSkin>b__0(Cosmetic_Skin v)
		{
			return v.GUID == this.id;
		}

		// Token: 0x04002184 RID: 8580
		public string id;

		// Token: 0x04002185 RID: 8581
		public Func<Cosmetic_Skin, bool> <>9__0;
	}

	// Token: 0x02000439 RID: 1081
	[CompilerGenerated]
	private sealed class <>c__DisplayClass20_0
	{
		// Token: 0x06002119 RID: 8473 RVA: 0x000C2327 File Offset: 0x000C0527
		public <>c__DisplayClass20_0()
		{
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x000C232F File Offset: 0x000C052F
		internal bool <GetSignature>b__0(Cosmetic_Signature v)
		{
			return v.GUID == this.id;
		}

		// Token: 0x04002186 RID: 8582
		public string id;

		// Token: 0x04002187 RID: 8583
		public Func<Cosmetic_Signature, bool> <>9__0;
	}

	// Token: 0x0200043A RID: 1082
	[CompilerGenerated]
	private sealed class <>c__DisplayClass21_0
	{
		// Token: 0x0600211B RID: 8475 RVA: 0x000C2342 File Offset: 0x000C0542
		public <>c__DisplayClass21_0()
		{
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x000C234A File Offset: 0x000C054A
		internal bool <GetBook>b__0(Cosmetic_Book v)
		{
			return v.GUID == this.id;
		}

		// Token: 0x04002188 RID: 8584
		public string id;

		// Token: 0x04002189 RID: 8585
		public Func<Cosmetic_Book, bool> <>9__0;
	}

	// Token: 0x0200043B RID: 1083
	[CompilerGenerated]
	private sealed class <>c__DisplayClass22_0
	{
		// Token: 0x0600211D RID: 8477 RVA: 0x000C235D File Offset: 0x000C055D
		public <>c__DisplayClass22_0()
		{
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000C2365 File Offset: 0x000C0565
		internal bool <GetEmote>b__0(Cosmetic_Emote v)
		{
			return v.GUID == this.id;
		}

		// Token: 0x0400218A RID: 8586
		public string id;

		// Token: 0x0400218B RID: 8587
		public Func<Cosmetic_Emote, bool> <>9__0;
	}
}
