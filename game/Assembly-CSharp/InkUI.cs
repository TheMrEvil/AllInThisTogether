using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B7 RID: 439
public class InkUI : MonoBehaviour
{
	// Token: 0x06001214 RID: 4628 RVA: 0x0007009D File Offset: 0x0006E29D
	private void Awake()
	{
		this.Motes = new List<InkUI.UIMote>();
		InkUI.instance = this;
	}

	// Token: 0x06001215 RID: 4629 RVA: 0x000700B0 File Offset: 0x0006E2B0
	public void DebugAdd()
	{
		this.AddInk(25, Fountain.instance.transform.position, false);
	}

	// Token: 0x06001216 RID: 4630 RVA: 0x000700CA File Offset: 0x0006E2CA
	private void Update()
	{
		PlayerControl.myInstance == null;
	}

	// Token: 0x06001217 RID: 4631 RVA: 0x000700D8 File Offset: 0x0006E2D8
	private void UpdateInteractionPrompt()
	{
		if (GameplayManager.CurState == GameState.InWave && FountainWorldUI.InFountainRange)
		{
			this.DepositFill.fillAmount = Fountain.instance.Interaction.HoldTimer / Fountain.instance.Interaction.InteractTime;
		}
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x00070118 File Offset: 0x0006E318
	private void UpdateMotes()
	{
		for (int i = this.Motes.Count - 1; i >= 0; i--)
		{
			this.Motes[i].TickUpdate(Time.deltaTime);
			if (this.Motes[i].ReachedTarget())
			{
				UnityEngine.Object.Destroy(this.Motes[i].t.gameObject);
				this.InkOffset -= this.Motes[i].InkValue;
				this.Motes.RemoveAt(i);
				this.Pulse();
			}
		}
	}

	// Token: 0x06001219 RID: 4633 RVA: 0x000701B4 File Offset: 0x0006E3B4
	private void Pulse()
	{
		if (Time.realtimeSinceStartup - this.lastInkPulse < 0.075f)
		{
			return;
		}
		this.InkAnim.Play("Ink_Pulse");
		this.lastInkPulse = Time.realtimeSinceStartup;
		AudioManager.PlayInterfaceSFX(this.InkGainClip, 1f, 0f);
	}

	// Token: 0x0600121A RID: 4634 RVA: 0x00070208 File Offset: 0x0006E408
	public void AddInk(int Amount, Vector3 AtPoint, bool is2D)
	{
		if (!is2D)
		{
			AtPoint = this.TransformVectorToUI(AtPoint);
		}
		this.InkOffset += (float)Amount;
		int num = (int)this.CountCurve.Evaluate((float)Amount);
		float value = (float)Amount / (float)num;
		for (int i = 0; i < num; i++)
		{
			this.AddMote(AtPoint, value);
		}
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x00070259 File Offset: 0x0006E459
	private Vector3 TransformVectorToUI(Vector3 AtPoint)
	{
		if (PlayerControl.myInstance == null)
		{
			return Vector3.zero;
		}
		this.pointHelper.FollowWorldPoint(AtPoint, this.rect, 0, 0f);
		return this.pointHelper.position;
	}

	// Token: 0x0600121C RID: 4636 RVA: 0x00070294 File Offset: 0x0006E494
	private void AddMote(Vector3 atPoint, float value)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.InkMoteRef, this.InkMoteRef.transform.parent);
		gameObject.transform.SetPositionAndRotation(atPoint, this.InkMoteRef.transform.rotation);
		gameObject.SetActive(true);
		InkUI.UIMote uimote = new InkUI.UIMote(gameObject.transform, this.MoteTarget.localPosition);
		uimote.InkValue = value;
		this.Motes.Add(uimote);
	}

	// Token: 0x0600121D RID: 4637 RVA: 0x00070308 File Offset: 0x0006E508
	public InkUI()
	{
	}

	// Token: 0x040010E9 RID: 4329
	public static InkUI instance;

	// Token: 0x040010EA RID: 4330
	public Animator InkAnim;

	// Token: 0x040010EB RID: 4331
	public TextMeshProUGUI InkText;

	// Token: 0x040010EC RID: 4332
	public AudioClip InkGainClip;

