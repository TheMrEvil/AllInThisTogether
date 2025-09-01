using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the different patterns available for <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> objects.</summary>
	// Token: 0x02000143 RID: 323
	public enum HatchStyle
	{
		/// <summary>A pattern of horizontal lines.</summary>
		// Token: 0x04000AE4 RID: 2788
		Horizontal,
		/// <summary>A pattern of vertical lines.</summary>
		// Token: 0x04000AE5 RID: 2789
		Vertical,
		/// <summary>A pattern of lines on a diagonal from upper left to lower right.</summary>
		// Token: 0x04000AE6 RID: 2790
		ForwardDiagonal,
		/// <summary>A pattern of lines on a diagonal from upper right to lower left.</summary>
		// Token: 0x04000AE7 RID: 2791
		BackwardDiagonal,
		/// <summary>Specifies horizontal and vertical lines that cross.</summary>
		// Token: 0x04000AE8 RID: 2792
		Cross,
		/// <summary>A pattern of crisscross diagonal lines.</summary>
		// Token: 0x04000AE9 RID: 2793
		DiagonalCross,
		/// <summary>Specifies a 5-percent hatch. The ratio of foreground color to background color is 5:95.</summary>
		// Token: 0x04000AEA RID: 2794
		Percent05,
		/// <summary>Specifies a 10-percent hatch. The ratio of foreground color to background color is 10:90.</summary>
		// Token: 0x04000AEB RID: 2795
		Percent10,
		/// <summary>Specifies a 20-percent hatch. The ratio of foreground color to background color is 20:80.</summary>
		// Token: 0x04000AEC RID: 2796
		Percent20,
		/// <summary>Specifies a 25-percent hatch. The ratio of foreground color to background color is 25:75.</summary>
		// Token: 0x04000AED RID: 2797
		Percent25,
		/// <summary>Specifies a 30-percent hatch. The ratio of foreground color to background color is 30:70.</summary>
		// Token: 0x04000AEE RID: 2798
		Percent30,
		/// <summary>Specifies a 40-percent hatch. The ratio of foreground color to background color is 40:60.</summary>
		// Token: 0x04000AEF RID: 2799
		Percent40,
		/// <summary>Specifies a 50-percent hatch. The ratio of foreground color to background color is 50:50.</summary>
		// Token: 0x04000AF0 RID: 2800
		Percent50,
		/// <summary>Specifies a 60-percent hatch. The ratio of foreground color to background color is 60:40.</summary>
		// Token: 0x04000AF1 RID: 2801
		Percent60,
		/// <summary>Specifies a 70-percent hatch. The ratio of foreground color to background color is 70:30.</summary>
		// Token: 0x04000AF2 RID: 2802
		Percent70,
		/// <summary>Specifies a 75-percent hatch. The ratio of foreground color to background color is 75:25.</summary>
		// Token: 0x04000AF3 RID: 2803
		Percent75,
		/// <summary>Specifies a 80-percent hatch. The ratio of foreground color to background color is 80:100.</summary>
		// Token: 0x04000AF4 RID: 2804
		Percent80,
		/// <summary>Specifies a 90-percent hatch. The ratio of foreground color to background color is 90:10.</summary>
		// Token: 0x04000AF5 RID: 2805
		Percent90,
		/// <summary>Specifies diagonal lines that slant to the right from top points to bottom points and are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal" />, but are not antialiased.</summary>
		// Token: 0x04000AF6 RID: 2806
		LightDownwardDiagonal,
		/// <summary>Specifies diagonal lines that slant to the left from top points to bottom points and are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal" />, but they are not antialiased.</summary>
		// Token: 0x04000AF7 RID: 2807
		LightUpwardDiagonal,
		/// <summary>Specifies diagonal lines that slant to the right from top points to bottom points, are spaced 50 percent closer together than, and are twice the width of <see cref="F:System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal" />. This hatch pattern is not antialiased.</summary>
		// Token: 0x04000AF8 RID: 2808
		DarkDownwardDiagonal,
		/// <summary>Specifies diagonal lines that slant to the left from top points to bottom points, are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal" />, and are twice its width, but the lines are not antialiased.</summary>
		// Token: 0x04000AF9 RID: 2809
		DarkUpwardDiagonal,
		/// <summary>Specifies diagonal lines that slant to the right from top points to bottom points, have the same spacing as hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal" />, and are triple its width, but are not antialiased.</summary>
		// Token: 0x04000AFA RID: 2810
		WideDownwardDiagonal,
		/// <summary>Specifies diagonal lines that slant to the left from top points to bottom points, have the same spacing as hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal" />, and are triple its width, but are not antialiased.</summary>
		// Token: 0x04000AFB RID: 2811
		WideUpwardDiagonal,
		/// <summary>Specifies vertical lines that are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.Vertical" />.</summary>
		// Token: 0x04000AFC RID: 2812
		LightVertical,
		/// <summary>Specifies horizontal lines that are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" />.</summary>
		// Token: 0x04000AFD RID: 2813
		LightHorizontal,
		/// <summary>Specifies vertical lines that are spaced 75 percent closer together than hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Vertical" /> (or 25 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.LightVertical" />).</summary>
		// Token: 0x04000AFE RID: 2814
		NarrowVertical,
		/// <summary>Specifies horizontal lines that are spaced 75 percent closer together than hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" /> (or 25 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.LightHorizontal" />).</summary>
		// Token: 0x04000AFF RID: 2815
		NarrowHorizontal,
		/// <summary>Specifies vertical lines that are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.Vertical" /> and are twice its width.</summary>
		// Token: 0x04000B00 RID: 2816
		DarkVertical,
		/// <summary>Specifies horizontal lines that are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" /> and are twice the width of <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" />.</summary>
		// Token: 0x04000B01 RID: 2817
		DarkHorizontal,
		/// <summary>Specifies dashed diagonal lines, that slant to the right from top points to bottom points.</summary>
		// Token: 0x04000B02 RID: 2818
		DashedDownwardDiagonal,
		/// <summary>Specifies dashed diagonal lines, that slant to the left from top points to bottom points.</summary>
		// Token: 0x04000B03 RID: 2819
		DashedUpwardDiagonal,
		/// <summary>Specifies dashed horizontal lines.</summary>
		// Token: 0x04000B04 RID: 2820
		DashedHorizontal,
		/// <summary>Specifies dashed vertical lines.</summary>
		// Token: 0x04000B05 RID: 2821
		DashedVertical,
		/// <summary>Specifies a hatch that has the appearance of confetti.</summary>
		// Token: 0x04000B06 RID: 2822
		SmallConfetti,
		/// <summary>Specifies a hatch that has the appearance of confetti, and is composed of larger pieces than <see cref="F:System.Drawing.Drawing2D.HatchStyle.SmallConfetti" />.</summary>
		// Token: 0x04000B07 RID: 2823
		LargeConfetti,
		/// <summary>Specifies horizontal lines that are composed of zigzags.</summary>
		// Token: 0x04000B08 RID: 2824
		ZigZag,
		/// <summary>Specifies horizontal lines that are composed of tildes.</summary>
		// Token: 0x04000B09 RID: 2825
		Wave,
		/// <summary>Specifies a hatch that has the appearance of layered bricks that slant to the left from top points to bottom points.</summary>
		// Token: 0x04000B0A RID: 2826
		DiagonalBrick,
		/// <summary>Specifies a hatch that has the appearance of horizontally layered bricks.</summary>
		// Token: 0x04000B0B RID: 2827
		HorizontalBrick,
		/// <summary>Specifies a hatch that has the appearance of a woven material.</summary>
		// Token: 0x04000B0C RID: 2828
		Weave,
		/// <summary>Specifies a hatch that has the appearance of a plaid material.</summary>
		// Token: 0x04000B0D RID: 2829
		Plaid,
		/// <summary>Specifies a hatch that has the appearance of divots.</summary>
		// Token: 0x04000B0E RID: 2830
		Divot,
		/// <summary>Specifies horizontal and vertical lines, each of which is composed of dots, that cross.</summary>
		// Token: 0x04000B0F RID: 2831
		DottedGrid,
		/// <summary>Specifies forward diagonal and backward diagonal lines, each of which is composed of dots, that cross.</summary>
		// Token: 0x04000B10 RID: 2832
		DottedDiamond,
		/// <summary>Specifies a hatch that has the appearance of diagonally layered shingles that slant to the right from top points to bottom points.</summary>
		// Token: 0x04000B11 RID: 2833
		Shingle,
		/// <summary>Specifies a hatch that has the appearance of a trellis.</summary>
		// Token: 0x04000B12 RID: 2834
		Trellis,
		/// <summary>Specifies a hatch that has the appearance of spheres laid adjacent to one another.</summary>
		// Token: 0x04000B13 RID: 2835
		Sphere,
		/// <summary>Specifies horizontal and vertical lines that cross and are spaced 50 percent closer together than hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Cross" />.</summary>
		// Token: 0x04000B14 RID: 2836
		SmallGrid,
		/// <summary>Specifies a hatch that has the appearance of a checkerboard.</summary>
		// Token: 0x04000B15 RID: 2837
		SmallCheckerBoard,
		/// <summary>Specifies a hatch that has the appearance of a checkerboard with squares that are twice the size of <see cref="F:System.Drawing.Drawing2D.HatchStyle.SmallCheckerBoard" />.</summary>
		// Token: 0x04000B16 RID: 2838
		LargeCheckerBoard,
		/// <summary>Specifies forward diagonal and backward diagonal lines that cross but are not antialiased.</summary>
		// Token: 0x04000B17 RID: 2839
		OutlinedDiamond,
		/// <summary>Specifies a hatch that has the appearance of a checkerboard placed diagonally.</summary>
		// Token: 0x04000B18 RID: 2840
		SolidDiamond,
		/// <summary>Specifies the hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Cross" />.</summary>
		// Token: 0x04000B19 RID: 2841
		LargeGrid = 4,
		/// <summary>Specifies hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" />.</summary>
		// Token: 0x04000B1A RID: 2842
		Min = 0,
		/// <summary>Specifies hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.SolidDiamond" />.</summary>
		// Token: 0x04000B1B RID: 2843
		Max = 4
	}
}
