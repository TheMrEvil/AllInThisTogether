using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F2 RID: 242
	public class DebugUIHandlerEnumHistory : DebugUIHandlerEnumField
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x0001FDC4 File Offset: 0x0001DFC4
		internal override void SetWidget(DebugUI.Widget widget)
		{
			DebugUI.HistoryEnumField historyEnumField = widget as DebugUI.HistoryEnumField;
			int num = (historyEnumField != null) ? historyEnumField.historyDepth : 0;
			this.historyValues = new Text[num];
			for (int i = 0; i < num; i++)
			{
				Text text = Object.Instantiate<Text>(this.valueLabel, base.transform);
				Vector3 position = text.transform.position;
				position.x += (float)(i + 1) * 60f;
				text.transform.position = position;
				Text component = text.GetComponent<Text>();
				component.color = new Color32(110, 110, 110, byte.MaxValue);
				this.historyValues[i] = component;
			}
			base.SetWidget(widget);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001FE6C File Offset: 0x0001E06C
		protected override void UpdateValueLabel()
		{
			int num = this.m_Field.currentIndex;
			if (num < 0)
			{
				num = 0;
			}
			this.valueLabel.text = this.m_Field.enumNames[num].text;
			DebugUI.HistoryEnumField historyEnumField = this.m_Field as DebugUI.HistoryEnumField;
			int num2 = (historyEnumField != null) ? historyEnumField.historyDepth : 0;
			for (int i = 0; i < num2; i++)
			{
				if (i < this.historyValues.Length && this.historyValues[i] != null)
				{
					this.historyValues[i].text = historyEnumField.enumNames[historyEnumField.GetHistoryValue(i)].text;
				}
			}
			if (base.isActiveAndEnabled)
			{
				base.StartCoroutine(this.RefreshAfterSanitization());
			}
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001FF1D File Offset: 0x0001E11D
		private IEnumerator RefreshAfterSanitization()
		{
			yield return null;
			this.m_Field.currentIndex = this.m_Field.getIndex();
			this.valueLabel.text = this.m_Field.enumNames[this.m_Field.currentIndex].text;
			yield break;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001FF2C File Offset: 0x0001E12C
		public DebugUIHandlerEnumHistory()
		{
		}

		// Token: 0x040003F5 RID: 1013
		private Text[] historyValues;

		// Token: 0x040003F6 RID: 1014
		private const float xDecal = 60f;

		// Token: 0x02000186 RID: 390
		[CompilerGenerated]
		private sealed class <RefreshAfterSanitization>d__4 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x0600092F RID: 2351 RVA: 0x00024B12 File Offset: 0x00022D12
			[DebuggerHidden]
			public <RefreshAfterSanitization>d__4(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000930 RID: 2352 RVA: 0x00024B21 File Offset: 0x00022D21
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000931 RID: 2353 RVA: 0x00024B24 File Offset: 0x00022D24
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				DebugUIHandlerEnumHistory debugUIHandlerEnumHistory = this;
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
				debugUIHandlerEnumHistory.m_Field.currentIndex = debugUIHandlerEnumHistory.m_Field.getIndex();
				debugUIHandlerEnumHistory.valueLabel.text = debugUIHandlerEnumHistory.m_Field.enumNames[debugUIHandlerEnumHistory.m_Field.currentIndex].text;
				return false;
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x06000932 RID: 2354 RVA: 0x00024BA9 File Offset: 0x00022DA9
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000933 RID: 2355 RVA: 0x00024BB1 File Offset: 0x00022DB1
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x06000934 RID: 2356 RVA: 0x00024BB8 File Offset: 0x00022DB8
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040005D1 RID: 1489
			private int <>1__state;

			// Token: 0x040005D2 RID: 1490
			private object <>2__current;

			// Token: 0x040005D3 RID: 1491
			public DebugUIHandlerEnumHistory <>4__this;
		}
	}
}
