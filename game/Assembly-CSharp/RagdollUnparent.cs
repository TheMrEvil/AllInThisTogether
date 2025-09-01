using System;
using UnityEngine;

// Token: 0x0200025B RID: 603
public class RagdollUnparent : MonoBehaviour
{
	// Token: 0x06001847 RID: 6215 RVA: 0x0009800E File Offset: 0x0009620E
	private void Awake()
	{
		this.rb = base.GetComponent<Rigidbody>();
		this.baseParent = base.transform.parent;
	}

	// Token: 0x06001848 RID: 6216 RVA: 0x00098030 File Offset: 0x00096230
	public void Detatch()
	{
		if (this.isDetatched)
		{
			return;
		}
		EntityDisplay.SetLayerRecursively(base.gameObject, 13);
		this.isDetatched = true;
		this.rb.transform.SetParent(null);
		this.rb.isKinematic = false;
		this.rb.AddExplosionForce(this.ExplosionForce, this.rb.position + UnityEngine.Random.onUnitSphere, 5f, 0.2f, ForceMode.Impulse);
	}

	// Token: 0x06001849 RID: 6217 RVA: 0x000980A8 File Offset: 0x000962A8
	public void Reattatch()
	{
		base.transform.SetParent(this.baseParent);
		this.rb.isKinematic = true;
	}

	// Token: 0x0600184A RID: 6218 RVA: 0x000980C7 File Offset: 0x000962C7
	public RagdollUnparent()
	{
	}

	// Token: 0x0400181C RID: 6172
	private bool isDetatched;

	// Token: 0x0400181D RID: 6173
	public float ExplosionForce = 6f;

	// Token: 0x0400181E RID: 6174
	private Rigidbody rb;

	// Token: 0x0400181F RID: 6175
	private Transform baseParent;
}
