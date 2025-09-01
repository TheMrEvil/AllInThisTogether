using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000080 RID: 128
	internal interface IStylePropertyAnimations
	{
		// Token: 0x0600032A RID: 810
		bool Start(StylePropertyId id, float from, float to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x0600032B RID: 811
		bool Start(StylePropertyId id, int from, int to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x0600032C RID: 812
		bool Start(StylePropertyId id, Length from, Length to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x0600032D RID: 813
		bool Start(StylePropertyId id, Color from, Color to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x0600032E RID: 814
		bool StartEnum(StylePropertyId id, int from, int to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x0600032F RID: 815
		bool Start(StylePropertyId id, Background from, Background to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000330 RID: 816
		bool Start(StylePropertyId id, FontDefinition from, FontDefinition to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000331 RID: 817
		bool Start(StylePropertyId id, Font from, Font to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000332 RID: 818
		bool Start(StylePropertyId id, TextShadow from, TextShadow to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000333 RID: 819
		bool Start(StylePropertyId id, Scale from, Scale to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000334 RID: 820
		bool Start(StylePropertyId id, Translate from, Translate to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000335 RID: 821
		bool Start(StylePropertyId id, Rotate from, Rotate to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000336 RID: 822
		bool Start(StylePropertyId id, TransformOrigin from, TransformOrigin to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000337 RID: 823
		bool HasRunningAnimation(StylePropertyId id);

		// Token: 0x06000338 RID: 824
		void UpdateAnimation(StylePropertyId id);

		// Token: 0x06000339 RID: 825
		void GetAllAnimations(List<StylePropertyId> outPropertyIds);

		// Token: 0x0600033A RID: 826
		void CancelAnimation(StylePropertyId id);

		// Token: 0x0600033B RID: 827
		void CancelAllAnimations();

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600033C RID: 828
		// (set) Token: 0x0600033D RID: 829
		int runningAnimationCount { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600033E RID: 830
		// (set) Token: 0x0600033F RID: 831
		int completedAnimationCount { get; set; }
	}
}
