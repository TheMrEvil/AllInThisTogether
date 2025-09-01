using System;

namespace RootMotion.FinalIK
{
	// Token: 0x020000DD RID: 221
	public abstract class IK : SolverManager
	{
		// Token: 0x06000987 RID: 2439
		public abstract IKSolver GetIKSolver();

		// Token: 0x06000988 RID: 2440 RVA: 0x0003EB41 File Offset: 0x0003CD41
		protected override void UpdateSolver()
		{
			if (!this.GetIKSolver().initiated)
			{
				this.InitiateSolver();
			}
			if (!this.GetIKSolver().initiated)
			{
				return;
			}
			this.GetIKSolver().Update();
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0003EB6F File Offset: 0x0003CD6F
		protected override void InitiateSolver()
		{
			if (this.GetIKSolver().initiated)
			{
				return;
			}
			this.GetIKSolver().Initiate(base.transform);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0003EB90 File Offset: 0x0003CD90
		protected override void FixTransforms()
		{
			if (!this.GetIKSolver().initiated)
			{
				return;
			}
			this.GetIKSolver().FixTransforms();
		}

		// Token: 0x0600098B RID: 2443
		protected abstract void OpenUserManual();

		// Token: 0x0600098C RID: 2444
		protected abstract void OpenScriptReference();

		// Token: 0x0600098D RID: 2445 RVA: 0x0003EBAB File Offset: 0x0003CDAB
		protected IK()
		{
		}
	}
}
