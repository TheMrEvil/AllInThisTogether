using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000131 RID: 305
	public class CharacterAnimationThirdPersonIK : CharacterAnimationThirdPerson
	{
		// Token: 0x06000CCB RID: 3275 RVA: 0x00056C45 File Offset: 0x00054E45
		protected override void Start()
		{
			base.Start();
			this.ik = base.GetComponent<FullBodyBipedIK>();
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00056C5C File Offset: 0x00054E5C
		protected override void LateUpdate()
		{
			base.LateUpdate();
			if (Vector3.Angle(base.transform.up, Vector3.up) <= 0.01f)
			{
				return;
			}
			Quaternion rotation = Quaternion.FromToRotation(base.transform.up, Vector3.up);
			this.RotateEffector(this.ik.solver.bodyEffector, rotation, 0.1f);
			this.RotateEffector(this.ik.solver.leftShoulderEffector, rotation, 0.2f);
			this.RotateEffector(this.ik.solver.rightShoulderEffector, rotation, 0.2f);
			this.RotateEffector(this.ik.solver.leftHandEffector, rotation, 0.1f);
			this.RotateEffector(this.ik.solver.rightHandEffector, rotation, 0.1f);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00056D30 File Offset: 0x00054F30
		private void RotateEffector(IKEffector effector, Quaternion rotation, float mlp)
		{
			Vector3 vector = effector.bone.position - base.transform.position;
			Vector3 a = rotation * vector - vector;
			effector.positionOffset += a * mlp;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00056D7F File Offset: 0x00054F7F
		public CharacterAnimationThirdPersonIK()
		{
		}

		// Token: 0x04000A48 RID: 2632
		private FullBodyBipedIK ik;
	}
}
