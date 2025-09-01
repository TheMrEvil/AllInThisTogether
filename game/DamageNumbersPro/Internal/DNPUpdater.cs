using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DamageNumbersPro.Internal
{
	// Token: 0x02000019 RID: 25
	public class DNPUpdater : MonoBehaviour
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x000079C5 File Offset: 0x00005BC5
		private void Start()
		{
			base.StartCoroutine(this.UpdatePopups());
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000079D4 File Offset: 0x00005BD4
		private IEnumerator UpdatePopups()
		{
			WaitForSecondsRealtime delay = new WaitForSecondsRealtime(this.updateDelay);
			for (;;)
			{
				DNPUpdater.vectorsNeedUpdate = true;
				foreach (DamageNumber damageNumber in this.activePopups)
				{
					if (damageNumber != null)
					{
						damageNumber.UpdateDamageNumber(this.delta, this.time);
					}
					else
					{
						this.removedPopups.Add(damageNumber);
					}
				}
				if (this.removedPopups.Count > 0)
				{
					foreach (DamageNumber item in this.removedPopups)
					{
						this.activePopups.Remove(item);
					}
					this.removedPopups = new HashSet<DamageNumber>();
				}
				if (this.isUnscaled)
				{
					this.lastUpdateTime = Time.unscaledTime;
					yield return delay;
					this.time = Time.unscaledTime;
					this.delta = this.time - this.lastUpdateTime;
				}
				else
				{
					this.lastUpdateTime = Time.time;
					yield return delay;
					this.time = Time.time;
					this.delta = this.time - this.lastUpdateTime;
				}
			}
			yield break;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000079E4 File Offset: 0x00005BE4
		public static void RegisterPopup(bool unscaledTime, float updateDelay, DamageNumber popup)
		{
			ref Dictionary<float, DNPUpdater> ptr = ref unscaledTime ? ref DNPUpdater.unscaledUpdaters : ref DNPUpdater.scaledUpdaters;
			if (ptr == null)
			{
				ptr = new Dictionary<float, DNPUpdater>();
			}
			bool flag = ptr.ContainsKey(updateDelay);
			if (flag && ptr[updateDelay] != null)
			{
				ptr[updateDelay].activePopups.Add(popup);
				return;
			}
			if (flag)
			{
				ptr.Remove(updateDelay);
			}
			GameObject gameObject = new GameObject("");
			gameObject.hideFlags = HideFlags.HideInHierarchy;
			DNPUpdater dnpupdater = gameObject.AddComponent<DNPUpdater>();
			dnpupdater.activePopups = new HashSet<DamageNumber>();
			dnpupdater.removedPopups = new HashSet<DamageNumber>();
			dnpupdater.isUnscaled = unscaledTime;
			dnpupdater.updateDelay = updateDelay;
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			ptr.Add(updateDelay, dnpupdater);
			dnpupdater.activePopups.Add(popup);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00007AA0 File Offset: 0x00005CA0
		public static void UnregisterPopup(bool unscaledTime, float updateDelay, DamageNumber popup)
		{
			Dictionary<float, DNPUpdater> dictionary = unscaledTime ? DNPUpdater.unscaledUpdaters : DNPUpdater.scaledUpdaters;
			if (dictionary != null && dictionary.ContainsKey(updateDelay) && dictionary[updateDelay].activePopups.Contains(popup))
			{
				dictionary[updateDelay].removedPopups.Add(popup);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00007AF0 File Offset: 0x00005CF0
		public static void UpdateVectors(Transform popup)
		{
			DNPUpdater.vectorsNeedUpdate = false;
			DNPUpdater.upVector = popup.up;
			DNPUpdater.rightVector = popup.right;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00007B0E File Offset: 0x00005D0E
		public DNPUpdater()
		{
		}

		// Token: 0x04000151 RID: 337
		private static Dictionary<float, DNPUpdater> unscaledUpdaters;

		// Token: 0x04000152 RID: 338
		private static Dictionary<float, DNPUpdater> scaledUpdaters;

		// Token: 0x04000153 RID: 339
		public static Vector3 upVector;

		// Token: 0x04000154 RID: 340
		public static Vector3 rightVector;

		// Token: 0x04000155 RID: 341
		public static bool vectorsNeedUpdate;

		// Token: 0x04000156 RID: 342
		public static Quaternion cameraRotation;

		// Token: 0x04000157 RID: 343
		public bool isUnscaled;

		// Token: 0x04000158 RID: 344
		public float updateDelay = 0.0125f;

		// Token: 0x04000159 RID: 345
		public HashSet<DamageNumber> activePopups;

		// Token: 0x0400015A RID: 346
		public HashSet<DamageNumber> removedPopups;

		// Token: 0x0400015B RID: 347
		private float lastUpdateTime;

		// Token: 0x0400015C RID: 348
		private float delta;

		// Token: 0x0400015D RID: 349
		private float time;

		// Token: 0x0200001D RID: 29
		[CompilerGenerated]
		private sealed class <UpdatePopups>d__14 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060000BA RID: 186 RVA: 0x00007D9D File Offset: 0x00005F9D
			[DebuggerHidden]
			public <UpdatePopups>d__14(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060000BB RID: 187 RVA: 0x00007DAC File Offset: 0x00005FAC
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060000BC RID: 188 RVA: 0x00007DB0 File Offset: 0x00005FB0
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				DNPUpdater dnpupdater = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					delay = new WaitForSecondsRealtime(dnpupdater.updateDelay);
					break;
				case 1:
					this.<>1__state = -1;
					dnpupdater.time = Time.unscaledTime;
					dnpupdater.delta = dnpupdater.time - dnpupdater.lastUpdateTime;
					break;
				case 2:
					this.<>1__state = -1;
					dnpupdater.time = Time.time;
					dnpupdater.delta = dnpupdater.time - dnpupdater.lastUpdateTime;
					break;
				default:
					return false;
				}
				DNPUpdater.vectorsNeedUpdate = true;
				foreach (DamageNumber damageNumber in dnpupdater.activePopups)
				{
					if (damageNumber != null)
					{
						damageNumber.UpdateDamageNumber(dnpupdater.delta, dnpupdater.time);
					}
					else
					{
						dnpupdater.removedPopups.Add(damageNumber);
					}
				}
				if (dnpupdater.removedPopups.Count > 0)
				{
					foreach (DamageNumber item in dnpupdater.removedPopups)
					{
						dnpupdater.activePopups.Remove(item);
					}
					dnpupdater.removedPopups = new HashSet<DamageNumber>();
				}
				if (dnpupdater.isUnscaled)
				{
					dnpupdater.lastUpdateTime = Time.unscaledTime;
					this.<>2__current = delay;
					this.<>1__state = 1;
					return true;
				}
				dnpupdater.lastUpdateTime = Time.time;
				this.<>2__current = delay;
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x060000BD RID: 189 RVA: 0x00007F64 File Offset: 0x00006164
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000BE RID: 190 RVA: 0x00007F6C File Offset: 0x0000616C
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x060000BF RID: 191 RVA: 0x00007F73 File Offset: 0x00006173
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400016F RID: 367
			private int <>1__state;

			// Token: 0x04000170 RID: 368
			private object <>2__current;

			// Token: 0x04000171 RID: 369
			public DNPUpdater <>4__this;

			// Token: 0x04000172 RID: 370
			private WaitForSecondsRealtime <delay>5__2;
		}
	}
}
