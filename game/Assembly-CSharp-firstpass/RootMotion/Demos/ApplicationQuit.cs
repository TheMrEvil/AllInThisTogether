using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000161 RID: 353
	public class ApplicationQuit : MonoBehaviour
	{
		// Token: 0x06000D9C RID: 3484 RVA: 0x0005C4A7 File Offset: 0x0005A6A7
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0005C4C0 File Offset: 0x0005A6C0
		public ApplicationQuit()
		{
		}
	}
}
