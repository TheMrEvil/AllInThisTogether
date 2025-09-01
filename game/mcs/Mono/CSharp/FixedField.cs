using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.CSharp
{
	// Token: 0x020002DA RID: 730
	public class FixedField : FieldBase
	{
		// Token: 0x060022B5 RID: 8885 RVA: 0x000AAD8E File Offset: 0x000A8F8E
		public FixedField(TypeDefinition parent, FullNamedExpression type, Modifiers mod, MemberName name, Attributes attrs) : base(parent, type, mod, Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.UNSAFE, name, attrs)
		{
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x000AADA2 File Offset: 0x000A8FA2
		// (set) Token: 0x060022B7 RID: 8887 RVA: 0x000AADAA File Offset: 0x000A8FAA
		public CharSet? CharSetValue
		{
			[CompilerGenerated]
			get
			{
				return this.<CharSetValue>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CharSetValue>k__BackingField = value;
			}
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000AADB3 File Offset: 0x000A8FB3
		public override Constant ConvertInitializer(ResolveContext rc, Constant expr)
		{
			return expr.ImplicitConversionRequired(rc, rc.BuiltinTypes.Int);
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000AADC8 File Offset: 0x000A8FC8
		public override bool Define()
		{
			if (!base.Define())
			{
				return false;
			}
			if (!BuiltinTypeSpec.IsPrimitiveType(base.MemberType))
			{
				base.Report.Error(1663, base.Location, "`{0}': Fixed size buffers type must be one of the following: bool, byte, short, int, long, char, sbyte, ushort, uint, ulong, float or double", this.GetSignatureForError());
			}
			else if (this.declarators != null)
			{
				foreach (FieldDeclarator fieldDeclarator in this.declarators)
				{
					FixedField fixedField = new FixedField(this.Parent, fieldDeclarator.GetFieldTypeExpression(this), base.ModFlags, new MemberName(fieldDeclarator.Name.Value, fieldDeclarator.Name.Location), base.OptAttributes);
					fixedField.initializer = fieldDeclarator.Initializer;
					((ConstInitializer)fixedField.initializer).Name = fieldDeclarator.Name.Value;
					fixedField.Define();
					this.Parent.PartialContainer.Members.Add(fixedField);
				}
			}
			string name = string.Format("<{0}>__FixedBuffer{1}", TypeDefinition.FilterNestedName(base.Name), FixedField.GlobalCounter++);
			this.fixed_buffer_type = this.Parent.TypeBuilder.DefineNestedType(name, TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, this.Compiler.BuiltinTypes.ValueType.GetMetaInfo());
			FieldBuilder info = this.fixed_buffer_type.DefineField("FixedElementField", base.MemberType.GetMetaInfo(), FieldAttributes.Public);
			this.FieldBuilder = this.Parent.TypeBuilder.DefineField(base.Name, this.fixed_buffer_type, ModifiersExtensions.FieldAttr(base.ModFlags));
			FieldSpec element = new FieldSpec(null, this, base.MemberType, info, base.ModFlags);
			this.spec = new FixedFieldSpec(this.Module, this.Parent.Definition, this, this.FieldBuilder, element, base.ModFlags);
			this.Parent.MemberCache.AddMember(this.spec);
			return true;
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x000AAFE4 File Offset: 0x000A91E4
		protected override void DoMemberTypeIndependentChecks()
		{
			base.DoMemberTypeIndependentChecks();
			if (!base.IsUnsafe)
			{
				Expression.UnsafeError(base.Report, base.Location);
			}
			if (this.Parent.PartialContainer.Kind != MemberKind.Struct)
			{
				base.Report.Error(1642, base.Location, "`{0}': Fixed size buffer fields may only be members of structs", this.GetSignatureForError());
			}
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x000AB048 File Offset: 0x000A9248
		public override void Emit()
		{
			ResolveContext rc = new ResolveContext(this);
			IntConstant intConstant = this.initializer.Resolve(rc) as IntConstant;
			if (intConstant == null)
			{
				return;
			}
			int value = intConstant.Value;
			if (value <= 0)
			{
				base.Report.Error(1665, base.Location, "`{0}': Fixed size buffers must have a length greater than zero", this.GetSignatureForError());
				return;
			}
			this.EmitFieldSize(value);
			this.Module.PredefinedAttributes.UnsafeValueType.EmitAttribute(this.fixed_buffer_type);
			this.Module.PredefinedAttributes.CompilerGenerated.EmitAttribute(this.fixed_buffer_type);
			this.fixed_buffer_type.CreateType();
			base.Emit();
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x000AB0F0 File Offset: 0x000A92F0
		private void EmitFieldSize(int buffer_size)
		{
			int size = BuiltinTypeSpec.GetSize(base.MemberType);
			if (buffer_size > 2147483647 / size)
			{
				base.Report.Error(1664, base.Location, "Fixed size buffer `{0}' of length `{1}' and type `{2}' exceeded 2^31 limit", new string[]
				{
					this.GetSignatureForError(),
					buffer_size.ToString(),
					base.MemberType.GetSignatureForError()
				});
				return;
			}
			CharSet v = this.CharSetValue ?? (this.Module.DefaultCharSet ?? ((CharSet)0));
			MethodSpec methodSpec = this.Module.PredefinedMembers.StructLayoutAttributeCtor.Resolve(base.Location);
			if (methodSpec == null)
			{
				return;
			}
			FieldSpec fieldSpec = this.Module.PredefinedMembers.StructLayoutSize.Resolve(base.Location);
			FieldSpec fieldSpec2 = this.Module.PredefinedMembers.StructLayoutCharSet.Resolve(base.Location);
			if (fieldSpec == null || fieldSpec2 == null)
			{
				return;
			}
			AttributeEncoder attributeEncoder = new AttributeEncoder();
			attributeEncoder.Encode(0);
			attributeEncoder.EncodeNamedArguments<FieldSpec>(new FieldSpec[]
			{
				fieldSpec,
				fieldSpec2
			}, new Constant[]
			{
				new IntConstant(this.Compiler.BuiltinTypes, buffer_size * size, base.Location),
				new IntConstant(this.Compiler.BuiltinTypes, (int)v, base.Location)
			});
			this.fixed_buffer_type.SetCustomAttribute((ConstructorInfo)methodSpec.GetMetaInfo(), attributeEncoder.ToArray());
			if ((base.ModFlags & Modifiers.PRIVATE) != (Modifiers)0)
			{
				return;
			}
			methodSpec = this.Module.PredefinedMembers.FixedBufferAttributeCtor.Resolve(base.Location);
			if (methodSpec == null)
			{
				return;
			}
			attributeEncoder = new AttributeEncoder();
			attributeEncoder.EncodeTypeName(base.MemberType);
			attributeEncoder.Encode(buffer_size);
			attributeEncoder.EncodeEmptyNamedArguments();
			this.FieldBuilder.SetCustomAttribute((ConstructorInfo)methodSpec.GetMetaInfo(), attributeEncoder.ToArray());
		}

		// Token: 0x04000D5E RID: 3422
		public const string FixedElementName = "FixedElementField";

		// Token: 0x04000D5F RID: 3423
		private static int GlobalCounter;

		// Token: 0x04000D60 RID: 3424
		private TypeBuilder fixed_buffer_type;

		// Token: 0x04000D61 RID: 3425
		private const Modifiers AllowedModifiers = Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.UNSAFE;

		// Token: 0x04000D62 RID: 3426
		[CompilerGenerated]
		private CharSet? <CharSetValue>k__BackingField;
	}
}
