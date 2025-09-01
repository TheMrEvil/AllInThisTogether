using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains an event opcode that is defined in an event provider. An opcode defines a numeric value that identifies the activity or a point within an activity that the application was performing when it raised the event.</summary>
	// Token: 0x020003B2 RID: 946
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventOpcode
	{
		// Token: 0x06001C3E RID: 7230 RVA: 0x0000235B File Offset: 0x0000055B
		internal EventOpcode()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the localized name for an event opcode.</summary>
		/// <returns>Returns a string that contains the localized name for an event opcode.</returns>
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x0005A05A File Offset: 0x0005825A
		public string DisplayName
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the non-localized name for an event opcode.</summary>
		/// <returns>Returns a string that contains the non-localized name for an event opcode.</returns>
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x0005A05A File Offset: 0x0005825A
		public string Name
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the numeric value associated with the event opcode.</summary>
		/// <returns>Returns an integer value.</returns>
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x0005AA28 File Offset: 0x00058C28
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
