using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A7 RID: 423
public class EnemyUIDisplay : MonoBehaviour
{
	// Token: 0x17000144 RID: 324
	// (get) Token: 0x06001193 RID: 4499 RVA: 0x0006CAC0 File Offset: 0x0006ACC0
	// (set) Token: 0x06001192 RID: 4498 RVA: 0x0006CAB7 File Offset: 0x0006ACB7
	public EntityControl followingControl
	{
		[CompilerGenerated]
		get
		{
			return this.<followingControl>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			this.<followingControl>k__BackingField = value;
		}
	}

	// Token: 0x17000145 RID: 325
	// (get) Token: 0x06001194 RID: 4500 RVA: 0x0006CAC8 File Offset: 0x0006ACC8
	private bool WantVisible
	{
		get
		{
			return !this.IsFree && this.FocusVal > 0f && PlayerControl.myInstance != null;
		}
	}

	// Token: 0x17000146 RID: 326
	// (get) Token: 0x06001195 RID: 4501 RVA: 0x0006CAEC File Offset: 0x0006ACEC
	public bool IsFree
	{
		get
		{
			return this.followingControl == null;
		}
	}

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x06001196 RID: 4502 RVA: 0x0006CAFA File Offset: 0x0006ACFA
	public float Opacity
	{
		get
		{
			return this.canvasGroup.alpha;
		}
	}

