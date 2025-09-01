using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200002D RID: 45
	[NativeHeader("IMGUIScriptingClasses.h")]
	[NativeHeader("Modules/IMGUI/GUIStyle.bindings.h")]
	[RequiredByNativeCode]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class GUIStyle
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002E4 RID: 740
		// (set) Token: 0x060002E5 RID: 741
		[NativeProperty("Name", false, TargetType.Function)]
		internal extern string rawName { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002E6 RID: 742
		// (set) Token: 0x060002E7 RID: 743
		[NativeProperty("Font", false, TargetType.Function)]
		public extern Font font { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002E8 RID: 744
		// (set) Token: 0x060002E9 RID: 745
		[NativeProperty("m_ImagePosition", false, TargetType.Field)]
		public extern ImagePosition imagePosition { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002EA RID: 746
		// (set) Token: 0x060002EB RID: 747
		[NativeProperty("m_Alignment", false, TargetType.Field)]
		public extern TextAnchor alignment { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002EC RID: 748
		// (set) Token: 0x060002ED RID: 749
		[NativeProperty("m_WordWrap", false, TargetType.Field)]
		public extern bool wordWrap { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002EE RID: 750
		// (set) Token: 0x060002EF RID: 751
		[NativeProperty("m_Clipping", false, TargetType.Field)]
		public extern TextClipping clipping { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000B62C File Offset: 0x0000982C
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000B642 File Offset: 0x00009842
		[NativeProperty("m_ContentOffset", false, TargetType.Field)]
		public Vector2 contentOffset
		{
			get
			{
				Vector2 result;
				this.get_contentOffset_Injected(out result);
				return result;
			}
			set
			{
				this.set_contentOffset_Injected(ref value);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002F2 RID: 754
		// (set) Token: 0x060002F3 RID: 755
		[NativeProperty("m_FixedWidth", false, TargetType.Field)]
		public extern float fixedWidth { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002F4 RID: 756
		// (set) Token: 0x060002F5 RID: 757
		[NativeProperty("m_FixedHeight", false, TargetType.Field)]
		public extern float fixedHeight { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002F6 RID: 758
		// (set) Token: 0x060002F7 RID: 759
		[NativeProperty("m_StretchWidth", false, TargetType.Field)]
		public extern bool stretchWidth { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002F8 RID: 760
		// (set) Token: 0x060002F9 RID: 761
		[NativeProperty("m_StretchHeight", false, TargetType.Field)]
		public extern bool stretchHeight { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002FA RID: 762
		// (set) Token: 0x060002FB RID: 763
		[NativeProperty("m_FontSize", false, TargetType.Field)]
		public extern int fontSize { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002FC RID: 764
		// (set) Token: 0x060002FD RID: 765
		[NativeProperty("m_FontStyle", false, TargetType.Field)]
		public extern FontStyle fontStyle { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002FE RID: 766
		// (set) Token: 0x060002FF RID: 767
		[NativeProperty("m_RichText", false, TargetType.Field)]
		public extern bool richText { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000B64C File Offset: 0x0000984C
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000B662 File Offset: 0x00009862
		[Obsolete("Don't use clipOffset - put things inside BeginGroup instead. This functionality will be removed in a later version.", false)]
		[NativeProperty("m_ClipOffset", false, TargetType.Field)]
		public Vector2 clipOffset
		{
			get
			{
				Vector2 result;
				this.get_clipOffset_Injected(out result);
				return result;
			}
			set
			{
				this.set_clipOffset_Injected(ref value);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000B66C File Offset: 0x0000986C
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0000B682 File Offset: 0x00009882
		[NativeProperty("m_ClipOffset", false, TargetType.Field)]
		internal Vector2 Internal_clipOffset
		{
			get
			{
				Vector2 result;
				this.get_Internal_clipOffset_Injected(out result);
				return result;
			}
			set
			{
				this.set_Internal_clipOffset_Injected(ref value);
			}
		}

		// Token: 0x06000304 RID: 772
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Create", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create(GUIStyle self);

		// Token: 0x06000305 RID: 773
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Copy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Copy(GUIStyle self, GUIStyle other);

		// Token: 0x06000306 RID: 774
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr self);

		// Token: 0x06000307 RID: 775
		[FreeFunction(Name = "GUIStyle_Bindings::GetStyleStatePtr", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetStyleStatePtr(int idx);

		// Token: 0x06000308 RID: 776
		[FreeFunction(Name = "GUIStyle_Bindings::AssignStyleState", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AssignStyleState(int idx, IntPtr srcStyleState);

		// Token: 0x06000309 RID: 777
		[FreeFunction(Name = "GUIStyle_Bindings::GetRectOffsetPtr", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetRectOffsetPtr(int idx);

		// Token: 0x0600030A RID: 778
		[FreeFunction(Name = "GUIStyle_Bindings::AssignRectOffset", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AssignRectOffset(int idx, IntPtr srcRectOffset);

		// Token: 0x0600030B RID: 779
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetLineHeight")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetLineHeight(IntPtr target);

		// Token: 0x0600030C RID: 780 RVA: 0x0000B68C File Offset: 0x0000988C
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Draw", HasExplicitThis = true)]
		private void Internal_Draw(Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Internal_Draw_Injected(ref screenRect, content, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B69E File Offset: 0x0000989E
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Draw2", HasExplicitThis = true)]
		private void Internal_Draw2(Rect position, GUIContent content, int controlID, bool on)
		{
			this.Internal_Draw2_Injected(ref position, content, controlID, on);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000B6AC File Offset: 0x000098AC
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_DrawCursor", HasExplicitThis = true)]
		private void Internal_DrawCursor(Rect position, GUIContent content, int pos, Color cursorColor)
		{
			this.Internal_DrawCursor_Injected(ref position, content, pos, ref cursorColor);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000B6BC File Offset: 0x000098BC
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_DrawWithTextSelection", HasExplicitThis = true)]
		private void Internal_DrawWithTextSelection(Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus, bool drawSelectionAsComposition, int cursorFirst, int cursorLast, Color cursorColor, Color selectionColor)
		{
			this.Internal_DrawWithTextSelection_Injected(ref screenRect, content, isHover, isActive, on, hasKeyboardFocus, drawSelectionAsComposition, cursorFirst, cursorLast, ref cursorColor, ref selectionColor);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000B6E4 File Offset: 0x000098E4
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetCursorPixelPosition", HasExplicitThis = true)]
		internal Vector2 Internal_GetCursorPixelPosition(Rect position, GUIContent content, int cursorStringIndex)
		{
			Vector2 result;
			this.Internal_GetCursorPixelPosition_Injected(ref position, content, cursorStringIndex, out result);
			return result;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000B6FE File Offset: 0x000098FE
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetCursorStringIndex", HasExplicitThis = true)]
		internal int Internal_GetCursorStringIndex(Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			return this.Internal_GetCursorStringIndex_Injected(ref position, content, ref cursorPixelPosition);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000B70B File Offset: 0x0000990B
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetSelectedRenderedText", HasExplicitThis = true)]
		internal string Internal_GetSelectedRenderedText(Rect localPosition, GUIContent mContent, int selectIndex, int cursorIndex)
		{
			return this.Internal_GetSelectedRenderedText_Injected(ref localPosition, mContent, selectIndex, cursorIndex);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000B719 File Offset: 0x00009919
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetHyperlinksRect", HasExplicitThis = true)]
		internal Rect[] Internal_GetHyperlinksRect(Rect localPosition, GUIContent mContent)
		{
			return this.Internal_GetHyperlinksRect_Injected(ref localPosition, mContent);
		}

		// Token: 0x06000314 RID: 788
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetNumCharactersThatFitWithinWidth", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int Internal_GetNumCharactersThatFitWithinWidth(string text, float width);

		// Token: 0x06000315 RID: 789 RVA: 0x0000B724 File Offset: 0x00009924
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcSize", HasExplicitThis = true)]
		internal Vector2 Internal_CalcSize(GUIContent content)
		{
			Vector2 result;
			this.Internal_CalcSize_Injected(content, out result);
			return result;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000B73C File Offset: 0x0000993C
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcSizeWithConstraints", HasExplicitThis = true)]
		internal Vector2 Internal_CalcSizeWithConstraints(GUIContent content, Vector2 maxSize)
		{
			Vector2 result;
			this.Internal_CalcSizeWithConstraints_Injected(content, ref maxSize, out result);
			return result;
		}

		// Token: 0x06000317 RID: 791
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcHeight", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float Internal_CalcHeight(GUIContent content, float width);

		// Token: 0x06000318 RID: 792 RVA: 0x0000B758 File Offset: 0x00009958
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcMinMaxWidth", HasExplicitThis = true)]
		private Vector2 Internal_CalcMinMaxWidth(GUIContent content)
		{
			Vector2 result;
			this.Internal_CalcMinMaxWidth_Injected(content, out result);
			return result;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000B76F File Offset: 0x0000996F
		[FreeFunction(Name = "GUIStyle_Bindings::SetMouseTooltip")]
		internal static void SetMouseTooltip(string tooltip, Rect screenRect)
		{
			GUIStyle.SetMouseTooltip_Injected(tooltip, ref screenRect);
		}

		// Token: 0x0600031A RID: 794
		[FreeFunction(Name = "GUIStyle_Bindings::IsTooltipActive")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsTooltipActive(string tooltip);

		// Token: 0x0600031B RID: 795
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetCursorFlashOffset")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetCursorFlashOffset();

		// Token: 0x0600031C RID: 796
		[FreeFunction(Name = "GUIStyle::SetDefaultFont")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetDefaultFont(Font font);

		// Token: 0x0600031D RID: 797 RVA: 0x0000B779 File Offset: 0x00009979
		public GUIStyle()
		{
			this.m_Ptr = GUIStyle.Internal_Create(this);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000B790 File Offset: 0x00009990
		public GUIStyle(GUIStyle other)
		{
			bool flag = other == null;
			if (flag)
			{
				Debug.LogError("Copied style is null. Using StyleNotFound instead.");
				other = GUISkin.error;
			}
			this.m_Ptr = GUIStyle.Internal_Copy(this, other);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000B7D0 File Offset: 0x000099D0
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					GUIStyle.Internal_Destroy(this.m_Ptr);
					this.m_Ptr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000B828 File Offset: 0x00009A28
		internal static void CleanupRoots()
		{
			GUIStyle.s_None = null;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000B834 File Offset: 0x00009A34
		internal void InternalOnAfterDeserialize()
		{
			this.m_Normal = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(0));
			this.m_Hover = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(1));
			this.m_Active = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(2));
			this.m_Focused = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(3));
			this.m_OnNormal = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(4));
			this.m_OnHover = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(5));
			this.m_OnActive = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(6));
			this.m_OnFocused = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(7));
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000B8DC File Offset: 0x00009ADC
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000B907 File Offset: 0x00009B07
		public string name
		{
			get
			{
				string result;
				if ((result = this.m_Name) == null)
				{
					result = (this.m_Name = this.rawName);
				}
				return result;
			}
			set
			{
				this.m_Name = value;
				this.rawName = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000B91C File Offset: 0x00009B1C
		// (set) Token: 0x06000325 RID: 805 RVA: 0x0000B94E File Offset: 0x00009B4E
		public GUIStyleState normal
		{
			get
			{
				GUIStyleState result;
				if ((result = this.m_Normal) == null)
				{
					result = (this.m_Normal = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(0)));
				}
				return result;
			}
			set
			{
				this.AssignStyleState(0, value.m_Ptr);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000B960 File Offset: 0x00009B60
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000B992 File Offset: 0x00009B92
		public GUIStyleState hover
		{
			get
			{
				GUIStyleState result;
				if ((result = this.m_Hover) == null)
				{
					result = (this.m_Hover = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(1)));
				}
				return result;
			}
			set
			{
				this.AssignStyleState(1, value.m_Ptr);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000B9A4 File Offset: 0x00009BA4
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0000B9D6 File Offset: 0x00009BD6
		public GUIStyleState active
		{
			get
			{
				GUIStyleState result;
				if ((result = this.m_Active) == null)
				{
					result = (this.m_Active = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(2)));
				}
				return result;
			}
			set
			{
				this.AssignStyleState(2, value.m_Ptr);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000BA1A File Offset: 0x00009C1A
		public GUIStyleState onNormal
		{
			get
			{
				GUIStyleState result;
				if ((result = this.m_OnNormal) == null)
				{
					result = (this.m_OnNormal = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(4)));
				}
				return result;
			}
			set
			{
				this.AssignStyleState(4, value.m_Ptr);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000BA2C File Offset: 0x00009C2C
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000BA5E File Offset: 0x00009C5E
		public GUIStyleState onHover
		{
			get
			{
				GUIStyleState result;
				if ((result = this.m_OnHover) == null)
				{
					result = (this.m_OnHover = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(5)));
				}
				return result;
			}
			set
			{
				this.AssignStyleState(5, value.m_Ptr);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000BA70 File Offset: 0x00009C70
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000BAA2 File Offset: 0x00009CA2
		public GUIStyleState onActive
		{
			get
			{
				GUIStyleState result;
				if ((result = this.m_OnActive) == null)
				{
					result = (this.m_OnActive = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(6)));
				}
				return result;
			}
			set
			{
				this.AssignStyleState(6, value.m_Ptr);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000BAE6 File Offset: 0x00009CE6
		public GUIStyleState focused
		{
			get
			{
				GUIStyleState result;
				if ((result = this.m_Focused) == null)
				{
					result = (this.m_Focused = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(3)));
				}
				return result;
			}
			set
			{
				this.AssignStyleState(3, value.m_Ptr);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000BB2A File Offset: 0x00009D2A
		public GUIStyleState onFocused
		{
			get
			{
				GUIStyleState result;
				if ((result = this.m_OnFocused) == null)
				{
					result = (this.m_OnFocused = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(7)));
				}
				return result;
			}
			set
			{
				this.AssignStyleState(7, value.m_Ptr);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000BB3C File Offset: 0x00009D3C
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000BB6E File Offset: 0x00009D6E
		public RectOffset border
		{
			get
			{
				RectOffset result;
				if ((result = this.m_Border) == null)
				{
					result = (this.m_Border = new RectOffset(this, this.GetRectOffsetPtr(0)));
				}
				return result;
			}
			set
			{
				this.AssignRectOffset(0, value.m_Ptr);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000BB80 File Offset: 0x00009D80
		// (set) Token: 0x06000337 RID: 823 RVA: 0x0000BBB2 File Offset: 0x00009DB2
		public RectOffset margin
		{
			get
			{
				RectOffset result;
				if ((result = this.m_Margin) == null)
				{
					result = (this.m_Margin = new RectOffset(this, this.GetRectOffsetPtr(1)));
				}
				return result;
			}
			set
			{
				this.AssignRectOffset(1, value.m_Ptr);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000BBC4 File Offset: 0x00009DC4
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0000BBF6 File Offset: 0x00009DF6
		public RectOffset padding
		{
			get
			{
				RectOffset result;
				if ((result = this.m_Padding) == null)
				{
					result = (this.m_Padding = new RectOffset(this, this.GetRectOffsetPtr(2)));
				}
				return result;
			}
			set
			{
				this.AssignRectOffset(2, value.m_Ptr);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000BC08 File Offset: 0x00009E08
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0000BC3A File Offset: 0x00009E3A
		public RectOffset overflow
		{
			get
			{
				RectOffset result;
				if ((result = this.m_Overflow) == null)
				{
					result = (this.m_Overflow = new RectOffset(this, this.GetRectOffsetPtr(3)));
				}
				return result;
			}
			set
			{
				this.AssignRectOffset(3, value.m_Ptr);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000BC4B File Offset: 0x00009E4B
		public float lineHeight
		{
			get
			{
				return Mathf.Round(GUIStyle.Internal_GetLineHeight(this.m_Ptr));
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000BC5D File Offset: 0x00009E5D
		public void Draw(Rect position, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, GUIContent.none, -1, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000BC74 File Offset: 0x00009E74
		public void Draw(Rect position, string text, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, GUIContent.Temp(text), -1, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000BC8D File Offset: 0x00009E8D
		public void Draw(Rect position, Texture image, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, GUIContent.Temp(image), -1, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000BCA6 File Offset: 0x00009EA6
		public void Draw(Rect position, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, content, -1, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000BCBA File Offset: 0x00009EBA
		public void Draw(Rect position, GUIContent content, int controlID)
		{
			this.Draw(position, content, controlID, false, false, false, false);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000BCCB File Offset: 0x00009ECB
		public void Draw(Rect position, GUIContent content, int controlID, bool on)
		{
			this.Draw(position, content, controlID, false, false, on, false);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000BCDD File Offset: 0x00009EDD
		public void Draw(Rect position, GUIContent content, int controlID, bool on, bool hover)
		{
			this.Draw(position, content, controlID, hover, GUIUtility.hotControl == controlID, on, GUIUtility.HasKeyFocus(controlID));
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000BCFC File Offset: 0x00009EFC
		private void Draw(Rect position, GUIContent content, int controlId, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			bool flag = controlId == -1;
			if (flag)
			{
				this.Internal_Draw(position, content, isHover, isActive, on, hasKeyboardFocus);
			}
			else
			{
				this.Internal_Draw2(position, content, controlId, on);
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000BD34 File Offset: 0x00009F34
		public void DrawCursor(Rect position, GUIContent content, int controlID, int character)
		{
			Event current = Event.current;
			bool flag = current.type == EventType.Repaint;
			if (flag)
			{
				Color cursorColor = new Color(0f, 0f, 0f, 0f);
				float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
				float num = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
				bool flag2 = cursorFlashSpeed == 0f || num < 0.5f;
				if (flag2)
				{
					cursorColor = GUI.skin.settings.cursorColor;
				}
				this.Internal_DrawCursor(position, content, character, cursorColor);
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000BDCC File Offset: 0x00009FCC
		internal void DrawWithTextSelection(Rect position, GUIContent content, bool isActive, bool hasKeyboardFocus, int firstSelectedCharacter, int lastSelectedCharacter, bool drawSelectionAsComposition, Color selectionColor)
		{
			Color cursorColor = new Color(0f, 0f, 0f, 0f);
			float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
			float num = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
			bool flag = cursorFlashSpeed == 0f || num < 0.5f;
			if (flag)
			{
				cursorColor = GUI.skin.settings.cursorColor;
			}
			bool isHover = position.Contains(Event.current.mousePosition);
			this.Internal_DrawWithTextSelection(position, content, isHover, isActive, false, hasKeyboardFocus, drawSelectionAsComposition, firstSelectedCharacter, lastSelectedCharacter, cursorColor, selectionColor);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000BE6C File Offset: 0x0000A06C
		internal void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter, bool drawSelectionAsComposition)
		{
			this.DrawWithTextSelection(position, content, controlID == GUIUtility.hotControl, controlID == GUIUtility.keyboardControl && GUIStyle.showKeyboardFocus, firstSelectedCharacter, lastSelectedCharacter, drawSelectionAsComposition, GUI.skin.settings.selectionColor);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000BEB0 File Offset: 0x0000A0B0
		public void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter)
		{
			this.DrawWithTextSelection(position, content, controlID, firstSelectedCharacter, lastSelectedCharacter, false);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000BEC4 File Offset: 0x0000A0C4
		public static implicit operator GUIStyle(string str)
		{
			bool flag = GUISkin.current == null;
			GUIStyle result;
			if (flag)
			{
				Debug.LogError("Unable to use a named GUIStyle without a current skin. Most likely you need to move your GUIStyle initialization code to OnGUI");
				result = GUISkin.error;
			}
			else
			{
				result = GUISkin.current.GetStyle(str);
			}
			return result;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000BF04 File Offset: 0x0000A104
		public static GUIStyle none
		{
			get
			{
				GUIStyle result;
				if ((result = GUIStyle.s_None) == null)
				{
					result = (GUIStyle.s_None = new GUIStyle());
				}
				return result;
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000BF1C File Offset: 0x0000A11C
		public Vector2 GetCursorPixelPosition(Rect position, GUIContent content, int cursorStringIndex)
		{
			return this.Internal_GetCursorPixelPosition(position, content, cursorStringIndex);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000BF38 File Offset: 0x0000A138
		public int GetCursorStringIndex(Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			return this.Internal_GetCursorStringIndex(position, content, cursorPixelPosition);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000BF54 File Offset: 0x0000A154
		internal int GetNumCharactersThatFitWithinWidth(string text, float width)
		{
			return this.Internal_GetNumCharactersThatFitWithinWidth(text, width);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000BF70 File Offset: 0x0000A170
		public Vector2 CalcSize(GUIContent content)
		{
			return this.Internal_CalcSize(content);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000BF8C File Offset: 0x0000A18C
		internal Vector2 CalcSizeWithConstraints(GUIContent content, Vector2 constraints)
		{
			return this.Internal_CalcSizeWithConstraints(content, constraints);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000BFA8 File Offset: 0x0000A1A8
		public Vector2 CalcScreenSize(Vector2 contentSize)
		{
			return new Vector2((this.fixedWidth != 0f) ? this.fixedWidth : Mathf.Ceil(contentSize.x + (float)this.padding.left + (float)this.padding.right), (this.fixedHeight != 0f) ? this.fixedHeight : Mathf.Ceil(contentSize.y + (float)this.padding.top + (float)this.padding.bottom));
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000C034 File Offset: 0x0000A234
		public float CalcHeight(GUIContent content, float width)
		{
			return this.Internal_CalcHeight(content, width);
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000C04E File Offset: 0x0000A24E
		public bool isHeightDependantOnWidth
		{
			get
			{
				return this.fixedHeight == 0f && this.wordWrap && this.imagePosition != ImagePosition.ImageOnly;
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000C078 File Offset: 0x0000A278
		public void CalcMinMaxWidth(GUIContent content, out float minWidth, out float maxWidth)
		{
			Vector2 vector = this.Internal_CalcMinMaxWidth(content);
			minWidth = vector.x;
			maxWidth = vector.y;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
		public override string ToString()
		{
			return UnityString.Format("GUIStyle '{0}'", new object[]
			{
				this.name
			});
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000C0CB File Offset: 0x0000A2CB
		// Note: this type is marked as 'beforefieldinit'.
		static GUIStyle()
		{
		}

		// Token: 0x06000356 RID: 854
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_contentOffset_Injected(out Vector2 ret);

		// Token: 0x06000357 RID: 855
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_contentOffset_Injected(ref Vector2 value);

		// Token: 0x06000358 RID: 856
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_clipOffset_Injected(out Vector2 ret);

		// Token: 0x06000359 RID: 857
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_clipOffset_Injected(ref Vector2 value);

		// Token: 0x0600035A RID: 858
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_Internal_clipOffset_Injected(out Vector2 ret);

		// Token: 0x0600035B RID: 859
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_Internal_clipOffset_Injected(ref Vector2 value);

		// Token: 0x0600035C RID: 860
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_Draw_Injected(ref Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus);

		// Token: 0x0600035D RID: 861
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_Draw2_Injected(ref Rect position, GUIContent content, int controlID, bool on);

		// Token: 0x0600035E RID: 862
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_DrawCursor_Injected(ref Rect position, GUIContent content, int pos, ref Color cursorColor);

		// Token: 0x0600035F RID: 863
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_DrawWithTextSelection_Injected(ref Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus, bool drawSelectionAsComposition, int cursorFirst, int cursorLast, ref Color cursorColor, ref Color selectionColor);

		// Token: 0x06000360 RID: 864
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetCursorPixelPosition_Injected(ref Rect position, GUIContent content, int cursorStringIndex, out Vector2 ret);

		// Token: 0x06000361 RID: 865
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int Internal_GetCursorStringIndex_Injected(ref Rect position, GUIContent content, ref Vector2 cursorPixelPosition);

		// Token: 0x06000362 RID: 866
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string Internal_GetSelectedRenderedText_Injected(ref Rect localPosition, GUIContent mContent, int selectIndex, int cursorIndex);

		// Token: 0x06000363 RID: 867
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Rect[] Internal_GetHyperlinksRect_Injected(ref Rect localPosition, GUIContent mContent);

		// Token: 0x06000364 RID: 868
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_CalcSize_Injected(GUIContent content, out Vector2 ret);

		// Token: 0x06000365 RID: 869
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_CalcSizeWithConstraints_Injected(GUIContent content, ref Vector2 maxSize, out Vector2 ret);

		// Token: 0x06000366 RID: 870
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_CalcMinMaxWidth_Injected(GUIContent content, out Vector2 ret);

		// Token: 0x06000367 RID: 871
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMouseTooltip_Injected(string tooltip, ref Rect screenRect);

		// Token: 0x040000CB RID: 203
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x040000CC RID: 204
		[NonSerialized]
		private GUIStyleState m_Normal;

		// Token: 0x040000CD RID: 205
		[NonSerialized]
		private GUIStyleState m_Hover;

		// Token: 0x040000CE RID: 206
		[NonSerialized]
		private GUIStyleState m_Active;

		// Token: 0x040000CF RID: 207
		[NonSerialized]
		private GUIStyleState m_Focused;

		// Token: 0x040000D0 RID: 208
		[NonSerialized]
		private GUIStyleState m_OnNormal;

		// Token: 0x040000D1 RID: 209
		[NonSerialized]
		private GUIStyleState m_OnHover;

		// Token: 0x040000D2 RID: 210
		[NonSerialized]
		private GUIStyleState m_OnActive;

		// Token: 0x040000D3 RID: 211
		[NonSerialized]
		private GUIStyleState m_OnFocused;

		// Token: 0x040000D4 RID: 212
		[NonSerialized]
		private RectOffset m_Border;

		// Token: 0x040000D5 RID: 213
		[NonSerialized]
		private RectOffset m_Padding;

		// Token: 0x040000D6 RID: 214
		[NonSerialized]
		private RectOffset m_Margin;

		// Token: 0x040000D7 RID: 215
		[NonSerialized]
		private RectOffset m_Overflow;

		// Token: 0x040000D8 RID: 216
		[NonSerialized]
		private string m_Name;

		// Token: 0x040000D9 RID: 217
		internal static bool showKeyboardFocus = true;

		// Token: 0x040000DA RID: 218
		private static GUIStyle s_None;
	}
}
