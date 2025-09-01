using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000687 RID: 1671
	internal enum InternalParseTypeE
	{
		// Token: 0x040027E9 RID: 10217
		Empty,
		// Token: 0x040027EA RID: 10218
		SerializedStreamHeader,
		// Token: 0x040027EB RID: 10219
		Object,
		// Token: 0x040027EC RID: 10220
		Member,
		// Token: 0x040027ED RID: 10221
		ObjectEnd,
		// Token: 0x040027EE RID: 10222
		MemberEnd,
		// Token: 0x040027EF RID: 10223
		Headers,
		// Token: 0x040027F0 RID: 10224
		HeadersEnd,
		// Token: 0x040027F1 RID: 10225
		SerializedStreamHeaderEnd,
		// Token: 0x040027F2 RID: 10226
		Envelope,
		// Token: 0x040027F3 RID: 10227
		EnvelopeEnd,
		// Token: 0x040027F4 RID: 10228
		Body,
		// Token: 0x040027F5 RID: 10229
		BodyEnd
	}
}
