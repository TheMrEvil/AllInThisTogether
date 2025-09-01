using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000130 RID: 304
public class TransitionScene : MonoBehaviour
{
	// Token: 0x06000E25 RID: 3621 RVA: 0x0005A1CB File Offset: 0x000583CB
	public void LoadSceneAdditive()
	{
		SceneManager.LoadScene(this.SceneName, LoadSceneMode.Additive);
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x0005A1D9 File Offset: 0x000583D9
	public TransitionScene()
	{
	}

	// Token: 0x04000B9A RID: 2970
	public string SceneName;
}
