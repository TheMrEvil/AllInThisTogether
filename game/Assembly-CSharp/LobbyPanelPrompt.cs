using System;
using MiniTools.BetterGizmos;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020000C1 RID: 193
public class LobbyPanelPrompt : PositionalInteraction
{
	// Token: 0x060008A9 RID: 2217 RVA: 0x0003B7D8 File Offset: 0x000399D8
	internal override void Awake()
	{
		base.Awake();
		PanelManager instance = PanelManager.instance;
		instance.OnPanelChanged = (Action<PanelType, PanelType>)Delegate.Combine(instance.OnPanelChanged, new Action<PanelType, PanelType>(this.OnPanelChanged));
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x0003B808 File Offset: 0x00039A08
	internal override void Update()
	{
		base.Update();
		this.Prompt.UpdateOpacity(this.PlayerInside && PanelManager.CurPanel == PanelType.GameInvisible, 2f, false);
		this.UnavailableGroup.UpdateOpacity(PanelManager.CurPanel == PanelType.GameInvisible && !this.CanInteract() && this.PlayerInside, 2f, true);
		if (this.InteractTime > 0f)
		{
			this.ProgressFill.fillAmount = this.HoldTimer / this.InteractTime;
		}
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x0003B88E File Offset: 0x00039A8E
	internal override bool CanInteract()
	{
		return LobbyPanelPrompt.CanLibraryInteractNow(this.Panel);
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x0003B89C File Offset: 0x00039A9C
	public static bool CanLibraryInteractNow(PanelType p)
	{
		if (LibraryRaces.IsPlayerRacing)
		{
			return false;
		}
		if (p == PanelType.Bindings && !Settings.HasCompletedLibraryTutorial(LibraryTutorial.Bindings))
		{
			return false;
		}
		if (p == PanelType.Bindings && !Settings.HasCompletedUITutorial(UITutorial.Tutorial.Bindings))
		{
			return false;
		}
		if (p == PanelType.Genre_Selection && GameplayManager.CurState == GameState.Hub_Bindings)
		{
			return false;
		}
		if (p == PanelType.Customize_Abilities && Settings.HasCompletedLibraryTutorial(LibraryTutorial.Abilities))
		{
			return true;
		}
		if (!LibraryManager.InLibraryTutorial)
		{
			return true;
		}
		switch (LibraryManager.CurrentStep)
		{
		case LibraryTutorial.Tomes:
			return p == PanelType.Genre_Selection;
		case LibraryTutorial.Meta:
			return p != PanelType.Main && p != PanelType.Genre_Selection;
		case LibraryTutorial.Abilities:
			return p == PanelType.Customize_Abilities;
		}
		return true;
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x0003B938 File Offset: 0x00039B38
	internal override void OnEnter()
	{
		base.OnEnter();
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x0003B940 File Offset: 0x00039B40
	private void OnPanelChanged(PanelType from, PanelType to)
	{
		if (from != this.Panel || PlayerControl.myInstance == null)
		{
			return;
		}
		if (!PanelManager.CurrentStackPanels.Contains(this.Panel) && this.CameraTargetPoint != null)
		{
			PlayerControl.myInstance.Input.ReturnCamera(6f, !this.NoPageFlip);
		}
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x0003B9A4 File Offset: 0x00039BA4
	internal override void OnInteract()
	{
		if (GameplayManager.CurState == GameState.Hub_Preparing || GameplayManager.CurState == GameState.Hub_Traveling)
		{
			return;
		}
		if (this.OpenUI)
		{
			if (PanelManager.StackContains(this.Panel) || PanelManager.CurPanel != PanelType.GameInvisible)
			{
				return;
			}
			if (this.Panel == PanelType.InkCores)
			{
				InkCoresPanel.instance.OpenUI(PlayerControl.myInstance.actions.core.Root.magicColor);
			}
			else
			{
				PanelManager.instance.PushPanel(this.Panel);
			}
		}
		UnityEvent onUsed = this.OnUsed;
		if (onUsed != null)
		{
			onUsed.Invoke();
		}
		if (this.CameraTargetPoint != null)
		{
			PlayerControl.myInstance.Input.OverrideCamera(this.CameraTargetPoint, 6f, this.NoPageFlip);
		}
		base.OnInteract();
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x0003BA64 File Offset: 0x00039C64
	internal override void OnExit()
	{
		PanelManager.instance.RemoveFromStack(this.Panel);
		base.OnExit();
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x0003BA7C File Offset: 0x00039C7C
	private new void OnDrawGizmos()
	{
		BetterGizmos.DrawSphere(new Color(0.5f, 1f, 0.5f, 0.2f), base.transform.position, this.InteractDistance);
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x0003BAAD File Offset: 0x00039CAD
	public LobbyPanelPrompt()
	{
	}

	// Token: 0x04000762 RID: 1890
	public CanvasGroup Prompt;

	// Token: 0x04000763 RID: 1891
	public bool OpenUI = true;

	// Token: 0x04000764 RID: 1892
	public PanelType Panel;

	// Token: 0x04000765 RID: 1893
	public CanvasGroup UnavailableGroup;

	// Token: 0x04000766 RID: 1894
	public Image ProgressFill;

	// Token: 0x04000767 RID: 1895
	public UnityEvent OnUsed;

	// Token: 0x04000768 RID: 1896
	public Transform CameraTargetPoint;

	// Token: 0x04000769 RID: 1897
	public bool NoPageFlip;
}
