using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000143 RID: 323
	public class HelpBox : VisualElement
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0002A818 File Offset: 0x00028A18
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x0002A835 File Offset: 0x00028A35
		public string text
		{
			get
			{
				return this.m_Label.text;
			}
			set
			{
				this.m_Label.text = value;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0002A848 File Offset: 0x00028A48
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x0002A860 File Offset: 0x00028A60
		public HelpBoxMessageType messageType
		{
			get
			{
				return this.m_HelpBoxMessageType;
			}
			set
			{
				bool flag = value != this.m_HelpBoxMessageType;
				if (flag)
				{
					this.m_HelpBoxMessageType = value;
					this.UpdateIcon(value);
				}
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0002A88F File Offset: 0x00028A8F
		public HelpBox() : this(string.Empty, HelpBoxMessageType.None)
		{
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0002A8A0 File Offset: 0x00028AA0
		public HelpBox(string text, HelpBoxMessageType messageType)
		{
			base.AddToClassList(HelpBox.ussClassName);
			this.m_HelpBoxMessageType = messageType;
			this.m_Label = new Label(text);
			this.m_Label.AddToClassList(HelpBox.labelUssClassName);
			base.Add(this.m_Label);
			this.m_Icon = new VisualElement();
			this.m_Icon.AddToClassList(HelpBox.iconUssClassName);
			this.UpdateIcon(messageType);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002A918 File Offset: 0x00028B18
		private string GetIconClass(HelpBoxMessageType messageType)
		{
			string result;
			switch (messageType)
			{
			case HelpBoxMessageType.Info:
				result = HelpBox.iconInfoUssClassName;
				break;
			case HelpBoxMessageType.Warning:
				result = HelpBox.iconwarningUssClassName;
				break;
			case HelpBoxMessageType.Error:
				result = HelpBox.iconErrorUssClassName;
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002A960 File Offset: 0x00028B60
		private void UpdateIcon(HelpBoxMessageType messageType)
		{
			bool flag = !string.IsNullOrEmpty(this.m_IconClass);
			if (flag)
			{
				this.m_Icon.RemoveFromClassList(this.m_IconClass);
			}
			this.m_IconClass = this.GetIconClass(messageType);
			bool flag2 = this.m_IconClass == null;
			if (flag2)
			{
				this.m_Icon.RemoveFromHierarchy();
			}
			else
			{
				this.m_Icon.AddToClassList(this.m_IconClass);
				bool flag3 = this.m_Icon.parent == null;
				if (flag3)
				{
					base.Insert(0, this.m_Icon);
				}
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0002A9F4 File Offset: 0x00028BF4
		// Note: this type is marked as 'beforefieldinit'.
		static HelpBox()
		{
		}

		// Token: 0x0400049A RID: 1178
		public static readonly string ussClassName = "unity-help-box";

		// Token: 0x0400049B RID: 1179
		public static readonly string labelUssClassName = HelpBox.ussClassName + "__label";

		// Token: 0x0400049C RID: 1180
		public static readonly string iconUssClassName = HelpBox.ussClassName + "__icon";

		// Token: 0x0400049D RID: 1181
		public static readonly string iconInfoUssClassName = HelpBox.iconUssClassName + "--info";

		// Token: 0x0400049E RID: 1182
		public static readonly string iconwarningUssClassName = HelpBox.iconUssClassName + "--warning";

		// Token: 0x0400049F RID: 1183
		public static readonly string iconErrorUssClassName = HelpBox.iconUssClassName + "--error";

		// Token: 0x040004A0 RID: 1184
		private HelpBoxMessageType m_HelpBoxMessageType;

		// Token: 0x040004A1 RID: 1185
		private VisualElement m_Icon;

		// Token: 0x040004A2 RID: 1186
		private string m_IconClass;

		// Token: 0x040004A3 RID: 1187
		private Label m_Label;

		// Token: 0x02000144 RID: 324
		public new class UxmlFactory : UxmlFactory<HelpBox, HelpBox.UxmlTraits>
		{
			// Token: 0x06000A9A RID: 2714 RVA: 0x0002AA6F File Offset: 0x00028C6F
			public UxmlFactory()
			{
			}
		}

		// Token: 0x02000145 RID: 325
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x06000A9B RID: 2715 RVA: 0x0002AA78 File Offset: 0x00028C78
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				HelpBox helpBox = ve as HelpBox;
				helpBox.text = this.m_Text.GetValueFromBag(bag, cc);
				helpBox.messageType = this.m_MessageType.GetValueFromBag(bag, cc);
			}

			// Token: 0x06000A9C RID: 2716 RVA: 0x0002AABF File Offset: 0x00028CBF
			public UxmlTraits()
			{
			}

			// Token: 0x040004A4 RID: 1188
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			// Token: 0x040004A5 RID: 1189
			private UxmlEnumAttributeDescription<HelpBoxMessageType> m_MessageType = new UxmlEnumAttributeDescription<HelpBoxMessageType>
			{
				name = "message-type",
				defaultValue = HelpBoxMessageType.None
			};
		}
	}
}
