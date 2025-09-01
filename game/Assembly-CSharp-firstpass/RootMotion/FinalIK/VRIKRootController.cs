using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000122 RID: 290
	public class VRIKRootController : MonoBehaviour
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00054A10 File Offset: 0x00052C10
		// (set) Token: 0x06000C84 RID: 3204 RVA: 0x00054A18 File Offset: 0x00052C18
		public Vector3 pelvisTargetRight
		{
			[CompilerGenerated]
			get
			{
				return this.<pelvisTargetRight>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<pelvisTargetRight>k__BackingField = value;
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00054A21 File Offset: 0x00052C21
		private void Awake()
		{
			this.ik = base.GetComponent<VRIK>();
			IKSolverVR solver = this.ik.solver;
			solver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPreUpdate, new IKSolver.UpdateDelegate(this.OnPreUpdate));
			this.Calibrate();
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00054A64 File Offset: 0x00052C64
		public void Calibrate()
		{
			if (this.ik == null)
			{
				Debug.LogError("No VRIK found on VRIKRootController's GameObject.", base.transform);
				return;
			}
			this.pelvisTarget = this.ik.solver.spine.pelvisTarget;
			this.leftFootTarget = this.ik.solver.leftLeg.target;
			this.rightFootTarget = this.ik.solver.rightLeg.target;
			if (this.pelvisTarget != null)
			{
				this.pelvisTargetRight = Quaternion.Inverse(this.pelvisTarget.rotation) * this.ik.references.root.right;
			}
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00054B20 File Offset: 0x00052D20
		public void Calibrate(VRIKCalibrator.CalibrationData data)
		{
			if (this.ik == null)
			{
				Debug.LogError("No VRIK found on VRIKRootController's GameObject.", base.transform);
				return;
			}
			this.pelvisTarget = this.ik.solver.spine.pelvisTarget;
			this.leftFootTarget = this.ik.solver.leftLeg.target;
			this.rightFootTarget = this.ik.solver.rightLeg.target;
			if (this.pelvisTarget != null)
			{
				this.pelvisTargetRight = data.pelvisTargetRight;
			}
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00054BB8 File Offset: 0x00052DB8
		private void OnPreUpdate()
		{
			if (!base.enabled)
			{
				return;
			}
			if (this.pelvisTarget != null)
			{
				this.ik.references.root.position = new Vector3(this.pelvisTarget.position.x, this.ik.references.root.position.y, this.pelvisTarget.position.z);
				Vector3 forward = Vector3.Cross(this.pelvisTarget.rotation * this.pelvisTargetRight, this.ik.references.root.up);
				forward.y = 0f;
				this.ik.references.root.rotation = Quaternion.LookRotation(forward);
				this.ik.references.pelvis.position = Vector3.Lerp(this.ik.references.pelvis.position, this.pelvisTarget.position, this.ik.solver.spine.pelvisPositionWeight);
				this.ik.references.pelvis.rotation = Quaternion.Slerp(this.ik.references.pelvis.rotation, this.pelvisTarget.rotation, this.ik.solver.spine.pelvisRotationWeight);
				return;
			}
			if (this.leftFootTarget != null && this.rightFootTarget != null)
			{
				this.ik.references.root.position = Vector3.Lerp(this.leftFootTarget.position, this.rightFootTarget.position, 0.5f);
			}
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00054D7B File Offset: 0x00052F7B
		private void OnDestroy()
		{
			if (this.ik != null)
			{
				IKSolverVR solver = this.ik.solver;
				solver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPreUpdate, new IKSolver.UpdateDelegate(this.OnPreUpdate));
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00054DB7 File Offset: 0x00052FB7
		public VRIKRootController()
		{
		}

		// Token: 0x040009CC RID: 2508
		[CompilerGenerated]
		private Vector3 <pelvisTargetRight>k__BackingField;

		// Token: 0x040009CD RID: 2509
		private Transform pelvisTarget;

		// Token: 0x040009CE RID: 2510
		private Transform leftFootTarget;

		// Token: 0x040009CF RID: 2511
		private Transform rightFootTarget;

		// Token: 0x040009D0 RID: 2512
		private VRIK ik;
	}
}
