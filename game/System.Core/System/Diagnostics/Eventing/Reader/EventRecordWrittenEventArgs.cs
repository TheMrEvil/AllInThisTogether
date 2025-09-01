using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>When the <see cref="E:System.Diagnostics.Eventing.Reader.EventLogWatcher.EventRecordWritten" /> event is raised, an instance of this object is passed to the delegate method that handles the event. This object contains the event that was published to the event log or the exception that occurred when the event subscription failed. </summary>
	// Token: 0x020003B0 RID: 944
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventRecordWrittenEventArgs : EventArgs
	{
		// Token: 0x06001C31 RID: 7217 RVA: 0x0000235B File Offset: 0x0000055B
		internal EventRecordWrittenEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the exception that occurred when the event subscription failed. The exception has a description of why the subscription failed.</summary>
		/// <returns>Returns an <see cref="T:System.Exception" /> object.</returns>
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x0005A05A File Offset: 0x0005825A
		public Exception EventException
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the event record that is published to the event log. This event matches the criteria from the query specified in the event subscription.</summary>
		/// <returns>Returns a <see cref="T:System.Diagnostics.Eventing.Reader.EventRecord" /> object.</returns>
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x0005A05A File Offset: 0x0005825A
		public EventRecord EventRecord
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
