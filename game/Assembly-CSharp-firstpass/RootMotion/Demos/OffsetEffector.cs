using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000143 RID: 323
	public class OffsetEffector : OffsetModifier
	{
		// Token: 0x06000D0F RID: 3343 RVA: 0x00058A9C File Offset: 0x00056C9C
		protected override void Start()
		{
			base.Start();
			foreach (OffsetEffector.EffectorLink effectorLink in this.effectorLinks)
			{
				effectorLink.localPosition = base.transform.InverseTransformPoint(this.ik.solver.GetEffector(effectorLink.effectorType).bone.position);
				if (effectorLink.effectorType == FullBodyBipedEffector.Body)
				{
					this.ik.solver.bodyEffector.effectChildNodes = false;
				}
			}
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00058B18 File Offset: 0x00056D18
		protected override void OnModifyOffset()
		{
			foreach (OffsetEffector.EffectorLink effectorLink in this.effectorLinks)
			{
				Vector3 a = base.transform.TransformPoint(effectorLink.localPosition);
				this.ik.solver.GetEffector(effectorLink.effectorType).positionOffset += (a - (this.ik.solver.GetEffector(effectorLink.effectorType).bone.position + this.ik.solver.GetEffector(effectorLink.effectorType).positionOffset)) * this.weight * effectorLink.weightMultiplier;
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00058BD8 File Offset: 0x00056DD8
		public OffsetEffector()
		{
		}

		// Token: 0x04000AB8 RID: 2744
		public OffsetEffector.EffectorLink[] effectorLinks;

		// Token: 0x02000233 RID: 563
		[Serializable]
		public class EffectorLink
		{
			// Token: 0x060011AE RID: 4526 RVA: 0x0006DD56 File Offset: 0x0006BF56
			public EffectorLink()
			{
			}

			// Token: 0x04001098 RID: 4248
			public FullBodyBipedEffector effectorType;

			// Token: 0x04001099 RID: 4249
			public float weightMultiplier = 1f;

			// Token: 0x0400109A RID: 4250
			[HideInInspector]
			public Vector3 localPosition;
		}
	}
}
