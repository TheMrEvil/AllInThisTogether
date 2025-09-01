using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains an event level that is defined in an event provider. The level signifies the severity of the event.</summary>
	// Token: 0x02000399 RID: 921
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventLevel
	{
		// Token: 0x06001B76 RID: 7030 RVA: 0x0000235B File Offset: 0x0000055B
		internal EventLevel()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the localized name for the event level. The name describes what severity level of events this level is used for.</summary>
		/// <returns>Returns a string that contains the localized name for the event level.</returns>
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x0005A05A File Offset: 0x0005825A
		public string DisplayName
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the non-localized name of the event level.</summary>
		/// <returns>Returns a string that contains the non-localized name of the event level.</returns>
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x0005A05A File Offset: 0x0005825A
		public string Name
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the numeric value of the event level.</summary>
		/// <returns>Returns an integer value.</returns>
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x0005A550 File Offset: 0x00058750
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
