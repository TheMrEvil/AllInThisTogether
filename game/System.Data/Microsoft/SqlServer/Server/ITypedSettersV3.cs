using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000024 RID: 36
	internal interface ITypedSettersV3
	{
		// Token: 0x06000132 RID: 306
		void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData);

		// Token: 0x06000133 RID: 307
		void SetDBNull(SmiEventSink sink, int ordinal);

		// Token: 0x06000134 RID: 308
		void SetBoolean(SmiEventSink sink, int ordinal, bool value);

		// Token: 0x06000135 RID: 309
		void SetByte(SmiEventSink sink, int ordinal, byte value);

		// Token: 0x06000136 RID: 310
		int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06000137 RID: 311
		void SetBytesLength(SmiEventSink sink, int ordinal, long length);

		// Token: 0x06000138 RID: 312
		int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06000139 RID: 313
		void SetCharsLength(SmiEventSink sink, int ordinal, long length);

		// Token: 0x0600013A RID: 314
		void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length);

		// Token: 0x0600013B RID: 315
		void SetInt16(SmiEventSink sink, int ordinal, short value);

		// Token: 0x0600013C RID: 316
		void SetInt32(SmiEventSink sink, int ordinal, int value);

		// Token: 0x0600013D RID: 317
		void SetInt64(SmiEventSink sink, int ordinal, long value);

		// Token: 0x0600013E RID: 318
		void SetSingle(SmiEventSink sink, int ordinal, float value);

		// Token: 0x0600013F RID: 319
		void SetDouble(SmiEventSink sink, int ordinal, double value);

		// Token: 0x06000140 RID: 320
		void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value);

		// Token: 0x06000141 RID: 321
		void SetDateTime(SmiEventSink sink, int ordinal, DateTime value);

		// Token: 0x06000142 RID: 322
		void SetGuid(SmiEventSink sink, int ordinal, Guid value);
	}
}
