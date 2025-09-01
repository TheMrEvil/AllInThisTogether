using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002A6 RID: 678
	internal sealed class BoundConstants
	{
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x0003DE17 File Offset: 0x0003C017
		internal int Count
		{
			get
			{
				return this._values.Count;
			}
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0003DE24 File Offset: 0x0003C024
		internal object[] ToArray()
		{
			return this._values.ToArray();
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0003DE31 File Offset: 0x0003C031
		internal void AddReference(object value, Type type)
		{
			if (this._indexes.TryAdd(value, this._values.Count))
			{
				this._values.Add(value);
			}
			Helpers.IncrementCount<BoundConstants.TypedConstant>(new BoundConstants.TypedConstant(value, type), this._references);
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0003DE6C File Offset: 0x0003C06C
		internal void EmitConstant(LambdaCompiler lc, object value, Type type)
		{
			if (!lc.CanEmitBoundConstants)
			{
				throw Error.CannotCompileConstant(value);
			}
			LocalBuilder local;
			if (this._cache.TryGetValue(new BoundConstants.TypedConstant(value, type), out local))
			{
				lc.IL.Emit(OpCodes.Ldloc, local);
				return;
			}
			BoundConstants.EmitConstantsArray(lc);
			this.EmitConstantFromArray(lc, value, type);
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0003DEC0 File Offset: 0x0003C0C0
		internal void EmitCacheConstants(LambdaCompiler lc)
		{
			int num = 0;
			foreach (KeyValuePair<BoundConstants.TypedConstant, int> keyValuePair in this._references)
			{
				if (!lc.CanEmitBoundConstants)
				{
					throw Error.CannotCompileConstant(keyValuePair.Key.Value);
				}
				if (BoundConstants.ShouldCache(keyValuePair.Value))
				{
					num++;
				}
			}
			if (num == 0)
			{
				return;
			}
			BoundConstants.EmitConstantsArray(lc);
			this._cache.Clear();
			foreach (KeyValuePair<BoundConstants.TypedConstant, int> keyValuePair2 in this._references)
			{
				if (BoundConstants.ShouldCache(keyValuePair2.Value))
				{
					if (--num > 0)
					{
						lc.IL.Emit(OpCodes.Dup);
					}
					LocalBuilder localBuilder = lc.IL.DeclareLocal(keyValuePair2.Key.Type);
					this.EmitConstantFromArray(lc, keyValuePair2.Key.Value, localBuilder.LocalType);
					lc.IL.Emit(OpCodes.Stloc, localBuilder);
					this._cache.Add(keyValuePair2.Key, localBuilder);
				}
			}
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0003E010 File Offset: 0x0003C210
		private static bool ShouldCache(int refCount)
		{
			return refCount > 2;
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0003E016 File Offset: 0x0003C216
		private static void EmitConstantsArray(LambdaCompiler lc)
		{
			lc.EmitClosureArgument();
			lc.IL.Emit(OpCodes.Ldfld, CachedReflectionInfo.Closure_Constants);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0003E034 File Offset: 0x0003C234
		private void EmitConstantFromArray(LambdaCompiler lc, object value, Type type)
		{
			int count;
			if (!this._indexes.TryGetValue(value, out count))
			{
				this._indexes.Add(value, count = this._values.Count);
				this._values.Add(value);
			}
			lc.IL.EmitPrimitive(count);
			lc.IL.Emit(OpCodes.Ldelem_Ref);
			if (type.IsValueType)
			{
				lc.IL.Emit(OpCodes.Unbox_Any, type);
				return;
			}
			if (type != typeof(object))
			{
				lc.IL.Emit(OpCodes.Castclass, type);
			}
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0003E0CF File Offset: 0x0003C2CF
		public BoundConstants()
		{
		}

		// Token: 0x04000A8F RID: 2703
		private readonly List<object> _values = new List<object>();

		// Token: 0x04000A90 RID: 2704
		private readonly Dictionary<object, int> _indexes = new Dictionary<object, int>(ReferenceEqualityComparer<object>.Instance);

		// Token: 0x04000A91 RID: 2705
		private readonly Dictionary<BoundConstants.TypedConstant, int> _references = new Dictionary<BoundConstants.TypedConstant, int>();

		// Token: 0x04000A92 RID: 2706
		private readonly Dictionary<BoundConstants.TypedConstant, LocalBuilder> _cache = new Dictionary<BoundConstants.TypedConstant, LocalBuilder>();

		// Token: 0x020002A7 RID: 679
		private readonly struct TypedConstant : IEquatable<BoundConstants.TypedConstant>
		{
			// Token: 0x0600142A RID: 5162 RVA: 0x0003E108 File Offset: 0x0003C308
			internal TypedConstant(object value, Type type)
			{
				this.Value = value;
				this.Type = type;
			}

			// Token: 0x0600142B RID: 5163 RVA: 0x0003E118 File Offset: 0x0003C318
			public override int GetHashCode()
			{
				return RuntimeHelpers.GetHashCode(this.Value) ^ this.Type.GetHashCode();
			}

			// Token: 0x0600142C RID: 5164 RVA: 0x0003E131 File Offset: 0x0003C331
			public bool Equals(BoundConstants.TypedConstant other)
			{
				return this.Value == other.Value && this.Type.Equals(other.Type);
			}

			// Token: 0x0600142D RID: 5165 RVA: 0x0003E154 File Offset: 0x0003C354
			public override bool Equals(object obj)
			{
				return obj is BoundConstants.TypedConstant && this.Equals((BoundConstants.TypedConstant)obj);
			}

			// Token: 0x04000A93 RID: 2707
			internal readonly object Value;

			// Token: 0x04000A94 RID: 2708
			internal readonly Type Type;
		}
	}
}
