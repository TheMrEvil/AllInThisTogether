using System;

namespace System.Drawing
{
	/// <summary>Each property of the <see cref="T:System.Drawing.SystemPens" /> class is a <see cref="T:System.Drawing.Pen" /> that is the color of a Windows display element and that has a width of 1 pixel.</summary>
	// Token: 0x0200008D RID: 141
	public sealed class SystemPens
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x00002050 File Offset: 0x00000250
		private SystemPens()
		{
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the text in the active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the text in the active window's title bar.</returns>
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x0001620A File Offset: 0x0001440A
		public static Pen ActiveCaptionText
		{
			get
			{
				if (SystemPens.active_caption_text == null)
				{
					SystemPens.active_caption_text = new Pen(SystemColors.ActiveCaptionText);
					SystemPens.active_caption_text.isModifiable = false;
				}
				return SystemPens.active_caption_text;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the face color of a 3-D element.</returns>
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00016232 File Offset: 0x00014432
		public static Pen Control
		{
			get
			{
				if (SystemPens.control == null)
				{
					SystemPens.control = new Pen(SystemColors.Control);
					SystemPens.control.isModifiable = false;
				}
				return SystemPens.control;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the shadow color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the shadow color of a 3-D element.</returns>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0001625A File Offset: 0x0001445A
		public static Pen ControlDark
		{
			get
			{
				if (SystemPens.control_dark == null)
				{
					SystemPens.control_dark = new Pen(SystemColors.ControlDark);
					SystemPens.control_dark.isModifiable = false;
				}
				return SystemPens.control_dark;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the dark shadow color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the dark shadow color of a 3-D element.</returns>
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x00016282 File Offset: 0x00014482
		public static Pen ControlDarkDark
		{
			get
			{
				if (SystemPens.control_dark_dark == null)
				{
					SystemPens.control_dark_dark = new Pen(SystemColors.ControlDarkDark);
					SystemPens.control_dark_dark.isModifiable = false;
				}
				return SystemPens.control_dark_dark;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the light color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the light color of a 3-D element.</returns>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x000162AA File Offset: 0x000144AA
		public static Pen ControlLight
		{
			get
			{
				if (SystemPens.control_light == null)
				{
					SystemPens.control_light = new Pen(SystemColors.ControlLight);
					SystemPens.control_light.isModifiable = false;
				}
				return SystemPens.control_light;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the highlight color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the highlight color of a 3-D element.</returns>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x000162D2 File Offset: 0x000144D2
		public static Pen ControlLightLight
		{
			get
			{
				if (SystemPens.control_light_light == null)
				{
					SystemPens.control_light_light = new Pen(SystemColors.ControlLightLight);
					SystemPens.control_light_light.isModifiable = false;
				}
				return SystemPens.control_light_light;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of text in a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of text in a 3-D element.</returns>
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x000162FA File Offset: 0x000144FA
		public static Pen ControlText
		{
			get
			{
				if (SystemPens.control_text == null)
				{
					SystemPens.control_text = new Pen(SystemColors.ControlText);
					SystemPens.control_text.isModifiable = false;
				}
				return SystemPens.control_text;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of dimmed text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of dimmed text.</returns>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x00016322 File Offset: 0x00014522
		public static Pen GrayText
		{
			get
			{
				if (SystemPens.gray_text == null)
				{
					SystemPens.gray_text = new Pen(SystemColors.GrayText);
					SystemPens.gray_text.isModifiable = false;
				}
				return SystemPens.gray_text;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background of selected items.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background of selected items.</returns>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0001634A File Offset: 0x0001454A
		public static Pen Highlight
		{
			get
			{
				if (SystemPens.highlight == null)
				{
					SystemPens.highlight = new Pen(SystemColors.Highlight);
					SystemPens.highlight.isModifiable = false;
				}
				return SystemPens.highlight;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the text of selected items.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the text of selected items.</returns>
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x00016372 File Offset: 0x00014572
		public static Pen HighlightText
		{
			get
			{
				if (SystemPens.highlight_text == null)
				{
					SystemPens.highlight_text = new Pen(SystemColors.HighlightText);
					SystemPens.highlight_text.isModifiable = false;
				}
				return SystemPens.highlight_text;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the text in an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the text in an inactive window's title bar.</returns>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0001639A File Offset: 0x0001459A
		public static Pen InactiveCaptionText
		{
			get
			{
				if (SystemPens.inactive_caption_text == null)
				{
					SystemPens.inactive_caption_text = new Pen(SystemColors.InactiveCaptionText);
					SystemPens.inactive_caption_text.isModifiable = false;
				}
				return SystemPens.inactive_caption_text;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the text of a ToolTip.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the text of a ToolTip.</returns>
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x000163C2 File Offset: 0x000145C2
		public static Pen InfoText
		{
			get
			{
				if (SystemPens.info_text == null)
				{
					SystemPens.info_text = new Pen(SystemColors.InfoText);
					SystemPens.info_text.isModifiable = false;
				}
				return SystemPens.info_text;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of a menu's text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of a menu's text.</returns>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x000163EA File Offset: 0x000145EA
		public static Pen MenuText
		{
			get
			{
				if (SystemPens.menu_text == null)
				{
					SystemPens.menu_text = new Pen(SystemColors.MenuText);
					SystemPens.menu_text.isModifiable = false;
				}
				return SystemPens.menu_text;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of a window frame.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of a window frame.</returns>
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00016412 File Offset: 0x00014612
		public static Pen WindowFrame
		{
			get
			{
				if (SystemPens.window_frame == null)
				{
					SystemPens.window_frame = new Pen(SystemColors.WindowFrame);
					SystemPens.window_frame.isModifiable = false;
				}
				return SystemPens.window_frame;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the text in the client area of a window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the text in the client area of a window.</returns>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0001643A File Offset: 0x0001463A
		public static Pen WindowText
		{
			get
			{
				if (SystemPens.window_text == null)
				{
					SystemPens.window_text = new Pen(SystemColors.WindowText);
					SystemPens.window_text.isModifiable = false;
				}
				return SystemPens.window_text;
			}
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Pen" /> from the specified <see cref="T:System.Drawing.Color" />.</summary>
		/// <param name="c">The <see cref="T:System.Drawing.Color" /> for the new <see cref="T:System.Drawing.Pen" />.</param>
		/// <returns>The <see cref="T:System.Drawing.Pen" /> this method creates.</returns>
		// Token: 0x06000797 RID: 1943 RVA: 0x00016462 File Offset: 0x00014662
		public static Pen FromSystemColor(Color c)
		{
			if (c.IsSystemColor)
			{
				return new Pen(c)
				{
					isModifiable = false
				};
			}
			throw new ArgumentException(string.Format("The color {0} is not a system color.", c));
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the active window's border.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the active window's border.</returns>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x00016490 File Offset: 0x00014690
		public static Pen ActiveBorder
		{
			get
			{
				if (SystemPens.active_border == null)
				{
					SystemPens.active_border = new Pen(SystemColors.ActiveBorder);
					SystemPens.active_border.isModifiable = false;
				}
				return SystemPens.active_border;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background of the active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background of the active window's title bar.</returns>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x000164B8 File Offset: 0x000146B8
		public static Pen ActiveCaption
		{
			get
			{
				if (SystemPens.active_caption == null)
				{
					SystemPens.active_caption = new Pen(SystemColors.ActiveCaption);
					SystemPens.active_caption.isModifiable = false;
				}
				return SystemPens.active_caption;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the application workspace.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the application workspace.</returns>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x000164E0 File Offset: 0x000146E0
		public static Pen AppWorkspace
		{
			get
			{
				if (SystemPens.app_workspace == null)
				{
					SystemPens.app_workspace = new Pen(SystemColors.AppWorkspace);
					SystemPens.app_workspace.isModifiable = false;
				}
				return SystemPens.app_workspace;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the face color of a 3-D element.</returns>
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00016508 File Offset: 0x00014708
		public static Pen ButtonFace
		{
			get
			{
				if (SystemPens.button_face == null)
				{
					SystemPens.button_face = new Pen(SystemColors.ButtonFace);
					SystemPens.button_face.isModifiable = false;
				}
				return SystemPens.button_face;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the highlight color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the highlight color of a 3-D element.</returns>
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00016530 File Offset: 0x00014730
		public static Pen ButtonHighlight
		{
			get
			{
				if (SystemPens.button_highlight == null)
				{
					SystemPens.button_highlight = new Pen(SystemColors.ButtonHighlight);
					SystemPens.button_highlight.isModifiable = false;
				}
				return SystemPens.button_highlight;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the shadow color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the shadow color of a 3-D element.</returns>
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x00016558 File Offset: 0x00014758
		public static Pen ButtonShadow
		{
			get
			{
				if (SystemPens.button_shadow == null)
				{
					SystemPens.button_shadow = new Pen(SystemColors.ButtonShadow);
					SystemPens.button_shadow.isModifiable = false;
				}
				return SystemPens.button_shadow;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the Windows desktop.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the Windows desktop.</returns>
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00016580 File Offset: 0x00014780
		public static Pen Desktop
		{
			get
			{
				if (SystemPens.desktop == null)
				{
					SystemPens.desktop = new Pen(SystemColors.Desktop);
					SystemPens.desktop.isModifiable = false;
				}
				return SystemPens.desktop;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the lightest color in the color gradient of an active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the lightest color in the color gradient of an active window's title bar.</returns>
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x000165A8 File Offset: 0x000147A8
		public static Pen GradientActiveCaption
		{
			get
			{
				if (SystemPens.gradient_activecaption == null)
				{
					SystemPens.gradient_activecaption = new Pen(SystemColors.GradientActiveCaption);
					SystemPens.gradient_activecaption.isModifiable = false;
				}
				return SystemPens.gradient_activecaption;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the lightest color in the color gradient of an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the lightest color in the color gradient of an inactive window's title bar.</returns>
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x000165D0 File Offset: 0x000147D0
		public static Pen GradientInactiveCaption
		{
			get
			{
				if (SystemPens.gradient_inactivecaption == null)
				{
					SystemPens.gradient_inactivecaption = new Pen(SystemColors.GradientInactiveCaption);
					SystemPens.gradient_inactivecaption.isModifiable = false;
				}
				return SystemPens.gradient_inactivecaption;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color used to designate a hot-tracked item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color used to designate a hot-tracked item.</returns>
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x000165F8 File Offset: 0x000147F8
		public static Pen HotTrack
		{
			get
			{
				if (SystemPens.hot_track == null)
				{
					SystemPens.hot_track = new Pen(SystemColors.HotTrack);
					SystemPens.hot_track.isModifiable = false;
				}
				return SystemPens.hot_track;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> is the color of the border of an inactive window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the border of an inactive window.</returns>
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00016620 File Offset: 0x00014820
		public static Pen InactiveBorder
		{
			get
			{
				if (SystemPens.inactive_border == null)
				{
					SystemPens.inactive_border = new Pen(SystemColors.InactiveBorder);
					SystemPens.inactive_border.isModifiable = false;
				}
				return SystemPens.inactive_border;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the title bar caption of an inactive window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the title bar caption of an inactive window.</returns>
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00016648 File Offset: 0x00014848
		public static Pen InactiveCaption
		{
			get
			{
				if (SystemPens.inactive_caption == null)
				{
					SystemPens.inactive_caption = new Pen(SystemColors.InactiveCaption);
					SystemPens.inactive_caption.isModifiable = false;
				}
				return SystemPens.inactive_caption;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background of a ToolTip.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background of a ToolTip.</returns>
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00016670 File Offset: 0x00014870
		public static Pen Info
		{
			get
			{
				if (SystemPens.info == null)
				{
					SystemPens.info = new Pen(SystemColors.Info);
					SystemPens.info.isModifiable = false;
				}
				return SystemPens.info;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of a menu's background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of a menu's background.</returns>
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00016698 File Offset: 0x00014898
		public static Pen Menu
		{
			get
			{
				if (SystemPens.menu == null)
				{
					SystemPens.menu = new Pen(SystemColors.Menu);
					SystemPens.menu.isModifiable = false;
				}
				return SystemPens.menu;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background of a menu bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background of a menu bar.</returns>
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x000166C0 File Offset: 0x000148C0
		public static Pen MenuBar
		{
			get
			{
				if (SystemPens.menu_bar == null)
				{
					SystemPens.menu_bar = new Pen(SystemColors.MenuBar);
					SystemPens.menu_bar.isModifiable = false;
				}
				return SystemPens.menu_bar;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color used to highlight menu items when the menu appears as a flat menu.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color used to highlight menu items when the menu appears as a flat menu.</returns>
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x000166E8 File Offset: 0x000148E8
		public static Pen MenuHighlight
		{
			get
			{
				if (SystemPens.menu_highlight == null)
				{
					SystemPens.menu_highlight = new Pen(SystemColors.MenuHighlight);
					SystemPens.menu_highlight.isModifiable = false;
				}
				return SystemPens.menu_highlight;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background of a scroll bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background of a scroll bar.</returns>
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00016710 File Offset: 0x00014910
		public static Pen ScrollBar
		{
			get
			{
				if (SystemPens.scroll_bar == null)
				{
					SystemPens.scroll_bar = new Pen(SystemColors.ScrollBar);
					SystemPens.scroll_bar.isModifiable = false;
				}
				return SystemPens.scroll_bar;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background in the client area of a window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background in the client area of a window.</returns>
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00016738 File Offset: 0x00014938
		public static Pen Window
		{
			get
			{
				if (SystemPens.window == null)
				{
					SystemPens.window = new Pen(SystemColors.Window);
					SystemPens.window.isModifiable = false;
				}
				return SystemPens.window;
			}
		}

		// Token: 0x04000571 RID: 1393
		private static Pen active_caption_text;

		// Token: 0x04000572 RID: 1394
		private static Pen control;

		// Token: 0x04000573 RID: 1395
		private static Pen control_dark;

		// Token: 0x04000574 RID: 1396
		private static Pen control_dark_dark;

		// Token: 0x04000575 RID: 1397
		private static Pen control_light;

		// Token: 0x04000576 RID: 1398
		private static Pen control_light_light;

		// Token: 0x04000577 RID: 1399
		private static Pen control_text;

		// Token: 0x04000578 RID: 1400
		private static Pen gray_text;

		// Token: 0x04000579 RID: 1401
		private static Pen highlight;

		// Token: 0x0400057A RID: 1402
		private static Pen highlight_text;

		// Token: 0x0400057B RID: 1403
		private static Pen inactive_caption_text;

		// Token: 0x0400057C RID: 1404
		private static Pen info_text;

		// Token: 0x0400057D RID: 1405
		private static Pen menu_text;

		// Token: 0x0400057E RID: 1406
		private static Pen window_frame;

		// Token: 0x0400057F RID: 1407
		private static Pen window_text;

		// Token: 0x04000580 RID: 1408
		private static Pen active_border;

		// Token: 0x04000581 RID: 1409
		private static Pen active_caption;

		// Token: 0x04000582 RID: 1410
		private static Pen app_workspace;

		// Token: 0x04000583 RID: 1411
		private static Pen button_face;

		// Token: 0x04000584 RID: 1412
		private static Pen button_highlight;

		// Token: 0x04000585 RID: 1413
		private static Pen button_shadow;

		// Token: 0x04000586 RID: 1414
		private static Pen desktop;

		// Token: 0x04000587 RID: 1415
		private static Pen gradient_activecaption;

		// Token: 0x04000588 RID: 1416
		private static Pen gradient_inactivecaption;

		// Token: 0x04000589 RID: 1417
		private static Pen hot_track;

		// Token: 0x0400058A RID: 1418
		private static Pen inactive_border;

		// Token: 0x0400058B RID: 1419
		private static Pen inactive_caption;

		// Token: 0x0400058C RID: 1420
		private static Pen info;

		// Token: 0x0400058D RID: 1421
		private static Pen menu;

		// Token: 0x0400058E RID: 1422
		private static Pen menu_bar;

		// Token: 0x0400058F RID: 1423
		private static Pen menu_highlight;

		// Token: 0x04000590 RID: 1424
		private static Pen scroll_bar;

		// Token: 0x04000591 RID: 1425
		private static Pen window;
	}
}
