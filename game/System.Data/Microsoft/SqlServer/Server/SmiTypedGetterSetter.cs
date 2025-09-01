using System;
using System.Data.Common;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000038 RID: 56
	internal abstract class SmiTypedGetterSetter : ITypedGettersV3, ITypedSettersV3
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000233 RID: 563
		internal abstract bool CanGet { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000234 RID: 564
		internal abstract bool CanSet { get; }

		// Token: 0x06000235 RID: 565 RVA: 0x00008060 File Offset: 0x00006260
		public virtual bool IsDBNull(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00008060 File Offset: 0x00006260
		public virtual SmiMetaData GetVariantType(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00008060 File Offset: 0x00006260
		public virtual bool GetBoolean(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00008060 File Offset: 0x00006260
		public virtual byte GetByte(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00008060 File Offset: 0x00006260
		public virtual long GetBytesLength(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00008060 File Offset: 0x00006260
		public virtual int GetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00008060 File Offset: 0x00006260
		public virtual long GetCharsLength(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00008060 File Offset: 0x00006260
		public virtual int GetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00008060 File Offset: 0x00006260
		public virtual string GetString(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00008060 File Offset: 0x00006260
		public virtual short GetInt16(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00008060 File Offset: 0x00006260
		public virtual int GetInt32(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00008060 File Offset: 0x00006260
		public virtual long GetInt64(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00008060 File Offset: 0x00006260
		public virtual float GetSingle(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00008060 File Offset: 0x00006260
		public virtual double GetDouble(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00008060 File Offset: 0x00006260
		public virtual SqlDecimal GetSqlDecimal(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00008060 File Offset: 0x00006260
		public virtual DateTime GetDateTime(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00008060 File Offset: 0x00006260
		public virtual Guid GetGuid(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00008060 File Offset: 0x00006260
		public virtual TimeSpan GetTimeSpan(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00008060 File Offset: 0x00006260
		public virtual DateTimeOffset GetDateTimeOffset(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007F77 File Offset: 0x00006177
		internal virtual SmiTypedGetterSetter GetTypedGetterSetter(SmiEventSink sink, int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00008079 File Offset: 0x00006279
		public virtual void SetDBNull(SmiEventSink sink, int ordinal)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00008079 File Offset: 0x00006279
		public virtual void SetBoolean(SmiEventSink sink, int ordinal, bool value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00008079 File Offset: 0x00006279
		public virtual void SetByte(SmiEventSink sink, int ordinal, byte value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00008079 File Offset: 0x00006279
		public virtual int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00008079 File Offset: 0x00006279
		public virtual void SetBytesLength(SmiEventSink sink, int ordinal, long length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00008079 File Offset: 0x00006279
		public virtual int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008079 File Offset: 0x00006279
		public virtual void SetCharsLength(SmiEventSink sink, int ordinal, long length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008079 File Offset: 0x00006279
		public virtual void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00008079 File Offset: 0x00006279
		public virtual void SetInt16(SmiEventSink sink, int ordinal, short value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00008079 File Offset: 0x00006279
		public virtual void SetInt32(SmiEventSink sink, int ordinal, int value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00008079 File Offset: 0x00006279
		public virtual void SetInt64(SmiEventSink sink, int ordinal, long value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00008079 File Offset: 0x00006279
		public virtual void SetSingle(SmiEventSink sink, int ordinal, float value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00008079 File Offset: 0x00006279
		public virtual void SetDouble(SmiEventSink sink, int ordinal, double value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00008079 File Offset: 0x00006279
		public virtual void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00008079 File Offset: 0x00006279
		public virtual void SetDateTime(SmiEventSink sink, int ordinal, DateTime value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00008079 File Offset: 0x00006279
		public virtual void SetGuid(SmiEventSink sink, int ordinal, Guid value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00008079 File Offset: 0x00006279
		public virtual void SetTimeSpan(SmiEventSink sink, int ordinal, TimeSpan value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00008079 File Offset: 0x00006279
		public virtual void SetDateTimeOffset(SmiEventSink sink, int ordinal, DateTimeOffset value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00007F77 File Offset: 0x00006177
		public virtual void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00008079 File Offset: 0x00006279
		internal virtual void NewElement(SmiEventSink sink)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00008079 File Offset: 0x00006279
		internal virtual void EndElements(SmiEventSink sink)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00003D93 File Offset: 0x00001F93
		protected SmiTypedGetterSetter()
		{
		}
	}
}
