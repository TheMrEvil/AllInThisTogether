using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Apple
{
	// Token: 0x0200048C RID: 1164
	[NativeConditional("PLATFORM_IOS || PLATFORM_TVOS || PLATFORM_OSX")]
	[NativeHeader("Runtime/Export/Apple/FrameCaptureMetalScriptBindings.h")]
	public class FrameCapture
	{
		// Token: 0x0600293B RID: 10555 RVA: 0x00008CBB File Offset: 0x00006EBB
		private FrameCapture()
		{
		}

		// Token: 0x0600293C RID: 10556
		[FreeFunction("FrameCaptureMetalScripting::IsDestinationSupported")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsDestinationSupportedImpl(FrameCaptureDestination dest);

		// Token: 0x0600293D RID: 10557
		[FreeFunction("FrameCaptureMetalScripting::BeginCapture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginCaptureImpl(FrameCaptureDestination dest, string path);

		// Token: 0x0600293E RID: 10558
		[FreeFunction("FrameCaptureMetalScripting::EndCapture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndCaptureImpl();

		// Token: 0x0600293F RID: 10559
		[FreeFunction("FrameCaptureMetalScripting::CaptureNextFrame")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CaptureNextFrameImpl(FrameCaptureDestination dest, string path);

		// Token: 0x06002940 RID: 10560 RVA: 0x000442BC File Offset: 0x000424BC
		public static bool IsDestinationSupported(FrameCaptureDestination dest)
		{
			bool flag = dest != FrameCaptureDestination.DevTools && dest != FrameCaptureDestination.GPUTraceDocument;
			if (flag)
			{
				throw new ArgumentException("dest", "Argument dest has bad value (not one of FrameCaptureDestination enum values)");
			}
			return FrameCapture.IsDestinationSupportedImpl(dest);
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x000442F8 File Offset: 0x000424F8
		public static void BeginCaptureToXcode()
		{
			bool flag = !FrameCapture.IsDestinationSupported(FrameCaptureDestination.DevTools);
			if (flag)
			{
				throw new InvalidOperationException("Frame Capture with DevTools is not supported.");
			}
			FrameCapture.BeginCaptureImpl(FrameCaptureDestination.DevTools, null);
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x00044328 File Offset: 0x00042528
		public static void BeginCaptureToFile(string path)
		{
			bool flag = !FrameCapture.IsDestinationSupported(FrameCaptureDestination.GPUTraceDocument);
			if (flag)
			{
				throw new InvalidOperationException("Frame Capture to file is not supported.");
			}
			bool flag2 = string.IsNullOrEmpty(path);
			if (flag2)
			{
				throw new ArgumentException("path", "Path must be supplied when capture destination is GPUTraceDocument.");
			}
			bool flag3 = Path.GetExtension(path) != ".gputrace";
			if (flag3)
			{
				throw new ArgumentException("path", "Destination file should have .gputrace extension.");
			}
			FrameCapture.BeginCaptureImpl(FrameCaptureDestination.GPUTraceDocument, new Uri(path).AbsoluteUri);
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x0004439E File Offset: 0x0004259E
		public static void EndCapture()
		{
			FrameCapture.EndCaptureImpl();
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x000443A8 File Offset: 0x000425A8
		public static void CaptureNextFrameToXcode()
		{
			bool flag = !FrameCapture.IsDestinationSupported(FrameCaptureDestination.DevTools);
			if (flag)
			{
				throw new InvalidOperationException("Frame Capture with DevTools is not supported.");
			}
			FrameCapture.CaptureNextFrameImpl(FrameCaptureDestination.DevTools, null);
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x000443D8 File Offset: 0x000425D8
		public static void CaptureNextFrameToFile(string path)
		{
			bool flag = !FrameCapture.IsDestinationSupported(FrameCaptureDestination.GPUTraceDocument);
			if (flag)
			{
				throw new InvalidOperationException("Frame Capture to file is not supported.");
			}
			bool flag2 = string.IsNullOrEmpty(path);
			if (flag2)
			{
				throw new ArgumentException("path", "Path must be supplied when capture destination is GPUTraceDocument.");
			}
			bool flag3 = Path.GetExtension(path) != ".gputrace";
			if (flag3)
			{
				throw new ArgumentException("path", "Destination file should have .gputrace extension.");
			}
			FrameCapture.CaptureNextFrameImpl(FrameCaptureDestination.GPUTraceDocument, new Uri(path).AbsoluteUri);
		}
	}
}
