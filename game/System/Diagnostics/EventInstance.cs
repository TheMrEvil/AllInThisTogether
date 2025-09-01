using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Represents language-neutral information for an event log entry.</summary>
	// Token: 0x02000258 RID: 600
	public class EventInstance
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventInstance" /> class using the specified resource identifiers for the localized message and category text of the event entry.</summary>
		/// <param name="instanceId">A resource identifier that corresponds to a string defined in the message resource file of the event source.</param>
		/// <param name="categoryId">A resource identifier that corresponds to a string defined in the category resource file of the event source, or zero to specify no category for the event.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="instanceId" /> parameter is a negative value or a value larger than <see cref="F:System.UInt32.MaxValue" />.  
		///  -or-  
		///  The <paramref name="categoryId" /> parameter is a negative value or a value larger than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x06001278 RID: 4728 RVA: 0x0004FC04 File Offset: 0x0004DE04
		public EventInstance(long instanceId, int categoryId) : this(instanceId, categoryId, EventLogEntryType.Information)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventInstance" /> class using the specified resource identifiers for the localized message and category text of the event entry and the specified event log entry type.</summary>
		/// <param name="instanceId">A resource identifier that corresponds to a string defined in the message resource file of the event source.</param>
		/// <param name="categoryId">A resource identifier that corresponds to a string defined in the category resource file of the event source, or zero to specify no category for the event.</param>
		/// <param name="entryType">An <see cref="T:System.Diagnostics.EventLogEntryType" /> value that indicates the event type.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="entryType" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" /> value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="instanceId" /> is a negative value or a value larger than <see cref="F:System.UInt32.MaxValue" />.  
		/// -or-  
		/// <paramref name="categoryId" /> is a negative value or a value larger than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x06001279 RID: 4729 RVA: 0x0004FC0F File Offset: 0x0004DE0F
		public EventInstance(long instanceId, int categoryId, EventLogEntryType entryType)
		{
			this.InstanceId = instanceId;
			this.CategoryId = categoryId;
			this.EntryType = entryType;
		}

		/// <summary>Gets or sets the resource identifier that specifies the application-defined category of the event entry.</summary>
		/// <returns>A numeric category value or resource identifier that corresponds to a string defined in the category resource file of the event source. The default is zero, which signifies that no category will be displayed for the event entry.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a negative value or to a value larger than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x0004FC2C File Offset: 0x0004DE2C
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x0004FC34 File Offset: 0x0004DE34
		public int CategoryId
		{
			get
			{
				return this._categoryId;
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._categoryId = value;
			}
		}

		/// <summary>Gets or sets the event type of the event log entry.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.EventLogEntryType" /> value that indicates the event entry type. The default value is <see cref="F:System.Diagnostics.EventLogEntryType.Information" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property is not set to a valid <see cref="T:System.Diagnostics.EventLogEntryType" /> value.</exception>
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x0004FC54 File Offset: 0x0004DE54
		// (set) Token: 0x0600127D RID: 4733 RVA: 0x0004FC5C File Offset: 0x0004DE5C
		public EventLogEntryType EntryType
		{
			get
			{
				return this._entryType;
			}
			set
			{
				if (!Enum.IsDefined(typeof(EventLogEntryType), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(EventLogEntryType));
				}
				this._entryType = value;
			}
		}

		/// <summary>Gets or sets the resource identifier that designates the message text of the event entry.</summary>
		/// <returns>A resource identifier that corresponds to a string defined in the message resource file of the event source.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a negative value or to a value larger than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x0004FC92 File Offset: 0x0004DE92
		// (set) Token: 0x0600127F RID: 4735 RVA: 0x0004FC9A File Offset: 0x0004DE9A
		public long InstanceId
		{
			get
			{
				return this._instanceId;
			}
			set
			{
				if (value < 0L || value > (long)((ulong)-1))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._instanceId = value;
			}
		}

		// Token: 0x04000AA8 RID: 2728
		private int _categoryId;

		// Token: 0x04000AA9 RID: 2729
		private EventLogEntryType _entryType;

		// Token: 0x04000AAA RID: 2730
		private long _instanceId;
	}
}
