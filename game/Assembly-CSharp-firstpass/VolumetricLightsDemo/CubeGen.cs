using System;
using UnityEngine;

namespace VolumetricLightsDemo
{
	// Token: 0x02000028 RID: 40
	public class CubeGen : MonoBehaviour
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00008360 File Offset: 0x00006560
		private void Update()
		{
			if (Time.time - this.last < this.delay)
			{
				return;
			}
			this.last = Time.time;
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
			gameObject.transform.position = base.transform.position;
			gameObject.transform.localScale = Vector3.one * UnityEngine.Random.Range(0.5f, 1.5f);
			gameObject.transform.forward = UnityEngine.Random.onUnitSphere;
			gameObject.AddComponent<Rigidbody>();
			int num = this.count - 1;
			this.count = num;
			if (num < 0)
			{
				UnityEngine.Object.Destroy(this);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000083FD File Offset: 0x000065FD
		public CubeGen()
		{
		}

		// Token: 0x04000167 RID: 359
		public int count;

		// Token: 0x04000168 RID: 360
		public float delay = 0.1f;

		// Token: 0x04000169 RID: 361
		private float last;
	}
}
