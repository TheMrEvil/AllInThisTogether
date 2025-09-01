using System;

namespace IKVM.Reflection
{
	// Token: 0x02000022 RID: 34
	[Flags]
	public enum TypeAttributes
	{
		// Token: 0x040000DA RID: 218
		AnsiClass = 0,
		// Token: 0x040000DB RID: 219
		Class = 0,
		// Token: 0x040000DC RID: 220
		AutoLayout = 0,
		// Token: 0x040000DD RID: 221
		NotPublic = 0,
		// Token: 0x040000DE RID: 222
		Public = 1,
		// Token: 0x040000DF RID: 223
		NestedPublic = 2,
		// Token: 0x040000E0 RID: 224
		NestedPrivate = 3,
		// Token: 0x040000E1 RID: 225
		NestedFamily = 4,
		// Token: 0x040000E2 RID: 226
		NestedAssembly = 5,
		// Token: 0x040000E3 RID: 227
		NestedFamANDAssem = 6,
		// Token: 0x040000E4 RID: 228
		VisibilityMask = 7,
		// Token: 0x040000E5 RID: 229
		NestedFamORAssem = 7,
		// Token: 0x040000E6 RID: 230
		SequentialLayout = 8,
		// Token: 0x040000E7 RID: 231
		ExplicitLayout = 16,
		// Token: 0x040000E8 RID: 232
		LayoutMask = 24,
		// Token: 0x040000E9 RID: 233
		ClassSemanticsMask = 32,
		// Token: 0x040000EA RID: 234
		Interface = 32,
		// Token: 0x040000EB RID: 235
		Abstract = 128,
		// Token: 0x040000EC RID: 236
		Sealed = 256,
		// Token: 0x040000ED RID: 237
		SpecialName = 1024,
		// Token: 0x040000EE RID: 238
		RTSpecialName = 2048,
		// Token: 0x040000EF RID: 239
		Import = 4096,
		// Token: 0x040000F0 RID: 240
		Serializable = 8192,
		// Token: 0x040000F1 RID: 241
		WindowsRuntime = 16384,
		// Token: 0x040000F2 RID: 242
		UnicodeClass = 65536,
		// Token: 0x040000F3 RID: 243
		AutoClass = 131072,
		// Token: 0x040000F4 RID: 244
		CustomFormatClass = 196608,
		// Token: 0x040000F5 RID: 245
		StringFormatMask = 196608,
		// Token: 0x040000F6 RID: 246
		HasSecurity = 262144,
		// Token: 0x040000F7 RID: 247
		ReservedMask = 264192,
		// Token: 0x040000F8 RID: 248
		BeforeFieldInit = 1048576,
		// Token: 0x040000F9 RID: 249
		CustomFormatMask = 12582912
	}
}
