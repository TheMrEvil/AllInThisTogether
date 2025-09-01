using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000AB RID: 171
	public static class BakerUtilities
	{
		// Token: 0x060007B0 RID: 1968 RVA: 0x00034EE5 File Offset: 0x000330E5
		public static void ReduceKeyframes(AnimationCurve curve, float maxError)
		{
			if (maxError <= 0f)
			{
				return;
			}
			curve.keys = BakerUtilities.GetReducedKeyframes(curve, maxError);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00034F00 File Offset: 0x00033100
		public static Keyframe[] GetReducedKeyframes(AnimationCurve curve, float maxError)
		{
			Keyframe[] array = curve.keys;
			int num = 1;
			while (num < array.Length - 1 && array.Length > 2)
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
			return array;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00035070 File Offset: 0x00033270
		public static void SetLoopFrame(float time, AnimationCurve curve)
		{
			Keyframe[] keys = curve.keys;
			keys[keys.Length - 1].value = keys[0].value;
			float inTangent = Mathf.Lerp(keys[0].inTangent, keys[keys.Length - 1].inTangent, 0.5f);
			keys[0].inTangent = inTangent;
			keys[keys.Length - 1].inTangent = inTangent;
			float outTangent = Mathf.Lerp(keys[0].outTangent, keys[keys.Length - 1].outTangent, 0.5f);
			keys[0].outTangent = outTangent;
			keys[keys.Length - 1].outTangent = outTangent;
			keys[keys.Length - 1].time = time;
			curve.keys = keys;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00035142 File Offset: 0x00033342
		public static void SetTangentMode(AnimationCurve curve)
		{
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00035144 File Offset: 0x00033344
		public static Quaternion EnsureQuaternionContinuity(Quaternion lastQ, Quaternion q)
		{
			Quaternion quaternion = new Quaternion(-q.x, -q.y, -q.z, -q.w);
			Quaternion b = new Quaternion(Mathf.Lerp(lastQ.x, q.x, 0.5f), Mathf.Lerp(lastQ.y, q.y, 0.5f), Mathf.Lerp(lastQ.z, q.z, 0.5f), Mathf.Lerp(lastQ.w, q.w, 0.5f));
			Quaternion b2 = new Quaternion(Mathf.Lerp(lastQ.x, quaternion.x, 0.5f), Mathf.Lerp(lastQ.y, quaternion.y, 0.5f), Mathf.Lerp(lastQ.z, quaternion.z, 0.5f), Mathf.Lerp(lastQ.w, quaternion.w, 0.5f));
			float num = Quaternion.Angle(lastQ, b);
			if (Quaternion.Angle(lastQ, b2) >= num)
			{
				return q;
			}
			return quaternion;
		}
	}
}
