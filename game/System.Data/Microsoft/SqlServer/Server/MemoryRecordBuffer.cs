using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000025 RID: 37
	internal sealed class MemoryRecordBuffer : SmiRecordBuffer
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00005758 File Offset: 0x00003958
		internal MemoryRecordBuffer(SmiMetaData[] metaData)
		{
			this._buffer = new SqlRecordBuffer[metaData.Length];
			for (int i = 0; i < this._buffer.Length; i++)
			{
				this._buffer[i] = new SqlRecordBuffer(metaData[i]);
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000579C File Offset: 0x0000399C
		public override bool IsDBNull(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].IsNull;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000057AB File Offset: 0x000039AB
		public override SmiMetaData GetVariantType(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].VariantType;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000057BA File Offset: 0x000039BA
		public override bool GetBoolean(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Boolean;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000057C9 File Offset: 0x000039C9
		public override byte GetByte(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Byte;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000057D8 File Offset: 0x000039D8
		public override long GetBytesLength(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].BytesLength;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000057E7 File Offset: 0x000039E7
		public override int GetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].GetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000057FD File Offset: 0x000039FD
		public override long GetCharsLength(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].CharsLength;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000580C File Offset: 0x00003A0C
		public override int GetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].GetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005822 File Offset: 0x00003A22
		public override string GetString(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].String;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005831 File Offset: 0x00003A31
		public override short GetInt16(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int16;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005840 File Offset: 0x00003A40
		public override int GetInt32(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int32;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000584F File Offset: 0x00003A4F
		public override long GetInt64(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int64;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000585E File Offset: 0x00003A5E
		public override float GetSingle(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Single;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000586D File Offset: 0x00003A6D
		public override double GetDouble(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Double;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000587C File Offset: 0x00003A7C
		public override SqlDecimal GetSqlDecimal(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].SqlDecimal;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000588B File Offset: 0x00003A8B
		public override DateTime GetDateTime(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].DateTime;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000589A File Offset: 0x00003A9A
		public override Guid GetGuid(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Guid;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000058A9 File Offset: 0x00003AA9
		public override TimeSpan GetTimeSpan(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].TimeSpan;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000058B8 File Offset: 0x00003AB8
		public override DateTimeOffset GetDateTimeOffset(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].DateTimeOffset;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000058C7 File Offset: 0x00003AC7
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._buffer[ordinal].SetNull();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000058D6 File Offset: 0x00003AD6
		public override void SetBoolean(SmiEventSink sink, int ordinal, bool value)
		{
			this._buffer[ordinal].Boolean = value;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000058E6 File Offset: 0x00003AE6
		public override void SetByte(SmiEventSink sink, int ordinal, byte value)
		{
			this._buffer[ordinal].Byte = value;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000058F6 File Offset: 0x00003AF6
		public override int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].SetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000590C File Offset: 0x00003B0C
		public override void SetBytesLength(SmiEventSink sink, int ordinal, long length)
		{
			this._buffer[ordinal].BytesLength = length;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000591C File Offset: 0x00003B1C
		public override int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].SetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005932 File Offset: 0x00003B32
		public override void SetCharsLength(SmiEventSink sink, int ordinal, long length)
		{
			this._buffer[ordinal].CharsLength = length;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005942 File Offset: 0x00003B42
		public override void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length)
		{
			this._buffer[ordinal].String = value.Substring(offset, length);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000595B File Offset: 0x00003B5B
		public override void SetInt16(SmiEventSink sink, int ordinal, short value)
		{
			this._buffer[ordinal].Int16 = value;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000596B File Offset: 0x00003B6B
		public override void SetInt32(SmiEventSink sink, int ordinal, int value)
		{
			this._buffer[ordinal].Int32 = value;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000597B File Offset: 0x00003B7B
		public override void SetInt64(SmiEventSink sink, int ordinal, long value)
		{
			this._buffer[ordinal].Int64 = value;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000598B File Offset: 0x00003B8B
		public override void SetSingle(SmiEventSink sink, int ordinal, float value)
		{
			this._buffer[ordinal].Single = value;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000599B File Offset: 0x00003B9B
		public override void SetDouble(SmiEventSink sink, int ordinal, double value)
		{
			this._buffer[ordinal].Double = value;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000059AB File Offset: 0x00003BAB
		public override void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value)
		{
			this._buffer[ordinal].SqlDecimal = value;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000059BB File Offset: 0x00003BBB
		public override void SetDateTime(SmiEventSink sink, int ordinal, DateTime value)
		{
			this._buffer[ordinal].DateTime = value;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000059CB File Offset: 0x00003BCB
		public override void SetGuid(SmiEventSink sink, int ordinal, Guid value)
		{
			this._buffer[ordinal].Guid = value;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000059DB File Offset: 0x00003BDB
		public override void SetTimeSpan(SmiEventSink sink, int ordinal, TimeSpan value)
		{
			this._buffer[ordinal].TimeSpan = value;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000059EB File Offset: 0x00003BEB
		public override void SetDateTimeOffset(SmiEventSink sink, int ordinal, DateTimeOffset value)
		{
			this._buffer[ordinal].DateTimeOffset = value;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000059FB File Offset: 0x00003BFB
		public override void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData)
		{
			this._buffer[ordinal].VariantType = metaData;
		}

		// Token: 0x0400044A RID: 1098
		private SqlRecordBuffer[] _buffer;
	}
}
