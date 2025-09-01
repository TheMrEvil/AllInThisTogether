using System;
using UnityEngine;
using UnityEngine.UI;

namespace DamageNumbersPro.Demo
{
	// Token: 0x0200001B RID: 27
	public class DNP_SineFadeText : MonoBehaviour
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00007BE1 File Offset: 0x00005DE1
		private void Awake()
		{
			this.text = base.GetComponent<Text>();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00007BF0 File Offset: 0x00005DF0
		private void FixedUpdate()
		{
			Color color = this.text.color;
			color.a = this.fromAlpha + (this.toAlpha - this.fromAlpha) * (Mathf.Sin(this.speed * Time.unscaledTime + this.startTimeBonus) * 0.5f + 0.5f);
			this.text.color = color;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00007C55 File Offset: 0x00005E55
		public DNP_SineFadeText()
		{
		}

		// Token: 0x04000163 RID: 355
		public float fromAlpha = 0.5f;

		// Token: 0x04000164 RID: 356
		public float toAlpha = 0.8f;

		// Token: 0x04000165 RID: 357
		public float speed = 4f;

		// Token: 0x04000166 RID: 358
		public float startTimeBonus;

		// Token: 0x04000167 RID: 359
		private Text text;
	}
}
