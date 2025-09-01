using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Encapsulates a <see cref="T:System.Drawing.Brush" /> with a linear gradient. This class cannot be inherited.</summary>
	// Token: 0x02000156 RID: 342
	public sealed class LinearGradientBrush : Brush
	{
		// Token: 0x06000EBC RID: 3772 RVA: 0x000216F1 File Offset: 0x0001F8F1
		internal LinearGradientBrush(IntPtr native)
		{
			Status status = GDIPlus.GdipGetLineRect(native, out this.rectangle);
			base.SetNativeBrush(native);
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class with the specified points and colors.</summary>
		/// <param name="point1">A <see cref="T:System.Drawing.Point" /> structure that represents the starting point of the linear gradient.</param>
		/// <param name="point2">A <see cref="T:System.Drawing.Point" /> structure that represents the endpoint of the linear gradient.</param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color of the linear gradient.</param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color of the linear gradient.</param>
		// Token: 0x06000EBD RID: 3773 RVA: 0x00021714 File Offset: 0x0001F914
		public LinearGradientBrush(Point point1, Point point2, Color color1, Color color2)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrushI(ref point1, ref point2, color1.ToArgb(), color2.ToArgb(), WrapMode.Tile, out intPtr));
			base.SetNativeBrush(intPtr);
			GDIPlus.CheckStatus(GDIPlus.GdipGetLineRect(intPtr, out this.rectangle));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class with the specified points and colors.</summary>
		/// <param name="point1">A <see cref="T:System.Drawing.PointF" /> structure that represents the starting point of the linear gradient.</param>
		/// <param name="point2">A <see cref="T:System.Drawing.PointF" /> structure that represents the endpoint of the linear gradient.</param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color of the linear gradient.</param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color of the linear gradient.</param>
		// Token: 0x06000EBE RID: 3774 RVA: 0x00021760 File Offset: 0x0001F960
		public LinearGradientBrush(PointF point1, PointF point2, Color color1, Color color2)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrush(ref point1, ref point2, color1.ToArgb(), color2.ToArgb(), WrapMode.Tile, out intPtr));
			base.SetNativeBrush(intPtr);
			GDIPlus.CheckStatus(GDIPlus.GdipGetLineRect(intPtr, out this.rectangle));
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class based on a rectangle, starting and ending colors, and orientation.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that specifies the bounds of the linear gradient.</param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient.</param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient.</param>
		/// <param name="linearGradientMode">A <see cref="T:System.Drawing.Drawing2D.LinearGradientMode" /> enumeration element that specifies the orientation of the gradient. The orientation determines the starting and ending points of the gradient. For example, <see langword="LinearGradientMode.ForwardDiagonal" /> specifies that the starting point is the upper-left corner of the rectangle and the ending point is the lower-right corner of the rectangle.</param>
		// Token: 0x06000EBF RID: 3775 RVA: 0x000217AC File Offset: 0x0001F9AC
		public LinearGradientBrush(Rectangle rect, Color color1, Color color2, LinearGradientMode linearGradientMode)
		{
			if (linearGradientMode < LinearGradientMode.Horizontal || linearGradientMode > LinearGradientMode.BackwardDiagonal)
			{
				throw new InvalidEnumArgumentException("linearGradientMode", (int)linearGradientMode, typeof(LinearGradientMode));
			}
			if (rect.Width == 0 || rect.Height == 0)
			{
				throw new ArgumentException(string.Format("Rectangle '{0}' cannot have a width or height equal to 0.", rect.ToString()));
			}
			IntPtr nativeBrush;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrushFromRectI(ref rect, color1.ToArgb(), color2.ToArgb(), linearGradientMode, WrapMode.Tile, out nativeBrush));
			base.SetNativeBrush(nativeBrush);
			this.rectangle = rect;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class based on a rectangle, starting and ending colors, and an orientation angle.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that specifies the bounds of the linear gradient.</param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient.</param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient.</param>
		/// <param name="angle">The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line.</param>
		// Token: 0x06000EC0 RID: 3776 RVA: 0x00021841 File Offset: 0x0001FA41
		public LinearGradientBrush(Rectangle rect, Color color1, Color color2, float angle) : this(rect, color1, color2, angle, false)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> based on a rectangle, starting and ending colors, and an orientation mode.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> structure that specifies the bounds of the linear gradient.</param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient.</param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient.</param>
		/// <param name="linearGradientMode">A <see cref="T:System.Drawing.Drawing2D.LinearGradientMode" /> enumeration element that specifies the orientation of the gradient. The orientation determines the starting and ending points of the gradient. For example, <see langword="LinearGradientMode.ForwardDiagonal" /> specifies that the starting point is the upper-left corner of the rectangle and the ending point is the lower-right corner of the rectangle.</param>
		// Token: 0x06000EC1 RID: 3777 RVA: 0x00021850 File Offset: 0x0001FA50
		public LinearGradientBrush(RectangleF rect, Color color1, Color color2, LinearGradientMode linearGradientMode)
		{
			if (linearGradientMode < LinearGradientMode.Horizontal || linearGradientMode > LinearGradientMode.BackwardDiagonal)
			{
				throw new InvalidEnumArgumentException("linearGradientMode", (int)linearGradientMode, typeof(LinearGradientMode));
			}
			if ((double)rect.Width == 0.0 || (double)rect.Height == 0.0)
			{
				throw new ArgumentException(string.Format("Rectangle '{0}' cannot have a width or height equal to 0.", rect.ToString()));
			}
			IntPtr nativeBrush;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrushFromRect(ref rect, color1.ToArgb(), color2.ToArgb(), linearGradientMode, WrapMode.Tile, out nativeBrush));
			base.SetNativeBrush(nativeBrush);
			this.rectangle = rect;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class based on a rectangle, starting and ending colors, and an orientation angle.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> structure that specifies the bounds of the linear gradient.</param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient.</param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient.</param>
		/// <param name="angle">The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line.</param>
		// Token: 0x06000EC2 RID: 3778 RVA: 0x000218F4 File Offset: 0x0001FAF4
		public LinearGradientBrush(RectangleF rect, Color color1, Color color2, float angle) : this(rect, color1, color2, angle, false)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class based on a rectangle, starting and ending colors, and an orientation angle.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that specifies the bounds of the linear gradient.</param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient.</param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient.</param>
		/// <param name="angle">The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line.</param>
		/// <param name="isAngleScaleable">Set to <see langword="true" /> to specify that the angle is affected by the transform associated with this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />; otherwise, <see langword="false" />.</param>
		// Token: 0x06000EC3 RID: 3779 RVA: 0x00021904 File Offset: 0x0001FB04
		public LinearGradientBrush(Rectangle rect, Color color1, Color color2, float angle, bool isAngleScaleable)
		{
			if (rect.Width == 0 || rect.Height == 0)
			{
				throw new ArgumentException(string.Format("Rectangle '{0}' cannot have a width or height equal to 0.", rect.ToString()));
			}
			IntPtr nativeBrush;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrushFromRectWithAngleI(ref rect, color1.ToArgb(), color2.ToArgb(), angle, isAngleScaleable, WrapMode.Tile, out nativeBrush));
			base.SetNativeBrush(nativeBrush);
			this.rectangle = rect;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class based on a rectangle, starting and ending colors, and an orientation angle.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> structure that specifies the bounds of the linear gradient.</param>
		/// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient.</param>
		/// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient.</param>
		/// <param name="angle">The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line.</param>
		/// <param name="isAngleScaleable">Set to <see langword="true" /> to specify that the angle is affected by the transform associated with this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />; otherwise, <see langword="false" />.</param>
		// Token: 0x06000EC4 RID: 3780 RVA: 0x0002197C File Offset: 0x0001FB7C
		public LinearGradientBrush(RectangleF rect, Color color1, Color color2, float angle, bool isAngleScaleable)
		{
			if (rect.Width == 0f || rect.Height == 0f)
			{
				throw new ArgumentException(string.Format("Rectangle '{0}' cannot have a width or height equal to 0.", rect.ToString()));
			}
			IntPtr nativeBrush;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrushFromRectWithAngle(ref rect, color1.ToArgb(), color2.ToArgb(), angle, isAngleScaleable, WrapMode.Tile, out nativeBrush));
			base.SetNativeBrush(nativeBrush);
			this.rectangle = rect;
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.Blend" /> that specifies positions and factors that define a custom falloff for the gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.Blend" /> that represents a custom falloff for the gradient.</returns>
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x000219F8 File Offset: 0x0001FBF8
		// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x00021A58 File Offset: 0x0001FC58
		public Blend Blend
		{
			get
			{
				if (this._interpolationColorsWasSet)
				{
					return null;
				}
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineBlendCount(base.NativeBrush, out num));
				float[] array = new float[num];
				float[] positions = new float[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineBlend(base.NativeBrush, array, positions, num));
				return new Blend
				{
					Factors = array,
					Positions = positions
				};
			}
			set
			{
				float[] factors = value.Factors;
				float[] positions = value.Positions;
				int num = factors.Length;
				if (num == 0 || positions.Length == 0)
				{
					throw new ArgumentException("Invalid Blend object. It should have at least 2 elements in each of the factors and positions arrays.");
				}
				if (num != positions.Length)
				{
					throw new ArgumentException("Invalid Blend object. It should contain the same number of factors and positions values.");
				}
				if (positions[0] != 0f)
				{
					throw new ArgumentException("Invalid Blend object. The positions array must have 0.0 as its first element.");
				}
				if (positions[num - 1] != 1f)
				{
					throw new ArgumentException("Invalid Blend object. The positions array must have 1.0 as its last element.");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetLineBlend(base.NativeBrush, factors, positions, num));
			}
		}

		/// <summary>Gets or sets a value indicating whether gamma correction is enabled for this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />.</summary>
		/// <returns>The value is <see langword="true" /> if gamma correction is enabled for this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00021ADC File Offset: 0x0001FCDC
		// (set) Token: 0x06000EC8 RID: 3784 RVA: 0x00021AFC File Offset: 0x0001FCFC
		[MonoTODO("The GammaCorrection value is ignored when using libgdiplus.")]
		public bool GammaCorrection
		{
			get
			{
				bool result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineGammaCorrection(base.NativeBrush, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetLineGammaCorrection(base.NativeBrush, value));
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> that defines a multicolor linear gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> that defines a multicolor linear gradient.</returns>
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00021B10 File Offset: 0x0001FD10
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x00021BA4 File Offset: 0x0001FDA4
		public ColorBlend InterpolationColors
		{
			get
			{
				if (!this._interpolationColorsWasSet)
				{
					throw new ArgumentException("Property must be set to a valid ColorBlend object to use interpolation colors.");
				}
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetLinePresetBlendCount(base.NativeBrush, out num));
				int[] array = new int[num];
				float[] positions = new float[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetLinePresetBlend(base.NativeBrush, array, positions, num));
				ColorBlend colorBlend = new ColorBlend();
				Color[] array2 = new Color[num];
				for (int i = 0; i < num; i++)
				{
					array2[i] = Color.FromArgb(array[i]);
				}
				colorBlend.Colors = array2;
				colorBlend.Positions = positions;
				return colorBlend;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentException("InterpolationColors is null");
				}
				Color[] colors = value.Colors;
				float[] positions = value.Positions;
				int num = colors.Length;
				if (num == 0 || positions.Length == 0)
				{
					throw new ArgumentException("Invalid ColorBlend object. It should have at least 2 elements in each of the colors and positions arrays.");
				}
				if (num != positions.Length)
				{
					throw new ArgumentException("Invalid ColorBlend object. It should contain the same number of positions and color values.");
				}
				if (positions[0] != 0f)
				{
					throw new ArgumentException("Invalid ColorBlend object. The positions array must have 0.0 as its first element.");
				}
				if (positions[num - 1] != 1f)
				{
					throw new ArgumentException("Invalid ColorBlend object. The positions array must have 1.0 as its last element.");
				}
				int[] array = new int[colors.Length];
				for (int i = 0; i < colors.Length; i++)
				{
					array[i] = colors[i].ToArgb();
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetLinePresetBlend(base.NativeBrush, array, positions, num));
				this._interpolationColorsWasSet = true;
			}
		}

		/// <summary>Gets or sets the starting and ending colors of the gradient.</summary>
		/// <returns>An array of two <see cref="T:System.Drawing.Color" /> structures that represents the starting and ending colors of the gradient.</returns>
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00021C68 File Offset: 0x0001FE68
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x00021CB1 File Offset: 0x0001FEB1
		public Color[] LinearColors
		{
			get
			{
				int[] array = new int[2];
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineColors(base.NativeBrush, array));
				return new Color[]
				{
					Color.FromArgb(array[0]),
					Color.FromArgb(array[1])
				};
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetLineColors(base.NativeBrush, value[0].ToArgb(), value[1].ToArgb()));
			}
		}

		/// <summary>Gets a rectangular region that defines the starting and ending points of the gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> structure that specifies the starting and ending points of the gradient.</returns>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00021CDB File Offset: 0x0001FEDB
		public RectangleF Rectangle
		{
			get
			{
				return this.rectangle;
			}
		}

		/// <summary>Gets or sets a copy <see cref="T:System.Drawing.Drawing2D.Matrix" /> that defines a local geometric transform for this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that defines a geometric transform that applies only to fills drawn with this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />.</returns>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00021CE4 File Offset: 0x0001FEE4
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x00021D0E File Offset: 0x0001FF0E
		public Matrix Transform
		{
			get
			{
				Matrix matrix = new Matrix();
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineTransform(base.NativeBrush, matrix.nativeMatrix));
				return matrix;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Transform");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetLineTransform(base.NativeBrush, value.nativeMatrix));
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.WrapMode" /> enumeration that indicates the wrap mode for this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> are tiled.</returns>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00021D34 File Offset: 0x0001FF34
		// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x00021D54 File Offset: 0x0001FF54
		public WrapMode WrapMode
		{
			get
			{
				WrapMode result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineWrapMode(base.NativeBrush, out result));
				return result;
			}
			set
			{
				if (value < WrapMode.Tile || value > WrapMode.Clamp)
				{
					throw new InvalidEnumArgumentException("WrapMode");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetLineWrapMode(base.NativeBrush, value));
			}
		}

		/// <summary>Multiplies the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents the local geometric transform of this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> by prepending the specified <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to multiply the geometric transform.</param>
		// Token: 0x06000ED2 RID: 3794 RVA: 0x00021D7A File Offset: 0x0001FF7A
		public void MultiplyTransform(Matrix matrix)
		{
			this.MultiplyTransform(matrix, MatrixOrder.Prepend);
		}

		/// <summary>Multiplies the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents the local geometric transform of this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to multiply the geometric transform.</param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies in which order to multiply the two matrices.</param>
		// Token: 0x06000ED3 RID: 3795 RVA: 0x00021D84 File Offset: 0x0001FF84
		public void MultiplyTransform(Matrix matrix, MatrixOrder order)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipMultiplyLineTransform(base.NativeBrush, matrix.nativeMatrix, order));
		}

		/// <summary>Resets the <see cref="P:System.Drawing.Drawing2D.LinearGradientBrush.Transform" /> property to identity.</summary>
		// Token: 0x06000ED4 RID: 3796 RVA: 0x00021DAB File Offset: 0x0001FFAB
		public void ResetTransform()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipResetLineTransform(base.NativeBrush));
		}

		/// <summary>Rotates the local geometric transform by the specified amount. This method prepends the rotation to the transform.</summary>
		/// <param name="angle">The angle of rotation.</param>
		// Token: 0x06000ED5 RID: 3797 RVA: 0x00021DBD File Offset: 0x0001FFBD
		public void RotateTransform(float angle)
		{
			this.RotateTransform(angle, MatrixOrder.Prepend);
		}

		/// <summary>Rotates the local geometric transform by the specified amount in the specified order.</summary>
		/// <param name="angle">The angle of rotation.</param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the rotation matrix.</param>
		// Token: 0x06000ED6 RID: 3798 RVA: 0x00021DC7 File Offset: 0x0001FFC7
		public void RotateTransform(float angle, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRotateLineTransform(base.NativeBrush, angle, order));
		}

		/// <summary>Scales the local geometric transform by the specified amounts. This method prepends the scaling matrix to the transform.</summary>
		/// <param name="sx">The amount by which to scale the transform in the x-axis direction.</param>
		/// <param name="sy">The amount by which to scale the transform in the y-axis direction.</param>
		// Token: 0x06000ED7 RID: 3799 RVA: 0x00021DDB File Offset: 0x0001FFDB
		public void ScaleTransform(float sx, float sy)
		{
			this.ScaleTransform(sx, sy, MatrixOrder.Prepend);
		}

		/// <summary>Scales the local geometric transform by the specified amounts in the specified order.</summary>
		/// <param name="sx">The amount by which to scale the transform in the x-axis direction.</param>
		/// <param name="sy">The amount by which to scale the transform in the y-axis direction.</param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the scaling matrix.</param>
		// Token: 0x06000ED8 RID: 3800 RVA: 0x00021DE6 File Offset: 0x0001FFE6
		public void ScaleTransform(float sx, float sy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipScaleLineTransform(base.NativeBrush, sx, sy, order));
		}

		/// <summary>Creates a linear gradient with a center color and a linear falloff to a single color on both ends.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies the center of the gradient (the point where the gradient is composed of only the ending color).</param>
		// Token: 0x06000ED9 RID: 3801 RVA: 0x00021DFB File Offset: 0x0001FFFB
		public void SetBlendTriangularShape(float focus)
		{
			this.SetBlendTriangularShape(focus, 1f);
		}

		/// <summary>Creates a linear gradient with a center color and a linear falloff to a single color on both ends.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies the center of the gradient (the point where the gradient is composed of only the ending color).</param>
		/// <param name="scale">A value from 0 through1 that specifies how fast the colors falloff from the starting color to <paramref name="focus" /> (ending color)</param>
		// Token: 0x06000EDA RID: 3802 RVA: 0x00021E0C File Offset: 0x0002000C
		public void SetBlendTriangularShape(float focus, float scale)
		{
			if (focus < 0f || focus > 1f || scale < 0f || scale > 1f)
			{
				throw new ArgumentException("Invalid parameter passed.");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetLineLinearBlend(base.NativeBrush, focus, scale));
			this._interpolationColorsWasSet = false;
		}

		/// <summary>Creates a gradient falloff based on a bell-shaped curve.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies the center of the gradient (the point where the starting color and ending color are blended equally).</param>
		// Token: 0x06000EDB RID: 3803 RVA: 0x00021E5D File Offset: 0x0002005D
		public void SetSigmaBellShape(float focus)
		{
			this.SetSigmaBellShape(focus, 1f);
		}

		/// <summary>Creates a gradient falloff based on a bell-shaped curve.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies the center of the gradient (the point where the gradient is composed of only the ending color).</param>
		/// <param name="scale">A value from 0 through 1 that specifies how fast the colors falloff from the <paramref name="focus" />.</param>
		// Token: 0x06000EDC RID: 3804 RVA: 0x00021E6C File Offset: 0x0002006C
		public void SetSigmaBellShape(float focus, float scale)
		{
			if (focus < 0f || focus > 1f || scale < 0f || scale > 1f)
			{
				throw new ArgumentException("Invalid parameter passed.");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetLineSigmaBlend(base.NativeBrush, focus, scale));
			this._interpolationColorsWasSet = false;
		}

		/// <summary>Translates the local geometric transform by the specified dimensions. This method prepends the translation to the transform.</summary>
		/// <param name="dx">The value of the translation in x.</param>
		/// <param name="dy">The value of the translation in y.</param>
		// Token: 0x06000EDD RID: 3805 RVA: 0x00021EBD File Offset: 0x000200BD
		public void TranslateTransform(float dx, float dy)
		{
			this.TranslateTransform(dx, dy, MatrixOrder.Prepend);
		}

		/// <summary>Translates the local geometric transform by the specified dimensions in the specified order.</summary>
		/// <param name="dx">The value of the translation in x.</param>
		/// <param name="dy">The value of the translation in y.</param>
		/// <param name="order">The order (prepend or append) in which to apply the translation.</param>
		// Token: 0x06000EDE RID: 3806 RVA: 0x00021EC8 File Offset: 0x000200C8
		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslateLineTransform(base.NativeBrush, dx, dy, order));
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> this method creates, cast as an object.</returns>
		// Token: 0x06000EDF RID: 3807 RVA: 0x00021EE0 File Offset: 0x000200E0
		public override object Clone()
		{
			IntPtr native;
			GDIPlus.CheckStatus((Status)GDIPlus.GdipCloneBrush(new HandleRef(this, base.NativeBrush), out native));
			return new LinearGradientBrush(native);
		}

		// Token: 0x04000B76 RID: 2934
		private RectangleF rectangle;

		// Token: 0x04000B77 RID: 2935
		private bool _interpolationColorsWasSet;
	}
}
