using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000120 RID: 288
	public static class VRIKCalibrator
	{
		// Token: 0x06000C70 RID: 3184 RVA: 0x000532AD File Offset: 0x000514AD
		public static void RecalibrateScale(VRIK ik, VRIKCalibrator.CalibrationData data, VRIKCalibrator.Settings settings)
		{
			VRIKCalibrator.RecalibrateScale(ik, data, settings.scaleMlp);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x000532BC File Offset: 0x000514BC
		public static void RecalibrateScale(VRIK ik, VRIKCalibrator.CalibrationData data, float scaleMlp)
		{
			VRIKCalibrator.CalibrateScale(ik, scaleMlp);
			data.scale = ik.references.root.localScale.y;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x000532E0 File Offset: 0x000514E0
		private static void CalibrateScale(VRIK ik, VRIKCalibrator.Settings settings)
		{
			VRIKCalibrator.CalibrateScale(ik, settings.scaleMlp);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x000532F0 File Offset: 0x000514F0
		private static void CalibrateScale(VRIK ik, float scaleMlp = 1f)
		{
			float num = (ik.solver.spine.headTarget.position.y - ik.references.root.position.y) / (ik.references.head.position.y - ik.references.root.position.y);
			ik.references.root.localScale *= num * scaleMlp;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00053378 File Offset: 0x00051578
		public static VRIKCalibrator.CalibrationData Calibrate(VRIK ik, VRIKCalibrator.Settings settings, Transform headTracker, Transform bodyTracker = null, Transform leftHandTracker = null, Transform rightHandTracker = null, Transform leftFootTracker = null, Transform rightFootTracker = null)
		{
			if (!ik.solver.initiated)
			{
				Debug.LogError("Can not calibrate before VRIK has initiated.");
				return null;
			}
			if (headTracker == null)
			{
				Debug.LogError("Can not calibrate VRIK without the head tracker.");
				return null;
			}
			VRIKCalibrator.CalibrationData calibrationData = new VRIKCalibrator.CalibrationData();
			ik.solver.FixTransforms();
			Vector3 vector = headTracker.position + headTracker.rotation * Quaternion.LookRotation(settings.headTrackerForward, settings.headTrackerUp) * settings.headOffset;
			ik.references.root.position = new Vector3(vector.x, ik.references.root.position.y, vector.z);
			Vector3 forward = headTracker.rotation * settings.headTrackerForward;
			forward.y = 0f;
			ik.references.root.rotation = Quaternion.LookRotation(forward);
			Transform transform = (ik.solver.spine.headTarget == null) ? new GameObject("Head Target").transform : ik.solver.spine.headTarget;
			transform.position = vector;
			transform.rotation = ik.references.head.rotation;
			transform.parent = headTracker;
			ik.solver.spine.headTarget = transform;
			float num = (transform.position.y - ik.references.root.position.y) / (ik.references.head.position.y - ik.references.root.position.y);
			ik.references.root.localScale *= num * settings.scaleMlp;
			if (bodyTracker != null)
			{
				Transform transform2 = (ik.solver.spine.pelvisTarget == null) ? new GameObject("Pelvis Target").transform : ik.solver.spine.pelvisTarget;
				transform2.position = ik.references.pelvis.position;
				transform2.rotation = ik.references.pelvis.rotation;
				transform2.parent = bodyTracker;
				ik.solver.spine.pelvisTarget = transform2;
				ik.solver.spine.pelvisPositionWeight = settings.pelvisPositionWeight;
				ik.solver.spine.pelvisRotationWeight = settings.pelvisRotationWeight;
				ik.solver.plantFeet = false;
				ik.solver.spine.maxRootAngle = 180f;
			}
			else if (leftFootTracker != null && rightFootTracker != null)
			{
				ik.solver.spine.maxRootAngle = 0f;
			}
			if (leftHandTracker != null)
			{
				Transform transform3 = (ik.solver.leftArm.target == null) ? new GameObject("Left Hand Target").transform : ik.solver.leftArm.target;
				transform3.position = leftHandTracker.position + leftHandTracker.rotation * Quaternion.LookRotation(settings.handTrackerForward, settings.handTrackerUp) * settings.handOffset;
				Vector3 upAxis = Vector3.Cross(ik.solver.leftArm.wristToPalmAxis, ik.solver.leftArm.palmToThumbAxis);
				transform3.rotation = QuaTools.MatchRotation(leftHandTracker.rotation * Quaternion.LookRotation(settings.handTrackerForward, settings.handTrackerUp), settings.handTrackerForward, settings.handTrackerUp, ik.solver.leftArm.wristToPalmAxis, upAxis);
				transform3.parent = leftHandTracker;
				ik.solver.leftArm.target = transform3;
				ik.solver.leftArm.positionWeight = 1f;
				ik.solver.leftArm.rotationWeight = 1f;
			}
			else
			{
				ik.solver.leftArm.positionWeight = 0f;
				ik.solver.leftArm.rotationWeight = 0f;
			}
			if (rightHandTracker != null)
			{
				Transform transform4 = (ik.solver.rightArm.target == null) ? new GameObject("Right Hand Target").transform : ik.solver.rightArm.target;
				transform4.position = rightHandTracker.position + rightHandTracker.rotation * Quaternion.LookRotation(settings.handTrackerForward, settings.handTrackerUp) * settings.handOffset;
				Vector3 upAxis2 = -Vector3.Cross(ik.solver.rightArm.wristToPalmAxis, ik.solver.rightArm.palmToThumbAxis);
				transform4.rotation = QuaTools.MatchRotation(rightHandTracker.rotation * Quaternion.LookRotation(settings.handTrackerForward, settings.handTrackerUp), settings.handTrackerForward, settings.handTrackerUp, ik.solver.rightArm.wristToPalmAxis, upAxis2);
				transform4.parent = rightHandTracker;
				ik.solver.rightArm.target = transform4;
				ik.solver.rightArm.positionWeight = 1f;
				ik.solver.rightArm.rotationWeight = 1f;
			}
			else
			{
				ik.solver.rightArm.positionWeight = 0f;
				ik.solver.rightArm.rotationWeight = 0f;
			}
			if (leftFootTracker != null)
			{
				VRIKCalibrator.CalibrateLeg(settings, leftFootTracker, ik.solver.leftLeg, (ik.references.leftToes != null) ? ik.references.leftToes : ik.references.leftFoot, ik.references.root.forward, true);
			}
			if (rightFootTracker != null)
			{
				VRIKCalibrator.CalibrateLeg(settings, rightFootTracker, ik.solver.rightLeg, (ik.references.rightToes != null) ? ik.references.rightToes : ik.references.rightFoot, ik.references.root.forward, false);
			}
			bool flag = bodyTracker != null || (leftFootTracker != null && rightFootTracker != null);
			VRIKRootController vrikrootController = ik.references.root.GetComponent<VRIKRootController>();
			if (flag)
			{
				if (vrikrootController == null)
				{
					vrikrootController = ik.references.root.gameObject.AddComponent<VRIKRootController>();
				}
				vrikrootController.Calibrate();
			}
			else if (vrikrootController != null)
			{
				UnityEngine.Object.Destroy(vrikrootController);
			}
			ik.solver.spine.minHeadHeight = 0f;
			ik.solver.locomotion.weight = ((bodyTracker == null && leftFootTracker == null && rightFootTracker == null) ? 1f : 0f);
			calibrationData.scale = ik.references.root.localScale.y;
			calibrationData.head = new VRIKCalibrator.CalibrationData.Target(ik.solver.spine.headTarget);
			calibrationData.pelvis = new VRIKCalibrator.CalibrationData.Target(ik.solver.spine.pelvisTarget);
			calibrationData.leftHand = new VRIKCalibrator.CalibrationData.Target(ik.solver.leftArm.target);
			calibrationData.rightHand = new VRIKCalibrator.CalibrationData.Target(ik.solver.rightArm.target);
			calibrationData.leftFoot = new VRIKCalibrator.CalibrationData.Target(ik.solver.leftLeg.target);
			calibrationData.rightFoot = new VRIKCalibrator.CalibrationData.Target(ik.solver.rightLeg.target);
			calibrationData.leftLegGoal = new VRIKCalibrator.CalibrationData.Target(ik.solver.leftLeg.bendGoal);
			calibrationData.rightLegGoal = new VRIKCalibrator.CalibrationData.Target(ik.solver.rightLeg.bendGoal);
			calibrationData.pelvisTargetRight = ((vrikrootController != null) ? vrikrootController.pelvisTargetRight : Vector3.zero);
			calibrationData.pelvisPositionWeight = ik.solver.spine.pelvisPositionWeight;
			calibrationData.pelvisRotationWeight = ik.solver.spine.pelvisRotationWeight;
			return calibrationData;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00053BC0 File Offset: 0x00051DC0
		private static void CalibrateLeg(VRIKCalibrator.Settings settings, Transform tracker, IKSolverVR.Leg leg, Transform lastBone, Vector3 rootForward, bool isLeft)
		{
			string str = isLeft ? "Left" : "Right";
			Transform transform = (leg.target == null) ? new GameObject(str + " Foot Target").transform : leg.target;
			Quaternion rotation = tracker.rotation * Quaternion.LookRotation(settings.footTrackerForward, settings.footTrackerUp);
			Vector3 vector = rotation * Vector3.forward;
			vector.y = 0f;
			rotation = Quaternion.LookRotation(vector);
			float x = isLeft ? settings.footInwardOffset : (-settings.footInwardOffset);
			transform.position = tracker.position + rotation * new Vector3(x, 0f, settings.footForwardOffset);
			transform.position = new Vector3(transform.position.x, lastBone.position.y, transform.position.z);
			transform.rotation = lastBone.rotation;
			Vector3 vector2 = AxisTools.GetAxisVectorToDirection(lastBone, rootForward);
			if (Vector3.Dot(lastBone.rotation * vector2, rootForward) < 0f)
			{
				vector2 = -vector2;
			}
			Vector3 vector3 = Quaternion.Inverse(Quaternion.LookRotation(transform.rotation * vector2)) * vector;
			float num = Mathf.Atan2(vector3.x, vector3.z) * 57.29578f;
			float num2 = isLeft ? settings.footHeadingOffset : (-settings.footHeadingOffset);
			transform.rotation = Quaternion.AngleAxis(num + num2, Vector3.up) * transform.rotation;
			transform.parent = tracker;
			leg.target = transform;
			leg.positionWeight = 1f;
			leg.rotationWeight = 1f;
			Transform transform2 = (leg.bendGoal == null) ? new GameObject(str + " Leg Bend Goal").transform : leg.bendGoal;
			transform2.position = lastBone.position + rotation * Vector3.forward + rotation * Vector3.up;
			transform2.parent = tracker;
			leg.bendGoal = transform2;
			leg.bendGoalWeight = 1f;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00053DF4 File Offset: 0x00051FF4
		public static void Calibrate(VRIK ik, VRIKCalibrator.CalibrationData data, Transform headTracker, Transform bodyTracker = null, Transform leftHandTracker = null, Transform rightHandTracker = null, Transform leftFootTracker = null, Transform rightFootTracker = null)
		{
			if (!ik.solver.initiated)
			{
				Debug.LogError("Can not calibrate before VRIK has initiated.");
				return;
			}
			if (headTracker == null)
			{
				Debug.LogError("Can not calibrate VRIK without the head tracker.");
				return;
			}
			ik.solver.FixTransforms();
			Transform transform = (ik.solver.spine.headTarget == null) ? new GameObject("Head Target").transform : ik.solver.spine.headTarget;
			transform.parent = headTracker;
			data.head.SetTo(transform);
			ik.solver.spine.headTarget = transform;
			ik.references.root.localScale = data.scale * Vector3.one;
			if (bodyTracker != null && data.pelvis != null)
			{
				Transform transform2 = (ik.solver.spine.pelvisTarget == null) ? new GameObject("Pelvis Target").transform : ik.solver.spine.pelvisTarget;
				transform2.parent = bodyTracker;
				data.pelvis.SetTo(transform2);
				ik.solver.spine.pelvisTarget = transform2;
				ik.solver.spine.pelvisPositionWeight = data.pelvisPositionWeight;
				ik.solver.spine.pelvisRotationWeight = data.pelvisRotationWeight;
				ik.solver.plantFeet = false;
				ik.solver.spine.maxRootAngle = 180f;
			}
			else if (leftFootTracker != null && rightFootTracker != null)
			{
				ik.solver.spine.maxRootAngle = 0f;
			}
			if (leftHandTracker != null)
			{
				Transform transform3 = (ik.solver.leftArm.target == null) ? new GameObject("Left Hand Target").transform : ik.solver.leftArm.target;
				transform3.parent = leftHandTracker;
				data.leftHand.SetTo(transform3);
				ik.solver.leftArm.target = transform3;
				ik.solver.leftArm.positionWeight = 1f;
				ik.solver.leftArm.rotationWeight = 1f;
			}
			else
			{
				ik.solver.leftArm.positionWeight = 0f;
				ik.solver.leftArm.rotationWeight = 0f;
			}
			if (rightHandTracker != null)
			{
				Transform transform4 = (ik.solver.rightArm.target == null) ? new GameObject("Right Hand Target").transform : ik.solver.rightArm.target;
				transform4.parent = rightHandTracker;
				data.rightHand.SetTo(transform4);
				ik.solver.rightArm.target = transform4;
				ik.solver.rightArm.positionWeight = 1f;
				ik.solver.rightArm.rotationWeight = 1f;
			}
			else
			{
				ik.solver.rightArm.positionWeight = 0f;
				ik.solver.rightArm.rotationWeight = 0f;
			}
			if (leftFootTracker != null)
			{
				VRIKCalibrator.CalibrateLeg(data, leftFootTracker, ik.solver.leftLeg, (ik.references.leftToes != null) ? ik.references.leftToes : ik.references.leftFoot, ik.references.root.forward, true);
			}
			if (rightFootTracker != null)
			{
				VRIKCalibrator.CalibrateLeg(data, rightFootTracker, ik.solver.rightLeg, (ik.references.rightToes != null) ? ik.references.rightToes : ik.references.rightFoot, ik.references.root.forward, false);
			}
			bool flag = bodyTracker != null || (leftFootTracker != null && rightFootTracker != null);
			VRIKRootController vrikrootController = ik.references.root.GetComponent<VRIKRootController>();
			if (flag)
			{
				if (vrikrootController == null)
				{
					vrikrootController = ik.references.root.gameObject.AddComponent<VRIKRootController>();
				}
				vrikrootController.Calibrate(data);
			}
			else if (vrikrootController != null)
			{
				UnityEngine.Object.Destroy(vrikrootController);
			}
			ik.solver.spine.minHeadHeight = 0f;
			ik.solver.locomotion.weight = ((bodyTracker == null && leftFootTracker == null && rightFootTracker == null) ? 1f : 0f);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00054298 File Offset: 0x00052498
		private static void CalibrateLeg(VRIKCalibrator.CalibrationData data, Transform tracker, IKSolverVR.Leg leg, Transform lastBone, Vector3 rootForward, bool isLeft)
		{
			if (isLeft && data.leftFoot == null)
			{
				return;
			}
			if (!isLeft && data.rightFoot == null)
			{
				return;
			}
			string str = isLeft ? "Left" : "Right";
			Transform transform = (leg.target == null) ? new GameObject(str + " Foot Target").transform : leg.target;
			transform.parent = tracker;
			if (isLeft)
			{
				data.leftFoot.SetTo(transform);
			}
			else
			{
				data.rightFoot.SetTo(transform);
			}
			leg.target = transform;
			leg.positionWeight = 1f;
			leg.rotationWeight = 1f;
			Transform transform2 = (leg.bendGoal == null) ? new GameObject(str + " Leg Bend Goal").transform : leg.bendGoal;
			transform2.parent = tracker;
			if (isLeft)
			{
				data.leftLegGoal.SetTo(transform2);
			}
			else
			{
				data.rightLegGoal.SetTo(transform2);
			}
			leg.bendGoal = transform2;
			leg.bendGoalWeight = 1f;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x000543A4 File Offset: 0x000525A4
		public static VRIKCalibrator.CalibrationData Calibrate(VRIK ik, Transform centerEyeAnchor, Transform leftHandAnchor, Transform rightHandAnchor, Vector3 centerEyePositionOffset, Vector3 centerEyeRotationOffset, Vector3 handPositionOffset, Vector3 handRotationOffset, float scaleMlp = 1f)
		{
			VRIKCalibrator.CalibrateHead(ik, centerEyeAnchor, centerEyePositionOffset, centerEyeRotationOffset);
			VRIKCalibrator.CalibrateHands(ik, leftHandAnchor, rightHandAnchor, handPositionOffset, handRotationOffset);
			VRIKCalibrator.CalibrateScale(ik, scaleMlp);
			return new VRIKCalibrator.CalibrationData
			{
				scale = ik.references.root.localScale.y,
				head = new VRIKCalibrator.CalibrationData.Target(ik.solver.spine.headTarget),
				leftHand = new VRIKCalibrator.CalibrationData.Target(ik.solver.leftArm.target),
				rightHand = new VRIKCalibrator.CalibrationData.Target(ik.solver.rightArm.target)
			};
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00054444 File Offset: 0x00052644
		public static void CalibrateHead(VRIK ik, Transform centerEyeAnchor, Vector3 anchorPositionOffset, Vector3 anchorRotationOffset)
		{
			if (ik.solver.spine.headTarget == null)
			{
				ik.solver.spine.headTarget = new GameObject("Head IK Target").transform;
			}
			Vector3 forward = Quaternion.Inverse(ik.references.head.rotation) * ik.references.root.forward;
			Vector3 upwards = Quaternion.Inverse(ik.references.head.rotation) * ik.references.root.up;
			Quaternion rhs = Quaternion.LookRotation(forward, upwards);
			Vector3 b = ik.references.head.position + ik.references.head.rotation * rhs * anchorPositionOffset;
			Quaternion quaternion = Quaternion.Inverse(ik.references.head.rotation * rhs * Quaternion.Euler(anchorRotationOffset));
			ik.solver.spine.headTarget.parent = centerEyeAnchor;
			ik.solver.spine.headTarget.localPosition = quaternion * (ik.references.head.position - b);
			ik.solver.spine.headTarget.localRotation = quaternion * ik.references.head.rotation;
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x000545B0 File Offset: 0x000527B0
		public static void CalibrateBody(VRIK ik, Transform pelvisTracker, Vector3 trackerPositionOffset, Vector3 trackerRotationOffset)
		{
			if (ik.solver.spine.pelvisTarget == null)
			{
				ik.solver.spine.pelvisTarget = new GameObject("Pelvis IK Target").transform;
			}
			ik.solver.spine.pelvisTarget.position = ik.references.pelvis.position + ik.references.root.rotation * trackerPositionOffset;
			ik.solver.spine.pelvisTarget.rotation = ik.references.root.rotation * Quaternion.Euler(trackerRotationOffset);
			ik.solver.spine.pelvisTarget.parent = pelvisTracker;
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0005467C File Offset: 0x0005287C
		public static void CalibrateHands(VRIK ik, Transform leftHandAnchor, Transform rightHandAnchor, Vector3 anchorPositionOffset, Vector3 anchorRotationOffset)
		{
			if (ik.solver.leftArm.target == null)
			{
				ik.solver.leftArm.target = new GameObject("Left Hand IK Target").transform;
			}
			if (ik.solver.rightArm.target == null)
			{
				ik.solver.rightArm.target = new GameObject("Right Hand IK Target").transform;
			}
			VRIKCalibrator.CalibrateHand(ik.references.leftHand, ik.references.leftForearm, ik.solver.leftArm.target, leftHandAnchor, anchorPositionOffset, anchorRotationOffset, true);
			VRIKCalibrator.CalibrateHand(ik.references.rightHand, ik.references.rightForearm, ik.solver.rightArm.target, rightHandAnchor, anchorPositionOffset, anchorRotationOffset, false);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00054758 File Offset: 0x00052958
		private static void CalibrateHand(Transform hand, Transform forearm, Transform target, Transform anchor, Vector3 positionOffset, Vector3 rotationOffset, bool isLeft)
		{
			if (isLeft)
			{
				positionOffset.x = -positionOffset.x;
				rotationOffset.y = -rotationOffset.y;
				rotationOffset.z = -rotationOffset.z;
			}
			Vector3 forward = VRIKCalibrator.GuessWristToPalmAxis(hand, forearm);
			Vector3 upwards = VRIKCalibrator.GuessPalmToThumbAxis(hand, forearm);
			Quaternion rhs = Quaternion.LookRotation(forward, upwards);
			Vector3 b = hand.position + hand.rotation * rhs * positionOffset;
			Quaternion quaternion = Quaternion.Inverse(hand.rotation * rhs * Quaternion.Euler(rotationOffset));
			target.parent = anchor;
			target.localPosition = quaternion * (hand.position - b);
			target.localRotation = quaternion * hand.rotation;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0005481C File Offset: 0x00052A1C
		public static Vector3 GuessWristToPalmAxis(Transform hand, Transform forearm)
		{
			Vector3 vector = forearm.position - hand.position;
			Vector3 vector2 = AxisTools.ToVector3(AxisTools.GetAxisToDirection(hand, vector));
			if (Vector3.Dot(vector, hand.rotation * vector2) > 0f)
			{
				vector2 = -vector2;
			}
			return vector2;
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0005486C File Offset: 0x00052A6C
		public static Vector3 GuessPalmToThumbAxis(Transform hand, Transform forearm)
		{
			if (hand.childCount == 0)
			{
				Debug.LogWarning("Hand " + hand.name + " does not have any fingers, VRIK can not guess the hand bone's orientation. Please assign 'Wrist To Palm Axis' and 'Palm To Thumb Axis' manually for both arms in VRIK settings.", hand);
				return Vector3.zero;
			}
			float num = float.PositiveInfinity;
			int index = 0;
			for (int i = 0; i < hand.childCount; i++)
			{
				float num2 = Vector3.SqrMagnitude(hand.GetChild(i).position - hand.position);
				if (num2 < num)
				{
					num = num2;
					index = i;
				}
			}
			Vector3 vector = Vector3.Cross(Vector3.Cross(hand.position - forearm.position, hand.GetChild(index).position - hand.position), hand.position - forearm.position);
			Vector3 vector2 = AxisTools.ToVector3(AxisTools.GetAxisToDirection(hand, vector));
			if (Vector3.Dot(vector, hand.rotation * vector2) < 0f)
			{
				vector2 = -vector2;
			}
			return vector2;
		}

		// Token: 0x02000229 RID: 553
		[Serializable]
		public class Settings
		{
			// Token: 0x06001198 RID: 4504 RVA: 0x0006D528 File Offset: 0x0006B728
			public Settings()
			{
			}

			// Token: 0x04001056 RID: 4182
			[Tooltip("Multiplies character scale")]
			public float scaleMlp = 1f;

			// Token: 0x04001057 RID: 4183
			[Tooltip("Local axis of the HMD facing forward.")]
			public Vector3 headTrackerForward = Vector3.forward;

			// Token: 0x04001058 RID: 4184
			[Tooltip("Local axis of the HMD facing up.")]
			public Vector3 headTrackerUp = Vector3.up;

			// Token: 0x04001059 RID: 4185
			[Tooltip("Local axis of the hand trackers pointing from the wrist towards the palm.")]
			public Vector3 handTrackerForward = Vector3.forward;

			// Token: 0x0400105A RID: 4186
			[Tooltip("Local axis of the hand trackers pointing in the direction of the surface normal of the back of the hand.")]
			public Vector3 handTrackerUp = Vector3.up;

			// Token: 0x0400105B RID: 4187
			[Tooltip("Local axis of the foot trackers towards the player's forward direction.")]
			public Vector3 footTrackerForward = Vector3.forward;

			// Token: 0x0400105C RID: 4188
			[Tooltip("Local axis of the foot tracker towards the up direction.")]
			public Vector3 footTrackerUp = Vector3.up;

			// Token: 0x0400105D RID: 4189
			[Space(10f)]
			[Tooltip("Offset of the head bone from the HMD in (headTrackerForward, headTrackerUp) space relative to the head tracker.")]
			public Vector3 headOffset;

			// Token: 0x0400105E RID: 4190
			[Tooltip("Offset of the hand bones from the hand trackers in (handTrackerForward, handTrackerUp) space relative to the hand trackers.")]
			public Vector3 handOffset;

			// Token: 0x0400105F RID: 4191
			[Tooltip("Forward offset of the foot bones from the foot trackers.")]
			public float footForwardOffset;

			// Token: 0x04001060 RID: 4192
			[Tooltip("Inward offset of the foot bones from the foot trackers.")]
			public float footInwardOffset;

			// Token: 0x04001061 RID: 4193
			[Tooltip("Used for adjusting foot heading relative to the foot trackers.")]
			[Range(-180f, 180f)]
			public float footHeadingOffset;

			// Token: 0x04001062 RID: 4194
			[Range(0f, 1f)]
			public float pelvisPositionWeight = 1f;

			// Token: 0x04001063 RID: 4195
			[Range(0f, 1f)]
			public float pelvisRotationWeight = 1f;
		}

		// Token: 0x0200022A RID: 554
		[Serializable]
		public class CalibrationData
		{
			// Token: 0x06001199 RID: 4505 RVA: 0x0006D59E File Offset: 0x0006B79E
			public CalibrationData()
			{
			}

			// Token: 0x04001064 RID: 4196
			public float scale;

			// Token: 0x04001065 RID: 4197
			public VRIKCalibrator.CalibrationData.Target head;

			// Token: 0x04001066 RID: 4198
			public VRIKCalibrator.CalibrationData.Target leftHand;

			// Token: 0x04001067 RID: 4199
			public VRIKCalibrator.CalibrationData.Target rightHand;

			// Token: 0x04001068 RID: 4200
			public VRIKCalibrator.CalibrationData.Target pelvis;

			// Token: 0x04001069 RID: 4201
			public VRIKCalibrator.CalibrationData.Target leftFoot;

			// Token: 0x0400106A RID: 4202
			public VRIKCalibrator.CalibrationData.Target rightFoot;

			// Token: 0x0400106B RID: 4203
			public VRIKCalibrator.CalibrationData.Target leftLegGoal;

			// Token: 0x0400106C RID: 4204
			public VRIKCalibrator.CalibrationData.Target rightLegGoal;

			// Token: 0x0400106D RID: 4205
			public Vector3 pelvisTargetRight;

			// Token: 0x0400106E RID: 4206
			public float pelvisPositionWeight;

			// Token: 0x0400106F RID: 4207
			public float pelvisRotationWeight;

			// Token: 0x02000252 RID: 594
			[Serializable]
			public class Target
			{
				// Token: 0x060011E6 RID: 4582 RVA: 0x0006E8DA File Offset: 0x0006CADA
				public Target(Transform t)
				{
					this.used = (t != null);
					if (!this.used)
					{
						return;
					}
					this.localPosition = t.localPosition;
					this.localRotation = t.localRotation;
				}

				// Token: 0x060011E7 RID: 4583 RVA: 0x0006E910 File Offset: 0x0006CB10
				public void SetTo(Transform t)
				{
					if (!this.used)
					{
						return;
					}
					t.localPosition = this.localPosition;
					t.localRotation = this.localRotation;
				}

				// Token: 0x04001120 RID: 4384
				public bool used;

				// Token: 0x04001121 RID: 4385
				public Vector3 localPosition;

				// Token: 0x04001122 RID: 4386
				public Quaternion localRotation;
			}
		}
	}
}
