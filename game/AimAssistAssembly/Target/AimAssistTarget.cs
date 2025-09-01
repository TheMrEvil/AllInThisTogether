using System;
using UnityEngine;
using UnityEngine.Events;

namespace Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Target
{
	// Token: 0x02000005 RID: 5
	public class AimAssistTarget : MonoBehaviour, IEquatable<AimAssistTarget>
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000226C File Offset: 0x0000046C
		private void Awake()
		{
			this.CheckForCollider();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002274 File Offset: 0x00000474
		public bool Equals(AimAssistTarget other)
		{
			return other != null && base.GetInstanceID() == other.GetInstanceID();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000228F File Offset: 0x0000048F
		private void CheckForCollider()
		{
			if (!base.GetComponent<Collider>())
			{
				Debug.LogWarning("No collider found on target " + base.name + ", the aim assist won't take effect.");
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022B8 File Offset: 0x000004B8
		public AimAssistTarget()
		{
		}

		// Token: 0x0400000C RID: 12
		[Header("Events")]
		public readonly UnityEvent TargetSelected = new UnityEvent();

		// Token: 0x0400000D RID: 13
		public readonly UnityEvent TargetLost = new UnityEvent();
	}
}
