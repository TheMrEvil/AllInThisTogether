using System;
using TMPro;
using UnityEngine;

// Token: 0x020001C2 RID: 450
public class PlayerNookHud : MonoBehaviour
{
	// Token: 0x17000155 RID: 341
	// (get) Token: 0x06001284 RID: 4740 RVA: 0x000724F2 File Offset: 0x000706F2
	private bool ShouldShowPrompt
	{
		get
		{
			return MapManager.InLobbyScene && PlayerNook.IsPlayerInside && !PlayerNook.IsInEditMode;
		}
	}

	// Token: 0x17000156 RID: 342
	// (get) Token: 0x06001285 RID: 4741 RVA: 0x0007250C File Offset: 0x0007070C
	private bool ShouldShow
	{
		get
		{
			return MapManager.InLobbyScene && PlayerNook.IsPlayerInside && PlayerNook.IsInEditMode;
		}
	}

	// Token: 0x06001286 RID: 4742 RVA: 0x00072524 File Offset: 0x00070724
	private void Update()
	{
		this.TogglePromptGroup.UpdateOpacity(this.ShouldShowPrompt, 6f, true);
		this.MainGroup.UpdateOpacity(this.ShouldShow, 6f, true);
		if (!MapManager.InLobbyScene)
		{
			return;
		}
		if (!this.ShouldShow || PlayerNook.MyNook == null)
		{
			return;
		}
		this.ItemCounter.text = PlayerNook.MyNook.ItemCount.ToString() + "/" + NookDB.DB.NookLimit.ToString();
		this.SnapModeGlow.UpdateOpacity(PlayerNook.MyNook.SnapMode, 6f, true);
		this.InventoryPrompt.SetActive(PlayerNook.MyNook.HeldItem == null);
		this.MovePrompt.SetActive(PlayerNook.MyNook.HeldItem == null && PlayerNook.MyNook.HilightedItem != null);
		this.PlacePrompt.SetActive(PlayerNook.MyNook.HeldItem != null);
		this.RemovePrompt.SetActive(PlayerNook.MyNook.HeldItem == null && PlayerNook.MyNook.HilightedItem != null);
		this.CancelPlcement.SetActive(PlayerNook.MyNook.HeldItem != null);
		this.Rotate_KBM.SetActive(PlayerNook.MyNook.HeldItem != null && !InputManager.IsUsingController);
		this.Rotate_Controller.SetActive(PlayerNook.MyNook.HeldItem != null && InputManager.IsUsingController);
	}

	// Token: 0x06001287 RID: 4743 RVA: 0x000726CC File Offset: 0x000708CC
	public PlayerNookHud()
	{
	}

	// Token: 0x04001180 RID: 4480
	public CanvasGroup TogglePromptGroup;

	// Token: 0x04001181 RID: 4481
	public CanvasGroup MainGroup;

	// Token: 0x04001182 RID: 4482
	public TextMeshProUGUI ItemCounter;

	// Token: 0x04001183 RID: 4483
	public CanvasGroup SnapModeGlow;

	// Token: 0x04001184 RID: 4484
	public GameObject InventoryPrompt;

	// Token: 0x04001185 RID: 4485
	public GameObject MovePrompt;

	// Token: 0x04001186 RID: 4486
	public GameObject PlacePrompt;

	// Token: 0x04001187 RID: 4487
	public GameObject RemovePrompt;

	// Token: 0x04001188 RID: 4488
	public GameObject Rotate_KBM;

	// Token: 0x04001189 RID: 4489
	public GameObject Rotate_Controller;

	// Token: 0x0400118A RID: 4490
	public GameObject CancelPlcement;
}
