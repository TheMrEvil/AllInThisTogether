using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000028 RID: 40
	[VisibleToOtherModules]
	internal interface IBindingsPreventExecution
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007A RID: 122
		// (set) Token: 0x0600007B RID: 123
		object singleFlagValue { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007C RID: 124
		// (set) Token: 0x0600007D RID: 125
		PreventExecutionSeverity severity { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007E RID: 126
		// (set) Token: 0x0600007F RID: 127
		string howToFix { get; set; }
	}
}
