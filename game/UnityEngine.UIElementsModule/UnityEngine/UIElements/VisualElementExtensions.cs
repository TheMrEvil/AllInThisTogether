using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E6 RID: 230
	public static class VisualElementExtensions
	{
		// Token: 0x0600074A RID: 1866 RVA: 0x0001AB94 File Offset: 0x00018D94
		public static void StretchToParentSize(this VisualElement elem)
		{
			bool flag = elem == null;
			if (flag)
			{
				throw new ArgumentNullException("elem");
			}
			IStyle style = elem.style;
			style.position = Position.Absolute;
			style.left = 0f;
			style.top = 0f;
			style.right = 0f;
			style.bottom = 0f;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001AC10 File Offset: 0x00018E10
		public static void StretchToParentWidth(this VisualElement elem)
		{
			bool flag = elem == null;
			if (flag)
			{
				throw new ArgumentNullException("elem");
			}
			IStyle style = elem.style;
			style.position = Position.Absolute;
			style.left = 0f;
			style.right = 0f;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001AC68 File Offset: 0x00018E68
		public static void AddManipulator(this VisualElement ele, IManipulator manipulator)
		{
			bool flag = manipulator != null;
			if (flag)
			{
				manipulator.target = ele;
			}
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001AC88 File Offset: 0x00018E88
		public static void RemoveManipulator(this VisualElement ele, IManipulator manipulator)
		{
			bool flag = manipulator != null;
			if (flag)
			{
				manipulator.target = null;
			}
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001ACA8 File Offset: 0x00018EA8
		public static Vector2 WorldToLocal(this VisualElement ele, Vector2 p)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			return VisualElement.MultiplyMatrix44Point2(ele.worldTransformInverse, p);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001ACDC File Offset: 0x00018EDC
		public static Vector2 LocalToWorld(this VisualElement ele, Vector2 p)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			return VisualElement.MultiplyMatrix44Point2(ele.worldTransformRef, p);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001AD10 File Offset: 0x00018F10
		public static Rect WorldToLocal(this VisualElement ele, Rect r)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			return VisualElement.CalculateConservativeRect(ele.worldTransformInverse, r);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001AD44 File Offset: 0x00018F44
		public static Rect LocalToWorld(this VisualElement ele, Rect r)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			return VisualElement.CalculateConservativeRect(ele.worldTransformRef, r);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001AD78 File Offset: 0x00018F78
		public static Vector2 ChangeCoordinatesTo(this VisualElement src, VisualElement dest, Vector2 point)
		{
			return dest.WorldToLocal(src.LocalToWorld(point));
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001AD98 File Offset: 0x00018F98
		public static Rect ChangeCoordinatesTo(this VisualElement src, VisualElement dest, Rect rect)
		{
			return dest.WorldToLocal(src.LocalToWorld(rect));
		}
	}
}
