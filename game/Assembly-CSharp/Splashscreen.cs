using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using InControl;
using UnityEngine;

// Token: 0x020001F1 RID: 497
public class Splashscreen : MonoBehaviour
{
	// Token: 0x06001532 RID: 5426 RVA: 0x000850A4 File Offset: 0x000832A4
	private void Awake()
	{
		this.CanInteract = false;
		this.DarkBackground.alpha = 1f;
		this.LogoGroup.alpha = 0f;
		if (!LibraryManager.DidLoad)
		{
			base.GetComponent<CanvasGroup>().alpha = 1f;
			base.GetComponentInParent<CanvasGroup>().alpha = 1f;
		}
		this.ButtonPrompt.alpha = 0f;
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnEntered));
		component.OnLeftPanel = (Action)Delegate.Combine(component.OnLeftPanel, new Action(this.OnLeftPanel));
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x00085158 File Offset: 0x00083358
	private void OnEntered()
	{
		Splashscreen.WantGreyscale = true;
		this.CanInteract = false;
		this.ButtonPrompt.alpha = 0f;
		base.StartCoroutine("FadeInPrompt");
		base.GetComponent<CanvasGroup>().alpha = 1f;
		base.GetComponentInParent<CanvasGroup>().alpha = 1f;
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x000851AE File Offset: 0x000833AE
	private void OnLeftPanel()
	{
		this.InkFX.Stop();
		base.StopAllCoroutines();
		GameStats.TryTakeSnapshot();
		UnlockManager.TryTakeSnapshot();
		Splashscreen.WantGreyscale = false;
	}

	// Token: 0x06001535 RID: 5429 RVA: 0x000851D4 File Offset: 0x000833D4
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.Splash || !this.CanInteract)
		{
			return;
		}
		if (LibraryManager.DoneLoading)
		{
			this.FullGroup.UpdateOpacity(false, 1f, true);
			return;
		}
		if (InControl.InputManager.AnyKeyIsPressed || InControl.InputManager.ActiveDevice.AnyButtonIsPressed || Input.GetMouseButtonDown(0))
		{
			LibraryManager.instance.LoadTransition();
			this.InkFX.Stop();
			this.OnLeftPanel();
		}
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x00085243 File Offset: 0x00083443
	private IEnumerator FadeInPrompt()
	{
		Splashscreen.WantGreyscale = true;
		yield return true;
		AudioManager.PlayInterfaceSFX(this.IntroStinger, 1f, 1f);
		yield return new WaitForSeconds(0.25f);
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime * 0.66f;
			this.DarkBackground.alpha = Mathf.Lerp(this.DarkBackground.alpha, 0f, Time.deltaTime * 0.75f);
			yield return true;
		}
		yield return new WaitForSeconds(0.15f);
		this.LogoRevealFX.Play();
		this.InkFX.Play();
		t = 0f;
		float loop = 0f;
		yield return new WaitForSeconds(0.1f);
		Splashscreen.WantGreyscale = false;
		for (;;)
		{
			t += Time.deltaTime;
			this.LogoGroup.alpha = Mathf.Lerp(this.LogoGroup.alpha, 1f, Time.deltaTime * 6f);
			this.DarkBackground.alpha = Mathf.Lerp(this.DarkBackground.alpha, 0f, Time.deltaTime * 0.5f);
			if (t > 0.5f)
			{
				this.CanInteract = true;
				this.ButtonPrompt.alpha = Mathf.Lerp(this.ButtonPrompt.alpha, 1f, Time.deltaTime * 1f);
			}
			if (t > 1.25f)
			{
				loop += Time.deltaTime * 0.375f;
				if (loop > 1f)
				{
					loop -= 1f;
				}
				this.TextGroup.alpha = this.ButtonFadeCurve.Evaluate(loop);
			}
			yield return true;
		}
		yield break;
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x00085252 File Offset: 0x00083452
	public Splashscreen()
	{
	}

	// Token: 0x040014A0 RID: 5280
	public CanvasGroup ButtonPrompt;

	// Token: 0x040014A1 RID: 5281
	public CanvasGroup DarkBackground;

	// Token: 0x040014A2 RID: 5282
	public CanvasGroup FullGroup;

	// Token: 0x040014A3 RID: 5283
	public CanvasGroup LogoGroup;

	// Token: 0x040014A4 RID: 5284
	public CanvasGroup TextGroup;

	// Token: 0x040014A5 RID: 5285
	public ParticleSystem LogoRevealFX;

	// Token: 0x040014A6 RID: 5286
	public ParticleSystem InkFX;

	// Token: 0x040014A7 RID: 5287
	public AudioClip IntroStinger;

	// Token: 0x040014A8 RID: 5288
	public AudioClip LoadSFX;

	// Token: 0x040014A9 RID: 5289
	public AudioClip RevealSFX;

	// Token: 0x040014AA RID: 5290
	private bool CanInteract;

	// Token: 0x040014AB RID: 5291
	public AnimationCurve ButtonFadeCurve;

	// Token: 0x040014AC RID: 5292
	public static bool WantGreyscale;

	// Token: 0x020005C3 RID: 1475
	[CompilerGenerated]
	private sealed class <FadeInPrompt>d__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002606 RID: 9734 RVA: 0x000D2C95 File Offset: 0x000D0E95
		[DebuggerHidden]
		public <FadeInPrompt>d__17(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x000D2CA4 File Offset: 0x000D0EA4
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x000D2CA8 File Offset: 0x000D0EA8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			Splashscreen splashscreen = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				Splashscreen.WantGreyscale = true;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				AudioManager.PlayInterfaceSFX(splashscreen.IntroStinger, 1f, 1f);
				this.<>2__current = new WaitForSeconds(0.25f);
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				t = 0f;
				break;
			case 3:
				this.<>1__state = -1;
				break;
			case 4:
				this.<>1__state = -1;
				splashscreen.LogoRevealFX.Play();
				splashscreen.InkFX.Play();
				t = 0f;
				loop = 0f;
				this.<>2__current = new WaitForSeconds(0.1f);
				this.<>1__state = 5;
				return true;
			case 5:
				this.<>1__state = -1;
				Splashscreen.WantGreyscale = false;
				goto IL_17B;
			case 6:
				this.<>1__state = -1;
				goto IL_17B;
			default:
				return false;
			}
			if (t >= 1f)
			{
				this.<>2__current = new WaitForSeconds(0.15f);
				this.<>1__state = 4;
				return true;
			}
			t += Time.deltaTime * 0.66f;
			splashscreen.DarkBackground.alpha = Mathf.Lerp(splashscreen.DarkBackground.alpha, 0f, Time.deltaTime * 0.75f);
			this.<>2__current = true;
			this.<>1__state = 3;
			return true;
			IL_17B:
			t += Time.deltaTime;
			splashscreen.LogoGroup.alpha = Mathf.Lerp(splashscreen.LogoGroup.alpha, 1f, Time.deltaTime * 6f);
			splashscreen.DarkBackground.alpha = Mathf.Lerp(splashscreen.DarkBackground.alpha, 0f, Time.deltaTime * 0.5f);
			if (t > 0.5f)
			{
				splashscreen.CanInteract = true;
				splashscreen.ButtonPrompt.alpha = Mathf.Lerp(splashscreen.ButtonPrompt.alpha, 1f, Time.deltaTime * 1f);
			}
			if (t > 1.25f)
			{
				loop += Time.deltaTime * 0.375f;
				if (loop > 1f)
				{
					loop -= 1f;
				}
				splashscreen.TextGroup.alpha = splashscreen.ButtonFadeCurve.Evaluate(loop);
			}
			this.<>2__current = true;
			this.<>1__state = 6;
			return true;
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x000D2F57 File Offset: 0x000D1157
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x000D2F5F File Offset: 0x000D115F
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x000D2F66 File Offset: 0x000D1166
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002895 RID: 10389
		private int <>1__state;

		// Token: 0x04002896 RID: 10390
		private object <>2__current;

		// Token: 0x04002897 RID: 10391
		public Splashscreen <>4__this;

		// Token: 0x04002898 RID: 10392
		private float <t>5__2;

		// Token: 0x04002899 RID: 10393
		private float <loop>5__3;
	}
}
