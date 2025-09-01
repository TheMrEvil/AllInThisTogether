using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000D2 RID: 210
	[HelpURL("https://www.youtube.com/watch?v=9MiZiaJorws&index=6&list=PLVxSIA1OaTOu8Nos3CalXbJ2DrKnntMv6")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/Grounder/Grounder Full Body Biped")]
	public class GrounderFBBIK : Grounder
	{
		// Token: 0x0600090A RID: 2314 RVA: 0x0003C2C7 File Offset: 0x0003A4C7
		[ContextMenu("TUTORIAL VIDEO")]
		private void OpenTutorial()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=9MiZiaJorws&index=6&list=PLVxSIA1OaTOu8Nos3CalXbJ2DrKnntMv6");
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0003C2D3 File Offset: 0x0003A4D3
		[ContextMenu("User Manual")]
		protected override void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page9.html");
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0003C2DF File Offset: 0x0003A4DF
		[ContextMenu("Scrpt Reference")]
		protected override void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_grounder_f_b_b_i_k.html");
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0003C2EB File Offset: 0x0003A4EB
		public override void ResetPosition()
		{
			this.solver.Reset();
			this.spineOffset = Vector3.zero;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0003C303 File Offset: 0x0003A503
		private bool IsReadyToInitiate()
		{
			return !(this.ik == null) && this.ik.solver.initiated;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0003C32C File Offset: 0x0003A52C
		private void Update()
		{
			this.firstSolve = true;
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

		// Token: 0x06000910 RID: 2320 RVA: 0x0003C381 File Offset: 0x0003A581
		private void FixedUpdate()
		{
			this.firstSolve = true;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0003C38A File Offset: 0x0003A58A
		private void LateUpdate()
		{
			this.firstSolve = true;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0003C394 File Offset: 0x0003A594
		private void Initiate()
		{
			this.ik.solver.leftLegMapping.maintainRotationWeight = 1f;
			this.ik.solver.rightLegMapping.maintainRotationWeight = 1f;
			this.feet = new Transform[2];
			this.feet[0] = this.ik.solver.leftFootEffector.bone;
			this.feet[1] = this.ik.solver.rightFootEffector.bone;
			IKSolverFullBodyBiped solver = this.ik.solver;
			solver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPreUpdate, new IKSolver.UpdateDelegate(this.OnSolverUpdate));
			this.solver.Initiate(this.ik.references.root, this.feet);
			base.initiated = true;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0003C470 File Offset: 0x0003A670
		private void OnSolverUpdate()
		{
			if (!this.firstSolve)
			{
				return;
			}
			this.firstSolve = false;
			if (!base.enabled)
			{
				return;
			}
			if (this.weight <= 0f)
			{
				return;
			}
			if (this.OnPreGrounder != null)
			{
				this.OnPreGrounder();
			}
			this.solver.Update();
			this.ik.references.pelvis.position += this.solver.pelvis.IKOffset * this.weight;
			this.SetLegIK(this.ik.solver.leftFootEffector, this.solver.legs[0]);
			this.SetLegIK(this.ik.solver.rightFootEffector, this.solver.legs[1]);
			if (this.spineBend != 0f)
			{
				this.spineSpeed = Mathf.Clamp(this.spineSpeed, 0f, this.spineSpeed);
				Vector3 a = base.GetSpineOffsetTarget() * this.weight;
				this.spineOffset = Vector3.Lerp(this.spineOffset, a * this.spineBend, Time.deltaTime * this.spineSpeed);
				Vector3 a2 = this.ik.references.root.up * this.spineOffset.magnitude;
				for (int i = 0; i < this.spine.Length; i++)
				{
					this.ik.solver.GetEffector(this.spine[i].effectorType).positionOffset += this.spineOffset * this.spine[i].horizontalWeight + a2 * this.spine[i].verticalWeight;
				}
			}
			if (this.OnPostGrounder != null)
			{
				this.OnPostGrounder();
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0003C654 File Offset: 0x0003A854
		private void SetLegIK(IKEffector effector, Grounding.Leg leg)
		{
			effector.positionOffset += (leg.IKPosition - effector.bone.position) * this.weight;
			effector.bone.rotation = Quaternion.Slerp(Quaternion.identity, leg.rotationOffset, this.weight) * effector.bone.rotation;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0003C6C4 File Offset: 0x0003A8C4
		private void OnDrawGizmosSelected()
		{
			if (this.ik == null)
			{
				this.ik = base.GetComponent<FullBodyBipedIK>();
			}
			if (this.ik == null)
			{
				this.ik = base.GetComponentInParent<FullBodyBipedIK>();
			}
			if (this.ik == null)
			{
				this.ik = base.GetComponentInChildren<FullBodyBipedIK>();
			}
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0003C720 File Offset: 0x0003A920
		private void OnDestroy()
		{
			if (base.initiated && this.ik != null)
			{
				IKSolverFullBodyBiped solver = this.ik.solver;
				solver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPreUpdate, new IKSolver.UpdateDelegate(this.OnSolverUpdate));
			}
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0003C76F File Offset: 0x0003A96F
		public GrounderFBBIK()
		{
		}

		// Token: 0x04000716 RID: 1814
		[Tooltip("Reference to the FBBIK componet.")]
		public FullBodyBipedIK ik;

		// Token: 0x04000717 RID: 1815
		[Tooltip("The amount of spine bending towards upward slopes.")]
		public float spineBend = 2f;

		// Token: 0x04000718 RID: 1816
		[Tooltip("The interpolation speed of spine bending.")]
		public float spineSpeed = 3f;

		// Token: 0x04000719 RID: 1817
		public GrounderFBBIK.SpineEffector[] spine = new GrounderFBBIK.SpineEffector[0];

		// Token: 0x0400071A RID: 1818
		private Transform[] feet = new Transform[2];

		// Token: 0x0400071B RID: 1819
		private Vector3 spineOffset;

		// Token: 0x0400071C RID: 1820
		private bool firstSolve;

		// Token: 0x020001E9 RID: 489
		[Serializable]
		public class SpineEffector
		{
			// Token: 0x06001011 RID: 4113 RVA: 0x000648C0 File Offset: 0x00062AC0
			public SpineEffector()
			{
			}

			// Token: 0x06001012 RID: 4114 RVA: 0x000648D3 File Offset: 0x00062AD3
			public SpineEffector(FullBodyBipedEffector effectorType, float horizontalWeight, float verticalWeight)
			{
				this.effectorType = effectorType;
				this.horizontalWeight = horizontalWeight;
				this.verticalWeight = verticalWeight;
			}

			// Token: 0x04000E6F RID: 3695
			[Tooltip("The type of the effector.")]
			public FullBodyBipedEffector effectorType;

			// Token: 0x04000E70 RID: 3696
			[Tooltip("The weight of horizontal bend offset towards the slope.")]
			public float horizontalWeight = 1f;

			// Token: 0x04000E71 RID: 3697
			[Tooltip("The vertical bend offset weight.")]
			public float verticalWeight;
		}
	}
}
