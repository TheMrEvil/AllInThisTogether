using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002C RID: 44
	[NativeHeader("Modules/IMGUI/GUIStyle.bindings.h")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class GUIStyleState
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002D7 RID: 727
		// (set) Token: 0x060002D8 RID: 728
		[NativeProperty("Background", false, TargetType.Function)]
		public extern Texture2D background { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000B558 File Offset: 0x00009758
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000B56E File Offset: 0x0000976E
		[NativeProperty("textColor", false, TargetType.Field)]
		public Color textColor
		{
			get
			{
				Color result;
				this.get_textColor_Injected(out result);
				return result;
			}
			set
			{
				this.set_textColor_Injected(ref value);
			}
		}

		// Token: 0x060002DB RID: 731
		[FreeFunction(Name = "GUIStyleState_Bindings::Init", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Init();

		// Token: 0x060002DC RID: 732
		[FreeFunction(Name = "GUIStyleState_Bindings::Cleanup", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup();

		// Token: 0x060002DD RID: 733 RVA: 0x0000B578 File Offset: 0x00009778
		public GUIStyleState()
		{
			this.m_Ptr = GUIStyleState.Init();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000B58D File Offset: 0x0000978D
		private GUIStyleState(GUIStyle sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000B5A8 File Offset: 0x000097A8
		internal static GUIStyleState ProduceGUIStyleStateFromDeserialization(GUIStyle sourceStyle, IntPtr source)
		{
			return new GUIStyleState(sourceStyle, source);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000B5C4 File Offset: 0x000097C4
		internal static GUIStyleState GetGUIStyleState(GUIStyle sourceStyle, IntPtr source)
		{
			return new GUIStyleState(sourceStyle, source);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000B5E0 File Offset: 0x000097E0
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_SourceStyle == null;
				if (flag)
				{
					this.Cleanup();
					this.m_Ptr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060002E2 RID: 738
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_textColor_Injected(out Color ret);

		// Token: 0x060002E3 RID: 739
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_textColor_Injected(ref Color value);

		// Token: 0x040000C9 RID: 201
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x040000CA RID: 202
		private readonly GUIStyle m_SourceStyle;
	}
}
