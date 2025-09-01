using System;
using System.Collections.Generic;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000C5 RID: 197
	internal sealed class MethodSemanticsTable : SortedTable<MethodSemanticsTable.Record>
	{
		// Token: 0x0600093F RID: 2367 RVA: 0x000200BC File Offset: 0x0001E2BC
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Semantics = mr.ReadInt16();
				this.records[i].Method = mr.ReadMethodDef();
				this.records[i].Association = mr.ReadHasSemantics();
			}
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00020124 File Offset: 0x0001E324
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.Write(this.records[i].Semantics);
				mw.WriteMethodDef(this.records[i].Method);
				mw.WriteHasSemantics(this.records[i].Association);
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00020187 File Offset: 0x0001E387
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(2).WriteMethodDef().WriteHasSemantics().Value;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x000201A0 File Offset: 0x0001E3A0
		internal void Fixup(ModuleBuilder moduleBuilder)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				moduleBuilder.FixupPseudoToken(ref this.records[i].Method);
				int num = this.records[i].Association;
				int num2 = num >> 24;
				if (num2 != 20)
				{
					if (num2 != 23)
					{
						throw new InvalidOperationException();
					}
					num = ((num & 16777215) << 1 | 1);
				}
				else
				{
					num = ((num & 16777215) << 1 | 0);
				}
				this.records[i].Association = num;
			}
			base.Sort();
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00020234 File Offset: 0x0001E434
		internal MethodInfo GetMethod(Module module, int token, bool nonPublic, short semantics)
		{
			foreach (int num in base.Filter(token))
			{
				if ((this.records[num].Semantics & semantics) != 0)
				{
					MethodBase methodBase = module.ResolveMethod((6 << 24) + this.records[num].Method);
					if (nonPublic || methodBase.IsPublic)
					{
						return (MethodInfo)methodBase;
					}
				}
			}
			return null;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x000202AC File Offset: 0x0001E4AC
		internal MethodInfo[] GetMethods(Module module, int token, bool nonPublic, short semantics)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			foreach (int num in base.Filter(token))
			{
				if ((this.records[num].Semantics & semantics) != 0)
				{
					MethodInfo methodInfo = (MethodInfo)module.ResolveMethod((6 << 24) + this.records[num].Method);
					if (nonPublic || methodInfo.IsPublic)
					{
						list.Add(methodInfo);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00020338 File Offset: 0x0001E538
		internal void ComputeFlags(Module module, int token, out bool isPublic, out bool isNonPrivate, out bool isStatic)
		{
			isPublic = false;
			isNonPrivate = false;
			isStatic = false;
			foreach (int num in base.Filter(token))
			{
				MethodBase methodBase = module.ResolveMethod((6 << 24) + this.records[num].Method);
				isPublic |= methodBase.IsPublic;
				isNonPrivate |= ((methodBase.Attributes & MethodAttributes.MemberAccessMask) > MethodAttributes.Private);
				isStatic |= methodBase.IsStatic;
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000203B8 File Offset: 0x0001E5B8
		public MethodSemanticsTable()
		{
		}

		// Token: 0x040003E4 RID: 996
		internal const int Index = 24;

		// Token: 0x040003E5 RID: 997
		internal const short Setter = 1;

		// Token: 0x040003E6 RID: 998
		internal const short Getter = 2;

		// Token: 0x040003E7 RID: 999
		internal const short Other = 4;

		// Token: 0x040003E8 RID: 1000
		internal const short AddOn = 8;

		// Token: 0x040003E9 RID: 1001
		internal const short RemoveOn = 16;

		// Token: 0x040003EA RID: 1002
		internal const short Fire = 32;

		// Token: 0x02000356 RID: 854
		internal struct Record : SortedTable<MethodSemanticsTable.Record>.IRecord
		{
			// Token: 0x170008B8 RID: 2232
			// (get) Token: 0x0600262E RID: 9774 RVA: 0x000B569F File Offset: 0x000B389F
			int SortedTable<MethodSemanticsTable.Record>.IRecord.SortKey
			{
				get
				{
					return this.Association;
				}
			}

			// Token: 0x170008B9 RID: 2233
			// (get) Token: 0x0600262F RID: 9775 RVA: 0x000B569F File Offset: 0x000B389F
			int SortedTable<MethodSemanticsTable.Record>.IRecord.FilterKey
			{
				get
				{
					return this.Association;
				}
			}

			// Token: 0x04000ECE RID: 3790
			internal short Semantics;

			// Token: 0x04000ECF RID: 3791
			internal int Method;

			// Token: 0x04000ED0 RID: 3792
			internal int Association;
		}
	}
}