	// Token: 0x040010ED RID: 4333
	public Image GoalFill;

	// Token: 0x040010EE RID: 4334
	public Image SelfFill;

	// Token: 0x040010EF RID: 4335
	public Image TeamFill;

	// Token: 0x040010F0 RID: 4336
	public CanvasGroup SelfBlink;

	// Token: 0x040010F1 RID: 4337
	public AnimationCurve BlinkCurve;

	// Token: 0x040010F2 RID: 4338
	[Header("UI Motes")]
	public AnimationCurve CountCurve;

	// Token: 0x040010F3 RID: 4339
	public float FlyOffset;

	// Token: 0x040010F4 RID: 4340
	public Vector2 FlyTime;

	// Token: 0x040010F5 RID: 4341
	public AnimationCurve FlyCurve;

	// Token: 0x040010F6 RID: 4342
	public AnimationCurve FlySizeCurve;

	// Token: 0x040010F7 RID: 4343
	public AnimationCurve FlyAlphaGroup;

	// Token: 0x040010F8 RID: 4344
	public GameObject InkMoteRef;

	// Token: 0x040010F9 RID: 4345
	private List<InkUI.UIMote> Motes = new List<InkUI.UIMote>();

	// Token: 0x040010FA RID: 4346
	public Transform MoteTarget;

	// Token: 0x040010FB RID: 4347
	public RectTransform pointHelper;

	// Token: 0x040010FC RID: 4348
	[Header("Interaction Prompt")]
	public CanvasGroup HintGroup;

	// Token: 0x040010FD RID: 4349
	public CanvasGroup DepositGroup;

	// Token: 0x040010FE RID: 4350
	public Image DepositFill;

	// Token: 0x040010FF RID: 4351
	public RectTransform rect;

	// Token: 0x04001100 RID: 4352
	private float InkOffset;

	// Token: 0x04001101 RID: 4353
	private float lerpAmt;

	// Token: 0x04001102 RID: 4354
	private float lastInkPulse;

	// Token: 0x0200057A RID: 1402
	private class UIMote
	{
		// Token: 0x06002513 RID: 9491 RVA: 0x000D02B4 File Offset: 0x000CE4B4
		public UIMote(Transform transform, Vector3 target)
		{
			this.t = transform;
			this.cgp = this.t.GetComponent<CanvasGroup>();
			this.A = this.t.localPosition;
			float flyOffset = InkUI.instance.FlyOffset;
			this.fT = UnityEngine.Random.Range(InkUI.instance.FlyTime.x, InkUI.instance.FlyTime.y);
			this.B += new Vector3(UnityEngine.Random.Range(-flyOffset, flyOffset), UnityEngine.Random.Range(-flyOffset, flyOffset), 0f);
			this.C = target;
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x000D0358 File Offset: 0x000CE558
		public void TickUpdate(float deltaTime)
		{
			this.T += deltaTime / this.fT;
			Vector3 a = Vector3.Lerp(this.A, this.B, this.T);
			Vector3 b = Vector3.Lerp(this.B, this.C, this.T);
			this.t.localPosition = Vector3.Lerp(a, b, InkUI.instance.FlyCurve.Evaluate(this.T));
			this.t.localScale = Vector3.one * InkUI.instance.FlySizeCurve.Evaluate(this.T);
			this.cgp.alpha = InkUI.instance.FlyAlphaGroup.Evaluate(this.T);
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000D041B File Offset: 0x000CE61B
		public bool ReachedTarget()
		{
			return Vector3.Distance(this.t.localPosition, this.C) < 20f;
		}

		// Token: 0x0400274D RID: 10061
		public Transform t;

		// Token: 0x0400274E RID: 10062
		private Vector3 A;

		// Token: 0x0400274F RID: 10063
		private Vector3 B;

		// Token: 0x04002750 RID: 10064
		private Vector3 C;

		// Token: 0x04002751 RID: 10065
		private float fT;

		// Token: 0x04002752 RID: 10066
		private float T;

		// Token: 0x04002753 RID: 10067
		public float InkValue;

		// Token: 0x04002754 RID: 10068
		private CanvasGroup cgp;
	}
}
