using System;
using System.ComponentModel;
using System.Numerics.Hashing;

namespace System.Drawing
{
	/// <summary>Represents an ordered pair of floating-point x- and y-coordinates that defines a point in a two-dimensional plane.</summary>
	// Token: 0x02000048 RID: 72
	[Serializable]
	public struct PointF : IEquatable<PointF>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.PointF" /> class with the specified coordinates.</summary>
		/// <param name="x">The horizontal position of the point.</param>
		/// <param name="y">The vertical position of the point.</param>
		// Token: 0x06000253 RID: 595 RVA: 0x00007295 File Offset: 0x00005495
		public PointF(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.PointF" /> is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if both <see cref="P:System.Drawing.PointF.X" /> and <see cref="P:System.Drawing.PointF.Y" /> are 0; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000254 RID: 596 RVA: 0x000072A5 File Offset: 0x000054A5
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.x == 0f && this.y == 0f;
			}
		}

		/// <summary>Gets or sets the x-coordinate of this <see cref="T:System.Drawing.PointF" />.</summary>
		/// <returns>The x-coordinate of this <see cref="T:System.Drawing.PointF" />.</returns>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000255 RID: 597 RVA: 0x000072C3 File Offset: 0x000054C3
		// (set) Token: 0x06000256 RID: 598 RVA: 0x000072CB File Offset: 0x000054CB
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

		/// <summary>Gets or sets the y-coordinate of this <see cref="T:System.Drawing.PointF" />.</summary>
		/// <returns>The y-coordinate of this <see cref="T:System.Drawing.PointF" />.</returns>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000257 RID: 599 RVA: 0x000072D4 File Offset: 0x000054D4
		// (set) Token: 0x06000258 RID: 600 RVA: 0x000072DC File Offset: 0x000054DC
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

		/// <summary>Translates a <see cref="T:System.Drawing.PointF" /> by a given <see cref="T:System.Drawing.Size" />.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
		/// <param name="sz">A <see cref="T:System.Drawing.Size" /> that specifies the pair of numbers to add to the coordinates of <paramref name="pt" />.</param>
		/// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
		// Token: 0x06000259 RID: 601 RVA: 0x000072E5 File Offset: 0x000054E5
		public static PointF operator +(PointF pt, Size sz)
		{
			return PointF.Add(pt, sz);
		}

		/// <summary>Translates a <see cref="T:System.Drawing.PointF" /> by the negative of a given <see cref="T:System.Drawing.Size" />.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.Size" /> that specifies the numbers to subtract from the coordinates of <paramref name="pt" />.</param>
		/// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
		// Token: 0x0600025A RID: 602 RVA: 0x000072EE File Offset: 0x000054EE
		public static PointF operator -(PointF pt, Size sz)
		{
			return PointF.Subtract(pt, sz);
		}

		/// <summary>Translates the <see cref="T:System.Drawing.PointF" /> by the specified <see cref="T:System.Drawing.SizeF" />.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.SizeF" /> that specifies the numbers to add to the x- and y-coordinates of the <see cref="T:System.Drawing.PointF" />.</param>
		/// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
		// Token: 0x0600025B RID: 603 RVA: 0x000072F7 File Offset: 0x000054F7
		public static PointF operator +(PointF pt, SizeF sz)
		{
			return PointF.Add(pt, sz);
		}

		/// <summary>Translates a <see cref="T:System.Drawing.PointF" /> by the negative of a specified <see cref="T:System.Drawing.SizeF" />.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.SizeF" /> that specifies the numbers to subtract from the coordinates of <paramref name="pt" />.</param>
		/// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
		// Token: 0x0600025C RID: 604 RVA: 0x00007300 File Offset: 0x00005500
		public static PointF operator -(PointF pt, SizeF sz)
		{
			return PointF.Subtract(pt, sz);
		}

		/// <summary>Compares two <see cref="T:System.Drawing.PointF" /> structures. The result specifies whether the values of the <see cref="P:System.Drawing.PointF.X" /> and <see cref="P:System.Drawing.PointF.Y" /> properties of the two <see cref="T:System.Drawing.PointF" /> structures are equal.</summary>
		/// <param name="left">A <see cref="T:System.Drawing.PointF" /> to compare.</param>
		/// <param name="right">A <see cref="T:System.Drawing.PointF" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Drawing.PointF.X" /> and <see cref="P:System.Drawing.PointF.Y" /> values of the left and right <see cref="T:System.Drawing.PointF" /> structures are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600025D RID: 605 RVA: 0x00007309 File Offset: 0x00005509
		public static bool operator ==(PointF left, PointF right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		/// <summary>Determines whether the coordinates of the specified points are not equal.</summary>
		/// <param name="left">A <see cref="T:System.Drawing.PointF" /> to compare.</param>
		/// <param name="right">A <see cref="T:System.Drawing.PointF" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> to indicate the <see cref="P:System.Drawing.PointF.X" /> and <see cref="P:System.Drawing.PointF.Y" /> values of <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600025E RID: 606 RVA: 0x0000732D File Offset: 0x0000552D
		public static bool operator !=(PointF left, PointF right)
		{
			return !(left == right);
		}

		/// <summary>Translates a given <see cref="T:System.Drawing.PointF" /> by the specified <see cref="T:System.Drawing.Size" />.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.Size" /> that specifies the numbers to add to the coordinates of <paramref name="pt" />.</param>
		/// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
		// Token: 0x0600025F RID: 607 RVA: 0x00007339 File Offset: 0x00005539
		public static PointF Add(PointF pt, Size sz)
		{
			return new PointF(pt.X + (float)sz.Width, pt.Y + (float)sz.Height);
		}

		/// <summary>Translates a <see cref="T:System.Drawing.PointF" /> by the negative of a specified size.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.Size" /> that specifies the numbers to subtract from the coordinates of <paramref name="pt" />.</param>
		/// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
		// Token: 0x06000260 RID: 608 RVA: 0x00007360 File Offset: 0x00005560
		public static PointF Subtract(PointF pt, Size sz)
		{
			return new PointF(pt.X - (float)sz.Width, pt.Y - (float)sz.Height);
		}

		/// <summary>Translates a given <see cref="T:System.Drawing.PointF" /> by a specified <see cref="T:System.Drawing.SizeF" />.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.SizeF" /> that specifies the numbers to add to the coordinates of <paramref name="pt" />.</param>
		/// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
		// Token: 0x06000261 RID: 609 RVA: 0x00007387 File Offset: 0x00005587
		public static PointF Add(PointF pt, SizeF sz)
		{
			return new PointF(pt.X + sz.Width, pt.Y + sz.Height);
		}

		/// <summary>Translates a <see cref="T:System.Drawing.PointF" /> by the negative of a specified size.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
		/// <param name="sz">The <see cref="T:System.Drawing.SizeF" /> that specifies the numbers to subtract from the coordinates of <paramref name="pt" />.</param>
		/// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
		// Token: 0x06000262 RID: 610 RVA: 0x000073AC File Offset: 0x000055AC
		public static PointF Subtract(PointF pt, SizeF sz)
		{
			return new PointF(pt.X - sz.Width, pt.Y - sz.Height);
		}

		/// <summary>Specifies whether this <see cref="T:System.Drawing.PointF" /> contains the same coordinates as the specified <see cref="T:System.Object" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to test.</param>
		/// <returns>This method returns <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.PointF" /> and has the same coordinates as this <see cref="T:System.Drawing.Point" />.</returns>
		// Token: 0x06000263 RID: 611 RVA: 0x000073D1 File Offset: 0x000055D1
		public override bool Equals(object obj)
		{
			return obj is PointF && this.Equals((PointF)obj);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000073E9 File Offset: 0x000055E9
		public bool Equals(PointF other)
		{
			return this == other;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.PointF" /> structure.</summary>
		/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.PointF" /> structure.</returns>
		// Token: 0x06000265 RID: 613 RVA: 0x000073F8 File Offset: 0x000055F8
		public override int GetHashCode()
		{
			return HashHelpers.Combine(this.X.GetHashCode(), this.Y.GetHashCode());
		}

		/// <summary>Converts this <see cref="T:System.Drawing.PointF" /> to a human readable string.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.PointF" />.</returns>
		// Token: 0x06000266 RID: 614 RVA: 0x00007428 File Offset: 0x00005628
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{X=",
				this.x.ToString(),
				", Y=",
				this.y.ToString(),
				"}"
			});
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000049FE File Offset: 0x00002BFE
		// Note: this type is marked as 'beforefieldinit'.
		static PointF()
		{
		}

		/// <summary>Represents a new instance of the <see cref="T:System.Drawing.PointF" /> class with member data left uninitialized.</summary>
		// Token: 0x0400038E RID: 910
		public static readonly PointF Empty;

		// Token: 0x0400038F RID: 911
		private float x;

		// Token: 0x04000390 RID: 912
		private float y;
	}
}
