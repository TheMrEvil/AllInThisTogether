using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Drawing.Printing
{
	/// <summary>Specifies the dimensions of the margins of a printed page.</summary>
	// Token: 0x020000B9 RID: 185
	[TypeConverter(typeof(MarginsConverter))]
	[Serializable]
	public class Margins : ICloneable
	{
		// Token: 0x06000A72 RID: 2674 RVA: 0x00017DC8 File Offset: 0x00015FC8
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			if (this._doubleLeft == 0.0 && this._left != 0)
			{
				this._doubleLeft = (double)this._left;
			}
			if (this._doubleRight == 0.0 && this._right != 0)
			{
				this._doubleRight = (double)this._right;
			}
			if (this._doubleTop == 0.0 && this._top != 0)
			{
				this._doubleTop = (double)this._top;
			}
			if (this._doubleBottom == 0.0 && this._bottom != 0)
			{
				this._doubleBottom = (double)this._bottom;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.Margins" /> class with 1-inch wide margins.</summary>
		// Token: 0x06000A73 RID: 2675 RVA: 0x00017E6D File Offset: 0x0001606D
		public Margins() : this(100, 100, 100, 100)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.Margins" /> class with the specified left, right, top, and bottom margins.</summary>
		/// <param name="left">The left margin, in hundredths of an inch.</param>
		/// <param name="right">The right margin, in hundredths of an inch.</param>
		/// <param name="top">The top margin, in hundredths of an inch.</param>
		/// <param name="bottom">The bottom margin, in hundredths of an inch.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="left" /> parameter value is less than 0.  
		///  -or-  
		///  The <paramref name="right" /> parameter value is less than 0.  
		///  -or-  
		///  The <paramref name="top" /> parameter value is less than 0.  
		///  -or-  
		///  The <paramref name="bottom" /> parameter value is less than 0.</exception>
		// Token: 0x06000A74 RID: 2676 RVA: 0x00017E80 File Offset: 0x00016080
		public Margins(int left, int right, int top, int bottom)
		{
			this.CheckMargin(left, "left");
			this.CheckMargin(right, "right");
			this.CheckMargin(top, "top");
			this.CheckMargin(bottom, "bottom");
			this._left = left;
			this._right = right;
			this._top = top;
			this._bottom = bottom;
			this._doubleLeft = (double)left;
			this._doubleRight = (double)right;
			this._doubleTop = (double)top;
			this._doubleBottom = (double)bottom;
		}

		/// <summary>Gets or sets the left margin width, in hundredths of an inch.</summary>
		/// <returns>The left margin width, in hundredths of an inch.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.Margins.Left" /> property is set to a value that is less than 0.</exception>
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00017F02 File Offset: 0x00016102
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x00017F0A File Offset: 0x0001610A
		public int Left
		{
			get
			{
				return this._left;
			}
			set
			{
				this.CheckMargin(value, "Left");
				this._left = value;
				this._doubleLeft = (double)value;
			}
		}

		/// <summary>Gets or sets the right margin width, in hundredths of an inch.</summary>
		/// <returns>The right margin width, in hundredths of an inch.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.Margins.Right" /> property is set to a value that is less than 0.</exception>
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00017F27 File Offset: 0x00016127
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x00017F2F File Offset: 0x0001612F
		public int Right
		{
			get
			{
				return this._right;
			}
			set
			{
				this.CheckMargin(value, "Right");
				this._right = value;
				this._doubleRight = (double)value;
			}
		}

		/// <summary>Gets or sets the top margin width, in hundredths of an inch.</summary>
		/// <returns>The top margin width, in hundredths of an inch.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.Margins.Top" /> property is set to a value that is less than 0.</exception>
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00017F4C File Offset: 0x0001614C
		// (set) Token: 0x06000A7A RID: 2682 RVA: 0x00017F54 File Offset: 0x00016154
		public int Top
		{
			get
			{
				return this._top;
			}
			set
			{
				this.CheckMargin(value, "Top");
				this._top = value;
				this._doubleTop = (double)value;
			}
		}

		/// <summary>Gets or sets the bottom margin, in hundredths of an inch.</summary>
		/// <returns>The bottom margin, in hundredths of an inch.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.Margins.Bottom" /> property is set to a value that is less than 0.</exception>
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00017F71 File Offset: 0x00016171
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x00017F79 File Offset: 0x00016179
		public int Bottom
		{
			get
			{
				return this._bottom;
			}
			set
			{
				this.CheckMargin(value, "Bottom");
				this._bottom = value;
				this._doubleBottom = (double)value;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00017F96 File Offset: 0x00016196
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x00017F9E File Offset: 0x0001619E
		internal double DoubleLeft
		{
			get
			{
				return this._doubleLeft;
			}
			set
			{
				this.Left = (int)Math.Round(value);
				this._doubleLeft = value;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00017FB4 File Offset: 0x000161B4
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x00017FBC File Offset: 0x000161BC
		internal double DoubleRight
		{
			get
			{
				return this._doubleRight;
			}
			set
			{
				this.Right = (int)Math.Round(value);
				this._doubleRight = value;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00017FD2 File Offset: 0x000161D2
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x00017FDA File Offset: 0x000161DA
		internal double DoubleTop
		{
			get
			{
				return this._doubleTop;
			}
			set
			{
				this.Top = (int)Math.Round(value);
				this._doubleTop = value;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00017FF0 File Offset: 0x000161F0
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x00017FF8 File Offset: 0x000161F8
		internal double DoubleBottom
		{
			get
			{
				return this._doubleBottom;
			}
			set
			{
				this.Bottom = (int)Math.Round(value);
				this._doubleBottom = value;
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001800E File Offset: 0x0001620E
		private void CheckMargin(int margin, string name)
		{
			if (margin < 0)
			{
				throw new ArgumentException(SR.Format("Value of '{1}' is not valid for '{0}'. '{0}' must be greater than or equal to {2}.", new object[]
				{
					name,
					margin,
					"0"
				}));
			}
		}

		/// <summary>Retrieves a duplicate of this object, member by member.</summary>
		/// <returns>A duplicate of this object.</returns>
		// Token: 0x06000A86 RID: 2694 RVA: 0x0001803F File Offset: 0x0001623F
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Compares this <see cref="T:System.Drawing.Printing.Margins" /> to the specified <see cref="T:System.Object" /> to determine whether they have the same dimensions.</summary>
		/// <param name="obj">The object to which to compare this <see cref="T:System.Drawing.Printing.Margins" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is a <see cref="T:System.Drawing.Printing.Margins" /> and has the same <see cref="P:System.Drawing.Printing.Margins.Top" />, <see cref="P:System.Drawing.Printing.Margins.Bottom" />, <see cref="P:System.Drawing.Printing.Margins.Right" /> and <see cref="P:System.Drawing.Printing.Margins.Left" /> values as this <see cref="T:System.Drawing.Printing.Margins" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A87 RID: 2695 RVA: 0x00018048 File Offset: 0x00016248
		public override bool Equals(object obj)
		{
			Margins margins = obj as Margins;
			return margins == this || (!(margins == null) && (margins.Left == this.Left && margins.Right == this.Right && margins.Top == this.Top) && margins.Bottom == this.Bottom);
		}

		/// <summary>Calculates and retrieves a hash code based on the width of the left, right, top, and bottom margins.</summary>
		/// <returns>A hash code based on the left, right, top, and bottom margins.</returns>
		// Token: 0x06000A88 RID: 2696 RVA: 0x000180AC File Offset: 0x000162AC
		public override int GetHashCode()
		{
			int left = this.Left;
			uint right = (uint)this.Right;
			uint top = (uint)this.Top;
			uint bottom = (uint)this.Bottom;
			return left ^ (int)(right << 13 | right >> 19) ^ (int)(top << 26 | top >> 6) ^ (int)(bottom << 7 | bottom >> 25);
		}

		/// <summary>Compares two <see cref="T:System.Drawing.Printing.Margins" /> to determine if they have the same dimensions.</summary>
		/// <param name="m1">The first <see cref="T:System.Drawing.Printing.Margins" /> to compare for equality.</param>
		/// <param name="m2">The second <see cref="T:System.Drawing.Printing.Margins" /> to compare for equality.</param>
		/// <returns>
		///   <see langword="true" /> to indicate the <see cref="P:System.Drawing.Printing.Margins.Left" />, <see cref="P:System.Drawing.Printing.Margins.Right" />, <see cref="P:System.Drawing.Printing.Margins.Top" />, and <see cref="P:System.Drawing.Printing.Margins.Bottom" /> properties of both margins have the same value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A89 RID: 2697 RVA: 0x000180F0 File Offset: 0x000162F0
		public static bool operator ==(Margins m1, Margins m2)
		{
			return m1 == null == (m2 == null) && (m1 == null || (m1.Left == m2.Left && m1.Top == m2.Top && m1.Right == m2.Right && m1.Bottom == m2.Bottom));
		}

		/// <summary>Compares two <see cref="T:System.Drawing.Printing.Margins" /> to determine whether they are of unequal width.</summary>
		/// <param name="m1">The first <see cref="T:System.Drawing.Printing.Margins" /> to compare for inequality.</param>
		/// <param name="m2">The second <see cref="T:System.Drawing.Printing.Margins" /> to compare for inequality.</param>
		/// <returns>
		///   <see langword="true" /> to indicate if the <see cref="P:System.Drawing.Printing.Margins.Left" />, <see cref="P:System.Drawing.Printing.Margins.Right" />, <see cref="P:System.Drawing.Printing.Margins.Top" />, or <see cref="P:System.Drawing.Printing.Margins.Bottom" /> properties of both margins are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A8A RID: 2698 RVA: 0x00018148 File Offset: 0x00016348
		public static bool operator !=(Margins m1, Margins m2)
		{
			return !(m1 == m2);
		}

		/// <summary>Converts the <see cref="T:System.Drawing.Printing.Margins" /> to a string.</summary>
		/// <returns>A <see cref="T:System.String" /> representation of the <see cref="T:System.Drawing.Printing.Margins" />.</returns>
		// Token: 0x06000A8B RID: 2699 RVA: 0x00018154 File Offset: 0x00016354
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[Margins Left=",
				this.Left.ToString(CultureInfo.InvariantCulture),
				" Right=",
				this.Right.ToString(CultureInfo.InvariantCulture),
				" Top=",
				this.Top.ToString(CultureInfo.InvariantCulture),
				" Bottom=",
				this.Bottom.ToString(CultureInfo.InvariantCulture),
				"]"
			});
		}

		// Token: 0x04000663 RID: 1635
		private int _left;

		// Token: 0x04000664 RID: 1636
		private int _right;

		// Token: 0x04000665 RID: 1637
		private int _bottom;

		// Token: 0x04000666 RID: 1638
		private int _top;

		// Token: 0x04000667 RID: 1639
		[OptionalField]
		private double _doubleLeft;

		// Token: 0x04000668 RID: 1640
		[OptionalField]
		private double _doubleRight;

		// Token: 0x04000669 RID: 1641
		[OptionalField]
		private double _doubleTop;

		// Token: 0x0400066A RID: 1642
		[OptionalField]
		private double _doubleBottom;
	}
}
