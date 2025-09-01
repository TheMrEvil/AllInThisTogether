using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies a series of conversion methods that are useful when interoperating with the Win32 printing API. This class cannot be inherited.</summary>
	// Token: 0x020000C6 RID: 198
	public sealed class PrinterUnitConvert
	{
		// Token: 0x06000AB5 RID: 2741 RVA: 0x00002050 File Offset: 0x00000250
		private PrinterUnitConvert()
		{
		}

		/// <summary>Converts a double-precision floating-point number from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.Point" /> being converted.</param>
		/// <param name="fromUnit">The unit to convert from.</param>
		/// <param name="toUnit">The unit to convert to.</param>
		/// <returns>A double-precision floating-point number that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
		// Token: 0x06000AB6 RID: 2742 RVA: 0x000185BC File Offset: 0x000167BC
		public static double Convert(double value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			double num = PrinterUnitConvert.UnitsPerDisplay(fromUnit);
			double num2 = PrinterUnitConvert.UnitsPerDisplay(toUnit);
			return value * num2 / num;
		}

		/// <summary>Converts a 32-bit signed integer from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
		/// <param name="value">The value being converted.</param>
		/// <param name="fromUnit">The unit to convert from.</param>
		/// <param name="toUnit">The unit to convert to.</param>
		/// <returns>A 32-bit signed integer that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
		// Token: 0x06000AB7 RID: 2743 RVA: 0x000185DC File Offset: 0x000167DC
		public static int Convert(int value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			return (int)Math.Round(PrinterUnitConvert.Convert((double)value, fromUnit, toUnit));
		}

		/// <summary>Converts a <see cref="T:System.Drawing.Point" /> from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.Point" /> being converted.</param>
		/// <param name="fromUnit">The unit to convert from.</param>
		/// <param name="toUnit">The unit to convert to.</param>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
		// Token: 0x06000AB8 RID: 2744 RVA: 0x000185ED File Offset: 0x000167ED
		public static Point Convert(Point value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			return new Point(PrinterUnitConvert.Convert(value.X, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Y, fromUnit, toUnit));
		}

		/// <summary>Converts a <see cref="T:System.Drawing.Size" /> from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.Size" /> being converted.</param>
		/// <param name="fromUnit">The unit to convert from.</param>
		/// <param name="toUnit">The unit to convert to.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
		// Token: 0x06000AB9 RID: 2745 RVA: 0x00018610 File Offset: 0x00016810
		public static Size Convert(Size value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			return new Size(PrinterUnitConvert.Convert(value.Width, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Height, fromUnit, toUnit));
		}

		/// <summary>Converts a <see cref="T:System.Drawing.Rectangle" /> from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.Rectangle" /> being converted.</param>
		/// <param name="fromUnit">The unit to convert from.</param>
		/// <param name="toUnit">The unit to convert to.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
		// Token: 0x06000ABA RID: 2746 RVA: 0x00018633 File Offset: 0x00016833
		public static Rectangle Convert(Rectangle value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			return new Rectangle(PrinterUnitConvert.Convert(value.X, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Y, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Width, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Height, fromUnit, toUnit));
		}

		/// <summary>Converts a <see cref="T:System.Drawing.Printing.Margins" /> from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.Printing.Margins" /> being converted.</param>
		/// <param name="fromUnit">The unit to convert from.</param>
		/// <param name="toUnit">The unit to convert to.</param>
		/// <returns>A <see cref="T:System.Drawing.Printing.Margins" /> that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
		// Token: 0x06000ABB RID: 2747 RVA: 0x00018674 File Offset: 0x00016874
		public static Margins Convert(Margins value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			return new Margins
			{
				DoubleLeft = PrinterUnitConvert.Convert(value.DoubleLeft, fromUnit, toUnit),
				DoubleRight = PrinterUnitConvert.Convert(value.DoubleRight, fromUnit, toUnit),
				DoubleTop = PrinterUnitConvert.Convert(value.DoubleTop, fromUnit, toUnit),
				DoubleBottom = PrinterUnitConvert.Convert(value.DoubleBottom, fromUnit, toUnit)
			};
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000186D4 File Offset: 0x000168D4
		private static double UnitsPerDisplay(PrinterUnit unit)
		{
			double result;
			switch (unit)
			{
			case PrinterUnit.Display:
				result = 1.0;
				break;
			case PrinterUnit.ThousandthsOfAnInch:
				result = 10.0;
				break;
			case PrinterUnit.HundredthsOfAMillimeter:
				result = 25.4;
				break;
			case PrinterUnit.TenthsOfAMillimeter:
				result = 2.54;
				break;
			default:
				result = 1.0;
				break;
			}
			return result;
		}
	}
}
