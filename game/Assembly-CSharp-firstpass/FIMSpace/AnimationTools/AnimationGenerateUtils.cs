using System;
using UnityEngine;

namespace FIMSpace.AnimationTools
{
	// Token: 0x0200004A RID: 74
	public static class AnimationGenerateUtils
	{
		// Token: 0x060001FF RID: 511 RVA: 0x00010430 File Offset: 0x0000E630
		public static AnimationCurve ReduceKeyframes(AnimationCurve curve, float maxError)
		{
			Keyframe[] array = curve.keys;
			int num = 1;
			while (array.Length > 2 && num < array.Length - 1)
			{
				Keyframe[] array2 = new Keyframe[array.Length - 1];
				int num2 = 0;
				for (int i = 0; i < array.Length; i++)
				{
					if (num != i)
					{
						array2[num2] = new Keyframe(array[i].time, array[i].value, array[i].inTangent, array[i].outTangent);
						num2++;
					}
				}
				AnimationCurve animationCurve = new AnimationCurve();
				animationCurve.keys = array2;
				float num3 = Mathf.Abs(animationCurve.Evaluate(array[num].time) - array[num].value);
				float time = array[num].time + (array[num - 1].time - array[num].time) * 0.5f;
				float time2 = array[num].time + (array[num + 1].time - array[num].time) * 0.5f;
				float num4 = Mathf.Abs(animationCurve.Evaluate(time) - curve.Evaluate(time));
				float num5 = Mathf.Abs(animationCurve.Evaluate(time2) - curve.Evaluate(time2));
				if (num3 < maxError && num4 < maxError && num5 < maxError)
				{
					array = array2;
				}
				else
				{
					num++;
				}
			}
			return new AnimationCurve(array);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000105A4 File Offset: 0x0000E7A4
		public static void LoopCurve(ref AnimationCurve curve, bool averageBoth = false, float? endTime = null)
		{
			float num = 0f;
			if (curve.keys.Length != 0)
			{
				num = curve.keys[0].value;
			}
			if (endTime == null)
			{
				if (curve.keys.Length == 0)
				{
					curve.AddKey(new Keyframe(0f, num));
					curve.AddKey(new Keyframe(1f, num));
					return;
				}
				if (curve.keys.Length == 1)
				{
					curve.AddKey(new Keyframe(Mathf.Max(1f, curve.keys[0].time + 0.5f), num));
					return;
				}
				float value = num;
				if (averageBoth)
				{
					value = Mathf.Lerp(num, curve.keys[curve.keys.Length - 1].value, 0.5f);
				}
				curve.MoveKey(0, new Keyframe(curve.keys[0].time, value));
				curve.MoveKey(curve.keys.Length - 1, new Keyframe(curve.keys[curve.keys.Length - 1].time, value));
				return;
			}
			else
			{
				float value2 = endTime.Value;
				if (curve.keys.Length == 0)
				{
					curve.AddKey(new Keyframe(0f, num));
					curve.AddKey(new Keyframe(value2, num));
					return;
				}
				if (curve.keys.Length == 1)
				{
					curve.AddKey(new Keyframe(value2, num));
					return;
				}
				float value3 = num;
				if (averageBoth)
				{
					value3 = Mathf.Lerp(num, curve.keys[curve.keys.Length - 1].value, 0.5f);
				}
				Keyframe keyframe = curve.keys[curve.keys.Length - 1];
				float num2 = keyframe.time;
				if (num2 != value2 && num2 < value2 && value2 - num2 < value2 * 0.1f)
				{
					num2 = value2;
				}
				curve.MoveKey(0, new Keyframe(curve.keys[0].time, value3));
				curve.MoveKey(curve.keys.Length - 1, new Keyframe(num2, value3));
				return;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000107C8 File Offset: 0x0000E9C8
		public static void DistrubuteCurveOnTime(ref AnimationCurve curve, float startTime, float endTime)
		{
			float time = curve.keys[0].time;
			float time2 = curve.keys[curve.keys.Length - 1].time;
			Keyframe[] array = new Keyframe[curve.keys.Length];
			curve.keys.CopyTo(array, 0);
			AnimationCurve animationCurve = new AnimationCurve(array);
			while (curve.keys.Length != 0)
			{
				curve.RemoveKey(curve.keys.Length - 1);
			}
			for (int i = 0; i < animationCurve.keys.Length; i++)
			{
				Keyframe keyframe = animationCurve.keys[i];
				Keyframe key = keyframe;
				key.time = Mathf.Lerp(startTime, endTime, Mathf.InverseLerp(time, time2, keyframe.time));
				curve.AddKey(key);
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00010898 File Offset: 0x0000EA98
		public static Quaternion EnsureQuaternionContinuity(Quaternion latestRot, Quaternion targetRot, bool normalize = false)
		{
			Quaternion quaternion = new Quaternion(-targetRot.x, -targetRot.y, -targetRot.z, -targetRot.w);
			Quaternion b = new Quaternion(Mathf.LerpUnclamped(latestRot.x, targetRot.x, 0.5f), Mathf.LerpUnclamped(latestRot.y, targetRot.y, 0.5f), Mathf.LerpUnclamped(latestRot.z, targetRot.z, 0.5f), Mathf.LerpUnclamped(latestRot.w, targetRot.w, 0.5f));
			Quaternion b2 = new Quaternion(Mathf.LerpUnclamped(latestRot.x, quaternion.x, 0.5f), Mathf.LerpUnclamped(latestRot.y, quaternion.y, 0.5f), Mathf.LerpUnclamped(latestRot.z, quaternion.z, 0.5f), Mathf.LerpUnclamped(latestRot.w, quaternion.w, 0.5f));
			float num = Quaternion.Angle(latestRot, b);
			float num2 = Quaternion.Angle(latestRot, b2);
			if (normalize)
			{
				if (num2 >= num)
				{
					return targetRot.normalized;
				}
				return quaternion.normalized;
			}
			else
			{
				if (num2 >= num)
				{
					return targetRot;
				}
				return quaternion;
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000109B7 File Offset: 0x0000EBB7
		public static void UpdateHumanoidIKPreview(Animator mecanim, AnimationClip clip, float time, bool restoreAnimator = true)
		{
		}
	}
}
