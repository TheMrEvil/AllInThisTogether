using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Provides field representations of the Microsoft Intermediate Language (MSIL) instructions for emission by the <see cref="T:System.Reflection.Emit.ILGenerator" /> class members (such as <see cref="M:System.Reflection.Emit.ILGenerator.Emit(System.Reflection.Emit.OpCode)" />).</summary>
	// Token: 0x0200093C RID: 2364
	[ComVisible(true)]
	public class OpCodes
	{
		// Token: 0x060051FB RID: 20987 RVA: 0x0000259F File Offset: 0x0000079F
		internal OpCodes()
		{
		}

		/// <summary>Returns true or false if the supplied opcode takes a single byte argument.</summary>
		/// <param name="inst">An instance of an Opcode object.</param>
		/// <returns>
		///   <see langword="true" /> or <see langword="false" />.</returns>
		// Token: 0x060051FC RID: 20988 RVA: 0x00100FE4 File Offset: 0x000FF1E4
		public static bool TakesSingleByteArgument(OpCode inst)
		{
			OperandType operandType = inst.OperandType;
			return operandType == OperandType.ShortInlineBrTarget || operandType == OperandType.ShortInlineI || operandType == OperandType.ShortInlineR || operandType == OperandType.ShortInlineVar;
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x00101010 File Offset: 0x000FF210
		// Note: this type is marked as 'beforefieldinit'.
		static OpCodes()
		{
		}

		/// <summary>Fills space if opcodes are patched. No meaningful operation is performed although a processing cycle can be consumed.</summary>
		// Token: 0x04003208 RID: 12808
		public static readonly OpCode Nop = new OpCode(1179903, 84215041);

		/// <summary>Signals the Common Language Infrastructure (CLI) to inform the debugger that a break point has been tripped.</summary>
		// Token: 0x04003209 RID: 12809
		public static readonly OpCode Break = new OpCode(1180159, 17106177);

		/// <summary>Loads the argument at index 0 onto the evaluation stack.</summary>
		// Token: 0x0400320A RID: 12810
		public static readonly OpCode Ldarg_0 = new OpCode(1245951, 84214017);

		/// <summary>Loads the argument at index 1 onto the evaluation stack.</summary>
		// Token: 0x0400320B RID: 12811
		public static readonly OpCode Ldarg_1 = new OpCode(1246207, 84214017);

		/// <summary>Loads the argument at index 2 onto the evaluation stack.</summary>
		// Token: 0x0400320C RID: 12812
		public static readonly OpCode Ldarg_2 = new OpCode(1246463, 84214017);

		/// <summary>Loads the argument at index 3 onto the evaluation stack.</summary>
		// Token: 0x0400320D RID: 12813
		public static readonly OpCode Ldarg_3 = new OpCode(1246719, 84214017);

		/// <summary>Loads the local variable at index 0 onto the evaluation stack.</summary>
		// Token: 0x0400320E RID: 12814
		public static readonly OpCode Ldloc_0 = new OpCode(1246975, 84214017);

		/// <summary>Loads the local variable at index 1 onto the evaluation stack.</summary>
		// Token: 0x0400320F RID: 12815
		public static readonly OpCode Ldloc_1 = new OpCode(1247231, 84214017);

		/// <summary>Loads the local variable at index 2 onto the evaluation stack.</summary>
		// Token: 0x04003210 RID: 12816
		public static readonly OpCode Ldloc_2 = new OpCode(1247487, 84214017);

		/// <summary>Loads the local variable at index 3 onto the evaluation stack.</summary>
		// Token: 0x04003211 RID: 12817
		public static readonly OpCode Ldloc_3 = new OpCode(1247743, 84214017);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 0.</summary>
		// Token: 0x04003212 RID: 12818
		public static readonly OpCode Stloc_0 = new OpCode(17959679, 84214017);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 1.</summary>
		// Token: 0x04003213 RID: 12819
		public static readonly OpCode Stloc_1 = new OpCode(17959935, 84214017);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 2.</summary>
		// Token: 0x04003214 RID: 12820
		public static readonly OpCode Stloc_2 = new OpCode(17960191, 84214017);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 3.</summary>
		// Token: 0x04003215 RID: 12821
		public static readonly OpCode Stloc_3 = new OpCode(17960447, 84214017);

		/// <summary>Loads the argument (referenced by a specified short form index) onto the evaluation stack.</summary>
		// Token: 0x04003216 RID: 12822
		public static readonly OpCode Ldarg_S = new OpCode(1249023, 85065985);

		/// <summary>Load an argument address, in short form, onto the evaluation stack.</summary>
		// Token: 0x04003217 RID: 12823
		public static readonly OpCode Ldarga_S = new OpCode(1380351, 85065985);

		/// <summary>Stores the value on top of the evaluation stack in the argument slot at a specified index, short form.</summary>
		// Token: 0x04003218 RID: 12824
		public static readonly OpCode Starg_S = new OpCode(17961215, 85065985);

		/// <summary>Loads the local variable at a specific index onto the evaluation stack, short form.</summary>
		// Token: 0x04003219 RID: 12825
		public static readonly OpCode Ldloc_S = new OpCode(1249791, 85065985);

		/// <summary>Loads the address of the local variable at a specific index onto the evaluation stack, short form.</summary>
		// Token: 0x0400321A RID: 12826
		public static readonly OpCode Ldloca_S = new OpCode(1381119, 85065985);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at <paramref name="index" /> (short form).</summary>
		// Token: 0x0400321B RID: 12827
		public static readonly OpCode Stloc_S = new OpCode(17961983, 85065985);

		/// <summary>Pushes a null reference (type <see langword="O" />) onto the evaluation stack.</summary>
		// Token: 0x0400321C RID: 12828
		public static readonly OpCode Ldnull = new OpCode(1643775, 84215041);

		/// <summary>Pushes the integer value of -1 onto the evaluation stack as an <see langword="int32" />.</summary>
		// Token: 0x0400321D RID: 12829
		public static readonly OpCode Ldc_I4_M1 = new OpCode(1381887, 84214017);

		/// <summary>Pushes the integer value of 0 onto the evaluation stack as an <see langword="int32" />.</summary>
		// Token: 0x0400321E RID: 12830
		public static readonly OpCode Ldc_I4_0 = new OpCode(1382143, 84214017);

		/// <summary>Pushes the integer value of 1 onto the evaluation stack as an <see langword="int32" />.</summary>
		// Token: 0x0400321F RID: 12831
		public static readonly OpCode Ldc_I4_1 = new OpCode(1382399, 84214017);

		/// <summary>Pushes the integer value of 2 onto the evaluation stack as an <see langword="int32" />.</summary>
		// Token: 0x04003220 RID: 12832
		public static readonly OpCode Ldc_I4_2 = new OpCode(1382655, 84214017);

		/// <summary>Pushes the integer value of 3 onto the evaluation stack as an <see langword="int32" />.</summary>
		// Token: 0x04003221 RID: 12833
		public static readonly OpCode Ldc_I4_3 = new OpCode(1382911, 84214017);

		/// <summary>Pushes the integer value of 4 onto the evaluation stack as an <see langword="int32" />.</summary>
		// Token: 0x04003222 RID: 12834
		public static readonly OpCode Ldc_I4_4 = new OpCode(1383167, 84214017);

		/// <summary>Pushes the integer value of 5 onto the evaluation stack as an <see langword="int32" />.</summary>
		// Token: 0x04003223 RID: 12835
		public static readonly OpCode Ldc_I4_5 = new OpCode(1383423, 84214017);

		/// <summary>Pushes the integer value of 6 onto the evaluation stack as an <see langword="int32" />.</summary>
		// Token: 0x04003224 RID: 12836
		public static readonly OpCode Ldc_I4_6 = new OpCode(1383679, 84214017);

		/// <summary>Pushes the integer value of 7 onto the evaluation stack as an <see langword="int32" />.</summary>
		// Token: 0x04003225 RID: 12837
		public static readonly OpCode Ldc_I4_7 = new OpCode(1383935, 84214017);

		/// <summary>Pushes the integer value of 8 onto the evaluation stack as an <see langword="int32" />.</summary>
		// Token: 0x04003226 RID: 12838
		public static readonly OpCode Ldc_I4_8 = new OpCode(1384191, 84214017);

		/// <summary>Pushes the supplied <see langword="int8" /> value onto the evaluation stack as an <see langword="int32" />, short form.</summary>
		// Token: 0x04003227 RID: 12839
		public static readonly OpCode Ldc_I4_S = new OpCode(1384447, 84934913);

		/// <summary>Pushes a supplied value of type <see langword="int32" /> onto the evaluation stack as an <see langword="int32" />.</summary>
		// Token: 0x04003228 RID: 12840
		public static readonly OpCode Ldc_I4 = new OpCode(1384703, 84018433);

		/// <summary>Pushes a supplied value of type <see langword="int64" /> onto the evaluation stack as an <see langword="int64" />.</summary>
		// Token: 0x04003229 RID: 12841
		public static readonly OpCode Ldc_I8 = new OpCode(1450495, 84083969);

		/// <summary>Pushes a supplied value of type <see langword="float32" /> onto the evaluation stack as type <see langword="F" /> (float).</summary>
		// Token: 0x0400322A RID: 12842
		public static readonly OpCode Ldc_R4 = new OpCode(1516287, 85001473);

		/// <summary>Pushes a supplied value of type <see langword="float64" /> onto the evaluation stack as type <see langword="F" /> (float).</summary>
		// Token: 0x0400322B RID: 12843
		public static readonly OpCode Ldc_R8 = new OpCode(1582079, 84346113);

		/// <summary>Copies the current topmost value on the evaluation stack, and then pushes the copy onto the evaluation stack.</summary>
		// Token: 0x0400322C RID: 12844
		public static readonly OpCode Dup = new OpCode(18097663, 84215041);

		/// <summary>Removes the value currently on top of the evaluation stack.</summary>
		// Token: 0x0400322D RID: 12845
		public static readonly OpCode Pop = new OpCode(17966847, 84215041);

		/// <summary>Exits current method and jumps to specified method.</summary>
		// Token: 0x0400322E RID: 12846
		public static readonly OpCode Jmp = new OpCode(1189887, 33817857);

		/// <summary>Calls the method indicated by the passed method descriptor.</summary>
		// Token: 0x0400322F RID: 12847
		public static readonly OpCode Call = new OpCode(437987583, 33817857);

		/// <summary>Calls the method indicated on the evaluation stack (as a pointer to an entry point) with arguments described by a calling convention.</summary>
		// Token: 0x04003230 RID: 12848
		public static readonly OpCode Calli = new OpCode(437987839, 34145537);

		/// <summary>Returns from the current method, pushing a return value (if present) from the callee's evaluation stack onto the caller's evaluation stack.</summary>
		// Token: 0x04003231 RID: 12849
		public static readonly OpCode Ret = new OpCode(437398271, 117769473);

		/// <summary>Unconditionally transfers control to a target instruction (short form).</summary>
		// Token: 0x04003232 RID: 12850
		public static readonly OpCode Br_S = new OpCode(1190911, 983297);

		/// <summary>Transfers control to a target instruction if <paramref name="value" /> is <see langword="false" />, a null reference, or zero.</summary>
		// Token: 0x04003233 RID: 12851
		public static readonly OpCode Brfalse_S = new OpCode(51522815, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if <paramref name="value" /> is <see langword="true" />, not null, or non-zero.</summary>
		// Token: 0x04003234 RID: 12852
		public static readonly OpCode Brtrue_S = new OpCode(51523071, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if two values are equal.</summary>
		// Token: 0x04003235 RID: 12853
		public static readonly OpCode Beq_S = new OpCode(34746111, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is greater than or equal to the second value.</summary>
		// Token: 0x04003236 RID: 12854
		public static readonly OpCode Bge_S = new OpCode(34746367, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is greater than the second value.</summary>
		// Token: 0x04003237 RID: 12855
		public static readonly OpCode Bgt_S = new OpCode(34746623, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is less than or equal to the second value.</summary>
		// Token: 0x04003238 RID: 12856
		public static readonly OpCode Ble_S = new OpCode(34746879, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is less than the second value.</summary>
		// Token: 0x04003239 RID: 12857
		public static readonly OpCode Blt_S = new OpCode(34747135, 51314945);

		/// <summary>Transfers control to a target instruction (short form) when two unsigned integer values or unordered float values are not equal.</summary>
		// Token: 0x0400323A RID: 12858
		public static readonly OpCode Bne_Un_S = new OpCode(34747391, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x0400323B RID: 12859
		public static readonly OpCode Bge_Un_S = new OpCode(34747647, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x0400323C RID: 12860
		public static readonly OpCode Bgt_Un_S = new OpCode(34747903, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is less than or equal to the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x0400323D RID: 12861
		public static readonly OpCode Ble_Un_S = new OpCode(34748159, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is less than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x0400323E RID: 12862
		public static readonly OpCode Blt_Un_S = new OpCode(34748415, 51314945);

		/// <summary>Unconditionally transfers control to a target instruction.</summary>
		// Token: 0x0400323F RID: 12863
		public static readonly OpCode Br = new OpCode(1194239, 1281);

		/// <summary>Transfers control to a target instruction if <paramref name="value" /> is <see langword="false" />, a null reference (<see langword="Nothing" /> in Visual Basic), or zero.</summary>
		// Token: 0x04003240 RID: 12864
		public static readonly OpCode Brfalse = new OpCode(51526143, 50332929);

		/// <summary>Transfers control to a target instruction if <paramref name="value" /> is <see langword="true" />, not null, or non-zero.</summary>
		// Token: 0x04003241 RID: 12865
		public static readonly OpCode Brtrue = new OpCode(51526399, 50332929);

		/// <summary>Transfers control to a target instruction if two values are equal.</summary>
		// Token: 0x04003242 RID: 12866
		public static readonly OpCode Beq = new OpCode(34749439, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is greater than or equal to the second value.</summary>
		// Token: 0x04003243 RID: 12867
		public static readonly OpCode Bge = new OpCode(34749695, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is greater than the second value.</summary>
		// Token: 0x04003244 RID: 12868
		public static readonly OpCode Bgt = new OpCode(34749951, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is less than or equal to the second value.</summary>
		// Token: 0x04003245 RID: 12869
		public static readonly OpCode Ble = new OpCode(34750207, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is less than the second value.</summary>
		// Token: 0x04003246 RID: 12870
		public static readonly OpCode Blt = new OpCode(34750463, 50331905);

		/// <summary>Transfers control to a target instruction when two unsigned integer values or unordered float values are not equal.</summary>
		// Token: 0x04003247 RID: 12871
		public static readonly OpCode Bne_Un = new OpCode(34750719, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x04003248 RID: 12872
		public static readonly OpCode Bge_Un = new OpCode(34750975, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x04003249 RID: 12873
		public static readonly OpCode Bgt_Un = new OpCode(34751231, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is less than or equal to the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x0400324A RID: 12874
		public static readonly OpCode Ble_Un = new OpCode(34751487, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is less than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x0400324B RID: 12875
		public static readonly OpCode Blt_Un = new OpCode(34751743, 50331905);

		/// <summary>Implements a jump table.</summary>
		// Token: 0x0400324C RID: 12876
		public static readonly OpCode Switch = new OpCode(51529215, 51053825);

		/// <summary>Loads a value of type <see langword="int8" /> as an <see langword="int32" /> onto the evaluation stack indirectly.</summary>
		// Token: 0x0400324D RID: 12877
		public static readonly OpCode Ldind_I1 = new OpCode(51726079, 84215041);

		/// <summary>Loads a value of type <see langword="unsigned int8" /> as an <see langword="int32" /> onto the evaluation stack indirectly.</summary>
		// Token: 0x0400324E RID: 12878
		public static readonly OpCode Ldind_U1 = new OpCode(51726335, 84215041);

		/// <summary>Loads a value of type <see langword="int16" /> as an <see langword="int32" /> onto the evaluation stack indirectly.</summary>
		// Token: 0x0400324F RID: 12879
		public static readonly OpCode Ldind_I2 = new OpCode(51726591, 84215041);

		/// <summary>Loads a value of type <see langword="unsigned int16" /> as an <see langword="int32" /> onto the evaluation stack indirectly.</summary>
		// Token: 0x04003250 RID: 12880
		public static readonly OpCode Ldind_U2 = new OpCode(51726847, 84215041);

		/// <summary>Loads a value of type <see langword="int32" /> as an <see langword="int32" /> onto the evaluation stack indirectly.</summary>
		// Token: 0x04003251 RID: 12881
		public static readonly OpCode Ldind_I4 = new OpCode(51727103, 84215041);

		/// <summary>Loads a value of type <see langword="unsigned int32" /> as an <see langword="int32" /> onto the evaluation stack indirectly.</summary>
		// Token: 0x04003252 RID: 12882
		public static readonly OpCode Ldind_U4 = new OpCode(51727359, 84215041);

		/// <summary>Loads a value of type <see langword="int64" /> as an <see langword="int64" /> onto the evaluation stack indirectly.</summary>
		// Token: 0x04003253 RID: 12883
		public static readonly OpCode Ldind_I8 = new OpCode(51793151, 84215041);

		/// <summary>Loads a value of type <see langword="native int" /> as a <see langword="native int" /> onto the evaluation stack indirectly.</summary>
		// Token: 0x04003254 RID: 12884
		public static readonly OpCode Ldind_I = new OpCode(51727871, 84215041);

		/// <summary>Loads a value of type <see langword="float32" /> as a type <see langword="F" /> (float) onto the evaluation stack indirectly.</summary>
		// Token: 0x04003255 RID: 12885
		public static readonly OpCode Ldind_R4 = new OpCode(51859199, 84215041);

		/// <summary>Loads a value of type <see langword="float64" /> as a type <see langword="F" /> (float) onto the evaluation stack indirectly.</summary>
		// Token: 0x04003256 RID: 12886
		public static readonly OpCode Ldind_R8 = new OpCode(51924991, 84215041);

		/// <summary>Loads an object reference as a type <see langword="O" /> (object reference) onto the evaluation stack indirectly.</summary>
		// Token: 0x04003257 RID: 12887
		public static readonly OpCode Ldind_Ref = new OpCode(51990783, 84215041);

		/// <summary>Stores a object reference value at a supplied address.</summary>
		// Token: 0x04003258 RID: 12888
		public static readonly OpCode Stind_Ref = new OpCode(85086719, 84215041);

		/// <summary>Stores a value of type <see langword="int8" /> at a supplied address.</summary>
		// Token: 0x04003259 RID: 12889
		public static readonly OpCode Stind_I1 = new OpCode(85086975, 84215041);

		/// <summary>Stores a value of type <see langword="int16" /> at a supplied address.</summary>
		// Token: 0x0400325A RID: 12890
		public static readonly OpCode Stind_I2 = new OpCode(85087231, 84215041);

		/// <summary>Stores a value of type <see langword="int32" /> at a supplied address.</summary>
		// Token: 0x0400325B RID: 12891
		public static readonly OpCode Stind_I4 = new OpCode(85087487, 84215041);

		/// <summary>Stores a value of type <see langword="int64" /> at a supplied address.</summary>
		// Token: 0x0400325C RID: 12892
		public static readonly OpCode Stind_I8 = new OpCode(101864959, 84215041);

		/// <summary>Stores a value of type <see langword="float32" /> at a supplied address.</summary>
		// Token: 0x0400325D RID: 12893
		public static readonly OpCode Stind_R4 = new OpCode(135419647, 84215041);

		/// <summary>Stores a value of type <see langword="float64" /> at a supplied address.</summary>
		// Token: 0x0400325E RID: 12894
		public static readonly OpCode Stind_R8 = new OpCode(152197119, 84215041);

		/// <summary>Adds two values and pushes the result onto the evaluation stack.</summary>
		// Token: 0x0400325F RID: 12895
		public static readonly OpCode Add = new OpCode(34822399, 84215041);

		/// <summary>Subtracts one value from another and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04003260 RID: 12896
		public static readonly OpCode Sub = new OpCode(34822655, 84215041);

		/// <summary>Multiplies two values and pushes the result on the evaluation stack.</summary>
		// Token: 0x04003261 RID: 12897
		public static readonly OpCode Mul = new OpCode(34822911, 84215041);

		/// <summary>Divides two values and pushes the result as a floating-point (type <see langword="F" />) or quotient (type <see langword="int32" />) onto the evaluation stack.</summary>
		// Token: 0x04003262 RID: 12898
		public static readonly OpCode Div = new OpCode(34823167, 84215041);

		/// <summary>Divides two unsigned integer values and pushes the result (<see langword="int32" />) onto the evaluation stack.</summary>
		// Token: 0x04003263 RID: 12899
		public static readonly OpCode Div_Un = new OpCode(34823423, 84215041);

		/// <summary>Divides two values and pushes the remainder onto the evaluation stack.</summary>
		// Token: 0x04003264 RID: 12900
		public static readonly OpCode Rem = new OpCode(34823679, 84215041);

		/// <summary>Divides two unsigned values and pushes the remainder onto the evaluation stack.</summary>
		// Token: 0x04003265 RID: 12901
		public static readonly OpCode Rem_Un = new OpCode(34823935, 84215041);

		/// <summary>Computes the bitwise AND of two values and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04003266 RID: 12902
		public static readonly OpCode And = new OpCode(34824191, 84215041);

		/// <summary>Compute the bitwise complement of the two integer values on top of the stack and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04003267 RID: 12903
		public static readonly OpCode Or = new OpCode(34824447, 84215041);

		/// <summary>Computes the bitwise XOR of the top two values on the evaluation stack, pushing the result onto the evaluation stack.</summary>
		// Token: 0x04003268 RID: 12904
		public static readonly OpCode Xor = new OpCode(34824703, 84215041);

		/// <summary>Shifts an integer value to the left (in zeroes) by a specified number of bits, pushing the result onto the evaluation stack.</summary>
		// Token: 0x04003269 RID: 12905
		public static readonly OpCode Shl = new OpCode(34824959, 84215041);

		/// <summary>Shifts an integer value (in sign) to the right by a specified number of bits, pushing the result onto the evaluation stack.</summary>
		// Token: 0x0400326A RID: 12906
		public static readonly OpCode Shr = new OpCode(34825215, 84215041);

		/// <summary>Shifts an unsigned integer value (in zeroes) to the right by a specified number of bits, pushing the result onto the evaluation stack.</summary>
		// Token: 0x0400326B RID: 12907
		public static readonly OpCode Shr_Un = new OpCode(34825471, 84215041);

		/// <summary>Negates a value and pushes the result onto the evaluation stack.</summary>
		// Token: 0x0400326C RID: 12908
		public static readonly OpCode Neg = new OpCode(18048511, 84215041);

		/// <summary>Computes the bitwise complement of the integer value on top of the stack and pushes the result onto the evaluation stack as the same type.</summary>
		// Token: 0x0400326D RID: 12909
		public static readonly OpCode Not = new OpCode(18048767, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to <see langword="int8" />, then extends (pads) it to <see langword="int32" />.</summary>
		// Token: 0x0400326E RID: 12910
		public static readonly OpCode Conv_I1 = new OpCode(18180095, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to <see langword="int16" />, then extends (pads) it to <see langword="int32" />.</summary>
		// Token: 0x0400326F RID: 12911
		public static readonly OpCode Conv_I2 = new OpCode(18180351, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to <see langword="int32" />.</summary>
		// Token: 0x04003270 RID: 12912
		public static readonly OpCode Conv_I4 = new OpCode(18180607, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to <see langword="int64" />.</summary>
		// Token: 0x04003271 RID: 12913
		public static readonly OpCode Conv_I8 = new OpCode(18246399, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to <see langword="float32" />.</summary>
		// Token: 0x04003272 RID: 12914
		public static readonly OpCode Conv_R4 = new OpCode(18312191, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to <see langword="float64" />.</summary>
		// Token: 0x04003273 RID: 12915
		public static readonly OpCode Conv_R8 = new OpCode(18377983, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to <see langword="unsigned int32" />, and extends it to <see langword="int32" />.</summary>
		// Token: 0x04003274 RID: 12916
		public static readonly OpCode Conv_U4 = new OpCode(18181631, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to <see langword="unsigned int64" />, and extends it to <see langword="int64" />.</summary>
		// Token: 0x04003275 RID: 12917
		public static readonly OpCode Conv_U8 = new OpCode(18247423, 84215041);

		/// <summary>Calls a late-bound method on an object, pushing the return value onto the evaluation stack.</summary>
		// Token: 0x04003276 RID: 12918
		public static readonly OpCode Callvirt = new OpCode(438005759, 33817345);

		/// <summary>Copies the value type located at the address of an object (type <see langword="&amp;" />, <see langword="" /> or <see langword="native int" />) to the address of the destination object (type <see langword="&amp;" />, <see langword="" /> or <see langword="native int" />).</summary>
		// Token: 0x04003277 RID: 12919
		public static readonly OpCode Cpobj = new OpCode(85094655, 84738817);

		/// <summary>Copies the value type object pointed to by an address to the top of the evaluation stack.</summary>
		// Token: 0x04003278 RID: 12920
		public static readonly OpCode Ldobj = new OpCode(51606015, 84738817);

		/// <summary>Pushes a new object reference to a string literal stored in the metadata.</summary>
		// Token: 0x04003279 RID: 12921
		public static readonly OpCode Ldstr = new OpCode(1667839, 84542209);

		/// <summary>Creates a new object or a new instance of a value type, pushing an object reference (type <see langword="O" />) onto the evaluation stack.</summary>
		// Token: 0x0400327A RID: 12922
		public static readonly OpCode Newobj = new OpCode(437875711, 33817345);

		/// <summary>Attempts to cast an object passed by reference to the specified class.</summary>
		// Token: 0x0400327B RID: 12923
		[ComVisible(true)]
		public static readonly OpCode Castclass = new OpCode(169440511, 84738817);

		/// <summary>Tests whether an object reference (type <see langword="O" />) is an instance of a particular class.</summary>
		// Token: 0x0400327C RID: 12924
		public static readonly OpCode Isinst = new OpCode(169178623, 84738817);

		/// <summary>Converts the unsigned integer value on top of the evaluation stack to <see langword="float32" />.</summary>
		// Token: 0x0400327D RID: 12925
		public static readonly OpCode Conv_R_Un = new OpCode(18380543, 84215041);

		/// <summary>Converts the boxed representation of a value type to its unboxed form.</summary>
		// Token: 0x0400327E RID: 12926
		public static readonly OpCode Unbox = new OpCode(169179647, 84739329);

		/// <summary>Throws the exception object currently on the evaluation stack.</summary>
		// Token: 0x0400327F RID: 12927
		public static readonly OpCode Throw = new OpCode(168983295, 134546177);

		/// <summary>Finds the value of a field in the object whose reference is currently on the evaluation stack.</summary>
		// Token: 0x04003280 RID: 12928
		public static readonly OpCode Ldfld = new OpCode(169049087, 83952385);

		/// <summary>Finds the address of a field in the object whose reference is currently on the evaluation stack.</summary>
		// Token: 0x04003281 RID: 12929
		public static readonly OpCode Ldflda = new OpCode(169180415, 83952385);

		/// <summary>Replaces the value stored in the field of an object reference or pointer with a new value.</summary>
		// Token: 0x04003282 RID: 12930
		public static readonly OpCode Stfld = new OpCode(185761279, 83952385);

		/// <summary>Pushes the value of a static field onto the evaluation stack.</summary>
		// Token: 0x04003283 RID: 12931
		public static readonly OpCode Ldsfld = new OpCode(1277695, 83952385);

		/// <summary>Pushes the address of a static field onto the evaluation stack.</summary>
		// Token: 0x04003284 RID: 12932
		public static readonly OpCode Ldsflda = new OpCode(1409023, 83952385);

		/// <summary>Replaces the value of a static field with a value from the evaluation stack.</summary>
		// Token: 0x04003285 RID: 12933
		public static readonly OpCode Stsfld = new OpCode(17989887, 83952385);

		/// <summary>Copies a value of a specified type from the evaluation stack into a supplied memory address.</summary>
		// Token: 0x04003286 RID: 12934
		public static readonly OpCode Stobj = new OpCode(68321791, 84739329);

		/// <summary>Converts the unsigned value on top of the evaluation stack to signed <see langword="int8" /> and extends it to <see langword="int32" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04003287 RID: 12935
		public static readonly OpCode Conv_Ovf_I1_Un = new OpCode(18187007, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to signed <see langword="int16" /> and extends it to <see langword="int32" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04003288 RID: 12936
		public static readonly OpCode Conv_Ovf_I2_Un = new OpCode(18187263, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to signed <see langword="int32" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04003289 RID: 12937
		public static readonly OpCode Conv_Ovf_I4_Un = new OpCode(18187519, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to signed <see langword="int64" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x0400328A RID: 12938
		public static readonly OpCode Conv_Ovf_I8_Un = new OpCode(18253311, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to <see langword="unsigned int8" /> and extends it to <see langword="int32" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x0400328B RID: 12939
		public static readonly OpCode Conv_Ovf_U1_Un = new OpCode(18188031, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to <see langword="unsigned int16" /> and extends it to <see langword="int32" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x0400328C RID: 12940
		public static readonly OpCode Conv_Ovf_U2_Un = new OpCode(18188287, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to <see langword="unsigned int32" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x0400328D RID: 12941
		public static readonly OpCode Conv_Ovf_U4_Un = new OpCode(18188543, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to <see langword="unsigned int64" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x0400328E RID: 12942
		public static readonly OpCode Conv_Ovf_U8_Un = new OpCode(18254335, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to signed <see langword="native int" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x0400328F RID: 12943
		public static readonly OpCode Conv_Ovf_I_Un = new OpCode(18189055, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to <see langword="unsigned native int" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04003290 RID: 12944
		public static readonly OpCode Conv_Ovf_U_Un = new OpCode(18189311, 84215041);

		/// <summary>Converts a value type to an object reference (type <see langword="O" />).</summary>
		// Token: 0x04003291 RID: 12945
		public static readonly OpCode Box = new OpCode(18451711, 84739329);

		/// <summary>Pushes an object reference to a new zero-based, one-dimensional array whose elements are of a specific type onto the evaluation stack.</summary>
		// Token: 0x04003292 RID: 12946
		public static readonly OpCode Newarr = new OpCode(52006399, 84738817);

		/// <summary>Pushes the number of elements of a zero-based, one-dimensional array onto the evaluation stack.</summary>
		// Token: 0x04003293 RID: 12947
		public static readonly OpCode Ldlen = new OpCode(169185023, 84214529);

		/// <summary>Loads the address of the array element at a specified array index onto the top of the evaluation stack as type <see langword="&amp;" /> (managed pointer).</summary>
		// Token: 0x04003294 RID: 12948
		public static readonly OpCode Ldelema = new OpCode(202739711, 84738817);

		/// <summary>Loads the element with type <see langword="int8" /> at a specified array index onto the top of the evaluation stack as an <see langword="int32" />.</summary>
		// Token: 0x04003295 RID: 12949
		public static readonly OpCode Ldelem_I1 = new OpCode(202739967, 84214529);

		/// <summary>Loads the element with type <see langword="unsigned int8" /> at a specified array index onto the top of the evaluation stack as an <see langword="int32" />.</summary>
		// Token: 0x04003296 RID: 12950
		public static readonly OpCode Ldelem_U1 = new OpCode(202740223, 84214529);

		/// <summary>Loads the element with type <see langword="int16" /> at a specified array index onto the top of the evaluation stack as an <see langword="int32" />.</summary>
		// Token: 0x04003297 RID: 12951
		public static readonly OpCode Ldelem_I2 = new OpCode(202740479, 84214529);

		/// <summary>Loads the element with type <see langword="unsigned int16" /> at a specified array index onto the top of the evaluation stack as an <see langword="int32" />.</summary>
		// Token: 0x04003298 RID: 12952
		public static readonly OpCode Ldelem_U2 = new OpCode(202740735, 84214529);

		/// <summary>Loads the element with type <see langword="int32" /> at a specified array index onto the top of the evaluation stack as an <see langword="int32" />.</summary>
		// Token: 0x04003299 RID: 12953
		public static readonly OpCode Ldelem_I4 = new OpCode(202740991, 84214529);

		/// <summary>Loads the element with type <see langword="unsigned int32" /> at a specified array index onto the top of the evaluation stack as an <see langword="int32" />.</summary>
		// Token: 0x0400329A RID: 12954
		public static readonly OpCode Ldelem_U4 = new OpCode(202741247, 84214529);

		/// <summary>Loads the element with type <see langword="int64" /> at a specified array index onto the top of the evaluation stack as an <see langword="int64" />.</summary>
		// Token: 0x0400329B RID: 12955
		public static readonly OpCode Ldelem_I8 = new OpCode(202807039, 84214529);

		/// <summary>Loads the element with type <see langword="native int" /> at a specified array index onto the top of the evaluation stack as a <see langword="native int" />.</summary>
		// Token: 0x0400329C RID: 12956
		public static readonly OpCode Ldelem_I = new OpCode(202741759, 84214529);

		/// <summary>Loads the element with type <see langword="float32" /> at a specified array index onto the top of the evaluation stack as type <see langword="F" /> (float).</summary>
		// Token: 0x0400329D RID: 12957
		public static readonly OpCode Ldelem_R4 = new OpCode(202873087, 84214529);

		/// <summary>Loads the element with type <see langword="float64" /> at a specified array index onto the top of the evaluation stack as type <see langword="F" /> (float).</summary>
		// Token: 0x0400329E RID: 12958
		public static readonly OpCode Ldelem_R8 = new OpCode(202938879, 84214529);

		/// <summary>Loads the element containing an object reference at a specified array index onto the top of the evaluation stack as type <see langword="O" /> (object reference).</summary>
		// Token: 0x0400329F RID: 12959
		public static readonly OpCode Ldelem_Ref = new OpCode(203004671, 84214529);

		/// <summary>Replaces the array element at a given index with the <see langword="native int" /> value on the evaluation stack.</summary>
		// Token: 0x040032A0 RID: 12960
		public static readonly OpCode Stelem_I = new OpCode(219323391, 84214529);

		/// <summary>Replaces the array element at a given index with the <see langword="int8" /> value on the evaluation stack.</summary>
		// Token: 0x040032A1 RID: 12961
		public static readonly OpCode Stelem_I1 = new OpCode(219323647, 84214529);

		/// <summary>Replaces the array element at a given index with the <see langword="int16" /> value on the evaluation stack.</summary>
		// Token: 0x040032A2 RID: 12962
		public static readonly OpCode Stelem_I2 = new OpCode(219323903, 84214529);

		/// <summary>Replaces the array element at a given index with the <see langword="int32" /> value on the evaluation stack.</summary>
		// Token: 0x040032A3 RID: 12963
		public static readonly OpCode Stelem_I4 = new OpCode(219324159, 84214529);

		/// <summary>Replaces the array element at a given index with the <see langword="int64" /> value on the evaluation stack.</summary>
		// Token: 0x040032A4 RID: 12964
		public static readonly OpCode Stelem_I8 = new OpCode(236101631, 84214529);

		/// <summary>Replaces the array element at a given index with the <see langword="float32" /> value on the evaluation stack.</summary>
		// Token: 0x040032A5 RID: 12965
		public static readonly OpCode Stelem_R4 = new OpCode(252879103, 84214529);

		/// <summary>Replaces the array element at a given index with the <see langword="float64" /> value on the evaluation stack.</summary>
		// Token: 0x040032A6 RID: 12966
		public static readonly OpCode Stelem_R8 = new OpCode(269656575, 84214529);

		/// <summary>Replaces the array element at a given index with the object ref value (type <see langword="O" />) on the evaluation stack.</summary>
		// Token: 0x040032A7 RID: 12967
		public static readonly OpCode Stelem_Ref = new OpCode(286434047, 84214529);

		/// <summary>Loads the element at a specified array index onto the top of the evaluation stack as the type specified in the instruction.</summary>
		// Token: 0x040032A8 RID: 12968
		public static readonly OpCode Ldelem = new OpCode(202613759, 84738817);

		/// <summary>Replaces the array element at a given index with the value on the evaluation stack, whose type is specified in the instruction.</summary>
		// Token: 0x040032A9 RID: 12969
		public static readonly OpCode Stelem = new OpCode(470983935, 84738817);

		/// <summary>Converts the boxed representation of a type specified in the instruction to its unboxed form.</summary>
		// Token: 0x040032AA RID: 12970
		public static readonly OpCode Unbox_Any = new OpCode(169059839, 84738817);

		/// <summary>Converts the signed value on top of the evaluation stack to signed <see langword="int8" /> and extends it to <see langword="int32" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x040032AB RID: 12971
		public static readonly OpCode Conv_Ovf_I1 = new OpCode(18199551, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to <see langword="unsigned int8" /> and extends it to <see langword="int32" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x040032AC RID: 12972
		public static readonly OpCode Conv_Ovf_U1 = new OpCode(18199807, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to signed <see langword="int16" /> and extending it to <see langword="int32" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x040032AD RID: 12973
		public static readonly OpCode Conv_Ovf_I2 = new OpCode(18200063, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to <see langword="unsigned int16" /> and extends it to <see langword="int32" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x040032AE RID: 12974
		public static readonly OpCode Conv_Ovf_U2 = new OpCode(18200319, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to signed <see langword="int32" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x040032AF RID: 12975
		public static readonly OpCode Conv_Ovf_I4 = new OpCode(18200575, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to <see langword="unsigned int32" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x040032B0 RID: 12976
		public static readonly OpCode Conv_Ovf_U4 = new OpCode(18200831, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to signed <see langword="int64" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x040032B1 RID: 12977
		public static readonly OpCode Conv_Ovf_I8 = new OpCode(18266623, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to <see langword="unsigned int64" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x040032B2 RID: 12978
		public static readonly OpCode Conv_Ovf_U8 = new OpCode(18266879, 84215041);

		/// <summary>Retrieves the address (type <see langword="&amp;" />) embedded in a typed reference.</summary>
		// Token: 0x040032B3 RID: 12979
		public static readonly OpCode Refanyval = new OpCode(18203391, 84739329);

		/// <summary>Throws <see cref="T:System.ArithmeticException" /> if value is not a finite number.</summary>
		// Token: 0x040032B4 RID: 12980
		public static readonly OpCode Ckfinite = new OpCode(18400255, 84215041);

		/// <summary>Pushes a typed reference to an instance of a specific type onto the evaluation stack.</summary>
		// Token: 0x040032B5 RID: 12981
		public static readonly OpCode Mkrefany = new OpCode(51627775, 84739329);

		/// <summary>Converts a metadata token to its runtime representation, pushing it onto the evaluation stack.</summary>
		// Token: 0x040032B6 RID: 12982
		public static readonly OpCode Ldtoken = new OpCode(1429759, 84673793);

		/// <summary>Converts the value on top of the evaluation stack to <see langword="unsigned int16" />, and extends it to <see langword="int32" />.</summary>
		// Token: 0x040032B7 RID: 12983
		public static readonly OpCode Conv_U2 = new OpCode(18207231, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to <see langword="unsigned int8" />, and extends it to <see langword="int32" />.</summary>
		// Token: 0x040032B8 RID: 12984
		public static readonly OpCode Conv_U1 = new OpCode(18207487, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to <see langword="native int" />.</summary>
		// Token: 0x040032B9 RID: 12985
		public static readonly OpCode Conv_I = new OpCode(18207743, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to signed <see langword="native int" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x040032BA RID: 12986
		public static readonly OpCode Conv_Ovf_I = new OpCode(18207999, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to <see langword="unsigned native int" />, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x040032BB RID: 12987
		public static readonly OpCode Conv_Ovf_U = new OpCode(18208255, 84215041);

		/// <summary>Adds two integers, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x040032BC RID: 12988
		public static readonly OpCode Add_Ovf = new OpCode(34854655, 84215041);

		/// <summary>Adds two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x040032BD RID: 12989
		public static readonly OpCode Add_Ovf_Un = new OpCode(34854911, 84215041);

		/// <summary>Multiplies two integer values, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x040032BE RID: 12990
		public static readonly OpCode Mul_Ovf = new OpCode(34855167, 84215041);

		/// <summary>Multiplies two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x040032BF RID: 12991
		public static readonly OpCode Mul_Ovf_Un = new OpCode(34855423, 84215041);

		/// <summary>Subtracts one integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x040032C0 RID: 12992
		public static readonly OpCode Sub_Ovf = new OpCode(34855679, 84215041);

		/// <summary>Subtracts one unsigned integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x040032C1 RID: 12993
		public static readonly OpCode Sub_Ovf_Un = new OpCode(34855935, 84215041);

		/// <summary>Transfers control from the <see langword="fault" /> or <see langword="finally" /> clause of an exception block back to the Common Language Infrastructure (CLI) exception handler.</summary>
		// Token: 0x040032C2 RID: 12994
		public static readonly OpCode Endfinally = new OpCode(1236223, 117769473);

		/// <summary>Exits a protected region of code, unconditionally transferring control to a specific target instruction.</summary>
		// Token: 0x040032C3 RID: 12995
		public static readonly OpCode Leave = new OpCode(1236479, 1281);

		/// <summary>Exits a protected region of code, unconditionally transferring control to a target instruction (short form).</summary>
		// Token: 0x040032C4 RID: 12996
		public static readonly OpCode Leave_S = new OpCode(1236735, 984321);

		/// <summary>Stores a value of type <see langword="native int" /> at a supplied address.</summary>
		// Token: 0x040032C5 RID: 12997
		public static readonly OpCode Stind_I = new OpCode(85123071, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to <see langword="unsigned native int" />, and extends it to <see langword="native int" />.</summary>
		// Token: 0x040032C6 RID: 12998
		public static readonly OpCode Conv_U = new OpCode(18211071, 84215041);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x040032C7 RID: 12999
		public static readonly OpCode Prefix7 = new OpCode(1243391, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x040032C8 RID: 13000
		public static readonly OpCode Prefix6 = new OpCode(1243647, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x040032C9 RID: 13001
		public static readonly OpCode Prefix5 = new OpCode(1243903, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x040032CA RID: 13002
		public static readonly OpCode Prefix4 = new OpCode(1244159, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x040032CB RID: 13003
		public static readonly OpCode Prefix3 = new OpCode(1244415, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x040032CC RID: 13004
		public static readonly OpCode Prefix2 = new OpCode(1244671, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x040032CD RID: 13005
		public static readonly OpCode Prefix1 = new OpCode(1244927, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x040032CE RID: 13006
		public static readonly OpCode Prefixref = new OpCode(1245183, 67437057);

		/// <summary>Returns an unmanaged pointer to the argument list of the current method.</summary>
		// Token: 0x040032CF RID: 13007
		public static readonly OpCode Arglist = new OpCode(1376510, 84215042);

		/// <summary>Compares two values. If they are equal, the integer value 1 <see langword="(int32" />) is pushed onto the evaluation stack; otherwise 0 (<see langword="int32" />) is pushed onto the evaluation stack.</summary>
		// Token: 0x040032D0 RID: 13008
		public static readonly OpCode Ceq = new OpCode(34931198, 84215042);

		/// <summary>Compares two values. If the first value is greater than the second, the integer value 1 <see langword="(int32" />) is pushed onto the evaluation stack; otherwise 0 (<see langword="int32" />) is pushed onto the evaluation stack.</summary>
		// Token: 0x040032D1 RID: 13009
		public static readonly OpCode Cgt = new OpCode(34931454, 84215042);

		/// <summary>Compares two unsigned or unordered values. If the first value is greater than the second, the integer value 1 <see langword="(int32" />) is pushed onto the evaluation stack; otherwise 0 (<see langword="int32" />) is pushed onto the evaluation stack.</summary>
		// Token: 0x040032D2 RID: 13010
		public static readonly OpCode Cgt_Un = new OpCode(34931710, 84215042);

		/// <summary>Compares two values. If the first value is less than the second, the integer value 1 <see langword="(int32" />) is pushed onto the evaluation stack; otherwise 0 (<see langword="int32" />) is pushed onto the evaluation stack.</summary>
		// Token: 0x040032D3 RID: 13011
		public static readonly OpCode Clt = new OpCode(34931966, 84215042);

		/// <summary>Compares the unsigned or unordered values <paramref name="value1" /> and <paramref name="value2" />. If <paramref name="value1" /> is less than <paramref name="value2" />, then the integer value 1 <see langword="(int32" />) is pushed onto the evaluation stack; otherwise 0 (<see langword="int32" />) is pushed onto the evaluation stack.</summary>
		// Token: 0x040032D4 RID: 13012
		public static readonly OpCode Clt_Un = new OpCode(34932222, 84215042);

		/// <summary>Pushes an unmanaged pointer (type <see langword="native int" />) to the native code implementing a specific method onto the evaluation stack.</summary>
		// Token: 0x040032D5 RID: 13013
		public static readonly OpCode Ldftn = new OpCode(1378046, 84149506);

		/// <summary>Pushes an unmanaged pointer (type <see langword="native int" />) to the native code implementing a particular virtual method associated with a specified object onto the evaluation stack.</summary>
		// Token: 0x040032D6 RID: 13014
		public static readonly OpCode Ldvirtftn = new OpCode(169150462, 84149506);

		/// <summary>Loads an argument (referenced by a specified index value) onto the stack.</summary>
		// Token: 0x040032D7 RID: 13015
		public static readonly OpCode Ldarg = new OpCode(1247742, 84804866);

		/// <summary>Load an argument address onto the evaluation stack.</summary>
		// Token: 0x040032D8 RID: 13016
		public static readonly OpCode Ldarga = new OpCode(1379070, 84804866);

		/// <summary>Stores the value on top of the evaluation stack in the argument slot at a specified index.</summary>
		// Token: 0x040032D9 RID: 13017
		public static readonly OpCode Starg = new OpCode(17959934, 84804866);

		/// <summary>Loads the local variable at a specific index onto the evaluation stack.</summary>
		// Token: 0x040032DA RID: 13018
		public static readonly OpCode Ldloc = new OpCode(1248510, 84804866);

		/// <summary>Loads the address of the local variable at a specific index onto the evaluation stack.</summary>
		// Token: 0x040032DB RID: 13019
		public static readonly OpCode Ldloca = new OpCode(1379838, 84804866);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at a specified index.</summary>
		// Token: 0x040032DC RID: 13020
		public static readonly OpCode Stloc = new OpCode(17960702, 84804866);

		/// <summary>Allocates a certain number of bytes from the local dynamic memory pool and pushes the address (a transient pointer, type <see langword="*" />) of the first allocated byte onto the evaluation stack.</summary>
		// Token: 0x040032DD RID: 13021
		public static readonly OpCode Localloc = new OpCode(51711998, 84215042);

		/// <summary>Transfers control from the <see langword="filter" /> clause of an exception back to the Common Language Infrastructure (CLI) exception handler.</summary>
		// Token: 0x040032DE RID: 13022
		public static readonly OpCode Endfilter = new OpCode(51515902, 117769474);

		/// <summary>Indicates that an address currently atop the evaluation stack might not be aligned to the natural size of the immediately following <see langword="ldind" />, <see langword="stind" />, <see langword="ldfld" />, <see langword="stfld" />, <see langword="ldobj" />, <see langword="stobj" />, <see langword="initblk" />, or <see langword="cpblk" /> instruction.</summary>
		// Token: 0x040032DF RID: 13023
		public static readonly OpCode Unaligned = new OpCode(1184510, 68158466);

		/// <summary>Specifies that an address currently atop the evaluation stack might be volatile, and the results of reading that location cannot be cached or that multiple stores to that location cannot be suppressed.</summary>
		// Token: 0x040032E0 RID: 13024
		public static readonly OpCode Volatile = new OpCode(1184766, 67437570);

		/// <summary>Performs a postfixed method call instruction such that the current method's stack frame is removed before the actual call instruction is executed.</summary>
		// Token: 0x040032E1 RID: 13025
		public static readonly OpCode Tailcall = new OpCode(1185022, 67437570);

		/// <summary>Initializes each field of the value type at a specified address to a null reference or a 0 of the appropriate primitive type.</summary>
		// Token: 0x040032E2 RID: 13026
		public static readonly OpCode Initobj = new OpCode(51516926, 84738818);

		/// <summary>Constrains the type on which a virtual method call is made.</summary>
		// Token: 0x040032E3 RID: 13027
		public static readonly OpCode Constrained = new OpCode(1185534, 67961858);

		/// <summary>Copies a specified number bytes from a source address to a destination address.</summary>
		// Token: 0x040032E4 RID: 13028
		public static readonly OpCode Cpblk = new OpCode(118626302, 84215042);

		/// <summary>Initializes a specified block of memory at a specific address to a given size and initial value.</summary>
		// Token: 0x040032E5 RID: 13029
		public static readonly OpCode Initblk = new OpCode(118626558, 84215042);

		/// <summary>Rethrows the current exception.</summary>
		// Token: 0x040032E6 RID: 13030
		public static readonly OpCode Rethrow = new OpCode(1186558, 134546178);

		/// <summary>Pushes the size, in bytes, of a supplied value type onto the evaluation stack.</summary>
		// Token: 0x040032E7 RID: 13031
		public static readonly OpCode Sizeof = new OpCode(1383678, 84739330);

		/// <summary>Retrieves the type token embedded in a typed reference.</summary>
		// Token: 0x040032E8 RID: 13032
		public static readonly OpCode Refanytype = new OpCode(18161150, 84215042);

		/// <summary>Specifies that the subsequent array address operation performs no type check at run time, and that it returns a managed pointer whose mutability is restricted.</summary>
		// Token: 0x040032E9 RID: 13033
		public static readonly OpCode Readonly = new OpCode(1187582, 67437570);
	}
}
