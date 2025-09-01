using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000156 RID: 342
	public class VRIKCalibrationController : MonoBehaviour
	{
		// Token: 0x06000D55 RID: 3413 RVA: 0x0005A0CC File Offset: 0x000582CC
		private void LateUpdate()
		{
			if (Input.GetKeyDown(KeyCode.C))
			{
				this.data = VRIKCalibrator.Calibrate(this.ik, this.settings, this.headTracker, this.bodyTracker, this.leftHandTracker, this.rightHandTracker, this.leftFootTracker, this.rightFootTracker);
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				if (this.data.scale == 0f)
				{
					Debug.LogError("No Calibration Data to calibrate to, please calibrate with settings first.");
				}
				else
				{
					VRIKCalibrator.Calibrate(this.ik, this.data, this.headTracker, this.bodyTracker, this.leftHandTracker, this.rightHandTracker, this.leftFootTracker, this.rightFootTracker);
				}
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				if (this.data.scale == 0f)
				{
					Debug.LogError("Avatar needs to be calibrated before RecalibrateScale is called.");
				}
				VRIKCalibrator.RecalibrateScale(this.ik, this.data, this.settings);
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0005A1B5 File Offset: 0x000583B5
		public VRIKCalibrationController()
		{
		}

		// Token: 0x04000B0C RID: 2828
		[Tooltip("Reference to the VRIK component on the avatar.")]
		public VRIK ik;

		// Token: 0x04000B0D RID: 2829
		[Tooltip("The settings for VRIK calibration.")]
		public VRIKCalibrator.Settings settings;

		// Token: 0x04000B0E RID: 2830
		[Tooltip("The HMD.")]
		public Transform headTracker;

		// Token: 0x04000B0F RID: 2831
		[Tooltip("(Optional) A tracker placed anywhere on the body of the player, preferrably close to the pelvis, on the belt area.")]
		public Transform bodyTracker;

		// Token: 0x04000B10 RID: 2832
		[Tooltip("(Optional) A tracker or hand controller device placed anywhere on or in the player's left hand.")]
		public Transform leftHandTracker;

		// Token: 0x04000B11 RID: 2833
		[Tooltip("(Optional) A tracker or hand controller device placed anywhere on or in the player's right hand.")]
		public Transform rightHandTracker;

		// Token: 0x04000B12 RID: 2834
		[Tooltip("(Optional) A tracker placed anywhere on the ankle or toes of the player's left leg.")]
		public Transform leftFootTracker;

		// Token: 0x04000B13 RID: 2835
		[Tooltip("(Optional) A tracker placed anywhere on the ankle or toes of the player's right leg.")]
		public Transform rightFootTracker;

		// Token: 0x04000B14 RID: 2836
		[Header("Data stored by Calibration")]
		public VRIKCalibrator.CalibrationData data = new VRIKCalibrator.CalibrationData();
	}
}
