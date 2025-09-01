using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000218 RID: 536
	public static class PointerType
	{
		// Token: 0x0600105F RID: 4191 RVA: 0x00041FD4 File Offset: 0x000401D4
		internal static string GetPointerType(int pointerId)
		{
			bool flag = pointerId == PointerId.mousePointerId;
			string result;
			if (flag)
			{
				result = PointerType.mouse;
			}
			else
			{
				result = PointerType.touch;
			}
			return result;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00042000 File Offset: 0x00040200
		internal static bool IsDirectManipulationDevice(string pointerType)
		{
			return pointerType == PointerType.touch || pointerType == PointerType.pen;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00042025 File Offset: 0x00040225
		// Note: this type is marked as 'beforefieldinit'.
		static PointerType()
		{
		}

		// Token: 0x04000746 RID: 1862
		public static readonly string mouse = "mouse";

		// Token: 0x04000747 RID: 1863
		public static readonly string touch = "touch";

		// Token: 0x04000748 RID: 1864
		public static readonly string pen = "pen";

		// Token: 0x04000749 RID: 1865
		public static readonly string unknown = "";
	}
}
