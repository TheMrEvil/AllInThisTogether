using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A4 RID: 420
public class DeathRecap : MonoBehaviour
{
	// Token: 0x0600117F RID: 4479 RVA: 0x0006C490 File Offset: 0x0006A690
	private void Awake()
	{
		DeathRecap.instance = this;
	}

	// Token: 0x06001180 RID: 4480 RVA: 0x0006C498 File Offset: 0x0006A698
	public static void OnDeath(DamageInfo dmg)
	{
		if (DeathRecap.instance == null)
		{
			return;
		}
		DeathRecap.instance.OnLocalPlayerDied(dmg);
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x0006C4B4 File Offset: 0x0006A6B4
	private void OnLocalPlayerDied(DamageInfo dmg)
	{
		this.SetupName(dmg.SourceID);
		string str = "<sprite name=\"sym_dmg\"> " + ((int)dmg.TotalAmount).ToString() + " <color=#ababab>|</color> ";
		string text = string.IsNullOrEmpty(dmg.CauseName) ? "Unknown" : dmg.CauseName;
		this.DetailText.text = str + text;
		this.minTimer = 5f;
		if (RaidManager.IsInRaid)
		{
			RaidRecord.AddDeathReason(text + "|" + ((int)dmg.TotalAmount).ToString());
		}
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x0006C54C File Offset: 0x0006A74C
	private void SetupName(int killerID)
	{
		EntityControl entity = EntityControl.GetEntity(killerID);
		if (entity == null)
		{
			if (killerID >= 0)
			{
				this.KillerName.text = "The Torn";
			}
			else
			{
				this.KillerName.text = "The Editors";
			}
		}
		else
		{
			AIControl aicontrol = entity as AIControl;
			if (aicontrol != null)
			{
				this.KillerName.text = (string.IsNullOrEmpty(aicontrol.DisplayName) ? "The Torn" : aicontrol.DisplayName);
			}
			else if (entity == PlayerControl.myInstance)
			{
				this.KillerName.text = "Self";
			}
			else
			{
				PlayerControl playerControl = entity as PlayerControl;
				if (playerControl != null)
				{
					this.KillerName.text = playerControl.Username;
				}
			}
		}
		if (entity != null)
		{
			AIControl aicontrol2 = entity as AIControl;
			if (aicontrol2 != null)
			{
				AIData.TornFamilyInfo familyData = AIManager.instance.DB.GetFamilyData(aicontrol2.EnemyType);
				if (familyData != null)
				{
					this.KillerIcon.sprite = familyData.FamilySprite;
				}
			}
		}
		base.StartCoroutine("ReLayout");
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x0006C648 File Offset: 0x0006A848
	private IEnumerator ReLayout()
	{
		yield return true;
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.NameGroup);
		yield break;
	}

	// Token: 0x06001184 RID: 4484 RVA: 0x0006C658 File Offset: 0x0006A858
	public void TickUpdate(bool isDead)
	{
		this.Fader.UpdateOpacity(isDead || this.minTimer > 0f, 6f, true);
		if (this.minTimer > 0f)
		{
			this.minTimer -= GameplayManager.deltaTime;
		}
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x0006C6A8 File Offset: 0x0006A8A8
	public DeathRecap()
	{
	}

	// Token: 0x04001019 RID: 4121
	public static DeathRecap instance;

	// Token: 0x0400101A RID: 4122
	public CanvasGroup Fader;

	// Token: 0x0400101B RID: 4123
	public Image KillerIcon;

	// Token: 0x0400101C RID: 4124
	public TextMeshProUGUI KillerName;

	// Token: 0x0400101D RID: 4125
	public TextMeshProUGUI DetailText;

	// Token: 0x0400101E RID: 4126
	public RectTransform NameGroup;

	// Token: 0x0400101F RID: 4127
	private float minTimer;

	// Token: 0x0200056D RID: 1389
	[CompilerGenerated]
	private sealed class <ReLayout>d__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060024E2 RID: 9442 RVA: 0x000CFAE8 File Offset: 0x000CDCE8
		[DebuggerHidden]
		public <ReLayout>d__11(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000CFAF7 File Offset: 0x000CDCF7
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000CFAFC File Offset: 0x000CDCFC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			DeathRecap deathRecap = this;
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
			LayoutRebuilder.ForceRebuildLayoutImmediate(deathRecap.NameGroup);
			return false;
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060024E5 RID: 9445 RVA: 0x000CFB4F File Offset: 0x000CDD4F
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000CFB57 File Offset: 0x000CDD57
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060024E7 RID: 9447 RVA: 0x000CFB5E File Offset: 0x000CDD5E
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400271E RID: 10014
		private int <>1__state;

		// Token: 0x0400271F RID: 10015
		private object <>2__current;

		// Token: 0x04002720 RID: 10016
		public DeathRecap <>4__this;
	}
}
