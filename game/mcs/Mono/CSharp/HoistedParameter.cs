using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000109 RID: 265
	public class HoistedParameter : HoistedVariable
	{
		// Token: 0x06000D44 RID: 3396 RVA: 0x000304F7 File Offset: 0x0002E6F7
		public HoistedParameter(AnonymousMethodStorey scope, ParameterReference par) : base(scope, par.Name, par.Type)
		{
			this.parameter = par;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00030513 File Offset: 0x0002E713
		public HoistedParameter(HoistedParameter hp, string name) : base(hp.storey, name, hp.parameter.Type)
		{
			this.parameter = hp.parameter;
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00030539 File Offset: 0x0002E739
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x00030541 File Offset: 0x0002E741
		public bool IsAssigned
		{
			[CompilerGenerated]
			get
			{
				return this.<IsAssigned>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsAssigned>k__BackingField = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0003054A File Offset: 0x0002E74A
		public ParameterReference Parameter
		{
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00030554 File Offset: 0x0002E754
		public void EmitHoistingAssignment(EmitContext ec)
		{
			HoistedParameter hoistedVariant = this.parameter.Parameter.HoistedVariant;
			this.parameter.Parameter.HoistedVariant = null;
			new HoistedParameter.HoistedFieldAssign(this.GetFieldExpression(ec), this.parameter).EmitStatement(ec);
			this.parameter.Parameter.HoistedVariant = hoistedVariant;
		}

		// Token: 0x04000650 RID: 1616
		private readonly ParameterReference parameter;

		// Token: 0x04000651 RID: 1617
		[CompilerGenerated]
		private bool <IsAssigned>k__BackingField;

		// Token: 0x0200037D RID: 893
		private sealed class HoistedFieldAssign : CompilerAssign
		{
			// Token: 0x0600268E RID: 9870 RVA: 0x000B6A23 File Offset: 0x000B4C23
			public HoistedFieldAssign(Expression target, Expression source) : base(target, source, target.Location)
			{
			}

			// Token: 0x0600268F RID: 9871 RVA: 0x00005936 File Offset: 0x00003B36
			protected override Expression ResolveConversions(ResolveContext ec)
			{
				return this;
			}
		}
	}
}
