using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000112 RID: 274
	public class BodyTilt : OffsetModifier
	{
		// Token: 0x06000C25 RID: 3109 RVA: 0x000516C3 File Offset: 0x0004F8C3
		protected override void Start()
		{
			base.Start();
			this.lastForward = base.transform.forward;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x000516DC File Offset: 0x0004F8DC
		protected override void OnModifyOffset()
		{
			Quaternion quaternion = Quaternion.FromToRotation(this.lastForward, base.transform.forward);
			float num = 0f;
			Vector3 zero = Vector3.zero;
			quaternion.ToAngleAxis(out num, out zero);
			if (zero.y > 0f)
			{
				num = -num;
			}
			num *= this.tiltSensitivity * 0.01f;
			num /= base.deltaTime;
			num = Mathf.Clamp(num, -1f, 1f);
			this.tiltAngle = Mathf.Lerp(this.tiltAngle, num, base.deltaTime * this.tiltSpeed);
			float weight = Mathf.Abs(this.tiltAngle) / 1f;
			if (this.tiltAngle < 0f)
			{
				this.poseRight.Apply(this.ik.solver, weight);
			}
			else
			{
				this.poseLeft.Apply(this.ik.solver, weight);
			}
			this.lastForward = base.transform.forward;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x000517CF File Offset: 0x0004F9CF
		public BodyTilt()
		{
		}

		// Token: 0x0400097C RID: 2428
		[Tooltip("Speed of tilting")]
		public float tiltSpeed = 6f;

		// Token: 0x0400097D RID: 2429
		[Tooltip("Sensitivity of tilting")]
		public float tiltSensitivity = 0.07f;

		// Token: 0x0400097E RID: 2430
		[Tooltip("The OffsetPose components")]
		public OffsetPose poseLeft;

		// Token: 0x0400097F RID: 2431
		[Tooltip("The OffsetPose components")]
		public OffsetPose poseRight;

		// Token: 0x04000980 RID: 2432
		private float tiltAngle;

		// Token: 0x04000981 RID: 2433
		private Vector3 lastForward;
	}
}
