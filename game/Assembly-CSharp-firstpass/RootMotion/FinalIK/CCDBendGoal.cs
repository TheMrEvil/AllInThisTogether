using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000113 RID: 275
	public class CCDBendGoal : MonoBehaviour
	{
		// Token: 0x06000C28 RID: 3112 RVA: 0x000517ED File Offset: 0x0004F9ED
		private void Start()
		{
			IKSolverCCD solver = this.ik.solver;
			solver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPreUpdate, new IKSolver.UpdateDelegate(this.BeforeIK));
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0005181C File Offset: 0x0004FA1C
		private void BeforeIK()
		{
			if (!base.enabled)
			{
				return;
			}
			float num = this.ik.solver.IKPositionWeight * this.weight;
			if (num <= 0f)
			{
				return;
			}
			Vector3 position = this.ik.solver.bones[0].transform.position;
			Quaternion quaternion = Quaternion.FromToRotation(this.ik.solver.bones[this.ik.solver.bones.Length - 1].transform.position - position, base.transform.position - position);
			if (num < 1f)
			{
				quaternion = Quaternion.Slerp(Quaternion.identity, quaternion, num);
			}
			this.ik.solver.bones[0].transform.rotation = quaternion * this.ik.solver.bones[0].transform.rotation;
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0005190F File Offset: 0x0004FB0F
		private void OnDestroy()
		{
			if (this.ik != null)
			{
				IKSolverCCD solver = this.ik.solver;
				solver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPreUpdate, new IKSolver.UpdateDelegate(this.BeforeIK));
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0005194B File Offset: 0x0004FB4B
		public CCDBendGoal()
		{
		}

		// Token: 0x04000982 RID: 2434
		public CCDIK ik;

		// Token: 0x04000983 RID: 2435
		[Range(0f, 1f)]
		public float weight = 1f;
	}
}
