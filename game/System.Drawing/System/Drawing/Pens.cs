using System;

namespace System.Drawing
{
	/// <summary>Pens for all the standard colors. This class cannot be inherited.</summary>
	// Token: 0x02000086 RID: 134
	public sealed class Pens
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x00002050 File Offset: 0x00000250
		private Pens()
		{
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001387C File Offset: 0x00011A7C
		public static Pen AliceBlue
		{
			get
			{
				if (Pens.aliceblue == null)
				{
					Pens.aliceblue = new Pen(Color.AliceBlue);
					Pens.aliceblue.isModifiable = false;
				}
				return Pens.aliceblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x000138A4 File Offset: 0x00011AA4
		public static Pen AntiqueWhite
		{
			get
			{
				if (Pens.antiquewhite == null)
				{
					Pens.antiquewhite = new Pen(Color.AntiqueWhite);
					Pens.antiquewhite.isModifiable = false;
				}
				return Pens.antiquewhite;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x000138CC File Offset: 0x00011ACC
		public static Pen Aqua
		{
			get
			{
				if (Pens.aqua == null)
				{
					Pens.aqua = new Pen(Color.Aqua);
					Pens.aqua.isModifiable = false;
				}
				return Pens.aqua;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x000138F4 File Offset: 0x00011AF4
		public static Pen Aquamarine
		{
			get
			{
				if (Pens.aquamarine == null)
				{
					Pens.aquamarine = new Pen(Color.Aquamarine);
					Pens.aquamarine.isModifiable = false;
				}
				return Pens.aquamarine;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0001391C File Offset: 0x00011B1C
		public static Pen Azure
		{
			get
			{
				if (Pens.azure == null)
				{
					Pens.azure = new Pen(Color.Azure);
					Pens.azure.isModifiable = false;
				}
				return Pens.azure;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00013944 File Offset: 0x00011B44
		public static Pen Beige
		{
			get
			{
				if (Pens.beige == null)
				{
					Pens.beige = new Pen(Color.Beige);
					Pens.beige.isModifiable = false;
				}
				return Pens.beige;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001396C File Offset: 0x00011B6C
		public static Pen Bisque
		{
			get
			{
				if (Pens.bisque == null)
				{
					Pens.bisque = new Pen(Color.Bisque);
					Pens.bisque.isModifiable = false;
				}
				return Pens.bisque;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x00013994 File Offset: 0x00011B94
		public static Pen Black
		{
			get
			{
				if (Pens.black == null)
				{
					Pens.black = new Pen(Color.Black);
					Pens.black.isModifiable = false;
				}
				return Pens.black;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x000139BC File Offset: 0x00011BBC
		public static Pen BlanchedAlmond
		{
			get
			{
				if (Pens.blanchedalmond == null)
				{
					Pens.blanchedalmond = new Pen(Color.BlanchedAlmond);
					Pens.blanchedalmond.isModifiable = false;
				}
				return Pens.blanchedalmond;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x000139E4 File Offset: 0x00011BE4
		public static Pen Blue
		{
			get
			{
				if (Pens.blue == null)
				{
					Pens.blue = new Pen(Color.Blue);
					Pens.blue.isModifiable = false;
				}
				return Pens.blue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00013A0C File Offset: 0x00011C0C
		public static Pen BlueViolet
		{
			get
			{
				if (Pens.blueviolet == null)
				{
					Pens.blueviolet = new Pen(Color.BlueViolet);
					Pens.blueviolet.isModifiable = false;
				}
				return Pens.blueviolet;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00013A34 File Offset: 0x00011C34
		public static Pen Brown
		{
			get
			{
				if (Pens.brown == null)
				{
					Pens.brown = new Pen(Color.Brown);
					Pens.brown.isModifiable = false;
				}
				return Pens.brown;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00013A5C File Offset: 0x00011C5C
		public static Pen BurlyWood
		{
			get
			{
				if (Pens.burlywood == null)
				{
					Pens.burlywood = new Pen(Color.BurlyWood);
					Pens.burlywood.isModifiable = false;
				}
				return Pens.burlywood;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00013A84 File Offset: 0x00011C84
		public static Pen CadetBlue
		{
			get
			{
				if (Pens.cadetblue == null)
				{
					Pens.cadetblue = new Pen(Color.CadetBlue);
					Pens.cadetblue.isModifiable = false;
				}
				return Pens.cadetblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00013AAC File Offset: 0x00011CAC
		public static Pen Chartreuse
		{
			get
			{
				if (Pens.chartreuse == null)
				{
					Pens.chartreuse = new Pen(Color.Chartreuse);
					Pens.chartreuse.isModifiable = false;
				}
				return Pens.chartreuse;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00013AD4 File Offset: 0x00011CD4
		public static Pen Chocolate
		{
			get
			{
				if (Pens.chocolate == null)
				{
					Pens.chocolate = new Pen(Color.Chocolate);
					Pens.chocolate.isModifiable = false;
				}
				return Pens.chocolate;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00013AFC File Offset: 0x00011CFC
		public static Pen Coral
		{
			get
			{
				if (Pens.coral == null)
				{
					Pens.coral = new Pen(Color.Coral);
					Pens.coral.isModifiable = false;
				}
				return Pens.coral;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00013B24 File Offset: 0x00011D24
		public static Pen CornflowerBlue
		{
			get
			{
				if (Pens.cornflowerblue == null)
				{
					Pens.cornflowerblue = new Pen(Color.CornflowerBlue);
					Pens.cornflowerblue.isModifiable = false;
				}
				return Pens.cornflowerblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00013B4C File Offset: 0x00011D4C
		public static Pen Cornsilk
		{
			get
			{
				if (Pens.cornsilk == null)
				{
					Pens.cornsilk = new Pen(Color.Cornsilk);
					Pens.cornsilk.isModifiable = false;
				}
				return Pens.cornsilk;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00013B74 File Offset: 0x00011D74
		public static Pen Crimson
		{
			get
			{
				if (Pens.crimson == null)
				{
					Pens.crimson = new Pen(Color.Crimson);
					Pens.crimson.isModifiable = false;
				}
				return Pens.crimson;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00013B9C File Offset: 0x00011D9C
		public static Pen Cyan
		{
			get
			{
				if (Pens.cyan == null)
				{
					Pens.cyan = new Pen(Color.Cyan);
					Pens.cyan.isModifiable = false;
				}
				return Pens.cyan;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00013BC4 File Offset: 0x00011DC4
		public static Pen DarkBlue
		{
			get
			{
				if (Pens.darkblue == null)
				{
					Pens.darkblue = new Pen(Color.DarkBlue);
					Pens.darkblue.isModifiable = false;
				}
				return Pens.darkblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00013BEC File Offset: 0x00011DEC
		public static Pen DarkCyan
		{
			get
			{
				if (Pens.darkcyan == null)
				{
					Pens.darkcyan = new Pen(Color.DarkCyan);
					Pens.darkcyan.isModifiable = false;
				}
				return Pens.darkcyan;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x00013C14 File Offset: 0x00011E14
		public static Pen DarkGoldenrod
		{
			get
			{
				if (Pens.darkgoldenrod == null)
				{
					Pens.darkgoldenrod = new Pen(Color.DarkGoldenrod);
					Pens.darkgoldenrod.isModifiable = false;
				}
				return Pens.darkgoldenrod;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00013C3C File Offset: 0x00011E3C
		public static Pen DarkGray
		{
			get
			{
				if (Pens.darkgray == null)
				{
					Pens.darkgray = new Pen(Color.DarkGray);
					Pens.darkgray.isModifiable = false;
				}
				return Pens.darkgray;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00013C64 File Offset: 0x00011E64
		public static Pen DarkGreen
		{
			get
			{
				if (Pens.darkgreen == null)
				{
					Pens.darkgreen = new Pen(Color.DarkGreen);
					Pens.darkgreen.isModifiable = false;
				}
				return Pens.darkgreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00013C8C File Offset: 0x00011E8C
		public static Pen DarkKhaki
		{
			get
			{
				if (Pens.darkkhaki == null)
				{
					Pens.darkkhaki = new Pen(Color.DarkKhaki);
					Pens.darkkhaki.isModifiable = false;
				}
				return Pens.darkkhaki;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00013CB4 File Offset: 0x00011EB4
		public static Pen DarkMagenta
		{
			get
			{
				if (Pens.darkmagenta == null)
				{
					Pens.darkmagenta = new Pen(Color.DarkMagenta);
					Pens.darkmagenta.isModifiable = false;
				}
				return Pens.darkmagenta;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00013CDC File Offset: 0x00011EDC
		public static Pen DarkOliveGreen
		{
			get
			{
				if (Pens.darkolivegreen == null)
				{
					Pens.darkolivegreen = new Pen(Color.DarkOliveGreen);
					Pens.darkolivegreen.isModifiable = false;
				}
				return Pens.darkolivegreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00013D04 File Offset: 0x00011F04
		public static Pen DarkOrange
		{
			get
			{
				if (Pens.darkorange == null)
				{
					Pens.darkorange = new Pen(Color.DarkOrange);
					Pens.darkorange.isModifiable = false;
				}
				return Pens.darkorange;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00013D2C File Offset: 0x00011F2C
		public static Pen DarkOrchid
		{
			get
			{
				if (Pens.darkorchid == null)
				{
					Pens.darkorchid = new Pen(Color.DarkOrchid);
					Pens.darkorchid.isModifiable = false;
				}
				return Pens.darkorchid;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00013D54 File Offset: 0x00011F54
		public static Pen DarkRed
		{
			get
			{
				if (Pens.darkred == null)
				{
					Pens.darkred = new Pen(Color.DarkRed);
					Pens.darkred.isModifiable = false;
				}
				return Pens.darkred;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00013D7C File Offset: 0x00011F7C
		public static Pen DarkSalmon
		{
			get
			{
				if (Pens.darksalmon == null)
				{
					Pens.darksalmon = new Pen(Color.DarkSalmon);
					Pens.darksalmon.isModifiable = false;
				}
				return Pens.darksalmon;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00013DA4 File Offset: 0x00011FA4
		public static Pen DarkSeaGreen
		{
			get
			{
				if (Pens.darkseagreen == null)
				{
					Pens.darkseagreen = new Pen(Color.DarkSeaGreen);
					Pens.darkseagreen.isModifiable = false;
				}
				return Pens.darkseagreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00013DCC File Offset: 0x00011FCC
		public static Pen DarkSlateBlue
		{
			get
			{
				if (Pens.darkslateblue == null)
				{
					Pens.darkslateblue = new Pen(Color.DarkSlateBlue);
					Pens.darkslateblue.isModifiable = false;
				}
				return Pens.darkslateblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00013DF4 File Offset: 0x00011FF4
		public static Pen DarkSlateGray
		{
			get
			{
				if (Pens.darkslategray == null)
				{
					Pens.darkslategray = new Pen(Color.DarkSlateGray);
					Pens.darkslategray.isModifiable = false;
				}
				return Pens.darkslategray;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00013E1C File Offset: 0x0001201C
		public static Pen DarkTurquoise
		{
			get
			{
				if (Pens.darkturquoise == null)
				{
					Pens.darkturquoise = new Pen(Color.DarkTurquoise);
					Pens.darkturquoise.isModifiable = false;
				}
				return Pens.darkturquoise;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00013E44 File Offset: 0x00012044
		public static Pen DarkViolet
		{
			get
			{
				if (Pens.darkviolet == null)
				{
					Pens.darkviolet = new Pen(Color.DarkViolet);
					Pens.darkviolet.isModifiable = false;
				}
				return Pens.darkviolet;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00013E6C File Offset: 0x0001206C
		public static Pen DeepPink
		{
			get
			{
				if (Pens.deeppink == null)
				{
					Pens.deeppink = new Pen(Color.DeepPink);
					Pens.deeppink.isModifiable = false;
				}
				return Pens.deeppink;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00013E94 File Offset: 0x00012094
		public static Pen DeepSkyBlue
		{
			get
			{
				if (Pens.deepskyblue == null)
				{
					Pens.deepskyblue = new Pen(Color.DeepSkyBlue);
					Pens.deepskyblue.isModifiable = false;
				}
				return Pens.deepskyblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00013EBC File Offset: 0x000120BC
		public static Pen DimGray
		{
			get
			{
				if (Pens.dimgray == null)
				{
					Pens.dimgray = new Pen(Color.DimGray);
					Pens.dimgray.isModifiable = false;
				}
				return Pens.dimgray;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00013EE4 File Offset: 0x000120E4
		public static Pen DodgerBlue
		{
			get
			{
				if (Pens.dodgerblue == null)
				{
					Pens.dodgerblue = new Pen(Color.DodgerBlue);
					Pens.dodgerblue.isModifiable = false;
				}
				return Pens.dodgerblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00013F0C File Offset: 0x0001210C
		public static Pen Firebrick
		{
			get
			{
				if (Pens.firebrick == null)
				{
					Pens.firebrick = new Pen(Color.Firebrick);
					Pens.firebrick.isModifiable = false;
				}
				return Pens.firebrick;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00013F34 File Offset: 0x00012134
		public static Pen FloralWhite
		{
			get
			{
				if (Pens.floralwhite == null)
				{
					Pens.floralwhite = new Pen(Color.FloralWhite);
					Pens.floralwhite.isModifiable = false;
				}
				return Pens.floralwhite;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00013F5C File Offset: 0x0001215C
		public static Pen ForestGreen
		{
			get
			{
				if (Pens.forestgreen == null)
				{
					Pens.forestgreen = new Pen(Color.ForestGreen);
					Pens.forestgreen.isModifiable = false;
				}
				return Pens.forestgreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00013F84 File Offset: 0x00012184
		public static Pen Fuchsia
		{
			get
			{
				if (Pens.fuchsia == null)
				{
					Pens.fuchsia = new Pen(Color.Fuchsia);
					Pens.fuchsia.isModifiable = false;
				}
				return Pens.fuchsia;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00013FAC File Offset: 0x000121AC
		public static Pen Gainsboro
		{
			get
			{
				if (Pens.gainsboro == null)
				{
					Pens.gainsboro = new Pen(Color.Gainsboro);
					Pens.gainsboro.isModifiable = false;
				}
				return Pens.gainsboro;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00013FD4 File Offset: 0x000121D4
		public static Pen GhostWhite
		{
			get
			{
				if (Pens.ghostwhite == null)
				{
					Pens.ghostwhite = new Pen(Color.GhostWhite);
					Pens.ghostwhite.isModifiable = false;
				}
				return Pens.ghostwhite;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00013FFC File Offset: 0x000121FC
		public static Pen Gold
		{
			get
			{
				if (Pens.gold == null)
				{
					Pens.gold = new Pen(Color.Gold);
					Pens.gold.isModifiable = false;
				}
				return Pens.gold;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x00014024 File Offset: 0x00012224
		public static Pen Goldenrod
		{
			get
			{
				if (Pens.goldenrod == null)
				{
					Pens.goldenrod = new Pen(Color.Goldenrod);
					Pens.goldenrod.isModifiable = false;
				}
				return Pens.goldenrod;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001404C File Offset: 0x0001224C
		public static Pen Gray
		{
			get
			{
				if (Pens.gray == null)
				{
					Pens.gray = new Pen(Color.Gray);
					Pens.gray.isModifiable = false;
				}
				return Pens.gray;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x00014074 File Offset: 0x00012274
		public static Pen Green
		{
			get
			{
				if (Pens.green == null)
				{
					Pens.green = new Pen(Color.Green);
					Pens.green.isModifiable = false;
				}
				return Pens.green;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0001409C File Offset: 0x0001229C
		public static Pen GreenYellow
		{
			get
			{
				if (Pens.greenyellow == null)
				{
					Pens.greenyellow = new Pen(Color.GreenYellow);
					Pens.greenyellow.isModifiable = false;
				}
				return Pens.greenyellow;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x000140C4 File Offset: 0x000122C4
		public static Pen Honeydew
		{
			get
			{
				if (Pens.honeydew == null)
				{
					Pens.honeydew = new Pen(Color.Honeydew);
					Pens.honeydew.isModifiable = false;
				}
				return Pens.honeydew;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x000140EC File Offset: 0x000122EC
		public static Pen HotPink
		{
			get
			{
				if (Pens.hotpink == null)
				{
					Pens.hotpink = new Pen(Color.HotPink);
					Pens.hotpink.isModifiable = false;
				}
				return Pens.hotpink;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00014114 File Offset: 0x00012314
		public static Pen IndianRed
		{
			get
			{
				if (Pens.indianred == null)
				{
					Pens.indianred = new Pen(Color.IndianRed);
					Pens.indianred.isModifiable = false;
				}
				return Pens.indianred;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001413C File Offset: 0x0001233C
		public static Pen Indigo
		{
			get
			{
				if (Pens.indigo == null)
				{
					Pens.indigo = new Pen(Color.Indigo);
					Pens.indigo.isModifiable = false;
				}
				return Pens.indigo;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00014164 File Offset: 0x00012364
		public static Pen Ivory
		{
			get
			{
				if (Pens.ivory == null)
				{
					Pens.ivory = new Pen(Color.Ivory);
					Pens.ivory.isModifiable = false;
				}
				return Pens.ivory;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0001418C File Offset: 0x0001238C
		public static Pen Khaki
		{
			get
			{
				if (Pens.khaki == null)
				{
					Pens.khaki = new Pen(Color.Khaki);
					Pens.khaki.isModifiable = false;
				}
				return Pens.khaki;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x000141B4 File Offset: 0x000123B4
		public static Pen Lavender
		{
			get
			{
				if (Pens.lavender == null)
				{
					Pens.lavender = new Pen(Color.Lavender);
					Pens.lavender.isModifiable = false;
				}
				return Pens.lavender;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x000141DC File Offset: 0x000123DC
		public static Pen LavenderBlush
		{
			get
			{
				if (Pens.lavenderblush == null)
				{
					Pens.lavenderblush = new Pen(Color.LavenderBlush);
					Pens.lavenderblush.isModifiable = false;
				}
				return Pens.lavenderblush;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00014204 File Offset: 0x00012404
		public static Pen LawnGreen
		{
			get
			{
				if (Pens.lawngreen == null)
				{
					Pens.lawngreen = new Pen(Color.LawnGreen);
					Pens.lawngreen.isModifiable = false;
				}
				return Pens.lawngreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001422C File Offset: 0x0001242C
		public static Pen LemonChiffon
		{
			get
			{
				if (Pens.lemonchiffon == null)
				{
					Pens.lemonchiffon = new Pen(Color.LemonChiffon);
					Pens.lemonchiffon.isModifiable = false;
				}
				return Pens.lemonchiffon;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00014254 File Offset: 0x00012454
		public static Pen LightBlue
		{
			get
			{
				if (Pens.lightblue == null)
				{
					Pens.lightblue = new Pen(Color.LightBlue);
					Pens.lightblue.isModifiable = false;
				}
				return Pens.lightblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001427C File Offset: 0x0001247C
		public static Pen LightCoral
		{
			get
			{
				if (Pens.lightcoral == null)
				{
					Pens.lightcoral = new Pen(Color.LightCoral);
					Pens.lightcoral.isModifiable = false;
				}
				return Pens.lightcoral;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x000142A4 File Offset: 0x000124A4
		public static Pen LightCyan
		{
			get
			{
				if (Pens.lightcyan == null)
				{
					Pens.lightcyan = new Pen(Color.LightCyan);
					Pens.lightcyan.isModifiable = false;
				}
				return Pens.lightcyan;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x000142CC File Offset: 0x000124CC
		public static Pen LightGoldenrodYellow
		{
			get
			{
				if (Pens.lightgoldenrodyellow == null)
				{
					Pens.lightgoldenrodyellow = new Pen(Color.LightGoldenrodYellow);
					Pens.lightgoldenrodyellow.isModifiable = false;
				}
				return Pens.lightgoldenrodyellow;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x000142F4 File Offset: 0x000124F4
		public static Pen LightGray
		{
			get
			{
				if (Pens.lightgray == null)
				{
					Pens.lightgray = new Pen(Color.LightGray);
					Pens.lightgray.isModifiable = false;
				}
				return Pens.lightgray;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001431C File Offset: 0x0001251C
		public static Pen LightGreen
		{
			get
			{
				if (Pens.lightgreen == null)
				{
					Pens.lightgreen = new Pen(Color.LightGreen);
					Pens.lightgreen.isModifiable = false;
				}
				return Pens.lightgreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00014344 File Offset: 0x00012544
		public static Pen LightPink
		{
			get
			{
				if (Pens.lightpink == null)
				{
					Pens.lightpink = new Pen(Color.LightPink);
					Pens.lightpink.isModifiable = false;
				}
				return Pens.lightpink;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001436C File Offset: 0x0001256C
		public static Pen LightSalmon
		{
			get
			{
				if (Pens.lightsalmon == null)
				{
					Pens.lightsalmon = new Pen(Color.LightSalmon);
					Pens.lightsalmon.isModifiable = false;
				}
				return Pens.lightsalmon;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00014394 File Offset: 0x00012594
		public static Pen LightSeaGreen
		{
			get
			{
				if (Pens.lightseagreen == null)
				{
					Pens.lightseagreen = new Pen(Color.LightSeaGreen);
					Pens.lightseagreen.isModifiable = false;
				}
				return Pens.lightseagreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x000143BC File Offset: 0x000125BC
		public static Pen LightSkyBlue
		{
			get
			{
				if (Pens.lightskyblue == null)
				{
					Pens.lightskyblue = new Pen(Color.LightSkyBlue);
					Pens.lightskyblue.isModifiable = false;
				}
				return Pens.lightskyblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x000143E4 File Offset: 0x000125E4
		public static Pen LightSlateGray
		{
			get
			{
				if (Pens.lightslategray == null)
				{
					Pens.lightslategray = new Pen(Color.LightSlateGray);
					Pens.lightslategray.isModifiable = false;
				}
				return Pens.lightslategray;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001440C File Offset: 0x0001260C
		public static Pen LightSteelBlue
		{
			get
			{
				if (Pens.lightsteelblue == null)
				{
					Pens.lightsteelblue = new Pen(Color.LightSteelBlue);
					Pens.lightsteelblue.isModifiable = false;
				}
				return Pens.lightsteelblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00014434 File Offset: 0x00012634
		public static Pen LightYellow
		{
			get
			{
				if (Pens.lightyellow == null)
				{
					Pens.lightyellow = new Pen(Color.LightYellow);
					Pens.lightyellow.isModifiable = false;
				}
				return Pens.lightyellow;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001445C File Offset: 0x0001265C
		public static Pen Lime
		{
			get
			{
				if (Pens.lime == null)
				{
					Pens.lime = new Pen(Color.Lime);
					Pens.lime.isModifiable = false;
				}
				return Pens.lime;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00014484 File Offset: 0x00012684
		public static Pen LimeGreen
		{
			get
			{
				if (Pens.limegreen == null)
				{
					Pens.limegreen = new Pen(Color.LimeGreen);
					Pens.limegreen.isModifiable = false;
				}
				return Pens.limegreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x000144AC File Offset: 0x000126AC
		public static Pen Linen
		{
			get
			{
				if (Pens.linen == null)
				{
					Pens.linen = new Pen(Color.Linen);
					Pens.linen.isModifiable = false;
				}
				return Pens.linen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x000144D4 File Offset: 0x000126D4
		public static Pen Magenta
		{
			get
			{
				if (Pens.magenta == null)
				{
					Pens.magenta = new Pen(Color.Magenta);
					Pens.magenta.isModifiable = false;
				}
				return Pens.magenta;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x000144FC File Offset: 0x000126FC
		public static Pen Maroon
		{
			get
			{
				if (Pens.maroon == null)
				{
					Pens.maroon = new Pen(Color.Maroon);
					Pens.maroon.isModifiable = false;
				}
				return Pens.maroon;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00014524 File Offset: 0x00012724
		public static Pen MediumAquamarine
		{
			get
			{
				if (Pens.mediumaquamarine == null)
				{
					Pens.mediumaquamarine = new Pen(Color.MediumAquamarine);
					Pens.mediumaquamarine.isModifiable = false;
				}
				return Pens.mediumaquamarine;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001454C File Offset: 0x0001274C
		public static Pen MediumBlue
		{
			get
			{
				if (Pens.mediumblue == null)
				{
					Pens.mediumblue = new Pen(Color.MediumBlue);
					Pens.mediumblue.isModifiable = false;
				}
				return Pens.mediumblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00014574 File Offset: 0x00012774
		public static Pen MediumOrchid
		{
			get
			{
				if (Pens.mediumorchid == null)
				{
					Pens.mediumorchid = new Pen(Color.MediumOrchid);
					Pens.mediumorchid.isModifiable = false;
				}
				return Pens.mediumorchid;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0001459C File Offset: 0x0001279C
		public static Pen MediumPurple
		{
			get
			{
				if (Pens.mediumpurple == null)
				{
					Pens.mediumpurple = new Pen(Color.MediumPurple);
					Pens.mediumpurple.isModifiable = false;
				}
				return Pens.mediumpurple;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x000145C4 File Offset: 0x000127C4
		public static Pen MediumSeaGreen
		{
			get
			{
				if (Pens.mediumseagreen == null)
				{
					Pens.mediumseagreen = new Pen(Color.MediumSeaGreen);
					Pens.mediumseagreen.isModifiable = false;
				}
				return Pens.mediumseagreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x000145EC File Offset: 0x000127EC
		public static Pen MediumSlateBlue
		{
			get
			{
				if (Pens.mediumslateblue == null)
				{
					Pens.mediumslateblue = new Pen(Color.MediumSlateBlue);
					Pens.mediumslateblue.isModifiable = false;
				}
				return Pens.mediumslateblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00014614 File Offset: 0x00012814
		public static Pen MediumSpringGreen
		{
			get
			{
				if (Pens.mediumspringgreen == null)
				{
					Pens.mediumspringgreen = new Pen(Color.MediumSpringGreen);
					Pens.mediumspringgreen.isModifiable = false;
				}
				return Pens.mediumspringgreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0001463C File Offset: 0x0001283C
		public static Pen MediumTurquoise
		{
			get
			{
				if (Pens.mediumturquoise == null)
				{
					Pens.mediumturquoise = new Pen(Color.MediumTurquoise);
					Pens.mediumturquoise.isModifiable = false;
				}
				return Pens.mediumturquoise;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00014664 File Offset: 0x00012864
		public static Pen MediumVioletRed
		{
			get
			{
				if (Pens.mediumvioletred == null)
				{
					Pens.mediumvioletred = new Pen(Color.MediumVioletRed);
					Pens.mediumvioletred.isModifiable = false;
				}
				return Pens.mediumvioletred;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0001468C File Offset: 0x0001288C
		public static Pen MidnightBlue
		{
			get
			{
				if (Pens.midnightblue == null)
				{
					Pens.midnightblue = new Pen(Color.MidnightBlue);
					Pens.midnightblue.isModifiable = false;
				}
				return Pens.midnightblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x000146B4 File Offset: 0x000128B4
		public static Pen MintCream
		{
			get
			{
				if (Pens.mintcream == null)
				{
					Pens.mintcream = new Pen(Color.MintCream);
					Pens.mintcream.isModifiable = false;
				}
				return Pens.mintcream;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x000146DC File Offset: 0x000128DC
		public static Pen MistyRose
		{
			get
			{
				if (Pens.mistyrose == null)
				{
					Pens.mistyrose = new Pen(Color.MistyRose);
					Pens.mistyrose.isModifiable = false;
				}
				return Pens.mistyrose;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00014704 File Offset: 0x00012904
		public static Pen Moccasin
		{
			get
			{
				if (Pens.moccasin == null)
				{
					Pens.moccasin = new Pen(Color.Moccasin);
					Pens.moccasin.isModifiable = false;
				}
				return Pens.moccasin;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001472C File Offset: 0x0001292C
		public static Pen NavajoWhite
		{
			get
			{
				if (Pens.navajowhite == null)
				{
					Pens.navajowhite = new Pen(Color.NavajoWhite);
					Pens.navajowhite.isModifiable = false;
				}
				return Pens.navajowhite;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00014754 File Offset: 0x00012954
		public static Pen Navy
		{
			get
			{
				if (Pens.navy == null)
				{
					Pens.navy = new Pen(Color.Navy);
					Pens.navy.isModifiable = false;
				}
				return Pens.navy;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001477C File Offset: 0x0001297C
		public static Pen OldLace
		{
			get
			{
				if (Pens.oldlace == null)
				{
					Pens.oldlace = new Pen(Color.OldLace);
					Pens.oldlace.isModifiable = false;
				}
				return Pens.oldlace;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x000147A4 File Offset: 0x000129A4
		public static Pen Olive
		{
			get
			{
				if (Pens.olive == null)
				{
					Pens.olive = new Pen(Color.Olive);
					Pens.olive.isModifiable = false;
				}
				return Pens.olive;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x000147CC File Offset: 0x000129CC
		public static Pen OliveDrab
		{
			get
			{
				if (Pens.olivedrab == null)
				{
					Pens.olivedrab = new Pen(Color.OliveDrab);
					Pens.olivedrab.isModifiable = false;
				}
				return Pens.olivedrab;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x000147F4 File Offset: 0x000129F4
		public static Pen Orange
		{
			get
			{
				if (Pens.orange == null)
				{
					Pens.orange = new Pen(Color.Orange);
					Pens.orange.isModifiable = false;
				}
				return Pens.orange;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0001481C File Offset: 0x00012A1C
		public static Pen OrangeRed
		{
			get
			{
				if (Pens.orangered == null)
				{
					Pens.orangered = new Pen(Color.OrangeRed);
					Pens.orangered.isModifiable = false;
				}
				return Pens.orangered;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00014844 File Offset: 0x00012A44
		public static Pen Orchid
		{
			get
			{
				if (Pens.orchid == null)
				{
					Pens.orchid = new Pen(Color.Orchid);
					Pens.orchid.isModifiable = false;
				}
				return Pens.orchid;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001486C File Offset: 0x00012A6C
		public static Pen PaleGoldenrod
		{
			get
			{
				if (Pens.palegoldenrod == null)
				{
					Pens.palegoldenrod = new Pen(Color.PaleGoldenrod);
					Pens.palegoldenrod.isModifiable = false;
				}
				return Pens.palegoldenrod;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00014894 File Offset: 0x00012A94
		public static Pen PaleGreen
		{
			get
			{
				if (Pens.palegreen == null)
				{
					Pens.palegreen = new Pen(Color.PaleGreen);
					Pens.palegreen.isModifiable = false;
				}
				return Pens.palegreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x000148BC File Offset: 0x00012ABC
		public static Pen PaleTurquoise
		{
			get
			{
				if (Pens.paleturquoise == null)
				{
					Pens.paleturquoise = new Pen(Color.PaleTurquoise);
					Pens.paleturquoise.isModifiable = false;
				}
				return Pens.paleturquoise;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x000148E4 File Offset: 0x00012AE4
		public static Pen PaleVioletRed
		{
			get
			{
				if (Pens.palevioletred == null)
				{
					Pens.palevioletred = new Pen(Color.PaleVioletRed);
					Pens.palevioletred.isModifiable = false;
				}
				return Pens.palevioletred;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0001490C File Offset: 0x00012B0C
		public static Pen PapayaWhip
		{
			get
			{
				if (Pens.papayawhip == null)
				{
					Pens.papayawhip = new Pen(Color.PapayaWhip);
					Pens.papayawhip.isModifiable = false;
				}
				return Pens.papayawhip;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00014934 File Offset: 0x00012B34
		public static Pen PeachPuff
		{
			get
			{
				if (Pens.peachpuff == null)
				{
					Pens.peachpuff = new Pen(Color.PeachPuff);
					Pens.peachpuff.isModifiable = false;
				}
				return Pens.peachpuff;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0001495C File Offset: 0x00012B5C
		public static Pen Peru
		{
			get
			{
				if (Pens.peru == null)
				{
					Pens.peru = new Pen(Color.Peru);
					Pens.peru.isModifiable = false;
				}
				return Pens.peru;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x00014984 File Offset: 0x00012B84
		public static Pen Pink
		{
			get
			{
				if (Pens.pink == null)
				{
					Pens.pink = new Pen(Color.Pink);
					Pens.pink.isModifiable = false;
				}
				return Pens.pink;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x000149AC File Offset: 0x00012BAC
		public static Pen Plum
		{
			get
			{
				if (Pens.plum == null)
				{
					Pens.plum = new Pen(Color.Plum);
					Pens.plum.isModifiable = false;
				}
				return Pens.plum;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x000149D4 File Offset: 0x00012BD4
		public static Pen PowderBlue
		{
			get
			{
				if (Pens.powderblue == null)
				{
					Pens.powderblue = new Pen(Color.PowderBlue);
					Pens.powderblue.isModifiable = false;
				}
				return Pens.powderblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x000149FC File Offset: 0x00012BFC
		public static Pen Purple
		{
			get
			{
				if (Pens.purple == null)
				{
					Pens.purple = new Pen(Color.Purple);
					Pens.purple.isModifiable = false;
				}
				return Pens.purple;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x00014A24 File Offset: 0x00012C24
		public static Pen Red
		{
			get
			{
				if (Pens.red == null)
				{
					Pens.red = new Pen(Color.Red);
					Pens.red.isModifiable = false;
				}
				return Pens.red;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x00014A4C File Offset: 0x00012C4C
		public static Pen RosyBrown
		{
			get
			{
				if (Pens.rosybrown == null)
				{
					Pens.rosybrown = new Pen(Color.RosyBrown);
					Pens.rosybrown.isModifiable = false;
				}
				return Pens.rosybrown;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x00014A74 File Offset: 0x00012C74
		public static Pen RoyalBlue
		{
			get
			{
				if (Pens.royalblue == null)
				{
					Pens.royalblue = new Pen(Color.RoyalBlue);
					Pens.royalblue.isModifiable = false;
				}
				return Pens.royalblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x00014A9C File Offset: 0x00012C9C
		public static Pen SaddleBrown
		{
			get
			{
				if (Pens.saddlebrown == null)
				{
					Pens.saddlebrown = new Pen(Color.SaddleBrown);
					Pens.saddlebrown.isModifiable = false;
				}
				return Pens.saddlebrown;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00014AC4 File Offset: 0x00012CC4
		public static Pen Salmon
		{
			get
			{
				if (Pens.salmon == null)
				{
					Pens.salmon = new Pen(Color.Salmon);
					Pens.salmon.isModifiable = false;
				}
				return Pens.salmon;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00014AEC File Offset: 0x00012CEC
		public static Pen SandyBrown
		{
			get
			{
				if (Pens.sandybrown == null)
				{
					Pens.sandybrown = new Pen(Color.SandyBrown);
					Pens.sandybrown.isModifiable = false;
				}
				return Pens.sandybrown;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00014B14 File Offset: 0x00012D14
		public static Pen SeaGreen
		{
			get
			{
				if (Pens.seagreen == null)
				{
					Pens.seagreen = new Pen(Color.SeaGreen);
					Pens.seagreen.isModifiable = false;
				}
				return Pens.seagreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00014B3C File Offset: 0x00012D3C
		public static Pen SeaShell
		{
			get
			{
				if (Pens.seashell == null)
				{
					Pens.seashell = new Pen(Color.SeaShell);
					Pens.seashell.isModifiable = false;
				}
				return Pens.seashell;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00014B64 File Offset: 0x00012D64
		public static Pen Sienna
		{
			get
			{
				if (Pens.sienna == null)
				{
					Pens.sienna = new Pen(Color.Sienna);
					Pens.sienna.isModifiable = false;
				}
				return Pens.sienna;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00014B8C File Offset: 0x00012D8C
		public static Pen Silver
		{
			get
			{
				if (Pens.silver == null)
				{
					Pens.silver = new Pen(Color.Silver);
					Pens.silver.isModifiable = false;
				}
				return Pens.silver;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00014BB4 File Offset: 0x00012DB4
		public static Pen SkyBlue
		{
			get
			{
				if (Pens.skyblue == null)
				{
					Pens.skyblue = new Pen(Color.SkyBlue);
					Pens.skyblue.isModifiable = false;
				}
				return Pens.skyblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00014BDC File Offset: 0x00012DDC
		public static Pen SlateBlue
		{
			get
			{
				if (Pens.slateblue == null)
				{
					Pens.slateblue = new Pen(Color.SlateBlue);
					Pens.slateblue.isModifiable = false;
				}
				return Pens.slateblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00014C04 File Offset: 0x00012E04
		public static Pen SlateGray
		{
			get
			{
				if (Pens.slategray == null)
				{
					Pens.slategray = new Pen(Color.SlateGray);
					Pens.slategray.isModifiable = false;
				}
				return Pens.slategray;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00014C2C File Offset: 0x00012E2C
		public static Pen Snow
		{
			get
			{
				if (Pens.snow == null)
				{
					Pens.snow = new Pen(Color.Snow);
					Pens.snow.isModifiable = false;
				}
				return Pens.snow;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00014C54 File Offset: 0x00012E54
		public static Pen SpringGreen
		{
			get
			{
				if (Pens.springgreen == null)
				{
					Pens.springgreen = new Pen(Color.SpringGreen);
					Pens.springgreen.isModifiable = false;
				}
				return Pens.springgreen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00014C7C File Offset: 0x00012E7C
		public static Pen SteelBlue
		{
			get
			{
				if (Pens.steelblue == null)
				{
					Pens.steelblue = new Pen(Color.SteelBlue);
					Pens.steelblue.isModifiable = false;
				}
				return Pens.steelblue;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00014CA4 File Offset: 0x00012EA4
		public static Pen Tan
		{
			get
			{
				if (Pens.tan == null)
				{
					Pens.tan = new Pen(Color.Tan);
					Pens.tan.isModifiable = false;
				}
				return Pens.tan;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00014CCC File Offset: 0x00012ECC
		public static Pen Teal
		{
			get
			{
				if (Pens.teal == null)
				{
					Pens.teal = new Pen(Color.Teal);
					Pens.teal.isModifiable = false;
				}
				return Pens.teal;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00014CF4 File Offset: 0x00012EF4
		public static Pen Thistle
		{
			get
			{
				if (Pens.thistle == null)
				{
					Pens.thistle = new Pen(Color.Thistle);
					Pens.thistle.isModifiable = false;
				}
				return Pens.thistle;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00014D1C File Offset: 0x00012F1C
		public static Pen Tomato
		{
			get
			{
				if (Pens.tomato == null)
				{
					Pens.tomato = new Pen(Color.Tomato);
					Pens.tomato.isModifiable = false;
				}
				return Pens.tomato;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00014D44 File Offset: 0x00012F44
		public static Pen Transparent
		{
			get
			{
				if (Pens.transparent == null)
				{
					Pens.transparent = new Pen(Color.Transparent);
					Pens.transparent.isModifiable = false;
				}
				return Pens.transparent;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x00014D6C File Offset: 0x00012F6C
		public static Pen Turquoise
		{
			get
			{
				if (Pens.turquoise == null)
				{
					Pens.turquoise = new Pen(Color.Turquoise);
					Pens.turquoise.isModifiable = false;
				}
				return Pens.turquoise;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x00014D94 File Offset: 0x00012F94
		public static Pen Violet
		{
			get
			{
				if (Pens.violet == null)
				{
					Pens.violet = new Pen(Color.Violet);
					Pens.violet.isModifiable = false;
				}
				return Pens.violet;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00014DBC File Offset: 0x00012FBC
		public static Pen Wheat
		{
			get
			{
				if (Pens.wheat == null)
				{
					Pens.wheat = new Pen(Color.Wheat);
					Pens.wheat.isModifiable = false;
				}
				return Pens.wheat;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00014DE4 File Offset: 0x00012FE4
		public static Pen White
		{
			get
			{
				if (Pens.white == null)
				{
					Pens.white = new Pen(Color.White);
					Pens.white.isModifiable = false;
				}
				return Pens.white;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x00014E0C File Offset: 0x0001300C
		public static Pen WhiteSmoke
		{
			get
			{
				if (Pens.whitesmoke == null)
				{
					Pens.whitesmoke = new Pen(Color.WhiteSmoke);
					Pens.whitesmoke.isModifiable = false;
				}
				return Pens.whitesmoke;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00014E34 File Offset: 0x00013034
		public static Pen Yellow
		{
			get
			{
				if (Pens.yellow == null)
				{
					Pens.yellow = new Pen(Color.Yellow);
					Pens.yellow.isModifiable = false;
				}
				return Pens.yellow;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x00014E5C File Offset: 0x0001305C
		public static Pen YellowGreen
		{
			get
			{
				if (Pens.yellowgreen == null)
				{
					Pens.yellowgreen = new Pen(Color.YellowGreen);
					Pens.yellowgreen.isModifiable = false;
				}
				return Pens.yellowgreen;
			}
		}

		// Token: 0x040004D9 RID: 1241
		private static Pen aliceblue;

		// Token: 0x040004DA RID: 1242
		private static Pen antiquewhite;

		// Token: 0x040004DB RID: 1243
		private static Pen aqua;

		// Token: 0x040004DC RID: 1244
		private static Pen aquamarine;

		// Token: 0x040004DD RID: 1245
		private static Pen azure;

		// Token: 0x040004DE RID: 1246
		private static Pen beige;

		// Token: 0x040004DF RID: 1247
		private static Pen bisque;

		// Token: 0x040004E0 RID: 1248
		private static Pen black;

		// Token: 0x040004E1 RID: 1249
		private static Pen blanchedalmond;

		// Token: 0x040004E2 RID: 1250
		private static Pen blue;

		// Token: 0x040004E3 RID: 1251
		private static Pen blueviolet;

		// Token: 0x040004E4 RID: 1252
		private static Pen brown;

		// Token: 0x040004E5 RID: 1253
		private static Pen burlywood;

		// Token: 0x040004E6 RID: 1254
		private static Pen cadetblue;

		// Token: 0x040004E7 RID: 1255
		private static Pen chartreuse;

		// Token: 0x040004E8 RID: 1256
		private static Pen chocolate;

		// Token: 0x040004E9 RID: 1257
		private static Pen coral;

		// Token: 0x040004EA RID: 1258
		private static Pen cornflowerblue;

		// Token: 0x040004EB RID: 1259
		private static Pen cornsilk;

		// Token: 0x040004EC RID: 1260
		private static Pen crimson;

		// Token: 0x040004ED RID: 1261
		private static Pen cyan;

		// Token: 0x040004EE RID: 1262
		private static Pen darkblue;

		// Token: 0x040004EF RID: 1263
		private static Pen darkcyan;

		// Token: 0x040004F0 RID: 1264
		private static Pen darkgoldenrod;

		// Token: 0x040004F1 RID: 1265
		private static Pen darkgray;

		// Token: 0x040004F2 RID: 1266
		private static Pen darkgreen;

		// Token: 0x040004F3 RID: 1267
		private static Pen darkkhaki;

		// Token: 0x040004F4 RID: 1268
		private static Pen darkmagenta;

		// Token: 0x040004F5 RID: 1269
		private static Pen darkolivegreen;

		// Token: 0x040004F6 RID: 1270
		private static Pen darkorange;

		// Token: 0x040004F7 RID: 1271
		private static Pen darkorchid;

		// Token: 0x040004F8 RID: 1272
		private static Pen darkred;

		// Token: 0x040004F9 RID: 1273
		private static Pen darksalmon;

		// Token: 0x040004FA RID: 1274
		private static Pen darkseagreen;

		// Token: 0x040004FB RID: 1275
		private static Pen darkslateblue;

		// Token: 0x040004FC RID: 1276
		private static Pen darkslategray;

		// Token: 0x040004FD RID: 1277
		private static Pen darkturquoise;

		// Token: 0x040004FE RID: 1278
		private static Pen darkviolet;

		// Token: 0x040004FF RID: 1279
		private static Pen deeppink;

		// Token: 0x04000500 RID: 1280
		private static Pen deepskyblue;

		// Token: 0x04000501 RID: 1281
		private static Pen dimgray;

		// Token: 0x04000502 RID: 1282
		private static Pen dodgerblue;

		// Token: 0x04000503 RID: 1283
		private static Pen firebrick;

		// Token: 0x04000504 RID: 1284
		private static Pen floralwhite;

		// Token: 0x04000505 RID: 1285
		private static Pen forestgreen;

		// Token: 0x04000506 RID: 1286
		private static Pen fuchsia;

		// Token: 0x04000507 RID: 1287
		private static Pen gainsboro;

		// Token: 0x04000508 RID: 1288
		private static Pen ghostwhite;

		// Token: 0x04000509 RID: 1289
		private static Pen gold;

		// Token: 0x0400050A RID: 1290
		private static Pen goldenrod;

		// Token: 0x0400050B RID: 1291
		private static Pen gray;

		// Token: 0x0400050C RID: 1292
		private static Pen green;

		// Token: 0x0400050D RID: 1293
		private static Pen greenyellow;

		// Token: 0x0400050E RID: 1294
		private static Pen honeydew;

		// Token: 0x0400050F RID: 1295
		private static Pen hotpink;

		// Token: 0x04000510 RID: 1296
		private static Pen indianred;

		// Token: 0x04000511 RID: 1297
		private static Pen indigo;

		// Token: 0x04000512 RID: 1298
		private static Pen ivory;

		// Token: 0x04000513 RID: 1299
		private static Pen khaki;

		// Token: 0x04000514 RID: 1300
		private static Pen lavender;

		// Token: 0x04000515 RID: 1301
		private static Pen lavenderblush;

		// Token: 0x04000516 RID: 1302
		private static Pen lawngreen;

		// Token: 0x04000517 RID: 1303
		private static Pen lemonchiffon;

		// Token: 0x04000518 RID: 1304
		private static Pen lightblue;

		// Token: 0x04000519 RID: 1305
		private static Pen lightcoral;

		// Token: 0x0400051A RID: 1306
		private static Pen lightcyan;

		// Token: 0x0400051B RID: 1307
		private static Pen lightgoldenrodyellow;

		// Token: 0x0400051C RID: 1308
		private static Pen lightgray;

		// Token: 0x0400051D RID: 1309
		private static Pen lightgreen;

		// Token: 0x0400051E RID: 1310
		private static Pen lightpink;

		// Token: 0x0400051F RID: 1311
		private static Pen lightsalmon;

		// Token: 0x04000520 RID: 1312
		private static Pen lightseagreen;

		// Token: 0x04000521 RID: 1313
		private static Pen lightskyblue;

		// Token: 0x04000522 RID: 1314
		private static Pen lightslategray;

		// Token: 0x04000523 RID: 1315
		private static Pen lightsteelblue;

		// Token: 0x04000524 RID: 1316
		private static Pen lightyellow;

		// Token: 0x04000525 RID: 1317
		private static Pen lime;

		// Token: 0x04000526 RID: 1318
		private static Pen limegreen;

		// Token: 0x04000527 RID: 1319
		private static Pen linen;

		// Token: 0x04000528 RID: 1320
		private static Pen magenta;

		// Token: 0x04000529 RID: 1321
		private static Pen maroon;

		// Token: 0x0400052A RID: 1322
		private static Pen mediumaquamarine;

		// Token: 0x0400052B RID: 1323
		private static Pen mediumblue;

		// Token: 0x0400052C RID: 1324
		private static Pen mediumorchid;

		// Token: 0x0400052D RID: 1325
		private static Pen mediumpurple;

		// Token: 0x0400052E RID: 1326
		private static Pen mediumseagreen;

		// Token: 0x0400052F RID: 1327
		private static Pen mediumslateblue;

		// Token: 0x04000530 RID: 1328
		private static Pen mediumspringgreen;

		// Token: 0x04000531 RID: 1329
		private static Pen mediumturquoise;

		// Token: 0x04000532 RID: 1330
		private static Pen mediumvioletred;

		// Token: 0x04000533 RID: 1331
		private static Pen midnightblue;

		// Token: 0x04000534 RID: 1332
		private static Pen mintcream;

		// Token: 0x04000535 RID: 1333
		private static Pen mistyrose;

		// Token: 0x04000536 RID: 1334
		private static Pen moccasin;

		// Token: 0x04000537 RID: 1335
		private static Pen navajowhite;

		// Token: 0x04000538 RID: 1336
		private static Pen navy;

		// Token: 0x04000539 RID: 1337
		private static Pen oldlace;

		// Token: 0x0400053A RID: 1338
		private static Pen olive;

		// Token: 0x0400053B RID: 1339
		private static Pen olivedrab;

		// Token: 0x0400053C RID: 1340
		private static Pen orange;

		// Token: 0x0400053D RID: 1341
		private static Pen orangered;

		// Token: 0x0400053E RID: 1342
		private static Pen orchid;

		// Token: 0x0400053F RID: 1343
		private static Pen palegoldenrod;

		// Token: 0x04000540 RID: 1344
		private static Pen palegreen;

		// Token: 0x04000541 RID: 1345
		private static Pen paleturquoise;

		// Token: 0x04000542 RID: 1346
		private static Pen palevioletred;

		// Token: 0x04000543 RID: 1347
		private static Pen papayawhip;

		// Token: 0x04000544 RID: 1348
		private static Pen peachpuff;

		// Token: 0x04000545 RID: 1349
		private static Pen peru;

		// Token: 0x04000546 RID: 1350
		private static Pen pink;

		// Token: 0x04000547 RID: 1351
		private static Pen plum;

		// Token: 0x04000548 RID: 1352
		private static Pen powderblue;

		// Token: 0x04000549 RID: 1353
		private static Pen purple;

		// Token: 0x0400054A RID: 1354
		private static Pen red;

		// Token: 0x0400054B RID: 1355
		private static Pen rosybrown;

		// Token: 0x0400054C RID: 1356
		private static Pen royalblue;

		// Token: 0x0400054D RID: 1357
		private static Pen saddlebrown;

		// Token: 0x0400054E RID: 1358
		private static Pen salmon;

		// Token: 0x0400054F RID: 1359
		private static Pen sandybrown;

		// Token: 0x04000550 RID: 1360
		private static Pen seagreen;

		// Token: 0x04000551 RID: 1361
		private static Pen seashell;

		// Token: 0x04000552 RID: 1362
		private static Pen sienna;

		// Token: 0x04000553 RID: 1363
		private static Pen silver;

		// Token: 0x04000554 RID: 1364
		private static Pen skyblue;

		// Token: 0x04000555 RID: 1365
		private static Pen slateblue;

		// Token: 0x04000556 RID: 1366
		private static Pen slategray;

		// Token: 0x04000557 RID: 1367
		private static Pen snow;

		// Token: 0x04000558 RID: 1368
		private static Pen springgreen;

		// Token: 0x04000559 RID: 1369
		private static Pen steelblue;

		// Token: 0x0400055A RID: 1370
		private static Pen tan;

		// Token: 0x0400055B RID: 1371
		private static Pen teal;

		// Token: 0x0400055C RID: 1372
		private static Pen thistle;

		// Token: 0x0400055D RID: 1373
		private static Pen tomato;

		// Token: 0x0400055E RID: 1374
		private static Pen transparent;

		// Token: 0x0400055F RID: 1375
		private static Pen turquoise;

		// Token: 0x04000560 RID: 1376
		private static Pen violet;

		// Token: 0x04000561 RID: 1377
		private static Pen wheat;

		// Token: 0x04000562 RID: 1378
		private static Pen white;

		// Token: 0x04000563 RID: 1379
		private static Pen whitesmoke;

		// Token: 0x04000564 RID: 1380
		private static Pen yellow;

		// Token: 0x04000565 RID: 1381
		private static Pen yellowgreen;
	}
}
