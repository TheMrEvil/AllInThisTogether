using System;
using UnityEngine;

// Token: 0x02000239 RID: 569
public class RandomRotation : MonoBehaviour
{
	// Token: 0x06001758 RID: 5976 RVA: 0x00093668 File Offset: 0x00091868
	private void Start()
	{
		base.transform.localRotation = Quaternion.Euler(UnityEngine.Random.Range(this.MinRotation.x, this.MaxRotation.x), UnityEngine.Random.Range(this.MinRotation.y, this.MaxRotation.y), UnityEngine.Random.Range(this.MinRotation.z, this.MaxRotation.z));
	}

	// Token: 0x06001759 RID: 5977 RVA: 0x000936D6 File Offset: 0x000918D6
	public RandomRotation()
	{
	}

	// Token: 0x04001710 RID: 5904
	public Vector3 MinRotation;

	// Token: 0x04001711 RID: 5905
	public Vector3 MaxRotation;
}
