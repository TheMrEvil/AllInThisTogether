using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000C8 RID: 200
	[Serializable]
	public abstract class Constraint
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0003AE07 File Offset: 0x00039007
		public bool isValid
		{
			get
			{
				return this.transform != null;
			}
		}

		// Token: 0x060008C6 RID: 2246
		public abstract void UpdateConstraint();

		// Token: 0x060008C7 RID: 2247 RVA: 0x0003AE15 File Offset: 0x00039015
		protected Constraint()
		{
		}

		// Token: 0x040006DD RID: 1757
		public Transform transform;

		// Token: 0x040006DE RID: 1758
		public float weight;
	}
}
