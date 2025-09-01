using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000132 RID: 306
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/LightmapData.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class LightmapData
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0000E804 File Offset: 0x0000CA04
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x0000E81C File Offset: 0x0000CA1C
		[Obsolete("Use lightmapColor property (UnityUpgradable) -> lightmapColor", false)]
		public Texture2D lightmapLight
		{
			get
			{
				return this.m_Light;
			}
			set
			{
				this.m_Light = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0000E828 File Offset: 0x0000CA28
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x0000E81C File Offset: 0x0000CA1C
		public Texture2D lightmapColor
		{
			get
			{
				return this.m_Light;
			}
			set
			{
				this.m_Light = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0000E840 File Offset: 0x0000CA40
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x0000E858 File Offset: 0x0000CA58
		public Texture2D lightmapDir
		{
			get
			{
				return this.m_Dir;
			}
			set
			{
				this.m_Dir = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0000E864 File Offset: 0x0000CA64
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x0000E87C File Offset: 0x0000CA7C
		public Texture2D shadowMask
		{
			get
			{
				return this.m_ShadowMask;
			}
			set
			{
				this.m_ShadowMask = value;
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00002072 File Offset: 0x00000272
		public LightmapData()
		{
		}

		// Token: 0x040003D9 RID: 985
		internal Texture2D m_Light;

		// Token: 0x040003DA RID: 986
		internal Texture2D m_Dir;

		// Token: 0x040003DB RID: 987
		internal Texture2D m_ShadowMask;
	}
}
