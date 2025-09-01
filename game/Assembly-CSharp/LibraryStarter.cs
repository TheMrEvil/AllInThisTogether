using System;
using MiniTools.BetterGizmos;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C0 RID: 192
public class LibraryStarter : MonoBehaviour
{
	// Token: 0x060008A2 RID: 2210 RVA: 0x0003B427 File Offset: 0x00039627
	private void Awake()
	{
		LibraryStarter.instance = this;
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x0003B430 File Offset: 0x00039630
	private void Update()
	{
		this.interactGroup.UpdateOpacity(this.canInteract, 2f, false);
		GameplayManager gameplayManager = GameplayManager.instance;
		GenreTree genreTree = (gameplayManager != null) ? gameplayManager.GameGraph : null;
		bool flag = genreTree != null && PlayerControl.myInstance != null;
		if (flag && !this.TomeSystem.isPlaying)
		{
			this.TomeSystem.Play();
			this.PreparedVFX.Play();
		}
		else if (!flag && this.TomeSystem.isPlaying)
		{
			this.TomeSystem.Stop();
			this.PreparedVFX.Stop();
		}
		if (flag && genreTree.Root.VFXIcon != null)
		{
			Texture2D texture2D = genreTree.Root.VFXIcon.texture;
			if (GameplayManager.IsChallengeActive)
			{
				texture2D = this.ChallengeIcon;
			}
			if (this.TomeSystem.shape.texture != texture2D)
			{
				this.TomeSystem.shape.texture = texture2D;
			}
		}
		this.isNearby = false;
		if (PlayerControl.myInstance == null || PlayerControl.myInstance.IsDead)
		{
			this.canvasGroup.UpdateOpacity(false, 2f, false);
			return;
		}
		this.isNearby = this.CheckNearby();
		this.canInteract = (this.isNearby && LibraryStarter.CanInteract());
		bool flag2 = this.canInteract && !LobbyPanelPrompt.CanLibraryInteractNow(PanelType.Main);
		if (flag2)
		{
			this.canInteract = false;
		}
		bool flag3 = PanelManager.CurPanel == PanelType.GameInvisible;
		this.unavailableGroup.UpdateOpacity(flag2 && flag3, 2f, true);
		this.canvasGroup.UpdateOpacity(this.isNearby && !flag2 && flag3, 2f, false);
		if (this.interactGroup.alpha <= 0f)
		{
			return;
		}
		bool flag4 = LibraryStarter.CanRequestStart();
		if (this.HoldDisplay.activeSelf != flag4)
		{
			this.HoldDisplay.SetActive(flag4);
		}
		if (this.canInteract && PlayerInput.myInstance != null && PlayerInput.myInstance.interactPressed && PanelManager.CurPanel == PanelType.GameInvisible)
		{
			if (flag4)
			{
				if (this.fillHold < 0.2f)
				{
					this.fillHold = 0.2f;
				}
				this.fillHold += Time.deltaTime / this.HoldTime;
			}
			else
			{
				PanelManager.instance.PushPanel(PanelType.Genre_Selection);
			}
		}
		else
		{
			this.fillHold -= Time.deltaTime / this.HoldTime;
		}
		this.fillHold = Mathf.Clamp(this.fillHold, 0f, 1f);
		this.fillImage.fillAmount = this.fillHold;
		if (this.fillHold >= 1f && this.canInteract)
		{
			GameplayManager.instance.PrepareForStart();
			this.fillHold = 0f;
		}
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x0003B6FC File Offset: 0x000398FC
	private static bool CanInteract()
	{
		return GameplayManager.CurState == GameState.Hub;
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x0003B706 File Offset: 0x00039906
	public static bool CanRequestStart()
	{
		GameplayManager gameplayManager = GameplayManager.instance;
		return ((gameplayManager != null) ? gameplayManager.GameGraph : null) != null && LibraryStarter.CanInteract();
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x0003B728 File Offset: 0x00039928
	private bool CheckNearby()
	{
		Vector3 vector = PlayerControl.myInstance.display.CenterOfMass.position - base.transform.position;
		return vector.magnitude <= this.InteractDistance && Vector3.Dot(vector.normalized, base.transform.forward) > 0.3f;
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x0003B789 File Offset: 0x00039989
	private void OnDrawGizmos()
	{
		BetterGizmos.DrawSphere(new Color(0.5f, 1f, 0.5f, 0.2f), base.transform.position, this.InteractDistance);
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x0003B7BA File Offset: 0x000399BA
	public LibraryStarter()
	{
	}

	// Token: 0x04000752 RID: 1874
	public static LibraryStarter instance;

	// Token: 0x04000753 RID: 1875
	public Image fillImage;

	// Token: 0x04000754 RID: 1876
	public CanvasGroup canvasGroup;

	// Token: 0x04000755 RID: 1877
	public CanvasGroup unavailableGroup;

	// Token: 0x04000756 RID: 1878
	public CanvasGroup interactGroup;

	// Token: 0x04000757 RID: 1879
	public GameObject HoldDisplay;

	// Token: 0x04000758 RID: 1880
	public float InteractDistance = 3f;

	// Token: 0x04000759 RID: 1881
	public float HoldTime = 1.5f;

	// Token: 0x0400075A RID: 1882
	private float fillHold;

	// Token: 0x0400075B RID: 1883
	public Texture2D ChallengeIcon;

	// Token: 0x0400075C RID: 1884
	public ParticleSystem TomeSystem;

	// Token: 0x0400075D RID: 1885
	public ParticleSystem PreparedVFX;

	// Token: 0x0400075E RID: 1886
	public AudioClip ChallengeActivatedSFX;

	// Token: 0x0400075F RID: 1887
	public ParticleSystem ChallengeActivatedVFX;

	// Token: 0x04000760 RID: 1888
	private bool isNearby;

	// Token: 0x04000761 RID: 1889
	private bool canInteract;
}
