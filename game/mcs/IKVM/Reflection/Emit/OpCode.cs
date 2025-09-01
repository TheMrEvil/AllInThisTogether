using System;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000ED RID: 237
	public struct OpCode
	{
		// Token: 0x06000B89 RID: 2953 RVA: 0x00028DA8 File Offset: 0x00026FA8
		internal OpCode(int value)
		{
			this.value = value;
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00028DB4 File Offset: 0x00026FB4
		public override bool Equals(object obj)
		{
			return this == obj as OpCode?;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00028DEC File Offset: 0x00026FEC
		public override int GetHashCode()
		{
			return this.value;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00028DF4 File Offset: 0x00026FF4
		public bool Equals(OpCode other)
		{
			return this == other;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00028E02 File Offset: 0x00027002
		public static bool operator ==(OpCode a, OpCode b)
		{
			return a.value == b.value;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00028E12 File Offset: 0x00027012
		public static bool operator !=(OpCode a, OpCode b)
		{
			return !(a == b);
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x00028E1E File Offset: 0x0002701E
		public short Value
		{
			get
			{
				return (short)(this.value >> 22);
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x00028E2A File Offset: 0x0002702A
		public int Size
		{
			get
			{
				if (this.value >= 0)
				{
					return 1;
				}
				return 2;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x00028E38 File Offset: 0x00027038
		public string Name
		{
			get
			{
				return OpCodes.GetName((int)this.Value);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00028E45 File Offset: 0x00027045
		public OperandType OperandType
		{
			get
			{
				return (OperandType)((this.value & 4194303) % 19);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00028E56 File Offset: 0x00027056
		public FlowControl FlowControl
		{
			get
			{
				return (FlowControl)((this.value & 4194303) / 19 % 9);
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00028E6A File Offset: 0x0002706A
		internal int StackDiff
		{
			get
			{
				return (this.value & 4194303) / 171 % 5 - 3;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x00028E82 File Offset: 0x00027082
		public OpCodeType OpCodeType
		{
			get
			{
				return (OpCodeType)((this.value & 4194303) / 855 % 6);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x00028E98 File Offset: 0x00027098
		public StackBehaviour StackBehaviourPop
		{
			get
			{
				return OpCode.pop[(this.value & 4194303) / 5130 % 20];
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x00028EB5 File Offset: 0x000270B5
		public StackBehaviour StackBehaviourPush
		{
			get
			{
				return OpCode.push[(this.value & 4194303) / 102600 % 9];
			}
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00028ED4 File Offset: 0x000270D4
		// Note: this type is marked as 'beforefieldinit'.
		static OpCode()
		{
		}

		// Token: 0x040004F1 RID: 1265
		private const int ValueCount = 1024;

		// Token: 0x040004F2 RID: 1266
		private const int OperandTypeCount = 19;

		// Token: 0x040004F3 RID: 1267
		private const int FlowControlCount = 9;

		// Token: 0x040004F4 RID: 1268
		private const int StackDiffCount = 5;

		// Token: 0x040004F5 RID: 1269
		private const int OpCodeTypeCount = 6;

		// Token: 0x040004F6 RID: 1270
		private const int StackBehaviourPopCount = 20;

		// Token: 0x040004F7 RID: 1271
		private const int StackBehaviourPushCount = 9;

		// Token: 0x040004F8 RID: 1272
		private static readonly StackBehaviour[] pop = new StackBehaviour[]
		{
			StackBehaviour.Pop0,
			StackBehaviour.Pop1,
			StackBehaviour.Pop1_pop1,
			StackBehaviour.Popi,
			StackBehaviour.Popi_pop1,
			StackBehaviour.Popi_popi,
			StackBehaviour.Popi_popi8,
			StackBehaviour.Popi_popi_popi,
			StackBehaviour.Popi_popr4,
			StackBehaviour.Popi_popr8,
			StackBehaviour.Popref,
			StackBehaviour.Popref_pop1,
			StackBehaviour.Popref_popi,
			StackBehaviour.Popref_popi_popi,
			StackBehaviour.Popref_popi_popi8,
			StackBehaviour.Popref_popi_popr4,
			StackBehaviour.Popref_popi_popr8,
			StackBehaviour.Popref_popi_popref,
			StackBehaviour.Varpop,
			StackBehaviour.Popref_popi_pop1
		};

		// Token: 0x040004F9 RID: 1273
		private static readonly StackBehaviour[] push = new StackBehaviour[]
		{
			StackBehaviour.Push0,
			StackBehaviour.Push1,
			StackBehaviour.Push1_push1,
			StackBehaviour.Pushi,
			StackBehaviour.Pushi8,
			StackBehaviour.Pushr4,
			StackBehaviour.Pushr8,
			StackBehaviour.Pushref,
			StackBehaviour.Varpush
		};

		// Token: 0x040004FA RID: 1274
		private readonly int value;
	}
}
