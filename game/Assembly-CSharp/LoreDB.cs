using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000052 RID: 82
public class LoreDB : ScriptableObject
{
	// Token: 0x0600027D RID: 637 RVA: 0x00015FD8 File Offset: 0x000141D8
	public static void SetInstance(LoreDB db)
	{
		LoreDB.instance = db;
	}

	// Token: 0x0600027E RID: 638 RVA: 0x00015FE0 File Offset: 0x000141E0
	public static LoreDB.LorePage GetPage(string UID)
	{
		if (LoreDB.instance == null)
		{
			return null;
		}
		return LoreDB.instance.Pages.Find((LoreDB.LorePage v) => v.UID == UID);
	}

	// Token: 0x0600027F RID: 639 RVA: 0x00016024 File Offset: 0x00014224
	public static LoreDB.CharacterInfo GetCharacter(LoreDB.Character character, LoreDB.Era era)
	{
		if (LoreDB.instance == null)
		{
			return null;
		}
		foreach (LoreDB.CharacterInfo characterInfo in LoreDB.instance.Characters)
		{
			if (characterInfo.Character == character && characterInfo.Era == era)
			{
				return characterInfo;
			}
		}
		return null;
	}

	// Token: 0x06000280 RID: 640 RVA: 0x0001609C File Offset: 0x0001429C
	public static LoreDB.PauseQuote GetPauseQuote(string ID)
	{
		return LoreDB.instance.PauseQuotes.FirstOrDefault((LoreDB.PauseQuote v) => v.ID == ID);
	}

	// Token: 0x06000281 RID: 641 RVA: 0x000160D4 File Offset: 0x000142D4
	public static string SelectPauseQuoteID(GenreTree tome, int bindingLevel)
	{
		List<LoreDB.PauseQuote> list = new List<LoreDB.PauseQuote>();
		foreach (LoreDB.PauseQuote pauseQuote in LoreDB.instance.PauseQuotes)
		{
			if (!pauseQuote.LibraryQuote && (pauseQuote.Tome == tome || pauseQuote.Tome == null))
			{
				list.Add(pauseQuote);
			}
		}
		if (list.Count == 0)
		{
			return "";
		}
		return list[UnityEngine.Random.Range(0, list.Count)].ID;
	}

	// Token: 0x06000282 RID: 642 RVA: 0x0001617C File Offset: 0x0001437C
	public static string SelectLibraryQuote()
	{
		List<LoreDB.PauseQuote> list = new List<LoreDB.PauseQuote>();
		foreach (LoreDB.PauseQuote pauseQuote in LoreDB.instance.PauseQuotes)
		{
			if (pauseQuote.LibraryQuote)
			{
				list.Add(pauseQuote);
			}
		}
		if (list.Count == 0)
		{
			return "";
		}
		return list[UnityEngine.Random.Range(0, list.Count)].ID;
	}

	// Token: 0x06000283 RID: 643 RVA: 0x00016208 File Offset: 0x00014408
	public LoreDB()
	{
	}

	// Token: 0x04000275 RID: 629
	public static LoreDB instance;

	// Token: 0x04000276 RID: 630
	public List<LoreDB.LorePage> Pages = new List<LoreDB.LorePage>();

	// Token: 0x04000277 RID: 631
	public List<LoreDB.CharacterInfo> Characters = new List<LoreDB.CharacterInfo>();

	// Token: 0x04000278 RID: 632
	public List<LoreDB.PauseQuote> PauseQuotes = new List<LoreDB.PauseQuote>();

