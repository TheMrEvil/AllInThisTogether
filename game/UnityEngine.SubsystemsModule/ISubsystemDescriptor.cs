using System;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	public interface ISubsystemDescriptor
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30
		string id { get; }

		// Token: 0x0600001F RID: 31
		ISubsystem Create();
	}
}
