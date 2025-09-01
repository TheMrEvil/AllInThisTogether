using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Represents a keyword for an event. Keywords are defined in an event provider and are used to group the event with other similar events (based on the usage of the events).</summary>
	// Token: 0x02000398 RID: 920
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventKeyword
	{
		// Token: 0x06001B72 RID: 7026 RVA: 0x0000235B File Offset: 0x0000055B
		internal EventKeyword()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the localized name of the keyword.</summary>
		/// <returns>Returns a string that contains a localized name for this keyword.</returns>
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x0005A05A File Offset: 0x0005825A
		public string DisplayName
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the non-localized name of the keyword.</summary>
		/// <returns>Returns a string that contains the non-localized name of this keyword.</returns>
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x0005A05A File Offset: 0x0005825A
		public string Name
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the numeric value associated with the keyword.</summary>
		/// <returns>Returns a <see langword="long" /> value.</returns>
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x0005A534 File Offset: 0x00058734
		public long Value
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}
	}
}
