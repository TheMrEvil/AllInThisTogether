using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000D1 RID: 209
	[HelpURL("http://www.root-motion.com/finalikdox/html/page9.html")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/Grounder/Grounder Biped")]
	public class GrounderBipedIK : Grounder
	{
		// Token: 0x060008FE RID: 2302 RVA: 0x0003BC97 File Offset: 0x00039E97
		[ContextMenu("User Manual")]
		protected override void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page9.html");
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0003BCA3 File Offset: 0x00039EA3
		[ContextMenu("Scrpt Reference")]
		protected override void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_grounder_biped_i_k.html");
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0003BCAF File Offset: 0x00039EAF
		public override void ResetPosition()
		{
			this.solver.Reset();
			this.spineOffset = Vector3.zero;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0003BCC8 File Offset: 0x00039EC8
		private bool IsReadyToInitiate()
		{
			return !(this.ik == null) && this.ik.solvers.leftFoot.initiated && this.ik.solvers.rightFoot.initiated;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0003BD18 File Offset: 0x00039F18
		private void Update()
		{
			this.weight = Mathf.Clamp(this.weight, 0f, 1f);
			if (this.weight <= 0f)
			{
				return;
			}
			if (base.initiated)
			{
				return;
			}
			if (!this.IsReadyToInitiate())
			{
				return;
			}
			this.Initiate();
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0003BD68 File Offset: 0x00039F68
		private void Initiate()
		{
			this.feet = new Transform[2];
			this.footRotations = new Quaternion[2];
			this.feet[0] = this.ik.references.leftFoot;
			this.feet[1] = this.ik.references.rightFoot;
			this.footRotations[0] = Quaternion.identity;
			this.footRotations[1] = Quaternion.identity;
			IKSolverFABRIK spine = this.ik.solvers.spine;
			spine.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(spine.OnPreUpdate, new IKSolver.UpdateDelegate(this.OnSolverUpdate));
			IKSolverLimb rightFoot = this.ik.solvers.rightFoot;
			rightFoot.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(rightFoot.OnPostUpdate, new IKSolver.UpdateDelegate(this.OnPostSolverUpdate));
			this.animatedPelvisLocalPosition = this.ik.references.pelvis.localPosition;
			this.solver.Initiate(this.ik.references.root, this.feet);
			base.initiated = true;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0003BE84 File Offset: 0x0003A084
		private void OnDisable()
		{
			if (!base.initiated)
			{
				return;
			}
			this.ik.solvers.leftFoot.IKPositionWeight = 0f;
			this.ik.solvers.rightFoot.IKPositionWeight = 0f;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0003BEC4 File Offset: 0x0003A0C4
		private void OnSolverUpdate()
		{
			if (!base.enabled)
			{
				return;
			}
			if (this.weight <= 0f)
			{
				if (this.lastWeight <= 0f)
				{
					return;
				}
				this.OnDisable();
			}
			this.lastWeight = this.weight;
			if (this.OnPreGrounder != null)
			{
				this.OnPreGrounder();
			}
			if (this.ik.references.pelvis.localPosition != this.solvedPelvisLocalPosition)
			{
				this.animatedPelvisLocalPosition = this.ik.references.pelvis.localPosition;
			}
			else
			{
				this.ik.references.pelvis.localPosition = this.animatedPelvisLocalPosition;
			}
			this.solver.Update();
			this.ik.references.pelvis.position += this.solver.pelvis.IKOffset * this.weight;
			this.SetLegIK(this.ik.solvers.leftFoot, 0);
			this.SetLegIK(this.ik.solvers.rightFoot, 1);
			if (this.spineBend != 0f && this.ik.references.spine.Length != 0)
			{
				this.spineSpeed = Mathf.Clamp(this.spineSpeed, 0f, this.spineSpeed);
				Vector3 a = base.GetSpineOffsetTarget() * this.weight;
				this.spineOffset = Vector3.Lerp(this.spineOffset, a * this.spineBend, Time.deltaTime * this.spineSpeed);
				Quaternion rotation = this.ik.references.leftUpperArm.rotation;
				Quaternion rotation2 = this.ik.references.rightUpperArm.rotation;
				Vector3 up = this.solver.up;
				Quaternion lhs = Quaternion.FromToRotation(up, up + this.spineOffset);
				this.ik.references.spine[0].rotation = lhs * this.ik.references.spine[0].rotation;
				this.ik.references.leftUpperArm.rotation = rotation;
				this.ik.references.rightUpperArm.rotation = rotation2;
				this.ik.solvers.lookAt.SetDirty();
			}
			if (this.OnPostGrounder != null)
			{
				this.OnPostGrounder();
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0003C136 File Offset: 0x0003A336
		private void SetLegIK(IKSolverLimb limb, int index)
		{
			this.footRotations[index] = this.feet[index].rotation;
			limb.IKPosition = this.solver.legs[index].IKPosition;
			limb.IKPositionWeight = this.weight;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0003C178 File Offset: 0x0003A378
		private void OnPostSolverUpdate()
		{
			if (this.weight <= 0f)
			{
				return;
			}
			if (!base.enabled)
			{
				return;
			}
			for (int i = 0; i < this.feet.Length; i++)
			{
				this.feet[i].rotation = Quaternion.Slerp(Quaternion.identity, this.solver.legs[i].rotationOffset, this.weight) * this.footRotations[i];
			}
			this.solvedPelvisLocalPosition = this.ik.references.pelvis.localPosition;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0003C20C File Offset: 0x0003A40C
		private void OnDestroy()
		{
			if (base.initiated && this.ik != null)
			{
				IKSolverFABRIK spine = this.ik.solvers.spine;
				spine.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(spine.OnPreUpdate, new IKSolver.UpdateDelegate(this.OnSolverUpdate));
				IKSolverLimb rightFoot = this.ik.solvers.rightFoot;
				rightFoot.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(rightFoot.OnPostUpdate, new IKSolver.UpdateDelegate(this.OnPostSolverUpdate));
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0003C291 File Offset: 0x0003A491
		public GrounderBipedIK()
		{
		}

		// Token: 0x0400070D RID: 1805
		[Tooltip("The BipedIK componet.")]
		public BipedIK ik;

		// Token: 0x0400070E RID: 1806
		[Tooltip("The amount of spine bending towards upward slopes.")]
		public float spineBend = 7f;

		// Token: 0x0400070F RID: 1807
		[Tooltip("The interpolation speed of spine bending.")]
		public float spineSpeed = 3f;

		// Token: 0x04000710 RID: 1808
		private Transform[] feet = new Transform[2];

		// Token: 0x04000711 RID: 1809
		private Quaternion[] footRotations = new Quaternion[2];

		// Token: 0x04000712 RID: 1810
		private Vector3 animatedPelvisLocalPosition;

		// Token: 0x04000713 RID: 1811
		private Vector3 solvedPelvisLocalPosition;

		// Token: 0x04000714 RID: 1812
		private Vector3 spineOffset;

		// Token: 0x04000715 RID: 1813
		private float lastWeight;
	}
}
