using System;
using System.Collections.Generic;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003AE RID: 942
	public class TriggerArea : MonoBehaviour
	{
		// Token: 0x06001F3C RID: 7996 RVA: 0x000BB183 File Offset: 0x000B9383
		private void OnTriggerEnter(Collider col)
		{
			if (col.attachedRigidbody != null && col.GetComponent<Mover>() != null)
			{
				this.rigidbodiesInTriggerArea.Add(col.attachedRigidbody);
			}
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x000BB1B2 File Offset: 0x000B93B2
		private void OnTriggerExit(Collider col)
		{
			if (col.attachedRigidbody != null && col.GetComponent<Mover>() != null)
			{
				this.rigidbodiesInTriggerArea.Remove(col.attachedRigidbody);
			}
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x000BB1E2 File Offset: 0x000B93E2
		public TriggerArea()
		{
		}

		// Token: 0x04001F8B RID: 8075
		public List<Rigidbody> rigidbodiesInTriggerArea = new List<Rigidbody>();
	}
}
