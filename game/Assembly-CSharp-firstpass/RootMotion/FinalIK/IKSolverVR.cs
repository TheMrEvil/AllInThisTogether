using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace RootMotion.FinalIK
{
	// Token: 0x020000FD RID: 253
	[Serializable]
	public class IKSolverVR : IKSolver
	{
		// Token: 0x06000B11 RID: 2833 RVA: 0x0004A52C File Offset: 0x0004872C
		public void SetToReferences(VRIK.References references)
		{
			if (!references.isFilled)
			{
				Debug.LogError("Invalid references, one or more Transforms are missing.");
				return;
			}
			this.solverTransforms = references.GetTransforms();
			this.hasChest = (this.solverTransforms[3] != null);
			this.hasNeck = (this.solverTransforms[4] != null);
			this.hasShoulders = (this.solverTransforms[6] != null && this.solverTransforms[10] != null);
			this.hasToes = (this.solverTransforms[17] != null && this.solverTransforms[21] != null);
			this.hasLegs = (this.solverTransforms[14] != null);
			this.readPositions = new Vector3[this.solverTransforms.Length];
			this.readRotations = new Quaternion[this.solverTransforms.Length];
			this.DefaultAnimationCurves();
			this.GuessHandOrientations(references, true);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0004A61C File Offset: 0x0004881C
		public void GuessHandOrientations(VRIK.References references, bool onlyIfZero)
		{
			if (!references.isFilled)
			{
				Debug.LogWarning("VRIK References are not filled in, can not guess hand orientations. Right-click on VRIK header and slect 'Guess Hand Orientations' when you have filled in the References.", references.root);
				return;
			}
			if (this.leftArm.wristToPalmAxis == Vector3.zero || !onlyIfZero)
			{
				this.leftArm.wristToPalmAxis = VRIKCalibrator.GuessWristToPalmAxis(references.leftHand, references.leftForearm);
			}
			if (this.leftArm.palmToThumbAxis == Vector3.zero || !onlyIfZero)
			{
				this.leftArm.palmToThumbAxis = VRIKCalibrator.GuessPalmToThumbAxis(references.leftHand, references.leftForearm);
			}
			if (this.rightArm.wristToPalmAxis == Vector3.zero || !onlyIfZero)
			{
				this.rightArm.wristToPalmAxis = VRIKCalibrator.GuessWristToPalmAxis(references.rightHand, references.rightForearm);
			}
			if (this.rightArm.palmToThumbAxis == Vector3.zero || !onlyIfZero)
			{
				this.rightArm.palmToThumbAxis = VRIKCalibrator.GuessPalmToThumbAxis(references.rightHand, references.rightForearm);
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0004A71C File Offset: 0x0004891C
		public void DefaultAnimationCurves()
		{
			if (this.locomotion.stepHeight == null)
			{
				this.locomotion.stepHeight = new AnimationCurve();
			}
			if (this.locomotion.heelHeight == null)
			{
				this.locomotion.heelHeight = new AnimationCurve();
			}
			if (this.locomotion.stepHeight.keys.Length == 0)
			{
				this.locomotion.stepHeight.keys = IKSolverVR.GetSineKeyframes(0.03f);
			}
			if (this.locomotion.heelHeight.keys.Length == 0)
			{
				this.locomotion.heelHeight.keys = IKSolverVR.GetSineKeyframes(0.03f);
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0004A7C0 File Offset: 0x000489C0
		public void AddPositionOffset(IKSolverVR.PositionOffset positionOffset, Vector3 value)
		{
			switch (positionOffset)
			{
			case IKSolverVR.PositionOffset.Pelvis:
				this.spine.pelvisPositionOffset += value;
				return;
			case IKSolverVR.PositionOffset.Chest:
				this.spine.chestPositionOffset += value;
				return;
			case IKSolverVR.PositionOffset.Head:
				this.spine.headPositionOffset += value;
				return;
			case IKSolverVR.PositionOffset.LeftHand:
				this.leftArm.handPositionOffset += value;
				return;
			case IKSolverVR.PositionOffset.RightHand:
				this.rightArm.handPositionOffset += value;
				return;
			case IKSolverVR.PositionOffset.LeftFoot:
				this.leftLeg.footPositionOffset += value;
				return;
			case IKSolverVR.PositionOffset.RightFoot:
				this.rightLeg.footPositionOffset += value;
				return;
			case IKSolverVR.PositionOffset.LeftHeel:
				this.leftLeg.heelPositionOffset += value;
				return;
			case IKSolverVR.PositionOffset.RightHeel:
				this.rightLeg.heelPositionOffset += value;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0004A8CF File Offset: 0x00048ACF
		public void AddRotationOffset(IKSolverVR.RotationOffset rotationOffset, Vector3 value)
		{
			this.AddRotationOffset(rotationOffset, Quaternion.Euler(value));
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0004A8E0 File Offset: 0x00048AE0
		public void AddRotationOffset(IKSolverVR.RotationOffset rotationOffset, Quaternion value)
		{
			switch (rotationOffset)
			{
			case IKSolverVR.RotationOffset.Pelvis:
				this.spine.pelvisRotationOffset = value * this.spine.pelvisRotationOffset;
				return;
			case IKSolverVR.RotationOffset.Chest:
				this.spine.chestRotationOffset = value * this.spine.chestRotationOffset;
				return;
			case IKSolverVR.RotationOffset.Head:
				this.spine.headRotationOffset = value * this.spine.headRotationOffset;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0004A958 File Offset: 0x00048B58
		public void AddPlatformMotion(Vector3 deltaPosition, Quaternion deltaRotation, Vector3 platformPivot)
		{
			this.locomotion.AddDeltaPosition(deltaPosition);
			this.raycastOriginPelvis += deltaPosition;
			this.locomotion.AddDeltaRotation(deltaRotation, platformPivot);
			this.spine.faceDirection = deltaRotation * this.spine.faceDirection;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0004A9AC File Offset: 0x00048BAC
		public void Reset()
		{
			if (!base.initiated)
			{
				return;
			}
			this.UpdateSolverTransforms();
			this.Read(this.readPositions, this.readRotations, this.hasChest, this.hasNeck, this.hasShoulders, this.hasToes, this.hasLegs);
			this.spine.faceDirection = this.rootBone.readRotation * Vector3.forward;
			if (this.hasLegs)
			{
				this.locomotion.Reset(this.readPositions, this.readRotations);
				this.raycastOriginPelvis = this.spine.pelvis.readPosition;
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0004AA50 File Offset: 0x00048C50
		public override void StoreDefaultLocalState()
		{
			for (int i = 1; i < this.solverTransforms.Length; i++)
			{
				if (this.solverTransforms[i] != null)
				{
					this.defaultLocalPositions[i - 1] = this.solverTransforms[i].localPosition;
					this.defaultLocalRotations[i - 1] = this.solverTransforms[i].localRotation;
				}
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0004AAB8 File Offset: 0x00048CB8
		public override void FixTransforms()
		{
			if (!base.initiated)
			{
				return;
			}
			if (this.LOD >= 2)
			{
				return;
			}
			for (int i = 1; i < this.solverTransforms.Length; i++)
			{
				if (this.solverTransforms[i] != null)
				{
					bool flag = i == 1;
					bool flag2 = i == 8 || i == 9 || i == 12 || i == 13;
					bool flag3 = (i >= 15 && i <= 17) || (i >= 19 && i <= 21);
					if (flag || flag2 || flag3)
					{
						this.solverTransforms[i].localPosition = this.defaultLocalPositions[i - 1];
					}
					this.solverTransforms[i].localRotation = this.defaultLocalRotations[i - 1];
				}
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0004AB76 File Offset: 0x00048D76
		public override IKSolver.Point[] GetPoints()
		{
			Debug.LogError("GetPoints() is not applicable to IKSolverVR.");
			return null;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0004AB83 File Offset: 0x00048D83
		public override IKSolver.Point GetPoint(Transform transform)
		{
			Debug.LogError("GetPoint is not applicable to IKSolverVR.");
			return null;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0004AB90 File Offset: 0x00048D90
		public override bool IsValid(ref string message)
		{
			if (this.solverTransforms == null || this.solverTransforms.Length == 0)
			{
				message = "Trying to initiate IKSolverVR with invalid bone references.";
				return false;
			}
			if (this.leftArm.wristToPalmAxis == Vector3.zero)
			{
				message = "Left arm 'Wrist To Palm Axis' needs to be set in VRIK. Please select the hand bone, set it to the axis that points from the wrist towards the palm. If the arrow points away from the palm, axis must be negative.";
				return false;
			}
			if (this.rightArm.wristToPalmAxis == Vector3.zero)
			{
				message = "Right arm 'Wrist To Palm Axis' needs to be set in VRIK. Please select the hand bone, set it to the axis that points from the wrist towards the palm. If the arrow points away from the palm, axis must be negative.";
				return false;
			}
			if (this.leftArm.palmToThumbAxis == Vector3.zero)
			{
				message = "Left arm 'Palm To Thumb Axis' needs to be set in VRIK. Please select the hand bone, set it to the axis that points from the palm towards the thumb. If the arrow points away from the thumb, axis must be negative.";
				return false;
			}
			if (this.rightArm.palmToThumbAxis == Vector3.zero)
			{
				message = "Right arm 'Palm To Thumb Axis' needs to be set in VRIK. Please select the hand bone, set it to the axis that points from the palm towards the thumb. If the arrow points away from the thumb, axis must be negative.";
				return false;
			}
			return true;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0004AC38 File Offset: 0x00048E38
		private Vector3 GetNormal(Transform[] transforms)
		{
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			for (int i = 0; i < transforms.Length; i++)
			{
				vector2 += transforms[i].position;
			}
			vector2 /= (float)transforms.Length;
			for (int j = 0; j < transforms.Length - 1; j++)
			{
				vector += Vector3.Cross(transforms[j].position - vector2, transforms[j + 1].position - vector2).normalized;
			}
			return vector;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0004ACC0 File Offset: 0x00048EC0
		private static Keyframe[] GetSineKeyframes(float mag)
		{
			Keyframe[] array = new Keyframe[3];
			array[0].time = 0f;
			array[0].value = 0f;
			array[1].time = 0.5f;
			array[1].value = mag;
			array[2].time = 1f;
			array[2].value = 0f;
			return array;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0004AD38 File Offset: 0x00048F38
		private void UpdateSolverTransforms()
		{
			for (int i = 0; i < this.solverTransforms.Length; i++)
			{
				if (this.solverTransforms[i] != null)
				{
					this.readPositions[i] = this.solverTransforms[i].position;
					this.readRotations[i] = this.solverTransforms[i].rotation;
				}
			}
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0004AD9A File Offset: 0x00048F9A
		protected override void OnInitiate()
		{
			this.UpdateSolverTransforms();
			this.Read(this.readPositions, this.readRotations, this.hasChest, this.hasNeck, this.hasShoulders, this.hasToes, this.hasLegs);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0004ADD4 File Offset: 0x00048FD4
		protected override void OnUpdate()
		{
			if (this.IKPositionWeight > 0f)
			{
				if (this.LOD < 2)
				{
					bool flag = false;
					if (this.lastLOD != this.LOD && this.lastLOD == 2)
					{
						this.spine.faceDirection = this.rootBone.readRotation * Vector3.forward;
						if (this.hasLegs)
						{
							if (this.locomotion.weight > 0f)
							{
								this.root.position = new Vector3(this.spine.headTarget.position.x, this.root.position.y, this.spine.headTarget.position.z);
								Vector3 faceDirection = this.spine.faceDirection;
								faceDirection.y = 0f;
								this.root.rotation = Quaternion.LookRotation(faceDirection, this.root.up);
								this.UpdateSolverTransforms();
								this.Read(this.readPositions, this.readRotations, this.hasChest, this.hasNeck, this.hasShoulders, this.hasToes, this.hasLegs);
								flag = true;
								this.locomotion.Reset(this.readPositions, this.readRotations);
							}
							this.raycastOriginPelvis = this.spine.pelvis.readPosition;
						}
					}
					if (!flag)
					{
						this.UpdateSolverTransforms();
						this.Read(this.readPositions, this.readRotations, this.hasChest, this.hasNeck, this.hasShoulders, this.hasToes, this.hasLegs);
					}
					this.Solve();
					this.Write();
					this.WriteTransforms();
				}
				else if (this.locomotion.weight > 0f)
				{
					this.root.position = new Vector3(this.spine.headTarget.position.x, this.root.position.y, this.spine.headTarget.position.z);
					Vector3 forward = this.spine.headTarget.rotation * this.spine.anchorRelativeToHead * Vector3.forward;
					forward.y = 0f;
					this.root.rotation = Quaternion.LookRotation(forward, this.root.up);
				}
			}
			this.lastLOD = this.LOD;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0004B050 File Offset: 0x00049250
		private void WriteTransforms()
		{
			for (int i = 0; i < this.solverTransforms.Length; i++)
			{
				if (this.solverTransforms[i] != null)
				{
					bool flag = i < 2;
					bool flag2 = i == 8 || i == 9 || i == 12 || i == 13;
					bool flag3 = (i >= 15 && i <= 17) || (i >= 19 && i <= 21);
					if (this.LOD > 0)
					{
						flag2 = false;
						flag3 = false;
					}
					if (flag)
					{
						this.solverTransforms[i].position = V3Tools.Lerp(this.solverTransforms[i].position, this.GetPosition(i), this.IKPositionWeight);
					}
					if (flag2 || flag3)
					{
						if (this.IKPositionWeight < 1f)
						{
							Vector3 localPosition = this.solverTransforms[i].localPosition;
							this.solverTransforms[i].position = V3Tools.Lerp(this.solverTransforms[i].position, this.GetPosition(i), this.IKPositionWeight);
							this.solverTransforms[i].localPosition = Vector3.Project(this.solverTransforms[i].localPosition, localPosition);
						}
						else
						{
							this.solverTransforms[i].position = V3Tools.Lerp(this.solverTransforms[i].position, this.GetPosition(i), this.IKPositionWeight);
						}
					}
					this.solverTransforms[i].rotation = QuaTools.Lerp(this.solverTransforms[i].rotation, this.GetRotation(i), this.IKPositionWeight);
				}
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0004B1C8 File Offset: 0x000493C8
		private void Read(Vector3[] positions, Quaternion[] rotations, bool hasChest, bool hasNeck, bool hasShoulders, bool hasToes, bool hasLegs)
		{
			if (this.rootBone == null)
			{
				this.rootBone = new IKSolverVR.VirtualBone(positions[0], rotations[0]);
			}
			else
			{
				this.rootBone.Read(positions[0], rotations[0]);
			}
			this.spine.Read(positions, rotations, hasChest, hasNeck, hasShoulders, hasToes, hasLegs, 0, 1);
			this.leftArm.Read(positions, rotations, hasChest, hasNeck, hasShoulders, hasToes, hasLegs, hasChest ? 3 : 2, 6);
			this.rightArm.Read(positions, rotations, hasChest, hasNeck, hasShoulders, hasToes, hasLegs, hasChest ? 3 : 2, 10);
			if (hasLegs)
			{
				this.leftLeg.Read(positions, rotations, hasChest, hasNeck, hasShoulders, hasToes, hasLegs, 1, 14);
				this.rightLeg.Read(positions, rotations, hasChest, hasNeck, hasShoulders, hasToes, hasLegs, 1, 18);
			}
			for (int i = 0; i < rotations.Length; i++)
			{
				this.solvedPositions[i] = positions[i];
				this.solvedRotations[i] = rotations[i];
			}
			if (!base.initiated)
			{
				if (hasLegs)
				{
					this.legs = new IKSolverVR.Leg[]
					{
						this.leftLeg,
						this.rightLeg
					};
				}
				this.arms = new IKSolverVR.Arm[]
				{
					this.leftArm,
					this.rightArm
				};
				if (hasLegs)
				{
					this.locomotion.Initiate(positions, rotations, hasToes, this.scale);
				}
				this.raycastOriginPelvis = this.spine.pelvis.readPosition;
				this.spine.faceDirection = this.readRotations[0] * Vector3.forward;
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0004B36C File Offset: 0x0004956C
		private void Solve()
		{
			if (this.scale <= 0f)
			{
				Debug.LogError("VRIK solver scale <= 0, can not solve!");
				return;
			}
			this.spine.SetLOD(this.LOD);
			IKSolverVR.Arm[] array = this.arms;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetLOD(this.LOD);
			}
			if (this.hasLegs)
			{
				IKSolverVR.Leg[] array2 = this.legs;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].SetLOD(this.LOD);
				}
			}
			this.spine.PreSolve();
			array = this.arms;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].PreSolve();
			}
			if (this.hasLegs)
			{
				IKSolverVR.Leg[] array2 = this.legs;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].PreSolve();
				}
			}
			array = this.arms;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ApplyOffsets(this.scale);
			}
			this.spine.ApplyOffsets(this.scale);
			this.spine.Solve(this.rootBone, this.legs, this.arms, this.scale);
			if (this.hasLegs && this.spine.pelvisPositionWeight > 0f && this.plantFeet)
			{
				Warning.Log("If VRIK 'Pelvis Position Weight' is > 0, 'Plant Feet' should be disabled to improve performance and stability.", this.root, false);
			}
			if (this.hasLegs && this.locomotion.weight > 0f)
			{
				Vector3 a = Vector3.zero;
				Vector3 a2 = Vector3.zero;
				Quaternion identity = Quaternion.identity;
				Quaternion identity2 = Quaternion.identity;
				float num = 0f;
				float num2 = 0f;
				float d = 0f;
				float d2 = 0f;
				this.locomotion.Solve(this.rootBone, this.spine, this.leftLeg, this.rightLeg, this.leftArm, this.rightArm, this.supportLegIndex, out a, out a2, out identity, out identity2, out num, out num2, out d, out d2, this.scale);
				a += this.root.up * num;
				a2 += this.root.up * num2;
				this.leftLeg.footPositionOffset += (a - this.leftLeg.lastBone.solverPosition) * this.IKPositionWeight * (1f - this.leftLeg.positionWeight) * this.locomotion.weight;
				this.rightLeg.footPositionOffset += (a2 - this.rightLeg.lastBone.solverPosition) * this.IKPositionWeight * (1f - this.rightLeg.positionWeight) * this.locomotion.weight;
				this.leftLeg.heelPositionOffset += this.root.up * d * this.locomotion.weight;
				this.rightLeg.heelPositionOffset += this.root.up * d2 * this.locomotion.weight;
				Quaternion quaternion = QuaTools.FromToRotation(this.leftLeg.lastBone.solverRotation, identity);
				Quaternion quaternion2 = QuaTools.FromToRotation(this.rightLeg.lastBone.solverRotation, identity2);
				quaternion = Quaternion.Lerp(Quaternion.identity, quaternion, this.IKPositionWeight * (1f - this.leftLeg.rotationWeight) * this.locomotion.weight);
				quaternion2 = Quaternion.Lerp(Quaternion.identity, quaternion2, this.IKPositionWeight * (1f - this.rightLeg.rotationWeight) * this.locomotion.weight);
				this.leftLeg.footRotationOffset = quaternion * this.leftLeg.footRotationOffset;
				this.rightLeg.footRotationOffset = quaternion2 * this.rightLeg.footRotationOffset;
				Vector3 vector = Vector3.Lerp(this.leftLeg.position + this.leftLeg.footPositionOffset, this.rightLeg.position + this.rightLeg.footPositionOffset, 0.5f);
				vector = V3Tools.PointToPlane(vector, this.rootBone.solverPosition, this.root.up);
				Vector3 vector2 = this.rootBone.solverPosition + this.rootVelocity * Time.deltaTime * 2f * this.locomotion.weight;
				vector2 = Vector3.Lerp(vector2, vector, Time.deltaTime * this.locomotion.rootSpeed * this.locomotion.weight);
				this.rootBone.solverPosition = vector2;
				this.rootVelocity += (vector - this.rootBone.solverPosition) * Time.deltaTime * 10f;
				Vector3 b = V3Tools.ExtractVertical(this.rootVelocity, this.root.up, 1f);
				this.rootVelocity -= b;
				float d3 = Mathf.Min(num + num2, this.locomotion.maxBodyYOffset * this.scale);
				this.bodyOffset = Vector3.Lerp(this.bodyOffset, this.root.up * d3, Time.deltaTime * 3f);
				this.bodyOffset = Vector3.Lerp(Vector3.zero, this.bodyOffset, this.locomotion.weight);
			}
			if (this.hasLegs)
			{
				IKSolverVR.Leg[] array2 = this.legs;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].ApplyOffsets(this.scale);
				}
				if (!this.plantFeet || this.LOD > 0)
				{
					this.spine.InverseTranslateToHead(this.legs, false, false, this.bodyOffset, 1f);
					array2 = this.legs;
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i].TranslateRoot(this.spine.pelvis.solverPosition, this.spine.pelvis.solverRotation);
					}
					array2 = this.legs;
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i].Solve(true);
					}
				}
				else
				{
					for (int j = 0; j < 2; j++)
					{
						this.spine.InverseTranslateToHead(this.legs, true, true, this.bodyOffset, 1f);
						array2 = this.legs;
						for (int i = 0; i < array2.Length; i++)
						{
							array2[i].TranslateRoot(this.spine.pelvis.solverPosition, this.spine.pelvis.solverRotation);
						}
						array2 = this.legs;
						for (int i = 0; i < array2.Length; i++)
						{
							array2[i].Solve(j == 0);
						}
					}
				}
			}
			else
			{
				this.spine.InverseTranslateToHead(this.legs, false, false, this.bodyOffset, 1f);
			}
			for (int k = 0; k < this.arms.Length; k++)
			{
				this.arms[k].TranslateRoot(this.spine.chest.solverPosition, this.spine.chest.solverRotation);
			}
			for (int l = 0; l < this.arms.Length; l++)
			{
				this.arms[l].Solve(l == 0);
			}
			this.spine.ResetOffsets();
			if (this.hasLegs)
			{
				IKSolverVR.Leg[] array2 = this.legs;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].ResetOffsets();
				}
			}
			array = this.arms;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ResetOffsets();
			}
			if (this.hasLegs)
			{
				this.spine.pelvisPositionOffset += this.GetPelvisOffset();
				this.spine.chestPositionOffset += this.spine.pelvisPositionOffset;
			}
			this.Write();
			if (this.hasLegs)
			{
				this.supportLegIndex = -1;
				float num3 = float.PositiveInfinity;
				for (int m = 0; m < this.legs.Length; m++)
				{
					float num4 = Vector3.SqrMagnitude(this.legs[m].lastBone.solverPosition - this.legs[m].bones[0].solverPosition);
					if (num4 < num3)
					{
						this.supportLegIndex = m;
						num3 = num4;
					}
				}
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0004BC28 File Offset: 0x00049E28
		private Vector3 GetPosition(int index)
		{
			return this.solvedPositions[index];
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0004BC36 File Offset: 0x00049E36
		private Quaternion GetRotation(int index)
		{
			return this.solvedRotations[index];
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0004BC44 File Offset: 0x00049E44
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x0004BC4C File Offset: 0x00049E4C
		[HideInInspector]
		public IKSolverVR.VirtualBone rootBone
		{
			[CompilerGenerated]
			get
			{
				return this.<rootBone>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<rootBone>k__BackingField = value;
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0004BC58 File Offset: 0x00049E58
		private void Write()
		{
			this.solvedPositions[0] = this.rootBone.solverPosition;
			this.solvedRotations[0] = this.rootBone.solverRotation;
			this.spine.Write(ref this.solvedPositions, ref this.solvedRotations);
			if (this.hasLegs)
			{
				IKSolverVR.Leg[] array = this.legs;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Write(ref this.solvedPositions, ref this.solvedRotations);
				}
			}
			IKSolverVR.Arm[] array2 = this.arms;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].Write(ref this.solvedPositions, ref this.solvedRotations);
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0004BD04 File Offset: 0x00049F04
		private Vector3 GetPelvisOffset()
		{
			if (this.locomotion.weight <= 0f)
			{
				return Vector3.zero;
			}
			if (this.locomotion.blockingLayers == -1)
			{
				return Vector3.zero;
			}
			Vector3 vector = this.raycastOriginPelvis;
			vector.y = this.spine.pelvis.solverPosition.y;
			Vector3 vector2 = this.spine.pelvis.readPosition;
			vector2.y = this.spine.pelvis.solverPosition.y;
			Vector3 direction = vector2 - vector;
			RaycastHit raycastHit;
			if (this.locomotion.raycastRadius <= 0f)
			{
				if (Physics.Raycast(vector, direction, out raycastHit, direction.magnitude * 1.1f, this.locomotion.blockingLayers))
				{
					vector2 = raycastHit.point;
				}
			}
			else if (Physics.SphereCast(vector, this.locomotion.raycastRadius * 1.1f, direction, out raycastHit, direction.magnitude, this.locomotion.blockingLayers))
			{
				vector2 = vector + direction.normalized * raycastHit.distance / 1.1f;
			}
			Vector3 a = this.spine.pelvis.solverPosition;
			direction = a - vector2;
			if (this.locomotion.raycastRadius <= 0f)
			{
				if (Physics.Raycast(vector2, direction, out raycastHit, direction.magnitude, this.locomotion.blockingLayers))
				{
					a = raycastHit.point;
				}
			}
			else if (Physics.SphereCast(vector2, this.locomotion.raycastRadius, direction, out raycastHit, direction.magnitude, this.locomotion.blockingLayers))
			{
				a = vector2 + direction.normalized * raycastHit.distance;
			}
			this.lastOffset = Vector3.Lerp(this.lastOffset, Vector3.zero, Time.deltaTime * 3f);
			a += Vector3.ClampMagnitude(this.lastOffset, 0.75f);
			a.y = this.spine.pelvis.solverPosition.y;
			this.lastOffset = Vector3.Lerp(this.lastOffset, a - this.spine.pelvis.solverPosition, Time.deltaTime * 15f);
			return this.lastOffset;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0004BF68 File Offset: 0x0004A168
		public IKSolverVR()
		{
		}

		// Token: 0x0400089E RID: 2206
		private Transform[] solverTransforms = new Transform[0];

		// Token: 0x0400089F RID: 2207
		private bool hasChest;

		// Token: 0x040008A0 RID: 2208
		private bool hasNeck;

		// Token: 0x040008A1 RID: 2209
		private bool hasShoulders;

		// Token: 0x040008A2 RID: 2210
		private bool hasToes;

		// Token: 0x040008A3 RID: 2211
		private bool hasLegs;

		// Token: 0x040008A4 RID: 2212
		private Vector3[] readPositions = new Vector3[0];

		// Token: 0x040008A5 RID: 2213
		private Quaternion[] readRotations = new Quaternion[0];

		// Token: 0x040008A6 RID: 2214
		private Vector3[] solvedPositions = new Vector3[22];

		// Token: 0x040008A7 RID: 2215
		private Quaternion[] solvedRotations = new Quaternion[22];

		// Token: 0x040008A8 RID: 2216
		private Quaternion[] defaultLocalRotations = new Quaternion[21];

		// Token: 0x040008A9 RID: 2217
		private Vector3[] defaultLocalPositions = new Vector3[21];

		// Token: 0x040008AA RID: 2218
		private Vector3 rootV;

		// Token: 0x040008AB RID: 2219
		private Vector3 rootVelocity;

		// Token: 0x040008AC RID: 2220
		private Vector3 bodyOffset;

		// Token: 0x040008AD RID: 2221
		private int supportLegIndex;

		// Token: 0x040008AE RID: 2222
		private int lastLOD;

		// Token: 0x040008AF RID: 2223
		[Tooltip("LOD 0: Full quality solving. LOD 1: Shoulder solving, stretching plant feet disabled, spine solving quality reduced. This provides about 30% of performance gain. LOD 2: Culled, but updating root position and rotation if locomotion is enabled.")]
		[Range(0f, 2f)]
		public int LOD;

		// Token: 0x040008B0 RID: 2224
		[Tooltip("Scale of the character. Value of 1 means normal adult human size.")]
		public float scale = 1f;

		// Token: 0x040008B1 RID: 2225
		[Tooltip("If true, will keep the toes planted even if head target is out of reach, so this can cause the camera to exit the head if it is too high for the model to reach. Enabling this increases the cost of the solver as the legs will have to be solved multiple times.")]
		public bool plantFeet = true;

		// Token: 0x040008B2 RID: 2226
		[CompilerGenerated]
		private IKSolverVR.VirtualBone <rootBone>k__BackingField;

		// Token: 0x040008B3 RID: 2227
		[Tooltip("The spine solver.")]
		public IKSolverVR.Spine spine = new IKSolverVR.Spine();

		// Token: 0x040008B4 RID: 2228
		[Tooltip("The left arm solver.")]
		public IKSolverVR.Arm leftArm = new IKSolverVR.Arm();

		// Token: 0x040008B5 RID: 2229
		[Tooltip("The right arm solver.")]
		public IKSolverVR.Arm rightArm = new IKSolverVR.Arm();

		// Token: 0x040008B6 RID: 2230
		[Tooltip("The left leg solver.")]
		public IKSolverVR.Leg leftLeg = new IKSolverVR.Leg();

		// Token: 0x040008B7 RID: 2231
		[Tooltip("The right leg solver.")]
		public IKSolverVR.Leg rightLeg = new IKSolverVR.Leg();

		// Token: 0x040008B8 RID: 2232
		[Tooltip("Procedural leg shuffling for stationary VR games. Not designed for roomscale and thumbstick locomotion. For those it would be better to use a strafing locomotion blend tree to make the character follow the horizontal direction towards the HMD by root motion or script.")]
		public IKSolverVR.Locomotion locomotion = new IKSolverVR.Locomotion();

		// Token: 0x040008B9 RID: 2233
		private IKSolverVR.Leg[] legs = new IKSolverVR.Leg[2];

		// Token: 0x040008BA RID: 2234
		private IKSolverVR.Arm[] arms = new IKSolverVR.Arm[2];

		// Token: 0x040008BB RID: 2235
		private Vector3 headPosition;

		// Token: 0x040008BC RID: 2236
		private Vector3 headDeltaPosition;

		// Token: 0x040008BD RID: 2237
		private Vector3 raycastOriginPelvis;

		// Token: 0x040008BE RID: 2238
		private Vector3 lastOffset;

		// Token: 0x040008BF RID: 2239
		private Vector3 debugPos1;

		// Token: 0x040008C0 RID: 2240
		private Vector3 debugPos2;

		// Token: 0x040008C1 RID: 2241
		private Vector3 debugPos3;

		// Token: 0x040008C2 RID: 2242
		private Vector3 debugPos4;

		// Token: 0x020001FD RID: 509
		[Serializable]
		public class Arm : IKSolverVR.BodyPart
		{
			// Token: 0x17000223 RID: 547
			// (get) Token: 0x06001090 RID: 4240 RVA: 0x00066988 File Offset: 0x00064B88
			// (set) Token: 0x06001091 RID: 4241 RVA: 0x00066990 File Offset: 0x00064B90
			public Vector3 position
			{
				[CompilerGenerated]
				get
				{
					return this.<position>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<position>k__BackingField = value;
				}
			}

			// Token: 0x17000224 RID: 548
			// (get) Token: 0x06001092 RID: 4242 RVA: 0x00066999 File Offset: 0x00064B99
			// (set) Token: 0x06001093 RID: 4243 RVA: 0x000669A1 File Offset: 0x00064BA1
			public Quaternion rotation
			{
				[CompilerGenerated]
				get
				{
					return this.<rotation>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<rotation>k__BackingField = value;
				}
			}

			// Token: 0x17000225 RID: 549
			// (get) Token: 0x06001094 RID: 4244 RVA: 0x000669AA File Offset: 0x00064BAA
			private IKSolverVR.VirtualBone shoulder
			{
				get
				{
					return this.bones[0];
				}
			}

			// Token: 0x17000226 RID: 550
			// (get) Token: 0x06001095 RID: 4245 RVA: 0x000669B4 File Offset: 0x00064BB4
			private IKSolverVR.VirtualBone upperArm
			{
				get
				{
					return this.bones[this.hasShoulder ? 1 : 0];
				}
			}

			// Token: 0x17000227 RID: 551
			// (get) Token: 0x06001096 RID: 4246 RVA: 0x000669C9 File Offset: 0x00064BC9
			private IKSolverVR.VirtualBone forearm
			{
				get
				{
					return this.bones[this.hasShoulder ? 2 : 1];
				}
			}

			// Token: 0x17000228 RID: 552
			// (get) Token: 0x06001097 RID: 4247 RVA: 0x000669DE File Offset: 0x00064BDE
			private IKSolverVR.VirtualBone hand
			{
				get
				{
					return this.bones[this.hasShoulder ? 3 : 2];
				}
			}

			// Token: 0x06001098 RID: 4248 RVA: 0x000669F4 File Offset: 0x00064BF4
			protected override void OnRead(Vector3[] positions, Quaternion[] rotations, bool hasChest, bool hasNeck, bool hasShoulders, bool hasToes, bool hasLegs, int rootIndex, int index)
			{
				Vector3 position = positions[index];
				Quaternion rotation = rotations[index];
				Vector3 vector = positions[index + 1];
				Quaternion quaternion = rotations[index + 1];
				Vector3 vector2 = positions[index + 2];
				Quaternion rotation2 = rotations[index + 2];
				Vector3 vector3 = positions[index + 3];
				Quaternion quaternion2 = rotations[index + 3];
				if (!this.initiated)
				{
					this.IKPosition = vector3;
					this.IKRotation = quaternion2;
					this.rotation = this.IKRotation;
					this.hasShoulder = hasShoulders;
					this.bones = new IKSolverVR.VirtualBone[this.hasShoulder ? 4 : 3];
					if (this.hasShoulder)
					{
						this.bones[0] = new IKSolverVR.VirtualBone(position, rotation);
						this.bones[1] = new IKSolverVR.VirtualBone(vector, quaternion);
						this.bones[2] = new IKSolverVR.VirtualBone(vector2, rotation2);
						this.bones[3] = new IKSolverVR.VirtualBone(vector3, quaternion2);
					}
					else
					{
						this.bones[0] = new IKSolverVR.VirtualBone(vector, quaternion);
						this.bones[1] = new IKSolverVR.VirtualBone(vector2, rotation2);
						this.bones[2] = new IKSolverVR.VirtualBone(vector3, quaternion2);
					}
					Vector3 vector4 = rotations[0] * Vector3.forward;
					this.chestForwardAxis = Quaternion.Inverse(this.rootRotation) * vector4;
					this.chestUpAxis = Quaternion.Inverse(this.rootRotation) * (rotations[0] * Vector3.up);
					Vector3 vector5 = AxisTools.GetAxisVectorToDirection(quaternion, vector4);
					if (Vector3.Dot(quaternion * vector5, vector4) < 0f)
					{
						vector5 = -vector5;
					}
					this.upperArmBendAxis = Vector3.Cross(Quaternion.Inverse(quaternion) * (vector2 - vector), vector5);
					if (this.upperArmBendAxis == Vector3.zero)
					{
						Debug.LogWarning("VRIK can not calculate which way to bend the arms because the arms are perfectly straight. Please rotate the elbow bones slightly in their natural bending direction in the Editor.");
					}
				}
				if (this.hasShoulder)
				{
					this.bones[0].Read(position, rotation);
					this.bones[1].Read(vector, quaternion);
					this.bones[2].Read(vector2, rotation2);
					this.bones[3].Read(vector3, quaternion2);
					return;
				}
				this.bones[0].Read(vector, quaternion);
				this.bones[1].Read(vector2, rotation2);
				this.bones[2].Read(vector3, quaternion2);
			}

			// Token: 0x06001099 RID: 4249 RVA: 0x00066C4C File Offset: 0x00064E4C
			public override void PreSolve()
			{
				if (this.target != null)
				{
					this.IKPosition = this.target.position;
					this.IKRotation = this.target.rotation;
				}
				this.position = V3Tools.Lerp(this.hand.solverPosition, this.IKPosition, this.positionWeight);
				this.rotation = QuaTools.Lerp(this.hand.solverRotation, this.IKRotation, this.rotationWeight);
				this.shoulder.axis = this.shoulder.axis.normalized;
				this.forearmRelToUpperArm = Quaternion.Inverse(this.upperArm.solverRotation) * this.forearm.solverRotation;
			}

			// Token: 0x0600109A RID: 4250 RVA: 0x00066D0E File Offset: 0x00064F0E
			public override void ApplyOffsets(float scale)
			{
				this.position += this.handPositionOffset;
			}

			// Token: 0x0600109B RID: 4251 RVA: 0x00066D28 File Offset: 0x00064F28
			private void Stretching()
			{
				float num = this.upperArm.length + this.forearm.length;
				Vector3 vector = Vector3.zero;
				Vector3 b = Vector3.zero;
				if (this.armLengthMlp != 1f)
				{
					num *= this.armLengthMlp;
					vector = (this.forearm.solverPosition - this.upperArm.solverPosition) * (this.armLengthMlp - 1f);
					b = (this.hand.solverPosition - this.forearm.solverPosition) * (this.armLengthMlp - 1f);
					this.forearm.solverPosition += vector;
					this.hand.solverPosition += vector + b;
				}
				float time = Vector3.Distance(this.upperArm.solverPosition, this.position) / num;
				float num2 = this.stretchCurve.Evaluate(time);
				num2 *= this.positionWeight;
				vector = (this.forearm.solverPosition - this.upperArm.solverPosition) * num2;
				b = (this.hand.solverPosition - this.forearm.solverPosition) * num2;
				this.forearm.solverPosition += vector;
				this.hand.solverPosition += vector + b;
			}

			// Token: 0x0600109C RID: 4252 RVA: 0x00066EAC File Offset: 0x000650AC
			public void Solve(bool isLeft)
			{
				this.chestRotation = Quaternion.LookRotation(this.rootRotation * this.chestForwardAxis, this.rootRotation * this.chestUpAxis);
				this.chestForward = this.chestRotation * Vector3.forward;
				this.chestUp = this.chestRotation * Vector3.up;
				Vector3 vector = Vector3.zero;
				if (this.hasShoulder && this.shoulderRotationWeight > 0f && this.LOD < 1)
				{
					IKSolverVR.Arm.ShoulderRotationMode shoulderRotationMode = this.shoulderRotationMode;
					if (shoulderRotationMode != IKSolverVR.Arm.ShoulderRotationMode.YawPitch)
					{
						if (shoulderRotationMode == IKSolverVR.Arm.ShoulderRotationMode.FromTo)
						{
							Quaternion solverRotation = this.shoulder.solverRotation;
							Quaternion quaternion = Quaternion.FromToRotation((this.upperArm.solverPosition - this.shoulder.solverPosition).normalized + this.chestForward, this.position - this.shoulder.solverPosition);
							quaternion = Quaternion.Slerp(Quaternion.identity, quaternion, 0.5f * this.shoulderRotationWeight * this.positionWeight);
							IKSolverVR.VirtualBone.RotateBy(this.bones, quaternion);
							this.Stretching();
							IKSolverVR.VirtualBone.SolveTrigonometric(this.bones, 0, 2, 3, this.position, Vector3.Cross(this.forearm.solverPosition - this.shoulder.solverPosition, this.hand.solverPosition - this.shoulder.solverPosition), 0.5f * this.shoulderRotationWeight * this.positionWeight);
							vector = this.GetBendNormal(this.position - this.upperArm.solverPosition);
							IKSolverVR.VirtualBone.SolveTrigonometric(this.bones, 1, 2, 3, this.position, vector, this.positionWeight);
							Quaternion rotation = Quaternion.Inverse(Quaternion.LookRotation(this.chestUp, this.chestForward));
							Vector3 vector2 = rotation * (solverRotation * this.shoulder.axis);
							Vector3 vector3 = rotation * (this.shoulder.solverRotation * this.shoulder.axis);
							float current = Mathf.Atan2(vector2.x, vector2.z) * 57.29578f;
							float num = Mathf.Atan2(vector3.x, vector3.z) * 57.29578f;
							float num2 = Mathf.DeltaAngle(current, num);
							if (isLeft)
							{
								num2 = -num2;
							}
							num2 = Mathf.Clamp(num2 * this.shoulderRotationWeight * this.shoulderTwistWeight * 2f * this.positionWeight, 0f, 180f);
							this.shoulder.solverRotation = Quaternion.AngleAxis(num2, this.shoulder.solverRotation * (isLeft ? this.shoulder.axis : (-this.shoulder.axis))) * this.shoulder.solverRotation;
							this.upperArm.solverRotation = Quaternion.AngleAxis(num2, this.upperArm.solverRotation * (isLeft ? this.upperArm.axis : (-this.upperArm.axis))) * this.upperArm.solverRotation;
						}
					}
					else
					{
						Vector3 point = (this.position - this.shoulder.solverPosition).normalized;
						float num3 = isLeft ? 45f : -45f;
						Quaternion quaternion2 = Quaternion.AngleAxis((isLeft ? -90f : 90f) + num3, this.chestUp) * this.chestRotation;
						Vector3 vector4 = Quaternion.Inverse(quaternion2) * point;
						float num4 = Mathf.Atan2(vector4.x, vector4.z) * 57.29578f;
						float num5 = Vector3.Dot(vector4, Vector3.up);
						num5 = 1f - Mathf.Abs(num5);
						num4 *= num5;
						num4 -= num3;
						float num6 = isLeft ? -20f : -50f;
						float num7 = isLeft ? 50f : 20f;
						num4 = this.DamperValue(num4, num6 - num3, num7 - num3, 0.7f);
						Vector3 fromDirection = this.shoulder.solverRotation * this.shoulder.axis;
						Vector3 toDirection = quaternion2 * (Quaternion.AngleAxis(num4, Vector3.up) * Vector3.forward);
						Quaternion rhs = Quaternion.FromToRotation(fromDirection, toDirection);
						quaternion2 = Quaternion.AngleAxis(isLeft ? -90f : 90f, this.chestUp) * this.chestRotation;
						quaternion2 = Quaternion.AngleAxis(isLeft ? -30f : 30f, this.chestForward) * quaternion2;
						point = this.position - (this.shoulder.solverPosition + this.chestRotation * (isLeft ? Vector3.right : Vector3.left) * base.mag);
						vector4 = Quaternion.Inverse(quaternion2) * point;
						float num8 = Mathf.Atan2(vector4.y, vector4.z) * 57.29578f;
						num8 -= -30f;
						num8 = this.DamperValue(num8, -15f, 75f, 1f);
						Quaternion quaternion3 = Quaternion.AngleAxis(-num8, quaternion2 * Vector3.right) * rhs;
						if (this.shoulderRotationWeight * this.positionWeight < 1f)
						{
							quaternion3 = Quaternion.Lerp(Quaternion.identity, quaternion3, this.shoulderRotationWeight * this.positionWeight);
						}
						IKSolverVR.VirtualBone.RotateBy(this.bones, quaternion3);
						this.Stretching();
						vector = this.GetBendNormal(this.position - this.upperArm.solverPosition);
						IKSolverVR.VirtualBone.SolveTrigonometric(this.bones, 1, 2, 3, this.position, vector, this.positionWeight);
						float angle = Mathf.Clamp(num8 * this.positionWeight * this.shoulderRotationWeight * this.shoulderTwistWeight * 2f, 0f, 180f);
						this.shoulder.solverRotation = Quaternion.AngleAxis(angle, this.shoulder.solverRotation * (isLeft ? this.shoulder.axis : (-this.shoulder.axis))) * this.shoulder.solverRotation;
						this.upperArm.solverRotation = Quaternion.AngleAxis(angle, this.upperArm.solverRotation * (isLeft ? this.upperArm.axis : (-this.upperArm.axis))) * this.upperArm.solverRotation;
					}
				}
				else
				{
					if (this.LOD < 1)
					{
						this.Stretching();
					}
					vector = this.GetBendNormal(this.position - this.upperArm.solverPosition);
					if (this.hasShoulder)
					{
						IKSolverVR.VirtualBone.SolveTrigonometric(this.bones, 1, 2, 3, this.position, vector, this.positionWeight);
					}
					else
					{
						IKSolverVR.VirtualBone.SolveTrigonometric(this.bones, 0, 1, 2, this.position, vector, this.positionWeight);
					}
				}
				if (this.LOD < 1 && this.positionWeight > 0f)
				{
					Vector3 vector5 = Quaternion.Inverse(Quaternion.LookRotation(this.upperArm.solverRotation * this.upperArmBendAxis, this.forearm.solverPosition - this.upperArm.solverPosition)) * vector;
					float num9 = Mathf.Atan2(vector5.x, vector5.z) * 57.29578f;
					this.upperArm.solverRotation = Quaternion.AngleAxis(num9 * this.positionWeight, this.forearm.solverPosition - this.upperArm.solverPosition) * this.upperArm.solverRotation;
					Quaternion quaternion4 = this.upperArm.solverRotation * this.forearmRelToUpperArm;
					Quaternion lhs = Quaternion.FromToRotation(quaternion4 * this.forearm.axis, this.hand.solverPosition - this.forearm.solverPosition);
					base.RotateTo(this.forearm, lhs * quaternion4, this.positionWeight);
				}
				if (this.rotationWeight >= 1f)
				{
					this.hand.solverRotation = this.rotation;
					return;
				}
				if (this.rotationWeight > 0f)
				{
					this.hand.solverRotation = Quaternion.Lerp(this.hand.solverRotation, this.rotation, this.rotationWeight);
				}
			}

			// Token: 0x0600109D RID: 4253 RVA: 0x00067734 File Offset: 0x00065934
			public override void ResetOffsets()
			{
				this.handPositionOffset = Vector3.zero;
			}

			// Token: 0x0600109E RID: 4254 RVA: 0x00067744 File Offset: 0x00065944
			public override void Write(ref Vector3[] solvedPositions, ref Quaternion[] solvedRotations)
			{
				if (this.hasShoulder)
				{
					solvedPositions[this.index] = this.shoulder.solverPosition;
					solvedRotations[this.index] = this.shoulder.solverRotation;
				}
				solvedPositions[this.index + 1] = this.upperArm.solverPosition;
				solvedPositions[this.index + 2] = this.forearm.solverPosition;
				solvedPositions[this.index + 3] = this.hand.solverPosition;
				solvedRotations[this.index + 1] = this.upperArm.solverRotation;
				solvedRotations[this.index + 2] = this.forearm.solverRotation;
				solvedRotations[this.index + 3] = this.hand.solverRotation;
			}

			// Token: 0x0600109F RID: 4255 RVA: 0x00067828 File Offset: 0x00065A28
			private float DamperValue(float value, float min, float max, float weight = 1f)
			{
				float num = max - min;
				if (weight < 1f)
				{
					float num2 = max - num * 0.5f;
					float num3 = value - num2;
					num3 *= 0.5f;
					value = num2 + num3;
				}
				value -= min;
				float t = Interp.Float(Mathf.Clamp(value / num, 0f, 1f), InterpolationMode.InOutQuintic);
				return Mathf.Lerp(min, max, t);
			}

			// Token: 0x060010A0 RID: 4256 RVA: 0x00067884 File Offset: 0x00065A84
			private Vector3 GetBendNormal(Vector3 dir)
			{
				if (this.bendGoal != null)
				{
					this.bendDirection = this.bendGoal.position - this.bones[1].solverPosition;
				}
				Vector3 vector = this.bones[0].solverRotation * this.bones[0].axis;
				Vector3 down = Vector3.down;
				Vector3 toDirection = Quaternion.Inverse(this.chestRotation) * dir.normalized + Vector3.forward;
				Vector3 vector2 = Quaternion.FromToRotation(down, toDirection) * Vector3.back;
				Vector3 fromDirection = Quaternion.Inverse(this.chestRotation) * vector;
				toDirection = Quaternion.Inverse(this.chestRotation) * dir;
				vector2 = Quaternion.FromToRotation(fromDirection, toDirection) * vector2;
				vector2 = this.chestRotation * vector2;
				vector2 += vector;
				vector2 -= this.rotation * this.wristToPalmAxis;
				vector2 -= this.rotation * this.palmToThumbAxis * 0.5f;
				if (this.bendGoalWeight > 0f)
				{
					vector2 = Vector3.Slerp(vector2, this.bendDirection, this.bendGoalWeight);
				}
				if (this.swivelOffset != 0f)
				{
					vector2 = Quaternion.AngleAxis(this.swivelOffset, -dir) * vector2;
				}
				return Vector3.Cross(vector2, dir);
			}

			// Token: 0x060010A1 RID: 4257 RVA: 0x000679E5 File Offset: 0x00065BE5
			private void Visualize(IKSolverVR.VirtualBone bone1, IKSolverVR.VirtualBone bone2, IKSolverVR.VirtualBone bone3, Color color)
			{
				Debug.DrawLine(bone1.solverPosition, bone2.solverPosition, color);
				Debug.DrawLine(bone2.solverPosition, bone3.solverPosition, color);
			}

			// Token: 0x060010A2 RID: 4258 RVA: 0x00067A10 File Offset: 0x00065C10
			public Arm()
			{
			}

			// Token: 0x04000EF3 RID: 3827
			[Tooltip("The hand target. This should not be the hand controller itself, but a child GameObject parented to it so you could adjust it's position/rotation to match the orientation of the hand bone. The best practice for setup would be to move the hand controller to the avatar's hand as it it was held by the avatar, duplicate the avatar's hand bone and parent it to the hand controller. Then assign the duplicate to this slot.")]
			public Transform target;

			// Token: 0x04000EF4 RID: 3828
			[Tooltip("The elbow will be bent towards this Transform if 'Bend Goal Weight' > 0.")]
			public Transform bendGoal;

			// Token: 0x04000EF5 RID: 3829
			[Tooltip("Positional weight of the hand target. Note that if you have nulled the target, the hand will still be pulled to the last position of the target until you set this value to 0.")]
			[Range(0f, 1f)]
			public float positionWeight = 1f;

			// Token: 0x04000EF6 RID: 3830
			[Tooltip("Rotational weight of the hand target. Note that if you have nulled the target, the hand will still be rotated to the last rotation of the target until you set this value to 0.")]
			[Range(0f, 1f)]
			public float rotationWeight = 1f;

			// Token: 0x04000EF7 RID: 3831
			[Tooltip("Different techniques for shoulder bone rotation.")]
			public IKSolverVR.Arm.ShoulderRotationMode shoulderRotationMode;

			// Token: 0x04000EF8 RID: 3832
			[Tooltip("The weight of shoulder rotation")]
			[Range(0f, 1f)]
			public float shoulderRotationWeight = 1f;

			// Token: 0x04000EF9 RID: 3833
			[Tooltip("The weight of twisting the shoulders backwards when arms are lifted up.")]
			[Range(0f, 1f)]
			public float shoulderTwistWeight = 1f;

			// Token: 0x04000EFA RID: 3834
			[Tooltip("If greater than 0, will bend the elbow towards the 'Bend Goal' Transform.")]
			[Range(0f, 1f)]
			public float bendGoalWeight;

			// Token: 0x04000EFB RID: 3835
			[Tooltip("Angular offset of the elbow bending direction.")]
			[Range(-180f, 180f)]
			public float swivelOffset;

			// Token: 0x04000EFC RID: 3836
			[Tooltip("Local axis of the hand bone that points from the wrist towards the palm. Used for defining hand bone orientation. If you have copied VRIK component from another avatar that has different bone orientations, right-click on VRIK header and select 'Guess Hand Orientations' from the context menu.")]
			public Vector3 wristToPalmAxis = Vector3.zero;

			// Token: 0x04000EFD RID: 3837
			[Tooltip("Local axis of the hand bone that points from the palm towards the thumb. Used for defining hand bone orientation. If you have copied VRIK component from another avatar that has different bone orientations, right-click on VRIK header and select 'Guess Hand Orientations' from the context menu.")]
			public Vector3 palmToThumbAxis = Vector3.zero;

			// Token: 0x04000EFE RID: 3838
			[Tooltip("Use this to make the arm shorter/longer. Works by displacement of hand and forearm localPosition.")]
			[Range(0.01f, 2f)]
			public float armLengthMlp = 1f;

			// Token: 0x04000EFF RID: 3839
			[Tooltip("Evaluates stretching of the arm by target distance relative to arm length. Value at time 1 represents stretching amount at the point where distance to the target is equal to arm length. Value at time 2 represents stretching amount at the point where distance to the target is double the arm length. Value represents the amount of stretching. Linear stretching would be achieved with a linear curve going up by 45 degrees. Increase the range of stretching by moving the last key up and right at the same amount. Smoothing in the curve can help reduce elbow snapping (start stretching the arm slightly before target distance reaches arm length). To get a good optimal value for this curve, please go to the 'VRIK (Basic)' demo scene and copy the stretch curve over from the Pilot character.")]
			public AnimationCurve stretchCurve = new AnimationCurve();

			// Token: 0x04000F00 RID: 3840
			[HideInInspector]
			[NonSerialized]
			public Vector3 IKPosition;

			// Token: 0x04000F01 RID: 3841
			[HideInInspector]
			[NonSerialized]
			public Quaternion IKRotation = Quaternion.identity;

			// Token: 0x04000F02 RID: 3842
			[HideInInspector]
			[NonSerialized]
			public Vector3 bendDirection = Vector3.back;

			// Token: 0x04000F03 RID: 3843
			[HideInInspector]
			[NonSerialized]
			public Vector3 handPositionOffset;

			// Token: 0x04000F04 RID: 3844
			[CompilerGenerated]
			private Vector3 <position>k__BackingField;

			// Token: 0x04000F05 RID: 3845
			[CompilerGenerated]
			private Quaternion <rotation>k__BackingField;

			// Token: 0x04000F06 RID: 3846
			private bool hasShoulder;

			// Token: 0x04000F07 RID: 3847
			private Vector3 chestForwardAxis;

			// Token: 0x04000F08 RID: 3848
			private Vector3 chestUpAxis;

			// Token: 0x04000F09 RID: 3849
			private Quaternion chestRotation = Quaternion.identity;

			// Token: 0x04000F0A RID: 3850
			private Vector3 chestForward;

			// Token: 0x04000F0B RID: 3851
			private Vector3 chestUp;

			// Token: 0x04000F0C RID: 3852
			private Quaternion forearmRelToUpperArm = Quaternion.identity;

			// Token: 0x04000F0D RID: 3853
			private Vector3 upperArmBendAxis;

			// Token: 0x04000F0E RID: 3854
			private const float yawOffsetAngle = 45f;

			// Token: 0x04000F0F RID: 3855
			private const float pitchOffsetAngle = -30f;

			// Token: 0x02000247 RID: 583
			[Serializable]
			public enum ShoulderRotationMode
			{
				// Token: 0x040010F7 RID: 4343
				YawPitch,
				// Token: 0x040010F8 RID: 4344
				FromTo
			}
		}

		// Token: 0x020001FE RID: 510
		[Serializable]
		public abstract class BodyPart
		{
			// Token: 0x060010A3 RID: 4259
			protected abstract void OnRead(Vector3[] positions, Quaternion[] rotations, bool hasChest, bool hasNeck, bool hasShoulders, bool hasToes, bool hasLegs, int rootIndex, int index);

			// Token: 0x060010A4 RID: 4260
			public abstract void PreSolve();

			// Token: 0x060010A5 RID: 4261
			public abstract void Write(ref Vector3[] solvedPositions, ref Quaternion[] solvedRotations);

			// Token: 0x060010A6 RID: 4262
			public abstract void ApplyOffsets(float scale);

			// Token: 0x060010A7 RID: 4263
			public abstract void ResetOffsets();

			// Token: 0x17000229 RID: 553
			// (get) Token: 0x060010A8 RID: 4264 RVA: 0x00067AA7 File Offset: 0x00065CA7
			// (set) Token: 0x060010A9 RID: 4265 RVA: 0x00067AAF File Offset: 0x00065CAF
			public float sqrMag
			{
				[CompilerGenerated]
				get
				{
					return this.<sqrMag>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<sqrMag>k__BackingField = value;
				}
			}

			// Token: 0x1700022A RID: 554
			// (get) Token: 0x060010AA RID: 4266 RVA: 0x00067AB8 File Offset: 0x00065CB8
			// (set) Token: 0x060010AB RID: 4267 RVA: 0x00067AC0 File Offset: 0x00065CC0
			public float mag
			{
				[CompilerGenerated]
				get
				{
					return this.<mag>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<mag>k__BackingField = value;
				}
			}

			// Token: 0x060010AC RID: 4268 RVA: 0x00067AC9 File Offset: 0x00065CC9
			public void SetLOD(int LOD)
			{
				this.LOD = LOD;
			}

			// Token: 0x060010AD RID: 4269 RVA: 0x00067AD4 File Offset: 0x00065CD4
			public void Read(Vector3[] positions, Quaternion[] rotations, bool hasChest, bool hasNeck, bool hasShoulders, bool hasToes, bool hasLegs, int rootIndex, int index)
			{
				this.index = index;
				this.rootPosition = positions[rootIndex];
				this.rootRotation = rotations[rootIndex];
				this.OnRead(positions, rotations, hasChest, hasNeck, hasShoulders, hasToes, hasLegs, rootIndex, index);
				this.mag = IKSolverVR.VirtualBone.PreSolve(ref this.bones);
				this.sqrMag = this.mag * this.mag;
				this.initiated = true;
			}

			// Token: 0x060010AE RID: 4270 RVA: 0x00067B48 File Offset: 0x00065D48
			public void MovePosition(Vector3 position)
			{
				Vector3 b = position - this.bones[0].solverPosition;
				IKSolverVR.VirtualBone[] array = this.bones;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].solverPosition += b;
				}
			}

			// Token: 0x060010AF RID: 4271 RVA: 0x00067B94 File Offset: 0x00065D94
			public void MoveRotation(Quaternion rotation)
			{
				Quaternion rotation2 = QuaTools.FromToRotation(this.bones[0].solverRotation, rotation);
				IKSolverVR.VirtualBone.RotateAroundPoint(this.bones, 0, this.bones[0].solverPosition, rotation2);
			}

			// Token: 0x060010B0 RID: 4272 RVA: 0x00067BCF File Offset: 0x00065DCF
			public void Translate(Vector3 position, Quaternion rotation)
			{
				this.MovePosition(position);
				this.MoveRotation(rotation);
			}

			// Token: 0x060010B1 RID: 4273 RVA: 0x00067BE0 File Offset: 0x00065DE0
			public void TranslateRoot(Vector3 newRootPos, Quaternion newRootRot)
			{
				Vector3 b = newRootPos - this.rootPosition;
				this.rootPosition = newRootPos;
				IKSolverVR.VirtualBone[] array = this.bones;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].solverPosition += b;
				}
				Quaternion rotation = QuaTools.FromToRotation(this.rootRotation, newRootRot);
				this.rootRotation = newRootRot;
				IKSolverVR.VirtualBone.RotateAroundPoint(this.bones, 0, newRootPos, rotation);
			}

			// Token: 0x060010B2 RID: 4274 RVA: 0x00067C4C File Offset: 0x00065E4C
			public void RotateTo(IKSolverVR.VirtualBone bone, Quaternion rotation, float weight = 1f)
			{
				if (weight <= 0f)
				{
					return;
				}
				Quaternion quaternion = QuaTools.FromToRotation(bone.solverRotation, rotation);
				if (weight < 1f)
				{
					quaternion = Quaternion.Slerp(Quaternion.identity, quaternion, weight);
				}
				for (int i = 0; i < this.bones.Length; i++)
				{
					if (this.bones[i] == bone)
					{
						IKSolverVR.VirtualBone.RotateAroundPoint(this.bones, i, this.bones[i].solverPosition, quaternion);
						return;
					}
				}
			}

			// Token: 0x060010B3 RID: 4275 RVA: 0x00067CC0 File Offset: 0x00065EC0
			public void Visualize(Color color)
			{
				for (int i = 0; i < this.bones.Length - 1; i++)
				{
					Debug.DrawLine(this.bones[i].solverPosition, this.bones[i + 1].solverPosition, color);
				}
			}

			// Token: 0x060010B4 RID: 4276 RVA: 0x00067D04 File Offset: 0x00065F04
			public void Visualize()
			{
				this.Visualize(Color.white);
			}

			// Token: 0x060010B5 RID: 4277 RVA: 0x00067D11 File Offset: 0x00065F11
			protected BodyPart()
			{
			}

			// Token: 0x04000F10 RID: 3856
			[CompilerGenerated]
			private float <sqrMag>k__BackingField;

			// Token: 0x04000F11 RID: 3857
			[CompilerGenerated]
			private float <mag>k__BackingField;

			// Token: 0x04000F12 RID: 3858
			[HideInInspector]
			public IKSolverVR.VirtualBone[] bones = new IKSolverVR.VirtualBone[0];

			// Token: 0x04000F13 RID: 3859
			protected bool initiated;

			// Token: 0x04000F14 RID: 3860
			protected Vector3 rootPosition;

			// Token: 0x04000F15 RID: 3861
			protected Quaternion rootRotation = Quaternion.identity;

			// Token: 0x04000F16 RID: 3862
			protected int index = -1;

			// Token: 0x04000F17 RID: 3863
			protected int LOD;
		}

		// Token: 0x020001FF RID: 511
		[Serializable]
		public class Footstep
		{
			// Token: 0x1700022B RID: 555
			// (get) Token: 0x060010B6 RID: 4278 RVA: 0x00067D37 File Offset: 0x00065F37
			public bool isStepping
			{
				get
				{
					return this.stepProgress < 1f;
				}
			}

			// Token: 0x1700022C RID: 556
			// (get) Token: 0x060010B7 RID: 4279 RVA: 0x00067D46 File Offset: 0x00065F46
			// (set) Token: 0x060010B8 RID: 4280 RVA: 0x00067D4E File Offset: 0x00065F4E
			public float stepProgress
			{
				[CompilerGenerated]
				get
				{
					return this.<stepProgress>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<stepProgress>k__BackingField = value;
				}
			}

			// Token: 0x060010B9 RID: 4281 RVA: 0x00067D58 File Offset: 0x00065F58
			public Footstep(Quaternion rootRotation, Vector3 footPosition, Quaternion footRotation, Vector3 characterSpaceOffset)
			{
				this.characterSpaceOffset = characterSpaceOffset;
				this.Reset(rootRotation, footPosition, footRotation);
				this.footRelativeToRoot = Quaternion.Inverse(rootRotation) * this.rotation;
			}

			// Token: 0x060010BA RID: 4282 RVA: 0x00067DD8 File Offset: 0x00065FD8
			public void Reset(Quaternion rootRotation, Vector3 footPosition, Quaternion footRotation)
			{
				this.position = footPosition;
				this.rotation = footRotation;
				this.stepFrom = this.position;
				this.stepTo = this.position;
				this.stepFromRot = this.rotation;
				this.stepToRot = this.rotation;
				this.stepToRootRot = rootRotation;
				this.stepProgress = 1f;
			}

			// Token: 0x060010BB RID: 4283 RVA: 0x00067E38 File Offset: 0x00066038
			public void StepTo(Vector3 p, Quaternion rootRotation, float stepThreshold)
			{
				if (this.relaxFlag)
				{
					stepThreshold = 0f;
					this.relaxFlag = false;
				}
				if (Vector3.Magnitude(p - this.stepTo) < stepThreshold && Quaternion.Angle(rootRotation, this.stepToRootRot) < 25f)
				{
					return;
				}
				this.stepFrom = this.position;
				this.stepTo = p;
				this.stepFromRot = this.rotation;
				this.stepToRootRot = rootRotation;
				this.stepToRot = rootRotation * this.footRelativeToRoot;
				this.stepProgress = 0f;
			}

			// Token: 0x060010BC RID: 4284 RVA: 0x00067EC8 File Offset: 0x000660C8
			public void UpdateStepping(Vector3 p, Quaternion rootRotation, float speed)
			{
				this.stepTo = Vector3.Lerp(this.stepTo, p, Time.deltaTime * speed);
				this.stepToRot = Quaternion.Lerp(this.stepToRot, rootRotation * this.footRelativeToRoot, Time.deltaTime * speed);
				this.stepToRootRot = this.stepToRot * Quaternion.Inverse(this.footRelativeToRoot);
			}

			// Token: 0x060010BD RID: 4285 RVA: 0x00067F30 File Offset: 0x00066130
			public void UpdateStanding(Quaternion rootRotation, float minAngle, float speed)
			{
				if (speed <= 0f || minAngle >= 180f)
				{
					return;
				}
				Quaternion quaternion = rootRotation * this.footRelativeToRoot;
				float num = Quaternion.Angle(this.rotation, quaternion);
				if (num > minAngle)
				{
					this.rotation = Quaternion.RotateTowards(this.rotation, quaternion, Mathf.Min(Time.deltaTime * speed * (1f - this.supportLegW), num - minAngle));
				}
			}

			// Token: 0x060010BE RID: 4286 RVA: 0x00067F9C File Offset: 0x0006619C
			public void Update(InterpolationMode interpolation, UnityEvent onStep)
			{
				float target = this.isSupportLeg ? 1f : 0f;
				this.supportLegW = Mathf.SmoothDamp(this.supportLegW, target, ref this.supportLegWV, 0.2f);
				if (!this.isStepping)
				{
					return;
				}
				this.stepProgress = Mathf.MoveTowards(this.stepProgress, 1f, Time.deltaTime * this.stepSpeed);
				if (this.stepProgress >= 1f)
				{
					onStep.Invoke();
				}
				float t = Interp.Float(this.stepProgress, interpolation);
				this.position = Vector3.Lerp(this.stepFrom, this.stepTo, t);
				this.rotation = Quaternion.Lerp(this.stepFromRot, this.stepToRot, t);
			}

			// Token: 0x04000F18 RID: 3864
			public float stepSpeed = 3f;

			// Token: 0x04000F19 RID: 3865
			public Vector3 characterSpaceOffset;

			// Token: 0x04000F1A RID: 3866
			public Vector3 position;

			// Token: 0x04000F1B RID: 3867
			public Quaternion rotation = Quaternion.identity;

			// Token: 0x04000F1C RID: 3868
			public Quaternion stepToRootRot = Quaternion.identity;

			// Token: 0x04000F1D RID: 3869
			public bool isSupportLeg;

			// Token: 0x04000F1E RID: 3870
			public bool relaxFlag;

			// Token: 0x04000F1F RID: 3871
			[CompilerGenerated]
			private float <stepProgress>k__BackingField;

			// Token: 0x04000F20 RID: 3872
			public Vector3 stepFrom;

			// Token: 0x04000F21 RID: 3873
			public Vector3 stepTo;

			// Token: 0x04000F22 RID: 3874
			public Quaternion stepFromRot = Quaternion.identity;

			// Token: 0x04000F23 RID: 3875
			public Quaternion stepToRot = Quaternion.identity;

			// Token: 0x04000F24 RID: 3876
			private Quaternion footRelativeToRoot = Quaternion.identity;

			// Token: 0x04000F25 RID: 3877
			private float supportLegW;

			// Token: 0x04000F26 RID: 3878
			private float supportLegWV;
		}

		// Token: 0x02000200 RID: 512
		[Serializable]
		public class Leg : IKSolverVR.BodyPart
		{
			// Token: 0x1700022D RID: 557
			// (get) Token: 0x060010BF RID: 4287 RVA: 0x00068056 File Offset: 0x00066256
			// (set) Token: 0x060010C0 RID: 4288 RVA: 0x0006805E File Offset: 0x0006625E
			public Vector3 position
			{
				[CompilerGenerated]
				get
				{
					return this.<position>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<position>k__BackingField = value;
				}
			}

			// Token: 0x1700022E RID: 558
			// (get) Token: 0x060010C1 RID: 4289 RVA: 0x00068067 File Offset: 0x00066267
			// (set) Token: 0x060010C2 RID: 4290 RVA: 0x0006806F File Offset: 0x0006626F
			public Quaternion rotation
			{
				[CompilerGenerated]
				get
				{
					return this.<rotation>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<rotation>k__BackingField = value;
				}
			}

			// Token: 0x1700022F RID: 559
			// (get) Token: 0x060010C3 RID: 4291 RVA: 0x00068078 File Offset: 0x00066278
			// (set) Token: 0x060010C4 RID: 4292 RVA: 0x00068080 File Offset: 0x00066280
			public bool hasToes
			{
				[CompilerGenerated]
				get
				{
					return this.<hasToes>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<hasToes>k__BackingField = value;
				}
			}

			// Token: 0x17000230 RID: 560
			// (get) Token: 0x060010C5 RID: 4293 RVA: 0x00068089 File Offset: 0x00066289
			public IKSolverVR.VirtualBone thigh
			{
				get
				{
					return this.bones[0];
				}
			}

			// Token: 0x17000231 RID: 561
			// (get) Token: 0x060010C6 RID: 4294 RVA: 0x00068093 File Offset: 0x00066293
			private IKSolverVR.VirtualBone calf
			{
				get
				{
					return this.bones[1];
				}
			}

			// Token: 0x17000232 RID: 562
			// (get) Token: 0x060010C7 RID: 4295 RVA: 0x0006809D File Offset: 0x0006629D
			private IKSolverVR.VirtualBone foot
			{
				get
				{
					return this.bones[2];
				}
			}

			// Token: 0x17000233 RID: 563
			// (get) Token: 0x060010C8 RID: 4296 RVA: 0x000680A7 File Offset: 0x000662A7
			private IKSolverVR.VirtualBone toes
			{
				get
				{
					return this.bones[3];
				}
			}

			// Token: 0x17000234 RID: 564
			// (get) Token: 0x060010C9 RID: 4297 RVA: 0x000680B1 File Offset: 0x000662B1
			public IKSolverVR.VirtualBone lastBone
			{
				get
				{
					return this.bones[this.bones.Length - 1];
				}
			}

			// Token: 0x17000235 RID: 565
			// (get) Token: 0x060010CA RID: 4298 RVA: 0x000680C4 File Offset: 0x000662C4
			// (set) Token: 0x060010CB RID: 4299 RVA: 0x000680CC File Offset: 0x000662CC
			public Vector3 thighRelativeToPelvis
			{
				[CompilerGenerated]
				get
				{
					return this.<thighRelativeToPelvis>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<thighRelativeToPelvis>k__BackingField = value;
				}
			}

			// Token: 0x060010CC RID: 4300 RVA: 0x000680D8 File Offset: 0x000662D8
			protected override void OnRead(Vector3[] positions, Quaternion[] rotations, bool hasChest, bool hasNeck, bool hasShoulders, bool hasToes, bool hasLegs, int rootIndex, int index)
			{
				Vector3 vector = positions[index];
				Quaternion rotation = rotations[index];
				Vector3 vector2 = positions[index + 1];
				Quaternion rotation2 = rotations[index + 1];
				Vector3 vector3 = positions[index + 2];
				Quaternion quaternion = rotations[index + 2];
				Vector3 vector4 = positions[index + 3];
				Quaternion quaternion2 = rotations[index + 3];
				if (!this.initiated)
				{
					this.hasToes = hasToes;
					this.bones = new IKSolverVR.VirtualBone[hasToes ? 4 : 3];
					if (hasToes)
					{
						this.bones[0] = new IKSolverVR.VirtualBone(vector, rotation);
						this.bones[1] = new IKSolverVR.VirtualBone(vector2, rotation2);
						this.bones[2] = new IKSolverVR.VirtualBone(vector3, quaternion);
						this.bones[3] = new IKSolverVR.VirtualBone(vector4, quaternion2);
						this.IKPosition = vector4;
						this.IKRotation = quaternion2;
					}
					else
					{
						this.bones[0] = new IKSolverVR.VirtualBone(vector, rotation);
						this.bones[1] = new IKSolverVR.VirtualBone(vector2, rotation2);
						this.bones[2] = new IKSolverVR.VirtualBone(vector3, quaternion);
						this.IKPosition = vector3;
						this.IKRotation = quaternion;
					}
					this.bendNormal = Vector3.Cross(vector2 - vector, vector3 - vector2);
					this.bendNormalRelToPelvis = Quaternion.Inverse(this.rootRotation) * this.bendNormal;
					this.bendNormalRelToTarget = Quaternion.Inverse(this.IKRotation) * this.bendNormal;
					this.rotation = this.IKRotation;
				}
				if (hasToes)
				{
					this.bones[0].Read(vector, rotation);
					this.bones[1].Read(vector2, rotation2);
					this.bones[2].Read(vector3, quaternion);
					this.bones[3].Read(vector4, quaternion2);
					return;
				}
				this.bones[0].Read(vector, rotation);
				this.bones[1].Read(vector2, rotation2);
				this.bones[2].Read(vector3, quaternion);
			}

			// Token: 0x060010CD RID: 4301 RVA: 0x000682CC File Offset: 0x000664CC
			public override void PreSolve()
			{
				if (this.target != null)
				{
					this.IKPosition = this.target.position;
					this.IKRotation = this.target.rotation;
				}
				this.footPosition = this.foot.solverPosition;
				this.footRotation = this.foot.solverRotation;
				this.position = this.lastBone.solverPosition;
				this.rotation = this.lastBone.solverRotation;
				if (this.rotationWeight > 0f)
				{
					this.ApplyRotationOffset(QuaTools.FromToRotation(this.rotation, this.IKRotation), this.rotationWeight);
				}
				if (this.positionWeight > 0f)
				{
					this.ApplyPositionOffset(this.IKPosition - this.position, this.positionWeight);
				}
				this.thighRelativeToPelvis = Quaternion.Inverse(this.rootRotation) * (this.thigh.solverPosition - this.rootPosition);
				this.calfRelToThigh = Quaternion.Inverse(this.thigh.solverRotation) * this.calf.solverRotation;
				this.thighRelToFoot = Quaternion.Inverse(this.lastBone.solverRotation) * this.thigh.solverRotation;
				if (this.useAnimatedBendNormal)
				{
					this.bendNormal = Vector3.Cross(this.calf.solverPosition - this.thigh.solverPosition, this.foot.solverPosition - this.calf.solverPosition);
				}
				else if (this.bendToTargetWeight <= 0f)
				{
					this.bendNormal = this.rootRotation * this.bendNormalRelToPelvis;
				}
				else if (this.bendToTargetWeight >= 1f)
				{
					this.bendNormal = this.rotation * this.bendNormalRelToTarget;
				}
				else
				{
					this.bendNormal = Vector3.Slerp(this.rootRotation * this.bendNormalRelToPelvis, this.rotation * this.bendNormalRelToTarget, this.bendToTargetWeight);
				}
				this.bendNormal = this.bendNormal.normalized;
			}

			// Token: 0x060010CE RID: 4302 RVA: 0x000684F4 File Offset: 0x000666F4
			public override void ApplyOffsets(float scale)
			{
				this.ApplyPositionOffset(this.footPositionOffset, 1f);
				this.ApplyRotationOffset(this.footRotationOffset, 1f);
				Quaternion quaternion = Quaternion.FromToRotation(this.footPosition - this.position, this.footPosition + this.heelPositionOffset - this.position);
				this.footPosition = this.position + quaternion * (this.footPosition - this.position);
				this.footRotation = quaternion * this.footRotation;
				float num = 0f;
				if (this.bendGoal != null && this.bendGoalWeight > 0f)
				{
					Vector3 point = Vector3.Cross(this.bendGoal.position - this.thigh.solverPosition, this.position - this.thigh.solverPosition);
					Vector3 vector = Quaternion.Inverse(Quaternion.LookRotation(this.bendNormal, this.thigh.solverPosition - this.foot.solverPosition)) * point;
					num = Mathf.Atan2(vector.x, vector.z) * 57.29578f * this.bendGoalWeight;
				}
				float num2 = this.swivelOffset + num;
				if (num2 != 0f)
				{
					this.bendNormal = Quaternion.AngleAxis(num2, this.thigh.solverPosition - this.lastBone.solverPosition) * this.bendNormal;
					this.thigh.solverRotation = Quaternion.AngleAxis(-num2, this.thigh.solverRotation * this.thigh.axis) * this.thigh.solverRotation;
				}
			}

			// Token: 0x060010CF RID: 4303 RVA: 0x000686C2 File Offset: 0x000668C2
			private void ApplyPositionOffset(Vector3 offset, float weight)
			{
				if (weight <= 0f)
				{
					return;
				}
				offset *= weight;
				this.footPosition += offset;
				this.position += offset;
			}

			// Token: 0x060010D0 RID: 4304 RVA: 0x000686FC File Offset: 0x000668FC
			private void ApplyRotationOffset(Quaternion offset, float weight)
			{
				if (weight <= 0f)
				{
					return;
				}
				if (weight < 1f)
				{
					offset = Quaternion.Lerp(Quaternion.identity, offset, weight);
				}
				this.footRotation = offset * this.footRotation;
				this.rotation = offset * this.rotation;
				this.bendNormal = offset * this.bendNormal;
				this.footPosition = this.position + offset * (this.footPosition - this.position);
			}

			// Token: 0x060010D1 RID: 4305 RVA: 0x00068788 File Offset: 0x00066988
			public void Solve(bool stretch)
			{
				if (stretch && this.LOD < 1)
				{
					this.Stretching();
				}
				IKSolverVR.VirtualBone.SolveTrigonometric(this.bones, 0, 1, 2, this.footPosition, this.bendNormal, 1f);
				base.RotateTo(this.foot, this.footRotation, 1f);
				if (!this.hasToes)
				{
					this.FixTwistRotations();
					return;
				}
				Vector3 normalized = Vector3.Cross(this.foot.solverPosition - this.thigh.solverPosition, this.toes.solverPosition - this.foot.solverPosition).normalized;
				IKSolverVR.VirtualBone.SolveTrigonometric(this.bones, 0, 2, 3, this.position, normalized, 1f);
				this.FixTwistRotations();
				this.toes.solverRotation = this.rotation;
			}

			// Token: 0x060010D2 RID: 4306 RVA: 0x00068864 File Offset: 0x00066A64
			private void FixTwistRotations()
			{
				if (this.LOD < 1)
				{
					if (this.bendToTargetWeight > 0f)
					{
						Quaternion quaternion = this.rotation * this.thighRelToFoot;
						Quaternion lhs = Quaternion.FromToRotation(quaternion * this.thigh.axis, this.calf.solverPosition - this.thigh.solverPosition);
						if (this.bendToTargetWeight < 1f)
						{
							this.thigh.solverRotation = Quaternion.Slerp(this.thigh.solverRotation, lhs * quaternion, this.bendToTargetWeight);
						}
						else
						{
							this.thigh.solverRotation = lhs * quaternion;
						}
					}
					Quaternion quaternion2 = this.thigh.solverRotation * this.calfRelToThigh;
					Quaternion lhs2 = Quaternion.FromToRotation(quaternion2 * this.calf.axis, this.foot.solverPosition - this.calf.solverPosition);
					this.calf.solverRotation = lhs2 * quaternion2;
				}
			}

			// Token: 0x060010D3 RID: 4307 RVA: 0x00068978 File Offset: 0x00066B78
			private void Stretching()
			{
				float num = this.thigh.length + this.calf.length;
				Vector3 vector = Vector3.zero;
				Vector3 b = Vector3.zero;
				if (this.legLengthMlp != 1f)
				{
					num *= this.legLengthMlp;
					vector = (this.calf.solverPosition - this.thigh.solverPosition) * (this.legLengthMlp - 1f) * this.positionWeight;
					b = (this.foot.solverPosition - this.calf.solverPosition) * (this.legLengthMlp - 1f) * this.positionWeight;
					this.calf.solverPosition += vector;
					this.foot.solverPosition += vector + b;
					if (this.hasToes)
					{
						this.toes.solverPosition += vector + b;
					}
				}
				float time = Vector3.Distance(this.thigh.solverPosition, this.footPosition) / num;
				float d = this.stretchCurve.Evaluate(time) * this.positionWeight;
				vector = (this.calf.solverPosition - this.thigh.solverPosition) * d;
				b = (this.foot.solverPosition - this.calf.solverPosition) * d;
				this.calf.solverPosition += vector;
				this.foot.solverPosition += vector + b;
				if (this.hasToes)
				{
					this.toes.solverPosition += vector + b;
				}
			}

			// Token: 0x060010D4 RID: 4308 RVA: 0x00068B58 File Offset: 0x00066D58
			public override void Write(ref Vector3[] solvedPositions, ref Quaternion[] solvedRotations)
			{
				solvedRotations[this.index] = this.thigh.solverRotation;
				solvedRotations[this.index + 1] = this.calf.solverRotation;
				solvedRotations[this.index + 2] = this.foot.solverRotation;
				solvedPositions[this.index] = this.thigh.solverPosition;
				solvedPositions[this.index + 1] = this.calf.solverPosition;
				solvedPositions[this.index + 2] = this.foot.solverPosition;
				if (this.hasToes)
				{
					solvedRotations[this.index + 3] = this.toes.solverRotation;
					solvedPositions[this.index + 3] = this.toes.solverPosition;
				}
			}

			// Token: 0x060010D5 RID: 4309 RVA: 0x00068C39 File Offset: 0x00066E39
			public override void ResetOffsets()
			{
				this.footPositionOffset = Vector3.zero;
				this.footRotationOffset = Quaternion.identity;
				this.heelPositionOffset = Vector3.zero;
			}

			// Token: 0x060010D6 RID: 4310 RVA: 0x00068C5C File Offset: 0x00066E5C
			public Leg()
			{
			}

			// Token: 0x04000F27 RID: 3879
			[Tooltip("The foot/toe target. This should not be the foot tracker itself, but a child GameObject parented to it so you could adjust it's position/rotation to match the orientation of the foot/toe bone. If a toe bone is assigned in the References, the solver will match the toe bone to this target. If no toe bone assigned, foot bone will be used instead.")]
			public Transform target;

			// Token: 0x04000F28 RID: 3880
			[Tooltip("The knee will be bent towards this Transform if 'Bend Goal Weight' > 0.")]
			public Transform bendGoal;

			// Token: 0x04000F29 RID: 3881
			[Tooltip("Positional weight of the toe/foot target. Note that if you have nulled the target, the foot will still be pulled to the last position of the target until you set this value to 0.")]
			[Range(0f, 1f)]
			public float positionWeight;

			// Token: 0x04000F2A RID: 3882
			[Tooltip("Rotational weight of the toe/foot target. Note that if you have nulled the target, the foot will still be rotated to the last rotation of the target until you set this value to 0.")]
			[Range(0f, 1f)]
			public float rotationWeight;

			// Token: 0x04000F2B RID: 3883
			[Tooltip("If greater than 0, will bend the knee towards the 'Bend Goal' Transform.")]
			[Range(0f, 1f)]
			public float bendGoalWeight;

			// Token: 0x04000F2C RID: 3884
			[Tooltip("Angular offset of knee bending direction.")]
			[Range(-180f, 180f)]
			public float swivelOffset;

			// Token: 0x04000F2D RID: 3885
			[Tooltip("If 0, the bend plane will be locked to the rotation of the pelvis and rotating the foot will have no effect on the knee direction. If 1, to the target rotation of the leg so that the knee will bend towards the forward axis of the foot. Values in between will be slerped between the two.")]
			[Range(0f, 1f)]
			public float bendToTargetWeight = 0.5f;

			// Token: 0x04000F2E RID: 3886
			[Tooltip("Use this to make the leg shorter/longer. Works by displacement of foot and calf localPosition.")]
			[Range(0.01f, 2f)]
			public float legLengthMlp = 1f;

			// Token: 0x04000F2F RID: 3887
			[Tooltip("Evaluates stretching of the leg by target distance relative to leg length. Value at time 1 represents stretching amount at the point where distance to the target is equal to leg length. Value at time 1 represents stretching amount at the point where distance to the target is double the leg length. Value represents the amount of stretching. Linear stretching would be achieved with a linear curve going up by 45 degrees. Increase the range of stretching by moving the last key up and right at the same amount. Smoothing in the curve can help reduce knee snapping (start stretching the arm slightly before target distance reaches leg length). To get a good optimal value for this curve, please go to the 'VRIK (Basic)' demo scene and copy the stretch curve over from the Pilot character.")]
			public AnimationCurve stretchCurve = new AnimationCurve();

			// Token: 0x04000F30 RID: 3888
			[HideInInspector]
			[NonSerialized]
			public Vector3 IKPosition;

			// Token: 0x04000F31 RID: 3889
			[HideInInspector]
			[NonSerialized]
			public Quaternion IKRotation = Quaternion.identity;

			// Token: 0x04000F32 RID: 3890
			[HideInInspector]
			[NonSerialized]
			public Vector3 footPositionOffset;

			// Token: 0x04000F33 RID: 3891
			[HideInInspector]
			[NonSerialized]
			public Vector3 heelPositionOffset;

			// Token: 0x04000F34 RID: 3892
			[HideInInspector]
			[NonSerialized]
			public Quaternion footRotationOffset = Quaternion.identity;

			// Token: 0x04000F35 RID: 3893
			[HideInInspector]
			[NonSerialized]
			public float currentMag;

			// Token: 0x04000F36 RID: 3894
			[HideInInspector]
			public bool useAnimatedBendNormal;

			// Token: 0x04000F37 RID: 3895
			[CompilerGenerated]
			private Vector3 <position>k__BackingField;

			// Token: 0x04000F38 RID: 3896
			[CompilerGenerated]
			private Quaternion <rotation>k__BackingField;

			// Token: 0x04000F39 RID: 3897
			[CompilerGenerated]
			private bool <hasToes>k__BackingField;

			// Token: 0x04000F3A RID: 3898
			[CompilerGenerated]
			private Vector3 <thighRelativeToPelvis>k__BackingField;

			// Token: 0x04000F3B RID: 3899
			private Vector3 footPosition;

			// Token: 0x04000F3C RID: 3900
			private Quaternion footRotation = Quaternion.identity;

			// Token: 0x04000F3D RID: 3901
			private Vector3 bendNormal;

			// Token: 0x04000F3E RID: 3902
			private Quaternion calfRelToThigh = Quaternion.identity;

			// Token: 0x04000F3F RID: 3903
			private Quaternion thighRelToFoot = Quaternion.identity;

			// Token: 0x04000F40 RID: 3904
			private Vector3 bendNormalRelToPelvis;

			// Token: 0x04000F41 RID: 3905
			private Vector3 bendNormalRelToTarget;
		}

		// Token: 0x02000201 RID: 513
		[Serializable]
		public class Locomotion
		{
			// Token: 0x17000236 RID: 566
			// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00068CC7 File Offset: 0x00066EC7
			// (set) Token: 0x060010D8 RID: 4312 RVA: 0x00068CCF File Offset: 0x00066ECF
			public Vector3 centerOfMass
			{
				[CompilerGenerated]
				get
				{
					return this.<centerOfMass>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<centerOfMass>k__BackingField = value;
				}
			}

			// Token: 0x060010D9 RID: 4313 RVA: 0x00068CD8 File Offset: 0x00066ED8
			public void Initiate(Vector3[] positions, Quaternion[] rotations, bool hasToes, float scale)
			{
				this.leftFootIndex = (hasToes ? 17 : 16);
				this.rightFootIndex = (hasToes ? 21 : 20);
				this.footsteps = new IKSolverVR.Footstep[]
				{
					new IKSolverVR.Footstep(rotations[0], positions[this.leftFootIndex], rotations[this.leftFootIndex], this.footDistance * scale * Vector3.left),
					new IKSolverVR.Footstep(rotations[0], positions[this.rightFootIndex], rotations[this.rightFootIndex], this.footDistance * scale * Vector3.right)
				};
			}

			// Token: 0x060010DA RID: 4314 RVA: 0x00068D84 File Offset: 0x00066F84
			public void Reset(Vector3[] positions, Quaternion[] rotations)
			{
				this.lastComPosition = Vector3.Lerp(positions[1], positions[5], 0.25f) + rotations[0] * this.offset;
				this.comVelocity = Vector3.zero;
				this.footsteps[0].Reset(rotations[0], positions[this.leftFootIndex], rotations[this.leftFootIndex]);
				this.footsteps[1].Reset(rotations[0], positions[this.rightFootIndex], rotations[this.rightFootIndex]);
			}

			// Token: 0x060010DB RID: 4315 RVA: 0x00068E29 File Offset: 0x00067029
			public void Relax()
			{
				this.footsteps[0].relaxFlag = true;
				this.footsteps[1].relaxFlag = true;
			}

			// Token: 0x060010DC RID: 4316 RVA: 0x00068E48 File Offset: 0x00067048
			public void AddDeltaRotation(Quaternion delta, Vector3 pivot)
			{
				Vector3 point = this.lastComPosition - pivot;
				this.lastComPosition = pivot + delta * point;
				foreach (IKSolverVR.Footstep footstep in this.footsteps)
				{
					footstep.rotation = delta * footstep.rotation;
					footstep.stepFromRot = delta * footstep.stepFromRot;
					footstep.stepToRot = delta * footstep.stepToRot;
					footstep.stepToRootRot = delta * footstep.stepToRootRot;
					Vector3 point2 = footstep.position - pivot;
					footstep.position = pivot + delta * point2;
					Vector3 point3 = footstep.stepFrom - pivot;
					footstep.stepFrom = pivot + delta * point3;
					Vector3 point4 = footstep.stepTo - pivot;
					footstep.stepTo = pivot + delta * point4;
				}
			}

			// Token: 0x060010DD RID: 4317 RVA: 0x00068F44 File Offset: 0x00067144
			public void AddDeltaPosition(Vector3 delta)
			{
				this.lastComPosition += delta;
				foreach (IKSolverVR.Footstep footstep in this.footsteps)
				{
					footstep.position += delta;
					footstep.stepFrom += delta;
					footstep.stepTo += delta;
				}
			}

			// Token: 0x060010DE RID: 4318 RVA: 0x00068FB0 File Offset: 0x000671B0
			public void Solve(IKSolverVR.VirtualBone rootBone, IKSolverVR.Spine spine, IKSolverVR.Leg leftLeg, IKSolverVR.Leg rightLeg, IKSolverVR.Arm leftArm, IKSolverVR.Arm rightArm, int supportLegIndex, out Vector3 leftFootPosition, out Vector3 rightFootPosition, out Quaternion leftFootRotation, out Quaternion rightFootRotation, out float leftFootOffset, out float rightFootOffset, out float leftHeelOffset, out float rightHeelOffset, float scale)
			{
				if (this.weight <= 0f)
				{
					leftFootPosition = Vector3.zero;
					rightFootPosition = Vector3.zero;
					leftFootRotation = Quaternion.identity;
					rightFootRotation = Quaternion.identity;
					leftFootOffset = 0f;
					rightFootOffset = 0f;
					leftHeelOffset = 0f;
					rightHeelOffset = 0f;
					return;
				}
				Vector3 vector = rootBone.solverRotation * Vector3.up;
				Vector3 vector2 = spine.pelvis.solverPosition + spine.pelvis.solverRotation * leftLeg.thighRelativeToPelvis;
				Vector3 vector3 = spine.pelvis.solverPosition + spine.pelvis.solverRotation * rightLeg.thighRelativeToPelvis;
				this.footsteps[0].characterSpaceOffset = this.footDistance * Vector3.left * scale;
				this.footsteps[1].characterSpaceOffset = this.footDistance * Vector3.right * scale;
				Vector3 faceDirection = spine.faceDirection;
				Vector3 b = V3Tools.ExtractVertical(faceDirection, vector, 1f);
				Quaternion quaternion = Quaternion.LookRotation(faceDirection - b, vector);
				if (spine.rootHeadingOffset != 0f)
				{
					quaternion = Quaternion.AngleAxis(spine.rootHeadingOffset, vector) * quaternion;
				}
				float num = 1f;
				float num2 = 1f;
				float num3 = 0.2f;
				float d = num + num2 + 2f * num3;
				this.centerOfMass = Vector3.zero;
				this.centerOfMass += spine.pelvis.solverPosition * num;
				this.centerOfMass += spine.head.solverPosition * num2;
				this.centerOfMass += leftArm.position * num3;
				this.centerOfMass += rightArm.position * num3;
				this.centerOfMass /= d;
				this.centerOfMass += rootBone.solverRotation * this.offset;
				this.comVelocity = ((Time.deltaTime > 0f) ? ((this.centerOfMass - this.lastComPosition) / Time.deltaTime) : Vector3.zero);
				this.lastComPosition = this.centerOfMass;
				this.comVelocity = Vector3.ClampMagnitude(this.comVelocity, this.maxVelocity) * this.velocityFactor * scale;
				Vector3 vector4 = this.centerOfMass + this.comVelocity;
				Vector3 a = V3Tools.PointToPlane(spine.pelvis.solverPosition, rootBone.solverPosition, vector);
				Vector3 a2 = V3Tools.PointToPlane(vector4, rootBone.solverPosition, vector);
				Vector3 b2 = Vector3.Lerp(this.footsteps[0].position, this.footsteps[1].position, 0.5f);
				float num4 = Vector3.Angle(vector4 - b2, rootBone.solverRotation * Vector3.up) * this.comAngleMlp;
				for (int i = 0; i < this.footsteps.Length; i++)
				{
					this.footsteps[i].isSupportLeg = (supportLegIndex == i);
				}
				for (int j = 0; j < this.footsteps.Length; j++)
				{
					if (this.footsteps[j].isStepping)
					{
						Vector3 vector5 = a2 + rootBone.solverRotation * this.footsteps[j].characterSpaceOffset;
						if (!this.StepBlocked(this.footsteps[j].stepFrom, vector5, rootBone.solverPosition))
						{
							this.footsteps[j].UpdateStepping(vector5, quaternion, 10f);
						}
					}
					else
					{
						this.footsteps[j].UpdateStanding(quaternion, this.relaxLegTwistMinAngle, this.relaxLegTwistSpeed);
					}
				}
				if (this.CanStep())
				{
					int num5 = -1;
					float num6 = float.NegativeInfinity;
					for (int k = 0; k < this.footsteps.Length; k++)
					{
						if (!this.footsteps[k].isStepping)
						{
							Vector3 vector6 = a2 + rootBone.solverRotation * this.footsteps[k].characterSpaceOffset;
							float num7 = (k == 0) ? leftLeg.mag : rightLeg.mag;
							Vector3 b3 = (k == 0) ? vector2 : vector3;
							float num8 = Vector3.Distance(this.footsteps[k].position, b3);
							bool flag = false;
							if (num8 >= num7 * this.maxLegStretch)
							{
								vector6 = a + rootBone.solverRotation * this.footsteps[k].characterSpaceOffset;
								flag = true;
							}
							bool flag2 = false;
							for (int l = 0; l < this.footsteps.Length; l++)
							{
								if (l != k && !flag)
								{
									if (Vector3.Distance(this.footsteps[k].position, this.footsteps[l].position) >= 0.25f * scale || (this.footsteps[k].position - vector6).sqrMagnitude >= (this.footsteps[l].position - vector6).sqrMagnitude)
									{
										flag2 = IKSolverVR.Locomotion.GetLineSphereCollision(this.footsteps[k].position, vector6, this.footsteps[l].position, 0.25f * scale);
									}
									if (flag2)
									{
										break;
									}
								}
							}
							float num9 = Quaternion.Angle(quaternion, this.footsteps[k].stepToRootRot);
							if (!flag2 || num9 > this.angleThreshold)
							{
								float num10 = Vector3.Distance(this.footsteps[k].position, vector6);
								float num11 = this.stepThreshold * scale;
								if (this.footsteps[k].relaxFlag)
								{
									num11 = 0f;
								}
								float num12 = Mathf.Lerp(num11, num11 * 0.1f, num4 * 0.015f);
								if (flag)
								{
									num12 *= 0.5f;
								}
								if (k == 0)
								{
									num12 *= 0.9f;
								}
								if (!this.StepBlocked(this.footsteps[k].position, vector6, rootBone.solverPosition) && (num10 > num12 || num9 > this.angleThreshold))
								{
									float num13 = 0f;
									num13 -= num10;
									if (num13 > num6)
									{
										num5 = k;
										num6 = num13;
									}
								}
							}
						}
					}
					if (num5 != -1)
					{
						Vector3 p = a2 + rootBone.solverRotation * this.footsteps[num5].characterSpaceOffset;
						this.footsteps[num5].stepSpeed = UnityEngine.Random.Range(this.stepSpeed, this.stepSpeed * 1.5f);
						this.footsteps[num5].StepTo(p, quaternion, this.stepThreshold * scale);
					}
				}
				this.footsteps[0].Update(this.stepInterpolation, this.onLeftFootstep);
				this.footsteps[1].Update(this.stepInterpolation, this.onRightFootstep);
				leftFootPosition = this.footsteps[0].position;
				rightFootPosition = this.footsteps[1].position;
				leftFootPosition = V3Tools.PointToPlane(leftFootPosition, leftLeg.lastBone.readPosition, vector);
				rightFootPosition = V3Tools.PointToPlane(rightFootPosition, rightLeg.lastBone.readPosition, vector);
				leftFootOffset = this.stepHeight.Evaluate(this.footsteps[0].stepProgress) * scale;
				rightFootOffset = this.stepHeight.Evaluate(this.footsteps[1].stepProgress) * scale;
				leftHeelOffset = this.heelHeight.Evaluate(this.footsteps[0].stepProgress) * scale;
				rightHeelOffset = this.heelHeight.Evaluate(this.footsteps[1].stepProgress) * scale;
				leftFootRotation = this.footsteps[0].rotation;
				rightFootRotation = this.footsteps[1].rotation;
			}

			// Token: 0x17000237 RID: 567
			// (get) Token: 0x060010DF RID: 4319 RVA: 0x000697C1 File Offset: 0x000679C1
			public Vector3 leftFootstepPosition
			{
				get
				{
					return this.footsteps[0].position;
				}
			}

			// Token: 0x17000238 RID: 568
			// (get) Token: 0x060010E0 RID: 4320 RVA: 0x000697D0 File Offset: 0x000679D0
			public Vector3 rightFootstepPosition
			{
				get
				{
					return this.footsteps[1].position;
				}
			}

			// Token: 0x17000239 RID: 569
			// (get) Token: 0x060010E1 RID: 4321 RVA: 0x000697DF File Offset: 0x000679DF
			public Quaternion leftFootstepRotation
			{
				get
				{
					return this.footsteps[0].rotation;
				}
			}

			// Token: 0x1700023A RID: 570
			// (get) Token: 0x060010E2 RID: 4322 RVA: 0x000697EE File Offset: 0x000679EE
			public Quaternion rightFootstepRotation
			{
				get
				{
					return this.footsteps[1].rotation;
				}
			}

			// Token: 0x060010E3 RID: 4323 RVA: 0x00069800 File Offset: 0x00067A00
			private bool StepBlocked(Vector3 fromPosition, Vector3 toPosition, Vector3 rootPosition)
			{
				if (this.blockingLayers == -1 || !this.blockingEnabled)
				{
					return false;
				}
				Vector3 vector = fromPosition;
				vector.y = rootPosition.y + this.raycastHeight + this.raycastRadius;
				Vector3 direction = toPosition - vector;
				direction.y = 0f;
				RaycastHit raycastHit;
				if (this.raycastRadius <= 0f)
				{
					return Physics.Raycast(vector, direction, out raycastHit, direction.magnitude, this.blockingLayers);
				}
				return Physics.SphereCast(vector, this.raycastRadius, direction, out raycastHit, direction.magnitude, this.blockingLayers);
			}

			// Token: 0x060010E4 RID: 4324 RVA: 0x000698A0 File Offset: 0x00067AA0
			private bool CanStep()
			{
				foreach (IKSolverVR.Footstep footstep in this.footsteps)
				{
					if (footstep.isStepping && footstep.stepProgress < 0.8f)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060010E5 RID: 4325 RVA: 0x000698E0 File Offset: 0x00067AE0
			private static bool GetLineSphereCollision(Vector3 lineStart, Vector3 lineEnd, Vector3 sphereCenter, float sphereRadius)
			{
				Vector3 forward = lineEnd - lineStart;
				Vector3 vector = sphereCenter - lineStart;
				float num = vector.magnitude - sphereRadius;
				if (num > forward.magnitude)
				{
					return false;
				}
				Vector3 vector2 = Quaternion.Inverse(Quaternion.LookRotation(forward, vector)) * vector;
				if (vector2.z < 0f)
				{
					return num < 0f;
				}
				return vector2.y - sphereRadius < 0f;
			}

			// Token: 0x060010E6 RID: 4326 RVA: 0x0006994C File Offset: 0x00067B4C
			public Locomotion()
			{
			}

			// Token: 0x04000F42 RID: 3906
			[Tooltip("Used for blending in/out of procedural locomotion.")]
			[Range(0f, 1f)]
			public float weight = 1f;

			// Token: 0x04000F43 RID: 3907
			[Tooltip("Tries to maintain this distance between the legs.")]
			public float footDistance = 0.3f;

			// Token: 0x04000F44 RID: 3908
			[Tooltip("Makes a step only if step target position is at least this far from the current footstep or the foot does not reach the current footstep anymore or footstep angle is past the 'Angle Threshold'.")]
			public float stepThreshold = 0.4f;

			// Token: 0x04000F45 RID: 3909
			[Tooltip("Makes a step only if step target position is at least 'Step Threshold' far from the current footstep or the foot does not reach the current footstep anymore or footstep angle is past this value.")]
			public float angleThreshold = 60f;

			// Token: 0x04000F46 RID: 3910
			[Tooltip("Multiplies angle of the center of mass - center of pressure vector. Larger value makes the character step sooner if losing balance.")]
			public float comAngleMlp = 1f;

			// Token: 0x04000F47 RID: 3911
			[Tooltip("Maximum magnitude of head/hand target velocity used in prediction.")]
			public float maxVelocity = 0.4f;

			// Token: 0x04000F48 RID: 3912
			[Tooltip("The amount of head/hand target velocity prediction.")]
			public float velocityFactor = 0.4f;

			// Token: 0x04000F49 RID: 3913
			[Tooltip("How much can a leg be extended before it is forced to step to another position? 1 means fully stretched.")]
			[Range(0.9f, 1f)]
			public float maxLegStretch = 1f;

			// Token: 0x04000F4A RID: 3914
			[Tooltip("The speed of lerping the root of the character towards the horizontal mid-point of the footsteps.")]
			public float rootSpeed = 20f;

			// Token: 0x04000F4B RID: 3915
			[Tooltip("The speed of moving a foot to the next position.")]
			public float stepSpeed = 3f;

			// Token: 0x04000F4C RID: 3916
			[Tooltip("The height of the foot by normalized step progress (0 - 1).")]
			public AnimationCurve stepHeight;

			// Token: 0x04000F4D RID: 3917
			[Tooltip("Reduce this value if locomotion makes the head bob too much.")]
			public float maxBodyYOffset = 0.05f;

			// Token: 0x04000F4E RID: 3918
			[Tooltip("The height offset of the heel by normalized step progress (0 - 1).")]
			public AnimationCurve heelHeight;

			// Token: 0x04000F4F RID: 3919
			[Tooltip("Rotates the foot while the leg is not stepping to relax the twist rotation of the leg if ideal rotation is past this angle.")]
			[Range(0f, 180f)]
			public float relaxLegTwistMinAngle = 20f;

			// Token: 0x04000F50 RID: 3920
			[Tooltip("The speed of rotating the foot while the leg is not stepping to relax the twist rotation of the leg.")]
			public float relaxLegTwistSpeed = 400f;

			// Token: 0x04000F51 RID: 3921
			[Tooltip("Interpolation mode of the step.")]
			public InterpolationMode stepInterpolation = InterpolationMode.InOutSine;

			// Token: 0x04000F52 RID: 3922
			[Tooltip("Offset for the approximated center of mass.")]
			public Vector3 offset;

			// Token: 0x04000F53 RID: 3923
			[HideInInspector]
			public bool blockingEnabled;

			// Token: 0x04000F54 RID: 3924
			[HideInInspector]
			public LayerMask blockingLayers;

			// Token: 0x04000F55 RID: 3925
			[HideInInspector]
			public float raycastRadius = 0.2f;

			// Token: 0x04000F56 RID: 3926
			[HideInInspector]
			public float raycastHeight = 0.2f;

			// Token: 0x04000F57 RID: 3927
			[Tooltip("Called when the left foot has finished a step.")]
			public UnityEvent onLeftFootstep = new UnityEvent();

			// Token: 0x04000F58 RID: 3928
			[Tooltip("Called when the right foot has finished a step")]
			public UnityEvent onRightFootstep = new UnityEvent();

			// Token: 0x04000F59 RID: 3929
			[CompilerGenerated]
			private Vector3 <centerOfMass>k__BackingField;

			// Token: 0x04000F5A RID: 3930
			private IKSolverVR.Footstep[] footsteps = new IKSolverVR.Footstep[0];

			// Token: 0x04000F5B RID: 3931
			private Vector3 lastComPosition;

			// Token: 0x04000F5C RID: 3932
			private Vector3 comVelocity;

			// Token: 0x04000F5D RID: 3933
			private int leftFootIndex;

			// Token: 0x04000F5E RID: 3934
			private int rightFootIndex;
		}

		// Token: 0x02000202 RID: 514
		[Serializable]
		public class Spine : IKSolverVR.BodyPart
		{
			// Token: 0x1700023B RID: 571
			// (get) Token: 0x060010E7 RID: 4327 RVA: 0x00069A2D File Offset: 0x00067C2D
			public IKSolverVR.VirtualBone pelvis
			{
				get
				{
					return this.bones[this.pelvisIndex];
				}
			}

			// Token: 0x1700023C RID: 572
			// (get) Token: 0x060010E8 RID: 4328 RVA: 0x00069A3C File Offset: 0x00067C3C
			public IKSolverVR.VirtualBone firstSpineBone
			{
				get
				{
					return this.bones[this.spineIndex];
				}
			}

			// Token: 0x1700023D RID: 573
			// (get) Token: 0x060010E9 RID: 4329 RVA: 0x00069A4B File Offset: 0x00067C4B
			public IKSolverVR.VirtualBone chest
			{
				get
				{
					if (this.hasChest)
					{
						return this.bones[this.chestIndex];
					}
					return this.bones[this.spineIndex];
				}
			}

			// Token: 0x1700023E RID: 574
			// (get) Token: 0x060010EA RID: 4330 RVA: 0x00069A70 File Offset: 0x00067C70
			private IKSolverVR.VirtualBone neck
			{
				get
				{
					return this.bones[this.neckIndex];
				}
			}

			// Token: 0x1700023F RID: 575
			// (get) Token: 0x060010EB RID: 4331 RVA: 0x00069A7F File Offset: 0x00067C7F
			public IKSolverVR.VirtualBone head
			{
				get
				{
					return this.bones[this.headIndex];
				}
			}

			// Token: 0x17000240 RID: 576
			// (get) Token: 0x060010EC RID: 4332 RVA: 0x00069A8E File Offset: 0x00067C8E
			// (set) Token: 0x060010ED RID: 4333 RVA: 0x00069A96 File Offset: 0x00067C96
			public Quaternion anchorRotation
			{
				[CompilerGenerated]
				get
				{
					return this.<anchorRotation>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<anchorRotation>k__BackingField = value;
				}
			}

			// Token: 0x17000241 RID: 577
			// (get) Token: 0x060010EE RID: 4334 RVA: 0x00069A9F File Offset: 0x00067C9F
			// (set) Token: 0x060010EF RID: 4335 RVA: 0x00069AA7 File Offset: 0x00067CA7
			public Quaternion anchorRelativeToHead
			{
				[CompilerGenerated]
				get
				{
					return this.<anchorRelativeToHead>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<anchorRelativeToHead>k__BackingField = value;
				}
			}

			// Token: 0x060010F0 RID: 4336 RVA: 0x00069AB0 File Offset: 0x00067CB0
			protected override void OnRead(Vector3[] positions, Quaternion[] rotations, bool hasChest, bool hasNeck, bool hasShoulders, bool hasToes, bool hasLegs, int rootIndex, int index)
			{
				Vector3 vector = positions[index];
				Quaternion quaternion = rotations[index];
				Vector3 vector2 = positions[index + 1];
				Quaternion quaternion2 = rotations[index + 1];
				Vector3 vector3 = positions[index + 2];
				Quaternion quaternion3 = rotations[index + 2];
				Vector3 position = positions[index + 3];
				Quaternion rotation = rotations[index + 3];
				Vector3 vector4 = positions[index + 4];
				Quaternion quaternion4 = rotations[index + 4];
				this.hasLegs = hasLegs;
				if (!hasChest)
				{
					vector3 = vector2;
					quaternion3 = quaternion2;
				}
				if (!this.initiated)
				{
					this.hasChest = hasChest;
					this.hasNeck = hasNeck;
					this.headHeight = V3Tools.ExtractVertical(vector4 - positions[0], rotations[0] * Vector3.up, 1f).magnitude;
					int num = 3;
					if (hasChest)
					{
						num++;
					}
					if (hasNeck)
					{
						num++;
					}
					this.bones = new IKSolverVR.VirtualBone[num];
					this.chestIndex = (hasChest ? 2 : 1);
					this.neckIndex = 1;
					if (hasChest)
					{
						this.neckIndex++;
					}
					if (hasNeck)
					{
						this.neckIndex++;
					}
					this.headIndex = 2;
					if (hasChest)
					{
						this.headIndex++;
					}
					if (hasNeck)
					{
						this.headIndex++;
					}
					this.bones[0] = new IKSolverVR.VirtualBone(vector, quaternion);
					this.bones[1] = new IKSolverVR.VirtualBone(vector2, quaternion2);
					if (hasChest)
					{
						this.bones[this.chestIndex] = new IKSolverVR.VirtualBone(vector3, quaternion3);
					}
					if (hasNeck)
					{
						this.bones[this.neckIndex] = new IKSolverVR.VirtualBone(position, rotation);
					}
					this.bones[this.headIndex] = new IKSolverVR.VirtualBone(vector4, quaternion4);
					this.pelvisRotationOffset = Quaternion.identity;
					this.chestRotationOffset = Quaternion.identity;
					this.headRotationOffset = Quaternion.identity;
					this.anchorRelativeToHead = Quaternion.Inverse(quaternion4) * rotations[0];
					this.anchorRelativeToPelvis = Quaternion.Inverse(quaternion) * rotations[0];
					this.faceDirection = rotations[0] * Vector3.forward;
					this.IKPositionHead = vector4;
					this.IKRotationHead = quaternion4;
					this.IKPositionPelvis = vector;
					this.IKRotationPelvis = quaternion;
					this.goalPositionChest = vector3 + rotations[0] * Vector3.forward;
				}
				this.pelvisRelativeRotation = Quaternion.Inverse(quaternion4) * quaternion;
				this.chestRelativeRotation = Quaternion.Inverse(quaternion4) * quaternion3;
				this.chestForward = Quaternion.Inverse(quaternion3) * (rotations[0] * Vector3.forward);
				this.bones[0].Read(vector, quaternion);
				this.bones[1].Read(vector2, quaternion2);
				if (hasChest)
				{
					this.bones[this.chestIndex].Read(vector3, quaternion3);
				}
				if (hasNeck)
				{
					this.bones[this.neckIndex].Read(position, rotation);
				}
				this.bones[this.headIndex].Read(vector4, quaternion4);
				float num2 = Vector3.Distance(vector, vector4);
				this.sizeMlp = num2 / 0.7f;
			}

			// Token: 0x060010F1 RID: 4337 RVA: 0x00069DEC File Offset: 0x00067FEC
			public override void PreSolve()
			{
				if (this.headTarget != null)
				{
					this.IKPositionHead = this.headTarget.position;
					this.IKRotationHead = this.headTarget.rotation;
				}
				if (this.chestGoal != null)
				{
					this.goalPositionChest = this.chestGoal.position;
				}
				if (this.pelvisTarget != null)
				{
					this.IKPositionPelvis = this.pelvisTarget.position;
					this.IKRotationPelvis = this.pelvisTarget.rotation;
				}
				this.headPosition = V3Tools.Lerp(this.head.solverPosition, this.IKPositionHead, this.positionWeight);
				this.headRotation = QuaTools.Lerp(this.head.solverRotation, this.IKRotationHead, this.rotationWeight);
				this.pelvisRotation = QuaTools.Lerp(this.pelvis.solverRotation, this.IKRotationPelvis, this.rotationWeight);
			}

			// Token: 0x060010F2 RID: 4338 RVA: 0x00069EE0 File Offset: 0x000680E0
			public override void ApplyOffsets(float scale)
			{
				this.headPosition += this.headPositionOffset;
				float num = this.minHeadHeight * scale;
				Vector3 vector = this.rootRotation * Vector3.up;
				if (vector == Vector3.up)
				{
					this.headPosition.y = Math.Max(this.rootPosition.y + num, this.headPosition.y);
				}
				else
				{
					Vector3 vector2 = this.headPosition - this.rootPosition;
					Vector3 b = V3Tools.ExtractHorizontal(vector2, vector, 1f);
					Vector3 vector3 = vector2 - b;
					if (Vector3.Dot(vector3, vector) > 0f)
					{
						if (vector3.magnitude < num)
						{
							vector3 = vector3.normalized * num;
						}
					}
					else
					{
						vector3 = -vector3.normalized * num;
					}
					this.headPosition = this.rootPosition + b + vector3;
				}
				this.headRotation = this.headRotationOffset * this.headRotation;
				this.headDeltaPosition = this.headPosition - this.head.solverPosition;
				this.pelvisDeltaRotation = QuaTools.FromToRotation(this.pelvis.solverRotation, this.headRotation * this.pelvisRelativeRotation);
				if (this.pelvisRotationWeight <= 0f)
				{
					this.anchorRotation = this.headRotation * this.anchorRelativeToHead;
					return;
				}
				if (this.pelvisRotationWeight > 0f && this.pelvisRotationWeight < 1f)
				{
					this.anchorRotation = Quaternion.Lerp(this.headRotation * this.anchorRelativeToHead, this.pelvisRotation * this.anchorRelativeToPelvis, this.pelvisRotationWeight);
					return;
				}
				if (this.pelvisRotationWeight >= 1f)
				{
					this.anchorRotation = this.pelvisRotation * this.anchorRelativeToPelvis;
				}
			}

			// Token: 0x060010F3 RID: 4339 RVA: 0x0006A0C0 File Offset: 0x000682C0
			private void CalculateChestTargetRotation(IKSolverVR.VirtualBone rootBone, IKSolverVR.Arm[] arms)
			{
				this.chestTargetRotation = this.headRotation * this.chestRelativeRotation;
				this.AdjustChestByHands(ref this.chestTargetRotation, arms);
				this.faceDirection = Vector3.Cross(this.anchorRotation * Vector3.right, rootBone.readRotation * Vector3.up) + this.anchorRotation * Vector3.forward;
			}

			// Token: 0x060010F4 RID: 4340 RVA: 0x0006A134 File Offset: 0x00068334
			public void Solve(IKSolverVR.VirtualBone rootBone, IKSolverVR.Leg[] legs, IKSolverVR.Arm[] arms, float scale)
			{
				this.CalculateChestTargetRotation(rootBone, arms);
				if (this.maxRootAngle < 180f)
				{
					Vector3 point = this.faceDirection;
					if (this.rootHeadingOffset != 0f)
					{
						point = Quaternion.AngleAxis(this.rootHeadingOffset, Vector3.up) * point;
					}
					Vector3 vector = Quaternion.Inverse(rootBone.solverRotation) * point;
					float num = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
					float angle = 0f;
					float num2 = this.maxRootAngle;
					if (num > num2)
					{
						angle = num - num2;
					}
					if (num < -num2)
					{
						angle = num + num2;
					}
					rootBone.solverRotation = Quaternion.AngleAxis(angle, rootBone.readRotation * Vector3.up) * rootBone.solverRotation;
				}
				Vector3 solverPosition = this.pelvis.solverPosition;
				Vector3 rootUp = rootBone.solverRotation * Vector3.up;
				this.TranslatePelvis(legs, this.headDeltaPosition, this.pelvisDeltaRotation, scale);
				this.FABRIKPass(solverPosition, rootUp, this.positionWeight);
				this.Bend(this.bones, this.pelvisIndex, this.chestIndex, this.chestTargetRotation, this.chestRotationOffset, this.chestClampWeight, false, this.neckStiffness * this.rotationWeight);
				if (this.LOD < 1 && this.chestGoalWeight > 0f)
				{
					Quaternion targetRotation = Quaternion.FromToRotation(this.bones[this.chestIndex].solverRotation * this.chestForward, this.goalPositionChest - this.bones[this.chestIndex].solverPosition) * this.bones[this.chestIndex].solverRotation;
					this.Bend(this.bones, this.pelvisIndex, this.chestIndex, targetRotation, this.chestRotationOffset, this.chestClampWeight, false, this.chestGoalWeight * this.rotationWeight);
				}
				this.InverseTranslateToHead(legs, false, false, Vector3.zero, this.positionWeight);
				if (this.LOD < 1)
				{
					this.FABRIKPass(solverPosition, rootUp, this.positionWeight);
				}
				this.Bend(this.bones, this.neckIndex, this.headIndex, this.headRotation, this.headClampWeight, true, this.rotationWeight);
				this.SolvePelvis();
			}

			// Token: 0x060010F5 RID: 4341 RVA: 0x0006A380 File Offset: 0x00068580
			private void FABRIKPass(Vector3 animatedPelvisPos, Vector3 rootUp, float weight)
			{
				Vector3 startPosition = Vector3.Lerp(this.pelvis.solverPosition, animatedPelvisPos, this.maintainPelvisPosition) + this.pelvisPositionOffset;
				Vector3 targetPosition = this.headPosition - this.chestPositionOffset;
				Vector3 zero = Vector3.zero;
				float num = Vector3.Distance(this.bones[0].solverPosition, this.bones[this.bones.Length - 1].solverPosition);
				IKSolverVR.VirtualBone.SolveFABRIK(this.bones, startPosition, targetPosition, weight, 1f, 1, num, zero);
			}

			// Token: 0x060010F6 RID: 4342 RVA: 0x0006A408 File Offset: 0x00068608
			private void SolvePelvis()
			{
				if (this.pelvisPositionWeight > 0f)
				{
					Quaternion solverRotation = this.head.solverRotation;
					Vector3 b = (this.IKPositionPelvis + this.pelvisPositionOffset - this.pelvis.solverPosition) * this.pelvisPositionWeight;
					IKSolverVR.VirtualBone[] bones = this.bones;
					for (int i = 0; i < bones.Length; i++)
					{
						bones[i].solverPosition += b;
					}
					Vector3 bendNormal = this.anchorRotation * Vector3.right;
					if (this.hasChest && this.hasNeck)
					{
						IKSolverVR.VirtualBone.SolveTrigonometric(this.bones, this.spineIndex, this.chestIndex, this.headIndex, this.headPosition, bendNormal, this.pelvisPositionWeight * 0.9f);
						IKSolverVR.VirtualBone.SolveTrigonometric(this.bones, this.chestIndex, this.neckIndex, this.headIndex, this.headPosition, bendNormal, this.pelvisPositionWeight);
					}
					else if (this.hasChest && !this.hasNeck)
					{
						IKSolverVR.VirtualBone.SolveTrigonometric(this.bones, this.spineIndex, this.chestIndex, this.headIndex, this.headPosition, bendNormal, this.pelvisPositionWeight);
					}
					else if (!this.hasChest && this.hasNeck)
					{
						IKSolverVR.VirtualBone.SolveTrigonometric(this.bones, this.spineIndex, this.neckIndex, this.headIndex, this.headPosition, bendNormal, this.pelvisPositionWeight);
					}
					else if (!this.hasNeck && !this.hasChest)
					{
						IKSolverVR.VirtualBone.SolveTrigonometric(this.bones, this.pelvisIndex, this.spineIndex, this.headIndex, this.headPosition, bendNormal, this.pelvisPositionWeight);
					}
					this.head.solverRotation = solverRotation;
				}
			}

			// Token: 0x060010F7 RID: 4343 RVA: 0x0006A5CC File Offset: 0x000687CC
			public override void Write(ref Vector3[] solvedPositions, ref Quaternion[] solvedRotations)
			{
				solvedPositions[this.index] = this.bones[0].solverPosition;
				solvedRotations[this.index] = this.bones[0].solverRotation;
				solvedRotations[this.index + 1] = this.bones[1].solverRotation;
				if (this.hasChest)
				{
					solvedRotations[this.index + 2] = this.bones[this.chestIndex].solverRotation;
				}
				if (this.hasNeck)
				{
					solvedRotations[this.index + 3] = this.bones[this.neckIndex].solverRotation;
				}
				solvedRotations[this.index + 4] = this.bones[this.headIndex].solverRotation;
			}

			// Token: 0x060010F8 RID: 4344 RVA: 0x0006A69C File Offset: 0x0006889C
			public override void ResetOffsets()
			{
				this.pelvisPositionOffset = Vector3.zero;
				this.chestPositionOffset = Vector3.zero;
				this.headPositionOffset = this.locomotionHeadPositionOffset;
				this.pelvisRotationOffset = Quaternion.identity;
				this.chestRotationOffset = Quaternion.identity;
				this.headRotationOffset = Quaternion.identity;
			}

			// Token: 0x060010F9 RID: 4345 RVA: 0x0006A6EC File Offset: 0x000688EC
			private void AdjustChestByHands(ref Quaternion chestTargetRotation, IKSolverVR.Arm[] arms)
			{
				if (this.LOD > 0)
				{
					return;
				}
				Quaternion rotation = Quaternion.Inverse(this.anchorRotation);
				Vector3 vector = rotation * (arms[0].position - this.headPosition) / this.sizeMlp;
				Vector3 vector2 = rotation * (arms[1].position - this.headPosition) / this.sizeMlp;
				Vector3 forward = Vector3.forward;
				forward.x += vector.x * Mathf.Abs(vector.x);
				forward.x += vector.z * Mathf.Abs(vector.z);
				forward.x += vector2.x * Mathf.Abs(vector2.x);
				forward.x -= vector2.z * Mathf.Abs(vector2.z);
				forward.x *= 5f * this.rotateChestByHands;
				Quaternion lhs = Quaternion.AngleAxis(Mathf.Atan2(forward.x, forward.z) * 57.29578f, this.rootRotation * Vector3.up);
				chestTargetRotation = lhs * chestTargetRotation;
				Vector3 up = Vector3.up;
				up.x += vector.y;
				up.x -= vector2.y;
				up.x *= 0.5f * this.rotateChestByHands;
				lhs = Quaternion.AngleAxis(Mathf.Atan2(up.x, up.y) * 57.29578f, this.rootRotation * Vector3.back);
				chestTargetRotation = lhs * chestTargetRotation;
			}

			// Token: 0x060010FA RID: 4346 RVA: 0x0006A8A8 File Offset: 0x00068AA8
			public void InverseTranslateToHead(IKSolverVR.Leg[] legs, bool limited, bool useCurrentLegMag, Vector3 offset, float w)
			{
				Vector3 b = (this.headPosition + offset - this.head.solverPosition) * w;
				Vector3 vector = this.pelvis.solverPosition + b;
				base.MovePosition(limited ? this.LimitPelvisPosition(legs, vector, useCurrentLegMag, 2) : vector);
			}

			// Token: 0x060010FB RID: 4347 RVA: 0x0006A904 File Offset: 0x00068B04
			private void TranslatePelvis(IKSolverVR.Leg[] legs, Vector3 deltaPosition, Quaternion deltaRotation, float scale)
			{
				Vector3 solverPosition = this.head.solverPosition;
				deltaRotation = QuaTools.ClampRotation(deltaRotation, this.chestClampWeight, 2);
				Quaternion quaternion = Quaternion.Slerp(Quaternion.identity, deltaRotation, this.bodyRotStiffness * this.rotationWeight);
				quaternion = Quaternion.Slerp(quaternion, QuaTools.FromToRotation(this.pelvis.solverRotation, this.IKRotationPelvis), this.pelvisRotationWeight);
				IKSolverVR.VirtualBone.RotateAroundPoint(this.bones, 0, this.pelvis.solverPosition, this.pelvisRotationOffset * quaternion);
				deltaPosition -= this.head.solverPosition - solverPosition;
				Vector3 a = this.rootRotation * Vector3.forward;
				float num = V3Tools.ExtractVertical(deltaPosition, this.rootRotation * Vector3.up, 1f).magnitude;
				if (scale > 0f)
				{
					num /= scale;
				}
				float d = num * -this.moveBodyBackWhenCrouching * this.headHeight;
				deltaPosition += a * d;
				base.MovePosition(this.LimitPelvisPosition(legs, this.pelvis.solverPosition + deltaPosition * this.bodyPosStiffness * this.positionWeight, false, 2));
			}

			// Token: 0x060010FC RID: 4348 RVA: 0x0006AA40 File Offset: 0x00068C40
			private Vector3 LimitPelvisPosition(IKSolverVR.Leg[] legs, Vector3 pelvisPosition, bool useCurrentLegMag, int it = 2)
			{
				if (!this.hasLegs)
				{
					return pelvisPosition;
				}
				if (useCurrentLegMag)
				{
					foreach (IKSolverVR.Leg leg in legs)
					{
						leg.currentMag = Vector3.Distance(leg.thigh.solverPosition, leg.lastBone.solverPosition);
					}
				}
				for (int j = 0; j < it; j++)
				{
					foreach (IKSolverVR.Leg leg2 in legs)
					{
						Vector3 b = pelvisPosition - this.pelvis.solverPosition;
						Vector3 vector = leg2.thigh.solverPosition + b;
						Vector3 vector2 = vector - leg2.position;
						float maxLength = useCurrentLegMag ? leg2.currentMag : leg2.mag;
						Vector3 a = leg2.position + Vector3.ClampMagnitude(vector2, maxLength);
						pelvisPosition += a - vector;
					}
				}
				return pelvisPosition;
			}

			// Token: 0x060010FD RID: 4349 RVA: 0x0006AB30 File Offset: 0x00068D30
			private void Bend(IKSolverVR.VirtualBone[] bones, int firstIndex, int lastIndex, Quaternion targetRotation, float clampWeight, bool uniformWeight, float w)
			{
				if (w <= 0f)
				{
					return;
				}
				if (bones.Length == 0)
				{
					return;
				}
				int num = lastIndex + 1 - firstIndex;
				if (num < 1)
				{
					return;
				}
				Quaternion quaternion = QuaTools.FromToRotation(bones[lastIndex].solverRotation, targetRotation);
				quaternion = QuaTools.ClampRotation(quaternion, clampWeight, 2);
				float num2 = uniformWeight ? (1f / (float)num) : 0f;
				for (int i = firstIndex; i < lastIndex + 1; i++)
				{
					if (!uniformWeight)
					{
						num2 = Mathf.Clamp((float)((i - firstIndex + 1) / num), 0f, 1f);
					}
					IKSolverVR.VirtualBone.RotateAroundPoint(bones, i, bones[i].solverPosition, Quaternion.Slerp(Quaternion.identity, quaternion, num2 * w));
				}
			}

			// Token: 0x060010FE RID: 4350 RVA: 0x0006ABD0 File Offset: 0x00068DD0
			private void Bend(IKSolverVR.VirtualBone[] bones, int firstIndex, int lastIndex, Quaternion targetRotation, Quaternion rotationOffset, float clampWeight, bool uniformWeight, float w)
			{
				if (w <= 0f)
				{
					return;
				}
				if (bones.Length == 0)
				{
					return;
				}
				int num = lastIndex + 1 - firstIndex;
				if (num < 1)
				{
					return;
				}
				Quaternion quaternion = QuaTools.FromToRotation(bones[lastIndex].solverRotation, targetRotation);
				quaternion = QuaTools.ClampRotation(quaternion, clampWeight, 2);
				float num2 = uniformWeight ? (1f / (float)num) : 0f;
				for (int i = firstIndex; i < lastIndex + 1; i++)
				{
					if (!uniformWeight)
					{
						if (num == 1)
						{
							num2 = 1f;
						}
						else if (num == 2)
						{
							num2 = ((i == 0) ? 0.2f : 0.8f);
						}
						else if (num == 3)
						{
							if (i == 0)
							{
								num2 = 0.15f;
							}
							else if (i == 1)
							{
								num2 = 0.4f;
							}
							else
							{
								num2 = 0.45f;
							}
						}
						else if (num > 3)
						{
							num2 = 1f / (float)num;
						}
					}
					IKSolverVR.VirtualBone.RotateAroundPoint(bones, i, bones[i].solverPosition, Quaternion.Slerp(Quaternion.Slerp(Quaternion.identity, rotationOffset, num2), quaternion, num2 * w));
				}
			}

			// Token: 0x060010FF RID: 4351 RVA: 0x0006ACB8 File Offset: 0x00068EB8
			public Spine()
			{
			}

			// Token: 0x04000F5F RID: 3935
			[Tooltip("The head target. This should not be the camera Transform itself, but a child GameObject parented to it so you could adjust it's position/rotation  to match the orientation of the head bone. The best practice for setup would be to move the camera to the avatar's eyes, duplicate the avatar's head bone and parent it to the camera. Then assign the duplicate to this slot.")]
			public Transform headTarget;

			// Token: 0x04000F60 RID: 3936
			[Tooltip("The pelvis target (optional), useful for seated rigs or if you had an additional tracker on the backpack or belt are. The best practice for setup would be to duplicate the avatar's pelvis bone and parenting it to the pelvis tracker. Then assign the duplicate to this slot.")]
			public Transform pelvisTarget;

			// Token: 0x04000F61 RID: 3937
			[Tooltip("Positional weight of the head target. Note that if you have nulled the headTarget, the head will still be pulled to the last position of the headTarget until you set this value to 0.")]
			[Range(0f, 1f)]
			public float positionWeight = 1f;

			// Token: 0x04000F62 RID: 3938
			[Tooltip("Rotational weight of the head target. Note that if you have nulled the headTarget, the head will still be rotated to the last rotation of the headTarget until you set this value to 0.")]
			[Range(0f, 1f)]
			public float rotationWeight = 1f;

			// Token: 0x04000F63 RID: 3939
			[Tooltip("Positional weight of the pelvis target. Note that if you have nulled the pelvisTarget, the pelvis will still be pulled to the last position of the pelvisTarget until you set this value to 0.")]
			[Range(0f, 1f)]
			public float pelvisPositionWeight;

			// Token: 0x04000F64 RID: 3940
			[Tooltip("Rotational weight of the pelvis target. Note that if you have nulled the pelvisTarget, the pelvis will still be rotated to the last rotation of the pelvisTarget until you set this value to 0.")]
			[Range(0f, 1f)]
			public float pelvisRotationWeight;

			// Token: 0x04000F65 RID: 3941
			[Tooltip("If 'Chest Goal Weight' is greater than 0, the chest will be turned towards this Transform.")]
			public Transform chestGoal;

			// Token: 0x04000F66 RID: 3942
			[Tooltip("Weight of turning the chest towards the 'Chest Goal'.")]
			[Range(0f, 1f)]
			public float chestGoalWeight;

			// Token: 0x04000F67 RID: 3943
			[Tooltip("Minimum height of the head from the root of the character.")]
			public float minHeadHeight = 0.8f;

			// Token: 0x04000F68 RID: 3944
			[Tooltip("Determines how much the body will follow the position of the head.")]
			[Range(0f, 1f)]
			public float bodyPosStiffness = 0.55f;

			// Token: 0x04000F69 RID: 3945
			[Tooltip("Determines how much the body will follow the rotation of the head.")]
			[Range(0f, 1f)]
			public float bodyRotStiffness = 0.1f;

			// Token: 0x04000F6A RID: 3946
			[Tooltip("Determines how much the chest will rotate to the rotation of the head.")]
			[FormerlySerializedAs("chestRotationWeight")]
			[Range(0f, 1f)]
			public float neckStiffness = 0.2f;

			// Token: 0x04000F6B RID: 3947
			[Tooltip("The amount of rotation applied to the chest based on hand positions.")]
			[Range(0f, 1f)]
			public float rotateChestByHands = 1f;

			// Token: 0x04000F6C RID: 3948
			[Tooltip("Clamps chest rotation. Value of 0.5 allows 90 degrees of rotation for the chest relative to the head. Value of 0 allows 180 degrees and value of 1 means the chest will be locked relative to the head.")]
			[Range(0f, 1f)]
			public float chestClampWeight = 0.5f;

			// Token: 0x04000F6D RID: 3949
			[Tooltip("Clamps head rotation. Value of 0.5 allows 90 degrees of rotation for the head relative to the headTarget. Value of 0 allows 180 degrees and value of 1 means head rotation will be locked to the target.")]
			[Range(0f, 1f)]
			public float headClampWeight = 0.6f;

			// Token: 0x04000F6E RID: 3950
			[Tooltip("Moves the body horizontally along -character.forward axis by that value when the player is crouching.")]
			public float moveBodyBackWhenCrouching = 0.5f;

			// Token: 0x04000F6F RID: 3951
			[Tooltip("How much will the pelvis maintain it's animated position?")]
			[Range(0f, 1f)]
			public float maintainPelvisPosition = 0.2f;

			// Token: 0x04000F70 RID: 3952
			[Tooltip("Will automatically rotate the root of the character if the head target has turned past this angle.")]
			[Range(0f, 180f)]
			public float maxRootAngle = 25f;

			// Token: 0x04000F71 RID: 3953
			[Tooltip("Angular offset for root heading. Adjust this value to turn the root relative to the HMD around the vertical axis. Usefulf for fighting or shooting games where you would sometimes want the avatar to stand at an angled stance.")]
			[Range(-180f, 180f)]
			public float rootHeadingOffset;

			// Token: 0x04000F72 RID: 3954
			[HideInInspector]
			[NonSerialized]
			public Vector3 IKPositionHead;

			// Token: 0x04000F73 RID: 3955
			[HideInInspector]
			[NonSerialized]
			public Quaternion IKRotationHead = Quaternion.identity;

			// Token: 0x04000F74 RID: 3956
			[HideInInspector]
			[NonSerialized]
			public Vector3 IKPositionPelvis;

			// Token: 0x04000F75 RID: 3957
			[HideInInspector]
			[NonSerialized]
			public Quaternion IKRotationPelvis = Quaternion.identity;

			// Token: 0x04000F76 RID: 3958
			[HideInInspector]
			[NonSerialized]
			public Vector3 goalPositionChest;

			// Token: 0x04000F77 RID: 3959
			[HideInInspector]
			[NonSerialized]
			public Vector3 pelvisPositionOffset;

			// Token: 0x04000F78 RID: 3960
			[HideInInspector]
			[NonSerialized]
			public Vector3 chestPositionOffset;

			// Token: 0x04000F79 RID: 3961
			[HideInInspector]
			[NonSerialized]
			public Vector3 headPositionOffset;

			// Token: 0x04000F7A RID: 3962
			[HideInInspector]
			[NonSerialized]
			public Quaternion pelvisRotationOffset = Quaternion.identity;

			// Token: 0x04000F7B RID: 3963
			[HideInInspector]
			[NonSerialized]
			public Quaternion chestRotationOffset = Quaternion.identity;

			// Token: 0x04000F7C RID: 3964
			[HideInInspector]
			[NonSerialized]
			public Quaternion headRotationOffset = Quaternion.identity;

			// Token: 0x04000F7D RID: 3965
			[HideInInspector]
			[NonSerialized]
			public Vector3 faceDirection;

			// Token: 0x04000F7E RID: 3966
			[HideInInspector]
			[NonSerialized]
			public Vector3 locomotionHeadPositionOffset;

			// Token: 0x04000F7F RID: 3967
			[HideInInspector]
			[NonSerialized]
			public Vector3 headPosition;

			// Token: 0x04000F80 RID: 3968
			[CompilerGenerated]
			private Quaternion <anchorRotation>k__BackingField;

			// Token: 0x04000F81 RID: 3969
			[CompilerGenerated]
			private Quaternion <anchorRelativeToHead>k__BackingField;

			// Token: 0x04000F82 RID: 3970
			private Quaternion headRotation = Quaternion.identity;

			// Token: 0x04000F83 RID: 3971
			private Quaternion pelvisRotation = Quaternion.identity;

			// Token: 0x04000F84 RID: 3972
			private Quaternion anchorRelativeToPelvis = Quaternion.identity;

			// Token: 0x04000F85 RID: 3973
			private Quaternion pelvisRelativeRotation = Quaternion.identity;

			// Token: 0x04000F86 RID: 3974
			private Quaternion chestRelativeRotation = Quaternion.identity;

			// Token: 0x04000F87 RID: 3975
			private Vector3 headDeltaPosition;

			// Token: 0x04000F88 RID: 3976
			private Quaternion pelvisDeltaRotation = Quaternion.identity;

			// Token: 0x04000F89 RID: 3977
			private Quaternion chestTargetRotation = Quaternion.identity;

			// Token: 0x04000F8A RID: 3978
			private int pelvisIndex;

			// Token: 0x04000F8B RID: 3979
			private int spineIndex = 1;

			// Token: 0x04000F8C RID: 3980
			private int chestIndex = -1;

			// Token: 0x04000F8D RID: 3981
			private int neckIndex = -1;

			// Token: 0x04000F8E RID: 3982
			private int headIndex = -1;

			// Token: 0x04000F8F RID: 3983
			private float length;

			// Token: 0x04000F90 RID: 3984
			private bool hasChest;

			// Token: 0x04000F91 RID: 3985
			private bool hasNeck;

			// Token: 0x04000F92 RID: 3986
			private bool hasLegs;

			// Token: 0x04000F93 RID: 3987
			private float headHeight;

			// Token: 0x04000F94 RID: 3988
			private float sizeMlp;

			// Token: 0x04000F95 RID: 3989
			private Vector3 chestForward;
		}

		// Token: 0x02000203 RID: 515
		[Serializable]
		public enum PositionOffset
		{
			// Token: 0x04000F97 RID: 3991
			Pelvis,
			// Token: 0x04000F98 RID: 3992
			Chest,
			// Token: 0x04000F99 RID: 3993
			Head,
			// Token: 0x04000F9A RID: 3994
			LeftHand,
			// Token: 0x04000F9B RID: 3995
			RightHand,
			// Token: 0x04000F9C RID: 3996
			LeftFoot,
			// Token: 0x04000F9D RID: 3997
			RightFoot,
			// Token: 0x04000F9E RID: 3998
			LeftHeel,
			// Token: 0x04000F9F RID: 3999
			RightHeel
		}

		// Token: 0x02000204 RID: 516
		[Serializable]
		public enum RotationOffset
		{
			// Token: 0x04000FA1 RID: 4001
			Pelvis,
			// Token: 0x04000FA2 RID: 4002
			Chest,
			// Token: 0x04000FA3 RID: 4003
			Head
		}

		// Token: 0x02000205 RID: 517
		[Serializable]
		public class VirtualBone
		{
			// Token: 0x06001100 RID: 4352 RVA: 0x0006ADEF File Offset: 0x00068FEF
			public VirtualBone(Vector3 position, Quaternion rotation)
			{
				this.Read(position, rotation);
			}

			// Token: 0x06001101 RID: 4353 RVA: 0x0006ADFF File Offset: 0x00068FFF
			public void Read(Vector3 position, Quaternion rotation)
			{
				this.readPosition = position;
				this.readRotation = rotation;
				this.solverPosition = position;
				this.solverRotation = rotation;
			}

			// Token: 0x06001102 RID: 4354 RVA: 0x0006AE20 File Offset: 0x00069020
			public static void SwingRotation(IKSolverVR.VirtualBone[] bones, int index, Vector3 swingTarget, float weight = 1f)
			{
				if (weight <= 0f)
				{
					return;
				}
				Quaternion quaternion = Quaternion.FromToRotation(bones[index].solverRotation * bones[index].axis, swingTarget - bones[index].solverPosition);
				if (weight < 1f)
				{
					quaternion = Quaternion.Lerp(Quaternion.identity, quaternion, weight);
				}
				for (int i = index; i < bones.Length; i++)
				{
					bones[i].solverRotation = quaternion * bones[i].solverRotation;
				}
			}

			// Token: 0x06001103 RID: 4355 RVA: 0x0006AE98 File Offset: 0x00069098
			public static float PreSolve(ref IKSolverVR.VirtualBone[] bones)
			{
				float num = 0f;
				for (int i = 0; i < bones.Length; i++)
				{
					if (i < bones.Length - 1)
					{
						bones[i].sqrMag = (bones[i + 1].solverPosition - bones[i].solverPosition).sqrMagnitude;
						bones[i].length = Mathf.Sqrt(bones[i].sqrMag);
						num += bones[i].length;
						bones[i].axis = Quaternion.Inverse(bones[i].solverRotation) * (bones[i + 1].solverPosition - bones[i].solverPosition);
					}
					else
					{
						bones[i].sqrMag = 0f;
						bones[i].length = 0f;
					}
				}
				return num;
			}

			// Token: 0x06001104 RID: 4356 RVA: 0x0006AF70 File Offset: 0x00069170
			public static void RotateAroundPoint(IKSolverVR.VirtualBone[] bones, int index, Vector3 point, Quaternion rotation)
			{
				for (int i = index; i < bones.Length; i++)
				{
					if (bones[i] != null)
					{
						Vector3 point2 = bones[i].solverPosition - point;
						bones[i].solverPosition = point + rotation * point2;
						bones[i].solverRotation = rotation * bones[i].solverRotation;
					}
				}
			}

			// Token: 0x06001105 RID: 4357 RVA: 0x0006AFCC File Offset: 0x000691CC
			public static void RotateBy(IKSolverVR.VirtualBone[] bones, int index, Quaternion rotation)
			{
				for (int i = index; i < bones.Length; i++)
				{
					if (bones[i] != null)
					{
						Vector3 point = bones[i].solverPosition - bones[index].solverPosition;
						bones[i].solverPosition = bones[index].solverPosition + rotation * point;
						bones[i].solverRotation = rotation * bones[i].solverRotation;
					}
				}
			}

			// Token: 0x06001106 RID: 4358 RVA: 0x0006B034 File Offset: 0x00069234
			public static void RotateBy(IKSolverVR.VirtualBone[] bones, Quaternion rotation)
			{
				for (int i = 0; i < bones.Length; i++)
				{
					if (bones[i] != null)
					{
						if (i > 0)
						{
							Vector3 point = bones[i].solverPosition - bones[0].solverPosition;
							bones[i].solverPosition = bones[0].solverPosition + rotation * point;
						}
						bones[i].solverRotation = rotation * bones[i].solverRotation;
					}
				}
			}

			// Token: 0x06001107 RID: 4359 RVA: 0x0006B0A0 File Offset: 0x000692A0
			public static void RotateTo(IKSolverVR.VirtualBone[] bones, int index, Quaternion rotation)
			{
				Quaternion rotation2 = QuaTools.FromToRotation(bones[index].solverRotation, rotation);
				IKSolverVR.VirtualBone.RotateAroundPoint(bones, index, bones[index].solverPosition, rotation2);
			}

			// Token: 0x06001108 RID: 4360 RVA: 0x0006B0CC File Offset: 0x000692CC
			public static void SolveTrigonometric(IKSolverVR.VirtualBone[] bones, int first, int second, int third, Vector3 targetPosition, Vector3 bendNormal, float weight)
			{
				if (weight <= 0f)
				{
					return;
				}
				targetPosition = Vector3.Lerp(bones[third].solverPosition, targetPosition, weight);
				Vector3 vector = targetPosition - bones[first].solverPosition;
				float sqrMagnitude = vector.sqrMagnitude;
				if (sqrMagnitude == 0f)
				{
					return;
				}
				float directionMag = Mathf.Sqrt(sqrMagnitude);
				float sqrMagnitude2 = (bones[second].solverPosition - bones[first].solverPosition).sqrMagnitude;
				float sqrMagnitude3 = (bones[third].solverPosition - bones[second].solverPosition).sqrMagnitude;
				Vector3 bendDirection = Vector3.Cross(vector, bendNormal);
				Vector3 directionToBendPoint = IKSolverVR.VirtualBone.GetDirectionToBendPoint(vector, directionMag, bendDirection, sqrMagnitude2, sqrMagnitude3);
				Quaternion quaternion = Quaternion.FromToRotation(bones[second].solverPosition - bones[first].solverPosition, directionToBendPoint);
				if (weight < 1f)
				{
					quaternion = Quaternion.Lerp(Quaternion.identity, quaternion, weight);
				}
				IKSolverVR.VirtualBone.RotateAroundPoint(bones, first, bones[first].solverPosition, quaternion);
				Quaternion quaternion2 = Quaternion.FromToRotation(bones[third].solverPosition - bones[second].solverPosition, targetPosition - bones[second].solverPosition);
				if (weight < 1f)
				{
					quaternion2 = Quaternion.Lerp(Quaternion.identity, quaternion2, weight);
				}
				IKSolverVR.VirtualBone.RotateAroundPoint(bones, second, bones[second].solverPosition, quaternion2);
			}

			// Token: 0x06001109 RID: 4361 RVA: 0x0006B214 File Offset: 0x00069414
			private static Vector3 GetDirectionToBendPoint(Vector3 direction, float directionMag, Vector3 bendDirection, float sqrMag1, float sqrMag2)
			{
				float num = (directionMag * directionMag + (sqrMag1 - sqrMag2)) / 2f / directionMag;
				float y = (float)Math.Sqrt((double)Mathf.Clamp(sqrMag1 - num * num, 0f, float.PositiveInfinity));
				if (direction == Vector3.zero)
				{
					return Vector3.zero;
				}
				return Quaternion.LookRotation(direction, bendDirection) * new Vector3(0f, y, num);
			}

			// Token: 0x0600110A RID: 4362 RVA: 0x0006B27C File Offset: 0x0006947C
			public static void SolveFABRIK(IKSolverVR.VirtualBone[] bones, Vector3 startPosition, Vector3 targetPosition, float weight, float minNormalizedTargetDistance, int iterations, float length, Vector3 startOffset)
			{
				if (weight <= 0f)
				{
					return;
				}
				if (minNormalizedTargetDistance > 0f)
				{
					Vector3 a = targetPosition - startPosition;
					float magnitude = a.magnitude;
					Vector3 b = startPosition + a / magnitude * Mathf.Max(length * minNormalizedTargetDistance, magnitude);
					targetPosition = Vector3.Lerp(targetPosition, b, weight);
				}
				for (int i = 0; i < iterations; i++)
				{
					bones[bones.Length - 1].solverPosition = Vector3.Lerp(bones[bones.Length - 1].solverPosition, targetPosition, weight);
					for (int j = bones.Length - 2; j > -1; j--)
					{
						bones[j].solverPosition = IKSolverVR.VirtualBone.SolveFABRIKJoint(bones[j].solverPosition, bones[j + 1].solverPosition, bones[j].length);
					}
					if (i == 0)
					{
						for (int k = 0; k < bones.Length; k++)
						{
							bones[k].solverPosition += startOffset;
						}
					}
					bones[0].solverPosition = startPosition;
					for (int l = 1; l < bones.Length; l++)
					{
						bones[l].solverPosition = IKSolverVR.VirtualBone.SolveFABRIKJoint(bones[l].solverPosition, bones[l - 1].solverPosition, bones[l - 1].length);
					}
				}
				for (int m = 0; m < bones.Length - 1; m++)
				{
					IKSolverVR.VirtualBone.SwingRotation(bones, m, bones[m + 1].solverPosition, 1f);
				}
			}

			// Token: 0x0600110B RID: 4363 RVA: 0x0006B3E8 File Offset: 0x000695E8
			private static Vector3 SolveFABRIKJoint(Vector3 pos1, Vector3 pos2, float length)
			{
				return pos2 + (pos1 - pos2).normalized * length;
			}

			// Token: 0x0600110C RID: 4364 RVA: 0x0006B410 File Offset: 0x00069610
			public static void SolveCCD(IKSolverVR.VirtualBone[] bones, Vector3 targetPosition, float weight, int iterations)
			{
				if (weight <= 0f)
				{
					return;
				}
				for (int i = 0; i < iterations; i++)
				{
					for (int j = bones.Length - 2; j > -1; j--)
					{
						Vector3 fromDirection = bones[bones.Length - 1].solverPosition - bones[j].solverPosition;
						Vector3 toDirection = targetPosition - bones[j].solverPosition;
						Quaternion quaternion = Quaternion.FromToRotation(fromDirection, toDirection);
						if (weight >= 1f)
						{
							IKSolverVR.VirtualBone.RotateBy(bones, j, quaternion);
						}
						else
						{
							IKSolverVR.VirtualBone.RotateBy(bones, j, Quaternion.Lerp(Quaternion.identity, quaternion, weight));
						}
					}
				}
			}

			// Token: 0x04000FA4 RID: 4004
			public Vector3 readPosition;

			// Token: 0x04000FA5 RID: 4005
			public Quaternion readRotation;

			// Token: 0x04000FA6 RID: 4006
			public Vector3 solverPosition;

			// Token: 0x04000FA7 RID: 4007
			public Quaternion solverRotation;

			// Token: 0x04000FA8 RID: 4008
			public float length;

			// Token: 0x04000FA9 RID: 4009
			public float sqrMag;

			// Token: 0x04000FAA RID: 4010
			public Vector3 axis;
		}
	}
}
