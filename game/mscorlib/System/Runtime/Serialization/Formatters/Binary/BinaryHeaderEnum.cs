using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000683 RID: 1667
	internal enum BinaryHeaderEnum
	{
		// Token: 0x040027BE RID: 10174
		SerializedStreamHeader,
		// Token: 0x040027BF RID: 10175
		Object,
		// Token: 0x040027C0 RID: 10176
		ObjectWithMap,
		// Token: 0x040027C1 RID: 10177
		ObjectWithMapAssemId,
		// Token: 0x040027C2 RID: 10178
		ObjectWithMapTyped,
		// Token: 0x040027C3 RID: 10179
		ObjectWithMapTypedAssemId,
		// Token: 0x040027C4 RID: 10180
		ObjectString,
		// Token: 0x040027C5 RID: 10181
		Array,
		// Token: 0x040027C6 RID: 10182
		MemberPrimitiveTyped,
		// Token: 0x040027C7 RID: 10183
		MemberReference,
		// Token: 0x040027C8 RID: 10184
		ObjectNull,
		// Token: 0x040027C9 RID: 10185
		MessageEnd,
		// Token: 0x040027CA RID: 10186
		Assembly,
		// Token: 0x040027CB RID: 10187
		ObjectNullMultiple256,
		// Token: 0x040027CC RID: 10188
		ObjectNullMultiple,
		// Token: 0x040027CD RID: 10189
		ArraySinglePrimitive,
		// Token: 0x040027CE RID: 10190
		ArraySingleObject,
		// Token: 0x040027CF RID: 10191
		ArraySingleString,
		// Token: 0x040027D0 RID: 10192
		CrossAppDomainMap,
		// Token: 0x040027D1 RID: 10193
		CrossAppDomainString,
		// Token: 0x040027D2 RID: 10194
		CrossAppDomainAssembly,
		// Token: 0x040027D3 RID: 10195
		MethodCall,
		// Token: 0x040027D4 RID: 10196
		MethodReturn
	}
}
