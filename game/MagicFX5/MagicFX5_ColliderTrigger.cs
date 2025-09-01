using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000008 RID: 8
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(Rigidbody))]
	public class MagicFX5_ColliderTrigger : MonoBehaviour
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002A3C File Offset: 0x00000C3C
		private void OnCollisionEnter(Collision collision)
		{
			MagicFX5_EffectSettings.EffectCollisionHit obj = default(MagicFX5_EffectSettings.EffectCollisionHit);
			ContactPoint contactPoint = collision.contacts[0];
			obj.Target = contactPoint.otherCollider.transform;
			obj.Position = contactPoint.point;
			obj.Normal = contactPoint.normal;
			Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectCollisionEnter = this.EffectSettings.OnEffectCollisionEnter;
			if (onEffectCollisionEnter == null)
			{
				return;
			}
			onEffectCollisionEnter(obj);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002AA3 File Offset: 0x00000CA3
		public MagicFX5_ColliderTrigger()
		{
		}

		// Token: 0x0400003B RID: 59
		public MagicFX5_EffectSettings EffectSettings;
	}
}
