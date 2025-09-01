using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	[NativeHeader("Modules/TextRendering/Public/TextMesh.h")]
	[NativeClass("TextRenderingPrivate::TextMesh")]
	[RequireComponent(typeof(Transform), typeof(MeshRenderer))]
	public sealed class TextMesh : Component
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000042 RID: 66
		// (set) Token: 0x06000043 RID: 67
		public extern string text { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000044 RID: 68
		// (set) Token: 0x06000045 RID: 69
		public extern Font font { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000046 RID: 70
		// (set) Token: 0x06000047 RID: 71
		public extern int fontSize { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000048 RID: 72
		// (set) Token: 0x06000049 RID: 73
		public extern FontStyle fontStyle { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004A RID: 74
		// (set) Token: 0x0600004B RID: 75
		public extern float offsetZ { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004C RID: 76
		// (set) Token: 0x0600004D RID: 77
		public extern TextAlignment alignment { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004E RID: 78
		// (set) Token: 0x0600004F RID: 79
		public extern TextAnchor anchor { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000050 RID: 80
		// (set) Token: 0x06000051 RID: 81
		public extern float characterSize { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000052 RID: 82
		// (set) Token: 0x06000053 RID: 83
		public extern float lineSpacing { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000054 RID: 84
		// (set) Token: 0x06000055 RID: 85
		public extern float tabSize { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000056 RID: 86
		// (set) Token: 0x06000057 RID: 87
		public extern bool richText { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000294C File Offset: 0x00000B4C
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002962 File Offset: 0x00000B62
		public Color color
		{
			get
			{
				Color result;
				this.get_color_Injected(out result);
				return result;
			}
			set
			{
				this.set_color_Injected(ref value);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000296C File Offset: 0x00000B6C
		public TextMesh()
		{
		}

		// Token: 0x0600005B RID: 91
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_color_Injected(out Color ret);

		// Token: 0x0600005C RID: 92
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_color_Injected(ref Color value);
	}
}
