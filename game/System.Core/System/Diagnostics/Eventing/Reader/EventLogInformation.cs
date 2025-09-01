using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Allows you to access the run-time properties of active event logs and event log files. These properties include the number of events in the log, the size of the log, a value that determines whether the log is full, and the last time the log was written to or accessed.</summary>
	// Token: 0x0200039E RID: 926
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventLogInformation
	{
		// Token: 0x06001BA8 RID: 7080 RVA: 0x0000235B File Offset: 0x0000055B
		internal EventLogInformation()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the file attributes of the log file associated with the log.</summary>
		/// <returns>Returns an integer value. This value can be null.</returns>
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x0005A6E0 File Offset: 0x000588E0
		public int? Attributes
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the time that the log file associated with the event log was created.</summary>
		/// <returns>Returns a <see cref="T:System.DateTime" /> object. This value can be null.</returns>
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x0005A6FC File Offset: 0x000588FC
		public DateTime? CreationTime
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the size of the file, in bytes, associated with the event log.</summary>
		/// <returns>Returns a long value.</returns>
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x0005A718 File Offset: 0x00058918
		public long? FileSize
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a Boolean value that determines whether the log file has reached its maximum size (the log is full).</summary>
		/// <returns>Returns <see langword="true" /> if the log is full, and returns <see langword="false" /> if the log is not full.</returns>
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x0005A734 File Offset: 0x00058934
		public bool? IsLogFull
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the last time the log file associated with the event log was accessed.</summary>
		/// <returns>Returns a <see cref="T:System.DateTime" /> object. This value can be null.</returns>
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x0005A750 File Offset: 0x00058950
		public DateTime? LastAccessTime
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the last time data was written to the log file associated with the event log.</summary>
		/// <returns>Returns a <see cref="T:System.DateTime" /> object. This value can be null.</returns>
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x0005A76C File Offset: 0x0005896C
		public DateTime? LastWriteTime
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the number of the oldest event record in the event log.</summary>
		/// <returns>Returns a long value that represents the number of the oldest event record in the event log. This value can be null.</returns>
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x0005A788 File Offset: 0x00058988
		public long? OldestRecordNumber
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the number of event records in the event log.</summary>
		/// <returns>Returns a long value that represents the number of event records in the event log. This value can be null.</returns>
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x0005A7A4 File Offset: 0x000589A4
		public long? RecordCount
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
