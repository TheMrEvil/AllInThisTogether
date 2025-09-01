using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.FTools
{
	// Token: 0x0200005B RID: 91
	[Serializable]
	public class FMuscle_Quaternion
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00017296 File Offset: 0x00015496
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0001729E File Offset: 0x0001549E
		public Quaternion ProceduralRotation
		{
			[CompilerGenerated]
			get
			{
				return this.<ProceduralRotation>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ProceduralRotation>k__BackingField = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000317 RID: 791 RVA: 0x000172A7 File Offset: 0x000154A7
		public bool IsCorrect
		{
			get
			{
				return this.x != null;
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000172B4 File Offset: 0x000154B4
		public void Initialize(Quaternion initRotation)
		{
			this.x = new FMuscle_Float();
			this.y = new FMuscle_Float();
			this.z = new FMuscle_Float();
			this.w = new FMuscle_Float();
			this.x.Initialize(initRotation.x);
			this.y.Initialize(initRotation.y);
			this.z.Initialize(initRotation.z);
			this.w.Initialize(initRotation.w);
			this.ProceduralRotation = initRotation;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00017338 File Offset: 0x00015538
		public bool IsWorking()
		{
			return this.x.IsWorking() || this.y.IsWorking() || this.z.IsWorking() || this.w.IsWorking();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00017370 File Offset: 0x00015570
		public void Push(Quaternion value)
		{
			this.x.Push(value.x);
			this.y.Push(value.y);
			this.z.Push(value.z);
			this.w.Push(value.w);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000173C1 File Offset: 0x000155C1
		public void Push(float v)
		{
			this.x.Push(v);
			this.y.Push(v);
			this.z.Push(v);
			this.w.Push(v);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000173F4 File Offset: 0x000155F4
		public void Push(Quaternion value, float multiply)
		{
			this.x.Push(value.x * multiply);
			this.y.Push(value.y * multiply);
			this.z.Push(value.z * multiply);
			this.w.Push(value.w * multiply);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00017450 File Offset: 0x00015650
		public void Update(float delta, Quaternion desired, float acceleration, float accelerationLimit, float damping, float brakePower)
		{
			this.x.Update(delta, this.ProceduralRotation.x, desired.x, acceleration, accelerationLimit, damping, brakePower);
			this.y.Update(delta, this.ProceduralRotation.y, desired.y, acceleration, accelerationLimit, damping, brakePower);
			this.z.Update(delta, this.ProceduralRotation.z, desired.z, acceleration, accelerationLimit, damping, brakePower);
			this.w.Update(delta, this.ProceduralRotation.w, desired.w, acceleration, accelerationLimit, damping, brakePower);
			this.ProceduralRotation = new Quaternion(this.x.OutValue, this.y.OutValue, this.z.OutValue, this.w.OutValue);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00017524 File Offset: 0x00015724
		public void Update(float delta, Quaternion desired)
		{
			this.x.Update(delta, this.ProceduralRotation.x, desired.x, this.Acceleration, this.AccelerationLimit, this.Damping, this.BrakePower);
			this.y.Update(delta, this.ProceduralRotation.y, desired.y, this.Acceleration, this.AccelerationLimit, this.Damping, this.BrakePower);
			this.z.Update(delta, this.ProceduralRotation.z, desired.z, this.Acceleration, this.AccelerationLimit, this.Damping, this.BrakePower);
			this.w.Update(delta, this.ProceduralRotation.w, desired.w, this.Acceleration, this.AccelerationLimit, this.Damping, this.BrakePower);
			this.ProceduralRotation = new Quaternion(this.x.OutValue, this.y.OutValue, this.z.OutValue, this.w.OutValue);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0001763C File Offset: 0x0001583C
		public void UpdateEnsured(float delta, Quaternion desired)
		{
			this.Update(delta, FMuscle_Quaternion.EnsureQuaternionContinuity(this.ProceduralRotation, desired));
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00017654 File Offset: 0x00015854
		public static Quaternion EnsureQuaternionContinuity(Quaternion latestRot, Quaternion targetRot)
		{
			Quaternion quaternion = new Quaternion(-targetRot.x, -targetRot.y, -targetRot.z, -targetRot.w);
			Quaternion b = new Quaternion(Mathf.LerpUnclamped(latestRot.x, targetRot.x, 0.5f), Mathf.LerpUnclamped(latestRot.y, targetRot.y, 0.5f), Mathf.LerpUnclamped(latestRot.z, targetRot.z, 0.5f), Mathf.LerpUnclamped(latestRot.w, targetRot.w, 0.5f));
			Quaternion b2 = new Quaternion(Mathf.LerpUnclamped(latestRot.x, quaternion.x, 0.5f), Mathf.LerpUnclamped(latestRot.y, quaternion.y, 0.5f), Mathf.LerpUnclamped(latestRot.z, quaternion.z, 0.5f), Mathf.LerpUnclamped(latestRot.w, quaternion.w, 0.5f));
			float num = Quaternion.Angle(latestRot, b);
			if (Quaternion.Angle(latestRot, b2) >= num)
			{
				return targetRot;
			}
			return quaternion;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00017757 File Offset: 0x00015957
		public IEnumerator PushImpulseCoroutine(Quaternion power, float duration, bool fadeOutPower = false, float delay = 0f)
		{
			if (delay > 0f)
			{
				yield return new WaitForSeconds(delay);
			}
			float elapsed = 0f;
			this.Push(0.001f);
			while (elapsed / duration < 1f)
			{
				if (!fadeOutPower)
				{
					this.Push(power, Time.deltaTime * 60f);
				}
				else
				{
					this.Push(power, (1f - elapsed / duration) * Time.deltaTime * 60f);
				}
				elapsed += Time.deltaTime;
				yield return null;
			}
			yield break;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00017784 File Offset: 0x00015984
		public static void Lerp(ref FMuscle_Quaternion source, FMuscle_Quaternion a, FMuscle_Quaternion b, float t)
		{
			if (a == null || b == null || source == null)
			{
				return;
			}
			source.Acceleration = Mathf.LerpUnclamped(a.Acceleration, b.Acceleration, t);
			source.AccelerationLimit = Mathf.LerpUnclamped(a.AccelerationLimit, b.AccelerationLimit, t);
			source.BrakePower = Mathf.LerpUnclamped(a.BrakePower, b.BrakePower, t);
			source.Damping = Mathf.LerpUnclamped(a.Damping, b.Damping, t);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00017800 File Offset: 0x00015A00
		public void OverrideProceduralRotation(Quaternion rotation)
		{
			this.ProceduralRotation = rotation;
			this.DesiredRotation = rotation;
			this.x.OverrideValue(rotation.x);
			this.y.OverrideValue(rotation.y);
			this.z.OverrideValue(rotation.z);
			this.w.OverrideValue(rotation.w);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001785F File Offset: 0x00015A5F
		public FMuscle_Quaternion()
		{
		}

		// Token: 0x040002C8 RID: 712
		[HideInInspector]
		public Quaternion DesiredRotation;

		// Token: 0x040002C9 RID: 713
		[CompilerGenerated]
		private Quaternion <ProceduralRotation>k__BackingField;

		// Token: 0x040002CA RID: 714
		private FMuscle_Float x;

		// Token: 0x040002CB RID: 715
		private FMuscle_Float y;

		// Token: 0x040002CC RID: 716
		private FMuscle_Float z;

		// Token: 0x040002CD RID: 717
		private FMuscle_Float w;

		// Token: 0x040002CE RID: 718
		[FPD_Suffix(0f, 10000f, FPD_SuffixAttribute.SuffixMode.From0to100, "%", true, 0)]
		public float Acceleration = 5000f;

		// Token: 0x040002CF RID: 719
		[FPD_Suffix(0f, 10000f, FPD_SuffixAttribute.SuffixMode.From0to100, "%", true, 0)]
		public float AccelerationLimit = 1000f;

		// Token: 0x040002D0 RID: 720
		[FPD_Suffix(0f, 50f, FPD_SuffixAttribute.SuffixMode.From0to100, "%", true, 0)]
		public float Damping = 10f;

		// Token: 0x040002D1 RID: 721
		[FPD_Suffix(0f, 1f, FPD_SuffixAttribute.SuffixMode.From0to100, "%", true, 0)]
		public float BrakePower = 0.2f;

		// Token: 0x020001AF RID: 431
		[CompilerGenerated]
		private sealed class <PushImpulseCoroutine>d__24 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06000F2E RID: 3886 RVA: 0x00062A39 File Offset: 0x00060C39
			[DebuggerHidden]
			public <PushImpulseCoroutine>d__24(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000F2F RID: 3887 RVA: 0x00062A48 File Offset: 0x00060C48
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000F30 RID: 3888 RVA: 0x00062A4C File Offset: 0x00060C4C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				FMuscle_Quaternion fmuscle_Quaternion = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					if (delay > 0f)
					{
						this.<>2__current = new WaitForSeconds(delay);
						this.<>1__state = 1;
						return true;
					}
					break;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					goto IL_E4;
				default:
					return false;
				}
				elapsed = 0f;
				fmuscle_Quaternion.Push(0.001f);
				IL_E4:
				if (elapsed / duration >= 1f)
				{
					return false;
				}
				if (!fadeOutPower)
				{
					fmuscle_Quaternion.Push(power, Time.deltaTime * 60f);
				}
				else
				{
					fmuscle_Quaternion.Push(power, (1f - elapsed / duration) * Time.deltaTime * 60f);
				}
				elapsed += Time.deltaTime;
				this.<>2__current = null;
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x170001D6 RID: 470
			// (get) Token: 0x06000F31 RID: 3889 RVA: 0x00062B55 File Offset: 0x00060D55
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000F32 RID: 3890 RVA: 0x00062B5D File Offset: 0x00060D5D
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001D7 RID: 471
			// (get) Token: 0x06000F33 RID: 3891 RVA: 0x00062B64 File Offset: 0x00060D64
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000D53 RID: 3411
			private int <>1__state;

			// Token: 0x04000D54 RID: 3412
			private object <>2__current;

			// Token: 0x04000D55 RID: 3413
			public float delay;

			// Token: 0x04000D56 RID: 3414
			public FMuscle_Quaternion <>4__this;

			// Token: 0x04000D57 RID: 3415
			public bool fadeOutPower;

			// Token: 0x04000D58 RID: 3416
			public Quaternion power;

			// Token: 0x04000D59 RID: 3417
			public float duration;

			// Token: 0x04000D5A RID: 3418
			private float <elapsed>5__2;
		}
	}
}
