using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200013F RID: 319
	public class GroupBox : BindableElement, IGroupBox
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0002A6E1 File Offset: 0x000288E1
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x0002A6F8 File Offset: 0x000288F8
		public string text
		{
			get
			{
				Label titleLabel = this.m_TitleLabel;
				return (titleLabel != null) ? titleLabel.text : null;
			}
			set
			{
				bool flag = !string.IsNullOrEmpty(value);
				if (flag)
				{
					bool flag2 = this.m_TitleLabel == null;
					if (flag2)
					{
						this.m_TitleLabel = new Label(value);
						this.m_TitleLabel.AddToClassList(GroupBox.labelUssClassName);
						base.Insert(0, this.m_TitleLabel);
					}
					this.m_TitleLabel.text = value;
				}
				else
				{
					bool flag3 = this.m_TitleLabel != null;
					if (flag3)
					{
						this.m_TitleLabel.RemoveFromHierarchy();
						this.m_TitleLabel = null;
					}
				}
			}
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002A780 File Offset: 0x00028980
		public GroupBox() : this(null)
		{
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002A78B File Offset: 0x0002898B
		public GroupBox(string text)
		{
			base.AddToClassList(GroupBox.ussClassName);
			this.text = text;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002A7A9 File Offset: 0x000289A9
		// Note: this type is marked as 'beforefieldinit'.
		static GroupBox()
		{
		}

		// Token: 0x04000491 RID: 1169
		public static readonly string ussClassName = "unity-group-box";

		// Token: 0x04000492 RID: 1170
		public static readonly string labelUssClassName = GroupBox.ussClassName + "__label";

		// Token: 0x04000493 RID: 1171
		private Label m_TitleLabel;

		// Token: 0x02000140 RID: 320
		public new class UxmlFactory : UxmlFactory<GroupBox, GroupBox.UxmlTraits>
		{
			// Token: 0x06000A8E RID: 2702 RVA: 0x0002A7C9 File Offset: 0x000289C9
			public UxmlFactory()
			{
			}
		}

		// Token: 0x02000141 RID: 321
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x06000A8F RID: 2703 RVA: 0x0002A7D2 File Offset: 0x000289D2
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((GroupBox)ve).text = this.m_Text.GetValueFromBag(bag, cc);
			}

			// Token: 0x06000A90 RID: 2704 RVA: 0x0002A7F8 File Offset: 0x000289F8
			public UxmlTraits()
			{
			}

			// Token: 0x04000494 RID: 1172
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};
		}
	}
}
