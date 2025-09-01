using System;
using System.ComponentModel;
using System.Numerics.Hashing;

namespace System.Drawing
{
	/// <summary>Stores an ordered pair of integers, which specify a <see cref="P:System.Drawing.Size.Height" /> and <see cref="P:System.Drawing.Size.Width" />.</summary>
	// Token: 0x0200004B RID: 75
	[TypeConverter(typeof(SizeConverter))]
	[Serializable]
	public struct Size : IEquatable<Size>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Size" /> structure from the specified <see cref="T:System.Drawing.Point" /> structure.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> structure from which to initialize this <see cref="T:System.Drawing.Size" /> structure.</param>
		// Token: 0x060002BA RID: 698 RVA: 0x00008109 File Offset: 0x00006309
		public Size(Point pt)
		{
			this.width = pt.X;
			this.height = pt.Y;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Size" /> structure from the specified dimensions.</summary>
		/// <param name="width">The width component of the new <see cref="T:System.Drawing.Size" />.</param>
		/// <param name="height">The height component of the new <see cref="T:System.Drawing.Size" />.</param>
		// Token: 0x060002BB RID: 699 RVA: 0x00008125 File Offset: 0x00006325
		public Size(int width, int height)
		{
			this.width = width;
			this.height = height;
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.Size" /> structure to a <see cref="T:System.Drawing.SizeF" /> structure.</summary>
		/// <param name="p">The <see cref="T:System.Drawing.Size" /> structure to convert.</param>
		/// <returns>The <see cref="T:System.Drawing.SizeF" /> structure to which this operator converts.</returns>
		// Token: 0x060002BC RID: 700 RVA: 0x00008135 File Offset: 0x00006335
		public static implicit operator SizeF(Size p)
		{
			return new SizeF((float)p.Width, (float)p.Height);
		}

		/// <summary>Adds the width and height of one <see cref="T:System.Drawing.Size" /> structure to the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <param name="sz1">The first <see cref="T:System.Drawing.Size" /> to add.</param>
		/// <param name="sz2">The second <see cref="T:System.Drawing.Size" /> to add.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the addition operation.</returns>
		// Token: 0x060002BD RID: 701 RVA: 0x0000814C File Offset: 0x0000634C
		public static Size operator +(Size sz1, Size sz2)
		{
			return Size.Add(sz1, sz2);
		}

		/// <summary>Subtracts the width and height of one <see cref="T:System.Drawing.Size" /> structure from the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left side of the subtraction operator.</param>
		/// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right side of the subtraction operator.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the subtraction operation.</returns>
		// Token: 0x060002BE RID: 702 RVA: 0x00008155 File Offset: 0x00006355
		public static Size operator -(Size sz1, Size sz2)
		{
			return Size.Subtract(sz1, sz2);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000815E File Offset: 0x0000635E
		public static Size operator *(int left, Size right)
		{
			return Size.Multiply(right, left);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00008167 File Offset: 0x00006367
		public static Size operator *(Size left, int right)
		{
			return Size.Multiply(left, right);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00008170 File Offset: 0x00006370
		public static Size operator /(Size left, int right)
		{
			return new Size(left.width / right, left.height / right);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00008187 File Offset: 0x00006387
		public static SizeF operator *(float left, Size right)
		{
			return Size.Multiply(right, left);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00008190 File Offset: 0x00006390
		public static SizeF operator *(Size left, float right)
		{
			return Size.Multiply(left, right);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00008199 File Offset: 0x00006399
		public static SizeF operator /(Size left, float right)
		{
			return new SizeF((float)left.width / right, (float)left.height / right);
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.Size" /> structures are equal.</summary>
		/// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left side of the equality operator.</param>
		/// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right of the equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="sz1" /> and <paramref name="sz2" /> have equal width and height; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002C5 RID: 709 RVA: 0x000081B2 File Offset: 0x000063B2
		public static bool operator ==(Size sz1, Size sz2)
		{
			return sz1.Width == sz2.Width && sz1.Height == sz2.Height;
		}

		/// <summary>Tests whether two <see cref="T:System.Drawing.Size" /> structures are different.</summary>
		/// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left of the inequality operator.</param>
		/// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right of the inequality operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="sz1" /> and <paramref name="sz2" /> differ either in width or height; <see langword="false" /> if <paramref name="sz1" /> and <paramref name="sz2" /> are equal.</returns>
		// Token: 0x060002C6 RID: 710 RVA: 0x000081D6 File Offset: 0x000063D6
		public static bool operator !=(Size sz1, Size sz2)
		{
			return !(sz1 == sz2);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.Size" /> structure to a <see cref="T:System.Drawing.Point" /> structure.</summary>
		/// <param name="size">The <see cref="T:System.Drawing.Size" /> structure to convert.</param>
		/// <returns>The <see cref="T:System.Drawing.Point" /> structure to which this operator converts.</returns>
		// Token: 0x060002C7 RID: 711 RVA: 0x000081E2 File Offset: 0x000063E2
		public static explicit operator Point(Size size)
		{
			return new Point(size.Width, size.Height);
		}

		/// <summary>Tests whether this <see cref="T:System.Drawing.Size" /> structure has width and height of 0.</summary>
		/// <returns>This property returns <see langword="true" /> when this <see cref="T:System.Drawing.Size" /> structure has both a width and height of 0; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x000081F7 File Offset: 0x000063F7
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.width == 0 && this.height == 0;
			}
		}

		/// <summary>Gets or sets the horizontal component of this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>The horizontal component of this <see cref="T:System.Drawing.Size" /> structure, typically measured in pixels.</returns>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000820C File Offset: 0x0000640C
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00008214 File Offset: 0x00006414
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

		/// <summary>Gets or sets the vertical component of this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>The vertical component of this <see cref="T:System.Drawing.Size" /> structure, typically measured in pixels.</returns>
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000821D File Offset: 0x0000641D
		// (set) Token: 0x060002CC RID: 716 RVA: 0x00008225 File Offset: 0x00006425
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

		/// <summary>Adds the width and height of one <see cref="T:System.Drawing.Size" /> structure to the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <param name="sz1">The first <see cref="T:System.Drawing.Size" /> structure to add.</param>
		/// <param name="sz2">The second <see cref="T:System.Drawing.Size" /> structure to add.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the addition operation.</returns>
		// Token: 0x060002CD RID: 717 RVA: 0x0000822E File Offset: 0x0000642E
		public static Size Add(Size sz1, Size sz2)
		{
			return new Size(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure by rounding the values of the <see cref="T:System.Drawing.Size" /> structure to the next higher integer values.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.SizeF" /> structure to convert.</param>
		/// <returns>The <see cref="T:System.Drawing.Size" /> structure this method converts to.</returns>
		// Token: 0x060002CE RID: 718 RVA: 0x00008253 File Offset: 0x00006453
		public static Size Ceiling(SizeF value)
		{
			return new Size((int)Math.Ceiling((double)value.Width), (int)Math.Ceiling((double)value.Height));
		}

		/// <summary>Subtracts the width and height of one <see cref="T:System.Drawing.Size" /> structure from the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left side of the subtraction operator.</param>
		/// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right side of the subtraction operator.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure that is a result of the subtraction operation.</returns>
		// Token: 0x060002CF RID: 719 RVA: 0x00008276 File Offset: 0x00006476
		public static Size Subtract(Size sz1, Size sz2)
		{
			return new Size(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure by truncating the values of the <see cref="T:System.Drawing.SizeF" /> structure to the next lower integer values.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.SizeF" /> structure to convert.</param>
		/// <returns>The <see cref="T:System.Drawing.Size" /> structure this method converts to.</returns>
		// Token: 0x060002D0 RID: 720 RVA: 0x0000829B File Offset: 0x0000649B
		public static Size Truncate(SizeF value)
		{
			return new Size((int)value.Width, (int)value.Height);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure by rounding the values of the <see cref="T:System.Drawing.SizeF" /> structure to the nearest integer values.</summary>
		/// <param name="value">The <see cref="T:System.Drawing.SizeF" /> structure to convert.</param>
		/// <returns>The <see cref="T:System.Drawing.Size" /> structure this method converts to.</returns>
		// Token: 0x060002D1 RID: 721 RVA: 0x000082B2 File Offset: 0x000064B2
		public static Size Round(SizeF value)
		{
			return new Size((int)Math.Round((double)value.Width), (int)Math.Round((double)value.Height));
		}

		/// <summary>Tests to see whether the specified object is a <see cref="T:System.Drawing.Size" /> structure with the same dimensions as this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.Size" /> and has the same width and height as this <see cref="T:System.Drawing.Size" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002D2 RID: 722 RVA: 0x000082D5 File Offset: 0x000064D5
		public override bool Equals(object obj)
		{
			return obj is Size && this.Equals((Size)obj);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000082ED File Offset: 0x000064ED
		public bool Equals(Size other)
		{
			return this == other;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.Size" /> structure.</returns>
		// Token: 0x060002D4 RID: 724 RVA: 0x000082FB File Offset: 0x000064FB
		public override int GetHashCode()
		{
			return HashHelpers.Combine(this.Width, this.Height);
		}

		/// <summary>Creates a human-readable string that represents this <see cref="T:System.Drawing.Size" /> structure.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.Size" />.</returns>
		// Token: 0x060002D5 RID: 725 RVA: 0x00008310 File Offset: 0x00006510
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

		// Token: 0x060002D6 RID: 726 RVA: 0x0000835C File Offset: 0x0000655C
		private static Size Multiply(Size size, int multiplier)
		{
			return new Size(size.width * multiplier, size.height * multiplier);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00008373 File Offset: 0x00006573
		private static SizeF Multiply(Size size, float multiplier)
		{
			return new SizeF((float)size.width * multiplier, (float)size.height * multiplier);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000049FE File Offset: 0x00002BFE
		// Note: this type is marked as 'beforefieldinit'.
		static Size()
		{
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Size" /> structure that has a <see cref="P:System.Drawing.Size.Height" /> and <see cref="P:System.Drawing.Size.Width" /> value of 0.</summary>
		// Token: 0x0400039B RID: 923
		public static readonly Size Empty;

		// Token: 0x0400039C RID: 924
		private int width;

		// Token: 0x0400039D RID: 925
		private int height;
	}
}
