using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000C2 RID: 194
	public class TriggerEventBroadcaster : MonoBehaviour
	{
		// Token: 0x06000889 RID: 2185 RVA: 0x00039F09 File Offset: 0x00038109
		private void OnTriggerEnter(Collider collider)
		{
			if (this.target != null)
			{
				this.target.SendMessage("OnTriggerEnter", collider, SendMessageOptions.DontRequireReceiver);
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00039F2B File Offset: 0x0003812B
		private void OnTriggerStay(Collider collider)
		{
			if (this.target != null)
			{
				this.target.SendMessage("OnTriggerStay", collider, SendMessageOptions.DontRequireReceiver);
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00039F4D File Offset: 0x0003814D
		private void OnTriggerExit(Collider collider)
		{
			if (this.target != null)
			{
				this.target.SendMessage("OnTriggerExit", collider, SendMessageOptions.DontRequireReceiver);
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00039F6F File Offset: 0x0003816F
		public TriggerEventBroadcaster()
		{
		}

		// Token: 0x040006CF RID: 1743
		public GameObject target;
	}
}
