using System;
using UnityEngine;

// Token: 0x02000248 RID: 584
[ExecuteInEditMode]
public class CameraDepth : MonoBehaviour
{
	// Token: 0x060017B4 RID: 6068 RVA: 0x00094BC5 File Offset: 0x00092DC5
	private void OnEnable()
	{
		base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.DepthNormals;
	}

	// Token: 0x060017B5 RID: 6069 RVA: 0x00094BD3 File Offset: 0x00092DD3
	public CameraDepth()
	{
	}
}
