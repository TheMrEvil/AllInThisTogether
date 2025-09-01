using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200012C RID: 300
	public class MechSpiderLeg : MonoBehaviour
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00055C53 File Offset: 0x00053E53
		public bool isStepping
		{
			get
			{
				return this.stepProgress < 1f;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00055C62 File Offset: 0x00053E62
		// (set) Token: 0x06000CAD RID: 3245 RVA: 0x00055C74 File Offset: 0x00053E74
		public Vector3 position
		{
			get
			{
				return this.ik.GetIKSolver().GetIKPosition();
			}
			set
			{
				this.ik.GetIKSolver().SetIKPosition(value);
			}
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00055C88 File Offset: 0x00053E88
		private void Awake()
		{
			this.ik = base.GetComponent<IK>();
			if (this.foot != null)
			{
				if (this.footUpAxis == Vector3.zero)
				{
					this.footUpAxis = Quaternion.Inverse(this.foot.rotation) * Vector3.up;
				}
				this.lastFootLocalRotation = this.foot.localRotation;
				IKSolver iksolver = this.ik.GetIKSolver();
				iksolver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(iksolver.OnPostUpdate, new IKSolver.UpdateDelegate(this.AfterIK));
			}
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00055D20 File Offset: 0x00053F20
		private void AfterIK()
		{
			if (this.foot == null)
			{
				return;
			}
			this.foot.localRotation = this.lastFootLocalRotation;
			this.smoothHitNormal = Vector3.Slerp(this.smoothHitNormal, this.hit.normal, Time.deltaTime * this.footRotationSpeed);
			Quaternion lhs = Quaternion.FromToRotation(this.foot.rotation * this.footUpAxis, this.smoothHitNormal);
			this.foot.rotation = lhs * this.foot.rotation;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00055DB4 File Offset: 0x00053FB4
		private void Start()
		{
			this.stepProgress = 1f;
			this.hit = default(RaycastHit);
			IKSolver.Point[] points = this.ik.GetIKSolver().GetPoints();
			this.position = points[points.Length - 1].transform.position;
			this.lastStepPosition = this.position;
			this.hit.point = this.position;
			this.defaultPosition = this.mechSpider.transform.InverseTransformPoint(this.position + this.offset * this.mechSpider.scale);
			base.StartCoroutine(this.Step(this.position, this.position));
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00055E70 File Offset: 0x00054070
		private Vector3 GetStepTarget(out bool stepFound, float focus, float distance)
		{
			stepFound = false;
			Vector3 a = this.mechSpider.transform.TransformPoint(this.defaultPosition) + this.mechSpider.velocity * this.velocityPrediction;
			Vector3 vector = this.mechSpider.transform.up;
			Vector3 rhs = this.mechSpider.body.position - this.position;
			Vector3 axis = Vector3.Cross(vector, rhs);
			vector = Quaternion.AngleAxis(focus, axis) * vector;
			if (Physics.Raycast(a + vector * this.mechSpider.raycastHeight * this.mechSpider.scale, -vector, out this.hit, this.mechSpider.raycastHeight * this.mechSpider.scale + distance, this.mechSpider.raycastLayers))
			{
				stepFound = true;
			}
			return this.hit.point + this.hit.normal * this.footHeight * this.mechSpider.scale;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00055F90 File Offset: 0x00054190
		private void UpdatePosition(float distance)
		{
			Vector3 up = this.mechSpider.transform.up;
			if (Physics.Raycast(this.lastStepPosition + up * this.mechSpider.raycastHeight * this.mechSpider.scale, -up, out this.hit, this.mechSpider.raycastHeight * this.mechSpider.scale + distance, this.mechSpider.raycastLayers))
			{
				this.position = this.hit.point + this.hit.normal * this.footHeight * this.mechSpider.scale;
			}
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00056054 File Offset: 0x00054254
		private void Update()
		{
			this.UpdatePosition(this.mechSpider.raycastDistance * this.mechSpider.scale);
			if (this.isStepping)
			{
				return;
			}
			if (Time.time < this.lastStepTime + this.minDelay)
			{
				return;
			}
			if (this.unSync != null && this.unSync.isStepping)
			{
				return;
			}
			bool flag = false;
			Vector3 stepTarget = this.GetStepTarget(out flag, this.raycastFocus, this.mechSpider.raycastDistance * this.mechSpider.scale);
			if (!flag)
			{
				stepTarget = this.GetStepTarget(out flag, -this.raycastFocus, this.mechSpider.raycastDistance * 3f * this.mechSpider.scale);
			}
			if (!flag)
			{
				return;
			}
			if (Vector3.Distance(this.position, stepTarget) < this.maxOffset * this.mechSpider.scale * UnityEngine.Random.Range(0.9f, 1.2f))
			{
				return;
			}
			base.StopAllCoroutines();
			base.StartCoroutine(this.Step(this.position, stepTarget));
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0005615F File Offset: 0x0005435F
		private IEnumerator Step(Vector3 stepStartPosition, Vector3 targetPosition)
		{
			this.stepProgress = 0f;
			while (this.stepProgress < 1f)
			{
				this.stepProgress += Time.deltaTime * this.stepSpeed;
				this.position = Vector3.Lerp(stepStartPosition, targetPosition, this.stepProgress);
				this.position += this.mechSpider.transform.up * this.yOffset.Evaluate(this.stepProgress) * this.mechSpider.scale;
				this.lastStepPosition = this.position;
				yield return null;
			}
			this.position = targetPosition;
			this.lastStepPosition = this.position;
			if (this.sand != null)
			{
				this.sand.transform.position = this.position - this.mechSpider.transform.up * this.footHeight * this.mechSpider.scale;
				this.sand.Emit(20);
			}
			this.lastStepTime = Time.time;
			yield break;
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0005617C File Offset: 0x0005437C
		public MechSpiderLeg()
		{
		}

		// Token: 0x04000A18 RID: 2584
		public MechSpider mechSpider;

		// Token: 0x04000A19 RID: 2585
		public MechSpiderLeg unSync;

		// Token: 0x04000A1A RID: 2586
		public Vector3 offset;

		// Token: 0x04000A1B RID: 2587
		public float minDelay = 0.2f;

		// Token: 0x04000A1C RID: 2588
		public float maxOffset = 1f;

		// Token: 0x04000A1D RID: 2589
		public float stepSpeed = 5f;

		// Token: 0x04000A1E RID: 2590
		public float footHeight = 0.15f;

		// Token: 0x04000A1F RID: 2591
		public float velocityPrediction = 0.2f;

		// Token: 0x04000A20 RID: 2592
		public float raycastFocus = 0.1f;

		// Token: 0x04000A21 RID: 2593
		public AnimationCurve yOffset;

		// Token: 0x04000A22 RID: 2594
		public Transform foot;

		// Token: 0x04000A23 RID: 2595
		public Vector3 footUpAxis;

		// Token: 0x04000A24 RID: 2596
		public float footRotationSpeed = 10f;

		// Token: 0x04000A25 RID: 2597
		public ParticleSystem sand;

		// Token: 0x04000A26 RID: 2598
		private IK ik;

		// Token: 0x04000A27 RID: 2599
		private float stepProgress = 1f;

		// Token: 0x04000A28 RID: 2600
		private float lastStepTime;

		// Token: 0x04000A29 RID: 2601
		private Vector3 defaultPosition;

		// Token: 0x04000A2A RID: 2602
		private RaycastHit hit;

		// Token: 0x04000A2B RID: 2603
		private Quaternion lastFootLocalRotation;

		// Token: 0x04000A2C RID: 2604
		private Vector3 smoothHitNormal = Vector3.up;

		// Token: 0x04000A2D RID: 2605
		private Vector3 lastStepPosition;

		// Token: 0x0200022C RID: 556
		[CompilerGenerated]
		private sealed class <Step>d__33 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x0600119C RID: 4508 RVA: 0x0006D60D File Offset: 0x0006B80D
			[DebuggerHidden]
			public <Step>d__33(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600119D RID: 4509 RVA: 0x0006D61C File Offset: 0x0006B81C
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600119E RID: 4510 RVA: 0x0006D620 File Offset: 0x0006B820
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				MechSpiderLeg mechSpiderLeg = this;
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
					mechSpiderLeg.stepProgress = 0f;
				}
				if (mechSpiderLeg.stepProgress >= 1f)
				{
					mechSpiderLeg.position = targetPosition;
					mechSpiderLeg.lastStepPosition = mechSpiderLeg.position;
					if (mechSpiderLeg.sand != null)
					{
						mechSpiderLeg.sand.transform.position = mechSpiderLeg.position - mechSpiderLeg.mechSpider.transform.up * mechSpiderLeg.footHeight * mechSpiderLeg.mechSpider.scale;
						mechSpiderLeg.sand.Emit(20);
					}
					mechSpiderLeg.lastStepTime = Time.time;
					return false;
				}
				mechSpiderLeg.stepProgress += Time.deltaTime * mechSpiderLeg.stepSpeed;
				mechSpiderLeg.position = Vector3.Lerp(stepStartPosition, targetPosition, mechSpiderLeg.stepProgress);
				mechSpiderLeg.position += mechSpiderLeg.mechSpider.transform.up * mechSpiderLeg.yOffset.Evaluate(mechSpiderLeg.stepProgress) * mechSpiderLeg.mechSpider.scale;
				mechSpiderLeg.lastStepPosition = mechSpiderLeg.position;
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x1700025A RID: 602
			// (get) Token: 0x0600119F RID: 4511 RVA: 0x0006D793 File Offset: 0x0006B993
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060011A0 RID: 4512 RVA: 0x0006D79B File Offset: 0x0006B99B
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700025B RID: 603
			// (get) Token: 0x060011A1 RID: 4513 RVA: 0x0006D7A2 File Offset: 0x0006B9A2
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04001074 RID: 4212
			private int <>1__state;

			// Token: 0x04001075 RID: 4213
			private object <>2__current;

			// Token: 0x04001076 RID: 4214
			public MechSpiderLeg <>4__this;

			// Token: 0x04001077 RID: 4215
			public Vector3 stepStartPosition;

			// Token: 0x04001078 RID: 4216
			public Vector3 targetPosition;
		}
	}
}
