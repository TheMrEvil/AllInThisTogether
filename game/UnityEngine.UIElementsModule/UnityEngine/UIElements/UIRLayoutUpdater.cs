using System;
using Unity.Profiling;
using UnityEngine.Yoga;

namespace UnityEngine.UIElements
{
	// Token: 0x0200025B RID: 603
	internal class UIRLayoutUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x00048C2A File Offset: 0x00046E2A
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return UIRLayoutUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00048C34 File Offset: 0x00046E34
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & (VersionChangeType.Hierarchy | VersionChangeType.Layout)) == (VersionChangeType)0;
			if (!flag)
			{
				YogaNode yogaNode = ve.yogaNode;
				bool flag2 = yogaNode != null && yogaNode.IsMeasureDefined;
				if (flag2)
				{
					yogaNode.MarkDirty();
				}
			}
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00048C70 File Offset: 0x00046E70
		public override void Update()
		{
			int num = 0;
			while (base.visualTree.yogaNode.IsDirty)
			{
				bool flag = num > 0;
				if (flag)
				{
					base.panel.ApplyStyles();
				}
				base.panel.duringLayoutPhase = true;
				base.visualTree.yogaNode.CalculateLayout(float.NaN, float.NaN);
				base.panel.duringLayoutPhase = false;
				using (new EventDispatcherGate(base.visualTree.panel.dispatcher))
				{
					this.UpdateSubTree(base.visualTree, num, true);
				}
				bool flag2 = num++ >= 10;
				if (flag2)
				{
					string str = "Layout update is struggling to process current layout (consider simplifying to avoid recursive layout): ";
					VisualElement visualTree = base.visualTree;
					Debug.LogError(str + ((visualTree != null) ? visualTree.ToString() : null));
					break;
				}
			}
			base.visualTree.focusController.ReevaluateFocus();
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00048D78 File Offset: 0x00046F78
		private void UpdateSubTree(VisualElement ve, int currentLayoutPass, bool isDisplayed = true)
		{
			Rect rect = new Rect(ve.yogaNode.LayoutX, ve.yogaNode.LayoutY, ve.yogaNode.LayoutWidth, ve.yogaNode.LayoutHeight);
			Rect lastPseudoPadding = new Rect(ve.yogaNode.LayoutPaddingLeft, ve.yogaNode.LayoutPaddingTop, ve.yogaNode.LayoutWidth - (ve.yogaNode.LayoutPaddingLeft + ve.yogaNode.LayoutPaddingRight), ve.yogaNode.LayoutHeight - (ve.yogaNode.LayoutPaddingTop + ve.yogaNode.LayoutPaddingBottom));
			Rect lastLayout = ve.lastLayout;
			Rect lastPseudoPadding2 = ve.lastPseudoPadding;
			bool isHierarchyDisplayed = ve.isHierarchyDisplayed;
			VersionChangeType versionChangeType = (VersionChangeType)0;
			bool flag = lastLayout.size != rect.size;
			bool flag2 = lastPseudoPadding2.size != lastPseudoPadding.size;
			bool flag3 = flag || flag2;
			if (flag3)
			{
				versionChangeType |= (VersionChangeType.Size | VersionChangeType.Repaint);
			}
			bool flag4 = rect.position != lastLayout.position;
			bool flag5 = lastPseudoPadding.position != lastPseudoPadding2.position;
			bool flag6 = flag4 || flag5;
			if (flag6)
			{
				versionChangeType |= VersionChangeType.Transform;
			}
			bool flag7 = (versionChangeType & VersionChangeType.Size) != (VersionChangeType)0 && (versionChangeType & VersionChangeType.Transform) == (VersionChangeType)0;
			if (flag7)
			{
				bool flag8 = !ve.hasDefaultRotationAndScale;
				if (flag8)
				{
					bool flag9 = !Mathf.Approximately(ve.resolvedStyle.transformOrigin.x, 0f) || !Mathf.Approximately(ve.resolvedStyle.transformOrigin.y, 0f);
					if (flag9)
					{
						versionChangeType |= VersionChangeType.Transform;
					}
				}
			}
			isDisplayed &= (ve.resolvedStyle.display != DisplayStyle.None);
			ve.isHierarchyDisplayed = isDisplayed;
			bool flag10 = versionChangeType > (VersionChangeType)0;
			if (flag10)
			{
				ve.IncrementVersion(versionChangeType);
			}
			ve.lastLayout = rect;
			ve.lastPseudoPadding = lastPseudoPadding;
			bool hasNewLayout = ve.yogaNode.HasNewLayout;
			bool flag11 = hasNewLayout;
			if (flag11)
			{
				int childCount = ve.hierarchy.childCount;
				for (int i = 0; i < childCount; i++)
				{
					this.UpdateSubTree(ve.hierarchy[i], currentLayoutPass, isDisplayed);
				}
			}
			bool flag12 = flag || flag4;
			if (flag12)
			{
				using (GeometryChangedEvent pooled = GeometryChangedEvent.GetPooled(lastLayout, rect))
				{
					pooled.layoutPass = currentLayoutPass;
					pooled.target = ve;
					ve.SendEvent(pooled);
				}
			}
			bool flag13 = hasNewLayout;
			if (flag13)
			{
				ve.yogaNode.MarkLayoutSeen();
			}
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00049034 File Offset: 0x00047234
		public UIRLayoutUpdater()
		{
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0004903D File Offset: 0x0004723D
		// Note: this type is marked as 'beforefieldinit'.
		static UIRLayoutUpdater()
		{
		}

		// Token: 0x04000851 RID: 2129
		private const int kMaxValidateLayoutCount = 10;

		// Token: 0x04000852 RID: 2130
		private static readonly string s_Description = "Update Layout";

		// Token: 0x04000853 RID: 2131
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(UIRLayoutUpdater.s_Description);
	}
}
