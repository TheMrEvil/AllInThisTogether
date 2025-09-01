using System;
using System.ComponentModel;
using System.Numerics.Hashing;

namespace System.Drawing
{
	/// <summary>Represents an ordered pair of integer x- and y-coordinates that defines a point in a two-dimensional plane.</summary>
	// Token: 0x02000047 RID: 71
	[TypeConverter(typeof(PointConverter))]
	[Serializable]
	public struct Point : IEquatable<Point>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Point" /> class with the specified coordinates.</summary>
		/// <param name="x">The horizontal position of the point.</param>
		/// <param name="y">The vertical position of the point.</param>
		// Token: 0x06000237 RID: 567 RVA: 0x0000702D File Offset: 0x0000522D
		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Point" /> class from a <see cref="T:System.Drawing.Size" />.</summary>
		/// <param name="sz">A <see cref="T:System.Drawing.Size" /> that specifies the coordinates for the new <see cref="T:System.Drawing.Point" />.</param>
		// Token: 0x06000238 RID: 568 RVA: 0x0000703D File Offset: 0x0000523D
		public Point(Size sz)
		{
			this.x = sz.Width;
			this.y = sz.Height;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Point" /> class using coordinates specified by an integer value.</summary>
		/// <param name="dw">A 32-bit integer that specifies the coordinates for the new <see cref="T:System.Drawing.Point" />.</param>
		// Token: 0x06000239 RID: 569 RVA: 0x00007059 File Offset: 0x00005259
		public Point(int dw)
		{
			this.x = (int)Point.LowInt16(dw);
			this.y = (int)Point.HighInt16(dw);
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Point" /> is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if both <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> are 0; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00007073 File Offset: 0x00005273
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.x == 0 && this.y == 0;
			}
		}

		/// <summary>Gets or sets the x-coordinate of this <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>The x-coordinate of this <see cref="T:System.Drawing.Point" />.</returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00007088 File Offset: 0x00005288
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00007090 File Offset: 0x00005290
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

		/// <summary>Gets or sets the y-coordinate of this <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>The y-coordinate of this <see cref="T:System.Drawing.Point" />.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00007099 File Offset: 0x00005299
		// (set) Token: 0x0600023E RID: 574 RVA: 0x000070A1 File Offset: 0x000052A1
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

