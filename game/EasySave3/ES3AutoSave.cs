using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ES3Internal;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class ES3AutoSave : MonoBehaviour, ISerializationCallbackReceiver
{
	// Token: 0x06000009 RID: 9 RVA: 0x00002172 File Offset: 0x00000372
	public void Reset()
	{
		this.saveLayer = false;
		this.saveTag = false;
		this.saveName = false;
		this.saveHideFlags = false;
		this.saveActive = false;
		this.saveChildren = false;
		this.saveDestroyed = false;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000021A5 File Offset: 0x000003A5
	public void Awake()
	{
		if (ES3AutoSaveMgr.Current == null)
		{
			ES3Debug.LogWarning("<b>No GameObjects in this scene will be autosaved</b> because there is no Easy Save 3 Manager. To add a manager to this scene, exit playmode and go to Assets > Easy Save 3 > Add Manager to Scene.", this, 0);
			return;
		}
		ES3AutoSaveMgr.AddAutoSave(this);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000021C7 File Offset: 0x000003C7
	public void OnApplicationQuit()
	{
		this.isQuitting = true;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000021D0 File Offset: 0x000003D0
	public void OnDestroy()
	{
		if (!this.isQuitting)
		{
			ES3AutoSaveMgr.DestroyAutoSave(this);
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000021E0 File Offset: 0x000003E0
	public void OnBeforeSerialize()
	{
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000021E2 File Offset: 0x000003E2
	public void OnAfterDeserialize()
	{
		this.componentsToSave.RemoveAll((Component c) => c == null || c.GetType() == typeof(Component));
	}

	// Token: 0x0600000F RID: 15 RVA: 0x0000220F File Offset: 0x0000040F
	public ES3AutoSave()
	{
	}

	// Token: 0x04000003 RID: 3
	public bool saveLayer = true;

	// Token: 0x04000004 RID: 4
	public bool saveTag = true;

	// Token: 0x04000005 RID: 5
	public bool saveName = true;

	// Token: 0x04000006 RID: 6
	public bool saveHideFlags = true;

	// Token: 0x04000007 RID: 7
	public bool saveActive = true;

	// Token: 0x04000008 RID: 8
	public bool saveChildren;

	// Token: 0x04000009 RID: 9
	public bool saveDestroyed = true;

	// Token: 0x0400000A RID: 10
	private bool isQuitting;

	// Token: 0x0400000B RID: 11
	public List<Component> componentsToSave = new List<Component>();

	// Token: 0x020000EC RID: 236
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x0001F4A0 File Offset: 0x0001D6A0
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001F4AC File Offset: 0x0001D6AC
		public <>c()
		{
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001F4B4 File Offset: 0x0001D6B4
		internal bool <OnAfterDeserialize>b__14_0(Component c)
		{
			return c == null || c.GetType() == typeof(Component);
		}

		// Token: 0x04000188 RID: 392
		public static readonly ES3AutoSave.<>c <>9 = new ES3AutoSave.<>c();

		// Token: 0x04000189 RID: 393
		public static Predicate<Component> <>9__14_0;
	}
}
