using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000139 RID: 313
	public class FPSAiming : MonoBehaviour
	{
		// Token: 0x06000CE9 RID: 3305 RVA: 0x000578D0 File Offset: 0x00055AD0
		private void Start()
		{
			this.gunTargetDefaultLocalPosition = this.gunTarget.localPosition;
			this.gunTargetDefaultLocalRotation = this.gunTarget.localEulerAngles;
			this.camDefaultLocalPosition = this.cam.transform.localPosition;
			this.cam.enabled = false;
			this.gunAim.enabled = false;
			if (this.headAim != null)
			{
				this.headAim.enabled = false;
			}
			this.ik.enabled = false;
			if (this.recoil != null && this.ik.solver.iterations == 0)
			{
				Debug.LogWarning("FPSAiming with Recoil needs FBBIK solver iteration count to be at least 1 to maintain accuracy.");
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0005797D File Offset: 0x00055B7D
		private void FixedUpdate()
		{
			this.updateFrame = true;
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00057988 File Offset: 0x00055B88
		private void LateUpdate()
		{
			if (!this.animatePhysics)
			{
				this.updateFrame = true;
			}
			if (!this.updateFrame)
			{
				return;
			}
			this.updateFrame = false;
			this.cam.transform.localPosition = this.camDefaultLocalPosition;
			this.camRelativeToGunTarget = this.gunTarget.InverseTransformPoint(this.cam.transform.position);
			this.cam.LateUpdate();
			this.RotateCharacter();
			this.Aiming();
			this.LookDownTheSight();
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00057A08 File Offset: 0x00055C08
		private void Aiming()
		{
			if (this.headAim == null && this.aimWeight <= 0f)
			{
				return;
			}
			Quaternion rotation = this.cam.transform.rotation;
			this.headAim.solver.IKPosition = this.cam.transform.position + this.cam.transform.forward * 10f;
			this.headAim.solver.IKPositionWeight = 1f - this.aimWeight;
			this.headAim.solver.Update();
			this.gunAim.solver.IKPosition = this.cam.transform.position + this.cam.transform.forward * 10f + this.cam.transform.rotation * this.aimOffset;
			this.gunAim.solver.IKPositionWeight = this.aimWeight;
			this.gunAim.solver.Update();
			this.cam.transform.rotation = rotation;
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00057B44 File Offset: 0x00055D44
		private void LookDownTheSight()
		{
			float t = this.aimWeight * this.sightWeight;
			this.gunTarget.position = Vector3.Lerp(this.gun.position, this.gunTarget.parent.TransformPoint(this.gunTargetDefaultLocalPosition), t);
			this.gunTarget.rotation = Quaternion.Lerp(this.gun.rotation, this.gunTarget.parent.rotation * Quaternion.Euler(this.gunTargetDefaultLocalRotation), t);
			Vector3 position = this.gun.InverseTransformPoint(this.ik.solver.leftHandEffector.bone.position);
			Vector3 position2 = this.gun.InverseTransformPoint(this.ik.solver.rightHandEffector.bone.position);
			Quaternion rhs = Quaternion.Inverse(this.gun.rotation) * this.ik.solver.leftHandEffector.bone.rotation;
			Quaternion rhs2 = Quaternion.Inverse(this.gun.rotation) * this.ik.solver.rightHandEffector.bone.rotation;
			float d = 1f;
			this.ik.solver.leftHandEffector.positionOffset += (this.gunTarget.TransformPoint(position) - (this.ik.solver.leftHandEffector.bone.position + this.ik.solver.leftHandEffector.positionOffset)) * d;
			this.ik.solver.rightHandEffector.positionOffset += (this.gunTarget.TransformPoint(position2) - (this.ik.solver.rightHandEffector.bone.position + this.ik.solver.rightHandEffector.positionOffset)) * d;
			this.ik.solver.headMapping.maintainRotationWeight = 1f;
			if (this.recoil != null)
			{
				this.recoil.SetHandRotations(this.gunTarget.rotation * rhs, this.gunTarget.rotation * rhs2);
			}
			this.ik.solver.Update();
			if (this.recoil != null)
			{
				this.ik.references.leftHand.rotation = this.recoil.rotationOffset * (this.gunTarget.rotation * rhs);
				this.ik.references.rightHand.rotation = this.recoil.rotationOffset * (this.gunTarget.rotation * rhs2);
			}
			else
			{
				this.ik.references.leftHand.rotation = this.gunTarget.rotation * rhs;
				this.ik.references.rightHand.rotation = this.gunTarget.rotation * rhs2;
			}
			this.cam.transform.position = Vector3.Lerp(this.cam.transform.position, Vector3.Lerp(this.gunTarget.TransformPoint(this.camRelativeToGunTarget), this.gun.transform.TransformPoint(this.camRelativeToGunTarget), this.cameraRecoilWeight), t);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00057EE4 File Offset: 0x000560E4
		private void RotateCharacter()
		{
			if (this.maxAngle >= 180f)
			{
				return;
			}
			if (this.maxAngle <= 0f)
			{
				base.transform.rotation = Quaternion.LookRotation(new Vector3(this.cam.transform.forward.x, 0f, this.cam.transform.forward.z));
				return;
			}
			Vector3 vector = base.transform.InverseTransformDirection(this.cam.transform.forward);
			float num = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
			if (Mathf.Abs(num) > Mathf.Abs(this.maxAngle))
			{
				float angle = num - this.maxAngle;
				if (num < 0f)
				{
					angle = num + this.maxAngle;
				}
				base.transform.rotation = Quaternion.AngleAxis(angle, base.transform.up) * base.transform.rotation;
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00057FDE File Offset: 0x000561DE
		public FPSAiming()
		{
		}

		// Token: 0x04000A75 RID: 2677
		[Range(0f, 1f)]
		public float aimWeight = 1f;

		// Token: 0x04000A76 RID: 2678
		[Range(0f, 1f)]
		public float sightWeight = 1f;

		// Token: 0x04000A77 RID: 2679
		[Range(0f, 180f)]
		public float maxAngle = 80f;

		// Token: 0x04000A78 RID: 2680
		public Vector3 aimOffset;

		// Token: 0x04000A79 RID: 2681
		public bool animatePhysics;

		// Token: 0x04000A7A RID: 2682
		public Transform gun;

		// Token: 0x04000A7B RID: 2683
		public Transform gunTarget;

		// Token: 0x04000A7C RID: 2684
		public FullBodyBipedIK ik;

		// Token: 0x04000A7D RID: 2685
		public AimIK gunAim;

		// Token: 0x04000A7E RID: 2686
		public AimIK headAim;

		// Token: 0x04000A7F RID: 2687
		public CameraControllerFPS cam;

		// Token: 0x04000A80 RID: 2688
		public Recoil recoil;

		// Token: 0x04000A81 RID: 2689
		[Range(0f, 1f)]
		public float cameraRecoilWeight = 0.5f;

		// Token: 0x04000A82 RID: 2690
		private Vector3 gunTargetDefaultLocalPosition;

		// Token: 0x04000A83 RID: 2691
		private Vector3 gunTargetDefaultLocalRotation;

		// Token: 0x04000A84 RID: 2692
		private Vector3 camDefaultLocalPosition;

		// Token: 0x04000A85 RID: 2693
		private Vector3 camRelativeToGunTarget;

		// Token: 0x04000A86 RID: 2694
		private bool updateFrame;
	}
}
