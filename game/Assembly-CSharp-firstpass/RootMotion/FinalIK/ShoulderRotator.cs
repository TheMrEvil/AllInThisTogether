using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x0200011F RID: 287
	public class ShoulderRotator : MonoBehaviour
	{
		// Token: 0x06000C6A RID: 3178 RVA: 0x00053024 File Offset: 0x00051224
		private void Start()
		{
			this.ik = base.GetComponent<FullBodyBipedIK>();
			IKSolverFullBodyBiped solver = this.ik.solver;
			solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPostUpdate, new IKSolver.UpdateDelegate(this.RotateShoulders));
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00053060 File Offset: 0x00051260
		private void RotateShoulders()
		{
			if (this.ik == null)
			{
				return;
			}
			if (this.ik.solver.IKPositionWeight <= 0f)
			{
				return;
			}
			if (this.skip)
			{
				this.skip = false;
				return;
			}
			this.RotateShoulder(FullBodyBipedChain.LeftArm, this.weight, this.offset);
			this.RotateShoulder(FullBodyBipedChain.RightArm, this.weight, this.offset);
			this.skip = true;
			this.ik.solver.Update();
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x000530E4 File Offset: 0x000512E4
		private void RotateShoulder(FullBodyBipedChain chain, float weight, float offset)
		{
			Quaternion b = Quaternion.FromToRotation(this.GetParentBoneMap(chain).swingDirection, this.ik.solver.GetEndEffector(chain).position - this.GetParentBoneMap(chain).transform.position);
			Vector3 vector = this.ik.solver.GetEndEffector(chain).position - this.ik.solver.GetLimbMapping(chain).bone1.position;
			float num = this.ik.solver.GetChain(chain).nodes[0].length + this.ik.solver.GetChain(chain).nodes[1].length;
			float num2 = vector.magnitude / num - 1f + offset;
			num2 = Mathf.Clamp(num2 * weight, 0f, 1f);
			Quaternion lhs = Quaternion.Lerp(Quaternion.identity, b, num2 * this.ik.solver.GetEndEffector(chain).positionWeight * this.ik.solver.IKPositionWeight);
			this.ik.solver.GetLimbMapping(chain).parentBone.rotation = lhs * this.ik.solver.GetLimbMapping(chain).parentBone.rotation;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0005323A File Offset: 0x0005143A
		private IKMapping.BoneMap GetParentBoneMap(FullBodyBipedChain chain)
		{
			return this.ik.solver.GetLimbMapping(chain).GetBoneMap(IKMappingLimb.BoneMapType.Parent);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00053253 File Offset: 0x00051453
		private void OnDestroy()
		{
			if (this.ik != null)
			{
				IKSolverFullBodyBiped solver = this.ik.solver;
				solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPostUpdate, new IKSolver.UpdateDelegate(this.RotateShoulders));
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0005328F File Offset: 0x0005148F
		public ShoulderRotator()
		{
		}

		// Token: 0x040009C4 RID: 2500
		[Tooltip("Weight of shoulder rotation")]
		public float weight = 1.5f;

		// Token: 0x040009C5 RID: 2501
		[Tooltip("The greater the offset, the sooner the shoulder will start rotating")]
		public float offset = 0.2f;

		// Token: 0x040009C6 RID: 2502
		private FullBodyBipedIK ik;

		// Token: 0x040009C7 RID: 2503
		private bool skip;
	}
}
