using System;
using UnityEngine;

namespace FluxySamples
{
	// Token: 0x0200001C RID: 28
	public class MoveAndRotate : MonoBehaviour
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00006B9E File Offset: 0x00004D9E
		private void Start()
		{
			this.m_LastRealTime = Time.realtimeSinceStartup;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00006BAC File Offset: 0x00004DAC
		private void Update()
		{
			float d = Time.deltaTime;
			if (this.ignoreTimescale)
			{
				d = Time.realtimeSinceStartup - this.m_LastRealTime;
				this.m_LastRealTime = Time.realtimeSinceStartup;
			}
			base.transform.Translate(this.moveUnitsPerSecond.value * d, this.moveUnitsPerSecond.space);
			base.transform.Rotate(this.rotateDegreesPerSecond.value * d, this.rotateDegreesPerSecond.space);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006C2D File Offset: 0x00004E2D
		public MoveAndRotate()
		{
		}

		// Token: 0x040000C9 RID: 201
		public MoveAndRotate.Vector3andSpace moveUnitsPerSecond;

		// Token: 0x040000CA RID: 202
		public MoveAndRotate.Vector3andSpace rotateDegreesPerSecond;

		// Token: 0x040000CB RID: 203
		public bool ignoreTimescale;

		// Token: 0x040000CC RID: 204
		private float m_LastRealTime;

		// Token: 0x02000030 RID: 48
		[Serializable]
		public class Vector3andSpace
		{
			// Token: 0x060000BB RID: 187 RVA: 0x00006F66 File Offset: 0x00005166
			public Vector3andSpace()
			{
			}

			// Token: 0x040000FD RID: 253
			public Vector3 value;

			// Token: 0x040000FE RID: 254
			public Space space = Space.Self;
		}
	}
}
