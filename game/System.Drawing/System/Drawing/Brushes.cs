using System;

namespace System.Drawing
{
	/// <summary>Brushes for all the standard colors. This class cannot be inherited.</summary>
	// Token: 0x0200004E RID: 78
	public sealed class Brushes
	{
		// Token: 0x06000312 RID: 786 RVA: 0x00002050 File Offset: 0x00000250
		private Brushes()
		{
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00008A67 File Offset: 0x00006C67
		public static Brush AliceBlue
		{
			get
			{
				if (Brushes.aliceBlue == null)
				{
					Brushes.aliceBlue = new SolidBrush(Color.AliceBlue);
				}
				return Brushes.aliceBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00008A84 File Offset: 0x00006C84
		public static Brush AntiqueWhite
		{
			get
			{
				if (Brushes.antiqueWhite == null)
				{
					Brushes.antiqueWhite = new SolidBrush(Color.AntiqueWhite);
				}
				return Brushes.antiqueWhite;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00008AA1 File Offset: 0x00006CA1
		public static Brush Aqua
		{
			get
			{
				if (Brushes.aqua == null)
				{
					Brushes.aqua = new SolidBrush(Color.Aqua);
				}
				return Brushes.aqua;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00008ABE File Offset: 0x00006CBE
		public static Brush Aquamarine
		{
			get
			{
				if (Brushes.aquamarine == null)
				{
					Brushes.aquamarine = new SolidBrush(Color.Aquamarine);
				}
				return Brushes.aquamarine;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00008ADB File Offset: 0x00006CDB
		public static Brush Azure
		{
			get
			{
				if (Brushes.azure == null)
				{
					Brushes.azure = new SolidBrush(Color.Azure);
				}
				return Brushes.azure;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00008AF8 File Offset: 0x00006CF8
		public static Brush Beige
		{
			get
			{
				if (Brushes.beige == null)
				{
					Brushes.beige = new SolidBrush(Color.Beige);
				}
				return Brushes.beige;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00008B15 File Offset: 0x00006D15
		public static Brush Bisque
		{
			get
			{
				if (Brushes.bisque == null)
				{
					Brushes.bisque = new SolidBrush(Color.Bisque);
				}
				return Brushes.bisque;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00008B32 File Offset: 0x00006D32
		public static Brush Black
		{
			get
			{
				if (Brushes.black == null)
				{
					Brushes.black = new SolidBrush(Color.Black);
				}
				return Brushes.black;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00008B4F File Offset: 0x00006D4F
		public static Brush BlanchedAlmond
		{
			get
			{
				if (Brushes.blanchedAlmond == null)
				{
					Brushes.blanchedAlmond = new SolidBrush(Color.BlanchedAlmond);
				}
				return Brushes.blanchedAlmond;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00008B6C File Offset: 0x00006D6C
		public static Brush Blue
		{
			get
			{
				if (Brushes.blue == null)
				{
					Brushes.blue = new SolidBrush(Color.Blue);
				}
				return Brushes.blue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00008B89 File Offset: 0x00006D89
		public static Brush BlueViolet
		{
			get
			{
				if (Brushes.blueViolet == null)
				{
					Brushes.blueViolet = new SolidBrush(Color.BlueViolet);
				}
				return Brushes.blueViolet;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00008BA6 File Offset: 0x00006DA6
		public static Brush Brown
		{
			get
			{
				if (Brushes.brown == null)
				{
					Brushes.brown = new SolidBrush(Color.Brown);
				}
				return Brushes.brown;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00008BC3 File Offset: 0x00006DC3
		public static Brush BurlyWood
		{
			get
			{
				if (Brushes.burlyWood == null)
				{
					Brushes.burlyWood = new SolidBrush(Color.BurlyWood);
				}
				return Brushes.burlyWood;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00008BE0 File Offset: 0x00006DE0
		public static Brush CadetBlue
		{
			get
			{
				if (Brushes.cadetBlue == null)
				{
					Brushes.cadetBlue = new SolidBrush(Color.CadetBlue);
				}
				return Brushes.cadetBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00008BFD File Offset: 0x00006DFD
		public static Brush Chartreuse
		{
			get
			{
				if (Brushes.chartreuse == null)
				{
					Brushes.chartreuse = new SolidBrush(Color.Chartreuse);
				}
				return Brushes.chartreuse;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00008C1A File Offset: 0x00006E1A
		public static Brush Chocolate
		{
			get
			{
				if (Brushes.chocolate == null)
				{
					Brushes.chocolate = new SolidBrush(Color.Chocolate);
				}
				return Brushes.chocolate;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00008C37 File Offset: 0x00006E37
		public static Brush Coral
		{
			get
			{
				if (Brushes.coral == null)
				{
					Brushes.coral = new SolidBrush(Color.Coral);
				}
				return Brushes.coral;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00008C54 File Offset: 0x00006E54
		public static Brush CornflowerBlue
		{
			get
			{
				if (Brushes.cornflowerBlue == null)
				{
					Brushes.cornflowerBlue = new SolidBrush(Color.CornflowerBlue);
				}
				return Brushes.cornflowerBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00008C71 File Offset: 0x00006E71
		public static Brush Cornsilk
		{
			get
			{
				if (Brushes.cornsilk == null)
				{
					Brushes.cornsilk = new SolidBrush(Color.Cornsilk);
				}
				return Brushes.cornsilk;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00008C8E File Offset: 0x00006E8E
		public static Brush Crimson
		{
			get
			{
				if (Brushes.crimson == null)
				{
					Brushes.crimson = new SolidBrush(Color.Crimson);
				}
				return Brushes.crimson;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00008CAB File Offset: 0x00006EAB
		public static Brush Cyan
		{
			get
			{
				if (Brushes.cyan == null)
				{
					Brushes.cyan = new SolidBrush(Color.Cyan);
				}
				return Brushes.cyan;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00008CC8 File Offset: 0x00006EC8
		public static Brush DarkBlue
		{
			get
			{
				if (Brushes.darkBlue == null)
				{
					Brushes.darkBlue = new SolidBrush(Color.DarkBlue);
				}
				return Brushes.darkBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00008CE5 File Offset: 0x00006EE5
		public static Brush DarkCyan
		{
			get
			{
				if (Brushes.darkCyan == null)
				{
					Brushes.darkCyan = new SolidBrush(Color.DarkCyan);
				}
				return Brushes.darkCyan;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00008D02 File Offset: 0x00006F02
		public static Brush DarkGoldenrod
		{
			get
			{
				if (Brushes.darkGoldenrod == null)
				{
					Brushes.darkGoldenrod = new SolidBrush(Color.DarkGoldenrod);
				}
				return Brushes.darkGoldenrod;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00008D1F File Offset: 0x00006F1F
		public static Brush DarkGray
		{
			get
			{
				if (Brushes.darkGray == null)
				{
					Brushes.darkGray = new SolidBrush(Color.DarkGray);
				}
				return Brushes.darkGray;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00008D3C File Offset: 0x00006F3C
		public static Brush DarkGreen
		{
			get
			{
				if (Brushes.darkGreen == null)
				{
					Brushes.darkGreen = new SolidBrush(Color.DarkGreen);
				}
				return Brushes.darkGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00008D59 File Offset: 0x00006F59
		public static Brush DarkKhaki
		{
			get
			{
				if (Brushes.darkKhaki == null)
				{
					Brushes.darkKhaki = new SolidBrush(Color.DarkKhaki);
				}
				return Brushes.darkKhaki;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00008D76 File Offset: 0x00006F76
		public static Brush DarkMagenta
		{
			get
			{
				if (Brushes.darkMagenta == null)
				{
					Brushes.darkMagenta = new SolidBrush(Color.DarkMagenta);
				}
				return Brushes.darkMagenta;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00008D93 File Offset: 0x00006F93
		public static Brush DarkOliveGreen
		{
			get
			{
				if (Brushes.darkOliveGreen == null)
				{
					Brushes.darkOliveGreen = new SolidBrush(Color.DarkOliveGreen);
				}
				return Brushes.darkOliveGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00008DB0 File Offset: 0x00006FB0
		public static Brush DarkOrange
		{
			get
			{
				if (Brushes.darkOrange == null)
				{
					Brushes.darkOrange = new SolidBrush(Color.DarkOrange);
				}
				return Brushes.darkOrange;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00008DCD File Offset: 0x00006FCD
		public static Brush DarkOrchid
		{
			get
			{
				if (Brushes.darkOrchid == null)
				{
					Brushes.darkOrchid = new SolidBrush(Color.DarkOrchid);
				}
				return Brushes.darkOrchid;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00008DEA File Offset: 0x00006FEA
		public static Brush DarkRed
		{
			get
			{
				if (Brushes.darkRed == null)
				{
					Brushes.darkRed = new SolidBrush(Color.DarkRed);
				}
				return Brushes.darkRed;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00008E07 File Offset: 0x00007007
		public static Brush DarkSalmon
		{
			get
			{
				if (Brushes.darkSalmon == null)
				{
					Brushes.darkSalmon = new SolidBrush(Color.DarkSalmon);
				}
				return Brushes.darkSalmon;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00008E24 File Offset: 0x00007024
		public static Brush DarkSeaGreen
		{
			get
			{
				if (Brushes.darkSeaGreen == null)
				{
					Brushes.darkSeaGreen = new SolidBrush(Color.DarkSeaGreen);
				}
				return Brushes.darkSeaGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00008E41 File Offset: 0x00007041
		public static Brush DarkSlateBlue
		{
			get
			{
				if (Brushes.darkSlateBlue == null)
				{
					Brushes.darkSlateBlue = new SolidBrush(Color.DarkSlateBlue);
				}
				return Brushes.darkSlateBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00008E5E File Offset: 0x0000705E
		public static Brush DarkSlateGray
		{
			get
			{
				if (Brushes.darkSlateGray == null)
				{
					Brushes.darkSlateGray = new SolidBrush(Color.DarkSlateGray);
				}
				return Brushes.darkSlateGray;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00008E7B File Offset: 0x0000707B
		public static Brush DarkTurquoise
		{
			get
			{
				if (Brushes.darkTurquoise == null)
				{
					Brushes.darkTurquoise = new SolidBrush(Color.DarkTurquoise);
				}
				return Brushes.darkTurquoise;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00008E98 File Offset: 0x00007098
		public static Brush DarkViolet
		{
			get
			{
				if (Brushes.darkViolet == null)
				{
					Brushes.darkViolet = new SolidBrush(Color.DarkViolet);
				}
				return Brushes.darkViolet;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00008EB5 File Offset: 0x000070B5
		public static Brush DeepPink
		{
			get
			{
				if (Brushes.deepPink == null)
				{
					Brushes.deepPink = new SolidBrush(Color.DeepPink);
				}
				return Brushes.deepPink;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00008ED2 File Offset: 0x000070D2
		public static Brush DeepSkyBlue
		{
			get
			{
				if (Brushes.deepSkyBlue == null)
				{
					Brushes.deepSkyBlue = new SolidBrush(Color.DeepSkyBlue);
				}
				return Brushes.deepSkyBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00008EEF File Offset: 0x000070EF
		public static Brush DimGray
		{
			get
			{
				if (Brushes.dimGray == null)
				{
					Brushes.dimGray = new SolidBrush(Color.DimGray);
				}
				return Brushes.dimGray;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00008F0C File Offset: 0x0000710C
		public static Brush DodgerBlue
		{
			get
			{
				if (Brushes.dodgerBlue == null)
				{
					Brushes.dodgerBlue = new SolidBrush(Color.DodgerBlue);
				}
				return Brushes.dodgerBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00008F29 File Offset: 0x00007129
		public static Brush Firebrick
		{
			get
			{
				if (Brushes.firebrick == null)
				{
					Brushes.firebrick = new SolidBrush(Color.Firebrick);
				}
				return Brushes.firebrick;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00008F46 File Offset: 0x00007146
		public static Brush FloralWhite
		{
			get
			{
				if (Brushes.floralWhite == null)
				{
					Brushes.floralWhite = new SolidBrush(Color.FloralWhite);
				}
				return Brushes.floralWhite;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00008F63 File Offset: 0x00007163
		public static Brush ForestGreen
		{
			get
			{
				if (Brushes.forestGreen == null)
				{
					Brushes.forestGreen = new SolidBrush(Color.ForestGreen);
				}
				return Brushes.forestGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00008F80 File Offset: 0x00007180
		public static Brush Fuchsia
		{
			get
			{
				if (Brushes.fuchsia == null)
				{
					Brushes.fuchsia = new SolidBrush(Color.Fuchsia);
				}
				return Brushes.fuchsia;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00008F9D File Offset: 0x0000719D
		public static Brush Gainsboro
		{
			get
			{
				if (Brushes.gainsboro == null)
				{
					Brushes.gainsboro = new SolidBrush(Color.Gainsboro);
				}
				return Brushes.gainsboro;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00008FBA File Offset: 0x000071BA
		public static Brush GhostWhite
		{
			get
			{
				if (Brushes.ghostWhite == null)
				{
					Brushes.ghostWhite = new SolidBrush(Color.GhostWhite);
				}
				return Brushes.ghostWhite;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00008FD7 File Offset: 0x000071D7
		public static Brush Gold
		{
			get
			{
				if (Brushes.gold == null)
				{
					Brushes.gold = new SolidBrush(Color.Gold);
				}
				return Brushes.gold;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00008FF4 File Offset: 0x000071F4
		public static Brush Goldenrod
		{
			get
			{
				if (Brushes.goldenrod == null)
				{
					Brushes.goldenrod = new SolidBrush(Color.Goldenrod);
				}
				return Brushes.goldenrod;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00009011 File Offset: 0x00007211
		public static Brush Gray
		{
			get
			{
				if (Brushes.gray == null)
				{
					Brushes.gray = new SolidBrush(Color.Gray);
				}
				return Brushes.gray;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000902E File Offset: 0x0000722E
		public static Brush Green
		{
			get
			{
				if (Brushes.green == null)
				{
					Brushes.green = new SolidBrush(Color.Green);
				}
				return Brushes.green;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000904B File Offset: 0x0000724B
		public static Brush GreenYellow
		{
			get
			{
				if (Brushes.greenYellow == null)
				{
					Brushes.greenYellow = new SolidBrush(Color.GreenYellow);
				}
				return Brushes.greenYellow;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00009068 File Offset: 0x00007268
		public static Brush Honeydew
		{
			get
			{
				if (Brushes.honeydew == null)
				{
					Brushes.honeydew = new SolidBrush(Color.Honeydew);
				}
				return Brushes.honeydew;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00009085 File Offset: 0x00007285
		public static Brush HotPink
		{
			get
			{
				if (Brushes.hotPink == null)
				{
					Brushes.hotPink = new SolidBrush(Color.HotPink);
				}
				return Brushes.hotPink;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600034A RID: 842 RVA: 0x000090A2 File Offset: 0x000072A2
		public static Brush IndianRed
		{
			get
			{
				if (Brushes.indianRed == null)
				{
					Brushes.indianRed = new SolidBrush(Color.IndianRed);
				}
				return Brushes.indianRed;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600034B RID: 843 RVA: 0x000090BF File Offset: 0x000072BF
		public static Brush Indigo
		{
			get
			{
				if (Brushes.indigo == null)
				{
					Brushes.indigo = new SolidBrush(Color.Indigo);
				}
				return Brushes.indigo;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600034C RID: 844 RVA: 0x000090DC File Offset: 0x000072DC
		public static Brush Ivory
		{
			get
			{
				if (Brushes.ivory == null)
				{
					Brushes.ivory = new SolidBrush(Color.Ivory);
				}
				return Brushes.ivory;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600034D RID: 845 RVA: 0x000090F9 File Offset: 0x000072F9
		public static Brush Khaki
		{
			get
			{
				if (Brushes.khaki == null)
				{
					Brushes.khaki = new SolidBrush(Color.Khaki);
				}
				return Brushes.khaki;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00009116 File Offset: 0x00007316
		public static Brush Lavender
		{
			get
			{
				if (Brushes.lavender == null)
				{
					Brushes.lavender = new SolidBrush(Color.Lavender);
				}
				return Brushes.lavender;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00009133 File Offset: 0x00007333
		public static Brush LavenderBlush
		{
			get
			{
				if (Brushes.lavenderBlush == null)
				{
					Brushes.lavenderBlush = new SolidBrush(Color.LavenderBlush);
				}
				return Brushes.lavenderBlush;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00009150 File Offset: 0x00007350
		public static Brush LawnGreen
		{
			get
			{
				if (Brushes.lawnGreen == null)
				{
					Brushes.lawnGreen = new SolidBrush(Color.LawnGreen);
				}
				return Brushes.lawnGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000916D File Offset: 0x0000736D
		public static Brush LemonChiffon
		{
			get
			{
				if (Brushes.lemonChiffon == null)
				{
					Brushes.lemonChiffon = new SolidBrush(Color.LemonChiffon);
				}
				return Brushes.lemonChiffon;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000918A File Offset: 0x0000738A
		public static Brush LightBlue
		{
			get
			{
				if (Brushes.lightBlue == null)
				{
					Brushes.lightBlue = new SolidBrush(Color.LightBlue);
				}
				return Brushes.lightBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000353 RID: 851 RVA: 0x000091A7 File Offset: 0x000073A7
		public static Brush LightCoral
		{
			get
			{
				if (Brushes.lightCoral == null)
				{
					Brushes.lightCoral = new SolidBrush(Color.LightCoral);
				}
				return Brushes.lightCoral;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000354 RID: 852 RVA: 0x000091C4 File Offset: 0x000073C4
		public static Brush LightCyan
		{
			get
			{
				if (Brushes.lightCyan == null)
				{
					Brushes.lightCyan = new SolidBrush(Color.LightCyan);
				}
				return Brushes.lightCyan;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000355 RID: 853 RVA: 0x000091E1 File Offset: 0x000073E1
		public static Brush LightGoldenrodYellow
		{
			get
			{
				if (Brushes.lightGoldenrodYellow == null)
				{
					Brushes.lightGoldenrodYellow = new SolidBrush(Color.LightGoldenrodYellow);
				}
				return Brushes.lightGoldenrodYellow;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000356 RID: 854 RVA: 0x000091FE File Offset: 0x000073FE
		public static Brush LightGray
		{
			get
			{
				if (Brushes.lightGray == null)
				{
					Brushes.lightGray = new SolidBrush(Color.LightGray);
				}
				return Brushes.lightGray;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000921B File Offset: 0x0000741B
		public static Brush LightGreen
		{
			get
			{
				if (Brushes.lightGreen == null)
				{
					Brushes.lightGreen = new SolidBrush(Color.LightGreen);
				}
				return Brushes.lightGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00009238 File Offset: 0x00007438
		public static Brush LightPink
		{
			get
			{
				if (Brushes.lightPink == null)
				{
					Brushes.lightPink = new SolidBrush(Color.LightPink);
				}
				return Brushes.lightPink;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00009255 File Offset: 0x00007455
		public static Brush LightSalmon
		{
			get
			{
				if (Brushes.lightSalmon == null)
				{
					Brushes.lightSalmon = new SolidBrush(Color.LightSalmon);
				}
				return Brushes.lightSalmon;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00009272 File Offset: 0x00007472
		public static Brush LightSeaGreen
		{
			get
			{
				if (Brushes.lightSeaGreen == null)
				{
					Brushes.lightSeaGreen = new SolidBrush(Color.LightSeaGreen);
				}
				return Brushes.lightSeaGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000928F File Offset: 0x0000748F
		public static Brush LightSkyBlue
		{
			get
			{
				if (Brushes.lightSkyBlue == null)
				{
					Brushes.lightSkyBlue = new SolidBrush(Color.LightSkyBlue);
				}
				return Brushes.lightSkyBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600035C RID: 860 RVA: 0x000092AC File Offset: 0x000074AC
		public static Brush LightSlateGray
		{
			get
			{
				if (Brushes.lightSlateGray == null)
				{
					Brushes.lightSlateGray = new SolidBrush(Color.LightSlateGray);
				}
				return Brushes.lightSlateGray;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600035D RID: 861 RVA: 0x000092C9 File Offset: 0x000074C9
		public static Brush LightSteelBlue
		{
			get
			{
				if (Brushes.lightSteelBlue == null)
				{
					Brushes.lightSteelBlue = new SolidBrush(Color.LightSteelBlue);
				}
				return Brushes.lightSteelBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600035E RID: 862 RVA: 0x000092E6 File Offset: 0x000074E6
		public static Brush LightYellow
		{
			get
			{
				if (Brushes.lightYellow == null)
				{
					Brushes.lightYellow = new SolidBrush(Color.LightYellow);
				}
				return Brushes.lightYellow;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00009303 File Offset: 0x00007503
		public static Brush Lime
		{
			get
			{
				if (Brushes.lime == null)
				{
					Brushes.lime = new SolidBrush(Color.Lime);
				}
				return Brushes.lime;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00009320 File Offset: 0x00007520
		public static Brush LimeGreen
		{
			get
			{
				if (Brushes.limeGreen == null)
				{
					Brushes.limeGreen = new SolidBrush(Color.LimeGreen);
				}
				return Brushes.limeGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000933D File Offset: 0x0000753D
		public static Brush Linen
		{
			get
			{
				if (Brushes.linen == null)
				{
					Brushes.linen = new SolidBrush(Color.Linen);
				}
				return Brushes.linen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000935A File Offset: 0x0000755A
		public static Brush Magenta
		{
			get
			{
				if (Brushes.magenta == null)
				{
					Brushes.magenta = new SolidBrush(Color.Magenta);
				}
				return Brushes.magenta;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00009377 File Offset: 0x00007577
		public static Brush Maroon
		{
			get
			{
				if (Brushes.maroon == null)
				{
					Brushes.maroon = new SolidBrush(Color.Maroon);
				}
				return Brushes.maroon;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00009394 File Offset: 0x00007594
		public static Brush MediumAquamarine
		{
			get
			{
				if (Brushes.mediumAquamarine == null)
				{
					Brushes.mediumAquamarine = new SolidBrush(Color.MediumAquamarine);
				}
				return Brushes.mediumAquamarine;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000093B1 File Offset: 0x000075B1
		public static Brush MediumBlue
		{
			get
			{
				if (Brushes.mediumBlue == null)
				{
					Brushes.mediumBlue = new SolidBrush(Color.MediumBlue);
				}
				return Brushes.mediumBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000366 RID: 870 RVA: 0x000093CE File Offset: 0x000075CE
		public static Brush MediumOrchid
		{
			get
			{
				if (Brushes.mediumOrchid == null)
				{
					Brushes.mediumOrchid = new SolidBrush(Color.MediumOrchid);
				}
				return Brushes.mediumOrchid;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000093EB File Offset: 0x000075EB
		public static Brush MediumPurple
		{
			get
			{
				if (Brushes.mediumPurple == null)
				{
					Brushes.mediumPurple = new SolidBrush(Color.MediumPurple);
				}
				return Brushes.mediumPurple;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00009408 File Offset: 0x00007608
		public static Brush MediumSeaGreen
		{
			get
			{
				if (Brushes.mediumSeaGreen == null)
				{
					Brushes.mediumSeaGreen = new SolidBrush(Color.MediumSeaGreen);
				}
				return Brushes.mediumSeaGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00009425 File Offset: 0x00007625
		public static Brush MediumSlateBlue
		{
			get
			{
				if (Brushes.mediumSlateBlue == null)
				{
					Brushes.mediumSlateBlue = new SolidBrush(Color.MediumSlateBlue);
				}
				return Brushes.mediumSlateBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00009442 File Offset: 0x00007642
		public static Brush MediumSpringGreen
		{
			get
			{
				if (Brushes.mediumSpringGreen == null)
				{
					Brushes.mediumSpringGreen = new SolidBrush(Color.MediumSpringGreen);
				}
				return Brushes.mediumSpringGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000945F File Offset: 0x0000765F
		public static Brush MediumTurquoise
		{
			get
			{
				if (Brushes.mediumTurquoise == null)
				{
					Brushes.mediumTurquoise = new SolidBrush(Color.MediumTurquoise);
				}
				return Brushes.mediumTurquoise;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000947C File Offset: 0x0000767C
		public static Brush MediumVioletRed
		{
			get
			{
				if (Brushes.mediumVioletRed == null)
				{
					Brushes.mediumVioletRed = new SolidBrush(Color.MediumVioletRed);
				}
				return Brushes.mediumVioletRed;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00009499 File Offset: 0x00007699
		public static Brush MidnightBlue
		{
			get
			{
				if (Brushes.midnightBlue == null)
				{
					Brushes.midnightBlue = new SolidBrush(Color.MidnightBlue);
				}
				return Brushes.midnightBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600036E RID: 878 RVA: 0x000094B6 File Offset: 0x000076B6
		public static Brush MintCream
		{
			get
			{
				if (Brushes.mintCream == null)
				{
					Brushes.mintCream = new SolidBrush(Color.MintCream);
				}
				return Brushes.mintCream;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600036F RID: 879 RVA: 0x000094D3 File Offset: 0x000076D3
		public static Brush MistyRose
		{
			get
			{
				if (Brushes.mistyRose == null)
				{
					Brushes.mistyRose = new SolidBrush(Color.MistyRose);
				}
				return Brushes.mistyRose;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000370 RID: 880 RVA: 0x000094F0 File Offset: 0x000076F0
		public static Brush Moccasin
		{
			get
			{
				if (Brushes.moccasin == null)
				{
					Brushes.moccasin = new SolidBrush(Color.Moccasin);
				}
				return Brushes.moccasin;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000950D File Offset: 0x0000770D
		public static Brush NavajoWhite
		{
			get
			{
				if (Brushes.navajoWhite == null)
				{
					Brushes.navajoWhite = new SolidBrush(Color.NavajoWhite);
				}
				return Brushes.navajoWhite;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000952A File Offset: 0x0000772A
		public static Brush Navy
		{
			get
			{
				if (Brushes.navy == null)
				{
					Brushes.navy = new SolidBrush(Color.Navy);
				}
				return Brushes.navy;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00009547 File Offset: 0x00007747
		public static Brush OldLace
		{
			get
			{
				if (Brushes.oldLace == null)
				{
					Brushes.oldLace = new SolidBrush(Color.OldLace);
				}
				return Brushes.oldLace;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00009564 File Offset: 0x00007764
		public static Brush Olive
		{
			get
			{
				if (Brushes.olive == null)
				{
					Brushes.olive = new SolidBrush(Color.Olive);
				}
				return Brushes.olive;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00009581 File Offset: 0x00007781
		public static Brush OliveDrab
		{
			get
			{
				if (Brushes.oliveDrab == null)
				{
					Brushes.oliveDrab = new SolidBrush(Color.OliveDrab);
				}
				return Brushes.oliveDrab;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000959E File Offset: 0x0000779E
		public static Brush Orange
		{
			get
			{
				if (Brushes.orange == null)
				{
					Brushes.orange = new SolidBrush(Color.Orange);
				}
				return Brushes.orange;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000377 RID: 887 RVA: 0x000095BB File Offset: 0x000077BB
		public static Brush OrangeRed
		{
			get
			{
				if (Brushes.orangeRed == null)
				{
					Brushes.orangeRed = new SolidBrush(Color.OrangeRed);
				}
				return Brushes.orangeRed;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000378 RID: 888 RVA: 0x000095D8 File Offset: 0x000077D8
		public static Brush Orchid
		{
			get
			{
				if (Brushes.orchid == null)
				{
					Brushes.orchid = new SolidBrush(Color.Orchid);
				}
				return Brushes.orchid;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000379 RID: 889 RVA: 0x000095F5 File Offset: 0x000077F5
		public static Brush PaleGoldenrod
		{
			get
			{
				if (Brushes.paleGoldenrod == null)
				{
					Brushes.paleGoldenrod = new SolidBrush(Color.PaleGoldenrod);
				}
				return Brushes.paleGoldenrod;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00009612 File Offset: 0x00007812
		public static Brush PaleGreen
		{
			get
			{
				if (Brushes.paleGreen == null)
				{
					Brushes.paleGreen = new SolidBrush(Color.PaleGreen);
				}
				return Brushes.paleGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000962F File Offset: 0x0000782F
		public static Brush PaleTurquoise
		{
			get
			{
				if (Brushes.paleTurquoise == null)
				{
					Brushes.paleTurquoise = new SolidBrush(Color.PaleTurquoise);
				}
				return Brushes.paleTurquoise;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000964C File Offset: 0x0000784C
		public static Brush PaleVioletRed
		{
			get
			{
				if (Brushes.paleVioletRed == null)
				{
					Brushes.paleVioletRed = new SolidBrush(Color.PaleVioletRed);
				}
				return Brushes.paleVioletRed;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00009669 File Offset: 0x00007869
		public static Brush PapayaWhip
		{
			get
			{
				if (Brushes.papayaWhip == null)
				{
					Brushes.papayaWhip = new SolidBrush(Color.PapayaWhip);
				}
				return Brushes.papayaWhip;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00009686 File Offset: 0x00007886
		public static Brush PeachPuff
		{
			get
			{
				if (Brushes.peachPuff == null)
				{
					Brushes.peachPuff = new SolidBrush(Color.PeachPuff);
				}
				return Brushes.peachPuff;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600037F RID: 895 RVA: 0x000096A3 File Offset: 0x000078A3
		public static Brush Peru
		{
			get
			{
				if (Brushes.peru == null)
				{
					Brushes.peru = new SolidBrush(Color.Peru);
				}
				return Brushes.peru;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000380 RID: 896 RVA: 0x000096C0 File Offset: 0x000078C0
		public static Brush Pink
		{
			get
			{
				if (Brushes.pink == null)
				{
					Brushes.pink = new SolidBrush(Color.Pink);
				}
				return Brushes.pink;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000381 RID: 897 RVA: 0x000096DD File Offset: 0x000078DD
		public static Brush Plum
		{
			get
			{
				if (Brushes.plum == null)
				{
					Brushes.plum = new SolidBrush(Color.Plum);
				}
				return Brushes.plum;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000382 RID: 898 RVA: 0x000096FA File Offset: 0x000078FA
		public static Brush PowderBlue
		{
			get
			{
				if (Brushes.powderBlue == null)
				{
					Brushes.powderBlue = new SolidBrush(Color.PowderBlue);
				}
				return Brushes.powderBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00009717 File Offset: 0x00007917
		public static Brush Purple
		{
			get
			{
				if (Brushes.purple == null)
				{
					Brushes.purple = new SolidBrush(Color.Purple);
				}
				return Brushes.purple;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00009734 File Offset: 0x00007934
		public static Brush Red
		{
			get
			{
				if (Brushes.red == null)
				{
					Brushes.red = new SolidBrush(Color.Red);
				}
				return Brushes.red;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00009751 File Offset: 0x00007951
		public static Brush RosyBrown
		{
			get
			{
				if (Brushes.rosyBrown == null)
				{
					Brushes.rosyBrown = new SolidBrush(Color.RosyBrown);
				}
				return Brushes.rosyBrown;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000976E File Offset: 0x0000796E
		public static Brush RoyalBlue
		{
			get
			{
				if (Brushes.royalBlue == null)
				{
					Brushes.royalBlue = new SolidBrush(Color.RoyalBlue);
				}
				return Brushes.royalBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000978B File Offset: 0x0000798B
		public static Brush SaddleBrown
		{
			get
			{
				if (Brushes.saddleBrown == null)
				{
					Brushes.saddleBrown = new SolidBrush(Color.SaddleBrown);
				}
				return Brushes.saddleBrown;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000388 RID: 904 RVA: 0x000097A8 File Offset: 0x000079A8
		public static Brush Salmon
		{
			get
			{
				if (Brushes.salmon == null)
				{
					Brushes.salmon = new SolidBrush(Color.Salmon);
				}
				return Brushes.salmon;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000389 RID: 905 RVA: 0x000097C5 File Offset: 0x000079C5
		public static Brush SandyBrown
		{
			get
			{
				if (Brushes.sandyBrown == null)
				{
					Brushes.sandyBrown = new SolidBrush(Color.SandyBrown);
				}
				return Brushes.sandyBrown;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600038A RID: 906 RVA: 0x000097E2 File Offset: 0x000079E2
		public static Brush SeaGreen
		{
			get
			{
				if (Brushes.seaGreen == null)
				{
					Brushes.seaGreen = new SolidBrush(Color.SeaGreen);
				}
				return Brushes.seaGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600038B RID: 907 RVA: 0x000097FF File Offset: 0x000079FF
		public static Brush SeaShell
		{
			get
			{
				if (Brushes.seaShell == null)
				{
					Brushes.seaShell = new SolidBrush(Color.SeaShell);
				}
				return Brushes.seaShell;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000981C File Offset: 0x00007A1C
		public static Brush Sienna
		{
			get
			{
				if (Brushes.sienna == null)
				{
					Brushes.sienna = new SolidBrush(Color.Sienna);
				}
				return Brushes.sienna;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00009839 File Offset: 0x00007A39
		public static Brush Silver
		{
			get
			{
				if (Brushes.silver == null)
				{
					Brushes.silver = new SolidBrush(Color.Silver);
				}
				return Brushes.silver;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00009856 File Offset: 0x00007A56
		public static Brush SkyBlue
		{
			get
			{
				if (Brushes.skyBlue == null)
				{
					Brushes.skyBlue = new SolidBrush(Color.SkyBlue);
				}
				return Brushes.skyBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00009873 File Offset: 0x00007A73
		public static Brush SlateBlue
		{
			get
			{
				if (Brushes.slateBlue == null)
				{
					Brushes.slateBlue = new SolidBrush(Color.SlateBlue);
				}
				return Brushes.slateBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00009890 File Offset: 0x00007A90
		public static Brush SlateGray
		{
			get
			{
				if (Brushes.slateGray == null)
				{
					Brushes.slateGray = new SolidBrush(Color.SlateGray);
				}
				return Brushes.slateGray;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000391 RID: 913 RVA: 0x000098AD File Offset: 0x00007AAD
		public static Brush Snow
		{
			get
			{
				if (Brushes.snow == null)
				{
					Brushes.snow = new SolidBrush(Color.Snow);
				}
				return Brushes.snow;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000392 RID: 914 RVA: 0x000098CA File Offset: 0x00007ACA
		public static Brush SpringGreen
		{
			get
			{
				if (Brushes.springGreen == null)
				{
					Brushes.springGreen = new SolidBrush(Color.SpringGreen);
				}
				return Brushes.springGreen;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000393 RID: 915 RVA: 0x000098E7 File Offset: 0x00007AE7
		public static Brush SteelBlue
		{
			get
			{
				if (Brushes.steelBlue == null)
				{
					Brushes.steelBlue = new SolidBrush(Color.SteelBlue);
				}
				return Brushes.steelBlue;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00009904 File Offset: 0x00007B04
		public static Brush Tan
		{
			get
			{
				if (Brushes.tan == null)
				{
					Brushes.tan = new SolidBrush(Color.Tan);
				}
				return Brushes.tan;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000395 RID: 917 RVA: 0x00009921 File Offset: 0x00007B21
		public static Brush Teal
		{
			get
			{
				if (Brushes.teal == null)
				{
					Brushes.teal = new SolidBrush(Color.Teal);
				}
				return Brushes.teal;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000993E File Offset: 0x00007B3E
		public static Brush Thistle
		{
			get
			{
				if (Brushes.thistle == null)
				{
					Brushes.thistle = new SolidBrush(Color.Thistle);
				}
				return Brushes.thistle;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000995B File Offset: 0x00007B5B
		public static Brush Tomato
		{
			get
			{
				if (Brushes.tomato == null)
				{
					Brushes.tomato = new SolidBrush(Color.Tomato);
				}
				return Brushes.tomato;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00009978 File Offset: 0x00007B78
		public static Brush Transparent
		{
			get
			{
				if (Brushes.transparent == null)
				{
					Brushes.transparent = new SolidBrush(Color.Transparent);
				}
				return Brushes.transparent;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00009995 File Offset: 0x00007B95
		public static Brush Turquoise
		{
			get
			{
				if (Brushes.turquoise == null)
				{
					Brushes.turquoise = new SolidBrush(Color.Turquoise);
				}
				return Brushes.turquoise;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600039A RID: 922 RVA: 0x000099B2 File Offset: 0x00007BB2
		public static Brush Violet
		{
			get
			{
				if (Brushes.violet == null)
				{
					Brushes.violet = new SolidBrush(Color.Violet);
				}
				return Brushes.violet;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600039B RID: 923 RVA: 0x000099CF File Offset: 0x00007BCF
		public static Brush Wheat
		{
			get
			{
				if (Brushes.wheat == null)
				{
					Brushes.wheat = new SolidBrush(Color.Wheat);
				}
				return Brushes.wheat;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600039C RID: 924 RVA: 0x000099EC File Offset: 0x00007BEC
		public static Brush White
		{
			get
			{
				if (Brushes.white == null)
				{
					Brushes.white = new SolidBrush(Color.White);
				}
				return Brushes.white;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00009A09 File Offset: 0x00007C09
		public static Brush WhiteSmoke
		{
			get
			{
				if (Brushes.whiteSmoke == null)
				{
					Brushes.whiteSmoke = new SolidBrush(Color.WhiteSmoke);
				}
				return Brushes.whiteSmoke;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00009A26 File Offset: 0x00007C26
		public static Brush Yellow
		{
			get
			{
				if (Brushes.yellow == null)
				{
					Brushes.yellow = new SolidBrush(Color.Yellow);
				}
				return Brushes.yellow;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00009A43 File Offset: 0x00007C43
		public static Brush YellowGreen
		{
			get
			{
				if (Brushes.yellowGreen == null)
				{
					Brushes.yellowGreen = new SolidBrush(Color.YellowGreen);
				}
				return Brushes.yellowGreen;
			}
		}

		// Token: 0x040003A1 RID: 929
		private static SolidBrush aliceBlue;

		// Token: 0x040003A2 RID: 930
		private static SolidBrush antiqueWhite;

		// Token: 0x040003A3 RID: 931
		private static SolidBrush aqua;

		// Token: 0x040003A4 RID: 932
		private static SolidBrush aquamarine;

		// Token: 0x040003A5 RID: 933
		private static SolidBrush azure;

		// Token: 0x040003A6 RID: 934
		private static SolidBrush beige;

		// Token: 0x040003A7 RID: 935
		private static SolidBrush bisque;

		// Token: 0x040003A8 RID: 936
		private static SolidBrush black;

		// Token: 0x040003A9 RID: 937
		private static SolidBrush blanchedAlmond;

		// Token: 0x040003AA RID: 938
		private static SolidBrush blue;

		// Token: 0x040003AB RID: 939
		private static SolidBrush blueViolet;

		// Token: 0x040003AC RID: 940
		private static SolidBrush brown;

		// Token: 0x040003AD RID: 941
		private static SolidBrush burlyWood;

		// Token: 0x040003AE RID: 942
		private static SolidBrush cadetBlue;

		// Token: 0x040003AF RID: 943
		private static SolidBrush chartreuse;

		// Token: 0x040003B0 RID: 944
		private static SolidBrush chocolate;

		// Token: 0x040003B1 RID: 945
		private static SolidBrush coral;

		// Token: 0x040003B2 RID: 946
		private static SolidBrush cornflowerBlue;

		// Token: 0x040003B3 RID: 947
		private static SolidBrush cornsilk;

		// Token: 0x040003B4 RID: 948
		private static SolidBrush crimson;

		// Token: 0x040003B5 RID: 949
		private static SolidBrush cyan;

		// Token: 0x040003B6 RID: 950
		private static SolidBrush darkBlue;

		// Token: 0x040003B7 RID: 951
		private static SolidBrush darkCyan;

		// Token: 0x040003B8 RID: 952
		private static SolidBrush darkGoldenrod;

		// Token: 0x040003B9 RID: 953
		private static SolidBrush darkGray;

		// Token: 0x040003BA RID: 954
		private static SolidBrush darkGreen;

		// Token: 0x040003BB RID: 955
		private static SolidBrush darkKhaki;

		// Token: 0x040003BC RID: 956
		private static SolidBrush darkMagenta;

		// Token: 0x040003BD RID: 957
		private static SolidBrush darkOliveGreen;

		// Token: 0x040003BE RID: 958
		private static SolidBrush darkOrange;

		// Token: 0x040003BF RID: 959
		private static SolidBrush darkOrchid;

		// Token: 0x040003C0 RID: 960
		private static SolidBrush darkRed;

		// Token: 0x040003C1 RID: 961
		private static SolidBrush darkSalmon;

		// Token: 0x040003C2 RID: 962
		private static SolidBrush darkSeaGreen;

		// Token: 0x040003C3 RID: 963
		private static SolidBrush darkSlateBlue;

		// Token: 0x040003C4 RID: 964
		private static SolidBrush darkSlateGray;

		// Token: 0x040003C5 RID: 965
		private static SolidBrush darkTurquoise;

		// Token: 0x040003C6 RID: 966
		private static SolidBrush darkViolet;

		// Token: 0x040003C7 RID: 967
		private static SolidBrush deepPink;

		// Token: 0x040003C8 RID: 968
		private static SolidBrush deepSkyBlue;

		// Token: 0x040003C9 RID: 969
		private static SolidBrush dimGray;

		// Token: 0x040003CA RID: 970
		private static SolidBrush dodgerBlue;

		// Token: 0x040003CB RID: 971
		private static SolidBrush firebrick;

		// Token: 0x040003CC RID: 972
		private static SolidBrush floralWhite;

		// Token: 0x040003CD RID: 973
		private static SolidBrush forestGreen;

		// Token: 0x040003CE RID: 974
		private static SolidBrush fuchsia;

		// Token: 0x040003CF RID: 975
		private static SolidBrush gainsboro;

		// Token: 0x040003D0 RID: 976
		private static SolidBrush ghostWhite;

		// Token: 0x040003D1 RID: 977
		private static SolidBrush gold;

		// Token: 0x040003D2 RID: 978
		private static SolidBrush goldenrod;

		// Token: 0x040003D3 RID: 979
		private static SolidBrush gray;

		// Token: 0x040003D4 RID: 980
		private static SolidBrush green;

		// Token: 0x040003D5 RID: 981
		private static SolidBrush greenYellow;

		// Token: 0x040003D6 RID: 982
		private static SolidBrush honeydew;

		// Token: 0x040003D7 RID: 983
		private static SolidBrush hotPink;

		// Token: 0x040003D8 RID: 984
		private static SolidBrush indianRed;

		// Token: 0x040003D9 RID: 985
		private static SolidBrush indigo;

		// Token: 0x040003DA RID: 986
		private static SolidBrush ivory;

		// Token: 0x040003DB RID: 987
		private static SolidBrush khaki;

		// Token: 0x040003DC RID: 988
		private static SolidBrush lavender;

		// Token: 0x040003DD RID: 989
		private static SolidBrush lavenderBlush;

		// Token: 0x040003DE RID: 990
		private static SolidBrush lawnGreen;

		// Token: 0x040003DF RID: 991
		private static SolidBrush lemonChiffon;

		// Token: 0x040003E0 RID: 992
		private static SolidBrush lightBlue;

		// Token: 0x040003E1 RID: 993
		private static SolidBrush lightCoral;

		// Token: 0x040003E2 RID: 994
		private static SolidBrush lightCyan;

		// Token: 0x040003E3 RID: 995
		private static SolidBrush lightGoldenrodYellow;

		// Token: 0x040003E4 RID: 996
		private static SolidBrush lightGray;

		// Token: 0x040003E5 RID: 997
		private static SolidBrush lightGreen;

		// Token: 0x040003E6 RID: 998
		private static SolidBrush lightPink;

		// Token: 0x040003E7 RID: 999
		private static SolidBrush lightSalmon;

		// Token: 0x040003E8 RID: 1000
		private static SolidBrush lightSeaGreen;

		// Token: 0x040003E9 RID: 1001
		private static SolidBrush lightSkyBlue;

		// Token: 0x040003EA RID: 1002
		private static SolidBrush lightSlateGray;

		// Token: 0x040003EB RID: 1003
		private static SolidBrush lightSteelBlue;

		// Token: 0x040003EC RID: 1004
		private static SolidBrush lightYellow;

		// Token: 0x040003ED RID: 1005
		private static SolidBrush lime;

		// Token: 0x040003EE RID: 1006
		private static SolidBrush limeGreen;

		// Token: 0x040003EF RID: 1007
		private static SolidBrush linen;

		// Token: 0x040003F0 RID: 1008
		private static SolidBrush magenta;

		// Token: 0x040003F1 RID: 1009
		private static SolidBrush maroon;

		// Token: 0x040003F2 RID: 1010
		private static SolidBrush mediumAquamarine;

		// Token: 0x040003F3 RID: 1011
		private static SolidBrush mediumBlue;

		// Token: 0x040003F4 RID: 1012
		private static SolidBrush mediumOrchid;

		// Token: 0x040003F5 RID: 1013
		private static SolidBrush mediumPurple;

		// Token: 0x040003F6 RID: 1014
		private static SolidBrush mediumSeaGreen;

		// Token: 0x040003F7 RID: 1015
		private static SolidBrush mediumSlateBlue;

		// Token: 0x040003F8 RID: 1016
		private static SolidBrush mediumSpringGreen;

		// Token: 0x040003F9 RID: 1017
		private static SolidBrush mediumTurquoise;

		// Token: 0x040003FA RID: 1018
		private static SolidBrush mediumVioletRed;

		// Token: 0x040003FB RID: 1019
		private static SolidBrush midnightBlue;

		// Token: 0x040003FC RID: 1020
		private static SolidBrush mintCream;

		// Token: 0x040003FD RID: 1021
		private static SolidBrush mistyRose;

		// Token: 0x040003FE RID: 1022
		private static SolidBrush moccasin;

		// Token: 0x040003FF RID: 1023
		private static SolidBrush navajoWhite;

		// Token: 0x04000400 RID: 1024
		private static SolidBrush navy;

		// Token: 0x04000401 RID: 1025
		private static SolidBrush oldLace;

		// Token: 0x04000402 RID: 1026
		private static SolidBrush olive;

		// Token: 0x04000403 RID: 1027
		private static SolidBrush oliveDrab;

		// Token: 0x04000404 RID: 1028
		private static SolidBrush orange;

		// Token: 0x04000405 RID: 1029
		private static SolidBrush orangeRed;

		// Token: 0x04000406 RID: 1030
		private static SolidBrush orchid;

		// Token: 0x04000407 RID: 1031
		private static SolidBrush paleGoldenrod;

		// Token: 0x04000408 RID: 1032
		private static SolidBrush paleGreen;

		// Token: 0x04000409 RID: 1033
		private static SolidBrush paleTurquoise;

		// Token: 0x0400040A RID: 1034
		private static SolidBrush paleVioletRed;

		// Token: 0x0400040B RID: 1035
		private static SolidBrush papayaWhip;

		// Token: 0x0400040C RID: 1036
		private static SolidBrush peachPuff;

		// Token: 0x0400040D RID: 1037
		private static SolidBrush peru;

		// Token: 0x0400040E RID: 1038
		private static SolidBrush pink;

		// Token: 0x0400040F RID: 1039
		private static SolidBrush plum;

		// Token: 0x04000410 RID: 1040
		private static SolidBrush powderBlue;

		// Token: 0x04000411 RID: 1041
		private static SolidBrush purple;

		// Token: 0x04000412 RID: 1042
		private static SolidBrush red;

		// Token: 0x04000413 RID: 1043
		private static SolidBrush rosyBrown;

		// Token: 0x04000414 RID: 1044
		private static SolidBrush royalBlue;

		// Token: 0x04000415 RID: 1045
		private static SolidBrush saddleBrown;

		// Token: 0x04000416 RID: 1046
		private static SolidBrush salmon;

		// Token: 0x04000417 RID: 1047
		private static SolidBrush sandyBrown;

		// Token: 0x04000418 RID: 1048
		private static SolidBrush seaGreen;

		// Token: 0x04000419 RID: 1049
		private static SolidBrush seaShell;

		// Token: 0x0400041A RID: 1050
		private static SolidBrush sienna;

		// Token: 0x0400041B RID: 1051
		private static SolidBrush silver;

		// Token: 0x0400041C RID: 1052
		private static SolidBrush skyBlue;

		// Token: 0x0400041D RID: 1053
		private static SolidBrush slateBlue;

		// Token: 0x0400041E RID: 1054
		private static SolidBrush slateGray;

		// Token: 0x0400041F RID: 1055
		private static SolidBrush snow;

		// Token: 0x04000420 RID: 1056
		private static SolidBrush springGreen;

		// Token: 0x04000421 RID: 1057
		private static SolidBrush steelBlue;

		// Token: 0x04000422 RID: 1058
		private static SolidBrush tan;

		// Token: 0x04000423 RID: 1059
		private static SolidBrush teal;

		// Token: 0x04000424 RID: 1060
		private static SolidBrush thistle;

		// Token: 0x04000425 RID: 1061
		private static SolidBrush tomato;

		// Token: 0x04000426 RID: 1062
		private static SolidBrush transparent;

		// Token: 0x04000427 RID: 1063
		private static SolidBrush turquoise;

		// Token: 0x04000428 RID: 1064
		private static SolidBrush violet;

		// Token: 0x04000429 RID: 1065
		private static SolidBrush wheat;

		// Token: 0x0400042A RID: 1066
		private static SolidBrush white;

		// Token: 0x0400042B RID: 1067
		private static SolidBrush whiteSmoke;

		// Token: 0x0400042C RID: 1068
		private static SolidBrush yellow;

		// Token: 0x0400042D RID: 1069
		private static SolidBrush yellowGreen;
	}
}
