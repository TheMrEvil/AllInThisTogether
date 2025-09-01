using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains an event task that is defined in an event provider. The task identifies a portion of an application or a component that publishes an event. A task is a 16-bit value with 16 top values reserved.</summary>
	// Token: 0x020003B3 RID: 947
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventTask
	{
		// Token: 0x06001C42 RID: 7234 RVA: 0x0000235B File Offset: 0x0000055B
		internal EventTask()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the localized name for the event task.</summary>
		/// <returns>Returns a string that contains the localized name for the event task.</returns>
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001C43 RID: 7235 RVA: 0x0005A05A File Offset: 0x0005825A
		public string DisplayName
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the event globally unique identifier (GUID) associated with the task. </summary>
		/// <returns>Returns a GUID value.</returns>
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x0005AA44 File Offset: 0x00058C44
		public Guid EventGuid
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(Guid);
			}
		}

		/// <summary>Gets the non-localized name of the event task.</summary>
		/// <returns>Returns a string that contains the non-localized name of the event task.</returns>
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x0005A05A File Offset: 0x0005825A
		public string Name
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the numeric value associated with the task.</summary>
		/// <returns>Returns an integer value.</returns>
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x0005AA60 File Offset: 0x00058C60
		public int Value
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}
	}
}
