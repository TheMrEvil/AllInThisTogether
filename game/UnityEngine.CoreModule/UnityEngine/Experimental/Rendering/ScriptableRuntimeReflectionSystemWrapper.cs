using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000473 RID: 1139
	[RequiredByNativeCode]
	internal class ScriptableRuntimeReflectionSystemWrapper
	{
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600283A RID: 10298 RVA: 0x0004305F File Offset: 0x0004125F
		// (set) Token: 0x0600283B RID: 10299 RVA: 0x00043067 File Offset: 0x00041267
		internal IScriptableRuntimeReflectionSystem implementation
		{
			[CompilerGenerated]
			get
			{
				return this.<implementation>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<implementation>k__BackingField = value;
			}
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x00043070 File Offset: 0x00041270
		[RequiredByNativeCode]
		private void Internal_ScriptableRuntimeReflectionSystemWrapper_TickRealtimeProbes(out bool result)
		{
			result = (this.implementation != null && this.implementation.TickRealtimeProbes());
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x00002072 File Offset: 0x00000272
		public ScriptableRuntimeReflectionSystemWrapper()
		{
		}

		// Token: 0x04000ECB RID: 3787
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IScriptableRuntimeReflectionSystem <implementation>k__BackingField;
	}
}
