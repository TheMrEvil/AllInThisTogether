using System;
using UnityEngine.SceneManagement;

namespace Photon.Pun
{
	// Token: 0x0200001B RID: 27
	public class SceneManagerHelper
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00008E5C File Offset: 0x0000705C
		public static string ActiveSceneName
		{
			get
			{
				return SceneManager.GetActiveScene().name;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00008E78 File Offset: 0x00007078
		public static int ActiveSceneBuildIndex
		{
			get
			{
				return SceneManager.GetActiveScene().buildIndex;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00008E92 File Offset: 0x00007092
		public SceneManagerHelper()
		{
		}
	}
}
