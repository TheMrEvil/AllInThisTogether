using System;

namespace TMPro
{
	// Token: 0x02000026 RID: 38
	public static class TMP_Compatibility
	{
		// Token: 0x06000139 RID: 313 RVA: 0x0001771C File Offset: 0x0001591C
		public static TextAlignmentOptions ConvertTextAlignmentEnumValues(TextAlignmentOptions oldValue)
		{
			switch (oldValue)
			{
			case (TextAlignmentOptions)0:
				return TextAlignmentOptions.TopLeft;
			case (TextAlignmentOptions)1:
				return TextAlignmentOptions.Top;
			case (TextAlignmentOptions)2:
				return TextAlignmentOptions.TopRight;
			case (TextAlignmentOptions)3:
				return TextAlignmentOptions.TopJustified;
			case (TextAlignmentOptions)4:
				return TextAlignmentOptions.Left;
			case (TextAlignmentOptions)5:
				return TextAlignmentOptions.Center;
			case (TextAlignmentOptions)6:
				return TextAlignmentOptions.Right;
			case (TextAlignmentOptions)7:
				return TextAlignmentOptions.Justified;
			case (TextAlignmentOptions)8:
				return TextAlignmentOptions.BottomLeft;
			case (TextAlignmentOptions)9:
				return TextAlignmentOptions.Bottom;
			case (TextAlignmentOptions)10:
				return TextAlignmentOptions.BottomRight;
			case (TextAlignmentOptions)11:
				return TextAlignmentOptions.BottomJustified;
			case (TextAlignmentOptions)12:
				return TextAlignmentOptions.BaselineLeft;
			case (TextAlignmentOptions)13:
				return TextAlignmentOptions.Baseline;
			case (TextAlignmentOptions)14:
				return TextAlignmentOptions.BaselineRight;
			case (TextAlignmentOptions)15:
				return TextAlignmentOptions.BaselineJustified;
			case (TextAlignmentOptions)16:
				return TextAlignmentOptions.MidlineLeft;
			case (TextAlignmentOptions)17:
				return TextAlignmentOptions.Midline;
			case (TextAlignmentOptions)18:
				return TextAlignmentOptions.MidlineRight;
			case (TextAlignmentOptions)19:
				return TextAlignmentOptions.MidlineJustified;
			case (TextAlignmentOptions)20:
				return TextAlignmentOptions.CaplineLeft;
			case (TextAlignmentOptions)21:
				return TextAlignmentOptions.Capline;
			case (TextAlignmentOptions)22:
				return TextAlignmentOptions.CaplineRight;
			case (TextAlignmentOptions)23:
				return TextAlignmentOptions.CaplineJustified;
			default:
				return TextAlignmentOptions.TopLeft;
			}
		}

		// Token: 0x02000078 RID: 120
		public enum AnchorPositions
		{
			// Token: 0x04000578 RID: 1400
			TopLeft,
			// Token: 0x04000579 RID: 1401
			Top,
			// Token: 0x0400057A RID: 1402
			TopRight,
			// Token: 0x0400057B RID: 1403
			Left,
			// Token: 0x0400057C RID: 1404
			Center,
			// Token: 0x0400057D RID: 1405
			Right,
			// Token: 0x0400057E RID: 1406
			BottomLeft,
			// Token: 0x0400057F RID: 1407
			Bottom,
			// Token: 0x04000580 RID: 1408
			BottomRight,
			// Token: 0x04000581 RID: 1409
			BaseLine,
			// Token: 0x04000582 RID: 1410
			None
		}
	}
}
