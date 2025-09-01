using System;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Represents an unstructured log record delivered as a COM+ <see langword="CrmLogRecordRead" /> structure. This class cannot be inherited.</summary>
	// Token: 0x02000075 RID: 117
	public sealed class LogRecord
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x000021E0 File Offset: 0x000003E0
		[MonoTODO]
		internal LogRecord()
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000027A1 File Offset: 0x000009A1
		[MonoTODO]
		internal LogRecord(_LogRecord logRecord)
		{
			this.flags = (LogRecordFlags)logRecord.dwCrmFlags;
			this.sequence = logRecord.dwSequenceNumber;
			this.record = logRecord.blobUserData;
		}

		/// <summary>Gets a value that indicates when the log record was written.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.LogRecordFlags" /> values which provides information about when this record was written.</returns>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000027CD File Offset: 0x000009CD
		public LogRecordFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		/// <summary>Gets the log record user data.</summary>
		/// <returns>A single BLOB that contains the user data.</returns>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000027D5 File Offset: 0x000009D5
		public object Record
		{
			get
			{
				return this.record;
			}
		}

		/// <summary>The sequence number of the log record.</summary>
		/// <returns>An integer value that specifies the sequence number of the log record.</returns>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000027DD File Offset: 0x000009DD
		public int Sequence
		{
			get
			{
				return this.sequence;
			}
		}

		// Token: 0x040000BE RID: 190
		private LogRecordFlags flags;

		// Token: 0x040000BF RID: 191
		private object record;

		// Token: 0x040000C0 RID: 192
		private int sequence;
	}
}
