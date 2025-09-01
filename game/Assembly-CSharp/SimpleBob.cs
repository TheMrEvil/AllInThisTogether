using System;
using UnityEngine;

// Token: 0x02000128 RID: 296
public class SimpleBob : MonoBehaviour
{
	// Token: 0x06000DF9 RID: 3577 RVA: 0x000596FE File Offset: 0x000578FE
	private void Start()
	{
		this.startPos = base.transform.position;
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x00059714 File Offset: 0x00057914
	private void Update()
	{
		if (this.relativeToParent)
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, this.startPos.y + this.bobAmount * Mathf.Sin((Time.time + this.bobOffset) * this.bobSpeed), base.transform.localPosition.z);
			return;
		}
		base.transform.position = new Vector3(this.startPos.x, this.startPos.y + this.bobAmount * Mathf.Sin((Time.time + this.bobOffset) * this.bobSpeed), this.startPos.z);
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x000597D6 File Offset: 0x000579D6
	public SimpleBob()
	{
	}

	// Token: 0x04000B6F RID: 2927
	[SerializeField]
	private float bobAmount;

	// Token: 0x04000B70 RID: 2928
	[SerializeField]
	private float bobSpeed;

	// Token: 0x04000B71 RID: 2929
	[SerializeField]
	private float bobOffset;

	// Token: 0x04000B72 RID: 2930
	[Tooltip("Buggy! Apparently.")]
	[SerializeField]
	private bool relativeToParent;

	// Token: 0x04000B73 RID: 2931
	private Vector3 startPos;
}
