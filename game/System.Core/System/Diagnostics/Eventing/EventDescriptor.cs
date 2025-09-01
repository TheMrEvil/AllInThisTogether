using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing
{
	/// <summary>Contains the metadata that defines an event.</summary>
	// Token: 0x02000393 RID: 915
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct EventDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.EventDescriptor" /> class.</summary>
		/// <param name="id">The event identifier.</param>
		/// <param name="version">Version of the event. The version indicates a revision to the event definition. You can use this member and the Id member to identify a unique event.</param>
		/// <param name="channel">Defines a potential target for the event.</param>
		/// <param name="level">Specifies the level of detail included in the event.</param>
		/// <param name="opcode">Operation being performed at the time the event is written.</param>
		/// <param name="task">Identifies a logical component of the application that is writing the event.</param>
		/// <param name="keywords">Bit mask that specifies the event category. The keyword can contain one or more provider-defined keywords, standard keywords, or both.</param>
		// Token: 0x06001B50 RID: 6992 RVA: 0x0000235B File Offset: 0x0000055B
		public EventDescriptor(int id, byte version, byte channel, byte level, byte opcode, int task, long keywords)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Retrieves the channel value from the event descriptor.</summary>
		/// <returns>The channel that defines a potential target for the event.</returns>
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x0005A33C File Offset: 0x0005853C
		public byte Channel
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Retrieves the event identifier value from the event descriptor.</summary>
		/// <returns>The event identifier.</returns>
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x0005A358 File Offset: 0x00058558
		public int EventId
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Retrieves the keyword value from the event descriptor.</summary>
		/// <returns>The keyword, which is a bit mask, that specifies the event category.</returns>
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x0005A374 File Offset: 0x00058574
		public long Keywords
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Retrieves the level value from the event descriptor.</summary>
		/// <returns>The level of detail included in the event.</returns>
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x0005A390 File Offset: 0x00058590
		public byte Level
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Retrieves the operation code value from the event descriptor.</summary>
		/// <returns>The operation being performed at the time the event is written.</returns>
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x0005A3AC File Offset: 0x000585AC
		public byte Opcode
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Retrieves the task value from the event descriptor.</summary>
		/// <returns>The task that identifies the logical component of the application that is writing the event.</returns>
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x0005A3C8 File Offset: 0x000585C8
		public int Task
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Retrieves the version value from the event descriptor.</summary>
		/// <returns>The version of the event. </returns>
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x0005A3E4 File Offset: 0x000585E4
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
