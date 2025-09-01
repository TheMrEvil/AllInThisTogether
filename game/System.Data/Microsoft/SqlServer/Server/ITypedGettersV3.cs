using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000022 RID: 34
	internal interface ITypedGettersV3
	{
		// Token: 0x060000FD RID: 253
		bool IsDBNull(SmiEventSink sink, int ordinal);

		// Token: 0x060000FE RID: 254
		SmiMetaData GetVariantType(SmiEventSink sink, int ordinal);

		// Token: 0x060000FF RID: 255
		bool GetBoolean(SmiEventSink sink, int ordinal);

		// Token: 0x06000100 RID: 256
		byte GetByte(SmiEventSink sink, int ordinal);

		// Token: 0x06000101 RID: 257
		long GetBytesLength(SmiEventSink sink, int ordinal);

		// Token: 0x06000102 RID: 258
		int GetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06000103 RID: 259
		long GetCharsLength(SmiEventSink sink, int ordinal);

		// Token: 0x06000104 RID: 260
		int GetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06000105 RID: 261
		string GetString(SmiEventSink sink, int ordinal);

		// Token: 0x06000106 RID: 262
		short GetInt16(SmiEventSink sink, int ordinal);

		// Token: 0x06000107 RID: 263
		int GetInt32(SmiEventSink sink, int ordinal);

		// Token: 0x06000108 RID: 264
		long GetInt64(SmiEventSink sink, int ordinal);

		// Token: 0x06000109 RID: 265
		float GetSingle(SmiEventSink sink, int ordinal);

		// Token: 0x0600010A RID: 266
		double GetDouble(SmiEventSink sink, int ordinal);

		// Token: 0x0600010B RID: 267
		SqlDecimal GetSqlDecimal(SmiEventSink sink, int ordinal);

		// Token: 0x0600010C RID: 268
		DateTime GetDateTime(SmiEventSink sink, int ordinal);

		// Token: 0x0600010D RID: 269
		Guid GetGuid(SmiEventSink sink, int ordinal);
	}
}
