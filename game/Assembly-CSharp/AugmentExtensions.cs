using System;

// Token: 0x020000AA RID: 170
public static class AugmentExtensions
{
	// Token: 0x060007BC RID: 1980 RVA: 0x00037A60 File Offset: 0x00035C60
	public static bool Matches(this QualitySelector s, AugmentQuality r)
	{
		if (s == QualitySelector.Any || s == QualitySelector.None)
		{
			return true;
		}
		bool result;
		switch (r)
		{
		case AugmentQuality.Basic:
			result = s.HasFlag(QualitySelector.Uncommon);
			break;
		case AugmentQuality.Normal:
			result = s.HasFlag(QualitySelector.Rare);
			break;
		case AugmentQuality.Epic:
			result = s.HasFlag(QualitySelector.Epic);
			break;
		case AugmentQuality.Legendary:
			result = s.HasFlag(QualitySelector.Legendary);
			break;
		case AugmentQuality.Artifact:
			result = s.HasFlag(QualitySelector.Unique);
			break;
		default:
			result = false;
			break;
		}
		return result;
	}
}
