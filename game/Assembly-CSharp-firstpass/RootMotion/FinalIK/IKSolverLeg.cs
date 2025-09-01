using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000F9 RID: 249
	[Serializable]
	public class IKSolverLeg : IKSolver
	{
		// Token: 0x06000AC9 RID: 2761 RVA: 0x00047FB8 File Offset: 0x000461B8
		public override bool IsValid(ref string message)
		{
			if (this.pelvis.transform == null || this.thigh.transform == null || this.calf.transform == null || this.foot.transform == null || this.toe.transform == null)
			{
				message = "Please assign all bone slots of the Leg IK solver.";
				return false;
			}
			UnityEngine.Object[] objects = new Transform[]
			{
				this.pelvis.transform,
				this.thigh.transform,
				this.calf.transform,
				this.foot.transform,
				this.toe.transform
			};
			Transform transform = (Transform)Hierarchy.ContainsDuplicate(objects);
			if (transform != null)
			{
				message = transform.name + " is represented multiple times in the LegIK.";
				return false;
			}
			return true;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000480A4 File Offset: 0x000462A4
		public bool SetChain(Transform pelvis, Transform thigh, Transform calf, Transform foot, Transform toe, Transform root)
		{
			this.pelvis.transform = pelvis;
			this.thigh.transform = thigh;
			this.calf.transform = calf;
			this.foot.transform = foot;
			this.toe.transform = toe;
			base.Initiate(root);
			return base.initiated;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000480FD File Offset: 0x000462FD
		public override IKSolver.Point[] GetPoints()
		{
			return new IKSolver.Point[]
			{
				this.pelvis,
				this.thigh,
				this.calf,
				this.foot,
				this.toe
			};
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00048134 File Offset: 0x00046334
		public override IKSolver.Point GetPoint(Transform transform)
		{
			if (this.pelvis.transform == transform)
			{
				return this.pelvis;
			}
			if (this.thigh.transform == transform)
			{
				return this.thigh;
			}
			if (this.calf.transform == transform)
			{
				return this.calf;
			}
			if (this.foot.transform == transform)
			{
				return this.foot;
			}
			if (this.toe.transform == transform)
			{
				return this.toe;
			}
			return null;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x000481C4 File Offset: 0x000463C4
		public override void StoreDefaultLocalState()
		{
			this.thigh.StoreDefaultLocalState();
			this.calf.StoreDefaultLocalState();
			this.foot.StoreDefaultLocalState();
			this.toe.StoreDefaultLocalState();
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000481F2 File Offset: 0x000463F2
		public override void FixTransforms()
		{
			if (!base.initiated)
			{
				return;
			}
			this.thigh.FixTransform();
			this.calf.FixTransform();
			this.foot.FixTransform();
			this.toe.FixTransform();
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00048229 File Offset: 0x00046429
		protected override void OnInitiate()
		{
			this.IKPosition = this.toe.transform.position;
			this.IKRotation = this.toe.transform.rotation;
			this.Read();
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0004825D File Offset: 0x0004645D
		protected override void OnUpdate()
		{
			this.Read();
			this.Solve();
			this.Write();
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00048274 File Offset: 0x00046474
		private void Solve()
		{
			this.leg.heelPositionOffset += this.heelOffset;
			this.leg.PreSolve();
			this.leg.ApplyOffsets(1f);
			this.leg.Solve(true);
			this.leg.ResetOffsets();
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x000482D0 File Offset: 0x000464D0
		private void Read()
		{
			this.leg.IKPosition = this.IKPosition;
			this.leg.positionWeight = this.IKPositionWeight;
			this.leg.IKRotation = this.IKRotation;
			this.leg.rotationWeight = this.IKRotationWeight;
			this.positions[0] = this.root.position;
			this.positions[1] = this.pelvis.transform.position;
			this.positions[2] = this.thigh.transform.position;
			this.positions[3] = this.calf.transform.position;
			this.positions[4] = this.foot.transform.position;
			this.positions[5] = this.toe.transform.position;
			this.rotations[0] = this.root.rotation;
			this.rotations[1] = this.pelvis.transform.rotation;
			this.rotations[2] = this.thigh.transform.rotation;
			this.rotations[3] = this.calf.transform.rotation;
			this.rotations[4] = this.foot.transform.rotation;
			this.rotations[5] = this.toe.transform.rotation;
			this.leg.Read(this.positions, this.rotations, false, false, false, true, true, 1, 2);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00048488 File Offset: 0x00046688
		private void Write()
		{
			this.leg.Write(ref this.positions, ref this.rotations);
			this.thigh.transform.rotation = this.rotations[2];
			this.calf.transform.rotation = this.rotations[3];
			this.foot.transform.rotation = this.rotations[4];
			this.toe.transform.rotation = this.rotations[5];
			this.calf.transform.position = this.positions[3];
			this.foot.transform.position = this.positions[4];
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00048554 File Offset: 0x00046754
		public IKSolverLeg()
		{
		}

		// Token: 0x04000869 RID: 2153
		[Range(0f, 1f)]
		public float IKRotationWeight = 1f;

		// Token: 0x0400086A RID: 2154
		public Quaternion IKRotation = Quaternion.identity;

		// Token: 0x0400086B RID: 2155
		public IKSolver.Point pelvis = new IKSolver.Point();

		// Token: 0x0400086C RID: 2156
		public IKSolver.Point thigh = new IKSolver.Point();

		// Token: 0x0400086D RID: 2157
		public IKSolver.Point calf = new IKSolver.Point();

		// Token: 0x0400086E RID: 2158
		public IKSolver.Point foot = new IKSolver.Point();

		// Token: 0x0400086F RID: 2159
		public IKSolver.Point toe = new IKSolver.Point();

		// Token: 0x04000870 RID: 2160
		public IKSolverVR.Leg leg = new IKSolverVR.Leg();

		// Token: 0x04000871 RID: 2161
		public Vector3 heelOffset;

		// Token: 0x04000872 RID: 2162
		private Vector3[] positions = new Vector3[6];

		// Token: 0x04000873 RID: 2163
		private Quaternion[] rotations = new Quaternion[6];
	}
}
