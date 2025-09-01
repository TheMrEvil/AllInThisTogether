using System;
using UnityEngine;

// Token: 0x0200022B RID: 555
public class FXGrounder : MonoBehaviour
{
	// Token: 0x06001726 RID: 5926 RVA: 0x000928FD File Offset: 0x00090AFD
	private void Awake()
	{
		this.Ground(base.transform.position);
	}

	// Token: 0x06001727 RID: 5927 RVA: 0x00092910 File Offset: 0x00090B10
	private void Update()
	{
		if (!this.Continuous)
		{
			return;
		}
		this.Ground(base.transform.position + Vector3.up * this.CheckDistance);
	}

	// Token: 0x06001728 RID: 5928 RVA: 0x00092944 File Offset: 0x00090B44
	private void Ground(Vector3 checkPoint)
	{
		Ray ray = new Ray(checkPoint, base.transform.forward);
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(ray, out raycastHit, this.MaxDist, this.rayMask))
		{
			if (Vector3.SqrMagnitude(base.transform.position - raycastHit.point) > 0.01f)
			{
				base.transform.position = raycastHit.point;
				return;
			}
		}
		else if (this.DestroyIfNoGround)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001729 RID: 5929 RVA: 0x000929CD File Offset: 0x00090BCD
	public FXGrounder()
	{
	}

	// Token: 0x040016ED RID: 5869
	public LayerMask rayMask;

	// Token: 0x040016EE RID: 5870
	public float MaxDist;

	// Token: 0x040016EF RID: 5871
	public bool DestroyIfNoGround;

	// Token: 0x040016F0 RID: 5872
	public bool Continuous;

	// Token: 0x040016F1 RID: 5873
	public float CheckDistance = 3f;
}
