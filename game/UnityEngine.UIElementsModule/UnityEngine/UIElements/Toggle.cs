using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000189 RID: 393
	public class Toggle : BaseBoolField
	{
		// Token: 0x06000CC5 RID: 3269 RVA: 0x00035260 File Offset: 0x00033460
		public Toggle() : this(null)
		{
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0003526C File Offset: 0x0003346C
		public Toggle(string label) : base(label)
		{
			base.AddToClassList(Toggle.ussClassName);
			base.visualInput.AddToClassList(Toggle.inputUssClassName);
			base.labelElement.AddToClassList(Toggle.labelUssClassName);
			this.m_CheckMark.AddToClassList(Toggle.checkmarkUssClassName);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000352C1 File Offset: 0x000334C1
		protected override void InitLabel()
		{
			base.InitLabel();
			this.m_Label.AddToClassList(Toggle.textUssClassName);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000352DC File Offset: 0x000334DC
		// Note: this type is marked as 'beforefieldinit'.
		static Toggle()
		{
		}

		// Token: 0x040005F3 RID: 1523
		public new static readonly string ussClassName = "unity-toggle";

		// Token: 0x040005F4 RID: 1524
		public new static readonly string labelUssClassName = Toggle.ussClassName + "__label";

		// Token: 0x040005F5 RID: 1525
		public new static readonly string inputUssClassName = Toggle.ussClassName + "__input";

		// Token: 0x040005F6 RID: 1526
		[Obsolete]
		public static readonly string noTextVariantUssClassName = Toggle.ussClassName + "--no-text";

		// Token: 0x040005F7 RID: 1527
		public static readonly string checkmarkUssClassName = Toggle.ussClassName + "__checkmark";

		// Token: 0x040005F8 RID: 1528
		public static readonly string textUssClassName = Toggle.ussClassName + "__text";

		// Token: 0x0200018A RID: 394
		public new class UxmlFactory : UxmlFactory<Toggle, Toggle.UxmlTraits>
		{
			// Token: 0x06000CC9 RID: 3273 RVA: 0x00035357 File Offset: 0x00033557
			public UxmlFactory()
			{
			}
		}

		// Token: 0x0200018B RID: 395
		public new class UxmlTraits : BaseFieldTraits<bool, UxmlBoolAttributeDescription>
		{
			// Token: 0x06000CCA RID: 3274 RVA: 0x00035360 File Offset: 0x00033560
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((Toggle)ve).text = this.m_Text.GetValueFromBag(bag, cc);
			}

			// Token: 0x06000CCB RID: 3275 RVA: 0x00035386 File Offset: 0x00033586
			public UxmlTraits()
			{
			}

			// Token: 0x040005F9 RID: 1529
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};
		}
	}
}
