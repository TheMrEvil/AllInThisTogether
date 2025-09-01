using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x0200027D RID: 637
	internal class TdsRecordBufferSetter : SmiRecordBuffer
	{
		// Token: 0x06001DE7 RID: 7655 RVA: 0x0008DCD4 File Offset: 0x0008BED4
		internal TdsRecordBufferSetter(TdsParserStateObject stateObj, SmiMetaData md)
		{
			this._fieldSetters = new TdsValueSetter[md.FieldMetaData.Count];
			for (int i = 0; i < md.FieldMetaData.Count; i++)
			{
				this._fieldSetters[i] = new TdsValueSetter(stateObj, md.FieldMetaData[i]);
			}
			this._stateObj = stateObj;
			this._metaData = md;
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x00006D64 File Offset: 0x00004F64
		internal override bool CanGet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x00006D61 File Offset: 0x00004F61
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x0008DD3B File Offset: 0x0008BF3B
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._fieldSetters[ordinal].SetDBNull();
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x0008DD4A File Offset: 0x0008BF4A
		public override void SetBoolean(SmiEventSink sink, int ordinal, bool value)
		{
			this._fieldSetters[ordinal].SetBoolean(value);
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x0008DD5A File Offset: 0x0008BF5A
		public override void SetByte(SmiEventSink sink, int ordinal, byte value)
		{
			this._fieldSetters[ordinal].SetByte(value);
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x0008DD6A File Offset: 0x0008BF6A
		public override int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._fieldSetters[ordinal].SetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x0008DD80 File Offset: 0x0008BF80
		public override void SetBytesLength(SmiEventSink sink, int ordinal, long length)
		{
			this._fieldSetters[ordinal].SetBytesLength(length);
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x0008DD90 File Offset: 0x0008BF90
		public override int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._fieldSetters[ordinal].SetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x0008DDA6 File Offset: 0x0008BFA6
		public override void SetCharsLength(SmiEventSink sink, int ordinal, long length)
		{
			this._fieldSetters[ordinal].SetCharsLength(length);
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x0008DDB6 File Offset: 0x0008BFB6
		public override void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length)
		{
			this._fieldSetters[ordinal].SetString(value, offset, length);
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x0008DDCA File Offset: 0x0008BFCA
		public override void SetInt16(SmiEventSink sink, int ordinal, short value)
		{
			this._fieldSetters[ordinal].SetInt16(value);
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x0008DDDA File Offset: 0x0008BFDA
		public override void SetInt32(SmiEventSink sink, int ordinal, int value)
		{
			this._fieldSetters[ordinal].SetInt32(value);
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x0008DDEA File Offset: 0x0008BFEA
		public override void SetInt64(SmiEventSink sink, int ordinal, long value)
		{
			this._fieldSetters[ordinal].SetInt64(value);
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x0008DDFA File Offset: 0x0008BFFA
		public override void SetSingle(SmiEventSink sink, int ordinal, float value)
		{
			this._fieldSetters[ordinal].SetSingle(value);
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x0008DE0A File Offset: 0x0008C00A
		public override void SetDouble(SmiEventSink sink, int ordinal, double value)
		{
			this._fieldSetters[ordinal].SetDouble(value);
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x0008DE1A File Offset: 0x0008C01A
		public override void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value)
		{
			this._fieldSetters[ordinal].SetSqlDecimal(value);
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x0008DE2A File Offset: 0x0008C02A
		public override void SetDateTime(SmiEventSink sink, int ordinal, DateTime value)
		{
			this._fieldSetters[ordinal].SetDateTime(value);
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x0008DE3A File Offset: 0x0008C03A
		public override void SetGuid(SmiEventSink sink, int ordinal, Guid value)
		{
			this._fieldSetters[ordinal].SetGuid(value);
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x0008DE4A File Offset: 0x0008C04A
		public override void SetTimeSpan(SmiEventSink sink, int ordinal, TimeSpan value)
		{
			this._fieldSetters[ordinal].SetTimeSpan(value);
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x0008DE5A File Offset: 0x0008C05A
		public override void SetDateTimeOffset(SmiEventSink sink, int ordinal, DateTimeOffset value)
		{
			this._fieldSetters[ordinal].SetDateTimeOffset(value);
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x0008DE6A File Offset: 0x0008C06A
		public override void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData)
		{
			this._fieldSetters[ordinal].SetVariantType(metaData);
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x0008DE7A File Offset: 0x0008C07A
		internal override void NewElement(SmiEventSink sink)
		{
			this._stateObj.WriteByte(1);
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x0008DE88 File Offset: 0x0008C088
		internal override void EndElements(SmiEventSink sink)
		{
			this._stateObj.WriteByte(0);
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x00007EED File Offset: 0x000060ED
		[Conditional("DEBUG")]
		private void CheckWritingToColumn(int ordinal)
		{
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x00007EED File Offset: 0x000060ED
		[Conditional("DEBUG")]
		private void SkipPossibleDefaultedColumns(int targetColumn)
		{
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x00007EED File Offset: 0x000060ED
		[Conditional("DEBUG")]
		internal void CheckSettingColumn(int ordinal)
		{
		}

		// Token: 0x040014A3 RID: 5283
		private TdsValueSetter[] _fieldSetters;

		// Token: 0x040014A4 RID: 5284
		private TdsParserStateObject _stateObj;

		// Token: 0x040014A5 RID: 5285
		private SmiMetaData _metaData;
	}
}
