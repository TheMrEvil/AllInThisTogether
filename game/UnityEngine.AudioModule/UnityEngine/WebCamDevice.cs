using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000021 RID: 33
	[UsedByNativeCode]
	public struct WebCamDevice
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00002DBC File Offset: 0x00000FBC
		public bool isFrontFacing
		{
			get
			{
				return (this.m_Flags & 1) != 0;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00002DDC File Offset: 0x00000FDC
		public WebCamKind kind
		{
			get
			{
				return this.m_Kind;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public string depthCameraName
		{
			get
			{
				return (this.m_DepthCameraName == "") ? null : this.m_DepthCameraName;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00002E24 File Offset: 0x00001024
		public bool isAutoFocusPointSupported
		{
			get
			{
				return (this.m_Flags & 2) != 0;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00002E44 File Offset: 0x00001044
		public Resolution[] availableResolutions
		{
			get
			{
				return this.m_Resolutions;
			}
		}

		// Token: 0x04000062 RID: 98
		[NativeName("name")]
		internal string m_Name;

		// Token: 0x04000063 RID: 99
		[NativeName("depthCameraName")]
		internal string m_DepthCameraName;

		// Token: 0x04000064 RID: 100
		[NativeName("flags")]
		internal int m_Flags;

		// Token: 0x04000065 RID: 101
		[NativeName("kind")]
		internal WebCamKind m_Kind;

		// Token: 0x04000066 RID: 102
		[NativeName("resolutions")]
		internal Resolution[] m_Resolutions;
	}
}
