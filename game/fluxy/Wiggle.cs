using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class Wiggle : MonoBehaviour
{
	// Token: 0x06000012 RID: 18 RVA: 0x000023C3 File Offset: 0x000005C3
	private void Start()
	{
		this.initialPos = base.transform.position;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000023D8 File Offset: 0x000005D8
	private void Update()
	{
		float d = Mathf.Sin(this.offset + Time.time * this.speed) * this.amplitude;
		base.transform.position = this.initialPos + this.axis.normalized * d;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x0000242C File Offset: 0x0000062C
	public Wiggle()
	{
	}

	// Token: 0x04000011 RID: 17
	public Vector3 axis = Vector3.up;

	// Token: 0x04000012 RID: 18
	public float amplitude = 1f;

	// Token: 0x04000013 RID: 19
	public float speed = 1f;

	// Token: 0x04000014 RID: 20
	public float offset;

	// Token: 0x04000015 RID: 21
	private Vector3 initialPos;
}
