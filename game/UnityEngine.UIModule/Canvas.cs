using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	[NativeClass("UI::Canvas")]
	[NativeHeader("Modules/UI/UIStructs.h")]
	[NativeHeader("Modules/UI/CanvasManager.h")]
	[NativeHeader("Modules/UI/Canvas.h")]
	[RequireComponent(typeof(RectTransform))]
	public sealed class Canvas : Behaviour
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000054 RID: 84 RVA: 0x00002990 File Offset: 0x00000B90
		// (remove) Token: 0x06000055 RID: 85 RVA: 0x000029C4 File Offset: 0x00000BC4
		public static event Canvas.WillRenderCanvases preWillRenderCanvases
		{
			[CompilerGenerated]
			add
			{
				Canvas.WillRenderCanvases willRenderCanvases = Canvas.preWillRenderCanvases;
				Canvas.WillRenderCanvases willRenderCanvases2;
				do
				{
					willRenderCanvases2 = willRenderCanvases;
					Canvas.WillRenderCanvases value2 = (Canvas.WillRenderCanvases)Delegate.Combine(willRenderCanvases2, value);
					willRenderCanvases = Interlocked.CompareExchange<Canvas.WillRenderCanvases>(ref Canvas.preWillRenderCanvases, value2, willRenderCanvases2);
				}
				while (willRenderCanvases != willRenderCanvases2);
			}
			[CompilerGenerated]
			remove
			{
				Canvas.WillRenderCanvases willRenderCanvases = Canvas.preWillRenderCanvases;
				Canvas.WillRenderCanvases willRenderCanvases2;
				do
				{
					willRenderCanvases2 = willRenderCanvases;
					Canvas.WillRenderCanvases value2 = (Canvas.WillRenderCanvases)Delegate.Remove(willRenderCanvases2, value);
					willRenderCanvases = Interlocked.CompareExchange<Canvas.WillRenderCanvases>(ref Canvas.preWillRenderCanvases, value2, willRenderCanvases2);
				}
				while (willRenderCanvases != willRenderCanvases2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000056 RID: 86 RVA: 0x000029F8 File Offset: 0x00000BF8
		// (remove) Token: 0x06000057 RID: 87 RVA: 0x00002A2C File Offset: 0x00000C2C
		public static event Canvas.WillRenderCanvases willRenderCanvases
		{
			[CompilerGenerated]
			add
			{
				Canvas.WillRenderCanvases willRenderCanvases = Canvas.willRenderCanvases;
				Canvas.WillRenderCanvases willRenderCanvases2;
				do
				{
					willRenderCanvases2 = willRenderCanvases;
					Canvas.WillRenderCanvases value2 = (Canvas.WillRenderCanvases)Delegate.Combine(willRenderCanvases2, value);
					willRenderCanvases = Interlocked.CompareExchange<Canvas.WillRenderCanvases>(ref Canvas.willRenderCanvases, value2, willRenderCanvases2);
				}
				while (willRenderCanvases != willRenderCanvases2);
			}
			[CompilerGenerated]
			remove
			{
				Canvas.WillRenderCanvases willRenderCanvases = Canvas.willRenderCanvases;
				Canvas.WillRenderCanvases willRenderCanvases2;
				do
				{
					willRenderCanvases2 = willRenderCanvases;
					Canvas.WillRenderCanvases value2 = (Canvas.WillRenderCanvases)Delegate.Remove(willRenderCanvases2, value);
					willRenderCanvases = Interlocked.CompareExchange<Canvas.WillRenderCanvases>(ref Canvas.willRenderCanvases, value2, willRenderCanvases2);
				}
				while (willRenderCanvases != willRenderCanvases2);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000058 RID: 88
		// (set) Token: 0x06000059 RID: 89
		public extern RenderMode renderMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005A RID: 90
		public extern bool isRootCanvas { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002A60 File Offset: 0x00000C60
		public Rect pixelRect
		{
			get
			{
				Rect result;
				this.get_pixelRect_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005C RID: 92
		// (set) Token: 0x0600005D RID: 93
		public extern float scaleFactor { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005E RID: 94
		// (set) Token: 0x0600005F RID: 95
		public extern float referencePixelsPerUnit { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000060 RID: 96
		// (set) Token: 0x06000061 RID: 97
		public extern bool overridePixelPerfect { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000062 RID: 98
		// (set) Token: 0x06000063 RID: 99
		public extern bool vertexColorAlwaysGammaSpace { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000064 RID: 100
		// (set) Token: 0x06000065 RID: 101
		public extern bool pixelPerfect { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000066 RID: 102
		// (set) Token: 0x06000067 RID: 103
		public extern float planeDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000068 RID: 104
		public extern int renderOrder { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000069 RID: 105
		// (set) Token: 0x0600006A RID: 106
		public extern bool overrideSorting { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006B RID: 107
		// (set) Token: 0x0600006C RID: 108
		public extern int sortingOrder { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006D RID: 109
		// (set) Token: 0x0600006E RID: 110
		public extern int targetDisplay { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006F RID: 111
		// (set) Token: 0x06000070 RID: 112
		public extern int sortingLayerID { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000071 RID: 113
		public extern int cachedSortingLayerValue { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000072 RID: 114
		// (set) Token: 0x06000073 RID: 115
		public extern AdditionalCanvasShaderChannels additionalShaderChannels { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000074 RID: 116
		// (set) Token: 0x06000075 RID: 117
		public extern string sortingLayerName { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000076 RID: 118
		public extern Canvas rootCanvas { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002A78 File Offset: 0x00000C78
		public Vector2 renderingDisplaySize
		{
			get
			{
				Vector2 result;
				this.get_renderingDisplaySize_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002A8E File Offset: 0x00000C8E
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002A95 File Offset: 0x00000C95
		internal static Action<int> externBeginRenderOverlays
		{
			[CompilerGenerated]
			get
			{
				return Canvas.<externBeginRenderOverlays>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				Canvas.<externBeginRenderOverlays>k__BackingField = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002A9D File Offset: 0x00000C9D
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002AA4 File Offset: 0x00000CA4
		internal static Action<int, int> externRenderOverlaysBefore
		{
			[CompilerGenerated]
			get
			{
				return Canvas.<externRenderOverlaysBefore>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				Canvas.<externRenderOverlaysBefore>k__BackingField = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002AAC File Offset: 0x00000CAC
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002AB3 File Offset: 0x00000CB3
		internal static Action<int> externEndRenderOverlays
		{
			[CompilerGenerated]
			get
			{
				return Canvas.<externEndRenderOverlays>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				Canvas.<externEndRenderOverlays>k__BackingField = value;
			}
		}

		// Token: 0x0600007E RID: 126
		[FreeFunction("UI::CanvasManager::SetExternalCanvasEnabled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetExternalCanvasEnabled(bool enabled);

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007F RID: 127
		// (set) Token: 0x06000080 RID: 128
		[NativeProperty("Camera", false, TargetType.Function)]
		public extern Camera worldCamera { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000081 RID: 129
		// (set) Token: 0x06000082 RID: 130
		[NativeProperty("SortingBucketNormalizedSize", false, TargetType.Function)]
		public extern float normalizedSortingGridSize { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000083 RID: 131
		// (set) Token: 0x06000084 RID: 132
		[NativeProperty("SortingBucketNormalizedSize", false, TargetType.Function)]
		[Obsolete("Setting normalizedSize via a int is not supported. Please use normalizedSortingGridSize", false)]
		public extern int sortingGridNormalizedSize { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000085 RID: 133
		[FreeFunction("UI::GetDefaultUIMaterial")]
		[Obsolete("Shared default material now used for text and general UI elements, call Canvas.GetDefaultCanvasMaterial()", false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Material GetDefaultCanvasTextMaterial();

		// Token: 0x06000086 RID: 134
		[FreeFunction("UI::GetDefaultUIMaterial")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Material GetDefaultCanvasMaterial();

		// Token: 0x06000087 RID: 135
		[FreeFunction("UI::GetETC1SupportedCanvasMaterial")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Material GetETC1SupportedCanvasMaterial();

		// Token: 0x06000088 RID: 136
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void UpdateCanvasRectTransform(bool alignWithCamera);

		// Token: 0x06000089 RID: 137 RVA: 0x00002ABB File Offset: 0x00000CBB
		public static void ForceUpdateCanvases()
		{
			Canvas.SendPreWillRenderCanvases();
			Canvas.SendWillRenderCanvases();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002ACA File Offset: 0x00000CCA
		[RequiredByNativeCode]
		private static void SendPreWillRenderCanvases()
		{
			Canvas.WillRenderCanvases willRenderCanvases = Canvas.preWillRenderCanvases;
			if (willRenderCanvases != null)
			{
				willRenderCanvases();
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002ADE File Offset: 0x00000CDE
		[RequiredByNativeCode]
		private static void SendWillRenderCanvases()
		{
			Canvas.WillRenderCanvases willRenderCanvases = Canvas.willRenderCanvases;
			if (willRenderCanvases != null)
			{
				willRenderCanvases();
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002AF2 File Offset: 0x00000CF2
		[RequiredByNativeCode]
		private static void BeginRenderExtraOverlays(int displayIndex)
		{
			Action<int> externBeginRenderOverlays = Canvas.externBeginRenderOverlays;
			if (externBeginRenderOverlays != null)
			{
				externBeginRenderOverlays(displayIndex);
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002B07 File Offset: 0x00000D07
		[RequiredByNativeCode]
		private static void RenderExtraOverlaysBefore(int displayIndex, int sortingOrder)
		{
			Action<int, int> externRenderOverlaysBefore = Canvas.externRenderOverlaysBefore;
			if (externRenderOverlaysBefore != null)
			{
				externRenderOverlaysBefore(displayIndex, sortingOrder);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002B1D File Offset: 0x00000D1D
		[RequiredByNativeCode]
		private static void EndRenderExtraOverlays(int displayIndex)
		{
			Action<int> externEndRenderOverlays = Canvas.externEndRenderOverlays;
			if (externEndRenderOverlays != null)
			{
				externEndRenderOverlays(displayIndex);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002068 File Offset: 0x00000268
		public Canvas()
		{
		}

		// Token: 0x06000090 RID: 144
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_pixelRect_Injected(out Rect ret);

		// Token: 0x06000091 RID: 145
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_renderingDisplaySize_Injected(out Vector2 ret);

		// Token: 0x0400000E RID: 14
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Canvas.WillRenderCanvases preWillRenderCanvases;

		// Token: 0x0400000F RID: 15
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Canvas.WillRenderCanvases willRenderCanvases;

		// Token: 0x04000010 RID: 16
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<int> <externBeginRenderOverlays>k__BackingField;

		// Token: 0x04000011 RID: 17
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<int, int> <externRenderOverlaysBefore>k__BackingField;

		// Token: 0x04000012 RID: 18
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<int> <externEndRenderOverlays>k__BackingField;

		// Token: 0x02000009 RID: 9
		// (Invoke) Token: 0x06000093 RID: 147
		public delegate void WillRenderCanvases();
	}
}
