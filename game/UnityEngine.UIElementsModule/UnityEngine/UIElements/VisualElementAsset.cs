using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002F4 RID: 756
	[Serializable]
	internal class VisualElementAsset : IUxmlAttributes, ISerializationCallbackReceiver
	{
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x00065ED4 File Offset: 0x000640D4
		// (set) Token: 0x06001904 RID: 6404 RVA: 0x00065EEC File Offset: 0x000640EC
		public int id
		{
			get
			{
				return this.m_Id;
			}
			set
			{
				this.m_Id = value;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x00065EF8 File Offset: 0x000640F8
		// (set) Token: 0x06001906 RID: 6406 RVA: 0x00065F10 File Offset: 0x00064110
		public int orderInDocument
		{
			get
			{
				return this.m_OrderInDocument;
			}
			set
			{
				this.m_OrderInDocument = value;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x00065F1C File Offset: 0x0006411C
		// (set) Token: 0x06001908 RID: 6408 RVA: 0x00065F34 File Offset: 0x00064134
		public int parentId
		{
			get
			{
				return this.m_ParentId;
			}
			set
			{
				this.m_ParentId = value;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x00065F40 File Offset: 0x00064140
		// (set) Token: 0x0600190A RID: 6410 RVA: 0x00065F58 File Offset: 0x00064158
		public int ruleIndex
		{
			get
			{
				return this.m_RuleIndex;
			}
			set
			{
				this.m_RuleIndex = value;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x00065F64 File Offset: 0x00064164
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x00065F7C File Offset: 0x0006417C
		public string fullTypeName
		{
			get
			{
				return this.m_FullTypeName;
			}
			set
			{
				this.m_FullTypeName = value;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x00065F88 File Offset: 0x00064188
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x00065FA0 File Offset: 0x000641A0
		public string[] classes
		{
			get
			{
				return this.m_Classes;
			}
			set
			{
				this.m_Classes = value;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x00065FAC File Offset: 0x000641AC
		// (set) Token: 0x06001910 RID: 6416 RVA: 0x00065FD6 File Offset: 0x000641D6
		public List<string> stylesheetPaths
		{
			get
			{
				List<string> result;
				if ((result = this.m_StylesheetPaths) == null)
				{
					result = (this.m_StylesheetPaths = new List<string>());
				}
				return result;
			}
			set
			{
				this.m_StylesheetPaths = value;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x00065FE0 File Offset: 0x000641E0
		public bool hasStylesheetPaths
		{
			get
			{
				return this.m_StylesheetPaths != null;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x00065FEC File Offset: 0x000641EC
		// (set) Token: 0x06001913 RID: 6419 RVA: 0x00066016 File Offset: 0x00064216
		public List<StyleSheet> stylesheets
		{
			get
			{
				List<StyleSheet> result;
				if ((result = this.m_Stylesheets) == null)
				{
					result = (this.m_Stylesheets = new List<StyleSheet>());
				}
				return result;
			}
			set
			{
				this.m_Stylesheets = value;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x00066020 File Offset: 0x00064220
		public bool hasStylesheets
		{
			get
			{
				return this.m_Stylesheets != null;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x0006602B File Offset: 0x0006422B
		// (set) Token: 0x06001916 RID: 6422 RVA: 0x00066033 File Offset: 0x00064233
		internal bool skipClone
		{
			get
			{
				return this.m_SkipClone;
			}
			set
			{
				this.m_SkipClone = value;
			}
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0006603C File Offset: 0x0006423C
		public VisualElementAsset(string fullTypeName)
		{
			this.m_FullTypeName = fullTypeName;
			this.m_Name = string.Empty;
			this.m_Text = string.Empty;
			this.m_PickingMode = PickingMode.Position;
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00002166 File Offset: 0x00000366
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0006606C File Offset: 0x0006426C
		public void OnAfterDeserialize()
		{
			bool flag = !string.IsNullOrEmpty(this.m_Name) && !this.m_Properties.Contains("name");
			if (flag)
			{
				this.AddProperty("name", this.m_Name);
			}
			bool flag2 = !string.IsNullOrEmpty(this.m_Text) && !this.m_Properties.Contains("text");
			if (flag2)
			{
				this.AddProperty("text", this.m_Text);
			}
			bool flag3 = this.m_PickingMode != PickingMode.Position && !this.m_Properties.Contains("picking-mode") && !this.m_Properties.Contains("pickingMode");
			if (flag3)
			{
				this.AddProperty("picking-mode", this.m_PickingMode.ToString());
			}
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x00066143 File Offset: 0x00064343
		public void AddProperty(string propertyName, string propertyValue)
		{
			this.SetOrAddProperty(propertyName, propertyValue);
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00066150 File Offset: 0x00064350
		private void SetOrAddProperty(string propertyName, string propertyValue)
		{
			bool flag = this.m_Properties == null;
			if (flag)
			{
				this.m_Properties = new List<string>();
			}
			for (int i = 0; i < this.m_Properties.Count - 1; i += 2)
			{
				bool flag2 = this.m_Properties[i] == propertyName;
				if (flag2)
				{
					this.m_Properties[i + 1] = propertyValue;
					return;
				}
			}
			this.m_Properties.Add(propertyName);
			this.m_Properties.Add(propertyValue);
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x000661D8 File Offset: 0x000643D8
		public bool TryGetAttributeValue(string propertyName, out string value)
		{
			bool flag = this.m_Properties == null;
			bool result;
			if (flag)
			{
				value = null;
				result = false;
			}
			else
			{
				for (int i = 0; i < this.m_Properties.Count - 1; i += 2)
				{
					bool flag2 = this.m_Properties[i] == propertyName;
					if (flag2)
					{
						value = this.m_Properties[i + 1];
						return true;
					}
				}
				value = null;
				result = false;
			}
			return result;
		}

		// Token: 0x04000AB9 RID: 2745
		[SerializeField]
		private string m_Name;

		// Token: 0x04000ABA RID: 2746
		[SerializeField]
		private int m_Id;

		// Token: 0x04000ABB RID: 2747
		[SerializeField]
		private int m_OrderInDocument;

		// Token: 0x04000ABC RID: 2748
		[SerializeField]
		private int m_ParentId;

		// Token: 0x04000ABD RID: 2749
		[SerializeField]
		private int m_RuleIndex;

		// Token: 0x04000ABE RID: 2750
		[SerializeField]
		private string m_Text;

		// Token: 0x04000ABF RID: 2751
		[SerializeField]
		private PickingMode m_PickingMode;

		// Token: 0x04000AC0 RID: 2752
		[SerializeField]
		private string m_FullTypeName;

		// Token: 0x04000AC1 RID: 2753
		[SerializeField]
		private string[] m_Classes;

		// Token: 0x04000AC2 RID: 2754
		[SerializeField]
		private List<string> m_StylesheetPaths;

		// Token: 0x04000AC3 RID: 2755
		[SerializeField]
		private List<StyleSheet> m_Stylesheets;

		// Token: 0x04000AC4 RID: 2756
		[SerializeField]
		private bool m_SkipClone;

		// Token: 0x04000AC5 RID: 2757
		[SerializeField]
		private List<string> m_Properties;
	}
}
