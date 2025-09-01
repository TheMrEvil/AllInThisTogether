using System;
using UnityEngine;

// Token: 0x0200012B RID: 299
public class SimpleRotate : MonoBehaviour
{
	// Token: 0x06000E01 RID: 3585 RVA: 0x000598B5 File Offset: 0x00057AB5
	private void Update()
	{
		base.transform.Rotate(this.axis, this.degPerSecond * Time.deltaTime, Space.Self);
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x000598D5 File Offset: 0x00057AD5
	public void ChangeRotSpeed(float newDegPerSecond)
	{
		this.degPerSecond = newDegPerSecond;
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x000598DE File Offset: 0x00057ADE
	public SimpleRotate()
	{
	}

	// Token: 0x04000B78 RID: 2936
	[SerializeField]
	private Vector3 axis;

	// Token: 0x04000B79 RID: 2937
	[SerializeField]
	private float degPerSecond;
}
