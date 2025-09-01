using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class ES3GlobalManager : MonoBehaviour
{
	// Token: 0x060000E2 RID: 226 RVA: 0x00004B54 File Offset: 0x00002D54
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
	private static void Run()
	{
		GameObject gameObject = new GameObject("Easy Save 3 Global Manager");
		gameObject.AddComponent<ES3GlobalManager>();
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x00004B6C File Offset: 0x00002D6C
	public IEnumerator Start()
	{
		for (;;)
		{
			yield return new WaitForEndOfFrame();
			if ((ES3Settings.defaultSettings.location == ES3.Location.Cache && ES3Settings.defaultSettings.storeCacheAtEndOfEveryFrame) || this.storeCache)
			{
				ES3File.StoreAll();
				this.storeCache = false;
			}
		}
		yield break;
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00004B7B File Offset: 0x00002D7B
	private void OnApplicationQuit()
	{
		if (ES3Settings.defaultSettings.storeCacheOnApplicationQuit)
		{
			this.storeCache = true;
		}
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00004B90 File Offset: 0x00002D90
	private void OnApplicationPause(bool paused)
	{
		if ((ES3Settings.defaultSettings.storeCacheOnApplicationPause || (Application.isMobilePlatform && ES3Settings.defaultSettings.storeCacheOnApplicationQuit)) && paused)
		{
			this.storeCache = true;
		}
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00004BC0 File Offset: 0x00002DC0
	public ES3GlobalManager()
	{
	}

	// Token: 0x0400001C RID: 28
	private bool storeCache;

	// Token: 0x020000F7 RID: 247
	[CompilerGenerated]
	private sealed class <Start>d__2 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06000555 RID: 1365 RVA: 0x0001F4F7 File Offset: 0x0001D6F7
		[DebuggerHidden]
		public <Start>d__2(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001F506 File Offset: 0x0001D706
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001F508 File Offset: 0x0001D708
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			ES3GlobalManager es3GlobalManager = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				if ((ES3Settings.defaultSettings.location == ES3.Location.Cache && ES3Settings.defaultSettings.storeCacheAtEndOfEveryFrame) || es3GlobalManager.storeCache)
				{
					ES3File.StoreAll();
					es3GlobalManager.storeCache = false;
				}
			}
			else
			{
				this.<>1__state = -1;
			}
			this.<>2__current = new WaitForEndOfFrame();
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0001F57C File Offset: 0x0001D77C
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001F584 File Offset: 0x0001D784
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0001F58B File Offset: 0x0001D78B
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040001AC RID: 428
		private int <>1__state;

		// Token: 0x040001AD RID: 429
		private object <>2__current;

		// Token: 0x040001AE RID: 430
		public ES3GlobalManager <>4__this;
	}
}
