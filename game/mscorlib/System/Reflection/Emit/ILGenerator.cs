using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Generates Microsoft intermediate language (MSIL) instructions.</summary>
	// Token: 0x0200092D RID: 2349
	[StructLayout(LayoutKind.Sequential)]
	public class ILGenerator : _ILGenerator
	{
		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array that receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060050A0 RID: 20640 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _ILGenerator.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060050A1 RID: 20641 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _ILGenerator.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060050A2 RID: 20642 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _ILGenerator.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060050A3 RID: 20643 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _ILGenerator.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x000FB406 File Offset: 0x000F9606
		internal ILGenerator(Module m, TokenGenerator token_gen, int size)
		{
			if (size < 0)
			{
				size = 128;
			}
			this.code = new byte[size];
			this.token_fixups = new ILTokenInfo[8];
			this.module = m;
			this.token_gen = token_gen;
		}

		// Token: 0x060050A5 RID: 20645 RVA: 0x000FB440 File Offset: 0x000F9640
		private void add_token_fixup(MemberInfo mi)
		{
			if (this.num_token_fixups == this.token_fixups.Length)
			{
				ILTokenInfo[] array = new ILTokenInfo[this.num_token_fixups * 2];
				this.token_fixups.CopyTo(array, 0);
				this.token_fixups = array;
			}
			this.token_fixups[this.num_token_fixups].member = mi;
			ILTokenInfo[] array2 = this.token_fixups;
			int num = this.num_token_fixups;
			this.num_token_fixups = num + 1;
			array2[num].code_pos = this.code_len;
		}

		// Token: 0x060050A6 RID: 20646 RVA: 0x000FB4C0 File Offset: 0x000F96C0
		private void make_room(int nbytes)
		{
			if (this.code_len + nbytes < this.code.Length)
			{
				return;
			}
			byte[] destinationArray = new byte[(this.code_len + nbytes) * 2 + 128];
			Array.Copy(this.code, 0, destinationArray, 0, this.code.Length);
			this.code = destinationArray;
		}

		// Token: 0x060050A7 RID: 20647 RVA: 0x000FB514 File Offset: 0x000F9714
		private void emit_int(int val)
		{
			byte[] array = this.code;
			int num = this.code_len;
			this.code_len = num + 1;
			array[num] = (byte)(val & 255);
			byte[] array2 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array2[num] = (byte)(val >> 8 & 255);
			byte[] array3 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array3[num] = (byte)(val >> 16 & 255);
			byte[] array4 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array4[num] = (byte)(val >> 24 & 255);
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x000FB5AC File Offset: 0x000F97AC
		private void ll_emit(OpCode opcode)
		{
			int num;
			if (opcode.Size == 2)
			{
				byte[] array = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array[num] = opcode.op1;
			}
			byte[] array2 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array2[num] = opcode.op2;
			switch (opcode.StackBehaviourPush)
			{
			case StackBehaviour.Push1:
			case StackBehaviour.Pushi:
			case StackBehaviour.Pushi8:
			case StackBehaviour.Pushr4:
			case StackBehaviour.Pushr8:
			case StackBehaviour.Pushref:
			case StackBehaviour.Varpush:
				this.cur_stack++;
				break;
			case StackBehaviour.Push1_push1:
				this.cur_stack += 2;
				break;
			}
			if (this.max_stack < this.cur_stack)
			{
				this.max_stack = this.cur_stack;
			}
			switch (opcode.StackBehaviourPop)
			{
			case StackBehaviour.Pop1:
			case StackBehaviour.Popi:
			case StackBehaviour.Popref:
				this.cur_stack--;
				return;
			case StackBehaviour.Pop1_pop1:
			case StackBehaviour.Popi_pop1:
			case StackBehaviour.Popi_popi:
			case StackBehaviour.Popi_popi8:
			case StackBehaviour.Popi_popr4:
			case StackBehaviour.Popi_popr8:
			case StackBehaviour.Popref_pop1:
			case StackBehaviour.Popref_popi:
				this.cur_stack -= 2;
				return;
			case StackBehaviour.Popi_popi_popi:
			case StackBehaviour.Popref_popi_popi:
			case StackBehaviour.Popref_popi_popi8:
			case StackBehaviour.Popref_popi_popr4:
			case StackBehaviour.Popref_popi_popr8:
			case StackBehaviour.Popref_popi_popref:
				this.cur_stack -= 3;
				break;
			case StackBehaviour.Push0:
			case StackBehaviour.Push1:
			case StackBehaviour.Push1_push1:
			case StackBehaviour.Pushi:
			case StackBehaviour.Pushi8:
			case StackBehaviour.Pushr4:
			case StackBehaviour.Pushr8:
			case StackBehaviour.Pushref:
			case StackBehaviour.Varpop:
				break;
			default:
				return;
			}
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x000FB713 File Offset: 0x000F9913
		private static int target_len(OpCode opcode)
		{
			if (opcode.OperandType == OperandType.InlineBrTarget)
			{
				return 4;
			}
			return 1;
		}

		// Token: 0x060050AA RID: 20650 RVA: 0x000FB724 File Offset: 0x000F9924
		private void InternalEndClause()
		{
			switch (this.ex_handlers[this.cur_block].LastClauseType())
			{
			case -1:
			case 0:
			case 1:
				this.Emit(OpCodes.Leave, this.ex_handlers[this.cur_block].end);
				return;
			case 2:
			case 4:
				this.Emit(OpCodes.Endfinally);
				break;
			case 3:
				break;
			default:
				return;
			}
		}

		/// <summary>Begins a catch block.</summary>
		/// <param name="exceptionType">The <see cref="T:System.Type" /> object that represents the exception.</param>
		/// <exception cref="T:System.ArgumentException">The catch block is within a filtered exception.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="exceptionType" /> is <see langword="null" />, and the exception filter block has not returned a value that indicates that finally blocks should be run until this catch block is located.</exception>
		/// <exception cref="T:System.NotSupportedException">The Microsoft intermediate language (MSIL) being generated is not currently in an exception block.</exception>
		// Token: 0x060050AB RID: 20651 RVA: 0x000FB798 File Offset: 0x000F9998
		public virtual void BeginCatchBlock(Type exceptionType)
		{
			if (this.open_blocks == null)
			{
				this.open_blocks = new Stack(2);
			}
			if (this.open_blocks.Count <= 0)
			{
				throw new NotSupportedException("Not in an exception block");
			}
			if (exceptionType != null && exceptionType.IsUserType)
			{
				throw new NotSupportedException("User defined subclasses of System.Type are not yet supported.");
			}
			if (this.ex_handlers[this.cur_block].LastClauseType() == -1)
			{
				if (exceptionType != null)
				{
					throw new ArgumentException("Do not supply an exception type for filter clause");
				}
				this.Emit(OpCodes.Endfilter);
				this.ex_handlers[this.cur_block].PatchFilterClause(this.code_len);
			}
			else
			{
				this.InternalEndClause();
				this.ex_handlers[this.cur_block].AddCatch(exceptionType, this.code_len);
			}
			this.cur_stack = 1;
			if (this.max_stack < this.cur_stack)
			{
				this.max_stack = this.cur_stack;
			}
		}

		/// <summary>Begins an exception block for a filtered exception.</summary>
		/// <exception cref="T:System.NotSupportedException">The Microsoft intermediate language (MSIL) being generated is not currently in an exception block.  
		///  -or-  
		///  This <see cref="T:System.Reflection.Emit.ILGenerator" /> belongs to a <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		// Token: 0x060050AC RID: 20652 RVA: 0x000FB888 File Offset: 0x000F9A88
		public virtual void BeginExceptFilterBlock()
		{
			if (this.open_blocks == null)
			{
				this.open_blocks = new Stack(2);
			}
			if (this.open_blocks.Count <= 0)
			{
				throw new NotSupportedException("Not in an exception block");
			}
			this.InternalEndClause();
			this.ex_handlers[this.cur_block].AddFilter(this.code_len);
		}

		/// <summary>Begins an exception block for a non-filtered exception.</summary>
		/// <returns>The label for the end of the block. This will leave you in the correct place to execute finally blocks or to finish the try.</returns>
		// Token: 0x060050AD RID: 20653 RVA: 0x000FB8E4 File Offset: 0x000F9AE4
		public virtual Label BeginExceptionBlock()
		{
			if (this.open_blocks == null)
			{
				this.open_blocks = new Stack(2);
			}
			if (this.ex_handlers != null)
			{
				this.cur_block = this.ex_handlers.Length;
				ILExceptionInfo[] destinationArray = new ILExceptionInfo[this.cur_block + 1];
				Array.Copy(this.ex_handlers, destinationArray, this.cur_block);
				this.ex_handlers = destinationArray;
			}
			else
			{
				this.ex_handlers = new ILExceptionInfo[1];
				this.cur_block = 0;
			}
			this.open_blocks.Push(this.cur_block);
			this.ex_handlers[this.cur_block].start = this.code_len;
			return this.ex_handlers[this.cur_block].end = this.DefineLabel();
		}

		/// <summary>Begins an exception fault block in the Microsoft intermediate language (MSIL) stream.</summary>
		/// <exception cref="T:System.NotSupportedException">The MSIL being generated is not currently in an exception block.  
		///  -or-  
		///  This <see cref="T:System.Reflection.Emit.ILGenerator" /> belongs to a <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		// Token: 0x060050AE RID: 20654 RVA: 0x000FB9A8 File Offset: 0x000F9BA8
		public virtual void BeginFaultBlock()
		{
			if (this.open_blocks == null)
			{
				this.open_blocks = new Stack(2);
			}
			if (this.open_blocks.Count <= 0)
			{
				throw new NotSupportedException("Not in an exception block");
			}
			if (this.ex_handlers[this.cur_block].LastClauseType() == -1)
			{
				this.Emit(OpCodes.Leave, this.ex_handlers[this.cur_block].end);
				this.ex_handlers[this.cur_block].PatchFilterClause(this.code_len);
			}
			this.InternalEndClause();
			this.ex_handlers[this.cur_block].AddFault(this.code_len);
		}

		/// <summary>Begins a finally block in the Microsoft intermediate language (MSIL) instruction stream.</summary>
		/// <exception cref="T:System.NotSupportedException">The MSIL being generated is not currently in an exception block.</exception>
		// Token: 0x060050AF RID: 20655 RVA: 0x000FBA5C File Offset: 0x000F9C5C
		public virtual void BeginFinallyBlock()
		{
			if (this.open_blocks == null)
			{
				this.open_blocks = new Stack(2);
			}
			if (this.open_blocks.Count <= 0)
			{
				throw new NotSupportedException("Not in an exception block");
			}
			this.InternalEndClause();
			if (this.ex_handlers[this.cur_block].LastClauseType() == -1)
			{
				this.Emit(OpCodes.Leave, this.ex_handlers[this.cur_block].end);
				this.ex_handlers[this.cur_block].PatchFilterClause(this.code_len);
			}
			this.ex_handlers[this.cur_block].AddFinally(this.code_len);
		}

		/// <summary>Begins a lexical scope.</summary>
		/// <exception cref="T:System.NotSupportedException">This <see cref="T:System.Reflection.Emit.ILGenerator" /> belongs to a <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		// Token: 0x060050B0 RID: 20656 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public virtual void BeginScope()
		{
		}

		/// <summary>Declares a local variable of the specified type.</summary>
		/// <param name="localType">A <see cref="T:System.Type" /> object that represents the type of the local variable.</param>
		/// <returns>The declared local variable.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created by the <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method.</exception>
		// Token: 0x060050B1 RID: 20657 RVA: 0x000FBB0E File Offset: 0x000F9D0E
		public virtual LocalBuilder DeclareLocal(Type localType)
		{
			return this.DeclareLocal(localType, false);
		}

		/// <summary>Declares a local variable of the specified type, optionally pinning the object referred to by the variable.</summary>
		/// <param name="localType">A <see cref="T:System.Type" /> object that represents the type of the local variable.</param>
		/// <param name="pinned">
		///   <see langword="true" /> to pin the object in memory; otherwise, <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.Reflection.Emit.LocalBuilder" /> object that represents the local variable.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created by the <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method.  
		///  -or-  
		///  The method body of the enclosing method has been created by the <see cref="M:System.Reflection.Emit.MethodBuilder.CreateMethodBody(System.Byte[],System.Int32)" /> method.</exception>
		/// <exception cref="T:System.NotSupportedException">The method with which this <see cref="T:System.Reflection.Emit.ILGenerator" /> is associated is not represented by a <see cref="T:System.Reflection.Emit.MethodBuilder" />.</exception>
		// Token: 0x060050B2 RID: 20658 RVA: 0x000FBB18 File Offset: 0x000F9D18
		public virtual LocalBuilder DeclareLocal(Type localType, bool pinned)
		{
			if (localType == null)
			{
				throw new ArgumentNullException("localType");
			}
			if (localType.IsUserType)
			{
				throw new NotSupportedException("User defined subclasses of System.Type are not yet supported.");
			}
			LocalBuilder localBuilder = new LocalBuilder(localType, this);
			localBuilder.is_pinned = pinned;
			if (this.locals != null)
			{
				LocalBuilder[] array = new LocalBuilder[this.locals.Length + 1];
				Array.Copy(this.locals, array, this.locals.Length);
				array[this.locals.Length] = localBuilder;
				this.locals = array;
			}
			else
			{
				this.locals = new LocalBuilder[1];
				this.locals[0] = localBuilder;
			}
			localBuilder.position = (ushort)(this.locals.Length - 1);
			return localBuilder;
		}

		/// <summary>Declares a new label.</summary>
		/// <returns>A new label that can be used as a token for branching.</returns>
		// Token: 0x060050B3 RID: 20659 RVA: 0x000FBBC4 File Offset: 0x000F9DC4
		public virtual Label DefineLabel()
		{
			if (this.labels == null)
			{
				this.labels = new ILGenerator.LabelData[4];
			}
			else if (this.num_labels >= this.labels.Length)
			{
				ILGenerator.LabelData[] destinationArray = new ILGenerator.LabelData[this.labels.Length * 2];
				Array.Copy(this.labels, destinationArray, this.labels.Length);
				this.labels = destinationArray;
			}
			this.labels[this.num_labels] = new ILGenerator.LabelData(-1, 0);
			int num = this.num_labels;
			this.num_labels = num + 1;
			return new Label(num);
		}

		/// <summary>Puts the specified instruction onto the stream of instructions.</summary>
		/// <param name="opcode">The Microsoft Intermediate Language (MSIL) instruction to be put onto the stream.</param>
		// Token: 0x060050B4 RID: 20660 RVA: 0x000FBC50 File Offset: 0x000F9E50
		public virtual void Emit(OpCode opcode)
		{
			this.make_room(2);
			this.ll_emit(opcode);
		}

		/// <summary>Puts the specified instruction and character argument onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be put onto the stream.</param>
		/// <param name="arg">The character argument pushed onto the stream immediately after the instruction.</param>
		// Token: 0x060050B5 RID: 20661 RVA: 0x000FBC60 File Offset: 0x000F9E60
		public virtual void Emit(OpCode opcode, byte arg)
		{
			this.make_room(3);
			this.ll_emit(opcode);
			byte[] array = this.code;
			int num = this.code_len;
			this.code_len = num + 1;
			array[num] = arg;
		}

		/// <summary>Puts the specified instruction and metadata token for the specified constructor onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="con">A <see langword="ConstructorInfo" /> representing a constructor.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is <see langword="null" />. This exception is new in the .NET Framework 4.</exception>
		// Token: 0x060050B6 RID: 20662 RVA: 0x000FBC94 File Offset: 0x000F9E94
		[ComVisible(true)]
		public virtual void Emit(OpCode opcode, ConstructorInfo con)
		{
			int token = this.token_gen.GetToken(con, true);
			this.make_room(6);
			this.ll_emit(opcode);
			if (con.DeclaringType.Module == this.module || con is ConstructorOnTypeBuilderInst || con is ConstructorBuilder)
			{
				this.add_token_fixup(con);
			}
			this.emit_int(token);
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				this.cur_stack -= con.GetParametersCount();
			}
		}

		/// <summary>Puts the specified instruction and numerical argument onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be put onto the stream. Defined in the <see langword="OpCodes" /> enumeration.</param>
		/// <param name="arg">The numerical argument pushed onto the stream immediately after the instruction.</param>
		// Token: 0x060050B7 RID: 20663 RVA: 0x000FBD14 File Offset: 0x000F9F14
		public virtual void Emit(OpCode opcode, double arg)
		{
			byte[] bytes = BitConverter.GetBytes(arg);
			this.make_room(10);
			this.ll_emit(opcode);
			if (BitConverter.IsLittleEndian)
			{
				Array.Copy(bytes, 0, this.code, this.code_len, 8);
				this.code_len += 8;
				return;
			}
			byte[] array = this.code;
			int num = this.code_len;
			this.code_len = num + 1;
			array[num] = bytes[7];
			byte[] array2 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array2[num] = bytes[6];
			byte[] array3 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array3[num] = bytes[5];
			byte[] array4 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array4[num] = bytes[4];
			byte[] array5 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array5[num] = bytes[3];
			byte[] array6 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array6[num] = bytes[2];
			byte[] array7 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array7[num] = bytes[1];
			byte[] array8 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array8[num] = bytes[0];
		}

		/// <summary>Puts the specified instruction and metadata token for the specified field onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="field">A <see langword="FieldInfo" /> representing a field.</param>
		// Token: 0x060050B8 RID: 20664 RVA: 0x000FBE3C File Offset: 0x000FA03C
		public virtual void Emit(OpCode opcode, FieldInfo field)
		{
			int token = this.token_gen.GetToken(field, true);
			this.make_room(6);
			this.ll_emit(opcode);
			if (field.DeclaringType.Module == this.module || field is FieldOnTypeBuilderInst || field is FieldBuilder)
			{
				this.add_token_fixup(field);
			}
			this.emit_int(token);
		}

		/// <summary>Puts the specified instruction and numerical argument onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="arg">The <see langword="Int" /> argument pushed onto the stream immediately after the instruction.</param>
		// Token: 0x060050B9 RID: 20665 RVA: 0x000FBE9C File Offset: 0x000FA09C
		public virtual void Emit(OpCode opcode, short arg)
		{
			this.make_room(4);
			this.ll_emit(opcode);
			byte[] array = this.code;
			int num = this.code_len;
			this.code_len = num + 1;
			array[num] = (byte)(arg & 255);
			byte[] array2 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array2[num] = (byte)(arg >> 8 & 255);
		}

		/// <summary>Puts the specified instruction and numerical argument onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be put onto the stream.</param>
		/// <param name="arg">The numerical argument pushed onto the stream immediately after the instruction.</param>
		// Token: 0x060050BA RID: 20666 RVA: 0x000FBEF9 File Offset: 0x000FA0F9
		public virtual void Emit(OpCode opcode, int arg)
		{
			this.make_room(6);
			this.ll_emit(opcode);
			this.emit_int(arg);
		}

		/// <summary>Puts the specified instruction and numerical argument onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be put onto the stream.</param>
		/// <param name="arg">The numerical argument pushed onto the stream immediately after the instruction.</param>
		// Token: 0x060050BB RID: 20667 RVA: 0x000FBF10 File Offset: 0x000FA110
		public virtual void Emit(OpCode opcode, long arg)
		{
			this.make_room(10);
			this.ll_emit(opcode);
			byte[] array = this.code;
			int num = this.code_len;
			this.code_len = num + 1;
			array[num] = (byte)(arg & 255L);
			byte[] array2 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array2[num] = (byte)(arg >> 8 & 255L);
			byte[] array3 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array3[num] = (byte)(arg >> 16 & 255L);
			byte[] array4 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array4[num] = (byte)(arg >> 24 & 255L);
			byte[] array5 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array5[num] = (byte)(arg >> 32 & 255L);
			byte[] array6 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array6[num] = (byte)(arg >> 40 & 255L);
			byte[] array7 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array7[num] = (byte)(arg >> 48 & 255L);
			byte[] array8 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array8[num] = (byte)(arg >> 56 & 255L);
		}

		/// <summary>Puts the specified instruction onto the Microsoft intermediate language (MSIL) stream and leaves space to include a label when fixes are done.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="label">The label to which to branch from this location.</param>
		// Token: 0x060050BC RID: 20668 RVA: 0x000FC048 File Offset: 0x000FA248
		public virtual void Emit(OpCode opcode, Label label)
		{
			int num = ILGenerator.target_len(opcode);
			this.make_room(6);
			this.ll_emit(opcode);
			if (this.cur_stack > this.labels[label.label].maxStack)
			{
				this.labels[label.label].maxStack = this.cur_stack;
			}
			if (this.fixups == null)
			{
				this.fixups = new ILGenerator.LabelFixup[4];
			}
			else if (this.num_fixups >= this.fixups.Length)
			{
				ILGenerator.LabelFixup[] destinationArray = new ILGenerator.LabelFixup[this.fixups.Length * 2];
				Array.Copy(this.fixups, destinationArray, this.fixups.Length);
				this.fixups = destinationArray;
			}
			this.fixups[this.num_fixups].offset = num;
			this.fixups[this.num_fixups].pos = this.code_len;
			this.fixups[this.num_fixups].label_idx = label.label;
			this.num_fixups++;
			this.code_len += num;
		}

		/// <summary>Puts the specified instruction onto the Microsoft intermediate language (MSIL) stream and leaves space to include a label when fixes are done.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="labels">The array of label objects to which to branch from this location. All of the labels will be used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is <see langword="null" />. This exception is new in the .NET Framework 4.</exception>
		// Token: 0x060050BD RID: 20669 RVA: 0x000FC160 File Offset: 0x000FA360
		public virtual void Emit(OpCode opcode, Label[] labels)
		{
			if (labels == null)
			{
				throw new ArgumentNullException("labels");
			}
			int num = labels.Length;
			this.make_room(6 + num * 4);
			this.ll_emit(opcode);
			for (int i = 0; i < num; i++)
			{
				if (this.cur_stack > this.labels[labels[i].label].maxStack)
				{
					this.labels[labels[i].label].maxStack = this.cur_stack;
				}
			}
			this.emit_int(num);
			if (this.fixups == null)
			{
				this.fixups = new ILGenerator.LabelFixup[4 + num];
			}
			else if (this.num_fixups + num >= this.fixups.Length)
			{
				ILGenerator.LabelFixup[] destinationArray = new ILGenerator.LabelFixup[num + this.fixups.Length * 2];
				Array.Copy(this.fixups, destinationArray, this.fixups.Length);
				this.fixups = destinationArray;
			}
			int j = 0;
			int num2 = num * 4;
			while (j < num)
			{
				this.fixups[this.num_fixups].offset = num2;
				this.fixups[this.num_fixups].pos = this.code_len;
				this.fixups[this.num_fixups].label_idx = labels[j].label;
				this.num_fixups++;
				this.code_len += 4;
				j++;
				num2 -= 4;
			}
		}

		/// <summary>Puts the specified instruction onto the Microsoft intermediate language (MSIL) stream followed by the index of the given local variable.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="local">A local variable.</param>
		/// <exception cref="T:System.ArgumentException">The parent method of the <paramref name="local" /> parameter does not match the method associated with this <see cref="T:System.Reflection.Emit.ILGenerator" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="local" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="opcode" /> is a single-byte instruction, and <paramref name="local" /> represents a local variable with an index greater than <see langword="Byte.MaxValue" />.</exception>
		// Token: 0x060050BE RID: 20670 RVA: 0x000FC2CC File Offset: 0x000FA4CC
		public virtual void Emit(OpCode opcode, LocalBuilder local)
		{
			if (local == null)
			{
				throw new ArgumentNullException("local");
			}
			if (local.ilgen != this)
			{
				throw new ArgumentException("Trying to emit a local from a different ILGenerator.");
			}
			uint position = (uint)local.position;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			this.make_room(6);
			if (opcode.StackBehaviourPop == StackBehaviour.Pop1)
			{
				this.cur_stack--;
				flag2 = true;
			}
			else if (opcode.StackBehaviourPush == StackBehaviour.Push1 || opcode.StackBehaviourPush == StackBehaviour.Pushi)
			{
				this.cur_stack++;
				flag3 = true;
				if (this.cur_stack > this.max_stack)
				{
					this.max_stack = this.cur_stack;
				}
				flag = (opcode.StackBehaviourPush == StackBehaviour.Pushi);
			}
			if (flag)
			{
				int num;
				if (position < 256U)
				{
					byte[] array = this.code;
					num = this.code_len;
					this.code_len = num + 1;
					array[num] = 18;
					byte[] array2 = this.code;
					num = this.code_len;
					this.code_len = num + 1;
					array2[num] = (byte)position;
					return;
				}
				byte[] array3 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array3[num] = 254;
				byte[] array4 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array4[num] = 13;
				byte[] array5 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array5[num] = (byte)(position & 255U);
				byte[] array6 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array6[num] = (byte)(position >> 8 & 255U);
				return;
			}
			else if (flag2)
			{
				int num;
				if (position < 4U)
				{
					byte[] array7 = this.code;
					num = this.code_len;
					this.code_len = num + 1;
					array7[num] = (byte)(10U + position);
					return;
				}
				if (position < 256U)
				{
					byte[] array8 = this.code;
					num = this.code_len;
					this.code_len = num + 1;
					array8[num] = 19;
					byte[] array9 = this.code;
					num = this.code_len;
					this.code_len = num + 1;
					array9[num] = (byte)position;
					return;
				}
				byte[] array10 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array10[num] = 254;
				byte[] array11 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array11[num] = 14;
				byte[] array12 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array12[num] = (byte)(position & 255U);
				byte[] array13 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array13[num] = (byte)(position >> 8 & 255U);
				return;
			}
			else
			{
				if (!flag3)
				{
					this.ll_emit(opcode);
					return;
				}
				int num;
				if (position < 4U)
				{
					byte[] array14 = this.code;
					num = this.code_len;
					this.code_len = num + 1;
					array14[num] = (byte)(6U + position);
					return;
				}
				if (position < 256U)
				{
					byte[] array15 = this.code;
					num = this.code_len;
					this.code_len = num + 1;
					array15[num] = 17;
					byte[] array16 = this.code;
					num = this.code_len;
					this.code_len = num + 1;
					array16[num] = (byte)position;
					return;
				}
				byte[] array17 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array17[num] = 254;
				byte[] array18 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array18[num] = 12;
				byte[] array19 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array19[num] = (byte)(position & 255U);
				byte[] array20 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array20[num] = (byte)(position >> 8 & 255U);
				return;
			}
		}

		/// <summary>Puts the specified instruction onto the Microsoft intermediate language (MSIL) stream followed by the metadata token for the given method.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="meth">A <see langword="MethodInfo" /> representing a method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="meth" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="meth" /> is a generic method for which the <see cref="P:System.Reflection.MethodBase.IsGenericMethodDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x060050BF RID: 20671 RVA: 0x000FC634 File Offset: 0x000FA834
		public virtual void Emit(OpCode opcode, MethodInfo meth)
		{
			if (meth == null)
			{
				throw new ArgumentNullException("meth");
			}
			if (meth is DynamicMethod && (opcode == OpCodes.Ldftn || opcode == OpCodes.Ldvirtftn || opcode == OpCodes.Ldtoken))
			{
				throw new ArgumentException("Ldtoken, Ldftn and Ldvirtftn OpCodes cannot target DynamicMethods.");
			}
			int token = this.token_gen.GetToken(meth, true);
			this.make_room(6);
			this.ll_emit(opcode);
			Type declaringType = meth.DeclaringType;
			if (declaringType != null && (declaringType.Module == this.module || meth is MethodOnTypeBuilderInst || meth is MethodBuilder))
			{
				this.add_token_fixup(meth);
			}
			this.emit_int(token);
			if (meth.ReturnType != typeof(void))
			{
				this.cur_stack++;
			}
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				this.cur_stack -= meth.GetParametersCount();
			}
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x000FC730 File Offset: 0x000FA930
		private void Emit(OpCode opcode, MethodInfo method, int token)
		{
			this.make_room(6);
			this.ll_emit(opcode);
			Type declaringType = method.DeclaringType;
			if (declaringType != null && (declaringType.Module == this.module || method is MethodBuilder))
			{
				this.add_token_fixup(method);
			}
			this.emit_int(token);
			if (method.ReturnType != typeof(void))
			{
				this.cur_stack++;
			}
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				this.cur_stack -= method.GetParametersCount();
			}
		}

		/// <summary>Puts the specified instruction and character argument onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be put onto the stream.</param>
		/// <param name="arg">The character argument pushed onto the stream immediately after the instruction.</param>
		// Token: 0x060050C1 RID: 20673 RVA: 0x000FC7C8 File Offset: 0x000FA9C8
		[CLSCompliant(false)]
		public void Emit(OpCode opcode, sbyte arg)
		{
			this.make_room(3);
			this.ll_emit(opcode);
			byte[] array = this.code;
			int num = this.code_len;
			this.code_len = num + 1;
			array[num] = (byte)arg;
		}

		/// <summary>Puts the specified instruction and a signature token onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="signature">A helper for constructing a signature token.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="signature" /> is <see langword="null" />.</exception>
		// Token: 0x060050C2 RID: 20674 RVA: 0x000FC800 File Offset: 0x000FAA00
		public virtual void Emit(OpCode opcode, SignatureHelper signature)
		{
			int token = this.token_gen.GetToken(signature);
			this.make_room(6);
			this.ll_emit(opcode);
			this.emit_int(token);
		}

		/// <summary>Puts the specified instruction and numerical argument onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be put onto the stream.</param>
		/// <param name="arg">The <see langword="Single" /> argument pushed onto the stream immediately after the instruction.</param>
		// Token: 0x060050C3 RID: 20675 RVA: 0x000FC830 File Offset: 0x000FAA30
		public virtual void Emit(OpCode opcode, float arg)
		{
			byte[] bytes = BitConverter.GetBytes(arg);
			this.make_room(6);
			this.ll_emit(opcode);
			if (BitConverter.IsLittleEndian)
			{
				Array.Copy(bytes, 0, this.code, this.code_len, 4);
				this.code_len += 4;
				return;
			}
			byte[] array = this.code;
			int num = this.code_len;
			this.code_len = num + 1;
			array[num] = bytes[3];
			byte[] array2 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array2[num] = bytes[2];
			byte[] array3 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array3[num] = bytes[1];
			byte[] array4 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array4[num] = bytes[0];
		}

		/// <summary>Puts the specified instruction onto the Microsoft intermediate language (MSIL) stream followed by the metadata token for the given string.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="str">The <see langword="String" /> to be emitted.</param>
		// Token: 0x060050C4 RID: 20676 RVA: 0x000FC8E8 File Offset: 0x000FAAE8
		public virtual void Emit(OpCode opcode, string str)
		{
			int token = this.token_gen.GetToken(str);
			this.make_room(6);
			this.ll_emit(opcode);
			this.emit_int(token);
		}

		/// <summary>Puts the specified instruction onto the Microsoft intermediate language (MSIL) stream followed by the metadata token for the given type.</summary>
		/// <param name="opcode">The MSIL instruction to be put onto the stream.</param>
		/// <param name="cls">A <see langword="Type" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cls" /> is <see langword="null" />.</exception>
		// Token: 0x060050C5 RID: 20677 RVA: 0x000FC918 File Offset: 0x000FAB18
		public virtual void Emit(OpCode opcode, Type cls)
		{
			if (cls != null && cls.IsByRef)
			{
				throw new ArgumentException("Cannot get TypeToken for a ByRef type.");
			}
			this.make_room(6);
			this.ll_emit(opcode);
			int token = this.token_gen.GetToken(cls, opcode != OpCodes.Ldtoken);
			if (cls is TypeBuilderInstantiation || cls is SymbolType || cls is TypeBuilder || cls is GenericTypeParameterBuilder || cls is EnumBuilder)
			{
				this.add_token_fixup(cls);
			}
			this.emit_int(token);
		}

		/// <summary>Puts a <see langword="call" /> or <see langword="callvirt" /> instruction onto the Microsoft intermediate language (MSIL) stream to call a <see langword="varargs" /> method.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream. Must be <see cref="F:System.Reflection.Emit.OpCodes.Call" />, <see cref="F:System.Reflection.Emit.OpCodes.Callvirt" />, or <see cref="F:System.Reflection.Emit.OpCodes.Newobj" />.</param>
		/// <param name="methodInfo">The <see langword="varargs" /> method to be called.</param>
		/// <param name="optionalParameterTypes">The types of the optional arguments if the method is a <see langword="varargs" /> method; otherwise, <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="opcode" /> does not specify a method call.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="methodInfo" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The calling convention for the method is not <see langword="varargs" />, but optional parameter types are supplied. This exception is thrown in the .NET Framework versions 1.0 and 1.1, In subsequent versions, no exception is thrown.</exception>
		// Token: 0x060050C6 RID: 20678 RVA: 0x000FC9A0 File Offset: 0x000FABA0
		[MonoLimitation("vararg methods are not supported")]
		public virtual void EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
		{
			if (methodInfo == null)
			{
				throw new ArgumentNullException("methodInfo");
			}
			short value = opcode.Value;
			if (value != OpCodes.Call.Value && value != OpCodes.Callvirt.Value)
			{
				throw new NotSupportedException("Only Call and CallVirt are allowed");
			}
			if ((methodInfo.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				optionalParameterTypes = null;
			}
			if (optionalParameterTypes == null)
			{
				this.Emit(opcode, methodInfo);
				return;
			}
			if ((methodInfo.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				throw new InvalidOperationException("Method is not VarArgs method and optional types were passed");
			}
			int token = this.token_gen.GetToken(methodInfo, optionalParameterTypes);
			this.Emit(opcode, methodInfo, token);
		}

		/// <summary>Puts a <see cref="F:System.Reflection.Emit.OpCodes.Calli" /> instruction onto the Microsoft intermediate language (MSIL) stream, specifying an unmanaged calling convention for the indirect call.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream. Must be <see cref="F:System.Reflection.Emit.OpCodes.Calli" />.</param>
		/// <param name="unmanagedCallConv">The unmanaged calling convention to be used.</param>
		/// <param name="returnType">The <see cref="T:System.Type" /> of the result.</param>
		/// <param name="parameterTypes">The types of the required arguments to the instruction.</param>
		// Token: 0x060050C7 RID: 20679 RVA: 0x000FCA34 File Offset: 0x000FAC34
		public virtual void EmitCalli(OpCode opcode, CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes)
		{
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(this.module as ModuleBuilder, (CallingConventions)0, unmanagedCallConv, returnType, parameterTypes);
			this.Emit(opcode, methodSigHelper);
		}

		/// <summary>Puts a <see cref="F:System.Reflection.Emit.OpCodes.Calli" /> instruction onto the Microsoft intermediate language (MSIL) stream, specifying a managed calling convention for the indirect call.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream. Must be <see cref="F:System.Reflection.Emit.OpCodes.Calli" />.</param>
		/// <param name="callingConvention">The managed calling convention to be used.</param>
		/// <param name="returnType">The <see cref="T:System.Type" /> of the result.</param>
		/// <param name="parameterTypes">The types of the required arguments to the instruction.</param>
		/// <param name="optionalParameterTypes">The types of the optional arguments for <see langword="varargs" /> calls.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="optionalParameterTypes" /> is not <see langword="null" />, but <paramref name="callingConvention" /> does not include the <see cref="F:System.Reflection.CallingConventions.VarArgs" /> flag.</exception>
		// Token: 0x060050C8 RID: 20680 RVA: 0x000FCA60 File Offset: 0x000FAC60
		public virtual void EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			if (optionalParameterTypes != null)
			{
				throw new NotImplementedException();
			}
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(this.module as ModuleBuilder, callingConvention, (CallingConvention)0, returnType, parameterTypes);
			this.Emit(opcode, methodSigHelper);
		}

		/// <summary>Emits the Microsoft intermediate language (MSIL) necessary to call <see cref="Overload:System.Console.WriteLine" /> with the given field.</summary>
		/// <param name="fld">The field whose value is to be written to the console.</param>
		/// <exception cref="T:System.ArgumentException">There is no overload of the <see cref="Overload:System.Console.WriteLine" /> method that accepts the type of the specified field.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fld" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The type of the field is <see cref="T:System.Reflection.Emit.TypeBuilder" /> or <see cref="T:System.Reflection.Emit.EnumBuilder" />, which are not supported.</exception>
		// Token: 0x060050C9 RID: 20681 RVA: 0x000FCA98 File Offset: 0x000FAC98
		public virtual void EmitWriteLine(FieldInfo fld)
		{
			if (fld == null)
			{
				throw new ArgumentNullException("fld");
			}
			if (fld.IsStatic)
			{
				this.Emit(OpCodes.Ldsfld, fld);
			}
			else
			{
				this.Emit(OpCodes.Ldarg_0);
				this.Emit(OpCodes.Ldfld, fld);
			}
			this.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[]
			{
				fld.FieldType
			}));
		}

		/// <summary>Emits the Microsoft intermediate language (MSIL) necessary to call <see cref="Overload:System.Console.WriteLine" /> with the given local variable.</summary>
		/// <param name="localBuilder">The local variable whose value is to be written to the console.</param>
		/// <exception cref="T:System.ArgumentException">The type of <paramref name="localBuilder" /> is <see cref="T:System.Reflection.Emit.TypeBuilder" /> or <see cref="T:System.Reflection.Emit.EnumBuilder" />, which are not supported.  
		///  -or-  
		///  There is no overload of <see cref="Overload:System.Console.WriteLine" /> that accepts the type of <paramref name="localBuilder" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localBuilder" /> is <see langword="null" />.</exception>
		// Token: 0x060050CA RID: 20682 RVA: 0x000FCB14 File Offset: 0x000FAD14
		public virtual void EmitWriteLine(LocalBuilder localBuilder)
		{
			if (localBuilder == null)
			{
				throw new ArgumentNullException("localBuilder");
			}
			if (localBuilder.LocalType is TypeBuilder)
			{
				throw new ArgumentException("Output streams do not support TypeBuilders.");
			}
			this.Emit(OpCodes.Ldloc, localBuilder);
			this.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[]
			{
				localBuilder.LocalType
			}));
		}

		/// <summary>Emits the Microsoft intermediate language (MSIL) to call <see cref="Overload:System.Console.WriteLine" /> with a string.</summary>
		/// <param name="value">The string to be printed.</param>
		// Token: 0x060050CB RID: 20683 RVA: 0x000FCB81 File Offset: 0x000FAD81
		public virtual void EmitWriteLine(string value)
		{
			this.Emit(OpCodes.Ldstr, value);
			this.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[]
			{
				typeof(string)
			}));
		}

		/// <summary>Ends an exception block.</summary>
		/// <exception cref="T:System.InvalidOperationException">The end exception block occurs in an unexpected place in the code stream.</exception>
		/// <exception cref="T:System.NotSupportedException">The Microsoft intermediate language (MSIL) being generated is not currently in an exception block.</exception>
		// Token: 0x060050CC RID: 20684 RVA: 0x000FCBC4 File Offset: 0x000FADC4
		public virtual void EndExceptionBlock()
		{
			if (this.open_blocks == null)
			{
				this.open_blocks = new Stack(2);
			}
			if (this.open_blocks.Count <= 0)
			{
				throw new NotSupportedException("Not in an exception block");
			}
			if (this.ex_handlers[this.cur_block].LastClauseType() == -1)
			{
				throw new InvalidOperationException("Incorrect code generation for exception block.");
			}
			this.InternalEndClause();
			this.MarkLabel(this.ex_handlers[this.cur_block].end);
			this.ex_handlers[this.cur_block].End(this.code_len);
			this.ex_handlers[this.cur_block].Debug(this.cur_block);
			this.open_blocks.Pop();
			if (this.open_blocks.Count > 0)
			{
				this.cur_block = (int)this.open_blocks.Peek();
			}
		}

		/// <summary>Ends a lexical scope.</summary>
		/// <exception cref="T:System.NotSupportedException">This <see cref="T:System.Reflection.Emit.ILGenerator" /> belongs to a <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		// Token: 0x060050CD RID: 20685 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public virtual void EndScope()
		{
		}

		/// <summary>Marks the Microsoft intermediate language (MSIL) stream's current position with the given label.</summary>
		/// <param name="loc">The label for which to set an index.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="loc" /> represents an invalid index into the label array.  
		/// -or-  
		/// An index for <paramref name="loc" /> has already been defined.</exception>
		// Token: 0x060050CE RID: 20686 RVA: 0x000FCCAC File Offset: 0x000FAEAC
		public virtual void MarkLabel(Label loc)
		{
			if (loc.label < 0 || loc.label >= this.num_labels)
			{
				throw new ArgumentException("The label is not valid");
			}
			if (this.labels[loc.label].addr >= 0)
			{
				throw new ArgumentException("The label was already defined");
			}
			this.labels[loc.label].addr = this.code_len;
			if (this.labels[loc.label].maxStack > this.cur_stack)
			{
				this.cur_stack = this.labels[loc.label].maxStack;
			}
		}

		/// <summary>Marks a sequence point in the Microsoft intermediate language (MSIL) stream.</summary>
		/// <param name="document">The document for which the sequence point is being defined.</param>
		/// <param name="startLine">The line where the sequence point begins.</param>
		/// <param name="startColumn">The column in the line where the sequence point begins.</param>
		/// <param name="endLine">The line where the sequence point ends.</param>
		/// <param name="endColumn">The column in the line where the sequence point ends.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startLine" /> or <paramref name="endLine" /> is &lt;= 0.</exception>
		/// <exception cref="T:System.NotSupportedException">This <see cref="T:System.Reflection.Emit.ILGenerator" /> belongs to a <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		// Token: 0x060050CF RID: 20687 RVA: 0x000FCD58 File Offset: 0x000FAF58
		public virtual void MarkSequencePoint(ISymbolDocumentWriter document, int startLine, int startColumn, int endLine, int endColumn)
		{
			if (this.currentSequence == null || this.currentSequence.Document != document)
			{
				if (this.sequencePointLists == null)
				{
					this.sequencePointLists = new ArrayList();
				}
				this.currentSequence = new SequencePointList(document);
				this.sequencePointLists.Add(this.currentSequence);
			}
			this.currentSequence.AddSequencePoint(this.code_len, startLine, startColumn, endLine, endColumn);
		}

		// Token: 0x060050D0 RID: 20688 RVA: 0x000FCDC4 File Offset: 0x000FAFC4
		internal void GenerateDebugInfo(ISymbolWriter symbolWriter)
		{
			if (this.sequencePointLists != null)
			{
				SequencePointList sequencePointList = (SequencePointList)this.sequencePointLists[0];
				SequencePointList sequencePointList2 = (SequencePointList)this.sequencePointLists[this.sequencePointLists.Count - 1];
				symbolWriter.SetMethodSourceRange(sequencePointList.Document, sequencePointList.StartLine, sequencePointList.StartColumn, sequencePointList2.Document, sequencePointList2.EndLine, sequencePointList2.EndColumn);
				foreach (object obj in this.sequencePointLists)
				{
					SequencePointList sequencePointList3 = (SequencePointList)obj;
					symbolWriter.DefineSequencePoints(sequencePointList3.Document, sequencePointList3.GetOffsets(), sequencePointList3.GetLines(), sequencePointList3.GetColumns(), sequencePointList3.GetEndLines(), sequencePointList3.GetEndColumns());
				}
				if (this.locals != null)
				{
					foreach (LocalBuilder localBuilder in this.locals)
					{
						if (localBuilder.Name != null && localBuilder.Name.Length > 0)
						{
							SignatureHelper localVarSigHelper = SignatureHelper.GetLocalVarSigHelper(this.module as ModuleBuilder);
							localVarSigHelper.AddArgument(localBuilder.LocalType);
							byte[] signature = localVarSigHelper.GetSignature();
							symbolWriter.DefineLocalVariable(localBuilder.Name, FieldAttributes.Public, signature, SymAddressKind.ILOffset, (int)localBuilder.position, 0, 0, localBuilder.StartOffset, localBuilder.EndOffset);
						}
					}
				}
				this.sequencePointLists = null;
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x060050D1 RID: 20689 RVA: 0x000FCF40 File Offset: 0x000FB140
		internal bool HasDebugInfo
		{
			get
			{
				return this.sequencePointLists != null;
			}
		}

		/// <summary>Emits an instruction to throw an exception.</summary>
		/// <param name="excType">The class of the type of exception to throw.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="excType" /> is not the <see cref="T:System.Exception" /> class or a derived class of <see cref="T:System.Exception" />.  
		/// -or-  
		/// The type does not have a default constructor.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="excType" /> is <see langword="null" />.</exception>
		// Token: 0x060050D2 RID: 20690 RVA: 0x000FCF4C File Offset: 0x000FB14C
		public virtual void ThrowException(Type excType)
		{
			if (excType == null)
			{
				throw new ArgumentNullException("excType");
			}
			if (!(excType == typeof(Exception)) && !excType.IsSubclassOf(typeof(Exception)))
			{
				throw new ArgumentException("Type should be an exception type", "excType");
			}
			ConstructorInfo constructor = excType.GetConstructor(Type.EmptyTypes);
			if (constructor == null)
			{
				throw new ArgumentException("Type should have a default constructor", "excType");
			}
			this.Emit(OpCodes.Newobj, constructor);
			this.Emit(OpCodes.Throw);
		}

		/// <summary>Specifies the namespace to be used in evaluating locals and watches for the current active lexical scope.</summary>
		/// <param name="usingNamespace">The namespace to be used in evaluating locals and watches for the current active lexical scope</param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="usingNamespace" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usingNamespace" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">This <see cref="T:System.Reflection.Emit.ILGenerator" /> belongs to a <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		// Token: 0x060050D3 RID: 20691 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO("Not implemented")]
		public virtual void UsingNamespace(string usingNamespace)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x000FCFE0 File Offset: 0x000FB1E0
		internal void label_fixup(MethodBase mb)
		{
			for (int i = 0; i < this.num_fixups; i++)
			{
				if (this.labels[this.fixups[i].label_idx].addr < 0)
				{
					throw new ArgumentException(string.Format("Label #{0} is not marked in method `{1}'", this.fixups[i].label_idx + 1, mb.Name));
				}
				int num = this.labels[this.fixups[i].label_idx].addr - (this.fixups[i].pos + this.fixups[i].offset);
				if (this.fixups[i].offset == 1)
				{
					this.code[this.fixups[i].pos] = (byte)((sbyte)num);
				}
				else
				{
					int num2 = this.code_len;
					this.code_len = this.fixups[i].pos;
					this.emit_int(num);
					this.code_len = num2;
				}
			}
		}

		// Token: 0x060050D5 RID: 20693 RVA: 0x000FD0FC File Offset: 0x000FB2FC
		internal void FixupTokens(Dictionary<int, int> token_map, Dictionary<int, MemberInfo> member_map)
		{
			for (int i = 0; i < this.num_token_fixups; i++)
			{
				int code_pos = this.token_fixups[i].code_pos;
				int key = (int)this.code[code_pos] | (int)this.code[code_pos + 1] << 8 | (int)this.code[code_pos + 2] << 16 | (int)this.code[code_pos + 3] << 24;
				int val;
				if (token_map.TryGetValue(key, out val))
				{
					this.token_fixups[i].member = member_map[key];
					int num = this.code_len;
					this.code_len = code_pos;
					this.emit_int(val);
					this.code_len = num;
				}
			}
		}

		// Token: 0x060050D6 RID: 20694 RVA: 0x000FD1A5 File Offset: 0x000FB3A5
		internal void SetExceptionHandlers(ILExceptionInfo[] exHandlers)
		{
			this.ex_handlers = exHandlers;
		}

		// Token: 0x060050D7 RID: 20695 RVA: 0x000FD1AE File Offset: 0x000FB3AE
		internal void SetTokenFixups(ILTokenInfo[] tokenFixups)
		{
			this.token_fixups = tokenFixups;
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x000FD1B7 File Offset: 0x000FB3B7
		internal void SetCode(byte[] code, int max_stack)
		{
			this.code = (byte[])code.Clone();
			this.code_len = code.Length;
			this.max_stack = max_stack;
			this.cur_stack = 0;
		}

		// Token: 0x060050D9 RID: 20697 RVA: 0x000FD1E4 File Offset: 0x000FB3E4
		internal unsafe void SetCode(byte* code, int code_size, int max_stack)
		{
			this.code = new byte[code_size];
			for (int i = 0; i < code_size; i++)
			{
				this.code[i] = code[i];
			}
			this.code_len = code_size;
			this.max_stack = max_stack;
			this.cur_stack = 0;
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x000FD22C File Offset: 0x000FB42C
		internal void Init(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
		{
			this.SetCode(il, maxStack);
			if (exceptionHandlers != null)
			{
				Dictionary<Tuple<int, int>, List<ExceptionHandler>> dictionary = new Dictionary<Tuple<int, int>, List<ExceptionHandler>>();
				foreach (ExceptionHandler item in exceptionHandlers)
				{
					Tuple<int, int> key = new Tuple<int, int>(item.TryOffset, item.TryLength);
					List<ExceptionHandler> list;
					if (!dictionary.TryGetValue(key, out list))
					{
						list = new List<ExceptionHandler>();
						dictionary.Add(key, list);
					}
					list.Add(item);
				}
				List<ILExceptionInfo> list2 = new List<ILExceptionInfo>();
				foreach (KeyValuePair<Tuple<int, int>, List<ExceptionHandler>> keyValuePair in dictionary)
				{
					ILExceptionInfo ilexceptionInfo = new ILExceptionInfo
					{
						start = keyValuePair.Key.Item1,
						len = keyValuePair.Key.Item2,
						handlers = new ILExceptionBlock[keyValuePair.Value.Count]
					};
					list2.Add(ilexceptionInfo);
					int num = 0;
					foreach (ExceptionHandler exceptionHandler in keyValuePair.Value)
					{
						ilexceptionInfo.handlers[num++] = new ILExceptionBlock
						{
							start = exceptionHandler.HandlerOffset,
							len = exceptionHandler.HandlerLength,
							filter_offset = exceptionHandler.FilterOffset,
							type = (int)exceptionHandler.Kind,
							extype = this.module.ResolveType(exceptionHandler.ExceptionTypeToken)
						};
					}
				}
				this.SetExceptionHandlers(list2.ToArray());
			}
			if (tokenFixups != null)
			{
				List<ILTokenInfo> list3 = new List<ILTokenInfo>();
				foreach (int num2 in tokenFixups)
				{
					int metadataToken = (int)BitConverter.ToUInt32(il, num2);
					ILTokenInfo item2 = new ILTokenInfo
					{
						code_pos = num2,
						member = ((ModuleBuilder)this.module).ResolveOrGetRegisteredToken(metadataToken, null, null)
					};
					list3.Add(item2);
				}
				this.SetTokenFixups(list3.ToArray());
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x060050DB RID: 20699 RVA: 0x000FD4D4 File Offset: 0x000FB6D4
		internal TokenGenerator TokenGenerator
		{
			get
			{
				return this.token_gen;
			}
		}

		// Token: 0x060050DC RID: 20700 RVA: 0x000FD4DC File Offset: 0x000FB6DC
		[Obsolete("Use ILOffset", true)]
		internal static int Mono_GetCurrentOffset(ILGenerator ig)
		{
			return ig.code_len;
		}

		/// <summary>Gets the current offset, in bytes, in the Microsoft intermediate language (MSIL) stream that is being emitted by the <see cref="T:System.Reflection.Emit.ILGenerator" />.</summary>
		/// <returns>The offset in the MSIL stream at which the next instruction will be emitted.</returns>
		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x060050DD RID: 20701 RVA: 0x000FD4DC File Offset: 0x000FB6DC
		public virtual int ILOffset
		{
			get
			{
				return this.code_len;
			}
		}

		// Token: 0x060050DE RID: 20702 RVA: 0x000173AD File Offset: 0x000155AD
		internal ILGenerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400318F RID: 12687
		private byte[] code;

		// Token: 0x04003190 RID: 12688
		private int code_len;

		// Token: 0x04003191 RID: 12689
		private int max_stack;

		// Token: 0x04003192 RID: 12690
		private int cur_stack;

		// Token: 0x04003193 RID: 12691
		private LocalBuilder[] locals;

		// Token: 0x04003194 RID: 12692
		private ILExceptionInfo[] ex_handlers;

		// Token: 0x04003195 RID: 12693
		private int num_token_fixups;

		// Token: 0x04003196 RID: 12694
		private ILTokenInfo[] token_fixups;

		// Token: 0x04003197 RID: 12695
		private ILGenerator.LabelData[] labels;

		// Token: 0x04003198 RID: 12696
		private int num_labels;

		// Token: 0x04003199 RID: 12697
		private ILGenerator.LabelFixup[] fixups;

		// Token: 0x0400319A RID: 12698
		private int num_fixups;

		// Token: 0x0400319B RID: 12699
		internal Module module;

		// Token: 0x0400319C RID: 12700
		private int cur_block;

		// Token: 0x0400319D RID: 12701
		private Stack open_blocks;

		// Token: 0x0400319E RID: 12702
		private TokenGenerator token_gen;

		// Token: 0x0400319F RID: 12703
		private const int defaultFixupSize = 4;

		// Token: 0x040031A0 RID: 12704
		private const int defaultLabelsSize = 4;

		// Token: 0x040031A1 RID: 12705
		private const int defaultExceptionStackSize = 2;

		// Token: 0x040031A2 RID: 12706
		private ArrayList sequencePointLists;

		// Token: 0x040031A3 RID: 12707
		private SequencePointList currentSequence;

		// Token: 0x0200092E RID: 2350
		private struct LabelFixup
		{
			// Token: 0x040031A4 RID: 12708
			public int offset;

			// Token: 0x040031A5 RID: 12709
			public int pos;

			// Token: 0x040031A6 RID: 12710
			public int label_idx;
		}

		// Token: 0x0200092F RID: 2351
		private struct LabelData
		{
			// Token: 0x060050DF RID: 20703 RVA: 0x000FD4E4 File Offset: 0x000FB6E4
			public LabelData(int addr, int maxStack)
			{
				this.addr = addr;
				this.maxStack = maxStack;
			}

			// Token: 0x040031A7 RID: 12711
			public int addr;

			// Token: 0x040031A8 RID: 12712
			public int maxStack;
		}
	}
}
