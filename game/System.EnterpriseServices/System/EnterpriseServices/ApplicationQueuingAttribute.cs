using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Enables queuing support for the marked assembly and enables the application to read method calls from Message Queuing queues. This class cannot be inherited.</summary>
	// Token: 0x02000010 RID: 16
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Assembly)]
	public sealed class ApplicationQueuingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ApplicationQueuingAttribute" /> class, enabling queuing support for the assembly and initializing <see cref="P:System.EnterpriseServices.ApplicationQueuingAttribute.Enabled" />, <see cref="P:System.EnterpriseServices.ApplicationQueuingAttribute.QueueListenerEnabled" />, and <see cref="P:System.EnterpriseServices.ApplicationQueuingAttribute.MaxListenerThreads" />.</summary>
		// Token: 0x0600002E RID: 46 RVA: 0x0000216A File Offset: 0x0000036A
		public ApplicationQueuingAttribute()
		{
			this.enabled = true;
			this.queueListenerEnabled = false;
			this.maxListenerThreads = 0;
		}

		/// <summary>Gets or sets a value indicating whether queuing support is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if queuing support is enabled; otherwise, <see langword="false" />. The default value set by the constructor is <see langword="true" />.</returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002187 File Offset: 0x00000387
		// (set) Token: 0x06000030 RID: 48 RVA: 0x0000218F File Offset: 0x0000038F
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		/// <summary>Gets or sets the number of threads used to extract messages from the queue and activate the corresponding component.</summary>
		/// <returns>The maximum number of threads to use for processing messages arriving in the queue. The default is zero.</returns>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002198 File Offset: 0x00000398
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000021A0 File Offset: 0x000003A0
		public int MaxListenerThreads
		{
			get
			{
				return this.maxListenerThreads;
			}
			set
			{
				this.maxListenerThreads = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the application will accept queued component calls from clients.</summary>
		/// <returns>
		///   <see langword="true" /> if the application accepts queued component calls; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000021A9 File Offset: 0x000003A9
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000021B1 File Offset: 0x000003B1
		public bool QueueListenerEnabled
		{
			get
			{
				return this.queueListenerEnabled;
			}
			set
			{
				this.queueListenerEnabled = value;
			}
		}

		// Token: 0x0400003A RID: 58
		private bool enabled;

		// Token: 0x0400003B RID: 59
		private int maxListenerThreads;

		// Token: 0x0400003C RID: 60
		private bool queueListenerEnabled;
	}
}
