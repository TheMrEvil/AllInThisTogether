using System;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public class CheckSliderSerializeData
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002058 File Offset: 0x00000258
		public CheckSliderSerializeData()
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002060 File Offset: 0x00000260
		public CheckSliderSerializeData(bool use, float value)
		{
			this.use = use;
			this.value = value;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002076 File Offset: 0x00000276
		public float GetValue(float unusedValue)
		{
			if (!this.use)
			{
				return unusedValue;
			}
			return this.value;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002088 File Offset: 0x00000288
		public void SetValue(bool use, float value)
		{
			this.use = use;
			this.value = value;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002098 File Offset: 0x00000298
		public void DataValidate(float min, float max)
		{
			this.value = Mathf.Clamp(this.value, min, max);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020AD File Offset: 0x000002AD
		public CheckSliderSerializeData Clone()
		{
			return new CheckSliderSerializeData
			{
				value = this.value,
				use = this.use
			};
		}

		// Token: 0x04000001 RID: 1
		public float value;

		// Token: 0x04000002 RID: 2
		public bool use;
	}
}
