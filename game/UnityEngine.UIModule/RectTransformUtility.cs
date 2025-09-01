using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	[NativeHeader("Runtime/Camera/Camera.h")]
	[StaticAccessor("UI", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Transform/RectTransform.h")]
	[NativeHeader("Modules/UI/RectTransformUtil.h")]
	[NativeHeader("Modules/UI/Canvas.h")]
	public sealed class RectTransformUtility
	{
		// Token: 0x06000040 RID: 64 RVA: 0x0000245C File Offset: 0x0000065C
		public static Vector2 PixelAdjustPoint(Vector2 point, Transform elementTransform, Canvas canvas)
		{
			Vector2 result;
			RectTransformUtility.PixelAdjustPoint_Injected(ref point, elementTransform, canvas, out result);
			return result;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002478 File Offset: 0x00000678
		public static Rect PixelAdjustRect(RectTransform rectTransform, Canvas canvas)
		{
			Rect result;
			RectTransformUtility.PixelAdjustRect_Injected(rectTransform, canvas, out result);
			return result;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000248F File Offset: 0x0000068F
		private static bool PointInRectangle(Vector2 screenPoint, RectTransform rect, Camera cam, Vector4 offset)
		{
			return RectTransformUtility.PointInRectangle_Injected(ref screenPoint, rect, cam, ref offset);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000249C File Offset: 0x0000069C
		private RectTransformUtility()
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000024A8 File Offset: 0x000006A8
		public static bool RectangleContainsScreenPoint(RectTransform rect, Vector2 screenPoint)
		{
			return RectTransformUtility.RectangleContainsScreenPoint(rect, screenPoint, null);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000024C4 File Offset: 0x000006C4
		public static bool RectangleContainsScreenPoint(RectTransform rect, Vector2 screenPoint, Camera cam)
		{
			return RectTransformUtility.RectangleContainsScreenPoint(rect, screenPoint, cam, Vector4.zero);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000024E4 File Offset: 0x000006E4
		public static bool RectangleContainsScreenPoint(RectTransform rect, Vector2 screenPoint, Camera cam, Vector4 offset)
		{
			return RectTransformUtility.PointInRectangle(screenPoint, rect, cam, offset);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002500 File Offset: 0x00000700
		public static bool ScreenPointToWorldPointInRectangle(RectTransform rect, Vector2 screenPoint, Camera cam, out Vector3 worldPoint)
		{
			worldPoint = Vector2.zero;
			Ray ray = RectTransformUtility.ScreenPointToRay(cam, screenPoint);
			Plane plane = new Plane(rect.rotation * Vector3.back, rect.position);
			float distance = 0f;
			float num = Vector3.Dot(Vector3.Normalize(rect.position - ray.origin), plane.normal);
			bool flag = num != 0f && !plane.Raycast(ray, out distance);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				worldPoint = ray.GetPoint(distance);
				result = true;
			}
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000025A8 File Offset: 0x000007A8
		public static bool ScreenPointToLocalPointInRectangle(RectTransform rect, Vector2 screenPoint, Camera cam, out Vector2 localPoint)
		{
			localPoint = Vector2.zero;
			Vector3 position;
			bool flag = RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPoint, cam, out position);
			bool result;
			if (flag)
			{
				localPoint = rect.InverseTransformPoint(position);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000025EC File Offset: 0x000007EC
		public static Ray ScreenPointToRay(Camera cam, Vector2 screenPos)
		{
			bool flag = cam != null;
			Ray result;
			if (flag)
			{
				result = cam.ScreenPointToRay(screenPos);
			}
			else
			{
				Vector3 origin = screenPos;
				origin.z -= 100f;
				result = new Ray(origin, Vector3.forward);
			}
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000263C File Offset: 0x0000083C
		public static Vector2 WorldToScreenPoint(Camera cam, Vector3 worldPoint)
		{
			bool flag = cam == null;
			Vector2 result;
			if (flag)
			{
				result = new Vector2(worldPoint.x, worldPoint.y);
			}
			else
			{
				result = cam.WorldToScreenPoint(worldPoint);
			}
			return result;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000267C File Offset: 0x0000087C
		public static Bounds CalculateRelativeRectTransformBounds(Transform root, Transform child)
		{
			RectTransform[] componentsInChildren = child.GetComponentsInChildren<RectTransform>(false);
			bool flag = componentsInChildren.Length != 0;
			Bounds result;
			if (flag)
			{
				Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
				Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
				Matrix4x4 worldToLocalMatrix = root.worldToLocalMatrix;
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					componentsInChildren[i].GetWorldCorners(RectTransformUtility.s_Corners);
					for (int j = 0; j < 4; j++)
					{
						Vector3 lhs = worldToLocalMatrix.MultiplyPoint3x4(RectTransformUtility.s_Corners[j]);
						vector = Vector3.Min(lhs, vector);
						vector2 = Vector3.Max(lhs, vector2);
					}
					i++;
				}
				Bounds bounds = new Bounds(vector, Vector3.zero);
				bounds.Encapsulate(vector2);
				result = bounds;
			}
			else
			{
				result = new Bounds(Vector3.zero, Vector3.zero);
			}
			return result;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002770 File Offset: 0x00000970
		public static Bounds CalculateRelativeRectTransformBounds(Transform trans)
		{
			return RectTransformUtility.CalculateRelativeRectTransformBounds(trans, trans);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000278C File Offset: 0x0000098C
		public static void FlipLayoutOnAxis(RectTransform rect, int axis, bool keepPositioning, bool recursive)
		{
			bool flag = rect == null;
			if (!flag)
			{
				if (recursive)
				{
					for (int i = 0; i < rect.childCount; i++)
					{
						RectTransform rectTransform = rect.GetChild(i) as RectTransform;
						bool flag2 = rectTransform != null;
						if (flag2)
						{
							RectTransformUtility.FlipLayoutOnAxis(rectTransform, axis, false, true);
						}
					}
				}
				Vector2 pivot = rect.pivot;
				pivot[axis] = 1f - pivot[axis];
				rect.pivot = pivot;
				if (!keepPositioning)
				{
					Vector2 anchoredPosition = rect.anchoredPosition;
					anchoredPosition[axis] = -anchoredPosition[axis];
					rect.anchoredPosition = anchoredPosition;
					Vector2 anchorMin = rect.anchorMin;
					Vector2 anchorMax = rect.anchorMax;
					float num = anchorMin[axis];
					anchorMin[axis] = 1f - anchorMax[axis];
					anchorMax[axis] = 1f - num;
					rect.anchorMin = anchorMin;
					rect.anchorMax = anchorMax;
				}
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000289C File Offset: 0x00000A9C
		public static void FlipLayoutAxes(RectTransform rect, bool keepPositioning, bool recursive)
		{
			bool flag = rect == null;
			if (!flag)
			{
				if (recursive)
				{
					for (int i = 0; i < rect.childCount; i++)
					{
						RectTransform rectTransform = rect.GetChild(i) as RectTransform;
						bool flag2 = rectTransform != null;
						if (flag2)
						{
							RectTransformUtility.FlipLayoutAxes(rectTransform, false, true);
						}
					}
				}
				rect.pivot = RectTransformUtility.GetTransposed(rect.pivot);
				rect.sizeDelta = RectTransformUtility.GetTransposed(rect.sizeDelta);
				if (!keepPositioning)
				{
					rect.anchoredPosition = RectTransformUtility.GetTransposed(rect.anchoredPosition);
					rect.anchorMin = RectTransformUtility.GetTransposed(rect.anchorMin);
					rect.anchorMax = RectTransformUtility.GetTransposed(rect.anchorMax);
				}
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002960 File Offset: 0x00000B60
		private static Vector2 GetTransposed(Vector2 input)
		{
			return new Vector2(input.y, input.x);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002983 File Offset: 0x00000B83
		// Note: this type is marked as 'beforefieldinit'.
		static RectTransformUtility()
		{
		}

		// Token: 0x06000051 RID: 81
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PixelAdjustPoint_Injected(ref Vector2 point, Transform elementTransform, Canvas canvas, out Vector2 ret);

		// Token: 0x06000052 RID: 82
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PixelAdjustRect_Injected(RectTransform rectTransform, Canvas canvas, out Rect ret);

		// Token: 0x06000053 RID: 83
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool PointInRectangle_Injected(ref Vector2 screenPoint, RectTransform rect, Camera cam, ref Vector4 offset);

		// Token: 0x04000002 RID: 2
		private static readonly Vector3[] s_Corners = new Vector3[4];
	}
}
