using System;

namespace System.Drawing
{
	// Token: 0x0200009B RID: 155
	internal struct GdiplusStartupInput
	{
		// Token: 0x06000A2B RID: 2603 RVA: 0x000172C4 File Offset: 0x000154C4
		internal static GdiplusStartupInput MakeGdiplusStartupInput()
		{
			return new GdiplusStartupInput
			{
				GdiplusVersion = 1U,
				DebugEventCallback = IntPtr.Zero,
				SuppressBackgroundThread = 0,
				SuppressExternalCodecs = 0
			};
		}

		// Token: 0x040005F0 RID: 1520
		internal uint GdiplusVersion;

		// Token: 0x040005F1 RID: 1521
		internal IntPtr DebugEventCallback;

		// Token: 0x040005F2 RID: 1522
		internal int SuppressBackgroundThread;

		// Token: 0x040005F3 RID: 1523
		internal int SuppressExternalCodecs;
	}
}
