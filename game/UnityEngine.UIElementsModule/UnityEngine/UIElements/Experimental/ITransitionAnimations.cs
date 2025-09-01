using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000388 RID: 904
	public interface ITransitionAnimations
	{
		// Token: 0x06001D29 RID: 7465
		ValueAnimation<float> Start(float from, float to, int durationMs, Action<VisualElement, float> onValueChanged);

		// Token: 0x06001D2A RID: 7466
		ValueAnimation<Rect> Start(Rect from, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged);

		// Token: 0x06001D2B RID: 7467
		ValueAnimation<Color> Start(Color from, Color to, int durationMs, Action<VisualElement, Color> onValueChanged);

		// Token: 0x06001D2C RID: 7468
		ValueAnimation<Vector3> Start(Vector3 from, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged);

		// Token: 0x06001D2D RID: 7469
		ValueAnimation<Vector2> Start(Vector2 from, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged);

		// Token: 0x06001D2E RID: 7470
		ValueAnimation<Quaternion> Start(Quaternion from, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged);

		// Token: 0x06001D2F RID: 7471
		ValueAnimation<StyleValues> Start(StyleValues from, StyleValues to, int durationMs);

		// Token: 0x06001D30 RID: 7472
		ValueAnimation<StyleValues> Start(StyleValues to, int durationMs);

		// Token: 0x06001D31 RID: 7473
		ValueAnimation<float> Start(Func<VisualElement, float> fromValueGetter, float to, int durationMs, Action<VisualElement, float> onValueChanged);

		// Token: 0x06001D32 RID: 7474
		ValueAnimation<Rect> Start(Func<VisualElement, Rect> fromValueGetter, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged);

		// Token: 0x06001D33 RID: 7475
		ValueAnimation<Color> Start(Func<VisualElement, Color> fromValueGetter, Color to, int durationMs, Action<VisualElement, Color> onValueChanged);

		// Token: 0x06001D34 RID: 7476
		ValueAnimation<Vector3> Start(Func<VisualElement, Vector3> fromValueGetter, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged);

		// Token: 0x06001D35 RID: 7477
		ValueAnimation<Vector2> Start(Func<VisualElement, Vector2> fromValueGetter, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged);

		// Token: 0x06001D36 RID: 7478
		ValueAnimation<Quaternion> Start(Func<VisualElement, Quaternion> fromValueGetter, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged);

		// Token: 0x06001D37 RID: 7479
		ValueAnimation<Rect> Layout(Rect to, int durationMs);

		// Token: 0x06001D38 RID: 7480
		ValueAnimation<Vector2> TopLeft(Vector2 to, int durationMs);

		// Token: 0x06001D39 RID: 7481
		ValueAnimation<Vector2> Size(Vector2 to, int durationMs);

		// Token: 0x06001D3A RID: 7482
		ValueAnimation<float> Scale(float to, int duration);

		// Token: 0x06001D3B RID: 7483
		ValueAnimation<Vector3> Position(Vector3 to, int duration);

		// Token: 0x06001D3C RID: 7484
		ValueAnimation<Quaternion> Rotation(Quaternion to, int duration);
	}
}
