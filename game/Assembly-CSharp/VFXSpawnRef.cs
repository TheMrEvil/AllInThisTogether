using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000027 RID: 39
public class VFXSpawnRef : MonoBehaviour
{
	// Token: 0x06000119 RID: 281 RVA: 0x0000CA30 File Offset: 0x0000AC30
	public void Setup(EffectProperties props, string nodeID, bool requireIndexMatch)
	{
		if (VFXSpawnRef.VFXElements == null)
		{
			VFXSpawnRef.VFXElements = new List<VFXSpawnRef>();
		}
		if (VFXSpawnRef.VFXElements.Contains(this))
		{
			return;
		}
		VFXSpawnRef.VFXElements.Add(this);
		this.NodeID = nodeID;
		if (props.SourceControl != null)
		{
			this.SourceID = props.SourceControl.ViewID;
		}
		if (props.AffectedControl != null)
		{
			this.AffectedID = props.AffectedControl.ViewID;
		}
		if (requireIndexMatch)
		{
			this.LocalIndex = props.LocalIndex;
		}
		this.isSetup = true;
	}

	// Token: 0x0600011A RID: 282 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
	private bool Matches(EffectProperties props, string nodeID)
	{
		return this.isSetup && !(nodeID != this.NodeID) && (this.SourceID == -1 || !(props.SourceControl != null) || props.SourceControl.ViewID == this.SourceID) && (this.AffectedID == -1 || (!(props.AffectedControl == null) && props.AffectedControl.ViewID == this.AffectedID)) && (this.LocalIndex == 0 || this.LocalIndex == props.LocalIndex);
	}

	// Token: 0x0600011B RID: 283 RVA: 0x0000CB5C File Offset: 0x0000AD5C
	public void Release()
	{
		if (!this.isSetup)
		{
			return;
		}
		if (VFXSpawnRef.VFXElements == null)
		{
			VFXSpawnRef.VFXElements = new List<VFXSpawnRef>();
		}
		this.isSetup = false;
		VFXSpawnRef.VFXElements.Remove(this);
	}

	// Token: 0x0600011C RID: 284 RVA: 0x0000CB8C File Offset: 0x0000AD8C
	public void CancelEffect(float destroyTime)
	{
		try
		{
			ParticleSystem component = base.GetComponent<ParticleSystem>();
			if (component != null)
			{
				component.Stop();
			}
			ParticleSystem[] componentsInChildren = base.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
			EntityIndicator[] componentsInChildren2 = base.GetComponentsInChildren<EntityIndicator>();
			for (int i = 0; i < componentsInChildren2.Length; i++)
			{
				componentsInChildren2[i].Deactivate();
			}
			EffectLight[] componentsInChildren3 = base.GetComponentsInChildren<EffectLight>();
			for (int i = 0; i < componentsInChildren3.Length; i++)
			{
				componentsInChildren3[i].Deactivate();
			}
			ActionScaleCurves[] componentsInChildren4 = base.GetComponentsInChildren<ActionScaleCurves>();
			for (int i = 0; i < componentsInChildren4.Length; i++)
			{
				componentsInChildren4[i].DoScaleOut();
			}
			foreach (DynamicDecal dynamicDecal in base.gameObject.GetAllComponents<DynamicDecal>())
			{
				dynamicDecal.Hide();
			}
			AudioSource component2 = base.GetComponent<AudioSource>();
			AudioFader component3 = base.GetComponent<AudioFader>();
			if (component2 != null && component3 == null)
			{
				component2.Stop();
			}
			if (component3 != null)
			{
				component3.Stop();
			}
			ActionEffect component4 = base.GetComponent<ActionEffect>();
			if (component4 != null)
			{
				component4.Finish();
			}
			if (base.GetComponent<DestroyTimed>() == null)
			{
				ActionPool.ReleaseDelayed(base.gameObject, destroyTime);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600011D RID: 285 RVA: 0x0000CD1C File Offset: 0x0000AF1C
	private void OnDisable()
	{
		this.Release();
	}

	// Token: 0x0600011E RID: 286 RVA: 0x0000CD24 File Offset: 0x0000AF24
	public static VFXSpawnRef GetVFX(EffectProperties props, string guid)
	{
		if (VFXSpawnRef.VFXElements == null)
		{
			VFXSpawnRef.VFXElements = new List<VFXSpawnRef>();
		}
		foreach (VFXSpawnRef vfxspawnRef in VFXSpawnRef.VFXElements)
		{
			if (vfxspawnRef.Matches(props, guid))
			{
				return vfxspawnRef;
			}
		}
		return null;
	}

	// Token: 0x0600011F RID: 287 RVA: 0x0000CD94 File Offset: 0x0000AF94
	public static void ClearNonPlayer()
	{
		List<VFXSpawnRef> list = new List<VFXSpawnRef>();
		foreach (VFXSpawnRef vfxspawnRef in VFXSpawnRef.VFXElements)
		{
			if (vfxspawnRef.SourceID < 0 || !(EntityControl.GetEntity(vfxspawnRef.SourceID) is PlayerControl))
			{
				list.Add(vfxspawnRef);
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			list[i].CancelEffect(0f);
			list[i].Release();
		}
	}

	// Token: 0x06000120 RID: 288 RVA: 0x0000CE38 File Offset: 0x0000B038
	public VFXSpawnRef()
	{
	}

	// Token: 0x06000121 RID: 289 RVA: 0x0000CE4E File Offset: 0x0000B04E
	// Note: this type is marked as 'beforefieldinit'.
	static VFXSpawnRef()
	{
	}

	// Token: 0x04000136 RID: 310
	private static List<VFXSpawnRef> VFXElements = new List<VFXSpawnRef>();

	// Token: 0x04000137 RID: 311
	public bool isSetup;

	// Token: 0x04000138 RID: 312
	public string NodeID;

	// Token: 0x04000139 RID: 313
	public int SourceID = -1;

	// Token: 0x0400013A RID: 314
	public int AffectedID = -1;

	// Token: 0x0400013B RID: 315
	public int LocalIndex;
}
