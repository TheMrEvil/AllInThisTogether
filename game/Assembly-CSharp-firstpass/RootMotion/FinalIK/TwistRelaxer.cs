using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000FE RID: 254
	public class TwistRelaxer : MonoBehaviour
	{
		// Token: 0x06000B2D RID: 2861 RVA: 0x0004C040 File Offset: 0x0004A240
		public void Start()
		{
			if (this.twistSolvers.Length == 0)
			{
				Debug.LogError("TwistRelaxer has no TwistSolvers. TwistRelaxer.cs was restructured for FIK v2.0 to support multiple relaxers on the same body part and TwistRelaxer components need to be set up again, sorry for the inconvenience!", base.transform);
				return;
			}
			TwistSolver[] array = this.twistSolvers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Initiate();
			}
			if (this.ik != null)
			{
				IKSolver iksolver = this.ik.GetIKSolver();
				iksolver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(iksolver.OnPostUpdate, new IKSolver.UpdateDelegate(this.OnPostUpdate));
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0004C0C0 File Offset: 0x0004A2C0
		private void OnPostUpdate()
		{
			if (this.ik != null)
			{
				TwistSolver[] array = this.twistSolvers;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Relax();
				}
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0004C0F8 File Offset: 0x0004A2F8
		private void LateUpdate()
		{
			if (this.ik == null)
			{
				TwistSolver[] array = this.twistSolvers;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Relax();
				}
			}
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0004C130 File Offset: 0x0004A330
		private void OnDestroy()
		{
			if (this.ik != null)
			{
				IKSolver iksolver = this.ik.GetIKSolver();
				iksolver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(iksolver.OnPostUpdate, new IKSolver.UpdateDelegate(this.OnPostUpdate));
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0004C16C File Offset: 0x0004A36C
		public TwistRelaxer()
		{
		}

		// Token: 0x040008C3 RID: 2243
		public IK ik;

		// Token: 0x040008C4 RID: 2244
		[Tooltip("If using multiple solvers, add them in inverse hierarchical order - first forearm roll bone, then forearm bone and upper arm bone.")]
		public TwistSolver[] twistSolvers = new TwistSolver[0];
	}
}
