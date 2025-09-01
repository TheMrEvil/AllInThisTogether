using System;
using System.ComponentModel;
using System.Numerics.Hashing;

namespace System.Drawing
{
	/// <summary>Stores a set of four integers that represent the location and size of a rectangle</summary>
	// Token: 0x02000049 RID: 73
	[TypeConverter(typeof(RectangleConverter))]
	[Serializable]
	public struct Rectangle : IEquatable<Rectangle>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Rectangle" /> class with the specified location and size.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle.</param>
		/// <param name="width">The width of the rectangle.</param>
		/// <param name="height">The height of the rectangle.</param>
		// Token: 0x06000268 RID: 616 RVA: 0x00007474 File Offset: 0x00005674
		public Rectangle(int x, int y, int width, int height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Rectangle" /> class with the specified location and size.</summary>
		/// <param name="location">A <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the rectangular region.</param>
		/// <param name="size">A <see cref="T:System.Drawing.Size" /> that represents the width and height of the rectangular region.</param>
		// Token: 0x06000269 RID: 617 RVA: 0x00007493 File Offset: 0x00005693
		public Rectangle(Point location, Size size)
		{
			this.x = location.X;
			this.y = location.Y;
			this.width = size.Width;
			this.height = size.Height;
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Rectangle" /> structure with the specified edge locations.</summary>
		/// <param name="left">The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</param>
		/// <param name="top">The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</param>
		/// <param name="right">The x-coordinate of the lower-right corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</param>
		/// <param name="bottom">The y-coordinate of the lower-right corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</param>
		/// <returns>The new <see cref="T:System.Drawing.Rectangle" /> that this method creates.</returns>
		// Token: 0x0600026A RID: 618 RVA: 0x000074C9 File Offset: 0x000056C9
		public static Rectangle FromLTRB(int left, int top, int right, int bottom)
		{
			return new Rectangle(left, top, right - left, bottom - top);
		}

		/// <summary>Gets or sets the coordinates of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000074D8 File Offset: 0x000056D8
		// (set) Token: 0x0600026C RID: 620 RVA: 0x000074EB File Offset: 0x000056EB
		[Browsable(false)]
		public Point Location
		{
			get
			{
				return new Point(this.X, this.Y);
			}
			set
			{
				this.X = value.X;
				this.Y = value.Y;
			}
		}

		/// <summary>Gets or sets the size of this <see cref="T:System.Drawing.Rectangle" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the width and height of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00007507 File Offset: 0x00005707
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000751A File Offset: 0x0000571A
		[Browsable(false)]
		public Size Size
		{
			get
			{
				return new Size(this.Width, this.Height);
			}
			set
			{
				this.Width = value.Width;
				this.Height = value.Height;
			}
		}

		/// <summary>Gets or sets the x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00007536 File Offset: 0x00005736
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000753E File Offset: 0x0000573E
		public int X
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

		/// <summary>Gets or sets the y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00007547 File Offset: 0x00005747
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000754F File Offset: 0x0000574F
		public int Y
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

		/// <summary>Gets or sets the width of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The width of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00007558 File Offset: 0x00005758
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00007560 File Offset: 0x00005760
		public int Width
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

		/// <summary>Gets or sets the height of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The height of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00007569 File Offset: 0x00005769
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00007571 File Offset: 0x00005771
		public int Height
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

		/// <summary>Gets the x-coordinate of the left edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The x-coordinate of the left edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000757A File Offset: 0x0000577A
		[Browsable(false)]
		public int Left
		{
			get
			{
				return this.X;
			}
		}

		/// <summary>Gets the y-coordinate of the top edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The y-coordinate of the top edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00007582 File Offset: 0x00005782
		[Browsable(false)]
		public int Top
		{
			get
			{
				return this.Y;
			}
		}

		/// <summary>Gets the x-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.X" /> and <see cref="P:System.Drawing.Rectangle.Width" /> property values of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The x-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.X" /> and <see cref="P:System.Drawing.Rectangle.Width" /> of this <see cref="T:System.Drawing.Rectangle" />.</returns>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000758A File Offset: 0x0000578A
		[Browsable(false)]
		public int Right
		{
			get
			{
				return this.X + this.Width;
			}
		}

		/// <summary>Gets the y-coordinate that is the sum of the <see cref="P:System.Drawing.Rectangle.Y" /> and <see cref="P:System.Drawing.Rectangle.Height" /> property values of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <returns>The y-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.Y" /> and <see cref="P:System.Drawing.Rectangle.Height" /> of this <see cref="T:System.Drawing.Rectangle" />.</returns>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00007599 File Offset: 0x00005799
		[Browsable(false)]
		public int Bottom
		{
			get
			{
				return this.Y + this.Height;
			}
		}

		/// <summary>Tests whether all numeric properties of this <see cref="T:System.Drawing.Rectangle" /> have values of zero.</summary>
		/// <returns>This property returns <see langword="true" /> if the <see cref="P:System.Drawing.Rectangle.Width" />, <see cref="P:System.Drawing.Rectangle.Height" />, <see cref="P:System.Drawing.Rectangle.X" />, and <see cref="P:System.Drawing.Rectangle.Y" /> properties of this <see cref="T:System.Drawing.Rectangle" /> all have values of zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600027B RID: 635 RVA: 0x000075A8 File Offset: 0x000057A8
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.height == 0 && this.width == 0 && this.x == 0 && this.y == 0;
			}
		}

