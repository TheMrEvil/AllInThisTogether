using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002A8 RID: 680
	internal sealed class CompilerScope
	{
		// Token: 0x0600142E RID: 5166 RVA: 0x0003E16C File Offset: 0x0003C36C
		internal CompilerScope(object node, bool isMethod)
		{
			this.Node = node;
			this.IsMethod = isMethod;
			IReadOnlyList<ParameterExpression> variables = CompilerScope.GetVariables(node);
			this.Definitions = new Dictionary<ParameterExpression, VariableStorageKind>(variables.Count);
			foreach (ParameterExpression key in variables)
			{
				this.Definitions.Add(key, VariableStorageKind.Local);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0003E1FC File Offset: 0x0003C3FC
		internal HoistedLocals NearestHoistedLocals
		{
			get
			{
				return this._hoistedLocals ?? this._closureHoistedLocals;
			}
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0003E210 File Offset: 0x0003C410
		internal CompilerScope Enter(LambdaCompiler lc, CompilerScope parent)
		{
			this.SetParent(lc, parent);
			this.AllocateLocals(lc);
			if (this.IsMethod && this._closureHoistedLocals != null)
			{
				this.EmitClosureAccess(lc, this._closureHoistedLocals);
			}
			this.EmitNewHoistedLocals(lc);
			if (this.IsMethod)
			{
				this.EmitCachedVariables();
			}
			return this;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0003E260 File Offset: 0x0003C460
		internal CompilerScope Exit()
		{
			if (!this.IsMethod)
			{
				foreach (CompilerScope.Storage storage in this._locals.Values)
				{
					storage.FreeLocal();
				}
			}
			CompilerScope parent = this._parent;
			this._parent = null;
			this._hoistedLocals = null;
			this._closureHoistedLocals = null;
			this._locals.Clear();
			return parent;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0003E2E4 File Offset: 0x0003C4E4
		internal void EmitVariableAccess(LambdaCompiler lc, ReadOnlyCollection<ParameterExpression> vars)
		{
			if (this.NearestHoistedLocals != null && vars.Count > 0)
			{
				ArrayBuilder<long> arrayBuilder = new ArrayBuilder<long>(vars.Count);
				foreach (ParameterExpression key in vars)
				{
					ulong num = 0UL;
					HoistedLocals hoistedLocals = this.NearestHoistedLocals;
					while (!hoistedLocals.Indexes.ContainsKey(key))
					{
						num += 1UL;
						hoistedLocals = hoistedLocals.Parent;
					}
					ulong item = num << 32 | (ulong)hoistedLocals.Indexes[key];
					arrayBuilder.UncheckedAdd((long)item);
				}
				this.EmitGet(this.NearestHoistedLocals.SelfVariable);
				lc.EmitConstantArray<long>(arrayBuilder.ToArray());
				lc.IL.Emit(OpCodes.Call, CachedReflectionInfo.RuntimeOps_CreateRuntimeVariables_ObjectArray_Int64Array);
				return;
			}
			lc.IL.Emit(OpCodes.Call, CachedReflectionInfo.RuntimeOps_CreateRuntimeVariables);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0003E3DC File Offset: 0x0003C5DC
		internal void AddLocal(LambdaCompiler gen, ParameterExpression variable)
		{
			this._locals.Add(variable, new CompilerScope.LocalStorage(gen, variable));
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0003E3F1 File Offset: 0x0003C5F1
		internal void EmitGet(ParameterExpression variable)
		{
			this.ResolveVariable(variable).EmitLoad();
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0003E3FF File Offset: 0x0003C5FF
		internal void EmitSet(ParameterExpression variable)
		{
			this.ResolveVariable(variable).EmitStore();
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0003E40D File Offset: 0x0003C60D
		internal void EmitAddressOf(ParameterExpression variable)
		{
			this.ResolveVariable(variable).EmitAddress();
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0003E41B File Offset: 0x0003C61B
		private CompilerScope.Storage ResolveVariable(ParameterExpression variable)
		{
			return this.ResolveVariable(variable, this.NearestHoistedLocals);
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0003E42C File Offset: 0x0003C62C
		private CompilerScope.Storage ResolveVariable(ParameterExpression variable, HoistedLocals hoistedLocals)
		{
			for (CompilerScope compilerScope = this; compilerScope != null; compilerScope = compilerScope._parent)
			{
				CompilerScope.Storage result;
				if (compilerScope._locals.TryGetValue(variable, out result))
				{
					return result;
				}
				if (compilerScope.IsMethod)
				{
					break;
				}
			}
			for (HoistedLocals hoistedLocals2 = hoistedLocals; hoistedLocals2 != null; hoistedLocals2 = hoistedLocals2.Parent)
			{
				int index;
				if (hoistedLocals2.Indexes.TryGetValue(variable, out index))
				{
					return new CompilerScope.ElementBoxStorage(this.ResolveVariable(hoistedLocals2.SelfVariable, hoistedLocals), index, variable);
				}
			}
			throw Error.UndefinedVariable(variable.Name, variable.Type, this.CurrentLambdaName);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0003E4AC File Offset: 0x0003C6AC
		private void SetParent(LambdaCompiler lc, CompilerScope parent)
		{
			this._parent = parent;
			if (this.NeedsClosure && this._parent != null)
			{
				this._closureHoistedLocals = this._parent.NearestHoistedLocals;
			}
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = (from p in this.GetVariables()
			where this.Definitions[p] == VariableStorageKind.Hoisted
			select p).ToReadOnly<ParameterExpression>();
			if (readOnlyCollection.Count > 0)
			{
				this._hoistedLocals = new HoistedLocals(this._closureHoistedLocals, readOnlyCollection);
				this.AddLocal(lc, this._hoistedLocals.SelfVariable);
			}
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0003E52C File Offset: 0x0003C72C
		private void EmitNewHoistedLocals(LambdaCompiler lc)
		{
			if (this._hoistedLocals == null)
			{
				return;
			}
			lc.IL.EmitPrimitive(this._hoistedLocals.Variables.Count);
			lc.IL.Emit(OpCodes.Newarr, typeof(object));
			int num = 0;
			foreach (ParameterExpression parameterExpression in this._hoistedLocals.Variables)
			{
				lc.IL.Emit(OpCodes.Dup);
				lc.IL.EmitPrimitive(num++);
				Type type = typeof(StrongBox<>).MakeGenericType(new Type[]
				{
					parameterExpression.Type
				});
				int index;
				if (this.IsMethod && (index = lc.Parameters.IndexOf(parameterExpression)) >= 0)
				{
					lc.EmitLambdaArgument(index);
					lc.IL.Emit(OpCodes.Newobj, type.GetConstructor(new Type[]
					{
						parameterExpression.Type
					}));
				}
				else if (parameterExpression == this._hoistedLocals.ParentVariable)
				{
					this.ResolveVariable(parameterExpression, this._closureHoistedLocals).EmitLoad();
					lc.IL.Emit(OpCodes.Newobj, type.GetConstructor(new Type[]
					{
						parameterExpression.Type
					}));
				}
				else
				{
					lc.IL.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));
				}
				if (this.ShouldCache(parameterExpression))
				{
					lc.IL.Emit(OpCodes.Dup);
					this.CacheBoxToLocal(lc, parameterExpression);
				}
				lc.IL.Emit(OpCodes.Stelem_Ref);
			}
			this.EmitSet(this._hoistedLocals.SelfVariable);
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0003E6F8 File Offset: 0x0003C8F8
		private void EmitCachedVariables()
		{
			if (this.ReferenceCount == null)
			{
				return;
			}
			foreach (KeyValuePair<ParameterExpression, int> keyValuePair in this.ReferenceCount)
			{
				if (this.ShouldCache(keyValuePair.Key, keyValuePair.Value))
				{
					CompilerScope.ElementBoxStorage elementBoxStorage = this.ResolveVariable(keyValuePair.Key) as CompilerScope.ElementBoxStorage;
					if (elementBoxStorage != null)
					{
						elementBoxStorage.EmitLoadBox();
						this.CacheBoxToLocal(elementBoxStorage.Compiler, keyValuePair.Key);
					}
				}
			}
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0003E794 File Offset: 0x0003C994
		private bool ShouldCache(ParameterExpression v, int refCount)
		{
			return refCount > 2 && !this._locals.ContainsKey(v);
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0003E7AC File Offset: 0x0003C9AC
		private bool ShouldCache(ParameterExpression v)
		{
			int refCount;
			return this.ReferenceCount != null && this.ReferenceCount.TryGetValue(v, out refCount) && this.ShouldCache(v, refCount);
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0003E7E0 File Offset: 0x0003C9E0
		private void CacheBoxToLocal(LambdaCompiler lc, ParameterExpression v)
		{
			CompilerScope.LocalBoxStorage localBoxStorage = new CompilerScope.LocalBoxStorage(lc, v);
			localBoxStorage.EmitStoreBox();
			this._locals.Add(v, localBoxStorage);
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0003E808 File Offset: 0x0003CA08
		private void EmitClosureAccess(LambdaCompiler lc, HoistedLocals locals)
		{
			if (locals == null)
			{
				return;
			}
			this.EmitClosureToVariable(lc, locals);
			while ((locals = locals.Parent) != null)
			{
				ParameterExpression selfVariable = locals.SelfVariable;
				CompilerScope.LocalStorage localStorage = new CompilerScope.LocalStorage(lc, selfVariable);
				localStorage.EmitStore(this.ResolveVariable(selfVariable));
				this._locals.Add(selfVariable, localStorage);
			}
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0003E857 File Offset: 0x0003CA57
		private void EmitClosureToVariable(LambdaCompiler lc, HoistedLocals locals)
		{
			lc.EmitClosureArgument();
			lc.IL.Emit(OpCodes.Ldfld, CachedReflectionInfo.Closure_Locals);
			this.AddLocal(lc, locals.SelfVariable);
			this.EmitSet(locals.SelfVariable);
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0003E890 File Offset: 0x0003CA90
		private void AllocateLocals(LambdaCompiler lc)
		{
			foreach (ParameterExpression parameterExpression in this.GetVariables())
			{
				if (this.Definitions[parameterExpression] == VariableStorageKind.Local)
				{
					CompilerScope.Storage value;
					if (this.IsMethod && lc.Parameters.Contains(parameterExpression))
					{
						value = new CompilerScope.ArgumentStorage(lc, parameterExpression);
					}
					else
					{
						value = new CompilerScope.LocalStorage(lc, parameterExpression);
					}
					this._locals.Add(parameterExpression, value);
				}
			}
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0003E91C File Offset: 0x0003CB1C
		private IEnumerable<ParameterExpression> GetVariables()
		{
			if (this.MergedScopes != null)
			{
				return this.GetVariablesIncludingMerged();
			}
			return CompilerScope.GetVariables(this.Node);
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0003E945 File Offset: 0x0003CB45
		private IEnumerable<ParameterExpression> GetVariablesIncludingMerged()
		{
			foreach (ParameterExpression parameterExpression in CompilerScope.GetVariables(this.Node))
			{
				yield return parameterExpression;
			}
			IEnumerator<ParameterExpression> enumerator = null;
			foreach (BlockExpression blockExpression in this.MergedScopes)
			{
				foreach (ParameterExpression parameterExpression2 in blockExpression.Variables)
				{
					yield return parameterExpression2;
				}
				enumerator = null;
			}
			HashSet<BlockExpression>.Enumerator enumerator2 = default(HashSet<BlockExpression>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0003E958 File Offset: 0x0003CB58
		private static IReadOnlyList<ParameterExpression> GetVariables(object scope)
		{
			LambdaExpression lambdaExpression = scope as LambdaExpression;
			if (lambdaExpression != null)
			{
				return new ParameterList(lambdaExpression);
			}
			BlockExpression blockExpression = scope as BlockExpression;
			if (blockExpression != null)
			{
				return blockExpression.Variables;
			}
			return new ParameterExpression[]
			{
				((CatchBlock)scope).Variable
			};
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x0003E99C File Offset: 0x0003CB9C
		private string CurrentLambdaName
		{
			get
			{
				for (CompilerScope compilerScope = this; compilerScope != null; compilerScope = compilerScope._parent)
				{
					LambdaExpression lambdaExpression = compilerScope.Node as LambdaExpression;
					if (lambdaExpression != null)
					{
						return lambdaExpression.Name;
					}
				}
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0003E9D2 File Offset: 0x0003CBD2
		[CompilerGenerated]
		private bool <SetParent>b__27_0(ParameterExpression p)
		{
			return this.Definitions[p] == VariableStorageKind.Hoisted;
		}

		// Token: 0x04000A95 RID: 2709
		private CompilerScope _parent;

		// Token: 0x04000A96 RID: 2710
		internal readonly object Node;

		// Token: 0x04000A97 RID: 2711
		internal readonly bool IsMethod;

		// Token: 0x04000A98 RID: 2712
		internal bool NeedsClosure;

		// Token: 0x04000A99 RID: 2713
		internal readonly Dictionary<ParameterExpression, VariableStorageKind> Definitions = new Dictionary<ParameterExpression, VariableStorageKind>();

		// Token: 0x04000A9A RID: 2714
		internal Dictionary<ParameterExpression, int> ReferenceCount;

		// Token: 0x04000A9B RID: 2715
		internal HashSet<BlockExpression> MergedScopes;

		// Token: 0x04000A9C RID: 2716
		private HoistedLocals _hoistedLocals;

		// Token: 0x04000A9D RID: 2717
		private HoistedLocals _closureHoistedLocals;

		// Token: 0x04000A9E RID: 2718
		private readonly Dictionary<ParameterExpression, CompilerScope.Storage> _locals = new Dictionary<ParameterExpression, CompilerScope.Storage>();

		// Token: 0x020002A9 RID: 681
		private abstract class Storage
		{
			// Token: 0x06001447 RID: 5191 RVA: 0x0003E9E3 File Offset: 0x0003CBE3
			internal Storage(LambdaCompiler compiler, ParameterExpression variable)
			{
				this.Compiler = compiler;
				this.Variable = variable;
			}

			// Token: 0x06001448 RID: 5192
			internal abstract void EmitLoad();

			// Token: 0x06001449 RID: 5193
			internal abstract void EmitAddress();

			// Token: 0x0600144A RID: 5194
			internal abstract void EmitStore();

			// Token: 0x0600144B RID: 5195 RVA: 0x0003E9F9 File Offset: 0x0003CBF9
			internal virtual void EmitStore(CompilerScope.Storage value)
			{
				value.EmitLoad();
				this.EmitStore();
			}

			// Token: 0x0600144C RID: 5196 RVA: 0x00003A59 File Offset: 0x00001C59
			internal virtual void FreeLocal()
			{
			}

			// Token: 0x04000A9F RID: 2719
			internal readonly LambdaCompiler Compiler;

			// Token: 0x04000AA0 RID: 2720
			internal readonly ParameterExpression Variable;
		}

		// Token: 0x020002AA RID: 682
		private sealed class LocalStorage : CompilerScope.Storage
		{
			// Token: 0x0600144D RID: 5197 RVA: 0x0003EA07 File Offset: 0x0003CC07
			internal LocalStorage(LambdaCompiler compiler, ParameterExpression variable) : base(compiler, variable)
			{
				this._local = compiler.GetLocal(variable.IsByRef ? variable.Type.MakeByRefType() : variable.Type);
			}

			// Token: 0x0600144E RID: 5198 RVA: 0x0003EA38 File Offset: 0x0003CC38
			internal override void EmitLoad()
			{
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._local);
			}

			// Token: 0x0600144F RID: 5199 RVA: 0x0003EA55 File Offset: 0x0003CC55
			internal override void EmitStore()
			{
				this.Compiler.IL.Emit(OpCodes.Stloc, this._local);
			}

			// Token: 0x06001450 RID: 5200 RVA: 0x0003EA72 File Offset: 0x0003CC72
			internal override void EmitAddress()
			{
				this.Compiler.IL.Emit(OpCodes.Ldloca, this._local);
			}

			// Token: 0x06001451 RID: 5201 RVA: 0x0003EA8F File Offset: 0x0003CC8F
			internal override void FreeLocal()
			{
				this.Compiler.FreeLocal(this._local);
			}

			// Token: 0x04000AA1 RID: 2721
			private readonly LocalBuilder _local;
		}

		// Token: 0x020002AB RID: 683
		private sealed class ArgumentStorage : CompilerScope.Storage
		{
			// Token: 0x06001452 RID: 5202 RVA: 0x0003EAA2 File Offset: 0x0003CCA2
			internal ArgumentStorage(LambdaCompiler compiler, ParameterExpression p) : base(compiler, p)
			{
				this._argument = compiler.GetLambdaArgument(compiler.Parameters.IndexOf(p));
			}

			// Token: 0x06001453 RID: 5203 RVA: 0x0003EAC4 File Offset: 0x0003CCC4
			internal override void EmitLoad()
			{
				this.Compiler.IL.EmitLoadArg(this._argument);
			}

			// Token: 0x06001454 RID: 5204 RVA: 0x0003EADC File Offset: 0x0003CCDC
			internal override void EmitStore()
			{
				this.Compiler.IL.EmitStoreArg(this._argument);
			}

			// Token: 0x06001455 RID: 5205 RVA: 0x0003EAF4 File Offset: 0x0003CCF4
			internal override void EmitAddress()
			{
				this.Compiler.IL.EmitLoadArgAddress(this._argument);
			}

			// Token: 0x04000AA2 RID: 2722
			private readonly int _argument;
		}

		// Token: 0x020002AC RID: 684
		private sealed class ElementBoxStorage : CompilerScope.Storage
		{
			// Token: 0x06001456 RID: 5206 RVA: 0x0003EB0C File Offset: 0x0003CD0C
			internal ElementBoxStorage(CompilerScope.Storage array, int index, ParameterExpression variable) : base(array.Compiler, variable)
			{
				this._array = array;
				this._index = index;
				this._boxType = typeof(StrongBox<>).MakeGenericType(new Type[]
				{
					variable.Type
				});
				this._boxValueField = this._boxType.GetField("Value");
			}

			// Token: 0x06001457 RID: 5207 RVA: 0x0003EB6E File Offset: 0x0003CD6E
			internal override void EmitLoad()
			{
				this.EmitLoadBox();
				this.Compiler.IL.Emit(OpCodes.Ldfld, this._boxValueField);
			}

			// Token: 0x06001458 RID: 5208 RVA: 0x0003EB94 File Offset: 0x0003CD94
			internal override void EmitStore()
			{
				LocalBuilder local = this.Compiler.GetLocal(this.Variable.Type);
				this.Compiler.IL.Emit(OpCodes.Stloc, local);
				this.EmitLoadBox();
				this.Compiler.IL.Emit(OpCodes.Ldloc, local);
				this.Compiler.FreeLocal(local);
				this.Compiler.IL.Emit(OpCodes.Stfld, this._boxValueField);
			}

			// Token: 0x06001459 RID: 5209 RVA: 0x0003EC11 File Offset: 0x0003CE11
			internal override void EmitStore(CompilerScope.Storage value)
			{
				this.EmitLoadBox();
				value.EmitLoad();
				this.Compiler.IL.Emit(OpCodes.Stfld, this._boxValueField);
			}

			// Token: 0x0600145A RID: 5210 RVA: 0x0003EC3A File Offset: 0x0003CE3A
			internal override void EmitAddress()
			{
				this.EmitLoadBox();
				this.Compiler.IL.Emit(OpCodes.Ldflda, this._boxValueField);
			}

			// Token: 0x0600145B RID: 5211 RVA: 0x0003EC60 File Offset: 0x0003CE60
			internal void EmitLoadBox()
			{
				this._array.EmitLoad();
				this.Compiler.IL.EmitPrimitive(this._index);
				this.Compiler.IL.Emit(OpCodes.Ldelem_Ref);
				this.Compiler.IL.Emit(OpCodes.Castclass, this._boxType);
			}

			// Token: 0x04000AA3 RID: 2723
			private readonly int _index;

			// Token: 0x04000AA4 RID: 2724
			private readonly CompilerScope.Storage _array;

			// Token: 0x04000AA5 RID: 2725
			private readonly Type _boxType;

			// Token: 0x04000AA6 RID: 2726
			private readonly FieldInfo _boxValueField;
		}

		// Token: 0x020002AD RID: 685
		private sealed class LocalBoxStorage : CompilerScope.Storage
		{
			// Token: 0x0600145C RID: 5212 RVA: 0x0003ECC0 File Offset: 0x0003CEC0
			internal LocalBoxStorage(LambdaCompiler compiler, ParameterExpression variable) : base(compiler, variable)
			{
				Type type = typeof(StrongBox<>).MakeGenericType(new Type[]
				{
					variable.Type
				});
				this._boxValueField = type.GetField("Value");
				this._boxLocal = compiler.GetLocal(type);
			}

			// Token: 0x0600145D RID: 5213 RVA: 0x0003ED12 File Offset: 0x0003CF12
			internal override void EmitLoad()
			{
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._boxLocal);
				this.Compiler.IL.Emit(OpCodes.Ldfld, this._boxValueField);
			}

			// Token: 0x0600145E RID: 5214 RVA: 0x0003ED4A File Offset: 0x0003CF4A
			internal override void EmitAddress()
			{
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._boxLocal);
				this.Compiler.IL.Emit(OpCodes.Ldflda, this._boxValueField);
			}

			// Token: 0x0600145F RID: 5215 RVA: 0x0003ED84 File Offset: 0x0003CF84
			internal override void EmitStore()
			{
				LocalBuilder local = this.Compiler.GetLocal(this.Variable.Type);
				this.Compiler.IL.Emit(OpCodes.Stloc, local);
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._boxLocal);
				this.Compiler.IL.Emit(OpCodes.Ldloc, local);
				this.Compiler.FreeLocal(local);
				this.Compiler.IL.Emit(OpCodes.Stfld, this._boxValueField);
			}

			// Token: 0x06001460 RID: 5216 RVA: 0x0003EE16 File Offset: 0x0003D016
			internal override void EmitStore(CompilerScope.Storage value)
			{
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._boxLocal);
				value.EmitLoad();
				this.Compiler.IL.Emit(OpCodes.Stfld, this._boxValueField);
			}

			// Token: 0x06001461 RID: 5217 RVA: 0x0003EE54 File Offset: 0x0003D054
			internal void EmitStoreBox()
			{
				this.Compiler.IL.Emit(OpCodes.Stloc, this._boxLocal);
			}

			// Token: 0x06001462 RID: 5218 RVA: 0x0003EE71 File Offset: 0x0003D071
			internal override void FreeLocal()
			{
				this.Compiler.FreeLocal(this._boxLocal);
			}

			// Token: 0x04000AA7 RID: 2727
			private readonly LocalBuilder _boxLocal;

			// Token: 0x04000AA8 RID: 2728
			private readonly FieldInfo _boxValueField;
		}

		// Token: 0x020002AE RID: 686
		[CompilerGenerated]
		private sealed class <GetVariablesIncludingMerged>d__37 : IEnumerable<ParameterExpression>, IEnumerable, IEnumerator<ParameterExpression>, IDisposable, IEnumerator
		{
			// Token: 0x06001463 RID: 5219 RVA: 0x0003EE84 File Offset: 0x0003D084
			[DebuggerHidden]
			public <GetVariablesIncludingMerged>d__37(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06001464 RID: 5220 RVA: 0x0003EEA0 File Offset: 0x0003D0A0
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				switch (num)
				{
				case -5:
				case -4:
				case 2:
					break;
				case -3:
				case 1:
					try
					{
						return;
					}
					finally
					{
						this.<>m__Finally1();
					}
					break;
				case -2:
				case -1:
				case 0:
					return;
				default:
					return;
				}
				try
				{
					if (num == -5 || num == 2)
					{
						try
						{
						}
						finally
						{
							this.<>m__Finally3();
						}
					}
				}
				finally
				{
					this.<>m__Finally2();
				}
			}

			// Token: 0x06001465 RID: 5221 RVA: 0x0003EF30 File Offset: 0x0003D130
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					CompilerScope compilerScope = this;
					switch (num)
					{
					case 0:
						this.<>1__state = -1;
						enumerator = CompilerScope.GetVariables(compilerScope.Node).GetEnumerator();
						this.<>1__state = -3;
						break;
					case 1:
						this.<>1__state = -3;
						break;
					case 2:
						this.<>1__state = -5;
						goto IL_FD;
					default:
						return false;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						enumerator2 = compilerScope.MergedScopes.GetEnumerator();
						this.<>1__state = -4;
						goto IL_117;
					}
					ParameterExpression parameterExpression = enumerator.Current;
					this.<>2__current = parameterExpression;
					this.<>1__state = 1;
					return true;
					IL_FD:
					if (enumerator.MoveNext())
					{
						ParameterExpression parameterExpression2 = enumerator.Current;
						this.<>2__current = parameterExpression2;
						this.<>1__state = 2;
						return true;
					}
					this.<>m__Finally3();
					enumerator = null;
					IL_117:
					if (enumerator2.MoveNext())
					{
						BlockExpression blockExpression = enumerator2.Current;
						enumerator = blockExpression.Variables.GetEnumerator();
						this.<>1__state = -5;
						goto IL_FD;
					}
					this.<>m__Finally2();
					enumerator2 = default(HashSet<BlockExpression>.Enumerator);
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06001466 RID: 5222 RVA: 0x0003F09C File Offset: 0x0003D29C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x06001467 RID: 5223 RVA: 0x0003F0B8 File Offset: 0x0003D2B8
			private void <>m__Finally2()
			{
				this.<>1__state = -1;
				((IDisposable)enumerator2).Dispose();
			}

			// Token: 0x06001468 RID: 5224 RVA: 0x0003F0D2 File Offset: 0x0003D2D2
			private void <>m__Finally3()
			{
				this.<>1__state = -4;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x170003B2 RID: 946
			// (get) Token: 0x06001469 RID: 5225 RVA: 0x0003F0EF File Offset: 0x0003D2EF
			ParameterExpression IEnumerator<ParameterExpression>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600146A RID: 5226 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003B3 RID: 947
			// (get) Token: 0x0600146B RID: 5227 RVA: 0x0003F0EF File Offset: 0x0003D2EF
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600146C RID: 5228 RVA: 0x0003F0F8 File Offset: 0x0003D2F8
			[DebuggerHidden]
			IEnumerator<ParameterExpression> IEnumerable<ParameterExpression>.GetEnumerator()
			{
				CompilerScope.<GetVariablesIncludingMerged>d__37 <GetVariablesIncludingMerged>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetVariablesIncludingMerged>d__ = this;
				}
				else
				{
					<GetVariablesIncludingMerged>d__ = new CompilerScope.<GetVariablesIncludingMerged>d__37(0);
					<GetVariablesIncludingMerged>d__.<>4__this = this;
				}
				return <GetVariablesIncludingMerged>d__;
			}

			// Token: 0x0600146D RID: 5229 RVA: 0x0003F13B File Offset: 0x0003D33B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression>.GetEnumerator();
			}

			// Token: 0x04000AA9 RID: 2729
			private int <>1__state;

			// Token: 0x04000AAA RID: 2730
			private ParameterExpression <>2__current;

			// Token: 0x04000AAB RID: 2731
			private int <>l__initialThreadId;

			// Token: 0x04000AAC RID: 2732
			public CompilerScope <>4__this;

			// Token: 0x04000AAD RID: 2733
			private IEnumerator<ParameterExpression> <>7__wrap1;

			// Token: 0x04000AAE RID: 2734
			private HashSet<BlockExpression>.Enumerator <>7__wrap2;
		}
	}
}
