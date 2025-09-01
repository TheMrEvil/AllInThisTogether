using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Unity.Profiling;
using UnityEngine.Assertions;
using UnityEngine.UIElements.Experimental;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.UIElements.UIR;
using UnityEngine.Yoga;

namespace UnityEngine.UIElements
{
	// Token: 0x02000081 RID: 129
	public class VisualElement : Focusable, IStylePropertyAnimations, ITransform, ITransitionAnimations, IExperimentalFeatures, IVisualElementScheduler, IResolvedStyle
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000BA8A File Offset: 0x00009C8A
		internal bool hasRunningAnimations
		{
			get
			{
				return this.styleAnimation.runningAnimationCount > 0;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000BA9A File Offset: 0x00009C9A
		internal bool hasCompletedAnimations
		{
			get
			{
				return this.styleAnimation.completedAnimationCount > 0;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000BAAA File Offset: 0x00009CAA
		// (set) Token: 0x06000343 RID: 835 RVA: 0x0000BAB2 File Offset: 0x00009CB2
		int IStylePropertyAnimations.runningAnimationCount
		{
			[CompilerGenerated]
			get
			{
				return this.<UnityEngine.UIElements.IStylePropertyAnimations.runningAnimationCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UnityEngine.UIElements.IStylePropertyAnimations.runningAnimationCount>k__BackingField = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000BABB File Offset: 0x00009CBB
		// (set) Token: 0x06000345 RID: 837 RVA: 0x0000BAC3 File Offset: 0x00009CC3
		int IStylePropertyAnimations.completedAnimationCount
		{
			[CompilerGenerated]
			get
			{
				return this.<UnityEngine.UIElements.IStylePropertyAnimations.completedAnimationCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UnityEngine.UIElements.IStylePropertyAnimations.completedAnimationCount>k__BackingField = value;
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000BACC File Offset: 0x00009CCC
		private IStylePropertyAnimationSystem GetStylePropertyAnimationSystem()
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			return (elementPanel != null) ? elementPanel.styleAnimationSystem : null;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		internal IStylePropertyAnimations styleAnimation
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000BAF4 File Offset: 0x00009CF4
		bool IStylePropertyAnimations.Start(StylePropertyId id, float from, float to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000BB1C File Offset: 0x00009D1C
		bool IStylePropertyAnimations.Start(StylePropertyId id, int from, int to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000BB44 File Offset: 0x00009D44
		bool IStylePropertyAnimations.Start(StylePropertyId id, Length from, Length to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000BB6C File Offset: 0x00009D6C
		bool IStylePropertyAnimations.Start(StylePropertyId id, Color from, Color to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000BB94 File Offset: 0x00009D94
		bool IStylePropertyAnimations.StartEnum(StylePropertyId id, int from, int to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000BBBC File Offset: 0x00009DBC
		bool IStylePropertyAnimations.Start(StylePropertyId id, Background from, Background to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000BBE4 File Offset: 0x00009DE4
		bool IStylePropertyAnimations.Start(StylePropertyId id, FontDefinition from, FontDefinition to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000BC0C File Offset: 0x00009E0C
		bool IStylePropertyAnimations.Start(StylePropertyId id, Font from, Font to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000BC34 File Offset: 0x00009E34
		bool IStylePropertyAnimations.Start(StylePropertyId id, TextShadow from, TextShadow to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000BC5C File Offset: 0x00009E5C
		bool IStylePropertyAnimations.Start(StylePropertyId id, Scale from, Scale to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000BC84 File Offset: 0x00009E84
		bool IStylePropertyAnimations.Start(StylePropertyId id, Translate from, Translate to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000BCAC File Offset: 0x00009EAC
		bool IStylePropertyAnimations.Start(StylePropertyId id, Rotate from, Rotate to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000BCD4 File Offset: 0x00009ED4
		bool IStylePropertyAnimations.Start(StylePropertyId id, TransformOrigin from, TransformOrigin to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000BCFB File Offset: 0x00009EFB
		void IStylePropertyAnimations.CancelAnimation(StylePropertyId id)
		{
			IStylePropertyAnimationSystem stylePropertyAnimationSystem = this.GetStylePropertyAnimationSystem();
			if (stylePropertyAnimationSystem != null)
			{
				stylePropertyAnimationSystem.CancelAnimation(this, id);
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000BD14 File Offset: 0x00009F14
		void IStylePropertyAnimations.CancelAllAnimations()
		{
			bool flag = this.hasRunningAnimations || this.hasCompletedAnimations;
			if (flag)
			{
				IStylePropertyAnimationSystem stylePropertyAnimationSystem = this.GetStylePropertyAnimationSystem();
				if (stylePropertyAnimationSystem != null)
				{
					stylePropertyAnimationSystem.CancelAllAnimations(this);
				}
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000BD4C File Offset: 0x00009F4C
		bool IStylePropertyAnimations.HasRunningAnimation(StylePropertyId id)
		{
			return this.hasRunningAnimations && this.GetStylePropertyAnimationSystem().HasRunningAnimation(this, id);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000BD76 File Offset: 0x00009F76
		void IStylePropertyAnimations.UpdateAnimation(StylePropertyId id)
		{
			this.GetStylePropertyAnimationSystem().UpdateAnimation(this, id);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000BD88 File Offset: 0x00009F88
		void IStylePropertyAnimations.GetAllAnimations(List<StylePropertyId> outPropertyIds)
		{
			bool flag = this.hasRunningAnimations || this.hasCompletedAnimations;
			if (flag)
			{
				this.GetStylePropertyAnimationSystem().GetAllAnimations(this, outPropertyIds);
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000BDBC File Offset: 0x00009FBC
		internal bool TryConvertLengthUnits(StylePropertyId id, ref Length from, ref Length to, int subPropertyIndex = 0)
		{
			bool flag = from.IsAuto() || from.IsNone() || to.IsAuto() || to.IsNone();
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = float.IsNaN(from.value) || float.IsNaN(to.value);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = from.unit == to.unit;
					if (flag3)
					{
						result = true;
					}
					else
					{
						bool flag4 = to.unit == LengthUnit.Pixel;
						if (flag4)
						{
							bool flag5 = Mathf.Approximately(from.value, 0f);
							if (flag5)
							{
								from = new Length(0f, LengthUnit.Pixel);
								return true;
							}
							float? parentSizeForLengthConversion = this.GetParentSizeForLengthConversion(id, subPropertyIndex);
							bool flag6 = parentSizeForLengthConversion == null || parentSizeForLengthConversion.Value < 0f;
							if (flag6)
							{
								return false;
							}
							from = new Length(from.value * parentSizeForLengthConversion.Value / 100f, LengthUnit.Pixel);
						}
						else
						{
							Assert.AreEqual<LengthUnit>(LengthUnit.Percent, to.unit);
							float? parentSizeForLengthConversion2 = this.GetParentSizeForLengthConversion(id, subPropertyIndex);
							bool flag7 = parentSizeForLengthConversion2 == null || parentSizeForLengthConversion2.Value <= 0f;
							if (flag7)
							{
								return false;
							}
							from = new Length(from.value * 100f / parentSizeForLengthConversion2.Value, LengthUnit.Percent);
						}
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000BF34 File Offset: 0x0000A134
		internal bool TryConvertTransformOriginUnits(ref TransformOrigin from, ref TransformOrigin to)
		{
			Length x = from.x;
			Length y = from.y;
			Length x2 = to.x;
			Length y2 = to.y;
			bool flag = !this.TryConvertLengthUnits(StylePropertyId.TransformOrigin, ref x, ref x2, 0);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !this.TryConvertLengthUnits(StylePropertyId.TransformOrigin, ref y, ref y2, 1);
				if (flag2)
				{
					result = false;
				}
				else
				{
					from.x = x;
					from.y = y;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		internal bool TryConvertTranslateUnits(ref Translate from, ref Translate to)
		{
			Length x = from.x;
			Length y = from.y;
			Length x2 = to.x;
			Length y2 = to.y;
			bool flag = !this.TryConvertLengthUnits(StylePropertyId.Translate, ref x, ref x2, 0);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !this.TryConvertLengthUnits(StylePropertyId.Translate, ref y, ref y2, 1);
				if (flag2)
				{
					result = false;
				}
				else
				{
					from.x = x;
					from.y = y;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000C034 File Offset: 0x0000A234
		private float? GetParentSizeForLengthConversion(StylePropertyId id, int subPropertyIndex = 0)
		{
			if (id <= StylePropertyId.WordSpacing)
			{
				if (id - StylePropertyId.FontSize <= 1 || id == StylePropertyId.UnityParagraphSpacing || id == StylePropertyId.WordSpacing)
				{
					return null;
				}
			}
			else if (id <= StylePropertyId.Translate)
			{
				switch (id)
				{
				case StylePropertyId.Bottom:
				case StylePropertyId.Height:
				case StylePropertyId.MaxHeight:
				case StylePropertyId.MinHeight:
				case StylePropertyId.Top:
				{
					VisualElement parent = this.hierarchy.parent;
					return (parent != null) ? new float?(parent.resolvedStyle.height) : null;
				}
				case StylePropertyId.Display:
				case StylePropertyId.FlexDirection:
				case StylePropertyId.FlexGrow:
				case StylePropertyId.FlexShrink:
				case StylePropertyId.FlexWrap:
				case StylePropertyId.JustifyContent:
				case StylePropertyId.Position:
					break;
				case StylePropertyId.FlexBasis:
				{
					bool flag = this.hierarchy.parent == null;
					if (flag)
					{
						return null;
					}
					FlexDirection flexDirection = this.hierarchy.parent.resolvedStyle.flexDirection;
					FlexDirection flexDirection2 = flexDirection;
					if (flexDirection2 > FlexDirection.ColumnReverse)
					{
						return new float?(this.hierarchy.parent.resolvedStyle.width);
					}
					return new float?(this.hierarchy.parent.resolvedStyle.height);
				}
				case StylePropertyId.Left:
				case StylePropertyId.MarginBottom:
				case StylePropertyId.MarginLeft:
				case StylePropertyId.MarginRight:
				case StylePropertyId.MarginTop:
				case StylePropertyId.MaxWidth:
				case StylePropertyId.MinWidth:
				case StylePropertyId.PaddingBottom:
				case StylePropertyId.PaddingLeft:
				case StylePropertyId.PaddingRight:
				case StylePropertyId.PaddingTop:
				case StylePropertyId.Right:
				case StylePropertyId.Width:
				{
					VisualElement parent2 = this.hierarchy.parent;
					return (parent2 != null) ? new float?(parent2.resolvedStyle.width) : null;
				}
				default:
					if (id - StylePropertyId.TransformOrigin <= 1)
					{
						return new float?((subPropertyIndex == 0) ? this.resolvedStyle.width : this.resolvedStyle.height);
					}
					break;
				}
			}
			else if (id - StylePropertyId.BorderBottomLeftRadius <= 1 || id - StylePropertyId.BorderTopLeftRadius <= 1)
			{
				return new float?(this.resolvedStyle.width);
			}
			return null;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000C276 File Offset: 0x0000A476
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0000C285 File Offset: 0x0000A485
		internal bool isCompositeRoot
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.CompositeRoot) == VisualElementFlags.CompositeRoot;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.CompositeRoot) : (this.m_Flags & ~VisualElementFlags.CompositeRoot));
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000C2A4 File Offset: 0x0000A4A4
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000C2B9 File Offset: 0x0000A4B9
		internal bool isHierarchyDisplayed
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.HierarchyDisplayed) == VisualElementFlags.HierarchyDisplayed;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.HierarchyDisplayed) : (this.m_Flags & ~VisualElementFlags.HierarchyDisplayed));
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000C2F8 File Offset: 0x0000A4F8
		public string viewDataKey
		{
			get
			{
				return this.m_ViewDataKey;
			}
			set
			{
				bool flag = this.m_ViewDataKey != value;
				if (flag)
				{
					this.m_ViewDataKey = value;
					bool flag2 = !string.IsNullOrEmpty(value);
					if (flag2)
					{
						this.IncrementVersion(VersionChangeType.ViewData);
					}
				}
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000C334 File Offset: 0x0000A534
		// (set) Token: 0x06000365 RID: 869 RVA: 0x0000C349 File Offset: 0x0000A549
		internal bool enableViewDataPersistence
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.EnableViewDataPersistence) == VisualElementFlags.EnableViewDataPersistence;
			}
			private set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.EnableViewDataPersistence) : (this.m_Flags & ~VisualElementFlags.EnableViewDataPersistence));
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000C370 File Offset: 0x0000A570
		// (set) Token: 0x06000367 RID: 871 RVA: 0x0000C391 File Offset: 0x0000A591
		public object userData
		{
			get
			{
				object result;
				this.TryGetPropertyInternal(VisualElement.userDataPropertyKey, out result);
				return result;
			}
			set
			{
				this.SetPropertyInternal(VisualElement.userDataPropertyKey, value);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000C3A4 File Offset: 0x0000A5A4
		public override bool canGrabFocus
		{
			get
			{
				bool flag = false;
				for (VisualElement parent = this.hierarchy.parent; parent != null; parent = parent.parent)
				{
					bool isCompositeRoot = parent.isCompositeRoot;
					if (isCompositeRoot)
					{
						flag |= !parent.canGrabFocus;
						break;
					}
				}
				return !flag && this.visible && this.resolvedStyle.display != DisplayStyle.None && this.enabledInHierarchy && base.canGrabFocus;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000C424 File Offset: 0x0000A624
		public override FocusController focusController
		{
			get
			{
				IPanel panel = this.panel;
				return (panel != null) ? panel.focusController : null;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000C448 File Offset: 0x0000A648
		// (set) Token: 0x0600036B RID: 875 RVA: 0x0000C498 File Offset: 0x0000A698
		public UsageHints usageHints
		{
			get
			{
				return (((this.renderHints & RenderHints.GroupTransform) != RenderHints.None) ? UsageHints.GroupTransform : UsageHints.None) | (((this.renderHints & RenderHints.BoneTransform) != RenderHints.None) ? UsageHints.DynamicTransform : UsageHints.None) | (((this.renderHints & RenderHints.MaskContainer) != RenderHints.None) ? UsageHints.MaskContainer : UsageHints.None) | (((this.renderHints & RenderHints.DynamicColor) != RenderHints.None) ? UsageHints.DynamicColor : UsageHints.None);
			}
			set
			{
				bool flag = (value & UsageHints.GroupTransform) > UsageHints.None;
				if (flag)
				{
					this.renderHints |= RenderHints.GroupTransform;
				}
				else
				{
					this.renderHints &= ~RenderHints.GroupTransform;
				}
				bool flag2 = (value & UsageHints.DynamicTransform) > UsageHints.None;
				if (flag2)
				{
					this.renderHints |= RenderHints.BoneTransform;
				}
				else
				{
					this.renderHints &= ~RenderHints.BoneTransform;
				}
				bool flag3 = (value & UsageHints.MaskContainer) > UsageHints.None;
				if (flag3)
				{
					this.renderHints |= RenderHints.MaskContainer;
				}
				else
				{
					this.renderHints &= ~RenderHints.MaskContainer;
				}
				bool flag4 = (value & UsageHints.DynamicColor) > UsageHints.None;
				if (flag4)
				{
					this.renderHints |= RenderHints.DynamicColor;
				}
				else
				{
					this.renderHints &= ~RenderHints.DynamicColor;
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000C554 File Offset: 0x0000A754
		// (set) Token: 0x0600036D RID: 877 RVA: 0x0000C56C File Offset: 0x0000A76C
		internal RenderHints renderHints
		{
			get
			{
				return this.m_RenderHints;
			}
			set
			{
				RenderHints renderHints = this.m_RenderHints & ~(RenderHints.DirtyGroupTransform | RenderHints.DirtyBoneTransform | RenderHints.DirtyClipWithScissors | RenderHints.DirtyMaskContainer | RenderHints.DirtyDynamicColor);
				RenderHints renderHints2 = value & ~(RenderHints.DirtyGroupTransform | RenderHints.DirtyBoneTransform | RenderHints.DirtyClipWithScissors | RenderHints.DirtyMaskContainer | RenderHints.DirtyDynamicColor);
				RenderHints renderHints3 = renderHints ^ renderHints2;
				bool flag = renderHints3 > RenderHints.None;
				if (flag)
				{
					RenderHints renderHints4 = this.m_RenderHints & RenderHints.DirtyAll;
					RenderHints renderHints5 = renderHints3 << 5;
					this.m_RenderHints = (renderHints2 | renderHints4 | renderHints5);
					this.IncrementVersion(VersionChangeType.RenderHints);
				}
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000C5C9 File Offset: 0x0000A7C9
		internal void MarkRenderHintsClean()
		{
			this.m_RenderHints &= ~(RenderHints.DirtyGroupTransform | RenderHints.DirtyBoneTransform | RenderHints.DirtyClipWithScissors | RenderHints.DirtyMaskContainer | RenderHints.DirtyDynamicColor);
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
		public ITransform transform
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000C5F4 File Offset: 0x0000A7F4
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0000C611 File Offset: 0x0000A811
		Vector3 ITransform.position
		{
			get
			{
				return this.resolvedStyle.translate;
			}
			set
			{
				this.style.translate = new Translate(value.x, value.y, value.z);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000C648 File Offset: 0x0000A848
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000C670 File Offset: 0x0000A870
		Quaternion ITransform.rotation
		{
			get
			{
				return this.resolvedStyle.rotate.ToQuaternion();
			}
			set
			{
				float value2;
				Vector3 axis;
				value.ToAngleAxis(out value2, out axis);
				this.style.rotate = new Rotate(value2, axis);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000C6CD File Offset: 0x0000A8CD
		Vector3 ITransform.scale
		{
			get
			{
				return this.resolvedStyle.scale.value;
			}
			set
			{
				this.style.scale = new Scale(value);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000C6F4 File Offset: 0x0000A8F4
		Matrix4x4 ITransform.matrix
		{
			get
			{
				return Matrix4x4.TRS(this.resolvedStyle.translate, this.resolvedStyle.rotate.ToQuaternion(), this.resolvedStyle.scale.value);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000C73C File Offset: 0x0000A93C
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000C74B File Offset: 0x0000A94B
		internal bool isLayoutManual
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.LayoutManual) == VisualElementFlags.LayoutManual;
			}
			private set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.LayoutManual) : (this.m_Flags & ~VisualElementFlags.LayoutManual));
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000C76A File Offset: 0x0000A96A
		internal float scaledPixelsPerPoint
		{
			get
			{
				BaseVisualElementPanel elementPanel = this.elementPanel;
				return (elementPanel != null) ? elementPanel.scaledPixelsPerPoint : GUIUtility.pixelsPerPoint;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000C784 File Offset: 0x0000A984
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0000C804 File Offset: 0x0000AA04
		public Rect layout
		{
			get
			{
				Rect layout = this.m_Layout;
				bool flag = this.yogaNode != null && !this.isLayoutManual;
				if (flag)
				{
					layout.x = this.yogaNode.LayoutX;
					layout.y = this.yogaNode.LayoutY;
					layout.width = this.yogaNode.LayoutWidth;
					layout.height = this.yogaNode.LayoutHeight;
				}
				return layout;
			}
			internal set
			{
				bool flag = this.yogaNode == null;
				if (flag)
				{
					this.yogaNode = new YogaNode(null);
				}
				bool flag2 = this.isLayoutManual && this.m_Layout == value;
				if (!flag2)
				{
					Rect layout = this.layout;
					VersionChangeType versionChangeType = (VersionChangeType)0;
					bool flag3 = !Mathf.Approximately(layout.x, value.x) || !Mathf.Approximately(layout.y, value.y);
					if (flag3)
					{
						versionChangeType |= VersionChangeType.Transform;
					}
					bool flag4 = !Mathf.Approximately(layout.width, value.width) || !Mathf.Approximately(layout.height, value.height);
					if (flag4)
					{
						versionChangeType |= VersionChangeType.Size;
					}
					this.m_Layout = value;
					this.isLayoutManual = true;
					IStyle style = this.style;
					style.position = Position.Absolute;
					style.marginLeft = 0f;
					style.marginRight = 0f;
					style.marginBottom = 0f;
					style.marginTop = 0f;
					style.left = value.x;
					style.top = value.y;
					style.right = float.NaN;
					style.bottom = float.NaN;
					style.width = value.width;
					style.height = value.height;
					bool flag5 = versionChangeType > (VersionChangeType)0;
					if (flag5)
					{
						this.IncrementVersion(versionChangeType);
					}
				}
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
		internal void ClearManualLayout()
		{
			this.isLayoutManual = false;
			IStyle style = this.style;
			style.position = StyleKeyword.Null;
			style.marginLeft = StyleKeyword.Null;
			style.marginRight = StyleKeyword.Null;
			style.marginBottom = StyleKeyword.Null;
			style.marginTop = StyleKeyword.Null;
			style.left = StyleKeyword.Null;
			style.top = StyleKeyword.Null;
			style.right = StyleKeyword.Null;
			style.bottom = StyleKeyword.Null;
			style.width = StyleKeyword.Null;
			style.height = StyleKeyword.Null;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000CA64 File Offset: 0x0000AC64
		public Rect contentRect
		{
			get
			{
				Spacing a = new Spacing(this.resolvedStyle.paddingLeft, this.resolvedStyle.paddingTop, this.resolvedStyle.paddingRight, this.resolvedStyle.paddingBottom);
				return this.paddingRect - a;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000CAB8 File Offset: 0x0000ACB8
		protected Rect paddingRect
		{
			get
			{
				Spacing a = new Spacing(this.resolvedStyle.borderLeftWidth, this.resolvedStyle.borderTopWidth, this.resolvedStyle.borderRightWidth, this.resolvedStyle.borderBottomWidth);
				return this.rect - a;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000CB09 File Offset: 0x0000AD09
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000CB16 File Offset: 0x0000AD16
		internal bool isBoundingBoxDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.BoundingBoxDirty) == VisualElementFlags.BoundingBoxDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.BoundingBoxDirty) : (this.m_Flags & ~VisualElementFlags.BoundingBoxDirty));
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000CB34 File Offset: 0x0000AD34
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000CB43 File Offset: 0x0000AD43
		internal bool isWorldBoundingBoxDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldBoundingBoxDirty) == VisualElementFlags.WorldBoundingBoxDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldBoundingBoxDirty) : (this.m_Flags & ~VisualElementFlags.WorldBoundingBoxDirty));
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000CB64 File Offset: 0x0000AD64
		internal Rect boundingBox
		{
			get
			{
				bool isBoundingBoxDirty = this.isBoundingBoxDirty;
				if (isBoundingBoxDirty)
				{
					this.UpdateBoundingBox();
					this.isBoundingBoxDirty = false;
				}
				return this.m_BoundingBox;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000CB98 File Offset: 0x0000AD98
		internal Rect worldBoundingBox
		{
			get
			{
				bool flag = this.isWorldBoundingBoxDirty || this.isBoundingBoxDirty;
				if (flag)
				{
					this.UpdateWorldBoundingBox();
					this.isWorldBoundingBoxDirty = false;
				}
				return this.m_WorldBoundingBox;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000CBD8 File Offset: 0x0000ADD8
		private Rect boundingBoxInParentSpace
		{
			get
			{
				Rect boundingBox = this.boundingBox;
				this.TransformAlignedRectToParentSpace(ref boundingBox);
				return boundingBox;
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000CBFC File Offset: 0x0000ADFC
		internal void UpdateBoundingBox()
		{
			bool flag = float.IsNaN(this.rect.x) || float.IsNaN(this.rect.y) || float.IsNaN(this.rect.width) || float.IsNaN(this.rect.height);
			if (flag)
			{
				this.m_BoundingBox = Rect.zero;
			}
			else
			{
				this.m_BoundingBox = this.rect;
				bool flag2 = !this.ShouldClip();
				if (flag2)
				{
					int count = this.m_Children.Count;
					for (int i = 0; i < count; i++)
					{
						Rect boundingBoxInParentSpace = this.m_Children[i].boundingBoxInParentSpace;
						this.m_BoundingBox.xMin = Math.Min(this.m_BoundingBox.xMin, boundingBoxInParentSpace.xMin);
						this.m_BoundingBox.xMax = Math.Max(this.m_BoundingBox.xMax, boundingBoxInParentSpace.xMax);
						this.m_BoundingBox.yMin = Math.Min(this.m_BoundingBox.yMin, boundingBoxInParentSpace.yMin);
						this.m_BoundingBox.yMax = Math.Max(this.m_BoundingBox.yMax, boundingBoxInParentSpace.yMax);
					}
				}
			}
			this.isWorldBoundingBoxDirty = true;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000CD67 File Offset: 0x0000AF67
		internal void UpdateWorldBoundingBox()
		{
			this.m_WorldBoundingBox = this.boundingBox;
			VisualElement.TransformAlignedRect(this.worldTransformRef, ref this.m_WorldBoundingBox);
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000CD88 File Offset: 0x0000AF88
		public Rect worldBound
		{
			get
			{
				Rect rect = this.rect;
				VisualElement.TransformAlignedRect(this.worldTransformRef, ref rect);
				return rect;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000CDB0 File Offset: 0x0000AFB0
		public Rect localBound
		{
			get
			{
				Rect rect = this.rect;
				this.TransformAlignedRectToParentSpace(ref rect);
				return rect;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000CDD4 File Offset: 0x0000AFD4
		internal Rect rect
		{
			get
			{
				Rect layout = this.layout;
				return new Rect(0f, 0f, layout.width, layout.height);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000CE0A File Offset: 0x0000B00A
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000CE17 File Offset: 0x0000B017
		internal bool isWorldTransformDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldTransformDirty) == VisualElementFlags.WorldTransformDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldTransformDirty) : (this.m_Flags & ~VisualElementFlags.WorldTransformDirty));
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000CE35 File Offset: 0x0000B035
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000CE42 File Offset: 0x0000B042
		internal bool isWorldTransformInverseDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldTransformInverseDirty) == VisualElementFlags.WorldTransformInverseDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldTransformInverseDirty) : (this.m_Flags & ~VisualElementFlags.WorldTransformInverseDirty));
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000CE60 File Offset: 0x0000B060
		public Matrix4x4 worldTransform
		{
			get
			{
				bool isWorldTransformDirty = this.isWorldTransformDirty;
				if (isWorldTransformDirty)
				{
					this.UpdateWorldTransform();
				}
				return this.m_WorldTransformCache;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000CE8C File Offset: 0x0000B08C
		internal ref Matrix4x4 worldTransformRef
		{
			get
			{
				bool isWorldTransformDirty = this.isWorldTransformDirty;
				if (isWorldTransformDirty)
				{
					this.UpdateWorldTransform();
				}
				return ref this.m_WorldTransformCache;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
		internal ref Matrix4x4 worldTransformInverse
		{
			get
			{
				bool flag = this.isWorldTransformDirty || this.isWorldTransformInverseDirty;
				if (flag)
				{
					this.UpdateWorldTransformInverse();
				}
				return ref this.m_WorldTransformInverseCache;
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000CEEC File Offset: 0x0000B0EC
		internal void UpdateWorldTransform()
		{
			bool flag = this.elementPanel != null && !this.elementPanel.duringLayoutPhase;
			if (flag)
			{
				this.isWorldTransformDirty = false;
			}
			bool flag2 = this.hierarchy.parent != null;
			if (flag2)
			{
				bool hasDefaultRotationAndScale = this.hasDefaultRotationAndScale;
				if (hasDefaultRotationAndScale)
				{
					VisualElement.TranslateMatrix34(this.hierarchy.parent.worldTransformRef, this.positionWithLayout, out this.m_WorldTransformCache);
				}
				else
				{
					Matrix4x4 matrix4x;
					this.GetPivotedMatrixWithLayout(out matrix4x);
					VisualElement.MultiplyMatrix34(this.hierarchy.parent.worldTransformRef, ref matrix4x, out this.m_WorldTransformCache);
				}
			}
			else
			{
				this.GetPivotedMatrixWithLayout(out this.m_WorldTransformCache);
			}
			this.isWorldTransformInverseDirty = true;
			this.isWorldBoundingBoxDirty = true;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000CFB8 File Offset: 0x0000B1B8
		internal void UpdateWorldTransformInverse()
		{
			Matrix4x4.Inverse3DAffine(this.worldTransform, ref this.m_WorldTransformInverseCache);
			this.isWorldTransformInverseDirty = false;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000CFD5 File Offset: 0x0000B1D5
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0000CFE2 File Offset: 0x0000B1E2
		internal bool isWorldClipDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldClipDirty) == VisualElementFlags.WorldClipDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldClipDirty) : (this.m_Flags & ~VisualElementFlags.WorldClipDirty));
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000D000 File Offset: 0x0000B200
		internal Rect worldClip
		{
			get
			{
				bool isWorldClipDirty = this.isWorldClipDirty;
				if (isWorldClipDirty)
				{
					this.UpdateWorldClip();
					this.isWorldClipDirty = false;
				}
				return this.m_WorldClip;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000D034 File Offset: 0x0000B234
		internal Rect worldClipMinusGroup
		{
			get
			{
				bool isWorldClipDirty = this.isWorldClipDirty;
				if (isWorldClipDirty)
				{
					this.UpdateWorldClip();
					this.isWorldClipDirty = false;
				}
				return this.m_WorldClipMinusGroup;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000D068 File Offset: 0x0000B268
		internal bool worldClipIsInfinite
		{
			get
			{
				bool isWorldClipDirty = this.isWorldClipDirty;
				if (isWorldClipDirty)
				{
					this.UpdateWorldClip();
					this.isWorldClipDirty = false;
				}
				return this.m_WorldClipIsInfinite;
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000D09C File Offset: 0x0000B29C
		internal void EnsureWorldTransformAndClipUpToDate()
		{
			bool isWorldTransformDirty = this.isWorldTransformDirty;
			if (isWorldTransformDirty)
			{
				this.UpdateWorldTransform();
			}
			bool isWorldClipDirty = this.isWorldClipDirty;
			if (isWorldClipDirty)
			{
				this.UpdateWorldClip();
				this.isWorldClipDirty = false;
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000D0D8 File Offset: 0x0000B2D8
		private void UpdateWorldClip()
		{
			bool flag = this.hierarchy.parent != null;
			if (flag)
			{
				this.m_WorldClip = this.hierarchy.parent.worldClip;
				bool flag2 = this.hierarchy.parent.worldClipIsInfinite;
				bool flag3 = this.hierarchy.parent != this.renderChainData.groupTransformAncestor;
				if (flag3)
				{
					this.m_WorldClipMinusGroup = this.hierarchy.parent.worldClipMinusGroup;
				}
				else
				{
					flag2 = true;
					this.m_WorldClipMinusGroup = VisualElement.s_InfiniteRect;
				}
				bool flag4 = this.ShouldClip();
				if (flag4)
				{
					Rect rect = this.SubstractBorderPadding(this.worldBound);
					this.m_WorldClip = this.CombineClipRects(rect, this.m_WorldClip);
					this.m_WorldClipMinusGroup = (flag2 ? rect : this.CombineClipRects(rect, this.m_WorldClipMinusGroup));
					this.m_WorldClipIsInfinite = false;
				}
				else
				{
					this.m_WorldClipIsInfinite = flag2;
				}
			}
			else
			{
				this.m_WorldClipMinusGroup = (this.m_WorldClip = ((this.panel != null) ? this.panel.visualTree.rect : VisualElement.s_InfiniteRect));
				this.m_WorldClipIsInfinite = true;
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000D214 File Offset: 0x0000B414
		private Rect CombineClipRects(Rect rect, Rect parentRect)
		{
			float num = Mathf.Max(rect.xMin, parentRect.xMin);
			float num2 = Mathf.Min(rect.xMax, parentRect.xMax);
			float num3 = Mathf.Max(rect.yMin, parentRect.yMin);
			float num4 = Mathf.Min(rect.yMax, parentRect.yMax);
			float width = Mathf.Max(num2 - num, 0f);
			float height = Mathf.Max(num4 - num3, 0f);
			return new Rect(num, num3, width, height);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000D2A4 File Offset: 0x0000B4A4
		private Rect SubstractBorderPadding(Rect worldRect)
		{
			float m = this.worldTransform.m00;
			float m2 = this.worldTransform.m11;
			worldRect.x += this.resolvedStyle.borderLeftWidth * m;
			worldRect.y += this.resolvedStyle.borderTopWidth * m2;
			worldRect.width -= (this.resolvedStyle.borderLeftWidth + this.resolvedStyle.borderRightWidth) * m;
			worldRect.height -= (this.resolvedStyle.borderTopWidth + this.resolvedStyle.borderBottomWidth) * m2;
			bool flag = this.computedStyle.unityOverflowClipBox == OverflowClipBox.ContentBox;
			if (flag)
			{
				worldRect.x += this.resolvedStyle.paddingLeft * m;
				worldRect.y += this.resolvedStyle.paddingTop * m2;
				worldRect.width -= (this.resolvedStyle.paddingLeft + this.resolvedStyle.paddingRight) * m;
				worldRect.height -= (this.resolvedStyle.paddingTop + this.resolvedStyle.paddingBottom) * m2;
			}
			return worldRect;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000D3F8 File Offset: 0x0000B5F8
		internal static Rect ComputeAAAlignedBound(Rect position, Matrix4x4 mat)
		{
			Rect rect = position;
			Vector3 vector = mat.MultiplyPoint3x4(new Vector3(rect.x, rect.y, 0f));
			Vector3 vector2 = mat.MultiplyPoint3x4(new Vector3(rect.x + rect.width, rect.y, 0f));
			Vector3 vector3 = mat.MultiplyPoint3x4(new Vector3(rect.x, rect.y + rect.height, 0f));
			Vector3 vector4 = mat.MultiplyPoint3x4(new Vector3(rect.x + rect.width, rect.y + rect.height, 0f));
			return Rect.MinMaxRect(Mathf.Min(vector.x, Mathf.Min(vector2.x, Mathf.Min(vector3.x, vector4.x))), Mathf.Min(vector.y, Mathf.Min(vector2.y, Mathf.Min(vector3.y, vector4.y))), Mathf.Max(vector.x, Mathf.Max(vector2.x, Mathf.Max(vector3.x, vector4.x))), Mathf.Max(vector.y, Mathf.Max(vector2.y, Mathf.Max(vector3.y, vector4.y))));
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000D554 File Offset: 0x0000B754
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0000D56C File Offset: 0x0000B76C
		internal PseudoStates pseudoStates
		{
			get
			{
				return this.m_PseudoStates;
			}
			set
			{
				PseudoStates pseudoStates = this.m_PseudoStates ^ value;
				bool flag = pseudoStates > (PseudoStates)0;
				if (flag)
				{
					bool flag2 = (value & PseudoStates.Root) == PseudoStates.Root;
					if (flag2)
					{
						this.isRootVisualContainer = true;
					}
					bool flag3 = pseudoStates != PseudoStates.Root;
					if (flag3)
					{
						PseudoStates pseudoStates2 = pseudoStates & value;
						PseudoStates pseudoStates3 = pseudoStates & this.m_PseudoStates;
						bool flag4 = (this.triggerPseudoMask & pseudoStates2) != (PseudoStates)0 || (this.dependencyPseudoMask & pseudoStates3) > (PseudoStates)0;
						if (flag4)
						{
							this.IncrementVersion(VersionChangeType.StyleSheet);
						}
					}
					this.m_PseudoStates = value;
				}
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000D5F9 File Offset: 0x0000B7F9
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000D601 File Offset: 0x0000B801
		internal int containedPointerIds
		{
			[CompilerGenerated]
			get
			{
				return this.<containedPointerIds>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<containedPointerIds>k__BackingField = value;
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000D60C File Offset: 0x0000B80C
		private void UpdateHoverPseudoState()
		{
			bool flag = this.containedPointerIds == 0;
			if (flag)
			{
				this.pseudoStates &= ~PseudoStates.Hover;
			}
			else
			{
				bool flag2 = false;
				for (int i = 0; i < PointerId.maxPointers; i++)
				{
					bool flag3 = (this.containedPointerIds & 1 << i) != 0;
					if (flag3)
					{
						IPanel panel = this.panel;
						IEventHandler eventHandler = (panel != null) ? panel.GetCapturingElement(i) : null;
						bool flag4 = eventHandler == null || eventHandler == this;
						if (flag4)
						{
							flag2 = true;
							break;
						}
					}
				}
				bool flag5 = flag2;
				if (flag5)
				{
					this.pseudoStates |= PseudoStates.Hover;
				}
				else
				{
					this.pseudoStates &= ~PseudoStates.Hover;
				}
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000D6C1 File Offset: 0x0000B8C1
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000D6CC File Offset: 0x0000B8CC
		public PickingMode pickingMode
		{
			get
			{
				return this.m_PickingMode;
			}
			set
			{
				bool flag = this.m_PickingMode == value;
				if (!flag)
				{
					this.m_PickingMode = value;
					this.IncrementVersion(VersionChangeType.Picking);
				}
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000D6FC File Offset: 0x0000B8FC
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000D714 File Offset: 0x0000B914
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				bool flag = this.m_Name == value;
				if (!flag)
				{
					this.m_Name = value;
					this.IncrementVersion(VersionChangeType.StyleSheet);
				}
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000D744 File Offset: 0x0000B944
		internal List<string> classList
		{
			get
			{
				bool flag = this.m_ClassList == VisualElement.s_EmptyClassList;
				if (flag)
				{
					this.m_ClassList = ObjectListPool<string>.Get();
				}
				return this.m_ClassList;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000D77A File Offset: 0x0000B97A
		internal string fullTypeName
		{
			get
			{
				return this.typeData.fullTypeName;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000D787 File Offset: 0x0000B987
		internal string typeName
		{
			get
			{
				return this.typeData.typeName;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000D794 File Offset: 0x0000B994
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0000D79C File Offset: 0x0000B99C
		internal YogaNode yogaNode
		{
			[CompilerGenerated]
			get
			{
				return this.<yogaNode>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<yogaNode>k__BackingField = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000D7A5 File Offset: 0x0000B9A5
		internal ref ComputedStyle computedStyle
		{
			get
			{
				return ref this.m_Style;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000D7AD File Offset: 0x0000B9AD
		internal bool hasInlineStyle
		{
			get
			{
				return this.inlineStyleAccess != null;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000D7B8 File Offset: 0x0000B9B8
		// (set) Token: 0x060003AF RID: 943 RVA: 0x0000D7CD File Offset: 0x0000B9CD
		internal bool styleInitialized
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.StyleInitialized) == VisualElementFlags.StyleInitialized;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.StyleInitialized) : (this.m_Flags & ~VisualElementFlags.StyleInitialized));
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000D7F4 File Offset: 0x0000B9F4
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0000D811 File Offset: 0x0000BA11
		internal float opacity
		{
			get
			{
				return this.resolvedStyle.opacity;
			}
			set
			{
				this.style.opacity = value;
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000D828 File Offset: 0x0000BA28
		private void ChangeIMGUIContainerCount(int delta)
		{
			for (VisualElement visualElement = this; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				visualElement.imguiContainerDescendantCount += delta;
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000D864 File Offset: 0x0000BA64
		public VisualElement()
		{
			UIElementsRuntimeUtilityNative.VisualElementCreation();
			this.m_Children = VisualElement.s_EmptyList;
			this.controlid = (VisualElement.s_NextId += 1U);
			this.hierarchy = new VisualElement.Hierarchy(this);
			this.m_ClassList = VisualElement.s_EmptyClassList;
			this.m_Flags = VisualElementFlags.Init;
			this.SetEnabled(true);
			base.focusable = false;
			this.name = string.Empty;
			this.yogaNode = new YogaNode(null);
			this.renderHints = RenderHints.None;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000D968 File Offset: 0x0000BB68
		protected override void ExecuteDefaultAction(EventBase evt)
		{
			base.ExecuteDefaultAction(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<MouseOverEvent>.TypeId() || evt.eventTypeId == EventBase<MouseOutEvent>.TypeId();
				if (flag2)
				{
					this.UpdateCursorStyle(evt.eventTypeId);
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<PointerEnterEvent>.TypeId();
					if (flag3)
					{
						this.containedPointerIds |= 1 << ((IPointerEvent)evt).pointerId;
						this.UpdateHoverPseudoState();
					}
					else
					{
						bool flag4 = evt.eventTypeId == EventBase<PointerLeaveEvent>.TypeId();
						if (flag4)
						{
							this.containedPointerIds &= ~(1 << ((IPointerEvent)evt).pointerId);
							this.UpdateHoverPseudoState();
						}
						else
						{
							bool flag5 = evt.eventTypeId == EventBase<PointerCaptureEvent>.TypeId() || evt.eventTypeId == EventBase<PointerCaptureOutEvent>.TypeId();
							if (flag5)
							{
								this.UpdateHoverPseudoState();
								BaseVisualElementPanel elementPanel = this.elementPanel;
								VisualElement visualElement = (elementPanel != null) ? elementPanel.GetTopElementUnderPointer(((IPointerCaptureEventInternal)evt).pointerId) : null;
								VisualElement visualElement2 = visualElement;
								while (visualElement2 != null && visualElement2 != this)
								{
									visualElement2.UpdateHoverPseudoState();
									visualElement2 = visualElement2.parent;
								}
							}
							else
							{
								bool flag6 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
								if (flag6)
								{
									this.pseudoStates &= ~PseudoStates.Focus;
								}
								else
								{
									bool flag7 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
									if (flag7)
									{
										this.pseudoStates |= PseudoStates.Focus;
									}
									else
									{
										bool flag8 = evt.eventTypeId == EventBase<TooltipEvent>.TypeId();
										if (flag8)
										{
											this.SetTooltip((TooltipEvent)evt);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000DB28 File Offset: 0x0000BD28
		internal virtual Rect GetTooltipRect()
		{
			return this.worldBound;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000DB40 File Offset: 0x0000BD40
		private void SetTooltip(TooltipEvent e)
		{
			VisualElement visualElement = e.currentTarget as VisualElement;
			bool flag = visualElement != null && !string.IsNullOrEmpty(visualElement.tooltip);
			if (flag)
			{
				e.rect = visualElement.GetTooltipRect();
				e.tooltip = visualElement.tooltip;
				e.StopImmediatePropagation();
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000DB98 File Offset: 0x0000BD98
		public sealed override void Focus()
		{
			bool flag = !this.canGrabFocus && this.hierarchy.parent != null;
			if (flag)
			{
				this.hierarchy.parent.Focus();
			}
			else
			{
				base.Focus();
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
		internal void SetPanel(BaseVisualElementPanel p)
		{
			bool flag = this.panel == p;
			if (!flag)
			{
				List<VisualElement> list = VisualElementListPool.Get(0);
				try
				{
					list.Add(this);
					this.GatherAllChildren(list);
					EventDispatcherGate? eventDispatcherGate = null;
					bool flag2 = ((p != null) ? p.dispatcher : null) != null;
					if (flag2)
					{
						eventDispatcherGate = new EventDispatcherGate?(new EventDispatcherGate(p.dispatcher));
					}
					EventDispatcherGate? eventDispatcherGate2 = null;
					IPanel panel = this.panel;
					bool flag3 = ((panel != null) ? panel.dispatcher : null) != null && this.panel.dispatcher != ((p != null) ? p.dispatcher : null);
					if (flag3)
					{
						eventDispatcherGate2 = new EventDispatcherGate?(new EventDispatcherGate(this.panel.dispatcher));
					}
					BaseVisualElementPanel elementPanel = this.elementPanel;
					uint num = (elementPanel != null) ? elementPanel.hierarchyVersion : 0U;
					EventDispatcherGate? eventDispatcherGate3 = eventDispatcherGate;
					try
					{
						EventDispatcherGate? eventDispatcherGate4 = eventDispatcherGate2;
						try
						{
							IPanel panel2 = this.panel;
							if (panel2 != null)
							{
								EventDispatcher dispatcher = panel2.dispatcher;
								if (dispatcher != null)
								{
									dispatcher.m_ClickDetector.Cleanup(list);
								}
							}
							foreach (VisualElement visualElement in list)
							{
								visualElement.WillChangePanel(p);
							}
							uint num2 = (elementPanel != null) ? elementPanel.hierarchyVersion : 0U;
							bool flag4 = num != num2;
							if (flag4)
							{
								list.Clear();
								list.Add(this);
								this.GatherAllChildren(list);
							}
							VisualElementFlags visualElementFlags = (p != null) ? VisualElementFlags.NeedsAttachToPanelEvent : ((VisualElementFlags)0);
							foreach (VisualElement visualElement2 in list)
							{
								visualElement2.elementPanel = p;
								visualElement2.m_Flags |= visualElementFlags;
							}
							foreach (VisualElement visualElement3 in list)
							{
								visualElement3.HasChangedPanel(elementPanel);
							}
						}
						finally
						{
							if (eventDispatcherGate4 != null)
							{
								((IDisposable)eventDispatcherGate4.GetValueOrDefault()).Dispose();
							}
						}
					}
					finally
					{
						if (eventDispatcherGate3 != null)
						{
							((IDisposable)eventDispatcherGate3.GetValueOrDefault()).Dispose();
						}
					}
				}
				finally
				{
					VisualElementListPool.Release(list);
				}
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000DED8 File Offset: 0x0000C0D8
		private void WillChangePanel(BaseVisualElementPanel destinationPanel)
		{
			bool flag = this.panel != null;
			if (flag)
			{
				this.UnregisterRunningAnimations();
				bool flag2 = (this.m_Flags & VisualElementFlags.NeedsAttachToPanelEvent) == (VisualElementFlags)0;
				if (flag2)
				{
					using (DetachFromPanelEvent pooled = PanelChangedEventBase<DetachFromPanelEvent>.GetPooled(this.panel, destinationPanel))
					{
						pooled.target = this;
						this.elementPanel.SendEvent(pooled, DispatchMode.Immediate);
					}
				}
				this.UnregisterRunningAnimations();
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000DF5C File Offset: 0x0000C15C
		private void HasChangedPanel(BaseVisualElementPanel prevPanel)
		{
			bool flag = this.panel != null;
			if (flag)
			{
				this.yogaNode.Config = this.elementPanel.yogaConfig;
				this.RegisterRunningAnimations();
				this.pseudoStates &= ~(PseudoStates.Active | PseudoStates.Hover | PseudoStates.Focus);
				bool flag2 = (this.m_Flags & VisualElementFlags.NeedsAttachToPanelEvent) == VisualElementFlags.NeedsAttachToPanelEvent;
				if (flag2)
				{
					using (AttachToPanelEvent pooled = PanelChangedEventBase<AttachToPanelEvent>.GetPooled(prevPanel, this.panel))
					{
						pooled.target = this;
						this.elementPanel.SendEvent(pooled, DispatchMode.Immediate);
					}
					this.m_Flags &= ~VisualElementFlags.NeedsAttachToPanelEvent;
				}
			}
			else
			{
				this.yogaNode.Config = YogaConfig.Default;
			}
			this.styleInitialized = false;
			this.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Transform);
			bool flag3 = !string.IsNullOrEmpty(this.viewDataKey);
			if (flag3)
			{
				this.IncrementVersion(VersionChangeType.ViewData);
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000E05C File Offset: 0x0000C25C
		public sealed override void SendEvent(EventBase e)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.SendEvent(e, DispatchMode.Default);
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000E073 File Offset: 0x0000C273
		internal sealed override void SendEvent(EventBase e, DispatchMode dispatchMode)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.SendEvent(e, dispatchMode);
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000E08A File Offset: 0x0000C28A
		internal void IncrementVersion(VersionChangeType changeType)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.OnVersionChanged(this, changeType);
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000E0A1 File Offset: 0x0000C2A1
		internal void InvokeHierarchyChanged(HierarchyChangeType changeType)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.InvokeHierarchyChanged(this, changeType);
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000E0B8 File Offset: 0x0000C2B8
		[Obsolete("SetEnabledFromHierarchy is deprecated and will be removed in a future release. Please use SetEnabled instead.")]
		protected internal bool SetEnabledFromHierarchy(bool state)
		{
			return this.SetEnabledFromHierarchyPrivate(state);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000E0D4 File Offset: 0x0000C2D4
		private bool SetEnabledFromHierarchyPrivate(bool state)
		{
			bool enabledInHierarchy = this.enabledInHierarchy;
			bool flag = false;
			if (state)
			{
				bool isParentEnabledInHierarchy = this.isParentEnabledInHierarchy;
				if (isParentEnabledInHierarchy)
				{
					bool enabledSelf = this.enabledSelf;
					if (enabledSelf)
					{
						this.RemoveFromClassList(VisualElement.disabledUssClassName);
					}
					else
					{
						flag = true;
						this.AddToClassList(VisualElement.disabledUssClassName);
					}
				}
				else
				{
					flag = true;
					this.RemoveFromClassList(VisualElement.disabledUssClassName);
				}
			}
			else
			{
				flag = true;
				this.EnableInClassList(VisualElement.disabledUssClassName, this.isParentEnabledInHierarchy);
			}
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = this.focusController != null && this.focusController.IsFocused(this);
				if (flag3)
				{
					EventDispatcherGate? eventDispatcherGate = null;
					IPanel panel = this.panel;
					bool flag4 = ((panel != null) ? panel.dispatcher : null) != null;
					if (flag4)
					{
						eventDispatcherGate = new EventDispatcherGate?(new EventDispatcherGate(this.panel.dispatcher));
					}
					EventDispatcherGate? eventDispatcherGate2 = eventDispatcherGate;
					try
					{
						base.BlurImmediately();
					}
					finally
					{
						if (eventDispatcherGate2 != null)
						{
							((IDisposable)eventDispatcherGate2.GetValueOrDefault()).Dispose();
						}
					}
				}
				this.pseudoStates |= PseudoStates.Disabled;
			}
			else
			{
				this.pseudoStates &= ~PseudoStates.Disabled;
			}
			return enabledInHierarchy != this.enabledInHierarchy;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000E234 File Offset: 0x0000C434
		private bool isParentEnabledInHierarchy
		{
			get
			{
				return this.hierarchy.parent == null || this.hierarchy.parent.enabledInHierarchy;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000E26C File Offset: 0x0000C46C
		public bool enabledInHierarchy
		{
			get
			{
				return (this.pseudoStates & PseudoStates.Disabled) != PseudoStates.Disabled;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000E28E File Offset: 0x0000C48E
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000E296 File Offset: 0x0000C496
		public bool enabledSelf
		{
			[CompilerGenerated]
			get
			{
				return this.<enabledSelf>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<enabledSelf>k__BackingField = value;
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000E2A0 File Offset: 0x0000C4A0
		public void SetEnabled(bool value)
		{
			bool flag = this.enabledSelf == value;
			if (!flag)
			{
				this.enabledSelf = value;
				this.PropagateEnabledToChildren(value);
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000E2D0 File Offset: 0x0000C4D0
		private void PropagateEnabledToChildren(bool value)
		{
			bool flag = this.SetEnabledFromHierarchyPrivate(value);
			if (flag)
			{
				int count = this.m_Children.Count;
				for (int i = 0; i < count; i++)
				{
					this.m_Children[i].PropagateEnabledToChildren(value);
				}
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000E31C File Offset: 0x0000C51C
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x0000E33C File Offset: 0x0000C53C
		public bool visible
		{
			get
			{
				return this.resolvedStyle.visibility == Visibility.Visible;
			}
			set
			{
				this.style.visibility = (value ? Visibility.Visible : Visibility.Hidden);
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000E357 File Offset: 0x0000C557
		public void MarkDirtyRepaint()
		{
			this.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000E366 File Offset: 0x0000C566
		// (set) Token: 0x060003CB RID: 971 RVA: 0x0000E36E File Offset: 0x0000C56E
		public Action<MeshGenerationContext> generateVisualContent
		{
			[CompilerGenerated]
			get
			{
				return this.<generateVisualContent>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<generateVisualContent>k__BackingField = value;
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000E378 File Offset: 0x0000C578
		internal void InvokeGenerateVisualContent(MeshGenerationContext mgc)
		{
			bool flag = this.generateVisualContent != null;
			if (flag)
			{
				try
				{
					using (this.k_GenerateVisualContentMarker.Auto())
					{
						this.generateVisualContent(mgc);
					}
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000E3EC File Offset: 0x0000C5EC
		internal void GetFullHierarchicalViewDataKey(StringBuilder key)
		{
			bool flag = this.parent != null;
			if (flag)
			{
				this.parent.GetFullHierarchicalViewDataKey(key);
			}
			bool flag2 = !string.IsNullOrEmpty(this.viewDataKey);
			if (flag2)
			{
				key.Append("__");
				key.Append(this.viewDataKey);
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000E444 File Offset: 0x0000C644
		internal string GetFullHierarchicalViewDataKey()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.GetFullHierarchicalViewDataKey(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000E46C File Offset: 0x0000C66C
		internal T GetOrCreateViewData<T>(object existing, string key) where T : class, new()
		{
			Debug.Assert(this.elementPanel != null, "VisualElement.elementPanel is null! Cannot load persistent data.");
			ISerializableJsonDictionary serializableJsonDictionary = (this.elementPanel == null || this.elementPanel.getViewDataDictionary == null) ? null : this.elementPanel.getViewDataDictionary();
			bool flag = serializableJsonDictionary == null || string.IsNullOrEmpty(this.viewDataKey) || !this.enableViewDataPersistence;
			T result;
			if (flag)
			{
				bool flag2 = existing != null;
				if (flag2)
				{
					result = (existing as T);
				}
				else
				{
					result = Activator.CreateInstance<T>();
				}
			}
			else
			{
				string str = "__";
				Type typeFromHandle = typeof(T);
				string key2 = key + str + ((typeFromHandle != null) ? typeFromHandle.ToString() : null);
				bool flag3 = !serializableJsonDictionary.ContainsKey(key2);
				if (flag3)
				{
					serializableJsonDictionary.Set<T>(key2, Activator.CreateInstance<T>());
				}
				result = serializableJsonDictionary.Get<T>(key2);
			}
			return result;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000E544 File Offset: 0x0000C744
		internal T GetOrCreateViewData<T>(ScriptableObject existing, string key) where T : ScriptableObject
		{
			Debug.Assert(this.elementPanel != null, "VisualElement.elementPanel is null! Cannot load view data.");
			ISerializableJsonDictionary serializableJsonDictionary = (this.elementPanel == null || this.elementPanel.getViewDataDictionary == null) ? null : this.elementPanel.getViewDataDictionary();
			bool flag = serializableJsonDictionary == null || string.IsNullOrEmpty(this.viewDataKey) || !this.enableViewDataPersistence;
			T result;
			if (flag)
			{
				bool flag2 = existing != null;
				if (flag2)
				{
					result = (existing as T);
				}
				else
				{
					result = ScriptableObject.CreateInstance<T>();
				}
			}
			else
			{
				string str = "__";
				Type typeFromHandle = typeof(T);
				string key2 = key + str + ((typeFromHandle != null) ? typeFromHandle.ToString() : null);
				bool flag3 = !serializableJsonDictionary.ContainsKey(key2);
				if (flag3)
				{
					serializableJsonDictionary.Set<T>(key2, ScriptableObject.CreateInstance<T>());
				}
				result = serializableJsonDictionary.GetScriptable<T>(key2);
			}
			return result;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000E620 File Offset: 0x0000C820
		internal void OverwriteFromViewData(object obj, string key)
		{
			bool flag = obj == null;
			if (flag)
			{
				throw new ArgumentNullException("obj");
			}
			Debug.Assert(this.elementPanel != null, "VisualElement.elementPanel is null! Cannot load view data.");
			ISerializableJsonDictionary serializableJsonDictionary = (this.elementPanel == null || this.elementPanel.getViewDataDictionary == null) ? null : this.elementPanel.getViewDataDictionary();
			bool flag2 = serializableJsonDictionary == null || string.IsNullOrEmpty(this.viewDataKey) || !this.enableViewDataPersistence;
			if (!flag2)
			{
				string str = "__";
				Type type = obj.GetType();
				string key2 = key + str + ((type != null) ? type.ToString() : null);
				bool flag3 = !serializableJsonDictionary.ContainsKey(key2);
				if (flag3)
				{
					serializableJsonDictionary.Set<object>(key2, obj);
				}
				else
				{
					serializableJsonDictionary.Overwrite(obj, key2);
				}
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000E6E8 File Offset: 0x0000C8E8
		internal void SaveViewData()
		{
			bool flag = this.elementPanel != null && this.elementPanel.saveViewData != null && !string.IsNullOrEmpty(this.viewDataKey) && this.enableViewDataPersistence;
			if (flag)
			{
				this.elementPanel.saveViewData();
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000E738 File Offset: 0x0000C938
		internal bool IsViewDataPersitenceSupportedOnChildren(bool existingState)
		{
			bool result = existingState;
			bool flag = string.IsNullOrEmpty(this.viewDataKey) && this != this.contentContainer;
			if (flag)
			{
				result = false;
			}
			bool flag2 = this.parent != null && this == this.parent.contentContainer;
			if (flag2)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000E78E File Offset: 0x0000C98E
		internal void OnViewDataReady(bool enablePersistence)
		{
			this.enableViewDataPersistence = enablePersistence;
			this.OnViewDataReady();
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00002166 File Offset: 0x00000366
		internal virtual void OnViewDataReady()
		{
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
		public virtual bool ContainsPoint(Vector2 localPoint)
		{
			return this.rect.Contains(localPoint);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000E7C4 File Offset: 0x0000C9C4
		public virtual bool Overlaps(Rect rectangle)
		{
			return this.rect.Overlaps(rectangle, true);
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000E7E6 File Offset: 0x0000C9E6
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x0000E7FC File Offset: 0x0000C9FC
		internal bool requireMeasureFunction
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.RequireMeasureFunction) == VisualElementFlags.RequireMeasureFunction;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.RequireMeasureFunction) : (this.m_Flags & ~VisualElementFlags.RequireMeasureFunction));
				bool flag = value && !this.yogaNode.IsMeasureDefined;
				if (flag)
				{
					this.AssignMeasureFunction();
				}
				else
				{
					bool flag2 = !value && this.yogaNode.IsMeasureDefined;
					if (flag2)
					{
						this.RemoveMeasureFunction();
					}
				}
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000E86E File Offset: 0x0000CA6E
		private void AssignMeasureFunction()
		{
			this.yogaNode.SetMeasureFunction((YogaNode node, float f, YogaMeasureMode mode, float f1, YogaMeasureMode heightMode) => this.Measure(node, f, mode, f1, heightMode));
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000E889 File Offset: 0x0000CA89
		private void RemoveMeasureFunction()
		{
			this.yogaNode.SetMeasureFunction(null);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000E89C File Offset: 0x0000CA9C
		protected internal virtual Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			return new Vector2(float.NaN, float.NaN);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
		internal YogaSize Measure(YogaNode node, float width, YogaMeasureMode widthMode, float height, YogaMeasureMode heightMode)
		{
			Debug.Assert(node == this.yogaNode, "YogaNode instance mismatch");
			Vector2 vector = this.DoMeasure(width, (VisualElement.MeasureMode)widthMode, height, (VisualElement.MeasureMode)heightMode);
			float scaledPixelsPerPoint = this.scaledPixelsPerPoint;
			return MeasureOutput.Make(AlignmentUtils.RoundToPixelGrid(vector.x, scaledPixelsPerPoint, 0.02f), AlignmentUtils.RoundToPixelGrid(vector.y, scaledPixelsPerPoint, 0.02f));
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000E924 File Offset: 0x0000CB24
		internal void SetSize(Vector2 size)
		{
			Rect layout = this.layout;
			layout.width = size.x;
			layout.height = size.y;
			this.layout = layout;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000E960 File Offset: 0x0000CB60
		private void FinalizeLayout()
		{
			bool flag = this.hasInlineStyle || this.hasRunningAnimations;
			if (flag)
			{
				this.computedStyle.SyncWithLayout(this.yogaNode);
			}
			else
			{
				this.yogaNode.CopyStyle(this.computedStyle.yogaNode);
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
		internal void SetInlineRule(StyleSheet sheet, StyleRule rule)
		{
			bool flag = this.inlineStyleAccess == null;
			if (flag)
			{
				this.inlineStyleAccess = new InlineStyleAccess(this);
			}
			this.inlineStyleAccess.SetInlineRule(sheet, rule);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000E9EC File Offset: 0x0000CBEC
		internal unsafe void UpdateInlineRule(StyleSheet sheet, StyleRule rule)
		{
			ComputedStyle computedStyle = this.computedStyle.Acquire();
			long matchingRulesHash = this.computedStyle.matchingRulesHash;
			ComputedStyle computedStyle2;
			bool flag = !StyleCache.TryGetValue(matchingRulesHash, out computedStyle2);
			if (flag)
			{
				computedStyle2 = *InitialStyle.Get();
			}
			this.m_Style.CopyFrom(ref computedStyle2);
			this.SetInlineRule(sheet, rule);
			this.FinalizeLayout();
			VersionChangeType changeType = ComputedStyle.CompareChanges(ref computedStyle, this.computedStyle);
			computedStyle.Release();
			this.IncrementVersion(changeType);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000EA6C File Offset: 0x0000CC6C
		internal void SetComputedStyle(ref ComputedStyle newStyle)
		{
			bool flag = this.m_Style.matchingRulesHash == newStyle.matchingRulesHash;
			if (!flag)
			{
				VersionChangeType changeType = ComputedStyle.CompareChanges(ref this.m_Style, ref newStyle);
				this.m_Style.CopyFrom(ref newStyle);
				this.FinalizeLayout();
				BaseVisualElementPanel elementPanel = this.elementPanel;
				bool flag2 = ((elementPanel != null) ? elementPanel.GetTopElementUnderPointer(PointerId.mousePointerId) : null) == this;
				if (flag2)
				{
					this.elementPanel.cursorManager.SetCursor(this.m_Style.cursor);
				}
				this.IncrementVersion(changeType);
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
		internal void ResetPositionProperties()
		{
			bool flag = !this.hasInlineStyle;
			if (!flag)
			{
				this.style.position = StyleKeyword.Null;
				this.style.marginLeft = StyleKeyword.Null;
				this.style.marginRight = StyleKeyword.Null;
				this.style.marginBottom = StyleKeyword.Null;
				this.style.marginTop = StyleKeyword.Null;
				this.style.left = StyleKeyword.Null;
				this.style.top = StyleKeyword.Null;
				this.style.right = StyleKeyword.Null;
				this.style.bottom = StyleKeyword.Null;
				this.style.width = StyleKeyword.Null;
				this.style.height = StyleKeyword.Null;
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000EBE0 File Offset: 0x0000CDE0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				" ",
				this.name,
				" ",
				this.layout.ToString(),
				" world rect: ",
				this.worldBound.ToString()
			});
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000EC5C File Offset: 0x0000CE5C
		public IEnumerable<string> GetClasses()
		{
			return this.m_ClassList;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000EC74 File Offset: 0x0000CE74
		internal List<string> GetClassesForIteration()
		{
			return this.m_ClassList;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000EC8C File Offset: 0x0000CE8C
		public void ClearClassList()
		{
			bool flag = this.m_ClassList.Count > 0;
			if (flag)
			{
				ObjectListPool<string>.Release(this.m_ClassList);
				this.m_ClassList = VisualElement.s_EmptyClassList;
				this.IncrementVersion(VersionChangeType.StyleSheet);
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000ECD0 File Offset: 0x0000CED0
		public void AddToClassList(string className)
		{
			bool flag = this.m_ClassList == VisualElement.s_EmptyClassList;
			if (flag)
			{
				this.m_ClassList = ObjectListPool<string>.Get();
			}
			else
			{
				bool flag2 = this.m_ClassList.Contains(className);
				if (flag2)
				{
					return;
				}
				bool flag3 = this.m_ClassList.Capacity == this.m_ClassList.Count;
				if (flag3)
				{
					this.m_ClassList.Capacity++;
				}
			}
			this.m_ClassList.Add(className);
			this.IncrementVersion(VersionChangeType.StyleSheet);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000ED5C File Offset: 0x0000CF5C
		public void RemoveFromClassList(string className)
		{
			bool flag = this.m_ClassList.Remove(className);
			if (flag)
			{
				bool flag2 = this.m_ClassList.Count == 0;
				if (flag2)
				{
					ObjectListPool<string>.Release(this.m_ClassList);
					this.m_ClassList = VisualElement.s_EmptyClassList;
				}
				this.IncrementVersion(VersionChangeType.StyleSheet);
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000EDB0 File Offset: 0x0000CFB0
		public void ToggleInClassList(string className)
		{
			bool flag = this.ClassListContains(className);
			if (flag)
			{
				this.RemoveFromClassList(className);
			}
			else
			{
				this.AddToClassList(className);
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000EDDC File Offset: 0x0000CFDC
		public void EnableInClassList(string className, bool enable)
		{
			if (enable)
			{
				this.AddToClassList(className);
			}
			else
			{
				this.RemoveFromClassList(className);
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000EE04 File Offset: 0x0000D004
		public bool ClassListContains(string cls)
		{
			for (int i = 0; i < this.m_ClassList.Count; i++)
			{
				bool flag = this.m_ClassList[i] == cls;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000EE50 File Offset: 0x0000D050
		public object FindAncestorUserData()
		{
			for (VisualElement parent = this.parent; parent != null; parent = parent.parent)
			{
				bool flag = parent.userData != null;
				if (flag)
				{
					return parent.userData;
				}
			}
			return null;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000EE94 File Offset: 0x0000D094
		internal object GetProperty(PropertyName key)
		{
			VisualElement.CheckUserKeyArgument(key);
			object result;
			this.TryGetPropertyInternal(key, out result);
			return result;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000EEB8 File Offset: 0x0000D0B8
		internal void SetProperty(PropertyName key, object value)
		{
			VisualElement.CheckUserKeyArgument(key);
			this.SetPropertyInternal(key, value);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000EECC File Offset: 0x0000D0CC
		internal bool HasProperty(PropertyName key)
		{
			VisualElement.CheckUserKeyArgument(key);
			object obj;
			return this.TryGetPropertyInternal(key, out obj);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000EEF0 File Offset: 0x0000D0F0
		private bool TryGetPropertyInternal(PropertyName key, out object value)
		{
			value = null;
			bool flag = this.m_PropertyBag != null;
			if (flag)
			{
				for (int i = 0; i < this.m_PropertyBag.Count; i++)
				{
					bool flag2 = this.m_PropertyBag[i].Key == key;
					if (flag2)
					{
						value = this.m_PropertyBag[i].Value;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000EF70 File Offset: 0x0000D170
		private static void CheckUserKeyArgument(PropertyName key)
		{
			bool flag = PropertyName.IsNullOrEmpty(key);
			if (flag)
			{
				throw new ArgumentNullException("key");
			}
			bool flag2 = key == VisualElement.userDataPropertyKey;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("The {0} key is reserved by the system", VisualElement.userDataPropertyKey));
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000EFBC File Offset: 0x0000D1BC
		private void SetPropertyInternal(PropertyName key, object value)
		{
			KeyValuePair<PropertyName, object> keyValuePair = new KeyValuePair<PropertyName, object>(key, value);
			bool flag = this.m_PropertyBag == null;
			if (flag)
			{
				this.m_PropertyBag = new List<KeyValuePair<PropertyName, object>>(1);
				this.m_PropertyBag.Add(keyValuePair);
			}
			else
			{
				for (int i = 0; i < this.m_PropertyBag.Count; i++)
				{
					bool flag2 = this.m_PropertyBag[i].Key == key;
					if (flag2)
					{
						this.m_PropertyBag[i] = keyValuePair;
						return;
					}
				}
				bool flag3 = this.m_PropertyBag.Capacity == this.m_PropertyBag.Count;
				if (flag3)
				{
					this.m_PropertyBag.Capacity++;
				}
				this.m_PropertyBag.Add(keyValuePair);
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000F094 File Offset: 0x0000D294
		private void UpdateCursorStyle(long eventType)
		{
			bool flag = this.elementPanel != null;
			if (flag)
			{
				bool flag2 = eventType == EventBase<MouseOverEvent>.TypeId() && this.elementPanel.GetTopElementUnderPointer(PointerId.mousePointerId) == this;
				if (flag2)
				{
					this.elementPanel.cursorManager.SetCursor(this.computedStyle.cursor);
				}
				else
				{
					bool flag3 = eventType == EventBase<MouseOutEvent>.TypeId();
					if (flag3)
					{
						this.elementPanel.cursorManager.ResetCursor();
					}
				}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000F114 File Offset: 0x0000D314
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0000F12C File Offset: 0x0000D32C
		internal VisualElement.RenderTargetMode subRenderTargetMode
		{
			get
			{
				return this.m_SubRenderTargetMode;
			}
			set
			{
				bool flag = this.m_SubRenderTargetMode == value;
				if (!flag)
				{
					Debug.Assert(Application.isEditor, "subRenderTargetMode is not supported on runtime yet");
					this.m_SubRenderTargetMode = value;
					this.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000F16C File Offset: 0x0000D36C
		private Material getRuntimeMaterial()
		{
			bool flag = VisualElement.s_runtimeMaterial != null;
			Material result;
			if (flag)
			{
				result = VisualElement.s_runtimeMaterial;
			}
			else
			{
				Shader shader = Shader.Find(UIRUtility.k_DefaultShaderName);
				Debug.Assert(shader != null, "Failed to load UIElements default shader");
				bool flag2 = shader != null;
				if (flag2)
				{
					shader.hideFlags |= HideFlags.DontSaveInEditor;
					Material material = new Material(shader);
					material.hideFlags |= HideFlags.DontSaveInEditor;
					result = (VisualElement.s_runtimeMaterial = material);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000F1F4 File Offset: 0x0000D3F4
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000F20C File Offset: 0x0000D40C
		internal Material defaultMaterial
		{
			get
			{
				return this.m_defaultMaterial;
			}
			private set
			{
				bool flag = this.m_defaultMaterial == value;
				if (!flag)
				{
					this.m_defaultMaterial = value;
					this.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000F240 File Offset: 0x0000D440
		private VisualElementAnimationSystem GetAnimationSystem()
		{
			bool flag = this.elementPanel != null;
			VisualElementAnimationSystem result;
			if (flag)
			{
				result = (this.elementPanel.GetUpdater(VisualTreeUpdatePhase.Animation) as VisualElementAnimationSystem);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000F278 File Offset: 0x0000D478
		internal void RegisterAnimation(IValueAnimationUpdate anim)
		{
			bool flag = this.m_RunningAnimations == null;
			if (flag)
			{
				this.m_RunningAnimations = new List<IValueAnimationUpdate>();
			}
			this.m_RunningAnimations.Add(anim);
			VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
			bool flag2 = animationSystem != null;
			if (flag2)
			{
				animationSystem.RegisterAnimation(anim);
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
		internal void UnregisterAnimation(IValueAnimationUpdate anim)
		{
			bool flag = this.m_RunningAnimations != null;
			if (flag)
			{
				this.m_RunningAnimations.Remove(anim);
			}
			VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
			bool flag2 = animationSystem != null;
			if (flag2)
			{
				animationSystem.UnregisterAnimation(anim);
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000F30C File Offset: 0x0000D50C
		private void UnregisterRunningAnimations()
		{
			bool flag = this.m_RunningAnimations != null && this.m_RunningAnimations.Count > 0;
			if (flag)
			{
				VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
				bool flag2 = animationSystem != null;
				if (flag2)
				{
					animationSystem.UnregisterAnimations(this.m_RunningAnimations);
				}
			}
			this.styleAnimation.CancelAllAnimations();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000F364 File Offset: 0x0000D564
		private void RegisterRunningAnimations()
		{
			bool flag = this.m_RunningAnimations != null && this.m_RunningAnimations.Count > 0;
			if (flag)
			{
				VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
				bool flag2 = animationSystem != null;
				if (flag2)
				{
					animationSystem.RegisterAnimations(this.m_RunningAnimations);
				}
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		ValueAnimation<float> ITransitionAnimations.Start(float from, float to, int durationMs, Action<VisualElement, float> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000F3F0 File Offset: 0x0000D5F0
		ValueAnimation<Rect> ITransitionAnimations.Start(Rect from, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000F430 File Offset: 0x0000D630
		ValueAnimation<Color> ITransitionAnimations.Start(Color from, Color to, int durationMs, Action<VisualElement, Color> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000F470 File Offset: 0x0000D670
		ValueAnimation<Vector3> ITransitionAnimations.Start(Vector3 from, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000F4B0 File Offset: 0x0000D6B0
		ValueAnimation<Vector2> ITransitionAnimations.Start(Vector2 from, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		ValueAnimation<Quaternion> ITransitionAnimations.Start(Quaternion from, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000F530 File Offset: 0x0000D730
		ValueAnimation<StyleValues> ITransitionAnimations.Start(StyleValues from, StyleValues to, int durationMs)
		{
			return this.Start((VisualElement e) => from, to, durationMs);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000F564 File Offset: 0x0000D764
		ValueAnimation<float> ITransitionAnimations.Start(Func<VisualElement, float> fromValueGetter, float to, int durationMs, Action<VisualElement, float> onValueChanged)
		{
			return VisualElement.StartAnimation<float>(ValueAnimation<float>.Create(this, new Func<float, float, float, float>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000F594 File Offset: 0x0000D794
		ValueAnimation<Rect> ITransitionAnimations.Start(Func<VisualElement, Rect> fromValueGetter, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged)
		{
			return VisualElement.StartAnimation<Rect>(ValueAnimation<Rect>.Create(this, new Func<Rect, Rect, float, Rect>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000F5C4 File Offset: 0x0000D7C4
		ValueAnimation<Color> ITransitionAnimations.Start(Func<VisualElement, Color> fromValueGetter, Color to, int durationMs, Action<VisualElement, Color> onValueChanged)
		{
			return VisualElement.StartAnimation<Color>(ValueAnimation<Color>.Create(this, new Func<Color, Color, float, Color>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000F5F4 File Offset: 0x0000D7F4
		ValueAnimation<Vector3> ITransitionAnimations.Start(Func<VisualElement, Vector3> fromValueGetter, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged)
		{
			return VisualElement.StartAnimation<Vector3>(ValueAnimation<Vector3>.Create(this, new Func<Vector3, Vector3, float, Vector3>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000F624 File Offset: 0x0000D824
		ValueAnimation<Vector2> ITransitionAnimations.Start(Func<VisualElement, Vector2> fromValueGetter, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged)
		{
			return VisualElement.StartAnimation<Vector2>(ValueAnimation<Vector2>.Create(this, new Func<Vector2, Vector2, float, Vector2>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000F654 File Offset: 0x0000D854
		ValueAnimation<Quaternion> ITransitionAnimations.Start(Func<VisualElement, Quaternion> fromValueGetter, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged)
		{
			return VisualElement.StartAnimation<Quaternion>(ValueAnimation<Quaternion>.Create(this, new Func<Quaternion, Quaternion, float, Quaternion>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000F684 File Offset: 0x0000D884
		private static ValueAnimation<T> StartAnimation<T>(ValueAnimation<T> anim, Func<VisualElement, T> fromValueGetter, T to, int durationMs, Action<VisualElement, T> onValueChanged)
		{
			anim.initialValue = fromValueGetter;
			anim.to = to;
			anim.durationMs = durationMs;
			anim.valueUpdated = onValueChanged;
			anim.Start();
			return anim;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
		private static void AssignStyleValues(VisualElement ve, StyleValues src)
		{
			IStyle style = ve.style;
			foreach (StyleValue styleValue in src.m_StyleValues.m_Values)
			{
				StylePropertyId id = styleValue.id;
				StylePropertyId stylePropertyId = id;
				if (stylePropertyId <= StylePropertyId.FontSize)
				{
					if (stylePropertyId != StylePropertyId.Unknown)
					{
						if (stylePropertyId != StylePropertyId.Color)
						{
							if (stylePropertyId == StylePropertyId.FontSize)
							{
								style.fontSize = styleValue.number;
							}
						}
						else
						{
							style.color = styleValue.color;
						}
					}
				}
				else if (stylePropertyId <= StylePropertyId.UnityBackgroundImageTintColor)
				{
					switch (stylePropertyId)
					{
					case StylePropertyId.BorderBottomWidth:
						style.borderBottomWidth = styleValue.number;
						break;
					case StylePropertyId.BorderLeftWidth:
						style.borderLeftWidth = styleValue.number;
						break;
					case StylePropertyId.BorderRightWidth:
						style.borderRightWidth = styleValue.number;
						break;
					case StylePropertyId.BorderTopWidth:
						style.borderTopWidth = styleValue.number;
						break;
					case StylePropertyId.Bottom:
						style.bottom = styleValue.number;
						break;
					case StylePropertyId.Display:
					case StylePropertyId.FlexBasis:
					case StylePropertyId.FlexDirection:
					case StylePropertyId.FlexWrap:
					case StylePropertyId.JustifyContent:
					case StylePropertyId.MaxHeight:
					case StylePropertyId.MaxWidth:
					case StylePropertyId.MinHeight:
					case StylePropertyId.MinWidth:
					case StylePropertyId.Position:
						break;
					case StylePropertyId.FlexGrow:
						style.flexGrow = styleValue.number;
						break;
					case StylePropertyId.FlexShrink:
						style.flexShrink = styleValue.number;
						break;
					case StylePropertyId.Height:
						style.height = styleValue.number;
						break;
					case StylePropertyId.Left:
						style.left = styleValue.number;
						break;
					case StylePropertyId.MarginBottom:
						style.marginBottom = styleValue.number;
						break;
					case StylePropertyId.MarginLeft:
						style.marginLeft = styleValue.number;
						break;
					case StylePropertyId.MarginRight:
						style.marginRight = styleValue.number;
						break;
					case StylePropertyId.MarginTop:
						style.marginTop = styleValue.number;
						break;
					case StylePropertyId.PaddingBottom:
						style.paddingBottom = styleValue.number;
						break;
					case StylePropertyId.PaddingLeft:
						style.paddingLeft = styleValue.number;
						break;
					case StylePropertyId.PaddingRight:
						style.paddingRight = styleValue.number;
						break;
					case StylePropertyId.PaddingTop:
						style.paddingTop = styleValue.number;
						break;
					case StylePropertyId.Right:
						style.right = styleValue.number;
						break;
					case StylePropertyId.Top:
						style.top = styleValue.number;
						break;
					case StylePropertyId.Width:
						style.width = styleValue.number;
						break;
					default:
						if (stylePropertyId == StylePropertyId.UnityBackgroundImageTintColor)
						{
							style.unityBackgroundImageTintColor = styleValue.color;
						}
						break;
					}
				}
				else if (stylePropertyId != StylePropertyId.BorderColor)
				{
					switch (stylePropertyId)
					{
					case StylePropertyId.BackgroundColor:
						style.backgroundColor = styleValue.color;
						break;
					case StylePropertyId.BorderBottomLeftRadius:
						style.borderBottomLeftRadius = styleValue.number;
						break;
					case StylePropertyId.BorderBottomRightRadius:
						style.borderBottomRightRadius = styleValue.number;
						break;
					case StylePropertyId.BorderTopLeftRadius:
						style.borderTopLeftRadius = styleValue.number;
						break;
					case StylePropertyId.BorderTopRightRadius:
						style.borderTopRightRadius = styleValue.number;
						break;
					case StylePropertyId.Opacity:
						style.opacity = styleValue.number;
						break;
					}
				}
				else
				{
					style.borderLeftColor = styleValue.color;
					style.borderTopColor = styleValue.color;
					style.borderRightColor = styleValue.color;
					style.borderBottomColor = styleValue.color;
				}
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000FB38 File Offset: 0x0000DD38
		private StyleValues ReadCurrentValues(VisualElement ve, StyleValues targetValuesToRead)
		{
			StyleValues result = default(StyleValues);
			IResolvedStyle resolvedStyle = ve.resolvedStyle;
			foreach (StyleValue styleValue in targetValuesToRead.m_StyleValues.m_Values)
			{
				StylePropertyId id = styleValue.id;
				StylePropertyId stylePropertyId = id;
				if (stylePropertyId <= StylePropertyId.Width)
				{
					if (stylePropertyId != StylePropertyId.Unknown)
					{
						if (stylePropertyId != StylePropertyId.Color)
						{
							switch (stylePropertyId)
							{
							case StylePropertyId.BorderBottomWidth:
								result.borderBottomWidth = resolvedStyle.borderBottomWidth;
								break;
							case StylePropertyId.BorderLeftWidth:
								result.borderLeftWidth = resolvedStyle.borderLeftWidth;
								break;
							case StylePropertyId.BorderRightWidth:
								result.borderRightWidth = resolvedStyle.borderRightWidth;
								break;
							case StylePropertyId.BorderTopWidth:
								result.borderTopWidth = resolvedStyle.borderTopWidth;
								break;
							case StylePropertyId.Bottom:
								result.bottom = resolvedStyle.bottom;
								break;
							case StylePropertyId.FlexGrow:
								result.flexGrow = resolvedStyle.flexGrow;
								break;
							case StylePropertyId.FlexShrink:
								result.flexShrink = resolvedStyle.flexShrink;
								break;
							case StylePropertyId.Height:
								result.height = resolvedStyle.height;
								break;
							case StylePropertyId.Left:
								result.left = resolvedStyle.left;
								break;
							case StylePropertyId.MarginBottom:
								result.marginBottom = resolvedStyle.marginBottom;
								break;
							case StylePropertyId.MarginLeft:
								result.marginLeft = resolvedStyle.marginLeft;
								break;
							case StylePropertyId.MarginRight:
								result.marginRight = resolvedStyle.marginRight;
								break;
							case StylePropertyId.MarginTop:
								result.marginTop = resolvedStyle.marginTop;
								break;
							case StylePropertyId.PaddingBottom:
								result.paddingBottom = resolvedStyle.paddingBottom;
								break;
							case StylePropertyId.PaddingLeft:
								result.paddingLeft = resolvedStyle.paddingLeft;
								break;
							case StylePropertyId.PaddingRight:
								result.paddingRight = resolvedStyle.paddingRight;
								break;
							case StylePropertyId.PaddingTop:
								result.paddingTop = resolvedStyle.paddingTop;
								break;
							case StylePropertyId.Right:
								result.right = resolvedStyle.right;
								break;
							case StylePropertyId.Top:
								result.top = resolvedStyle.top;
								break;
							case StylePropertyId.Width:
								result.width = resolvedStyle.width;
								break;
							}
						}
						else
						{
							result.color = resolvedStyle.color;
						}
					}
				}
				else if (stylePropertyId != StylePropertyId.UnityBackgroundImageTintColor)
				{
					if (stylePropertyId != StylePropertyId.BorderColor)
					{
						switch (stylePropertyId)
						{
						case StylePropertyId.BackgroundColor:
							result.backgroundColor = resolvedStyle.backgroundColor;
							break;
						case StylePropertyId.BorderBottomLeftRadius:
							result.borderBottomLeftRadius = resolvedStyle.borderBottomLeftRadius;
							break;
						case StylePropertyId.BorderBottomRightRadius:
							result.borderBottomRightRadius = resolvedStyle.borderBottomRightRadius;
							break;
						case StylePropertyId.BorderTopLeftRadius:
							result.borderTopLeftRadius = resolvedStyle.borderTopLeftRadius;
							break;
						case StylePropertyId.BorderTopRightRadius:
							result.borderTopRightRadius = resolvedStyle.borderTopRightRadius;
							break;
						case StylePropertyId.Opacity:
							result.opacity = resolvedStyle.opacity;
							break;
						}
					}
					else
					{
						result.borderColor = resolvedStyle.borderLeftColor;
					}
				}
				else
				{
					result.unityBackgroundImageTintColor = resolvedStyle.unityBackgroundImageTintColor;
				}
			}
			return result;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000FEE0 File Offset: 0x0000E0E0
		ValueAnimation<StyleValues> ITransitionAnimations.Start(StyleValues to, int durationMs)
		{
			return this.Start((VisualElement e) => this.ReadCurrentValues(e, to), to, durationMs);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000FF20 File Offset: 0x0000E120
		private ValueAnimation<StyleValues> Start(Func<VisualElement, StyleValues> fromValueGetter, StyleValues to, int durationMs)
		{
			return VisualElement.StartAnimation<StyleValues>(ValueAnimation<StyleValues>.Create(this, new Func<StyleValues, StyleValues, float, StyleValues>(Lerp.Interpolate)), fromValueGetter, to, durationMs, new Action<VisualElement, StyleValues>(VisualElement.AssignStyleValues));
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000FF58 File Offset: 0x0000E158
		ValueAnimation<Rect> ITransitionAnimations.Layout(Rect to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => new Rect(e.resolvedStyle.left, e.resolvedStyle.top, e.resolvedStyle.width, e.resolvedStyle.height), to, durationMs, delegate(VisualElement e, Rect c)
			{
				e.style.left = c.x;
				e.style.top = c.y;
				e.style.width = c.width;
				e.style.height = c.height;
			});
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000FFBC File Offset: 0x0000E1BC
		ValueAnimation<Vector2> ITransitionAnimations.TopLeft(Vector2 to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => new Vector2(e.resolvedStyle.left, e.resolvedStyle.top), to, durationMs, delegate(VisualElement e, Vector2 c)
			{
				e.style.left = c.x;
				e.style.top = c.y;
			});
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00010020 File Offset: 0x0000E220
		ValueAnimation<Vector2> ITransitionAnimations.Size(Vector2 to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => e.layout.size, to, durationMs, delegate(VisualElement e, Vector2 c)
			{
				e.style.width = c.x;
				e.style.height = c.y;
			});
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00010084 File Offset: 0x0000E284
		ValueAnimation<float> ITransitionAnimations.Scale(float to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => e.transform.scale.x, to, durationMs, delegate(VisualElement e, float c)
			{
				e.transform.scale = new Vector3(c, c, c);
			});
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000100E8 File Offset: 0x0000E2E8
		ValueAnimation<Vector3> ITransitionAnimations.Position(Vector3 to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => e.transform.position, to, durationMs, delegate(VisualElement e, Vector3 c)
			{
				e.transform.position = c;
			});
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001014C File Offset: 0x0000E34C
		ValueAnimation<Quaternion> ITransitionAnimations.Rotation(Quaternion to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => e.transform.rotation, to, durationMs, delegate(VisualElement e, Quaternion c)
			{
				e.transform.rotation = c;
			});
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x000101B0 File Offset: 0x0000E3B0
		public IExperimentalFeatures experimental
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x000101C4 File Offset: 0x0000E3C4
		ITransitionAnimations IExperimentalFeatures.animation
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x000101D7 File Offset: 0x0000E3D7
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x000101DF File Offset: 0x0000E3DF
		public VisualElement.Hierarchy hierarchy
		{
			[CompilerGenerated]
			get
			{
				return this.<hierarchy>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<hierarchy>k__BackingField = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x000101E8 File Offset: 0x0000E3E8
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x000101F0 File Offset: 0x0000E3F0
		internal bool isRootVisualContainer
		{
			[CompilerGenerated]
			get
			{
				return this.<isRootVisualContainer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<isRootVisualContainer>k__BackingField = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x000101F9 File Offset: 0x0000E3F9
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00010201 File Offset: 0x0000E401
		[Obsolete("VisualElement.cacheAsBitmap is deprecated and has no effect")]
		public bool cacheAsBitmap
		{
			[CompilerGenerated]
			get
			{
				return this.<cacheAsBitmap>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<cacheAsBitmap>k__BackingField = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0001020A File Offset: 0x0000E40A
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x0001021F File Offset: 0x0000E41F
		internal bool disableClipping
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.DisableClipping) == VisualElementFlags.DisableClipping;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.DisableClipping) : (this.m_Flags & ~VisualElementFlags.DisableClipping));
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00010244 File Offset: 0x0000E444
		internal bool ShouldClip()
		{
			return this.computedStyle.overflow != OverflowInternal.Visible && !this.disableClipping;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00010270 File Offset: 0x0000E470
		public VisualElement parent
		{
			get
			{
				return this.m_LogicalParent;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00010288 File Offset: 0x0000E488
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00010290 File Offset: 0x0000E490
		internal BaseVisualElementPanel elementPanel
		{
			[CompilerGenerated]
			get
			{
				return this.<elementPanel>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<elementPanel>k__BackingField = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001029C File Offset: 0x0000E49C
		public IPanel panel
		{
			get
			{
				return this.elementPanel;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x000102B4 File Offset: 0x0000E4B4
		public virtual VisualElement contentContainer
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x000102C7 File Offset: 0x0000E4C7
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x000102CF File Offset: 0x0000E4CF
		public VisualTreeAsset visualTreeAssetSource
		{
			get
			{
				return this.m_VisualTreeAssetSource;
			}
			internal set
			{
				this.m_VisualTreeAssetSource = value;
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000102D8 File Offset: 0x0000E4D8
		public void Add(VisualElement child)
		{
			bool flag = child == null;
			if (!flag)
			{
				VisualElement contentContainer = this.contentContainer;
				bool flag2 = contentContainer == null;
				if (flag2)
				{
					throw new InvalidOperationException("You can't add directly to this VisualElement. Use hierarchy.Add() if you know what you're doing.");
				}
				bool flag3 = contentContainer == this;
				if (flag3)
				{
					this.hierarchy.Add(child);
				}
				else if (contentContainer != null)
				{
					contentContainer.Add(child);
				}
				child.m_LogicalParent = this;
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00010340 File Offset: 0x0000E540
		public void Insert(int index, VisualElement element)
		{
			bool flag = element == null;
			if (!flag)
			{
				bool flag2 = this.contentContainer == this;
				if (flag2)
				{
					this.hierarchy.Insert(index, element);
				}
				else
				{
					VisualElement contentContainer = this.contentContainer;
					if (contentContainer != null)
					{
						contentContainer.Insert(index, element);
					}
				}
				element.m_LogicalParent = this;
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00010398 File Offset: 0x0000E598
		public void Remove(VisualElement element)
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.Remove(element);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.Remove(element);
				}
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000103DC File Offset: 0x0000E5DC
		public void RemoveAt(int index)
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.RemoveAt(index);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.RemoveAt(index);
				}
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00010420 File Offset: 0x0000E620
		public void Clear()
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.Clear();
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.Clear();
				}
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00010464 File Offset: 0x0000E664
		public VisualElement ElementAt(int index)
		{
			return this[index];
		}

		// Token: 0x170000EE RID: 238
		public VisualElement this[int key]
		{
			get
			{
				bool flag = this.contentContainer == this;
				VisualElement result;
				if (flag)
				{
					result = this.hierarchy[key];
				}
				else
				{
					VisualElement contentContainer = this.contentContainer;
					result = ((contentContainer != null) ? contentContainer[key] : null);
				}
				return result;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x000104C8 File Offset: 0x0000E6C8
		public int childCount
		{
			get
			{
				bool flag = this.contentContainer == this;
				int result;
				if (flag)
				{
					result = this.hierarchy.childCount;
				}
				else
				{
					VisualElement contentContainer = this.contentContainer;
					result = ((contentContainer != null) ? contentContainer.childCount : 0);
				}
				return result;
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001050C File Offset: 0x0000E70C
		public int IndexOf(VisualElement element)
		{
			bool flag = this.contentContainer == this;
			int result;
			if (flag)
			{
				result = this.hierarchy.IndexOf(element);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				result = ((contentContainer != null) ? contentContainer.IndexOf(element) : -1);
			}
			return result;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00010554 File Offset: 0x0000E754
		internal VisualElement ElementAtTreePath(List<int> childIndexes)
		{
			VisualElement visualElement = this;
			foreach (int num in childIndexes)
			{
				bool flag = num >= 0 && num < visualElement.hierarchy.childCount;
				if (!flag)
				{
					return null;
				}
				visualElement = visualElement.hierarchy[num];
			}
			return visualElement;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000105E4 File Offset: 0x0000E7E4
		internal bool FindElementInTree(VisualElement element, List<int> outChildIndexes)
		{
			VisualElement visualElement = element;
			for (VisualElement parent = visualElement.hierarchy.parent; parent != null; parent = parent.hierarchy.parent)
			{
				outChildIndexes.Insert(0, parent.hierarchy.IndexOf(visualElement));
				bool flag = parent == this;
				if (flag)
				{
					return true;
				}
				visualElement = parent;
			}
			outChildIndexes.Clear();
			return false;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00010658 File Offset: 0x0000E858
		public IEnumerable<VisualElement> Children()
		{
			bool flag = this.contentContainer == this;
			IEnumerable<VisualElement> result;
			if (flag)
			{
				result = this.hierarchy.Children();
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				result = (((contentContainer != null) ? contentContainer.Children() : null) ?? VisualElement.s_EmptyList);
			}
			return result;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000106A4 File Offset: 0x0000E8A4
		public void Sort(Comparison<VisualElement> comp)
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.Sort(comp);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.Sort(comp);
				}
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000106E8 File Offset: 0x0000E8E8
		public void BringToFront()
		{
			bool flag = this.hierarchy.parent == null;
			if (!flag)
			{
				this.hierarchy.parent.hierarchy.BringToFront(this);
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001072C File Offset: 0x0000E92C
		public void SendToBack()
		{
			bool flag = this.hierarchy.parent == null;
			if (!flag)
			{
				this.hierarchy.parent.hierarchy.SendToBack(this);
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00010770 File Offset: 0x0000E970
		public void PlaceBehind(VisualElement sibling)
		{
			bool flag = sibling == null;
			if (flag)
			{
				throw new ArgumentNullException("sibling");
			}
			bool flag2 = this.hierarchy.parent == null || sibling.hierarchy.parent != this.hierarchy.parent;
			if (flag2)
			{
				throw new ArgumentException("VisualElements are not siblings");
			}
			this.hierarchy.parent.hierarchy.PlaceBehind(this, sibling);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000107F4 File Offset: 0x0000E9F4
		public void PlaceInFront(VisualElement sibling)
		{
			bool flag = sibling == null;
			if (flag)
			{
				throw new ArgumentNullException("sibling");
			}
			bool flag2 = this.hierarchy.parent == null || sibling.hierarchy.parent != this.hierarchy.parent;
			if (flag2)
			{
				throw new ArgumentException("VisualElements are not siblings");
			}
			this.hierarchy.parent.hierarchy.PlaceInFront(this, sibling);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00010878 File Offset: 0x0000EA78
		public void RemoveFromHierarchy()
		{
			bool flag = this.hierarchy.parent != null;
			if (flag)
			{
				this.hierarchy.parent.hierarchy.Remove(this);
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000108BC File Offset: 0x0000EABC
		public T GetFirstOfType<T>() where T : class
		{
			T t = this as T;
			bool flag = t != null;
			T result;
			if (flag)
			{
				result = t;
			}
			else
			{
				result = this.GetFirstAncestorOfType<T>();
			}
			return result;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x000108F4 File Offset: 0x0000EAF4
		public T GetFirstAncestorOfType<T>() where T : class
		{
			for (VisualElement parent = this.hierarchy.parent; parent != null; parent = parent.hierarchy.parent)
			{
				T t = parent as T;
				bool flag = t != null;
				if (flag)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00010960 File Offset: 0x0000EB60
		public bool Contains(VisualElement child)
		{
			while (child != null)
			{
				bool flag = child.hierarchy.parent == this;
				if (flag)
				{
					return true;
				}
				child = child.hierarchy.parent;
			}
			return false;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x000109AC File Offset: 0x0000EBAC
		private void GatherAllChildren(List<VisualElement> elements)
		{
			bool flag = this.m_Children.Count > 0;
			if (flag)
			{
				int i = elements.Count;
				elements.AddRange(this.m_Children);
				while (i < elements.Count)
				{
					VisualElement visualElement = elements[i];
					elements.AddRange(visualElement.m_Children);
					i++;
				}
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00010A0C File Offset: 0x0000EC0C
		public VisualElement FindCommonAncestor(VisualElement other)
		{
			bool flag = other == null;
			if (flag)
			{
				throw new ArgumentNullException("other");
			}
			bool flag2 = this.panel != other.panel;
			VisualElement result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				VisualElement visualElement = this;
				int i = 0;
				while (visualElement != null)
				{
					i++;
					visualElement = visualElement.hierarchy.parent;
				}
				VisualElement visualElement2 = other;
				int j = 0;
				while (visualElement2 != null)
				{
					j++;
					visualElement2 = visualElement2.hierarchy.parent;
				}
				visualElement = this;
				visualElement2 = other;
				while (i > j)
				{
					i--;
					visualElement = visualElement.hierarchy.parent;
				}
				while (j > i)
				{
					j--;
					visualElement2 = visualElement2.hierarchy.parent;
				}
				while (visualElement != visualElement2)
				{
					visualElement = visualElement.hierarchy.parent;
					visualElement2 = visualElement2.hierarchy.parent;
				}
				result = visualElement;
			}
			return result;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00010B1C File Offset: 0x0000ED1C
		internal VisualElement GetRoot()
		{
			bool flag = this.panel != null;
			VisualElement result;
			if (flag)
			{
				result = this.panel.visualTree;
			}
			else
			{
				VisualElement visualElement = this;
				while (visualElement.m_PhysicalParent != null)
				{
					visualElement = visualElement.m_PhysicalParent;
				}
				result = visualElement;
			}
			return result;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00010B68 File Offset: 0x0000ED68
		internal VisualElement GetRootVisualContainer()
		{
			VisualElement result = null;
			for (VisualElement visualElement = this; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				bool isRootVisualContainer = visualElement.isRootVisualContainer;
				if (isRootVisualContainer)
				{
					result = visualElement;
				}
			}
			return result;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00010BAC File Offset: 0x0000EDAC
		internal VisualElement GetNextElementDepthFirst()
		{
			bool flag = this.m_Children.Count > 0;
			VisualElement result;
			if (flag)
			{
				result = this.m_Children[0];
			}
			else
			{
				VisualElement physicalParent = this.m_PhysicalParent;
				VisualElement visualElement = this;
				while (physicalParent != null)
				{
					int i;
					for (i = 0; i < physicalParent.m_Children.Count; i++)
					{
						bool flag2 = physicalParent.m_Children[i] == visualElement;
						if (flag2)
						{
							break;
						}
					}
					bool flag3 = i < physicalParent.m_Children.Count - 1;
					if (flag3)
					{
						return physicalParent.m_Children[i + 1];
					}
					visualElement = physicalParent;
					physicalParent = physicalParent.m_PhysicalParent;
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00010C6C File Offset: 0x0000EE6C
		internal VisualElement GetPreviousElementDepthFirst()
		{
			bool flag = this.m_PhysicalParent != null;
			VisualElement result;
			if (flag)
			{
				int i;
				for (i = 0; i < this.m_PhysicalParent.m_Children.Count; i++)
				{
					bool flag2 = this.m_PhysicalParent.m_Children[i] == this;
					if (flag2)
					{
						break;
					}
				}
				bool flag3 = i > 0;
				if (flag3)
				{
					VisualElement visualElement = this.m_PhysicalParent.m_Children[i - 1];
					while (visualElement.m_Children.Count > 0)
					{
						visualElement = visualElement.m_Children[visualElement.m_Children.Count - 1];
					}
					result = visualElement;
				}
				else
				{
					result = this.m_PhysicalParent;
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00010D34 File Offset: 0x0000EF34
		internal VisualElement RetargetElement(VisualElement retargetAgainst)
		{
			bool flag = retargetAgainst == null;
			VisualElement result;
			if (flag)
			{
				result = this;
			}
			else
			{
				VisualElement visualElement = retargetAgainst.m_PhysicalParent ?? retargetAgainst;
				while (visualElement.m_PhysicalParent != null && !visualElement.isCompositeRoot)
				{
					visualElement = visualElement.m_PhysicalParent;
				}
				VisualElement result2 = this;
				VisualElement physicalParent = this.m_PhysicalParent;
				while (physicalParent != null)
				{
					physicalParent = physicalParent.m_PhysicalParent;
					bool flag2 = physicalParent == visualElement;
					if (flag2)
					{
						return result2;
					}
					bool flag3 = physicalParent != null && physicalParent.isCompositeRoot;
					if (flag3)
					{
						result2 = physicalParent;
					}
				}
				result = this;
			}
			return result;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00010DCC File Offset: 0x0000EFCC
		private Vector3 positionWithLayout
		{
			get
			{
				return this.ResolveTranslate() + this.layout.min;
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00010DFC File Offset: 0x0000EFFC
		internal void GetPivotedMatrixWithLayout(out Matrix4x4 result)
		{
			Vector3 vector = this.ResolveTransformOrigin();
			result = Matrix4x4.TRS(this.positionWithLayout + vector, this.ResolveRotation(), this.ResolveScale());
			VisualElement.TranslateMatrix34InPlace(ref result, -vector);
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00010E44 File Offset: 0x0000F044
		internal bool hasDefaultRotationAndScale
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.computedStyle.rotate.angle.value == 0f && this.computedStyle.scale.value == Vector3.one;
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00010E98 File Offset: 0x0000F098
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float Min(float a, float b, float c, float d)
		{
			return Mathf.Min(Mathf.Min(a, b), Mathf.Min(c, d));
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00010EC0 File Offset: 0x0000F0C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float Max(float a, float b, float c, float d)
		{
			return Mathf.Max(Mathf.Max(a, b), Mathf.Max(c, d));
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00010EE8 File Offset: 0x0000F0E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void TransformAlignedRectToParentSpace(ref Rect rect)
		{
			bool hasDefaultRotationAndScale = this.hasDefaultRotationAndScale;
			if (hasDefaultRotationAndScale)
			{
				rect.position += this.positionWithLayout;
			}
			else
			{
				Matrix4x4 matrix4x;
				this.GetPivotedMatrixWithLayout(out matrix4x);
				rect = VisualElement.CalculateConservativeRect(ref matrix4x, rect);
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00010F40 File Offset: 0x0000F140
		internal static Rect CalculateConservativeRect(ref Matrix4x4 matrix, Rect rect)
		{
			bool flag = float.IsNaN(rect.height) | float.IsNaN(rect.width) | float.IsNaN(rect.x) | float.IsNaN(rect.y);
			Rect result;
			if (flag)
			{
				rect = new Rect(VisualElement.MultiplyMatrix44Point2(ref matrix, rect.position), VisualElement.MultiplyVector2(ref matrix, rect.size));
				VisualElement.OrderMinMaxRect(ref rect);
				result = rect;
			}
			else
			{
				Vector2 v = new Vector2(rect.xMin, rect.yMin);
				Vector2 v2 = new Vector2(rect.xMax, rect.yMax);
				Vector2 v3 = new Vector2(rect.xMax, rect.yMin);
				Vector2 v4 = new Vector2(rect.xMin, rect.yMax);
				Vector3 vector = matrix.MultiplyPoint3x4(v);
				Vector3 vector2 = matrix.MultiplyPoint3x4(v2);
				Vector3 vector3 = matrix.MultiplyPoint3x4(v3);
				Vector3 vector4 = matrix.MultiplyPoint3x4(v4);
				Vector2 vector5 = new Vector2(VisualElement.Min(vector.x, vector2.x, vector3.x, vector4.x), VisualElement.Min(vector.y, vector2.y, vector3.y, vector4.y));
				Vector2 vector6 = new Vector2(VisualElement.Max(vector.x, vector2.x, vector3.x, vector4.x), VisualElement.Max(vector.y, vector2.y, vector3.y, vector4.y));
				result = new Rect(vector5.x, vector5.y, vector6.x - vector5.x, vector6.y - vector5.y);
			}
			return result;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001110E File Offset: 0x0000F30E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void TransformAlignedRect(ref Matrix4x4 matrix, ref Rect rect)
		{
			rect = VisualElement.CalculateConservativeRect(ref matrix, rect);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00011124 File Offset: 0x0000F324
		internal static void OrderMinMaxRect(ref Rect rect)
		{
			bool flag = rect.width < 0f;
			if (flag)
			{
				rect.x += rect.width;
				rect.width = -rect.width;
			}
			bool flag2 = rect.height < 0f;
			if (flag2)
			{
				rect.y += rect.height;
				rect.height = -rect.height;
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001119C File Offset: 0x0000F39C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Vector2 MultiplyMatrix44Point2(ref Matrix4x4 lhs, Vector2 point)
		{
			Vector2 result;
			result.x = lhs.m00 * point.x + lhs.m01 * point.y + lhs.m03;
			result.y = lhs.m10 * point.x + lhs.m11 * point.y + lhs.m13;
			return result;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00011204 File Offset: 0x0000F404
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Vector2 MultiplyVector2(ref Matrix4x4 lhs, Vector2 vector)
		{
			Vector2 result;
			result.x = lhs.m00 * vector.x + lhs.m01 * vector.y;
			result.y = lhs.m10 * vector.x + lhs.m11 * vector.y;
			return result;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001125C File Offset: 0x0000F45C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Rect MultiplyMatrix44Rect2(ref Matrix4x4 lhs, Rect r)
		{
			r.position = VisualElement.MultiplyMatrix44Point2(ref lhs, r.position);
			r.size = VisualElement.MultiplyVector2(ref lhs, r.size);
			return r;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001129C File Offset: 0x0000F49C
		internal static void MultiplyMatrix34(ref Matrix4x4 lhs, ref Matrix4x4 rhs, out Matrix4x4 res)
		{
			res.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20;
			res.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21;
			res.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22;
			res.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03;
			res.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20;
			res.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21;
			res.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22;
			res.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13;
			res.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20;
			res.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21;
			res.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22;
			res.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23;
			res.m30 = 0f;
			res.m31 = 0f;
			res.m32 = 0f;
			res.m33 = 1f;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001151F File Offset: 0x0000F71F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void TranslateMatrix34(ref Matrix4x4 lhs, Vector3 rhs, out Matrix4x4 res)
		{
			res = lhs;
			VisualElement.TranslateMatrix34InPlace(ref res, rhs);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00011538 File Offset: 0x0000F738
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void TranslateMatrix34InPlace(ref Matrix4x4 lhs, Vector3 rhs)
		{
			lhs.m03 += lhs.m00 * rhs.x + lhs.m01 * rhs.y + lhs.m02 * rhs.z;
			lhs.m13 += lhs.m10 * rhs.x + lhs.m11 * rhs.y + lhs.m12 * rhs.z;
			lhs.m23 += lhs.m20 * rhs.x + lhs.m21 * rhs.y + lhs.m22 * rhs.z;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x000115E0 File Offset: 0x0000F7E0
		public IVisualElementScheduler schedule
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000115F4 File Offset: 0x0000F7F4
		IVisualElementScheduledItem IVisualElementScheduler.Execute(Action<TimerState> timerUpdateEvent)
		{
			VisualElement.TimerStateScheduledItem timerStateScheduledItem = new VisualElement.TimerStateScheduledItem(this, timerUpdateEvent)
			{
				timerUpdateStopCondition = ScheduledItem.OnceCondition
			};
			timerStateScheduledItem.Resume();
			return timerStateScheduledItem;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00011624 File Offset: 0x0000F824
		IVisualElementScheduledItem IVisualElementScheduler.Execute(Action updateEvent)
		{
			VisualElement.SimpleScheduledItem simpleScheduledItem = new VisualElement.SimpleScheduledItem(this, updateEvent)
			{
				timerUpdateStopCondition = ScheduledItem.OnceCondition
			};
			simpleScheduledItem.Resume();
			return simpleScheduledItem;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00011654 File Offset: 0x0000F854
		public IStyle style
		{
			get
			{
				bool flag = this.inlineStyleAccess == null;
				if (flag)
				{
					this.inlineStyleAccess = new InlineStyleAccess(this);
				}
				return this.inlineStyleAccess;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00011688 File Offset: 0x0000F888
		public ICustomStyle customStyle
		{
			get
			{
				VisualElement.s_CustomStyleAccess.SetContext(this.computedStyle.customProperties, this.computedStyle.dpiScaling);
				return VisualElement.s_CustomStyleAccess;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x000116C0 File Offset: 0x0000F8C0
		public VisualElementStyleSheetSet styleSheets
		{
			get
			{
				return new VisualElementStyleSheetSet(this);
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000116C8 File Offset: 0x0000F8C8
		internal void AddStyleSheetPath(string sheetPath)
		{
			StyleSheet styleSheet = Panel.LoadResource(sheetPath, typeof(StyleSheet), this.scaledPixelsPerPoint) as StyleSheet;
			bool flag = styleSheet == null;
			if (flag)
			{
				bool flag2 = !VisualElement.s_InternalStyleSheetPath.IsMatch(sheetPath);
				if (flag2)
				{
					Debug.LogWarning(string.Format("Style sheet not found for path \"{0}\"", sheetPath));
				}
			}
			else
			{
				this.styleSheets.Add(styleSheet);
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00011738 File Offset: 0x0000F938
		internal bool HasStyleSheetPath(string sheetPath)
		{
			StyleSheet styleSheet = Panel.LoadResource(sheetPath, typeof(StyleSheet), this.scaledPixelsPerPoint) as StyleSheet;
			bool flag = styleSheet == null;
			bool result;
			if (flag)
			{
				Debug.LogWarning(string.Format("Style sheet not found for path \"{0}\"", sheetPath));
				result = false;
			}
			else
			{
				result = this.styleSheets.Contains(styleSheet);
			}
			return result;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00011798 File Offset: 0x0000F998
		internal void RemoveStyleSheetPath(string sheetPath)
		{
			StyleSheet styleSheet = Panel.LoadResource(sheetPath, typeof(StyleSheet), this.scaledPixelsPerPoint) as StyleSheet;
			bool flag = styleSheet == null;
			if (flag)
			{
				Debug.LogWarning(string.Format("Style sheet not found for path \"{0}\"", sheetPath));
			}
			else
			{
				this.styleSheets.Remove(styleSheet);
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000117F4 File Offset: 0x0000F9F4
		private StyleFloat ResolveLengthValue(Length length, bool isRow)
		{
			bool flag = length.IsAuto();
			StyleFloat result;
			if (flag)
			{
				result = new StyleFloat(StyleKeyword.Auto);
			}
			else
			{
				bool flag2 = length.IsNone();
				if (flag2)
				{
					result = new StyleFloat(StyleKeyword.None);
				}
				else
				{
					bool flag3 = length.unit != LengthUnit.Percent;
					if (flag3)
					{
						result = new StyleFloat(length.value);
					}
					else
					{
						VisualElement parent = this.hierarchy.parent;
						bool flag4 = parent == null;
						if (flag4)
						{
							result = 0f;
						}
						else
						{
							float num = isRow ? parent.resolvedStyle.width : parent.resolvedStyle.height;
							result = length.value * num / 100f;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000118AC File Offset: 0x0000FAAC
		private Vector3 ResolveTranslate()
		{
			Translate translate = this.computedStyle.translate;
			Length x = translate.x;
			bool flag = x.unit == LengthUnit.Percent;
			float num;
			if (flag)
			{
				float width = this.resolvedStyle.width;
				num = (float.IsNaN(width) ? 0f : (width * x.value / 100f));
			}
			else
			{
				num = x.value;
				num = (float.IsNaN(num) ? 0f : num);
			}
			Length y = translate.y;
			bool flag2 = y.unit == LengthUnit.Percent;
			float num2;
			if (flag2)
			{
				float height = this.resolvedStyle.height;
				num2 = (float.IsNaN(height) ? 0f : (height * y.value / 100f));
			}
			else
			{
				num2 = y.value;
				num2 = (float.IsNaN(num2) ? 0f : num2);
			}
			float num3 = translate.z;
			num3 = (float.IsNaN(num3) ? 0f : num3);
			return new Vector3(num, num2, num3);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000119C0 File Offset: 0x0000FBC0
		private Vector3 ResolveTransformOrigin()
		{
			TransformOrigin transformOrigin = this.computedStyle.transformOrigin;
			Length x = transformOrigin.x;
			bool flag = x.IsNone();
			float x2;
			if (flag)
			{
				float width = this.resolvedStyle.width;
				x2 = (float.IsNaN(width) ? 0f : (width / 2f));
			}
			else
			{
				bool flag2 = x.unit == LengthUnit.Percent;
				if (flag2)
				{
					float width2 = this.resolvedStyle.width;
					x2 = (float.IsNaN(width2) ? 0f : (width2 * x.value / 100f));
				}
				else
				{
					x2 = x.value;
				}
			}
			Length y = transformOrigin.y;
			bool flag3 = y.IsNone();
			float y2;
			if (flag3)
			{
				float height = this.resolvedStyle.height;
				y2 = (float.IsNaN(height) ? 0f : (height / 2f));
			}
			else
			{
				bool flag4 = y.unit == LengthUnit.Percent;
				if (flag4)
				{
					float height2 = this.resolvedStyle.height;
					y2 = (float.IsNaN(height2) ? 0f : (height2 * y.value / 100f));
				}
				else
				{
					y2 = y.value;
				}
			}
			float z = transformOrigin.z;
			return new Vector3(x2, y2, z);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00011B18 File Offset: 0x0000FD18
		private Quaternion ResolveRotation()
		{
			Rotate rotate = this.computedStyle.rotate;
			Vector3 axis = rotate.axis;
			bool flag = float.IsNaN(rotate.angle.value) || float.IsNaN(axis.x) || float.IsNaN(axis.y) || float.IsNaN(axis.z);
			if (flag)
			{
				rotate = Rotate.Initial();
			}
			return rotate.ToQuaternion();
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00011B90 File Offset: 0x0000FD90
		private Vector3 ResolveScale()
		{
			Vector3 value = this.computedStyle.scale.value;
			return (float.IsNaN(value.x) || float.IsNaN(value.y) || float.IsNaN(value.z)) ? Vector3.one : value;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00011BE8 File Offset: 0x0000FDE8
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00011C18 File Offset: 0x0000FE18
		public string tooltip
		{
			get
			{
				string text = this.GetProperty(VisualElement.tooltipPropertyKey) as string;
				return text ?? string.Empty;
			}
			set
			{
				bool flag = !this.HasProperty(VisualElement.tooltipPropertyKey);
				if (flag)
				{
					base.RegisterCallback<TooltipEvent>(new EventCallback<TooltipEvent>(this.SetTooltip), TrickleDown.NoTrickleDown);
				}
				this.SetProperty(VisualElement.tooltipPropertyKey, value);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00011C5C File Offset: 0x0000FE5C
		private VisualElement.TypeData typeData
		{
			get
			{
				bool flag = this.m_TypeData == null;
				if (flag)
				{
					Type type = base.GetType();
					bool flag2 = !VisualElement.s_TypeData.TryGetValue(type, out this.m_TypeData);
					if (flag2)
					{
						this.m_TypeData = new VisualElement.TypeData(type);
						VisualElement.s_TypeData.Add(type, this.m_TypeData);
					}
				}
				return this.m_TypeData;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		public IResolvedStyle resolvedStyle
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00011CC2 File Offset: 0x0000FEC2
		Align IResolvedStyle.alignContent
		{
			get
			{
				return this.computedStyle.alignContent;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00011CCF File Offset: 0x0000FECF
		Align IResolvedStyle.alignItems
		{
			get
			{
				return this.computedStyle.alignItems;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00011CDC File Offset: 0x0000FEDC
		Align IResolvedStyle.alignSelf
		{
			get
			{
				return this.computedStyle.alignSelf;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00011CE9 File Offset: 0x0000FEE9
		Color IResolvedStyle.backgroundColor
		{
			get
			{
				return this.computedStyle.backgroundColor;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x00011CF6 File Offset: 0x0000FEF6
		Background IResolvedStyle.backgroundImage
		{
			get
			{
				return this.computedStyle.backgroundImage;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00011D03 File Offset: 0x0000FF03
		Color IResolvedStyle.borderBottomColor
		{
			get
			{
				return this.computedStyle.borderBottomColor;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00011D10 File Offset: 0x0000FF10
		float IResolvedStyle.borderBottomLeftRadius
		{
			get
			{
				return this.computedStyle.borderBottomLeftRadius.value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00011D30 File Offset: 0x0000FF30
		float IResolvedStyle.borderBottomRightRadius
		{
			get
			{
				return this.computedStyle.borderBottomRightRadius.value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00011D50 File Offset: 0x0000FF50
		float IResolvedStyle.borderBottomWidth
		{
			get
			{
				return this.yogaNode.LayoutBorderBottom;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00011D5D File Offset: 0x0000FF5D
		Color IResolvedStyle.borderLeftColor
		{
			get
			{
				return this.computedStyle.borderLeftColor;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00011D6A File Offset: 0x0000FF6A
		float IResolvedStyle.borderLeftWidth
		{
			get
			{
				return this.yogaNode.LayoutBorderLeft;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00011D77 File Offset: 0x0000FF77
		Color IResolvedStyle.borderRightColor
		{
			get
			{
				return this.computedStyle.borderRightColor;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00011D84 File Offset: 0x0000FF84
		float IResolvedStyle.borderRightWidth
		{
			get
			{
				return this.yogaNode.LayoutBorderRight;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00011D91 File Offset: 0x0000FF91
		Color IResolvedStyle.borderTopColor
		{
			get
			{
				return this.computedStyle.borderTopColor;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00011DA0 File Offset: 0x0000FFA0
		float IResolvedStyle.borderTopLeftRadius
		{
			get
			{
				return this.computedStyle.borderTopLeftRadius.value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00011DC0 File Offset: 0x0000FFC0
		float IResolvedStyle.borderTopRightRadius
		{
			get
			{
				return this.computedStyle.borderTopRightRadius.value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00011DE0 File Offset: 0x0000FFE0
		float IResolvedStyle.borderTopWidth
		{
			get
			{
				return this.yogaNode.LayoutBorderTop;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00011DED File Offset: 0x0000FFED
		float IResolvedStyle.bottom
		{
			get
			{
				return this.yogaNode.LayoutBottom;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00011DFA File Offset: 0x0000FFFA
		Color IResolvedStyle.color
		{
			get
			{
				return this.computedStyle.color;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00011E07 File Offset: 0x00010007
		DisplayStyle IResolvedStyle.display
		{
			get
			{
				return this.computedStyle.display;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00011E14 File Offset: 0x00010014
		StyleFloat IResolvedStyle.flexBasis
		{
			get
			{
				return new StyleFloat(this.yogaNode.ComputedFlexBasis);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00011E26 File Offset: 0x00010026
		FlexDirection IResolvedStyle.flexDirection
		{
			get
			{
				return this.computedStyle.flexDirection;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00011E33 File Offset: 0x00010033
		float IResolvedStyle.flexGrow
		{
			get
			{
				return this.computedStyle.flexGrow;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00011E40 File Offset: 0x00010040
		float IResolvedStyle.flexShrink
		{
			get
			{
				return this.computedStyle.flexShrink;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00011E4D File Offset: 0x0001004D
		Wrap IResolvedStyle.flexWrap
		{
			get
			{
				return this.computedStyle.flexWrap;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00011E5C File Offset: 0x0001005C
		float IResolvedStyle.fontSize
		{
			get
			{
				return this.computedStyle.fontSize.value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00011E7C File Offset: 0x0001007C
		float IResolvedStyle.height
		{
			get
			{
				return this.yogaNode.LayoutHeight;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00011E89 File Offset: 0x00010089
		Justify IResolvedStyle.justifyContent
		{
			get
			{
				return this.computedStyle.justifyContent;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00011E96 File Offset: 0x00010096
		float IResolvedStyle.left
		{
			get
			{
				return this.yogaNode.LayoutX;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00011EA4 File Offset: 0x000100A4
		float IResolvedStyle.letterSpacing
		{
			get
			{
				return this.computedStyle.letterSpacing.value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00011EC4 File Offset: 0x000100C4
		float IResolvedStyle.marginBottom
		{
			get
			{
				return this.yogaNode.LayoutMarginBottom;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00011ED1 File Offset: 0x000100D1
		float IResolvedStyle.marginLeft
		{
			get
			{
				return this.yogaNode.LayoutMarginLeft;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00011EDE File Offset: 0x000100DE
		float IResolvedStyle.marginRight
		{
			get
			{
				return this.yogaNode.LayoutMarginRight;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00011EEB File Offset: 0x000100EB
		float IResolvedStyle.marginTop
		{
			get
			{
				return this.yogaNode.LayoutMarginTop;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00011EF8 File Offset: 0x000100F8
		StyleFloat IResolvedStyle.maxHeight
		{
			get
			{
				return this.ResolveLengthValue(this.computedStyle.maxHeight, false);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00011F0C File Offset: 0x0001010C
		StyleFloat IResolvedStyle.maxWidth
		{
			get
			{
				return this.ResolveLengthValue(this.computedStyle.maxWidth, true);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00011F20 File Offset: 0x00010120
		StyleFloat IResolvedStyle.minHeight
		{
			get
			{
				return this.ResolveLengthValue(this.computedStyle.minHeight, false);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00011F34 File Offset: 0x00010134
		StyleFloat IResolvedStyle.minWidth
		{
			get
			{
				return this.ResolveLengthValue(this.computedStyle.minWidth, true);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00011F48 File Offset: 0x00010148
		float IResolvedStyle.opacity
		{
			get
			{
				return this.computedStyle.opacity;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00011F55 File Offset: 0x00010155
		float IResolvedStyle.paddingBottom
		{
			get
			{
				return this.yogaNode.LayoutPaddingBottom;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00011F62 File Offset: 0x00010162
		float IResolvedStyle.paddingLeft
		{
			get
			{
				return this.yogaNode.LayoutPaddingLeft;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00011F6F File Offset: 0x0001016F
		float IResolvedStyle.paddingRight
		{
			get
			{
				return this.yogaNode.LayoutPaddingRight;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00011F7C File Offset: 0x0001017C
		float IResolvedStyle.paddingTop
		{
			get
			{
				return this.yogaNode.LayoutPaddingTop;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00011F89 File Offset: 0x00010189
		Position IResolvedStyle.position
		{
			get
			{
				return this.computedStyle.position;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00011F96 File Offset: 0x00010196
		float IResolvedStyle.right
		{
			get
			{
				return this.yogaNode.LayoutRight;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00011FA3 File Offset: 0x000101A3
		Rotate IResolvedStyle.rotate
		{
			get
			{
				return this.computedStyle.rotate;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00011FB0 File Offset: 0x000101B0
		Scale IResolvedStyle.scale
		{
			get
			{
				return this.computedStyle.scale;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00011FBD File Offset: 0x000101BD
		TextOverflow IResolvedStyle.textOverflow
		{
			get
			{
				return this.computedStyle.textOverflow;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00011FCA File Offset: 0x000101CA
		float IResolvedStyle.top
		{
			get
			{
				return this.yogaNode.LayoutY;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00011FD7 File Offset: 0x000101D7
		Vector3 IResolvedStyle.transformOrigin
		{
			get
			{
				return this.ResolveTransformOrigin();
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00011FDF File Offset: 0x000101DF
		IEnumerable<TimeValue> IResolvedStyle.transitionDelay
		{
			get
			{
				return this.computedStyle.transitionDelay;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00011FEC File Offset: 0x000101EC
		IEnumerable<TimeValue> IResolvedStyle.transitionDuration
		{
			get
			{
				return this.computedStyle.transitionDuration;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00011FF9 File Offset: 0x000101F9
		IEnumerable<StylePropertyName> IResolvedStyle.transitionProperty
		{
			get
			{
				return this.computedStyle.transitionProperty;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00012006 File Offset: 0x00010206
		IEnumerable<EasingFunction> IResolvedStyle.transitionTimingFunction
		{
			get
			{
				return this.computedStyle.transitionTimingFunction;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00012013 File Offset: 0x00010213
		Vector3 IResolvedStyle.translate
		{
			get
			{
				return this.ResolveTranslate();
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0001201B File Offset: 0x0001021B
		Color IResolvedStyle.unityBackgroundImageTintColor
		{
			get
			{
				return this.computedStyle.unityBackgroundImageTintColor;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00012028 File Offset: 0x00010228
		ScaleMode IResolvedStyle.unityBackgroundScaleMode
		{
			get
			{
				return this.computedStyle.unityBackgroundScaleMode;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00012035 File Offset: 0x00010235
		Font IResolvedStyle.unityFont
		{
			get
			{
				return this.computedStyle.unityFont;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00012042 File Offset: 0x00010242
		FontDefinition IResolvedStyle.unityFontDefinition
		{
			get
			{
				return this.computedStyle.unityFontDefinition;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0001204F File Offset: 0x0001024F
		FontStyle IResolvedStyle.unityFontStyleAndWeight
		{
			get
			{
				return this.computedStyle.unityFontStyleAndWeight;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0001205C File Offset: 0x0001025C
		float IResolvedStyle.unityParagraphSpacing
		{
			get
			{
				return this.computedStyle.unityParagraphSpacing.value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0001207C File Offset: 0x0001027C
		int IResolvedStyle.unitySliceBottom
		{
			get
			{
				return this.computedStyle.unitySliceBottom;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00012089 File Offset: 0x00010289
		int IResolvedStyle.unitySliceLeft
		{
			get
			{
				return this.computedStyle.unitySliceLeft;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00012096 File Offset: 0x00010296
		int IResolvedStyle.unitySliceRight
		{
			get
			{
				return this.computedStyle.unitySliceRight;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x000120A3 File Offset: 0x000102A3
		int IResolvedStyle.unitySliceTop
		{
			get
			{
				return this.computedStyle.unitySliceTop;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x000120B0 File Offset: 0x000102B0
		TextAnchor IResolvedStyle.unityTextAlign
		{
			get
			{
				return this.computedStyle.unityTextAlign;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000120BD File Offset: 0x000102BD
		Color IResolvedStyle.unityTextOutlineColor
		{
			get
			{
				return this.computedStyle.unityTextOutlineColor;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x000120CA File Offset: 0x000102CA
		float IResolvedStyle.unityTextOutlineWidth
		{
			get
			{
				return this.computedStyle.unityTextOutlineWidth;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x000120D7 File Offset: 0x000102D7
		TextOverflowPosition IResolvedStyle.unityTextOverflowPosition
		{
			get
			{
				return this.computedStyle.unityTextOverflowPosition;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x000120E4 File Offset: 0x000102E4
		Visibility IResolvedStyle.visibility
		{
			get
			{
				return this.computedStyle.visibility;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x000120F1 File Offset: 0x000102F1
		WhiteSpace IResolvedStyle.whiteSpace
		{
			get
			{
				return this.computedStyle.whiteSpace;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x000120FE File Offset: 0x000102FE
		float IResolvedStyle.width
		{
			get
			{
				return this.yogaNode.LayoutWidth;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0001210C File Offset: 0x0001030C
		float IResolvedStyle.wordSpacing
		{
			get
			{
				return this.computedStyle.wordSpacing.value;
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001212C File Offset: 0x0001032C
		// Note: this type is marked as 'beforefieldinit'.
		static VisualElement()
		{
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000121B8 File Offset: 0x000103B8
		[CompilerGenerated]
		private YogaSize <AssignMeasureFunction>b__254_0(YogaNode node, float f, YogaMeasureMode mode, float f1, YogaMeasureMode heightMode)
		{
			return this.Measure(node, f, mode, f1, heightMode);
		}

		// Token: 0x040001AA RID: 426
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <UnityEngine.UIElements.IStylePropertyAnimations.runningAnimationCount>k__BackingField;

		// Token: 0x040001AB RID: 427
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <UnityEngine.UIElements.IStylePropertyAnimations.completedAnimationCount>k__BackingField;

		// Token: 0x040001AC RID: 428
		private static uint s_NextId;

		// Token: 0x040001AD RID: 429
		private static List<string> s_EmptyClassList = new List<string>(0);

		// Token: 0x040001AE RID: 430
		internal static readonly PropertyName userDataPropertyKey = new PropertyName("--unity-user-data");

		// Token: 0x040001AF RID: 431
		public static readonly string disabledUssClassName = "unity-disabled";

		// Token: 0x040001B0 RID: 432
		private string m_Name;

		// Token: 0x040001B1 RID: 433
		private List<string> m_ClassList;

		// Token: 0x040001B2 RID: 434
		private List<KeyValuePair<PropertyName, object>> m_PropertyBag;

		// Token: 0x040001B3 RID: 435
		private VisualElementFlags m_Flags;

		// Token: 0x040001B4 RID: 436
		private string m_ViewDataKey;

		// Token: 0x040001B5 RID: 437
		private RenderHints m_RenderHints;

		// Token: 0x040001B6 RID: 438
		internal Rect lastLayout;

		// Token: 0x040001B7 RID: 439
		internal Rect lastPseudoPadding;

		// Token: 0x040001B8 RID: 440
		internal RenderChainVEData renderChainData;

		// Token: 0x040001B9 RID: 441
		private Rect m_Layout;

		// Token: 0x040001BA RID: 442
		private Rect m_BoundingBox;

		// Token: 0x040001BB RID: 443
		private Rect m_WorldBoundingBox;

		// Token: 0x040001BC RID: 444
		private Matrix4x4 m_WorldTransformCache = Matrix4x4.identity;

		// Token: 0x040001BD RID: 445
		private Matrix4x4 m_WorldTransformInverseCache = Matrix4x4.identity;

		// Token: 0x040001BE RID: 446
		private Rect m_WorldClip = Rect.zero;

		// Token: 0x040001BF RID: 447
		private Rect m_WorldClipMinusGroup = Rect.zero;

		// Token: 0x040001C0 RID: 448
		private bool m_WorldClipIsInfinite = false;

		// Token: 0x040001C1 RID: 449
		internal static readonly Rect s_InfiniteRect = new Rect(-10000f, -10000f, 40000f, 40000f);

		// Token: 0x040001C2 RID: 450
		internal PseudoStates triggerPseudoMask;

		// Token: 0x040001C3 RID: 451
		internal PseudoStates dependencyPseudoMask;

		// Token: 0x040001C4 RID: 452
		private PseudoStates m_PseudoStates;

		// Token: 0x040001C5 RID: 453
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <containedPointerIds>k__BackingField;

		// Token: 0x040001C6 RID: 454
		private PickingMode m_PickingMode;

		// Token: 0x040001C7 RID: 455
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private YogaNode <yogaNode>k__BackingField;

		// Token: 0x040001C8 RID: 456
		internal ComputedStyle m_Style = InitialStyle.Acquire();

		// Token: 0x040001C9 RID: 457
		internal StyleVariableContext variableContext = StyleVariableContext.none;

		// Token: 0x040001CA RID: 458
		internal int inheritedStylesHash = 0;

		// Token: 0x040001CB RID: 459
		internal readonly uint controlid;

		// Token: 0x040001CC RID: 460
		internal int imguiContainerDescendantCount = 0;

		// Token: 0x040001CD RID: 461
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <enabledSelf>k__BackingField;

		// Token: 0x040001CE RID: 462
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<MeshGenerationContext> <generateVisualContent>k__BackingField;

		// Token: 0x040001CF RID: 463
		private ProfilerMarker k_GenerateVisualContentMarker = new ProfilerMarker("GenerateVisualContent");

		// Token: 0x040001D0 RID: 464
		private VisualElement.RenderTargetMode m_SubRenderTargetMode = VisualElement.RenderTargetMode.None;

		// Token: 0x040001D1 RID: 465
		private static Material s_runtimeMaterial;

		// Token: 0x040001D2 RID: 466
		private Material m_defaultMaterial;

		// Token: 0x040001D3 RID: 467
		private List<IValueAnimationUpdate> m_RunningAnimations;

		// Token: 0x040001D4 RID: 468
		internal const string k_RootVisualContainerName = "rootVisualContainer";

		// Token: 0x040001D5 RID: 469
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VisualElement.Hierarchy <hierarchy>k__BackingField;

		// Token: 0x040001D6 RID: 470
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <isRootVisualContainer>k__BackingField;

		// Token: 0x040001D7 RID: 471
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <cacheAsBitmap>k__BackingField;

		// Token: 0x040001D8 RID: 472
		private VisualElement m_PhysicalParent;

		// Token: 0x040001D9 RID: 473
		private VisualElement m_LogicalParent;

		// Token: 0x040001DA RID: 474
		private static readonly List<VisualElement> s_EmptyList = new List<VisualElement>();

		// Token: 0x040001DB RID: 475
		private List<VisualElement> m_Children;

		// Token: 0x040001DC RID: 476
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private BaseVisualElementPanel <elementPanel>k__BackingField;

		// Token: 0x040001DD RID: 477
		private VisualTreeAsset m_VisualTreeAssetSource = null;

		// Token: 0x040001DE RID: 478
		internal static VisualElement.CustomStyleAccess s_CustomStyleAccess = new VisualElement.CustomStyleAccess();

		// Token: 0x040001DF RID: 479
		internal InlineStyleAccess inlineStyleAccess;

		// Token: 0x040001E0 RID: 480
		internal List<StyleSheet> styleSheetList;

		// Token: 0x040001E1 RID: 481
		private static readonly Regex s_InternalStyleSheetPath = new Regex("^instanceId:[-0-9]+$", RegexOptions.Compiled);

		// Token: 0x040001E2 RID: 482
		internal static readonly PropertyName tooltipPropertyKey = new PropertyName("--unity-tooltip");

		// Token: 0x040001E3 RID: 483
		private static readonly Dictionary<Type, VisualElement.TypeData> s_TypeData = new Dictionary<Type, VisualElement.TypeData>();

		// Token: 0x040001E4 RID: 484
		private VisualElement.TypeData m_TypeData;

		// Token: 0x02000082 RID: 130
		public class UxmlFactory : UxmlFactory<VisualElement, VisualElement.UxmlTraits>
		{
			// Token: 0x060004B1 RID: 1201 RVA: 0x000121C7 File Offset: 0x000103C7
			public UxmlFactory()
			{
			}
		}

		// Token: 0x02000083 RID: 131
		public class UxmlTraits : UnityEngine.UIElements.UxmlTraits
		{
			// Token: 0x17000142 RID: 322
			// (get) Token: 0x060004B2 RID: 1202 RVA: 0x000121D0 File Offset: 0x000103D0
			// (set) Token: 0x060004B3 RID: 1203 RVA: 0x000121D8 File Offset: 0x000103D8
			protected UxmlIntAttributeDescription focusIndex
			{
				[CompilerGenerated]
				get
				{
					return this.<focusIndex>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<focusIndex>k__BackingField = value;
				}
			} = new UxmlIntAttributeDescription
			{
				name = null,
				obsoleteNames = new string[]
				{
					"focus-index",
					"focusIndex"
				},
				defaultValue = -1
			};

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000121E1 File Offset: 0x000103E1
			// (set) Token: 0x060004B5 RID: 1205 RVA: 0x000121E9 File Offset: 0x000103E9
			protected UxmlBoolAttributeDescription focusable
			{
				[CompilerGenerated]
				get
				{
					return this.<focusable>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<focusable>k__BackingField = value;
				}
			} = new UxmlBoolAttributeDescription
			{
				name = "focusable",
				defaultValue = false
			};

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000121F4 File Offset: 0x000103F4
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield return new UxmlChildElementDescription(typeof(VisualElement));
					yield break;
				}
			}

			// Token: 0x060004B7 RID: 1207 RVA: 0x00012214 File Offset: 0x00010414
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				bool flag = ve == null;
				if (flag)
				{
					throw new ArgumentNullException("ve");
				}
				ve.name = this.m_Name.GetValueFromBag(bag, cc);
				ve.viewDataKey = this.m_ViewDataKey.GetValueFromBag(bag, cc);
				ve.pickingMode = this.m_PickingMode.GetValueFromBag(bag, cc);
				ve.usageHints = this.m_UsageHints.GetValueFromBag(bag, cc);
				int num = 0;
				bool flag2 = this.focusIndex.TryGetValueFromBag(bag, cc, ref num);
				if (flag2)
				{
					ve.tabIndex = ((num >= 0) ? num : 0);
					ve.focusable = (num >= 0);
				}
				bool flag3 = this.m_TabIndex.TryGetValueFromBag(bag, cc, ref num);
				if (flag3)
				{
					ve.tabIndex = num;
				}
				bool focusable = false;
				bool flag4 = this.focusable.TryGetValueFromBag(bag, cc, ref focusable);
				if (flag4)
				{
					ve.focusable = focusable;
				}
				ve.tooltip = this.m_Tooltip.GetValueFromBag(bag, cc);
			}

			// Token: 0x060004B8 RID: 1208 RVA: 0x00012318 File Offset: 0x00010518
			public UxmlTraits()
			{
			}

			// Token: 0x040001E5 RID: 485
			protected UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
			{
				name = "name"
			};

			// Token: 0x040001E6 RID: 486
			private UxmlStringAttributeDescription m_ViewDataKey = new UxmlStringAttributeDescription
			{
				name = "view-data-key"
			};

			// Token: 0x040001E7 RID: 487
			protected UxmlEnumAttributeDescription<PickingMode> m_PickingMode = new UxmlEnumAttributeDescription<PickingMode>
			{
				name = "picking-mode",
				obsoleteNames = new string[]
				{
					"pickingMode"
				}
			};

			// Token: 0x040001E8 RID: 488
			private UxmlStringAttributeDescription m_Tooltip = new UxmlStringAttributeDescription
			{
				name = "tooltip"
			};

			// Token: 0x040001E9 RID: 489
			private UxmlEnumAttributeDescription<UsageHints> m_UsageHints = new UxmlEnumAttributeDescription<UsageHints>
			{
				name = "usage-hints"
			};

			// Token: 0x040001EA RID: 490
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private UxmlIntAttributeDescription <focusIndex>k__BackingField;

			// Token: 0x040001EB RID: 491
			private UxmlIntAttributeDescription m_TabIndex = new UxmlIntAttributeDescription
			{
				name = "tabindex",
				defaultValue = 0
			};

			// Token: 0x040001EC RID: 492
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private UxmlBoolAttributeDescription <focusable>k__BackingField;

			// Token: 0x040001ED RID: 493
			private UxmlStringAttributeDescription m_Class = new UxmlStringAttributeDescription
			{
				name = "class"
			};

			// Token: 0x040001EE RID: 494
			private UxmlStringAttributeDescription m_ContentContainer = new UxmlStringAttributeDescription
			{
				name = "content-container",
				obsoleteNames = new string[]
				{
					"contentContainer"
				}
			};

			// Token: 0x040001EF RID: 495
			private UxmlStringAttributeDescription m_Style = new UxmlStringAttributeDescription
			{
				name = "style"
			};

			// Token: 0x02000084 RID: 132
			[CompilerGenerated]
			private sealed class <get_uxmlChildElementsDescription>d__18 : IEnumerable<UxmlChildElementDescription>, IEnumerable, IEnumerator<UxmlChildElementDescription>, IEnumerator, IDisposable
			{
				// Token: 0x060004B9 RID: 1209 RVA: 0x0001248A File Offset: 0x0001068A
				[DebuggerHidden]
				public <get_uxmlChildElementsDescription>d__18(int <>1__state)
				{
					this.<>1__state = <>1__state;
					this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
				}

				// Token: 0x060004BA RID: 1210 RVA: 0x000080DB File Offset: 0x000062DB
				[DebuggerHidden]
				void IDisposable.Dispose()
				{
				}

				// Token: 0x060004BB RID: 1211 RVA: 0x000124AC File Offset: 0x000106AC
				bool IEnumerator.MoveNext()
				{
					int num = this.<>1__state;
					if (num == 0)
					{
						this.<>1__state = -1;
						this.<>2__current = new UxmlChildElementDescription(typeof(VisualElement));
						this.<>1__state = 1;
						return true;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					return false;
				}

				// Token: 0x17000145 RID: 325
				// (get) Token: 0x060004BC RID: 1212 RVA: 0x000124FF File Offset: 0x000106FF
				UxmlChildElementDescription IEnumerator<UxmlChildElementDescription>.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x060004BD RID: 1213 RVA: 0x0000810E File Offset: 0x0000630E
				[DebuggerHidden]
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x17000146 RID: 326
				// (get) Token: 0x060004BE RID: 1214 RVA: 0x000124FF File Offset: 0x000106FF
				object IEnumerator.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x060004BF RID: 1215 RVA: 0x00012508 File Offset: 0x00010708
				[DebuggerHidden]
				IEnumerator<UxmlChildElementDescription> IEnumerable<UxmlChildElementDescription>.GetEnumerator()
				{
					VisualElement.UxmlTraits.<get_uxmlChildElementsDescription>d__18 <get_uxmlChildElementsDescription>d__;
					if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
					{
						this.<>1__state = 0;
						<get_uxmlChildElementsDescription>d__ = this;
					}
					else
					{
						<get_uxmlChildElementsDescription>d__ = new VisualElement.UxmlTraits.<get_uxmlChildElementsDescription>d__18(0);
						<get_uxmlChildElementsDescription>d__.<>4__this = this;
					}
					return <get_uxmlChildElementsDescription>d__;
				}

				// Token: 0x060004C0 RID: 1216 RVA: 0x00012550 File Offset: 0x00010750
				[DebuggerHidden]
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.System.Collections.Generic.IEnumerable<UnityEngine.UIElements.UxmlChildElementDescription>.GetEnumerator();
				}

				// Token: 0x040001F0 RID: 496
				private int <>1__state;

				// Token: 0x040001F1 RID: 497
				private UxmlChildElementDescription <>2__current;

				// Token: 0x040001F2 RID: 498
				private int <>l__initialThreadId;

				// Token: 0x040001F3 RID: 499
				public VisualElement.UxmlTraits <>4__this;
			}
		}

		// Token: 0x02000085 RID: 133
		public enum MeasureMode
		{
			// Token: 0x040001F5 RID: 501
			Undefined,
			// Token: 0x040001F6 RID: 502
			Exactly,
			// Token: 0x040001F7 RID: 503
			AtMost
		}

		// Token: 0x02000086 RID: 134
		internal enum RenderTargetMode
		{
			// Token: 0x040001F9 RID: 505
			None,
			// Token: 0x040001FA RID: 506
			NoColorConversion,
			// Token: 0x040001FB RID: 507
			LinearToGamma,
			// Token: 0x040001FC RID: 508
			GammaToLinear
		}

		// Token: 0x02000087 RID: 135
		public struct Hierarchy
		{
			// Token: 0x17000147 RID: 327
			// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00012558 File Offset: 0x00010758
			public VisualElement parent
			{
				get
				{
					return this.m_Owner.m_PhysicalParent;
				}
			}

			// Token: 0x060004C2 RID: 1218 RVA: 0x00012575 File Offset: 0x00010775
			internal Hierarchy(VisualElement element)
			{
				this.m_Owner = element;
			}

			// Token: 0x060004C3 RID: 1219 RVA: 0x00012580 File Offset: 0x00010780
			public void Add(VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot add null child");
				}
				this.Insert(this.childCount, child);
			}

			// Token: 0x060004C4 RID: 1220 RVA: 0x000125B0 File Offset: 0x000107B0
			public void Insert(int index, VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot insert null child");
				}
				bool flag2 = index > this.childCount;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("Index out of range: " + index.ToString());
				}
				bool flag3 = child == this.m_Owner;
				if (flag3)
				{
					throw new ArgumentException("Cannot insert element as its own child");
				}
				bool flag4 = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag4)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				child.RemoveFromHierarchy();
				bool flag5 = this.m_Owner.m_Children == VisualElement.s_EmptyList;
				if (flag5)
				{
					this.m_Owner.m_Children = VisualElementListPool.Get(0);
				}
				bool isMeasureDefined = this.m_Owner.yogaNode.IsMeasureDefined;
				if (isMeasureDefined)
				{
					this.m_Owner.RemoveMeasureFunction();
				}
				this.PutChildAtIndex(child, index);
				int num = child.imguiContainerDescendantCount + (child.isIMGUIContainer ? 1 : 0);
				bool flag6 = num > 0;
				if (flag6)
				{
					this.m_Owner.ChangeIMGUIContainerCount(num);
				}
				child.hierarchy.SetParent(this.m_Owner);
				child.PropagateEnabledToChildren(this.m_Owner.enabledInHierarchy);
				child.InvokeHierarchyChanged(HierarchyChangeType.Add);
				child.IncrementVersion(VersionChangeType.Hierarchy);
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
			}

			// Token: 0x060004C5 RID: 1221 RVA: 0x00012710 File Offset: 0x00010910
			public void Remove(VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot remove null child");
				}
				bool flag2 = child.hierarchy.parent != this.m_Owner;
				if (flag2)
				{
					throw new ArgumentException("This VisualElement is not my child");
				}
				int index = this.m_Owner.m_Children.IndexOf(child);
				this.RemoveAt(index);
			}

			// Token: 0x060004C6 RID: 1222 RVA: 0x00012774 File Offset: 0x00010974
			public void RemoveAt(int index)
			{
				bool flag = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				bool flag2 = index < 0 || index >= this.childCount;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("Index out of range: " + index.ToString());
				}
				VisualElement visualElement = this.m_Owner.m_Children[index];
				visualElement.InvokeHierarchyChanged(HierarchyChangeType.Remove);
				this.RemoveChildAtIndex(index);
				int num = visualElement.imguiContainerDescendantCount + (visualElement.isIMGUIContainer ? 1 : 0);
				bool flag3 = num > 0;
				if (flag3)
				{
					this.m_Owner.ChangeIMGUIContainerCount(-num);
				}
				visualElement.hierarchy.SetParent(null);
				bool flag4 = this.childCount == 0;
				if (flag4)
				{
					this.ReleaseChildList();
					bool requireMeasureFunction = this.m_Owner.requireMeasureFunction;
					if (requireMeasureFunction)
					{
						this.m_Owner.AssignMeasureFunction();
					}
				}
				BaseVisualElementPanel elementPanel = this.m_Owner.elementPanel;
				if (elementPanel != null)
				{
					elementPanel.OnVersionChanged(visualElement, VersionChangeType.Hierarchy);
				}
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
			}

			// Token: 0x060004C7 RID: 1223 RVA: 0x0001289C File Offset: 0x00010A9C
			public void Clear()
			{
				bool flag = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				bool flag2 = this.childCount > 0;
				if (flag2)
				{
					List<VisualElement> list = VisualElementListPool.Copy(this.m_Owner.m_Children);
					this.ReleaseChildList();
					this.m_Owner.yogaNode.Clear();
					bool requireMeasureFunction = this.m_Owner.requireMeasureFunction;
					if (requireMeasureFunction)
					{
						this.m_Owner.AssignMeasureFunction();
					}
					foreach (VisualElement visualElement in list)
					{
						visualElement.InvokeHierarchyChanged(HierarchyChangeType.Remove);
						visualElement.hierarchy.SetParent(null);
						visualElement.m_LogicalParent = null;
						BaseVisualElementPanel elementPanel = this.m_Owner.elementPanel;
						if (elementPanel != null)
						{
							elementPanel.OnVersionChanged(visualElement, VersionChangeType.Hierarchy);
						}
					}
					bool flag3 = this.m_Owner.imguiContainerDescendantCount > 0;
					if (flag3)
					{
						int num = this.m_Owner.imguiContainerDescendantCount;
						bool isIMGUIContainer = this.m_Owner.isIMGUIContainer;
						if (isIMGUIContainer)
						{
							num--;
						}
						this.m_Owner.ChangeIMGUIContainerCount(-num);
					}
					VisualElementListPool.Release(list);
					this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
				}
			}

			// Token: 0x060004C8 RID: 1224 RVA: 0x00012A0C File Offset: 0x00010C0C
			internal void BringToFront(VisualElement child)
			{
				bool flag = this.childCount > 1;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num >= 0 && num < this.childCount - 1;
					if (flag2)
					{
						this.MoveChildElement(child, num, this.childCount);
					}
				}
			}

			// Token: 0x060004C9 RID: 1225 RVA: 0x00012A64 File Offset: 0x00010C64
			internal void SendToBack(VisualElement child)
			{
				bool flag = this.childCount > 1;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num > 0;
					if (flag2)
					{
						this.MoveChildElement(child, num, 0);
					}
				}
			}

			// Token: 0x060004CA RID: 1226 RVA: 0x00012AA8 File Offset: 0x00010CA8
			internal void PlaceBehind(VisualElement child, VisualElement over)
			{
				bool flag = this.childCount > 0;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num < 0;
					if (!flag2)
					{
						int num2 = this.m_Owner.m_Children.IndexOf(over);
						bool flag3 = num2 > 0 && num < num2;
						if (flag3)
						{
							num2--;
						}
						this.MoveChildElement(child, num, num2);
					}
				}
			}

			// Token: 0x060004CB RID: 1227 RVA: 0x00012B14 File Offset: 0x00010D14
			internal void PlaceInFront(VisualElement child, VisualElement under)
			{
				bool flag = this.childCount > 0;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num < 0;
					if (!flag2)
					{
						int num2 = this.m_Owner.m_Children.IndexOf(under);
						bool flag3 = num > num2;
						if (flag3)
						{
							num2++;
						}
						this.MoveChildElement(child, num, num2);
					}
				}
			}

			// Token: 0x060004CC RID: 1228 RVA: 0x00012B7C File Offset: 0x00010D7C
			private void MoveChildElement(VisualElement child, int currentIndex, int nextIndex)
			{
				bool flag = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				child.InvokeHierarchyChanged(HierarchyChangeType.Remove);
				this.RemoveChildAtIndex(currentIndex);
				this.PutChildAtIndex(child, nextIndex);
				child.InvokeHierarchyChanged(HierarchyChangeType.Add);
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x060004CD RID: 1229 RVA: 0x00012BE8 File Offset: 0x00010DE8
			public int childCount
			{
				get
				{
					return this.m_Owner.m_Children.Count;
				}
			}

			// Token: 0x17000149 RID: 329
			public VisualElement this[int key]
			{
				get
				{
					return this.m_Owner.m_Children[key];
				}
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x00012C30 File Offset: 0x00010E30
			public int IndexOf(VisualElement element)
			{
				return this.m_Owner.m_Children.IndexOf(element);
			}

			// Token: 0x060004D0 RID: 1232 RVA: 0x00012C54 File Offset: 0x00010E54
			public VisualElement ElementAt(int index)
			{
				return this[index];
			}

			// Token: 0x060004D1 RID: 1233 RVA: 0x00012C70 File Offset: 0x00010E70
			public IEnumerable<VisualElement> Children()
			{
				return this.m_Owner.m_Children;
			}

			// Token: 0x060004D2 RID: 1234 RVA: 0x00012C90 File Offset: 0x00010E90
			private void SetParent(VisualElement value)
			{
				this.m_Owner.m_PhysicalParent = value;
				this.m_Owner.m_LogicalParent = value;
				bool flag = value != null;
				if (flag)
				{
					this.m_Owner.SetPanel(this.m_Owner.m_PhysicalParent.elementPanel);
				}
				else
				{
					this.m_Owner.SetPanel(null);
				}
			}

			// Token: 0x060004D3 RID: 1235 RVA: 0x00012CF0 File Offset: 0x00010EF0
			public void Sort(Comparison<VisualElement> comp)
			{
				bool flag = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				bool flag2 = this.childCount > 1;
				if (flag2)
				{
					this.m_Owner.m_Children.Sort(comp);
					this.m_Owner.yogaNode.Clear();
					for (int i = 0; i < this.m_Owner.m_Children.Count; i++)
					{
						this.m_Owner.yogaNode.Insert(i, this.m_Owner.m_Children[i].yogaNode);
					}
					this.m_Owner.InvokeHierarchyChanged(HierarchyChangeType.Move);
					this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
				}
			}

			// Token: 0x060004D4 RID: 1236 RVA: 0x00012DC8 File Offset: 0x00010FC8
			private void PutChildAtIndex(VisualElement child, int index)
			{
				bool flag = index >= this.childCount;
				if (flag)
				{
					this.m_Owner.m_Children.Add(child);
					this.m_Owner.yogaNode.Insert(this.m_Owner.yogaNode.Count, child.yogaNode);
				}
				else
				{
					this.m_Owner.m_Children.Insert(index, child);
					this.m_Owner.yogaNode.Insert(index, child.yogaNode);
				}
			}

			// Token: 0x060004D5 RID: 1237 RVA: 0x00012E50 File Offset: 0x00011050
			private void RemoveChildAtIndex(int index)
			{
				this.m_Owner.m_Children.RemoveAt(index);
				this.m_Owner.yogaNode.RemoveAt(index);
			}

			// Token: 0x060004D6 RID: 1238 RVA: 0x00012E78 File Offset: 0x00011078
			private void ReleaseChildList()
			{
				bool flag = this.m_Owner.m_Children != VisualElement.s_EmptyList;
				if (flag)
				{
					List<VisualElement> children = this.m_Owner.m_Children;
					this.m_Owner.m_Children = VisualElement.s_EmptyList;
					VisualElementListPool.Release(children);
				}
			}

			// Token: 0x060004D7 RID: 1239 RVA: 0x00012EC4 File Offset: 0x000110C4
			public bool Equals(VisualElement.Hierarchy other)
			{
				return other == this;
			}

			// Token: 0x060004D8 RID: 1240 RVA: 0x00012EE4 File Offset: 0x000110E4
			public override bool Equals(object obj)
			{
				bool flag = obj == null;
				return !flag && obj is VisualElement.Hierarchy && this.Equals((VisualElement.Hierarchy)obj);
			}

			// Token: 0x060004D9 RID: 1241 RVA: 0x00012F1C File Offset: 0x0001111C
			public override int GetHashCode()
			{
				return (this.m_Owner != null) ? this.m_Owner.GetHashCode() : 0;
			}

			// Token: 0x060004DA RID: 1242 RVA: 0x00012F44 File Offset: 0x00011144
			public static bool operator ==(VisualElement.Hierarchy x, VisualElement.Hierarchy y)
			{
				return x.m_Owner == y.m_Owner;
			}

			// Token: 0x060004DB RID: 1243 RVA: 0x00012F64 File Offset: 0x00011164
			public static bool operator !=(VisualElement.Hierarchy x, VisualElement.Hierarchy y)
			{
				return !(x == y);
			}

			// Token: 0x040001FD RID: 509
			private const string k_InvalidHierarchyChangeMsg = "Cannot modify VisualElement hierarchy during layout calculation";

			// Token: 0x040001FE RID: 510
			private readonly VisualElement m_Owner;
		}

		// Token: 0x02000088 RID: 136
		private abstract class BaseVisualElementScheduledItem : ScheduledItem, IVisualElementScheduledItem, IVisualElementPanelActivatable
		{
			// Token: 0x1700014A RID: 330
			// (get) Token: 0x060004DC RID: 1244 RVA: 0x00012F80 File Offset: 0x00011180
			// (set) Token: 0x060004DD RID: 1245 RVA: 0x00012F88 File Offset: 0x00011188
			public VisualElement element
			{
				[CompilerGenerated]
				get
				{
					return this.<element>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<element>k__BackingField = value;
				}
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x060004DE RID: 1246 RVA: 0x00012F94 File Offset: 0x00011194
			public bool isActive
			{
				get
				{
					return this.m_Activator.isActive;
				}
			}

			// Token: 0x060004DF RID: 1247 RVA: 0x00012FB1 File Offset: 0x000111B1
			protected BaseVisualElementScheduledItem(VisualElement handler)
			{
				this.element = handler;
				this.m_Activator = new VisualElementPanelActivator(this);
			}

			// Token: 0x060004E0 RID: 1248 RVA: 0x00012FD8 File Offset: 0x000111D8
			public IVisualElementScheduledItem StartingIn(long delayMs)
			{
				base.delayMs = delayMs;
				return this;
			}

			// Token: 0x060004E1 RID: 1249 RVA: 0x00012FF4 File Offset: 0x000111F4
			public IVisualElementScheduledItem Until(Func<bool> stopCondition)
			{
				bool flag = stopCondition == null;
				if (flag)
				{
					stopCondition = ScheduledItem.ForeverCondition;
				}
				this.timerUpdateStopCondition = stopCondition;
				return this;
			}

			// Token: 0x060004E2 RID: 1250 RVA: 0x00013020 File Offset: 0x00011220
			public IVisualElementScheduledItem ForDuration(long durationMs)
			{
				base.SetDuration(durationMs);
				return this;
			}

			// Token: 0x060004E3 RID: 1251 RVA: 0x0001303C File Offset: 0x0001123C
			public IVisualElementScheduledItem Every(long intervalMs)
			{
				base.intervalMs = intervalMs;
				bool flag = this.timerUpdateStopCondition == ScheduledItem.OnceCondition;
				if (flag)
				{
					this.timerUpdateStopCondition = ScheduledItem.ForeverCondition;
				}
				return this;
			}

			// Token: 0x060004E4 RID: 1252 RVA: 0x00013078 File Offset: 0x00011278
			internal override void OnItemUnscheduled()
			{
				base.OnItemUnscheduled();
				this.isScheduled = false;
				bool flag = !this.m_Activator.isDetaching;
				if (flag)
				{
					this.m_Activator.SetActive(false);
				}
			}

			// Token: 0x060004E5 RID: 1253 RVA: 0x000130B5 File Offset: 0x000112B5
			public void Resume()
			{
				this.m_Activator.SetActive(true);
			}

			// Token: 0x060004E6 RID: 1254 RVA: 0x000130C5 File Offset: 0x000112C5
			public void Pause()
			{
				this.m_Activator.SetActive(false);
			}

			// Token: 0x060004E7 RID: 1255 RVA: 0x000130D8 File Offset: 0x000112D8
			public void ExecuteLater(long delayMs)
			{
				bool flag = !this.isScheduled;
				if (flag)
				{
					this.Resume();
				}
				base.ResetStartTime();
				this.StartingIn(delayMs);
			}

			// Token: 0x060004E8 RID: 1256 RVA: 0x0001310C File Offset: 0x0001130C
			public void OnPanelActivate()
			{
				bool flag = !this.isScheduled;
				if (flag)
				{
					this.isScheduled = true;
					base.ResetStartTime();
					this.element.elementPanel.scheduler.Schedule(this);
				}
			}

			// Token: 0x060004E9 RID: 1257 RVA: 0x00013150 File Offset: 0x00011350
			public void OnPanelDeactivate()
			{
				bool flag = this.isScheduled;
				if (flag)
				{
					this.isScheduled = false;
					this.element.elementPanel.scheduler.Unschedule(this);
				}
			}

			// Token: 0x060004EA RID: 1258 RVA: 0x00013188 File Offset: 0x00011388
			public bool CanBeActivated()
			{
				return this.element != null && this.element.elementPanel != null && this.element.elementPanel.scheduler != null;
			}

			// Token: 0x040001FF RID: 511
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private VisualElement <element>k__BackingField;

			// Token: 0x04000200 RID: 512
			public bool isScheduled = false;

			// Token: 0x04000201 RID: 513
			private VisualElementPanelActivator m_Activator;
		}

		// Token: 0x02000089 RID: 137
		private abstract class VisualElementScheduledItem<ActionType> : VisualElement.BaseVisualElementScheduledItem
		{
			// Token: 0x060004EB RID: 1259 RVA: 0x000131C5 File Offset: 0x000113C5
			public VisualElementScheduledItem(VisualElement handler, ActionType upEvent) : base(handler)
			{
				this.updateEvent = upEvent;
			}

			// Token: 0x060004EC RID: 1260 RVA: 0x000131D8 File Offset: 0x000113D8
			public static bool Matches(ScheduledItem item, ActionType updateEvent)
			{
				VisualElement.VisualElementScheduledItem<ActionType> visualElementScheduledItem = item as VisualElement.VisualElementScheduledItem<ActionType>;
				bool flag = visualElementScheduledItem != null;
				return flag && EqualityComparer<ActionType>.Default.Equals(visualElementScheduledItem.updateEvent, updateEvent);
			}

			// Token: 0x04000202 RID: 514
			public ActionType updateEvent;
		}

		// Token: 0x0200008A RID: 138
		private class TimerStateScheduledItem : VisualElement.VisualElementScheduledItem<Action<TimerState>>
		{
			// Token: 0x060004ED RID: 1261 RVA: 0x0001320F File Offset: 0x0001140F
			public TimerStateScheduledItem(VisualElement handler, Action<TimerState> updateEvent) : base(handler, updateEvent)
			{
			}

			// Token: 0x060004EE RID: 1262 RVA: 0x0001321C File Offset: 0x0001141C
			public override void PerformTimerUpdate(TimerState state)
			{
				bool isScheduled = this.isScheduled;
				if (isScheduled)
				{
					this.updateEvent(state);
				}
			}
		}

		// Token: 0x0200008B RID: 139
		private class SimpleScheduledItem : VisualElement.VisualElementScheduledItem<Action>
		{
			// Token: 0x060004EF RID: 1263 RVA: 0x00013243 File Offset: 0x00011443
			public SimpleScheduledItem(VisualElement handler, Action updateEvent) : base(handler, updateEvent)
			{
			}

			// Token: 0x060004F0 RID: 1264 RVA: 0x00013250 File Offset: 0x00011450
			public override void PerformTimerUpdate(TimerState state)
			{
				bool isScheduled = this.isScheduled;
				if (isScheduled)
				{
					this.updateEvent();
				}
			}
		}

		// Token: 0x0200008C RID: 140
		internal class CustomStyleAccess : ICustomStyle
		{
			// Token: 0x060004F1 RID: 1265 RVA: 0x00013276 File Offset: 0x00011476
			public void SetContext(Dictionary<string, StylePropertyValue> customProperties, float dpiScaling)
			{
				this.m_CustomProperties = customProperties;
				this.m_DpiScaling = dpiScaling;
			}

			// Token: 0x060004F2 RID: 1266 RVA: 0x00013288 File Offset: 0x00011488
			public bool TryGetValue(CustomStyleProperty<float> property, out float value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.TryGetValue(property.name, StyleValueType.Float, out stylePropertyValue);
				if (flag)
				{
					bool flag2 = stylePropertyValue.sheet.TryReadFloat(stylePropertyValue.handle, out value);
					if (flag2)
					{
						return true;
					}
				}
				value = 0f;
				return false;
			}

			// Token: 0x060004F3 RID: 1267 RVA: 0x000132D4 File Offset: 0x000114D4
			public bool TryGetValue(CustomStyleProperty<int> property, out int value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.TryGetValue(property.name, StyleValueType.Float, out stylePropertyValue);
				if (flag)
				{
					float num;
					bool flag2 = stylePropertyValue.sheet.TryReadFloat(stylePropertyValue.handle, out num);
					if (flag2)
					{
						value = (int)num;
						return true;
					}
				}
				value = 0;
				return false;
			}

			// Token: 0x060004F4 RID: 1268 RVA: 0x00013324 File Offset: 0x00011524
			public bool TryGetValue(CustomStyleProperty<bool> property, out bool value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out stylePropertyValue);
				bool result;
				if (flag)
				{
					value = (stylePropertyValue.sheet.ReadKeyword(stylePropertyValue.handle) == StyleValueKeyword.True);
					result = true;
				}
				else
				{
					value = false;
					result = false;
				}
				return result;
			}

			// Token: 0x060004F5 RID: 1269 RVA: 0x00013378 File Offset: 0x00011578
			public bool TryGetValue(CustomStyleProperty<Color> property, out Color value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out stylePropertyValue);
				if (flag)
				{
					StyleValueHandle handle = stylePropertyValue.handle;
					StyleValueType valueType = handle.valueType;
					StyleValueType styleValueType = valueType;
					if (styleValueType != StyleValueType.Color)
					{
						if (styleValueType == StyleValueType.Enum)
						{
							string text = stylePropertyValue.sheet.ReadAsString(handle);
							return StyleSheetColor.TryGetColor(text.ToLower(), out value);
						}
						VisualElement.CustomStyleAccess.LogCustomPropertyWarning(property.name, StyleValueType.Color, stylePropertyValue);
					}
					else
					{
						bool flag2 = stylePropertyValue.sheet.TryReadColor(stylePropertyValue.handle, out value);
						if (flag2)
						{
							return true;
						}
					}
				}
				value = Color.clear;
				return false;
			}

			// Token: 0x060004F6 RID: 1270 RVA: 0x00013430 File Offset: 0x00011630
			public bool TryGetValue(CustomStyleProperty<Texture2D> property, out Texture2D value)
			{
				StylePropertyValue propertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out propertyValue);
				if (flag)
				{
					ImageSource imageSource = default(ImageSource);
					bool flag2 = StylePropertyReader.TryGetImageSourceFromValue(propertyValue, this.m_DpiScaling, out imageSource) && imageSource.texture != null;
					if (flag2)
					{
						value = imageSource.texture;
						return true;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x060004F7 RID: 1271 RVA: 0x000134A8 File Offset: 0x000116A8
			public bool TryGetValue(CustomStyleProperty<Sprite> property, out Sprite value)
			{
				StylePropertyValue propertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out propertyValue);
				if (flag)
				{
					ImageSource imageSource = default(ImageSource);
					bool flag2 = StylePropertyReader.TryGetImageSourceFromValue(propertyValue, this.m_DpiScaling, out imageSource) && imageSource.sprite != null;
					if (flag2)
					{
						value = imageSource.sprite;
						return true;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x060004F8 RID: 1272 RVA: 0x00013520 File Offset: 0x00011720
			public bool TryGetValue(CustomStyleProperty<VectorImage> property, out VectorImage value)
			{
				StylePropertyValue propertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out propertyValue);
				if (flag)
				{
					ImageSource imageSource = default(ImageSource);
					bool flag2 = StylePropertyReader.TryGetImageSourceFromValue(propertyValue, this.m_DpiScaling, out imageSource) && imageSource.vectorImage != null;
					if (flag2)
					{
						value = imageSource.vectorImage;
						return true;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x060004F9 RID: 1273 RVA: 0x00013598 File Offset: 0x00011798
			public bool TryGetValue<T>(CustomStyleProperty<T> property, out T value) where T : Object
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out stylePropertyValue);
				if (flag)
				{
					Object @object;
					bool flag2 = stylePropertyValue.sheet.TryReadAssetReference(stylePropertyValue.handle, out @object);
					if (flag2)
					{
						value = (@object as T);
						return value != null;
					}
				}
				value = default(T);
				return false;
			}

			// Token: 0x060004FA RID: 1274 RVA: 0x00013618 File Offset: 0x00011818
			public bool TryGetValue(CustomStyleProperty<string> property, out string value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out stylePropertyValue);
				bool result;
				if (flag)
				{
					value = stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
					result = true;
				}
				else
				{
					value = string.Empty;
					result = false;
				}
				return result;
			}

			// Token: 0x060004FB RID: 1275 RVA: 0x00013670 File Offset: 0x00011870
			private bool TryGetValue(string propertyName, StyleValueType valueType, out StylePropertyValue customProp)
			{
				customProp = default(StylePropertyValue);
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(propertyName, out customProp);
				bool result;
				if (flag)
				{
					StyleValueHandle handle = customProp.handle;
					bool flag2 = handle.valueType != valueType;
					if (flag2)
					{
						VisualElement.CustomStyleAccess.LogCustomPropertyWarning(propertyName, valueType, customProp);
						result = false;
					}
					else
					{
						result = true;
					}
				}
				else
				{
					result = false;
				}
				return result;
			}

			// Token: 0x060004FC RID: 1276 RVA: 0x000136D6 File Offset: 0x000118D6
			private static void LogCustomPropertyWarning(string propertyName, StyleValueType valueType, StylePropertyValue customProp)
			{
				Debug.LogWarning(string.Format("Trying to read custom property {0} value as {1} while parsed type is {2}", propertyName, valueType, customProp.handle.valueType));
			}

			// Token: 0x060004FD RID: 1277 RVA: 0x000020C2 File Offset: 0x000002C2
			public CustomStyleAccess()
			{
			}

			// Token: 0x04000203 RID: 515
			private Dictionary<string, StylePropertyValue> m_CustomProperties;

			// Token: 0x04000204 RID: 516
			private float m_DpiScaling;
		}

		// Token: 0x0200008D RID: 141
		private class TypeData
		{
			// Token: 0x1700014C RID: 332
			// (get) Token: 0x060004FE RID: 1278 RVA: 0x00013701 File Offset: 0x00011901
			public Type type
			{
				[CompilerGenerated]
				get
				{
					return this.<type>k__BackingField;
				}
			}

			// Token: 0x060004FF RID: 1279 RVA: 0x00013709 File Offset: 0x00011909
			public TypeData(Type type)
			{
				this.type = type;
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06000500 RID: 1280 RVA: 0x00013730 File Offset: 0x00011930
			public string fullTypeName
			{
				get
				{
					bool flag = string.IsNullOrEmpty(this.m_FullTypeName);
					if (flag)
					{
						this.m_FullTypeName = this.type.FullName;
					}
					return this.m_FullTypeName;
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x06000501 RID: 1281 RVA: 0x00013768 File Offset: 0x00011968
			public string typeName
			{
				get
				{
					bool flag = string.IsNullOrEmpty(this.m_TypeName);
					if (flag)
					{
						bool isGenericType = this.type.IsGenericType;
						this.m_TypeName = this.type.Name;
						bool flag2 = isGenericType;
						if (flag2)
						{
							int num = this.m_TypeName.IndexOf('`');
							bool flag3 = num >= 0;
							if (flag3)
							{
								this.m_TypeName = this.m_TypeName.Remove(num);
							}
						}
					}
					return this.m_TypeName;
				}
			}

			// Token: 0x04000205 RID: 517
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private readonly Type <type>k__BackingField;

			// Token: 0x04000206 RID: 518
			private string m_FullTypeName = string.Empty;

			// Token: 0x04000207 RID: 519
			private string m_TypeName = string.Empty;
		}

		// Token: 0x0200008E RID: 142
		[CompilerGenerated]
		private sealed class <>c__DisplayClass298_0
		{
			// Token: 0x06000502 RID: 1282 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c__DisplayClass298_0()
			{
			}

			// Token: 0x06000503 RID: 1283 RVA: 0x000137E6 File Offset: 0x000119E6
			internal float <UnityEngine.UIElements.Experimental.ITransitionAnimations.Start>b__0(VisualElement e)
			{
				return this.from;
			}

			// Token: 0x04000208 RID: 520
			public float from;
		}

		// Token: 0x0200008F RID: 143
		[CompilerGenerated]
		private sealed class <>c__DisplayClass299_0
		{
			// Token: 0x06000504 RID: 1284 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c__DisplayClass299_0()
			{
			}

			// Token: 0x06000505 RID: 1285 RVA: 0x000137EE File Offset: 0x000119EE
			internal Rect <UnityEngine.UIElements.Experimental.ITransitionAnimations.Start>b__0(VisualElement e)
			{
				return this.from;
			}

			// Token: 0x04000209 RID: 521
			public Rect from;
		}

		// Token: 0x02000090 RID: 144
		[CompilerGenerated]
		private sealed class <>c__DisplayClass300_0
		{
			// Token: 0x06000506 RID: 1286 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c__DisplayClass300_0()
			{
			}

			// Token: 0x06000507 RID: 1287 RVA: 0x000137F6 File Offset: 0x000119F6
			internal Color <UnityEngine.UIElements.Experimental.ITransitionAnimations.Start>b__0(VisualElement e)
			{
				return this.from;
			}

			// Token: 0x0400020A RID: 522
			public Color from;
		}

		// Token: 0x02000091 RID: 145
		[CompilerGenerated]
		private sealed class <>c__DisplayClass301_0
		{
			// Token: 0x06000508 RID: 1288 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c__DisplayClass301_0()
			{
			}

			// Token: 0x06000509 RID: 1289 RVA: 0x000137FE File Offset: 0x000119FE
			internal Vector3 <UnityEngine.UIElements.Experimental.ITransitionAnimations.Start>b__0(VisualElement e)
			{
				return this.from;
			}

			// Token: 0x0400020B RID: 523
			public Vector3 from;
		}

		// Token: 0x02000092 RID: 146
		[CompilerGenerated]
		private sealed class <>c__DisplayClass302_0
		{
			// Token: 0x0600050A RID: 1290 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c__DisplayClass302_0()
			{
			}

			// Token: 0x0600050B RID: 1291 RVA: 0x00013806 File Offset: 0x00011A06
			internal Vector2 <UnityEngine.UIElements.Experimental.ITransitionAnimations.Start>b__0(VisualElement e)
			{
				return this.from;
			}

			// Token: 0x0400020C RID: 524
			public Vector2 from;
		}

		// Token: 0x02000093 RID: 147
		[CompilerGenerated]
		private sealed class <>c__DisplayClass303_0
		{
			// Token: 0x0600050C RID: 1292 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c__DisplayClass303_0()
			{
			}

			// Token: 0x0600050D RID: 1293 RVA: 0x0001380E File Offset: 0x00011A0E
			internal Quaternion <UnityEngine.UIElements.Experimental.ITransitionAnimations.Start>b__0(VisualElement e)
			{
				return this.from;
			}

			// Token: 0x0400020D RID: 525
			public Quaternion from;
		}

		// Token: 0x02000094 RID: 148
		[CompilerGenerated]
		private sealed class <>c__DisplayClass304_0
		{
			// Token: 0x0600050E RID: 1294 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c__DisplayClass304_0()
			{
			}

			// Token: 0x0600050F RID: 1295 RVA: 0x00013816 File Offset: 0x00011A16
			internal StyleValues <UnityEngine.UIElements.Experimental.ITransitionAnimations.Start>b__0(VisualElement e)
			{
				return this.from;
			}

			// Token: 0x0400020E RID: 526
			public StyleValues from;
		}

		// Token: 0x02000095 RID: 149
		[CompilerGenerated]
		private sealed class <>c__DisplayClass314_0
		{
			// Token: 0x06000510 RID: 1296 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c__DisplayClass314_0()
			{
			}

			// Token: 0x06000511 RID: 1297 RVA: 0x0001381E File Offset: 0x00011A1E
			internal StyleValues <UnityEngine.UIElements.Experimental.ITransitionAnimations.Start>b__0(VisualElement e)
			{
				return this.<>4__this.ReadCurrentValues(e, this.to);
			}

			// Token: 0x0400020F RID: 527
			public VisualElement <>4__this;

			// Token: 0x04000210 RID: 528
			public StyleValues to;
		}

		// Token: 0x02000096 RID: 150
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000512 RID: 1298 RVA: 0x00013832 File Offset: 0x00011A32
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000513 RID: 1299 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x06000514 RID: 1300 RVA: 0x0001383E File Offset: 0x00011A3E
			internal Rect <UnityEngine.UIElements.Experimental.ITransitionAnimations.Layout>b__316_0(VisualElement e)
			{
				return new Rect(e.resolvedStyle.left, e.resolvedStyle.top, e.resolvedStyle.width, e.resolvedStyle.height);
			}

			// Token: 0x06000515 RID: 1301 RVA: 0x00013874 File Offset: 0x00011A74
			internal void <UnityEngine.UIElements.Experimental.ITransitionAnimations.Layout>b__316_1(VisualElement e, Rect c)
			{
				e.style.left = c.x;
				e.style.top = c.y;
				e.style.width = c.width;
				e.style.height = c.height;
			}

			// Token: 0x06000516 RID: 1302 RVA: 0x000138E2 File Offset: 0x00011AE2
			internal Vector2 <UnityEngine.UIElements.Experimental.ITransitionAnimations.TopLeft>b__317_0(VisualElement e)
			{
				return new Vector2(e.resolvedStyle.left, e.resolvedStyle.top);
			}

			// Token: 0x06000517 RID: 1303 RVA: 0x000138FF File Offset: 0x00011AFF
			internal void <UnityEngine.UIElements.Experimental.ITransitionAnimations.TopLeft>b__317_1(VisualElement e, Vector2 c)
			{
				e.style.left = c.x;
				e.style.top = c.y;
			}

			// Token: 0x06000518 RID: 1304 RVA: 0x00013930 File Offset: 0x00011B30
			internal Vector2 <UnityEngine.UIElements.Experimental.ITransitionAnimations.Size>b__318_0(VisualElement e)
			{
				return e.layout.size;
			}

			// Token: 0x06000519 RID: 1305 RVA: 0x0001394B File Offset: 0x00011B4B
			internal void <UnityEngine.UIElements.Experimental.ITransitionAnimations.Size>b__318_1(VisualElement e, Vector2 c)
			{
				e.style.width = c.x;
				e.style.height = c.y;
			}

			// Token: 0x0600051A RID: 1306 RVA: 0x0001397C File Offset: 0x00011B7C
			internal float <UnityEngine.UIElements.Experimental.ITransitionAnimations.Scale>b__319_0(VisualElement e)
			{
				return e.transform.scale.x;
			}

			// Token: 0x0600051B RID: 1307 RVA: 0x0001398E File Offset: 0x00011B8E
			internal void <UnityEngine.UIElements.Experimental.ITransitionAnimations.Scale>b__319_1(VisualElement e, float c)
			{
				e.transform.scale = new Vector3(c, c, c);
			}

			// Token: 0x0600051C RID: 1308 RVA: 0x000139A5 File Offset: 0x00011BA5
			internal Vector3 <UnityEngine.UIElements.Experimental.ITransitionAnimations.Position>b__320_0(VisualElement e)
			{
				return e.transform.position;
			}

			// Token: 0x0600051D RID: 1309 RVA: 0x000139B2 File Offset: 0x00011BB2
			internal void <UnityEngine.UIElements.Experimental.ITransitionAnimations.Position>b__320_1(VisualElement e, Vector3 c)
			{
				e.transform.position = c;
			}

			// Token: 0x0600051E RID: 1310 RVA: 0x000139C2 File Offset: 0x00011BC2
			internal Quaternion <UnityEngine.UIElements.Experimental.ITransitionAnimations.Rotation>b__321_0(VisualElement e)
			{
				return e.transform.rotation;
			}

			// Token: 0x0600051F RID: 1311 RVA: 0x000139CF File Offset: 0x00011BCF
			internal void <UnityEngine.UIElements.Experimental.ITransitionAnimations.Rotation>b__321_1(VisualElement e, Quaternion c)
			{
				e.transform.rotation = c;
			}

			// Token: 0x04000211 RID: 529
			public static readonly VisualElement.<>c <>9 = new VisualElement.<>c();

			// Token: 0x04000212 RID: 530
			public static Func<VisualElement, Rect> <>9__316_0;

			// Token: 0x04000213 RID: 531
			public static Action<VisualElement, Rect> <>9__316_1;

			// Token: 0x04000214 RID: 532
			public static Func<VisualElement, Vector2> <>9__317_0;

			// Token: 0x04000215 RID: 533
			public static Action<VisualElement, Vector2> <>9__317_1;

			// Token: 0x04000216 RID: 534
			public static Func<VisualElement, Vector2> <>9__318_0;

			// Token: 0x04000217 RID: 535
			public static Action<VisualElement, Vector2> <>9__318_1;

			// Token: 0x04000218 RID: 536
			public static Func<VisualElement, float> <>9__319_0;

			// Token: 0x04000219 RID: 537
			public static Action<VisualElement, float> <>9__319_1;

			// Token: 0x0400021A RID: 538
			public static Func<VisualElement, Vector3> <>9__320_0;

			// Token: 0x0400021B RID: 539
			public static Action<VisualElement, Vector3> <>9__320_1;

			// Token: 0x0400021C RID: 540
			public static Func<VisualElement, Quaternion> <>9__321_0;

			// Token: 0x0400021D RID: 541
			public static Action<VisualElement, Quaternion> <>9__321_1;
		}
	}
}
