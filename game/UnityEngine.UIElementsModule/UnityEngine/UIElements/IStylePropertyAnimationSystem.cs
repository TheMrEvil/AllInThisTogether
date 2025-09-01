using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000097 RID: 151
	internal interface IStylePropertyAnimationSystem
	{
		// Token: 0x06000520 RID: 1312
		bool StartTransition(VisualElement owner, StylePropertyId prop, float startValue, float endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x06000521 RID: 1313
		bool StartTransition(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x06000522 RID: 1314
		bool StartTransition(VisualElement owner, StylePropertyId prop, Length startValue, Length endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x06000523 RID: 1315
		bool StartTransition(VisualElement owner, StylePropertyId prop, Color startValue, Color endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x06000524 RID: 1316
		bool StartAnimationEnum(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x06000525 RID: 1317
		bool StartTransition(VisualElement owner, StylePropertyId prop, Background startValue, Background endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x06000526 RID: 1318
		bool StartTransition(VisualElement owner, StylePropertyId prop, FontDefinition startValue, FontDefinition endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x06000527 RID: 1319
		bool StartTransition(VisualElement owner, StylePropertyId prop, Font startValue, Font endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x06000528 RID: 1320
		bool StartTransition(VisualElement owner, StylePropertyId prop, TextShadow startValue, TextShadow endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x06000529 RID: 1321
		bool StartTransition(VisualElement owner, StylePropertyId prop, Scale startValue, Scale endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x0600052A RID: 1322
		bool StartTransition(VisualElement owner, StylePropertyId prop, TransformOrigin startValue, TransformOrigin endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x0600052B RID: 1323
		bool StartTransition(VisualElement owner, StylePropertyId prop, Translate startValue, Translate endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x0600052C RID: 1324
		bool StartTransition(VisualElement owner, StylePropertyId prop, Rotate startValue, Rotate endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x0600052D RID: 1325
		void CancelAllAnimations();

		// Token: 0x0600052E RID: 1326
		void CancelAllAnimations(VisualElement owner);

		// Token: 0x0600052F RID: 1327
		void CancelAnimation(VisualElement owner, StylePropertyId id);

		// Token: 0x06000530 RID: 1328
		bool HasRunningAnimation(VisualElement owner, StylePropertyId id);

		// Token: 0x06000531 RID: 1329
		void UpdateAnimation(VisualElement owner, StylePropertyId id);

		// Token: 0x06000532 RID: 1330
		void GetAllAnimations(VisualElement owner, List<StylePropertyId> propertyIds);

		// Token: 0x06000533 RID: 1331
		void Update();
	}
}
