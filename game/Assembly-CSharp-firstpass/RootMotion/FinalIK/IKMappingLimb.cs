using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000EC RID: 236
	[Serializable]
	public class IKMappingLimb : IKMapping
	{
		// Token: 0x06000A0A RID: 2570 RVA: 0x00042828 File Offset: 0x00040A28
		public override bool IsValid(IKSolver solver, ref string message)
		{
			return base.IsValid(solver, ref message) && base.BoneIsValid(this.bone1, solver, ref message, null) && base.BoneIsValid(this.bone2, solver, ref message, null) && base.BoneIsValid(this.bone3, solver, ref message, null);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0004287C File Offset: 0x00040A7C
		public IKMapping.BoneMap GetBoneMap(IKMappingLimb.BoneMapType boneMap)
		{
			switch (boneMap)
			{
			case IKMappingLimb.BoneMapType.Parent:
				if (this.parentBone == null)
				{
					Warning.Log("This limb does not have a parent (shoulder) bone", this.bone1, false);
				}
				return this.boneMapParent;
			case IKMappingLimb.BoneMapType.Bone1:
				return this.boneMap1;
			case IKMappingLimb.BoneMapType.Bone2:
				return this.boneMap2;
			default:
				return this.boneMap3;
			}
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x000428D8 File Offset: 0x00040AD8
		public void SetLimbOrientation(Vector3 upper, Vector3 lower)
		{
			this.boneMap1.defaultLocalTargetRotation = Quaternion.Inverse(Quaternion.Inverse(this.bone1.rotation) * Quaternion.LookRotation(this.bone2.position - this.bone1.position, this.bone1.rotation * -upper));
			this.boneMap2.defaultLocalTargetRotation = Quaternion.Inverse(Quaternion.Inverse(this.bone2.rotation) * Quaternion.LookRotation(this.bone3.position - this.bone2.position, this.bone2.rotation * -lower));
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0004299C File Offset: 0x00040B9C
		public IKMappingLimb()
		{
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x000429F0 File Offset: 0x00040BF0
		public IKMappingLimb(Transform bone1, Transform bone2, Transform bone3, Transform parentBone = null)
		{
			this.SetBones(bone1, bone2, bone3, parentBone);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00042A4C File Offset: 0x00040C4C
		public void SetBones(Transform bone1, Transform bone2, Transform bone3, Transform parentBone = null)
		{
			this.bone1 = bone1;
			this.bone2 = bone2;
			this.bone3 = bone3;
			this.parentBone = parentBone;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00042A6B File Offset: 0x00040C6B
		public void StoreDefaultLocalState()
		{
			if (this.parentBone != null)
			{
				this.boneMapParent.StoreDefaultLocalState();
			}
			this.boneMap1.StoreDefaultLocalState();
			this.boneMap2.StoreDefaultLocalState();
			this.boneMap3.StoreDefaultLocalState();
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00042AA7 File Offset: 0x00040CA7
		public void FixTransforms()
		{
			if (this.parentBone != null)
			{
				this.boneMapParent.FixTransform(false);
			}
			this.boneMap1.FixTransform(true);
			this.boneMap2.FixTransform(false);
			this.boneMap3.FixTransform(false);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00042AE8 File Offset: 0x00040CE8
		public override void Initiate(IKSolverFullBody solver)
		{
			if (this.boneMapParent == null)
			{
				this.boneMapParent = new IKMapping.BoneMap();
			}
			if (this.boneMap1 == null)
			{
				this.boneMap1 = new IKMapping.BoneMap();
			}
			if (this.boneMap2 == null)
			{
				this.boneMap2 = new IKMapping.BoneMap();
			}
			if (this.boneMap3 == null)
			{
				this.boneMap3 = new IKMapping.BoneMap();
			}
			if (this.parentBone != null)
			{
				this.boneMapParent.Initiate(this.parentBone, solver);
			}
			this.boneMap1.Initiate(this.bone1, solver);
			this.boneMap2.Initiate(this.bone2, solver);
			this.boneMap3.Initiate(this.bone3, solver);
			this.boneMap1.SetPlane(solver, this.boneMap1.transform, this.boneMap2.transform, this.boneMap3.transform);
			this.boneMap2.SetPlane(solver, this.boneMap2.transform, this.boneMap3.transform, this.boneMap1.transform);
			if (this.parentBone != null)
			{
				this.boneMapParent.SetLocalSwingAxis(this.boneMap1);
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00042C10 File Offset: 0x00040E10
		public void ReadPose()
		{
			this.boneMap1.UpdatePlane(this.updatePlaneRotations, true);
			this.boneMap2.UpdatePlane(this.updatePlaneRotations, false);
			this.weight = Mathf.Clamp(this.weight, 0f, 1f);
			this.boneMap3.MaintainRotation();
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00042C68 File Offset: 0x00040E68
		public void WritePose(IKSolverFullBody solver, bool fullBody)
		{
			if (this.weight <= 0f)
			{
				return;
			}
			if (fullBody)
			{
				if (this.parentBone != null)
				{
					this.boneMapParent.Swing(solver.GetNode(this.boneMap1.chainIndex, this.boneMap1.nodeIndex).solverPosition, this.weight);
				}
				this.boneMap1.FixToNode(solver, this.weight, null);
			}
			this.boneMap1.RotateToPlane(solver, this.weight);
			this.boneMap2.RotateToPlane(solver, this.weight);
			this.boneMap3.RotateToMaintain(this.maintainRotationWeight * this.weight * solver.IKPositionWeight);
			this.boneMap3.RotateToEffector(solver, this.weight);
		}

		// Token: 0x040007F8 RID: 2040
		public Transform parentBone;

		// Token: 0x040007F9 RID: 2041
		public Transform bone1;

		// Token: 0x040007FA RID: 2042
		public Transform bone2;

		// Token: 0x040007FB RID: 2043
		public Transform bone3;

		// Token: 0x040007FC RID: 2044
		[Range(0f, 1f)]
		public float maintainRotationWeight;

		// Token: 0x040007FD RID: 2045
		[Range(0f, 1f)]
		public float weight = 1f;

		// Token: 0x040007FE RID: 2046
		[NonSerialized]
		public bool updatePlaneRotations = true;

		// Token: 0x040007FF RID: 2047
		private IKMapping.BoneMap boneMapParent = new IKMapping.BoneMap();

		// Token: 0x04000800 RID: 2048
		private IKMapping.BoneMap boneMap1 = new IKMapping.BoneMap();

		// Token: 0x04000801 RID: 2049
		private IKMapping.BoneMap boneMap2 = new IKMapping.BoneMap();

		// Token: 0x04000802 RID: 2050
		private IKMapping.BoneMap boneMap3 = new IKMapping.BoneMap();

		// Token: 0x020001F3 RID: 499
		[Serializable]
		public enum BoneMapType
		{
			// Token: 0x04000ED4 RID: 3796
			Parent,
			// Token: 0x04000ED5 RID: 3797
			Bone1,
			// Token: 0x04000ED6 RID: 3798
			Bone2,
			// Token: 0x04000ED7 RID: 3799
			Bone3
		}
	}
}
