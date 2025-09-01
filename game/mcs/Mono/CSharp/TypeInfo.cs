using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x02000216 RID: 534
	public class TypeInfo
	{
		// Token: 0x06001B36 RID: 6966 RVA: 0x0008447E File Offset: 0x0008267E
		static TypeInfo()
		{
			TypeInfo.Reset();
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x00084490 File Offset: 0x00082690
		public static void Reset()
		{
			TypeInfo.StructInfo.field_type_hash = new Dictionary<TypeSpec, TypeInfo.StructInfo>();
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0008449C File Offset: 0x0008269C
		private TypeInfo(int totalLength)
		{
			this.TotalLength = totalLength;
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x000844AC File Offset: 0x000826AC
		private TypeInfo(TypeInfo.StructInfo struct_info, int offset)
		{
			this.struct_info = struct_info;
			this.Offset = offset;
			this.Length = struct_info.Length;
			this.TotalLength = struct_info.TotalLength;
			this.SubStructInfo = struct_info.StructFields;
			this.IsStruct = true;
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x000844F8 File Offset: 0x000826F8
		public int GetFieldIndex(string name)
		{
			if (this.struct_info == null)
			{
				return 0;
			}
			return this.struct_info[name];
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00084510 File Offset: 0x00082710
		public TypeInfo GetStructField(string name)
		{
			if (this.struct_info == null)
			{
				return null;
			}
			return this.struct_info.GetStructField(name);
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00084528 File Offset: 0x00082728
		public static TypeInfo GetTypeInfo(TypeSpec type, IMemberContext context)
		{
			if (!type.IsStruct)
			{
				return TypeInfo.simple_type;
			}
			Dictionary<TypeSpec, TypeInfo> dictionary;
			TypeInfo typeInfo;
			if (type.BuiltinType > BuiltinTypeSpec.Type.None)
			{
				dictionary = null;
			}
			else
			{
				dictionary = context.Module.TypeInfoCache;
				if (dictionary.TryGetValue(type, out typeInfo))
				{
					return typeInfo;
				}
			}
			TypeInfo.StructInfo structInfo = TypeInfo.StructInfo.GetStructInfo(type, context);
			if (structInfo != null)
			{
				typeInfo = new TypeInfo(structInfo, 0);
			}
			else
			{
				typeInfo = TypeInfo.simple_type;
			}
			if (dictionary != null)
			{
				dictionary.Add(type, typeInfo);
			}
			return typeInfo;
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x00084590 File Offset: 0x00082790
		public bool IsFullyInitialized(FlowAnalysisContext fc, VariableInfo vi, Location loc)
		{
			if (this.struct_info == null)
			{
				return true;
			}
			bool result = true;
			for (int i = 0; i < this.struct_info.Count; i++)
			{
				FieldSpec fieldSpec = this.struct_info.Fields[i];
				if (!fc.IsStructFieldDefinitelyAssigned(vi, fieldSpec.Name))
				{
					Property.BackingFieldDeclaration backingFieldDeclaration = fieldSpec.MemberDefinition as Property.BackingFieldDeclaration;
					if (backingFieldDeclaration != null)
					{
						if (backingFieldDeclaration.Initializer == null)
						{
							fc.Report.Error(843, loc, "An automatically implemented property `{0}' must be fully assigned before control leaves the constructor. Consider calling the default struct contructor from a constructor initializer", fieldSpec.GetSignatureForError());
							result = false;
						}
					}
					else
					{
						fc.Report.Error(171, loc, "Field `{0}' must be fully assigned before control leaves the constructor", fieldSpec.GetSignatureForError());
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x00084639 File Offset: 0x00082839
		public override string ToString()
		{
			return string.Format("TypeInfo ({0}:{1}:{2})", this.Offset, this.Length, this.TotalLength);
		}

		// Token: 0x04000A1F RID: 2591
		public readonly int TotalLength;

		// Token: 0x04000A20 RID: 2592
		public readonly int Length;

		// Token: 0x04000A21 RID: 2593
		public readonly int Offset;

		// Token: 0x04000A22 RID: 2594
		public readonly bool IsStruct;

		// Token: 0x04000A23 RID: 2595
		public TypeInfo[] SubStructInfo;

		// Token: 0x04000A24 RID: 2596
		private readonly TypeInfo.StructInfo struct_info;

		// Token: 0x04000A25 RID: 2597
		private static readonly TypeInfo simple_type = new TypeInfo(1);

		// Token: 0x020003BF RID: 959
		private class StructInfo
		{
			// Token: 0x0600273D RID: 10045 RVA: 0x000BBCB0 File Offset: 0x000B9EB0
			private StructInfo(TypeSpec type, IMemberContext context)
			{
				TypeInfo.StructInfo.field_type_hash.Add(type, this);
				this.fields = MemberCache.GetAllFieldsForDefiniteAssignment(type, context);
				this.struct_field_hash = new Dictionary<string, TypeInfo>();
				this.field_hash = new Dictionary<string, int>(this.fields.Count);
				this.StructFields = new TypeInfo[this.fields.Count];
				TypeInfo.StructInfo[] array = new TypeInfo.StructInfo[this.fields.Count];
				this.InTransit = true;
				for (int i = 0; i < this.fields.Count; i++)
				{
					FieldSpec fieldSpec = this.fields[i];
					if (fieldSpec.MemberType.IsStruct)
					{
						array[i] = TypeInfo.StructInfo.GetStructInfo(fieldSpec.MemberType, context);
					}
					if (array[i] == null)
					{
						Dictionary<string, int> dictionary = this.field_hash;
						string name = fieldSpec.Name;
						int num = this.Length + 1;
						this.Length = num;
						dictionary.Add(name, num);
					}
					else if (array[i].InTransit)
					{
						array[i] = null;
						return;
					}
				}
				this.InTransit = false;
				this.TotalLength = this.Length + 1;
				for (int j = 0; j < this.fields.Count; j++)
				{
					FieldSpec fieldSpec2 = this.fields[j];
					if (array[j] != null)
					{
						this.field_hash.Add(fieldSpec2.Name, this.TotalLength);
						this.StructFields[j] = new TypeInfo(array[j], this.TotalLength);
						this.struct_field_hash.Add(fieldSpec2.Name, this.StructFields[j]);
						this.TotalLength += array[j].TotalLength;
					}
				}
			}

			// Token: 0x170008DF RID: 2271
			// (get) Token: 0x0600273E RID: 10046 RVA: 0x000BBE4A File Offset: 0x000BA04A
			public int Count
			{
				get
				{
					return this.fields.Count;
				}
			}

			// Token: 0x170008E0 RID: 2272
			// (get) Token: 0x0600273F RID: 10047 RVA: 0x000BBE57 File Offset: 0x000BA057
			public List<FieldSpec> Fields
			{
				get
				{
					return this.fields;
				}
			}

			// Token: 0x170008E1 RID: 2273
			public int this[string name]
			{
				get
				{
					int result;
					if (!this.field_hash.TryGetValue(name, out result))
					{
						return 0;
					}
					return result;
				}
			}

			// Token: 0x06002741 RID: 10049 RVA: 0x000BBE80 File Offset: 0x000BA080
			public TypeInfo GetStructField(string name)
			{
				TypeInfo result;
				if (this.struct_field_hash.TryGetValue(name, out result))
				{
					return result;
				}
				return null;
			}

			// Token: 0x06002742 RID: 10050 RVA: 0x000BBEA0 File Offset: 0x000BA0A0
			public static TypeInfo.StructInfo GetStructInfo(TypeSpec type, IMemberContext context)
			{
				if (type.BuiltinType > BuiltinTypeSpec.Type.None && type != context.CurrentType)
				{
					return null;
				}
				TypeInfo.StructInfo result;
				if (TypeInfo.StructInfo.field_type_hash.TryGetValue(type, out result))
				{
					return result;
				}
				return new TypeInfo.StructInfo(type, context);
			}

			// Token: 0x040010A4 RID: 4260
			private readonly List<FieldSpec> fields;

			// Token: 0x040010A5 RID: 4261
			public readonly TypeInfo[] StructFields;

			// Token: 0x040010A6 RID: 4262
			public readonly int Length;

			// Token: 0x040010A7 RID: 4263
			public readonly int TotalLength;

			// Token: 0x040010A8 RID: 4264
			public static Dictionary<TypeSpec, TypeInfo.StructInfo> field_type_hash;

			// Token: 0x040010A9 RID: 4265
			private Dictionary<string, TypeInfo> struct_field_hash;

			// Token: 0x040010AA RID: 4266
			private Dictionary<string, int> field_hash;

			// Token: 0x040010AB RID: 4267
			private bool InTransit;
		}
	}
}
