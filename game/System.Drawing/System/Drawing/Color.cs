using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Numerics.Hashing;

namespace System.Drawing
{
	/// <summary>Represents an ARGB (alpha, red, green, blue) color.</summary>
	// Token: 0x02000046 RID: 70
	[TypeConverter(typeof(ColorConverter))]
	[Editor("System.Drawing.Design.ColorEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[DebuggerDisplay("{NameAndARGBValue}")]
	[Serializable]
	public readonly struct Color : IEquatable<Color>
	{
		/// <summary>Gets a system-defined color.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000646C File Offset: 0x0000466C
		public static Color Transparent
		{
			get
			{
				return new Color(KnownColor.Transparent);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF0F8FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00006475 File Offset: 0x00004675
		public static Color AliceBlue
		{
			get
			{
				return new Color(KnownColor.AliceBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFAEBD7.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000647E File Offset: 0x0000467E
		public static Color AntiqueWhite
		{
			get
			{
				return new Color(KnownColor.AntiqueWhite);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00FFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00006487 File Offset: 0x00004687
		public static Color Aqua
		{
			get
			{
				return new Color(KnownColor.Aqua);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF7FFFD4.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00006490 File Offset: 0x00004690
		public static Color Aquamarine
		{
			get
			{
				return new Color(KnownColor.Aquamarine);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF0FFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00006499 File Offset: 0x00004699
		public static Color Azure
		{
			get
			{
				return new Color(KnownColor.Azure);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF5F5DC.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000064A2 File Offset: 0x000046A2
		public static Color Beige
		{
			get
			{
				return new Color(KnownColor.Beige);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFE4C4.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000064AB File Offset: 0x000046AB
		public static Color Bisque
		{
			get
			{
				return new Color(KnownColor.Bisque);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF000000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000064B4 File Offset: 0x000046B4
		public static Color Black
		{
			get
			{
				return new Color(KnownColor.Black);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFEBCD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000064BD File Offset: 0x000046BD
		public static Color BlanchedAlmond
		{
			get
			{
				return new Color(KnownColor.BlanchedAlmond);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF0000FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000064C6 File Offset: 0x000046C6
		public static Color Blue
		{
			get
			{
				return new Color(KnownColor.Blue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF8A2BE2.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000064CF File Offset: 0x000046CF
		public static Color BlueViolet
		{
			get
			{
				return new Color(KnownColor.BlueViolet);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFA52A2A.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000064D8 File Offset: 0x000046D8
		public static Color Brown
		{
			get
			{
				return new Color(KnownColor.Brown);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDEB887.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000064E1 File Offset: 0x000046E1
		public static Color BurlyWood
		{
			get
			{
				return new Color(KnownColor.BurlyWood);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF5F9EA0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000064EA File Offset: 0x000046EA
		public static Color CadetBlue
		{
			get
			{
				return new Color(KnownColor.CadetBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF7FFF00.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000198 RID: 408 RVA: 0x000064F3 File Offset: 0x000046F3
		public static Color Chartreuse
		{
			get
			{
				return new Color(KnownColor.Chartreuse);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFD2691E.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000064FC File Offset: 0x000046FC
		public static Color Chocolate
		{
			get
			{
				return new Color(KnownColor.Chocolate);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF7F50.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00006505 File Offset: 0x00004705
		public static Color Coral
		{
			get
			{
				return new Color(KnownColor.Coral);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF6495ED.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000650E File Offset: 0x0000470E
		public static Color CornflowerBlue
		{
			get
			{
				return new Color(KnownColor.CornflowerBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFF8DC.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00006517 File Offset: 0x00004717
		public static Color Cornsilk
		{
			get
			{
				return new Color(KnownColor.Cornsilk);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDC143C.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00006520 File Offset: 0x00004720
		public static Color Crimson
		{
			get
			{
				return new Color(KnownColor.Crimson);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00FFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00006529 File Offset: 0x00004729
		public static Color Cyan
		{
			get
			{
				return new Color(KnownColor.Cyan);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00008B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00006532 File Offset: 0x00004732
		public static Color DarkBlue
		{
			get
			{
				return new Color(KnownColor.DarkBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF008B8B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000653B File Offset: 0x0000473B
		public static Color DarkCyan
		{
			get
			{
				return new Color(KnownColor.DarkCyan);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFB8860B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00006544 File Offset: 0x00004744
		public static Color DarkGoldenrod
		{
			get
			{
				return new Color(KnownColor.DarkGoldenrod);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFA9A9A9.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000654D File Offset: 0x0000474D
		public static Color DarkGray
		{
			get
			{
				return new Color(KnownColor.DarkGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF006400.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00006556 File Offset: 0x00004756
		public static Color DarkGreen
		{
			get
			{
				return new Color(KnownColor.DarkGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFBDB76B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000655F File Offset: 0x0000475F
		public static Color DarkKhaki
		{
			get
			{
				return new Color(KnownColor.DarkKhaki);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF8B008B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00006568 File Offset: 0x00004768
		public static Color DarkMagenta
		{
			get
			{
				return new Color(KnownColor.DarkMagenta);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF556B2F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00006571 File Offset: 0x00004771
		public static Color DarkOliveGreen
		{
			get
			{
				return new Color(KnownColor.DarkOliveGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF8C00.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000657A File Offset: 0x0000477A
		public static Color DarkOrange
		{
			get
			{
				return new Color(KnownColor.DarkOrange);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF9932CC.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00006583 File Offset: 0x00004783
		public static Color DarkOrchid
		{
			get
			{
				return new Color(KnownColor.DarkOrchid);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF8B0000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000658C File Offset: 0x0000478C
		public static Color DarkRed
		{
			get
			{
				return new Color(KnownColor.DarkRed);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFE9967A.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00006595 File Offset: 0x00004795
		public static Color DarkSalmon
		{
			get
			{
				return new Color(KnownColor.DarkSalmon);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF8FBC8F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000659E File Offset: 0x0000479E
		public static Color DarkSeaGreen
		{
			get
			{
				return new Color(KnownColor.DarkSeaGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF483D8B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000065A7 File Offset: 0x000047A7
		public static Color DarkSlateBlue
		{
			get
			{
				return new Color(KnownColor.DarkSlateBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF2F4F4F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000065B0 File Offset: 0x000047B0
		public static Color DarkSlateGray
		{
			get
			{
				return new Color(KnownColor.DarkSlateGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00CED1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000065B9 File Offset: 0x000047B9
		public static Color DarkTurquoise
		{
			get
			{
				return new Color(KnownColor.DarkTurquoise);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF9400D3.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000065C2 File Offset: 0x000047C2
		public static Color DarkViolet
		{
			get
			{
				return new Color(KnownColor.DarkViolet);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF1493.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000065CB File Offset: 0x000047CB
		public static Color DeepPink
		{
			get
			{
				return new Color(KnownColor.DeepPink);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00BFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000065D4 File Offset: 0x000047D4
		public static Color DeepSkyBlue
		{
			get
			{
				return new Color(KnownColor.DeepSkyBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF696969.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000065DD File Offset: 0x000047DD
		public static Color DimGray
		{
			get
			{
				return new Color(KnownColor.DimGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF1E90FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000065E6 File Offset: 0x000047E6
		public static Color DodgerBlue
		{
			get
			{
				return new Color(KnownColor.DodgerBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFB22222.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000065EF File Offset: 0x000047EF
		public static Color Firebrick
		{
			get
			{
				return new Color(KnownColor.Firebrick);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFAF0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x000065F8 File Offset: 0x000047F8
		public static Color FloralWhite
		{
			get
			{
				return new Color(KnownColor.FloralWhite);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF228B22.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00006601 File Offset: 0x00004801
		public static Color ForestGreen
		{
			get
			{
				return new Color(KnownColor.ForestGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF00FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000660A File Offset: 0x0000480A
		public static Color Fuchsia
		{
			get
			{
				return new Color(KnownColor.Fuchsia);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDCDCDC.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00006613 File Offset: 0x00004813
		public static Color Gainsboro
		{
			get
			{
				return new Color(KnownColor.Gainsboro);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF8F8FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000661C File Offset: 0x0000481C
		public static Color GhostWhite
		{
			get
			{
				return new Color(KnownColor.GhostWhite);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFD700.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00006625 File Offset: 0x00004825
		public static Color Gold
		{
			get
			{
				return new Color(KnownColor.Gold);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDAA520.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000662E File Offset: 0x0000482E
		public static Color Goldenrod
		{
			get
			{
				return new Color(KnownColor.Goldenrod);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF808080.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> strcture representing a system-defined color.</returns>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00006637 File Offset: 0x00004837
		public static Color Gray
		{
			get
			{
				return new Color(KnownColor.Gray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF008000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00006640 File Offset: 0x00004840
		public static Color Green
		{
			get
			{
				return new Color(KnownColor.Green);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFADFF2F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00006649 File Offset: 0x00004849
		public static Color GreenYellow
		{
			get
			{
				return new Color(KnownColor.GreenYellow);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF0FFF0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00006652 File Offset: 0x00004852
		public static Color Honeydew
		{
			get
			{
				return new Color(KnownColor.Honeydew);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF69B4.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000665B File Offset: 0x0000485B
		public static Color HotPink
		{
			get
			{
				return new Color(KnownColor.HotPink);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFCD5C5C.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00006664 File Offset: 0x00004864
		public static Color IndianRed
		{
			get
			{
				return new Color(KnownColor.IndianRed);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF4B0082.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000666D File Offset: 0x0000486D
		public static Color Indigo
		{
			get
			{
				return new Color(KnownColor.Indigo);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFFF0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00006676 File Offset: 0x00004876
		public static Color Ivory
		{
			get
			{
				return new Color(KnownColor.Ivory);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF0E68C.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000667F File Offset: 0x0000487F
		public static Color Khaki
		{
			get
			{
				return new Color(KnownColor.Khaki);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFE6E6FA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00006688 File Offset: 0x00004888
		public static Color Lavender
		{
			get
			{
				return new Color(KnownColor.Lavender);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFF0F5.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00006691 File Offset: 0x00004891
		public static Color LavenderBlush
		{
			get
			{
				return new Color(KnownColor.LavenderBlush);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF7CFC00.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000669A File Offset: 0x0000489A
		public static Color LawnGreen
		{
			get
			{
				return new Color(KnownColor.LawnGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFACD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x000066A3 File Offset: 0x000048A3
		public static Color LemonChiffon
		{
			get
			{
				return new Color(KnownColor.LemonChiffon);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFADD8E6.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000066AC File Offset: 0x000048AC
		public static Color LightBlue
		{
			get
			{
				return new Color(KnownColor.LightBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF08080.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000066B5 File Offset: 0x000048B5
		public static Color LightCoral
		{
			get
			{
				return new Color(KnownColor.LightCoral);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFE0FFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000066BE File Offset: 0x000048BE
		public static Color LightCyan
		{
			get
			{
				return new Color(KnownColor.LightCyan);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFAFAD2.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000066C7 File Offset: 0x000048C7
		public static Color LightGoldenrodYellow
		{
			get
			{
				return new Color(KnownColor.LightGoldenrodYellow);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF90EE90.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000066D0 File Offset: 0x000048D0
		public static Color LightGreen
		{
			get
			{
				return new Color(KnownColor.LightGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFD3D3D3.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000066D9 File Offset: 0x000048D9
		public static Color LightGray
		{
			get
			{
				return new Color(KnownColor.LightGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFB6C1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000066E2 File Offset: 0x000048E2
		public static Color LightPink
		{
			get
			{
				return new Color(KnownColor.LightPink);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFA07A.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000066EB File Offset: 0x000048EB
		public static Color LightSalmon
		{
			get
			{
				return new Color(KnownColor.LightSalmon);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF20B2AA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000066F4 File Offset: 0x000048F4
		public static Color LightSeaGreen
		{
			get
			{
				return new Color(KnownColor.LightSeaGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF87CEFA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x000066FD File Offset: 0x000048FD
		public static Color LightSkyBlue
		{
			get
			{
				return new Color(KnownColor.LightSkyBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF778899.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00006706 File Offset: 0x00004906
		public static Color LightSlateGray
		{
			get
			{
				return new Color(KnownColor.LightSlateGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFB0C4DE.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000670F File Offset: 0x0000490F
		public static Color LightSteelBlue
		{
			get
			{
				return new Color(KnownColor.LightSteelBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFFE0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00006718 File Offset: 0x00004918
		public static Color LightYellow
		{
			get
			{
				return new Color(KnownColor.LightYellow);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00FF00.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00006721 File Offset: 0x00004921
		public static Color Lime
		{
			get
			{
				return new Color(KnownColor.Lime);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF32CD32.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000672A File Offset: 0x0000492A
		public static Color LimeGreen
		{
			get
			{
				return new Color(KnownColor.LimeGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFAF0E6.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00006733 File Offset: 0x00004933
		public static Color Linen
		{
			get
			{
				return new Color(KnownColor.Linen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF00FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000673C File Offset: 0x0000493C
		public static Color Magenta
		{
			get
			{
				return new Color(KnownColor.Magenta);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF800000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00006745 File Offset: 0x00004945
		public static Color Maroon
		{
			get
			{
				return new Color(KnownColor.Maroon);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF66CDAA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000674E File Offset: 0x0000494E
		public static Color MediumAquamarine
		{
			get
			{
				return new Color(KnownColor.MediumAquamarine);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF0000CD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00006757 File Offset: 0x00004957
		public static Color MediumBlue
		{
			get
			{
				return new Color(KnownColor.MediumBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFBA55D3.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00006760 File Offset: 0x00004960
		public static Color MediumOrchid
		{
			get
			{
				return new Color(KnownColor.MediumOrchid);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF9370DB.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00006769 File Offset: 0x00004969
		public static Color MediumPurple
		{
			get
			{
				return new Color(KnownColor.MediumPurple);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF3CB371.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00006772 File Offset: 0x00004972
		public static Color MediumSeaGreen
		{
			get
			{
				return new Color(KnownColor.MediumSeaGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF7B68EE.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000677B File Offset: 0x0000497B
		public static Color MediumSlateBlue
		{
			get
			{
				return new Color(KnownColor.MediumSlateBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00FA9A.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00006784 File Offset: 0x00004984
		public static Color MediumSpringGreen
		{
			get
			{
				return new Color(KnownColor.MediumSpringGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF48D1CC.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000678D File Offset: 0x0000498D
		public static Color MediumTurquoise
		{
			get
			{
				return new Color(KnownColor.MediumTurquoise);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFC71585.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00006796 File Offset: 0x00004996
		public static Color MediumVioletRed
		{
			get
			{
				return new Color(KnownColor.MediumVioletRed);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF191970.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000679F File Offset: 0x0000499F
		public static Color MidnightBlue
		{
			get
			{
				return new Color(KnownColor.MidnightBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF5FFFA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000067A8 File Offset: 0x000049A8
		public static Color MintCream
		{
			get
			{
				return new Color(KnownColor.MintCream);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFE4E1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000067B1 File Offset: 0x000049B1
		public static Color MistyRose
		{
			get
			{
				return new Color(KnownColor.MistyRose);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFE4B5.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000067BA File Offset: 0x000049BA
		public static Color Moccasin
		{
			get
			{
				return new Color(KnownColor.Moccasin);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFDEAD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000067C3 File Offset: 0x000049C3
		public static Color NavajoWhite
		{
			get
			{
				return new Color(KnownColor.NavajoWhite);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF000080.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000067CC File Offset: 0x000049CC
		public static Color Navy
		{
			get
			{
				return new Color(KnownColor.Navy);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFDF5E6.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000067D5 File Offset: 0x000049D5
		public static Color OldLace
		{
			get
			{
				return new Color(KnownColor.OldLace);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF808000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000067DE File Offset: 0x000049DE
		public static Color Olive
		{
			get
			{
				return new Color(KnownColor.Olive);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF6B8E23.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001EC RID: 492 RVA: 0x000067E7 File Offset: 0x000049E7
		public static Color OliveDrab
		{
			get
			{
				return new Color(KnownColor.OliveDrab);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFA500.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000067F0 File Offset: 0x000049F0
		public static Color Orange
		{
			get
			{
				return new Color(KnownColor.Orange);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF4500.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000067F9 File Offset: 0x000049F9
		public static Color OrangeRed
		{
			get
			{
				return new Color(KnownColor.OrangeRed);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDA70D6.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00006805 File Offset: 0x00004A05
		public static Color Orchid
		{
			get
			{
				return new Color(KnownColor.Orchid);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFEEE8AA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00006811 File Offset: 0x00004A11
		public static Color PaleGoldenrod
		{
			get
			{
				return new Color(KnownColor.PaleGoldenrod);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF98FB98.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000681D File Offset: 0x00004A1D
		public static Color PaleGreen
		{
			get
			{
				return new Color(KnownColor.PaleGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFAFEEEE.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00006829 File Offset: 0x00004A29
		public static Color PaleTurquoise
		{
			get
			{
				return new Color(KnownColor.PaleTurquoise);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDB7093.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00006835 File Offset: 0x00004A35
		public static Color PaleVioletRed
		{
			get
			{
				return new Color(KnownColor.PaleVioletRed);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFEFD5.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00006841 File Offset: 0x00004A41
		public static Color PapayaWhip
		{
			get
			{
				return new Color(KnownColor.PapayaWhip);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFDAB9.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000684D File Offset: 0x00004A4D
		public static Color PeachPuff
		{
			get
			{
				return new Color(KnownColor.PeachPuff);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFCD853F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00006859 File Offset: 0x00004A59
		public static Color Peru
		{
			get
			{
				return new Color(KnownColor.Peru);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFC0CB.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00006865 File Offset: 0x00004A65
		public static Color Pink
		{
			get
			{
				return new Color(KnownColor.Pink);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDDA0DD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00006871 File Offset: 0x00004A71
		public static Color Plum
		{
			get
			{
				return new Color(KnownColor.Plum);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFB0E0E6.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000687D File Offset: 0x00004A7D
		public static Color PowderBlue
		{
			get
			{
				return new Color(KnownColor.PowderBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF800080.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00006889 File Offset: 0x00004A89
		public static Color Purple
		{
			get
			{
				return new Color(KnownColor.Purple);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF0000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00006895 File Offset: 0x00004A95
		public static Color Red
		{
			get
			{
				return new Color(KnownColor.Red);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFBC8F8F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000068A1 File Offset: 0x00004AA1
		public static Color RosyBrown
		{
			get
			{
				return new Color(KnownColor.RosyBrown);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF4169E1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000068AD File Offset: 0x00004AAD
		public static Color RoyalBlue
		{
			get
			{
				return new Color(KnownColor.RoyalBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF8B4513.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001FE RID: 510 RVA: 0x000068B9 File Offset: 0x00004AB9
		public static Color SaddleBrown
		{
			get
			{
				return new Color(KnownColor.SaddleBrown);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFA8072.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000068C5 File Offset: 0x00004AC5
		public static Color Salmon
		{
			get
			{
				return new Color(KnownColor.Salmon);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF4A460.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000200 RID: 512 RVA: 0x000068D1 File Offset: 0x00004AD1
		public static Color SandyBrown
		{
			get
			{
				return new Color(KnownColor.SandyBrown);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF2E8B57.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000201 RID: 513 RVA: 0x000068DD File Offset: 0x00004ADD
		public static Color SeaGreen
		{
			get
			{
				return new Color(KnownColor.SeaGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFF5EE.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000202 RID: 514 RVA: 0x000068E9 File Offset: 0x00004AE9
		public static Color SeaShell
		{
			get
			{
				return new Color(KnownColor.SeaShell);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFA0522D.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000068F5 File Offset: 0x00004AF5
		public static Color Sienna
		{
			get
			{
				return new Color(KnownColor.Sienna);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFC0C0C0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00006901 File Offset: 0x00004B01
		public static Color Silver
		{
			get
			{
				return new Color(KnownColor.Silver);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF87CEEB.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000690D File Offset: 0x00004B0D
		public static Color SkyBlue
		{
			get
			{
				return new Color(KnownColor.SkyBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF6A5ACD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00006919 File Offset: 0x00004B19
		public static Color SlateBlue
		{
			get
			{
				return new Color(KnownColor.SlateBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF708090.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00006925 File Offset: 0x00004B25
		public static Color SlateGray
		{
			get
			{
				return new Color(KnownColor.SlateGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFAFA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00006931 File Offset: 0x00004B31
		public static Color Snow
		{
			get
			{
				return new Color(KnownColor.Snow);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00FF7F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000693D File Offset: 0x00004B3D
		public static Color SpringGreen
		{
			get
			{
				return new Color(KnownColor.SpringGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF4682B4.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00006949 File Offset: 0x00004B49
		public static Color SteelBlue
		{
			get
			{
				return new Color(KnownColor.SteelBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFD2B48C.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00006955 File Offset: 0x00004B55
		public static Color Tan
		{
			get
			{
				return new Color(KnownColor.Tan);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF008080.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00006961 File Offset: 0x00004B61
		public static Color Teal
		{
			get
			{
				return new Color(KnownColor.Teal);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFD8BFD8.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000696D File Offset: 0x00004B6D
		public static Color Thistle
		{
			get
			{
				return new Color(KnownColor.Thistle);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF6347.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00006979 File Offset: 0x00004B79
		public static Color Tomato
		{
			get
			{
				return new Color(KnownColor.Tomato);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF40E0D0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00006985 File Offset: 0x00004B85
		public static Color Turquoise
		{
			get
			{
				return new Color(KnownColor.Turquoise);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFEE82EE.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00006991 File Offset: 0x00004B91
		public static Color Violet
		{
			get
			{
				return new Color(KnownColor.Violet);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF5DEB3.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000699D File Offset: 0x00004B9D
		public static Color Wheat
		{
			get
			{
				return new Color(KnownColor.Wheat);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000069A9 File Offset: 0x00004BA9
		public static Color White
		{
			get
			{
				return new Color(KnownColor.White);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF5F5F5.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000069B5 File Offset: 0x00004BB5
		public static Color WhiteSmoke
		{
			get
			{
				return new Color(KnownColor.WhiteSmoke);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFF00.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000069C1 File Offset: 0x00004BC1
		public static Color Yellow
		{
			get
			{
				return new Color(KnownColor.Yellow);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF9ACD32.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000069CD File Offset: 0x00004BCD
		public static Color YellowGreen
		{
			get
			{
				return new Color(KnownColor.YellowGreen);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000069D9 File Offset: 0x00004BD9
		internal Color(KnownColor knownColor)
		{
			this.value = 0L;
			this.state = 1;
			this.name = null;
			this.knownColor = (short)knownColor;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000069F9 File Offset: 0x00004BF9
		private Color(long value, short state, string name, KnownColor knownColor)
		{
			this.value = value;
			this.state = state;
			this.name = name;
			this.knownColor = (short)knownColor;
		}

		/// <summary>Gets the red component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The red component value of this <see cref="T:System.Drawing.Color" />.</returns>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00006A19 File Offset: 0x00004C19
		public byte R
		{
			get
			{
				return (byte)(this.Value >> 16 & 255L);
			}
		}

		/// <summary>Gets the green component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The green component value of this <see cref="T:System.Drawing.Color" />.</returns>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00006A2C File Offset: 0x00004C2C
		public byte G
		{
			get
			{
				return (byte)(this.Value >> 8 & 255L);
			}
		}

		/// <summary>Gets the blue component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The blue component value of this <see cref="T:System.Drawing.Color" />.</returns>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00006A3E File Offset: 0x00004C3E
		public byte B
		{
			get
			{
				return (byte)(this.Value & 255L);
			}
		}

		/// <summary>Gets the alpha component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The alpha component value of this <see cref="T:System.Drawing.Color" />.</returns>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00006A4E File Offset: 0x00004C4E
		public byte A
		{
			get
			{
				return (byte)(this.Value >> 24 & 255L);
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Color" /> structure is a predefined color. Predefined colors are represented by the elements of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Drawing.Color" /> was created from a predefined color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00006A61 File Offset: 0x00004C61
		public bool IsKnownColor
		{
			get
			{
				return (this.state & 1) != 0;
			}
		}

		/// <summary>Specifies whether this <see cref="T:System.Drawing.Color" /> structure is uninitialized.</summary>
		/// <returns>This property returns <see langword="true" /> if this color is uninitialized; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00006A6E File Offset: 0x00004C6E
		public bool IsEmpty
		{
			get
			{
				return this.state == 0;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Color" /> structure is a named color or a member of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Drawing.Color" /> was created by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00006A79 File Offset: 0x00004C79
		public bool IsNamedColor
		{
			get
			{
				return (this.state & 8) != 0 || this.IsKnownColor;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Color" /> structure is a system color. A system color is a color that is used in a Windows display element. System colors are represented by elements of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Drawing.Color" /> was created from a system color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00006A8D File Offset: 0x00004C8D
		public bool IsSystemColor
		{
			get
			{
				return this.IsKnownColor && (this.knownColor <= 26 || this.knownColor > 167);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00006AB4 File Offset: 0x00004CB4
		private string NameAndARGBValue
		{
			get
			{
				return string.Format("{{Name={0}, ARGB=({1}, {2}, {3}, {4})}}", new object[]
				{
					this.Name,
					this.A,
					this.R,
					this.G,
					this.B
				});
			}
		}

		/// <summary>Gets the name of this <see cref="T:System.Drawing.Color" />.</summary>
		/// <returns>The name of this <see cref="T:System.Drawing.Color" />.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00006B12 File Offset: 0x00004D12
		public string Name
		{
			get
			{
				if ((this.state & 8) != 0)
				{
					return this.name;
				}
				if (this.IsKnownColor)
				{
					return KnownColorTable.KnownColorToName((KnownColor)this.knownColor);
				}
				return Convert.ToString(this.value, 16);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00006B46 File Offset: 0x00004D46
		private long Value
		{
			get
			{
				if ((this.state & 2) != 0)
				{
					return this.value;
				}
				if (this.IsKnownColor)
				{
					return (long)KnownColorTable.KnownColorToArgb((KnownColor)this.knownColor);
				}
				return 0L;
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006B70 File Offset: 0x00004D70
		private static void CheckByte(int value, string name)
		{
			if (value < 0 || value > 255)
			{
				throw new ArgumentException(SR.Format("Value of '{1}' is not valid for '{0}'. '{0}' should be greater than or equal to {2} and less than or equal to {3}.", new object[]
				{
					name,
					value,
					0,
					255
				}));
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006BC2 File Offset: 0x00004DC2
		private static long MakeArgb(byte alpha, byte red, byte green, byte blue)
		{
			return (long)((ulong)((int)red << 16 | (int)green << 8 | (int)blue | (int)alpha << 24) & (ulong)-1);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from a 32-bit ARGB value.</summary>
		/// <param name="argb">A value specifying the 32-bit ARGB value.</param>
		/// <returns>The <see cref="T:System.Drawing.Color" /> structure that this method creates.</returns>
		// Token: 0x06000225 RID: 549 RVA: 0x00006BD7 File Offset: 0x00004DD7
		public static Color FromArgb(int argb)
		{
			return new Color((long)argb & (long)((ulong)-1), 2, null, (KnownColor)0);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the four ARGB component (alpha, red, green, and blue) values. Although this method allows a 32-bit value to be passed for each component, the value of each component is limited to 8 bits.</summary>
		/// <param name="alpha">The alpha component. Valid values are 0 through 255.</param>
		/// <param name="red">The red component. Valid values are 0 through 255.</param>
		/// <param name="green">The green component. Valid values are 0 through 255.</param>
		/// <param name="blue">The blue component. Valid values are 0 through 255.</param>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="alpha" />, <paramref name="red" />, <paramref name="green" />, or <paramref name="blue" /> is less than 0 or greater than 255.</exception>
		// Token: 0x06000226 RID: 550 RVA: 0x00006BE8 File Offset: 0x00004DE8
		public static Color FromArgb(int alpha, int red, int green, int blue)
		{
			Color.CheckByte(alpha, "alpha");
			Color.CheckByte(red, "red");
			Color.CheckByte(green, "green");
			Color.CheckByte(blue, "blue");
			return new Color(Color.MakeArgb((byte)alpha, (byte)red, (byte)green, (byte)blue), 2, null, (KnownColor)0);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified <see cref="T:System.Drawing.Color" /> structure, but with the new specified alpha value. Although this method allows a 32-bit value to be passed for the alpha value, the value is limited to 8 bits.</summary>
		/// <param name="alpha">The alpha value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255.</param>
		/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> from which to create the new <see cref="T:System.Drawing.Color" />.</param>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="alpha" /> is less than 0 or greater than 255.</exception>
		// Token: 0x06000227 RID: 551 RVA: 0x00006C36 File Offset: 0x00004E36
		public static Color FromArgb(int alpha, Color baseColor)
		{
			Color.CheckByte(alpha, "alpha");
			return new Color(Color.MakeArgb((byte)alpha, baseColor.R, baseColor.G, baseColor.B), 2, null, (KnownColor)0);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified 8-bit color values (red, green, and blue). The alpha value is implicitly 255 (fully opaque). Although this method allows a 32-bit value to be passed for each color component, the value of each component is limited to 8 bits.</summary>
		/// <param name="red">The red component value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255.</param>
		/// <param name="green">The green component value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255.</param>
		/// <param name="blue">The blue component value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255.</param>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="red" />, <paramref name="green" />, or <paramref name="blue" /> is less than 0 or greater than 255.</exception>
		// Token: 0x06000228 RID: 552 RVA: 0x00006C67 File Offset: 0x00004E67
		public static Color FromArgb(int red, int green, int blue)
		{
			return Color.FromArgb(255, red, green, blue);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified predefined color.</summary>
		/// <param name="color">An element of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</param>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
		// Token: 0x06000229 RID: 553 RVA: 0x00006C76 File Offset: 0x00004E76
		public static Color FromKnownColor(KnownColor color)
		{
			if (color > (KnownColor)0 && color <= KnownColor.MenuHighlight)
			{
				return new Color(color);
			}
			return Color.FromName(color.ToString());
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified name of a predefined color.</summary>
		/// <param name="name">A string that is the name of a predefined color. Valid names are the same as the names of the elements of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</param>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
		// Token: 0x0600022A RID: 554 RVA: 0x00006CA0 File Offset: 0x00004EA0
		public static Color FromName(string name)
		{
			Color result;
			if (ColorTable.TryGetNamedColor(name, out result))
			{
				return result;
			}
			return new Color(0L, 8, name, (KnownColor)0);
		}

		/// <summary>Gets the hue-saturation-lightness (HSL) lightness value for this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The lightness of this <see cref="T:System.Drawing.Color" />. The lightness ranges from 0.0 through 1.0, where 0.0 represents black and 1.0 represents white.</returns>
		// Token: 0x0600022B RID: 555 RVA: 0x00006CC4 File Offset: 0x00004EC4
		public float GetBrightness()
		{
			float num = (float)this.R / 255f;
			float num2 = (float)this.G / 255f;
			float num3 = (float)this.B / 255f;
			float num4 = num;
			float num5 = num;
			if (num2 > num4)
			{
				num4 = num2;
			}
			else if (num2 < num5)
			{
				num5 = num2;
			}
			if (num3 > num4)
			{
				num4 = num3;
			}
			else if (num3 < num5)
			{
				num5 = num3;
			}
			return (num4 + num5) / 2f;
		}

		/// <summary>Gets the hue-saturation-lightness (HSL) hue value, in degrees, for this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The hue, in degrees, of this <see cref="T:System.Drawing.Color" />. The hue is measured in degrees, ranging from 0.0 through 360.0, in HSL color space.</returns>
		// Token: 0x0600022C RID: 556 RVA: 0x00006D24 File Offset: 0x00004F24
		public float GetHue()
		{
			if (this.R == this.G && this.G == this.B)
			{
				return 0f;
			}
			float num = (float)this.R / 255f;
			float num2 = (float)this.G / 255f;
			float num3 = (float)this.B / 255f;
			float num4 = num;
			float num5 = num;
			if (num2 > num4)
			{
				num4 = num2;
			}
			else if (num2 < num5)
			{
				num5 = num2;
			}
			if (num3 > num4)
			{
				num4 = num3;
			}
			else if (num3 < num5)
			{
				num5 = num3;
			}
			float num6 = num4 - num5;
			float num7;
			if (num == num4)
			{
				num7 = (num2 - num3) / num6;
			}
			else if (num2 == num4)
			{
				num7 = 2f + (num3 - num) / num6;
			}
			else
			{
				num7 = 4f + (num - num2) / num6;
			}
			num7 *= 60f;
			if (num7 < 0f)
			{
				num7 += 360f;
			}
			return num7;
		}

		/// <summary>Gets the hue-saturation-lightness (HSL) saturation value for this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The saturation of this <see cref="T:System.Drawing.Color" />. The saturation ranges from 0.0 through 1.0, where 0.0 is grayscale and 1.0 is the most saturated.</returns>
		// Token: 0x0600022D RID: 557 RVA: 0x00006DF8 File Offset: 0x00004FF8
		public float GetSaturation()
		{
			float num = (float)this.R / 255f;
			float num2 = (float)this.G / 255f;
			float num3 = (float)this.B / 255f;
			float result = 0f;
			float num4 = num;
			float num5 = num;
			if (num2 > num4)
			{
				num4 = num2;
			}
			else if (num2 < num5)
			{
				num5 = num2;
			}
			if (num3 > num4)
			{
				num4 = num3;
			}
			else if (num3 < num5)
			{
				num5 = num3;
			}
			if (num4 != num5)
			{
				if ((double)((num4 + num5) / 2f) <= 0.5)
				{
					result = (num4 - num5) / (num4 + num5);
				}
				else
				{
					result = (num4 - num5) / (2f - num4 - num5);
				}
			}
			return result;
		}

		/// <summary>Gets the 32-bit ARGB value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The 32-bit ARGB value of this <see cref="T:System.Drawing.Color" />.</returns>
		// Token: 0x0600022E RID: 558 RVA: 0x00006E90 File Offset: 0x00005090
		public int ToArgb()
		{
			return (int)this.Value;
		}

		/// <summary>Gets the <see cref="T:System.Drawing.KnownColor" /> value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>An element of the <see cref="T:System.Drawing.KnownColor" /> enumeration, if the <see cref="T:System.Drawing.Color" /> is created from a predefined color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, 0.</returns>
		// Token: 0x0600022F RID: 559 RVA: 0x00006E99 File Offset: 0x00005099
		public KnownColor ToKnownColor()
		{
			return (KnownColor)this.knownColor;
		}

		/// <summary>Converts this <see cref="T:System.Drawing.Color" /> structure to a human-readable string.</summary>
		/// <returns>A string that is the name of this <see cref="T:System.Drawing.Color" />, if the <see cref="T:System.Drawing.Color" /> is created from a predefined color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, a string that consists of the ARGB component names and their values.</returns>
		// Token: 0x06000230 RID: 560 RVA: 0x00006EA4 File Offset: 0x000050A4
		public override string ToString()
		{
			if ((this.state & 8) != 0 || (this.state & 1) != 0)
			{
				return "Color [" + this.Name + "]";
			}
			if ((this.state & 2) != 0)
			{
				return string.Concat(new string[]
				{
					"Color [A=",
					this.A.ToString(),
					", R=",
					this.R.ToString(),
					", G=",
					this.G.ToString(),
					", B=",
					this.B.ToString(),
					"]"
				});
			}
			return "Color [Empty]";
		}

		/// <summary>Tests whether two specified <see cref="T:System.Drawing.Color" /> structures are equivalent.</summary>
		/// <param name="left">The <see cref="T:System.Drawing.Color" /> that is to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.Drawing.Color" /> that is to the right of the equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Drawing.Color" /> structures are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000231 RID: 561 RVA: 0x00006F63 File Offset: 0x00005163
		public static bool operator ==(Color left, Color right)
		{
			return left.value == right.value && left.state == right.state && left.knownColor == right.knownColor && left.name == right.name;
		}

		/// <summary>Tests whether two specified <see cref="T:System.Drawing.Color" /> structures are different.</summary>
		/// <param name="left">The <see cref="T:System.Drawing.Color" /> that is to the left of the inequality operator.</param>
		/// <param name="right">The <see cref="T:System.Drawing.Color" /> that is to the right of the inequality operator.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Drawing.Color" /> structures are different; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000232 RID: 562 RVA: 0x00006FA2 File Offset: 0x000051A2
		public static bool operator !=(Color left, Color right)
		{
			return !(left == right);
		}

		/// <summary>Tests whether the specified object is a <see cref="T:System.Drawing.Color" /> structure and is equivalent to this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <param name="obj">The object to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.Color" /> structure equivalent to this <see cref="T:System.Drawing.Color" /> structure; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000233 RID: 563 RVA: 0x00006FAE File Offset: 0x000051AE
		public override bool Equals(object obj)
		{
			return obj is Color && this.Equals((Color)obj);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00006FC6 File Offset: 0x000051C6
		public bool Equals(Color other)
		{
			return this == other;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>An integer value that specifies the hash code for this <see cref="T:System.Drawing.Color" />.</returns>
		// Token: 0x06000235 RID: 565 RVA: 0x00006FD4 File Offset: 0x000051D4
		public override int GetHashCode()
		{
			if (this.name != null & !this.IsKnownColor)
			{
				return this.name.GetHashCode();
			}
			return HashHelpers.Combine(HashHelpers.Combine(this.value.GetHashCode(), this.state.GetHashCode()), this.knownColor.GetHashCode());
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000049FE File Offset: 0x00002BFE
		// Note: this type is marked as 'beforefieldinit'.
		static Color()
		{
		}

		/// <summary>Represents a color that is <see langword="null" />.</summary>
		// Token: 0x0400037D RID: 893
		public static readonly Color Empty;

		// Token: 0x0400037E RID: 894
		private const short StateKnownColorValid = 1;

		// Token: 0x0400037F RID: 895
		private const short StateARGBValueValid = 2;

		// Token: 0x04000380 RID: 896
		private const short StateValueMask = 2;

		// Token: 0x04000381 RID: 897
		private const short StateNameValid = 8;

		// Token: 0x04000382 RID: 898
		private const long NotDefinedValue = 0L;

		// Token: 0x04000383 RID: 899
		private const int ARGBAlphaShift = 24;

		// Token: 0x04000384 RID: 900
		private const int ARGBRedShift = 16;

		// Token: 0x04000385 RID: 901
		private const int ARGBGreenShift = 8;

		// Token: 0x04000386 RID: 902
		private const int ARGBBlueShift = 0;

		// Token: 0x04000387 RID: 903
		private readonly string name;

		// Token: 0x04000388 RID: 904
		private readonly long value;

		// Token: 0x04000389 RID: 905
		private readonly short knownColor;

		// Token: 0x0400038A RID: 906
		private readonly short state;
	}
}
