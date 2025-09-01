using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fluxy
{
	// Token: 0x0200000D RID: 13
	public class FluXYSolverFinder : MonoBehaviour
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00004E45 File Offset: 0x00003045
		private void Awake()
		{
			FluXYSolverFinder.instance = this;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004E50 File Offset: 0x00003050
		public static FluxySolver GetSolver(FluXYGroup group)
		{
			if (FluXYSolverFinder.instance == null)
			{
				return null;
			}
			foreach (FluXYSolverFinder.SolverGroup solverGroup in FluXYSolverFinder.instance.Groups)
			{
				if (solverGroup.Group == group)
				{
					return solverGroup.Solver;
				}
			}
			return null;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004EC4 File Offset: 0x000030C4
		private void OnDestroy()
		{
			FluXYSolverFinder.instance = null;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004ECC File Offset: 0x000030CC
		public FluXYSolverFinder()
		{
		}

		// Token: 0x0400006B RID: 107
		private static FluXYSolverFinder instance;

		// Token: 0x0400006C RID: 108
		public List<FluXYSolverFinder.SolverGroup> Groups;

		// Token: 0x0200002A RID: 42
		[Serializable]
		public class SolverGroup
		{
			// Token: 0x060000B2 RID: 178 RVA: 0x00006ED0 File Offset: 0x000050D0
			public SolverGroup()
			{
			}

			// Token: 0x040000EC RID: 236
			public FluXYGroup Group;

			// Token: 0x040000ED RID: 237
			public FluxySolver Solver;
		}
	}
}
