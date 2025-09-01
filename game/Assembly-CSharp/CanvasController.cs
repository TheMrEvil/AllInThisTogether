using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// Token: 0x02000187 RID: 391
public class CanvasController : MonoBehaviour
{
	// Token: 0x0600106C RID: 4204 RVA: 0x00066CB5 File Offset: 0x00064EB5
	private void Awake()
	{
		CanvasController.instance = this;
		CanvasController.SetMenuVisibility(true);
		this.SetupButtonSounds();
		this.MenuUI.alpha = 1f;
	}

	// Token: 0x0600106D RID: 4205 RVA: 0x00066CD9 File Offset: 0x00064ED9
	private void Start()
	{
		CanvasController.StopVideo();
	}

	// Token: 0x0600106E RID: 4206 RVA: 0x00066CE0 File Offset: 0x00064EE0
	private void SetupButtonSounds()
	{
		foreach (Button button in base.gameObject.GetComponentsInChildren<Button>(true))
		{
			this.SetupButtonSounds(button);
		}
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x00066D13 File Offset: 0x00064F13
	public static void SetupButton(Button button)
	{
		if (CanvasController.instance.AudioButtons.Contains(button))
		{
			return;
		}
		CanvasController.instance.SetupButtonSounds(button);
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x00066D34 File Offset: 0x00064F34
	private void SetupButtonSounds(Button button)
	{
		if (button == null)
		{
			return;
		}
		AudioButton orAddComponent = button.gameObject.GetOrAddComponent<AudioButton>();
		if (orAddComponent.buttonType != AudioButton.ButtonType.Silent)
		{
			List<AudioClip> clips = this.BClick_Default;
			if (orAddComponent.buttonType == AudioButton.ButtonType.Tab)
			{
				clips = this.BClick_Tab;
			}
			button.onClick.AddListener(delegate()
			{
				if (Time.realtimeSinceStartup - CanvasController.lastClickTime < 0.2f)
				{
					return;
				}
				CanvasController.lastClickTime = Time.realtimeSinceStartup;
				AudioManager.PlayInterfaceSFX(clips.GetRandomClip(-1), 1f, 0f);
			});
		}
		this.AudioButtons.Add(button);
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x00066DAB File Offset: 0x00064FAB
	public static void GoToPause()
	{
		PanelManager.instance.PushPanel(PanelType.Pause);
		GameStats.SaveIfNeeded();
	}

	// Token: 0x06001072 RID: 4210 RVA: 0x00066DBD File Offset: 0x00064FBD
	public static void Resume()
	{
		if (PanelManager.CurPanel == PanelType.Pause)
		{
			PanelManager.instance.PopPanel();
		}
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x00066DD1 File Offset: 0x00064FD1
	public static void PlaySelectionChanged()
	{
		if (Time.realtimeSinceStartup - CanvasController.lastPlayed < 0.2f)
		{
			return;
		}
		CanvasController.lastPlayed = Time.realtimeSinceStartup;
		AudioManager.PlayInterfaceSFX(CanvasController.instance.BHover.GetRandomClip(-1), 1f, 0f);
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x00066E0F File Offset: 0x0006500F
	public static void SetMenuVisibility(bool visible)
	{
		CanvasController.instance.MenuUI.interactable = visible;
		CanvasController.instance.MenuUI.blocksRaycasts = visible;
		CanvasController.instance.wantMenuVisible = visible;
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x00066E3C File Offset: 0x0006503C
	public static void SetGameVisibility(bool visible)
	{
		CanvasController.instance.GameUI.interactable = visible;
		CanvasController.instance.GameUI.blocksRaycasts = visible;
		CanvasController.instance.wantGameVisible = visible;
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x00066E69 File Offset: 0x00065069
	public static void EnterGameplay()
	{
		PanelManager.instance.ClearStack();
		PanelManager.instance.PushPanel(PanelType.GameInvisible);
		CanvasController.SetMenuVisibility(false);
		CanvasController.SetGameVisibility(true);
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x00066E8C File Offset: 0x0006508C
	public static void LeaveGameplay()
	{
		PanelManager.instance.ClearStack();
		PanelManager.instance.PushPanel(PanelType.Main);
		CanvasController.SetMenuVisibility(true);
		CanvasController.SetGameVisibility(false);
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x00066EB0 File Offset: 0x000650B0
	private void Update()
	{
		CanvasController.AlphaDelta(this.wantMenuVisible, this.MenuUI);
		CanvasController.AlphaDelta(this.wantGameVisible, this.GameUI);
		bool flag = this.wantGameVisible && MapManager.InLobbyScene;
		CanvasController.AlphaDelta(flag, this.LobbyUI);
		if (!flag && this.LobbyUI.alpha <= 0f && this.LobbyUI.gameObject.activeSelf)
		{
			this.LobbyUI.gameObject.SetActive(false);
		}
		else if (flag && !this.LobbyUI.gameObject.activeSelf)
		{
			this.LobbyUI.gameObject.SetActive(true);
		}
		foreach (SelectableFader selectableFader in CanvasController.SelectFaders)
		{
			selectableFader.TickUpdate();
		}
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x00066FA0 File Offset: 0x000651A0
	public static void AlphaDelta(bool wantVisible, CanvasGroup cgp)
	{
		if (wantVisible && cgp.alpha < 1f)
		{
			cgp.alpha += Time.deltaTime * 3f;
			return;
		}
		if (!wantVisible && cgp.alpha > 0f)
		{
			cgp.alpha -= Time.deltaTime * 3f;
		}
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x00067000 File Offset: 0x00065200
	public static void ChangeVideo(VideoClip clip)
	{
		CanvasController.instance.VidPlayer.enabled = true;
		CanvasController.instance.VidPlayer.Stop();
		CanvasController.instance.VidPlayer.clip = clip;
		CanvasController.instance.VidPlayer.frame = 0L;
		CanvasController.instance.VidPlayer.Play();
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x0006705C File Offset: 0x0006525C
	public static void StopVideo()
	{
		CanvasController.instance.VidPlayer.Stop();
		CanvasController.instance.VidPlayer.enabled = false;
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x0006707D File Offset: 0x0006527D
	public CanvasController()
	{
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x00067090 File Offset: 0x00065290
	// Note: this type is marked as 'beforefieldinit'.
	static CanvasController()
	{
	}

	// Token: 0x04000E94 RID: 3732
	public CanvasGroup GlobalUI;

	// Token: 0x04000E95 RID: 3733
	public CanvasGroup MenuUI;

	// Token: 0x04000E96 RID: 3734
	public Camera UICamera;

	// Token: 0x04000E97 RID: 3735
	private bool wantMenuVisible;

	// Token: 0x04000E98 RID: 3736
	public CanvasGroup GameUI;

	// Token: 0x04000E99 RID: 3737
	public CanvasGroup LobbyUI;

	// Token: 0x04000E9A RID: 3738
	public VideoPlayer VidPlayer;

	// Token: 0x04000E9B RID: 3739
	[NonSerialized]
	public bool wantGameVisible;

	// Token: 0x04000E9C RID: 3740
	public static CanvasController instance;

	// Token: 0x04000E9D RID: 3741
	public static List<SelectableFader> SelectFaders = new List<SelectableFader>();

	// Token: 0x04000E9E RID: 3742
	[Header("UI Sounds")]
	public List<AudioClip> BHover;

	// Token: 0x04000E9F RID: 3743
	public List<AudioClip> BClick_Default;

	// Token: 0x04000EA0 RID: 3744
	public List<AudioClip> BClick_Tab;

	// Token: 0x04000EA1 RID: 3745
	public List<AudioClip> WindowChange;

	// Token: 0x04000EA2 RID: 3746
	private HashSet<Button> AudioButtons = new HashSet<Button>();

	// Token: 0x04000EA3 RID: 3747
	private static float lastClickTime;

	// Token: 0x04000EA4 RID: 3748
	private static float lastPlayed;

	// Token: 0x0200055D RID: 1373
	[CompilerGenerated]
	private sealed class <>c__DisplayClass20_0
	{
		// Token: 0x0600249B RID: 9371 RVA: 0x000CE8E8 File Offset: 0x000CCAE8
		public <>c__DisplayClass20_0()
		{
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000CE8F0 File Offset: 0x000CCAF0
		internal void <SetupButtonSounds>b__0()
		{
			if (Time.realtimeSinceStartup - CanvasController.lastClickTime < 0.2f)
			{
				return;
			}
			CanvasController.lastClickTime = Time.realtimeSinceStartup;
			AudioManager.PlayInterfaceSFX(this.clips.GetRandomClip(-1), 1f, 0f);
		}

		// Token: 0x040026E0 RID: 9952
		public List<AudioClip> clips;
	}
}
