using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200021A RID: 538
	public class VarianceDecl
	{
		// Token: 0x06001B62 RID: 7010 RVA: 0x00084DC1 File Offset: 0x00082FC1
		public VarianceDecl(Variance variance, Location loc)
		{
			this.Variance = variance;
			this.Location = loc;
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x00084DD7 File Offset: 0x00082FD7
		// (set) Token: 0x06001B64 RID: 7012 RVA: 0x00084DDF File Offset: 0x00082FDF
		public Variance Variance
		{
			[CompilerGenerated]
			get
			{
				return this.<Variance>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Variance>k__BackingField = value;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x00084DE8 File Offset: 0x00082FE8
		// (set) Token: 0x06001B66 RID: 7014 RVA: 0x00084DF0 File Offset: 0x00082FF0
		public Location Location
		{
			[CompilerGenerated]
			get
			{
				return this.<Location>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Location>k__BackingField = value;
			}
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x00084DFC File Offset: 0x00082FFC
		public static Variance CheckTypeVariance(TypeSpec t, Variance expected, IMemberContext member)
		{
			TypeParameterSpec typeParameterSpec = t as TypeParameterSpec;
			if (typeParameterSpec != null)
			{
				Variance variance = typeParameterSpec.Variance;
				if ((expected == Variance.None && variance != expected) || (expected == Variance.Covariant && variance == Variance.Contravariant) || (expected == Variance.Contravariant && variance == Variance.Covariant))
				{
					((TypeParameter)typeParameterSpec.MemberDefinition).ErrorInvalidVariance(member, expected);
				}
				return expected;
			}
			if (t.TypeArguments.Length != 0)
			{
				TypeParameterSpec[] typeParameters = t.MemberDefinition.TypeParameters;
				TypeSpec[] typeArguments = TypeManager.GetTypeArguments(t);
				for (int i = 0; i < typeArguments.Length; i++)
				{
					Variance variance2 = typeParameters[i].Variance;
					VarianceDecl.CheckTypeVariance(typeArguments[i], variance2 * expected, member);
				}
				return expected;
			}
			ArrayContainer arrayContainer = t as ArrayContainer;
			if (arrayContainer != null)
			{
				return VarianceDecl.CheckTypeVariance(arrayContainer.Element, expected, member);
			}
			return Variance.None;
		}

		// Token: 0x04000A32 RID: 2610
		[CompilerGenerated]
		private Variance <Variance>k__BackingField;

		// Token: 0x04000A33 RID: 2611
		[CompilerGenerated]
		private Location <Location>k__BackingField;
	}
}
