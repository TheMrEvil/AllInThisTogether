using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x0200027E RID: 638
	public class IndexerSpec : PropertySpec, IParametersMember, IInterfaceMemberSpec
	{
		// Token: 0x06001F40 RID: 8000 RVA: 0x00099DC8 File Offset: 0x00097FC8
		public IndexerSpec(TypeSpec declaringType, IMemberDefinition definition, TypeSpec memberType, AParametersCollection parameters, PropertyInfo info, Modifiers modifiers) : base(MemberKind.Indexer, declaringType, definition, memberType, info, modifiers)
		{
			this.parameters = parameters;
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x00099DE1 File Offset: 0x00097FE1
		public AParametersCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x00099DE9 File Offset: 0x00097FE9
		public override string GetSignatureForDocumentation()
		{
			return base.GetSignatureForDocumentation() + this.parameters.GetSignatureForDocumentation();
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x00099E01 File Offset: 0x00098001
		public override string GetSignatureForError()
		{
			return base.DeclaringType.GetSignatureForError() + ".this" + this.parameters.GetSignatureForError("[", "]", this.parameters.Count);
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00099E38 File Offset: 0x00098038
		public override MemberSpec InflateMember(TypeParameterInflator inflator)
		{
			IndexerSpec indexerSpec = (IndexerSpec)base.InflateMember(inflator);
			indexerSpec.parameters = this.parameters.Inflate(inflator);
			return indexerSpec;
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x00099E58 File Offset: 0x00098058
		public override List<MissingTypeSpecReference> ResolveMissingDependencies(MemberSpec caller)
		{
			List<MissingTypeSpecReference> list = base.ResolveMissingDependencies(caller);
			TypeSpec[] types = this.parameters.Types;
			for (int i = 0; i < types.Length; i++)
			{
				List<MissingTypeSpecReference> missingDependencies = types[i].GetMissingDependencies(caller);
				if (missingDependencies != null)
				{
					if (list == null)
					{
						list = new List<MissingTypeSpecReference>();
					}
					list.AddRange(missingDependencies);
				}
			}
			return list;
		}

		// Token: 0x04000B7A RID: 2938
		private AParametersCollection parameters;
	}
}
