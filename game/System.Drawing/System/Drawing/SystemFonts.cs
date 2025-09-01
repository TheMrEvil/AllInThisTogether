using System;

namespace System.Drawing
{
	/// <summary>Specifies the fonts used to display text in Windows display elements.</summary>
	// Token: 0x0200008B RID: 139
	public sealed class SystemFonts
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x000049FE File Offset: 0x00002BFE
		static SystemFonts()
		{
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00002050 File Offset: 0x00000250
		private SystemFonts()
		{
		}

		/// <summary>Returns a font object that corresponds to the specified system font name.</summary>
		/// <param name="systemFontName">The name of the system font you need a font object for.</param>
		/// <returns>A <see cref="T:System.Drawing.Font" /> if the specified name matches a value in <see cref="T:System.Drawing.SystemFonts" />; otherwise, <see langword="null" />.</returns>
		// Token: 0x06000772 RID: 1906 RVA: 0x00015FF8 File Offset: 0x000141F8
		public static Font GetFontByName(string systemFontName)
		{
			if (systemFontName == "CaptionFont")
			{
				return SystemFonts.CaptionFont;
			}
			if (systemFontName == "DefaultFont")
			{
				return SystemFonts.DefaultFont;
			}
			if (systemFontName == "DialogFont")
			{
				return SystemFonts.DialogFont;
			}
			if (systemFontName == "IconTitleFont")
			{
				return SystemFonts.IconTitleFont;
			}
			if (systemFontName == "MenuFont")
			{
				return SystemFonts.MenuFont;
			}
			if (systemFontName == "MessageBoxFont")
			{
				return SystemFonts.MessageBoxFont;
			}
			if (systemFontName == "SmallCaptionFont")
			{
				return SystemFonts.SmallCaptionFont;
			}
			if (systemFontName == "StatusFont")
			{
				return SystemFonts.StatusFont;
			}
			return null;
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used to display text in the title bars of windows.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that is used to display text in the title bars of windows.</returns>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0001609E File Offset: 0x0001429E
		public static Font CaptionFont
		{
			get
			{
				return new Font("Microsoft Sans Serif", 11f, "CaptionFont");
			}
		}

		/// <summary>Gets the default font that applications can use for dialog boxes and forms.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Font" /> of the system. The value returned will vary depending on the user's operating system and the local culture setting of their system.</returns>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x000160B4 File Offset: 0x000142B4
		public static Font DefaultFont
		{
			get
			{
				return new Font("Microsoft Sans Serif", 8.25f, "DefaultFont");
			}
		}

		/// <summary>Gets a font that applications can use for dialog boxes and forms.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that can be used for dialog boxes and forms, depending on the operating system and local culture setting of the system.</returns>
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x000160CA File Offset: 0x000142CA
		public static Font DialogFont
		{
			get
			{
				return new Font("Tahoma", 8f, "DialogFont");
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used for icon titles.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that is used for icon titles.</returns>
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x000160E0 File Offset: 0x000142E0
		public static Font IconTitleFont
		{
			get
			{
				return new Font("Microsoft Sans Serif", 11f, "IconTitleFont");
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used for menus.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that is used for menus.</returns>
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x000160F6 File Offset: 0x000142F6
		public static Font MenuFont
		{
			get
			{
				return new Font("Microsoft Sans Serif", 11f, "MenuFont");
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used for message boxes.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that is used for message boxes</returns>
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0001610C File Offset: 0x0001430C
		public static Font MessageBoxFont
		{
			get
			{
				return new Font("Microsoft Sans Serif", 11f, "MessageBoxFont");
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used to display text in the title bars of small windows, such as tool windows.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that is used to display text in the title bars of small windows, such as tool windows.</returns>
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00016122 File Offset: 0x00014322
		public static Font SmallCaptionFont
		{
			get
			{
				return new Font("Microsoft Sans Serif", 11f, "SmallCaptionFont");
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used to display text in the status bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that is used to display text in the status bar.</returns>
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x00016138 File Offset: 0x00014338
		public static Font StatusFont
		{
			get
			{
				return new Font("Microsoft Sans Serif", 11f, "StatusFont");
			}
		}
	}
}
