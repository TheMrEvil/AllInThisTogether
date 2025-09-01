using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x0200011B RID: 283
	public abstract class OffsetModifierVRIK : MonoBehaviour
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x000527EB File Offset: 0x000509EB
		protected float deltaTime
		{
			get
			{
				return Time.time - this.lastTime;
			}
		}

		// Token: 0x06000C53 RID: 3155
		protected abstract void OnModifyOffset();

		// Token: 0x06000C54 RID: 3156 RVA: 0x000527F9 File Offset: 0x000509F9
		protected virtual void Start()
		{
			base.StartCoroutine(this.Initiate());
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00052808 File Offset: 0x00050A08
		private IEnumerator Initiate()
		{
			while (this.ik == null)
			{
				yield return null;
			}
			IKSolverVR solver = this.ik.solver;
			solver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPreUpdate, new IKSolver.UpdateDelegate(this.ModifyOffset));
			this.lastTime = Time.time;
			yield break;
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00052818 File Offset: 0x00050A18
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

		// Token: 0x06000C57 RID: 3159 RVA: 0x00052885 File Offset: 0x00050A85
		protected virtual void OnDestroy()
		{
			if (this.ik != null)
			{
				IKSolverVR solver = this.ik.solver;
				solver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPreUpdate, new IKSolver.UpdateDelegate(this.ModifyOffset));
			}
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x000528C1 File Offset: 0x00050AC1
		protected OffsetModifierVRIK()
		{
		}

		// Token: 0x040009A8 RID: 2472
		[Tooltip("The master weight")]
		public float weight = 1f;

		// Token: 0x040009A9 RID: 2473
		[Tooltip("Reference to the VRIK component")]
		public VRIK ik;

		// Token: 0x040009AA RID: 2474
		private float lastTime;

		// Token: 0x02000224 RID: 548
		[CompilerGenerated]
		private sealed class <Initiate>d__7 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06001189 RID: 4489 RVA: 0x0006D0B5 File Offset: 0x0006B2B5
			[DebuggerHidden]
			public <Initiate>d__7(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600118A RID: 4490 RVA: 0x0006D0C4 File Offset: 0x0006B2C4
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600118B RID: 4491 RVA: 0x0006D0C8 File Offset: 0x0006B2C8
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				OffsetModifierVRIK offsetModifierVRIK = this;
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
				if (!(offsetModifierVRIK.ik == null))
				{
					IKSolverVR solver = offsetModifierVRIK.ik.solver;
					solver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPreUpdate, new IKSolver.UpdateDelegate(offsetModifierVRIK.ModifyOffset));
					offsetModifierVRIK.lastTime = Time.time;
					return false;
				}
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000258 RID: 600
			// (get) Token: 0x0600118C RID: 4492 RVA: 0x0006D152 File Offset: 0x0006B352
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600118D RID: 4493 RVA: 0x0006D15A File Offset: 0x0006B35A
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000259 RID: 601
			// (get) Token: 0x0600118E RID: 4494 RVA: 0x0006D161 File Offset: 0x0006B361
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400103C RID: 4156
			private int <>1__state;

			// Token: 0x0400103D RID: 4157
			private object <>2__current;

			// Token: 0x0400103E RID: 4158
			public OffsetModifierVRIK <>4__this;
		}
	}
}
