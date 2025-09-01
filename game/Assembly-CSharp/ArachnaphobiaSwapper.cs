using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class ArachnaphobiaSwapper : MonoBehaviour
{
	// Token: 0x06000A03 RID: 2563 RVA: 0x000420D0 File Offset: 0x000402D0
	private void Awake()
	{
		Settings.OnSystemSettingsChanged = (Action<SystemSetting>)Delegate.Combine(Settings.OnSystemSettingsChanged, new Action<SystemSetting>(this.OnSettingChanged));
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x000420F2 File Offset: 0x000402F2
	private IEnumerator Start()
	{
		yield return true;
		this.HideIfNeeded();
		yield break;
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x00042101 File Offset: 0x00040301
	private void OnSettingChanged(SystemSetting setting)
	{
		if (setting != SystemSetting.Arachnophobia)
		{
			return;
		}
		this.HideIfNeeded();
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x0004210F File Offset: 0x0004030F
	private void HideIfNeeded()
	{
		if (!Settings.GetBool(SystemSetting.Arachnophobia, false))
		{
			return;
		}
		this.TryHide();
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x00042124 File Offset: 0x00040324
	private void TryHide()
	{
		if (this.ReplacementObjects.Length == 0 || this.ReplacementObjects[0].gameObject.activeSelf)
		{
			return;
		}
		EntityDisplay component = base.GetComponent<EntityDisplay>();
		if (component == null)
		{
			return;
		}
		component.ResetDisplay();
		foreach (Renderer renderer in component.Meshes)
		{
			renderer.enabled = false;
		}
		component.ClearMeshes();
		foreach (MeshRenderer meshRenderer in this.ReplacementObjects)
		{
			meshRenderer.gameObject.SetActive(true);
			component.AddMesh(meshRenderer);
		}
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x000421E0 File Offset: 0x000403E0
	private void OnDestroy()
	{
		Settings.OnSystemSettingsChanged = (Action<SystemSetting>)Delegate.Remove(Settings.OnSystemSettingsChanged, new Action<SystemSetting>(this.OnSettingChanged));
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x00042202 File Offset: 0x00040402
	public ArachnaphobiaSwapper()
	{
	}

	// Token: 0x04000894 RID: 2196
	public MeshRenderer[] ReplacementObjects;

	// Token: 0x020004D4 RID: 1236
	[CompilerGenerated]
	private sealed class <Start>d__2 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060022FC RID: 8956 RVA: 0x000C837C File Offset: 0x000C657C
		[DebuggerHidden]
		public <Start>d__2(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x000C838B File Offset: 0x000C658B
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000C8390 File Offset: 0x000C6590
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			ArachnaphobiaSwapper arachnaphobiaSwapper = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			arachnaphobiaSwapper.HideIfNeeded();
			return false;
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x000C83DE File Offset: 0x000C65DE
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x000C83E6 File Offset: 0x000C65E6
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x000C83ED File Offset: 0x000C65ED
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400247C RID: 9340
		private int <>1__state;

		// Token: 0x0400247D RID: 9341
		private object <>2__current;

		// Token: 0x0400247E RID: 9342
		public ArachnaphobiaSwapper <>4__this;
	}
}
