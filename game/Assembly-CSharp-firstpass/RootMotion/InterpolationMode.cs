using System;

namespace RootMotion
{
	// Token: 0x020000BB RID: 187
	[Serializable]
	public enum InterpolationMode
	{
		// Token: 0x040006AB RID: 1707
		None,
		// Token: 0x040006AC RID: 1708
		InOutCubic,
		// Token: 0x040006AD RID: 1709
		InOutQuintic,
		// Token: 0x040006AE RID: 1710
		InOutSine,
		// Token: 0x040006AF RID: 1711
		InQuintic,
		// Token: 0x040006B0 RID: 1712
		InQuartic,
		// Token: 0x040006B1 RID: 1713
		InCubic,
		// Token: 0x040006B2 RID: 1714
		InQuadratic,
		// Token: 0x040006B3 RID: 1715
		InElastic,
		// Token: 0x040006B4 RID: 1716
		InElasticSmall,
		// Token: 0x040006B5 RID: 1717
		InElasticBig,
		// Token: 0x040006B6 RID: 1718
		InSine,
		// Token: 0x040006B7 RID: 1719
		InBack,
		// Token: 0x040006B8 RID: 1720
		OutQuintic,
		// Token: 0x040006B9 RID: 1721
		OutQuartic,
		// Token: 0x040006BA RID: 1722
		OutCubic,
		// Token: 0x040006BB RID: 1723
		OutInCubic,
		// Token: 0x040006BC RID: 1724
		OutInQuartic,
		// Token: 0x040006BD RID: 1725
		OutElastic,
		// Token: 0x040006BE RID: 1726
		OutElasticSmall,
		// Token: 0x040006BF RID: 1727
		OutElasticBig,
		// Token: 0x040006C0 RID: 1728
		OutSine,
		// Token: 0x040006C1 RID: 1729
		OutBack,
		// Token: 0x040006C2 RID: 1730
		OutBackCubic,
		// Token: 0x040006C3 RID: 1731
		OutBackQuartic,
		// Token: 0x040006C4 RID: 1732
		BackInCubic,
		// Token: 0x040006C5 RID: 1733
		BackInQuartic
	}
}
