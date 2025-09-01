using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000269 RID: 617
	public class ParamsParameter : Parameter
	{
		// Token: 0x06001E54 RID: 7764 RVA: 0x00095849 File Offset: 0x00093A49
		public ParamsParameter(FullNamedExpression type, string name, Attributes attrs, Location loc) : base(type, name, Parameter.Modifier.PARAMS, attrs, loc)
		{
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x00095858 File Offset: 0x00093A58
		public override TypeSpec Resolve(IMemberContext ec, int index)
		{
			if (base.Resolve(ec, index) == null)
			{
				return null;
			}
			ArrayContainer arrayContainer = this.parameter_type as ArrayContainer;
			if (arrayContainer == null || arrayContainer.Rank != 1)
			{
				ec.Module.Compiler.Report.Error(225, base.Location, "The params parameter must be a single dimensional array");
				return null;
			}
			return this.parameter_type;
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x000958B6 File Offset: 0x00093AB6
		public override void ApplyAttributes(MethodBuilder mb, ConstructorBuilder cb, int index, PredefinedAttributes pa)
		{
			base.ApplyAttributes(mb, cb, index, pa);
			pa.ParamArray.EmitAttribute(this.builder);
		}
	}
}
