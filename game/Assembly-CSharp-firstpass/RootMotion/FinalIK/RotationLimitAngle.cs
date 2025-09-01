using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x0200010B RID: 267
	[HelpURL("http://www.root-motion.com/finalikdox/html/page14.html")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/Rotation Limits/Rotation Limit Angle")]
	public class RotationLimitAngle : RotationLimit
	{
		// Token: 0x06000BF3 RID: 3059 RVA: 0x000503EB File Offset: 0x0004E5EB
		[ContextMenu("User Manual")]
		private void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page14.html");
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x000503F7 File Offset: 0x0004E5F7
		[ContextMenu("Scrpt Reference")]
		private void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_rotation_limit_angle.html");
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00050403 File Offset: 0x0004E603
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0005040F File Offset: 0x0004E60F
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0005041B File Offset: 0x0004E61B
		protected override Quaternion LimitRotation(Quaternion rotation)
		{
			return RotationLimit.LimitTwist(this.LimitSwing(rotation), this.axis, base.secondaryAxis, this.twistLimit);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0005043C File Offset: 0x0004E63C
		private Quaternion LimitSwing(Quaternion rotation)
		{
			if (this.axis == Vector3.zero)
			{
				return rotation;
			}
			if (rotation == Quaternion.identity)
			{
				return rotation;
			}
			if (this.limit >= 180f)
			{
				return rotation;
			}
			Vector3 vector = rotation * this.axis;
			Quaternion to = Quaternion.FromToRotation(this.axis, vector);
			Quaternion rotation2 = Quaternion.RotateTowards(Quaternion.identity, to, this.limit);
			return Quaternion.FromToRotation(vector, rotation2 * this.axis) * rotation;
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x000504BF File Offset: 0x0004E6BF
		public RotationLimitAngle()
		{
		}

		// Token: 0x0400094C RID: 2380
		[Range(0f, 180f)]
		public float limit = 45f;

		// Token: 0x0400094D RID: 2381
		[Range(0f, 180f)]
		public float twistLimit = 180f;
	}
}
