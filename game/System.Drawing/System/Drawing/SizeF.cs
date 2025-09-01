using System;
using System.ComponentModel;
using System.Numerics.Hashing;

namespace System.Drawing
{
	/// <summary>Stores an ordered pair of floating-point numbers, typically the width and height of a rectangle.</summary>
	// Token: 0x0200004C RID: 76
	[TypeConverter(typeof(SizeFConverter))]
	[Serializable]
	public struct SizeF : IEquatable<SizeF>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.SizeF" /> structure from the specified existing <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <param name="size">The <see cref="T:System.Drawing.SizeF" /> structure from which to create the new <see cref="T:System.Drawing.SizeF" /> structure.</param>
		// Token: 0x060002D9 RID: 729 RVA: 0x0000838C File Offset: 0x0000658C
		public SizeF(SizeF size)
		{
			this.width = size.width;
			this.height = size.height;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.SizeF" /> structure from the specified <see cref="T:System.Drawing.PointF" /> structure.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> structure from which to initialize this <see cref="T:System.Drawing.SizeF" /> structure.</param>
		// Token: 0x060002DA RID: 730 RVA: 0x000083A6 File Offset: 0x000065A6
		public SizeF(PointF pt)
		{
			this.width = pt.X;
			this.height = pt.Y;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.SizeF" /> structure from the specified dimensions.</summary>
		/// <param name="width">The width component of the new <see cref="T:System.Drawing.SizeF" /> structure.</param>
		/// <param name="height">The height component of the new <see cref="T:System.Drawing.SizeF" /> structure.</param>
		// Token: 0x060002DB RID: 731 RVA: 0x000083C2 File Offset: 0x000065C2
		public SizeF(float width, float height)
		{
			this.width = width;
			this.height = height;
		}

		/// <summary>Adds the width and height of one <see cref="T:System.Drawing.SizeF" /> structure to the width and height of another <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <param name="sz1">The first <see cref="T:System.Drawing.SizeF" /> structure to add.</param>
		/// <param name="sz2">The second <see cref="T:System.Drawing.SizeF" /> structure to add.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the addition operation.</returns>
		// Token: 0x060002DC RID: 732 RVA: 0x000083D2 File Offset: 0x000065D2
		public static SizeF operator +(SizeF sz1, SizeF sz2)
		{
			return SizeF.Add(sz1, sz2);
		}

		/// <summary>Subtracts the width and height of one <see cref="T:System.Drawing.SizeF" /> structure from the width and height of another <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left side of the subtraction operator.</param>
		/// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right side of the subtraction operator.</param>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> that is the result of the subtraction operation.</returns>
		// Token: 0x060002DD RID: 733 RVA: 0x000083DB File Offset: 0x000065DB
		public static SizeF operator -(SizeF sz1, SizeF sz2)
		{
			return SizeF.Subtract(sz1, sz2);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000083E4 File Offset: 0x000065E4
		public static SizeF operator *(float left, SizeF right)
		{
			return SizeF.Multiply(right, left);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000083ED File Offset: 0x000065ED
		public static SizeF operator *(SizeF left, float right)
		{
			return SizeF.Multiply(left, right);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000083F6 File Offset: 0x000065F6
		public static SizeF operator /(SizeF left, float right)
		{
			return new SizeF(left.width / right, left.height / right);
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.SizeF" /> structures are equal.</summary>
		/// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left side of the equality operator.</param>
		/// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right of the equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="sz1" /> and <paramref name="sz2" /> have equal width and height; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002E1 RID: 737 RVA: 0x0000840D File Offset: 0x0000660D
		public static bool operator ==(SizeF sz1, SizeF sz2)
		{
			return sz1.Width == sz2.Width && sz1.Height == sz2.Height;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.SizeF" /> structures are different.</summary>
		/// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left of the inequality operator.</param>
		/// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right of the inequality operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="sz1" /> and <paramref name="sz2" /> differ either in width or height; <see langword="false" /> if <paramref name="sz1" /> and <paramref name="sz2" /> are equal.</returns>
		// Token: 0x060002E2 RID: 738 RVA: 0x00008431 File Offset: 0x00006631
		public static bool operator !=(SizeF sz1, SizeF sz2)
		{
			return !(sz1 == sz2);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.PointF" /> structure.</summary>
		/// <param name="size">The <see cref="T:System.Drawing.SizeF" /> structure to be converted</param>
		/// <returns>The <see cref="T:System.Drawing.PointF" /> structure to which this operator converts.</returns>
		// Token: 0x060002E3 RID: 739 RVA: 0x0000843D File Offset: 0x0000663D
		public static explicit operator PointF(SizeF size)
		{
			return new PointF(size.Width, size.Height);
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Drawing.SizeF" /> structure has zero width and height.</summary>
		/// <returns>
		///   <see langword="true" /> when this <see cref="T:System.Drawing.SizeF" /> structure has both a width and height of zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00008452 File Offset: 0x00006652
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.width == 0f && this.height == 0f;
			}
		}

		/// <summary>Gets or sets the horizontal component of this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <returns>The horizontal component of this <see cref="T:System.Drawing.SizeF" /> structure, typically measured in pixels.</returns>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00008470 File Offset: 0x00006670
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x00008478 File Offset: 0x00006678
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

		/// <summary>Gets or sets the vertical component of this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <returns>The vertical component of this <see cref="T:System.Drawing.SizeF" /> structure, typically measured in pixels.</returns>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00008481 File Offset: 0x00006681
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x00008489 File Offset: 0x00006689
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

		/// <summary>Adds the width and height of one <see cref="T:System.Drawing.SizeF" /> structure to the width and height of another <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <param name="sz1">The first <see cref="T:System.Drawing.SizeF" /> structure to add.</param>
		/// <param name="sz2">The second <see cref="T:System.Drawing.SizeF" /> structure to add.</param>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> structure that is the result of the addition operation.</returns>
		// Token: 0x060002E9 RID: 745 RVA: 0x00008492 File Offset: 0x00006692
		public static SizeF Add(SizeF sz1, SizeF sz2)
		{
			return new SizeF(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		}

		/// <summary>Subtracts the width and height of one <see cref="T:System.Drawing.SizeF" /> structure from the width and height of another <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left side of the subtraction operator.</param>
		/// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right side of the subtraction operator.</param>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> structure that is a result of the subtraction operation.</returns>
		// Token: 0x060002EA RID: 746 RVA: 0x000084B7 File Offset: 0x000066B7
		public static SizeF Subtract(SizeF sz1, SizeF sz2)
		{
			return new SizeF(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		}

		/// <summary>Tests to see whether the specified object is a <see cref="T:System.Drawing.SizeF" /> structure with the same dimensions as this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.SizeF" /> and has the same width and height as this <see cref="T:System.Drawing.SizeF" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002EB RID: 747 RVA: 0x000084DC File Offset: 0x000066DC
		public override bool Equals(object obj)
		{
			return obj is SizeF && this.Equals((SizeF)obj);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000084F4 File Offset: 0x000066F4
		public bool Equals(SizeF other)
		{
			return this == other;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.Size" /> structure.</returns>
		// Token: 0x060002ED RID: 749 RVA: 0x00008504 File Offset: 0x00006704
		public override int GetHashCode()
		{
			return HashHelpers.Combine(this.Width.GetHashCode(), this.Height.GetHashCode());
		}

		/// <summary>Converts a <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.PointF" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.PointF" /> structure.</returns>
		// Token: 0x060002EE RID: 750 RVA: 0x00008532 File Offset: 0x00006732
		public PointF ToPointF()
		{
			return (PointF)this;
		}

		/// <summary>Converts a <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure.</returns>
		// Token: 0x060002EF RID: 751 RVA: 0x0000853F File Offset: 0x0000673F
		public Size ToSize()
		{
			return Size.Truncate(this);
		}

		/// <summary>Creates a human-readable string that represents this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.SizeF" /> structure.</returns>
		// Token: 0x060002F0 RID: 752 RVA: 0x0000854C File Offset: 0x0000674C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{Width=",
				this.width.ToString(),
				", Height=",
				this.height.ToString(),
				"}"
			});
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00008598 File Offset: 0x00006798
		private static SizeF Multiply(SizeF size, float multiplier)
		{
			return new SizeF(size.width * multiplier, size.height * multiplier);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000049FE File Offset: 0x00002BFE
		// Note: this type is marked as 'beforefieldinit'.
		static SizeF()
		{
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SizeF" /> structure that has a <see cref="P:System.Drawing.SizeF.Height" /> and <see cref="P:System.Drawing.SizeF.Width" /> value of 0.</summary>
		// Token: 0x0400039E RID: 926
		public static readonly SizeF Empty;

		// Token: 0x0400039F RID: 927
		private float width;

		// Token: 0x040003A0 RID: 928
		private float height;
	}
}
