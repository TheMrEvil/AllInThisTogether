using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x020002E4 RID: 740
	public class ArrayContainer : ElementTypeSpec
	{
		// Token: 0x06002357 RID: 9047 RVA: 0x000ACBC2 File Offset: 0x000AADC2
		private ArrayContainer(ModuleContainer module, TypeSpec element, int rank) : base(MemberKind.ArrayType, element, null)
		{
			this.module = module;
			this.rank = rank;
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002358 RID: 9048 RVA: 0x000ACBDF File Offset: 0x000AADDF
		public int Rank
		{
			get
			{
				return this.rank;
			}
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000ACBE8 File Offset: 0x000AADE8
		public MethodInfo GetConstructor()
		{
			ModuleBuilder builder = this.module.Builder;
			Type[] array = new Type[this.rank];
			for (int i = 0; i < this.rank; i++)
			{
				array[i] = this.module.Compiler.BuiltinTypes.Int.GetMetaInfo();
			}
			return builder.GetArrayMethod(this.GetMetaInfo(), Constructor.ConstructorName, CallingConventions.HasThis, null, array);
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x000ACC50 File Offset: 0x000AAE50
		public MethodInfo GetAddressMethod()
		{
			ModuleBuilder builder = this.module.Builder;
			Type[] array = new Type[this.rank];
			for (int i = 0; i < this.rank; i++)
			{
				array[i] = this.module.Compiler.BuiltinTypes.Int.GetMetaInfo();
			}
			return builder.GetArrayMethod(this.GetMetaInfo(), "Address", CallingConventions.Standard | CallingConventions.HasThis, ReferenceContainer.MakeType(this.module, base.Element).GetMetaInfo(), array);
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000ACCD0 File Offset: 0x000AAED0
		public MethodInfo GetGetMethod()
		{
			ModuleBuilder builder = this.module.Builder;
			Type[] array = new Type[this.rank];
			for (int i = 0; i < this.rank; i++)
			{
				array[i] = this.module.Compiler.BuiltinTypes.Int.GetMetaInfo();
			}
			return builder.GetArrayMethod(this.GetMetaInfo(), "Get", CallingConventions.Standard | CallingConventions.HasThis, base.Element.GetMetaInfo(), array);
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000ACD44 File Offset: 0x000AAF44
		public MethodInfo GetSetMethod()
		{
			ModuleBuilder builder = this.module.Builder;
			Type[] array = new Type[this.rank + 1];
			for (int i = 0; i < this.rank; i++)
			{
				array[i] = this.module.Compiler.BuiltinTypes.Int.GetMetaInfo();
			}
			array[this.rank] = base.Element.GetMetaInfo();
			return builder.GetArrayMethod(this.GetMetaInfo(), "Set", CallingConventions.Standard | CallingConventions.HasThis, this.module.Compiler.BuiltinTypes.Void.GetMetaInfo(), array);
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000ACDDC File Offset: 0x000AAFDC
		public override Type GetMetaInfo()
		{
			if (this.info == null)
			{
				if (this.rank == 1)
				{
					this.info = base.Element.GetMetaInfo().MakeArrayType();
				}
				else
				{
					this.info = base.Element.GetMetaInfo().MakeArrayType(this.rank);
				}
			}
			return this.info;
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x000ACE34 File Offset: 0x000AB034
		protected override string GetPostfixSignature()
		{
			return ArrayContainer.GetPostfixSignature(this.rank);
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x000ACE44 File Offset: 0x000AB044
		public static string GetPostfixSignature(int rank)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			for (int i = 1; i < rank; i++)
			{
				stringBuilder.Append(",");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000ACE90 File Offset: 0x000AB090
		public override string GetSignatureForDocumentation(bool explicitName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.GetElementSignatureForDocumentation(stringBuilder, explicitName);
			return stringBuilder.ToString();
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000ACEB4 File Offset: 0x000AB0B4
		private void GetElementSignatureForDocumentation(StringBuilder sb, bool explicitName)
		{
			ArrayContainer arrayContainer = base.Element as ArrayContainer;
			if (arrayContainer == null)
			{
				sb.Append(base.Element.GetSignatureForDocumentation(explicitName));
			}
			else
			{
				arrayContainer.GetElementSignatureForDocumentation(sb, explicitName);
			}
			if (explicitName)
			{
				sb.Append(ArrayContainer.GetPostfixSignature(this.rank));
				return;
			}
			sb.Append("[");
			for (int i = 1; i < this.rank; i++)
			{
				if (i == 1)
				{
					sb.Append("0:");
				}
				sb.Append(",0:");
			}
			sb.Append("]");
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x000ACF48 File Offset: 0x000AB148
		public static ArrayContainer MakeType(ModuleContainer module, TypeSpec element)
		{
			return ArrayContainer.MakeType(module, element, 1);
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x000ACF54 File Offset: 0x000AB154
		public static ArrayContainer MakeType(ModuleContainer module, TypeSpec element, int rank)
		{
			ArrayContainer.TypeRankPair key = new ArrayContainer.TypeRankPair(element, rank);
			ArrayContainer arrayContainer;
			if (!module.ArrayTypesCache.TryGetValue(key, out arrayContainer))
			{
				arrayContainer = new ArrayContainer(module, element, rank);
				arrayContainer.BaseType = module.Compiler.BuiltinTypes.Array;
				ArrayContainer arrayContainer2 = arrayContainer;
				arrayContainer2.Interfaces = arrayContainer2.BaseType.Interfaces;
				module.ArrayTypesCache.Add(key, arrayContainer);
			}
			return arrayContainer;
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000ACFB8 File Offset: 0x000AB1B8
		public override List<MissingTypeSpecReference> ResolveMissingDependencies(MemberSpec caller)
		{
			return base.Element.ResolveMissingDependencies(caller);
		}

		// Token: 0x04000D7B RID: 3451
		private readonly int rank;

		// Token: 0x04000D7C RID: 3452
		private readonly ModuleContainer module;

		// Token: 0x0200040A RID: 1034
		public struct TypeRankPair : IEquatable<ArrayContainer.TypeRankPair>
		{
			// Token: 0x06002846 RID: 10310 RVA: 0x000BF1B6 File Offset: 0x000BD3B6
			public TypeRankPair(TypeSpec ts, int rank)
			{
				this.ts = ts;
				this.rank = rank;
			}

			// Token: 0x06002847 RID: 10311 RVA: 0x000BF1C6 File Offset: 0x000BD3C6
			public override int GetHashCode()
			{
				return this.ts.GetHashCode() ^ this.rank.GetHashCode();
			}

			// Token: 0x06002848 RID: 10312 RVA: 0x000BF1DF File Offset: 0x000BD3DF
			public bool Equals(ArrayContainer.TypeRankPair other)
			{
				return other.ts == this.ts && other.rank == this.rank;
			}

			// Token: 0x0400118D RID: 4493
			private TypeSpec ts;

			// Token: 0x0400118E RID: 4494
			private int rank;
		}
	}
}
