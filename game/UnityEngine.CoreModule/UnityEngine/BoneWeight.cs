using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200019F RID: 415
	[UsedByNativeCode]
	[Serializable]
	public struct BoneWeight : IEquatable<BoneWeight>
	{
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x00015BE8 File Offset: 0x00013DE8
		// (set) Token: 0x06001094 RID: 4244 RVA: 0x00015C00 File Offset: 0x00013E00
		public float weight0
		{
			get
			{
				return this.m_Weight0;
			}
			set
			{
				this.m_Weight0 = value;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x00015C0C File Offset: 0x00013E0C
		// (set) Token: 0x06001096 RID: 4246 RVA: 0x00015C24 File Offset: 0x00013E24
		public float weight1
		{
			get
			{
				return this.m_Weight1;
			}
			set
			{
				this.m_Weight1 = value;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x00015C30 File Offset: 0x00013E30
		// (set) Token: 0x06001098 RID: 4248 RVA: 0x00015C48 File Offset: 0x00013E48
		public float weight2
		{
			get
			{
				return this.m_Weight2;
			}
			set
			{
				this.m_Weight2 = value;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x00015C54 File Offset: 0x00013E54
		// (set) Token: 0x0600109A RID: 4250 RVA: 0x00015C6C File Offset: 0x00013E6C
		public float weight3
		{
			get
			{
				return this.m_Weight3;
			}
			set
			{
				this.m_Weight3 = value;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x00015C78 File Offset: 0x00013E78
		// (set) Token: 0x0600109C RID: 4252 RVA: 0x00015C90 File Offset: 0x00013E90
		public int boneIndex0
		{
			get
			{
				return this.m_BoneIndex0;
			}
			set
			{
				this.m_BoneIndex0 = value;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x00015C9C File Offset: 0x00013E9C
		// (set) Token: 0x0600109E RID: 4254 RVA: 0x00015CB4 File Offset: 0x00013EB4
		public int boneIndex1
		{
			get
			{
				return this.m_BoneIndex1;
			}
			set
			{
				this.m_BoneIndex1 = value;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x00015CC0 File Offset: 0x00013EC0
		// (set) Token: 0x060010A0 RID: 4256 RVA: 0x00015CD8 File Offset: 0x00013ED8
		public int boneIndex2
		{
			get
			{
				return this.m_BoneIndex2;
			}
			set
			{
				this.m_BoneIndex2 = value;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x00015CE4 File Offset: 0x00013EE4
		// (set) Token: 0x060010A2 RID: 4258 RVA: 0x00015CFC File Offset: 0x00013EFC
		public int boneIndex3
		{
			get
			{
				return this.m_BoneIndex3;
			}
			set
			{
				this.m_BoneIndex3 = value;
			}
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00015D08 File Offset: 0x00013F08
		public override int GetHashCode()
		{
			return this.boneIndex0.GetHashCode() ^ this.boneIndex1.GetHashCode() << 2 ^ this.boneIndex2.GetHashCode() >> 2 ^ this.boneIndex3.GetHashCode() >> 1 ^ this.weight0.GetHashCode() << 5 ^ this.weight1.GetHashCode() << 4 ^ this.weight2.GetHashCode() >> 4 ^ this.weight3.GetHashCode() >> 3;
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00015DA0 File Offset: 0x00013FA0
		public override bool Equals(object other)
		{
			return other is BoneWeight && this.Equals((BoneWeight)other);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00015DCC File Offset: 0x00013FCC
		public bool Equals(BoneWeight other)
		{
			return this.boneIndex0.Equals(other.boneIndex0) && this.boneIndex1.Equals(other.boneIndex1) && this.boneIndex2.Equals(other.boneIndex2) && this.boneIndex3.Equals(other.boneIndex3) && new Vector4(this.weight0, this.weight1, this.weight2, this.weight3).Equals(new Vector4(other.weight0, other.weight1, other.weight2, other.weight3));
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00015E88 File Offset: 0x00014088
		public static bool operator ==(BoneWeight lhs, BoneWeight rhs)
		{
			return lhs.boneIndex0 == rhs.boneIndex0 && lhs.boneIndex1 == rhs.boneIndex1 && lhs.boneIndex2 == rhs.boneIndex2 && lhs.boneIndex3 == rhs.boneIndex3 && new Vector4(lhs.weight0, lhs.weight1, lhs.weight2, lhs.weight3) == new Vector4(rhs.weight0, rhs.weight1, rhs.weight2, rhs.weight3);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00015F24 File Offset: 0x00014124
		public static bool operator !=(BoneWeight lhs, BoneWeight rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040005AE RID: 1454
		[SerializeField]
		private float m_Weight0;

		// Token: 0x040005AF RID: 1455
		[SerializeField]
		private float m_Weight1;

		// Token: 0x040005B0 RID: 1456
		[SerializeField]
		private float m_Weight2;

		// Token: 0x040005B1 RID: 1457
		[SerializeField]
		private float m_Weight3;

		// Token: 0x040005B2 RID: 1458
		[SerializeField]
		private int m_BoneIndex0;

		// Token: 0x040005B3 RID: 1459
		[SerializeField]
		private int m_BoneIndex1;

		// Token: 0x040005B4 RID: 1460
		[SerializeField]
		private int m_BoneIndex2;

		// Token: 0x040005B5 RID: 1461
		[SerializeField]
		private int m_BoneIndex3;
	}
}
