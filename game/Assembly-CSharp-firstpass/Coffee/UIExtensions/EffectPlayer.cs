using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Coffee.UIExtensions
{
	// Token: 0x02000095 RID: 149
	[Serializable]
	public class EffectPlayer
	{
		// Token: 0x0600057C RID: 1404 RVA: 0x000283AC File Offset: 0x000265AC
		public void OnEnable(Action<float> callback = null)
		{
			if (EffectPlayer.s_UpdateActions == null)
			{
				EffectPlayer.s_UpdateActions = new List<Action>();
				Canvas.willRenderCanvases += delegate()
				{
					int count = EffectPlayer.s_UpdateActions.Count;
					for (int i = 0; i < count; i++)
					{
						EffectPlayer.s_UpdateActions[i]();
					}
				};
			}
			EffectPlayer.s_UpdateActions.Add(new Action(this.OnWillRenderCanvases));
			this._time = 0f;
			this._callback = callback;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00028416 File Offset: 0x00026616
		public void OnDisable()
		{
			this._callback = null;
			EffectPlayer.s_UpdateActions.Remove(new Action(this.OnWillRenderCanvases));
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00028436 File Offset: 0x00026636
		public void Play(Action<float> callback = null)
		{
			this._time = 0f;
			this.play = true;
			if (callback != null)
			{
				this._callback = callback;
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00028454 File Offset: 0x00026654
		public void Stop()
		{
			this.play = false;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00028460 File Offset: 0x00026660
		private void OnWillRenderCanvases()
		{
			if (!this.play || !Application.isPlaying || this._callback == null)
			{
				return;
			}
			this._time += ((this.updateMode == AnimatorUpdateMode.UnscaledTime) ? Time.unscaledDeltaTime : Time.deltaTime);
			float obj = this._time / this.duration;
			if (this.duration <= this._time)
			{
				this.play = this.loop;
				this._time = (this.loop ? (-this.loopDelay) : 0f);
			}
			this._callback(obj);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000284F7 File Offset: 0x000266F7
		public EffectPlayer()
		{
		}

		// Token: 0x04000512 RID: 1298
		[Tooltip("Playing.")]
		public bool play;

		// Token: 0x04000513 RID: 1299
		[Tooltip("Loop.")]
		public bool loop;

		// Token: 0x04000514 RID: 1300
		[Tooltip("Duration.")]
		[Range(0.01f, 10f)]
		public float duration = 1f;

		// Token: 0x04000515 RID: 1301
		[Tooltip("Delay before looping.")]
		[Range(0f, 10f)]
		public float loopDelay;

		// Token: 0x04000516 RID: 1302
		[Tooltip("Update mode")]
		public AnimatorUpdateMode updateMode;

		// Token: 0x04000517 RID: 1303
		private static List<Action> s_UpdateActions;

		// Token: 0x04000518 RID: 1304
		private float _time;

		// Token: 0x04000519 RID: 1305
		private Action<float> _callback;

		// Token: 0x020001C9 RID: 457
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000F9C RID: 3996 RVA: 0x00063A70 File Offset: 0x00061C70
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000F9D RID: 3997 RVA: 0x00063A7C File Offset: 0x00061C7C
			public <>c()
			{
			}

			// Token: 0x06000F9E RID: 3998 RVA: 0x00063A84 File Offset: 0x00061C84
			internal void <OnEnable>b__6_0()
			{
				int count = EffectPlayer.s_UpdateActions.Count;
				for (int i = 0; i < count; i++)
				{
					EffectPlayer.s_UpdateActions[i]();
				}
			}

			// Token: 0x04000DF2 RID: 3570
			public static readonly EffectPlayer.<>c <>9 = new EffectPlayer.<>c();

			// Token: 0x04000DF3 RID: 3571
			public static Canvas.WillRenderCanvases <>9__6_0;
		}
	}
}
