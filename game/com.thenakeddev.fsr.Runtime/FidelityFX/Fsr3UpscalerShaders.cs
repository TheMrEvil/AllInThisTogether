using System;
using UnityEngine;

namespace FidelityFX
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public class Fsr3UpscalerShaders
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000025F7 File Offset: 0x000007F7
		public Fsr3UpscalerShaders Clone()
		{
			return (Fsr3UpscalerShaders)base.MemberwiseClone();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002604 File Offset: 0x00000804
		public Fsr3UpscalerShaders DeepCopy()
		{
			return new Fsr3UpscalerShaders
			{
				prepareInputsPass = UnityEngine.Object.Instantiate<ComputeShader>(this.prepareInputsPass),
				lumaPyramidPass = UnityEngine.Object.Instantiate<ComputeShader>(this.lumaPyramidPass),
				shadingChangePyramidPass = UnityEngine.Object.Instantiate<ComputeShader>(this.shadingChangePyramidPass),
				shadingChangePass = UnityEngine.Object.Instantiate<ComputeShader>(this.shadingChangePass),
				prepareReactivityPass = UnityEngine.Object.Instantiate<ComputeShader>(this.prepareReactivityPass),
				lumaInstabilityPass = UnityEngine.Object.Instantiate<ComputeShader>(this.lumaInstabilityPass),
				accumulatePass = UnityEngine.Object.Instantiate<ComputeShader>(this.accumulatePass),
				sharpenPass = UnityEngine.Object.Instantiate<ComputeShader>(this.sharpenPass),
				autoGenReactivePass = UnityEngine.Object.Instantiate<ComputeShader>(this.autoGenReactivePass),
				tcrAutoGenPass = UnityEngine.Object.Instantiate<ComputeShader>(this.tcrAutoGenPass),
				debugViewPass = UnityEngine.Object.Instantiate<ComputeShader>(this.debugViewPass)
			};
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000026D4 File Offset: 0x000008D4
		public void Dispose()
		{
			UnityEngine.Object.Destroy(this.prepareInputsPass);
			UnityEngine.Object.Destroy(this.lumaPyramidPass);
			UnityEngine.Object.Destroy(this.shadingChangePyramidPass);
			UnityEngine.Object.Destroy(this.shadingChangePass);
			UnityEngine.Object.Destroy(this.prepareReactivityPass);
			UnityEngine.Object.Destroy(this.lumaInstabilityPass);
			UnityEngine.Object.Destroy(this.accumulatePass);
			UnityEngine.Object.Destroy(this.sharpenPass);
			UnityEngine.Object.Destroy(this.autoGenReactivePass);
			UnityEngine.Object.Destroy(this.tcrAutoGenPass);
			UnityEngine.Object.Destroy(this.debugViewPass);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000275A File Offset: 0x0000095A
		public Fsr3UpscalerShaders()
		{
		}

		// Token: 0x04000041 RID: 65
		public ComputeShader prepareInputsPass;

		// Token: 0x04000042 RID: 66
		public ComputeShader lumaPyramidPass;

		// Token: 0x04000043 RID: 67
		public ComputeShader shadingChangePyramidPass;

		// Token: 0x04000044 RID: 68
		public ComputeShader shadingChangePass;

		// Token: 0x04000045 RID: 69
		public ComputeShader prepareReactivityPass;

		// Token: 0x04000046 RID: 70
		public ComputeShader lumaInstabilityPass;

		// Token: 0x04000047 RID: 71
		public ComputeShader accumulatePass;

		// Token: 0x04000048 RID: 72
		public ComputeShader sharpenPass;

		// Token: 0x04000049 RID: 73
		public ComputeShader autoGenReactivePass;

		// Token: 0x0400004A RID: 74
		public ComputeShader tcrAutoGenPass;

		// Token: 0x0400004B RID: 75
		public ComputeShader debugViewPass;
	}
}
