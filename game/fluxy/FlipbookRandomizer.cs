using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class FlipbookRandomizer : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void Start()
	{
		this.mpb = new MaterialPropertyBlock();
		for (int i = 0; i < this.amount; i++)
		{
			Vector2 vector = UnityEngine.Random.insideUnitCircle * this.radius;
			MeshRenderer component = UnityEngine.Object.Instantiate<GameObject>(this.flipbook, new Vector3(vector.x, base.transform.position.y, vector.y), Quaternion.identity).GetComponent<MeshRenderer>();
			if (component != null)
			{
				component.GetPropertyBlock(this.mpb);
				this.mpb.SetFloat("_PlaybackOffset", UnityEngine.Random.value * 2f);
				this.mpb.SetFloat("_PlaybackSpeed", 0.5f + UnityEngine.Random.value);
				component.SetPropertyBlock(this.mpb);
			}
		}
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000211E File Offset: 0x0000031E
	public FlipbookRandomizer()
	{
	}

	// Token: 0x04000001 RID: 1
	public GameObject flipbook;

	// Token: 0x04000002 RID: 2
	public float radius = 5f;

	// Token: 0x04000003 RID: 3
	public int amount = 10;

	// Token: 0x04000004 RID: 4
	private MaterialPropertyBlock mpb;
}
