using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200011C RID: 284
public class Library_LorePages : MonoBehaviour
{
	// Token: 0x06000D5A RID: 3418 RVA: 0x00055541 File Offset: 0x00053741
	private void Awake()
	{
		Library_LorePages.instance = this;
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x0005554C File Offset: 0x0005374C
	private void Start()
	{
		Tutorial_CoreSelect component = this.CoreSelector.GetComponent<Tutorial_CoreSelect>();
		component.OnSelectedCore = (Action)Delegate.Combine(component.OnSelectedCore, new Action(this.TryNextLibraryTutorial));
		LorePage postTutorialPage = this.PostTutorialPage;
		postTutorialPage.OnUse = (Action)Delegate.Combine(postTutorialPage.OnUse, new Action(this.TryNextLibraryTutorial));
		LorePage firstRunPage = this.FirstRunPage;
		firstRunPage.OnUse = (Action)Delegate.Combine(firstRunPage.OnUse, new Action(delegate()
		{
			LibraryManager.instance.BookstandIndicator.gameObject.SetActive(true);
		}));
		LorePage bindingLorePage = this.BindingLorePage;
		bindingLorePage.OnUse = (Action)Delegate.Combine(bindingLorePage.OnUse, new Action(delegate()
		{
			LibraryManager.FinishedTutorial(LibraryTutorial.Bindings);
		}));
		LorePage metaLorePage = this.MetaLorePage;
		metaLorePage.OnUse = (Action)Delegate.Combine(metaLorePage.OnUse, new Action(delegate()
		{
			LibraryManager.instance.MetaIndicator.gameObject.SetActive(true);
		}));
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x0005565C File Offset: 0x0005385C
	public void TryNextLibraryTutorial()
	{
		if (TutorialManager.IsReturning)
		{
			TutorialManager.IsReturning = false;
			this.PostTutorialPage.gameObject.SetActive(true);
			return;
		}
		if (!Settings.HasCompletedLibraryTutorial(LibraryTutorial.Abilities))
		{
			this.StartLibraryTutorial(LibraryTutorial.Abilities);
		}
		else if (!UnlockManager.HasSeenLorePage(this.FirstRunPage.UID))
		{
			this.FirstRunPage.gameObject.SetActive(true);
		}
		else if (!Settings.HasCompletedLibraryTutorial(LibraryTutorial.Meta) && GameStats.GetGlobalStat(GameStats.Stat.TomesPlayed, 0) > 0)
		{
			this.StartLibraryTutorial(LibraryTutorial.Meta);
		}
		else if (!Settings.HasCompletedLibraryTutorial(LibraryTutorial.Bindings) && GameStats.GetTomeStat(LibraryManager.instance.BindingWinRequirement, GameStats.Stat.TomesWon, 0) > 0 && !UnlockManager.SeenLorePages.Contains("LIB_BINDINGS"))
		{
			this.StartLibraryTutorial(LibraryTutorial.Bindings);
		}
		else if (Progression.BindingAttunement >= 1)
		{
			if (!UnlockManager.HasSeenLorePage(this.CodexLorePage.UID))
			{
				this.CodexLorePage.gameObject.SetActive(true);
			}
			if (!UnlockManager.HasSeenLorePage(this.CosmeticLorePage.UID))
			{
				this.CosmeticLorePage.gameObject.SetActive(true);
			}
		}
		foreach (Library_LorePages.TomePage tomePage in this.TomePages)
		{
			if (!(tomePage.Tome == null) && GameStats.GetTomeStat(tomePage.Tome, GameStats.Stat.TomesWon, 0) > 0)
			{
				if (tomePage.FirstCompletion != null && !UnlockManager.HasSeenLorePage(tomePage.FirstCompletion.UID))
				{
					tomePage.FirstCompletion.gameObject.SetActive(true);
				}
				if (tomePage.Binding20 != null && GameStats.GetTomeStat(tomePage.Tome, GameStats.Stat.MaxBinding, 0) >= 20 && !UnlockManager.HasSeenLorePage(tomePage.Binding20.UID))
				{
					tomePage.Binding20.gameObject.SetActive(true);
				}
			}
		}
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x00055840 File Offset: 0x00053A40
	public void OnEnteredLibrary()
	{
		if (PlayerControl.myInstance != null && PlayerControl.myInstance.actions.core.Root.magicColor == MagicColor.Neutral)
		{
			this.CoreSelector.gameObject.SetActive(true);
			return;
		}
		this.TryNextLibraryTutorial();
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x00055890 File Offset: 0x00053A90
	private Library_LorePages.TomePage GetTomePage(GenreTree tome)
	{
		foreach (Library_LorePages.TomePage tomePage in this.TomePages)
		{
			if (tomePage.Tome == tome)
			{
				return tomePage;
			}
		}
		return null;
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x000558F4 File Offset: 0x00053AF4
	public static bool ShouldShowNookTutorial(string UID)
	{
		return Progression.BindingAttunement >= 1 && !UnlockManager.HasSeenLorePage(UID);
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x0005590C File Offset: 0x00053B0C
	private void StartLibraryTutorial(LibraryTutorial step)
	{
		if (Settings.HasCompletedLibraryTutorial(step))
		{
			return;
		}
		Debug.Log("Starting Library Tutorial" + step.ToString());
		LibraryManager.InLibraryTutorial = true;
		LibraryManager.CurrentStep = step;
		switch (step)
		{
		case LibraryTutorial.Tomes:
			LibraryManager.instance.TomesIndicator.gameObject.SetActive(true);
			return;
		case LibraryTutorial.Bindings:
			this.BindingLorePage.gameObject.SetActive(true);
			Fountain.instance.WaveTornado_Base.Play();
			Fountain.instance.WaveTornado_Extra.Play();
			return;
		case LibraryTutorial.Meta:
			this.BookItemsLorePage.gameObject.SetActive(true);
			this.MetaLorePage.gameObject.SetActive(true);
			return;
		case LibraryTutorial.Abilities:
			LibraryManager.instance.AbilityIndicator.gameObject.SetActive(true);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x000559DF File Offset: 0x00053BDF
	public Library_LorePages()
	{
	}

	// Token: 0x04000AEE RID: 2798
	public static Library_LorePages instance;

	// Token: 0x04000AEF RID: 2799
	public GameObject CoreSelector;

	// Token: 0x04000AF0 RID: 2800
	public LorePage PostTutorialPage;

	// Token: 0x04000AF1 RID: 2801
	public LorePage FirstRunPage;

	// Token: 0x04000AF2 RID: 2802
	public LorePage BookItemsLorePage;

	// Token: 0x04000AF3 RID: 2803
	public LorePage MetaLorePage;

	// Token: 0x04000AF4 RID: 2804
	public LorePage BindingLorePage;

	// Token: 0x04000AF5 RID: 2805
	public LorePage CodexLorePage;

	// Token: 0x04000AF6 RID: 2806
	public LorePage CosmeticLorePage;

	// Token: 0x04000AF7 RID: 2807
	public GenreTree RaidAccessTome;

	// Token: 0x04000AF8 RID: 2808
	public LorePage MyriadRaidPage;

	// Token: 0x04000AF9 RID: 2809
	public LorePage VerseRaidPage;

	// Token: 0x04000AFA RID: 2810
	public LorePage HorizonRaidPage;

	// Token: 0x04000AFB RID: 2811
	public List<Library_LorePages.TomePage> TomePages = new List<Library_LorePages.TomePage>();

	// Token: 0x02000527 RID: 1319
	[Serializable]
	public class TomePage
	{
		// Token: 0x060023F3 RID: 9203 RVA: 0x000CC883 File Offset: 0x000CAA83
		public TomePage()
		{
		}

		// Token: 0x04002613 RID: 9747
		public GenreTree Tome;

		// Token: 0x04002614 RID: 9748
		public LorePage FirstCompletion;

		// Token: 0x04002615 RID: 9749
		public LorePage Binding20;
	}

	// Token: 0x02000528 RID: 1320
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x060023F4 RID: 9204 RVA: 0x000CC88B File Offset: 0x000CAA8B
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x000CC897 File Offset: 0x000CAA97
		public <>c()
		{
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000CC89F File Offset: 0x000CAA9F
		internal void <Start>b__15_0()
		{
			LibraryManager.instance.BookstandIndicator.gameObject.SetActive(true);
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000CC8B6 File Offset: 0x000CAAB6
		internal void <Start>b__15_1()
		{
			LibraryManager.FinishedTutorial(LibraryTutorial.Bindings);
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x000CC8BE File Offset: 0x000CAABE
		internal void <Start>b__15_2()
		{
			LibraryManager.instance.MetaIndicator.gameObject.SetActive(true);
		}

		// Token: 0x04002616 RID: 9750
		public static readonly Library_LorePages.<>c <>9 = new Library_LorePages.<>c();

		// Token: 0x04002617 RID: 9751
		public static Action <>9__15_0;

		// Token: 0x04002618 RID: 9752
		public static Action <>9__15_1;

		// Token: 0x04002619 RID: 9753
		public static Action <>9__15_2;
	}
}
