using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000244 RID: 580
public class TrailerEventHelper : MonoBehaviour
{
	// Token: 0x0600179A RID: 6042 RVA: 0x0009486F File Offset: 0x00092A6F
	private void Awake()
	{
		StateManager.OnNetworkEvent = (Action)Delegate.Combine(StateManager.OnNetworkEvent, new Action(this.TriggerEvent));
	}

	// Token: 0x0600179B RID: 6043 RVA: 0x00094891 File Offset: 0x00092A91
	public void TriggerEvent()
	{
		this.Step++;
		this.TriggerStep();
	}

	// Token: 0x0600179C RID: 6044 RVA: 0x000948A7 File Offset: 0x00092AA7
	private void Update()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		if (PlayerControl.myInstance.actions.Input.interactDownPressed)
		{
			this.ControllerInteract();
		}
	}

	// Token: 0x0600179D RID: 6045 RVA: 0x000948D3 File Offset: 0x00092AD3
	private void ControllerInteract()
	{
	}

	// Token: 0x0600179E RID: 6046 RVA: 0x000948D5 File Offset: 0x00092AD5
	private void TriggerStep()
	{
		this.ScribeLibrarySequence();
	}

	// Token: 0x0600179F RID: 6047 RVA: 0x000948E0 File Offset: 0x00092AE0
	private void ScribeLibrarySequence()
	{
		if (this.Step == 0)
		{
			base.StopAllCoroutines();
			base.StartCoroutine("ScribeOne");
			return;
		}
		base.StopAllCoroutines();
		this.ScribeA.SetActive(false);
		this.ScribeB.SetActive(false);
		this.ScribeC.SetActive(false);
		this.ScribeD.SetActive(false);
		this.RenderA.SetActive(false);
		this.RenderB.SetActive(false);
		this.RenderC.SetActive(false);
		this.RenderD.SetActive(false);
		this.Step = -1;
	}

	// Token: 0x060017A0 RID: 6048 RVA: 0x00094975 File Offset: 0x00092B75
	private IEnumerator ScribeOne()
	{
		yield return new WaitForSeconds(this.firstScribeDelay);
		float fxDelay = 0.05f;
		this.ScribeA.SetActive(true);
		yield return new WaitForSeconds(fxDelay);
		this.RenderA.SetActive(true);
		yield return new WaitForSeconds(this.delayA - fxDelay);
		this.ScribeB.SetActive(true);
		yield return new WaitForSeconds(fxDelay);
		this.RenderB.SetActive(true);
		yield return new WaitForSeconds(this.delayB - fxDelay);
		this.ScribeC.SetActive(true);
		yield return new WaitForSeconds(fxDelay);
		this.RenderC.SetActive(true);
		yield return new WaitForSeconds(this.delayC - fxDelay);
		this.ScribeD.SetActive(true);
		yield return new WaitForSeconds(fxDelay);
		this.RenderD.SetActive(true);
		yield break;
	}

	// Token: 0x060017A1 RID: 6049 RVA: 0x00094984 File Offset: 0x00092B84
	private void ParchmentFX()
	{
		PostFXManager.instance.ActivateSketch(true, true);
	}

	// Token: 0x060017A2 RID: 6050 RVA: 0x00094994 File Offset: 0x00092B94
	private void ParticleSequence()
	{
		if (this.Step == 0)
		{
			base.StartCoroutine("StepOne");
			return;
		}
		if (this.Step == 1)
		{
			base.StartCoroutine("StepTwo");
			return;
		}
		if (this.Step == 2)
		{
			base.StartCoroutine("StepThree");
		}
	}

	// Token: 0x060017A3 RID: 6051 RVA: 0x000949E1 File Offset: 0x00092BE1
	private IEnumerator StepOne()
	{
		float delay = 0.7f;
		this.OrbBlueBurst.Play();
		this.OrbBlue.Play();
		yield return new WaitForSeconds(delay);
		this.OrbYellowBurst.Play();
		this.OrbYellow.Play();
		yield return new WaitForSeconds(delay);
		this.OrbRedBurst.Play();
		this.OrbRed.Play();
		yield break;
	}

	// Token: 0x060017A4 RID: 6052 RVA: 0x000949F0 File Offset: 0x00092BF0
	private IEnumerator StepTwo()
	{
		this.OrbBlue.Stop();
		this.OrbBlueBurst.Play();
		yield return true;
		PlayerControl.myInstance.actions.SetCore(PlayerDB.GetCore(MagicColor.Blue).core);
		AugmentTree augmentByName = GraphDB.GetAugmentByName("Brawler");
		PlayerControl.myInstance.AddAugment(augmentByName, 1);
		yield break;
	}

	// Token: 0x060017A5 RID: 6053 RVA: 0x000949FF File Offset: 0x00092BFF
	private IEnumerator StepThree()
	{
		this.Step = -1;
		this.OrbRed.Stop();
		this.OrbYellow.Stop();
		PlayerControl.myInstance.actions.SetCore(PlayerDB.GetCore(MagicColor.Neutral).core);
		yield return true;
		yield break;
	}

	// Token: 0x060017A6 RID: 6054 RVA: 0x00094A0E File Offset: 0x00092C0E
	public TrailerEventHelper()
	{
	}

	// Token: 0x0400176E RID: 5998
	public int Step = -1;

	// Token: 0x0400176F RID: 5999
	public ParticleSystem OrbBlueBurst;

	// Token: 0x04001770 RID: 6000
	public ParticleSystem OrbBlue;

	// Token: 0x04001771 RID: 6001
	public ParticleSystem OrbYellowBurst;

	// Token: 0x04001772 RID: 6002
	public ParticleSystem OrbYellow;

	// Token: 0x04001773 RID: 6003
	public ParticleSystem OrbRedBurst;

	// Token: 0x04001774 RID: 6004
	public ParticleSystem OrbRed;

	// Token: 0x04001775 RID: 6005
	public float firstScribeDelay;

	// Token: 0x04001776 RID: 6006
	public ParticleSystem[] StartFX;

	// Token: 0x04001777 RID: 6007
	public GameObject ScribeA;

	// Token: 0x04001778 RID: 6008
	public GameObject RenderA;

	// Token: 0x04001779 RID: 6009
	public float delayA = 0.5f;

	// Token: 0x0400177A RID: 6010
	public GameObject ScribeB;

	// Token: 0x0400177B RID: 6011
	public GameObject RenderB;

	// Token: 0x0400177C RID: 6012
	public float delayB = 0.5f;

	// Token: 0x0400177D RID: 6013
	public GameObject ScribeC;

	// Token: 0x0400177E RID: 6014
	public GameObject RenderC;

	// Token: 0x0400177F RID: 6015
	public float delayC = 0.5f;

	// Token: 0x04001780 RID: 6016
	public GameObject ScribeD;

	// Token: 0x04001781 RID: 6017
	public GameObject RenderD;

	// Token: 0x02000604 RID: 1540
	[CompilerGenerated]
	private sealed class <ScribeOne>d__26 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060026D6 RID: 9942 RVA: 0x000D43BC File Offset: 0x000D25BC
		[DebuggerHidden]
		public <ScribeOne>d__26(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x000D43CB File Offset: 0x000D25CB
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x000D43D0 File Offset: 0x000D25D0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			TrailerEventHelper trailerEventHelper = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(trailerEventHelper.firstScribeDelay);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				fxDelay = 0.05f;
				trailerEventHelper.ScribeA.SetActive(true);
				this.<>2__current = new WaitForSeconds(fxDelay);
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				trailerEventHelper.RenderA.SetActive(true);
				this.<>2__current = new WaitForSeconds(trailerEventHelper.delayA - fxDelay);
				this.<>1__state = 3;
				return true;
			case 3:
				this.<>1__state = -1;
				trailerEventHelper.ScribeB.SetActive(true);
				this.<>2__current = new WaitForSeconds(fxDelay);
				this.<>1__state = 4;
				return true;
			case 4:
				this.<>1__state = -1;
				trailerEventHelper.RenderB.SetActive(true);
				this.<>2__current = new WaitForSeconds(trailerEventHelper.delayB - fxDelay);
				this.<>1__state = 5;
				return true;
			case 5:
				this.<>1__state = -1;
				trailerEventHelper.ScribeC.SetActive(true);
				this.<>2__current = new WaitForSeconds(fxDelay);
				this.<>1__state = 6;
				return true;
			case 6:
				this.<>1__state = -1;
				trailerEventHelper.RenderC.SetActive(true);
				this.<>2__current = new WaitForSeconds(trailerEventHelper.delayC - fxDelay);
				this.<>1__state = 7;
				return true;
			case 7:
				this.<>1__state = -1;
				trailerEventHelper.ScribeD.SetActive(true);
				this.<>2__current = new WaitForSeconds(fxDelay);
				this.<>1__state = 8;
				return true;
			case 8:
				this.<>1__state = -1;
				trailerEventHelper.RenderD.SetActive(true);
				return false;
			default:
				return false;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060026D9 RID: 9945 RVA: 0x000D45A7 File Offset: 0x000D27A7
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x000D45AF File Offset: 0x000D27AF
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060026DB RID: 9947 RVA: 0x000D45B6 File Offset: 0x000D27B6
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002982 RID: 10626
		private int <>1__state;

		// Token: 0x04002983 RID: 10627
		private object <>2__current;

		// Token: 0x04002984 RID: 10628
		public TrailerEventHelper <>4__this;

		// Token: 0x04002985 RID: 10629
		private float <fxDelay>5__2;
	}

	// Token: 0x02000605 RID: 1541
	[CompilerGenerated]
	private sealed class <StepOne>d__29 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060026DC RID: 9948 RVA: 0x000D45BE File Offset: 0x000D27BE
		[DebuggerHidden]
		public <StepOne>d__29(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x000D45CD File Offset: 0x000D27CD
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x000D45D0 File Offset: 0x000D27D0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			TrailerEventHelper trailerEventHelper = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				delay = 0.7f;
				trailerEventHelper.OrbBlueBurst.Play();
				trailerEventHelper.OrbBlue.Play();
				this.<>2__current = new WaitForSeconds(delay);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				trailerEventHelper.OrbYellowBurst.Play();
				trailerEventHelper.OrbYellow.Play();
				this.<>2__current = new WaitForSeconds(delay);
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				trailerEventHelper.OrbRedBurst.Play();
				trailerEventHelper.OrbRed.Play();
				return false;
			default:
				return false;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060026DF RID: 9951 RVA: 0x000D4696 File Offset: 0x000D2896
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x000D469E File Offset: 0x000D289E
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060026E1 RID: 9953 RVA: 0x000D46A5 File Offset: 0x000D28A5
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002986 RID: 10630
		private int <>1__state;

		// Token: 0x04002987 RID: 10631
		private object <>2__current;

		// Token: 0x04002988 RID: 10632
		public TrailerEventHelper <>4__this;

		// Token: 0x04002989 RID: 10633
		private float <delay>5__2;
	}

	// Token: 0x02000606 RID: 1542
	[CompilerGenerated]
	private sealed class <StepTwo>d__30 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060026E2 RID: 9954 RVA: 0x000D46AD File Offset: 0x000D28AD
		[DebuggerHidden]
		public <StepTwo>d__30(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x000D46BC File Offset: 0x000D28BC
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x000D46C0 File Offset: 0x000D28C0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			TrailerEventHelper trailerEventHelper = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				trailerEventHelper.OrbBlue.Stop();
				trailerEventHelper.OrbBlueBurst.Play();
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			PlayerControl.myInstance.actions.SetCore(PlayerDB.GetCore(MagicColor.Blue).core);
			AugmentTree augmentByName = GraphDB.GetAugmentByName("Brawler");
			PlayerControl.myInstance.AddAugment(augmentByName, 1);
			return false;
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060026E5 RID: 9957 RVA: 0x000D474F File Offset: 0x000D294F
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x000D4757 File Offset: 0x000D2957
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060026E7 RID: 9959 RVA: 0x000D475E File Offset: 0x000D295E
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400298A RID: 10634
		private int <>1__state;

		// Token: 0x0400298B RID: 10635
		private object <>2__current;

		// Token: 0x0400298C RID: 10636
		public TrailerEventHelper <>4__this;
	}

	// Token: 0x02000607 RID: 1543
	[CompilerGenerated]
	private sealed class <StepThree>d__31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060026E8 RID: 9960 RVA: 0x000D4766 File Offset: 0x000D2966
		[DebuggerHidden]
		public <StepThree>d__31(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x000D4775 File Offset: 0x000D2975
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x000D4778 File Offset: 0x000D2978
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			TrailerEventHelper trailerEventHelper = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				trailerEventHelper.Step = -1;
				trailerEventHelper.OrbRed.Stop();
				trailerEventHelper.OrbYellow.Stop();
				PlayerControl.myInstance.actions.SetCore(PlayerDB.GetCore(MagicColor.Neutral).core);
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			return false;
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060026EB RID: 9963 RVA: 0x000D47F7 File Offset: 0x000D29F7
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x000D47FF File Offset: 0x000D29FF
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060026ED RID: 9965 RVA: 0x000D4806 File Offset: 0x000D2A06
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400298D RID: 10637
		private int <>1__state;

		// Token: 0x0400298E RID: 10638
		private object <>2__current;

		// Token: 0x0400298F RID: 10639
		public TrailerEventHelper <>4__this;
	}
}