		/// <summary>Converts the specified <see cref="T:System.Drawing.Point" /> structure to a <see cref="T:System.Drawing.PointF" /> structure.</summary>
		/// <param name="p">The <see cref="T:System.Drawing.Point" /> to be converted.</param>
		/// <returns>The <see cref="T:System.Drawing.PointF" /> that results from the conversion.</returns>
		// Token: 0x0600023F RID: 575 RVA: 0x000070AA File Offset: 0x000052AA
		public static implicit operator PointF(Point p)
		{
			return new PointF((float)p.X, (float)p.Y);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.Point" /> structure to a <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <param name="p">The <see cref="T:System.Drawing.Point" /> to be converted.</param>
		/// <returns>The <see cref="T:System.Drawing.Size" /> that results from the conversion.</returns>
		// Token: 0x06000240 RID: 576 RVA: 0x000070C1 File Offset: 0x000052C1
		public static explicit operator Size(Point p)
		{
			return new Size(p.X, p.Y);
		}

		/// <summary>Translates a <see cref="T:System.Drawing.Point" /> by a given <see cref="T:System.Drawing.Size" />.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to translate.</param>
		/// <param name="sz">A <see cref="T:System.Drawing.Size" /> that specifies the pair of numbers to add to the coordinates of <paramref name="pt" />.</param>
		/// <returns>The translated <see cref="T:System.Drawing.Point" />.</returns>
		// Token: 0x06000241 RID: 577 RVA: 0x000070D6 File Offset: 0x000052D6
		public static Point operator +(Point pt, Size sz)
		{
			return Point.Add(pt, sz);
		}

		/// <summary>Translates a <see cref="T:System.Drawing.Point" /> by the negative of a given <see cref="T:System.Drawing.Size" />.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to translate.</param>
		/// <param name="sz">A <see cref="T:System.Drawing.Size" /> that specifies the pair of numbers to subtract from the coordinates of <paramref name="pt" />.</param>
		/// <returns>A <see cref="T:System.Drawing.Point" /> structure that is translated by the negative of a given <see cref="T:System.Drawing.Size" /> structure.</returns>
		// Token: 0x06000242 RID: 578 RVA: 0x000070DF File Offset: 0x000052DF
		public static Point operator -(Point pt, Size sz)
		{
			return Point.Subtract(pt, sz);
		}

		/// <summary>Compares two <see cref="T:System.Drawing.Point" /> objects. The result specifies whether the values of the <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> properties of the two <see cref="T:System.Drawing.Point" /> objects are equal.</summary>
		/// <param name="left">A <see cref="T:System.Drawing.Point" /> to compare.</param>
		/// <param name="right">A <see cref="T:System.Drawing.Point" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> values of <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000243 RID: 579 RVA: 0x000070E8 File Offset: 0x000052E8
		public static bool operator ==(Point left, Point right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		/// <summary>Compares two <see cref="T:System.Drawing.Point" /> objects. The result specifies whether the values of the <see cref="P:System.Drawing.Point.X" /> or <see cref="P:System.Drawing.Point.Y" /> properties of the two <see cref="T:System.Drawing.Point" /> objects are unequal.</summary>
		/// <param name="left">A <see cref="T:System.Drawing.Point" /> to compare.</param>
		/// <param name="right">A <see cref="T:System.Drawing.Point" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the values of either the <see cref="P:System.Drawing.Point.X" /> properties or the <see cref="P:System.Drawing.Point.Y" /> properties of <paramref name="left" /> and <paramref name="right" /> differ; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000244 RID: 580 RVA: 0x0000710C File Offset: 0x0000530C
		public static bool operator !=(Point left, Point right)
		{
			return !(left == right);
		}

		/// <summary>Adds the specified <see cref="T:System.Drawing.Size" /> to the specified <see cref="T:System.Drawing.Point" />.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to add.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.Size" /> to add</param>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that is the result of the addition operation.</returns>
		// Token: 0x06000245 RID: 581 RVA: 0x00007118 File Offset: 0x00005318
		public static Point Add(Point pt, Size sz)
		{
			return new Point(pt.X + sz.Width, pt.Y + sz.Height);
		}

		/// <summary>Returns the result of subtracting specified <see cref="T:System.Drawing.Size" /> from the specified <see cref="T:System.Drawing.Point" />.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to be subtracted from.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.Size" /> to subtract from the <see cref="T:System.Drawing.Point" />.</param>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that is the result of the subtraction operation.</returns>
		// Token: 0x06000246 RID: 582 RVA: 0x0000713D File Offset: 0x0000533D
		public static Point Subtract(Point pt, Size sz)
		{
			return new Point(pt.X - sz.Width, pt.Y - sz.Height);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.PointF" /> to a <see cref="T:System.Drawing.Point" /> by rounding the values of the <see cref="T:System.Drawing.PointF" /> to the next higher integer values.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.PointF" /> to convert.</param>
		/// <returns>The <see cref="T:System.Drawing.Point" /> this method converts to.</returns>
		// Token: 0x06000247 RID: 583 RVA: 0x00007162 File Offset: 0x00005362
		public static Point Ceiling(PointF value)
		{
			return new Point((int)Math.Ceiling((double)value.X), (int)Math.Ceiling((double)value.Y));
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.PointF" /> to a <see cref="T:System.Drawing.Point" /> by truncating the values of the <see cref="T:System.Drawing.Point" />.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.PointF" /> to convert.</param>
		/// <returns>The <see cref="T:System.Drawing.Point" /> this method converts to.</returns>
		// Token: 0x06000248 RID: 584 RVA: 0x00007185 File Offset: 0x00005385
		public static Point Truncate(PointF value)
		{
			return new Point((int)value.X, (int)value.Y);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.PointF" /> to a <see cref="T:System.Drawing.Point" /> object by rounding the <see cref="T:System.Drawing.Point" /> values to the nearest integer.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.PointF" /> to convert.</param>
		/// <returns>The <see cref="T:System.Drawing.Point" /> this method converts to.</returns>
		// Token: 0x06000249 RID: 585 RVA: 0x0000719C File Offset: 0x0000539C
		public static Point Round(PointF value)
		{
			return new Point((int)Math.Round((double)value.X), (int)Math.Round((double)value.Y));
		}

		/// <summary>Specifies whether this <see cref="T:System.Drawing.Point" /> contains the same coordinates as the specified <see cref="T:System.Object" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.Point" /> and has the same coordinates as this <see cref="T:System.Drawing.Point" />.</returns>
		// Token: 0x0600024A RID: 586 RVA: 0x000071BF File Offset: 0x000053BF
		public override bool Equals(object obj)
		{
			return obj is Point && this.Equals((Point)obj);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000071D7 File Offset: 0x000053D7
		public bool Equals(Point other)
		{
			return this == other;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.Point" />.</returns>
		// Token: 0x0600024C RID: 588 RVA: 0x000071E5 File Offset: 0x000053E5
		public override int GetHashCode()
		{
			return HashHelpers.Combine(this.X, this.Y);
		}

		/// <summary>Translates this <see cref="T:System.Drawing.Point" /> by the specified amount.</summary>
		/// <param name="dx">The amount to offset the x-coordinate.</param>
		/// <param name="dy">The amount to offset the y-coordinate.</param>
		// Token: 0x0600024D RID: 589 RVA: 0x000071F8 File Offset: 0x000053F8
		public void Offset(int dx, int dy)
		{
			this.X += dx;
			this.Y += dy;
		}

		/// <summary>Translates this <see cref="T:System.Drawing.Point" /> by the specified <see cref="T:System.Drawing.Point" />.</summary>
		/// <param name="p">The <see cref="T:System.Drawing.Point" /> used offset this <see cref="T:System.Drawing.Point" />.</param>
		// Token: 0x0600024E RID: 590 RVA: 0x00007216 File Offset: 0x00005416
		public void Offset(Point p)
		{
			this.Offset(p.X, p.Y);
		}

		/// <summary>Converts this <see cref="T:System.Drawing.Point" /> to a human-readable string.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.Point" />.</returns>
		// Token: 0x0600024F RID: 591 RVA: 0x0000722C File Offset: 0x0000542C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{X=",
				this.X.ToString(),
				",Y=",
				this.Y.ToString(),
				"}"
			});
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000727E File Offset: 0x0000547E
		private static short HighInt16(int n)
		{
			return (short)(n >> 16 & 65535);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000728B File Offset: 0x0000548B
		private static short LowInt16(int n)
		{
			return (short)(n & 65535);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000049FE File Offset: 0x00002BFE
		// Note: this type is marked as 'beforefieldinit'.
		static Point()
		{
		}

		/// <summary>Represents a <see cref="T:System.Drawing.Point" /> that has <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> values set to zero.</summary>
		// Token: 0x0400038B RID: 907
		public static readonly Point Empty;

		// Token: 0x0400038C RID: 908
		private int x;

		// Token: 0x0400038D RID: 909
		private int y;
	}
}
