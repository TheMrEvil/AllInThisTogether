using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Defines the access modes for a dynamic assembly.</summary>
	// Token: 0x02000914 RID: 2324
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum AssemblyBuilderAccess
	{
		/// <summary>The dynamic assembly can be executed, but not saved.</summary>
		// Token: 0x04003114 RID: 12564
		Run = 1,
		/// <summary>The dynamic assembly can be saved, but not executed.</summary>
		// Token: 0x04003115 RID: 12565
		Save = 2,
		/// <summary>The dynamic assembly can be executed and saved.</summary>
		// Token: 0x04003116 RID: 12566
		RunAndSave = 3,
		/// <summary>The dynamic assembly is loaded into the reflection-only context, and cannot be executed.</summary>
		// Token: 0x04003117 RID: 12567
		ReflectionOnly = 6,
		/// <summary>The dynamic assembly will be automatically unloaded and its memory reclaimed, when it's no longer accessible.</summary>
		// Token: 0x04003118 RID: 12568
		RunAndCollect = 9
	}
}
