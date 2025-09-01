using System;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000FD RID: 253
	public class DebugUIHandlerRow : DebugUIHandlerFoldout
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x00020BD3 File Offset: 0x0001EDD3
		protected override void OnEnable()
		{
			this.m_Timer = 0f;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00020BE0 File Offset: 0x0001EDE0
		protected void Update()
		{
			DebugUI.Table.Row row = base.CastWidget<DebugUI.Table.Row>();
			DebugUI.Table table = row.parent as DebugUI.Table;
			float num = 0.1f;
			bool flag = this.m_Timer >= num;
			if (flag)
			{
				this.m_Timer -= num;
			}
			this.m_Timer += Time.deltaTime;
			for (int i = 0; i < row.children.Count; i++)
			{
				GameObject gameObject = base.gameObject.transform.GetChild(1).GetChild(i).gameObject;
				bool columnVisibility = table.GetColumnVisibility(i);
				gameObject.SetActive(columnVisibility);
				if (columnVisibility && flag)
				{
					DebugUIHandlerColor debugUIHandlerColor;
					if (gameObject.TryGetComponent<DebugUIHandlerColor>(out debugUIHandlerColor))
					{
						debugUIHandlerColor.UpdateColor();
					}
					DebugUIHandlerToggle debugUIHandlerToggle;
					if (gameObject.TryGetComponent<DebugUIHandlerToggle>(out debugUIHandlerToggle))
					{
						debugUIHandlerToggle.UpdateValueLabel();
					}
				}
			}
			DebugUIHandlerWidget debugUIHandlerWidget = base.gameObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<DebugUIHandlerWidget>();
			DebugUIHandlerWidget previousUIHandler = null;
			for (int j = 0; j < row.children.Count; j++)
			{
				debugUIHandlerWidget.previousUIHandler = previousUIHandler;
				if (table.GetColumnVisibility(j))
				{
					previousUIHandler = debugUIHandlerWidget;
				}
				bool flag2 = false;
				for (int k = j + 1; k < row.children.Count; k++)
				{
					if (table.GetColumnVisibility(k))
					{
						DebugUIHandlerWidget component = base.gameObject.transform.GetChild(1).GetChild(k).gameObject.GetComponent<DebugUIHandlerWidget>();
						debugUIHandlerWidget.nextUIHandler = component;
						debugUIHandlerWidget = component;
						j = k - 1;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					debugUIHandlerWidget.nextUIHandler = null;
					return;
				}
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00020D7E File Offset: 0x0001EF7E
		public DebugUIHandlerRow()
		{
		}

		// Token: 0x04000427 RID: 1063
		private float m_Timer;
	}
}
