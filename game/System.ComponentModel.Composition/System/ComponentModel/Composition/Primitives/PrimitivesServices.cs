using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition.ReflectionModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Composition.Primitives
{
	// Token: 0x0200009A RID: 154
	internal static class PrimitivesServices
	{
		// Token: 0x06000405 RID: 1029 RVA: 0x0000B673 File Offset: 0x00009873
		public static bool IsGeneric(this ComposablePartDefinition part)
		{
			return part.Metadata.GetValue("System.ComponentModel.Composition.IsGenericPart");
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000B688 File Offset: 0x00009888
		public static ImportDefinition GetProductImportDefinition(this ImportDefinition import)
		{
			IPartCreatorImportDefinition partCreatorImportDefinition = import as IPartCreatorImportDefinition;
			if (partCreatorImportDefinition != null)
			{
				return partCreatorImportDefinition.ProductImportDefinition;
			}
			return import;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000B6A7 File Offset: 0x000098A7
		internal static IEnumerable<string> GetCandidateContractNames(this ImportDefinition import, ComposablePartDefinition part)
		{
			import = import.GetProductImportDefinition();
			string text = import.ContractName;
			string genericContractName = import.Metadata.GetValue("System.ComponentModel.Composition.GenericContractName");
			int[] value = import.Metadata.GetValue("System.ComponentModel.Composition.GenericImportParametersOrderMetadataName");
			if (value != null)
			{
				int value2 = part.Metadata.GetValue("System.ComponentModel.Composition.GenericPartArity");
				if (value2 > 0)
				{
					text = GenericServices.GetGenericName(text, value, value2);
				}
			}
			yield return text;
			if (!string.IsNullOrEmpty(genericContractName))
			{
				yield return genericContractName;
			}
			yield break;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000B6BE File Offset: 0x000098BE
		internal static bool IsImportDependentOnPart(this ImportDefinition import, ComposablePartDefinition part, ExportDefinition export, bool expandGenerics)
		{
			import = import.GetProductImportDefinition();
			if (expandGenerics)
			{
				return part.GetExports(import).Any<Tuple<ComposablePartDefinition, ExportDefinition>>();
			}
			return PrimitivesServices.TranslateImport(import, part).IsConstraintSatisfiedBy(export);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000B6E8 File Offset: 0x000098E8
		private static ImportDefinition TranslateImport(ImportDefinition import, ComposablePartDefinition part)
		{
			ContractBasedImportDefinition contractBasedImportDefinition = import as ContractBasedImportDefinition;
			if (contractBasedImportDefinition == null)
			{
				return import;
			}
			int[] value = contractBasedImportDefinition.Metadata.GetValue("System.ComponentModel.Composition.GenericImportParametersOrderMetadataName");
			if (value == null)
			{
				return import;
			}
			int value2 = part.Metadata.GetValue("System.ComponentModel.Composition.GenericPartArity");
			if (value2 == 0)
			{
				return import;
			}
			string genericName = GenericServices.GetGenericName(contractBasedImportDefinition.ContractName, value, value2);
			string genericName2 = GenericServices.GetGenericName(contractBasedImportDefinition.RequiredTypeIdentity, value, value2);
			return new ContractBasedImportDefinition(genericName, genericName2, contractBasedImportDefinition.RequiredMetadata, contractBasedImportDefinition.Cardinality, contractBasedImportDefinition.IsRecomposable, false, contractBasedImportDefinition.RequiredCreationPolicy, contractBasedImportDefinition.Metadata);
		}

		// Token: 0x0200009B RID: 155
		[CompilerGenerated]
		private sealed class <GetCandidateContractNames>d__2 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
		{
			// Token: 0x0600040A RID: 1034 RVA: 0x0000B76D File Offset: 0x0000996D
			[DebuggerHidden]
			public <GetCandidateContractNames>d__2(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600040B RID: 1035 RVA: 0x000028FF File Offset: 0x00000AFF
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600040C RID: 1036 RVA: 0x0000B788 File Offset: 0x00009988
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
				{
					this.<>1__state = -1;
					import = import.GetProductImportDefinition();
					string originalGenericName = import.ContractName;
					genericContractName = import.Metadata.GetValue("System.ComponentModel.Composition.GenericContractName");
					int[] value = import.Metadata.GetValue("System.ComponentModel.Composition.GenericImportParametersOrderMetadataName");
					if (value != null)
					{
						int value2 = part.Metadata.GetValue("System.ComponentModel.Composition.GenericPartArity");
						if (value2 > 0)
						{
							originalGenericName = GenericServices.GetGenericName(originalGenericName, value, value2);
						}
					}
					this.<>2__current = originalGenericName;
					this.<>1__state = 1;
					return true;
				}
				case 1:
					this.<>1__state = -1;
					if (!string.IsNullOrEmpty(genericContractName))
					{
						this.<>2__current = genericContractName;
						this.<>1__state = 2;
						return true;
					}
					break;
				case 2:
					this.<>1__state = -1;
					break;
				default:
					return false;
				}
				return false;
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000B86C File Offset: 0x00009A6C
			string IEnumerator<string>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600040E RID: 1038 RVA: 0x00002C6B File Offset: 0x00000E6B
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000B86C File Offset: 0x00009A6C
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000410 RID: 1040 RVA: 0x0000B874 File Offset: 0x00009A74
			[DebuggerHidden]
			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				PrimitivesServices.<GetCandidateContractNames>d__2 <GetCandidateContractNames>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetCandidateContractNames>d__ = this;
				}
				else
				{
					<GetCandidateContractNames>d__ = new PrimitivesServices.<GetCandidateContractNames>d__2(0);
				}
				<GetCandidateContractNames>d__.import = import;
				<GetCandidateContractNames>d__.part = part;
				return <GetCandidateContractNames>d__;
			}

			// Token: 0x06000411 RID: 1041 RVA: 0x0000B8C3 File Offset: 0x00009AC3
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();
			}

			// Token: 0x04000195 RID: 405
			private int <>1__state;

			// Token: 0x04000196 RID: 406
			private string <>2__current;

			// Token: 0x04000197 RID: 407
			private int <>l__initialThreadId;

			// Token: 0x04000198 RID: 408
			private ImportDefinition import;

			// Token: 0x04000199 RID: 409
			public ImportDefinition <>3__import;

			// Token: 0x0400019A RID: 410
			private ComposablePartDefinition part;

			// Token: 0x0400019B RID: 411
			public ComposablePartDefinition <>3__part;

			// Token: 0x0400019C RID: 412
			private string <genericContractName>5__2;
		}
	}
}
