using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000266 RID: 614
	public abstract class ParameterBase : Attributable
	{
		// Token: 0x06001E49 RID: 7753 RVA: 0x000956E8 File Offset: 0x000938E8
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.HasSecurityAttribute)
			{
				a.Error_InvalidSecurityParent();
				return;
			}
			if (a.Type == pa.Dynamic)
			{
				a.Error_MisusedDynamicAttribute();
				return;
			}
			this.builder.SetCustomAttribute((ConstructorInfo)ctor.GetMetaInfo(), cdata);
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x00095736 File Offset: 0x00093936
		public ParameterBuilder Builder
		{
			get
			{
				return this.builder;
			}
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsClsComplianceRequired()
		{
			return false;
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x0009573E File Offset: 0x0009393E
		protected ParameterBase()
		{
		}

		// Token: 0x04000B3E RID: 2878
		protected ParameterBuilder builder;
	}
}
