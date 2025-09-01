using System;

namespace System.Drawing
{
	// Token: 0x0200009C RID: 156
	internal struct GdiplusStartupOutput
	{
		// Token: 0x06000A2C RID: 2604 RVA: 0x00017300 File Offset: 0x00015500
		internal static GdiplusStartupOutput MakeGdiplusStartupOutput()
		{
			GdiplusStartupOutput result = default(GdiplusStartupOutput);
			result.NotificationHook = (result.NotificationUnhook = IntPtr.Zero);
			return result;
		}

		// Token: 0x040005F4 RID: 1524
		internal IntPtr NotificationHook;

		// Token: 0x040005F5 RID: 1525
		internal IntPtr NotificationUnhook;
	}
}
