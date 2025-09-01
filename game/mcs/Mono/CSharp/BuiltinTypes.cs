using System;

namespace Mono.CSharp
{
	// Token: 0x020002D0 RID: 720
	public class BuiltinTypes
	{
		// Token: 0x06002267 RID: 8807 RVA: 0x000A8450 File Offset: 0x000A6650
		public BuiltinTypes()
		{
			this.Object = new BuiltinTypeSpec(MemberKind.Class, "System", "Object", BuiltinTypeSpec.Type.Object);
			this.ValueType = new BuiltinTypeSpec(MemberKind.Class, "System", "ValueType", BuiltinTypeSpec.Type.ValueType);
			this.Attribute = new BuiltinTypeSpec(MemberKind.Class, "System", "Attribute", BuiltinTypeSpec.Type.Attribute);
			this.Int = new BuiltinTypeSpec(MemberKind.Struct, "System", "Int32", BuiltinTypeSpec.Type.Int);
			this.Long = new BuiltinTypeSpec(MemberKind.Struct, "System", "Int64", BuiltinTypeSpec.Type.Long);
			this.UInt = new BuiltinTypeSpec(MemberKind.Struct, "System", "UInt32", BuiltinTypeSpec.Type.UInt);
			this.ULong = new BuiltinTypeSpec(MemberKind.Struct, "System", "UInt64", BuiltinTypeSpec.Type.ULong);
			this.Byte = new BuiltinTypeSpec(MemberKind.Struct, "System", "Byte", BuiltinTypeSpec.Type.Byte);
			this.SByte = new BuiltinTypeSpec(MemberKind.Struct, "System", "SByte", BuiltinTypeSpec.Type.SByte);
			this.Short = new BuiltinTypeSpec(MemberKind.Struct, "System", "Int16", BuiltinTypeSpec.Type.Short);
			this.UShort = new BuiltinTypeSpec(MemberKind.Struct, "System", "UInt16", BuiltinTypeSpec.Type.UShort);
			this.IEnumerator = new BuiltinTypeSpec(MemberKind.Interface, "System.Collections", "IEnumerator", BuiltinTypeSpec.Type.IEnumerator);
			this.IEnumerable = new BuiltinTypeSpec(MemberKind.Interface, "System.Collections", "IEnumerable", BuiltinTypeSpec.Type.IEnumerable);
			this.IDisposable = new BuiltinTypeSpec(MemberKind.Interface, "System", "IDisposable", BuiltinTypeSpec.Type.IDisposable);
			this.Char = new BuiltinTypeSpec(MemberKind.Struct, "System", "Char", BuiltinTypeSpec.Type.Char);
			this.String = new BuiltinTypeSpec(MemberKind.Class, "System", "String", BuiltinTypeSpec.Type.String);
			this.Float = new BuiltinTypeSpec(MemberKind.Struct, "System", "Single", BuiltinTypeSpec.Type.Float);
			this.Double = new BuiltinTypeSpec(MemberKind.Struct, "System", "Double", BuiltinTypeSpec.Type.Double);
			this.Decimal = new BuiltinTypeSpec(MemberKind.Struct, "System", "Decimal", BuiltinTypeSpec.Type.Decimal);
			this.Bool = new BuiltinTypeSpec(MemberKind.Struct, "System", "Boolean", BuiltinTypeSpec.Type.FirstPrimitive);
			this.IntPtr = new BuiltinTypeSpec(MemberKind.Struct, "System", "IntPtr", BuiltinTypeSpec.Type.IntPtr);
			this.UIntPtr = new BuiltinTypeSpec(MemberKind.Struct, "System", "UIntPtr", BuiltinTypeSpec.Type.UIntPtr);
			this.MulticastDelegate = new BuiltinTypeSpec(MemberKind.Class, "System", "MulticastDelegate", BuiltinTypeSpec.Type.MulticastDelegate);
			this.Delegate = new BuiltinTypeSpec(MemberKind.Class, "System", "Delegate", BuiltinTypeSpec.Type.Delegate);
			this.Enum = new BuiltinTypeSpec(MemberKind.Class, "System", "Enum", BuiltinTypeSpec.Type.Enum);
			this.Array = new BuiltinTypeSpec(MemberKind.Class, "System", "Array", BuiltinTypeSpec.Type.Array);
			this.Void = new BuiltinTypeSpec(MemberKind.Void, "System", "Void", BuiltinTypeSpec.Type.Other);
			this.Type = new BuiltinTypeSpec(MemberKind.Class, "System", "Type", BuiltinTypeSpec.Type.Type);
			this.Exception = new BuiltinTypeSpec(MemberKind.Class, "System", "Exception", BuiltinTypeSpec.Type.Exception);
			this.RuntimeFieldHandle = new BuiltinTypeSpec(MemberKind.Struct, "System", "RuntimeFieldHandle", BuiltinTypeSpec.Type.Other);
			this.RuntimeTypeHandle = new BuiltinTypeSpec(MemberKind.Struct, "System", "RuntimeTypeHandle", BuiltinTypeSpec.Type.Other);
			this.Dynamic = new BuiltinTypeSpec("dynamic", BuiltinTypeSpec.Type.Dynamic);
			this.OperatorsBinaryStandard = Binary.CreateStandardOperatorsTable(this);
			this.OperatorsBinaryEquality = Binary.CreateEqualityOperatorsTable(this);
			this.OperatorsBinaryUnsafe = Binary.CreatePointerOperatorsTable(this);
			this.OperatorsUnary = Unary.CreatePredefinedOperatorsTable(this);
			this.OperatorsUnaryMutator = UnaryMutator.CreatePredefinedOperatorsTable(this);
			this.BinaryPromotionsTypes = ConstantFold.CreateBinaryPromotionsTypes(this);
			this.types = new BuiltinTypeSpec[]
			{
				this.Object,
				this.ValueType,
				this.Attribute,
				this.Int,
				this.UInt,
				this.Long,
				this.ULong,
				this.Float,
				this.Double,
				this.Char,
				this.Short,
				this.Decimal,
				this.Bool,
				this.SByte,
				this.Byte,
				this.UShort,
				this.String,
				this.Enum,
				this.Delegate,
				this.MulticastDelegate,
				this.Void,
				this.Array,
				this.Type,
				this.IEnumerator,
				this.IEnumerable,
				this.IDisposable,
				this.IntPtr,
				this.UIntPtr,
				this.RuntimeFieldHandle,
				this.RuntimeTypeHandle,
				this.Exception
			};
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x000A8953 File Offset: 0x000A6B53
		public BuiltinTypeSpec[] AllTypes
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x000A895C File Offset: 0x000A6B5C
		public bool CheckDefinitions(ModuleContainer module)
		{
			CompilerContext compiler = module.Compiler;
			foreach (BuiltinTypeSpec builtinTypeSpec in this.types)
			{
				TypeSpec typeSpec = PredefinedType.Resolve(module, builtinTypeSpec.Kind, builtinTypeSpec.Namespace, builtinTypeSpec.Name, builtinTypeSpec.Arity, true, true);
				if (typeSpec != null && typeSpec != builtinTypeSpec)
				{
					TypeDefinition typeDefinition = typeSpec.MemberDefinition as TypeDefinition;
					if (typeDefinition != null)
					{
						module.GlobalRootNamespace.GetNamespace(builtinTypeSpec.Namespace, false).SetBuiltinType(builtinTypeSpec);
						typeDefinition.SetPredefinedSpec(builtinTypeSpec);
						builtinTypeSpec.SetDefinition(typeSpec);
					}
				}
			}
			if (compiler.Report.Errors != 0)
			{
				return false;
			}
			this.Dynamic.SetDefinition(this.Object);
			return true;
		}

		// Token: 0x04000CB0 RID: 3248
		public readonly BuiltinTypeSpec Object;

		// Token: 0x04000CB1 RID: 3249
		public readonly BuiltinTypeSpec ValueType;

		// Token: 0x04000CB2 RID: 3250
		public readonly BuiltinTypeSpec Attribute;

		// Token: 0x04000CB3 RID: 3251
		public readonly BuiltinTypeSpec Int;

		// Token: 0x04000CB4 RID: 3252
		public readonly BuiltinTypeSpec UInt;

		// Token: 0x04000CB5 RID: 3253
		public readonly BuiltinTypeSpec Long;

		// Token: 0x04000CB6 RID: 3254
		public readonly BuiltinTypeSpec ULong;

		// Token: 0x04000CB7 RID: 3255
		public readonly BuiltinTypeSpec Float;

		// Token: 0x04000CB8 RID: 3256
		public readonly BuiltinTypeSpec Double;

		// Token: 0x04000CB9 RID: 3257
		public readonly BuiltinTypeSpec Char;

		// Token: 0x04000CBA RID: 3258
		public readonly BuiltinTypeSpec Short;

		// Token: 0x04000CBB RID: 3259
		public readonly BuiltinTypeSpec Decimal;

		// Token: 0x04000CBC RID: 3260
		public readonly BuiltinTypeSpec Bool;

		// Token: 0x04000CBD RID: 3261
		public readonly BuiltinTypeSpec SByte;

		// Token: 0x04000CBE RID: 3262
		public readonly BuiltinTypeSpec Byte;

		// Token: 0x04000CBF RID: 3263
		public readonly BuiltinTypeSpec UShort;

		// Token: 0x04000CC0 RID: 3264
		public readonly BuiltinTypeSpec String;

		// Token: 0x04000CC1 RID: 3265
		public readonly BuiltinTypeSpec Enum;

		// Token: 0x04000CC2 RID: 3266
		public readonly BuiltinTypeSpec Delegate;

		// Token: 0x04000CC3 RID: 3267
		public readonly BuiltinTypeSpec MulticastDelegate;

		// Token: 0x04000CC4 RID: 3268
		public readonly BuiltinTypeSpec Void;

		// Token: 0x04000CC5 RID: 3269
		public readonly BuiltinTypeSpec Array;

		// Token: 0x04000CC6 RID: 3270
		public readonly BuiltinTypeSpec Type;

		// Token: 0x04000CC7 RID: 3271
		public readonly BuiltinTypeSpec IEnumerator;

		// Token: 0x04000CC8 RID: 3272
		public readonly BuiltinTypeSpec IEnumerable;

		// Token: 0x04000CC9 RID: 3273
		public readonly BuiltinTypeSpec IDisposable;

		// Token: 0x04000CCA RID: 3274
		public readonly BuiltinTypeSpec IntPtr;

		// Token: 0x04000CCB RID: 3275
		public readonly BuiltinTypeSpec UIntPtr;

		// Token: 0x04000CCC RID: 3276
		public readonly BuiltinTypeSpec RuntimeFieldHandle;

		// Token: 0x04000CCD RID: 3277
		public readonly BuiltinTypeSpec RuntimeTypeHandle;

		// Token: 0x04000CCE RID: 3278
		public readonly BuiltinTypeSpec Exception;

		// Token: 0x04000CCF RID: 3279
		public readonly BuiltinTypeSpec Dynamic;

		// Token: 0x04000CD0 RID: 3280
		public readonly Binary.PredefinedOperator[] OperatorsBinaryStandard;

		// Token: 0x04000CD1 RID: 3281
		public readonly Binary.PredefinedOperator[] OperatorsBinaryEquality;

		// Token: 0x04000CD2 RID: 3282
		public readonly Binary.PredefinedOperator[] OperatorsBinaryUnsafe;

		// Token: 0x04000CD3 RID: 3283
		public readonly TypeSpec[][] OperatorsUnary;

		// Token: 0x04000CD4 RID: 3284
		public readonly TypeSpec[] OperatorsUnaryMutator;

		// Token: 0x04000CD5 RID: 3285
		public readonly TypeSpec[] BinaryPromotionsTypes;

		// Token: 0x04000CD6 RID: 3286
		private readonly BuiltinTypeSpec[] types;
	}
}
