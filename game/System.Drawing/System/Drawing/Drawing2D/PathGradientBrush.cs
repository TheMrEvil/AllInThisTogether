using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Encapsulates a <see cref="T:System.Drawing.Brush" /> object that fills the interior of a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object with a gradient. This class cannot be inherited.</summary>
	// Token: 0x02000158 RID: 344
	[MonoTODO("libgdiplus/cairo doesn't support path gradients - unless it can be mapped to a radial gradient")]
	public sealed class PathGradientBrush : Brush
	{
		// Token: 0x06000F04 RID: 3844 RVA: 0x00022430 File Offset: 0x00020630
		internal PathGradientBrush(IntPtr native)
		{
			base.SetNativeBrush(native);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified path.</summary>
		/// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that defines the area filled by this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</param>
		// Token: 0x06000F05 RID: 3845 RVA: 0x00022440 File Offset: 0x00020640
		public PathGradientBrush(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			IntPtr nativeBrush;
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePathGradientFromPath(path.nativePath, out nativeBrush));
			base.SetNativeBrush(nativeBrush);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified points.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that make up the vertices of the path.</param>
		// Token: 0x06000F06 RID: 3846 RVA: 0x0002247A File Offset: 0x0002067A
		public PathGradientBrush(Point[] points) : this(points, WrapMode.Clamp)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified points.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that make up the vertices of the path.</param>
		// Token: 0x06000F07 RID: 3847 RVA: 0x00022484 File Offset: 0x00020684
		public PathGradientBrush(PointF[] points) : this(points, WrapMode.Clamp)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified points and wrap mode.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that make up the vertices of the path.</param>
		/// <param name="wrapMode">A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> are tiled.</param>
		// Token: 0x06000F08 RID: 3848 RVA: 0x00022490 File Offset: 0x00020690
		public PathGradientBrush(Point[] points, WrapMode wrapMode)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (wrapMode < WrapMode.Tile || wrapMode > WrapMode.Clamp)
			{
				throw new InvalidEnumArgumentException("WrapMode");
			}
			IntPtr nativeBrush;
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePathGradientI(points, points.Length, wrapMode, out nativeBrush));
			base.SetNativeBrush(nativeBrush);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified points and wrap mode.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that make up the vertices of the path.</param>
		/// <param name="wrapMode">A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> are tiled.</param>
		// Token: 0x06000F09 RID: 3849 RVA: 0x000224DC File Offset: 0x000206DC
		public PathGradientBrush(PointF[] points, WrapMode wrapMode)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (wrapMode < WrapMode.Tile || wrapMode > WrapMode.Clamp)
			{
				throw new InvalidEnumArgumentException("WrapMode");
			}
			IntPtr nativeBrush;
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePathGradient(points, points.Length, wrapMode, out nativeBrush));
			base.SetNativeBrush(nativeBrush);
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.Blend" /> that specifies positions and factors that define a custom falloff for the gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.Blend" /> that represents a custom falloff for the gradient.</returns>
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00022528 File Offset: 0x00020728
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x0002257C File Offset: 0x0002077C
		public Blend Blend
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientBlendCount(base.NativeBrush, out num));
				float[] array = new float[num];
				float[] positions = new float[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientBlend(base.NativeBrush, array, positions, num));
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
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientBlend(base.NativeBrush, factors, positions, num));
			}
		}

		/// <summary>Gets or sets the color at the center of the path gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color at the center of the path gradient.</returns>
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00022600 File Offset: 0x00020800
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x00022625 File Offset: 0x00020825
		public Color CenterColor
		{
			get
			{
				int argb;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientCenterColor(base.NativeBrush, out argb));
				return Color.FromArgb(argb);
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientCenterColor(base.NativeBrush, value.ToArgb()));
			}
		}

		/// <summary>Gets or sets the center point of the path gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.PointF" /> that represents the center point of the path gradient.</returns>
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x00022640 File Offset: 0x00020840
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x00022660 File Offset: 0x00020860
		public PointF CenterPoint
		{
			get
			{
				PointF result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientCenterPoint(base.NativeBrush, out result));
				return result;
			}
			set
			{
				PointF pointF = value;
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientCenterPoint(base.NativeBrush, ref pointF));
			}
		}

		/// <summary>Gets or sets the focus point for the gradient falloff.</summary>
		/// <returns>A <see cref="T:System.Drawing.PointF" /> that represents the focus point for the gradient falloff.</returns>
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00022684 File Offset: 0x00020884
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x000226AC File Offset: 0x000208AC
		public PointF FocusScales
		{
			get
			{
				float x;
				float y;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientFocusScales(base.NativeBrush, out x, out y));
				return new PointF(x, y);
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientFocusScales(base.NativeBrush, value.X, value.Y));
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> that defines a multicolor linear gradient.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> that defines a multicolor linear gradient.</returns>
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x000226CC File Offset: 0x000208CC
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x00022758 File Offset: 0x00020958
		public ColorBlend InterpolationColors
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientPresetBlendCount(base.NativeBrush, out num));
				if (num < 1)
				{
					num = 1;
				}
				int[] array = new int[num];
				float[] positions = new float[num];
				if (num > 1)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientPresetBlend(base.NativeBrush, array, positions, num));
				}
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
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientPresetBlend(base.NativeBrush, array, positions, num));
			}
		}

		/// <summary>Gets a bounding rectangle for this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> that represents a rectangular region that bounds the path this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> fills.</returns>
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x00022808 File Offset: 0x00020A08
		public RectangleF Rectangle
		{
			get
			{
				RectangleF result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientRect(base.NativeBrush, out result));
				return result;
			}
		}

		/// <summary>Gets or sets an array of colors that correspond to the points in the path this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> fills.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.Color" /> structures that represents the colors associated with each point in the path this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> fills.</returns>
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x00022828 File Offset: 0x00020A28
		// (set) Token: 0x06000F16 RID: 3862 RVA: 0x00022884 File Offset: 0x00020A84
		public Color[] SurroundColors
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientSurroundColorCount(base.NativeBrush, out num));
				int[] array = new int[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientSurroundColorsWithCount(base.NativeBrush, array, ref num));
				Color[] array2 = new Color[num];
				for (int i = 0; i < num; i++)
				{
					array2[i] = Color.FromArgb(array[i]);
				}
				return array2;
			}
			set
			{
				int num = value.Length;
				int[] array = new int[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = value[i].ToArgb();
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientSurroundColorsWithCount(base.NativeBrush, array, ref num));
			}
		}

		/// <summary>Gets or sets a copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that defines a local geometric transform for this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that defines a geometric transform that applies only to fills drawn with this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</returns>
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x000228CC File Offset: 0x00020ACC
		// (set) Token: 0x06000F18 RID: 3864 RVA: 0x000228F6 File Offset: 0x00020AF6
		public Matrix Transform
		{
			get
			{
				Matrix matrix = new Matrix();
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientTransform(base.NativeBrush, matrix.nativeMatrix));
				return matrix;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Transform");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientTransform(base.NativeBrush, value.nativeMatrix));
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that indicates the wrap mode for this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> are tiled.</returns>
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x0002291C File Offset: 0x00020B1C
		// (set) Token: 0x06000F1A RID: 3866 RVA: 0x0002293C File Offset: 0x00020B3C
		public WrapMode WrapMode
		{
			get
			{
				WrapMode result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientWrapMode(base.NativeBrush, out result));
				return result;
			}
			set
			{
				if (value < WrapMode.Tile || value > WrapMode.Clamp)
				{
					throw new InvalidEnumArgumentException("WrapMode");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientWrapMode(base.NativeBrush, value));
			}
		}

		/// <summary>Updates the brush's transformation matrix with the product of brush's transformation matrix multiplied by another matrix.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that will be multiplied by the brush's current transformation matrix.</param>
		// Token: 0x06000F1B RID: 3867 RVA: 0x00022962 File Offset: 0x00020B62
		public void MultiplyTransform(Matrix matrix)
		{
			this.MultiplyTransform(matrix, MatrixOrder.Prepend);
		}

		/// <summary>Updates the brush's transformation matrix with the product of the brush's transformation matrix multiplied by another matrix.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that will be multiplied by the brush's current transformation matrix.</param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies in which order to multiply the two matrices.</param>
		// Token: 0x06000F1C RID: 3868 RVA: 0x0002296C File Offset: 0x00020B6C
		public void MultiplyTransform(Matrix matrix, MatrixOrder order)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipMultiplyPathGradientTransform(base.NativeBrush, matrix.nativeMatrix, order));
		}

		/// <summary>Resets the <see cref="P:System.Drawing.Drawing2D.PathGradientBrush.Transform" /> property to identity.</summary>
		// Token: 0x06000F1D RID: 3869 RVA: 0x00022993 File Offset: 0x00020B93
		public void ResetTransform()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipResetPathGradientTransform(base.NativeBrush));
		}

		/// <summary>Rotates the local geometric transform by the specified amount. This method prepends the rotation to the transform.</summary>
		/// <param name="angle">The angle (extent) of rotation.</param>
		// Token: 0x06000F1E RID: 3870 RVA: 0x000229A5 File Offset: 0x00020BA5
		public void RotateTransform(float angle)
		{
			this.RotateTransform(angle, MatrixOrder.Prepend);
		}

		/// <summary>Rotates the local geometric transform by the specified amount in the specified order.</summary>
		/// <param name="angle">The angle (extent) of rotation.</param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the rotation matrix.</param>
		// Token: 0x06000F1F RID: 3871 RVA: 0x000229AF File Offset: 0x00020BAF
		public void RotateTransform(float angle, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRotatePathGradientTransform(base.NativeBrush, angle, order));
		}

		/// <summary>Scales the local geometric transform by the specified amounts. This method prepends the scaling matrix to the transform.</summary>
		/// <param name="sx">The transform scale factor in the x-axis direction.</param>
		/// <param name="sy">The transform scale factor in the y-axis direction.</param>
		// Token: 0x06000F20 RID: 3872 RVA: 0x000229C3 File Offset: 0x00020BC3
		public void ScaleTransform(float sx, float sy)
		{
			this.ScaleTransform(sx, sy, MatrixOrder.Prepend);
		}

		/// <summary>Scales the local geometric transform by the specified amounts in the specified order.</summary>
		/// <param name="sx">The transform scale factor in the x-axis direction.</param>
		/// <param name="sy">The transform scale factor in the y-axis direction.</param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the scaling matrix.</param>
		// Token: 0x06000F21 RID: 3873 RVA: 0x000229CE File Offset: 0x00020BCE
		public void ScaleTransform(float sx, float sy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipScalePathGradientTransform(base.NativeBrush, sx, sy, order));
		}

		/// <summary>Creates a gradient with a center color and a linear falloff to one surrounding color.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies where, along any radial from the center of the path to the path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest intensity at the center of the path.</param>
		// Token: 0x06000F22 RID: 3874 RVA: 0x000229E3 File Offset: 0x00020BE3
		public void SetBlendTriangularShape(float focus)
		{
			this.SetBlendTriangularShape(focus, 1f);
		}

		/// <summary>Creates a gradient with a center color and a linear falloff to each surrounding color.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies where, along any radial from the center of the path to the path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest intensity at the center of the path.</param>
		/// <param name="scale">A value from 0 through 1 that specifies the maximum intensity of the center color that gets blended with the boundary color. A value of 1 causes the highest possible intensity of the center color, and it is the default value.</param>
		// Token: 0x06000F23 RID: 3875 RVA: 0x000229F1 File Offset: 0x00020BF1
		public void SetBlendTriangularShape(float focus, float scale)
		{
			if (focus < 0f || focus > 1f || scale < 0f || scale > 1f)
			{
				throw new ArgumentException("Invalid parameter passed.");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientLinearBlend(base.NativeBrush, focus, scale));
		}

		/// <summary>Creates a gradient brush that changes color starting from the center of the path outward to the path's boundary. The transition from one color to another is based on a bell-shaped curve.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies where, along any radial from the center of the path to the path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest intensity at the center of the path.</param>
		// Token: 0x06000F24 RID: 3876 RVA: 0x00022A30 File Offset: 0x00020C30
		public void SetSigmaBellShape(float focus)
		{
			this.SetSigmaBellShape(focus, 1f);
		}

		/// <summary>Creates a gradient brush that changes color starting from the center of the path outward to the path's boundary. The transition from one color to another is based on a bell-shaped curve.</summary>
		/// <param name="focus">A value from 0 through 1 that specifies where, along any radial from the center of the path to the path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest intensity at the center of the path.</param>
		/// <param name="scale">A value from 0 through 1 that specifies the maximum intensity of the center color that gets blended with the boundary color. A value of 1 causes the highest possible intensity of the center color, and it is the default value.</param>
		// Token: 0x06000F25 RID: 3877 RVA: 0x00022A3E File Offset: 0x00020C3E
		public void SetSigmaBellShape(float focus, float scale)
		{
			if (focus < 0f || focus > 1f || scale < 0f || scale > 1f)
			{
				throw new ArgumentException("Invalid parameter passed.");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientSigmaBlend(base.NativeBrush, focus, scale));
		}

		/// <summary>Applies the specified translation to the local geometric transform. This method prepends the translation to the transform.</summary>
		/// <param name="dx">The value of the translation in x.</param>
		/// <param name="dy">The value of the translation in y.</param>
		// Token: 0x06000F26 RID: 3878 RVA: 0x00022A7D File Offset: 0x00020C7D
		public void TranslateTransform(float dx, float dy)
		{
			this.TranslateTransform(dx, dy, MatrixOrder.Prepend);
		}

		/// <summary>Applies the specified translation to the local geometric transform in the specified order.</summary>
		/// <param name="dx">The value of the translation in x.</param>
		/// <param name="dy">The value of the translation in y.</param>
		/// <param name="order">The order (prepend or append) in which to apply the translation.</param>
		// Token: 0x06000F27 RID: 3879 RVA: 0x00022A88 File Offset: 0x00020C88
		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslatePathGradientTransform(base.NativeBrush, dx, dy, order));
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> this method creates, cast as an object.</returns>
		// Token: 0x06000F28 RID: 3880 RVA: 0x00022AA0 File Offset: 0x00020CA0
		public override object Clone()
		{
			IntPtr native;
			GDIPlus.CheckStatus((Status)GDIPlus.GdipCloneBrush(new HandleRef(this, base.NativeBrush), out native));
			return new PathGradientBrush(native);
		}
	}
}