	// Token: 0x06001197 RID: 4503 RVA: 0x0006CB07 File Offset: 0x0006AD07
	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.canvasGroup.alpha = 0f;
		this.TypeRect = this.TypeInfo.GetComponent<RectTransform>();
	}

	// Token: 0x06001198 RID: 4504 RVA: 0x0006CB38 File Offset: 0x0006AD38
	public void Setup(EntityControl entity, float FocusVal)
	{
		this.followingControl = entity;
		if (this.followingControl == null)
		{
			return;
		}
		if (this.followingControl.display.HUDAnchor == null)
		{
			Debug.LogError(this.followingControl.gameObject.name + " has no HUD Anchor!");
			this.followingControl = null;
			return;
		}
		this.FocusVal = FocusVal;
		this.UpdatePosition();
		this.UpdateHealthbar(true);
		if (!this.IgnoreColorSetting)
		{
			int @int = Settings.GetInt(SystemSetting.EnemyHealthbar, 0);
			this.HealthbarMain.color = ((@int == 0) ? AIManager.instance.DB.ThematicColor : AIManager.instance.DB.TraditionalColor);
		}
		this.TypeInfo.alpha = 0f;
		AIControl aicontrol = entity as AIControl;
		if (aicontrol != null && entity.TeamID == 2 && !aicontrol.Display.ShowAsAlly)
		{
			if (aicontrol.EnemyType != EnemyType.Any)
			{
				GameDB.EnemyTypeInfo enemyInfo = GameDB.GetEnemyInfo(aicontrol.EnemyType);
				this.TypeIcon.sprite = ((enemyInfo != null) ? enemyInfo.Icon : null);
				this.TypeText.text = ((enemyInfo != null) ? enemyInfo.NameSimple : null);
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.TypeRect);
			}
			if (this.EliteRight != null)
			{
				GameObject eliteRight = this.EliteRight;
				if (eliteRight != null)
				{
					eliteRight.SetActive(aicontrol.Level == EnemyLevel.Elite || aicontrol.Level == EnemyLevel.Boss);
				}
				GameObject eliteLeft = this.EliteLeft;
				if (eliteLeft != null)
				{
					eliteLeft.SetActive(aicontrol.Level == EnemyLevel.Elite);
				}
				GameObject bossLeft = this.BossLeft;
				if (bossLeft == null)
				{
					return;
				}
				bossLeft.SetActive(aicontrol.Level == EnemyLevel.Boss);
			}
		}
	}

	// Token: 0x06001199 RID: 4505 RVA: 0x0006CCDC File Offset: 0x0006AEDC
	public void TickUpdate()
	{
		this.canvasGroup.UpdateOpacity(this.WantVisible, this.WantVisible ? 8f : 2f, false);
		if (this.followingControl == null)
		{
			return;
		}
		if (this.followingControl.IsDead || this.LOSBlocked())
		{
			this.FocusVal = 0f;
		}
		if (!this.followingControl.Targetable && !this.followingControl.Affectable && !(this.followingControl as AIControl).Display.HealthbarAlwaysOn)
		{
			this.FocusVal = 0f;
		}
		this.FocusVal -= Time.deltaTime;
		this.UpdatePosition();
		this.UpdateHealthbar(false);
		this.UpdateShields(false);
		this.UpdateDetails();
		this.UpdateStatusEffects();
		if (this.canvasGroup.alpha <= 0f && !this.WantVisible)
		{
			this.followingControl = null;
		}
	}

	// Token: 0x0600119A RID: 4506 RVA: 0x0006CDD0 File Offset: 0x0006AFD0
	private bool LOSBlocked()
	{
		if (PlayerMovement.myCamera == null)
		{
			return true;
		}
		Vector3 vector = PlayerMovement.myCamera.transform.position - base.transform.position;
		return Physics.Raycast(base.transform.position, vector.normalized, vector.magnitude, EnemyUIDisplayManager.instance.LOSMask);
	}

	// Token: 0x0600119B RID: 4507 RVA: 0x0006CE40 File Offset: 0x0006B040
	private void UpdateHealthbar(bool instant = false)
	{
		float currentHealthProportion = this.followingControl.health.CurrentHealthProportion;
		this.HealthbarMain.fillAmount = currentHealthProportion;
		this.HealthAt.value = currentHealthProportion;
		this.HealthAtOpacity.UpdateOpacity(currentHealthProportion < 1f && currentHealthProportion > 0f, 4f, true);
		this.HealthbarDelayed.fillAmount = (instant ? currentHealthProportion : Mathf.Lerp(this.HealthbarDelayed.fillAmount, currentHealthProportion, Time.deltaTime));
		int num = Mathf.FloorToInt((float)this.followingControl.health.MaxHealth / 20f);
		for (int i = 0; i < this.HealthTicks.Count; i++)
		{
			this.HealthTicks[i].SetActive(i < num);
		}
	}

	// Token: 0x0600119C RID: 4508 RVA: 0x0006CF0C File Offset: 0x0006B10C
	private void UpdateShields(bool instant = false)
	{
		float currentShieldProportion = this.followingControl.health.CurrentShieldProportion;
		this.ShieldGroup.UpdateOpacity(this.followingControl.health.MaxShield > 0, 2f, true);
		this.ShieldBar.fillAmount = (instant ? currentShieldProportion : Mathf.Lerp(this.ShieldBar.fillAmount, currentShieldProportion, Time.deltaTime * 2f));
		this.ShieldAt.value = this.ShieldBar.fillAmount;
		this.ShieldAtOpacity.UpdateOpacity(currentShieldProportion < 0.93f && (double)currentShieldProportion > 0.05, 4f, true);
		int num = Mathf.FloorToInt((float)this.followingControl.health.MaxShield / 20f);
		for (int i = 0; i < this.ShieldTicks.Count; i++)
		{
			this.ShieldTicks[i].SetActive(i < num);
		}
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x0006D004 File Offset: 0x0006B204
	private void UpdateDetails()
	{
		UnityEngine.Object followingControl = this.followingControl;
		PlayerControl myInstance = PlayerControl.myInstance;
		bool shouldShow = followingControl == ((myInstance != null) ? myInstance.currentTarget : null) && this.TypeText.text.Length > 0;
		this.TypeInfo.UpdateOpacity(shouldShow, 3f, true);
		float d = (float)((this.ShieldGroup.alpha > 0f) ? 25 : 0);
		this.TypeRect.anchoredPosition = Vector3.Lerp(this.TypeRect.anchoredPosition, Vector3.up * d, Time.deltaTime * 6f);
	}

	// Token: 0x0600119E RID: 4510 RVA: 0x0006D0AC File Offset: 0x0006B2AC
	private void UpdateStatusEffects()
	{
		if (this.followingControl == null)
		{
			foreach (EnemyUIDisplay.StatusDisplayGeneric statusDisplayGeneric in this.GenericStatuses)
			{
				statusDisplayGeneric.DisplayObj.SetActive(false);
			}
			foreach (EnemyUIDisplay.SignatureStatusDisplay signatureStatusDisplay in this.SignatureStatuses)
			{
				signatureStatusDisplay.DisplayObj.SetActive(false);
			}
			if (this.ImmunityGroup.alpha > 0f)
			{
				this.ImmunityGroup.HideImmediate();
			}
			return;
		}
		foreach (EnemyUIDisplay.StatusDisplayGeneric statusDisplayGeneric2 in this.GenericStatuses)
		{
			EntityControl.AppliedStatus statusInfoByID = this.followingControl.GetStatusInfoByID(statusDisplayGeneric2.Status.ID, -1);
			if (statusInfoByID != null != statusDisplayGeneric2.DisplayObj.activeSelf)
			{
				statusDisplayGeneric2.DisplayObj.SetActive(statusInfoByID != null);
			}
		}
		bool flag = false;
		foreach (EnemyUIDisplay.SignatureStatusDisplay signatureStatusDisplay2 in this.SignatureStatuses)
		{
			EntityControl.AppliedStatus statusInfoByID2 = this.followingControl.GetStatusInfoByID(signatureStatusDisplay2.Status.ID, PlayerControl.myInstance.ViewID);
			if (statusInfoByID2 != null && !flag)
			{
				flag = true;
				if (!signatureStatusDisplay2.DisplayObj.activeSelf)
				{
					signatureStatusDisplay2.DisplayObj.SetActive(true);
				}
				if (signatureStatusDisplay2.Label != null && signatureStatusDisplay2.Label.text != statusInfoByID2.Stacks.ToString())
				{
					signatureStatusDisplay2.Label.text = statusInfoByID2.Stacks.ToString();
				}
			}
			else if (signatureStatusDisplay2.DisplayObj.activeSelf)
			{
				signatureStatusDisplay2.DisplayObj.SetActive(false);
			}
		}
		EnemyUIDisplay.StatusDisplayGeneric statusDisplayGeneric3 = null;
		foreach (EnemyUIDisplay.StatusDisplayGeneric statusDisplayGeneric4 in this.ImmunityStatuses)
		{
			if (this.followingControl.GetStatusInfoByID(statusDisplayGeneric4.Status.ID, -1) != null)
			{
				statusDisplayGeneric3 = statusDisplayGeneric4;
				break;
			}
		}
		if (statusDisplayGeneric3 != null)
		{
			CanvasGroup immunityGroup = this.ImmunityGroup;
			if (immunityGroup != null)
			{
				immunityGroup.UpdateOpacity(true, 8f, true);
			}
			using (List<EnemyUIDisplay.StatusDisplayGeneric>.Enumerator enumerator = this.ImmunityStatuses.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EnemyUIDisplay.StatusDisplayGeneric statusDisplayGeneric5 = enumerator.Current;
					statusDisplayGeneric5.DisplayObj.SetActive(statusDisplayGeneric5 == statusDisplayGeneric3);
				}
				return;
			}
		}
		if (this.ImmunityGroup != null)
		{
			this.ImmunityGroup.UpdateOpacity(false, 4f, true);
		}
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x0006D3D4 File Offset: 0x0006B5D4
	private void UpdatePosition()
	{
		base.transform.position = this.followingControl.display.HUDAnchor.position;
		if (PlayerControl.myInstance != null)
		{
			Vector3 vector = PlayerMovement.myCamera.transform.position - base.transform.position;
			base.transform.LookAt(base.transform.position - vector.normalized);
			float magnitude = vector.magnitude;
			float d = 1f;
			if (this.Display == EnemyUIDisplay.DisplayType.Elite)
			{
				d = 1.5f;
			}
			base.transform.localScale = Vector3.one * magnitude * 0.033f * d;
		}
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x0006D496 File Offset: 0x0006B696
	public EnemyUIDisplay()
	{
	}

	// Token: 0x0400102F RID: 4143
	private CanvasGroup canvasGroup;

	// Token: 0x04001030 RID: 4144
	[CompilerGenerated]
	private EntityControl <followingControl>k__BackingField;

	// Token: 0x04001031 RID: 4145
	public EnemyUIDisplay.DisplayType Display;

	// Token: 0x04001032 RID: 4146
	public Image HealthbarMain;

	// Token: 0x04001033 RID: 4147
	public Image HealthbarDelayed;

	// Token: 0x04001034 RID: 4148
	public Slider HealthAt;

	// Token: 0x04001035 RID: 4149
	public CanvasGroup HealthAtOpacity;

	// Token: 0x04001036 RID: 4150
	public List<GameObject> HealthTicks = new List<GameObject>();

	// Token: 0x04001037 RID: 4151
	public CanvasGroup ShieldGroup;

	// Token: 0x04001038 RID: 4152
	public Image ShieldBar;

	// Token: 0x04001039 RID: 4153
	public Slider ShieldAt;

	// Token: 0x0400103A RID: 4154
	public CanvasGroup ShieldAtOpacity;

	// Token: 0x0400103B RID: 4155
	public List<GameObject> ShieldTicks = new List<GameObject>();

	// Token: 0x0400103C RID: 4156
	public List<EnemyUIDisplay.StatusDisplayGeneric> GenericStatuses = new List<EnemyUIDisplay.StatusDisplayGeneric>();

	// Token: 0x0400103D RID: 4157
	public List<EnemyUIDisplay.SignatureStatusDisplay> SignatureStatuses = new List<EnemyUIDisplay.SignatureStatusDisplay>();

	// Token: 0x0400103E RID: 4158
	public CanvasGroup ImmunityGroup;

	// Token: 0x0400103F RID: 4159
	public List<EnemyUIDisplay.StatusDisplayGeneric> ImmunityStatuses = new List<EnemyUIDisplay.StatusDisplayGeneric>();

	// Token: 0x04001040 RID: 4160
	public CanvasGroup TypeInfo;

	// Token: 0x04001041 RID: 4161
	public Image TypeIcon;

	// Token: 0x04001042 RID: 4162
	public TextMeshProUGUI TypeText;

	// Token: 0x04001043 RID: 4163
	public RectTransform TypeRect;

	// Token: 0x04001044 RID: 4164
	public GameObject EliteRight;

	// Token: 0x04001045 RID: 4165
	public GameObject BossLeft;

	// Token: 0x04001046 RID: 4166
	public GameObject EliteLeft;

	// Token: 0x04001047 RID: 4167
	public bool IgnoreColorSetting;

	// Token: 0x04001048 RID: 4168
	public float FocusVal;

	// Token: 0x0200056F RID: 1391
	public enum DisplayType
	{
		// Token: 0x04002728 RID: 10024
		Default,
		// Token: 0x04002729 RID: 10025
		Elite,
		// Token: 0x0400272A RID: 10026
		Ally
	}

	// Token: 0x02000570 RID: 1392
	[Serializable]
	public class StatusDisplayGeneric
	{
		// Token: 0x060024ED RID: 9453 RVA: 0x000CFC27 File Offset: 0x000CDE27
		public StatusDisplayGeneric()
		{
		}

		// Token: 0x0400272B RID: 10027
		public StatusTree Status;

		// Token: 0x0400272C RID: 10028
		public GameObject DisplayObj;
	}

	// Token: 0x02000571 RID: 1393
	[Serializable]
	public class SignatureStatusDisplay : EnemyUIDisplay.StatusDisplayGeneric
	{
		// Token: 0x060024EE RID: 9454 RVA: 0x000CFC2F File Offset: 0x000CDE2F
		public SignatureStatusDisplay()
		{
		}

		// Token: 0x0400272D RID: 10029
		public TextMeshProUGUI Label;
	}
}
