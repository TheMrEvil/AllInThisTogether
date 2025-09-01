using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x02000116 RID: 278
	internal class ReusableTreeViewItem : ReusableCollectionItem
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x000230D4 File Offset: 0x000212D4
		public override VisualElement rootElement
		{
			get
			{
				return this.m_Container ?? base.bindableElement;
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000900 RID: 2304 RVA: 0x000230E8 File Offset: 0x000212E8
		// (remove) Token: 0x06000901 RID: 2305 RVA: 0x00023120 File Offset: 0x00021320
		public event Action<PointerUpEvent> onPointerUp
		{
			[CompilerGenerated]
			add
			{
				Action<PointerUpEvent> action = this.onPointerUp;
				Action<PointerUpEvent> action2;
				do
				{
					action2 = action;
					Action<PointerUpEvent> value2 = (Action<PointerUpEvent>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<PointerUpEvent>>(ref this.onPointerUp, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<PointerUpEvent> action = this.onPointerUp;
				Action<PointerUpEvent> action2;
				do
				{
					action2 = action;
					Action<PointerUpEvent> value2 = (Action<PointerUpEvent>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<PointerUpEvent>>(ref this.onPointerUp, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000902 RID: 2306 RVA: 0x00023158 File Offset: 0x00021358
		// (remove) Token: 0x06000903 RID: 2307 RVA: 0x00023190 File Offset: 0x00021390
		public event Action<ChangeEvent<bool>> onToggleValueChanged
		{
			[CompilerGenerated]
			add
			{
				Action<ChangeEvent<bool>> action = this.onToggleValueChanged;
				Action<ChangeEvent<bool>> action2;
				do
				{
					action2 = action;
					Action<ChangeEvent<bool>> value2 = (Action<ChangeEvent<bool>>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ChangeEvent<bool>>>(ref this.onToggleValueChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ChangeEvent<bool>> action = this.onToggleValueChanged;
				Action<ChangeEvent<bool>> action2;
				do
				{
					action2 = action;
					Action<ChangeEvent<bool>> value2 = (Action<ChangeEvent<bool>>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ChangeEvent<bool>>>(ref this.onToggleValueChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x000231C5 File Offset: 0x000213C5
		internal float indentWidth
		{
			get
			{
				return this.m_IndentWidth;
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x000231CD File Offset: 0x000213CD
		public ReusableTreeViewItem()
		{
			this.m_PointerUpCallback = new EventCallback<PointerUpEvent>(this.OnPointerUp);
			this.m_ToggleValueChangedCallback = new EventCallback<ChangeEvent<bool>>(this.OnToggleValueChanged);
			this.m_ToggleGeometryChangedCallback = new EventCallback<GeometryChangedEvent>(this.OnToggleGeometryChanged);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00023210 File Offset: 0x00021410
		public override void Init(VisualElement item)
		{
			base.Init(item);
			VisualElement visualElement = new VisualElement
			{
				name = TreeView.itemUssClassName
			};
			visualElement.AddToClassList(TreeView.itemUssClassName);
			this.InitExpandHierarchy(visualElement, item);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00023250 File Offset: 0x00021450
		protected void InitExpandHierarchy(VisualElement root, VisualElement item)
		{
			this.m_Container = root;
			this.m_Container.style.flexDirection = FlexDirection.Row;
			this.m_IndentElement = new VisualElement
			{
				name = TreeView.itemIndentUssClassName,
				style = 
				{
					flexDirection = FlexDirection.Row
				}
			};
			this.m_Container.hierarchy.Add(this.m_IndentElement);
			this.m_Toggle = new Toggle
			{
				name = TreeView.itemToggleUssClassName
			};
			this.m_Toggle.userData = this;
			this.m_Toggle.AddToClassList(Foldout.toggleUssClassName);
			this.m_Toggle.AddToClassList(TreeView.itemToggleUssClassName);
			this.m_Toggle.visualInput.AddToClassList(Foldout.inputUssClassName);
			this.m_Checkmark = this.m_Toggle.visualInput.Q(null, Toggle.checkmarkUssClassName);
			this.m_Checkmark.AddToClassList(Foldout.checkmarkUssClassName);
			this.m_Container.hierarchy.Add(this.m_Toggle);
			this.m_BindableContainer = new VisualElement
			{
				name = TreeView.itemContentContainerUssClassName,
				style = 
				{
					flexGrow = 1f
				}
			};
			this.m_BindableContainer.AddToClassList(TreeView.itemContentContainerUssClassName);
			this.m_Container.Add(this.m_BindableContainer);
			this.m_BindableContainer.Add(item);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x000233C0 File Offset: 0x000215C0
		public override void PreAttachElement()
		{
			base.PreAttachElement();
			this.rootElement.AddToClassList(TreeView.itemUssClassName);
			VisualElement container = this.m_Container;
			if (container != null)
			{
				container.RegisterCallback<PointerUpEvent>(this.m_PointerUpCallback, TrickleDown.NoTrickleDown);
			}
			Toggle toggle = this.m_Toggle;
			if (toggle != null)
			{
				toggle.visualInput.Q(null, Toggle.checkmarkUssClassName).RegisterCallback<GeometryChangedEvent>(this.m_ToggleGeometryChangedCallback, TrickleDown.NoTrickleDown);
			}
			Toggle toggle2 = this.m_Toggle;
			if (toggle2 != null)
			{
				toggle2.RegisterValueChangedCallback(this.m_ToggleValueChangedCallback);
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00023440 File Offset: 0x00021640
		public override void DetachElement()
		{
			base.DetachElement();
			this.rootElement.RemoveFromClassList(TreeView.itemUssClassName);
			VisualElement container = this.m_Container;
			if (container != null)
			{
				container.UnregisterCallback<PointerUpEvent>(this.m_PointerUpCallback, TrickleDown.NoTrickleDown);
			}
			Toggle toggle = this.m_Toggle;
			if (toggle != null)
			{
				toggle.visualInput.Q(null, Toggle.checkmarkUssClassName).UnregisterCallback<GeometryChangedEvent>(this.m_ToggleGeometryChangedCallback, TrickleDown.NoTrickleDown);
			}
			Toggle toggle2 = this.m_Toggle;
			if (toggle2 != null)
			{
				toggle2.UnregisterValueChangedCallback(this.m_ToggleValueChangedCallback);
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x000234C0 File Offset: 0x000216C0
		public void Indent(int depth)
		{
			bool flag = this.m_IndentElement == null;
			if (!flag)
			{
				this.m_Depth = depth;
				this.UpdateIndentLayout();
			}
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x000234EB File Offset: 0x000216EB
		public void SetExpandedWithoutNotify(bool expanded)
		{
			Toggle toggle = this.m_Toggle;
			if (toggle != null)
			{
				toggle.SetValueWithoutNotify(expanded);
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00023504 File Offset: 0x00021704
		public void SetToggleVisibility(bool visible)
		{
			bool flag = this.m_Toggle != null;
			if (flag)
			{
				this.m_Toggle.visible = visible;
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0002352C File Offset: 0x0002172C
		private void OnToggleGeometryChanged(GeometryChangedEvent evt)
		{
			float num = this.m_Checkmark.resolvedStyle.width + this.m_Checkmark.resolvedStyle.marginLeft + this.m_Checkmark.resolvedStyle.marginRight;
			bool flag = Math.Abs(num - this.m_IndentWidth) < float.Epsilon;
			if (!flag)
			{
				this.m_IndentWidth = num;
				this.UpdateIndentLayout();
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00023598 File Offset: 0x00021798
		private void UpdateIndentLayout()
		{
			this.m_IndentElement.style.width = this.m_IndentWidth * (float)this.m_Depth;
			this.m_IndentElement.EnableInClassList(TreeView.itemIndentUssClassName, this.m_Depth > 0);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000235E4 File Offset: 0x000217E4
		private void OnPointerUp(PointerUpEvent evt)
		{
			Action<PointerUpEvent> action = this.onPointerUp;
			if (action != null)
			{
				action(evt);
			}
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000235FA File Offset: 0x000217FA
		private void OnToggleValueChanged(ChangeEvent<bool> evt)
		{
			Action<ChangeEvent<bool>> action = this.onToggleValueChanged;
			if (action != null)
			{
				action(evt);
			}
		}

		// Token: 0x040003B1 RID: 945
		private Toggle m_Toggle;

		// Token: 0x040003B2 RID: 946
		private VisualElement m_Container;

		// Token: 0x040003B3 RID: 947
		private VisualElement m_IndentElement;

		// Token: 0x040003B4 RID: 948
		private VisualElement m_BindableContainer;

		// Token: 0x040003B5 RID: 949
		private VisualElement m_Checkmark;

		// Token: 0x040003B6 RID: 950
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<PointerUpEvent> onPointerUp;

		// Token: 0x040003B7 RID: 951
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<ChangeEvent<bool>> onToggleValueChanged;

		// Token: 0x040003B8 RID: 952
		private int m_Depth;

		// Token: 0x040003B9 RID: 953
		private float m_IndentWidth;

		// Token: 0x040003BA RID: 954
		private EventCallback<PointerUpEvent> m_PointerUpCallback;

		// Token: 0x040003BB RID: 955
		private EventCallback<ChangeEvent<bool>> m_ToggleValueChangedCallback;

		// Token: 0x040003BC RID: 956
		private EventCallback<GeometryChangedEvent> m_ToggleGeometryChangedCallback;
	}
}
