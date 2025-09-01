using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200023B RID: 571
public class RootBoneTransfer : MonoBehaviour
{
	// Token: 0x0600175C RID: 5980 RVA: 0x0009375C File Offset: 0x0009195C
	private void Awake()
	{
		this.mr = base.GetComponent<SkinnedMeshRenderer>();
	}

	// Token: 0x0600175D RID: 5981 RVA: 0x0009376A File Offset: 0x0009196A
	private void Transfer()
	{
		this.TransferRoot(this.WantRootBone);
	}

	// Token: 0x0600175E RID: 5982 RVA: 0x00093778 File Offset: 0x00091978
	public void TransferRoot(Transform t)
	{
		if (this.mr == null)
		{
			this.mr = base.GetComponent<SkinnedMeshRenderer>();
		}
		Dictionary<string, Transform> dictionary = new Dictionary<string, Transform>();
		foreach (Transform transform in t.GetComponentsInChildren<Transform>())
		{
			dictionary[transform.name] = transform;
		}
		Transform[] array = new Transform[this.mr.bones.Length];
		for (int j = 0; j < this.mr.bones.Length; j++)
		{
			Transform transform2;
			if (dictionary.TryGetValue(this.mr.bones[j].name, out transform2))
			{
				array[j] = transform2;
			}
		}
		this.mr.bones = array;
	}

	// Token: 0x0600175F RID: 5983 RVA: 0x0009382C File Offset: 0x00091A2C
	public RootBoneTransfer()
	{
	}

	// Token: 0x04001713 RID: 5907
	[SerializeField]
	private Transform WantRootBone;

	// Token: 0x04001714 RID: 5908
	private SkinnedMeshRenderer mr;
}
