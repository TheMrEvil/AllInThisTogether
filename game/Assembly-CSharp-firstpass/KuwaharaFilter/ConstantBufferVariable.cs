using System;
using UnityEngine;

namespace KuwaharaFilter
{
	// Token: 0x02000034 RID: 52
	public class ConstantBufferVariable
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00009034 File Offset: 0x00007234
		public static void Apply(ComputeShader shader, ConstantBufferVariable buffer)
		{
			shader.SetInt("GaussRadius", buffer.GaussRadius);
			shader.SetInt("KuwaharaRadius", buffer.KuwaharaRadius);
			shader.SetInt("KuwaharaQ", buffer.KuwaharaQ);
			shader.SetFloat("KuwaharaAlpha", buffer.KuwaharaAlpha);
			shader.SetFloat("KuwaharaDepthClose", buffer.KuwaharaDepthClose);
			shader.SetFloat("KuwaharaDepthFar", buffer.KuwaharaDepthFar);
			shader.SetFloat("KuwaharaOpacity", buffer.KuwaharaOpacity);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000090B8 File Offset: 0x000072B8
		public ConstantBufferVariable()
		{
		}

		// Token: 0x040001A2 RID: 418
		public int GaussRadius;

		// Token: 0x040001A3 RID: 419
		public int KuwaharaRadius;

		// Token: 0x040001A4 RID: 420
		public int KuwaharaQ;

		// Token: 0x040001A5 RID: 421
		public float KuwaharaAlpha;

		// Token: 0x040001A6 RID: 422
		public float KuwaharaDepthClose;

		// Token: 0x040001A7 RID: 423
		public float KuwaharaDepthFar;

		// Token: 0x040001A8 RID: 424
		public float KuwaharaOpacity;
	}
}
