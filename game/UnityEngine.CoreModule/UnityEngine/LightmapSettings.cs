using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000133 RID: 307
	[NativeHeader("Runtime/Graphics/LightmapSettings.h")]
	[StaticAccessor("GetLightmapSettings()")]
	public sealed class LightmapSettings : Object
	{
		// Token: 0x0600099D RID: 2461 RVA: 0x0000E886 File Offset: 0x0000CA86
		private LightmapSettings()
		{
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600099E RID: 2462
		// (set) Token: 0x0600099F RID: 2463
		public static extern LightmapData[] lightmaps { [FreeFunction] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction(ThrowsException = true)] [MethodImpl(MethodImplOptions.InternalCall)] [param: Unmarshalled] set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060009A0 RID: 2464
		// (set) Token: 0x060009A1 RID: 2465
		public static extern LightmapsMode lightmapsMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction(ThrowsException = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060009A2 RID: 2466
		// (set) Token: 0x060009A3 RID: 2467
		public static extern LightProbes lightProbes { [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction] [NativeName("SetLightProbes")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060009A4 RID: 2468
		[NativeName("ResetAndAwakeFromLoad")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Reset();

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0000E890 File Offset: 0x0000CA90
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x00004563 File Offset: 0x00002763
		[Obsolete("Use lightmapsMode instead.", false)]
		public static LightmapsModeLegacy lightmapsModeLegacy
		{
			get
			{
				return LightmapsModeLegacy.Single;
			}
			set
			{
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x0000E8A4 File Offset: 0x0000CAA4
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x00004563 File Offset: 0x00002763
		[Obsolete("Use QualitySettings.desiredColorSpace instead.", false)]
		public static ColorSpace bakedColorSpace
		{
			get
			{
				return QualitySettings.desiredColorSpace;
			}
			set
			{
			}
		}
	}
}
