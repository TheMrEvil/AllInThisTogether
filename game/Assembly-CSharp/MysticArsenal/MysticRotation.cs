using System;
using UnityEngine;

namespace MysticArsenal
{
	// Token: 0x020003D9 RID: 985
	public class MysticRotation : MonoBehaviour
	{
		// Token: 0x06002018 RID: 8216 RVA: 0x000BE8C7 File Offset: 0x000BCAC7
		private void Start()
		{
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x000BE8CC File Offset: 0x000BCACC
		private void Update()
		{
			if (this.rotateSpace == MysticRotation.spaceEnum.Local)
			{
				base.transform.Rotate(this.rotateVector * Time.deltaTime);
			}
			if (this.rotateSpace == MysticRotation.spaceEnum.World)
			{
				base.transform.Rotate(this.rotateVector * Time.deltaTime, Space.World);
			}
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x000BE921 File Offset: 0x000BCB21
		public MysticRotation()
		{
		}

		// Token: 0x04002066 RID: 8294
		[Header("Rotate axises by degrees per second")]
		public Vector3 rotateVector = Vector3.zero;

		// Token: 0x04002067 RID: 8295
		public MysticRotation.spaceEnum rotateSpace;

		// Token: 0x020006A5 RID: 1701
		public enum spaceEnum
		{
			// Token: 0x04002C7B RID: 11387
			Local,
			// Token: 0x04002C7C RID: 11388
			World
		}
	}
}
