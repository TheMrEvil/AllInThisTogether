using System;

namespace System
{
	// Token: 0x0200016B RID: 363
	[Flags]
	internal enum UriSyntaxFlags
	{
		// Token: 0x04000683 RID: 1667
		None = 0,
		// Token: 0x04000684 RID: 1668
		MustHaveAuthority = 1,
		// Token: 0x04000685 RID: 1669
		OptionalAuthority = 2,
		// Token: 0x04000686 RID: 1670
		MayHaveUserInfo = 4,
		// Token: 0x04000687 RID: 1671
		MayHavePort = 8,
		// Token: 0x04000688 RID: 1672
		MayHavePath = 16,
		// Token: 0x04000689 RID: 1673
		MayHaveQuery = 32,
		// Token: 0x0400068A RID: 1674
		MayHaveFragment = 64,
		// Token: 0x0400068B RID: 1675
		AllowEmptyHost = 128,
		// Token: 0x0400068C RID: 1676
		AllowUncHost = 256,
		// Token: 0x0400068D RID: 1677
		AllowDnsHost = 512,
		// Token: 0x0400068E RID: 1678
		AllowIPv4Host = 1024,
		// Token: 0x0400068F RID: 1679
		AllowIPv6Host = 2048,
		// Token: 0x04000690 RID: 1680
		AllowAnInternetHost = 3584,
		// Token: 0x04000691 RID: 1681
		AllowAnyOtherHost = 4096,
		// Token: 0x04000692 RID: 1682
		FileLikeUri = 8192,
		// Token: 0x04000693 RID: 1683
		MailToLikeUri = 16384,
		// Token: 0x04000694 RID: 1684
		V1_UnknownUri = 65536,
		// Token: 0x04000695 RID: 1685
		SimpleUserSyntax = 131072,
		// Token: 0x04000696 RID: 1686
		BuiltInSyntax = 262144,
		// Token: 0x04000697 RID: 1687
		ParserSchemeOnly = 524288,
		// Token: 0x04000698 RID: 1688
		AllowDOSPath = 1048576,
		// Token: 0x04000699 RID: 1689
		PathIsRooted = 2097152,
		// Token: 0x0400069A RID: 1690
		ConvertPathSlashes = 4194304,
		// Token: 0x0400069B RID: 1691
		CompressPath = 8388608,
		// Token: 0x0400069C RID: 1692
		CanonicalizeAsFilePath = 16777216,
		// Token: 0x0400069D RID: 1693
		UnEscapeDotsAndSlashes = 33554432,
		// Token: 0x0400069E RID: 1694
		AllowIdn = 67108864,
		// Token: 0x0400069F RID: 1695
		AllowIriParsing = 268435456
	}
}
