using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000192 RID: 402
public class GenreUIControl : MonoBehaviour
{
	// Token: 0x17000138 RID: 312
	// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00068B3C File Offset: 0x00066D3C
	public static List<GenreUIBook> AllBooks
	{
		get
		{
			List<GenreUIBook> result = new List<GenreUIBook>();
			if (GenreUIControl.instance == null)
			{
				return result;
			}
			return GenreUIControl.instance.Books;
		}
	}

	// Token: 0x060010D8 RID: 4312 RVA: 0x00068B68 File Offset: 0x00066D68
	private void Awake()
	{
		GenreUIControl.instance = this;
		PanelManager panelManager = PanelManager.instance;
		panelManager.OnPanelChanged = (Action<PanelType, PanelType>)Delegate.Combine(panelManager.OnPanelChanged, new Action<PanelType, PanelType>(this.OnPanelChanged));
		GameplayManager.OnGenereChanged = (Action<GenreTree>)Delegate.Combine(GameplayManager.OnGenereChanged, new Action<GenreTree>(this.UpdateGenreLabel));
		this.Books = base.GetComponentsInChildren<GenreUIBook>().ToList<GenreUIBook>();
	}

	// Token: 0x060010D9 RID: 4313 RVA: 0x00068BD2 File Offset: 0x00066DD2
	private void Start()
	{
		GenreUIControl.UpdateBookDisplays();
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x00068BDC File Offset: 0x00066DDC
	public static void UpdateBookDisplays()
	{
		if (GenreUIControl.instance == null)
		{
			return;
		}
		foreach (GenreUIBook genreUIBook in GenreUIControl.instance.Books)
		{
			genreUIBook.UpdateLockedInfo();
		}
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x00068C40 File Offset: 0x00066E40
	public void UpdateGenreLabel(GenreTree genre)
	{
		string text = "Choose a Tome";
		if (GameplayManager.IsChallengeActive)
		{
			text = "Book Club:  " + GameplayManager.Challenge.Name;
		}
		else if (GameplayManager.instance.GameGraph != null)
		{
			if (!Settings.HasCompletedUITutorial(UITutorial.Tutorial.Bindings) && GameStats.GetTomeStat(GenrePanel.instance.bindingReqGenre, GameStats.Stat.TomesWon, 0) > 0)
			{
				text = "Bind: " + GameplayManager.instance.GameGraph.Root.ShortName;
			}
			else
			{
				text = GameplayManager.instance.GameGraph.Root.Name;
			}
		}
		this.genreName.text = text;
		if (GameplayManager.IsChallengeActive)
		{
			ParticleSystem challengeActivatedVFX = LibraryStarter.instance.ChallengeActivatedVFX;
			if (challengeActivatedVFX != null)
			{
				challengeActivatedVFX.Play();
			}
			AudioManager.PlaySFX2D(LibraryStarter.instance.ChallengeActivatedSFX, 0.6f, 0.1f);
			return;
		}
		ParticleSystem genreChangedFX = this.GenreChangedFX;
		if (genreChangedFX != null)
		{
			genreChangedFX.Play();
		}
		if (!this.didFirstSetup || !(genre != null))
		{
			this.didFirstSetup = true;
			return;
		}
		AudioManager.PlaySFX2D(this.GenreChangedSFX, 0.6f, 0.1f);
		ParticleSystem genrePullFX = this.GenrePullFX;
		if (genrePullFX == null)
		{
			return;
		}
		genrePullFX.Play();
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x00068D68 File Offset: 0x00066F68
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.Genre_Selection || PlayerControl.MyCamera == null)
		{
			return;
		}
		foreach (GenreUIBook genreUIBook in this.Books)
		{
			genreUIBook.TickUpdate(genreUIBook == this.hovered, genreUIBook == this.selected);
		}
		if (UITutorial.InTutorial || InputManager.IsUsingController)
		{
			return;
		}
		RaycastHit raycastHit;
		if (Physics.Raycast(PlayerControl.MyCamera.ScreenPointToRay(Input.mousePosition), out raycastHit, 10f, this.RayMask))
		{
			GenreUIBook component = raycastHit.collider.GetComponent<GenreUIBook>();
			if (component == null)
			{
				return;
			}
			this.HoverBook(component);
			if (Input.GetMouseButtonDown(0) && component.isUnlocked)
			{
				this.SelectBook(component);
				return;
			}
		}
		else if (this.selected != null)
		{
			this.HoverBook(null);
		}
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x00068E6C File Offset: 0x0006706C
	public GenreUIBook GetUIBook(GenreTree tome)
	{
		foreach (GenreUIBook genreUIBook in this.Books)
		{
			if (genreUIBook.Genre == tome)
			{
				return genreUIBook;
			}
		}
		return null;
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x00068ED0 File Offset: 0x000670D0
	public void HoverBook(GenreUIBook book)
	{
		if (this.hovered == book)
		{
			return;
		}
		if (this.hovered != null)
		{
			this.hovered.OnDeselect(new BaseEventData(null));
		}
		this.hovered = book;
		Tooltip.Release();
		if (book == null)
		{
			return;
		}
		book.OnHovered();
		Vector3 locationFromWorld = Tooltip.GetLocationFromWorld(book.TooltipAnchor.position);
		if (!book.isUnlocked)
		{
			Tooltip.Show(locationFromWorld, book.TooltipView, book.Genre, true);
		}
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x00068F54 File Offset: 0x00067154
	public void SetSelected(GenreTree tome)
	{
		foreach (GenreUIBook genreUIBook in this.Books)
		{
			if (genreUIBook.Genre == tome)
			{
				this.SelectBook(genreUIBook);
				break;
			}
		}
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x00068FB8 File Offset: 0x000671B8
	public void SelectBook(GenreUIBook book)
	{
		if (this.selected == book)
		{
			return;
		}
		this.ControllerSelect(book);
		if (!book.isUnlocked)
		{
			return;
		}
		this.selected = book;
		this.SelectedFX.transform.SetPositionAndRotation(book.BottomAnchor.position, book.BottomAnchor.rotation);
		GenrePanel.SetGenre(book.Genre);
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x0006901C File Offset: 0x0006721C
	public void ControllerSelect(GenreUIBook book)
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		if (this.hovered != book)
		{
			this.HoverBook(book);
			UISelector.SelectSelectable(book);
		}
	}

	// Token: 0x060010E2 RID: 4322 RVA: 0x00069044 File Offset: 0x00067244
	public void TransitionIn()
	{
		PlayerControl.myInstance.Display.CamRays.enabled = false;
		this.SelectedFX.Play();
		this.selected = null;
		List<GenreTree> unlockedTomes = UnlockManager.GetUnlockedTomes();
		GenreTree y = GenrePanel.instance.defaultSelected;
		foreach (GenreTree genreTree in unlockedTomes)
		{
			if (!(genreTree == y))
			{
				y = genreTree;
				if (GameStats.GetTomeStat(genreTree, GameStats.Stat.TomesPlayed, 0) == 0)
				{
					y = genreTree;
					break;
				}
			}
		}
		this.SetSelected(y);
		PlayerControl.myInstance.Input.OverrideCamera(this.CameraAnchor, 6f, false);
	}

	// Token: 0x060010E3 RID: 4323 RVA: 0x000690FC File Offset: 0x000672FC
	public void TransitionOut()
	{
		this.SelectedFX.Stop();
		PlayerControl.myInstance.Input.ReturnCamera(6f, true);
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x0006911E File Offset: 0x0006731E
	private void OnPanelChanged(PanelType from, PanelType to)
	{
		if (to == PanelType.Genre_Selection)
		{
			this.TransitionIn();
			return;
		}
		if (from == PanelType.Genre_Selection)
		{
			this.TransitionOut();
		}
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x00069138 File Offset: 0x00067338
	private void OnDestroy()
	{
		PanelManager panelManager = PanelManager.instance;
		panelManager.OnPanelChanged = (Action<PanelType, PanelType>)Delegate.Remove(panelManager.OnPanelChanged, new Action<PanelType, PanelType>(this.OnPanelChanged));
		GameplayManager.OnGenereChanged = (Action<GenreTree>)Delegate.Remove(GameplayManager.OnGenereChanged, new Action<GenreTree>(this.UpdateGenreLabel));
	}

	// Token: 0x060010E6 RID: 4326 RVA: 0x0006918B File Offset: 0x0006738B
	public GenreUIControl()
	{
	}

	// Token: 0x04000F24 RID: 3876
	public Transform CameraAnchor;

	// Token: 0x04000F25 RID: 3877
	public float MoveTime = 3f;

	// Token: 0x04000F26 RID: 3878
	private Transform camHolderParent;

	// Token: 0x04000F27 RID: 3879
	public LayerMask RayMask;

	// Token: 0x04000F28 RID: 3880
	public ParticleSystem HoverFX;

	// Token: 0x04000F29 RID: 3881
	public ParticleSystem SelectedFX;

	// Token: 0x04000F2A RID: 3882
	public GenreTree Layer2Requirement;

	// Token: 0x04000F2B RID: 3883
	public GameObject Layer2Display;

	// Token: 0x04000F2C RID: 3884
	public AudioClip GenreChangedSFX;

	// Token: 0x04000F2D RID: 3885
	public ParticleSystem GenrePullFX;

	// Token: 0x04000F2E RID: 3886
	public ParticleSystem GenreChangedFX;

	// Token: 0x04000F2F RID: 3887
	private bool didFirstSetup;

	// Token: 0x04000F30 RID: 3888
	public static GenreUIControl instance;

	// Token: 0x04000F31 RID: 3889
	private GenreUIBook hovered;

	// Token: 0x04000F32 RID: 3890
	[NonSerialized]
	public GenreUIBook selected;

	// Token: 0x04000F33 RID: 3891
	public TextMeshProUGUI genreName;

	// Token: 0x04000F34 RID: 3892
	private List<GenreUIBook> Books = new List<GenreUIBook>();
}
