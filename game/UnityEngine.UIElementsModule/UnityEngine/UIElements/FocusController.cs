using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000031 RID: 49
	public class FocusController
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00005F27 File Offset: 0x00004127
		public FocusController(IFocusRing focusRing)
		{
			this.focusRing = focusRing;
			this.imguiKeyboardControl = 0;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005F52 File Offset: 0x00004152
		private IFocusRing focusRing
		{
			[CompilerGenerated]
			get
			{
				return this.<focusRing>k__BackingField;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005F5C File Offset: 0x0000415C
		public Focusable focusedElement
		{
			get
			{
				Focusable retargetedFocusedElement = this.GetRetargetedFocusedElement(null);
				return this.IsLocalElement(retargetedFocusedElement) ? retargetedFocusedElement : null;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005F84 File Offset: 0x00004184
		internal bool IsFocused(Focusable f)
		{
			bool flag = !this.IsLocalElement(f);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				foreach (FocusController.FocusedElement focusedElement in this.m_FocusedElements)
				{
					bool flag2 = focusedElement.m_FocusedElement == f;
					if (flag2)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006000 File Offset: 0x00004200
		internal Focusable GetRetargetedFocusedElement(VisualElement retargetAgainst)
		{
			VisualElement visualElement = (retargetAgainst != null) ? retargetAgainst.hierarchy.parent : null;
			bool flag = visualElement == null;
			if (flag)
			{
				bool flag2 = this.m_FocusedElements.Count > 0;
				if (flag2)
				{
					return this.m_FocusedElements[this.m_FocusedElements.Count - 1].m_FocusedElement;
				}
			}
			else
			{
				while (!visualElement.isCompositeRoot && visualElement.hierarchy.parent != null)
				{
					visualElement = visualElement.hierarchy.parent;
				}
				foreach (FocusController.FocusedElement focusedElement in this.m_FocusedElements)
				{
					bool flag3 = focusedElement.m_SubTreeRoot == visualElement;
					if (flag3)
					{
						return focusedElement.m_FocusedElement;
					}
				}
			}
			return null;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006108 File Offset: 0x00004308
		internal Focusable GetLeafFocusedElement()
		{
			bool flag = this.m_FocusedElements.Count > 0;
			Focusable result;
			if (flag)
			{
				Focusable focusedElement = this.m_FocusedElements[0].m_FocusedElement;
				result = (this.IsLocalElement(focusedElement) ? focusedElement : null);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006150 File Offset: 0x00004350
		private bool IsLocalElement(Focusable f)
		{
			return ((f != null) ? f.focusController : null) == this;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006171 File Offset: 0x00004371
		internal void ClearPendingFocusEvents()
		{
			this.m_PendingFocusCount = 0;
			this.m_LastPendingFocusedElement = null;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006184 File Offset: 0x00004384
		internal bool IsPendingFocus(Focusable f)
		{
			for (VisualElement visualElement = this.m_LastPendingFocusedElement as VisualElement; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				bool flag = f == visualElement;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000061CC File Offset: 0x000043CC
		internal void SetFocusToLastFocusedElement()
		{
			bool flag = this.m_LastFocusedElement != null && !(this.m_LastFocusedElement is IMGUIContainer);
			if (flag)
			{
				this.m_LastFocusedElement.Focus();
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006208 File Offset: 0x00004408
		internal void BlurLastFocusedElement()
		{
			bool flag = this.m_LastFocusedElement != null && !(this.m_LastFocusedElement is IMGUIContainer);
			if (flag)
			{
				Focusable lastFocusedElement = this.m_LastFocusedElement;
				this.m_LastFocusedElement.Blur();
				this.m_LastFocusedElement = lastFocusedElement;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006254 File Offset: 0x00004454
		internal void DoFocusChange(Focusable f)
		{
			this.m_FocusedElements.Clear();
			for (VisualElement visualElement = f as VisualElement; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				bool flag = visualElement.hierarchy.parent == null || visualElement.isCompositeRoot;
				if (flag)
				{
					this.m_FocusedElements.Add(new FocusController.FocusedElement
					{
						m_SubTreeRoot = visualElement,
						m_FocusedElement = f
					});
					f = visualElement;
				}
			}
			this.m_PendingFocusCount--;
			bool flag2 = this.m_PendingFocusCount == 0;
			if (flag2)
			{
				this.m_LastPendingFocusedElement = null;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006300 File Offset: 0x00004500
		internal Focusable FocusNextInDirection(FocusChangeDirection direction)
		{
			Focusable nextFocusable = this.focusRing.GetNextFocusable(this.GetLeafFocusedElement(), direction);
			direction.ApplyTo(this, nextFocusable);
			return nextFocusable;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006330 File Offset: 0x00004530
		private void AboutToReleaseFocus(Focusable focusable, Focusable willGiveFocusTo, FocusChangeDirection direction, DispatchMode dispatchMode)
		{
			using (FocusOutEvent pooled = FocusEventBase<FocusOutEvent>.GetPooled(focusable, willGiveFocusTo, direction, this, false))
			{
				focusable.SendEvent(pooled, dispatchMode);
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006374 File Offset: 0x00004574
		private void ReleaseFocus(Focusable focusable, Focusable willGiveFocusTo, FocusChangeDirection direction, DispatchMode dispatchMode)
		{
			using (BlurEvent pooled = FocusEventBase<BlurEvent>.GetPooled(focusable, willGiveFocusTo, direction, this, false))
			{
				focusable.SendEvent(pooled, dispatchMode);
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000063B8 File Offset: 0x000045B8
		private void AboutToGrabFocus(Focusable focusable, Focusable willTakeFocusFrom, FocusChangeDirection direction, DispatchMode dispatchMode)
		{
			using (FocusInEvent pooled = FocusEventBase<FocusInEvent>.GetPooled(focusable, willTakeFocusFrom, direction, this, false))
			{
				focusable.SendEvent(pooled, dispatchMode);
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000063FC File Offset: 0x000045FC
		private void GrabFocus(Focusable focusable, Focusable willTakeFocusFrom, FocusChangeDirection direction, bool bIsFocusDelegated, DispatchMode dispatchMode)
		{
			using (FocusEvent pooled = FocusEventBase<FocusEvent>.GetPooled(focusable, willTakeFocusFrom, direction, this, bIsFocusDelegated))
			{
				focusable.SendEvent(pooled, dispatchMode);
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006440 File Offset: 0x00004640
		internal void Blur(Focusable focusable, bool bIsFocusDelegated = false, DispatchMode dispatchMode = DispatchMode.Default)
		{
			bool flag = (this.m_PendingFocusCount > 0) ? this.IsPendingFocus(focusable) : this.IsFocused(focusable);
			bool flag2 = flag;
			if (flag2)
			{
				this.SwitchFocus(null, bIsFocusDelegated, dispatchMode);
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006479 File Offset: 0x00004679
		internal void SwitchFocus(Focusable newFocusedElement, bool bIsFocusDelegated = false, DispatchMode dispatchMode = DispatchMode.Default)
		{
			this.SwitchFocus(newFocusedElement, FocusChangeDirection.unspecified, bIsFocusDelegated, dispatchMode);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000648C File Offset: 0x0000468C
		internal void SwitchFocus(Focusable newFocusedElement, FocusChangeDirection direction, bool bIsFocusDelegated = false, DispatchMode dispatchMode = DispatchMode.Default)
		{
			this.m_LastFocusedElement = newFocusedElement;
			Focusable focusable = (this.m_PendingFocusCount > 0) ? this.m_LastPendingFocusedElement : this.GetLeafFocusedElement();
			bool flag = focusable == newFocusedElement;
			if (!flag)
			{
				bool flag2 = newFocusedElement == null || !newFocusedElement.canGrabFocus;
				if (flag2)
				{
					bool flag3 = focusable != null;
					if (flag3)
					{
						this.m_LastPendingFocusedElement = null;
						this.m_PendingFocusCount++;
						this.AboutToReleaseFocus(focusable, null, direction, dispatchMode);
						this.ReleaseFocus(focusable, null, direction, dispatchMode);
					}
				}
				else
				{
					bool flag4 = newFocusedElement != focusable;
					if (flag4)
					{
						VisualElement visualElement = newFocusedElement as VisualElement;
						Focusable willGiveFocusTo = ((visualElement != null) ? visualElement.RetargetElement(focusable as VisualElement) : null) ?? newFocusedElement;
						VisualElement visualElement2 = focusable as VisualElement;
						Focusable willTakeFocusFrom = ((visualElement2 != null) ? visualElement2.RetargetElement(newFocusedElement as VisualElement) : null) ?? focusable;
						this.m_LastPendingFocusedElement = newFocusedElement;
						this.m_PendingFocusCount++;
						bool flag5 = focusable != null;
						if (flag5)
						{
							this.AboutToReleaseFocus(focusable, willGiveFocusTo, direction, dispatchMode);
						}
						this.AboutToGrabFocus(newFocusedElement, willTakeFocusFrom, direction, dispatchMode);
						bool flag6 = focusable != null;
						if (flag6)
						{
							this.ReleaseFocus(focusable, willGiveFocusTo, direction, dispatchMode);
						}
						this.GrabFocus(newFocusedElement, willTakeFocusFrom, direction, bIsFocusDelegated, dispatchMode);
					}
				}
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000065CC File Offset: 0x000047CC
		internal Focusable SwitchFocusOnEvent(EventBase e)
		{
			bool processedByFocusController = e.processedByFocusController;
			Focusable leafFocusedElement;
			if (processedByFocusController)
			{
				leafFocusedElement = this.GetLeafFocusedElement();
			}
			else
			{
				using (FocusChangeDirection focusChangeDirection = this.focusRing.GetFocusChangeDirection(this.GetLeafFocusedElement(), e))
				{
					bool flag = focusChangeDirection != FocusChangeDirection.none;
					if (flag)
					{
						Focusable result = this.FocusNextInDirection(focusChangeDirection);
						e.processedByFocusController = true;
						return result;
					}
				}
				leafFocusedElement = this.GetLeafFocusedElement();
			}
			return leafFocusedElement;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006650 File Offset: 0x00004850
		internal void ReevaluateFocus()
		{
			VisualElement visualElement = this.focusedElement as VisualElement;
			bool flag = visualElement != null;
			if (flag)
			{
				bool flag2 = !visualElement.isHierarchyDisplayed || !visualElement.visible;
				if (flag2)
				{
					visualElement.Blur();
				}
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006694 File Offset: 0x00004894
		internal bool GetFocusableParentForPointerEvent(Focusable target, out Focusable effectiveTarget)
		{
			bool flag = target == null || !target.focusable;
			bool result;
			if (flag)
			{
				effectiveTarget = target;
				result = (target != null);
			}
			else
			{
				effectiveTarget = target;
				for (;;)
				{
					VisualElement visualElement = effectiveTarget as VisualElement;
					bool flag2 = visualElement != null && (!visualElement.enabledInHierarchy || !visualElement.focusable) && visualElement.hierarchy.parent != null;
					if (!flag2)
					{
						break;
					}
					effectiveTarget = visualElement.hierarchy.parent;
				}
				result = !this.IsFocused(effectiveTarget);
			}
			return result;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000671B File Offset: 0x0000491B
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00006723 File Offset: 0x00004923
		internal int imguiKeyboardControl
		{
			[CompilerGenerated]
			get
			{
				return this.<imguiKeyboardControl>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<imguiKeyboardControl>k__BackingField = value;
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000672C File Offset: 0x0000492C
		internal void SyncIMGUIFocus(int imguiKeyboardControlID, Focusable imguiContainerHavingKeyboardControl, bool forceSwitch)
		{
			this.imguiKeyboardControl = imguiKeyboardControlID;
			bool flag = forceSwitch || this.imguiKeyboardControl != 0;
			if (flag)
			{
				this.SwitchFocus(imguiContainerHavingKeyboardControl, FocusChangeDirection.unspecified, false, DispatchMode.Default);
			}
			else
			{
				this.SwitchFocus(null, FocusChangeDirection.unspecified, false, DispatchMode.Default);
			}
		}

		// Token: 0x04000088 RID: 136
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly IFocusRing <focusRing>k__BackingField;

		// Token: 0x04000089 RID: 137
		private List<FocusController.FocusedElement> m_FocusedElements = new List<FocusController.FocusedElement>();

		// Token: 0x0400008A RID: 138
		private Focusable m_LastFocusedElement;

		// Token: 0x0400008B RID: 139
		private Focusable m_LastPendingFocusedElement;

		// Token: 0x0400008C RID: 140
		private int m_PendingFocusCount = 0;

		// Token: 0x0400008D RID: 141
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <imguiKeyboardControl>k__BackingField;

		// Token: 0x02000032 RID: 50
		private struct FocusedElement
		{
			// Token: 0x0400008E RID: 142
			public VisualElement m_SubTreeRoot;

			// Token: 0x0400008F RID: 143
			public Focusable m_FocusedElement;
		}
	}
}
