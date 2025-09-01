using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x0200011A RID: 282
	public abstract class OffsetModifier : MonoBehaviour
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x000526B5 File Offset: 0x000508B5
		protected float deltaTime
		{
			get
			{
				return Time.time - this.lastTime;
			}
		}

		// Token: 0x06000C4B RID: 3147
		protected abstract void OnModifyOffset();

		// Token: 0x06000C4C RID: 3148 RVA: 0x000526C3 File Offset: 0x000508C3
		protected virtual void Start()
		{
			base.StartCoroutine(this.Initiate());
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x000526D2 File Offset: 0x000508D2
		private IEnumerator Initiate()
		{
			while (this.ik == null)
			{
				yield return null;
			}
			IKSolverFullBodyBiped solver = this.ik.solver;
			solver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPreUpdate, new IKSolver.UpdateDelegate(this.ModifyOffset));
			this.lastTime = Time.time;
			yield break;
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x000526E4 File Offset: 0x000508E4
		private void ModifyOffset()
		{
			if (!base.enabled)
			{
				return;
			}
			if (this.weight <= 0f)
			{
				return;
			}
			if (this.deltaTime <= 0f)
			{
				return;
			}
			if (this.ik == null)
			{
				return;
			}
			this.weight = Mathf.Clamp(this.weight, 0f, 1f);
			this.OnModifyOffset();
			this.lastTime = Time.time;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00052754 File Offset: 0x00050954
		protected void ApplyLimits(OffsetModifier.OffsetLimits[] limits)
		{
			foreach (OffsetModifier.OffsetLimits offsetLimits in limits)
			{
				offsetLimits.Apply(this.ik.solver.GetEffector(offsetLimits.effector), base.transform.rotation);
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0005279C File Offset: 0x0005099C
		protected virtual void OnDestroy()
		{
			if (this.ik != null)
			{
				IKSolverFullBodyBiped solver = this.ik.solver;
				solver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPreUpdate, new IKSolver.UpdateDelegate(this.ModifyOffset));
			}
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000527D8 File Offset: 0x000509D8
		protected OffsetModifier()
		{
		}

		// Token: 0x040009A5 RID: 2469
		[Tooltip("The master weight")]
		public float weight = 1f;

		// Token: 0x040009A6 RID: 2470
		[Tooltip("Reference to the FBBIK component")]
		public FullBodyBipedIK ik;

		// Token: 0x040009A7 RID: 2471
		protected float lastTime;

		// Token: 0x02000222 RID: 546
		[Serializable]
		public class OffsetLimits
		{
			// Token: 0x0600117F RID: 4479 RVA: 0x0006CE74 File Offset: 0x0006B074
			public void Apply(IKEffector e, Quaternion rootRotation)
			{
				Vector3 vector = Quaternion.Inverse(rootRotation) * e.positionOffset;
				if (this.spring <= 0f)
				{
					if (this.x)
					{
						vector.x = Mathf.Clamp(vector.x, this.minX, this.maxX);
					}
					if (this.y)
					{
						vector.y = Mathf.Clamp(vector.y, this.minY, this.maxY);
					}
					if (this.z)
					{
						vector.z = Mathf.Clamp(vector.z, this.minZ, this.maxZ);
					}
				}
				else
				{
					if (this.x)
					{
						vector.x = this.SpringAxis(vector.x, this.minX, this.maxX);
					}
					if (this.y)
					{
						vector.y = this.SpringAxis(vector.y, this.minY, this.maxY);
					}
					if (this.z)
					{
						vector.z = this.SpringAxis(vector.z, this.minZ, this.maxZ);
					}
				}
				e.positionOffset = rootRotation * vector;
			}

			// Token: 0x06001180 RID: 4480 RVA: 0x0006CF99 File Offset: 0x0006B199
			private float SpringAxis(float value, float min, float max)
			{
				if (value > min && value < max)
				{
					return value;
				}
				if (value < min)
				{
					return this.Spring(value, min, true);
				}
				return this.Spring(value, max, false);
			}

			// Token: 0x06001181 RID: 4481 RVA: 0x0006CFBC File Offset: 0x0006B1BC
			private float Spring(float value, float limit, bool negative)
			{
				float num = value - limit;
				float num2 = num * this.spring;
				if (negative)
				{
					return value + Mathf.Clamp(-num2, 0f, -num);
				}
				return value - Mathf.Clamp(num2, 0f, num);
			}

			// Token: 0x06001182 RID: 4482 RVA: 0x0006CFF8 File Offset: 0x0006B1F8
			public OffsetLimits()
			{
			}

			// Token: 0x0400102E RID: 4142
			[Tooltip("The effector type (this is just an enum)")]
			public FullBodyBipedEffector effector;

			// Token: 0x0400102F RID: 4143
			[Tooltip("Spring force, if zero then this is a hard limit, if not, offset can exceed the limit.")]
			public float spring;

			// Token: 0x04001030 RID: 4144
			[Tooltip("Which axes to limit the offset on?")]
			public bool x;

			// Token: 0x04001031 RID: 4145
			[Tooltip("Which axes to limit the offset on?")]
			public bool y;

			// Token: 0x04001032 RID: 4146
			[Tooltip("Which axes to limit the offset on?")]
			public bool z;

			// Token: 0x04001033 RID: 4147
			[Tooltip("The limits")]
			public float minX;

			// Token: 0x04001034 RID: 4148
			[Tooltip("The limits")]
			public float maxX;

			// Token: 0x04001035 RID: 4149
			[Tooltip("The limits")]
			public float minY;

			// Token: 0x04001036 RID: 4150
			[Tooltip("The limits")]
			public float maxY;

			// Token: 0x04001037 RID: 4151
			[Tooltip("The limits")]
			public float minZ;

			// Token: 0x04001038 RID: 4152
			[Tooltip("The limits")]
			public float maxZ;
		}

		// Token: 0x02000223 RID: 547
		[CompilerGenerated]
		private sealed class <Initiate>d__8 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06001183 RID: 4483 RVA: 0x0006D000 File Offset: 0x0006B200
			[DebuggerHidden]
			public <Initiate>d__8(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06001184 RID: 4484 RVA: 0x0006D00F File Offset: 0x0006B20F
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06001185 RID: 4485 RVA: 0x0006D014 File Offset: 0x0006B214
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				OffsetModifier offsetModifier = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
				}
				else
				{
					this.<>1__state = -1;
				}
				if (!(offsetModifier.ik == null))
				{
					IKSolverFullBodyBiped solver = offsetModifier.ik.solver;
					solver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPreUpdate, new IKSolver.UpdateDelegate(offsetModifier.ModifyOffset));
					offsetModifier.lastTime = Time.time;
					return false;
				}
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000256 RID: 598
			// (get) Token: 0x06001186 RID: 4486 RVA: 0x0006D09E File Offset: 0x0006B29E
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001187 RID: 4487 RVA: 0x0006D0A6 File Offset: 0x0006B2A6
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000257 RID: 599
			// (get) Token: 0x06001188 RID: 4488 RVA: 0x0006D0AD File Offset: 0x0006B2AD
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04001039 RID: 4153
			private int <>1__state;

			// Token: 0x0400103A RID: 4154
			private object <>2__current;

			// Token: 0x0400103B RID: 4155
			public OffsetModifier <>4__this;
		}
	}
}
