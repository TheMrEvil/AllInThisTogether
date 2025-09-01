using System;
using Unity;

namespace System.Drawing
{
	/// <summary>Each property of the <see cref="T:System.Drawing.SystemBrushes" /> class is a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a Windows display element.</summary>
	// Token: 0x02000040 RID: 64
	public static class SystemBrushes
	{
		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the active window's border.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the active window's border.</returns>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00005949 File Offset: 0x00003B49
		public static Brush ActiveBorder
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ActiveBorder);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of the active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of the active window's title bar.</returns>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00005955 File Offset: 0x00003B55
		public static Brush ActiveCaption
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ActiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in the active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of the active window's title bar.</returns>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00005961 File Offset: 0x00003B61
		public static Brush ActiveCaptionText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ActiveCaptionText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the application workspace.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the application workspace.</returns>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000596D File Offset: 0x00003B6D
		public static Brush AppWorkspace
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.AppWorkspace);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</returns>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00005979 File Offset: 0x00003B79
		public static Brush ButtonFace
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ButtonFace);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element.</returns>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005985 File Offset: 0x00003B85
		public static Brush ButtonHighlight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ButtonHighlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element.</returns>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00005991 File Offset: 0x00003B91
		public static Brush ButtonShadow
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ButtonShadow);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</returns>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000599D File Offset: 0x00003B9D
		public static Brush Control
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Control);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element.</returns>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000059A9 File Offset: 0x00003BA9
		public static Brush ControlLightLight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlLightLight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the light color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the light color of a 3-D element.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000059B5 File Offset: 0x00003BB5
		public static Brush ControlLight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlLight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element.</returns>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000059C1 File Offset: 0x00003BC1
		public static Brush ControlDark
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlDark);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the dark shadow color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the dark shadow color of a 3-D element.</returns>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000059CD File Offset: 0x00003BCD
		public static Brush ControlDarkDark
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlDarkDark);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of text in a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of text in a 3-D element.</returns>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000059D9 File Offset: 0x00003BD9
		public static Brush ControlText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the desktop.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the desktop.</returns>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000059E5 File Offset: 0x00003BE5
		public static Brush Desktop
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Desktop);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the lightest color in the color gradient of an active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the lightest color in the color gradient of an active window's title bar.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000059F1 File Offset: 0x00003BF1
		public static Brush GradientActiveCaption
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.GradientActiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the lightest color in the color gradient of an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the lightest color in the color gradient of an inactive window's title bar.</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000059FD File Offset: 0x00003BFD
		public static Brush GradientInactiveCaption
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.GradientInactiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of dimmed text.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of dimmed text.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00005A09 File Offset: 0x00003C09
		public static Brush GrayText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.GrayText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of selected items.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of selected items.</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005A15 File Offset: 0x00003C15
		public static Brush Highlight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Highlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text of selected items.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text of selected items.</returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00005A21 File Offset: 0x00003C21
		public static Brush HighlightText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.HighlightText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color used to designate a hot-tracked item.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color used to designate a hot-tracked item.</returns>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005A2D File Offset: 0x00003C2D
		public static Brush HotTrack
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.HotTrack);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of an inactive window's title bar.</returns>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00005A39 File Offset: 0x00003C39
		public static Brush InactiveCaption
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.InactiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of an inactive window's border.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of an inactive window's border.</returns>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005A45 File Offset: 0x00003C45
		public static Brush InactiveBorder
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.InactiveBorder);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in an inactive window's title bar.</returns>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005A51 File Offset: 0x00003C51
		public static Brush InactiveCaptionText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.InactiveCaptionText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a ToolTip.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a ToolTip.</returns>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005A5D File Offset: 0x00003C5D
		public static Brush Info
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Info);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text of a ToolTip.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> is the color of the text of a ToolTip.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005A69 File Offset: 0x00003C69
		public static Brush InfoText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.InfoText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's background.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's background.</returns>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00005A75 File Offset: 0x00003C75
		public static Brush Menu
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Menu);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a menu bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a menu bar.</returns>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005A81 File Offset: 0x00003C81
		public static Brush MenuBar
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.MenuBar);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color used to highlight menu items when the menu appears as a flat menu.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color used to highlight menu items when the menu appears as a flat menu.</returns>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005A8D File Offset: 0x00003C8D
		public static Brush MenuHighlight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.MenuHighlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's text.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's text.</returns>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005A99 File Offset: 0x00003C99
		public static Brush MenuText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.MenuText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a scroll bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a scroll bar.</returns>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00005AA5 File Offset: 0x00003CA5
		public static Brush ScrollBar
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ScrollBar);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background in the client area of a window.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background in the client area of a window.</returns>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005AB1 File Offset: 0x00003CB1
		public static Brush Window
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Window);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a window frame.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of a window frame.</returns>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005ABD File Offset: 0x00003CBD
		public static Brush WindowFrame
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.WindowFrame);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in the client area of a window.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in the client area of a window.</returns>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005AC9 File Offset: 0x00003CC9
		public static Brush WindowText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.WindowText);
			}
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Brush" /> from the specified <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <param name="c">The <see cref="T:System.Drawing.Color" /> structure from which to create the <see cref="T:System.Drawing.Brush" />.</param>
		/// <returns>The <see cref="T:System.Drawing.Brush" /> this method creates.</returns>
		// Token: 0x0600011F RID: 287 RVA: 0x00005AD8 File Offset: 0x00003CD8
		public static Brush FromSystemColor(Color c)
		{
			if (!c.IsSystemColor())
			{
				throw new ArgumentException(SR.Format("The color {0} is not a system color.", new object[]
				{
					c.ToString()
				}));
			}
			Brush[] array = (Brush[])SafeNativeMethods.Gdip.ThreadData[SystemBrushes.s_systemBrushesKey];
			if (array == null)
			{
				array = new Brush[33];
				SafeNativeMethods.Gdip.ThreadData[SystemBrushes.s_systemBrushesKey] = array;
			}
			int num = (int)c.ToKnownColor();
			if (num > 167)
			{
				num -= 141;
			}
			num--;
			if (array[num] == null)
			{
				array[num] = new SolidBrush(c, true);
			}
			return array[num];
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005B71 File Offset: 0x00003D71
		// Note: this type is marked as 'beforefieldinit'.
		static SystemBrushes()
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005B7D File Offset: 0x00003D7D
		internal SystemBrushes()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400037A RID: 890
		private static readonly object s_systemBrushesKey = new object();
	}
}
