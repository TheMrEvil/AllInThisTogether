using System;
using System.Collections.Generic;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002BB RID: 699
	internal sealed class LabelScopeInfo
	{
		// Token: 0x060014CB RID: 5323 RVA: 0x000412F9 File Offset: 0x0003F4F9
		internal LabelScopeInfo(LabelScopeInfo parent, LabelScopeKind kind)
		{
			this.Parent = parent;
			this.Kind = kind;
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x00041310 File Offset: 0x0003F510
		internal bool CanJumpInto
		{
			get
			{
				LabelScopeKind kind = this.Kind;
				return kind <= LabelScopeKind.Lambda;
			}
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0004132B File Offset: 0x0003F52B
		internal bool ContainsTarget(LabelTarget target)
		{
			return this._labels != null && this._labels.ContainsKey(target);
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00041343 File Offset: 0x0003F543
		internal bool TryGetLabelInfo(LabelTarget target, out LabelInfo info)
		{
			if (this._labels == null)
			{
				info = null;
				return false;
			}
			return this._labels.TryGetValue(target, out info);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0004135F File Offset: 0x0003F55F
		internal void AddLabelInfo(LabelTarget target, LabelInfo info)
		{
			if (this._labels == null)
			{
				this._labels = new Dictionary<LabelTarget, LabelInfo>();
			}
			this._labels.Add(target, info);
		}

		// Token: 0x04000ADB RID: 2779
		private Dictionary<LabelTarget, LabelInfo> _labels;

		// Token: 0x04000ADC RID: 2780
		internal readonly LabelScopeKind Kind;

		// Token: 0x04000ADD RID: 2781
		internal readonly LabelScopeInfo Parent;
	}
}
