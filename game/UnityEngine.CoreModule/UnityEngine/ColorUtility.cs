using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001BF RID: 447
	[NativeHeader("Runtime/Export/Math/ColorUtility.bindings.h")]
	public class ColorUtility
	{
		// Token: 0x060013B5 RID: 5045
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool DoTryParseHtmlColor(string htmlString, out Color32 color);

		// Token: 0x060013B6 RID: 5046 RVA: 0x0001C52C File Offset: 0x0001A72C
		public static bool TryParseHtmlString(string htmlString, out Color color)
		{
			Color32 c;
			bool result = ColorUtility.DoTryParseHtmlColor(htmlString, out c);
			color = c;
			return result;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0001C554 File Offset: 0x0001A754
		public static string ToHtmlStringRGB(Color color)
		{
			Color32 color2 = new Color32((byte)Mathf.Clamp(Mathf.RoundToInt(color.r * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.g * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.b * 255f), 0, 255), 1);
			return UnityString.Format("{0:X2}{1:X2}{2:X2}", new object[]
			{
				color2.r,
				color2.g,
				color2.b
			});
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0001C600 File Offset: 0x0001A800
		public static string ToHtmlStringRGBA(Color color)
		{
			Color32 color2 = new Color32((byte)Mathf.Clamp(Mathf.RoundToInt(color.r * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.g * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.b * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.a * 255f), 0, 255));
			return UnityString.Format("{0:X2}{1:X2}{2:X2}{3:X2}", new object[]
			{
				color2.r,
				color2.g,
				color2.b,
				color2.a
			});
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00002072 File Offset: 0x00000272
		public ColorUtility()
		{
		}
	}
}
