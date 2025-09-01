using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001A0 RID: 416
	[UsedByNativeCode]
	[Serializable]
	public struct BoneWeight1 : IEquatable<BoneWeight1>
	{
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x00015F40 File Offset: 0x00014140
		// (set) Token: 0x060010A9 RID: 4265 RVA: 0x00015F58 File Offset: 0x00014158
		public float weight
		{
			get
			{
				return this.m_Weight;
			}
			set
			{
				this.m_Weight = value;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x00015F64 File Offset: 0x00014164
		// (set) Token: 0x060010AB RID: 4267 RVA: 0x00015F7C File Offset: 0x0001417C
		public int boneIndex
		{
			get
			{
				return this.m_BoneIndex;
			}
			set
			{
				this.m_BoneIndex = value;
			}
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00015F88 File Offset: 0x00014188
		public override bool Equals(object other)
		{
			return other is BoneWeight1 && this.Equals((BoneWeight1)other);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00015FB4 File Offset: 0x000141B4
		public bool Equals(BoneWeight1 other)
		{
			return this.boneIndex.Equals(other.boneIndex) && this.weight.Equals(other.weight);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00015FF8 File Offset: 0x000141F8
		public override int GetHashCode()
		{
			return this.boneIndex.GetHashCode() ^ this.weight.GetHashCode();
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00016028 File Offset: 0x00014228
		public static bool operator ==(BoneWeight1 lhs, BoneWeight1 rhs)
		{
			return lhs.boneIndex == rhs.boneIndex && lhs.weight == rhs.weight;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00016060 File Offset: 0x00014260
		public static bool operator !=(BoneWeight1 lhs, BoneWeight1 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040005B6 RID: 1462
		[SerializeField]
		private float m_Weight;

		// Token: 0x040005B7 RID: 1463
		[SerializeField]
		private int m_BoneIndex;
	}
}
