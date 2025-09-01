using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000036 RID: 54
	internal abstract class SmiRecordBuffer : SmiTypedGetterSetter, ITypedGettersV3, ITypedSettersV3, ITypedGetters, ITypedSetters, IDisposable
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00006D61 File Offset: 0x00004F61
		internal override bool CanGet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00006D61 File Offset: 0x00004F61
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void Dispose()
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual bool IsDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlDbType GetVariantType(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual bool GetBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual byte GetByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual char GetChar(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual short GetInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual int GetInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual long GetInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual float GetFloat(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual double GetDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual string GetString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual decimal GetDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual DateTime GetDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual Guid GetGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlBoolean GetSqlBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlByte GetSqlByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlInt16 GetSqlInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlInt32 GetSqlInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlInt64 GetSqlInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlSingle GetSqlSingle(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlDouble GetSqlDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlMoney GetSqlMoney(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlDateTime GetSqlDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlDecimal GetSqlDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlString GetSqlString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlBinary GetSqlBinary(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlGuid GetSqlGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlChars GetSqlChars(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlBytes GetSqlBytes(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlXml GetSqlXml(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlXml GetSqlXmlRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlBytes GetSqlBytesRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual SqlChars GetSqlCharsRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetBoolean(int ordinal, bool value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetByte(int ordinal, byte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetChar(int ordinal, char value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetInt16(int ordinal, short value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetInt32(int ordinal, int value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetInt64(int ordinal, long value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetFloat(int ordinal, float value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetDouble(int ordinal, double value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetString(int ordinal, string value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetString(int ordinal, string value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetDecimal(int ordinal, decimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetDateTime(int ordinal, DateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetGuid(int ordinal, Guid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlBoolean(int ordinal, SqlBoolean value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlByte(int ordinal, SqlByte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlInt16(int ordinal, SqlInt16 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlInt32(int ordinal, SqlInt32 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlInt64(int ordinal, SqlInt64 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlSingle(int ordinal, SqlSingle value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlDouble(int ordinal, SqlDouble value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlMoney(int ordinal, SqlMoney value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlDateTime(int ordinal, SqlDateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlDecimal(int ordinal, SqlDecimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlString(int ordinal, SqlString value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlString(int ordinal, SqlString value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlBinary(int ordinal, SqlBinary value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlBinary(int ordinal, SqlBinary value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlGuid(int ordinal, SqlGuid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlChars(int ordinal, SqlChars value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlChars(int ordinal, SqlChars value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlBytes(int ordinal, SqlBytes value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlBytes(int ordinal, SqlBytes value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetSqlXml(int ordinal, SqlXml value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007F80 File Offset: 0x00006180
		protected SmiRecordBuffer()
		{
		}
	}
}
