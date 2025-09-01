using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000D5 RID: 213
	[HelpURL("https://www.youtube.com/watch?v=9MiZiaJorws&index=6&list=PLVxSIA1OaTOu8Nos3CalXbJ2DrKnntMv6")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/Grounder/Grounder VRIK")]
	public class GrounderVRIK : Grounder
	{
		// Token: 0x06000936 RID: 2358 RVA: 0x0003DBC6 File Offset: 0x0003BDC6
		[ContextMenu("TUTORIAL VIDEO")]
		private void OpenTutorial()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=9MiZiaJorws&index=6&list=PLVxSIA1OaTOu8Nos3CalXbJ2DrKnntMv6");
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0003DBD2 File Offset: 0x0003BDD2
		[ContextMenu("User Manual")]
		protected override void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page9.html");
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0003DBDE File Offset: 0x0003BDDE
		[ContextMenu("Scrpt Reference")]
		protected override void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_grounder_v_r_i_k.html");
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0003DBEA File Offset: 0x0003BDEA
		public override void ResetPosition()
		{
			this.solver.Reset();
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0003DBF7 File Offset: 0x0003BDF7
		private bool IsReadyToInitiate()
		{
			return !(this.ik == null) && this.ik.solver.initiated;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0003DC20 File Offset: 0x0003BE20
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

		// Token: 0x0600093C RID: 2364 RVA: 0x0003DC70 File Offset: 0x0003BE70
		private void Initiate()
		{
			this.feet = new Transform[2];
			this.feet[0] = this.ik.references.leftFoot;
			this.feet[1] = this.ik.references.rightFoot;
			IKSolverVR solver = this.ik.solver;
			solver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPreUpdate, new IKSolver.UpdateDelegate(this.OnSolverUpdate));
			IKSolverVR solver2 = this.ik.solver;
			solver2.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver2.OnPostUpdate, new IKSolver.UpdateDelegate(this.OnPostSolverUpdate));
			this.solver.Initiate(this.ik.references.root, this.feet);
			base.initiated = true;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0003DD3C File Offset: 0x0003BF3C
		private void OnSolverUpdate()
		{
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
			this.ik.solver.AddPositionOffset(IKSolverVR.PositionOffset.LeftFoot, (this.solver.legs[0].IKPosition - this.ik.references.leftFoot.position) * this.weight);
			this.ik.solver.AddPositionOffset(IKSolverVR.PositionOffset.RightFoot, (this.solver.legs[1].IKPosition - this.ik.references.rightFoot.position) * this.weight);
			if (this.OnPostGrounder != null)
			{
				this.OnPostGrounder();
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0003DE5C File Offset: 0x0003C05C
		private void SetLegIK(IKSolverVR.PositionOffset positionOffset, Transform bone, Grounding.Leg leg)
		{
			this.ik.solver.AddPositionOffset(positionOffset, (leg.IKPosition - bone.position) * this.weight);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0003DE8C File Offset: 0x0003C08C
		private void OnPostSolverUpdate()
		{
			this.ik.references.leftFoot.rotation = Quaternion.Slerp(Quaternion.identity, this.solver.legs[0].rotationOffset, this.weight) * this.ik.references.leftFoot.rotation;
			this.ik.references.rightFoot.rotation = Quaternion.Slerp(Quaternion.identity, this.solver.legs[1].rotationOffset, this.weight) * this.ik.references.rightFoot.rotation;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0003DF3C File Offset: 0x0003C13C
		private void OnDrawGizmosSelected()
		{
			if (this.ik == null)
			{
				this.ik = base.GetComponent<VRIK>();
			}
			if (this.ik == null)
			{
				this.ik = base.GetComponentInParent<VRIK>();
			}
			if (this.ik == null)
			{
				this.ik = base.GetComponentInChildren<VRIK>();
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0003DF98 File Offset: 0x0003C198
		private void OnDestroy()
		{
			if (base.initiated && this.ik != null)
			{
				IKSolverVR solver = this.ik.solver;
				solver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPreUpdate, new IKSolver.UpdateDelegate(this.OnSolverUpdate));
				IKSolverVR solver2 = this.ik.solver;
				solver2.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver2.OnPostUpdate, new IKSolver.UpdateDelegate(this.OnPostSolverUpdate));
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0003E013 File Offset: 0x0003C213
		public GrounderVRIK()
		{
		}

		// Token: 0x04000748 RID: 1864
		[Tooltip("Reference to the VRIK componet.")]
		public VRIK ik;

		// Token: 0x04000749 RID: 1865
		private Transform[] feet = new Transform[2];
	}
}
