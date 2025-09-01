using System;
using UnityEngine;

namespace QFSW.QC
{
	// Token: 0x02000030 RID: 48
	public class QuantumLocalization : ScriptableObject
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00006BE4 File Offset: 0x00004DE4
		public QuantumLocalization()
		{
		}

		// Token: 0x040000E3 RID: 227
		[SerializeField]
		public string Loading = "Loading...";

		// Token: 0x040000E4 RID: 228
		[SerializeField]
		public string ExecutingAsyncCommand = "Executing async command...";

		// Token: 0x040000E5 RID: 229
		[SerializeField]
		public string EnterCommand = "Enter Command...";

		// Token: 0x040000E6 RID: 230
		[SerializeField]
		public string CommandError = "Error";

		// Token: 0x040000E7 RID: 231
		[SerializeField]
		public string ConsoleError = "Vellum Processor Error";

		// Token: 0x040000E8 RID: 232
		[SerializeField]
		public string MaxLogSizeExceeded = "Log of size {0} exceeded the maximum log size of {1}";

		// Token: 0x040000E9 RID: 233
		[SerializeField]
		[TextArea]
		public string InitializationProgress = "T:\\>Vellum Console is initializing\nT:\\>Command generation in progress\nT:\\>{0} commands have been loaded";

		// Token: 0x040000EA RID: 234
		[SerializeField]
		[TextArea]
		public string InitializationComplete = "T:\\>Vellum Console ready";
	}
}
