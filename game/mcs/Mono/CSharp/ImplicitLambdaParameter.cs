using System;

namespace Mono.CSharp
{
	// Token: 0x02000268 RID: 616
	public class ImplicitLambdaParameter : Parameter
	{
		// Token: 0x06001E51 RID: 7761 RVA: 0x00095802 File Offset: 0x00093A02
		public ImplicitLambdaParameter(string name, Location loc) : base(null, name, Parameter.Modifier.NONE, null, loc)
		{
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x0009580F File Offset: 0x00093A0F
		public override TypeSpec Resolve(IMemberContext ec, int index)
		{
			if (this.parameter_type == null)
			{
				throw new InternalErrorException("A type of implicit lambda parameter `{0}' is not set", new object[]
				{
					base.Name
				});
			}
			this.idx = index;
			return this.parameter_type;
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x00095840 File Offset: 0x00093A40
		public void SetParameterType(TypeSpec type)
		{
			this.parameter_type = type;
		}
	}
}
