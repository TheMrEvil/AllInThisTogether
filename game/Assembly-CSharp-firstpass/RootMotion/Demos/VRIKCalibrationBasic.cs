using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000155 RID: 341
	public class VRIKCalibrationBasic : MonoBehaviour
	{
		// Token: 0x06000D53 RID: 3411 RVA: 0x00059FCC File Offset: 0x000581CC
		private void LateUpdate()
		{
			if (Input.GetKeyDown(KeyCode.C))
			{
				this.data = VRIKCalibrator.Calibrate(this.ik, this.centerEyeAnchor, this.leftHandAnchor, this.rightHandAnchor, this.headAnchorPositionOffset, this.headAnchorRotationOffset, this.handAnchorPositionOffset, this.handAnchorRotationOffset, this.scaleMlp);
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				if (this.data.scale == 0f)
				{
					Debug.LogError("No Calibration Data to calibrate to, please calibrate with 'C' first.");
				}
				else
				{
					VRIKCalibrator.Calibrate(this.ik, this.data, this.centerEyeAnchor, null, this.leftHandAnchor, this.rightHandAnchor, null, null);
				}
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				if (this.data.scale == 0f)
				{
					Debug.LogError("Avatar needs to be calibrated before RecalibrateScale is called.");
				}
				VRIKCalibrator.RecalibrateScale(this.ik, this.data, this.scaleMlp);
			}
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0005A0AC File Offset: 0x000582AC
		public VRIKCalibrationBasic()
		{
		}

		// Token: 0x04000B02 RID: 2818
		[Tooltip("The VRIK component.")]
		public VRIK ik;

		// Token: 0x04000B03 RID: 2819
		[Header("Head")]
		[Tooltip("HMD.")]
		public Transform centerEyeAnchor;

		// Token: 0x04000B04 RID: 2820
		[Tooltip("Position offset of the camera from the head bone (root space).")]
		public Vector3 headAnchorPositionOffset;

		// Token: 0x04000B05 RID: 2821
		[Tooltip("Rotation offset of the camera from the head bone (root space).")]
		public Vector3 headAnchorRotationOffset;

		// Token: 0x04000B06 RID: 2822
		[Header("Hands")]
		[Tooltip("Left Hand Controller")]
		public Transform leftHandAnchor;

		// Token: 0x04000B07 RID: 2823
		[Tooltip("Right Hand Controller")]
		public Transform rightHandAnchor;

		// Token: 0x04000B08 RID: 2824
		[Tooltip("Position offset of the hand controller from the hand bone (controller space).")]
		public Vector3 handAnchorPositionOffset;

		// Token: 0x04000B09 RID: 2825
		[Tooltip("Rotation offset of the hand controller from the hand bone (controller space).")]
		public Vector3 handAnchorRotationOffset;

		// Token: 0x04000B0A RID: 2826
		[Header("Scale")]
		[Tooltip("Multiplies the scale of the root.")]
		public float scaleMlp = 1f;

		// Token: 0x04000B0B RID: 2827
		[Header("Data stored by Calibration")]
		public VRIKCalibrator.CalibrationData data = new VRIKCalibrator.CalibrationData();
	}
}
