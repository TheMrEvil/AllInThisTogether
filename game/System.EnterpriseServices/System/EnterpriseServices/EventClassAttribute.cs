using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Marks the attributed class as an event class. This class cannot be inherited.</summary>
	// Token: 0x0200001B RID: 27
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class EventClassAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.EventClassAttribute" /> class.</summary>
		// Token: 0x0600005F RID: 95 RVA: 0x000022A8 File Offset: 0x000004A8
		public EventClassAttribute()
		{
			this.allowInProcSubscribers = true;
			this.fireInParallel = false;
			this.publisherFilter = null;
		}

		/// <summary>Gets or sets a value that indicates whether subscribers can be activated in the publisher's process.</summary>
		/// <returns>
		///   <see langword="true" /> if subscribers can be activated in the publisher's process; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000022C5 File Offset: 0x000004C5
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000022CD File Offset: 0x000004CD
		public bool AllowInprocSubscribers
		{
			get
			{
				return this.allowInProcSubscribers;
			}
			set
			{
				this.allowInProcSubscribers = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether events are to be delivered to subscribers in parallel.</summary>
		/// <returns>
		///   <see langword="true" /> if events are to be delivered to subscribers in parallel; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000022D6 File Offset: 0x000004D6
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000022DE File Offset: 0x000004DE
		public bool FireInParallel
		{
			get
			{
				return this.fireInParallel;
			}
			set
			{
				this.fireInParallel = value;
			}
		}

		/// <summary>Gets or sets a publisher filter for an event method.</summary>
		/// <returns>The publisher filter.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000022E7 File Offset: 0x000004E7
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000022EF File Offset: 0x000004EF
		public string PublisherFilter
		{
			get
			{
				return this.publisherFilter;
			}
			set
			{
				this.publisherFilter = value;
			}
		}

		// Token: 0x04000050 RID: 80
		private bool allowInProcSubscribers;

		// Token: 0x04000051 RID: 81
		private bool fireInParallel;

		// Token: 0x04000052 RID: 82
		private string publisherFilter;
	}
}
