using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000107 RID: 263
	[ExecuteAlways]
	public class UIFoldout : Toggle
	{
		// Token: 0x060007E0 RID: 2016 RVA: 0x00021ED1 File Offset: 0x000200D1
		protected override void Start()
		{
			base.Start();
			this.onValueChanged.AddListener(new UnityAction<bool>(this.SetState));
			this.SetState(base.isOn);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00021EFC File Offset: 0x000200FC
		private void OnValidate()
		{
			this.SetState(base.isOn, false);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00021F0B File Offset: 0x0002010B
		public void SetState(bool state)
		{
			this.SetState(state, true);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00021F18 File Offset: 0x00020118
		public void SetState(bool state, bool rebuildLayout)
		{
			if (this.arrowOpened == null || this.arrowClosed == null || this.content == null)
			{
				return;
			}
			if (this.arrowOpened.activeSelf != state)
			{
				this.arrowOpened.SetActive(state);
			}
			if (this.arrowClosed.activeSelf == state)
			{
				this.arrowClosed.SetActive(!state);
			}
			if (this.content.activeSelf != state)
			{
				this.content.SetActive(state);
			}
			if (rebuildLayout)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(base.transform.parent as RectTransform);
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00021FB9 File Offset: 0x000201B9
		public UIFoldout()
		{
		}

		// Token: 0x04000451 RID: 1105
		public GameObject content;

		// Token: 0x04000452 RID: 1106
		public GameObject arrowOpened;

		// Token: 0x04000453 RID: 1107
		public GameObject arrowClosed;
	}
}
