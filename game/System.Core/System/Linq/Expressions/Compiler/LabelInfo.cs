using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002B8 RID: 696
	internal sealed class LabelInfo
	{
		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x00040E85 File Offset: 0x0003F085
		internal Label Label
		{
			get
			{
				this.EnsureLabelAndValue();
				return this._label;
			}
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x00040E93 File Offset: 0x0003F093
		internal LabelInfo(ILGenerator il, LabelTarget node, bool canReturn)
		{
			this._ilg = il;
			this._node = node;
			this._canReturn = canReturn;
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x00040ED1 File Offset: 0x0003F0D1
		internal bool CanReturn
		{
			get
			{
				return this._canReturn;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x00040ED9 File Offset: 0x0003F0D9
		internal bool CanBranch
		{
			get
			{
				return this._opCode != OpCodes.Leave;
			}
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x00040EEB File Offset: 0x0003F0EB
		internal void Reference(LabelScopeInfo block)
		{
			this._references.Add(block);
			if (this._definitions.Count > 0)
			{
				this.ValidateJump(block);
			}
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00040F10 File Offset: 0x0003F110
		internal void Define(LabelScopeInfo block)
		{
			for (LabelScopeInfo labelScopeInfo = block; labelScopeInfo != null; labelScopeInfo = labelScopeInfo.Parent)
			{
				if (labelScopeInfo.ContainsTarget(this._node))
				{
					throw Error.LabelTargetAlreadyDefined(this._node.Name);
				}
			}
			this._definitions.Add(block);
			block.AddLabelInfo(this._node, this);
			if (this._definitions.Count == 1)
			{
				using (List<LabelScopeInfo>.Enumerator enumerator = this._references.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						LabelScopeInfo reference = enumerator.Current;
						this.ValidateJump(reference);
					}
					return;
				}
			}
			if (this._acrossBlockJump)
			{
				throw Error.AmbiguousJump(this._node.Name);
			}
			this._labelDefined = false;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00040FD8 File Offset: 0x0003F1D8
		private void ValidateJump(LabelScopeInfo reference)
		{
			this._opCode = (this._canReturn ? OpCodes.Ret : OpCodes.Br);
			for (LabelScopeInfo labelScopeInfo = reference; labelScopeInfo != null; labelScopeInfo = labelScopeInfo.Parent)
			{
				if (this._definitions.Contains(labelScopeInfo))
				{
					return;
				}
				if (labelScopeInfo.Kind == LabelScopeKind.Finally || labelScopeInfo.Kind == LabelScopeKind.Filter)
				{
					break;
				}
				if (labelScopeInfo.Kind == LabelScopeKind.Try || labelScopeInfo.Kind == LabelScopeKind.Catch)
				{
					this._opCode = OpCodes.Leave;
				}
			}
			this._acrossBlockJump = true;
			if (this._node != null && this._node.Type != typeof(void))
			{
				throw Error.NonLocalJumpWithValue(this._node.Name);
			}
			if (this._definitions.Count > 1)
			{
				throw Error.AmbiguousJump(this._node.Name);
			}
			LabelScopeInfo labelScopeInfo2 = this._definitions.First<LabelScopeInfo>();
			LabelScopeInfo labelScopeInfo3 = Helpers.CommonNode<LabelScopeInfo>(labelScopeInfo2, reference, (LabelScopeInfo b) => b.Parent);
			this._opCode = (this._canReturn ? OpCodes.Ret : OpCodes.Br);
			for (LabelScopeInfo labelScopeInfo4 = reference; labelScopeInfo4 != labelScopeInfo3; labelScopeInfo4 = labelScopeInfo4.Parent)
			{
				if (labelScopeInfo4.Kind == LabelScopeKind.Finally)
				{
					throw Error.ControlCannotLeaveFinally();
				}
				if (labelScopeInfo4.Kind == LabelScopeKind.Filter)
				{
					throw Error.ControlCannotLeaveFilterTest();
				}
				if (labelScopeInfo4.Kind == LabelScopeKind.Try || labelScopeInfo4.Kind == LabelScopeKind.Catch)
				{
					this._opCode = OpCodes.Leave;
				}
			}
			LabelScopeInfo labelScopeInfo5 = labelScopeInfo2;
			while (labelScopeInfo5 != labelScopeInfo3)
			{
				if (!labelScopeInfo5.CanJumpInto)
				{
					if (labelScopeInfo5.Kind == LabelScopeKind.Expression)
					{
						throw Error.ControlCannotEnterExpression();
					}
					throw Error.ControlCannotEnterTry();
				}
				else
				{
					labelScopeInfo5 = labelScopeInfo5.Parent;
				}
			}
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0004116F File Offset: 0x0003F36F
		internal void ValidateFinish()
		{
			if (this._references.Count > 0 && this._definitions.Count == 0)
			{
				throw Error.LabelTargetUndefined(this._node.Name);
			}
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x000411A0 File Offset: 0x0003F3A0
		internal void EmitJump()
		{
			if (this._opCode == OpCodes.Ret)
			{
				this._ilg.Emit(OpCodes.Ret);
				return;
			}
			this.StoreValue();
			this._ilg.Emit(this._opCode, this.Label);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x000411ED File Offset: 0x0003F3ED
		private void StoreValue()
		{
			this.EnsureLabelAndValue();
			if (this._value != null)
			{
				this._ilg.Emit(OpCodes.Stloc, this._value);
			}
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00041213 File Offset: 0x0003F413
		internal void Mark()
		{
			if (this._canReturn)
			{
				if (!this._labelDefined)
				{
					return;
				}
				this._ilg.Emit(OpCodes.Ret);
			}
			else
			{
				this.StoreValue();
			}
			this.MarkWithEmptyStack();
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x00041244 File Offset: 0x0003F444
		internal void MarkWithEmptyStack()
		{
			this._ilg.MarkLabel(this.Label);
			if (this._value != null)
			{
				this._ilg.Emit(OpCodes.Ldloc, this._value);
			}
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x00041278 File Offset: 0x0003F478
		private void EnsureLabelAndValue()
		{
			if (!this._labelDefined)
			{
				this._labelDefined = true;
				this._label = this._ilg.DefineLabel();
				if (this._node != null && this._node.Type != typeof(void))
				{
					this._value = this._ilg.DeclareLocal(this._node.Type);
				}
			}
		}

		// Token: 0x04000AC5 RID: 2757
		private readonly LabelTarget _node;

		// Token: 0x04000AC6 RID: 2758
		private Label _label;

		// Token: 0x04000AC7 RID: 2759
		private bool _labelDefined;

		// Token: 0x04000AC8 RID: 2760
		private LocalBuilder _value;

		// Token: 0x04000AC9 RID: 2761
		private readonly HashSet<LabelScopeInfo> _definitions = new HashSet<LabelScopeInfo>();

		// Token: 0x04000ACA RID: 2762
		private readonly List<LabelScopeInfo> _references = new List<LabelScopeInfo>();

		// Token: 0x04000ACB RID: 2763
		private readonly bool _canReturn;

		// Token: 0x04000ACC RID: 2764
		private bool _acrossBlockJump;

		// Token: 0x04000ACD RID: 2765
		private OpCode _opCode = OpCodes.Leave;

		// Token: 0x04000ACE RID: 2766
		private readonly ILGenerator _ilg;

		// Token: 0x020002B9 RID: 697
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060014C8 RID: 5320 RVA: 0x000412E5 File Offset: 0x0003F4E5
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060014C9 RID: 5321 RVA: 0x00002162 File Offset: 0x00000362
			public <>c()
			{
			}

			// Token: 0x060014CA RID: 5322 RVA: 0x000412F1 File Offset: 0x0003F4F1
			internal LabelScopeInfo <ValidateJump>b__19_0(LabelScopeInfo b)
			{
				return b.Parent;
			}

			// Token: 0x04000ACF RID: 2767
			public static readonly LabelInfo.<>c <>9 = new LabelInfo.<>c();

			// Token: 0x04000AD0 RID: 2768
			public static Func<LabelScopeInfo, LabelScopeInfo> <>9__19_0;
		}
	}
}