		/// <summary>Tests whether <paramref name="obj" /> is a <see cref="T:System.Drawing.Rectangle" /> structure with the same location and size of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to test.</param>
		/// <returns>This method returns <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.Rectangle" /> structure and its <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" />, and <see cref="P:System.Drawing.Rectangle.Height" /> properties are equal to the corresponding properties of this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600027C RID: 636 RVA: 0x000075CD File Offset: 0x000057CD
		public override bool Equals(object obj)
		{
			return obj is Rectangle && this.Equals((Rectangle)obj);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000075E5 File Offset: 0x000057E5
		public bool Equals(Rectangle other)
		{
			return this == other;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.Rectangle" /> structures have equal location and size.</summary>
		/// <param name="left">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the right of the equality operator.</param>
		/// <returns>This operator returns <see langword="true" /> if the two <see cref="T:System.Drawing.Rectangle" /> structures have equal <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" />, and <see cref="P:System.Drawing.Rectangle.Height" /> properties.</returns>
		// Token: 0x0600027E RID: 638 RVA: 0x000075F4 File Offset: 0x000057F4
		public static bool operator ==(Rectangle left, Rectangle right)
		{
			return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.Rectangle" /> structures differ in location or size.</summary>
		/// <param name="left">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the left of the inequality operator.</param>
		/// <param name="right">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the right of the inequality operator.</param>
		/// <returns>This operator returns <see langword="true" /> if any of the <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" /> or <see cref="P:System.Drawing.Rectangle.Height" /> properties of the two <see cref="T:System.Drawing.Rectangle" /> structures are unequal; otherwise <see langword="false" />.</returns>
		// Token: 0x0600027F RID: 639 RVA: 0x00007643 File Offset: 0x00005843
		public static bool operator !=(Rectangle left, Rectangle right)
		{
			return !(left == right);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF" /> structure to a <see cref="T:System.Drawing.Rectangle" /> structure by rounding the <see cref="T:System.Drawing.RectangleF" /> values to the next higher integer values.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.RectangleF" /> structure to be converted.</param>
		/// <returns>Returns a <see cref="T:System.Drawing.Rectangle" />.</returns>
		// Token: 0x06000280 RID: 640 RVA: 0x0000764F File Offset: 0x0000584F
		public static Rectangle Ceiling(RectangleF value)
		{
			return new Rectangle((int)Math.Ceiling((double)value.X), (int)Math.Ceiling((double)value.Y), (int)Math.Ceiling((double)value.Width), (int)Math.Ceiling((double)value.Height));
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF" /> to a <see cref="T:System.Drawing.Rectangle" /> by truncating the <see cref="T:System.Drawing.RectangleF" /> values.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.RectangleF" /> to be converted.</param>
		/// <returns>The truncated value of the  <see cref="T:System.Drawing.Rectangle" />.</returns>
		// Token: 0x06000281 RID: 641 RVA: 0x0000768E File Offset: 0x0000588E
		public static Rectangle Truncate(RectangleF value)
		{
			return new Rectangle((int)value.X, (int)value.Y, (int)value.Width, (int)value.Height);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF" /> to a <see cref="T:System.Drawing.Rectangle" /> by rounding the <see cref="T:System.Drawing.RectangleF" /> values to the nearest integer values.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.RectangleF" /> to be converted.</param>
		/// <returns>The rounded interger value of the <see cref="T:System.Drawing.Rectangle" />.</returns>
		// Token: 0x06000282 RID: 642 RVA: 0x000076B5 File Offset: 0x000058B5
		public static Rectangle Round(RectangleF value)
		{
			return new Rectangle((int)Math.Round((double)value.X), (int)Math.Round((double)value.Y), (int)Math.Round((double)value.Width), (int)Math.Round((double)value.Height));
		}

		/// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		/// <returns>This method returns <see langword="true" /> if the point defined by <paramref name="x" /> and <paramref name="y" /> is contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise <see langword="false" />.</returns>
		// Token: 0x06000283 RID: 643 RVA: 0x000076F4 File Offset: 0x000058F4
		public bool Contains(int x, int y)
		{
			return this.X <= x && x < this.X + this.Width && this.Y <= y && y < this.Y + this.Height;
		}

		/// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to test.</param>
		/// <returns>This method returns <see langword="true" /> if the point represented by <paramref name="pt" /> is contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise <see langword="false" />.</returns>
		// Token: 0x06000284 RID: 644 RVA: 0x0000772A File Offset: 0x0000592A
		public bool Contains(Point pt)
		{
			return this.Contains(pt.X, pt.Y);
		}

		/// <summary>Determines if the rectangular region represented by <paramref name="rect" /> is entirely contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> to test.</param>
		/// <returns>This method returns <see langword="true" /> if the rectangular region represented by <paramref name="rect" /> is entirely contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise <see langword="false" />.</returns>
		// Token: 0x06000285 RID: 645 RVA: 0x00007740 File Offset: 0x00005940
		public bool Contains(Rectangle rect)
		{
			return this.X <= rect.X && rect.X + rect.Width <= this.X + this.Width && this.Y <= rect.Y && rect.Y + rect.Height <= this.Y + this.Height;
		}

		/// <summary>Returns the hash code for this <see cref="T:System.Drawing.Rectangle" /> structure. For information about the use of hash codes, see <see cref="M:System.Object.GetHashCode" /> .</summary>
		/// <returns>An integer that represents the hash code for this rectangle.</returns>
		// Token: 0x06000286 RID: 646 RVA: 0x000077AC File Offset: 0x000059AC
		public override int GetHashCode()
		{
			return HashHelpers.Combine(HashHelpers.Combine(HashHelpers.Combine(this.X, this.Y), this.Width), this.Height);
		}

		/// <summary>Enlarges this <see cref="T:System.Drawing.Rectangle" /> by the specified amount.</summary>
		/// <param name="width">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> horizontally.</param>
		/// <param name="height">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> vertically.</param>
		// Token: 0x06000287 RID: 647 RVA: 0x000077D5 File Offset: 0x000059D5
		public void Inflate(int width, int height)
		{
			this.X -= width;
			this.Y -= height;
			this.Width += 2 * width;
			this.Height += 2 * height;
		}

		/// <summary>Enlarges this <see cref="T:System.Drawing.Rectangle" /> by the specified amount.</summary>
		/// <param name="size">The amount to inflate this rectangle.</param>
		// Token: 0x06000288 RID: 648 RVA: 0x00007813 File Offset: 0x00005A13
		public void Inflate(Size size)
		{
			this.Inflate(size.Width, size.Height);
		}

		/// <summary>Creates and returns an enlarged copy of the specified <see cref="T:System.Drawing.Rectangle" /> structure. The copy is enlarged by the specified amount. The original <see cref="T:System.Drawing.Rectangle" /> structure remains unmodified.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> with which to start. This rectangle is not modified.</param>
		/// <param name="x">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> horizontally.</param>
		/// <param name="y">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> vertically.</param>
		/// <returns>The enlarged <see cref="T:System.Drawing.Rectangle" />.</returns>
		// Token: 0x06000289 RID: 649 RVA: 0x0000782C File Offset: 0x00005A2C
		public static Rectangle Inflate(Rectangle rect, int x, int y)
		{
			Rectangle result = rect;
			result.Inflate(x, y);
			return result;
		}

		/// <summary>Replaces this <see cref="T:System.Drawing.Rectangle" /> with the intersection of itself and the specified <see cref="T:System.Drawing.Rectangle" />.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> with which to intersect.</param>
		// Token: 0x0600028A RID: 650 RVA: 0x00007848 File Offset: 0x00005A48
		public void Intersect(Rectangle rect)
		{
			Rectangle rectangle = Rectangle.Intersect(rect, this);
			this.X = rectangle.X;
			this.Y = rectangle.Y;
			this.Width = rectangle.Width;
			this.Height = rectangle.Height;
		}

		/// <summary>Returns a third <see cref="T:System.Drawing.Rectangle" /> structure that represents the intersection of two other <see cref="T:System.Drawing.Rectangle" /> structures. If there is no intersection, an empty <see cref="T:System.Drawing.Rectangle" /> is returned.</summary>
		/// <param name="a">A rectangle to intersect.</param>
		/// <param name="b">A rectangle to intersect.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the intersection of <paramref name="a" /> and <paramref name="b" />.</returns>
		// Token: 0x0600028B RID: 651 RVA: 0x00007898 File Offset: 0x00005A98
		public static Rectangle Intersect(Rectangle a, Rectangle b)
		{
			int num = Math.Max(a.X, b.X);
			int num2 = Math.Min(a.X + a.Width, b.X + b.Width);
			int num3 = Math.Max(a.Y, b.Y);
			int num4 = Math.Min(a.Y + a.Height, b.Y + b.Height);
			if (num2 >= num && num4 >= num3)
			{
				return new Rectangle(num, num3, num2 - num, num4 - num3);
			}
			return Rectangle.Empty;
		}

		/// <summary>Determines if this rectangle intersects with <paramref name="rect" />.</summary>
		/// <param name="rect">The rectangle to test.</param>
		/// <returns>This method returns <see langword="true" /> if there is any intersection, otherwise <see langword="false" />.</returns>
		// Token: 0x0600028C RID: 652 RVA: 0x00007930 File Offset: 0x00005B30
		public bool IntersectsWith(Rectangle rect)
		{
			return rect.X < this.X + this.Width && this.X < rect.X + rect.Width && rect.Y < this.Y + this.Height && this.Y < rect.Y + rect.Height;
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Rectangle" /> structure that contains the union of two <see cref="T:System.Drawing.Rectangle" /> structures.</summary>
		/// <param name="a">A rectangle to union.</param>
		/// <param name="b">A rectangle to union.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> structure that bounds the union of the two <see cref="T:System.Drawing.Rectangle" /> structures.</returns>
		// Token: 0x0600028D RID: 653 RVA: 0x0000799C File Offset: 0x00005B9C
		public static Rectangle Union(Rectangle a, Rectangle b)
		{
			int num = Math.Min(a.X, b.X);
			int num2 = Math.Max(a.X + a.Width, b.X + b.Width);
			int num3 = Math.Min(a.Y, b.Y);
			int num4 = Math.Max(a.Y + a.Height, b.Y + b.Height);
			return new Rectangle(num, num3, num2 - num, num4 - num3);
		}

		/// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
		/// <param name="pos">Amount to offset the location.</param>
		// Token: 0x0600028E RID: 654 RVA: 0x00007A26 File Offset: 0x00005C26
		public void Offset(Point pos)
		{
			this.Offset(pos.X, pos.Y);
		}

		/// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
		/// <param name="x">The horizontal offset.</param>
		/// <param name="y">The vertical offset.</param>
		// Token: 0x0600028F RID: 655 RVA: 0x00007A3C File Offset: 0x00005C3C
		public void Offset(int x, int y)
		{
			this.X += x;
			this.Y += y;
		}

		/// <summary>Converts the attributes of this <see cref="T:System.Drawing.Rectangle" /> to a human-readable string.</summary>
		/// <returns>A string that contains the position, width, and height of this <see cref="T:System.Drawing.Rectangle" /> structure ¾ for example, {X=20, Y=20, Width=100, Height=50}</returns>
		// Token: 0x06000290 RID: 656 RVA: 0x00007A5C File Offset: 0x00005C5C
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

		// Token: 0x06000291 RID: 657 RVA: 0x000049FE File Offset: 0x00002BFE
		// Note: this type is marked as 'beforefieldinit'.
		static Rectangle()
		{
		}

		/// <summary>Represents a <see cref="T:System.Drawing.Rectangle" /> structure with its properties left uninitialized.</summary>
		// Token: 0x04000391 RID: 913
		public static readonly Rectangle Empty;

		// Token: 0x04000392 RID: 914
		private int x;

		// Token: 0x04000393 RID: 915
		private int y;

		// Token: 0x04000394 RID: 916
		private int width;

		// Token: 0x04000395 RID: 917
		private int height;
	}
}
