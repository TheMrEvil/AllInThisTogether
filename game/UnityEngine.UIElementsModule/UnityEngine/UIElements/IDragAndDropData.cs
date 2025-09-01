using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AC RID: 428
	internal interface IDragAndDropData
	{
		// Token: 0x06000E1B RID: 3611
		object GetGenericData(string key);

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000E1C RID: 3612
		object userData { get; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000E1D RID: 3613
		IEnumerable<Object> unityObjectReferences { get; }
	}
}
