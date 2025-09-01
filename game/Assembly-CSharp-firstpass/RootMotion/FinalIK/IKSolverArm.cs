using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000F0 RID: 240
	[Serializable]
	public class IKSolverArm : IKSolver
	{
		// Token: 0x06000A43 RID: 2627 RVA: 0x000441B0 File Offset: 0x000423B0
		public override bool IsValid(ref string message)
		{
			if (this.chest.transform == null || this.shoulder.transform == null || this.upperArm.transform == null || this.forearm.transform == null || this.hand.transform == null)
			{
				message = "Please assign all bone slots of the Arm IK solver.";
				return false;
			}
			UnityEngine.Object[] objects = new Transform[]
			{
				this.chest.transform,
				this.shoulder.transform,
				this.upperArm.transform,
				this.forearm.transform,
				this.hand.transform
			};
			Transform transform = (Transform)Hierarchy.ContainsDuplicate(objects);
			if (transform != null)
			{
				message = transform.name + " is represented multiple times in the ArmIK.";
				return false;
			}
			return true;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0004429C File Offset: 0x0004249C
		public bool SetChain(Transform chest, Transform shoulder, Transform upperArm, Transform forearm, Transform hand, Transform root)
		{
			this.chest.transform = chest;
			this.shoulder.transform = shoulder;
			this.upperArm.transform = upperArm;
			this.forearm.transform = forearm;
			this.hand.transform = hand;
			base.Initiate(root);
			return base.initiated;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000442F5 File Offset: 0x000424F5
		public override IKSolver.Point[] GetPoints()
		{
			return new IKSolver.Point[]
			{
				this.chest,
				this.shoulder,
				this.upperArm,
				this.forearm,
				this.hand
			};
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0004432C File Offset: 0x0004252C
		public override IKSolver.Point GetPoint(Transform transform)
		{
			if (this.chest.transform == transform)
			{
				return this.chest;
			}
			if (this.shoulder.transform == transform)
			{
				return this.shoulder;
			}
			if (this.upperArm.transform == transform)
			{
				return this.upperArm;
			}
			if (this.forearm.transform == transform)
			{
				return this.forearm;
			}
			if (this.hand.transform == transform)
			{
				return this.hand;
			}
			return null;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000443BC File Offset: 0x000425BC
		public override void StoreDefaultLocalState()
		{
			this.shoulder.StoreDefaultLocalState();
			this.upperArm.StoreDefaultLocalState();
			this.forearm.StoreDefaultLocalState();
			this.hand.StoreDefaultLocalState();
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x000443EA File Offset: 0x000425EA
		public override void FixTransforms()
		{
			if (!base.initiated)
			{
				return;
			}
			this.shoulder.FixTransform();
			this.upperArm.FixTransform();
			this.forearm.FixTransform();
			this.hand.FixTransform();
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00044421 File Offset: 0x00042621
		protected override void OnInitiate()
		{
			this.IKPosition = this.hand.transform.position;
			this.IKRotation = this.hand.transform.rotation;
			this.Read();
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00044455 File Offset: 0x00042655
		protected override void OnUpdate()
		{
			this.Read();
			this.Solve();
			this.Write();
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00044469 File Offset: 0x00042669
		private void Solve()
		{
			this.arm.PreSolve();
			this.arm.ApplyOffsets(1f);
			this.arm.Solve(this.isLeft);
			this.arm.ResetOffsets();
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x000444A4 File Offset: 0x000426A4
		private void Read()
		{
			this.arm.IKPosition = this.IKPosition;
			this.arm.positionWeight = this.IKPositionWeight;
			this.arm.IKRotation = this.IKRotation;
			this.arm.rotationWeight = this.IKRotationWeight;
			this.positions[0] = this.root.position;
			this.positions[1] = this.chest.transform.position;
			this.positions[2] = this.shoulder.transform.position;
			this.positions[3] = this.upperArm.transform.position;
			this.positions[4] = this.forearm.transform.position;
			this.positions[5] = this.hand.transform.position;
			this.rotations[0] = this.root.rotation;
			this.rotations[1] = this.chest.transform.rotation;
			this.rotations[2] = this.shoulder.transform.rotation;
			this.rotations[3] = this.upperArm.transform.rotation;
			this.rotations[4] = this.forearm.transform.rotation;
			this.rotations[5] = this.hand.transform.rotation;
			this.arm.Read(this.positions, this.rotations, false, false, true, false, false, 1, 2);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0004465C File Offset: 0x0004285C
		private void Write()
		{
			this.arm.Write(ref this.positions, ref this.rotations);
			this.shoulder.transform.rotation = this.rotations[2];
			this.upperArm.transform.rotation = this.rotations[3];
			this.forearm.transform.rotation = this.rotations[4];
			this.hand.transform.rotation = this.rotations[5];
			this.forearm.transform.position = this.positions[4];
			this.hand.transform.position = this.positions[5];
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00044728 File Offset: 0x00042928
		public IKSolverArm()
		{
		}

		// Token: 0x04000828 RID: 2088
		[Range(0f, 1f)]
		public float IKRotationWeight = 1f;

		// Token: 0x04000829 RID: 2089
		public Quaternion IKRotation = Quaternion.identity;

		// Token: 0x0400082A RID: 2090
		public IKSolver.Point chest = new IKSolver.Point();

		// Token: 0x0400082B RID: 2091
		public IKSolver.Point shoulder = new IKSolver.Point();

		// Token: 0x0400082C RID: 2092
		public IKSolver.Point upperArm = new IKSolver.Point();

		// Token: 0x0400082D RID: 2093
		public IKSolver.Point forearm = new IKSolver.Point();

		// Token: 0x0400082E RID: 2094
		public IKSolver.Point hand = new IKSolver.Point();

		// Token: 0x0400082F RID: 2095
		public bool isLeft;

		// Token: 0x04000830 RID: 2096
		public IKSolverVR.Arm arm = new IKSolverVR.Arm();

		// Token: 0x04000831 RID: 2097
		private Vector3[] positions = new Vector3[6];

		// Token: 0x04000832 RID: 2098
		private Quaternion[] rotations = new Quaternion[6];
	}
}
