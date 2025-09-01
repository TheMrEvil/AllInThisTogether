using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000267 RID: 615
	public class ReturnParameter : ParameterBase
	{
		// Token: 0x06001E4D RID: 7757 RVA: 0x00095748 File Offset: 0x00093948
		public ReturnParameter(MemberCore method, MethodBuilder mb, Location location)
		{
			this.method = method;
			try
			{
				this.builder = mb.DefineParameter(0, ParameterAttributes.None, "");
			}
			catch (ArgumentOutOfRangeException)
			{
				method.Compiler.Report.RuntimeMissingSupport(location, "custom attributes on the return type");
			}
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x000957A0 File Offset: 0x000939A0
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.Type == pa.CLSCompliant)
			{
				this.method.Compiler.Report.Warning(3023, 1, a.Location, "CLSCompliant attribute has no meaning when applied to return types. Try putting it on the method instead");
			}
			if (this.builder == null)
			{
				return;
			}
			base.ApplyAttributeBuilder(a, ctor, cdata, pa);
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001E4F RID: 7759 RVA: 0x000957FB File Offset: 0x000939FB
		public override AttributeTargets AttributeTargets
		{
			get
			{
				return AttributeTargets.ReturnValue;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001E50 RID: 7760 RVA: 0x000055E7 File Offset: 0x000037E7
		public override string[] ValidAttributeTargets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000B3F RID: 2879
		private MemberCore method;
	}
}
