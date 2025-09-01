using System;
using Unity;

namespace System.Drawing
{
	/// <summary>Each property of the <see cref="T:System.Drawing.SystemColors" /> class is a <see cref="T:System.Drawing.Color" /> structure that is the color of a Windows display element.</summary>
	// Token: 0x02000041 RID: 65
	public static class SystemColors
	{
		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the active window's border.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the active window's border.</returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005B84 File Offset: 0x00003D84
		public static Color ActiveBorder
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ActiveBorder);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of the active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the active window's title bar.</returns>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00005B8C File Offset: 0x00003D8C
		public static Color ActiveCaption
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ActiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text in the active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text in the active window's title bar.</returns>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005B94 File Offset: 0x00003D94
		public static Color ActiveCaptionText
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ActiveCaptionText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the application workspace.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the application workspace.</returns>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00005B9C File Offset: 0x00003D9C
		public static Color AppWorkspace
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.AppWorkspace);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the face color of a 3-D element.</returns>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00005BA4 File Offset: 0x00003DA4
		public static Color ButtonFace
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ButtonFace);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the highlight color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the highlight color of a 3-D element.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005BB0 File Offset: 0x00003DB0
		public static Color ButtonHighlight
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ButtonHighlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the shadow color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the shadow color of a 3-D element.</returns>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00005BBC File Offset: 0x00003DBC
		public static Color ButtonShadow
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ButtonShadow);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the face color of a 3-D element.</returns>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00005BC8 File Offset: 0x00003DC8
		public static Color Control
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.Control);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the shadow color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the shadow color of a 3-D element.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005BD0 File Offset: 0x00003DD0
		public static Color ControlDark
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ControlDark);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the dark shadow color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the dark shadow color of a 3-D element.</returns>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00005BD8 File Offset: 0x00003DD8
		public static Color ControlDarkDark
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ControlDarkDark);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the light color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the light color of a 3-D element.</returns>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00005BE0 File Offset: 0x00003DE0
		public static Color ControlLight
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ControlLight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the highlight color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the highlight color of a 3-D element.</returns>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00005BE8 File Offset: 0x00003DE8
		public static Color ControlLightLight
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ControlLightLight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of text in a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of text in a 3-D element.</returns>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00005BF1 File Offset: 0x00003DF1
		public static Color ControlText
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ControlText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the desktop.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the desktop.</returns>
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00005BFA File Offset: 0x00003DFA
		public static Color Desktop
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.Desktop);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the lightest color in the color gradient of an active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the lightest color in the color gradient of an active window's title bar.</returns>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00005C03 File Offset: 0x00003E03
		public static Color GradientActiveCaption
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.GradientActiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the lightest color in the color gradient of an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the lightest color in the color gradient of an inactive window's title bar.</returns>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00005C0F File Offset: 0x00003E0F
		public static Color GradientInactiveCaption
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.GradientInactiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of dimmed text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of dimmed text.</returns>
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00005C1B File Offset: 0x00003E1B
		public static Color GrayText
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.GrayText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of selected items.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of selected items.</returns>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005C24 File Offset: 0x00003E24
		public static Color Highlight
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.Highlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text of selected items.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text of selected items.</returns>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005C2D File Offset: 0x00003E2D
		public static Color HighlightText
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.HighlightText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color used to designate a hot-tracked item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color used to designate a hot-tracked item.</returns>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00005C36 File Offset: 0x00003E36
		public static Color HotTrack
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.HotTrack);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of an inactive window's border.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of an inactive window's border.</returns>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00005C3F File Offset: 0x00003E3F
		public static Color InactiveBorder
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.InactiveBorder);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of an inactive window's title bar.</returns>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005C48 File Offset: 0x00003E48
		public static Color InactiveCaption
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.InactiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text in an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text in an inactive window's title bar.</returns>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00005C51 File Offset: 0x00003E51
		public static Color InactiveCaptionText
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.InactiveCaptionText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of a ToolTip.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of a ToolTip.</returns>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005C5A File Offset: 0x00003E5A
		public static Color Info
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.Info);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text of a ToolTip.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text of a ToolTip.</returns>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00005C63 File Offset: 0x00003E63
		public static Color InfoText
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.InfoText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of a menu's background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of a menu's background.</returns>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00005C6C File Offset: 0x00003E6C
		public static Color Menu
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.Menu);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of a menu bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of a menu bar.</returns>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00005C75 File Offset: 0x00003E75
		public static Color MenuBar
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.MenuBar);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color used to highlight menu items when the menu appears as a flat menu.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color used to highlight menu items when the menu appears as a flat menu.</returns>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00005C81 File Offset: 0x00003E81
		public static Color MenuHighlight
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.MenuHighlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of a menu's text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of a menu's text.</returns>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00005C8D File Offset: 0x00003E8D
		public static Color MenuText
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.MenuText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of a scroll bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of a scroll bar.</returns>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00005C96 File Offset: 0x00003E96
		public static Color ScrollBar
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.ScrollBar);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background in the client area of a window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background in the client area of a window.</returns>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005C9F File Offset: 0x00003E9F
		public static Color Window
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.Window);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of a window frame.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of a window frame.</returns>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005CA8 File Offset: 0x00003EA8
		public static Color WindowFrame
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.WindowFrame);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text in the client area of a window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text in the client area of a window.</returns>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005CB1 File Offset: 0x00003EB1
		public static Color WindowText
		{
			get
			{
				return ColorUtil.FromKnownColor(KnownColor.WindowText);
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005B7D File Offset: 0x00003D7D
		internal SystemColors()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
