using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains the metadata (properties and settings) for an event that is defined in an event provider. </summary>
	// Token: 0x020003B1 RID: 945
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventMetadata
	{
		// Token: 0x06001C34 RID: 7220 RVA: 0x0000235B File Offset: 0x0000055B
		internal EventMetadata()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the description template associated with the event using the current thread locale for the description language.</summary>
		/// <returns>Returns a string that contains the description template associated with the event.</returns>
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001C35 RID: 7221 RVA: 0x0005A05A File Offset: 0x0005825A
		public string Description
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the identifier of the event that is defined in the event provider.</summary>
		/// <returns>Returns a <see langword="long" /> value that is the event identifier.</returns>
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001C36 RID: 7222 RVA: 0x0005A9F0 File Offset: 0x00058BF0
		public long Id
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Gets the keywords associated with the event that is defined in the even provider.</summary>
		/// <returns>Returns an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventKeyword" /> objects.</returns>
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001C37 RID: 7223 RVA: 0x0005A6D7 File Offset: 0x000588D7
		public IEnumerable<EventKeyword> Keywords
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the level associated with the event that is defined in the event provider. The level defines the severity of the event.</summary>
		/// <returns>Returns an <see cref="T:System.Diagnostics.Eventing.Reader.EventLevel" /> object.</returns>
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0005A05A File Offset: 0x0005825A
		public EventLevel Level
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a link to the event log that receives this event when the provider publishes this event.</summary>
		/// <returns>Returns a <see cref="T:System.Diagnostics.Eventing.Reader.EventLogLink" /> object.</returns>
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x0005A05A File Offset: 0x0005825A
		public EventLogLink LogLink
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the opcode associated with this event that is defined by an event provider. The opcode defines a numeric value that identifies the activity or a point within an activity that the application was performing when it raised the event.</summary>
		/// <returns>Returns a <see cref="T:System.Diagnostics.Eventing.Reader.EventOpcode" /> object.</returns>
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x0005A05A File Offset: 0x0005825A
		public EventOpcode Opcode
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the task associated with the event. A task identifies a portion of an application or a component that publishes an event. </summary>
		/// <returns>Returns a <see cref="T:System.Diagnostics.Eventing.Reader.EventTask" /> object.</returns>
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001C3B RID: 7227 RVA: 0x0005A05A File Offset: 0x0005825A
		public EventTask Task
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the template string for the event. Templates are used to describe data that is used by a provider when an event is published. Templates optionally specify XML that provides the structure of an event. The XML allows values that the event publisher provides to be inserted during the rendering of an event.</summary>
		/// <returns>Returns a string that contains the template for the event.</returns>
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x0005A05A File Offset: 0x0005825A
		public string Template
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the version of the event that qualifies the event identifier.</summary>
		/// <returns>Returns a byte value.</returns>
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001C3D RID: 7229 RVA: 0x0005AA0C File Offset: 0x00058C0C
		public byte Version
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}
	}
}
