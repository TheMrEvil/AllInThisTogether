using System;

namespace IKVM.Reflection
{
	// Token: 0x0200001A RID: 26
	[Flags]
	public enum MethodAttributes
	{
		// Token: 0x0400008A RID: 138
		MemberAccessMask = 7,
		// Token: 0x0400008B RID: 139
		PrivateScope = 0,
		// Token: 0x0400008C RID: 140
		Private = 1,
		// Token: 0x0400008D RID: 141
		FamANDAssem = 2,
		// Token: 0x0400008E RID: 142
		Assembly = 3,
		// Token: 0x0400008F RID: 143
		Family = 4,
		// Token: 0x04000090 RID: 144
		FamORAssem = 5,
		// Token: 0x04000091 RID: 145
		Public = 6,
		// Token: 0x04000092 RID: 146
		Static = 16,
		// Token: 0x04000093 RID: 147
		Final = 32,
		// Token: 0x04000094 RID: 148
		Virtual = 64,
		// Token: 0x04000095 RID: 149
		HideBySig = 128,
		// Token: 0x04000096 RID: 150
		VtableLayoutMask = 256,
		// Token: 0x04000097 RID: 151
		ReuseSlot = 0,
		// Token: 0x04000098 RID: 152
		NewSlot = 256,
		// Token: 0x04000099 RID: 153
		CheckAccessOnOverride = 512,
		// Token: 0x0400009A RID: 154
		Abstract = 1024,
		// Token: 0x0400009B RID: 155
		SpecialName = 2048,
		// Token: 0x0400009C RID: 156
		PinvokeImpl = 8192,
		// Token: 0x0400009D RID: 157
		UnmanagedExport = 8,
		// Token: 0x0400009E RID: 158
		RTSpecialName = 4096,
		// Token: 0x0400009F RID: 159
		HasSecurity = 16384,
		// Token: 0x040000A0 RID: 160
		RequireSecObject = 32768,
		// Token: 0x040000A1 RID: 161
		ReservedMask = 53248
	}
}
