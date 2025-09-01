using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001C1 RID: 449
public class PlayerIndicator : MonoBehaviour
{
	// Token: 0x17000153 RID: 339
	// (get) Token: 0x06001277 RID: 4727 RVA: 0x00072125 File Offset: 0x00070325
	// (set) Token: 0x06001278 RID: 4728 RVA: 0x0007212D File Offset: 0x0007032D
	public PlayerWorldUI target
	{
		[CompilerGenerated]
		get
		{
			return this.<target>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			this.<target>k__BackingField = value;
		}
	}

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x06001279 RID: 4729 RVA: 0x00072136 File Offset: 0x00070336
	public PlayerControl refPlayer
	{
		get
		{
			return this.target.Player;
		}
	}

	// Token: 0x0600127A RID: 4730 RVA: 0x00072143 File Offset: 0x00070343
	private void Awake()
	{
		this.rect = base.GetComponent<RectTransform>();
		this.nameLayout = this.nameGroup.GetComponent<LayoutElement>();
	}

	// Token: 0x0600127B RID: 4731 RVA: 0x00072162 File Offset: 0x00070362
	public void Setup(PlayerWorldUI target)
	{
		this.target = target;
		PlayerControl refPlayer = this.refPlayer;
		this.savedPrestigeLevel = ((refPlayer != null) ? refPlayer.PrestigeLevel : 0);
		this.UpdateUsernameText();
	}

	// Token: 0x0600127C RID: 4732 RVA: 0x0007218C File Offset: 0x0007038C
	public void UpdatePosition(int index)
	{
		if (this.target == null)
		{
			return;
		}
		this.displayGroup.alpha = this.GetOpacity();
		float t = this.rect.FollowWorldObject(this.target.transform, this.canvas, index, 0.1f);
		Vector3 a = -this.rect.localPosition.normalized;
		this.rect.up = Vector3.Lerp(a, Vector3.up, t);
		PlayerControl refPlayer = this.refPlayer;
		string text = (refPlayer != null) ? refPlayer.GetUsernameText() : null;
		if (this.NameLabel.text != text)
		{
			this.NameLabel.text = text;
		}
		if (!this.refPlayer.IsDead)
		{
			this.UpdateIcon();
		}
		else
		{
			this.UpdateGhost();
		}
		this.UpdateHealth();
	}

	// Token: 0x0600127D RID: 4733 RVA: 0x00072260 File Offset: 0x00070460
	private float GetOpacity()
	{
		if (this.target.Player.IsSpectator || PlayerControl.myInstance.IsSpectator)
		{
			return 0f;
		}
		if (PanelManager.CurPanel != PanelType.GameInvisible)
		{
			return 0f;
		}
		if (GameHUD.Mode == GameHUD.HUDMode.Off)
		{
			return 0f;
		}
		if (MapManager.InLobbyScene && Vector3.Distance(PlayerCamera.myInstance.transform.position, this.target.transform.position) > 512f)
		{
			return 0f;
		}
		return 1f;
	}

	// Token: 0x0600127E RID: 4734 RVA: 0x000722EC File Offset: 0x000704EC
	private void UpdateIcon()
	{
		this.icon.gameObject.SetActive(true);
		this.GhostGroup.gameObject.SetActive(false);
		Sprite sprite = null;
		if (this.refPlayer.CurMenu != PanelType.GameInvisible)
		{
			sprite = this.MenuIcon;
		}
		this.icon.sprite = sprite;
		this.icon.enabled = (sprite != null);
		if (this.icon.enabled)
		{
			this.icon.transform.localEulerAngles = new Vector3(0f, 0f, -this.rect.localEulerAngles.z);
		}
	}

	// Token: 0x0600127F RID: 4735 RVA: 0x0007238D File Offset: 0x0007058D
	private void UpdateUsernameText()
	{
		TMP_Text nameLabel = this.NameLabel;
		PlayerControl refPlayer = this.refPlayer;
		nameLabel.text = ((refPlayer != null) ? refPlayer.GetUsernameText() : null);
	}

	// Token: 0x06001280 RID: 4736 RVA: 0x000723AC File Offset: 0x000705AC
	private void UpdateGhost()
	{
		this.icon.gameObject.SetActive(false);
		this.GhostGroup.gameObject.SetActive(true);
		this.GhostFill.fillAmount = Mathf.Lerp(this.GhostFill.fillAmount, this.refPlayer.Health.ReviveProgress, Time.deltaTime * 6f);
		bool isAutoReviving = this.refPlayer.Health.isAutoReviving;
		if (this.ReviveComplete.activeSelf != isAutoReviving)
		{
			this.ReviveComplete.SetActive(isAutoReviving);
			this.DeadIconDisplay.SetActive(!isAutoReviving);
		}
	}

	// Token: 0x06001281 RID: 4737 RVA: 0x0007244B File Offset: 0x0007064B
	private void UpdateHealth()
	{
		this.healthFill.fillAmount = Mathf.Lerp(this.healthFill.fillAmount, this.refPlayer.health.CurrentHealthProportion, Time.deltaTime * 4f);
	}

	// Token: 0x06001282 RID: 4738 RVA: 0x00072484 File Offset: 0x00070684
	public void UpdateOpacity()
	{
		bool shouldShow = this.target != null && this.target.ShouldShowName();
		if (PanelManager.CurPanel != PanelType.GameInvisible)
		{
			shouldShow = false;
		}
		this.nameGroup.UpdateOpacity(shouldShow, 4f, false);
		this.nameLayout.ignoreLayout = (this.nameGroup.alpha <= 0f);
	}

	// Token: 0x06001283 RID: 4739 RVA: 0x000724EA File Offset: 0x000706EA
	public PlayerIndicator()
	{
	}

	// Token: 0x04001171 RID: 4465
	public CanvasGroup displayGroup;

	// Token: 0x04001172 RID: 4466
	public CanvasGroup nameGroup;

	// Token: 0x04001173 RID: 4467
	private RectTransform rect;

	// Token: 0x04001174 RID: 4468
	public RectTransform canvas;

	// Token: 0x04001175 RID: 4469
	[CompilerGenerated]
	private PlayerWorldUI <target>k__BackingField;

	// Token: 0x04001176 RID: 4470
	public TextMeshProUGUI NameLabel;

	// Token: 0x04001177 RID: 4471
	private LayoutElement nameLayout;

	// Token: 0x04001178 RID: 4472
	public Image healthFill;

	// Token: 0x04001179 RID: 4473
	private int savedPrestigeLevel;

	// Token: 0x0400117A RID: 4474
	[Header("Context Icons")]
	public Image icon;

	// Token: 0x0400117B RID: 4475
	public Sprite MenuIcon;

	// Token: 0x0400117C RID: 4476
	[Header("Ghost Display")]
	public GameObject GhostGroup;

	// Token: 0x0400117D RID: 4477
	public Image GhostFill;

	// Token: 0x0400117E RID: 4478
	public GameObject DeadIconDisplay;

	// Token: 0x0400117F RID: 4479
	public GameObject ReviveComplete;
}
