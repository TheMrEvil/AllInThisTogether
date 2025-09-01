using System;

namespace Mono.CSharp
{
	// Token: 0x02000244 RID: 580
	[Flags]
	public enum MemberKind
	{
		// Token: 0x04000AA0 RID: 2720
		Constructor = 1,
		// Token: 0x04000AA1 RID: 2721
		Event = 2,
		// Token: 0x04000AA2 RID: 2722
		Field = 4,
		// Token: 0x04000AA3 RID: 2723
		Method = 8,
		// Token: 0x04000AA4 RID: 2724
		Property = 16,
		// Token: 0x04000AA5 RID: 2725
		Indexer = 32,
		// Token: 0x04000AA6 RID: 2726
		Operator = 64,
		// Token: 0x04000AA7 RID: 2727
		Destructor = 128,
		// Token: 0x04000AA8 RID: 2728
		Class = 2048,
		// Token: 0x04000AA9 RID: 2729
		Struct = 4096,
		// Token: 0x04000AAA RID: 2730
		Delegate = 8192,
		// Token: 0x04000AAB RID: 2731
		Enum = 16384,
		// Token: 0x04000AAC RID: 2732
		Interface = 32768,
		// Token: 0x04000AAD RID: 2733
		TypeParameter = 65536,
		// Token: 0x04000AAE RID: 2734
		ArrayType = 524288,
		// Token: 0x04000AAF RID: 2735
		PointerType = 1048576,
		// Token: 0x04000AB0 RID: 2736
		InternalCompilerType = 2097152,
		// Token: 0x04000AB1 RID: 2737
		MissingType = 4194304,
		// Token: 0x04000AB2 RID: 2738
		Void = 8388608,
		// Token: 0x04000AB3 RID: 2739
		Namespace = 16777216,
		// Token: 0x04000AB4 RID: 2740
		NestedMask = 63488,
		// Token: 0x04000AB5 RID: 2741
		GenericMask = 47112,
		// Token: 0x04000AB6 RID: 2742
		MaskType = 63743
	}
}
