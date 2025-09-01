using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000081 RID: 129
	[Serializable]
	public sealed class LensFlareDataSRP : ScriptableObject
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x0001431D File Offset: 0x0001251D
		public LensFlareDataSRP()
		{
			this.elements = null;
		}

		// Token: 0x040002B4 RID: 692
		public LensFlareDataElementSRP[] elements;
	}
}