	// Token: 0x02000447 RID: 1095
	[Serializable]
	public class LorePage
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06002132 RID: 8498 RVA: 0x000C2605 File Offset: 0x000C0805
		public LoreDB.CharacterInfo CharacterInfo
		{
			get
			{
				return LoreDB.GetCharacter(this.Character, this.Era);
			}
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x000C2618 File Offset: 0x000C0818
		public string GetUnlockInfo()
		{
			if (this.Location == LoreDB.LorePage.PageLocation.Attunement)
			{
				return string.Format("Attunement: {0}", this.AttunementLevel);
			}
			if (this.Location == LoreDB.LorePage.PageLocation.Vignette)
			{
				return this.Vignette;
			}
			if (this.Location == LoreDB.LorePage.PageLocation.Explicit || this.Location == LoreDB.LorePage.PageLocation.Tutorial || this.Location == LoreDB.LorePage.PageLocation.Library)
			{
				return this.UnlockText;
			}
			return "Unknown Unlock Info";
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x000C267A File Offset: 0x000C087A
		public LorePage()
		{
		}

		// Token: 0x040021B6 RID: 8630
		public string UID;

		// Token: 0x040021B7 RID: 8631
		public string Title;

		// Token: 0x040021B8 RID: 8632
		[TextArea(3, 4)]
		public string Body;

		// Token: 0x040021B9 RID: 8633
		public int PageNumber;

		// Token: 0x040021BA RID: 8634
		public LoreDB.Character Character;

		// Token: 0x040021BB RID: 8635
		public LoreDB.Era Era;

		// Token: 0x040021BC RID: 8636
		[TextArea(1, 2)]
		public string Signature;

		// Token: 0x040021BD RID: 8637
		public LoreDB.LorePage.PageLocation Location;

		// Token: 0x040021BE RID: 8638
		public string Vignette;

		// Token: 0x040021BF RID: 8639
		public int AttunementLevel;

		// Token: 0x040021C0 RID: 8640
		public string UnlockText;

		// Token: 0x020006B2 RID: 1714
		public enum PageLocation
		{
			// Token: 0x04002CA7 RID: 11431
			Vignette,
			// Token: 0x04002CA8 RID: 11432
			Tutorial,
			// Token: 0x04002CA9 RID: 11433
			Library,
			// Token: 0x04002CAA RID: 11434
			Attunement,
			// Token: 0x04002CAB RID: 11435
			Explicit
		}
	}

	// Token: 0x02000448 RID: 1096
	[Serializable]
	public class CharacterInfo
	{
		// Token: 0x06002135 RID: 8501 RVA: 0x000C2682 File Offset: 0x000C0882
		public CharacterInfo()
		{
		}

		// Token: 0x040021C1 RID: 8641
		public LoreDB.Character Character;

		// Token: 0x040021C2 RID: 8642
		public LoreDB.Era Era;

		// Token: 0x040021C3 RID: 8643
		public string Name;

		// Token: 0x040021C4 RID: 8644
		public string Signature;

		// Token: 0x040021C5 RID: 8645
		public Color TextColor = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x02000449 RID: 1097
	[Serializable]
	public class PauseQuote
	{
		// Token: 0x06002136 RID: 8502 RVA: 0x000C26A9 File Offset: 0x000C08A9
		private void Apply_1()
		{
			this.Apply(this.Chapter_1);
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x000C26B7 File Offset: 0x000C08B7
		private void Apply_2()
		{
			this.Apply(this.Chapter_2);
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x000C26C5 File Offset: 0x000C08C5
		private void Apply_3()
		{
			this.Apply(this.Chapter_3);
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x000C26D3 File Offset: 0x000C08D3
		private void Apply_4()
		{
			this.Apply(this.Chapter_4);
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x000C26E1 File Offset: 0x000C08E1
		private void Apply_5()
		{
			this.Apply(this.Chapter_5);
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x000C26EF File Offset: 0x000C08EF
		private void Apply_6()
		{
			this.Apply(this.Chapter_6);
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x000C26FD File Offset: 0x000C08FD
		private void Apply_Appendix()
		{
			this.Apply(this.Appendix);
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x000C270C File Offset: 0x000C090C
		public string GetText(int chapter)
		{
			string result;
			switch (chapter)
			{
			case 1:
				result = this.Chapter_1;
				break;
			case 2:
				result = this.Chapter_2;
				break;
			case 3:
				result = this.Chapter_3;
				break;
			case 4:
				result = this.Chapter_4;
				break;
			case 5:
				result = this.Chapter_5;
				break;
			case 6:
				result = this.Chapter_6;
				break;
			default:
				result = this.Chapter_1;
				break;
			}
			return result;
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x000C2779 File Offset: 0x000C0979
		private void Apply(string text)
		{
			if (!Application.isPlaying || PlayerControl.myInstance == null)
			{
				return;
			}
			PausePanel.instance.Quotes.Apply(this, text);
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x000C27A1 File Offset: 0x000C09A1
		public PauseQuote()
		{
		}

		// Token: 0x040021C6 RID: 8646
		public string ID;

		// Token: 0x040021C7 RID: 8647
		[TextArea(8, 12)]
		public string Chapter_1;

		// Token: 0x040021C8 RID: 8648
		[TextArea(8, 12)]
		public string Chapter_2;

		// Token: 0x040021C9 RID: 8649
		[TextArea(8, 12)]
		public string Chapter_3;

		// Token: 0x040021CA RID: 8650
		[TextArea(8, 12)]
		public string Chapter_4;

		// Token: 0x040021CB RID: 8651
		[TextArea(8, 12)]
		public string Chapter_5;

		// Token: 0x040021CC RID: 8652
		[TextArea(8, 12)]
		public string Chapter_6;

		// Token: 0x040021CD RID: 8653
		[TextArea(8, 12)]
		public string Appendix;

		// Token: 0x040021CE RID: 8654
		public LoreDB.PauseQuote.TextAlign Alignment = LoreDB.PauseQuote.TextAlign.Center;

		// Token: 0x040021CF RID: 8655
		public bool LibraryQuote;

		// Token: 0x040021D0 RID: 8656
		public GenreTree Tome;

		// Token: 0x020006B3 RID: 1715
		public enum TextAlign
		{
			// Token: 0x04002CAD RID: 11437
			Left,
			// Token: 0x04002CAE RID: 11438
			Center,
			// Token: 0x04002CAF RID: 11439
			Right
		}
	}

	// Token: 0x0200044A RID: 1098
	public enum Character
	{
		// Token: 0x040021D2 RID: 8658
		FirstScribe,
		// Token: 0x040021D3 RID: 8659
		Eigengrau,
		// Token: 0x040021D4 RID: 8660
		Blurb,
		// Token: 0x040021D5 RID: 8661
		Horizon,
		// Token: 0x040021D6 RID: 8662
		Font,
		// Token: 0x040021D7 RID: 8663
		Narrator,
		// Token: 0x040021D8 RID: 8664
		Tome
	}

	// Token: 0x0200044B RID: 1099
	public enum Era
	{
		// Token: 0x040021DA RID: 8666
		OldWorld,
		// Token: 0x040021DB RID: 8667
		PostTragedy,
		// Token: 0x040021DC RID: 8668
		NewWorld
	}

	// Token: 0x0200044C RID: 1100
	[CompilerGenerated]
	private sealed class <>c__DisplayClass5_0
	{
		// Token: 0x06002140 RID: 8512 RVA: 0x000C27B0 File Offset: 0x000C09B0
		public <>c__DisplayClass5_0()
		{
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x000C27B8 File Offset: 0x000C09B8
		internal bool <GetPage>b__0(LoreDB.LorePage v)
		{
			return v.UID == this.UID;
		}

		// Token: 0x040021DD RID: 8669
		public string UID;
	}

	// Token: 0x0200044D RID: 1101
	[CompilerGenerated]
	private sealed class <>c__DisplayClass7_0
	{
		// Token: 0x06002142 RID: 8514 RVA: 0x000C27CB File Offset: 0x000C09CB
		public <>c__DisplayClass7_0()
		{
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x000C27D3 File Offset: 0x000C09D3
		internal bool <GetPauseQuote>b__0(LoreDB.PauseQuote v)
		{
			return v.ID == this.ID;
		}

		// Token: 0x040021DE RID: 8670
		public string ID;
	}
}
