using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class ActionEnabler : MonoBehaviour, EffectBase
{
	// Token: 0x0600005E RID: 94 RVA: 0x000053BC File Offset: 0x000035BC
	private void Awake()
	{
		this.comps = new Dictionary<GameObject, ActionEnabler.ComponentRef>();
		foreach (ActionEnabler.TagCheck tagCheck in this.SubSystems)
		{
			this.comps.TryAdd(tagCheck.ToEnable, new ActionEnabler.ComponentRef(tagCheck.ToEnable));
			foreach (GameObject gameObject in tagCheck.ToDisable)
			{
				this.comps.TryAdd(gameObject, new ActionEnabler.ComponentRef(gameObject));
			}
		}
		foreach (GameObject gameObject2 in this.Photosensitve)
		{
			this.comps.TryAdd(gameObject2, new ActionEnabler.ComponentRef(gameObject2));
		}
		foreach (GameObject gameObject3 in this.FullVFX)
		{
			this.comps.TryAdd(gameObject3, new ActionEnabler.ComponentRef(gameObject3));
		}
		foreach (GameObject gameObject4 in this.ReducedFX)
		{
			this.comps.TryAdd(gameObject4, new ActionEnabler.ComponentRef(gameObject4));
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00005574 File Offset: 0x00003774
	public void SetupInfo(EffectProperties props)
	{
		if (this.Photosensitve.Count > 0)
		{
			bool @bool = Settings.GetBool(SystemSetting.Photosensitivity, false);
			foreach (GameObject key in this.Photosensitve)
			{
				if (this.comps[key].IsExternal != !@bool)
				{
					if (@bool)
					{
						this.comps[key].MoveExternal(false);
					}
					else
					{
						this.comps[key].MoveInternal(false);
					}
				}
			}
		}
		if (this.localPlayerQuality != ActionEnabler.PlayerQuality || this.localEnemyQuality != ActionEnabler.EnemyQuality)
		{
			bool isPlayerEffect = props.SourceControl != null && props.SourceControl.TeamID == 1;
			this.UpdateQualityObjects(isPlayerEffect);
		}
		if (props.SourceControl == null)
		{
			return;
		}
		foreach (GameObject gameObject in this.DisableOnEnd)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(true);
			}
		}
		if (this.SubSystems.Count == 0)
		{
			return;
		}
		HashSet<GameObject> hashSet = new HashSet<GameObject>();
		try
		{
			foreach (ActionEnabler.TagCheck tagCheck in this.SubSystems)
			{
				bool flag = props.HasModTag(tagCheck.Requirement);
				if (flag)
				{
					this.comps[tagCheck.ToEnable].MoveInternal(false);
				}
				else
				{
					this.comps[tagCheck.ToEnable].MoveExternal(false);
				}
				foreach (GameObject gameObject2 in tagCheck.ToDisable)
				{
					if (flag)
					{
						this.comps[gameObject2].MoveExternal(false);
						hashSet.Add(gameObject2);
					}
					else if (!hashSet.Contains(gameObject2))
					{
						this.comps[gameObject2].MoveInternal(false);
					}
				}
			}
		}
		catch (Exception)
		{
			string str = "Error in ActionEnabler: ";
			GameObject gameObject3 = base.gameObject;
			Debug.LogError(str + ((gameObject3 != null) ? gameObject3.ToString() : null));
		}
	}

	// Token: 0x06000060 RID: 96 RVA: 0x000057FC File Offset: 0x000039FC
	private void UpdateQualityObjects(bool isPlayerEffect)
	{
		this.localPlayerQuality = ActionEnabler.PlayerQuality;
		this.localEnemyQuality = ActionEnabler.EnemyQuality;
		ActionEnabler.EffectQuality effectQuality = isPlayerEffect ? this.localPlayerQuality : this.localEnemyQuality;
		if (effectQuality > ActionEnabler.EffectQuality.Full)
		{
			using (List<GameObject>.Enumerator enumerator = this.FullVFX.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GameObject key = enumerator.Current;
					this.comps[key].MoveExternal(true);
				}
				goto IL_AE;
			}
		}
		foreach (GameObject key2 in this.FullVFX)
		{
			this.comps[key2].MoveInternal(true);
		}
		IL_AE:
		if (effectQuality > ActionEnabler.EffectQuality.Reduced)
		{
			using (List<GameObject>.Enumerator enumerator = this.ReducedFX.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GameObject key3 = enumerator.Current;
					this.comps[key3].MoveExternal(true);
				}
				return;
			}
		}
		foreach (GameObject key4 in this.ReducedFX)
		{
			this.comps[key4].MoveInternal(true);
		}
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00005978 File Offset: 0x00003B78
	public void Ended()
	{
		foreach (GameObject gameObject in this.DisableOnEnd)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000062 RID: 98 RVA: 0x000059D0 File Offset: 0x00003BD0
	private void OnDestroy()
	{
		foreach (ActionEnabler.ComponentRef componentRef in this.comps.Values)
		{
			componentRef.DestroyExternal();
		}
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00005A28 File Offset: 0x00003C28
	public ActionEnabler()
	{
	}

	// Token: 0x04000052 RID: 82
	public static ActionEnabler.EffectQuality PlayerQuality;

	// Token: 0x04000053 RID: 83
	public static ActionEnabler.EffectQuality EnemyQuality;

	// Token: 0x04000054 RID: 84
	public List<GameObject> FullVFX;

	// Token: 0x04000055 RID: 85
	public List<GameObject> ReducedFX;

	// Token: 0x04000056 RID: 86
	public List<ActionEnabler.TagCheck> SubSystems;

	// Token: 0x04000057 RID: 87
	public List<GameObject> DisableOnEnd = new List<GameObject>();

	// Token: 0x04000058 RID: 88
	public List<GameObject> Photosensitve;

	// Token: 0x04000059 RID: 89
	private ActionEnabler.EffectQuality localPlayerQuality;

	// Token: 0x0400005A RID: 90
	private ActionEnabler.EffectQuality localEnemyQuality;

	// Token: 0x0400005B RID: 91
	private Dictionary<GameObject, ActionEnabler.ComponentRef> comps = new Dictionary<GameObject, ActionEnabler.ComponentRef>();

	// Token: 0x020003E0 RID: 992
	[Serializable]
	public class TagCheck
	{
		// Token: 0x0600203B RID: 8251 RVA: 0x000BFC44 File Offset: 0x000BDE44
		public TagCheck()
		{
		}

		// Token: 0x040020B1 RID: 8369
		[Header("Requirement")]
		public ModTag Requirement;

		// Token: 0x040020B2 RID: 8370
		public GameObject ToEnable;

		// Token: 0x040020B3 RID: 8371
		public List<GameObject> ToDisable = new List<GameObject>();

		// Token: 0x040020B4 RID: 8372
		[NonSerialized]
		public bool HasRequirement;
	}

	// Token: 0x020003E1 RID: 993
	public enum EffectQuality
	{
		// Token: 0x040020B6 RID: 8374
		Full,
		// Token: 0x040020B7 RID: 8375
		Reduced,
		// Token: 0x040020B8 RID: 8376
		Minimal
	}

	// Token: 0x020003E2 RID: 994
	private class ComponentRef
	{
		// Token: 0x0600203C RID: 8252 RVA: 0x000BFC58 File Offset: 0x000BDE58
		public ComponentRef(GameObject o)
		{
			this.Target = o;
			this.t = o.transform;
			this.Parent = this.t.parent;
			this.LocalPosition = this.t.localPosition;
			this.LocalRotation = this.t.localRotation;
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x000BFCB1 File Offset: 0x000BDEB1
		public void MoveExternal(bool fromSetting = false)
		{
			if (this.IsExternal)
			{
				return;
			}
			this.IsExternal = true;
			this.SettingExternal = fromSetting;
			this.t.SetParent(null, false);
			this.Target.SetActive(false);
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x000BFCE4 File Offset: 0x000BDEE4
		public void MoveInternal(bool fromSetting = false)
		{
			this.Target.SetActive(true);
			if (!this.IsExternal)
			{
				return;
			}
			if (this.SettingExternal && !fromSetting)
			{
				return;
			}
			this.IsExternal = false;
			this.SettingExternal = false;
			this.t.SetParent(this.Parent, false);
			this.t.SetLocalPositionAndRotation(this.LocalPosition, this.LocalRotation);
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x000BFD49 File Offset: 0x000BDF49
		public void DestroyExternal()
		{
			if (!this.IsExternal)
			{
				return;
			}
			UnityEngine.Object.Destroy(this.Target);
		}

		// Token: 0x040020B9 RID: 8377
		private GameObject Target;

		// Token: 0x040020BA RID: 8378
		public bool IsExternal;

		// Token: 0x040020BB RID: 8379
		public bool SettingExternal;

		// Token: 0x040020BC RID: 8380
		private Transform t;

		// Token: 0x040020BD RID: 8381
		private Transform Parent;

		// Token: 0x040020BE RID: 8382
		private Vector3 LocalPosition;

		// Token: 0x040020BF RID: 8383
		private Quaternion LocalRotation;
	}
}
