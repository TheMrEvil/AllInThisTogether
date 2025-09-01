using System;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000011 RID: 17
	[ExecuteAlways]
	[AddComponentMenu("Light/Probe Volume (Experimental)")]
	public class ProbeVolume : MonoBehaviour
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00006245 File Offset: 0x00004445
		public ProbeVolume()
		{
		}

		// Token: 0x0400007D RID: 125
		public bool globalVolume;

		// Token: 0x0400007E RID: 126
		public Vector3 size = new Vector3(10f, 10f, 10f);

		// Token: 0x0400007F RID: 127
		[HideInInspector]
		[Range(0f, 2f)]
		public float geometryDistanceOffset = 0.2f;

		// Token: 0x04000080 RID: 128
		public LayerMask objectLayerMask = -1;

		// Token: 0x04000081 RID: 129
		[HideInInspector]
		public int lowestSubdivLevelOverride;

		// Token: 0x04000082 RID: 130
		[HideInInspector]
		public int highestSubdivLevelOverride = -1;

		// Token: 0x04000083 RID: 131
		[HideInInspector]
		public bool overridesSubdivLevels;

		// Token: 0x04000084 RID: 132
		[SerializeField]
		internal bool mightNeedRebaking;

		// Token: 0x04000085 RID: 133
		[SerializeField]
		internal Matrix4x4 cachedTransform;

		// Token: 0x04000086 RID: 134
		[SerializeField]
		internal int cachedHashCode;
	}
}
