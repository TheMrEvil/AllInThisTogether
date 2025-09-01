using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Encapsulates a 3-by-3 affine matrix that represents a geometric transform. This class cannot be inherited.</summary>
	// Token: 0x02000157 RID: 343
	public sealed class Matrix : MarshalByRefObject, IDisposable
	{
		// Token: 0x06000EE0 RID: 3808 RVA: 0x00021F0B File Offset: 0x0002010B
		internal Matrix(IntPtr ptr)
		{
			this.nativeMatrix = ptr;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> class as the identity matrix.</summary>
		// Token: 0x06000EE1 RID: 3809 RVA: 0x00021F1A File Offset: 0x0002011A
		public Matrix()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateMatrix(out this.nativeMatrix));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> class to the geometric transform defined by the specified rectangle and array of points.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle to be transformed.</param>
		/// <param name="plgpts">An array of three <see cref="T:System.Drawing.Point" /> structures that represents the points of a parallelogram to which the upper-left, upper-right, and lower-left corners of the rectangle is to be transformed. The lower-right corner of the parallelogram is implied by the first three corners.</param>
		// Token: 0x06000EE2 RID: 3810 RVA: 0x00021F32 File Offset: 0x00020132
		public Matrix(Rectangle rect, Point[] plgpts)
		{
			if (plgpts == null)
			{
				throw new ArgumentNullException("plgpts");
			}
			if (plgpts.Length != 3)
			{
				throw new ArgumentException("plgpts");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreateMatrix3I(ref rect, plgpts, out this.nativeMatrix));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> class to the geometric transform defined by the specified rectangle and array of points.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle to be transformed.</param>
		/// <param name="plgpts">An array of three <see cref="T:System.Drawing.PointF" /> structures that represents the points of a parallelogram to which the upper-left, upper-right, and lower-left corners of the rectangle is to be transformed. The lower-right corner of the parallelogram is implied by the first three corners.</param>
		// Token: 0x06000EE3 RID: 3811 RVA: 0x00021F6C File Offset: 0x0002016C
		public Matrix(RectangleF rect, PointF[] plgpts)
		{
			if (plgpts == null)
			{
				throw new ArgumentNullException("plgpts");
			}
			if (plgpts.Length != 3)
			{
				throw new ArgumentException("plgpts");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreateMatrix3(ref rect, plgpts, out this.nativeMatrix));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> class with the specified elements.</summary>
		/// <param name="m11">The value in the first row and first column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
		/// <param name="m12">The value in the first row and second column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
		/// <param name="m21">The value in the second row and first column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
		/// <param name="m22">The value in the second row and second column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
		/// <param name="dx">The value in the third row and first column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
		/// <param name="dy">The value in the third row and second column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
		// Token: 0x06000EE4 RID: 3812 RVA: 0x00021FA6 File Offset: 0x000201A6
		public Matrix(float m11, float m12, float m21, float m22, float dx, float dy)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateMatrix2(m11, m12, m21, m22, dx, dy, out this.nativeMatrix));
		}

		/// <summary>Gets an array of floating-point values that represents the elements of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <returns>An array of floating-point values that represents the elements of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</returns>
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x00021FC8 File Offset: 0x000201C8
		public float[] Elements
		{
			get
			{
				float[] array = new float[6];
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(float)) * 6);
				try
				{
					GDIPlus.CheckStatus(GDIPlus.GdipGetMatrixElements(this.nativeMatrix, intPtr));
					Marshal.Copy(intPtr, array, 0, 6);
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				return array;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is the identity matrix.</summary>
		/// <returns>This property is <see langword="true" /> if this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is identity; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00022028 File Offset: 0x00020228
		public bool IsIdentity
		{
			get
			{
				bool result;
				GDIPlus.CheckStatus(GDIPlus.GdipIsMatrixIdentity(this.nativeMatrix, out result));
				return result;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is invertible.</summary>
		/// <returns>This property is <see langword="true" /> if this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is invertible; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x00022048 File Offset: 0x00020248
		public bool IsInvertible
		{
			get
			{
				bool result;
				GDIPlus.CheckStatus(GDIPlus.GdipIsMatrixInvertible(this.nativeMatrix, out result));
				return result;
			}
		}

		/// <summary>Gets the x translation value (the dx value, or the element in the third row and first column) of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <returns>The x translation value of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</returns>
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x00022068 File Offset: 0x00020268
		public float OffsetX
		{
			get
			{
				return this.Elements[4];
			}
		}

		/// <summary>Gets the y translation value (the dy value, or the element in the third row and second column) of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <returns>The y translation value of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</returns>
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00022072 File Offset: 0x00020272
		public float OffsetY
		{
			get
			{
				return this.Elements[5];
			}
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that this method creates.</returns>
		// Token: 0x06000EEA RID: 3818 RVA: 0x0002207C File Offset: 0x0002027C
		public Matrix Clone()
		{
			IntPtr ptr;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneMatrix(this.nativeMatrix, out ptr));
			return new Matrix(ptr);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		// Token: 0x06000EEB RID: 3819 RVA: 0x000220A1 File Offset: 0x000202A1
		public void Dispose()
		{
			if (this.nativeMatrix != IntPtr.Zero)
			{
				GDIPlus.CheckStatus(GDIPlus.GdipDeleteMatrix(this.nativeMatrix));
				this.nativeMatrix = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		/// <summary>Tests whether the specified object is a <see cref="T:System.Drawing.Drawing2D.Matrix" /> and is identical to this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <param name="obj">The object to test.</param>
		/// <returns>This method returns <see langword="true" /> if <paramref name="obj" /> is the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> identical to this <see cref="T:System.Drawing.Drawing2D.Matrix" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EEC RID: 3820 RVA: 0x000220D8 File Offset: 0x000202D8
		public override bool Equals(object obj)
		{
			Matrix matrix = obj as Matrix;
			if (matrix != null)
			{
				bool result;
				GDIPlus.CheckStatus(GDIPlus.GdipIsMatrixEqual(this.nativeMatrix, matrix.nativeMatrix, out result));
				return result;
			}
			return false;
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x06000EED RID: 3821 RVA: 0x0002210C File Offset: 0x0002030C
		~Matrix()
		{
			this.Dispose();
		}

		/// <summary>Returns a hash code.</summary>
		/// <returns>The hash code for this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</returns>
		// Token: 0x06000EEE RID: 3822 RVA: 0x00003BAB File Offset: 0x00001DAB
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Inverts this <see cref="T:System.Drawing.Drawing2D.Matrix" />, if it is invertible.</summary>
		// Token: 0x06000EEF RID: 3823 RVA: 0x00022138 File Offset: 0x00020338
		public void Invert()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipInvertMatrix(this.nativeMatrix));
		}

		/// <summary>Multiplies this <see cref="T:System.Drawing.Drawing2D.Matrix" /> by the matrix specified in the <paramref name="matrix" /> parameter, by prepending the specified <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is to be multiplied.</param>
		// Token: 0x06000EF0 RID: 3824 RVA: 0x0002214A File Offset: 0x0002034A
		public void Multiply(Matrix matrix)
		{
			this.Multiply(matrix, MatrixOrder.Prepend);
		}

		/// <summary>Multiplies this <see cref="T:System.Drawing.Drawing2D.Matrix" /> by the matrix specified in the <paramref name="matrix" /> parameter, and in the order specified in the <paramref name="order" /> parameter.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is to be multiplied.</param>
		/// <param name="order">The <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that represents the order of the multiplication.</param>
		// Token: 0x06000EF1 RID: 3825 RVA: 0x00022154 File Offset: 0x00020354
		public void Multiply(Matrix matrix, MatrixOrder order)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipMultiplyMatrix(this.nativeMatrix, matrix.nativeMatrix, order));
		}

		/// <summary>Resets this <see cref="T:System.Drawing.Drawing2D.Matrix" /> to have the elements of the identity matrix.</summary>
		// Token: 0x06000EF2 RID: 3826 RVA: 0x0002217B File Offset: 0x0002037B
		public void Reset()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetMatrixElements(this.nativeMatrix, 1f, 0f, 0f, 1f, 0f, 0f));
		}

		/// <summary>Prepend to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> a clockwise rotation, around the origin and by the specified angle.</summary>
		/// <param name="angle">The angle of the rotation, in degrees.</param>
		// Token: 0x06000EF3 RID: 3827 RVA: 0x000221AB File Offset: 0x000203AB
		public void Rotate(float angle)
		{
			this.Rotate(angle, MatrixOrder.Prepend);
		}

		/// <summary>Applies a clockwise rotation of an amount specified in the <paramref name="angle" /> parameter, around the origin (zero x and y coordinates) for this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <param name="angle">The angle (extent) of the rotation, in degrees.</param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or prepend) in which the rotation is applied to this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
		// Token: 0x06000EF4 RID: 3828 RVA: 0x000221B5 File Offset: 0x000203B5
		public void Rotate(float angle, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRotateMatrix(this.nativeMatrix, angle, order));
		}

		/// <summary>Applies a clockwise rotation to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> around the point specified in the <paramref name="point" /> parameter, and by prepending the rotation.</summary>
		/// <param name="angle">The angle (extent) of the rotation, in degrees.</param>
		/// <param name="point">A <see cref="T:System.Drawing.PointF" /> that represents the center of the rotation.</param>
		// Token: 0x06000EF5 RID: 3829 RVA: 0x000221C9 File Offset: 0x000203C9
		public void RotateAt(float angle, PointF point)
		{
			this.RotateAt(angle, point, MatrixOrder.Prepend);
		}

		/// <summary>Applies a clockwise rotation about the specified point to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
		/// <param name="angle">The angle of the rotation, in degrees.</param>
		/// <param name="point">A <see cref="T:System.Drawing.PointF" /> that represents the center of the rotation.</param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or prepend) in which the rotation is applied.</param>
		// Token: 0x06000EF6 RID: 3830 RVA: 0x000221D4 File Offset: 0x000203D4
		public void RotateAt(float angle, PointF point, MatrixOrder order)
		{
			if (order < MatrixOrder.Prepend || order > MatrixOrder.Append)
			{
				throw new ArgumentException("order");
			}
			angle *= 0.017453292f;
			float num = (float)Math.Cos((double)angle);
			float num2 = (float)Math.Sin((double)angle);
			float num3 = -point.X * num + point.Y * num2 + point.X;
			float num4 = -point.X * num2 - point.Y * num + point.Y;
			float[] elements = this.Elements;
			Status status;
			if (order == MatrixOrder.Prepend)
			{
				status = GDIPlus.GdipSetMatrixElements(this.nativeMatrix, num * elements[0] + num2 * elements[2], num * elements[1] + num2 * elements[3], -num2 * elements[0] + num * elements[2], -num2 * elements[1] + num * elements[3], num3 * elements[0] + num4 * elements[2] + elements[4], num3 * elements[1] + num4 * elements[3] + elements[5]);
			}
			else
			{
				status = GDIPlus.GdipSetMatrixElements(this.nativeMatrix, elements[0] * num + elements[1] * -num2, elements[0] * num2 + elements[1] * num, elements[2] * num + elements[3] * -num2, elements[2] * num2 + elements[3] * num, elements[4] * num + elements[5] * -num2 + num3, elements[4] * num2 + elements[5] * num + num4);
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Applies the specified scale vector to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> by prepending the scale vector.</summary>
		/// <param name="scaleX">The value by which to scale this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the x-axis direction.</param>
		/// <param name="scaleY">The value by which to scale this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the y-axis direction.</param>
		// Token: 0x06000EF7 RID: 3831 RVA: 0x00022326 File Offset: 0x00020526
		public void Scale(float scaleX, float scaleY)
		{
			this.Scale(scaleX, scaleY, MatrixOrder.Prepend);
		}

		/// <summary>Applies the specified scale vector (<paramref name="scaleX" /> and <paramref name="scaleY" />) to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> using the specified order.</summary>
		/// <param name="scaleX">The value by which to scale this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the x-axis direction.</param>
		/// <param name="scaleY">The value by which to scale this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the y-axis direction.</param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or prepend) in which the scale vector is applied to this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
		// Token: 0x06000EF8 RID: 3832 RVA: 0x00022331 File Offset: 0x00020531
		public void Scale(float scaleX, float scaleY, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipScaleMatrix(this.nativeMatrix, scaleX, scaleY, order));
		}

		/// <summary>Applies the specified shear vector to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> by prepending the shear transformation.</summary>
		/// <param name="shearX">The horizontal shear factor.</param>
		/// <param name="shearY">The vertical shear factor.</param>
		// Token: 0x06000EF9 RID: 3833 RVA: 0x00022346 File Offset: 0x00020546
		public void Shear(float shearX, float shearY)
		{
			this.Shear(shearX, shearY, MatrixOrder.Prepend);
		}

		/// <summary>Applies the specified shear vector to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
		/// <param name="shearX">The horizontal shear factor.</param>
		/// <param name="shearY">The vertical shear factor.</param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or prepend) in which the shear is applied.</param>
		// Token: 0x06000EFA RID: 3834 RVA: 0x00022351 File Offset: 0x00020551
		public void Shear(float shearX, float shearY, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipShearMatrix(this.nativeMatrix, shearX, shearY, order));
		}

		/// <summary>Applies the geometric transform represented by this <see cref="T:System.Drawing.Drawing2D.Matrix" /> to a specified array of points.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points to transform.</param>
		// Token: 0x06000EFB RID: 3835 RVA: 0x00022366 File Offset: 0x00020566
		public void TransformPoints(Point[] pts)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipTransformMatrixPointsI(this.nativeMatrix, pts, pts.Length));
		}

		/// <summary>Applies the geometric transform represented by this <see cref="T:System.Drawing.Drawing2D.Matrix" /> to a specified array of points.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points to transform.</param>
		// Token: 0x06000EFC RID: 3836 RVA: 0x0002238A File Offset: 0x0002058A
		public void TransformPoints(PointF[] pts)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipTransformMatrixPoints(this.nativeMatrix, pts, pts.Length));
		}

		/// <summary>Applies only the scale and rotate components of this <see cref="T:System.Drawing.Drawing2D.Matrix" /> to the specified array of points.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points to transform.</param>
		// Token: 0x06000EFD RID: 3837 RVA: 0x000223AE File Offset: 0x000205AE
		public void TransformVectors(Point[] pts)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipVectorTransformMatrixPointsI(this.nativeMatrix, pts, pts.Length));
		}

		/// <summary>Multiplies each vector in an array by the matrix. The translation elements of this matrix (third row) are ignored.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points to transform.</param>
		// Token: 0x06000EFE RID: 3838 RVA: 0x000223D2 File Offset: 0x000205D2
		public void TransformVectors(PointF[] pts)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipVectorTransformMatrixPoints(this.nativeMatrix, pts, pts.Length));
		}

		/// <summary>Applies the specified translation vector (<paramref name="offsetX" /> and <paramref name="offsetY" />) to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> by prepending the translation vector.</summary>
		/// <param name="offsetX">The x value by which to translate this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
		/// <param name="offsetY">The y value by which to translate this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
		// Token: 0x06000EFF RID: 3839 RVA: 0x000223F6 File Offset: 0x000205F6
		public void Translate(float offsetX, float offsetY)
		{
			this.Translate(offsetX, offsetY, MatrixOrder.Prepend);
		}

		/// <summary>Applies the specified translation vector to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
		/// <param name="offsetX">The x value by which to translate this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
		/// <param name="offsetY">The y value by which to translate this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or prepend) in which the translation is applied to this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
		// Token: 0x06000F00 RID: 3840 RVA: 0x00022401 File Offset: 0x00020601
		public void Translate(float offsetX, float offsetY, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslateMatrix(this.nativeMatrix, offsetX, offsetY, order));
		}

		/// <summary>Multiplies each vector in an array by the matrix. The translation elements of this matrix (third row) are ignored.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points to transform.</param>
		// Token: 0x06000F01 RID: 3841 RVA: 0x00022416 File Offset: 0x00020616
		public void VectorTransformPoints(Point[] pts)
		{
			this.TransformVectors(pts);
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x0002241F File Offset: 0x0002061F
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x00022427 File Offset: 0x00020627
		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeMatrix;
			}
			set
			{
				this.nativeMatrix = value;
			}
		}

		// Token: 0x04000B78 RID: 2936
		internal IntPtr nativeMatrix;
	}
}
