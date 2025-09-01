using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	[NativeHeader("Modules/IMGUI/GUISkin.bindings.h")]
	[NativeHeader("Modules/IMGUI/GUI.bindings.h")]
	public class GUI
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000382C File Offset: 0x00001A2C
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00003841 File Offset: 0x00001A41
		public static Color color
		{
			get
			{
				Color result;
				GUI.get_color_Injected(out result);
				return result;
			}
			set
			{
				GUI.set_color_Injected(ref value);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000384C File Offset: 0x00001A4C
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00003861 File Offset: 0x00001A61
		public static Color backgroundColor
		{
			get
			{
				Color result;
				GUI.get_backgroundColor_Injected(out result);
				return result;
			}
			set
			{
				GUI.set_backgroundColor_Injected(ref value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000386C File Offset: 0x00001A6C
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003881 File Offset: 0x00001A81
		public static Color contentColor
		{
			get
			{
				Color result;
				GUI.get_contentColor_Injected(out result);
				return result;
			}
			set
			{
				GUI.set_contentColor_Injected(ref value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000059 RID: 89
		// (set) Token: 0x0600005A RID: 90
		public static extern bool changed { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005B RID: 91
		// (set) Token: 0x0600005C RID: 92
		public static extern bool enabled { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005D RID: 93
		// (set) Token: 0x0600005E RID: 94
		public static extern int depth { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005F RID: 95
		internal static extern bool usePageScrollbars { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000060 RID: 96
		// (set) Token: 0x06000061 RID: 97
		internal static extern bool isInsideList { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000062 RID: 98
		internal static extern Material blendMaterial { [FreeFunction("GetGUIBlendMaterial")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000063 RID: 99
		internal static extern Material blitMaterial { [FreeFunction("GetGUIBlitMaterial")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000064 RID: 100
		internal static extern Material roundedRectMaterial { [FreeFunction("GetGUIRoundedRectMaterial")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000065 RID: 101
		internal static extern Material roundedRectWithColorPerBorderMaterial { [FreeFunction("GetGUIRoundedRectWithColorPerBorderMaterial")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000066 RID: 102
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GrabMouseControl(int id);

		// Token: 0x06000067 RID: 103
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasMouseControl(int id);

		// Token: 0x06000068 RID: 104
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReleaseMouseControl();

		// Token: 0x06000069 RID: 105
		[FreeFunction("GetGUIState().SetNameOfNextControl")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetNextControlName(string name);

		// Token: 0x0600006A RID: 106
		[FreeFunction("GetGUIState().GetNameOfFocusedControl")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetNameOfFocusedControl();

		// Token: 0x0600006B RID: 107
		[FreeFunction("GetGUIState().FocusKeyboardControl")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FocusControl(string name);

		// Token: 0x0600006C RID: 108
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalRepaintEditorWindow();

		// Token: 0x0600006D RID: 109
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetTooltip();

		// Token: 0x0600006E RID: 110
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetTooltip(string value);

		// Token: 0x0600006F RID: 111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetMouseTooltip();

		// Token: 0x06000070 RID: 112 RVA: 0x0000388C File Offset: 0x00001A8C
		private static Rect Internal_DoModalWindow(int id, int instanceID, Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, object skin)
		{
			Rect result;
			GUI.Internal_DoModalWindow_Injected(id, instanceID, ref clientRect, func, content, style, skin, out result);
			return result;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000038AC File Offset: 0x00001AAC
		private static Rect Internal_DoWindow(int id, int instanceID, Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style, object skin, bool forceRectOnLayout)
		{
			Rect result;
			GUI.Internal_DoWindow_Injected(id, instanceID, ref clientRect, func, title, style, skin, forceRectOnLayout, out result);
			return result;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000038CE File Offset: 0x00001ACE
		public static void DragWindow(Rect position)
		{
			GUI.DragWindow_Injected(ref position);
		}

		// Token: 0x06000073 RID: 115
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BringWindowToFront(int windowID);

		// Token: 0x06000074 RID: 116
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BringWindowToBack(int windowID);

		// Token: 0x06000075 RID: 117
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FocusWindow(int windowID);

		// Token: 0x06000076 RID: 118
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnfocusWindow();

		// Token: 0x06000077 RID: 119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BeginWindows();

		// Token: 0x06000078 RID: 120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_EndWindows();

		// Token: 0x06000079 RID: 121
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string Internal_Concatenate(GUIContent first, GUIContent second);

		// Token: 0x0600007A RID: 122 RVA: 0x000038D8 File Offset: 0x00001AD8
		static GUI()
		{
			GUI.nextScrollStepTime = DateTime.Now;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003979 File Offset: 0x00001B79
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003980 File Offset: 0x00001B80
		internal static int scrollTroughSide
		{
			[CompilerGenerated]
			get
			{
				return GUI.<scrollTroughSide>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				GUI.<scrollTroughSide>k__BackingField = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003988 File Offset: 0x00001B88
		// (set) Token: 0x0600007E RID: 126 RVA: 0x0000398F File Offset: 0x00001B8F
		internal static DateTime nextScrollStepTime
		{
			[CompilerGenerated]
			get
			{
				return GUI.<nextScrollStepTime>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				GUI.<nextScrollStepTime>k__BackingField = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000039A8 File Offset: 0x00001BA8
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003997 File Offset: 0x00001B97
		public static GUISkin skin
		{
			get
			{
				GUIUtility.CheckOnGUI();
				return GUI.s_Skin;
			}
			set
			{
				GUIUtility.CheckOnGUI();
				GUI.DoSetSkin(value);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000039C8 File Offset: 0x00001BC8
		internal static void DoSetSkin(GUISkin newSkin)
		{
			bool flag = !newSkin;
			if (flag)
			{
				newSkin = GUIUtility.GetDefaultSkin();
			}
			GUI.s_Skin = newSkin;
			newSkin.MakeCurrent();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000039F7 File Offset: 0x00001BF7
		internal static void CleanupRoots()
		{
			GUI.s_Skin = null;
			GUIUtility.CleanupRoots();
			GUILayoutUtility.CleanupRoots();
			GUISkin.CleanupRoots();
			GUIStyle.CleanupRoots();
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003A18 File Offset: 0x00001C18
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003A2F File Offset: 0x00001C2F
		public static Matrix4x4 matrix
		{
			get
			{
				return GUIClip.GetMatrix();
			}
			set
			{
				GUIClip.SetMatrix(value);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003A3C File Offset: 0x00001C3C
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003A65 File Offset: 0x00001C65
		public static string tooltip
		{
			get
			{
				string text = GUI.Internal_GetTooltip();
				bool flag = text != null;
				string result;
				if (flag)
				{
					result = text;
				}
				else
				{
					result = "";
				}
				return result;
			}
			set
			{
				GUI.Internal_SetTooltip(value);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003A6F File Offset: 0x00001C6F
		protected static string mouseTooltip
		{
			get
			{
				return GUI.Internal_GetMouseTooltip();
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003A78 File Offset: 0x00001C78
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003A8F File Offset: 0x00001C8F
		protected static Rect tooltipRect
		{
			get
			{
				return GUI.s_ToolTipRect;
			}
			set
			{
				GUI.s_ToolTipRect = value;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003A98 File Offset: 0x00001C98
		public static void Label(Rect position, string text)
		{
			GUI.Label(position, GUIContent.Temp(text), GUI.s_Skin.label);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003AB2 File Offset: 0x00001CB2
		public static void Label(Rect position, Texture image)
		{
			GUI.Label(position, GUIContent.Temp(image), GUI.s_Skin.label);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003ACC File Offset: 0x00001CCC
		public static void Label(Rect position, GUIContent content)
		{
			GUI.Label(position, content, GUI.s_Skin.label);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003AE1 File Offset: 0x00001CE1
		public static void Label(Rect position, string text, GUIStyle style)
		{
			GUI.Label(position, GUIContent.Temp(text), style);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003AF2 File Offset: 0x00001CF2
		public static void Label(Rect position, Texture image, GUIStyle style)
		{
			GUI.Label(position, GUIContent.Temp(image), style);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003B03 File Offset: 0x00001D03
		public static void Label(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			GUI.DoLabel(position, content, style);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003B15 File Offset: 0x00001D15
		public static void DrawTexture(Rect position, Texture image)
		{
			GUI.DrawTexture(position, image, ScaleMode.StretchToFill);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003B21 File Offset: 0x00001D21
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode)
		{
			GUI.DrawTexture(position, image, scaleMode, true);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003B2E File Offset: 0x00001D2E
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, 0f);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003B40 File Offset: 0x00001D40
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, GUI.color, 0f, 0f);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003B60 File Offset: 0x00001D60
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color color, float borderWidth, float borderRadius)
		{
			Vector4 borderWidths = Vector4.one * borderWidth;
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, color, borderWidths, borderRadius);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003B8C File Offset: 0x00001D8C
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color color, Vector4 borderWidths, float borderRadius)
		{
			Vector4 borderRadiuses = Vector4.one * borderRadius;
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, color, borderWidths, borderRadiuses);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003BB8 File Offset: 0x00001DB8
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color color, Vector4 borderWidths, Vector4 borderRadiuses)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, color, borderWidths, borderRadiuses, true);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003BDC File Offset: 0x00001DDC
		internal static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color color, Vector4 borderWidths, Vector4 borderRadiuses, bool drawSmoothCorners)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, color, color, color, color, borderWidths, borderRadiuses, drawSmoothCorners);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003C04 File Offset: 0x00001E04
		internal static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color leftColor, Color topColor, Color rightColor, Color bottomColor, Vector4 borderWidths, Vector4 borderRadiuses)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, leftColor, topColor, rightColor, bottomColor, borderWidths, borderRadiuses, true);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003C2C File Offset: 0x00001E2C
		internal static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color leftColor, Color topColor, Color rightColor, Color bottomColor, Vector4 borderWidths, Vector4 borderRadiuses, bool drawSmoothCorners)
		{
			GUIUtility.CheckOnGUI();
			bool flag = Event.current.type == EventType.Repaint;
			if (flag)
			{
				bool flag2 = image == null;
				if (flag2)
				{
					Debug.LogWarning("null texture passed to GUI.DrawTexture");
				}
				else
				{
					bool flag3 = imageAspect == 0f;
					if (flag3)
					{
						imageAspect = (float)image.width / (float)image.height;
					}
					bool flag4 = borderWidths != Vector4.zero;
					Material mat;
					if (flag4)
					{
						bool flag5 = leftColor != topColor || leftColor != rightColor || leftColor != bottomColor;
						if (flag5)
						{
							mat = GUI.roundedRectWithColorPerBorderMaterial;
						}
						else
						{
							mat = GUI.roundedRectMaterial;
						}
					}
					else
					{
						bool flag6 = borderRadiuses != Vector4.zero;
						if (flag6)
						{
							mat = GUI.roundedRectMaterial;
						}
						else
						{
							mat = (alphaBlend ? GUI.blendMaterial : GUI.blitMaterial);
						}
					}
					Internal_DrawTextureArguments internal_DrawTextureArguments = new Internal_DrawTextureArguments
					{
						leftBorder = 0,
						rightBorder = 0,
						topBorder = 0,
						bottomBorder = 0,
						color = leftColor,
						leftBorderColor = leftColor,
						topBorderColor = topColor,
						rightBorderColor = rightColor,
						bottomBorderColor = bottomColor,
						borderWidths = borderWidths,
						cornerRadiuses = borderRadiuses,
						texture = image,
						smoothCorners = drawSmoothCorners,
						mat = mat
					};
					GUI.CalculateScaledTextureRects(position, scaleMode, imageAspect, ref internal_DrawTextureArguments.screenRect, ref internal_DrawTextureArguments.sourceRect);
					Graphics.Internal_DrawTexture(ref internal_DrawTextureArguments);
				}
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003DB8 File Offset: 0x00001FB8
		internal static bool CalculateScaledTextureRects(Rect position, ScaleMode scaleMode, float imageAspect, ref Rect outScreenRect, ref Rect outSourceRect)
		{
			float num = position.width / position.height;
			bool result = false;
			switch (scaleMode)
			{
			case ScaleMode.StretchToFill:
				outScreenRect = position;
				outSourceRect = new Rect(0f, 0f, 1f, 1f);
				result = true;
				break;
			case ScaleMode.ScaleAndCrop:
			{
				bool flag = num > imageAspect;
				if (flag)
				{
					float num2 = imageAspect / num;
					outScreenRect = position;
					outSourceRect = new Rect(0f, (1f - num2) * 0.5f, 1f, num2);
					result = true;
				}
				else
				{
					float num3 = num / imageAspect;
					outScreenRect = position;
					outSourceRect = new Rect(0.5f - num3 * 0.5f, 0f, num3, 1f);
					result = true;
				}
				break;
			}
			case ScaleMode.ScaleToFit:
			{
				bool flag2 = num > imageAspect;
				if (flag2)
				{
					float num4 = imageAspect / num;
					outScreenRect = new Rect(position.xMin + position.width * (1f - num4) * 0.5f, position.yMin, num4 * position.width, position.height);
					outSourceRect = new Rect(0f, 0f, 1f, 1f);
					result = true;
				}
				else
				{
					float num5 = num / imageAspect;
					outScreenRect = new Rect(position.xMin, position.yMin + position.height * (1f - num5) * 0.5f, position.width, num5 * position.height);
					outSourceRect = new Rect(0f, 0f, 1f, 1f);
					result = true;
				}
				break;
			}
			}
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003F87 File Offset: 0x00002187
		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords)
		{
			GUI.DrawTextureWithTexCoords(position, image, texCoords, true);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003F94 File Offset: 0x00002194
		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords, bool alphaBlend)
		{
			GUIUtility.CheckOnGUI();
			bool flag = Event.current.type == EventType.Repaint;
			if (flag)
			{
				Material mat = alphaBlend ? GUI.blendMaterial : GUI.blitMaterial;
				Internal_DrawTextureArguments internal_DrawTextureArguments = default(Internal_DrawTextureArguments);
				internal_DrawTextureArguments.texture = image;
				internal_DrawTextureArguments.mat = mat;
				internal_DrawTextureArguments.leftBorder = 0;
				internal_DrawTextureArguments.rightBorder = 0;
				internal_DrawTextureArguments.topBorder = 0;
				internal_DrawTextureArguments.bottomBorder = 0;
				internal_DrawTextureArguments.color = GUI.color;
				internal_DrawTextureArguments.leftBorderColor = GUI.color;
				internal_DrawTextureArguments.topBorderColor = GUI.color;
				internal_DrawTextureArguments.rightBorderColor = GUI.color;
				internal_DrawTextureArguments.bottomBorderColor = GUI.color;
				internal_DrawTextureArguments.screenRect = position;
				internal_DrawTextureArguments.sourceRect = texCoords;
				Graphics.Internal_DrawTexture(ref internal_DrawTextureArguments);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000405A File Offset: 0x0000225A
		public static void Box(Rect position, string text)
		{
			GUI.Box(position, GUIContent.Temp(text), GUI.s_Skin.box);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004074 File Offset: 0x00002274
		public static void Box(Rect position, Texture image)
		{
			GUI.Box(position, GUIContent.Temp(image), GUI.s_Skin.box);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000408E File Offset: 0x0000228E
		public static void Box(Rect position, GUIContent content)
		{
			GUI.Box(position, content, GUI.s_Skin.box);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000040A3 File Offset: 0x000022A3
		public static void Box(Rect position, string text, GUIStyle style)
		{
			GUI.Box(position, GUIContent.Temp(text), style);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000040B4 File Offset: 0x000022B4
		public static void Box(Rect position, Texture image, GUIStyle style)
		{
			GUI.Box(position, GUIContent.Temp(image), style);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000040C8 File Offset: 0x000022C8
		public static void Box(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_BoxHash, FocusType.Passive);
			bool flag = Event.current.type == EventType.Repaint;
			if (flag)
			{
				style.Draw(position, content, controlID, false, position.Contains(Event.current.mousePosition));
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004118 File Offset: 0x00002318
		public static bool Button(Rect position, string text)
		{
			return GUI.Button(position, GUIContent.Temp(text), GUI.s_Skin.button);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004140 File Offset: 0x00002340
		public static bool Button(Rect position, Texture image)
		{
			return GUI.Button(position, GUIContent.Temp(image), GUI.s_Skin.button);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004168 File Offset: 0x00002368
		public static bool Button(Rect position, GUIContent content)
		{
			return GUI.Button(position, content, GUI.s_Skin.button);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000418C File Offset: 0x0000238C
		public static bool Button(Rect position, string text, GUIStyle style)
		{
			return GUI.Button(position, GUIContent.Temp(text), style);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000041AC File Offset: 0x000023AC
		public static bool Button(Rect position, Texture image, GUIStyle style)
		{
			return GUI.Button(position, GUIContent.Temp(image), style);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000041CC File Offset: 0x000023CC
		public static bool Button(Rect position, GUIContent content, GUIStyle style)
		{
			int controlID = GUIUtility.GetControlID(GUI.s_ButonHash, FocusType.Passive, position);
			return GUI.Button(position, controlID, content, style);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000041F4 File Offset: 0x000023F4
		internal static bool Button(Rect position, int id, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoButton(position, id, content, style);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004218 File Offset: 0x00002418
		public static bool RepeatButton(Rect position, string text)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(text), GUI.s_Skin.button, FocusType.Passive);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004244 File Offset: 0x00002444
		public static bool RepeatButton(Rect position, Texture image)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(image), GUI.s_Skin.button, FocusType.Passive);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004270 File Offset: 0x00002470
		public static bool RepeatButton(Rect position, GUIContent content)
		{
			return GUI.DoRepeatButton(position, content, GUI.s_Skin.button, FocusType.Passive);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004294 File Offset: 0x00002494
		public static bool RepeatButton(Rect position, string text, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(text), style, FocusType.Passive);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000042B4 File Offset: 0x000024B4
		public static bool RepeatButton(Rect position, Texture image, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(image), style, FocusType.Passive);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000042D4 File Offset: 0x000024D4
		public static bool RepeatButton(Rect position, GUIContent content, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, content, style, FocusType.Passive);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000042F0 File Offset: 0x000024F0
		private static bool DoRepeatButton(Rect position, GUIContent content, GUIStyle style, FocusType focusType)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_RepeatButtonHash, focusType, position);
			EventType typeForControl = Event.current.GetTypeForControl(controlID);
			EventType eventType = typeForControl;
			bool result;
			if (eventType != EventType.MouseDown)
			{
				if (eventType != EventType.MouseUp)
				{
					if (eventType != EventType.Repaint)
					{
						result = false;
					}
					else
					{
						style.Draw(position, content, controlID, false, position.Contains(Event.current.mousePosition));
						result = (controlID == GUIUtility.hotControl && position.Contains(Event.current.mousePosition));
					}
				}
				else
				{
					bool flag = GUIUtility.hotControl == controlID;
					if (flag)
					{
						GUIUtility.hotControl = 0;
						Event.current.Use();
						result = position.Contains(Event.current.mousePosition);
					}
					else
					{
						result = false;
					}
				}
			}
			else
			{
				bool flag2 = position.Contains(Event.current.mousePosition);
				if (flag2)
				{
					GUIUtility.hotControl = controlID;
					Event.current.Use();
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000043E0 File Offset: 0x000025E0
		public static string TextField(Rect position, string text)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, -1, GUI.skin.textField);
			return guicontent.text;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000441C File Offset: 0x0000261C
		public static string TextField(Rect position, string text, int maxLength)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, GUI.skin.textField);
			return guicontent.text;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004458 File Offset: 0x00002658
		public static string TextField(Rect position, string text, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, -1, style);
			return guicontent.text;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000448C File Offset: 0x0000268C
		public static string TextField(Rect position, string text, int maxLength, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, style);
			return guicontent.text;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000044C0 File Offset: 0x000026C0
		public static string PasswordField(Rect position, string password, char maskChar)
		{
			return GUI.PasswordField(position, password, maskChar, -1, GUI.skin.textField);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000044E8 File Offset: 0x000026E8
		public static string PasswordField(Rect position, string password, char maskChar, int maxLength)
		{
			return GUI.PasswordField(position, password, maskChar, maxLength, GUI.skin.textField);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004510 File Offset: 0x00002710
		public static string PasswordField(Rect position, string password, char maskChar, GUIStyle style)
		{
			return GUI.PasswordField(position, password, maskChar, -1, style);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000452C File Offset: 0x0000272C
		public static string PasswordField(Rect position, string password, char maskChar, int maxLength, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			string text = GUI.PasswordFieldGetStrToShow(password, maskChar);
			GUIContent guicontent = GUIContent.Temp(text);
			bool changed = GUI.changed;
			GUI.changed = false;
			bool flag = TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
			if (flag)
			{
				GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard), guicontent, false, maxLength, style, password, maskChar);
			}
			else
			{
				GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, style);
			}
			text = (GUI.changed ? guicontent.text : password);
			GUI.changed = (GUI.changed || changed);
			return text;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000045C0 File Offset: 0x000027C0
		internal static string PasswordFieldGetStrToShow(string password, char maskChar)
		{
			return (Event.current.type == EventType.Repaint || Event.current.type == EventType.MouseDown) ? "".PadRight(password.Length, maskChar) : password;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004600 File Offset: 0x00002800
		public static string TextArea(Rect position, string text)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, -1, GUI.skin.textArea);
			return guicontent.text;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000463C File Offset: 0x0000283C
		public static string TextArea(Rect position, string text, int maxLength)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, maxLength, GUI.skin.textArea);
			return guicontent.text;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004678 File Offset: 0x00002878
		public static string TextArea(Rect position, string text, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, -1, style);
			return guicontent.text;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000046AC File Offset: 0x000028AC
		public static string TextArea(Rect position, string text, int maxLength, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, maxLength, style);
			return guicontent.text;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000046DD File Offset: 0x000028DD
		internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style)
		{
			GUI.DoTextField(position, id, content, multiline, maxLength, style, null);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000046EF File Offset: 0x000028EF
		internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText)
		{
			GUI.DoTextField(position, id, content, multiline, maxLength, style, secureText, '\0');
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004704 File Offset: 0x00002904
		internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText, char maskChar)
		{
			GUIUtility.CheckOnGUI();
			bool flag = maxLength >= 0 && content.text.Length > maxLength;
			if (flag)
			{
				content.text = content.text.Substring(0, maxLength);
			}
			TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), id);
			textEditor.text = content.text;
			textEditor.SaveBackup();
			textEditor.position = position;
			textEditor.style = style;
			textEditor.multiline = multiline;
			textEditor.controlID = id;
			textEditor.DetectFocusChange();
			bool isRequiredToForceOpen = TouchScreenKeyboard.isRequiredToForceOpen;
			if (isRequiredToForceOpen)
			{
				GUI.HandleTextFieldEventForDesktopWithForcedKeyboard(position, id, content, multiline, maxLength, style, secureText, textEditor);
			}
			else
			{
				bool flag2 = TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
				if (flag2)
				{
					GUI.HandleTextFieldEventForTouchscreen(position, id, content, multiline, maxLength, style, secureText, maskChar, textEditor);
				}
				else
				{
					GUI.HandleTextFieldEventForDesktop(position, id, content, multiline, maxLength, style, textEditor);
				}
			}
			textEditor.UpdateScrollOffsetIfNeeded(Event.current);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004800 File Offset: 0x00002A00
		private static void HandleTextFieldEventForTouchscreen(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText, char maskChar, TextEditor editor)
		{
			Event current = Event.current;
			EventType type = current.type;
			EventType eventType = type;
			if (eventType != EventType.MouseDown)
			{
				if (eventType == EventType.Repaint)
				{
					bool flag = editor.keyboardOnScreen != null;
					if (flag)
					{
						content.text = editor.keyboardOnScreen.text;
						bool flag2 = maxLength >= 0 && content.text.Length > maxLength;
						if (flag2)
						{
							content.text = content.text.Substring(0, maxLength);
						}
						bool flag3 = editor.keyboardOnScreen.status > TouchScreenKeyboard.Status.Visible;
						if (flag3)
						{
							editor.keyboardOnScreen = null;
							GUI.changed = true;
						}
					}
					string text = content.text;
					bool flag4 = secureText != null;
					if (flag4)
					{
						content.text = GUI.PasswordFieldGetStrToShow(text, maskChar);
					}
					style.Draw(position, content, id, false);
					content.text = text;
				}
			}
			else
			{
				bool flag5 = position.Contains(current.mousePosition);
				if (flag5)
				{
					GUIUtility.hotControl = id;
					bool flag6 = GUI.s_HotTextField != -1 && GUI.s_HotTextField != id;
					if (flag6)
					{
						TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUI.s_HotTextField);
						textEditor.keyboardOnScreen = null;
					}
					GUI.s_HotTextField = id;
					bool flag7 = GUIUtility.keyboardControl != id;
					if (flag7)
					{
						GUIUtility.keyboardControl = id;
					}
					editor.keyboardOnScreen = TouchScreenKeyboard.Open(secureText ?? content.text, TouchScreenKeyboardType.Default, true, multiline, secureText != null);
					current.Use();
				}
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000498C File Offset: 0x00002B8C
		private static void HandleTextFieldEventForDesktop(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, TextEditor editor)
		{
			Event current = Event.current;
			bool flag = false;
			switch (current.type)
			{
			case EventType.MouseDown:
			{
				bool flag2 = position.Contains(current.mousePosition);
				if (flag2)
				{
					GUIUtility.hotControl = id;
					GUIUtility.keyboardControl = id;
					editor.m_HasFocus = true;
					editor.MoveCursorToPosition(Event.current.mousePosition);
					bool flag3 = Event.current.clickCount == 2 && GUI.skin.settings.doubleClickSelectsWord;
					if (flag3)
					{
						editor.SelectCurrentWord();
						editor.DblClickSnap(TextEditor.DblClickSnapping.WORDS);
						editor.MouseDragSelectsWholeWords(true);
					}
					bool flag4 = Event.current.clickCount == 3 && GUI.skin.settings.tripleClickSelectsLine;
					if (flag4)
					{
						editor.SelectCurrentParagraph();
						editor.MouseDragSelectsWholeWords(true);
						editor.DblClickSnap(TextEditor.DblClickSnapping.PARAGRAPHS);
					}
					current.Use();
				}
				break;
			}
			case EventType.MouseUp:
			{
				bool flag5 = GUIUtility.hotControl == id;
				if (flag5)
				{
					editor.MouseDragSelectsWholeWords(false);
					GUIUtility.hotControl = 0;
					current.Use();
				}
				break;
			}
			case EventType.MouseDrag:
			{
				bool flag6 = GUIUtility.hotControl == id;
				if (flag6)
				{
					bool shift = current.shift;
					if (shift)
					{
						editor.MoveCursorToPosition(Event.current.mousePosition);
					}
					else
					{
						editor.SelectToPosition(Event.current.mousePosition);
					}
					current.Use();
				}
				break;
			}
			case EventType.KeyDown:
			{
				bool flag7 = GUIUtility.keyboardControl != id;
				if (flag7)
				{
					return;
				}
				bool flag8 = editor.HandleKeyEvent(current);
				if (flag8)
				{
					current.Use();
					flag = true;
					content.text = editor.text;
				}
				else
				{
					bool flag9 = current.keyCode == KeyCode.Tab || current.character == '\t';
					if (flag9)
					{
						return;
					}
					char character = current.character;
					bool flag10 = character == '\n' && !multiline && !current.alt;
					if (flag10)
					{
						return;
					}
					Font font = style.font;
					bool flag11 = !font;
					if (flag11)
					{
						font = GUI.skin.font;
					}
					bool flag12 = font.HasCharacter(character) || character == '\n';
					if (flag12)
					{
						editor.Insert(character);
						flag = true;
					}
					else
					{
						bool flag13 = character == '\0';
						if (flag13)
						{
							bool flag14 = GUIUtility.compositionString.Length > 0;
							if (flag14)
							{
								editor.ReplaceSelection("");
								flag = true;
							}
							current.Use();
						}
					}
				}
				break;
			}
			case EventType.Repaint:
			{
				bool flag15 = GUIUtility.keyboardControl != id;
				if (flag15)
				{
					style.Draw(position, content, id, false);
				}
				else
				{
					editor.DrawCursor(content.text);
				}
				break;
			}
			}
			bool flag16 = GUIUtility.keyboardControl == id;
			if (flag16)
			{
				GUIUtility.textFieldInput = true;
			}
			bool flag17 = flag;
			if (flag17)
			{
				GUI.changed = true;
				content.text = editor.text;
				bool flag18 = maxLength >= 0 && content.text.Length > maxLength;
				if (flag18)
				{
					content.text = content.text.Substring(0, maxLength);
				}
				current.Use();
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004CCC File Offset: 0x00002ECC
		private static void HandleTextFieldEventForDesktopWithForcedKeyboard(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText, TextEditor editor)
		{
			bool flag = false;
			bool flag2 = Event.current.type == EventType.Repaint;
			if (flag2)
			{
				bool flag3 = GUI.s_HotTextField != -1 && GUI.s_HotTextField != id;
				if (flag3)
				{
					TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUI.s_HotTextField);
					textEditor.keyboardOnScreen.active = false;
					textEditor.keyboardOnScreen = null;
				}
				bool flag4 = editor.keyboardOnScreen != null;
				if (flag4)
				{
					bool flag5 = GUIUtility.keyboardControl != id || !Application.isFocused;
					if (flag5)
					{
						editor.keyboardOnScreen.active = false;
						editor.keyboardOnScreen = null;
					}
					else
					{
						bool flag6 = !editor.keyboardOnScreen.active;
						if (flag6)
						{
							flag = true;
						}
					}
				}
				else
				{
					bool flag7 = GUIUtility.keyboardControl == id && Application.isFocused;
					if (flag7)
					{
						flag = true;
					}
				}
			}
			bool flag8 = flag;
			if (flag8)
			{
				editor.keyboardOnScreen = TouchScreenKeyboard.Open(secureText ?? content.text, TouchScreenKeyboardType.Default, true, multiline, secureText != null);
			}
			GUI.HandleTextFieldEventForDesktop(position, id, content, multiline, maxLength, style, editor);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004DF0 File Offset: 0x00002FF0
		public static bool Toggle(Rect position, bool value, string text)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(text), GUI.s_Skin.toggle);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004E1C File Offset: 0x0000301C
		public static bool Toggle(Rect position, bool value, Texture image)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(image), GUI.s_Skin.toggle);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004E48 File Offset: 0x00003048
		public static bool Toggle(Rect position, bool value, GUIContent content)
		{
			return GUI.Toggle(position, value, content, GUI.s_Skin.toggle);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004E6C File Offset: 0x0000306C
		public static bool Toggle(Rect position, bool value, string text, GUIStyle style)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(text), style);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004E8C File Offset: 0x0000308C
		public static bool Toggle(Rect position, bool value, Texture image, GUIStyle style)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(image), style);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004EAC File Offset: 0x000030AC
		public static bool Toggle(Rect position, bool value, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoToggle(position, GUIUtility.GetControlID(GUI.s_ToggleHash, FocusType.Passive, position), value, content, style);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004EDC File Offset: 0x000030DC
		public static bool Toggle(Rect position, int id, bool value, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoToggle(position, id, value, content, style);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004F00 File Offset: 0x00003100
		public static int Toolbar(Rect position, int selected, string[] texts)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(texts), GUI.s_Skin.button);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004F2C File Offset: 0x0000312C
		public static int Toolbar(Rect position, int selected, Texture[] images)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(images), GUI.s_Skin.button);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004F58 File Offset: 0x00003158
		public static int Toolbar(Rect position, int selected, GUIContent[] contents)
		{
			return GUI.Toolbar(position, selected, contents, GUI.s_Skin.button);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004F7C File Offset: 0x0000317C
		public static int Toolbar(Rect position, int selected, string[] texts, GUIStyle style)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(texts), style);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004F9C File Offset: 0x0000319C
		public static int Toolbar(Rect position, int selected, Texture[] images, GUIStyle style)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(images), style);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004FBC File Offset: 0x000031BC
		public static int Toolbar(Rect position, int selected, GUIContent[] contents, GUIStyle style)
		{
			return GUI.Toolbar(position, selected, contents, null, style, GUI.ToolbarButtonSize.Fixed, null);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004FDC File Offset: 0x000031DC
		public static int Toolbar(Rect position, int selected, GUIContent[] contents, GUIStyle style, GUI.ToolbarButtonSize buttonSize)
		{
			return GUI.Toolbar(position, selected, contents, null, style, buttonSize, null);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004FFC File Offset: 0x000031FC
		internal static int Toolbar(Rect position, int selected, GUIContent[] contents, string[] controlNames, GUIStyle style, GUI.ToolbarButtonSize buttonSize, bool[] contentsEnabled = null)
		{
			GUIUtility.CheckOnGUI();
			GUIStyle firstStyle;
			GUIStyle midStyle;
			GUIStyle lastStyle;
			GUI.FindStyles(ref style, out firstStyle, out midStyle, out lastStyle, "left", "mid", "right");
			return GUI.DoButtonGrid(position, selected, contents, controlNames, contents.Length, style, firstStyle, midStyle, lastStyle, buttonSize, contentsEnabled);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005048 File Offset: 0x00003248
		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, null);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000506C File Offset: 0x0000326C
		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(images), xCount, null);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005090 File Offset: 0x00003290
		public static int SelectionGrid(Rect position, int selected, GUIContent[] content, int xCount)
		{
			return GUI.SelectionGrid(position, selected, content, xCount, null);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000050AC File Offset: 0x000032AC
		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount, GUIStyle style)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, style);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000050D0 File Offset: 0x000032D0
		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount, GUIStyle style)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(images), xCount, style);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000050F4 File Offset: 0x000032F4
		public static int SelectionGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style)
		{
			bool flag = style == null;
			if (flag)
			{
				style = GUI.s_Skin.button;
			}
			return GUI.DoButtonGrid(position, selected, contents, null, xCount, style, style, style, style, GUI.ToolbarButtonSize.Fixed, null);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005130 File Offset: 0x00003330
		internal static void FindStyles(ref GUIStyle style, out GUIStyle firstStyle, out GUIStyle midStyle, out GUIStyle lastStyle, string first, string mid, string last)
		{
			bool flag = style == null;
			if (flag)
			{
				style = GUI.skin.button;
			}
			string name = style.name;
			midStyle = (GUI.skin.FindStyle(name + mid) ?? style);
			firstStyle = (GUI.skin.FindStyle(name + first) ?? midStyle);
			lastStyle = (GUI.skin.FindStyle(name + last) ?? midStyle);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000051AC File Offset: 0x000033AC
		internal static int CalcTotalHorizSpacing(int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle)
		{
			bool flag = xCount < 2;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = xCount == 2;
				if (flag2)
				{
					result = Mathf.Max(firstStyle.margin.right, lastStyle.margin.left);
				}
				else
				{
					int num = Mathf.Max(midStyle.margin.left, midStyle.margin.right);
					result = Mathf.Max(firstStyle.margin.right, midStyle.margin.left) + Mathf.Max(midStyle.margin.right, lastStyle.margin.left) + num * (xCount - 3);
				}
			}
			return result;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005250 File Offset: 0x00003450
		internal static bool DoControl(Rect position, int id, bool on, bool hover, GUIContent content, GUIStyle style)
		{
			Event current = Event.current;
			switch (current.type)
			{
			case EventType.MouseDown:
			{
				bool flag = GUIUtility.HitTest(position, current);
				if (flag)
				{
					GUI.GrabMouseControl(id);
					current.Use();
				}
				break;
			}
			case EventType.MouseUp:
			{
				bool flag2 = GUI.HasMouseControl(id);
				if (flag2)
				{
					GUI.ReleaseMouseControl();
					current.Use();
					bool flag3 = GUIUtility.HitTest(position, current);
					if (flag3)
					{
						GUI.changed = true;
						return !on;
					}
				}
				break;
			}
			case EventType.MouseDrag:
			{
				bool flag4 = GUI.HasMouseControl(id);
				if (flag4)
				{
					current.Use();
				}
				break;
			}
			case EventType.KeyDown:
			{
				bool flag5 = current.alt || current.shift || current.command || current.control;
				bool flag6 = (current.keyCode == KeyCode.Space || current.keyCode == KeyCode.Return || current.keyCode == KeyCode.KeypadEnter) && !flag5 && GUIUtility.keyboardControl == id;
				if (flag6)
				{
					current.Use();
					GUI.changed = true;
					return !on;
				}
				break;
			}
			case EventType.Repaint:
				style.Draw(position, content, id, on, hover);
				break;
			}
			return on;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005394 File Offset: 0x00003594
		private static void DoLabel(Rect position, GUIContent content, GUIStyle style)
		{
			Event current = Event.current;
			bool flag = current.type != EventType.Repaint;
			if (!flag)
			{
				bool flag2 = position.Contains(current.mousePosition);
				style.Draw(position, content, flag2, false, false, false);
				bool flag3 = !string.IsNullOrEmpty(content.tooltip) && flag2 && GUIClip.visibleRect.Contains(current.mousePosition);
				if (flag3)
				{
					bool flag4 = !GUIStyle.IsTooltipActive(content.tooltip);
					if (flag4)
					{
						GUI.s_ToolTipRect = new Rect(current.mousePosition, Vector2.zero);
					}
					GUIStyle.SetMouseTooltip(content.tooltip, GUI.s_ToolTipRect);
				}
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005444 File Offset: 0x00003644
		internal static bool DoToggle(Rect position, int id, bool value, GUIContent content, GUIStyle style)
		{
			return GUI.DoControl(position, id, value, position.Contains(Event.current.mousePosition), content, style);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005474 File Offset: 0x00003674
		internal static bool DoButton(Rect position, int id, GUIContent content, GUIStyle style)
		{
			return GUI.DoControl(position, id, false, position.Contains(Event.current.mousePosition), content, style);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000054A4 File Offset: 0x000036A4
		private static Rect[] CalcGridRectsFixedWidthFixedMargin(Rect position, int itemCount, int itemsPerRow, float elemWidth, float elemHeight, float spacingHorizontal, float spacingVertical)
		{
			int num = 0;
			float x = position.xMin;
			float num2 = position.yMin;
			Rect[] array = new Rect[itemCount];
			for (int i = 0; i < itemCount; i++)
			{
				array[i] = new Rect(x, num2, elemWidth, elemHeight);
				array[i] = GUIUtility.AlignRectToDevice(array[i]);
				x = array[i].xMax + spacingHorizontal;
				bool flag = ++num >= itemsPerRow;
				if (flag)
				{
					num = 0;
					num2 += elemHeight + spacingVertical;
					x = position.xMin;
				}
			}
			return array;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005548 File Offset: 0x00003748
		internal static int DoCustomSelectionGrid(Rect position, int selected, int itemCount, GUI.CustomSelectionGridItemGUI itemGUI, int itemsPerRow, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			bool flag = itemCount == 0;
			int result;
			if (flag)
			{
				result = selected;
			}
			else
			{
				bool flag2 = itemsPerRow <= 0;
				if (flag2)
				{
					Debug.LogWarning("You are trying to create a SelectionGrid with zero or less elements to be displayed in the horizontal direction. Set itemsPerRow to a positive value.");
					result = selected;
				}
				else
				{
					int num = (itemCount + itemsPerRow - 1) / itemsPerRow;
					float spacingHorizontal = (float)Mathf.Max(style.margin.left, style.margin.right);
					float num2 = (float)Mathf.Max(style.margin.top, style.margin.bottom);
					float elemWidth = (style.fixedWidth != 0f) ? style.fixedWidth : ((position.width - (float)GUI.CalcTotalHorizSpacing(itemsPerRow, style, style, style, style)) / (float)itemsPerRow);
					float elemHeight = (style.fixedHeight != 0f) ? style.fixedHeight : ((position.height - num2 * (float)(num - 1)) / (float)num);
					Rect[] array = GUI.CalcGridRectsFixedWidthFixedMargin(position, itemCount, itemsPerRow, elemWidth, elemHeight, spacingHorizontal, num2);
					int controlID = 0;
					for (int i = 0; i < itemCount; i++)
					{
						Rect rect = array[i];
						int controlID2 = GUIUtility.GetControlID(GUI.s_ButtonGridHash, FocusType.Passive, rect);
						bool flag3 = i == selected;
						if (flag3)
						{
							controlID = controlID2;
						}
						EventType typeForControl = Event.current.GetTypeForControl(controlID2);
						EventType eventType = typeForControl;
						EventType eventType2 = eventType;
						switch (eventType2)
						{
						case EventType.MouseDown:
						{
							bool flag4 = GUIUtility.HitTest(rect, Event.current);
							if (flag4)
							{
								GUIUtility.hotControl = controlID2;
								Event.current.Use();
							}
							break;
						}
						case EventType.MouseUp:
						{
							bool flag5 = GUIUtility.hotControl == controlID2;
							if (flag5)
							{
								GUIUtility.hotControl = 0;
								Event.current.Use();
								GUI.changed = true;
								return i;
							}
							break;
						}
						case EventType.MouseMove:
							break;
						case EventType.MouseDrag:
						{
							bool flag6 = GUIUtility.hotControl == controlID2;
							if (flag6)
							{
								Event.current.Use();
							}
							break;
						}
						default:
							if (eventType2 == EventType.Repaint)
							{
								bool flag7 = selected != i;
								if (flag7)
								{
									itemGUI(i, rect, style, controlID2);
								}
							}
							break;
						}
						bool flag8 = typeForControl != EventType.Repaint || selected != i;
						if (flag8)
						{
							itemGUI(i, rect, style, controlID2);
						}
					}
					bool flag9 = selected >= 0 && selected < itemCount && Event.current.type == EventType.Repaint;
					if (flag9)
					{
						itemGUI(selected, array[selected], style, controlID);
					}
					result = selected;
				}
			}
			return result;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000057C0 File Offset: 0x000039C0
		private static int DoButtonGrid(Rect position, int selected, GUIContent[] contents, string[] controlNames, int itemsPerRow, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle, GUI.ToolbarButtonSize buttonSize, bool[] contentsEnabled = null)
		{
			GUIUtility.CheckOnGUI();
			int num = contents.Length;
			bool flag = num == 0;
			int result;
			if (flag)
			{
				result = selected;
			}
			else
			{
				bool flag2 = itemsPerRow <= 0;
				if (flag2)
				{
					Debug.LogWarning("You are trying to create a SelectionGrid with zero or less elements to be displayed in the horizontal direction. Set itemsPerRow to a positive value.");
					result = selected;
				}
				else
				{
					bool flag3 = contentsEnabled != null && contentsEnabled.Length != num;
					if (flag3)
					{
						throw new ArgumentException("contentsEnabled");
					}
					int num2 = (num + itemsPerRow - 1) / itemsPerRow;
					float elemWidth = (style.fixedWidth != 0f) ? style.fixedWidth : ((position.width - (float)GUI.CalcTotalHorizSpacing(itemsPerRow, style, firstStyle, midStyle, lastStyle)) / (float)itemsPerRow);
					float elemHeight = (style.fixedHeight != 0f) ? style.fixedHeight : ((position.height - (float)(Mathf.Max(style.margin.top, style.margin.bottom) * (num2 - 1))) / (float)num2);
					Rect[] array = GUI.CalcGridRects(position, contents, itemsPerRow, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, buttonSize);
					GUIStyle guistyle = null;
					int num3 = 0;
					for (int i = 0; i < num; i++)
					{
						bool enabled = GUI.enabled;
						GUI.enabled &= (contentsEnabled == null || contentsEnabled[i]);
						Rect rect = array[i];
						GUIContent guicontent = contents[i];
						bool flag4 = controlNames != null;
						if (flag4)
						{
							GUI.SetNextControlName(controlNames[i]);
						}
						int controlID = GUIUtility.GetControlID(GUI.s_ButtonGridHash, FocusType.Passive, rect);
						bool flag5 = i == selected;
						if (flag5)
						{
							num3 = controlID;
						}
						EventType typeForControl = Event.current.GetTypeForControl(controlID);
						EventType eventType = typeForControl;
						switch (eventType)
						{
						case EventType.MouseDown:
						{
							bool flag6 = GUIUtility.HitTest(rect, Event.current);
							if (flag6)
							{
								GUIUtility.hotControl = controlID;
								Event.current.Use();
							}
							break;
						}
						case EventType.MouseUp:
						{
							bool flag7 = GUIUtility.hotControl == controlID;
							if (flag7)
							{
								GUIUtility.hotControl = 0;
								Event.current.Use();
								GUI.changed = true;
								return i;
							}
							break;
						}
						case EventType.MouseMove:
							break;
						case EventType.MouseDrag:
						{
							bool flag8 = GUIUtility.hotControl == controlID;
							if (flag8)
							{
								Event.current.Use();
							}
							break;
						}
						default:
							if (eventType == EventType.Repaint)
							{
								GUIStyle guistyle2 = (num == 1) ? style : ((i == 0) ? firstStyle : ((i == num - 1) ? lastStyle : midStyle));
								bool flag9 = rect.Contains(Event.current.mousePosition);
								bool flag10 = GUIUtility.hotControl == controlID;
								bool flag11 = selected == i;
								bool flag12 = !flag11;
								if (flag12)
								{
									guistyle2.Draw(rect, guicontent, GUI.enabled && flag9 && (flag10 || GUIUtility.hotControl == 0), GUI.enabled && flag10, false, false);
								}
								else
								{
									guistyle = guistyle2;
								}
								bool flag13 = flag9;
								if (flag13)
								{
									GUIUtility.mouseUsed = true;
									bool flag14 = !string.IsNullOrEmpty(guicontent.tooltip);
									if (flag14)
									{
										GUIStyle.SetMouseTooltip(guicontent.tooltip, rect);
									}
								}
							}
							break;
						}
						GUI.enabled = enabled;
					}
					bool flag15 = guistyle != null;
					if (flag15)
					{
						Rect position2 = array[selected];
						GUIContent content = contents[selected];
						bool flag16 = position2.Contains(Event.current.mousePosition);
						bool flag17 = GUIUtility.hotControl == num3;
						bool enabled2 = GUI.enabled;
						GUI.enabled &= (contentsEnabled == null || contentsEnabled[selected]);
						guistyle.Draw(position2, content, GUI.enabled && flag16 && (flag17 || GUIUtility.hotControl == 0), GUI.enabled && flag17, true, false);
						GUI.enabled = enabled2;
					}
					result = selected;
				}
			}
			return result;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005B6C File Offset: 0x00003D6C
		private static Rect[] CalcGridRects(Rect position, GUIContent[] contents, int xCount, float elemWidth, float elemHeight, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle, GUI.ToolbarButtonSize buttonSize)
		{
			int num = contents.Length;
			int num2 = 0;
			float x = position.xMin;
			float num3 = position.yMin;
			GUIStyle guistyle = style;
			Rect[] array = new Rect[num];
			bool flag = num > 1;
			if (flag)
			{
				guistyle = firstStyle;
			}
			for (int i = 0; i < num; i++)
			{
				float width = 0f;
				if (buttonSize != GUI.ToolbarButtonSize.Fixed)
				{
					if (buttonSize == GUI.ToolbarButtonSize.FitToContents)
					{
						width = guistyle.CalcSize(contents[i]).x;
					}
				}
				else
				{
					width = elemWidth;
				}
				array[i] = new Rect(x, num3, width, elemHeight);
				array[i] = GUIUtility.AlignRectToDevice(array[i]);
				GUIStyle guistyle2 = midStyle;
				bool flag2 = i == num - 2 || i == xCount - 2;
				if (flag2)
				{
					guistyle2 = lastStyle;
				}
				x = array[i].xMax + (float)Mathf.Max(guistyle.margin.right, guistyle2.margin.left);
				num2++;
				bool flag3 = num2 >= xCount;
				if (flag3)
				{
					num2 = 0;
					num3 += elemHeight + (float)Mathf.Max(style.margin.top, style.margin.bottom);
					x = position.xMin;
					guistyle2 = firstStyle;
				}
				guistyle = guistyle2;
			}
			return array;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005CC8 File Offset: 0x00003EC8
		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue)
		{
			return GUI.Slider(position, value, 0f, leftValue, rightValue, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, true, 0, GUI.skin.horizontalSliderThumbExtent);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005D08 File Offset: 0x00003F08
		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb)
		{
			return GUI.Slider(position, value, 0f, leftValue, rightValue, slider, thumb, true, 0, null);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005D30 File Offset: 0x00003F30
		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUIStyle thumbExtent)
		{
			return GUI.Slider(position, value, 0f, leftValue, rightValue, slider, thumb, true, 0, (thumbExtent == null && thumb == GUI.skin.horizontalSliderThumb) ? GUI.skin.horizontalSliderThumbExtent : thumbExtent);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005D78 File Offset: 0x00003F78
		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue)
		{
			return GUI.Slider(position, value, 0f, topValue, bottomValue, GUI.skin.verticalSlider, GUI.skin.verticalSliderThumb, false, 0, GUI.skin.verticalSliderThumbExtent);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005DB8 File Offset: 0x00003FB8
		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue, GUIStyle slider, GUIStyle thumb)
		{
			return GUI.Slider(position, value, 0f, topValue, bottomValue, slider, thumb, false, 0, null);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005DE0 File Offset: 0x00003FE0
		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue, GUIStyle slider, GUIStyle thumb, GUIStyle thumbExtent)
		{
			return GUI.Slider(position, value, 0f, topValue, bottomValue, slider, thumb, false, 0, (thumbExtent == null && thumb == GUI.skin.verticalSliderThumb) ? GUI.skin.verticalSliderThumbExtent : thumbExtent);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005E28 File Offset: 0x00004028
		public static float Slider(Rect position, float value, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id, GUIStyle thumbExtent = null)
		{
			GUIUtility.CheckOnGUI();
			bool flag = id == 0;
			if (flag)
			{
				id = GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive, position);
			}
			return new SliderHandler(position, value, size, start, end, slider, thumb, horiz, id, thumbExtent).Handle();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005E78 File Offset: 0x00004078
		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue)
		{
			return GUI.Scroller(position, value, size, leftValue, rightValue, GUI.skin.horizontalScrollbar, GUI.skin.horizontalScrollbarThumb, GUI.skin.horizontalScrollbarLeftButton, GUI.skin.horizontalScrollbarRightButton, true);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005EC0 File Offset: 0x000040C0
		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle style)
		{
			return GUI.Scroller(position, value, size, leftValue, rightValue, style, GUI.skin.GetStyle(style.name + "thumb"), GUI.skin.GetStyle(style.name + "leftbutton"), GUI.skin.GetStyle(style.name + "rightbutton"), true);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005F34 File Offset: 0x00004134
		internal static bool ScrollerRepeatButton(int scrollerID, Rect rect, GUIStyle style)
		{
			bool result = false;
			bool flag = GUI.DoRepeatButton(rect, GUIContent.none, style, FocusType.Passive);
			if (flag)
			{
				bool flag2 = GUI.s_ScrollControlId != scrollerID;
				GUI.s_ScrollControlId = scrollerID;
				bool flag3 = flag2;
				if (flag3)
				{
					result = true;
					GUI.nextScrollStepTime = DateTime.Now.AddMilliseconds(250.0);
				}
				else
				{
					bool flag4 = DateTime.Now >= GUI.nextScrollStepTime;
					if (flag4)
					{
						result = true;
						GUI.nextScrollStepTime = DateTime.Now.AddMilliseconds(30.0);
					}
				}
				bool flag5 = Event.current.type == EventType.Repaint;
				if (flag5)
				{
					GUI.InternalRepaintEditorWindow();
				}
			}
			return result;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005FEC File Offset: 0x000041EC
		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue)
		{
			return GUI.Scroller(position, value, size, topValue, bottomValue, GUI.skin.verticalScrollbar, GUI.skin.verticalScrollbarThumb, GUI.skin.verticalScrollbarUpButton, GUI.skin.verticalScrollbarDownButton, false);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006034 File Offset: 0x00004234
		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue, GUIStyle style)
		{
			return GUI.Scroller(position, value, size, topValue, bottomValue, style, GUI.skin.GetStyle(style.name + "thumb"), GUI.skin.GetStyle(style.name + "upbutton"), GUI.skin.GetStyle(style.name + "downbutton"), false);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000060A8 File Offset: 0x000042A8
		internal static float Scroller(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUIStyle leftButton, GUIStyle rightButton, bool horiz)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive, position);
			Rect position2;
			Rect rect;
			Rect rect2;
			if (horiz)
			{
				position2 = new Rect(position.x + leftButton.fixedWidth, position.y, position.width - leftButton.fixedWidth - rightButton.fixedWidth, position.height);
				rect = new Rect(position.x, position.y, leftButton.fixedWidth, position.height);
				rect2 = new Rect(position.xMax - rightButton.fixedWidth, position.y, rightButton.fixedWidth, position.height);
			}
			else
			{
				position2 = new Rect(position.x, position.y + leftButton.fixedHeight, position.width, position.height - leftButton.fixedHeight - rightButton.fixedHeight);
				rect = new Rect(position.x, position.y, position.width, leftButton.fixedHeight);
				rect2 = new Rect(position.x, position.yMax - rightButton.fixedHeight, position.width, rightButton.fixedHeight);
			}
			value = GUI.Slider(position2, value, size, leftValue, rightValue, slider, thumb, horiz, controlID, null);
			bool flag = Event.current.type == EventType.MouseUp;
			bool flag2 = GUI.ScrollerRepeatButton(controlID, rect, leftButton);
			if (flag2)
			{
				value -= 10f * ((leftValue < rightValue) ? 1f : -1f);
			}
			bool flag3 = GUI.ScrollerRepeatButton(controlID, rect2, rightButton);
			if (flag3)
			{
				value += 10f * ((leftValue < rightValue) ? 1f : -1f);
			}
			bool flag4 = flag && Event.current.type == EventType.Used;
			if (flag4)
			{
				GUI.s_ScrollControlId = 0;
			}
			bool flag5 = leftValue < rightValue;
			if (flag5)
			{
				value = Mathf.Clamp(value, leftValue, rightValue - size);
			}
			else
			{
				value = Mathf.Clamp(value, rightValue, leftValue - size);
			}
			return value;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000062B6 File Offset: 0x000044B6
		public static void BeginClip(Rect position, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
		{
			GUIUtility.CheckOnGUI();
			GUIClip.Push(position, scrollOffset, renderOffset, resetOffset);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000062C9 File Offset: 0x000044C9
		public static void BeginGroup(Rect position)
		{
			GUI.BeginGroup(position, GUIContent.none, GUIStyle.none);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000062DD File Offset: 0x000044DD
		public static void BeginGroup(Rect position, string text)
		{
			GUI.BeginGroup(position, GUIContent.Temp(text), GUIStyle.none);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000062F2 File Offset: 0x000044F2
		public static void BeginGroup(Rect position, Texture image)
		{
			GUI.BeginGroup(position, GUIContent.Temp(image), GUIStyle.none);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00006307 File Offset: 0x00004507
		public static void BeginGroup(Rect position, GUIContent content)
		{
			GUI.BeginGroup(position, content, GUIStyle.none);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006317 File Offset: 0x00004517
		public static void BeginGroup(Rect position, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.none, style);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006327 File Offset: 0x00004527
		public static void BeginGroup(Rect position, string text, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.Temp(text), style);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006338 File Offset: 0x00004538
		public static void BeginGroup(Rect position, Texture image, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.Temp(image), style);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006349 File Offset: 0x00004549
		public static void BeginGroup(Rect position, GUIContent content, GUIStyle style)
		{
			GUI.BeginGroup(position, content, style, Vector2.zero);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000635C File Offset: 0x0000455C
		internal static void BeginGroup(Rect position, GUIContent content, GUIStyle style, Vector2 scrollOffset)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_BeginGroupHash, FocusType.Passive);
			bool flag = content != GUIContent.none || style != GUIStyle.none;
			if (flag)
			{
				EventType type = Event.current.type;
				EventType eventType = type;
				if (eventType != EventType.Repaint)
				{
					bool flag2 = position.Contains(Event.current.mousePosition);
					if (flag2)
					{
						GUIUtility.mouseUsed = true;
					}
				}
				else
				{
					style.Draw(position, content, controlID);
				}
			}
			GUIClip.Push(position, scrollOffset, Vector2.zero, false);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000063E5 File Offset: 0x000045E5
		public static void EndGroup()
		{
			GUIUtility.CheckOnGUI();
			GUIClip.Internal_Pop();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000063F4 File Offset: 0x000045F4
		public static void BeginClip(Rect position)
		{
			GUIUtility.CheckOnGUI();
			GUIClip.Push(position, Vector2.zero, Vector2.zero, false);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000640F File Offset: 0x0000460F
		public static void EndClip()
		{
			GUIUtility.CheckOnGUI();
			GUIClip.Pop();
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000641E File Offset: 0x0000461E
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00006425 File Offset: 0x00004625
		internal static GenericStack scrollViewStates
		{
			[CompilerGenerated]
			get
			{
				return GUI.<scrollViewStates>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				GUI.<scrollViewStates>k__BackingField = value;
			}
		} = new GenericStack();

		// Token: 0x060000FF RID: 255 RVA: 0x00006430 File Offset: 0x00004630
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, false, false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000646C File Offset: 0x0000466C
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000064A8 File Offset: 0x000046A8
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, false, false, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000064D4 File Offset: 0x000046D4
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006500 File Offset: 0x00004700
		protected static Vector2 DoBeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006524 File Offset: 0x00004724
		internal static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_ScrollviewHash, FocusType.Passive);
			ScrollViewState scrollViewState = (ScrollViewState)GUIUtility.GetStateObject(typeof(ScrollViewState), controlID);
			bool apply = scrollViewState.apply;
			if (apply)
			{
				scrollPosition = scrollViewState.scrollPosition;
				scrollViewState.apply = false;
			}
			scrollViewState.position = position;
			scrollViewState.scrollPosition = scrollPosition;
			scrollViewState.visibleRect = (scrollViewState.viewRect = viewRect);
			scrollViewState.visibleRect.width = position.width;
			scrollViewState.visibleRect.height = position.height;
			GUI.scrollViewStates.Push(scrollViewState);
			Rect screenRect = new Rect(position);
			EventType type = Event.current.type;
			EventType eventType = type;
			if (eventType != EventType.Layout)
			{
				if (eventType != EventType.Used)
				{
					bool flag = alwaysShowVertical;
					bool flag2 = alwaysShowHorizontal;
					bool flag3 = flag2 || viewRect.width > screenRect.width;
					if (flag3)
					{
						scrollViewState.visibleRect.height = position.height - horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
						screenRect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
						flag2 = true;
					}
					bool flag4 = flag || viewRect.height > screenRect.height;
					if (flag4)
					{
						scrollViewState.visibleRect.width = position.width - verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
						screenRect.width -= verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
						flag = true;
						bool flag5 = !flag2 && viewRect.width > screenRect.width;
						if (flag5)
						{
							scrollViewState.visibleRect.height = position.height - horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
							screenRect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
							flag2 = true;
						}
					}
					bool flag6 = Event.current.type == EventType.Repaint && background != GUIStyle.none;
					if (flag6)
					{
						background.Draw(position, position.Contains(Event.current.mousePosition), false, flag2 && flag, false);
					}
					bool flag7 = flag2 && horizontalScrollbar != GUIStyle.none;
					if (flag7)
					{
						scrollPosition.x = GUI.HorizontalScrollbar(new Rect(position.x, position.yMax - horizontalScrollbar.fixedHeight, screenRect.width, horizontalScrollbar.fixedHeight), scrollPosition.x, Mathf.Min(screenRect.width, viewRect.width), 0f, viewRect.width, horizontalScrollbar);
					}
					else
					{
						GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
						scrollPosition.x = ((horizontalScrollbar != GUIStyle.none) ? 0f : Mathf.Clamp(scrollPosition.x, 0f, Mathf.Max(viewRect.width - position.width, 0f)));
					}
					bool flag8 = flag && verticalScrollbar != GUIStyle.none;
					if (flag8)
					{
						scrollPosition.y = GUI.VerticalScrollbar(new Rect(screenRect.xMax + (float)verticalScrollbar.margin.left, screenRect.y, verticalScrollbar.fixedWidth, screenRect.height), scrollPosition.y, Mathf.Min(screenRect.height, viewRect.height), 0f, viewRect.height, verticalScrollbar);
					}
					else
					{
						GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
						scrollPosition.y = ((verticalScrollbar != GUIStyle.none) ? 0f : Mathf.Clamp(scrollPosition.y, 0f, Mathf.Max(viewRect.height - position.height, 0f)));
					}
				}
			}
			else
			{
				GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
			}
			GUIClip.Push(screenRect, new Vector2(Mathf.Round(-scrollPosition.x - viewRect.x), Mathf.Round(-scrollPosition.y - viewRect.y)), Vector2.zero, false);
			return scrollPosition;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000069E3 File Offset: 0x00004BE3
		public static void EndScrollView()
		{
			GUI.EndScrollView(true);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000069F0 File Offset: 0x00004BF0
		public static void EndScrollView(bool handleScrollWheel)
		{
			GUIUtility.CheckOnGUI();
			bool flag = GUI.scrollViewStates.Count == 0;
			if (!flag)
			{
				ScrollViewState scrollViewState = (ScrollViewState)GUI.scrollViewStates.Peek();
				GUIClip.Pop();
				GUI.scrollViewStates.Pop();
				bool flag2 = false;
				float num = Time.realtimeSinceStartup - scrollViewState.previousTimeSinceStartup;
				scrollViewState.previousTimeSinceStartup = Time.realtimeSinceStartup;
				bool flag3 = Event.current.type == EventType.Repaint && scrollViewState.velocity != Vector2.zero;
				if (flag3)
				{
					for (int i = 0; i < 2; i++)
					{
						ref Vector2 ptr = ref scrollViewState.velocity;
						int index = i;
						ptr[index] *= Mathf.Pow(0.1f, num);
						float num2 = 0.1f / num;
						bool flag4 = Mathf.Abs(scrollViewState.velocity[i]) < num2;
						if (flag4)
						{
							scrollViewState.velocity[i] = 0f;
						}
						else
						{
							ptr = ref scrollViewState.velocity;
							index = i;
							ptr[index] += ((scrollViewState.velocity[i] < 0f) ? num2 : (-num2));
							ptr = ref scrollViewState.scrollPosition;
							index = i;
							ptr[index] += scrollViewState.velocity[i] * num;
							flag2 = true;
							scrollViewState.touchScrollStartMousePosition = Event.current.mousePosition;
							scrollViewState.touchScrollStartPosition = scrollViewState.scrollPosition;
						}
					}
					bool flag5 = scrollViewState.velocity != Vector2.zero;
					if (flag5)
					{
						GUI.InternalRepaintEditorWindow();
					}
				}
				bool flag6 = handleScrollWheel && (Event.current.type == EventType.ScrollWheel || Event.current.type == EventType.TouchDown || Event.current.type == EventType.TouchUp || Event.current.type == EventType.TouchMove);
				if (flag6)
				{
					bool flag7 = Event.current.type == EventType.ScrollWheel && scrollViewState.position.Contains(Event.current.mousePosition);
					if (flag7)
					{
						scrollViewState.scrollPosition.x = Mathf.Clamp(scrollViewState.scrollPosition.x + Event.current.delta.x * 20f, 0f, scrollViewState.viewRect.width - scrollViewState.visibleRect.width);
						scrollViewState.scrollPosition.y = Mathf.Clamp(scrollViewState.scrollPosition.y + Event.current.delta.y * 20f, 0f, scrollViewState.viewRect.height - scrollViewState.visibleRect.height);
						Event.current.Use();
						flag2 = true;
					}
					else
					{
						bool flag8 = Event.current.type == EventType.TouchDown && (Event.current.modifiers & EventModifiers.Alt) == EventModifiers.Alt && scrollViewState.position.Contains(Event.current.mousePosition);
						if (flag8)
						{
							scrollViewState.isDuringTouchScroll = true;
							scrollViewState.touchScrollStartMousePosition = Event.current.mousePosition;
							scrollViewState.touchScrollStartPosition = scrollViewState.scrollPosition;
							GUIUtility.hotControl = GUIUtility.GetControlID(GUI.s_ScrollviewHash, FocusType.Passive, scrollViewState.position);
							Event.current.Use();
						}
						else
						{
							bool flag9 = scrollViewState.isDuringTouchScroll && Event.current.type == EventType.TouchUp;
							if (flag9)
							{
								scrollViewState.isDuringTouchScroll = false;
							}
							else
							{
								bool flag10 = scrollViewState.isDuringTouchScroll && Event.current.type == EventType.TouchMove;
								if (flag10)
								{
									Vector2 scrollPosition = scrollViewState.scrollPosition;
									scrollViewState.scrollPosition.x = Mathf.Clamp(scrollViewState.touchScrollStartPosition.x - (Event.current.mousePosition.x - scrollViewState.touchScrollStartMousePosition.x), 0f, scrollViewState.viewRect.width - scrollViewState.visibleRect.width);
									scrollViewState.scrollPosition.y = Mathf.Clamp(scrollViewState.touchScrollStartPosition.y - (Event.current.mousePosition.y - scrollViewState.touchScrollStartMousePosition.y), 0f, scrollViewState.viewRect.height - scrollViewState.visibleRect.height);
									Event.current.Use();
									Vector2 b = (scrollViewState.scrollPosition - scrollPosition) / num;
									scrollViewState.velocity = Vector2.Lerp(scrollViewState.velocity, b, num * 10f);
									flag2 = true;
								}
							}
						}
					}
				}
				bool flag11 = flag2;
				if (flag11)
				{
					bool flag12 = scrollViewState.scrollPosition.x < 0f;
					if (flag12)
					{
						scrollViewState.scrollPosition.x = 0f;
					}
					bool flag13 = scrollViewState.scrollPosition.y < 0f;
					if (flag13)
					{
						scrollViewState.scrollPosition.y = 0f;
					}
					scrollViewState.apply = true;
				}
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00006EF4 File Offset: 0x000050F4
		internal static ScrollViewState GetTopScrollView()
		{
			bool flag = GUI.scrollViewStates.Count != 0;
			ScrollViewState result;
			if (flag)
			{
				result = (ScrollViewState)GUI.scrollViewStates.Peek();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006F2C File Offset: 0x0000512C
		public static void ScrollTo(Rect position)
		{
			ScrollViewState topScrollView = GUI.GetTopScrollView();
			if (topScrollView != null)
			{
				topScrollView.ScrollTo(position);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006F50 File Offset: 0x00005150
		public static bool ScrollTowards(Rect position, float maxDelta)
		{
			ScrollViewState topScrollView = GUI.GetTopScrollView();
			bool flag = topScrollView == null;
			return !flag && topScrollView.ScrollTowards(position, maxDelta);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006F7C File Offset: 0x0000517C
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, string text)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(text), GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006FB4 File Offset: 0x000051B4
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, Texture image)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(image), GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006FEC File Offset: 0x000051EC
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, content, GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007020 File Offset: 0x00005220
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, string text, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(text), style, GUI.skin, true);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00007050 File Offset: 0x00005250
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, Texture image, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(image), style, GUI.skin, true);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00007080 File Offset: 0x00005280
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, title, style, GUI.skin, true);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000070AC File Offset: 0x000052AC
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, string text)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(text), GUI.skin.window, GUI.skin);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000070E4 File Offset: 0x000052E4
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, Texture image)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(image), GUI.skin.window, GUI.skin);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000711C File Offset: 0x0000531C
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, content, GUI.skin.window, GUI.skin);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000714C File Offset: 0x0000534C
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, string text, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(text), style, GUI.skin);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000717C File Offset: 0x0000537C
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, Texture image, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(image), style, GUI.skin);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000071AC File Offset: 0x000053AC
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, content, style, GUI.skin);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000071D4 File Offset: 0x000053D4
		private static Rect DoWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style, GUISkin skin, bool forceRectOnLayout)
		{
			return GUI.Internal_DoWindow(id, GUIUtility.s_OriginalID, clientRect, func, title, style, skin, forceRectOnLayout);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000071FC File Offset: 0x000053FC
		private static Rect DoModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, GUISkin skin)
		{
			return GUI.Internal_DoModalWindow(id, GUIUtility.s_OriginalID, clientRect, func, content, style, skin);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00007220 File Offset: 0x00005420
		[RequiredByNativeCode]
		internal static void CallWindowDelegate(GUI.WindowFunction func, int id, int instanceID, GUISkin _skin, int forceRect, float width, float height, GUIStyle style)
		{
			GUILayoutUtility.SelectIDList(id, true);
			GUISkin skin = GUI.skin;
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				bool flag2 = forceRect != 0;
				if (flag2)
				{
					GUILayoutOption[] options = new GUILayoutOption[]
					{
						GUILayout.Width(width),
						GUILayout.Height(height)
					};
					GUILayoutUtility.BeginWindow(id, style, options);
				}
				else
				{
					GUILayoutUtility.BeginWindow(id, style, null);
				}
			}
			else
			{
				GUILayoutUtility.BeginWindow(id, GUIStyle.none, null);
			}
			GUI.skin = _skin;
			if (func != null)
			{
				func(id);
			}
			bool flag3 = Event.current.type == EventType.Layout;
			if (flag3)
			{
				GUILayoutUtility.Layout();
			}
			GUI.skin = skin;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000072D4 File Offset: 0x000054D4
		public static void DragWindow()
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 10000f));
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000072F8 File Offset: 0x000054F8
		internal static void BeginWindows(int skinMode, int editorWindowInstanceID)
		{
			GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
			GenericStack layoutGroups = GUILayoutUtility.current.layoutGroups;
			GUILayoutGroup windows = GUILayoutUtility.current.windows;
			Matrix4x4 matrix = GUI.matrix;
			GUI.Internal_BeginWindows();
			GUI.matrix = matrix;
			GUILayoutUtility.current.topLevel = topLevel;
			GUILayoutUtility.current.layoutGroups = layoutGroups;
			GUILayoutUtility.current.windows = windows;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000735C File Offset: 0x0000555C
		internal static void EndWindows()
		{
			GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
			GenericStack layoutGroups = GUILayoutUtility.current.layoutGroups;
			GUILayoutGroup windows = GUILayoutUtility.current.windows;
			GUI.Internal_EndWindows();
			GUILayoutUtility.current.topLevel = topLevel;
			GUILayoutUtility.current.layoutGroups = layoutGroups;
			GUILayoutUtility.current.windows = windows;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000073B2 File Offset: 0x000055B2
		public GUI()
		{
		}

		// Token: 0x0600011D RID: 285
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_color_Injected(out Color ret);

		// Token: 0x0600011E RID: 286
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_color_Injected(ref Color value);

		// Token: 0x0600011F RID: 287
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_backgroundColor_Injected(out Color ret);

		// Token: 0x06000120 RID: 288
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_backgroundColor_Injected(ref Color value);

		// Token: 0x06000121 RID: 289
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_contentColor_Injected(out Color ret);

		// Token: 0x06000122 RID: 290
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_contentColor_Injected(ref Color value);

		// Token: 0x06000123 RID: 291
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DoModalWindow_Injected(int id, int instanceID, ref Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, object skin, out Rect ret);

		// Token: 0x06000124 RID: 292
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DoWindow_Injected(int id, int instanceID, ref Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style, object skin, bool forceRectOnLayout, out Rect ret);

		// Token: 0x06000125 RID: 293
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DragWindow_Injected(ref Rect position);

		// Token: 0x04000052 RID: 82
		private const float s_ScrollStepSize = 10f;

		// Token: 0x04000053 RID: 83
		private static int s_ScrollControlId;

		// Token: 0x04000054 RID: 84
		private static int s_HotTextField = -1;

		// Token: 0x04000055 RID: 85
		private static readonly int s_BoxHash = "Box".GetHashCode();

		// Token: 0x04000056 RID: 86
		private static readonly int s_ButonHash = "Button".GetHashCode();

		// Token: 0x04000057 RID: 87
		private static readonly int s_RepeatButtonHash = "repeatButton".GetHashCode();

		// Token: 0x04000058 RID: 88
		private static readonly int s_ToggleHash = "Toggle".GetHashCode();

		// Token: 0x04000059 RID: 89
		private static readonly int s_ButtonGridHash = "ButtonGrid".GetHashCode();

		// Token: 0x0400005A RID: 90
		private static readonly int s_SliderHash = "Slider".GetHashCode();

		// Token: 0x0400005B RID: 91
		private static readonly int s_BeginGroupHash = "BeginGroup".GetHashCode();

		// Token: 0x0400005C RID: 92
		private static readonly int s_ScrollviewHash = "scrollView".GetHashCode();

		// Token: 0x0400005D RID: 93
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static int <scrollTroughSide>k__BackingField;

		// Token: 0x0400005E RID: 94
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static DateTime <nextScrollStepTime>k__BackingField;

		// Token: 0x0400005F RID: 95
		private static GUISkin s_Skin;

		// Token: 0x04000060 RID: 96
		internal static Rect s_ToolTipRect;

		// Token: 0x04000061 RID: 97
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static GenericStack <scrollViewStates>k__BackingField;

		// Token: 0x0200000B RID: 11
		public enum ToolbarButtonSize
		{
			// Token: 0x04000063 RID: 99
			Fixed,
			// Token: 0x04000064 RID: 100
			FitToContents
		}

		// Token: 0x0200000C RID: 12
		// (Invoke) Token: 0x06000127 RID: 295
		internal delegate void CustomSelectionGridItemGUI(int item, Rect rect, GUIStyle style, int controlID);

		// Token: 0x0200000D RID: 13
		// (Invoke) Token: 0x0600012B RID: 299
		public delegate void WindowFunction(int id);

		// Token: 0x0200000E RID: 14
		public abstract class Scope : IDisposable
		{
			// Token: 0x0600012E RID: 302 RVA: 0x000073BC File Offset: 0x000055BC
			internal virtual void Dispose(bool disposing)
			{
				bool disposed = this.m_Disposed;
				if (!disposed)
				{
					bool flag = disposing && !GUIUtility.guiIsExiting;
					if (flag)
					{
						this.CloseScope();
					}
					this.m_Disposed = true;
				}
			}

			// Token: 0x0600012F RID: 303 RVA: 0x000073F8 File Offset: 0x000055F8
			protected override void Finalize()
			{
				try
				{
					bool flag = !this.m_Disposed && !GUIUtility.guiIsExiting;
					if (flag)
					{
						Console.WriteLine(base.GetType().Name + " was not disposed! You should use the 'using' keyword or manually call Dispose.");
					}
					this.Dispose(false);
				}
				finally
				{
					base.Finalize();
				}
			}

			// Token: 0x06000130 RID: 304 RVA: 0x0000745C File Offset: 0x0000565C
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06000131 RID: 305
			protected abstract void CloseScope();

			// Token: 0x06000132 RID: 306 RVA: 0x000073B2 File Offset: 0x000055B2
			protected Scope()
			{
			}

			// Token: 0x04000065 RID: 101
			private bool m_Disposed;
		}

		// Token: 0x0200000F RID: 15
		public class GroupScope : GUI.Scope
		{
			// Token: 0x06000133 RID: 307 RVA: 0x0000746E File Offset: 0x0000566E
			public GroupScope(Rect position)
			{
				GUI.BeginGroup(position);
			}

			// Token: 0x06000134 RID: 308 RVA: 0x0000747F File Offset: 0x0000567F
			public GroupScope(Rect position, string text)
			{
				GUI.BeginGroup(position, text);
			}

			// Token: 0x06000135 RID: 309 RVA: 0x00007491 File Offset: 0x00005691
			public GroupScope(Rect position, Texture image)
			{
				GUI.BeginGroup(position, image);
			}

			// Token: 0x06000136 RID: 310 RVA: 0x000074A3 File Offset: 0x000056A3
			public GroupScope(Rect position, GUIContent content)
			{
				GUI.BeginGroup(position, content);
			}

			// Token: 0x06000137 RID: 311 RVA: 0x000074B5 File Offset: 0x000056B5
			public GroupScope(Rect position, GUIStyle style)
			{
				GUI.BeginGroup(position, style);
			}

			// Token: 0x06000138 RID: 312 RVA: 0x000074C7 File Offset: 0x000056C7
			public GroupScope(Rect position, string text, GUIStyle style)
			{
				GUI.BeginGroup(position, text, style);
			}

			// Token: 0x06000139 RID: 313 RVA: 0x000074DA File Offset: 0x000056DA
			public GroupScope(Rect position, Texture image, GUIStyle style)
			{
				GUI.BeginGroup(position, image, style);
			}

			// Token: 0x0600013A RID: 314 RVA: 0x000074ED File Offset: 0x000056ED
			protected override void CloseScope()
			{
				GUI.EndGroup();
			}
		}

		// Token: 0x02000010 RID: 16
		public class ScrollViewScope : GUI.Scope
		{
			// Token: 0x17000032 RID: 50
			// (get) Token: 0x0600013B RID: 315 RVA: 0x000074F6 File Offset: 0x000056F6
			// (set) Token: 0x0600013C RID: 316 RVA: 0x000074FE File Offset: 0x000056FE
			public Vector2 scrollPosition
			{
				[CompilerGenerated]
				get
				{
					return this.<scrollPosition>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<scrollPosition>k__BackingField = value;
				}
			}

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x0600013D RID: 317 RVA: 0x00007507 File Offset: 0x00005707
			// (set) Token: 0x0600013E RID: 318 RVA: 0x0000750F File Offset: 0x0000570F
			public bool handleScrollWheel
			{
				[CompilerGenerated]
				get
				{
					return this.<handleScrollWheel>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<handleScrollWheel>k__BackingField = value;
				}
			}

			// Token: 0x0600013F RID: 319 RVA: 0x00007518 File Offset: 0x00005718
			public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect);
			}

			// Token: 0x06000140 RID: 320 RVA: 0x00007539 File Offset: 0x00005739
			public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical);
			}

			// Token: 0x06000141 RID: 321 RVA: 0x0000755E File Offset: 0x0000575E
			public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect, horizontalScrollbar, verticalScrollbar);
			}

			// Token: 0x06000142 RID: 322 RVA: 0x00007583 File Offset: 0x00005783
			public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar);
			}

			// Token: 0x06000143 RID: 323 RVA: 0x000075AC File Offset: 0x000057AC
			internal ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
			}

			// Token: 0x06000144 RID: 324 RVA: 0x000075E2 File Offset: 0x000057E2
			protected override void CloseScope()
			{
				GUI.EndScrollView(this.handleScrollWheel);
			}

			// Token: 0x04000066 RID: 102
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private Vector2 <scrollPosition>k__BackingField;

			// Token: 0x04000067 RID: 103
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private bool <handleScrollWheel>k__BackingField;
		}

		// Token: 0x02000011 RID: 17
		public class ClipScope : GUI.Scope
		{
			// Token: 0x06000145 RID: 325 RVA: 0x000075F1 File Offset: 0x000057F1
			public ClipScope(Rect position)
			{
				GUI.BeginClip(position);
			}

			// Token: 0x06000146 RID: 326 RVA: 0x00007604 File Offset: 0x00005804
			internal ClipScope(Rect position, Vector2 scrollOffset)
			{
				GUI.BeginClip(position, scrollOffset, default(Vector2), false);
			}

			// Token: 0x06000147 RID: 327 RVA: 0x0000762B File Offset: 0x0000582B
			protected override void CloseScope()
			{
				GUI.EndClip();
			}
		}

		// Token: 0x02000012 RID: 18
		internal struct ColorScope : IDisposable
		{
			// Token: 0x06000148 RID: 328 RVA: 0x00007634 File Offset: 0x00005834
			public ColorScope(Color newColor)
			{
				this.m_Disposed = false;
				this.m_PreviousColor = GUI.color;
				GUI.color = newColor;
			}

			// Token: 0x06000149 RID: 329 RVA: 0x00007650 File Offset: 0x00005850
			public ColorScope(float r, float g, float b, float a = 1f)
			{
				this = new GUI.ColorScope(new Color(r, g, b, a));
			}

			// Token: 0x0600014A RID: 330 RVA: 0x00007664 File Offset: 0x00005864
			public void Dispose()
			{
				bool disposed = this.m_Disposed;
				if (!disposed)
				{
					this.m_Disposed = true;
					GUI.color = this.m_PreviousColor;
				}
			}

			// Token: 0x04000068 RID: 104
			private bool m_Disposed;

			// Token: 0x04000069 RID: 105
			private Color m_PreviousColor;
		}

		// Token: 0x02000013 RID: 19
		internal struct BackgroundColorScope : IDisposable
		{
			// Token: 0x0600014B RID: 331 RVA: 0x00007691 File Offset: 0x00005891
			public BackgroundColorScope(Color newColor)
			{
				this.m_Disposed = false;
				this.m_PreviousColor = GUI.backgroundColor;
				GUI.backgroundColor = newColor;
			}

			// Token: 0x0600014C RID: 332 RVA: 0x000076AD File Offset: 0x000058AD
			public BackgroundColorScope(float r, float g, float b, float a = 1f)
			{
				this = new GUI.BackgroundColorScope(new Color(r, g, b, a));
			}

			// Token: 0x0600014D RID: 333 RVA: 0x000076C4 File Offset: 0x000058C4
			public void Dispose()
			{
				bool disposed = this.m_Disposed;
				if (!disposed)
				{
					this.m_Disposed = true;
					GUI.backgroundColor = this.m_PreviousColor;
				}
			}

			// Token: 0x0400006A RID: 106
			private bool m_Disposed;

			// Token: 0x0400006B RID: 107
			private Color m_PreviousColor;
		}
	}
}
