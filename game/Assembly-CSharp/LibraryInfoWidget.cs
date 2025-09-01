using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

// Token: 0x02000160 RID: 352
public class LibraryInfoWidget : MonoBehaviour
{
	// Token: 0x1700012C RID: 300
	// (get) Token: 0x06000F61 RID: 3937 RVA: 0x00061884 File Offset: 0x0005FA84
	private bool WantVisible
	{
		get
		{
			return GameHUD.Mode != GameHUD.HUDMode.Off && !SignatureInkUIControl.IsInPrestige && GameplayManager.CurState != GameState.Hub_Traveling && PanelManager.CurPanel != PanelType.Codex && PanelManager.CurPanel != PanelType.Codex_Enemies && PanelManager.CurPanel != PanelType.Codex_Stats && PanelManager.CurPanel != PanelType.BookClub && PanelManager.CurPanel != PanelType.RaidDifficulty && PanelManager.CurPanel != PanelType.LibraryMeta && !PlayerNook.IsInEditMode;
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x06000F62 RID: 3938 RVA: 0x000618F6 File Offset: 0x0005FAF6
	private bool ShowQuillmarks
	{
		get
		{
			return !LibraryInfoWidget.notifying && !this.ShowAttunement && !this.ShowGildings;
		}
	}

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x06000F63 RID: 3939 RVA: 0x00061912 File Offset: 0x0005FB12
	private bool ShowGildings
	{
		get
		{
			return Library_DiceGame.IsInAreaOfInfluence;
		}
	}

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0006191C File Offset: 0x0005FB1C
	private bool ShowAttunement
	{
		get
		{
			return !LibraryInfoWidget.notifying && ((UITutorial.InTutorial && UITutorial.CurTut == UITutorial.Tutorial.Bindings) || (Library_RaidControl.IsInAntechamber && PanelManager.CurPanel == PanelType.GameInvisible) || (GameStats.GetTomeStat(GenrePanel.instance.bindingReqGenre, GameStats.Stat.TomesWon, 0) > 0 && (PanelManager.CurPanel == PanelType.Genre_Selection || PanelManager.CurPanel == PanelType.Bindings)));
		}
	}

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0006197E File Offset: 0x0005FB7E
	private bool ShouldQuillGlow
	{
		get
		{
			return this.WantVisible && this.ShowQuillmarks && (PanelManager.CurPanel == PanelType.SignatureInk || PanelManager.CurPanel == PanelType.Customize_Abilities);
		}
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x000619A5 File Offset: 0x0005FBA5
	private void Awake()
	{
		LibraryInfoWidget.instance = this;
		LibraryInfoWidget.notifying = false;
		GameplayManager.OnGenereChanged = (Action<GenreTree>)Delegate.Combine(GameplayManager.OnGenereChanged, new Action<GenreTree>(this.TomePrepared));
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x000619D4 File Offset: 0x0005FBD4
	private void Update()
	{
		if (!MapManager.InLobbyScene || PlayerControl.myInstance == null)
		{
			if (this.MainGroup.alpha > 0f)
			{
				this.MainGroup.alpha = 0f;
			}
			if (this.notes.Count > 0)
			{
				this.notes.Clear();
			}
			this.NoteGroup.alpha = 0f;
			LibraryInfoWidget.notifying = false;
			return;
		}
		this.MainGroup.UpdateOpacity(this.WantVisible, 3f, false);
		this.UpdateNotifications();
		this.UpdateQuillmarks();
		this.UpdateGildings();
		this.UpdateAttunement();
	}

	// Token: 0x06000F68 RID: 3944 RVA: 0x00061A78 File Offset: 0x0005FC78
	public static void Notify(string header, string title, string detail)
	{
		LibraryInfoWidget.Note item = new LibraryInfoWidget.Note
		{
			Category = header,
			Title = title,
			Detail = detail
		};
		LibraryInfoWidget.instance.notes.Enqueue(item);
		LibraryInfoWidget.instance.noteSafety = 25f;
	}

	// Token: 0x06000F69 RID: 3945 RVA: 0x00061ABF File Offset: 0x0005FCBF
	public static void GildingsGained(int amount)
	{
		if (!MapManager.InLobbyScene || PlayerControl.myInstance == null)
		{
			return;
		}
		LibraryInfoWidget.Notify(string.Format("{0} Gildings!", amount), "\n<size=100><sprite name=gilding>", "");
	}

	// Token: 0x06000F6A RID: 3946 RVA: 0x00061AF5 File Offset: 0x0005FCF5
	public static void QuillmarksGained(int amount)
	{
		if (!MapManager.InLobbyScene || PlayerControl.myInstance == null)
		{
			return;
		}
		LibraryInfoWidget.Notify(string.Format("{0} Quillmarks!", amount), "\n<size=80><sprite name=quillmark>", "");
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x00061B2C File Offset: 0x0005FD2C
	private void TomePrepared(GenreTree tome)
	{
		if (TutorialManager.InTutorial || tome == null)
		{
			return;
		}
		if (GameplayManager.IsChallengeActive)
		{
			LibraryInfoWidget.Notify("Challenge Prepared", GameplayManager.Challenge.Name, "Go to The Font to begin!");
			return;
		}
		LibraryInfoWidget.Notify("Tome Prepared", tome.Root.Name, "Go to The Font to begin!");
	}

	// Token: 0x06000F6C RID: 3948 RVA: 0x00061B88 File Offset: 0x0005FD88
	private void AddNotification(GraphTree Item)
	{
		LibraryInfoWidget.Note note = null;
		GenreTree genreTree = Item as GenreTree;
		if (genreTree != null)
		{
			note = LibraryInfoWidget.RewardNote.FromTome(genreTree);
		}
		else
		{
			AugmentTree augmentTree = Item as AugmentTree;
			if (augmentTree != null)
			{
				note = LibraryInfoWidget.RewardNote.FromAugment(augmentTree);
			}
		}
		if (note != null)
		{
			LibraryInfoWidget.instance.noteSafety = 25f;
			this.notes.Enqueue(note);
		}
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x00061BDC File Offset: 0x0005FDDC
	private void AddBinding(AugmentTree augment)
	{
		LibraryInfoWidget.Note item = LibraryInfoWidget.RewardNote.FromBinding(augment);
		this.notes.Enqueue(item);
		LibraryInfoWidget.instance.noteSafety = 25f;
	}

	// Token: 0x06000F6E RID: 3950 RVA: 0x00061C0C File Offset: 0x0005FE0C
	private void UpdateNotifications()
	{
		if (LibraryInfoWidget.notifying)
		{
			this.noteSafety -= Time.deltaTime;
			if (this.noteSafety <= 0f || PanelManager.CurPanel == PanelType.Bindings)
			{
				LibraryInfoWidget.notifying = false;
			}
			return;
		}
		if (this.notes.Count > 0)
		{
			LibraryInfoWidget.Note n = this.notes.Dequeue();
			this.ShowNotification(n);
			return;
		}
		this.NoteGroup.UpdateOpacity(false, 2f, true);
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x00061C84 File Offset: 0x0005FE84
	public void LoadRewardToasts()
	{
		if (Progression.TomeRewards.Count > 0)
		{
			foreach (GenreTree item in Progression.TomeRewards)
			{
				this.AddNotification(item);
			}
		}
		if (Progression.BindingRewards.Count > 0)
		{
			foreach (AugmentTree augment in Progression.BindingRewards)
			{
				this.AddBinding(augment);
			}
		}
		if (Progression.AugmentRewards.Count > 0)
		{
			foreach (AugmentTree item2 in Progression.AugmentRewards)
			{
				this.AddNotification(item2);
			}
		}
		Progression.ConsumeRewardInfo();
	}

	// Token: 0x06000F70 RID: 3952 RVA: 0x00061D88 File Offset: 0x0005FF88
	private void ShowNotification(LibraryInfoWidget.Note n)
	{
		LibraryInfoWidget.notifying = true;
		this.RewardTitle.text = n.Title;
		this.RewardCategory.text = n.Category;
		this.RewardDetail.text = n.Detail;
		float pitch = 1.1f - Mathf.Min(0.2f, (float)this.notes.Count * 0.05f);
		AudioManager.PlayInterfaceSFX(this.NewRewardSFX, 1f, pitch);
		this.NoteGroup.alpha = 0f;
		float showTime = n.showTime;
		base.StopCoroutine("NoteSequence");
		base.StartCoroutine("NoteSequence", showTime);
	}

	// Token: 0x06000F71 RID: 3953 RVA: 0x00061E36 File Offset: 0x00060036
	private IEnumerator NoteSequence(float closeTime)
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime * 4f;
			this.NoteGroup.alpha = t;
			yield return true;
		}
		yield return new WaitForSeconds(closeTime);
		t = 1f;
		while (t > 0f)
		{
			t -= Time.deltaTime * 4f;
			this.NoteGroup.alpha = t;
			yield return true;
		}
		LibraryInfoWidget.notifying = false;
		yield break;
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x00061E4C File Offset: 0x0006004C
	private void UpdateQuillmarks()
	{
		this.QuillmarkGroup.UpdateOpacity(this.ShowQuillmarks, 3f, false);
		this.QuillGlow.UpdateOpacity(this.ShouldQuillGlow, 2f, true);
		if (!this.ShowQuillmarks && this.isQuillHovered)
		{
			this.OnReleaseHover();
		}
		this.curQuillDisplay = Mathf.Lerp(this.curQuillDisplay, (float)Currency.LoadoutCoin, Time.deltaTime * 4f);
		this.curQuillDisplay = Mathf.MoveTowards(this.curQuillDisplay, (float)Currency.LoadoutCoin, 1f);
		string text = string.Format("{0}", (int)this.curQuillDisplay);
		if (this.QuillText.text != text)
		{
			this.QuillText.text = text;
		}
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x00061F18 File Offset: 0x00060118
	public static void QuillmarksSpent(int amount, Transform t)
	{
		if (LibraryInfoWidget.instance == null || !LibraryInfoWidget.instance.WantVisible || t == null)
		{
			return;
		}
		AudioManager.PlayInterfaceSFX(LibraryInfoWidget.instance.QuillsSpentSFX, 1f, 0f);
		LibraryInfoWidget.instance.QuillPullField.position = t.position;
		LibraryInfoWidget.instance.QuillParticles.Play();
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x00061F88 File Offset: 0x00060188
	public static void QuilmarkBurst(int amount, Transform t)
	{
		if (LibraryInfoWidget.instance == null || !LibraryInfoWidget.instance.WantVisible || t == null)
		{
			return;
		}
		LibraryInfoWidget.instance.QuillPullField.position = t.position;
		LibraryInfoWidget.instance.QuillParticles.Emit(amount);
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x00061FE0 File Offset: 0x000601E0
	public void OnHover()
	{
		if (this.isQuillHovered)
		{
			return;
		}
		this.isQuillHovered = true;
		Tooltip.SimpleInfoData data = new Tooltip.SimpleInfoData
		{
			Title = "Quillmarks",
			Detail = "Quillmarks are earned by mending Tomes.",
			Size = new Vector2(400f, 200f)
		};
		Tooltip.Show(this.TooltipAnchor.position, TextAnchor.MiddleRight, data);
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x00062040 File Offset: 0x00060240
	public void OnReleaseHover()
	{
		if (!this.isQuillHovered)
		{
			return;
		}
		this.isQuillHovered = false;
		Tooltip.Release();
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x00062058 File Offset: 0x00060258
	private void UpdateGildings()
	{
		this.GildingGroup.UpdateOpacity(this.ShowGildings, 3f, false);
		this.curGildingDisplay = Mathf.Lerp(this.curGildingDisplay, (float)Currency.Gildings, Time.deltaTime * 4f);
		this.curGildingDisplay = Mathf.MoveTowards(this.curGildingDisplay, (float)Currency.Gildings, 1f);
		string text = string.Format("{0}", (int)this.curGildingDisplay);
		if (this.GildingText.text != text)
		{
			this.GildingText.text = text;
		}
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x000620F8 File Offset: 0x000602F8
	private void UpdateAttunement()
	{
		this.AttuneGroup.UpdateOpacity(this.ShowAttunement, 3f, false);
		if (this.AttuneText.text != Progression.BindingAttunement.ToString())
		{
			this.AttuneText.text = Progression.BindingAttunement.ToString();
		}
		if (this.ShowAttunement)
		{
			int currentBindingLevel = BindingsPanel.instance.CurrentBindingLevel;
			bool flag = PanelManager.CurPanel == PanelType.Bindings;
			flag &= (GameplayManager.CurState == GameState.Hub_Bindings);
			flag &= (currentBindingLevel >= Progression.AttunementTarget);
			flag &= (Progression.BindingAttunement < 20);
			this.AttuneRewardGroup.UpdateOpacity(flag, 3f, true);
			if (Progression.OverbindAllowed > 0)
			{
				int attunementBoost = Progression.GetAttunementBoost(currentBindingLevel);
				this.AttuneBoostText.text = string.Format("+{0}", attunementBoost);
				return;
			}
			this.AttuneBoostText.text = "+1";
		}
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x000621E0 File Offset: 0x000603E0
	public LibraryInfoWidget()
	{
	}

	// Token: 0x04000D49 RID: 3401
	public static LibraryInfoWidget instance;

	// Token: 0x04000D4A RID: 3402
	public CanvasGroup MainGroup;

	// Token: 0x04000D4B RID: 3403
	public CanvasGroup QuillmarkGroup;

	// Token: 0x04000D4C RID: 3404
	public CanvasGroup QuillGlow;

	// Token: 0x04000D4D RID: 3405
	public TextMeshProUGUI QuillText;

	// Token: 0x04000D4E RID: 3406
	private float curQuillDisplay;

	// Token: 0x04000D4F RID: 3407
	public ParticleSystem QuillParticles;

	// Token: 0x04000D50 RID: 3408
	public Transform QuillPullField;

	// Token: 0x04000D51 RID: 3409
	public AudioClip QuillsSpentSFX;

	// Token: 0x04000D52 RID: 3410
	public RectTransform TooltipAnchor;

	// Token: 0x04000D53 RID: 3411
	public CanvasGroup GildingGroup;

	// Token: 0x04000D54 RID: 3412
	public CanvasGroup GildingGlow;

	// Token: 0x04000D55 RID: 3413
	public TextMeshProUGUI GildingText;

	// Token: 0x04000D56 RID: 3414
	private float curGildingDisplay;

	// Token: 0x04000D57 RID: 3415
	public ParticleSystem GildingParticles;

	// Token: 0x04000D58 RID: 3416
	public Transform GildingPullField;

	// Token: 0x04000D59 RID: 3417
	public AudioClip GildingSpentSFX;

	// Token: 0x04000D5A RID: 3418
	public RectTransform GildingTooltipAnchor;

	// Token: 0x04000D5B RID: 3419
	private float noteSafety;

	// Token: 0x04000D5C RID: 3420
	public CanvasGroup NoteGroup;

	// Token: 0x04000D5D RID: 3421
	public AudioClip NewRewardSFX;

	// Token: 0x04000D5E RID: 3422
	public TextMeshProUGUI RewardTitle;

	// Token: 0x04000D5F RID: 3423
	public TextMeshProUGUI RewardCategory;

	// Token: 0x04000D60 RID: 3424
	public TextMeshProUGUI RewardDetail;

	// Token: 0x04000D61 RID: 3425
	public static bool notifying;

	// Token: 0x04000D62 RID: 3426
	[HideInInspector]
	private Queue<LibraryInfoWidget.Note> notes = new Queue<LibraryInfoWidget.Note>();

	// Token: 0x04000D63 RID: 3427
	public CanvasGroup AttuneGroup;

	// Token: 0x04000D64 RID: 3428
	public TextMeshProUGUI AttuneText;

	// Token: 0x04000D65 RID: 3429
	public TextMeshProUGUI AttuneBoostText;

	// Token: 0x04000D66 RID: 3430
	public CanvasGroup AttuneRewardGroup;

	// Token: 0x04000D67 RID: 3431
	private bool isQuillHovered;

	// Token: 0x02000553 RID: 1363
	private class RewardNote : LibraryInfoWidget.Note
	{
		// Token: 0x06002476 RID: 9334 RVA: 0x000CE394 File Offset: 0x000CC594
		public static LibraryInfoWidget.RewardNote FromTome(GenreTree tome)
		{
			LibraryInfoWidget.RewardNote rewardNote = new LibraryInfoWidget.RewardNote();
			rewardNote.Title = tome.Root.ShortName;
			rewardNote.Category = "New Tome!";
			string detail = "Collected from the <b>Torn</b>...";
			UnlockDB.GenreUnlock genreUnlock = UnlockDB.GetGenreUnlock(tome);
			if (genreUnlock.UnlockedBy == Unlockable.UnlockType.Achievement)
			{
				AchievementRootNode achievement = GraphDB.GetAchievement(genreUnlock.Achievement);
				detail = ((achievement != null) ? achievement.Detail : null);
			}
			rewardNote.Detail = detail;
			return rewardNote;
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000CE3F8 File Offset: 0x000CC5F8
		public static LibraryInfoWidget.RewardNote FromAugment(AugmentTree augment)
		{
			LibraryInfoWidget.RewardNote rewardNote = new LibraryInfoWidget.RewardNote();
			rewardNote.Title = augment.Root.Name;
			rewardNote.Category = "New Page!";
			string detail = "Collected from the <b>Torn</b>...";
			foreach (UnlockDB.AugmentUnlock augmentUnlock in UnlockDB.DB.GenreAugments)
			{
				if (augmentUnlock.Augment == augment)
				{
					detail = "Tome Mended: " + augmentUnlock.Genre.Root.ShortName;
				}
			}
			rewardNote.Detail = detail;
			return rewardNote;
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000CE4A4 File Offset: 0x000CC6A4
		public static LibraryInfoWidget.RewardNote FromBinding(AugmentTree augment)
		{
			return new LibraryInfoWidget.RewardNote
			{
				Title = augment.Root.Name,
				Category = "New Binding!",
				Detail = "Collected from the <b>Torn</b>..."
			};
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000CE4D2 File Offset: 0x000CC6D2
		public RewardNote()
		{
		}
	}

	// Token: 0x02000554 RID: 1364
	private class Note
	{
		// Token: 0x0600247A RID: 9338 RVA: 0x000CE4DA File Offset: 0x000CC6DA
		public Note()
		{
		}

		// Token: 0x040026BD RID: 9917
		public string Title;

		// Token: 0x040026BE RID: 9918
		public string Category;

		// Token: 0x040026BF RID: 9919
		public string Detail;

		// Token: 0x040026C0 RID: 9920
		public float showTime = 3.5f;
	}

	// Token: 0x02000555 RID: 1365
	[CompilerGenerated]
	private sealed class <NoteSequence>d__51 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600247B RID: 9339 RVA: 0x000CE4ED File Offset: 0x000CC6ED
		[DebuggerHidden]
		public <NoteSequence>d__51(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000CE4FC File Offset: 0x000CC6FC
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000CE500 File Offset: 0x000CC700
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			LibraryInfoWidget libraryInfoWidget = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				t = 0f;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				t = 1f;
				goto IL_FF;
			case 3:
				this.<>1__state = -1;
				goto IL_FF;
			default:
				return false;
			}
			if (t >= 1f)
			{
				this.<>2__current = new WaitForSeconds(closeTime);
				this.<>1__state = 2;
				return true;
			}
			t += Time.deltaTime * 4f;
			libraryInfoWidget.NoteGroup.alpha = t;
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
			IL_FF:
			if (t <= 0f)
			{
				LibraryInfoWidget.notifying = false;
				return false;
			}
			t -= Time.deltaTime * 4f;
			libraryInfoWidget.NoteGroup.alpha = t;
			this.<>2__current = true;
			this.<>1__state = 3;
			return true;
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600247E RID: 9342 RVA: 0x000CE620 File Offset: 0x000CC820
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x000CE628 File Offset: 0x000CC828
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x000CE62F File Offset: 0x000CC82F
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040026C1 RID: 9921
		private int <>1__state;

		// Token: 0x040026C2 RID: 9922
		private object <>2__current;

		// Token: 0x040026C3 RID: 9923
		public LibraryInfoWidget <>4__this;

		// Token: 0x040026C4 RID: 9924
		public float closeTime;

		// Token: 0x040026C5 RID: 9925
		private float <t>5__2;
	}
}
