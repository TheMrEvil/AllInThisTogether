using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000123 RID: 291
	[NativeHeader("Runtime/Graphics/DisplayManager.h")]
	[UsedByNativeCode]
	public class Display
	{
		// Token: 0x06000808 RID: 2056 RVA: 0x0000C123 File Offset: 0x0000A323
		internal Display()
		{
			this.nativeDisplay = new IntPtr(0);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0000C139 File Offset: 0x0000A339
		internal Display(IntPtr nativeDisplay)
		{
			this.nativeDisplay = nativeDisplay;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0000C14C File Offset: 0x0000A34C
		public int renderingWidth
		{
			get
			{
				int result = 0;
				int num = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out result, out num);
				return result;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0000C174 File Offset: 0x0000A374
		public int renderingHeight
		{
			get
			{
				int num = 0;
				int result = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out num, out result);
				return result;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0000C19C File Offset: 0x0000A39C
		public int systemWidth
		{
			get
			{
				int result = 0;
				int num = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out result, out num);
				return result;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0000C1C4 File Offset: 0x0000A3C4
		public int systemHeight
		{
			get
			{
				int num = 0;
				int result = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out num, out result);
				return result;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		public RenderBuffer colorBuffer
		{
			get
			{
				RenderBuffer result;
				RenderBuffer renderBuffer;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out result, out renderBuffer);
				return result;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0000C210 File Offset: 0x0000A410
		public RenderBuffer depthBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				RenderBuffer result;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out renderBuffer, out result);
				return result;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0000C234 File Offset: 0x0000A434
		public bool active
		{
			get
			{
				return Display.GetActiveImpl(this.nativeDisplay);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0000C254 File Offset: 0x0000A454
		public bool requiresBlitToBackbuffer
		{
			get
			{
				int num = this.nativeDisplay.ToInt32();
				bool flag = num < HDROutputSettings.displays.Length;
				if (flag)
				{
					bool flag2 = HDROutputSettings.displays[num].available && HDROutputSettings.displays[num].active;
					bool flag3 = flag2;
					if (flag3)
					{
						return true;
					}
				}
				return Display.RequiresBlitToBackbufferImpl(this.nativeDisplay);
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
		public bool requiresSrgbBlitToBackbuffer
		{
			get
			{
				return Display.RequiresSrgbBlitToBackbufferImpl(this.nativeDisplay);
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0000C2D5 File Offset: 0x0000A4D5
		public void Activate()
		{
			Display.ActivateDisplayImpl(this.nativeDisplay, 0, 0, 60);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		public void Activate(int width, int height, int refreshRate)
		{
			Display.ActivateDisplayImpl(this.nativeDisplay, width, height, refreshRate);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0000C2FA File Offset: 0x0000A4FA
		public void SetParams(int width, int height, int x, int y)
		{
			Display.SetParamsImpl(this.nativeDisplay, width, height, x, y);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0000C30E File Offset: 0x0000A50E
		public void SetRenderingResolution(int w, int h)
		{
			Display.SetRenderingResolutionImpl(this.nativeDisplay, w, h);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0000C320 File Offset: 0x0000A520
		[Obsolete("MultiDisplayLicense has been deprecated.", false)]
		public static bool MultiDisplayLicense()
		{
			return true;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0000C334 File Offset: 0x0000A534
		public static Vector3 RelativeMouseAt(Vector3 inputMouseCoordinates)
		{
			int num = 0;
			int num2 = 0;
			int x = (int)inputMouseCoordinates.x;
			int y = (int)inputMouseCoordinates.y;
			Vector3 result;
			result.z = (float)Display.RelativeMouseAtImpl(x, y, out num, out num2);
			result.x = (float)num;
			result.y = (float)num2;
			return result;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x0000C384 File Offset: 0x0000A584
		public static Display main
		{
			get
			{
				return Display._mainDisplay;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0000C39C File Offset: 0x0000A59C
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x0000C3B3 File Offset: 0x0000A5B3
		public static int activeEditorGameViewTarget
		{
			get
			{
				return Display.m_ActiveEditorGameViewTarget;
			}
			internal set
			{
				Display.m_ActiveEditorGameViewTarget = value;
			}
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0000C3BC File Offset: 0x0000A5BC
		[RequiredByNativeCode]
		private static void RecreateDisplayList(IntPtr[] nativeDisplay)
		{
			bool flag = nativeDisplay.Length == 0;
			if (!flag)
			{
				Display.displays = new Display[nativeDisplay.Length];
				for (int i = 0; i < nativeDisplay.Length; i++)
				{
					Display.displays[i] = new Display(nativeDisplay[i]);
				}
				Display._mainDisplay = Display.displays[0];
			}
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0000C410 File Offset: 0x0000A610
		[RequiredByNativeCode]
		private static void FireDisplaysUpdated()
		{
			bool flag = Display.onDisplaysUpdated != null;
			if (flag)
			{
				Display.onDisplaysUpdated();
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600081E RID: 2078 RVA: 0x0000C438 File Offset: 0x0000A638
		// (remove) Token: 0x0600081F RID: 2079 RVA: 0x0000C46C File Offset: 0x0000A66C
		public static event Display.DisplaysUpdatedDelegate onDisplaysUpdated
		{
			[CompilerGenerated]
			add
			{
				Display.DisplaysUpdatedDelegate displaysUpdatedDelegate = Display.onDisplaysUpdated;
				Display.DisplaysUpdatedDelegate displaysUpdatedDelegate2;
				do
				{
					displaysUpdatedDelegate2 = displaysUpdatedDelegate;
					Display.DisplaysUpdatedDelegate value2 = (Display.DisplaysUpdatedDelegate)Delegate.Combine(displaysUpdatedDelegate2, value);
					displaysUpdatedDelegate = Interlocked.CompareExchange<Display.DisplaysUpdatedDelegate>(ref Display.onDisplaysUpdated, value2, displaysUpdatedDelegate2);
				}
				while (displaysUpdatedDelegate != displaysUpdatedDelegate2);
			}
			[CompilerGenerated]
			remove
			{
				Display.DisplaysUpdatedDelegate displaysUpdatedDelegate = Display.onDisplaysUpdated;
				Display.DisplaysUpdatedDelegate displaysUpdatedDelegate2;
				do
				{
					displaysUpdatedDelegate2 = displaysUpdatedDelegate;
					Display.DisplaysUpdatedDelegate value2 = (Display.DisplaysUpdatedDelegate)Delegate.Remove(displaysUpdatedDelegate2, value);
					displaysUpdatedDelegate = Interlocked.CompareExchange<Display.DisplaysUpdatedDelegate>(ref Display.onDisplaysUpdated, value2, displaysUpdatedDelegate2);
				}
				while (displaysUpdatedDelegate != displaysUpdatedDelegate2);
			}
		}

		// Token: 0x06000820 RID: 2080
		[FreeFunction("UnityDisplayManager_DisplaySystemResolution")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSystemExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x06000821 RID: 2081
		[FreeFunction("UnityDisplayManager_DisplayRenderingResolution")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRenderingExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x06000822 RID: 2082
		[FreeFunction("UnityDisplayManager_GetRenderingBuffersWrapper")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRenderingBuffersImpl(IntPtr nativeDisplay, out RenderBuffer color, out RenderBuffer depth);

		// Token: 0x06000823 RID: 2083
		[FreeFunction("UnityDisplayManager_SetRenderingResolution")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderingResolutionImpl(IntPtr nativeDisplay, int w, int h);

		// Token: 0x06000824 RID: 2084
		[FreeFunction("UnityDisplayManager_ActivateDisplay")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ActivateDisplayImpl(IntPtr nativeDisplay, int width, int height, int refreshRate);

		// Token: 0x06000825 RID: 2085
		[FreeFunction("UnityDisplayManager_SetDisplayParam")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetParamsImpl(IntPtr nativeDisplay, int width, int height, int x, int y);

		// Token: 0x06000826 RID: 2086
		[FreeFunction("UnityDisplayManager_RelativeMouseAt")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RelativeMouseAtImpl(int x, int y, out int rx, out int ry);

		// Token: 0x06000827 RID: 2087
		[FreeFunction("UnityDisplayManager_DisplayActive")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetActiveImpl(IntPtr nativeDisplay);

		// Token: 0x06000828 RID: 2088
		[FreeFunction("UnityDisplayManager_RequiresBlitToBackbuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RequiresBlitToBackbufferImpl(IntPtr nativeDisplay);

		// Token: 0x06000829 RID: 2089
		[FreeFunction("UnityDisplayManager_RequiresSRGBBlitToBackbuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RequiresSrgbBlitToBackbufferImpl(IntPtr nativeDisplay);

		// Token: 0x0600082A RID: 2090 RVA: 0x0000C49F File Offset: 0x0000A69F
		// Note: this type is marked as 'beforefieldinit'.
		static Display()
		{
		}

		// Token: 0x040003AA RID: 938
		internal IntPtr nativeDisplay;

		// Token: 0x040003AB RID: 939
		public static Display[] displays = new Display[]
		{
			new Display()
		};

		// Token: 0x040003AC RID: 940
		private static Display _mainDisplay = Display.displays[0];

		// Token: 0x040003AD RID: 941
		private static int m_ActiveEditorGameViewTarget = -1;

		// Token: 0x040003AE RID: 942
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Display.DisplaysUpdatedDelegate onDisplaysUpdated = null;

		// Token: 0x02000124 RID: 292
		// (Invoke) Token: 0x0600082C RID: 2092
		public delegate void DisplaysUpdatedDelegate();
	}
}
