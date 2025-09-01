using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Metadata;

namespace IKVM.Reflection.Reader
{
	// Token: 0x02000094 RID: 148
	internal sealed class FieldDefImpl : FieldInfo
	{
		// Token: 0x060007A8 RID: 1960 RVA: 0x000194EC File Offset: 0x000176EC
		internal FieldDefImpl(ModuleReader module, TypeDefImpl declaringType, int index)
		{
			this.module = module;
			this.declaringType = declaringType;
			this.index = index;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00019509 File Offset: 0x00017709
		public override FieldAttributes Attributes
		{
			get
			{
				return (FieldAttributes)this.module.Field.records[this.index].Flags;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0001952B File Offset: 0x0001772B
		public override Type DeclaringType
		{
			get
			{
				if (!this.declaringType.IsModulePseudoType)
				{
					return this.declaringType;
				}
				return null;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00019542 File Offset: 0x00017742
		public override string Name
		{
			get
			{
				return this.module.GetString(this.module.Field.records[this.index].Name);
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0000A673 File Offset: 0x00008873
		public override string ToString()
		{
			return base.FieldType.Name + " " + this.Name;
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0001956F File Offset: 0x0001776F
		public override Module Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00019577 File Offset: 0x00017777
		public override int MetadataToken
		{
			get
			{
				return (4 << 24) + this.index + 1;
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00019586 File Offset: 0x00017786
		public override object GetRawConstantValue()
		{
			return this.module.Constant.GetRawConstantValue(this.module, this.MetadataToken);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x000195A4 File Offset: 0x000177A4
		public override void __GetDataFromRVA(byte[] data, int offset, int length)
		{
			int _FieldRVA = this.__FieldRVA;
			if (_FieldRVA == 0)
			{
				Array.Clear(data, offset, length);
				return;
			}
			this.module.__ReadDataFromRVA(_FieldRVA, data, offset, length);
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x000195D4 File Offset: 0x000177D4
		public override int __FieldRVA
		{
			get
			{
				SortedTable<FieldRVATable.Record>.Enumerator enumerator = this.module.FieldRVA.Filter(this.index + 1).GetEnumerator();
				if (!enumerator.MoveNext())
				{
					throw new InvalidOperationException();
				}
				int num = enumerator.Current;
				return this.module.FieldRVA.records[num].RVA;
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00019638 File Offset: 0x00017838
		public override bool __TryGetFieldOffset(out int offset)
		{
			SortedTable<FieldLayoutTable.Record>.Enumerator enumerator = this.Module.FieldLayout.Filter(this.index + 1).GetEnumerator();
			if (!enumerator.MoveNext())
			{
				offset = 0;
				return false;
			}
			int num = enumerator.Current;
			offset = this.Module.FieldLayout.records[num].Offset;
			return true;
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0001969C File Offset: 0x0001789C
		internal override FieldSignature FieldSignature
		{
			get
			{
				FieldSignature result;
				if ((result = this.lazyFieldSig) == null)
				{
					result = (this.lazyFieldSig = FieldSignature.ReadSig(this.module, this.module.GetBlob(this.module.Field.records[this.index].Signature), this.declaringType));
				}
				return result;
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x000196F8 File Offset: 0x000178F8
		internal override int ImportTo(ModuleBuilder module)
		{
			return module.ImportMethodOrField(this.declaringType, this.Name, this.FieldSignature);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00010856 File Offset: 0x0000EA56
		internal override int GetCurrentToken()
		{
			return this.MetadataToken;
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0000212D File Offset: 0x0000032D
		internal override bool IsBaked
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400030E RID: 782
		private readonly ModuleReader module;

		// Token: 0x0400030F RID: 783
		private readonly TypeDefImpl declaringType;

		// Token: 0x04000310 RID: 784
		private readonly int index;

		// Token: 0x04000311 RID: 785
		private FieldSignature lazyFieldSig;
	}
}
