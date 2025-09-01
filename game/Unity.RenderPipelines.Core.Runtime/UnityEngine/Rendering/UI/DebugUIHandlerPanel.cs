using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000FB RID: 251
	public class DebugUIHandlerPanel : MonoBehaviour
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x000208C9 File Offset: 0x0001EAC9
		private void OnEnable()
		{
			this.m_ScrollTransform = this.scrollRect.GetComponent<RectTransform>();
			this.m_ContentTransform = base.GetComponent<DebugUIHandlerContainer>().contentHolder;
			this.m_MaskTransform = base.GetComponentInChildren<Mask>(true).rectTransform;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000208FF File Offset: 0x0001EAFF
		internal void SetPanel(DebugUI.Panel panel)
		{
			this.m_Panel = panel;
			this.nameLabel.text = panel.displayName;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00020919 File Offset: 0x0001EB19
		internal DebugUI.Panel GetPanel()
		{
			return this.m_Panel;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00020921 File Offset: 0x0001EB21
		public void SelectNextItem()
		{
			this.Canvas.SelectNextPanel();
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0002092E File Offset: 0x0001EB2E
		public void SelectPreviousItem()
		{
			this.Canvas.SelectPreviousPanel();
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0002093B File Offset: 0x0001EB3B
		public void OnScrollbarClicked()
		{
			DebugManager.instance.SetScrollTarget(null);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00020948 File Offset: 0x0001EB48
		internal void SetScrollTarget(DebugUIHandlerWidget target)
		{
			this.m_ScrollTarget = target;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00020954 File Offset: 0x0001EB54
		internal void UpdateScroll()
		{
			if (this.m_ScrollTarget == null)
			{
				return;
			}
			RectTransform component = this.m_ScrollTarget.GetComponent<RectTransform>();
			float yposInScroll = this.GetYPosInScroll(component);
			float num = (this.GetYPosInScroll(this.m_MaskTransform) - yposInScroll) / (this.m_ContentTransform.rect.size.y - this.m_ScrollTransform.rect.size.y);
			float num2 = this.scrollRect.verticalNormalizedPosition - num;
			num2 = Mathf.Clamp01(num2);
			this.scrollRect.verticalNormalizedPosition = Mathf.Lerp(this.scrollRect.verticalNormalizedPosition, num2, Time.deltaTime * 10f);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00020A04 File Offset: 0x0001EC04
		private float GetYPosInScroll(RectTransform target)
		{
			Vector3 b = new Vector3((0.5f - target.pivot.x) * target.rect.size.x, (0.5f - target.pivot.y) * target.rect.size.y, 0f);
			Vector3 position = target.localPosition + b;
			Vector3 position2 = target.parent.TransformPoint(position);
			return this.m_ScrollTransform.TransformPoint(position2).y;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00020A92 File Offset: 0x0001EC92
		internal DebugUIHandlerWidget GetFirstItem()
		{
			return base.GetComponent<DebugUIHandlerContainer>().GetFirstItem();
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00020A9F File Offset: 0x0001EC9F
		public void ResetDebugManager()
		{
			DebugManager.instance.Reset();
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00020AAB File Offset: 0x0001ECAB
		public DebugUIHandlerPanel()
		{
		}

		// Token: 0x0400041B RID: 1051
		public Text nameLabel;

		// Token: 0x0400041C RID: 1052
		public ScrollRect scrollRect;

		// Token: 0x0400041D RID: 1053
		public RectTransform viewport;

		// Token: 0x0400041E RID: 1054
		public DebugUIHandlerCanvas Canvas;

		// Token: 0x0400041F RID: 1055
		private RectTransform m_ScrollTransform;

		// Token: 0x04000420 RID: 1056
		private RectTransform m_ContentTransform;

		// Token: 0x04000421 RID: 1057
		private RectTransform m_MaskTransform;

		// Token: 0x04000422 RID: 1058
		private DebugUIHandlerWidget m_ScrollTarget;

		// Token: 0x04000423 RID: 1059
		protected internal DebugUI.Panel m_Panel;
	}
}
