using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B2 RID: 178
	internal class EmptyStylePropertyAnimationSystem : IStylePropertyAnimationSystem
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x0001682C File Offset: 0x00014A2C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, float startValue, float endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00016840 File Offset: 0x00014A40
		public bool StartTransition(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00016854 File Offset: 0x00014A54
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Length startValue, Length endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00016868 File Offset: 0x00014A68
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Color startValue, Color endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001687C File Offset: 0x00014A7C
		public bool StartAnimationEnum(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00016890 File Offset: 0x00014A90
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Background startValue, Background endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000168A4 File Offset: 0x00014AA4
		public bool StartTransition(VisualElement owner, StylePropertyId prop, FontDefinition startValue, FontDefinition endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000168B8 File Offset: 0x00014AB8
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Font startValue, Font endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000168CC File Offset: 0x00014ACC
		public bool StartTransition(VisualElement owner, StylePropertyId prop, TextShadow startValue, TextShadow endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x000168E0 File Offset: 0x00014AE0
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Scale startValue, Scale endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000168F4 File Offset: 0x00014AF4
		public bool StartTransition(VisualElement owner, StylePropertyId prop, TransformOrigin startValue, TransformOrigin endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00016908 File Offset: 0x00014B08
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Translate startValue, Translate endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001691C File Offset: 0x00014B1C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Rotate startValue, Rotate endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00002166 File Offset: 0x00000366
		public void CancelAllAnimations()
		{
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00002166 File Offset: 0x00000366
		public void CancelAllAnimations(VisualElement owner)
		{
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00002166 File Offset: 0x00000366
		public void CancelAnimation(VisualElement owner, StylePropertyId id)
		{
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00016930 File Offset: 0x00014B30
		public bool HasRunningAnimation(VisualElement owner, StylePropertyId id)
		{
			return false;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00002166 File Offset: 0x00000366
		public void UpdateAnimation(VisualElement owner, StylePropertyId id)
		{
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00002166 File Offset: 0x00000366
		public void GetAllAnimations(VisualElement owner, List<StylePropertyId> propertyIds)
		{
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00002166 File Offset: 0x00000366
		public void Update()
		{
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x000020C2 File Offset: 0x000002C2
		public EmptyStylePropertyAnimationSystem()
		{
		}
	}
}
