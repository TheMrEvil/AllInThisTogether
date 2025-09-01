using System;
using UnityEngine;

// Token: 0x0200035D RID: 861
[Serializable]
public class SceneField
{
	// Token: 0x17000199 RID: 409
	// (get) Token: 0x06001CB5 RID: 7349 RVA: 0x000AEB15 File Offset: 0x000ACD15
	public string SceneName
	{
		get
		{
			return this.m_SceneName;
		}
	}

	// Token: 0x06001CB6 RID: 7350 RVA: 0x000AEB1D File Offset: 0x000ACD1D
	public static implicit operator string(SceneField sceneField)
	{
		return sceneField.SceneName;
	}

	// Token: 0x06001CB7 RID: 7351 RVA: 0x000AEB25 File Offset: 0x000ACD25
	public SceneField()
	{
	}

	// Token: 0x04001D6E RID: 7534
	[SerializeField]
	private UnityEngine.Object m_SceneAsset;

	// Token: 0x04001D6F RID: 7535
	[SerializeField]
	private string m_SceneName = "";
}
