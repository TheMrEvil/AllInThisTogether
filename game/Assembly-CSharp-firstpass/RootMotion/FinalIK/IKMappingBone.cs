using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000EB RID: 235
	[Serializable]
	public class IKMappingBone : IKMapping
	{
		// Token: 0x06000A02 RID: 2562 RVA: 0x00042758 File Offset: 0x00040958
		public override bool IsValid(IKSolver solver, ref string message)
		{
			if (!base.IsValid(solver, ref message))
			{
				return false;
			}
			if (this.bone == null)
			{
				message = "IKMappingBone's bone is null.";
				return false;
			}
			return true;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0004277E File Offset: 0x0004097E
		public IKMappingBone()
		{
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0004279C File Offset: 0x0004099C
		public IKMappingBone(Transform bone)
		{
			this.bone = bone;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000427C1 File Offset: 0x000409C1
		public void StoreDefaultLocalState()
		{
			this.boneMap.StoreDefaultLocalState();
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000427CE File Offset: 0x000409CE
		public void FixTransforms()
		{
			this.boneMap.FixTransform(false);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x000427DC File Offset: 0x000409DC
		public override void Initiate(IKSolverFullBody solver)
		{
			if (this.boneMap == null)
			{
				this.boneMap = new IKMapping.BoneMap();
			}
			this.boneMap.Initiate(this.bone, solver);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00042803 File Offset: 0x00040A03
		public void ReadPose()
		{
			this.boneMap.MaintainRotation();
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00042810 File Offset: 0x00040A10
		public void WritePose(float solverWeight)
		{
			this.boneMap.RotateToMaintain(solverWeight * this.maintainRotationWeight);
		}

		// Token: 0x040007F5 RID: 2037
		public Transform bone;

		// Token: 0x040007F6 RID: 2038
		[Range(0f, 1f)]
		public float maintainRotationWeight = 1f;

		// Token: 0x040007F7 RID: 2039
		private IKMapping.BoneMap boneMap = new IKMapping.BoneMap();
	}
}
