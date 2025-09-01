using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Represents a link between an event provider and an event log that the provider publishes events into. This object cannot be instantiated.</summary>
	// Token: 0x020003A4 RID: 932
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventLogLink
	{
		// Token: 0x06001BBA RID: 7098 RVA: 0x0000235B File Offset: 0x0000055B
		internal EventLogLink()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the localized name of the event log.</summary>
		/// <returns>Returns a string that contains the localized name of the event log.</returns>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x0005A05A File Offset: 0x0005825A
		public string DisplayName
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a Boolean value that determines whether the event log is imported, rather than defined in the event provider. An imported event log is defined in a different provider.</summary>
		/// <returns>Returns <see langword="true" /> if the event log is imported by the event provider, and returns <see langword="false" /> if the event log is not imported by the event provider.</returns>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x0005A7C0 File Offset: 0x000589C0
		public bool IsImported
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets the non-localized name of the event log associated with this object.</summary>
		/// <returns>Returns a string that contains the non-localized name of the event log associated with this object.</returns>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x0005A05A File Offset: 0x0005825A
		public string LogName
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
