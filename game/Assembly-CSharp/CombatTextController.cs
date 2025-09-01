using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DamageNumbersPro;
using UnityEngine;

// Token: 0x020001A0 RID: 416
public class CombatTextController : MonoBehaviour
{
	// Token: 0x17000142 RID: 322
	// (get) Token: 0x06001162 RID: 4450 RVA: 0x0006BAF0 File Offset: 0x00069CF0
	public static float CurrentDPS
	{
		get
		{
			if (CombatTextController.instance == null)
			{
				return 0f;
			}
			CombatTextController.instance.CleanOldEvents(Time.realtimeSinceStartup);
			float a = 1f;
			if (CombatTextController.instance.LocalDmgEvents.Count > 0)
			{
				a = Time.realtimeSinceStartup - CombatTextController.instance.LocalDmgEvents.Peek().Item2;
			}
			return CombatTextController.instance.LocalDamageTotal / Mathf.Max(a, 1f);
		}
	}

	// Token: 0x06001163 RID: 4451 RVA: 0x0006BB68 File Offset: 0x00069D68
	private void Awake()
	{
		CombatTextController.instance = this;
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x0006BB70 File Offset: 0x00069D70
	private void LateUpdate()
	{
		if (PlayerControl.MyCamera != null)
		{
			Transform transform = PlayerControl.MyCamera.transform;
			this.camPos.SetPositionAndRotation(transform.position, transform.rotation);
		}
		if (this.emphasisThreshold > 25f)
		{
			this.emphasisThreshold -= Time.deltaTime * this.emphasisThreshold * 0.05f;
		}
		for (int i = this.worldNumbers.Count - 1; i >= 0; i--)
		{
			if (this.worldNumbers[i] == null || !this.worldNumbers[i].gameObject.activeSelf)
			{
				this.worldNumbers.RemoveAt(i);
			}
		}
		this.nextUpdateTime -= Time.deltaTime;
		if (this.nextUpdateTime <= 0f && PlayerControl.myInstance != null)
		{
			this.nextUpdateTime = this.UpdateRate;
			PlayerControl.myInstance.Net.CurrentDPS = CombatTextController.CurrentDPS;
		}
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x0006BC74 File Offset: 0x00069E74
	public static void ShowWorldHeal(int num, Vector3 point)
	{
		if (num <= 0 || !Settings.GetBool(SystemSetting.DamageNumbers, true))
		{
			return;
		}
		CombatTextController.instance.HealNumberAlly.Spawn(point, (float)num);
	}

	// Token: 0x06001166 RID: 4454 RVA: 0x0006BC98 File Offset: 0x00069E98
	public static void ShowDamageNum(DNumType numType, int num, Vector3 point, int depth)
	{
		if (!Settings.GetBool(SystemSetting.DamageNumbers, true))
		{
			return;
		}
		DamageNumber damageNumberRef = CombatTextController.GetDamageNumberRef(numType);
		float currentDPS = PlayerControl.myInstance.Net.CurrentDPS;
		bool flag = (float)num < currentDPS * 0.5f && CombatTextController.instance.worldNumbers.Count > 4;
		if (CombatTextController.instance.ShouldIgnore(numType, num) && depth > 0)
		{
			return;
		}
		if (numType == DNumType.Finalize)
		{
			point += Vector3.up;
		}
		DamageNumber spawned = null;
		spawned = ((num > 0) ? damageNumberRef.Spawn(point, (float)num) : damageNumberRef.Spawn(point, "Absorb"));
		spawned.scaleByNumberSettings.toNumber = CombatTextController.instance.emphasisThreshold;
		spawned.scaleByNumberSettings.fromNumber = Mathf.Min(CombatTextController.instance.emphasisThreshold - 1f, currentDPS * 0.66f);
		CombatTextController.instance.UpdateEmphasisInfo(num);
		CombatTextController.instance.worldNumbers.Add(spawned);
		if (flag)
		{
			UnityMainThreadDispatcher.Instance().Invoke(delegate
			{
				spawned.TriggerDestriction();
			}, 0.15f);
		}
	}

	// Token: 0x06001167 RID: 4455 RVA: 0x0006BDC3 File Offset: 0x00069FC3
	private void UpdateEmphasisInfo(int newValue)
	{
		this.emphasisThreshold = Mathf.Max((float)newValue, this.emphasisThreshold);
		this.emphasisThreshold = Mathf.Max(this.emphasisThreshold, PlayerControl.myInstance.Net.CurrentDPS * 0.8f);
	}

	// Token: 0x06001168 RID: 4456 RVA: 0x0006BDFE File Offset: 0x00069FFE
	private bool ShouldIgnore(DNumType numType, int val)
	{
		return numType != DNumType.Blot && this.worldNumbers.Count >= 15 && (float)val < PlayerControl.myInstance.Net.CurrentDPS * 0.5f;
	}

	// Token: 0x06001169 RID: 4457 RVA: 0x0006BE30 File Offset: 0x0006A030
	private static DamageNumber GetDamageNumberRef(DNumType numType)
	{
		DamageNumber result;
		switch (numType)
		{
		case DNumType.Default:
			result = CombatTextController.instance.DamageNumBase;
			break;
		case DNumType.Crit:
			result = CombatTextController.instance.DamageNumCrit;
			break;
		case DNumType.Blot:
			result = CombatTextController.instance.DamageNumDot;
			break;
		case DNumType.Finalize:
			result = CombatTextController.instance.DamageNumPrimed;
			break;
		case DNumType.Greenling:
			result = CombatTextController.instance.DamageNumGreenling;
			break;
		case DNumType.Red:
			result = CombatTextController.instance.DamageNumRed;
			break;
		case DNumType.Orange:
			result = CombatTextController.instance.DamageNumOrange;
			break;
		case DNumType.Teal:
			result = CombatTextController.instance.DamageNumTeal;
			break;
		default:
			result = CombatTextController.instance.DamageNumBase;
			break;
		}
		return result;
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x0006BED9 File Offset: 0x0006A0D9
	public static void ShowLocalHeal(int num)
	{
		CombatTextController.instance.HealFCT.Spawn(Vector3.zero, (float)num).SetAnchoredPosition(CombatTextController.instance.HealPoint, Vector2.zero);
	}

	// Token: 0x0600116B RID: 4459 RVA: 0x0006BF08 File Offset: 0x0006A108
	public static void ShowDamageTaken(DamageInfo dmg)
	{
		if ((int)dmg.TotalAmount == 0)
		{
			CombatTextController.instance.ShieldTakenFCT.Spawn(Vector3.zero, "Absorb").SetAnchoredPosition(CombatTextController.instance.DamageTakenPoint, Vector2.zero);
			return;
		}
		if (dmg.ShieldAmount > dmg.Amount)
		{
			CombatTextController.instance.ShieldTakenFCT.Spawn(Vector3.zero, (float)((int)dmg.TotalAmount)).SetAnchoredPosition(CombatTextController.instance.DamageTakenPoint, Vector2.zero);
			return;
		}
		CombatTextController.instance.DamageTakenFCT.Spawn(Vector3.zero, (float)((int)dmg.TotalAmount)).SetAnchoredPosition(CombatTextController.instance.DamageTakenPoint, Vector2.zero);
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x0006BFBC File Offset: 0x0006A1BC
	public void AddLocalDamageEvent(DamageInfo dmg)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		this.LocalDmgEvents.Enqueue(new ValueTuple<float, float>(dmg.TotalAmount, realtimeSinceStartup));
		this.LocalDamageTotal += dmg.TotalAmount;
		CombatTextController.lastUpdateTime = realtimeSinceStartup;
		this.CleanOldEvents(realtimeSinceStartup);
	}

	// Token: 0x0600116D RID: 4461 RVA: 0x0006C008 File Offset: 0x0006A208
	private void CleanOldEvents(float currentTimestamp)
	{
		while (this.LocalDmgEvents.Count > 0 && currentTimestamp - this.LocalDmgEvents.Peek().Item2 >= this.WindowSize)
		{
			ValueTuple<float, float> valueTuple = this.LocalDmgEvents.Dequeue();
			this.LocalDamageTotal -= valueTuple.Item1;
		}
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x0006C05E File Offset: 0x0006A25E
	public CombatTextController()
	{
	}

	// Token: 0x04000FE3 RID: 4067
	public static CombatTextController instance;

	// Token: 0x04000FE4 RID: 4068
	[Header("World Space Damage Numbers")]
	public DamageNumber DamageNumBase;

	// Token: 0x04000FE5 RID: 4069
	public DamageNumber DamageNumCrit;

	// Token: 0x04000FE6 RID: 4070
	public DamageNumber DamageNumDot;

	// Token: 0x04000FE7 RID: 4071
	public DamageNumber DamageNumRed;

	// Token: 0x04000FE8 RID: 4072
	public DamageNumber DamageNumPrimed;

	// Token: 0x04000FE9 RID: 4073
	public DamageNumber DamageNumGreenling;

	// Token: 0x04000FEA RID: 4074
	public DamageNumber DamageNumOrange;

	// Token: 0x04000FEB RID: 4075
	public DamageNumber DamageNumTeal;

	// Token: 0x04000FEC RID: 4076
	public DamageNumber HealNumberAlly;

	// Token: 0x04000FED RID: 4077
	public Transform camPos;

	// Token: 0x04000FEE RID: 4078
	[Header("HUD Space Numbers")]
	public RectTransform HealPoint;

	// Token: 0x04000FEF RID: 4079
	public DamageNumber HealFCT;

	// Token: 0x04000FF0 RID: 4080
	public RectTransform DamageTakenPoint;

	// Token: 0x04000FF1 RID: 4081
	public DamageNumber DamageTakenFCT;

	// Token: 0x04000FF2 RID: 4082
	public DamageNumber ShieldTakenFCT;

	// Token: 0x04000FF3 RID: 4083
	private List<DamageNumber> worldNumbers = new List<DamageNumber>();

	// Token: 0x04000FF4 RID: 4084
	private float emphasisThreshold = 25f;

	// Token: 0x04000FF5 RID: 4085
	[Header("DPS Tracking")]
	public float LocalDamageTotal;

	// Token: 0x04000FF6 RID: 4086
	[TupleElementNames(new string[]
	{
		"dmg",
		"time"
	})]
	private Queue<ValueTuple<float, float>> LocalDmgEvents = new Queue<ValueTuple<float, float>>();

	// Token: 0x04000FF7 RID: 4087
	private float nextUpdateTime;

	// Token: 0x04000FF8 RID: 4088
	private static float lastUpdateTime;

	// Token: 0x04000FF9 RID: 4089
	public float UpdateRate = 1f;

	// Token: 0x04000FFA RID: 4090
	public float WindowSize = 8f;

	// Token: 0x0200056B RID: 1387
	[CompilerGenerated]
	private sealed class <>c__DisplayClass29_0
	{
		// Token: 0x060024DA RID: 9434 RVA: 0x000CF791 File Offset: 0x000CD991
		public <>c__DisplayClass29_0()
		{
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000CF799 File Offset: 0x000CD999
		internal void <ShowDamageNum>b__0()
		{
			this.spawned.TriggerDestriction();
		}

		// Token: 0x04002715 RID: 10005
		public DamageNumber spawned;
	}
}
