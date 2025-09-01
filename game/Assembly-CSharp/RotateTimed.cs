using System;
using UnityEngine;

// Token: 0x0200023C RID: 572
public class RotateTimed : MonoBehaviour
{
	// Token: 0x06001760 RID: 5984 RVA: 0x00093834 File Offset: 0x00091A34
	private void Awake()
	{
		this.cgroup = base.GetComponent<CanvasGroup>();
	}

	// Token: 0x06001761 RID: 5985 RVA: 0x00093842 File Offset: 0x00091A42
	private void Update()
	{
		if (this.cgroup != null && this.cgroup.alpha <= 0f)
		{
			return;
		}
		base.transform.Rotate(this.rotation * Time.deltaTime);
	}

	// Token: 0x06001762 RID: 5986 RVA: 0x00093880 File Offset: 0x00091A80
	public RotateTimed()
	{
	}

	// Token: 0x04001715 RID: 5909
	public Vector3 rotation;

	// Token: 0x04001716 RID: 5910
	private CanvasGroup cgroup;
}
