using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Sets the queuing exception class for the queued class. This class cannot be inherited.</summary>
	// Token: 0x0200001D RID: 29
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class ExceptionClassAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ExceptionClassAttribute" /> class.</summary>
		/// <param name="name">The name of the exception class for the player to activate and play back before the message is routed to the dead letter queue.</param>
		// Token: 0x06000069 RID: 105 RVA: 0x0000231E File Offset: 0x0000051E
		public ExceptionClassAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets the name of the exception class for the player to activate and play back before the message is routed to the dead letter queue.</summary>
		/// <returns>The name of the exception class for the player to activate and play back before the message is routed to the dead letter queue.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000232D File Offset: 0x0000052D
		public string Value
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000054 RID: 84
		private string name;
	}
}
