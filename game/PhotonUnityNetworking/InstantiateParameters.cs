using System;
using Photon.Realtime;
using UnityEngine;

namespace Photon.Pun
{
	// Token: 0x02000011 RID: 17
	public struct InstantiateParameters
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000027AC File Offset: 0x000009AC
		public InstantiateParameters(string prefabName, Vector3 position, Quaternion rotation, byte group, object[] data, byte objLevelPrefix, int[] viewIDs, Player creator, int timestamp)
		{
			this.prefabName = prefabName;
			this.position = position;
			this.rotation = rotation;
			this.group = group;
			this.data = data;
			this.objLevelPrefix = objLevelPrefix;
			this.viewIDs = viewIDs;
			this.creator = creator;
			this.timestamp = timestamp;
		}

		// Token: 0x04000026 RID: 38
		public int[] viewIDs;

		// Token: 0x04000027 RID: 39
		public byte objLevelPrefix;

		// Token: 0x04000028 RID: 40
		public object[] data;

		// Token: 0x04000029 RID: 41
		public byte group;

		// Token: 0x0400002A RID: 42
		public Quaternion rotation;

		// Token: 0x0400002B RID: 43
		public Vector3 position;

		// Token: 0x0400002C RID: 44
		public string prefabName;

		// Token: 0x0400002D RID: 45
		public Player creator;

		// Token: 0x0400002E RID: 46
		public int timestamp;
	}
}
