using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000119 RID: 281
public class Library_Dice : MonoBehaviour
{
	// Token: 0x06000D3B RID: 3387 RVA: 0x00054BA3 File Offset: 0x00052DA3
	public void StartRolling()
	{
		this.CurrentState = Library_DiceGame.DiceState.Rolling;
		this.SpinFX.Play();
		this.RollAudio.Play();
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x00054BC4 File Offset: 0x00052DC4
	public void Land(int value, Library_DiceGame.Result result)
	{
		this.CurrentState = Library_DiceGame.DiceState.Landed;
		this.CurrentValue = value;
		this.SpinFX.Stop();
		this.RollAudio.Stop();
		switch (result)
		{
		case Library_DiceGame.Result.Single:
			AudioManager.PlayClipAtPoint(this.Land_Single, base.transform.position, 1f, 1f, 1f, 10f, 250f);
			this.Land_SingleFX.Play();
			return;
		case Library_DiceGame.Result.Double:
			AudioManager.PlayClipAtPoint(this.Land_Double, base.transform.position, 1f, 1f, 1f, 10f, 250f);
			this.Land_DoubleFX.Play();
			return;
		case Library_DiceGame.Result.Triple:
			AudioManager.PlayClipAtPoint(this.Land_Triple, base.transform.position, 1f, 1f, 1f, 10f, 250f);
			this.Land_TripleFX.Play();
			return;
		case Library_DiceGame.Result.Bad:
			AudioManager.PlayClipAtPoint(this.Land_Nat1, base.transform.position, 1f, 1f, 1f, 10f, 250f);
			this.Land_Nat1FX.Play();
			return;
		default:
			return;
		}
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x00054CFB File Offset: 0x00052EFB
	public void ReturnToWaiting()
	{
		this.CurrentState = Library_DiceGame.DiceState.Waiting;
		this.SpinFX.Stop();
		this.RollAudio.Stop();
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x00054D1C File Offset: 0x00052F1C
	private void Update()
	{
		if (this.debugNum > 0)
		{
			Library_Dice.NumValue number = this.GetNumber(this.debugNum);
			if (number != null)
			{
				this.Cube.localRotation = Quaternion.Lerp(this.Cube.localRotation, number.Rotation, Time.deltaTime * 10f);
			}
			return;
		}
		if (this.CurrentState == Library_DiceGame.DiceState.Waiting)
		{
			this.RotateRandomly(this.RollingShiftSpeed * 0.05f, this.RollinSpinSpeed * 0.05f);
			return;
		}
		if (this.CurrentState == Library_DiceGame.DiceState.Rolling)
		{
			this.RotateRandomly(this.RollingShiftSpeed, this.RollinSpinSpeed);
			return;
		}
		if (this.CurrentState == Library_DiceGame.DiceState.Landed)
		{
			Library_Dice.NumValue number2 = this.GetNumber(this.CurrentValue);
			if (number2 != null)
			{
				this.Cube.localRotation = Quaternion.Lerp(this.Cube.localRotation, number2.Rotation, Time.deltaTime * 10f);
			}
		}
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x00054DF8 File Offset: 0x00052FF8
	private void RotateRandomly(float shiftSpeed, float spinSpeed)
	{
		Vector3 b = UnityEngine.Random.onUnitSphere * shiftSpeed * Time.deltaTime;
		this.rotationVelocity += b;
		this.rotationVelocity = this.rotationVelocity.normalized;
		Quaternion rhs = Quaternion.Euler(this.rotationVelocity * spinSpeed * Time.deltaTime);
		this.Cube.rotation *= rhs;
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x00054E74 File Offset: 0x00053074
	private Library_Dice.NumValue GetNumber(int index)
	{
		foreach (Library_Dice.NumValue numValue in this.Numbers)
		{
			if (numValue.Value == index)
			{
				return numValue;
			}
		}
		return null;
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x00054ED0 File Offset: 0x000530D0
	public void SpinD20(int seed)
	{
		if (this.CurrentState == Library_DiceGame.DiceState.Rolling)
		{
			return;
		}
		int value = new System.Random(seed).Next(1, 21);
		base.StartCoroutine(this.SpinRoutine(value));
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x00054F04 File Offset: 0x00053104
	private IEnumerator SpinRoutine(int value)
	{
		this.StartRolling();
		yield return new WaitForSeconds(2f);
		Library_DiceGame.Result result = Library_DiceGame.Result.Single;
		if (value == 20)
		{
			result = Library_DiceGame.Result.Triple;
		}
		else if (value > 10)
		{
			result = Library_DiceGame.Result.Double;
		}
		else if (value == 1)
		{
			result = Library_DiceGame.Result.Bad;
		}
		this.Land(value, result);
		yield return new WaitForSeconds(1f);
		this.ReturnToWaiting();
		yield break;
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x00054F1C File Offset: 0x0005311C
	public void AddNumber()
	{
		Library_Dice.NumValue numValue = new Library_Dice.NumValue();
		numValue.Value = this.Numbers.Count + 1;
		numValue.Rotation = base.transform.localRotation;
		this.Numbers.Add(numValue);
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x00054F5F File Offset: 0x0005315F
	public Library_Dice()
	{
	}

	// Token: 0x04000AC0 RID: 2752
	public Transform Cube;

	// Token: 0x04000AC1 RID: 2753
	public ParticleSystem SpinFX;

	// Token: 0x04000AC2 RID: 2754
	public float RollingShiftSpeed = 8f;

	// Token: 0x04000AC3 RID: 2755
	public float RollinSpinSpeed = 1800f;

	// Token: 0x04000AC4 RID: 2756
	public AudioFader RollAudio;

	// Token: 0x04000AC5 RID: 2757
	private Vector3 rotationVelocity;

	// Token: 0x04000AC6 RID: 2758
	public AudioClip Land_Single;

	// Token: 0x04000AC7 RID: 2759
	public ParticleSystem Land_SingleFX;

	// Token: 0x04000AC8 RID: 2760
	public AudioClip Land_Double;

	// Token: 0x04000AC9 RID: 2761
	public ParticleSystem Land_DoubleFX;

	// Token: 0x04000ACA RID: 2762
	public AudioClip Land_Triple;

	// Token: 0x04000ACB RID: 2763
	public ParticleSystem Land_TripleFX;

	// Token: 0x04000ACC RID: 2764
	public AudioClip Land_Nat1;

	// Token: 0x04000ACD RID: 2765
	public ParticleSystem Land_Nat1FX;

	// Token: 0x04000ACE RID: 2766
	public List<Library_Dice.NumValue> Numbers = new List<Library_Dice.NumValue>();

	// Token: 0x04000ACF RID: 2767
	public Library_DiceGame.DiceState CurrentState;

	// Token: 0x04000AD0 RID: 2768
	private int CurrentValue;

	// Token: 0x04000AD1 RID: 2769
	public int debugNum;

	// Token: 0x04000AD2 RID: 2770
	private float t;

	// Token: 0x04000AD3 RID: 2771
	private Quaternion wantRot;

	// Token: 0x02000522 RID: 1314
	[Serializable]
	public class NumValue
	{
		// Token: 0x060023E6 RID: 9190 RVA: 0x000CC42C File Offset: 0x000CA62C
		public NumValue()
		{
		}

		// Token: 0x040025FA RID: 9722
		public int Value;

		// Token: 0x040025FB RID: 9723
		public Quaternion Rotation;
	}

	// Token: 0x02000523 RID: 1315
	[CompilerGenerated]
	private sealed class <SpinRoutine>d__27 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060023E7 RID: 9191 RVA: 0x000CC434 File Offset: 0x000CA634
		[DebuggerHidden]
		public <SpinRoutine>d__27(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x000CC443 File Offset: 0x000CA643
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000CC448 File Offset: 0x000CA648
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			Library_Dice library_Dice = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				library_Dice.StartRolling();
				this.<>2__current = new WaitForSeconds(2f);
				this.<>1__state = 1;
				return true;
			case 1:
			{
				this.<>1__state = -1;
				Library_DiceGame.Result result = Library_DiceGame.Result.Single;
				if (value == 20)
				{
					result = Library_DiceGame.Result.Triple;
				}
				else if (value > 10)
				{
					result = Library_DiceGame.Result.Double;
				}
				else if (value == 1)
				{
					result = Library_DiceGame.Result.Bad;
				}
				library_Dice.Land(value, result);
				this.<>2__current = new WaitForSeconds(1f);
				this.<>1__state = 2;
				return true;
			}
			case 2:
				this.<>1__state = -1;
				library_Dice.ReturnToWaiting();
				return false;
			default:
				return false;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060023EA RID: 9194 RVA: 0x000CC501 File Offset: 0x000CA701
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x000CC509 File Offset: 0x000CA709
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060023EC RID: 9196 RVA: 0x000CC510 File Offset: 0x000CA710
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040025FC RID: 9724
		private int <>1__state;

		// Token: 0x040025FD RID: 9725
		private object <>2__current;

		// Token: 0x040025FE RID: 9726
		public Library_Dice <>4__this;

		// Token: 0x040025FF RID: 9727
		public int value;
	}
}
