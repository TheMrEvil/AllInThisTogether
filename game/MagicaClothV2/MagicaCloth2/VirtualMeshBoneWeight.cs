using System;
using Unity.Mathematics;

namespace MagicaCloth2
{
	// Token: 0x02000133 RID: 307
	public struct VirtualMeshBoneWeight
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0002B2F7 File Offset: 0x000294F7
		public bool IsValid
		{
			get
			{
				return this.weights[0] >= 1E-06f;
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0002B30F File Offset: 0x0002950F
		public VirtualMeshBoneWeight(int4 boneIndices, float4 weights)
		{
			this.boneIndices = boneIndices;
			this.weights = weights;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0002B320 File Offset: 0x00029520
		public int Count
		{
			get
			{
				if (this.weights[3] > 0f)
				{
					return 4;
				}
				if (this.weights[2] > 0f)
				{
					return 3;
				}
				if (this.weights[1] > 0f)
				{
					return 2;
				}
				if (this.weights[0] > 0f)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0002B384 File Offset: 0x00029584
		public void AddWeight(int boneIndex, float weight)
		{
			if (weight < 1E-06f)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < 4; i++)
			{
				float num2 = this.weights[i];
				if (num2 == 0f)
				{
					break;
				}
				if (this.boneIndices[i] == boneIndex)
				{
					num2 += weight;
					this.weights[i] = num2;
					for (int j = i; j >= 1; j--)
					{
						if (this.weights[j] > this.weights[j - 1])
						{
							num2 = this.weights[j - 1];
							this.weights[j - 1] = this.weights[j];
							this.weights[j] = num2;
							int value = this.boneIndices[j - 1];
							this.boneIndices[j - 1] = this.boneIndices[j];
							this.boneIndices[j] = value;
						}
					}
					return;
				}
				num++;
			}
			for (int k = 0; k < 4; k++)
			{
				float num3 = this.weights[k];
				if (num3 == 0f)
				{
					this.weights[k] = weight;
					this.boneIndices[k] = boneIndex;
					return;
				}
				if (weight > num3)
				{
					for (int l = 2; l >= k; l--)
					{
						this.weights[l + 1] = this.weights[l];
						this.boneIndices[l + 1] = this.boneIndices[l];
					}
					this.weights[k] = weight;
					this.boneIndices[k] = boneIndex;
					return;
				}
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0002B540 File Offset: 0x00029740
		public void AddWeight(in VirtualMeshBoneWeight bw)
		{
			VirtualMeshBoneWeight virtualMeshBoneWeight = bw;
			if (virtualMeshBoneWeight.IsValid)
			{
				for (int i = 0; i < 4; i++)
				{
					int4 @int = bw.boneIndices;
					int boneIndex = @int[i];
					float4 @float = bw.weights;
					this.AddWeight(boneIndex, @float[i]);
				}
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0002B590 File Offset: 0x00029790
		public void AdjustWeight()
		{
			if (!this.IsValid)
			{
				return;
			}
			float num = math.csum(this.weights);
			float rhs = 1f / num;
			this.weights *= rhs;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0002B5CC File Offset: 0x000297CC
		public override string ToString()
		{
			return string.Format("[{0}] w({1})", this.boneIndices, this.weights);
		}

		// Token: 0x040007E1 RID: 2017
		public float4 weights;

		// Token: 0x040007E2 RID: 2018
		public int4 boneIndices;
	}
}
