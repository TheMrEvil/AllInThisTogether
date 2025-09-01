using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies which event is raised on initialization. This class cannot be inherited.</summary>
	// Token: 0x02000373 RID: 883
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class InitializationEventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InitializationEventAttribute" /> class.</summary>
		/// <param name="eventName">The name of the initialization event.</param>
		// Token: 0x06001D26 RID: 7462 RVA: 0x0006853F File Offset: 0x0006673F
		public InitializationEventAttribute(string eventName)
		{
			this.EventName = eventName;
		}

		/// <summary>Gets the name of the initialization event.</summary>
		/// <returns>The name of the initialization event.</returns>
		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x0006854E File Offset: 0x0006674E
		public string EventName
		{
			[CompilerGenerated]
			get
			{
				return this.<EventName>k__BackingField;
			}
		}

		// Token: 0x04000EC1 RID: 3777
		[CompilerGenerated]
		private readonly string <EventName>k__BackingField;
	}
}
