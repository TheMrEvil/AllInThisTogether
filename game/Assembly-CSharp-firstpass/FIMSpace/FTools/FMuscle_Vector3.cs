using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.FTools
{
	// Token: 0x0200005A RID: 90
	[Serializable]
	public class FMuscle_Vector3
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00016E46 File Offset: 0x00015046
		// (set) Token: 0x06000306 RID: 774 RVA: 0x00016E4E File Offset: 0x0001504E
		public Vector3 ProceduralPosition
		{
			[CompilerGenerated]
			get
			{
				return this.<ProceduralPosition>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ProceduralPosition>k__BackingField = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00016E57 File Offset: 0x00015057
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00016E5F File Offset: 0x0001505F
		public bool Initialized
		{
			[CompilerGenerated]
			get
			{
				return this.<Initialized>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Initialized>k__BackingField = value;
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00016E68 File Offset: 0x00015068
		public void Initialize(Vector3 initPosition)
		{
			this.x = new FMuscle_Float();
			this.y = new FMuscle_Float();
			this.z = new FMuscle_Float();
			this.x.Initialize(initPosition.x);
			this.y.Initialize(initPosition.y);
			this.z.Initialize(initPosition.z);
			this.ProceduralPosition = initPosition;
			this.Initialized = true;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00016ED7 File Offset: 0x000150D7
		public bool IsWorking()
		{
			return this.x.IsWorking() || this.y.IsWorking() || this.z.IsWorking();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00016F00 File Offset: 0x00015100
		public void Push(Vector3 value)
		{
			this.x.Push(value.x);
			this.y.Push(value.y);
			this.z.Push(value.z);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00016F35 File Offset: 0x00015135
		public void Reset(Vector3 value)
		{
			this.x.Initialize(value.x);
			this.y.Initialize(value.y);
			this.z.Initialize(value.z);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00016F6A File Offset: 0x0001516A
		public void Push(float v)
		{
			this.x.Push(v);
			this.y.Push(v);
			this.z.Push(v);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00016F90 File Offset: 0x00015190
		public void MotionInfluence(Vector3 offset)
		{
			this.x.OffsetValue(offset.x);
			this.y.OffsetValue(offset.y);
			this.z.OffsetValue(offset.z);
			this.ProceduralPosition += offset;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00016FE4 File Offset: 0x000151E4
		public void Update(float delta, Vector3 desired, float acceleration, float accelerationLimit, float damping, float brakePower)
		{
			this.x.Update(delta, this.ProceduralPosition.x, desired.x, acceleration, accelerationLimit, damping, brakePower);
			this.y.Update(delta, this.ProceduralPosition.y, desired.y, acceleration, accelerationLimit, damping, brakePower);
			this.z.Update(delta, this.ProceduralPosition.z, desired.z, acceleration, accelerationLimit, damping, brakePower);
			this.ProceduralPosition = new Vector3(this.x.OutValue, this.y.OutValue, this.z.OutValue);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0001708C File Offset: 0x0001528C
		public Vector3 Update(float delta, Vector3 desired)
		{
			this.x.Update(delta, this.ProceduralPosition.x, desired.x, this.Acceleration, this.AccelerationLimit, this.Damping, this.BrakePower);
			this.y.Update(delta, this.ProceduralPosition.y, desired.y, this.Acceleration, this.AccelerationLimit, this.Damping, this.BrakePower);
			this.z.Update(delta, this.ProceduralPosition.z, desired.z, this.Acceleration, this.AccelerationLimit, this.Damping, this.BrakePower);
			this.ProceduralPosition = new Vector3(this.x.OutValue, this.y.OutValue, this.z.OutValue);
			return this.ProceduralPosition;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001716A File Offset: 0x0001536A
		public IEnumerator PushImpulseCoroutine(Vector3 power, float duration, bool fadeOutPower = false, float delay = 0f)
		{
			if (delay > 0f)
			{
				yield return new WaitForSeconds(delay);
			}
			float elapsed = 0f;
			this.Push(0.0001f);
			while (elapsed / duration < 1f)
			{
				if (!fadeOutPower)
				{
					this.Push(power * Time.deltaTime * 60f);
				}
				else
				{
					this.Push(power * (1f - elapsed / duration) * Time.deltaTime * 60f);
				}
				elapsed += Time.deltaTime;
				yield return null;
			}
			yield break;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00017198 File Offset: 0x00015398
		public static void Lerp(ref FMuscle_Vector3 source, FMuscle_Vector3 a, FMuscle_Vector3 b, float t)
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

		// Token: 0x06000313 RID: 787 RVA: 0x00017214 File Offset: 0x00015414
		public void OverrideProceduralPosition(Vector3 newPos)
		{
			this.ProceduralPosition = newPos;
			this.DesiredPosition = newPos;
			this.x.OverrideValue(newPos.x);
			this.y.OverrideValue(newPos.y);
			this.z.OverrideValue(newPos.z);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00017262 File Offset: 0x00015462
		public FMuscle_Vector3()
		{
		}

		// Token: 0x040002BE RID: 702
		[HideInInspector]
		public Vector3 DesiredPosition;

		// Token: 0x040002BF RID: 703
		[CompilerGenerated]
		private Vector3 <ProceduralPosition>k__BackingField;

		// Token: 0x040002C0 RID: 704
		[CompilerGenerated]
		private bool <Initialized>k__BackingField;

		// Token: 0x040002C1 RID: 705
		private FMuscle_Float x;

		// Token: 0x040002C2 RID: 706
		private FMuscle_Float y;

		// Token: 0x040002C3 RID: 707
		private FMuscle_Float z;

		// Token: 0x040002C4 RID: 708
		[FPD_Suffix(0f, 10000f, FPD_SuffixAttribute.SuffixMode.From0to100, "%", true, 0)]
		public float Acceleration = 10000f;

		// Token: 0x040002C5 RID: 709
		[FPD_Suffix(0f, 10000f, FPD_SuffixAttribute.SuffixMode.From0to100, "%", true, 0)]
		public float AccelerationLimit = 5000f;

		// Token: 0x040002C6 RID: 710
		[FPD_Suffix(0f, 50f, FPD_SuffixAttribute.SuffixMode.From0to100, "%", true, 0)]
		public float Damping = 10f;

		// Token: 0x040002C7 RID: 711
		[FPD_Suffix(0f, 1f, FPD_SuffixAttribute.SuffixMode.From0to100, "%", true, 0)]
		public float BrakePower = 0.2f;

		// Token: 0x020001AE RID: 430
		[CompilerGenerated]
		private sealed class <PushImpulseCoroutine>d__24 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06000F28 RID: 3880 RVA: 0x000628EF File Offset: 0x00060AEF
			[DebuggerHidden]
			public <PushImpulseCoroutine>d__24(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000F29 RID: 3881 RVA: 0x000628FE File Offset: 0x00060AFE
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000F2A RID: 3882 RVA: 0x00062900 File Offset: 0x00060B00
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				FMuscle_Vector3 fmuscle_Vector = this;
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
					goto IL_FD;
				default:
					return false;
				}
				elapsed = 0f;
				fmuscle_Vector.Push(0.0001f);
				IL_FD:
				if (elapsed / duration >= 1f)
				{
					return false;
				}
				if (!fadeOutPower)
				{
					fmuscle_Vector.Push(power * Time.deltaTime * 60f);
				}
				else
				{
					fmuscle_Vector.Push(power * (1f - elapsed / duration) * Time.deltaTime * 60f);
				}
				elapsed += Time.deltaTime;
				this.<>2__current = null;
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x170001D4 RID: 468
			// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00062A22 File Offset: 0x00060C22
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000F2C RID: 3884 RVA: 0x00062A2A File Offset: 0x00060C2A
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001D5 RID: 469
			// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00062A31 File Offset: 0x00060C31
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000D4B RID: 3403
			private int <>1__state;

			// Token: 0x04000D4C RID: 3404
			private object <>2__current;

			// Token: 0x04000D4D RID: 3405
			public float delay;

			// Token: 0x04000D4E RID: 3406
			public FMuscle_Vector3 <>4__this;

			// Token: 0x04000D4F RID: 3407
			public bool fadeOutPower;

			// Token: 0x04000D50 RID: 3408
			public Vector3 power;

			// Token: 0x04000D51 RID: 3409
			public float duration;

			// Token: 0x04000D52 RID: 3410
			private float <elapsed>5__2;
		}
	}
}
