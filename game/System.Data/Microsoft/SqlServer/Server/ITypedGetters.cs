using System;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000021 RID: 33
	internal interface ITypedGetters
	{
		// Token: 0x060000DA RID: 218
		bool IsDBNull(int ordinal);

		// Token: 0x060000DB RID: 219
		SqlDbType GetVariantType(int ordinal);

		// Token: 0x060000DC RID: 220
		bool GetBoolean(int ordinal);

		// Token: 0x060000DD RID: 221
		byte GetByte(int ordinal);

		// Token: 0x060000DE RID: 222
		long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x060000DF RID: 223
		char GetChar(int ordinal);

		// Token: 0x060000E0 RID: 224
		long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x060000E1 RID: 225
		short GetInt16(int ordinal);

		// Token: 0x060000E2 RID: 226
		int GetInt32(int ordinal);

		// Token: 0x060000E3 RID: 227
		long GetInt64(int ordinal);

		// Token: 0x060000E4 RID: 228
		float GetFloat(int ordinal);

		// Token: 0x060000E5 RID: 229
		double GetDouble(int ordinal);

		// Token: 0x060000E6 RID: 230
		string GetString(int ordinal);

		// Token: 0x060000E7 RID: 231
		decimal GetDecimal(int ordinal);

		// Token: 0x060000E8 RID: 232
		DateTime GetDateTime(int ordinal);

		// Token: 0x060000E9 RID: 233
		Guid GetGuid(int ordinal);

		// Token: 0x060000EA RID: 234
		SqlBoolean GetSqlBoolean(int ordinal);

		// Token: 0x060000EB RID: 235
		SqlByte GetSqlByte(int ordinal);

		// Token: 0x060000EC RID: 236
		SqlInt16 GetSqlInt16(int ordinal);

		// Token: 0x060000ED RID: 237
		SqlInt32 GetSqlInt32(int ordinal);

		// Token: 0x060000EE RID: 238
		SqlInt64 GetSqlInt64(int ordinal);

		// Token: 0x060000EF RID: 239
		SqlSingle GetSqlSingle(int ordinal);

		// Token: 0x060000F0 RID: 240
		SqlDouble GetSqlDouble(int ordinal);

		// Token: 0x060000F1 RID: 241
		SqlMoney GetSqlMoney(int ordinal);

		// Token: 0x060000F2 RID: 242
		SqlDateTime GetSqlDateTime(int ordinal);

		// Token: 0x060000F3 RID: 243
		SqlDecimal GetSqlDecimal(int ordinal);

		// Token: 0x060000F4 RID: 244
		SqlString GetSqlString(int ordinal);

		// Token: 0x060000F5 RID: 245
		SqlBinary GetSqlBinary(int ordinal);

		// Token: 0x060000F6 RID: 246
		SqlGuid GetSqlGuid(int ordinal);

		// Token: 0x060000F7 RID: 247
		SqlChars GetSqlChars(int ordinal);

		// Token: 0x060000F8 RID: 248
		SqlBytes GetSqlBytes(int ordinal);

		// Token: 0x060000F9 RID: 249
		SqlXml GetSqlXml(int ordinal);

		// Token: 0x060000FA RID: 250
		SqlBytes GetSqlBytesRef(int ordinal);

		// Token: 0x060000FB RID: 251
		SqlChars GetSqlCharsRef(int ordinal);

		// Token: 0x060000FC RID: 252
		SqlXml GetSqlXmlRef(int ordinal);
	}
}
