using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Encapsulates a single record in the event log. This class cannot be inherited.</summary>
	// Token: 0x0200025A RID: 602
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[Serializable]
	public sealed class EventLogEntry : Component, ISerializable
	{
		// Token: 0x060012C3 RID: 4803 RVA: 0x00050750 File Offset: 0x0004E950
		internal EventLogEntry(string category, short categoryNumber, int index, int eventID, string source, string message, string userName, string machineName, EventLogEntryType entryType, DateTime timeGenerated, DateTime timeWritten, byte[] data, string[] replacementStrings, long instanceId)
		{
			this.category = category;
			this.categoryNumber = categoryNumber;
			this.data = data;
			this.entryType = entryType;
			this.eventID = eventID;
			this.index = index;
			this.machineName = machineName;
			this.message = message;
			this.replacementStrings = replacementStrings;
			this.source = source;
			this.timeGenerated = timeGenerated;
			this.timeWritten = timeWritten;
			this.userName = userName;
			this.instanceId = instanceId;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x000507D0 File Offset: 0x0004E9D0
		[MonoTODO]
		private EventLogEntry(SerializationInfo info, StreamingContext context)
		{
		}

		/// <summary>Gets the text associated with the <see cref="P:System.Diagnostics.EventLogEntry.CategoryNumber" /> property for this entry.</summary>
		/// <returns>The application-specific category text.</returns>
		/// <exception cref="T:System.Exception">The space could not be allocated for one of the insertion strings associated with the category.</exception>
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x000507D8 File Offset: 0x0004E9D8
		[MonitoringDescription("The category of this event entry.")]
		public string Category
		{
			get
			{
				return this.category;
			}
		}

		/// <summary>Gets the category number of the event log entry.</summary>
		/// <returns>The application-specific category number for this entry.</returns>
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x000507E0 File Offset: 0x0004E9E0
		[MonitoringDescription("An ID for the category of this event entry.")]
		public short CategoryNumber
		{
			get
			{
				return this.categoryNumber;
			}
		}

		/// <summary>Gets the binary data associated with the entry.</summary>
		/// <returns>An array of bytes that holds the binary data associated with the entry.</returns>
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x000507E8 File Offset: 0x0004E9E8
		[MonitoringDescription("Binary data associated with this event entry.")]
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		/// <summary>Gets the event type of this entry.</summary>
		/// <returns>The event type that is associated with the entry in the event log.</returns>
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x000507F0 File Offset: 0x0004E9F0
		[MonitoringDescription("The type of this event entry.")]
		public EventLogEntryType EntryType
		{
			get
			{
				return this.entryType;
			}
		}

		/// <summary>Gets the application-specific event identifier for the current event entry.</summary>
		/// <returns>The application-specific identifier for the event message.</returns>
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x000507F8 File Offset: 0x0004E9F8
		[MonitoringDescription("An ID number for this event entry.")]
		[Obsolete("Use InstanceId")]
		public int EventID
		{
			get
			{
				return this.eventID;
			}
		}

		/// <summary>Gets the index of this entry in the event log.</summary>
		/// <returns>The index of this entry in the event log.</returns>
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x00050800 File Offset: 0x0004EA00
		[MonitoringDescription("Sequence numer of this event entry.")]
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets the resource identifier that designates the message text of the event entry.</summary>
		/// <returns>A resource identifier that corresponds to a string definition in the message resource file of the event source.</returns>
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x00050808 File Offset: 0x0004EA08
		[MonitoringDescription("The instance ID for this event entry.")]
		[ComVisible(false)]
		public long InstanceId
		{
			get
			{
				return this.instanceId;
			}
		}

		/// <summary>Gets the name of the computer on which this entry was generated.</summary>
		/// <returns>The name of the computer that contains the event log.</returns>
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x00050810 File Offset: 0x0004EA10
		[MonitoringDescription("The Computer on which this event entry occured.")]
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		/// <summary>Gets the localized message associated with this event entry.</summary>
		/// <returns>The formatted, localized text for the message. This includes associated replacement strings.</returns>
		/// <exception cref="T:System.Exception">The space could not be allocated for one of the insertion strings associated with the message.</exception>
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x00050818 File Offset: 0x0004EA18
		[MonitoringDescription("The message of this event entry.")]
		[Editor("System.ComponentModel.Design.BinaryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		/// <summary>Gets the replacement strings associated with the event log entry.</summary>
		/// <returns>An array that holds the replacement strings stored in the event entry.</returns>
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x00050820 File Offset: 0x0004EA20
		[MonitoringDescription("Application strings for this event entry.")]
		public string[] ReplacementStrings
		{
			get
			{
				return this.replacementStrings;
			}
		}

		/// <summary>Gets the name of the application that generated this event.</summary>
		/// <returns>The name registered with the event log as the source of this event.</returns>
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x00050828 File Offset: 0x0004EA28
		[MonitoringDescription("The source application of this event entry.")]
		public string Source
		{
			get
			{
				return this.source;
			}
		}

		/// <summary>Gets the local time at which this event was generated.</summary>
		/// <returns>The local time at which this event was generated.</returns>
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x00050830 File Offset: 0x0004EA30
		[MonitoringDescription("Generation time of this event entry.")]
		public DateTime TimeGenerated
		{
			get
			{
				return this.timeGenerated;
			}
		}

		/// <summary>Gets the local time at which this event was written to the log.</summary>
		/// <returns>The local time at which this event was written to the log.</returns>
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x00050838 File Offset: 0x0004EA38
		[MonitoringDescription("The time at which this event entry was written to the logfile.")]
		public DateTime TimeWritten
		{
			get
			{
				return this.timeWritten;
			}
		}

		/// <summary>Gets the name of the user who is responsible for this event.</summary>
		/// <returns>The security identifier (SID) that uniquely identifies a user or group.</returns>
		/// <exception cref="T:System.SystemException">Account information could not be obtained for the user's SID.</exception>
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00050840 File Offset: 0x0004EA40
		[MonitoringDescription("The name of a user associated with this event entry.")]
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		/// <summary>Performs a comparison between two event log entries.</summary>
		/// <param name="otherEntry">The <see cref="T:System.Diagnostics.EventLogEntry" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Diagnostics.EventLogEntry" /> objects are identical; otherwise, <see langword="false" />.</returns>
		// Token: 0x060012D3 RID: 4819 RVA: 0x00050848 File Offset: 0x0004EA48
		public bool Equals(EventLogEntry otherEntry)
		{
			return otherEntry == this || (otherEntry.Category == this.category && otherEntry.CategoryNumber == this.categoryNumber && otherEntry.Data.Equals(this.data) && otherEntry.EntryType == this.entryType && otherEntry.InstanceId == this.instanceId && otherEntry.Index == this.index && otherEntry.MachineName == this.machineName && otherEntry.Message == this.message && otherEntry.ReplacementStrings.Equals(this.replacementStrings) && otherEntry.Source == this.source && otherEntry.TimeGenerated.Equals(this.timeGenerated) && otherEntry.TimeWritten.Equals(this.timeWritten) && otherEntry.UserName == this.userName);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x060012D4 RID: 4820 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO("Needs serialization support")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal EventLogEntry()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000AB6 RID: 2742
		private string category;

		// Token: 0x04000AB7 RID: 2743
		private short categoryNumber;

		// Token: 0x04000AB8 RID: 2744
		private byte[] data;

		// Token: 0x04000AB9 RID: 2745
		private EventLogEntryType entryType;

		// Token: 0x04000ABA RID: 2746
		private int eventID;

		// Token: 0x04000ABB RID: 2747
		private int index;

		// Token: 0x04000ABC RID: 2748
		private string machineName;

		// Token: 0x04000ABD RID: 2749
		private string message;

		// Token: 0x04000ABE RID: 2750
		private string[] replacementStrings;

		// Token: 0x04000ABF RID: 2751
		private string source;

		// Token: 0x04000AC0 RID: 2752
		private DateTime timeGenerated;

		// Token: 0x04000AC1 RID: 2753
		private DateTime timeWritten;

		// Token: 0x04000AC2 RID: 2754
		private string userName;

		// Token: 0x04000AC3 RID: 2755
		private long instanceId;
	}
}
