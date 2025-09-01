using System;

namespace System.Reflection
{
	/// <summary>Specifies the attributes of an event.</summary>
	// Token: 0x02000898 RID: 2200
	[Flags]
	public enum EventAttributes
	{
		/// <summary>Specifies that the event has no attributes.</summary>
		// Token: 0x04002E87 RID: 11911
		None = 0,
		/// <summary>Specifies that the event is special in a way described by the name.</summary>
		// Token: 0x04002E88 RID: 11912
		SpecialName = 512,
		/// <summary>Specifies that the common language runtime should check name encoding.</summary>
		// Token: 0x04002E89 RID: 11913
		RTSpecialName = 1024,
		/// <summary>Specifies a reserved flag for common language runtime use only.</summary>
		// Token: 0x04002E8A RID: 11914
		ReservedMask = 1024
	}
}
