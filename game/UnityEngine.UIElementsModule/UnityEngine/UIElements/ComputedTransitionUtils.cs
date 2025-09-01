using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements.Experimental;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000271 RID: 625
	internal static class ComputedTransitionUtils
	{
		// Token: 0x06001373 RID: 4979 RVA: 0x00055150 File Offset: 0x00053350
		internal static void UpdateComputedTransitions(ref ComputedStyle computedStyle)
		{
			bool flag = computedStyle.computedTransitions == null;
			if (flag)
			{
				computedStyle.computedTransitions = ComputedTransitionUtils.GetOrComputeTransitionPropertyData(ref computedStyle);
			}
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0005517C File Offset: 0x0005337C
		internal static bool HasTransitionProperty(this ComputedStyle computedStyle, StylePropertyId id)
		{
			for (int i = computedStyle.computedTransitions.Length - 1; i >= 0; i--)
			{
				ComputedTransitionProperty computedTransitionProperty = computedStyle.computedTransitions[i];
				bool flag = computedTransitionProperty.id == id || StylePropertyUtil.IsMatchingShorthand(computedTransitionProperty.id, id);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x000551DC File Offset: 0x000533DC
		internal static bool GetTransitionProperty(this ComputedStyle computedStyle, StylePropertyId id, out ComputedTransitionProperty result)
		{
			for (int i = computedStyle.computedTransitions.Length - 1; i >= 0; i--)
			{
				ComputedTransitionProperty computedTransitionProperty = computedStyle.computedTransitions[i];
				bool flag = computedTransitionProperty.id == id || StylePropertyUtil.IsMatchingShorthand(computedTransitionProperty.id, id);
				if (flag)
				{
					result = computedTransitionProperty;
					return true;
				}
			}
			result = default(ComputedTransitionProperty);
			return false;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0005524C File Offset: 0x0005344C
		private static ComputedTransitionProperty[] GetOrComputeTransitionPropertyData(ref ComputedStyle computedStyle)
		{
			int transitionHashCode = ComputedTransitionUtils.GetTransitionHashCode(ref computedStyle);
			ComputedTransitionProperty[] array;
			bool flag = !StyleCache.TryGetValue(transitionHashCode, out array);
			if (flag)
			{
				ComputedTransitionUtils.ComputeTransitionPropertyData(ref computedStyle, ComputedTransitionUtils.s_ComputedTransitionsBuffer);
				array = new ComputedTransitionProperty[ComputedTransitionUtils.s_ComputedTransitionsBuffer.Count];
				ComputedTransitionUtils.s_ComputedTransitionsBuffer.CopyTo(array);
				ComputedTransitionUtils.s_ComputedTransitionsBuffer.Clear();
				StyleCache.SetValue(transitionHashCode, array);
			}
			return array;
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x000552B4 File Offset: 0x000534B4
		private static int GetTransitionHashCode(ref ComputedStyle cs)
		{
			int num = 0;
			foreach (TimeValue timeValue in cs.transitionDelay)
			{
				num = (num * 397 ^ timeValue.GetHashCode());
			}
			foreach (TimeValue timeValue2 in cs.transitionDuration)
			{
				num = (num * 397 ^ timeValue2.GetHashCode());
			}
			foreach (StylePropertyName stylePropertyName in cs.transitionProperty)
			{
				num = (num * 397 ^ stylePropertyName.GetHashCode());
			}
			foreach (EasingFunction easingFunction in cs.transitionTimingFunction)
			{
				num = (num * 397 ^ easingFunction.GetHashCode());
			}
			return num;
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00055424 File Offset: 0x00053624
		internal static bool SameTransitionProperty(ref ComputedStyle x, ref ComputedStyle y)
		{
			bool flag = x.computedTransitions == y.computedTransitions && x.computedTransitions != null;
			return flag || (ComputedTransitionUtils.SameTransitionProperty(x.transitionProperty, y.transitionProperty) && ComputedTransitionUtils.SameTransitionProperty(x.transitionDuration, y.transitionDuration) && ComputedTransitionUtils.SameTransitionProperty(x.transitionDelay, y.transitionDelay));
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00055494 File Offset: 0x00053694
		private static bool SameTransitionProperty(List<StylePropertyName> a, List<StylePropertyName> b)
		{
			bool flag = a == b;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = a == null || b == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = a.Count != b.Count;
					if (flag3)
					{
						result = false;
					}
					else
					{
						int count = a.Count;
						for (int i = 0; i < count; i++)
						{
							bool flag4 = a[i] != b[i];
							if (flag4)
							{
								return false;
							}
						}
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0005551C File Offset: 0x0005371C
		private static bool SameTransitionProperty(List<TimeValue> a, List<TimeValue> b)
		{
			bool flag = a == b;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = a == null || b == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = a.Count != b.Count;
					if (flag3)
					{
						result = false;
					}
					else
					{
						int count = a.Count;
						for (int i = 0; i < count; i++)
						{
							bool flag4 = a[i] != b[i];
							if (flag4)
							{
								return false;
							}
						}
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x000555A4 File Offset: 0x000537A4
		private static void ComputeTransitionPropertyData(ref ComputedStyle computedStyle, List<ComputedTransitionProperty> outData)
		{
			List<StylePropertyName> transitionProperty = computedStyle.transitionProperty;
			bool flag = transitionProperty == null || transitionProperty.Count == 0;
			if (!flag)
			{
				List<TimeValue> transitionDuration = computedStyle.transitionDuration;
				List<TimeValue> transitionDelay = computedStyle.transitionDelay;
				List<EasingFunction> transitionTimingFunction = computedStyle.transitionTimingFunction;
				int count = transitionProperty.Count;
				for (int i = 0; i < count; i++)
				{
					StylePropertyId id = transitionProperty[i].id;
					bool flag2 = id == StylePropertyId.Unknown || !StylePropertyUtil.IsAnimatable(id);
					if (!flag2)
					{
						int num = ComputedTransitionUtils.ConvertTransitionTime(ComputedTransitionUtils.GetWrappingTransitionData<TimeValue>(transitionDuration, i, new TimeValue(0f)));
						int num2 = ComputedTransitionUtils.ConvertTransitionTime(ComputedTransitionUtils.GetWrappingTransitionData<TimeValue>(transitionDelay, i, new TimeValue(0f)));
						float num3 = (float)(Mathf.Max(0, num) + num2);
						bool flag3 = num3 <= 0f;
						if (!flag3)
						{
							EasingFunction wrappingTransitionData = ComputedTransitionUtils.GetWrappingTransitionData<EasingFunction>(transitionTimingFunction, i, EasingMode.Ease);
							outData.Add(new ComputedTransitionProperty
							{
								id = id,
								durationMs = num,
								delayMs = num2,
								easingCurve = ComputedTransitionUtils.ConvertTransitionFunction(wrappingTransitionData.mode)
							});
						}
					}
				}
			}
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x000556E4 File Offset: 0x000538E4
		private static T GetWrappingTransitionData<T>(List<T> list, int i, T defaultValue)
		{
			return (list.Count == 0) ? defaultValue : list[i % list.Count];
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00055710 File Offset: 0x00053910
		private static int ConvertTransitionTime(TimeValue time)
		{
			return Mathf.RoundToInt((time.unit == TimeUnit.Millisecond) ? time.value : (time.value * 1000f));
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00055748 File Offset: 0x00053948
		private static Func<float, float> ConvertTransitionFunction(EasingMode mode)
		{
			Func<float, float> result;
			switch (mode)
			{
			default:
				result = ((float t) => t * (1.8f + t * (-0.6f + t * -0.2f)));
				break;
			case EasingMode.EaseIn:
				result = ((float t) => Easing.InQuad(t));
				break;
			case EasingMode.EaseOut:
				result = ((float t) => Easing.OutQuad(t));
				break;
			case EasingMode.EaseInOut:
				result = ((float t) => Easing.InOutQuad(t));
				break;
			case EasingMode.Linear:
				result = ((float t) => Easing.Linear(t));
				break;
			case EasingMode.EaseInSine:
				result = ((float t) => Easing.InSine(t));
				break;
			case EasingMode.EaseOutSine:
				result = ((float t) => Easing.OutSine(t));
				break;
			case EasingMode.EaseInOutSine:
				result = ((float t) => Easing.InOutSine(t));
				break;
			case EasingMode.EaseInCubic:
				result = ((float t) => Easing.InCubic(t));
				break;
			case EasingMode.EaseOutCubic:
				result = ((float t) => Easing.OutCubic(t));
				break;
			case EasingMode.EaseInOutCubic:
				result = ((float t) => Easing.InOutCubic(t));
				break;
			case EasingMode.EaseInCirc:
				result = ((float t) => Easing.InCirc(t));
				break;
			case EasingMode.EaseOutCirc:
				result = ((float t) => Easing.OutCirc(t));
				break;
			case EasingMode.EaseInOutCirc:
				result = ((float t) => Easing.InOutCirc(t));
				break;
			case EasingMode.EaseInElastic:
				result = ((float t) => Easing.InElastic(t));
				break;
			case EasingMode.EaseOutElastic:
				result = ((float t) => Easing.OutElastic(t));
				break;
			case EasingMode.EaseInOutElastic:
				result = ((float t) => Easing.InOutElastic(t));
				break;
			case EasingMode.EaseInBack:
				result = ((float t) => Easing.InBack(t));
				break;
			case EasingMode.EaseOutBack:
				result = ((float t) => Easing.OutBack(t));
				break;
			case EasingMode.EaseInOutBack:
				result = ((float t) => Easing.InOutBack(t));
				break;
			case EasingMode.EaseInBounce:
				result = ((float t) => Easing.InBounce(t));
				break;
			case EasingMode.EaseOutBounce:
				result = ((float t) => Easing.OutBounce(t));
				break;
			case EasingMode.EaseInOutBounce:
				result = ((float t) => Easing.InOutBounce(t));
				break;
			}
			return result;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00055B06 File Offset: 0x00053D06
		// Note: this type is marked as 'beforefieldinit'.
		static ComputedTransitionUtils()
		{
		}

		// Token: 0x040008E4 RID: 2276
		private static List<ComputedTransitionProperty> s_ComputedTransitionsBuffer = new List<ComputedTransitionProperty>();

		// Token: 0x02000272 RID: 626
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001380 RID: 4992 RVA: 0x00055B12 File Offset: 0x00053D12
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001381 RID: 4993 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x06001382 RID: 4994 RVA: 0x00055B1E File Offset: 0x00053D1E
			internal float <ConvertTransitionFunction>b__12_0(float t)
			{
				return t * (1.8f + t * (-0.6f + t * -0.2f));
			}

			// Token: 0x06001383 RID: 4995 RVA: 0x00055B37 File Offset: 0x00053D37
			internal float <ConvertTransitionFunction>b__12_1(float t)
			{
				return Easing.InQuad(t);
			}

			// Token: 0x06001384 RID: 4996 RVA: 0x00055B3F File Offset: 0x00053D3F
			internal float <ConvertTransitionFunction>b__12_2(float t)
			{
				return Easing.OutQuad(t);
			}

			// Token: 0x06001385 RID: 4997 RVA: 0x00055B47 File Offset: 0x00053D47
			internal float <ConvertTransitionFunction>b__12_3(float t)
			{
				return Easing.InOutQuad(t);
			}

			// Token: 0x06001386 RID: 4998 RVA: 0x00055B4F File Offset: 0x00053D4F
			internal float <ConvertTransitionFunction>b__12_4(float t)
			{
				return Easing.Linear(t);
			}

			// Token: 0x06001387 RID: 4999 RVA: 0x00055B57 File Offset: 0x00053D57
			internal float <ConvertTransitionFunction>b__12_5(float t)
			{
				return Easing.InSine(t);
			}

			// Token: 0x06001388 RID: 5000 RVA: 0x00055B5F File Offset: 0x00053D5F
			internal float <ConvertTransitionFunction>b__12_6(float t)
			{
				return Easing.OutSine(t);
			}

			// Token: 0x06001389 RID: 5001 RVA: 0x00055B67 File Offset: 0x00053D67
			internal float <ConvertTransitionFunction>b__12_7(float t)
			{
				return Easing.InOutSine(t);
			}

			// Token: 0x0600138A RID: 5002 RVA: 0x00055B6F File Offset: 0x00053D6F
			internal float <ConvertTransitionFunction>b__12_8(float t)
			{
				return Easing.InCubic(t);
			}

			// Token: 0x0600138B RID: 5003 RVA: 0x00055B77 File Offset: 0x00053D77
			internal float <ConvertTransitionFunction>b__12_9(float t)
			{
				return Easing.OutCubic(t);
			}

			// Token: 0x0600138C RID: 5004 RVA: 0x00055B7F File Offset: 0x00053D7F
			internal float <ConvertTransitionFunction>b__12_10(float t)
			{
				return Easing.InOutCubic(t);
			}

			// Token: 0x0600138D RID: 5005 RVA: 0x00055B87 File Offset: 0x00053D87
			internal float <ConvertTransitionFunction>b__12_11(float t)
			{
				return Easing.InCirc(t);
			}

			// Token: 0x0600138E RID: 5006 RVA: 0x00055B8F File Offset: 0x00053D8F
			internal float <ConvertTransitionFunction>b__12_12(float t)
			{
				return Easing.OutCirc(t);
			}

			// Token: 0x0600138F RID: 5007 RVA: 0x00055B97 File Offset: 0x00053D97
			internal float <ConvertTransitionFunction>b__12_13(float t)
			{
				return Easing.InOutCirc(t);
			}

			// Token: 0x06001390 RID: 5008 RVA: 0x00055B9F File Offset: 0x00053D9F
			internal float <ConvertTransitionFunction>b__12_14(float t)
			{
				return Easing.InElastic(t);
			}

			// Token: 0x06001391 RID: 5009 RVA: 0x00055BA7 File Offset: 0x00053DA7
			internal float <ConvertTransitionFunction>b__12_15(float t)
			{
				return Easing.OutElastic(t);
			}

			// Token: 0x06001392 RID: 5010 RVA: 0x00055BAF File Offset: 0x00053DAF
			internal float <ConvertTransitionFunction>b__12_16(float t)
			{
				return Easing.InOutElastic(t);
			}

			// Token: 0x06001393 RID: 5011 RVA: 0x00055BB7 File Offset: 0x00053DB7
			internal float <ConvertTransitionFunction>b__12_17(float t)
			{
				return Easing.InBack(t);
			}

			// Token: 0x06001394 RID: 5012 RVA: 0x00055BBF File Offset: 0x00053DBF
			internal float <ConvertTransitionFunction>b__12_18(float t)
			{
				return Easing.OutBack(t);
			}

			// Token: 0x06001395 RID: 5013 RVA: 0x00055BC7 File Offset: 0x00053DC7
			internal float <ConvertTransitionFunction>b__12_19(float t)
			{
				return Easing.InOutBack(t);
			}

			// Token: 0x06001396 RID: 5014 RVA: 0x00055BCF File Offset: 0x00053DCF
			internal float <ConvertTransitionFunction>b__12_20(float t)
			{
				return Easing.InBounce(t);
			}

			// Token: 0x06001397 RID: 5015 RVA: 0x00055BD7 File Offset: 0x00053DD7
			internal float <ConvertTransitionFunction>b__12_21(float t)
			{
				return Easing.OutBounce(t);
			}

			// Token: 0x06001398 RID: 5016 RVA: 0x00055BDF File Offset: 0x00053DDF
			internal float <ConvertTransitionFunction>b__12_22(float t)
			{
				return Easing.InOutBounce(t);
			}

			// Token: 0x040008E5 RID: 2277
			public static readonly ComputedTransitionUtils.<>c <>9 = new ComputedTransitionUtils.<>c();

			// Token: 0x040008E6 RID: 2278
			public static Func<float, float> <>9__12_0;

			// Token: 0x040008E7 RID: 2279
			public static Func<float, float> <>9__12_1;

			// Token: 0x040008E8 RID: 2280
			public static Func<float, float> <>9__12_2;

			// Token: 0x040008E9 RID: 2281
			public static Func<float, float> <>9__12_3;

			// Token: 0x040008EA RID: 2282
			public static Func<float, float> <>9__12_4;

			// Token: 0x040008EB RID: 2283
			public static Func<float, float> <>9__12_5;

			// Token: 0x040008EC RID: 2284
			public static Func<float, float> <>9__12_6;

			// Token: 0x040008ED RID: 2285
			public static Func<float, float> <>9__12_7;

			// Token: 0x040008EE RID: 2286
			public static Func<float, float> <>9__12_8;

			// Token: 0x040008EF RID: 2287
			public static Func<float, float> <>9__12_9;

			// Token: 0x040008F0 RID: 2288
			public static Func<float, float> <>9__12_10;

			// Token: 0x040008F1 RID: 2289
			public static Func<float, float> <>9__12_11;

			// Token: 0x040008F2 RID: 2290
			public static Func<float, float> <>9__12_12;

			// Token: 0x040008F3 RID: 2291
			public static Func<float, float> <>9__12_13;

			// Token: 0x040008F4 RID: 2292
			public static Func<float, float> <>9__12_14;

			// Token: 0x040008F5 RID: 2293
			public static Func<float, float> <>9__12_15;

			// Token: 0x040008F6 RID: 2294
			public static Func<float, float> <>9__12_16;

			// Token: 0x040008F7 RID: 2295
			public static Func<float, float> <>9__12_17;

			// Token: 0x040008F8 RID: 2296
			public static Func<float, float> <>9__12_18;

			// Token: 0x040008F9 RID: 2297
			public static Func<float, float> <>9__12_19;

			// Token: 0x040008FA RID: 2298
			public static Func<float, float> <>9__12_20;

			// Token: 0x040008FB RID: 2299
			public static Func<float, float> <>9__12_21;

			// Token: 0x040008FC RID: 2300
			public static Func<float, float> <>9__12_22;
		}
	}
}
