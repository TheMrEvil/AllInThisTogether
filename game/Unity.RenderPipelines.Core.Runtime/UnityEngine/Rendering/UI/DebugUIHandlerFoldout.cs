using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F4 RID: 244
	public class DebugUIHandlerFoldout : DebugUIHandlerWidget
	{
		// Token: 0x0600073A RID: 1850 RVA: 0x00020070 File Offset: 0x0001E270
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Foldout>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			string[] columnLabels = this.m_Field.columnLabels;
			int num = (columnLabels != null) ? columnLabels.Length : 0;
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.nameLabel.gameObject, base.GetComponent<DebugUIHandlerContainer>().contentHolder);
				gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
				RectTransform rectTransform = gameObject.transform as RectTransform;
				RectTransform rectTransform2 = this.nameLabel.transform as RectTransform;
				Vector2 vector = new Vector2(0f, 1f);
				rectTransform.anchorMin = vector;
				rectTransform.anchorMax = vector;
				rectTransform.sizeDelta = new Vector2(100f, 26f);
				Vector3 v = rectTransform2.anchoredPosition;
				v.x += (float)(i + 1) * 60f + 230f;
				rectTransform.anchoredPosition = v;
				rectTransform.pivot = new Vector2(0f, 0.5f);
				rectTransform.eulerAngles = new Vector3(0f, 0f, 13f);
				Text component = gameObject.GetComponent<Text>();
				component.fontSize = 15;
				component.text = this.m_Field.columnLabels[i];
			}
			this.UpdateValue();
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x000201D8 File Offset: 0x0001E3D8
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			if (fromNext || !this.valueToggle.isOn)
			{
				this.nameLabel.color = this.colorSelected;
			}
			else if (this.valueToggle.isOn)
			{
				if (this.m_Container.IsDirectChild(previous))
				{
					this.nameLabel.color = this.colorSelected;
				}
				else
				{
					DebugUIHandlerWidget lastItem = this.m_Container.GetLastItem();
					DebugManager.instance.ChangeSelection(lastItem, false);
				}
			}
			return true;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0002024F File Offset: 0x0001E44F
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00020262 File Offset: 0x0001E462
		public override void OnIncrement(bool fast)
		{
			this.m_Field.SetValue(true);
			this.UpdateValue();
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00020276 File Offset: 0x0001E476
		public override void OnDecrement(bool fast)
		{
			this.m_Field.SetValue(false);
			this.UpdateValue();
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0002028C File Offset: 0x0001E48C
		public override void OnAction()
		{
			bool value = !this.m_Field.GetValue();
			this.m_Field.SetValue(value);
			this.UpdateValue();
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x000202BA File Offset: 0x0001E4BA
		private void UpdateValue()
		{
			this.valueToggle.isOn = this.m_Field.GetValue();
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000202D4 File Offset: 0x0001E4D4
		public override DebugUIHandlerWidget Next()
		{
			if (!this.m_Field.GetValue() || this.m_Container == null)
			{
				return base.Next();
			}
			DebugUIHandlerWidget firstItem = this.m_Container.GetFirstItem();
			if (firstItem == null)
			{
				return base.Next();
			}
			return firstItem;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00020320 File Offset: 0x0001E520
		public DebugUIHandlerFoldout()
		{
		}

		// Token: 0x040003FA RID: 1018
		public Text nameLabel;

		// Token: 0x040003FB RID: 1019
		public UIFoldout valueToggle;

		// Token: 0x040003FC RID: 1020
		private DebugUI.Foldout m_Field;

		// Token: 0x040003FD RID: 1021
		private DebugUIHandlerContainer m_Container;

		// Token: 0x040003FE RID: 1022
		private const float xDecal = 60f;

		// Token: 0x040003FF RID: 1023
		private const float xDecalInit = 230f;
	}
}
