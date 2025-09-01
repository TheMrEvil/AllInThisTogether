using System;

namespace System.Reflection
{
	// Token: 0x020008E5 RID: 2277
	[Serializable]
	internal enum MetadataTokenType
	{
		// Token: 0x04002FE6 RID: 12262
		Module,
		// Token: 0x04002FE7 RID: 12263
		TypeRef = 16777216,
		// Token: 0x04002FE8 RID: 12264
		TypeDef = 33554432,
		// Token: 0x04002FE9 RID: 12265
		FieldDef = 67108864,
		// Token: 0x04002FEA RID: 12266
		MethodDef = 100663296,
		// Token: 0x04002FEB RID: 12267
		ParamDef = 134217728,
		// Token: 0x04002FEC RID: 12268
		InterfaceImpl = 150994944,
		// Token: 0x04002FED RID: 12269
		MemberRef = 167772160,
		// Token: 0x04002FEE RID: 12270
		CustomAttribute = 201326592,
		// Token: 0x04002FEF RID: 12271
		Permission = 234881024,
		// Token: 0x04002FF0 RID: 12272
		Signature = 285212672,
		// Token: 0x04002FF1 RID: 12273
		Event = 335544320,
		// Token: 0x04002FF2 RID: 12274
		Property = 385875968,
		// Token: 0x04002FF3 RID: 12275
		ModuleRef = 436207616,
		// Token: 0x04002FF4 RID: 12276
		TypeSpec = 452984832,
		// Token: 0x04002FF5 RID: 12277
		Assembly = 536870912,
		// Token: 0x04002FF6 RID: 12278
		AssemblyRef = 587202560,
		// Token: 0x04002FF7 RID: 12279
		File = 637534208,
		// Token: 0x04002FF8 RID: 12280
		ExportedType = 654311424,
		// Token: 0x04002FF9 RID: 12281
		ManifestResource = 671088640,
		// Token: 0x04002FFA RID: 12282
		GenericPar = 704643072,
		// Token: 0x04002FFB RID: 12283
		MethodSpec = 721420288,
		// Token: 0x04002FFC RID: 12284
		String = 1879048192,
		// Token: 0x04002FFD RID: 12285
		Name = 1895825408,
		// Token: 0x04002FFE RID: 12286
		BaseType = 1912602624,
		// Token: 0x04002FFF RID: 12287
		Invalid = 2147483647
	}
}
