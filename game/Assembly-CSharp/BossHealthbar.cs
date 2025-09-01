using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200019D RID: 413
public class BossHealthbar : MonoBehaviour
{
	// Token: 0x06001143 RID: 4419 RVA: 0x0006AF19 File Offset: 0x00069119
	private void Awake()
	{
		BossHealthbar.instance = this;
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.canvasGroup.alpha = 0f;
	}

	// Token: 0x06001144 RID: 4420 RVA: 0x0006AF40 File Offset: 0x00069140
	public void Setup(EntityControl entity)
	{
		this.boss = (entity as AIControl);
		if (this.boss == null)
		{
			return;
		}
		this.UpdateHealthbar(true);
		this.TypeText.text = this.boss.DisplayName;
		if (this.boss.EnemyType != EnemyType.Any)
		{
			GameDB.EnemyTypeInfo enemyInfo = GameDB.GetEnemyInfo(this.boss.EnemyType);
			this.TypeIcon.sprite = ((enemyInfo != null) ? enemyInfo.Icon : null);
		}
		base.StartCoroutine(this.UpdateNameLayout());
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x0006AFC7 File Offset: 0x000691C7
	private IEnumerator UpdateNameLayout()
	{
		yield return true;
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.TypeRect);
		yield break;
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x0006AFD8 File Offset: 0x000691D8
	private void Update()
	{
		bool shouldShow = BossHealthbar.ShouldShow();
		this.canvasGroup.UpdateOpacity(shouldShow, 8f, false);
		if (this.boss == null)
		{
			return;
		}
		this.UpdateHealthbar(false);
		this.UpdateShields(false);
		if (this.TypeText.text != this.boss.DisplayName)
		{
			this.TypeText.text = this.boss.DisplayName;
		}
	}

	// Token: 0x06001147 RID: 4423 RVA: 0x0006B050 File Offset: 0x00069250
	private void UpdateHealthbar(bool instant = false)
	{
		float currentHealthProportion = this.boss.health.CurrentHealthProportion;
		this.HealthbarMain.fillAmount = currentHealthProportion;
		this.HealthAt.value = currentHealthProportion;
		this.HealthAtOpacity.UpdateOpacity(currentHealthProportion < 1f && currentHealthProportion > 0f, 4f, true);
		this.HealthbarDelayed.fillAmount = (instant ? currentHealthProportion : Mathf.Lerp(this.HealthbarDelayed.fillAmount, currentHealthProportion, Time.deltaTime));
		int num = Mathf.FloorToInt((float)this.boss.health.MaxHealth / 50f);
		for (int i = 0; i < this.HealthTicks.Count; i++)
		{
			this.HealthTicks[i].SetActive(i < num);
		}
	}

	// Token: 0x06001148 RID: 4424 RVA: 0x0006B11C File Offset: 0x0006931C
	private void UpdateShields(bool instant = false)
	{
		float currentShieldProportion = this.boss.health.CurrentShieldProportion;
		this.ShieldGroup.UpdateOpacity(this.boss.health.MaxShield > 0, 2f, true);
		this.ShieldBar.fillAmount = currentShieldProportion;
		this.ShieldAt.value = this.ShieldBar.fillAmount;
		this.ShieldAtOpacity.UpdateOpacity(currentShieldProportion < 0.99f && (double)currentShieldProportion > 0.01, 4f, true);
		this.ShieldBarDelayed.fillAmount = (instant ? currentShieldProportion : Mathf.Lerp(this.ShieldBarDelayed.fillAmount, currentShieldProportion, Time.deltaTime));
		int num = Mathf.FloorToInt((float)this.boss.health.MaxShield / 40f);
		for (int i = 0; i < this.ShieldTicks.Count; i++)
		{
			this.ShieldTicks[i].SetActive(i < num);
		}
	}

	// Token: 0x06001149 RID: 4425 RVA: 0x0006B218 File Offset: 0x00069418
	public static bool ShouldShow()
	{
		if (AIManager.instance == null || AIManager.Enemies == null || PlayerControl.myInstance == null)
		{
			return false;
		}
		if (BossHealthbar.instance.boss != null)
		{
			return true;
		}
		if (RaidManager.IsInRaid && !RaidManager.IsEncounterStarted)
		{
			return false;
		}
		foreach (EntityControl entityControl in AIManager.Enemies)
		{
			if (!(entityControl == null))
			{
				AIControl aicontrol = entityControl as AIControl;
				if (aicontrol != null && aicontrol.Level == EnemyLevel.Boss)
				{
					BossHealthbar.instance.Setup(aicontrol);
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600114A RID: 4426 RVA: 0x0006B2DC File Offset: 0x000694DC
	public BossHealthbar()
	{
	}

	// Token: 0x04000FB2 RID: 4018
	private CanvasGroup canvasGroup;

	// Token: 0x04000FB3 RID: 4019
	private static BossHealthbar instance;

	// Token: 0x04000FB4 RID: 4020
	public Image HealthbarMain;

	// Token: 0x04000FB5 RID: 4021
	public Image HealthbarDelayed;

	// Token: 0x04000FB6 RID: 4022
	public Slider HealthAt;

	// Token: 0x04000FB7 RID: 4023
	public CanvasGroup HealthAtOpacity;

	// Token: 0x04000FB8 RID: 4024
	public List<GameObject> HealthTicks = new List<GameObject>();

	// Token: 0x04000FB9 RID: 4025
	public CanvasGroup ShieldGroup;

	// Token: 0x04000FBA RID: 4026
	public Image ShieldBar;

	// Token: 0x04000FBB RID: 4027
	public Image ShieldBarDelayed;

	// Token: 0x04000FBC RID: 4028
	public Slider ShieldAt;

	// Token: 0x04000FBD RID: 4029
	public CanvasGroup ShieldAtOpacity;

	// Token: 0x04000FBE RID: 4030
	public List<GameObject> ShieldTicks = new List<GameObject>();

	// Token: 0x04000FBF RID: 4031
	public Image TypeIcon;

	// Token: 0x04000FC0 RID: 4032
	public TextMeshProUGUI TypeText;

	// Token: 0x04000FC1 RID: 4033
	public RectTransform TypeRect;

	// Token: 0x04000FC2 RID: 4034
	private AIControl boss;

	// Token: 0x02000568 RID: 1384
	[CompilerGenerated]
	private sealed class <UpdateNameLayout>d__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060024CD RID: 9421 RVA: 0x000CF642 File Offset: 0x000CD842
		[DebuggerHidden]
		public <UpdateNameLayout>d__19(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000CF651 File Offset: 0x000CD851
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000CF654 File Offset: 0x000CD854
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			BossHealthbar bossHealthbar = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			LayoutRebuilder.ForceRebuildLayoutImmediate(bossHealthbar.TypeRect);
			return false;
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x000CF6A7 File Offset: 0x000CD8A7
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000CF6AF File Offset: 0x000CD8AF
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x000CF6B6 File Offset: 0x000CD8B6
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400270C RID: 9996
		private int <>1__state;

		// Token: 0x0400270D RID: 9997
		private object <>2__current;

		// Token: 0x0400270E RID: 9998
		public BossHealthbar <>4__this;
	}
}
