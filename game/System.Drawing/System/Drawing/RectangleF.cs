using System;
using System.ComponentModel;
using System.Numerics.Hashing;

namespace System.Drawing
{
	/// <summary>Stores a set of four floating-point numbers that represent the location and size of a rectangle. For more advanced region functions, use a <see cref="T:System.Drawing.Region" /> object.</summary>
	// Token: 0x0200004A RID: 74
	[Serializable]
	public struct RectangleF : IEquatable<RectangleF>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.RectangleF" /> class with the specified location and size.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle.</param>
		/// <param name="width">The width of the rectangle.</param>
		/// <param name="height">The height of the rectangle.</param>
		// Token: 0x06000292 RID: 658 RVA: 0x00007AE1 File Offset: 0x00005CE1
		public RectangleF(float x, float y, float width, float height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.RectangleF" /> class with the specified location and size.</summary>
		/// <param name="location">A <see cref="T:System.Drawing.PointF" /> that represents the upper-left corner of the rectangular region.</param>
		/// <param name="size">A <see cref="T:System.Drawing.SizeF" /> that represents the width and height of the rectangular region.</param>
		// Token: 0x06000293 RID: 659 RVA: 0x00007B00 File Offset: 0x00005D00
		public RectangleF(PointF location, SizeF size)
		{
			this.x = location.X;
			this.y = location.Y;
			this.width = size.Width;
			this.height = size.Height;
		}

		/// <summary>Creates a <see cref="T:System.Drawing.RectangleF" /> structure with upper-left corner and lower-right corner at the specified locations.</summary>
		/// <param name="left">The x-coordinate of the upper-left corner of the rectangular region.</param>
		/// <param name="top">The y-coordinate of the upper-left corner of the rectangular region.</param>
		/// <param name="right">The x-coordinate of the lower-right corner of the rectangular region.</param>
		/// <param name="bottom">The y-coordinate of the lower-right corner of the rectangular region.</param>
		/// <returns>The new <see cref="T:System.Drawing.RectangleF" /> that this method creates.</returns>
		// Token: 0x06000294 RID: 660 RVA: 0x00007B36 File Offset: 0x00005D36
		public static RectangleF FromLTRB(float left, float top, float right, float bottom)
		{
			return new RectangleF(left, top, right - left, bottom - top);
		}

		/// <summary>Gets or sets the coordinates of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.PointF" /> that represents the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00007B45 File Offset: 0x00005D45
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00007B58 File Offset: 0x00005D58
		[Browsable(false)]
		public PointF Location
		{
			get
			{
				return new PointF(this.X, this.Y);
			}
			set
			{
				this.X = value.X;
				this.Y = value.Y;
			}
		}

		/// <summary>Gets or sets the size of this <see cref="T:System.Drawing.RectangleF" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> that represents the width and height of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00007B74 File Offset: 0x00005D74
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00007B87 File Offset: 0x00005D87
		[Browsable(false)]
		public SizeF Size
		{
			get
			{
				return new SizeF(this.Width, this.Height);
			}
			set
			{
				this.Width = value.Width;
				this.Height = value.Height;
			}
		}

		/// <summary>Gets or sets the x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00007BA3 File Offset: 0x00005DA3
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00007BAB File Offset: 0x00005DAB
		public float X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		/// <summary>Gets or sets the y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00007BB4 File Offset: 0x00005DB4
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00007BBC File Offset: 0x00005DBC
		public float Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		/// <summary>Gets or sets the width of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The width of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00007BC5 File Offset: 0x00005DC5
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00007BCD File Offset: 0x00005DCD
		public float Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		/// <summary>Gets or sets the height of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The height of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00007BD6 File Offset: 0x00005DD6
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x00007BDE File Offset: 0x00005DDE
		public float Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		/// <summary>Gets the x-coordinate of the left edge of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The x-coordinate of the left edge of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00007BE7 File Offset: 0x00005DE7
		[Browsable(false)]
		public float Left
		{
			get
			{
				return this.X;
			}
		}

		/// <summary>Gets the y-coordinate of the top edge of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The y-coordinate of the top edge of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00007BEF File Offset: 0x00005DEF
		[Browsable(false)]
		public float Top
		{
			get
			{
				return this.Y;
			}
		}

		/// <summary>Gets the x-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.X" /> and <see cref="P:System.Drawing.RectangleF.Width" /> of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The x-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.X" /> and <see cref="P:System.Drawing.RectangleF.Width" /> of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00007BF7 File Offset: 0x00005DF7
		[Browsable(false)]
		public float Right
		{
			get
			{
				return this.X + this.Width;
			}
		}

		/// <summary>Gets the y-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.Y" /> and <see cref="P:System.Drawing.RectangleF.Height" /> of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <returns>The y-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.Y" /> and <see cref="P:System.Drawing.RectangleF.Height" /> of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00007C06 File Offset: 0x00005E06
		[Browsable(false)]
		public float Bottom
		{
			get
			{
				return this.Y + this.Height;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="P:System.Drawing.RectangleF.Width" /> or <see cref="P:System.Drawing.RectangleF.Height" /> property of this <see cref="T:System.Drawing.RectangleF" /> has a value of zero.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Drawing.RectangleF.Width" /> or <see cref="P:System.Drawing.RectangleF.Height" /> property of this <see cref="T:System.Drawing.RectangleF" /> has a value of zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00007C15 File Offset: 0x00005E15
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.Width <= 0f || this.Height <= 0f;
			}
		}

		/// <summary>Tests whether <paramref name="obj" /> is a <see cref="T:System.Drawing.RectangleF" /> with the same location and size of this <see cref="T:System.Drawing.RectangleF" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.RectangleF" /> and its <see langword="X" />, <see langword="Y" />, <see langword="Width" />, and <see langword="Height" /> properties are equal to the corresponding properties of this <see cref="T:System.Drawing.RectangleF" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002A6 RID: 678 RVA: 0x00007C36 File Offset: 0x00005E36
		public override bool Equals(object obj)
		{
			return obj is RectangleF && this.Equals((RectangleF)obj);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00007C4E File Offset: 0x00005E4E
		public bool Equals(RectangleF other)
		{
			return this == other;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.RectangleF" /> structures have equal location and size.</summary>
		/// <param name="left">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the right of the equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if the two specified <see cref="T:System.Drawing.RectangleF" /> structures have equal <see cref="P:System.Drawing.RectangleF.X" />, <see cref="P:System.Drawing.RectangleF.Y" />, <see cref="P:System.Drawing.RectangleF.Width" />, and <see cref="P:System.Drawing.RectangleF.Height" /> properties; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002A8 RID: 680 RVA: 0x00007C5C File Offset: 0x00005E5C
		public static bool operator ==(RectangleF left, RectangleF right)
		{
			return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.RectangleF" /> structures differ in location or size.</summary>
		/// <param name="left">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the left of the inequality operator.</param>
		/// <param name="right">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the right of the inequality operator.</param>
		/// <returns>
		///   <see langword="true" /> if any of the <see cref="P:System.Drawing.RectangleF.X" /> , <see cref="P:System.Drawing.RectangleF.Y" />, <see cref="P:System.Drawing.RectangleF.Width" />, or <see cref="P:System.Drawing.RectangleF.Height" /> properties of the two <see cref="T:System.Drawing.Rectangle" /> structures are unequal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002A9 RID: 681 RVA: 0x00007CAB File Offset: 0x00005EAB
		public static bool operator !=(RectangleF left, RectangleF right)
		{
			return !(left == right);
		}

		/// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <returns>
		///   <see langword="true" /> if the point defined by <paramref name="x" /> and <paramref name="y" /> is contained within this <see cref="T:System.Drawing.RectangleF" /> structure; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002AA RID: 682 RVA: 0x00007CB7 File Offset: 0x00005EB7
		public bool Contains(float x, float y)
		{
			return this.X <= x && x < this.X + this.Width && this.Y <= y && y < this.Y + this.Height;
		}

		/// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to test.</param>
		/// <returns>
		///   <see langword="true" /> if the point represented by the <paramref name="pt" /> parameter is contained within this <see cref="T:System.Drawing.RectangleF" /> structure; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002AB RID: 683 RVA: 0x00007CED File Offset: 0x00005EED
		public bool Contains(PointF pt)
		{
			return this.Contains(pt.X, pt.Y);
		}

		/// <summary>Determines if the rectangular region represented by <paramref name="rect" /> is entirely contained within this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> to test.</param>
		/// <returns>
		///   <see langword="true" /> if the rectangular region represented by <paramref name="rect" /> is entirely contained within the rectangular region represented by this <see cref="T:System.Drawing.RectangleF" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002AC RID: 684 RVA: 0x00007D04 File Offset: 0x00005F04
		public bool Contains(RectangleF rect)
		{
			return this.X <= rect.X && rect.X + rect.Width <= this.X + this.Width && this.Y <= rect.Y && rect.Y + rect.Height <= this.Y + this.Height;
		}

		/// <summary>Gets the hash code for this <see cref="T:System.Drawing.RectangleF" /> structure. For information about the use of hash codes, see <see langword="Object.GetHashCode" />.</summary>
		/// <returns>The hash code for this <see cref="T:System.Drawing.RectangleF" />.</returns>
		// Token: 0x060002AD RID: 685 RVA: 0x00007D70 File Offset: 0x00005F70
		public override int GetHashCode()
		{
			return HashHelpers.Combine(HashHelpers.Combine(HashHelpers.Combine(this.X.GetHashCode(), this.Y.GetHashCode()), this.Width.GetHashCode()), this.Height.GetHashCode());
		}

		/// <summary>Enlarges this <see cref="T:System.Drawing.RectangleF" /> structure by the specified amount.</summary>
		/// <param name="x">The amount to inflate this <see cref="T:System.Drawing.RectangleF" /> structure horizontally.</param>
		/// <param name="y">The amount to inflate this <see cref="T:System.Drawing.RectangleF" /> structure vertically.</param>
		// Token: 0x060002AE RID: 686 RVA: 0x00007DC4 File Offset: 0x00005FC4
		public void Inflate(float x, float y)
		{
			this.X -= x;
			this.Y -= y;
			this.Width += 2f * x;
			this.Height += 2f * y;
		}

		/// <summary>Enlarges this <see cref="T:System.Drawing.RectangleF" /> by the specified amount.</summary>
		/// <param name="size">The amount to inflate this rectangle.</param>
		// Token: 0x060002AF RID: 687 RVA: 0x00007E15 File Offset: 0x00006015
		public void Inflate(SizeF size)
		{
			this.Inflate(size.Width, size.Height);
		}

		/// <summary>Creates and returns an enlarged copy of the specified <see cref="T:System.Drawing.RectangleF" /> structure. The copy is enlarged by the specified amount and the original rectangle remains unmodified.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> to be copied. This rectangle is not modified.</param>
		/// <param name="x">The amount to enlarge the copy of the rectangle horizontally.</param>
		/// <param name="y">The amount to enlarge the copy of the rectangle vertically.</param>
		/// <returns>The enlarged <see cref="T:System.Drawing.RectangleF" />.</returns>
		// Token: 0x060002B0 RID: 688 RVA: 0x00007E2C File Offset: 0x0000602C
		public static RectangleF Inflate(RectangleF rect, float x, float y)
		{
			RectangleF result = rect;
			result.Inflate(x, y);
			return result;
		}

		/// <summary>Replaces this <see cref="T:System.Drawing.RectangleF" /> structure with the intersection of itself and the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">The rectangle to intersect.</param>
		// Token: 0x060002B1 RID: 689 RVA: 0x00007E48 File Offset: 0x00006048
		public void Intersect(RectangleF rect)
		{
			RectangleF rectangleF = RectangleF.Intersect(rect, this);
			this.X = rectangleF.X;
			this.Y = rectangleF.Y;
			this.Width = rectangleF.Width;
			this.Height = rectangleF.Height;
		}

		/// <summary>Returns a <see cref="T:System.Drawing.RectangleF" /> structure that represents the intersection of two rectangles. If there is no intersection, and empty <see cref="T:System.Drawing.RectangleF" /> is returned.</summary>
		/// <param name="a">A rectangle to intersect.</param>
		/// <param name="b">A rectangle to intersect.</param>
		/// <returns>A third <see cref="T:System.Drawing.RectangleF" /> structure the size of which represents the overlapped area of the two specified rectangles.</returns>
		// Token: 0x060002B2 RID: 690 RVA: 0x00007E98 File Offset: 0x00006098
		public static RectangleF Intersect(RectangleF a, RectangleF b)
		{
			float num = Math.Max(a.X, b.X);
			float num2 = Math.Min(a.X + a.Width, b.X + b.Width);
			float num3 = Math.Max(a.Y, b.Y);
			float num4 = Math.Min(a.Y + a.Height, b.Y + b.Height);
			if (num2 >= num && num4 >= num3)
			{
				return new RectangleF(num, num3, num2 - num, num4 - num3);
			}
			return RectangleF.Empty;
		}

		/// <summary>Determines if this rectangle intersects with <paramref name="rect" />.</summary>
		/// <param name="rect">The rectangle to test.</param>
		/// <returns>
		///   <see langword="true" /> if there is any intersection; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002B3 RID: 691 RVA: 0x00007F30 File Offset: 0x00006130
		public bool IntersectsWith(RectangleF rect)
		{
			return rect.X < this.X + this.Width && this.X < rect.X + rect.Width && rect.Y < this.Y + this.Height && this.Y < rect.Y + rect.Height;
		}

		/// <summary>Creates the smallest possible third rectangle that can contain both of two rectangles that form a union.</summary>
		/// <param name="a">A rectangle to union.</param>
		/// <param name="b">A rectangle to union.</param>
		/// <returns>A third <see cref="T:System.Drawing.RectangleF" /> structure that contains both of the two rectangles that form the union.</returns>
		// Token: 0x060002B4 RID: 692 RVA: 0x00007F9C File Offset: 0x0000619C
		public static RectangleF Union(RectangleF a, RectangleF b)
		{
			float num = Math.Min(a.X, b.X);
			float num2 = Math.Max(a.X + a.Width, b.X + b.Width);
			float num3 = Math.Min(a.Y, b.Y);
			float num4 = Math.Max(a.Y + a.Height, b.Y + b.Height);
			return new RectangleF(num, num3, num2 - num, num4 - num3);
		}

		/// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
		/// <param name="pos">The amount to offset the location.</param>
		// Token: 0x060002B5 RID: 693 RVA: 0x00008026 File Offset: 0x00006226
		public void Offset(PointF pos)
		{
			this.Offset(pos.X, pos.Y);
		}

		/// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
		/// <param name="x">The amount to offset the location horizontally.</param>
		/// <param name="y">The amount to offset the location vertically.</param>
		// Token: 0x060002B6 RID: 694 RVA: 0x0000803C File Offset: 0x0000623C
		public void Offset(float x, float y)
		{
			this.X += x;
			this.Y += y;
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.Rectangle" /> structure to a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="r">The <see cref="T:System.Drawing.Rectangle" /> structure to convert.</param>
		/// <returns>The <see cref="T:System.Drawing.RectangleF" /> structure that is converted from the specified <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		// Token: 0x060002B7 RID: 695 RVA: 0x0000805A File Offset: 0x0000625A
		public static implicit operator RectangleF(Rectangle r)
		{
			return new RectangleF((float)r.X, (float)r.Y, (float)r.Width, (float)r.Height);
		}

		/// <summary>Converts the <see langword="Location" /> and <see cref="T:System.Drawing.Size" /> of this <see cref="T:System.Drawing.RectangleF" /> to a human-readable string.</summary>
		/// <returns>A string that contains the position, width, and height of this <see cref="T:System.Drawing.RectangleF" /> structure. For example, "{X=20, Y=20, Width=100, Height=50}".</returns>
		// Token: 0x060002B8 RID: 696 RVA: 0x00008084 File Offset: 0x00006284
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{X=",
				this.X.ToString(),
				",Y=",
				this.Y.ToString(),
				",Width=",
				this.Width.ToString(),
				",Height=",
				this.Height.ToString(),
				"}"
			});
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000049FE File Offset: 0x00002BFE
		// Note: this type is marked as 'beforefieldinit'.
		static RectangleF()
		{
		}

		/// <summary>Represents an instance of the <see cref="T:System.Drawing.RectangleF" /> class with its members uninitialized.</summary>
		// Token: 0x04000396 RID: 918
		public static readonly RectangleF Empty;

		// Token: 0x04000397 RID: 919
		private float x;

		// Token: 0x04000398 RID: 920
		private float y;

		// Token: 0x04000399 RID: 921
		private float width;

		// Token: 0x0400039A RID: 922
		private float height;
	}
}
