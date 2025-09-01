using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000065 RID: 101
	[Serializable]
	public class XRGraphics
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000F0E5 File Offset: 0x0000D2E5
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000F0EC File Offset: 0x0000D2EC
		public static float eyeTextureResolutionScale
		{
			get
			{
				return 1f;
			}
			set
			{
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000F0EE File Offset: 0x0000D2EE
		public static float renderViewportScale
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000F0F5 File Offset: 0x0000D2F5
		public static bool enabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000F0F8 File Offset: 0x0000D2F8
		public static bool isDeviceActive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000F0FB File Offset: 0x0000D2FB
		public static string loadedDeviceName
		{
			get
			{
				return "No XR device loaded";
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000F102 File Offset: 0x0000D302
		public static string[] supportedDevices
		{
			get
			{
				return new string[1];
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000F10A File Offset: 0x0000D30A
		public static XRGraphics.StereoRenderingMode stereoRenderingMode
		{
			get
			{
				return XRGraphics.StereoRenderingMode.SinglePass;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000F10D File Offset: 0x0000D30D
		public static RenderTextureDescriptor eyeTextureDesc
		{
			get
			{
				return new RenderTextureDescriptor(0, 0);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000F116 File Offset: 0x0000D316
		public static int eyeTextureWidth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000F119 File Offset: 0x0000D319
		public static int eyeTextureHeight
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000F11C File Offset: 0x0000D31C
		public XRGraphics()
		{
		}

		// Token: 0x02000144 RID: 324
		public enum StereoRenderingMode
		{
			// Token: 0x0400050D RID: 1293
			MultiPass,
			// Token: 0x0400050E RID: 1294
			SinglePass,
			// Token: 0x0400050F RID: 1295
			SinglePassInstanced,
			// Token: 0x04000510 RID: 1296
			SinglePassMultiView
		}
	}
}
