using System;

namespace Mono.CSharp
{
	// Token: 0x0200019A RID: 410
	public class DocumentationParameter
	{
		// Token: 0x06001610 RID: 5648 RVA: 0x0006A4AB File Offset: 0x000686AB
		public DocumentationParameter(Parameter.Modifier modifier, FullNamedExpression type) : this(type)
		{
			this.Modifier = modifier;
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x0006A4BB File Offset: 0x000686BB
		public DocumentationParameter(FullNamedExpression type)
		{
			this.Type = type;
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x0006A4CA File Offset: 0x000686CA
		public TypeSpec TypeSpec
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0006A4D2 File Offset: 0x000686D2
		public void Resolve(IMemberContext context)
		{
			this.type = this.Type.ResolveAsType(context, false);
		}

		// Token: 0x04000936 RID: 2358
		public readonly Parameter.Modifier Modifier;

		// Token: 0x04000937 RID: 2359
		public FullNamedExpression Type;

		// Token: 0x04000938 RID: 2360
		private TypeSpec type;
	}
}
