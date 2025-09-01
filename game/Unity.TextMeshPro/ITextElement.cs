using System;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200005A RID: 90
	public interface ITextElement
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600042E RID: 1070
		Material sharedMaterial { get; }

		// Token: 0x0600042F RID: 1071
		void Rebuild(CanvasUpdate update);

		// Token: 0x06000430 RID: 1072
		int GetInstanceID();
	}
}
