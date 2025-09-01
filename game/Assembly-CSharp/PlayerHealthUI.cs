using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001C0 RID: 448
public class PlayerHealthUI : MonoBehaviour
{
	// Token: 0x17000152 RID: 338
	// (get) Token: 0x0600126D RID: 4717 RVA: 0x00071CD3 File Offset: 0x0006FED3
	private bool ShouldShow
	{
		get
		{
			return !UITransitionHelper.InWavePrelim() && PanelManager.CurPanel != PanelType.Augments && (!TutorialManager.InTutorial || TutorialManager.CurrentStep >= TutorialStep.CombatA) && (!PlayerNook.IsInEditMode || !MapManager.InLobbyScene);
		}
	}

	// Token: 0x0600126E RID: 4718 RVA: 0x00071D0A File Offset: 0x0006FF0A
	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.shieldMat = this.shieldFill.material;
		this.healthMat = this.healthFill.material;
	}

	// Token: 0x0600126F RID: 4719 RVA: 0x00071D3C File Offset: 0x0006FF3C
	private void Update()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		this.canvasGroup.UpdateOpacity(this.ShouldShow, 2f, true);
		this.teammateGroup.UpdateOpacity(this.ShouldShow, 2f, true);
		this.health = PlayerControl.myInstance.health;
		this.HealthDisplays();
		this.ShieldDisplay();
		this.ReviveDisplay();
		if (this.cachedPrestige != Progression.PrestigeCount)
		{
			this.cachedPrestige = Progression.PrestigeCount;
			this.PrestigeIcon.gameObject.SetActive(this.cachedPrestige > 0);
			this.PrestigeIcon.sprite = MetaDB.GetPrestigeIcon(Progression.PrestigeCount);
		}
	}

	// Token: 0x06001270 RID: 4720 RVA: 0x00071DF0 File Offset: 0x0006FFF0
	private void HealthDisplays()
	{
		this.healthText.text = string.Format("<size=52>{0}</size><color=#BAD47E>/{1}</color>", this.health.health, this.health.MaxHealth);
		float @float = this.healthMat.GetFloat(PlayerHealthUI.Value);
		float num = this.health.CurrentHealthProportion;
		if (Mathf.Abs(@float - this.health.CurrentHealthProportion) > 0.025f)
		{
			num = Mathf.Lerp(@float, num, Time.deltaTime * 3f);
		}
		this.healthMat.SetFloat(PlayerHealthUI.Value, num);
	}

	// Token: 0x06001271 RID: 4721 RVA: 0x00071E8C File Offset: 0x0007008C
	private void ShieldDisplay()
	{
		int num = Mathf.Max(Mathf.FloorToInt(this.health.shield), 0);
		if (this.health.MaxShield > 0)
		{
			this.shieldText.text = string.Format("<size=54>{0}</size>/<color=#8BBAEF>{1}</color>", num, this.health.MaxShield);
		}
		else
		{
			this.shieldText.text = string.Format("<color=#8BBAEF><size=54>{0}</size></color>", num);
		}
		float @float = this.shieldMat.GetFloat(PlayerHealthUI.Value);
		float num2 = this.health.CurrentShieldProportion;
		if (Mathf.Abs(@float - num2) > 0.025f)
		{
			num2 = Mathf.Lerp(@float, num2, Time.deltaTime * 3f);
		}
		this.shieldMat.SetFloat(PlayerHealthUI.Value, num2);
		this.overShieldGroup.UpdateOpacity(this.health.shield > (float)this.health.MaxShield, 4f, false);
	}

	// Token: 0x06001272 RID: 4722 RVA: 0x00071F80 File Offset: 0x00070180
	private void ReviveDisplay()
	{
		this.t -= Time.deltaTime;
		if (this.t <= 0f)
		{
			this.cachedReviveCount = (RaidManager.IsInRaid ? 0 : PlayerControl.myInstance.Health.AutoReviveCount);
			this.t = 0.5f;
		}
		int num = Mathf.Max(0, this.cachedReviveCount - PlayerControl.myInstance.Health.AutoRevivesUsed);
		if (num < this.lastRemaining && GameplayManager.IsInGame)
		{
			this.ReviveUsedAnim(this.lastRemaining, num);
		}
		this.lastRemaining = num;
		this.ReviveGroup.UpdateOpacity(num > 0 || this.isShowingAnim, 3f, true);
		if (this.ReviveCount.text != num.ToString())
		{
			this.ReviveCount.text = num.ToString();
		}
	}

	// Token: 0x06001273 RID: 4723 RVA: 0x00072060 File Offset: 0x00070260
	private void ReviveUsedAnim(int prev, int cur)
	{
		this.isShowingAnim = true;
		this.Anim_PrevCount.text = prev.ToString();
		this.Anim_CurCount.text = cur.ToString();
		this.AnimationHolder.gameObject.SetActive(true);
		this.AnimationHolder.Play();
		this.ReviveCount.gameObject.SetActive(false);
		UnityMainThreadDispatcher.Instance().Invoke(delegate
		{
			this.AnimationHolder.gameObject.SetActive(false);
			this.ReviveCount.gameObject.SetActive(true);
			this.isShowingAnim = false;
		}, 1.2f);
	}

	// Token: 0x06001274 RID: 4724 RVA: 0x000720E1 File Offset: 0x000702E1
	public PlayerHealthUI()
	{
	}

	// Token: 0x06001275 RID: 4725 RVA: 0x000720E9 File Offset: 0x000702E9
	// Note: this type is marked as 'beforefieldinit'.
	static PlayerHealthUI()
	{
	}

	// Token: 0x06001276 RID: 4726 RVA: 0x000720FA File Offset: 0x000702FA
	[CompilerGenerated]
	private void <ReviveUsedAnim>b__29_0()
	{
		this.AnimationHolder.gameObject.SetActive(false);
		this.ReviveCount.gameObject.SetActive(true);
		this.isShowingAnim = false;
	}

	// Token: 0x0400115B RID: 4443
	[Header("Health")]
	public Image healthFill;

	// Token: 0x0400115C RID: 4444
	private Material healthMat;

	// Token: 0x0400115D RID: 4445
	public TextMeshProUGUI healthText;

	// Token: 0x0400115E RID: 4446
	[Header("Shield")]
	public CanvasGroup overShieldGroup;

	// Token: 0x0400115F RID: 4447
	public Image shieldFill;

	// Token: 0x04001160 RID: 4448
	private Material shieldMat;

	// Token: 0x04001161 RID: 4449
	public TextMeshProUGUI shieldText;

	// Token: 0x04001162 RID: 4450
	[Header("Revives")]
	public CanvasGroup ReviveGroup;

	// Token: 0x04001163 RID: 4451
	public TextMeshProUGUI ReviveCount;

	// Token: 0x04001164 RID: 4452
	public Animation AnimationHolder;

	// Token: 0x04001165 RID: 4453
	public TextMeshProUGUI Anim_PrevCount;

	// Token: 0x04001166 RID: 4454
	public TextMeshProUGUI Anim_CurCount;

	// Token: 0x04001167 RID: 4455
	[Header("Others")]
	public Image PrestigeIcon;

	// Token: 0x04001168 RID: 4456
	private CanvasGroup canvasGroup;

	// Token: 0x04001169 RID: 4457
	public CanvasGroup teammateGroup;

	// Token: 0x0400116A RID: 4458
	private int cachedPrestige;

	// Token: 0x0400116B RID: 4459
	private EntityHealth health;

	// Token: 0x0400116C RID: 4460
	private static readonly int Value = Shader.PropertyToID("_Value");

	// Token: 0x0400116D RID: 4461
	private int cachedReviveCount;

	// Token: 0x0400116E RID: 4462
	private int lastRemaining;

	// Token: 0x0400116F RID: 4463
	private bool isShowingAnim;

	// Token: 0x04001170 RID: 4464
	private float t;
}
