using System;
using UnityEngine;

namespace VolumetricLightsDemo
{
	// Token: 0x02000029 RID: 41
	public class FenceGen : MonoBehaviour
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00008410 File Offset: 0x00006610
		private void Start()
		{
			this.pos = base.transform.position;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00008424 File Offset: 0x00006624
		private void Update()
		{
			if (Time.time - this.last < this.delay)
			{
				return;
			}
			this.last = Time.time;
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
			gameObject.transform.position = this.pos;
			this.pos += this.step;
			gameObject.transform.localScale = new Vector3(1f, 4f, 1f);
			int num = this.count - 1;
			this.count = num;
			if (num < 0)
			{
				UnityEngine.Object.Destroy(this);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000084B7 File Offset: 0x000066B7
		public FenceGen()
		{
		}

		// Token: 0x0400016A RID: 362
		public int count;

		// Token: 0x0400016B RID: 363
		public float delay = 0.1f;

		// Token: 0x0400016C RID: 364
		public Vector3 step = new Vector3(0f, 0f, -2f);

		// Token: 0x0400016D RID: 365
		private float last;

		// Token: 0x0400016E RID: 366
		private Vector3 pos;
	}
}
