using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.FTools
{
	// Token: 0x0200005C RID: 92
	[Serializable]
	public class FMuscle_Eulers
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00017893 File Offset: 0x00015A93
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0001789B File Offset: 0x00015A9B
		public Vector3 ProceduralEulerAngles
		{
			[CompilerGenerated]
			get
			{
				return this.<ProceduralEulerAngles>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ProceduralEulerAngles>k__BackingField = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000327 RID: 807 RVA: 0x000178A4 File Offset: 0x00015AA4
		public Quaternion ProceduralRotation
		{
			get
			{
				return Quaternion.Euler(this.ProceduralEulerAngles);
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000178B4 File Offset: 0x00015AB4
		public void Initialize(Vector3 initEulerAngles)
		{
			this.x = new FMuscle_Angle();
			this.y = new FMuscle_Angle();
			this.z = new FMuscle_Angle();
			this.x.Initialize(initEulerAngles.x);
			this.y.Initialize(initEulerAngles.y);
			this.z.Initialize(initEulerAngles.z);
			this.ProceduralEulerAngles = initEulerAngles;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0001791C File Offset: 0x00015B1C
		public void Initialize(Quaternion initRotation)
		{
			this.Initialize(initRotation.eulerAngles);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0001792B File Offset: 0x00015B2B
		public bool IsWorking()
		{
			return this.x.IsWorking() || this.y.IsWorking() || this.z.IsWorking();
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00017954 File Offset: 0x00015B54
		public void Push(Vector3 value)
		{
			this.x.Push(value.x);
			this.y.Push(value.y);
			this.z.Push(value.z);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00017989 File Offset: 0x00015B89
		public void Push(float v)
		{
			this.x.Push(v);
			this.y.Push(v);
			this.z.Push(v);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000179AF File Offset: 0x00015BAF
		public void Push(Vector3 value, float multiply)
		{
			this.x.Push(value.x * multiply);
			this.y.Push(value.y * multiply);
			this.z.Push(value.z * multiply);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000179EC File Offset: 0x00015BEC
		public void Update(float delta, Vector3 desired, float acceleration, float accelerationLimit, float damping, float brakePower)
		{
			this.x.Update(delta, this.ProceduralEulerAngles.x, desired.x, acceleration, accelerationLimit, damping, brakePower);
			this.y.Update(delta, this.ProceduralEulerAngles.y, desired.y, acceleration, accelerationLimit, damping, brakePower);
			this.z.Update(delta, this.ProceduralEulerAngles.z, desired.z, acceleration, accelerationLimit, damping, brakePower);
			this.ProceduralEulerAngles = new Vector3(this.x.OutValue, this.y.OutValue, this.z.OutValue);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00017A94 File Offset: 0x00015C94
		public Vector3 Update(float delta, Vector3 desired)
		{
			this.x.Update(delta, this.ProceduralEulerAngles.x, desired.x, this.Acceleration, this.AccelerationLimit, this.Damping, this.BrakePower);
			this.y.Update(delta, this.ProceduralEulerAngles.y, desired.y, this.Acceleration, this.AccelerationLimit, this.Damping, this.BrakePower);
			this.z.Update(delta, this.ProceduralEulerAngles.z, desired.z, this.Acceleration, this.AccelerationLimit, this.Damping, this.BrakePower);
			this.ProceduralEulerAngles = new Vector3(this.x.OutValue, this.y.OutValue, this.z.OutValue);
			return this.ProceduralEulerAngles;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00017B72 File Offset: 0x00015D72
		public void Update(float delta, Quaternion desired)
		{
			this.Update(delta, desired.eulerAngles);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00017B83 File Offset: 0x00015D83
		public IEnumerator PushImpulseCoroutine(Vector3 power, float duration, bool fadeOutPower = false, float delay = 0f)
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

		// Token: 0x06000332 RID: 818 RVA: 0x00017BB0 File Offset: 0x00015DB0
		public static void Lerp(ref FMuscle_Eulers source, FMuscle_Eulers a, FMuscle_Eulers b, float t)
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

		// Token: 0x06000333 RID: 819 RVA: 0x00017C2C File Offset: 0x00015E2C
		public FMuscle_Eulers()
		{
		}

		// Token: 0x040002D2 RID: 722
		[HideInInspector]
		public Vector3 DesiredEulerAngles;

		// Token: 0x040002D3 RID: 723
		[CompilerGenerated]
		private Vector3 <ProceduralEulerAngles>k__BackingField;

		// Token: 0x040002D4 RID: 724
		private FMuscle_Angle x;

		// Token: 0x040002D5 RID: 725
		private FMuscle_Angle y;

		// Token: 0x040002D6 RID: 726
		private FMuscle_Angle z;

		// Token: 0x040002D7 RID: 727
		[FPD_Suffix(0f, 10000f, FPD_SuffixAttribute.SuffixMode.From0to100, "%", true, 0)]
		public float Acceleration = 5000f;

		// Token: 0x040002D8 RID: 728
		[FPD_Suffix(0f, 10000f, FPD_SuffixAttribute.SuffixMode.From0to100, "%", true, 0)]
		public float AccelerationLimit = 1000f;

		// Token: 0x040002D9 RID: 729
		[FPD_Suffix(0f, 50f, FPD_SuffixAttribute.SuffixMode.From0to100, "%", true, 0)]
		public float Damping = 10f;

		// Token: 0x040002DA RID: 730
		[FPD_Suffix(0f, 1f, FPD_SuffixAttribute.SuffixMode.From0to100, "%", true, 0)]
		public float BrakePower = 0.2f;

		// Token: 0x020001B0 RID: 432
		[CompilerGenerated]
		private sealed class <PushImpulseCoroutine>d__23 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06000F34 RID: 3892 RVA: 0x00062B6C File Offset: 0x00060D6C
			[DebuggerHidden]
			public <PushImpulseCoroutine>d__23(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000F35 RID: 3893 RVA: 0x00062B7B File Offset: 0x00060D7B
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000F36 RID: 3894 RVA: 0x00062B80 File Offset: 0x00060D80
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				FMuscle_Eulers fmuscle_Eulers = this;
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
				fmuscle_Eulers.Push(0.001f);
				IL_E4:
				if (elapsed / duration >= 1f)
				{
					return false;
				}
				if (!fadeOutPower)
				{
					fmuscle_Eulers.Push(power, Time.deltaTime * 60f);
				}
				else
				{
					fmuscle_Eulers.Push(power, (1f - elapsed / duration) * Time.deltaTime * 60f);
				}
				elapsed += Time.deltaTime;
				this.<>2__current = null;
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x170001D8 RID: 472
			// (get) Token: 0x06000F37 RID: 3895 RVA: 0x00062C89 File Offset: 0x00060E89
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000F38 RID: 3896 RVA: 0x00062C91 File Offset: 0x00060E91
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001D9 RID: 473
			// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00062C98 File Offset: 0x00060E98
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000D5B RID: 3419
			private int <>1__state;

			// Token: 0x04000D5C RID: 3420
			private object <>2__current;

			// Token: 0x04000D5D RID: 3421
			public float delay;

			// Token: 0x04000D5E RID: 3422
			public FMuscle_Eulers <>4__this;

			// Token: 0x04000D5F RID: 3423
			public bool fadeOutPower;

			// Token: 0x04000D60 RID: 3424
			public Vector3 power;

			// Token: 0x04000D61 RID: 3425
			public float duration;

			// Token: 0x04000D62 RID: 3426
			private float <elapsed>5__2;
		}
	}
}
