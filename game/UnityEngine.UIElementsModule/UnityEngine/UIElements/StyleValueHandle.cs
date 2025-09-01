using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B0 RID: 688
	[Serializable]
	internal struct StyleValueHandle
	{
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x00062414 File Offset: 0x00060614
		// (set) Token: 0x0600177E RID: 6014 RVA: 0x0006242C File Offset: 0x0006062C
		public StyleValueType valueType
		{
			get
			{
				return this.m_ValueType;
			}
			internal set
			{
				this.m_ValueType = value;
			}
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00062436 File Offset: 0x00060636
		internal StyleValueHandle(int valueIndex, StyleValueType valueType)
		{
			this.valueIndex = valueIndex;
			this.m_ValueType = valueType;
		}

		// Token: 0x04000A08 RID: 2568
		[SerializeField]
		private StyleValueType m_ValueType;

		// Token: 0x04000A09 RID: 2569
		[SerializeField]
		internal int valueIndex;
	}
}
