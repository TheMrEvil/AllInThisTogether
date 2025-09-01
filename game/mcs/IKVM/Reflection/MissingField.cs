using System;
using IKVM.Reflection.Emit;

namespace IKVM.Reflection
{
	// Token: 0x02000049 RID: 73
	internal sealed class MissingField : FieldInfo
	{
		// Token: 0x0600031D RID: 797 RVA: 0x0000A47D File Offset: 0x0000867D
		internal MissingField(Type declaringType, string name, FieldSignature signature)
		{
			this.declaringType = declaringType;
			this.name = name;
			this.signature = signature;
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000A49A File Offset: 0x0000869A
		private FieldInfo Forwarder
		{
			get
			{
				FieldInfo fieldInfo = this.TryGetForwarder();
				if (fieldInfo == null)
				{
					throw new MissingMemberException(this);
				}
				return fieldInfo;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000A4B2 File Offset: 0x000086B2
		private FieldInfo TryGetForwarder()
		{
			if (this.forwarder == null && !this.declaringType.__IsMissing)
			{
				this.forwarder = this.declaringType.FindField(this.name, this.signature);
			}
			return this.forwarder;
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000A4F2 File Offset: 0x000086F2
		public override bool __IsMissing
		{
			get
			{
				return this.TryGetForwarder() == null;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000A500 File Offset: 0x00008700
		public override FieldAttributes Attributes
		{
			get
			{
				return this.Forwarder.Attributes;
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000A50D File Offset: 0x0000870D
		public override void __GetDataFromRVA(byte[] data, int offset, int length)
		{
			this.Forwarder.__GetDataFromRVA(data, offset, length);
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000A51D File Offset: 0x0000871D
		public override int __FieldRVA
		{
			get
			{
				return this.Forwarder.__FieldRVA;
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000A52A File Offset: 0x0000872A
		public override bool __TryGetFieldOffset(out int offset)
		{
			return this.Forwarder.__TryGetFieldOffset(out offset);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000A538 File Offset: 0x00008738
		public override object GetRawConstantValue()
		{
			return this.Forwarder.GetRawConstantValue();
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000A545 File Offset: 0x00008745
		internal override FieldSignature FieldSignature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000A550 File Offset: 0x00008750
		internal override int ImportTo(ModuleBuilder module)
		{
			FieldInfo fieldInfo = this.TryGetForwarder();
			if (fieldInfo != null)
			{
				return fieldInfo.ImportTo(module);
			}
			return module.ImportMethodOrField(this.declaringType, this.Name, this.FieldSignature);
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000A58D File Offset: 0x0000878D
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000A595 File Offset: 0x00008795
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

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000A5AC File Offset: 0x000087AC
		public override Module Module
		{
			get
			{
				return this.declaringType.Module;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000A5BC File Offset: 0x000087BC
		internal override FieldInfo BindTypeParameters(Type type)
		{
			FieldInfo fieldInfo = this.TryGetForwarder();
			if (fieldInfo != null)
			{
				return fieldInfo.BindTypeParameters(type);
			}
			return new GenericFieldInstance(type, this);
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000A5E8 File Offset: 0x000087E8
		public override int MetadataToken
		{
			get
			{
				return this.Forwarder.MetadataToken;
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000A5F8 File Offset: 0x000087F8
		public override bool Equals(object obj)
		{
			MissingField missingField = obj as MissingField;
			return missingField != null && missingField.declaringType == this.declaringType && missingField.name == this.name && missingField.signature.Equals(this.signature);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000A64E File Offset: 0x0000884E
		public override int GetHashCode()
		{
			return this.declaringType.GetHashCode() ^ this.name.GetHashCode() ^ this.signature.GetHashCode();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000A673 File Offset: 0x00008873
		public override string ToString()
		{
			return base.FieldType.Name + " " + this.Name;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000A690 File Offset: 0x00008890
		internal override int GetCurrentToken()
		{
			return this.Forwarder.GetCurrentToken();
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000A69D File Offset: 0x0000889D
		internal override bool IsBaked
		{
			get
			{
				return this.Forwarder.IsBaked;
			}
		}

		// Token: 0x04000182 RID: 386
		private readonly Type declaringType;

		// Token: 0x04000183 RID: 387
		private readonly string name;

		// Token: 0x04000184 RID: 388
		private readonly FieldSignature signature;

		// Token: 0x04000185 RID: 389
		private FieldInfo forwarder;
	}
}
