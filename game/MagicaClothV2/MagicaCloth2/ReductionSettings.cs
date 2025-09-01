using System;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x020000AA RID: 170
	[Serializable]
	public class ReductionSettings : IDataValidate
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00005302 File Offset: 0x00003502
		public bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0001AE66 File Offset: 0x00019066
		public float GetMaxConnectionDistance()
		{
			return math.max(math.max(0.001f, this.simpleDistance), this.shapeDistance);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0001AE83 File Offset: 0x00019083
		public ReductionSettings Clone()
		{
			return new ReductionSettings
			{
				simpleDistance = this.simpleDistance,
				shapeDistance = this.shapeDistance
			};
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0001AEA2 File Offset: 0x000190A2
		public void DataValidate()
		{
			this.simpleDistance = Mathf.Clamp(this.simpleDistance, 0f, 0.1f);
			this.shapeDistance = Mathf.Clamp(this.shapeDistance, 0f, 0.1f);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0001AEDA File Offset: 0x000190DA
		public override int GetHashCode()
		{
			return 0 + this.simpleDistance.GetHashCode() + this.shapeDistance.GetHashCode();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0001AEF8 File Offset: 0x000190F8
		public override string ToString()
		{
			return string.Format("ReductionSettings. sameDist:{0}, simpleDist:{1}, shapeDist:{2} maxStep:{3}", new object[]
			{
				0.001f,
				this.simpleDistance,
				this.shapeDistance,
				100
			});
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00002058 File Offset: 0x00000258
		public ReductionSettings()
		{
		}

		// Token: 0x04000564 RID: 1380
		[Range(0f, 0.1f)]
		public float simpleDistance;

		// Token: 0x04000565 RID: 1381
		[Range(0f, 0.1f)]
		public float shapeDistance;
	}
}
