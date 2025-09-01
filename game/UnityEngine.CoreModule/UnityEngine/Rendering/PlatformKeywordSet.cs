using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000423 RID: 1059
	[UsedByNativeCode]
	public struct PlatformKeywordSet
	{
		// Token: 0x060024EE RID: 9454 RVA: 0x0003E95C File Offset: 0x0003CB5C
		private ulong ComputeKeywordMask(BuiltinShaderDefine define)
		{
			return (ulong)(1L << (int)(define % (BuiltinShaderDefine)64 & BuiltinShaderDefine.SHADER_API_GLES30));
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x0003E978 File Offset: 0x0003CB78
		public bool IsEnabled(BuiltinShaderDefine define)
		{
			return (this.m_Bits & this.ComputeKeywordMask(define)) > 0UL;
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x0003E99C File Offset: 0x0003CB9C
		public void Enable(BuiltinShaderDefine define)
		{
			this.m_Bits |= this.ComputeKeywordMask(define);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x0003E9B3 File Offset: 0x0003CBB3
		public void Disable(BuiltinShaderDefine define)
		{
			this.m_Bits &= ~this.ComputeKeywordMask(define);
		}

		// Token: 0x04000DAC RID: 3500
		private const int k_SizeInBits = 64;

		// Token: 0x04000DAD RID: 3501
		internal ulong m_Bits;
	}
}
