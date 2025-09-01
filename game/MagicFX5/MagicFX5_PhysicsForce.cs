using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000017 RID: 23
	public class MagicFX5_PhysicsForce : MonoBehaviour
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00004500 File Offset: 0x00002700
		private void OnEnable()
		{
			if (this.EffectSettings.UseForce)
			{
				base.Invoke("SetForce", this.Delay);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004520 File Offset: 0x00002720
		private void OnDisable()
		{
			if (this.EffectSettings.UseForce)
			{
				base.CancelInvoke("SetForce");
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000453C File Offset: 0x0000273C
		public void SetForce()
		{
			Collider[] array = Physics.OverlapSphere(base.transform.position, this.EffectSettings.ForceRadius, this.EffectSettings.ForceLayerMask);
			if (array.Length == 0)
			{
				return;
			}
			Vector3 b = this.PhysicsForceEpicenter ? this.PhysicsForceEpicenter.position : base.transform.position;
			foreach (Collider collider in array)
			{
				if (!collider.gameObject.isStatic && collider.attachedRigidbody != null)
				{
					Vector3 vector = collider.transform.position - b;
					float magnitude = vector.magnitude;
					collider.attachedRigidbody.AddForce(vector.normalized * this.EffectSettings.Force * (1f - magnitude / this.EffectSettings.ForceRadius), ForceMode.Impulse);
				}
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004632 File Offset: 0x00002832
		public MagicFX5_PhysicsForce()
		{
		}

		// Token: 0x040000A3 RID: 163
		public MagicFX5_EffectSettings EffectSettings;

		// Token: 0x040000A4 RID: 164
		public Transform PhysicsForceEpicenter;

		// Token: 0x040000A5 RID: 165
		public float Delay;
	}
}
