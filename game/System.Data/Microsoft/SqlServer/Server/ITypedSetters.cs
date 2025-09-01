using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000023 RID: 35
	internal interface ITypedSetters
	{
		// Token: 0x0600010E RID: 270
		void SetDBNull(int ordinal);

		// Token: 0x0600010F RID: 271
		void SetBoolean(int ordinal, bool value);

		// Token: 0x06000110 RID: 272
		void SetByte(int ordinal, byte value);

		// Token: 0x06000111 RID: 273
		void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06000112 RID: 274
		void SetChar(int ordinal, char value);

		// Token: 0x06000113 RID: 275
		void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06000114 RID: 276
		void SetInt16(int ordinal, short value);

		// Token: 0x06000115 RID: 277
		void SetInt32(int ordinal, int value);

		// Token: 0x06000116 RID: 278
		void SetInt64(int ordinal, long value);

		// Token: 0x06000117 RID: 279
		void SetFloat(int ordinal, float value);

		// Token: 0x06000118 RID: 280
		void SetDouble(int ordinal, double value);

		// Token: 0x06000119 RID: 281
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetString(int ordinal, string value);

		// Token: 0x0600011A RID: 282
		void SetString(int ordinal, string value, int offset);

		// Token: 0x0600011B RID: 283
		void SetDecimal(int ordinal, decimal value);

		// Token: 0x0600011C RID: 284
		void SetDateTime(int ordinal, DateTime value);

		// Token: 0x0600011D RID: 285
		void SetGuid(int ordinal, Guid value);

		// Token: 0x0600011E RID: 286
		void SetSqlBoolean(int ordinal, SqlBoolean value);

		// Token: 0x0600011F RID: 287
		void SetSqlByte(int ordinal, SqlByte value);

		// Token: 0x06000120 RID: 288
		void SetSqlInt16(int ordinal, SqlInt16 value);

		// Token: 0x06000121 RID: 289
		void SetSqlInt32(int ordinal, SqlInt32 value);

		// Token: 0x06000122 RID: 290
		void SetSqlInt64(int ordinal, SqlInt64 value);

		// Token: 0x06000123 RID: 291
		void SetSqlSingle(int ordinal, SqlSingle value);

		// Token: 0x06000124 RID: 292
		void SetSqlDouble(int ordinal, SqlDouble value);

		// Token: 0x06000125 RID: 293
		void SetSqlMoney(int ordinal, SqlMoney value);

		// Token: 0x06000126 RID: 294
		void SetSqlDateTime(int ordinal, SqlDateTime value);

		// Token: 0x06000127 RID: 295
		void SetSqlDecimal(int ordinal, SqlDecimal value);

		// Token: 0x06000128 RID: 296
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlString(int ordinal, SqlString value);

		// Token: 0x06000129 RID: 297
		void SetSqlString(int ordinal, SqlString value, int offset);

		// Token: 0x0600012A RID: 298
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlBinary(int ordinal, SqlBinary value);

		// Token: 0x0600012B RID: 299
		void SetSqlBinary(int ordinal, SqlBinary value, int offset);

		// Token: 0x0600012C RID: 300
		void SetSqlGuid(int ordinal, SqlGuid value);

		// Token: 0x0600012D RID: 301
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlChars(int ordinal, SqlChars value);

		// Token: 0x0600012E RID: 302
		void SetSqlChars(int ordinal, SqlChars value, int offset);

		// Token: 0x0600012F RID: 303
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlBytes(int ordinal, SqlBytes value);

		// Token: 0x06000130 RID: 304
		void SetSqlBytes(int ordinal, SqlBytes value, int offset);

		// Token: 0x06000131 RID: 305
		void SetSqlXml(int ordinal, SqlXml value);
	}
}
