using System;
using IKVM.Reflection.Metadata;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000E4 RID: 228
	public sealed class FieldBuilder : FieldInfo
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x00023C3C File Offset: 0x00021E3C
		internal FieldBuilder(TypeBuilder type, string name, Type fieldType, CustomModifiers customModifiers, FieldAttributes attribs)
		{
			this.typeBuilder = type;
			this.name = name;
			this.pseudoToken = type.ModuleBuilder.AllocPseudoToken();
			this.nameIndex = type.ModuleBuilder.Strings.Add(name);
			this.fieldSig = FieldSignature.Create(fieldType, customModifiers);
			ByteBuffer bb = new ByteBuffer(5);
			this.fieldSig.WriteSig(this.typeBuilder.ModuleBuilder, bb);
			this.signature = this.typeBuilder.ModuleBuilder.Blobs.Add(bb);
			this.attribs = attribs;
			this.typeBuilder.ModuleBuilder.Field.AddVirtualRecord();
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00023CEB File Offset: 0x00021EEB
		public void SetConstant(object defaultValue)
		{
			this.attribs |= FieldAttributes.HasDefault;
			this.typeBuilder.ModuleBuilder.AddConstant(this.pseudoToken, defaultValue);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00023D16 File Offset: 0x00021F16
		public override object GetRawConstantValue()
		{
			if (!this.typeBuilder.IsCreated())
			{
				throw new NotSupportedException();
			}
			return this.typeBuilder.Module.Constant.GetRawConstantValue(this.typeBuilder.Module, this.GetCurrentToken());
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00023D51 File Offset: 0x00021F51
		public void __SetDataAndRVA(byte[] data)
		{
			this.SetDataAndRvaImpl(data, this.typeBuilder.ModuleBuilder.initializedData, 0);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00023D6B File Offset: 0x00021F6B
		public void __SetReadOnlyDataAndRVA(byte[] data)
		{
			this.SetDataAndRvaImpl(data, this.typeBuilder.ModuleBuilder.methodBodies, int.MinValue);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00023D8C File Offset: 0x00021F8C
		private void SetDataAndRvaImpl(byte[] data, ByteBuffer bb, int readonlyMarker)
		{
			this.attribs |= FieldAttributes.HasFieldRVA;
			FieldRVATable.Record newRecord = default(FieldRVATable.Record);
			bb.Align(8);
			newRecord.RVA = bb.Position + readonlyMarker;
			newRecord.Field = this.pseudoToken;
			this.typeBuilder.ModuleBuilder.FieldRVA.AddRecord(newRecord);
			bb.Write(data);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override void __GetDataFromRVA(byte[] data, int offset, int length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override int __FieldRVA
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00023DFC File Offset: 0x00021FFC
		public override bool __TryGetFieldOffset(out int offset)
		{
			int token = this.pseudoToken;
			if (this.typeBuilder.ModuleBuilder.IsSaved)
			{
				token = (this.typeBuilder.ModuleBuilder.ResolvePseudoToken(this.pseudoToken) & 16777215);
			}
			SortedTable<FieldLayoutTable.Record>.Enumerator enumerator = this.Module.FieldLayout.Filter(token).GetEnumerator();
			if (!enumerator.MoveNext())
			{
				offset = 0;
				return false;
			}
			int num = enumerator.Current;
			offset = this.Module.FieldLayout.records[num].Offset;
			return true;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00023E8F File Offset: 0x0002208F
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00023EA0 File Offset: 0x000220A0
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			KnownCA knownCA = customBuilder.KnownCA;
			if (knownCA <= KnownCA.MarshalAsAttribute)
			{
				if (knownCA == KnownCA.NonSerializedAttribute)
				{
					this.attribs |= FieldAttributes.NotSerialized;
					return;
				}
				if (knownCA == KnownCA.MarshalAsAttribute)
				{
					FieldMarshal.SetMarshalAsAttribute(this.typeBuilder.ModuleBuilder, this.pseudoToken, customBuilder);
					this.attribs |= FieldAttributes.HasFieldMarshal;
					return;
				}
			}
			else
			{
				if (knownCA == KnownCA.FieldOffsetAttribute)
				{
					this.SetOffset((int)customBuilder.DecodeBlob(this.Module.Assembly).GetConstructorArgument(0));
					return;
				}
				if (knownCA == KnownCA.SpecialNameAttribute)
				{
					this.attribs |= FieldAttributes.SpecialName;
					return;
				}
			}
			this.typeBuilder.ModuleBuilder.SetCustomAttribute(this.pseudoToken, customBuilder);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00023F58 File Offset: 0x00022158
		public void SetOffset(int iOffset)
		{
			FieldLayoutTable.Record newRecord = default(FieldLayoutTable.Record);
			newRecord.Offset = iOffset;
			newRecord.Field = this.pseudoToken;
			this.typeBuilder.ModuleBuilder.FieldLayout.AddRecord(newRecord);
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00023F99 File Offset: 0x00022199
		public override FieldAttributes Attributes
		{
			get
			{
				return this.attribs;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00023FA1 File Offset: 0x000221A1
		public override Type DeclaringType
		{
			get
			{
				if (!this.typeBuilder.IsModulePseudoType)
				{
					return this.typeBuilder;
				}
				return null;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x00023FB8 File Offset: 0x000221B8
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00023FC0 File Offset: 0x000221C0
		public override int MetadataToken
		{
			get
			{
				return this.pseudoToken;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00023FC8 File Offset: 0x000221C8
		public override Module Module
		{
			get
			{
				return this.typeBuilder.Module;
			}
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00023FD5 File Offset: 0x000221D5
		public FieldToken GetToken()
		{
			return new FieldToken(this.pseudoToken);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00023FE2 File Offset: 0x000221E2
		internal void WriteFieldRecords(MetadataWriter mw)
		{
			mw.Write((short)this.attribs);
			mw.WriteStringIndex(this.nameIndex);
			mw.WriteBlobIndex(this.signature);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00024009 File Offset: 0x00022209
		internal void FixupToken(int token)
		{
			this.typeBuilder.ModuleBuilder.RegisterTokenFixup(this.pseudoToken, token);
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00024022 File Offset: 0x00022222
		internal override FieldSignature FieldSignature
		{
			get
			{
				return this.fieldSig;
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002402A File Offset: 0x0002222A
		internal override int ImportTo(ModuleBuilder other)
		{
			return other.ImportMethodOrField(this.typeBuilder, this.name, this.fieldSig);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00024044 File Offset: 0x00022244
		internal override int GetCurrentToken()
		{
			if (this.typeBuilder.ModuleBuilder.IsSaved)
			{
				return this.typeBuilder.ModuleBuilder.ResolvePseudoToken(this.pseudoToken);
			}
			return this.pseudoToken;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00024075 File Offset: 0x00022275
		internal override bool IsBaked
		{
			get
			{
				return this.typeBuilder.IsBaked;
			}
		}

		// Token: 0x04000493 RID: 1171
		private readonly TypeBuilder typeBuilder;

		// Token: 0x04000494 RID: 1172
		private readonly string name;

		// Token: 0x04000495 RID: 1173
		private readonly int pseudoToken;

		// Token: 0x04000496 RID: 1174
		private FieldAttributes attribs;

		// Token: 0x04000497 RID: 1175
		private readonly int nameIndex;

		// Token: 0x04000498 RID: 1176
		private readonly int signature;

		// Token: 0x04000499 RID: 1177
		private readonly FieldSignature fieldSig;
	}
}
