using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.AnimationTools
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	public class LegAnimationClipMotion
	{
		// Token: 0x06000204 RID: 516 RVA: 0x000109BC File Offset: 0x0000EBBC
		public LegAnimationClipMotion(AnimationClip clip)
		{
			this.analyzed = false;
			this.targetClip = clip;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00010A28 File Offset: 0x0000EC28
		public List<LegAnimationClipMotion.MotionSample> AnalyzeClip(Transform rootTr, Transform hips, Transform upperLeg, Transform knee, Transform foot, Transform optionalToes, Transform animator, int samples = 20, float heelTreshold = 1f, float toesTreshold = 1f, float holdOnGround = 0f, bool displayEditorProgressBar = false, bool removeRootMotion = false, float floorOffset = 0f, LegAnimationClipMotion.EAnalyzeMode mode = LegAnimationClipMotion.EAnalyzeMode.HeelFeetTight, int cutFirstFrames = 0, int cutLastFrames = 0)
		{
			this.analyzed = false;
			Matrix4x4 inverse = Matrix4x4.TRS(animator.position, animator.rotation, animator.lossyScale).inverse;
			this._latestToesTesh = toesTreshold;
			this._latestHeelTesh = heelTreshold;
			this._latestHoldOnGround = holdOnGround;
			this._latestAnalyzeMode = mode;
			this._latestFloorOffset = floorOffset;
			if (cutFirstFrames < 0)
			{
				cutFirstFrames = 0;
			}
			if (cutLastFrames < 0)
			{
				cutLastFrames = 0;
			}
			this.sampledData = new List<LegAnimationClipMotion.MotionSample>();
			LegAnimationClipMotion.StorePoseBackup(rootTr);
			this._initFootRot = foot.rotation;
			this._initFootLocRot = foot.localRotation;
			this._groundToFootHeight = rootTr.InverseTransformPoint(foot.position).y;
			float num = (float)cutFirstFrames / (float)samples;
			samples -= cutFirstFrames + cutLastFrames;
			float num2 = 1f / (float)samples;
			if (displayEditorProgressBar)
			{
				this.ProgressBar("Preparing foot analysis...", 0f);
			}
			Transform transform = null;
			SkinnedMeshRenderer[] componentsInChildren = animator.GetComponentsInChildren<SkinnedMeshRenderer>();
			if (componentsInChildren != null && componentsInChildren.Length != 0)
			{
				SkinnedMeshRenderer skinnedMeshRenderer = (from s in componentsInChildren
				orderby s.bones.Length
				select s).First<SkinnedMeshRenderer>();
				if (skinnedMeshRenderer != null)
				{
					transform = skinnedMeshRenderer.rootBone;
				}
			}
			if (transform == null)
			{
				transform = hips;
			}
			Vector3 vector = new Vector3(foot.position.x, rootTr.position.y, foot.position.z);
			vector = foot.InverseTransformPoint(vector);
			Vector3 normalized = foot.InverseTransformPoint(foot.position + rootTr.forward).normalized;
			Vector3 vector2;
			if (optionalToes)
			{
				vector2 = foot.InverseTransformPoint(optionalToes.position);
			}
			else
			{
				vector2 = normalized * (Vector3.Distance(foot.position, knee.position) + Vector3.Distance(upperLeg.position, knee.position)) * 0.325f;
			}
			Vector3 vector3;
			if (optionalToes)
			{
				Vector3 position = optionalToes.position;
				position.y = foot.position.y;
				vector3 = foot.InverseTransformPoint(position);
			}
			else
			{
				vector3 = vector2;
			}
			Vector3 normalized2 = vector2.normalized;
			this._footForward = normalized;
			this._footToToes = vector2;
			this._footToToesForw = vector3;
			this._footLocalToGround = vector;
			this._footRotMapping = Quaternion.FromToRotation(foot.InverseTransformDirection(rootTr.right), Vector3.right);
			this._footRotMapping *= Quaternion.FromToRotation(foot.InverseTransformDirection(rootTr.up), Vector3.up);
			Vector3 vector4 = rootTr.InverseTransformPoint(transform.position);
			this.GroundingCurve = new AnimationCurve();
			for (int i = 0; i < samples; i++)
			{
				if (displayEditorProgressBar)
				{
					this.ProgressBar("Sampling leg animation data " + i.ToString() + " / " + samples.ToString(), (float)i / (float)samples);
				}
				LegAnimationClipMotion.MotionSample motionSample = new LegAnimationClipMotion.MotionSample();
				this.targetClip.SampleAnimation(animator.gameObject, num + num2 * (float)i * this.targetClip.length);
				if (removeRootMotion)
				{
					transform.localPosition = Vector3.zero;
					Vector3 position2 = animator.InverseTransformPoint(hips.position);
					position2.z = 0f;
					hips.position = animator.TransformPoint(position2);
				}
				motionSample.sampledAnkleRoot = inverse.MultiplyPoint(foot.position);
				Vector3 position3 = transform.position;
				Vector3 vector5 = rootTr.InverseTransformPoint(transform.position);
				transform.position = rootTr.TransformPoint(vector5.x, vector5.y, vector4.z);
				motionSample.sampledRootLocal = rootTr.InverseTransformPoint(transform.position);
				motionSample.sampledFootInRMLocal = transform.InverseTransformPoint(transform.position);
				motionSample.sampledFootInAnimLocal = animator.InverseTransformPoint(transform.position);
				Vector3 vector6 = foot.position;
				motionSample.sampledAnkleLocal = rootTr.InverseTransformPoint(vector6);
				vector6 += foot.TransformDirection(vector);
				motionSample.sampledFootLocal = rootTr.InverseTransformPoint(vector6);
				Vector3 vector7 = this.GetSamplingToesPoint(foot, toesTreshold);
				Vector3 vector8 = this.GetSamplingHeelPoint(foot, heelTreshold);
				if (mode == LegAnimationClipMotion.EAnalyzeMode.HeelFeetTight)
				{
					motionSample.sampledToesLocal = rootTr.InverseTransformPoint(vector7);
					motionSample.sampledHeelLocal = rootTr.InverseTransformPoint(vector8);
				}
				else
				{
					vector7 = foot.position;
					vector7 += foot.TransformDirection(vector2);
					vector7 += foot.TransformDirection(vector3) * vector.magnitude * 4f;
					motionSample.sampledToesLocal = rootTr.InverseTransformPoint(vector7);
					vector8 = foot.position;
					vector8 += foot.TransformDirection(vector);
					vector8 -= foot.TransformDirection(normalized) * vector.magnitude * 0.8f;
					motionSample.sampledHeelLocal = rootTr.InverseTransformPoint(vector8);
				}
				motionSample.sampledKneeLocal = rootTr.InverseTransformPoint(knee.position);
				motionSample.sampledUpperLegLocal = rootTr.InverseTransformPoint(upperLeg.position);
				this.sampledData.Add(motionSample);
			}
			if (displayEditorProgressBar)
			{
				this.ProgressBar("Restoring character pose", 1f);
			}
			AnimationClip animationClip = this.targetClip;
			string[] array = new string[]
			{
				"idle",
				"stop",
				"none"
			};
			Animator component = animator.GetComponent<Animator>();
			if (component)
			{
				if (component.runtimeAnimatorController)
				{
					for (int j = 0; j < component.runtimeAnimatorController.animationClips.Length; j++)
					{
						for (int k = 0; k < array.Length; k++)
						{
							if (component.runtimeAnimatorController.animationClips[j].name.ToLower().Contains(array[k]))
							{
								animationClip = component.runtimeAnimatorController.animationClips[j];
								break;
							}
						}
					}
				}
				else
				{
					Debug.Log("[Error] No Animator Controller in " + component.name);
				}
			}
			animationClip.SampleAnimation(animator.gameObject, 0f);
			if (displayEditorProgressBar)
			{
				this.ProgressBar("Checking collected data", 0f);
			}
			this.LowestFootCoords = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			this.HighestFootCoords = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			for (int l = 0; l < samples; l++)
			{
				if (this.sampledData[l].sampledFootLocal.x < this.LowestFootCoords.x)
				{
					this.LowestFootCoords.x = this.sampledData[l].sampledFootLocal.x;
				}
				if (this.sampledData[l].sampledFootLocal.y < this.LowestFootCoords.y)
				{
					this.LowestFootCoords.y = this.sampledData[l].sampledFootLocal.y;
				}
				if (this.sampledData[l].sampledFootLocal.z < this.LowestFootCoords.z)
				{
					this.LowestFootCoords.z = this.sampledData[l].sampledFootLocal.z;
				}
				if (this.sampledData[l].sampledFootLocal.x > this.HighestFootCoords.x)
				{
					this.HighestFootCoords.x = this.sampledData[l].sampledFootLocal.x;
				}
				if (this.sampledData[l].sampledFootLocal.y > this.HighestFootCoords.y)
				{
					this.HighestFootCoords.y = this.sampledData[l].sampledFootLocal.y;
				}
				if (this.sampledData[l].sampledFootLocal.z > this.HighestFootCoords.z)
				{
					this.HighestFootCoords.z = this.sampledData[l].sampledFootLocal.z;
				}
			}
			this.FootCoordsDiffs = new Vector3(this.LowestFootCoords.x - this.HighestFootCoords.x, this.LowestFootCoords.y - this.HighestFootCoords.y, this.LowestFootCoords.z - this.HighestFootCoords.z);
			float refScale = this.GetRefScale(knee, foot);
			float heightTresholdScale = this.GetHeightTresholdScale(knee, foot, new float?(refScale));
			bool? flag = null;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 1f / (float)samples;
			this.approximateFootLocalXPosDuringPush = this.sampledData[0].sampledFootLocal.x;
			this.approximateFootLocalZPosDuringPush = this.sampledData[0].sampledFootLocal.z;
			bool flag2 = false;
			bool flag3 = false;
			int m = 0;
			while (m < samples)
			{
				if (displayEditorProgressBar)
				{
					this.ProgressBar("Sampling collected data " + m.ToString() + " / " + samples.ToString(), (float)m / (float)samples);
				}
				float num6 = (float)m / (float)samples;
				LegAnimationClipMotion.MotionSample motionSample2 = this.sampledData[m];
				motionSample2.grounded = false;
				if (mode == LegAnimationClipMotion.EAnalyzeMode.HeelFeetTight)
				{
					if (motionSample2.sampledHeelLocal.y < heightTresholdScale * heelTreshold + floorOffset)
					{
						motionSample2.grounded = true;
					}
					if (motionSample2.sampledToesLocal.y < heightTresholdScale * toesTreshold + floorOffset)
					{
						motionSample2.grounded = true;
					}
				}
				else if (motionSample2.sampledHeelLocal.y < heightTresholdScale * heelTreshold + floorOffset || motionSample2.sampledFootLocal.y < heightTresholdScale * toesTreshold + floorOffset)
				{
					motionSample2.grounded = true;
				}
				if (!motionSample2.grounded)
				{
					goto IL_9F2;
				}
				bool? flag4 = flag;
				bool flag5 = true;
				if (!(flag4.GetValueOrDefault() == flag5 & flag4 != null))
				{
					goto IL_9F2;
				}
				num3 += num5;
				num4 = 0f;
				IL_A3B:
				flag4 = flag;
				flag5 = true;
				if (!(flag4.GetValueOrDefault() == flag5 & flag4 != null) && motionSample2.grounded)
				{
					this.startStepFootLocal = motionSample2.sampledFootLocal;
					this.startStepFootLocalIndex = m;
					this.startStepFootProgress = (float)m / (float)samples;
				}
				else
				{
					flag4 = flag;
					flag5 = true;
					if ((flag4.GetValueOrDefault() == flag5 & flag4 != null) && !motionSample2.grounded)
					{
						this.endStepFootLocal = motionSample2.sampledFootLocal;
						this.endStepFootLocalIndex = m;
						this.endStepFootProgress = (float)m / (float)samples;
					}
				}
				flag = new bool?(motionSample2.grounded);
				if (motionSample2.grounded)
				{
					float num7 = num6 - 2f / (float)samples;
					if (num7 < 0f)
					{
						num7 += 1f;
					}
					Vector3 b = this.GetFootLocalPosition(num6) - this.GetFootLocalPosition(num7);
					b.y = 0f;
					this.approximateFootPushDirection += b;
					if (!flag2)
					{
						flag2 = true;
						this.approximateFootLocalXPosDuringPush = motionSample2.sampledFootLocal.x;
					}
					else
					{
						this.approximateFootLocalXPosDuringPush = Mathf.Lerp(this.approximateFootLocalXPosDuringPush, motionSample2.sampledFootLocal.x, 0.5f);
					}
					if (!flag3)
					{
						flag3 = true;
						this.approximateFootLocalZPosDuringPush = motionSample2.sampledFootLocal.z;
					}
					else
					{
						this.approximateFootLocalZPosDuringPush = Mathf.Lerp(this.approximateFootLocalZPosDuringPush, motionSample2.sampledFootLocal.z, 0.5f);
					}
				}
				this.approximateFootPushDirection.Normalize();
				this.approximateFootPushDirectionDominant = FVectorMethods.ChooseDominantAxis(this.approximateFootPushDirection);
				Vector3 footLocalPosition = this.GetFootLocalPosition(num6 - num2);
				if (this.GetFootLocalPosition(num6).z > footLocalPosition.z)
				{
					motionSample2.swingForwards = true;
				}
				float num8 = refScale;
				if (num8 < 0.4f)
				{
					num8 = 0.4f;
				}
				float value = Mathf.Clamp(motionSample2.sampledFootLocal.y / num8, 0.2f, 1f);
				if (motionSample2.grounded)
				{
					value = 0f;
				}
				this.GroundingCurve.AddKey(num6, value);
				m++;
				continue;
				IL_9F2:
				if (num3 > 0f)
				{
					if (holdOnGround > 0f && num3 < holdOnGround)
					{
						num3 += num5;
						motionSample2.grounded = true;
						goto IL_A3B;
					}
					goto IL_A3B;
				}
				else
				{
					num4 += num5;
					if (num4 > num5 * 3f && num3 > holdOnGround)
					{
						num3 = 0f;
						goto IL_A3B;
					}
					goto IL_A3B;
				}
			}
			List<Keyframe> list = new List<Keyframe>();
			list.Add(this.GroundingCurve.keys[0]);
			for (int n = 1; n < this.GroundingCurve.keys.Length - 1; n++)
			{
				if (this.GroundingCurve.keys[n].value == 0f && this.GroundingCurve.keys[n + 1].value > 0f)
				{
					list.Add(this.GroundingCurve.keys[n]);
				}
				else if (this.GroundingCurve.keys[n - 1].value > 0f && this.GroundingCurve.keys[n].value == 0f)
				{
					list.Add(this.GroundingCurve.keys[n]);
				}
				else if (this.GroundingCurve.keys[n - 1].value != this.GroundingCurve.keys[n].value)
				{
					list.Add(this.GroundingCurve.keys[n]);
				}
			}
			list.Add(this.GroundingCurve.keys[this.GroundingCurve.keys.Length - 1]);
			this.GroundingCurve = new AnimationCurve(list.ToArray());
			for (int num9 = 0; num9 < this.GroundingCurve.keys.Length; num9++)
			{
				this.GroundingCurve.SmoothTangents(num9, 0.2f);
			}
			this.GroundingCurve = AnimationGenerateUtils.ReduceKeyframes(this.GroundingCurve, 0.022f);
			for (int num10 = 0; num10 < samples; num10++)
			{
				if (displayEditorProgressBar)
				{
					this.ProgressBar("Defining Additional Data " + num10.ToString() + " / " + samples.ToString(), (float)num10 / (float)samples);
				}
				LegAnimationClipMotion.MotionSample motionSample3 = this.sampledData[num10];
				if (!motionSample3.grounded)
				{
					if (motionSample3.sampledFootLocal.z < 0f)
					{
						if (motionSample3.swingForwards && motionSample3.sampledFootLocal.z > this.LowestFootCoords.z / 2f)
						{
							motionSample3.predictState = true;
						}
					}
					else
					{
						motionSample3.predictState = true;
					}
				}
			}
			if (displayEditorProgressBar)
			{
				this.ProgressBar("Finalizing", 1f);
			}
			LegAnimationClipMotion.RestorePoseBackup();
			if (displayEditorProgressBar)
			{
				this.ProgressBar("", 1.1f);
			}
			this.analyzed = true;
			return this.sampledData;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00011938 File Offset: 0x0000FB38
		public float GetRefScale(Transform knee, Transform foot)
		{
			return Vector3.Distance(foot.position, knee.position) * 0.1f;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00011954 File Offset: 0x0000FB54
		public float GetTresholdLength(Transform knee, Transform foot, float treshold, float amplification = 1f)
		{
			float num = this.GetHeightTresholdScale(knee, foot, null) + this.LowestFootCoords.y;
			return this.GetAmplified02Range(1f - treshold, amplification) * num;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00011990 File Offset: 0x0000FB90
		public float GetHeightTresholdScale(Transform knee, Transform foot, float? refScale = null)
		{
			float num;
			if (refScale == null)
			{
				num = this.GetRefScale(knee, foot);
			}
			else
			{
				num = refScale.Value;
			}
			return num + this.LowestFootCoords.y;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000119C6 File Offset: 0x0000FBC6
		public Vector3 GetToesFootLocalPosition(float progress, float toesToFoot = 0.5f)
		{
			return Vector3.LerpUnclamped(this.GetToesLocalPosition(progress), this.GetFootLocalPosition(progress), toesToFoot);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000119DC File Offset: 0x0000FBDC
		public Vector3 GetToesLocalPosition(float progress)
		{
			this.RefreshInterpolationIndexes(progress);
			return Vector3.LerpUnclamped(this.sampledData[this.i_lowerIndex].sampledToesLocal, this.sampledData[this.i_higherIndex].sampledToesLocal, this.i_progress);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00011A1C File Offset: 0x0000FC1C
		public Vector3 GetAnkleLocalPosition(float progress)
		{
			this.RefreshInterpolationIndexes(progress);
			return Vector3.LerpUnclamped(this.sampledData[this.i_lowerIndex].sampledAnkleLocal, this.sampledData[this.i_higherIndex].sampledAnkleLocal, this.i_progress);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00011A5C File Offset: 0x0000FC5C
		public LegAnimationClipMotion.MotionSample GetSampleInProgress(float progress, bool higher = true)
		{
			this.RefreshInterpolationIndexes(progress);
			if (higher)
			{
				return this.sampledData[this.i_higherIndex];
			}
			return this.sampledData[this.i_lowerIndex];
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00011A8C File Offset: 0x0000FC8C
		public Vector3 GetFootLocalPositionInAnimator(float progress)
		{
			progress = LegAnimationClipMotion.RoundCycle(progress);
			this.RefreshInterpolationIndexes(progress);
			return Vector3.LerpUnclamped(this.sampledData[this.i_lowerIndex].sampledFootInAnimLocal, this.sampledData[this.i_higherIndex].sampledFootInAnimLocal, this.i_progress);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00011AE0 File Offset: 0x0000FCE0
		public Vector3 GetFootLocalPositionInRootMotion(float progress)
		{
			progress = LegAnimationClipMotion.RoundCycle(progress);
			this.RefreshInterpolationIndexes(progress);
			return Vector3.LerpUnclamped(this.sampledData[this.i_lowerIndex].sampledFootInRMLocal, this.sampledData[this.i_higherIndex].sampledFootInRMLocal, this.i_progress);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00011B34 File Offset: 0x0000FD34
		public Vector3 GetFootLocalPosition(float progress)
		{
			progress = LegAnimationClipMotion.RoundCycle(progress);
			this.RefreshInterpolationIndexes(progress);
			return Vector3.LerpUnclamped(this.sampledData[this.i_lowerIndex].sampledFootLocal, this.sampledData[this.i_higherIndex].sampledFootLocal, this.i_progress);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00011B87 File Offset: 0x0000FD87
		public Vector3 GetHeelLocalPosition(float progress)
		{
			this.RefreshInterpolationIndexes(progress);
			return Vector3.LerpUnclamped(this.sampledData[this.i_lowerIndex].sampledHeelLocal, this.sampledData[this.i_higherIndex].sampledHeelLocal, this.i_progress);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00011BC7 File Offset: 0x0000FDC7
		public Vector3 GetUpperLegLocalPosition(float progress)
		{
			this.RefreshInterpolationIndexes(progress);
			return Vector3.LerpUnclamped(this.sampledData[this.i_lowerIndex].sampledUpperLegLocal, this.sampledData[this.i_higherIndex].sampledUpperLegLocal, this.i_progress);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00011C07 File Offset: 0x0000FE07
		public Vector3 GetRootLocalPosition(float progress)
		{
			this.RefreshInterpolationIndexes(progress);
			return Vector3.LerpUnclamped(this.sampledData[this.i_lowerIndex].sampledRootLocal, this.sampledData[this.i_higherIndex].sampledRootLocal, this.i_progress);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00011C47 File Offset: 0x0000FE47
		public Vector3 GetKneeLocalPosition(float progress)
		{
			this.RefreshInterpolationIndexes(progress);
			return Vector3.LerpUnclamped(this.sampledData[this.i_lowerIndex].sampledKneeLocal, this.sampledData[this.i_higherIndex].sampledKneeLocal, this.i_progress);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00011C87 File Offset: 0x0000FE87
		public bool GroundedIn(float progress)
		{
			return this.sampledData != null && this.sampledData.Count != 0 && this.GroundingCurve.Evaluate(progress) < 0.05f;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00011CB8 File Offset: 0x0000FEB8
		public bool PredictIn(float progress)
		{
			this.RefreshInterpolationIndexes(progress);
			if (this.i_progress > 0.5f)
			{
				return this.sampledData[this.i_higherIndex].predictState;
			}
			return this.sampledData[this.i_lowerIndex].predictState;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00011D08 File Offset: 0x0000FF08
		public bool GetSwingingForward(float progress)
		{
			this.RefreshInterpolationIndexes(progress);
			if (this.i_progress > 0.5f)
			{
				return this.sampledData[this.i_higherIndex].swingForwards;
			}
			return this.sampledData[this.i_lowerIndex].swingForwards;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00011D58 File Offset: 0x0000FF58
		public float GetAmplified02Range(float enterValue, float amplifyAfter1UpTo2 = 5f)
		{
			if (enterValue <= 1f)
			{
				return enterValue;
			}
			float num = -(1f - enterValue);
			return enterValue + num * amplifyAfter1UpTo2;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00011D80 File Offset: 0x0000FF80
		public void RefreshInterpolationIndexes(float progress)
		{
			if (this.sampledData == null)
			{
				return;
			}
			if (this.sampledData.Count == 0)
			{
				return;
			}
			if (progress == this.i_forProgress && this.i_gameFrame == Time.frameCount)
			{
				return;
			}
			this.i_gameFrame = Time.frameCount;
			this.i_forProgress = progress;
			this.i_progress = LegAnimationClipMotion.RoundCycle(this.i_forProgress);
			this.i_higherIndex = Mathf.CeilToInt(this.i_progress * (float)this.sampledData.Count);
			if (this.i_higherIndex > this.sampledData.Count - 1)
			{
				this.i_lowerIndex = this.sampledData.Count - 1;
				this.i_higherIndex = 0;
				this.i_progress = Mathf.InverseLerp((float)this.i_lowerIndex, (float)this.sampledData.Count, this.i_progress * (float)this.sampledData.Count);
				return;
			}
			this.i_lowerIndex = Mathf.FloorToInt(this.i_progress * (float)this.sampledData.Count);
			this.i_progress = Mathf.InverseLerp((float)this.i_lowerIndex, (float)this.i_higherIndex, this.i_progress * (float)this.sampledData.Count);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00011EA7 File Offset: 0x000100A7
		private void ProgressBar(string text, float prog)
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00011EA9 File Offset: 0x000100A9
		public static float RoundCycle(float cycleProgress)
		{
			return cycleProgress - Mathf.Floor(cycleProgress);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00011EB3 File Offset: 0x000100B3
		public LegAnimationClipMotion GetCopy()
		{
			return base.MemberwiseClone() as LegAnimationClipMotion;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00011EC0 File Offset: 0x000100C0
		private static void StorePoseBackup(Transform rootTransform)
		{
			LegAnimationClipMotion.poseBackup.Clear();
			foreach (Transform transform in rootTransform.GetComponentsInChildren<Transform>())
			{
				LegAnimationClipMotion.TransformsBackup item = default(LegAnimationClipMotion.TransformsBackup);
				item.t = transform;
				item.localPos = transform.localPosition;
				item.localRot = transform.localRotation;
				item.localScale = transform.localScale;
				LegAnimationClipMotion.poseBackup.Add(item);
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00011F34 File Offset: 0x00010134
		private static void RestorePoseBackup()
		{
			for (int i = LegAnimationClipMotion.poseBackup.Count - 1; i >= 0; i--)
			{
				LegAnimationClipMotion.poseBackup[i].Restore();
			}
			LegAnimationClipMotion.poseBackup.Clear();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00011F78 File Offset: 0x00010178
		public Vector3 GetSamplingToesPoint(Transform foot, float tresh)
		{
			return foot.position + foot.TransformDirection(this._footLocalToGround) + foot.TransformDirection(this._footToToesForw) * this._footLocalToGround.magnitude * (4f - Mathf.LerpUnclamped(0f, 0.1f, tresh));
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00011FD8 File Offset: 0x000101D8
		public Vector3 GetSamplingHeelPoint(Transform foot, float tresh)
		{
			return foot.position + foot.TransformDirection(this._footLocalToGround) - foot.TransformDirection(this._footForward) * this._footLocalToGround.magnitude * (0.6f - Mathf.Lerp(0f, 2f, tresh));
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00012038 File Offset: 0x00010238
		// Note: this type is marked as 'beforefieldinit'.
		static LegAnimationClipMotion()
		{
		}

		// Token: 0x04000206 RID: 518
		public bool analyzed;

		// Token: 0x04000207 RID: 519
		public AnimationClip targetClip;

		// Token: 0x04000208 RID: 520
		public List<LegAnimationClipMotion.MotionSample> sampledData;

		// Token: 0x04000209 RID: 521
		public Vector3 LowestFootCoords;

		// Token: 0x0400020A RID: 522
		public Vector3 HighestFootCoords;

		// Token: 0x0400020B RID: 523
		public Vector3 FootCoordsDiffs;

		// Token: 0x0400020C RID: 524
		public Vector3 startStepFootLocal;

		// Token: 0x0400020D RID: 525
		public int startStepFootLocalIndex;

		// Token: 0x0400020E RID: 526
		public float startStepFootProgress;

		// Token: 0x0400020F RID: 527
		public Vector3 endStepFootLocal;

		// Token: 0x04000210 RID: 528
		public int endStepFootLocalIndex;

		// Token: 0x04000211 RID: 529
		public float endStepFootProgress;

		// Token: 0x04000212 RID: 530
		public Vector3 approximateFootPushDirection;

		// Token: 0x04000213 RID: 531
		public Vector3 approximateFootPushDirectionDominant;

		// Token: 0x04000214 RID: 532
		public float approximateFootLocalXPosDuringPush;

		// Token: 0x04000215 RID: 533
		public float approximateFootLocalZPosDuringPush;

		// Token: 0x04000216 RID: 534
		public Vector3 _footForward = Vector3.forward;

		// Token: 0x04000217 RID: 535
		public Vector3 _footToToes = Vector3.forward;

		// Token: 0x04000218 RID: 536
		public Vector3 _footToToesForw = Vector3.forward;

		// Token: 0x04000219 RID: 537
		public Vector3 _footLocalToGround = Vector3.zero;

		// Token: 0x0400021A RID: 538
		public float _latestToesTesh = 1f;

		// Token: 0x0400021B RID: 539
		public float _latestHeelTesh = 1f;

		// Token: 0x0400021C RID: 540
		public float _latestHoldOnGround;

		// Token: 0x0400021D RID: 541
		public float _latestFloorOffset;

		// Token: 0x0400021E RID: 542
		public LegAnimationClipMotion.EAnalyzeMode _latestAnalyzeMode;

		// Token: 0x0400021F RID: 543
		public float _groundToFootHeight;

		// Token: 0x04000220 RID: 544
		public Quaternion _initFootRot;

		// Token: 0x04000221 RID: 545
		public Quaternion _initFootLocRot;

		// Token: 0x04000222 RID: 546
		public Quaternion _footRotMapping;

		// Token: 0x04000223 RID: 547
		public AnimationCurve GroundingCurve;

		// Token: 0x04000224 RID: 548
		private int i_gameFrame = -1;

		// Token: 0x04000225 RID: 549
		public int i_lowerIndex;

		// Token: 0x04000226 RID: 550
		public int i_higherIndex;

		// Token: 0x04000227 RID: 551
		public float i_progress;

		// Token: 0x04000228 RID: 552
		public float i_forProgress;

		// Token: 0x04000229 RID: 553
		public static List<LegAnimationClipMotion.TransformsBackup> poseBackup = new List<LegAnimationClipMotion.TransformsBackup>();

		// Token: 0x0200019E RID: 414
		public enum EAnalyzeMode
		{
			// Token: 0x04000CC5 RID: 3269
			HeelFeetTight,
			// Token: 0x04000CC6 RID: 3270
			HeelFeetWide
		}

		// Token: 0x0200019F RID: 415
		[Serializable]
		public class MotionSample
		{
			// Token: 0x06000ECE RID: 3790 RVA: 0x000602DA File Offset: 0x0005E4DA
			public LegAnimationClipMotion.MotionSample GetCopy()
			{
				return base.MemberwiseClone() as LegAnimationClipMotion.MotionSample;
			}

			// Token: 0x06000ECF RID: 3791 RVA: 0x000602E7 File Offset: 0x0005E4E7
			public MotionSample()
			{
			}

			// Token: 0x04000CC7 RID: 3271
			public Vector3 sampledToesLocal;

			// Token: 0x04000CC8 RID: 3272
			public Vector3 sampledAnkleRoot;

			// Token: 0x04000CC9 RID: 3273
			public Vector3 sampledFootLocal;

			// Token: 0x04000CCA RID: 3274
			public Vector3 sampledAnkleLocal;

			// Token: 0x04000CCB RID: 3275
			public Vector3 sampledHeelLocal;

			// Token: 0x04000CCC RID: 3276
			public Vector3 sampledKneeLocal;

			// Token: 0x04000CCD RID: 3277
			public Vector3 sampledUpperLegLocal;

			// Token: 0x04000CCE RID: 3278
			public Vector3 sampledRootLocal;

			// Token: 0x04000CCF RID: 3279
			public Vector3 sampledFootInRMLocal;

			// Token: 0x04000CD0 RID: 3280
			public Vector3 sampledFootInAnimLocal;

			// Token: 0x04000CD1 RID: 3281
			public bool grounded;

			// Token: 0x04000CD2 RID: 3282
			public bool predictState;

			// Token: 0x04000CD3 RID: 3283
			public bool swingForwards;
		}

		// Token: 0x020001A0 RID: 416
		public struct TransformsBackup
		{
			// Token: 0x06000ED0 RID: 3792 RVA: 0x000602F0 File Offset: 0x0005E4F0
			public void Restore()
			{
				if (this.t == null)
				{
					return;
				}
				this.t.localPosition = this.localPos;
				this.t.localRotation = this.localRot;
				this.t.localScale = this.localScale;
			}

			// Token: 0x04000CD4 RID: 3284
			public Transform t;

			// Token: 0x04000CD5 RID: 3285
			public Vector3 localPos;

			// Token: 0x04000CD6 RID: 3286
			public Quaternion localRot;

			// Token: 0x04000CD7 RID: 3287
			public Vector3 localScale;
		}

		// Token: 0x020001A1 RID: 417
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000ED1 RID: 3793 RVA: 0x0006033F File Offset: 0x0005E53F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000ED2 RID: 3794 RVA: 0x0006034B File Offset: 0x0005E54B
			public <>c()
			{
			}

			// Token: 0x06000ED3 RID: 3795 RVA: 0x00060353 File Offset: 0x0005E553
			internal int <AnalyzeClip>b__32_0(SkinnedMeshRenderer s)
			{
				return s.bones.Length;
			}

			// Token: 0x04000CD8 RID: 3288
			public static readonly LegAnimationClipMotion.<>c <>9 = new LegAnimationClipMotion.<>c();

			// Token: 0x04000CD9 RID: 3289
			public static Func<SkinnedMeshRenderer, int> <>9__32_0;
		}
	}
}
