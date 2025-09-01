using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Defines a rectangular brush with a hatch style, a foreground color, and a background color. This class cannot be inherited.</summary>
	// Token: 0x02000142 RID: 322
	public sealed class HatchBrush : Brush
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> class with the specified <see cref="T:System.Drawing.Drawing2D.HatchStyle" /> enumeration and foreground color.</summary>
		/// <param name="hatchstyle">One of the <see cref="T:System.Drawing.Drawing2D.HatchStyle" /> values that represents the pattern drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> structure that represents the color of lines drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</param>
		// Token: 0x06000E3F RID: 3647 RVA: 0x00020593 File Offset: 0x0001E793
		public HatchBrush(HatchStyle hatchstyle, Color foreColor) : this(hatchstyle, foreColor, Color.FromArgb(-16777216))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> class with the specified <see cref="T:System.Drawing.Drawing2D.HatchStyle" /> enumeration, foreground color, and background color.</summary>
		/// <param name="hatchstyle">One of the <see cref="T:System.Drawing.Drawing2D.HatchStyle" /> values that represents the pattern drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> structure that represents the color of lines drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> structure that represents the color of spaces between the lines drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</param>
		// Token: 0x06000E40 RID: 3648 RVA: 0x000205A8 File Offset: 0x0001E7A8
		public HatchBrush(HatchStyle hatchstyle, Color foreColor, Color backColor)
		{
			if (hatchstyle < HatchStyle.Horizontal || hatchstyle > HatchStyle.SolidDiamond)
			{
				throw new ArgumentException(SR.Format("The value of argument '{0}' ({1}) is invalid for Enum type '{2}'.", new object[]
				{
					"hatchstyle",
					hatchstyle,
					"HatchStyle"
				}), "hatchstyle");
			}
			IntPtr nativeBrushInternal;
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipCreateHatchBrush((int)hatchstyle, foreColor.ToArgb(), backColor.ToArgb(), out nativeBrushInternal));
			base.SetNativeBrushInternal(nativeBrushInternal);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00005F38 File Offset: 0x00004138
		internal HatchBrush(IntPtr nativeBrush)
		{
			base.SetNativeBrushInternal(nativeBrush);
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> object.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> this method creates, cast as an object.</returns>
		// Token: 0x06000E42 RID: 3650 RVA: 0x0002061C File Offset: 0x0001E81C
		public override object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipCloneBrush(new HandleRef(this, base.NativeBrush), out zero));
			return new HatchBrush(zero);
		}

		/// <summary>Gets the hatch style of this <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> object.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Drawing2D.HatchStyle" /> values that represents the pattern of this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</returns>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x00020650 File Offset: 0x0001E850
		public HatchStyle HatchStyle
		{
			get
			{
				int result;
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetHatchStyle(new HandleRef(this, base.NativeBrush), out result));
				return (HatchStyle)result;
			}
		}

		/// <summary>Gets the color of hatch lines drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the foreground color for this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</returns>
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x00020678 File Offset: 0x0001E878
		public Color ForegroundColor
		{
			get
			{
				int argb;
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetHatchForegroundColor(new HandleRef(this, base.NativeBrush), out argb));
				return Color.FromArgb(argb);
			}
		}

		/// <summary>Gets the color of spaces between the hatch lines drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the background color for this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</returns>
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x000206A4 File Offset: 0x0001E8A4
		public Color BackgroundColor
		{
			get
			{
				int argb;
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetHatchBackgroundColor(new HandleRef(this, base.NativeBrush), out argb));
				return Color.FromArgb(argb);
			}
		}
	}
}
