using System;
using System.ComponentModel;

namespace System.Drawing.Drawing2D
{
	/// <summary>Represents a series of connected lines and curves. This class cannot be inherited.</summary>
	// Token: 0x02000155 RID: 341
	public sealed class GraphicsPath : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x06000E57 RID: 3671 RVA: 0x000207CE File Offset: 0x0001E9CE
		private GraphicsPath(IntPtr ptr)
		{
			this.nativePath = ptr;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> class with a <see cref="P:System.Drawing.Drawing2D.GraphicsPath.FillMode" /> value of <see cref="F:System.Drawing.Drawing2D.FillMode.Alternate" />.</summary>
		// Token: 0x06000E58 RID: 3672 RVA: 0x000207E8 File Offset: 0x0001E9E8
		public GraphicsPath()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePath(FillMode.Alternate, out this.nativePath));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> class with the specified <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration.</summary>
		/// <param name="fillMode">The <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the interior of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> is filled.</param>
		// Token: 0x06000E59 RID: 3673 RVA: 0x0002080C File Offset: 0x0001EA0C
		public GraphicsPath(FillMode fillMode)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePath(fillMode, out this.nativePath));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> class with the specified <see cref="T:System.Drawing.Drawing2D.PathPointType" /> and <see cref="T:System.Drawing.Point" /> arrays.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that defines the coordinates of the points that make up this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</param>
		/// <param name="types">An array of <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration elements that specifies the type of each corresponding point in the <paramref name="pts" /> array.</param>
		// Token: 0x06000E5A RID: 3674 RVA: 0x00020830 File Offset: 0x0001EA30
		public GraphicsPath(Point[] pts, byte[] types) : this(pts, types, FillMode.Alternate)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> array with the specified <see cref="T:System.Drawing.Drawing2D.PathPointType" /> and <see cref="T:System.Drawing.PointF" /> arrays.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.PointF" /> structures that defines the coordinates of the points that make up this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</param>
		/// <param name="types">An array of <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration elements that specifies the type of each corresponding point in the <paramref name="pts" /> array.</param>
		// Token: 0x06000E5B RID: 3675 RVA: 0x0002083B File Offset: 0x0001EA3B
		public GraphicsPath(PointF[] pts, byte[] types) : this(pts, types, FillMode.Alternate)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> class with the specified <see cref="T:System.Drawing.Drawing2D.PathPointType" /> and <see cref="T:System.Drawing.Point" /> arrays and with the specified <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration element.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that defines the coordinates of the points that make up this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</param>
		/// <param name="types">An array of <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration elements that specifies the type of each corresponding point in the <paramref name="pts" /> array.</param>
		/// <param name="fillMode">A <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that specifies how the interiors of shapes in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> are filled.</param>
		// Token: 0x06000E5C RID: 3676 RVA: 0x00020848 File Offset: 0x0001EA48
		public GraphicsPath(Point[] pts, byte[] types, FillMode fillMode)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			if (pts.Length != types.Length)
			{
				throw new ArgumentException("Invalid parameter passed. Number of points and types must be same.");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePath2I(pts, types, pts.Length, fillMode, out this.nativePath));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> array with the specified <see cref="T:System.Drawing.Drawing2D.PathPointType" /> and <see cref="T:System.Drawing.PointF" /> arrays and with the specified <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration element.</summary>
		/// <param name="pts">An array of <see cref="T:System.Drawing.PointF" /> structures that defines the coordinates of the points that make up this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</param>
		/// <param name="types">An array of <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration elements that specifies the type of each corresponding point in the <paramref name="pts" /> array.</param>
		/// <param name="fillMode">A <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that specifies how the interiors of shapes in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> are filled.</param>
		// Token: 0x06000E5D RID: 3677 RVA: 0x000208A0 File Offset: 0x0001EAA0
		public GraphicsPath(PointF[] pts, byte[] types, FillMode fillMode)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			if (pts.Length != types.Length)
			{
				throw new ArgumentException("Invalid parameter passed. Number of points and types must be same.");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePath2(pts, types, pts.Length, fillMode, out this.nativePath));
		}

		/// <summary>Creates an exact copy of this path.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> this method creates, cast as an object.</returns>
		// Token: 0x06000E5E RID: 3678 RVA: 0x000208F8 File Offset: 0x0001EAF8
		public object Clone()
		{
			IntPtr ptr;
			GDIPlus.CheckStatus(GDIPlus.GdipClonePath(this.nativePath, out ptr));
			return new GraphicsPath(ptr);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		// Token: 0x06000E5F RID: 3679 RVA: 0x0002091D File Offset: 0x0001EB1D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x06000E60 RID: 3680 RVA: 0x0002092C File Offset: 0x0001EB2C
		~GraphicsPath()
		{
			this.Dispose(false);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0002095C File Offset: 0x0001EB5C
		private void Dispose(bool disposing)
		{
			if (this.nativePath != IntPtr.Zero)
			{
				GDIPlus.CheckStatus(GDIPlus.GdipDeletePath(this.nativePath));
				this.nativePath = IntPtr.Zero;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the interiors of shapes in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> are filled.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that specifies how the interiors of shapes in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> are filled.</returns>
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x0002098C File Offset: 0x0001EB8C
		// (set) Token: 0x06000E63 RID: 3683 RVA: 0x000209AC File Offset: 0x0001EBAC
		public FillMode FillMode
		{
			get
			{
				FillMode result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathFillMode(this.nativePath, out result));
				return result;
			}
			set
			{
				if (value < FillMode.Alternate || value > FillMode.Winding)
				{
					throw new InvalidEnumArgumentException("FillMode", (int)value, typeof(FillMode));
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathFillMode(this.nativePath, value));
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Drawing2D.PathData" /> that encapsulates arrays of points (<paramref name="points" />) and types (<paramref name="types" />) for this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.PathData" /> that encapsulates arrays for both the points and types for this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x000209E0 File Offset: 0x0001EBE0
		public PathData PathData
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPointCount(this.nativePath, out num));
				PointF[] points = new PointF[num];
				byte[] types = new byte[num];
				if (num > 0)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipGetPathPoints(this.nativePath, points, num));
					GDIPlus.CheckStatus(GDIPlus.GdipGetPathTypes(this.nativePath, types, num));
				}
				return new PathData
				{
					Points = points,
					Types = types
				};
			}
		}

		/// <summary>Gets the points in the path.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.PointF" /> objects that represent the path.</returns>
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x00020A48 File Offset: 0x0001EC48
		public PointF[] PathPoints
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPointCount(this.nativePath, out num));
				if (num == 0)
				{
					throw new ArgumentException("PathPoints");
				}
				PointF[] array = new PointF[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathPoints(this.nativePath, array, num));
				return array;
			}
		}

		/// <summary>Gets the types of the corresponding points in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> array.</summary>
		/// <returns>An array of bytes that specifies the types of the corresponding points in the path.</returns>
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x00020A90 File Offset: 0x0001EC90
		public byte[] PathTypes
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPointCount(this.nativePath, out num));
				if (num == 0)
				{
					throw new ArgumentException("PathTypes");
				}
				byte[] array = new byte[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathTypes(this.nativePath, array, num));
				return array;
			}
		}

		/// <summary>Gets the number of elements in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> or the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathTypes" /> array.</summary>
		/// <returns>An integer that specifies the number of elements in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> or the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathTypes" /> array.</returns>
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x00020AD8 File Offset: 0x0001ECD8
		public int PointCount
		{
			get
			{
				int result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPointCount(this.nativePath, out result));
				return result;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00020AF8 File Offset: 0x0001ECF8
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x00020B00 File Offset: 0x0001ED00
		internal IntPtr NativeObject
		{
			get
			{
				return this.nativePath;
			}
			set
			{
				this.nativePath = value;
			}
		}

		/// <summary>Appends an elliptical arc to the current figure.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangular bounds of the ellipse from which the arc is taken.</param>
		/// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis.</param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc.</param>
		// Token: 0x06000E6A RID: 3690 RVA: 0x00020B09 File Offset: 0x0001ED09
		public void AddArc(Rectangle rect, float startAngle, float sweepAngle)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathArcI(this.nativePath, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle));
		}

		/// <summary>Appends an elliptical arc to the current figure.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangular bounds of the ellipse from which the arc is taken.</param>
		/// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis.</param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc.</param>
		// Token: 0x06000E6B RID: 3691 RVA: 0x00020B39 File Offset: 0x0001ED39
		public void AddArc(RectangleF rect, float startAngle, float sweepAngle)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathArc(this.nativePath, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle));
		}

		/// <summary>Appends an elliptical arc to the current figure.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="width">The width of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="height">The height of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis.</param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc.</param>
		// Token: 0x06000E6C RID: 3692 RVA: 0x00020B69 File Offset: 0x0001ED69
		public void AddArc(int x, int y, int width, int height, float startAngle, float sweepAngle)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathArcI(this.nativePath, x, y, width, height, startAngle, sweepAngle));
		}

		/// <summary>Appends an elliptical arc to the current figure.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="width">The width of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="height">The height of the rectangular region that defines the ellipse from which the arc is drawn.</param>
		/// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis.</param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc.</param>
		// Token: 0x06000E6D RID: 3693 RVA: 0x00020B84 File Offset: 0x0001ED84
		public void AddArc(float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathArc(this.nativePath, x, y, width, height, startAngle, sweepAngle));
		}

		/// <summary>Adds a cubic Bézier curve to the current figure.</summary>
		/// <param name="pt1">A <see cref="T:System.Drawing.Point" /> that represents the starting point of the curve.</param>
		/// <param name="pt2">A <see cref="T:System.Drawing.Point" /> that represents the first control point for the curve.</param>
		/// <param name="pt3">A <see cref="T:System.Drawing.Point" /> that represents the second control point for the curve.</param>
		/// <param name="pt4">A <see cref="T:System.Drawing.Point" /> that represents the endpoint of the curve.</param>
		// Token: 0x06000E6E RID: 3694 RVA: 0x00020BA0 File Offset: 0x0001EDA0
		public void AddBezier(Point pt1, Point pt2, Point pt3, Point pt4)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathBezierI(this.nativePath, pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y));
		}

		/// <summary>Adds a cubic Bézier curve to the current figure.</summary>
		/// <param name="pt1">A <see cref="T:System.Drawing.PointF" /> that represents the starting point of the curve.</param>
		/// <param name="pt2">A <see cref="T:System.Drawing.PointF" /> that represents the first control point for the curve.</param>
		/// <param name="pt3">A <see cref="T:System.Drawing.PointF" /> that represents the second control point for the curve.</param>
		/// <param name="pt4">A <see cref="T:System.Drawing.PointF" /> that represents the endpoint of the curve.</param>
		// Token: 0x06000E6F RID: 3695 RVA: 0x00020BF8 File Offset: 0x0001EDF8
		public void AddBezier(PointF pt1, PointF pt2, PointF pt3, PointF pt4)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathBezier(this.nativePath, pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y));
		}

		/// <summary>Adds a cubic Bézier curve to the current figure.</summary>
		/// <param name="x1">The x-coordinate of the starting point of the curve.</param>
		/// <param name="y1">The y-coordinate of the starting point of the curve.</param>
		/// <param name="x2">The x-coordinate of the first control point for the curve.</param>
		/// <param name="y2">The y-coordinate of the first control point for the curve.</param>
		/// <param name="x3">The x-coordinate of the second control point for the curve.</param>
		/// <param name="y3">The y-coordinate of the second control point for the curve.</param>
		/// <param name="x4">The x-coordinate of the endpoint of the curve.</param>
		/// <param name="y4">The y-coordinate of the endpoint of the curve.</param>
		// Token: 0x06000E70 RID: 3696 RVA: 0x00020C50 File Offset: 0x0001EE50
		public void AddBezier(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathBezierI(this.nativePath, x1, y1, x2, y2, x3, y3, x4, y4));
		}

		/// <summary>Adds a cubic Bézier curve to the current figure.</summary>
		/// <param name="x1">The x-coordinate of the starting point of the curve.</param>
		/// <param name="y1">The y-coordinate of the starting point of the curve.</param>
		/// <param name="x2">The x-coordinate of the first control point for the curve.</param>
		/// <param name="y2">The y-coordinate of the first control point for the curve.</param>
		/// <param name="x3">The x-coordinate of the second control point for the curve.</param>
		/// <param name="y3">The y-coordinate of the second control point for the curve.</param>
		/// <param name="x4">The x-coordinate of the endpoint of the curve.</param>
		/// <param name="y4">The y-coordinate of the endpoint of the curve.</param>
		// Token: 0x06000E71 RID: 3697 RVA: 0x00020C7C File Offset: 0x0001EE7C
		public void AddBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathBezier(this.nativePath, x1, y1, x2, y2, x3, y3, x4, y4));
		}

		/// <summary>Adds a sequence of connected cubic Bézier curves to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curves.</param>
		// Token: 0x06000E72 RID: 3698 RVA: 0x00020CA6 File Offset: 0x0001EEA6
		public void AddBeziers(params Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathBeziersI(this.nativePath, points, points.Length));
		}

		/// <summary>Adds a sequence of connected cubic Bézier curves to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curves.</param>
		// Token: 0x06000E73 RID: 3699 RVA: 0x00020CCA File Offset: 0x0001EECA
		public void AddBeziers(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathBeziers(this.nativePath, points, points.Length));
		}

		/// <summary>Adds an ellipse to the current path.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> that represents the bounding rectangle that defines the ellipse.</param>
		// Token: 0x06000E74 RID: 3700 RVA: 0x00020CEE File Offset: 0x0001EEEE
		public void AddEllipse(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathEllipse(this.nativePath, rect.X, rect.Y, rect.Width, rect.Height));
		}

		/// <summary>Adds an ellipse to the current path.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="y">The y-coordinate of the upper left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="width">The width of the bounding rectangle that defines the ellipse.</param>
		/// <param name="height">The height of the bounding rectangle that defines the ellipse.</param>
		// Token: 0x06000E75 RID: 3701 RVA: 0x00020D1C File Offset: 0x0001EF1C
		public void AddEllipse(float x, float y, float width, float height)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathEllipse(this.nativePath, x, y, width, height));
		}

		/// <summary>Adds an ellipse to the current path.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle that defines the ellipse.</param>
		// Token: 0x06000E76 RID: 3702 RVA: 0x00020D33 File Offset: 0x0001EF33
		public void AddEllipse(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathEllipseI(this.nativePath, rect.X, rect.Y, rect.Width, rect.Height));
		}

		/// <summary>Adds an ellipse to the current path.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="width">The width of the bounding rectangle that defines the ellipse.</param>
		/// <param name="height">The height of the bounding rectangle that defines the ellipse.</param>
		// Token: 0x06000E77 RID: 3703 RVA: 0x00020D61 File Offset: 0x0001EF61
		public void AddEllipse(int x, int y, int width, int height)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathEllipseI(this.nativePath, x, y, width, height));
		}

		/// <summary>Appends a line segment to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="pt1">A <see cref="T:System.Drawing.Point" /> that represents the starting point of the line.</param>
		/// <param name="pt2">A <see cref="T:System.Drawing.Point" /> that represents the endpoint of the line.</param>
		// Token: 0x06000E78 RID: 3704 RVA: 0x00020D78 File Offset: 0x0001EF78
		public void AddLine(Point pt1, Point pt2)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathLineI(this.nativePath, pt1.X, pt1.Y, pt2.X, pt2.Y));
		}

		/// <summary>Appends a line segment to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="pt1">A <see cref="T:System.Drawing.PointF" /> that represents the starting point of the line.</param>
		/// <param name="pt2">A <see cref="T:System.Drawing.PointF" /> that represents the endpoint of the line.</param>
		// Token: 0x06000E79 RID: 3705 RVA: 0x00020DA6 File Offset: 0x0001EFA6
		public void AddLine(PointF pt1, PointF pt2)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathLine(this.nativePath, pt1.X, pt1.Y, pt2.X, pt2.Y));
		}

		/// <summary>Appends a line segment to the current figure.</summary>
		/// <param name="x1">The x-coordinate of the starting point of the line.</param>
		/// <param name="y1">The y-coordinate of the starting point of the line.</param>
		/// <param name="x2">The x-coordinate of the endpoint of the line.</param>
		/// <param name="y2">The y-coordinate of the endpoint of the line.</param>
		// Token: 0x06000E7A RID: 3706 RVA: 0x00020DD4 File Offset: 0x0001EFD4
		public void AddLine(int x1, int y1, int x2, int y2)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathLineI(this.nativePath, x1, y1, x2, y2));
		}

		/// <summary>Appends a line segment to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="x1">The x-coordinate of the starting point of the line.</param>
		/// <param name="y1">The y-coordinate of the starting point of the line.</param>
		/// <param name="x2">The x-coordinate of the endpoint of the line.</param>
		/// <param name="y2">The y-coordinate of the endpoint of the line.</param>
		// Token: 0x06000E7B RID: 3707 RVA: 0x00020DEB File Offset: 0x0001EFEB
		public void AddLine(float x1, float y1, float x2, float y2)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathLine(this.nativePath, x1, y1, x2, y2));
		}

		/// <summary>Appends a series of connected line segments to the end of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the line segments to add.</param>
		// Token: 0x06000E7C RID: 3708 RVA: 0x00020E02 File Offset: 0x0001F002
		public void AddLines(Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (points.Length == 0)
			{
				throw new ArgumentException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathLine2I(this.nativePath, points, points.Length));
		}

		/// <summary>Appends a series of connected line segments to the end of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the line segments to add.</param>
		// Token: 0x06000E7D RID: 3709 RVA: 0x00020E35 File Offset: 0x0001F035
		public void AddLines(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (points.Length == 0)
			{
				throw new ArgumentException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathLine2(this.nativePath, points, points.Length));
		}

		/// <summary>Adds the outline of a pie shape to this path.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="startAngle">The starting angle for the pie section, measured in degrees clockwise from the x-axis.</param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the pie section, measured in degrees clockwise from <paramref name="startAngle" />.</param>
		// Token: 0x06000E7E RID: 3710 RVA: 0x00020E68 File Offset: 0x0001F068
		public void AddPie(Rectangle rect, float startAngle, float sweepAngle)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathPie(this.nativePath, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height, startAngle, sweepAngle));
		}

		/// <summary>Adds the outline of a pie shape to this path.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="width">The width of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="height">The height of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="startAngle">The starting angle for the pie section, measured in degrees clockwise from the x-axis.</param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the pie section, measured in degrees clockwise from <paramref name="startAngle" />.</param>
		// Token: 0x06000E7F RID: 3711 RVA: 0x00020E9C File Offset: 0x0001F09C
		public void AddPie(int x, int y, int width, int height, float startAngle, float sweepAngle)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathPieI(this.nativePath, x, y, width, height, startAngle, sweepAngle));
		}

		/// <summary>Adds the outline of a pie shape to this path.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="width">The width of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="height">The height of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
		/// <param name="startAngle">The starting angle for the pie section, measured in degrees clockwise from the x-axis.</param>
		/// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the pie section, measured in degrees clockwise from <paramref name="startAngle" />.</param>
		// Token: 0x06000E80 RID: 3712 RVA: 0x00020EB7 File Offset: 0x0001F0B7
		public void AddPie(float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathPie(this.nativePath, x, y, width, height, startAngle, sweepAngle));
		}

		/// <summary>Adds a polygon to this path.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that defines the polygon to add.</param>
		// Token: 0x06000E81 RID: 3713 RVA: 0x00020ED2 File Offset: 0x0001F0D2
		public void AddPolygon(Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathPolygonI(this.nativePath, points, points.Length));
		}

		/// <summary>Adds a polygon to this path.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that defines the polygon to add.</param>
		// Token: 0x06000E82 RID: 3714 RVA: 0x00020EF6 File Offset: 0x0001F0F6
		public void AddPolygon(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathPolygon(this.nativePath, points, points.Length));
		}

		/// <summary>Adds a rectangle to this path.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle to add.</param>
		// Token: 0x06000E83 RID: 3715 RVA: 0x00020F1A File Offset: 0x0001F11A
		public void AddRectangle(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathRectangleI(this.nativePath, rect.X, rect.Y, rect.Width, rect.Height));
		}

		/// <summary>Adds a rectangle to this path.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle to add.</param>
		// Token: 0x06000E84 RID: 3716 RVA: 0x00020F48 File Offset: 0x0001F148
		public void AddRectangle(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathRectangle(this.nativePath, rect.X, rect.Y, rect.Width, rect.Height));
		}

		/// <summary>Adds a series of rectangles to this path.</summary>
		/// <param name="rects">An array of <see cref="T:System.Drawing.Rectangle" /> structures that represents the rectangles to add.</param>
		// Token: 0x06000E85 RID: 3717 RVA: 0x00020F76 File Offset: 0x0001F176
		public void AddRectangles(Rectangle[] rects)
		{
			if (rects == null)
			{
				throw new ArgumentNullException("rects");
			}
			if (rects.Length == 0)
			{
				throw new ArgumentException("rects");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathRectanglesI(this.nativePath, rects, rects.Length));
		}

		/// <summary>Adds a series of rectangles to this path.</summary>
		/// <param name="rects">An array of <see cref="T:System.Drawing.RectangleF" /> structures that represents the rectangles to add.</param>
		// Token: 0x06000E86 RID: 3718 RVA: 0x00020FA9 File Offset: 0x0001F1A9
		public void AddRectangles(RectangleF[] rects)
		{
			if (rects == null)
			{
				throw new ArgumentNullException("rects");
			}
			if (rects.Length == 0)
			{
				throw new ArgumentException("rects");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathRectangles(this.nativePath, rects, rects.Length));
		}

		/// <summary>Appends the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to this path.</summary>
		/// <param name="addingPath">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to add.</param>
		/// <param name="connect">A Boolean value that specifies whether the first figure in the added path is part of the last figure in this path. A value of <see langword="true" /> specifies that (if possible) the first figure in the added path is part of the last figure in this path. A value of <see langword="false" /> specifies that the first figure in the added path is separate from the last figure in this path.</param>
		// Token: 0x06000E87 RID: 3719 RVA: 0x00020FDC File Offset: 0x0001F1DC
		public void AddPath(GraphicsPath addingPath, bool connect)
		{
			if (addingPath == null)
			{
				throw new ArgumentNullException("addingPath");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathPath(this.nativePath, addingPath.nativePath, connect));
		}

		/// <summary>Gets the last point in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> array of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.PointF" /> that represents the last point in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
		// Token: 0x06000E88 RID: 3720 RVA: 0x00021004 File Offset: 0x0001F204
		public PointF GetLastPoint()
		{
			PointF result;
			GDIPlus.CheckStatus(GDIPlus.GdipGetPathLastPoint(this.nativePath, out result));
			return result;
		}

		/// <summary>Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve.</param>
		// Token: 0x06000E89 RID: 3721 RVA: 0x00021024 File Offset: 0x0001F224
		public void AddClosedCurve(Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathClosedCurveI(this.nativePath, points, points.Length));
		}

		/// <summary>Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve.</param>
		// Token: 0x06000E8A RID: 3722 RVA: 0x00021048 File Offset: 0x0001F248
		public void AddClosedCurve(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathClosedCurve(this.nativePath, points, points.Length));
		}

		/// <summary>Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve.</param>
		/// <param name="tension">A value between from 0 through 1 that specifies the amount that the curve bends between points, with 0 being the smallest curve (sharpest corner) and 1 being the smoothest curve.</param>
		// Token: 0x06000E8B RID: 3723 RVA: 0x0002106C File Offset: 0x0001F26C
		public void AddClosedCurve(Point[] points, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathClosedCurve2I(this.nativePath, points, points.Length, tension));
		}

		/// <summary>Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve.</param>
		/// <param name="tension">A value between from 0 through 1 that specifies the amount that the curve bends between points, with 0 being the smallest curve (sharpest corner) and 1 being the smoothest curve.</param>
		// Token: 0x06000E8C RID: 3724 RVA: 0x00021091 File Offset: 0x0001F291
		public void AddClosedCurve(PointF[] points, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathClosedCurve2(this.nativePath, points, points.Length, tension));
		}

		/// <summary>Adds a spline curve to the current figure. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve.</param>
		// Token: 0x06000E8D RID: 3725 RVA: 0x000210B6 File Offset: 0x0001F2B6
		public void AddCurve(Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathCurveI(this.nativePath, points, points.Length));
		}

		/// <summary>Adds a spline curve to the current figure. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve.</param>
		// Token: 0x06000E8E RID: 3726 RVA: 0x000210DA File Offset: 0x0001F2DA
		public void AddCurve(PointF[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathCurve(this.nativePath, points, points.Length));
		}

		/// <summary>Adds a spline curve to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve.</param>
		/// <param name="tension">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results.</param>
		// Token: 0x06000E8F RID: 3727 RVA: 0x000210FE File Offset: 0x0001F2FE
		public void AddCurve(Point[] points, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathCurve2I(this.nativePath, points, points.Length, tension));
		}

		/// <summary>Adds a spline curve to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve.</param>
		/// <param name="tension">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results.</param>
		// Token: 0x06000E90 RID: 3728 RVA: 0x00021123 File Offset: 0x0001F323
		public void AddCurve(PointF[] points, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathCurve2(this.nativePath, points, points.Length, tension));
		}

		/// <summary>Adds a spline curve to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve.</param>
		/// <param name="offset">The index of the element in the <paramref name="points" /> array that is used as the first point in the curve.</param>
		/// <param name="numberOfSegments">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results.</param>
		/// <param name="tension">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results.</param>
		// Token: 0x06000E91 RID: 3729 RVA: 0x00021148 File Offset: 0x0001F348
		public void AddCurve(Point[] points, int offset, int numberOfSegments, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathCurve3I(this.nativePath, points, points.Length, offset, numberOfSegments, tension));
		}

		/// <summary>Adds a spline curve to the current figure.</summary>
		/// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve.</param>
		/// <param name="offset">The index of the element in the <paramref name="points" /> array that is used as the first point in the curve.</param>
		/// <param name="numberOfSegments">The number of segments used to draw the curve. A segment can be thought of as a line connecting two points.</param>
		/// <param name="tension">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results.</param>
		// Token: 0x06000E92 RID: 3730 RVA: 0x00021170 File Offset: 0x0001F370
		public void AddCurve(PointF[] points, int offset, int numberOfSegments, float tension)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathCurve3(this.nativePath, points, points.Length, offset, numberOfSegments, tension));
		}

		/// <summary>Empties the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> and <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathTypes" /> arrays and sets the <see cref="T:System.Drawing.Drawing2D.FillMode" /> to <see cref="F:System.Drawing.Drawing2D.FillMode.Alternate" />.</summary>
		// Token: 0x06000E93 RID: 3731 RVA: 0x00021198 File Offset: 0x0001F398
		public void Reset()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipResetPath(this.nativePath));
		}

		/// <summary>Reverses the order of points in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> array of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		// Token: 0x06000E94 RID: 3732 RVA: 0x000211AA File Offset: 0x0001F3AA
		public void Reverse()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipReversePath(this.nativePath));
		}

		/// <summary>Applies a transform matrix to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents the transformation to apply.</param>
		// Token: 0x06000E95 RID: 3733 RVA: 0x000211BC File Offset: 0x0001F3BC
		public void Transform(Matrix matrix)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipTransformPath(this.nativePath, matrix.nativeMatrix));
		}

		/// <summary>Adds a text string to this path.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to add.</param>
		/// <param name="family">A <see cref="T:System.Drawing.FontFamily" /> that represents the name of the font with which the test is drawn.</param>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> enumeration that represents style information about the text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section).</param>
		/// <param name="emSize">The height of the em square box that bounds the character.</param>
		/// <param name="origin">A <see cref="T:System.Drawing.Point" /> that represents the point where the text starts.</param>
		/// <param name="format">A <see cref="T:System.Drawing.StringFormat" /> that specifies text formatting information, such as line spacing and alignment.</param>
		// Token: 0x06000E96 RID: 3734 RVA: 0x000211E4 File Offset: 0x0001F3E4
		[MonoTODO("The StringFormat parameter is ignored when using libgdiplus.")]
		public void AddString(string s, FontFamily family, int style, float emSize, Point origin, StringFormat format)
		{
			this.AddString(s, family, style, emSize, new Rectangle
			{
				X = origin.X,
				Y = origin.Y
			}, format);
		}

		/// <summary>Adds a text string to this path.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to add.</param>
		/// <param name="family">A <see cref="T:System.Drawing.FontFamily" /> that represents the name of the font with which the test is drawn.</param>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> enumeration that represents style information about the text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section).</param>
		/// <param name="emSize">The height of the em square box that bounds the character.</param>
		/// <param name="origin">A <see cref="T:System.Drawing.PointF" /> that represents the point where the text starts.</param>
		/// <param name="format">A <see cref="T:System.Drawing.StringFormat" /> that specifies text formatting information, such as line spacing and alignment.</param>
		// Token: 0x06000E97 RID: 3735 RVA: 0x00021224 File Offset: 0x0001F424
		[MonoTODO("The StringFormat parameter is ignored when using libgdiplus.")]
		public void AddString(string s, FontFamily family, int style, float emSize, PointF origin, StringFormat format)
		{
			this.AddString(s, family, style, emSize, new RectangleF
			{
				X = origin.X,
				Y = origin.Y
			}, format);
		}

		/// <summary>Adds a text string to this path.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to add.</param>
		/// <param name="family">A <see cref="T:System.Drawing.FontFamily" /> that represents the name of the font with which the test is drawn.</param>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> enumeration that represents style information about the text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section).</param>
		/// <param name="emSize">The height of the em square box that bounds the character.</param>
		/// <param name="layoutRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the text.</param>
		/// <param name="format">A <see cref="T:System.Drawing.StringFormat" /> that specifies text formatting information, such as line spacing and alignment.</param>
		// Token: 0x06000E98 RID: 3736 RVA: 0x00021264 File Offset: 0x0001F464
		[MonoTODO("The layoutRect and StringFormat parameters are ignored when using libgdiplus.")]
		public void AddString(string s, FontFamily family, int style, float emSize, Rectangle layoutRect, StringFormat format)
		{
			if (family == null)
			{
				throw new ArgumentException("family");
			}
			IntPtr format2 = (format == null) ? IntPtr.Zero : format.NativeObject;
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathStringI(this.nativePath, s, s.Length, family.NativeFamily, style, emSize, ref layoutRect, format2));
		}

		/// <summary>Adds a text string to this path.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to add.</param>
		/// <param name="family">A <see cref="T:System.Drawing.FontFamily" /> that represents the name of the font with which the test is drawn.</param>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> enumeration that represents style information about the text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section).</param>
		/// <param name="emSize">The height of the em square box that bounds the character.</param>
		/// <param name="layoutRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the text.</param>
		/// <param name="format">A <see cref="T:System.Drawing.StringFormat" /> that specifies text formatting information, such as line spacing and alignment.</param>
		// Token: 0x06000E99 RID: 3737 RVA: 0x000212B8 File Offset: 0x0001F4B8
		[MonoTODO("The layoutRect and StringFormat parameters are ignored when using libgdiplus.")]
		public void AddString(string s, FontFamily family, int style, float emSize, RectangleF layoutRect, StringFormat format)
		{
			if (family == null)
			{
				throw new ArgumentException("family");
			}
			IntPtr format2 = (format == null) ? IntPtr.Zero : format.NativeObject;
			GDIPlus.CheckStatus(GDIPlus.GdipAddPathString(this.nativePath, s, s.Length, family.NativeFamily, style, emSize, ref layoutRect, format2));
		}

		/// <summary>Clears all markers from this path.</summary>
		// Token: 0x06000E9A RID: 3738 RVA: 0x00021309 File Offset: 0x0001F509
		public void ClearMarkers()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipClearPathMarkers(this.nativePath));
		}

		/// <summary>Closes all open figures in this path and starts a new figure. It closes each open figure by connecting a line from its endpoint to its starting point.</summary>
		// Token: 0x06000E9B RID: 3739 RVA: 0x0002131B File Offset: 0x0001F51B
		public void CloseAllFigures()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipClosePathFigures(this.nativePath));
		}

		/// <summary>Closes the current figure and starts a new figure. If the current figure contains a sequence of connected lines and curves, the method closes the loop by connecting a line from the endpoint to the starting point.</summary>
		// Token: 0x06000E9C RID: 3740 RVA: 0x0002132D File Offset: 0x0001F52D
		public void CloseFigure()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipClosePathFigure(this.nativePath));
		}

		/// <summary>Converts each curve in this path into a sequence of connected line segments.</summary>
		// Token: 0x06000E9D RID: 3741 RVA: 0x0002133F File Offset: 0x0001F53F
		public void Flatten()
		{
			this.Flatten(null, 0.25f);
		}

		/// <summary>Applies the specified transform and then converts each curve in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> into a sequence of connected line segments.</summary>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to transform this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> before flattening.</param>
		// Token: 0x06000E9E RID: 3742 RVA: 0x0002134D File Offset: 0x0001F54D
		public void Flatten(Matrix matrix)
		{
			this.Flatten(matrix, 0.25f);
		}

		/// <summary>Converts each curve in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> into a sequence of connected line segments.</summary>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to transform this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> before flattening.</param>
		/// <param name="flatness">Specifies the maximum permitted error between the curve and its flattened approximation. A value of 0.25 is the default. Reducing the flatness value will increase the number of line segments in the approximation.</param>
		// Token: 0x06000E9F RID: 3743 RVA: 0x0002135C File Offset: 0x0001F55C
		public void Flatten(Matrix matrix, float flatness)
		{
			IntPtr matrix2 = (matrix == null) ? IntPtr.Zero : matrix.nativeMatrix;
			GDIPlus.CheckStatus(GDIPlus.GdipFlattenPath(this.nativePath, matrix2, flatness));
		}

		/// <summary>Returns a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> that represents a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
		// Token: 0x06000EA0 RID: 3744 RVA: 0x0002138C File Offset: 0x0001F58C
		public RectangleF GetBounds()
		{
			return this.GetBounds(null, null);
		}

		/// <summary>Returns a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when this path is transformed by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a transformation to be applied to this path before the bounding rectangle is calculated. This path is not permanently transformed; the transformation is used only during the process of calculating the bounding rectangle.</param>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> that represents a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
		// Token: 0x06000EA1 RID: 3745 RVA: 0x00021396 File Offset: 0x0001F596
		public RectangleF GetBounds(Matrix matrix)
		{
			return this.GetBounds(matrix, null);
		}

		/// <summary>Returns a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when the current path is transformed by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> and drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a transformation to be applied to this path before the bounding rectangle is calculated. This path is not permanently transformed; the transformation is used only during the process of calculating the bounding rectangle.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> with which to draw the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</param>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> that represents a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
		// Token: 0x06000EA2 RID: 3746 RVA: 0x000213A0 File Offset: 0x0001F5A0
		public RectangleF GetBounds(Matrix matrix, Pen pen)
		{
			IntPtr matrix2 = (matrix == null) ? IntPtr.Zero : matrix.nativeMatrix;
			IntPtr pen2 = (pen == null) ? IntPtr.Zero : pen.NativePen;
			RectangleF result;
			GDIPlus.CheckStatus(GDIPlus.GdipGetPathWorldBounds(this.nativePath, out result, matrix2, pen2));
			return result;
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
		/// <param name="point">A <see cref="T:System.Drawing.Point" /> that specifies the location to test.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EA3 RID: 3747 RVA: 0x000213E4 File Offset: 0x0001F5E4
		public bool IsOutlineVisible(Point point, Pen pen)
		{
			return this.IsOutlineVisible(point.X, point.Y, pen, null);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
		/// <param name="point">A <see cref="T:System.Drawing.PointF" /> that specifies the location to test.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EA4 RID: 3748 RVA: 0x000213FC File Offset: 0x0001F5FC
		public bool IsOutlineVisible(PointF point, Pen pen)
		{
			return this.IsOutlineVisible(point.X, point.Y, pen, null);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EA5 RID: 3749 RVA: 0x00021414 File Offset: 0x0001F614
		public bool IsOutlineVisible(int x, int y, Pen pen)
		{
			return this.IsOutlineVisible(x, y, pen, null);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EA6 RID: 3750 RVA: 0x00021420 File Offset: 0x0001F620
		public bool IsOutlineVisible(float x, float y, Pen pen)
		{
			return this.IsOutlineVisible(x, y, pen, null);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that specifies the location to test.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EA7 RID: 3751 RVA: 0x0002142C File Offset: 0x0001F62C
		public bool IsOutlineVisible(Point pt, Pen pen, Graphics graphics)
		{
			return this.IsOutlineVisible(pt.X, pt.Y, pen, graphics);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="pt">A <see cref="T:System.Drawing.PointF" /> that specifies the location to test.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EA8 RID: 3752 RVA: 0x00021444 File Offset: 0x0001F644
		public bool IsOutlineVisible(PointF pt, Pen pen, Graphics graphics)
		{
			return this.IsOutlineVisible(pt.X, pt.Y, pen, graphics);
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EA9 RID: 3753 RVA: 0x0002145C File Offset: 0x0001F65C
		public bool IsOutlineVisible(int x, int y, Pen pen, Graphics graphics)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			IntPtr graphics2 = (graphics == null) ? IntPtr.Zero : graphics.nativeObject;
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsOutlineVisiblePathPointI(this.nativePath, x, y, pen.NativePen, graphics2, out result));
			return result;
		}

		/// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EAA RID: 3754 RVA: 0x000214A8 File Offset: 0x0001F6A8
		public bool IsOutlineVisible(float x, float y, Pen pen, Graphics graphics)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			IntPtr graphics2 = (graphics == null) ? IntPtr.Zero : graphics.nativeObject;
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsOutlineVisiblePathPoint(this.nativePath, x, y, pen.NativePen, graphics2, out result));
			return result;
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="point">A <see cref="T:System.Drawing.Point" /> that represents the point to test.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EAB RID: 3755 RVA: 0x000214F2 File Offset: 0x0001F6F2
		public bool IsVisible(Point point)
		{
			return this.IsVisible(point.X, point.Y, null);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="point">A <see cref="T:System.Drawing.PointF" /> that represents the point to test.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EAC RID: 3756 RVA: 0x00021509 File Offset: 0x0001F709
		public bool IsVisible(PointF point)
		{
			return this.IsVisible(point.X, point.Y, null);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EAD RID: 3757 RVA: 0x00021520 File Offset: 0x0001F720
		public bool IsVisible(int x, int y)
		{
			return this.IsVisible(x, y, null);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EAE RID: 3758 RVA: 0x0002152B File Offset: 0x0001F72B
		public bool IsVisible(float x, float y)
		{
			return this.IsVisible(x, y, null);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that represents the point to test.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EAF RID: 3759 RVA: 0x00021536 File Offset: 0x0001F736
		public bool IsVisible(Point pt, Graphics graphics)
		{
			return this.IsVisible(pt.X, pt.Y, graphics);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="pt">A <see cref="T:System.Drawing.PointF" /> that represents the point to test.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within this; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EB0 RID: 3760 RVA: 0x0002154D File Offset: 0x0001F74D
		public bool IsVisible(PointF pt, Graphics graphics)
		{
			return this.IsVisible(pt.X, pt.Y, graphics);
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />, using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EB1 RID: 3761 RVA: 0x00021564 File Offset: 0x0001F764
		public bool IsVisible(int x, int y, Graphics graphics)
		{
			IntPtr graphics2 = (graphics == null) ? IntPtr.Zero : graphics.nativeObject;
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisiblePathPointI(this.nativePath, x, y, graphics2, out result));
			return result;
		}

		/// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> in the visible clip region of the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
		/// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EB2 RID: 3762 RVA: 0x00021598 File Offset: 0x0001F798
		public bool IsVisible(float x, float y, Graphics graphics)
		{
			IntPtr graphics2 = (graphics == null) ? IntPtr.Zero : graphics.nativeObject;
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisiblePathPoint(this.nativePath, x, y, graphics2, out result));
			return result;
		}

		/// <summary>Sets a marker on this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		// Token: 0x06000EB3 RID: 3763 RVA: 0x000215CC File Offset: 0x0001F7CC
		public void SetMarkers()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetPathMarker(this.nativePath));
		}

		/// <summary>Starts a new figure without closing the current figure. All subsequent points added to the path are added to this new figure.</summary>
		// Token: 0x06000EB4 RID: 3764 RVA: 0x000215DE File Offset: 0x0001F7DE
		public void StartFigure()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipStartPathFigure(this.nativePath));
		}

		/// <summary>Applies a warp transform, defined by a rectangle and a parallelogram, to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="destPoints">An array of <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram to which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the first three points.</param>
		/// <param name="srcRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that is transformed to the parallelogram defined by <paramref name="destPoints" />.</param>
		// Token: 0x06000EB5 RID: 3765 RVA: 0x000215F0 File Offset: 0x0001F7F0
		[MonoTODO("GdipWarpPath isn't implemented in libgdiplus")]
		public void Warp(PointF[] destPoints, RectangleF srcRect)
		{
			this.Warp(destPoints, srcRect, null, WarpMode.Perspective, 0.25f);
		}

		/// <summary>Applies a warp transform, defined by a rectangle and a parallelogram, to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="destPoints">An array of <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram to which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the first three points.</param>
		/// <param name="srcRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that is transformed to the parallelogram defined by <paramref name="destPoints" />.</param>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a geometric transform to apply to the path.</param>
		// Token: 0x06000EB6 RID: 3766 RVA: 0x00021601 File Offset: 0x0001F801
		[MonoTODO("GdipWarpPath isn't implemented in libgdiplus")]
		public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix)
		{
			this.Warp(destPoints, srcRect, matrix, WarpMode.Perspective, 0.25f);
		}

		/// <summary>Applies a warp transform, defined by a rectangle and a parallelogram, to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="destPoints">An array of <see cref="T:System.Drawing.PointF" /> structures that defines a parallelogram to which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the first three points.</param>
		/// <param name="srcRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that is transformed to the parallelogram defined by <paramref name="destPoints" />.</param>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a geometric transform to apply to the path.</param>
		/// <param name="warpMode">A <see cref="T:System.Drawing.Drawing2D.WarpMode" /> enumeration that specifies whether this warp operation uses perspective or bilinear mode.</param>
		// Token: 0x06000EB7 RID: 3767 RVA: 0x00021612 File Offset: 0x0001F812
		[MonoTODO("GdipWarpPath isn't implemented in libgdiplus")]
		public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix, WarpMode warpMode)
		{
			this.Warp(destPoints, srcRect, matrix, warpMode, 0.25f);
		}

		/// <summary>Applies a warp transform, defined by a rectangle and a parallelogram, to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="destPoints">An array of <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram to which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the first three points.</param>
		/// <param name="srcRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that is transformed to the parallelogram defined by <paramref name="destPoints" />.</param>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a geometric transform to apply to the path.</param>
		/// <param name="warpMode">A <see cref="T:System.Drawing.Drawing2D.WarpMode" /> enumeration that specifies whether this warp operation uses perspective or bilinear mode.</param>
		/// <param name="flatness">A value from 0 through 1 that specifies how flat the resulting path is. For more information, see the <see cref="M:System.Drawing.Drawing2D.GraphicsPath.Flatten" /> methods.</param>
		// Token: 0x06000EB8 RID: 3768 RVA: 0x00021624 File Offset: 0x0001F824
		[MonoTODO("GdipWarpPath isn't implemented in libgdiplus")]
		public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix, WarpMode warpMode, float flatness)
		{
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			IntPtr matrix2 = (matrix == null) ? IntPtr.Zero : matrix.nativeMatrix;
			GDIPlus.CheckStatus(GDIPlus.GdipWarpPath(this.nativePath, matrix2, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, warpMode, flatness));
		}

		/// <summary>Adds an additional outline to the path.</summary>
		/// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that specifies the width between the original outline of the path and the new outline this method creates.</param>
		// Token: 0x06000EB9 RID: 3769 RVA: 0x00021685 File Offset: 0x0001F885
		[MonoTODO("GdipWidenPath isn't implemented in libgdiplus")]
		public void Widen(Pen pen)
		{
			this.Widen(pen, null, 0.25f);
		}

		/// <summary>Adds an additional outline to the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that specifies the width between the original outline of the path and the new outline this method creates.</param>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a transform to apply to the path before widening.</param>
		// Token: 0x06000EBA RID: 3770 RVA: 0x00021694 File Offset: 0x0001F894
		[MonoTODO("GdipWidenPath isn't implemented in libgdiplus")]
		public void Widen(Pen pen, Matrix matrix)
		{
			this.Widen(pen, matrix, 0.25f);
		}

		/// <summary>Replaces this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> with curves that enclose the area that is filled when this path is drawn by the specified pen.</summary>
		/// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that specifies the width between the original outline of the path and the new outline this method creates.</param>
		/// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a transform to apply to the path before widening.</param>
		/// <param name="flatness">A value that specifies the flatness for curves.</param>
		// Token: 0x06000EBB RID: 3771 RVA: 0x000216A4 File Offset: 0x0001F8A4
		[MonoTODO("GdipWidenPath isn't implemented in libgdiplus")]
		public void Widen(Pen pen, Matrix matrix, float flatness)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (this.PointCount == 0)
			{
				return;
			}
			IntPtr matrix2 = (matrix == null) ? IntPtr.Zero : matrix.nativeMatrix;
			GDIPlus.CheckStatus(GDIPlus.GdipWidenPath(this.nativePath, pen.NativePen, matrix2, flatness));
		}

		// Token: 0x04000B74 RID: 2932
		private const float FlatnessDefault = 0.25f;

		// Token: 0x04000B75 RID: 2933
		internal IntPtr nativePath = IntPtr.Zero;
	}
}
