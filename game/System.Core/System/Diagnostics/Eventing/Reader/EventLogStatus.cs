using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains the status code or error code for a specific event log. This status can be used to determine if the event log is available for an operation.</summary>
	// Token: 0x020003AA RID: 938
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventLogStatus
	{
		// Token: 0x06001BE0 RID: 7136 RVA: 0x0000235B File Offset: 0x0000055B
		internal EventLogStatus()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the name of the event log for which the status code is obtained.</summary>
		/// <returns>Returns a string that contains the name of the event log for which the status code is obtained.</returns>
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x0005A05A File Offset: 0x0005825A
		public string LogName
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the status code or error code for the event log. This status or error is the result of a read or subscription operation on the event log.</summary>
		/// <returns>Returns an integer value.</returns>
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x0005A830 File Offset: 0x00058A30
		public int StatusCode
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}
	}
}
