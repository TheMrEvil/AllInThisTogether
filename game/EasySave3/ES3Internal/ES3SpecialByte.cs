using System;

namespace ES3Internal
{
	// Token: 0x020000E7 RID: 231
	internal enum ES3SpecialByte : byte
	{
		// Token: 0x0400016C RID: 364
		Null,
		// Token: 0x0400016D RID: 365
		Bool,
		// Token: 0x0400016E RID: 366
		Byte,
		// Token: 0x0400016F RID: 367
		Sbyte,
		// Token: 0x04000170 RID: 368
		Char,
		// Token: 0x04000171 RID: 369
		Decimal,
		// Token: 0x04000172 RID: 370
		Double,
		// Token: 0x04000173 RID: 371
		Float,
		// Token: 0x04000174 RID: 372
		Int,
		// Token: 0x04000175 RID: 373
		Uint,
		// Token: 0x04000176 RID: 374
		Long,
		// Token: 0x04000177 RID: 375
		Ulong,
		// Token: 0x04000178 RID: 376
		Short,
		// Token: 0x04000179 RID: 377
		Ushort,
		// Token: 0x0400017A RID: 378
		String,
		// Token: 0x0400017B RID: 379
		ByteArray,
		// Token: 0x0400017C RID: 380
		Collection = 128,
		// Token: 0x0400017D RID: 381
		Dictionary,
		// Token: 0x0400017E RID: 382
		CollectionItem,
		// Token: 0x0400017F RID: 383
		Object = 254,
		// Token: 0x04000180 RID: 384
		Terminator
	}
}
