using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000217 RID: 535
	public class VariableInfo
	{
		// Token: 0x06001B3F RID: 6975 RVA: 0x00084666 File Offset: 0x00082866
		private VariableInfo(string name, TypeSpec type, int offset, IMemberContext context)
		{
			this.Name = name;
			this.Offset = offset;
			this.TypeInfo = TypeInfo.GetTypeInfo(type, context);
			this.Length = this.TypeInfo.TotalLength;
			this.Initialize();
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x000846A4 File Offset: 0x000828A4
		private VariableInfo(VariableInfo parent, TypeInfo type)
		{
			this.Name = parent.Name;
			this.TypeInfo = type;
			this.Offset = parent.Offset + type.Offset;
			this.Length = type.TotalLength;
			this.IsParameter = parent.IsParameter;
			this.Initialize();
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x000846FC File Offset: 0x000828FC
		private void Initialize()
		{
			TypeInfo[] subStructInfo = this.TypeInfo.SubStructInfo;
			if (subStructInfo != null)
			{
				this.sub_info = new VariableInfo[subStructInfo.Length];
				for (int i = 0; i < subStructInfo.Length; i++)
				{
					if (subStructInfo[i] != null)
					{
						this.sub_info[i] = new VariableInfo(this, subStructInfo[i]);
					}
				}
				return;
			}
			this.sub_info = new VariableInfo[0];
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x00084758 File Offset: 0x00082958
		public static VariableInfo Create(BlockContext bc, LocalVariable variable)
		{
			VariableInfo variableInfo = new VariableInfo(variable.Name, variable.Type, bc.AssignmentInfoOffset, bc);
			bc.AssignmentInfoOffset += variableInfo.Length;
			return variableInfo;
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x00084794 File Offset: 0x00082994
		public static VariableInfo Create(BlockContext bc, Parameter parameter)
		{
			VariableInfo variableInfo = new VariableInfo(parameter.Name, parameter.Type, bc.AssignmentInfoOffset, bc)
			{
				IsParameter = true
			};
			bc.AssignmentInfoOffset += variableInfo.Length;
			return variableInfo;
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000847D8 File Offset: 0x000829D8
		public bool IsAssigned(DefiniteAssignmentBitSet vector)
		{
			if (vector == null)
			{
				return true;
			}
			if (vector[this.Offset])
			{
				return true;
			}
			if (!this.TypeInfo.IsStruct)
			{
				return false;
			}
			for (int i = this.Offset + 1; i <= this.TypeInfo.Length + this.Offset; i++)
			{
				if (!vector[i])
				{
					return false;
				}
			}
			for (int j = 0; j < this.sub_info.Length; j++)
			{
				VariableInfo variableInfo = this.sub_info[j];
				if (variableInfo != null && !variableInfo.IsAssigned(vector))
				{
					return false;
				}
			}
			vector.Set(this.Offset);
			return true;
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x0008486F File Offset: 0x00082A6F
		// (set) Token: 0x06001B46 RID: 6982 RVA: 0x00084877 File Offset: 0x00082A77
		public bool IsEverAssigned
		{
			[CompilerGenerated]
			get
			{
				return this.<IsEverAssigned>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsEverAssigned>k__BackingField = value;
			}
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x00084880 File Offset: 0x00082A80
		public bool IsFullyInitialized(FlowAnalysisContext fc, Location loc)
		{
			return this.TypeInfo.IsFullyInitialized(fc, this, loc);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x00084890 File Offset: 0x00082A90
		public bool IsStructFieldAssigned(DefiniteAssignmentBitSet vector, string field_name)
		{
			int fieldIndex = this.TypeInfo.GetFieldIndex(field_name);
			return fieldIndex == 0 || vector[this.Offset + fieldIndex];
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x000848BD File Offset: 0x00082ABD
		public void SetAssigned(DefiniteAssignmentBitSet vector, bool generatedAssignment)
		{
			if (this.Length == 1)
			{
				vector.Set(this.Offset);
			}
			else
			{
				vector.Set(this.Offset, this.Length);
			}
			if (!generatedAssignment)
			{
				this.IsEverAssigned = true;
			}
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x000848F4 File Offset: 0x00082AF4
		public void SetStructFieldAssigned(DefiniteAssignmentBitSet vector, string field_name)
		{
			if (vector[this.Offset])
			{
				return;
			}
			int fieldIndex = this.TypeInfo.GetFieldIndex(field_name);
			if (fieldIndex == 0)
			{
				return;
			}
			TypeInfo structField = this.TypeInfo.GetStructField(field_name);
			if (structField != null)
			{
				vector.Set(this.Offset + structField.Offset, structField.TotalLength);
			}
			else
			{
				vector.Set(this.Offset + fieldIndex);
			}
			this.IsEverAssigned = true;
			for (int i = this.Offset + 1; i < this.TypeInfo.TotalLength + this.Offset; i++)
			{
				if (!vector[i])
				{
					return;
				}
			}
			vector.Set(this.Offset);
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0008499C File Offset: 0x00082B9C
		public VariableInfo GetStructFieldInfo(string fieldName)
		{
			TypeInfo structField = this.TypeInfo.GetStructField(fieldName);
			if (structField == null)
			{
				return null;
			}
			return new VariableInfo(this, structField);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x000849C2 File Offset: 0x00082BC2
		public override string ToString()
		{
			return string.Format("Name={0} Offset={1} Length={2} {3})", new object[]
			{
				this.Name,
				this.Offset,
				this.Length,
				this.TypeInfo
			});
		}

		// Token: 0x04000A26 RID: 2598
		private readonly string Name;

		// Token: 0x04000A27 RID: 2599
		private readonly TypeInfo TypeInfo;

		// Token: 0x04000A28 RID: 2600
		private readonly int Offset;

		// Token: 0x04000A29 RID: 2601
		private readonly int Length;

		// Token: 0x04000A2A RID: 2602
		public bool IsParameter;

		// Token: 0x04000A2B RID: 2603
		private VariableInfo[] sub_info;

		// Token: 0x04000A2C RID: 2604
		[CompilerGenerated]
		private bool <IsEverAssigned>k__BackingField;
	}
}
