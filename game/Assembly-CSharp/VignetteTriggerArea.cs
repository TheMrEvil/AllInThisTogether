using System;
using UnityEngine;

// Token: 0x020000BD RID: 189
public class VignetteTriggerArea : MonoBehaviour
{
	// Token: 0x06000887 RID: 2183 RVA: 0x0003AB29 File Offset: 0x00038D29
	private void Awake()
	{
		if (this.col == null)
		{
			this.col = base.GetComponent<Collider>();
		}
		if (this.col != null)
		{
			this.col.isTrigger = true;
		}
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x0003AB60 File Offset: 0x00038D60
	private void OnTriggerEnter(Collider other)
	{
		if (this.wasEntered)
		{
			return;
		}
		PlayerControl componentInParent = other.GetComponentInParent<PlayerControl>();
		if (componentInParent == null)
		{
			return;
		}
		if (!componentInParent.IsMine)
		{
			return;
		}
		this.wasEntered = true;
		if (!string.IsNullOrEmpty(this.ActionID))
		{
			StateManager.VignetteAction(this.ActionID);
		}
		if (this.OpenExit)
		{
			StateManager.instance.VignetteOpenExit();
		}
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x0003ABC1 File Offset: 0x00038DC1
	private void OnTriggerExit(Collider other)
	{
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x0003ABC3 File Offset: 0x00038DC3
	private void OnValidate()
	{
		if (this.col != null)
		{
			return;
		}
		this.col = base.GetComponent<Collider>();
		if (this.col == null)
		{
			this.col = base.gameObject.AddComponent<SphereCollider>();
		}
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x0003ABFF File Offset: 0x00038DFF
	public VignetteTriggerArea()
	{
	}

	// Token: 0x04000730 RID: 1840
	private Collider col;

	// Token: 0x04000731 RID: 1841
	public string ActionID;

	// Token: 0x04000732 RID: 1842
	public bool OpenExit;

	// Token: 0x04000733 RID: 1843
	private bool wasEntered;
}
