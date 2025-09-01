using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.LookDev
{
	// Token: 0x020000E8 RID: 232
	public interface IDataProvider
	{
		// Token: 0x060006DD RID: 1757
		void FirstInitScene(StageRuntimeInterface stage);

		// Token: 0x060006DE RID: 1758
		void UpdateSky(Camera camera, Sky sky, StageRuntimeInterface stage);

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060006DF RID: 1759
		IEnumerable<string> supportedDebugModes { get; }

		// Token: 0x060006E0 RID: 1760
		void UpdateDebugMode(int debugIndex);

		// Token: 0x060006E1 RID: 1761
		void GetShadowMask(ref RenderTexture output, StageRuntimeInterface stage);

		// Token: 0x060006E2 RID: 1762
		void OnBeginRendering(StageRuntimeInterface stage);

		// Token: 0x060006E3 RID: 1763
		void OnEndRendering(StageRuntimeInterface stage);

		// Token: 0x060006E4 RID: 1764
		void Cleanup(StageRuntimeInterface SRI);
	}
}
