using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000138 RID: 312
	public class Foldout : BindableElement, INotifyValueChanged<bool>
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x00029380 File Offset: 0x00027580
		internal Toggle toggle
		{
			get
			{
				return this.m_Toggle;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00029388 File Offset: 0x00027588
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_Container;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00029390 File Offset: 0x00027590
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x0002939D File Offset: 0x0002759D
		public string text
		{
			get
			{
				return this.m_Toggle.text;
			}
			set
			{
				this.m_Toggle.text = value;
				VisualElement visualElement = this.m_Toggle.visualInput.Q(null, Toggle.textUssClassName);
				if (visualElement != null)
				{
					visualElement.AddToClassList(Foldout.textUssClassName);
				}
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x000293D4 File Offset: 0x000275D4
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x000293DC File Offset: 0x000275DC
		public bool value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				bool flag = this.m_Value == value;
				if (!flag)
				{
					using (ChangeEvent<bool> pooled = ChangeEvent<bool>.GetPooled(this.m_Value, value))
					{
						pooled.target = this;
						this.SetValueWithoutNotify(value);
						this.SendEvent(pooled);
						base.SaveViewData();
					}
				}
			}
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00029444 File Offset: 0x00027644
		public void SetValueWithoutNotify(bool newValue)
		{
			this.m_Value = newValue;
			this.m_Toggle.value = this.m_Value;
			this.contentContainer.style.display = (newValue ? DisplayStyle.Flex : DisplayStyle.None);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00029480 File Offset: 0x00027680
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			this.SetValueWithoutNotify(this.m_Value);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000294B4 File Offset: 0x000276B4
		public Foldout()
		{
			this.m_Value = true;
			base.AddToClassList(Foldout.ussClassName);
			base.delegatesFocus = true;
			this.m_Toggle.RegisterValueChangedCallback(delegate(ChangeEvent<bool> evt)
			{
				this.value = this.m_Toggle.value;
				evt.StopPropagation();
			});
			this.m_Toggle.AddToClassList(Foldout.toggleUssClassName);
			this.m_Toggle.visualInput.AddToClassList(Foldout.inputUssClassName);
			this.m_Toggle.visualInput.Q(null, Toggle.checkmarkUssClassName).AddToClassList(Foldout.checkmarkUssClassName);
			base.hierarchy.Add(this.m_Toggle);
			this.m_Container = new VisualElement
			{
				name = "unity-content"
			};
			this.m_Container.AddToClassList(Foldout.contentUssClassName);
			base.hierarchy.Add(this.m_Container);
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000295C0 File Offset: 0x000277C0
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			for (int i = 0; i <= Foldout.ussFoldoutMaxDepth; i++)
			{
				base.RemoveFromClassList(Foldout.ussFoldoutDepthClassName + i.ToString());
			}
			base.RemoveFromClassList(Foldout.ussFoldoutDepthClassName + "max");
			int foldoutDepth = this.GetFoldoutDepth();
			bool flag = foldoutDepth > Foldout.ussFoldoutMaxDepth;
			if (flag)
			{
				base.AddToClassList(Foldout.ussFoldoutDepthClassName + "max");
			}
			else
			{
				base.AddToClassList(Foldout.ussFoldoutDepthClassName + foldoutDepth.ToString());
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002965C File Offset: 0x0002785C
		// Note: this type is marked as 'beforefieldinit'.
		static Foldout()
		{
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000296F1 File Offset: 0x000278F1
		[CompilerGenerated]
		private void <.ctor>b__25_0(ChangeEvent<bool> evt)
		{
			this.value = this.m_Toggle.value;
			evt.StopPropagation();
		}

		// Token: 0x0400046E RID: 1134
		private Toggle m_Toggle = new Toggle
		{
			value = true
		};

		// Token: 0x0400046F RID: 1135
		private VisualElement m_Container;

		// Token: 0x04000470 RID: 1136
		[SerializeField]
		private bool m_Value;

		// Token: 0x04000471 RID: 1137
		public static readonly string ussClassName = "unity-foldout";

		// Token: 0x04000472 RID: 1138
		public static readonly string toggleUssClassName = Foldout.ussClassName + "__toggle";

		// Token: 0x04000473 RID: 1139
		public static readonly string contentUssClassName = Foldout.ussClassName + "__content";

		// Token: 0x04000474 RID: 1140
		public static readonly string inputUssClassName = Foldout.ussClassName + "__input";

		// Token: 0x04000475 RID: 1141
		public static readonly string checkmarkUssClassName = Foldout.ussClassName + "__checkmark";

		// Token: 0x04000476 RID: 1142
		public static readonly string textUssClassName = Foldout.ussClassName + "__text";

		// Token: 0x04000477 RID: 1143
		internal static readonly string ussFoldoutDepthClassName = Foldout.ussClassName + "--depth-";

		// Token: 0x04000478 RID: 1144
		internal static readonly int ussFoldoutMaxDepth = 4;

		// Token: 0x02000139 RID: 313
		public new class UxmlFactory : UxmlFactory<Foldout, Foldout.UxmlTraits>
		{
			// Token: 0x06000A63 RID: 2659 RVA: 0x0002970D File Offset: 0x0002790D
			public UxmlFactory()
			{
			}
		}

		// Token: 0x0200013A RID: 314
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x06000A64 RID: 2660 RVA: 0x00029718 File Offset: 0x00027918
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Foldout foldout = ve as Foldout;
				bool flag = foldout != null;
				if (flag)
				{
					foldout.text = this.m_Text.GetValueFromBag(bag, cc);
					foldout.SetValueWithoutNotify(this.m_Value.GetValueFromBag(bag, cc));
				}
			}

			// Token: 0x06000A65 RID: 2661 RVA: 0x00029769 File Offset: 0x00027969
			public UxmlTraits()
			{
			}

			// Token: 0x04000479 RID: 1145
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			// Token: 0x0400047A RID: 1146
			private UxmlBoolAttributeDescription m_Value = new UxmlBoolAttributeDescription
			{
				name = "value",
				defaultValue = true
			};
		}
	}
}
