using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000FF RID: 255
	public class DebugUIHandlerToggleHistory : DebugUIHandlerToggle
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x00020E94 File Offset: 0x0001F094
		internal override void SetWidget(DebugUI.Widget widget)
		{
			DebugUI.HistoryBoolField historyBoolField = widget as DebugUI.HistoryBoolField;
			int num = (historyBoolField != null) ? historyBoolField.historyDepth : 0;
			this.historyToggles = new Toggle[num];
			for (int i = 0; i < num; i++)
			{
				Toggle toggle = Object.Instantiate<Toggle>(this.valueToggle, base.transform);
				Vector3 position = toggle.transform.position;
				position.x += (float)(i + 1) * 60f;
				toggle.transform.position = position;
				Image component = toggle.transform.GetChild(0).GetComponent<Image>();
				component.sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(-1f, -1f, 2f, 2f), Vector2.zero);
				component.color = new Color32(50, 50, 50, 120);
				component.transform.GetChild(0).GetComponent<Image>().color = new Color32(110, 110, 110, byte.MaxValue);
				this.historyToggles[i] = toggle.GetComponent<Toggle>();
			}
			base.SetWidget(widget);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00020FA8 File Offset: 0x0001F1A8
		protected internal override void UpdateValueLabel()
		{
			base.UpdateValueLabel();
			DebugUI.HistoryBoolField historyBoolField = this.m_Field as DebugUI.HistoryBoolField;
			int num = (historyBoolField != null) ? historyBoolField.historyDepth : 0;
			for (int i = 0; i < num; i++)
			{
				if (i < this.historyToggles.Length && this.historyToggles[i] != null)
				{
					this.historyToggles[i].isOn = historyBoolField.GetHistoryValue(i);
				}
			}
			if (base.isActiveAndEnabled)
			{
				base.StartCoroutine(this.RefreshAfterSanitization());
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00021024 File Offset: 0x0001F224
		private IEnumerator RefreshAfterSanitization()
		{
			yield return null;
			this.valueToggle.isOn = this.m_Field.getter();
			yield break;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00021033 File Offset: 0x0001F233
		public DebugUIHandlerToggleHistory()
		{
		}

		// Token: 0x0400042C RID: 1068
		private Toggle[] historyToggles;

		// Token: 0x0400042D RID: 1069
		private const float xDecal = 60f;

		// Token: 0x02000188 RID: 392
		[CompilerGenerated]
		private sealed class <RefreshAfterSanitization>d__4 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06000937 RID: 2359 RVA: 0x00024BD8 File Offset: 0x00022DD8
			[DebuggerHidden]
			public <RefreshAfterSanitization>d__4(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000938 RID: 2360 RVA: 0x00024BE7 File Offset: 0x00022DE7
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000939 RID: 2361 RVA: 0x00024BEC File Offset: 0x00022DEC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				DebugUIHandlerToggleHistory debugUIHandlerToggleHistory = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = null;
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				debugUIHandlerToggleHistory.valueToggle.isOn = debugUIHandlerToggleHistory.m_Field.getter();
				return false;
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x0600093A RID: 2362 RVA: 0x00024C4A File Offset: 0x00022E4A
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600093B RID: 2363 RVA: 0x00024C52 File Offset: 0x00022E52
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x0600093C RID: 2364 RVA: 0x00024C59 File Offset: 0x00022E59
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040005D5 RID: 1493
			private int <>1__state;

			// Token: 0x040005D6 RID: 1494
			private object <>2__current;

			// Token: 0x040005D7 RID: 1495
			public DebugUIHandlerToggleHistory <>4__this;
		}
	}
}
