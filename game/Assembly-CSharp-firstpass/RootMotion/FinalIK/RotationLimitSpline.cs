using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x0200010E RID: 270
	[HelpURL("http://www.root-motion.com/finalikdox/html/page14.html")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/Rotation Limits/Rotation Limit Spline")]
	public class RotationLimitSpline : RotationLimit
	{
		// Token: 0x06000C11 RID: 3089 RVA: 0x00050D73 File Offset: 0x0004EF73
		[ContextMenu("User Manual")]
		private void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page14.html");
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00050D7F File Offset: 0x0004EF7F
		[ContextMenu("Scrpt Reference")]
		private void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_rotation_limit_spline.html");
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00050D8B File Offset: 0x0004EF8B
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00050D97 File Offset: 0x0004EF97
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00050DA3 File Offset: 0x0004EFA3
		public void SetSpline(Keyframe[] keyframes)
		{
			this.spline.keys = keyframes;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00050DB1 File Offset: 0x0004EFB1
		protected override Quaternion LimitRotation(Quaternion rotation)
		{
			return RotationLimit.LimitTwist(this.LimitSwing(rotation), this.axis, base.secondaryAxis, this.twistLimit);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00050DD4 File Offset: 0x0004EFD4
		public Quaternion LimitSwing(Quaternion rotation)
		{
			if (this.axis == Vector3.zero)
			{
				return rotation;
			}
			if (rotation == Quaternion.identity)
			{
				return rotation;
			}
			Vector3 vector = rotation * this.axis;
			float num = RotationLimit.GetOrthogonalAngle(vector, base.secondaryAxis, this.axis);
			if (Vector3.Dot(vector, base.crossAxis) < 0f)
			{
				num = 180f + (180f - num);
			}
			float maxDegreesDelta = this.spline.Evaluate(num);
			Quaternion to = Quaternion.FromToRotation(this.axis, vector);
			Quaternion rotation2 = Quaternion.RotateTowards(Quaternion.identity, to, maxDegreesDelta);
			return Quaternion.FromToRotation(vector, rotation2 * this.axis) * rotation;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00050E86 File Offset: 0x0004F086
		public RotationLimitSpline()
		{
		}

		// Token: 0x04000958 RID: 2392
		[Range(0f, 180f)]
		public float twistLimit = 180f;

		// Token: 0x04000959 RID: 2393
		[HideInInspector]
		public AnimationCurve spline;
	}
}
