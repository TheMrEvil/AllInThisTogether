using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002BC RID: 700
	internal enum TypeFlags
	{
		// Token: 0x0400197E RID: 6526
		None,
		// Token: 0x0400197F RID: 6527
		Abstract,
		// Token: 0x04001980 RID: 6528
		Reference,
		// Token: 0x04001981 RID: 6529
		Special = 4,
		// Token: 0x04001982 RID: 6530
		CanBeAttributeValue = 8,
		// Token: 0x04001983 RID: 6531
		CanBeTextValue = 16,
		// Token: 0x04001984 RID: 6532
		CanBeElementValue = 32,
		// Token: 0x04001985 RID: 6533
		HasCustomFormatter = 64,
		// Token: 0x04001986 RID: 6534
		AmbiguousDataType = 128,
		// Token: 0x04001987 RID: 6535
		IgnoreDefault = 512,
		// Token: 0x04001988 RID: 6536
		HasIsEmpty = 1024,
		// Token: 0x04001989 RID: 6537
		HasDefaultConstructor = 2048,
		// Token: 0x0400198A RID: 6538
		XmlEncodingNotRequired = 4096,
		// Token: 0x0400198B RID: 6539
		UseReflection = 16384,
		// Token: 0x0400198C RID: 6540
		CollapseWhitespace = 32768,
		// Token: 0x0400198D RID: 6541
		OptionalValue = 65536,
		// Token: 0x0400198E RID: 6542
		CtorInaccessible = 131072,
		// Token: 0x0400198F RID: 6543
		UsePrivateImplementation = 262144,
		// Token: 0x04001990 RID: 6544
		GenericInterface = 524288,
		// Token: 0x04001991 RID: 6545
		Unsupported = 1048576
	}
}
