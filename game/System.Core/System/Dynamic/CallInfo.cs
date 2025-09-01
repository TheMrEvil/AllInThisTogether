using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Describes arguments in the dynamic binding process.</summary>
	// Token: 0x020002F8 RID: 760
	public sealed class CallInfo
	{
		/// <summary>Creates a new PositionalArgumentInfo.</summary>
		/// <param name="argCount">The number of arguments.</param>
		/// <param name="argNames">The argument names.</param>
		// Token: 0x060016E6 RID: 5862 RVA: 0x0004CEB2 File Offset: 0x0004B0B2
		public CallInfo(int argCount, params string[] argNames) : this(argCount, argNames)
		{
		}

		/// <summary>Creates a new CallInfo that represents arguments in the dynamic binding process.</summary>
		/// <param name="argCount">The number of arguments.</param>
		/// <param name="argNames">The argument names.</param>
		// Token: 0x060016E7 RID: 5863 RVA: 0x0004CEBC File Offset: 0x0004B0BC
		public CallInfo(int argCount, IEnumerable<string> argNames)
		{
			ContractUtils.RequiresNotNull(argNames, "argNames");
			ReadOnlyCollection<string> readOnlyCollection = argNames.ToReadOnly<string>();
			if (argCount < readOnlyCollection.Count)
			{
				throw Error.ArgCntMustBeGreaterThanNameCnt();
			}
			ContractUtils.RequiresNotNullItems<string>(readOnlyCollection, "argNames");
			this.ArgumentCount = argCount;
			this.ArgumentNames = readOnlyCollection;
		}

		/// <summary>The number of arguments.</summary>
		/// <returns>The number of arguments.</returns>
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x0004CF09 File Offset: 0x0004B109
		public int ArgumentCount
		{
			[CompilerGenerated]
			get
			{
				return this.<ArgumentCount>k__BackingField;
			}
		}

		/// <summary>The argument names.</summary>
		/// <returns>The read-only collection of argument names.</returns>
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x0004CF11 File Offset: 0x0004B111
		public ReadOnlyCollection<string> ArgumentNames
		{
			[CompilerGenerated]
			get
			{
				return this.<ArgumentNames>k__BackingField;
			}
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Dynamic.CallInfo" />.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Dynamic.CallInfo" />.</returns>
		// Token: 0x060016EA RID: 5866 RVA: 0x0004CF19 File Offset: 0x0004B119
		public override int GetHashCode()
		{
			return this.ArgumentCount ^ this.ArgumentNames.ListHashCode<string>();
		}

		/// <summary>Determines whether the specified CallInfo instance is considered equal to the current.</summary>
		/// <param name="obj">The instance of <see cref="T:System.Dynamic.CallInfo" /> to compare with the current instance.</param>
		/// <returns>true if the specified instance is equal to the current one otherwise, false.</returns>
		// Token: 0x060016EB RID: 5867 RVA: 0x0004CF30 File Offset: 0x0004B130
		public override bool Equals(object obj)
		{
			CallInfo callInfo = obj as CallInfo;
			return callInfo != null && this.ArgumentCount == callInfo.ArgumentCount && this.ArgumentNames.ListEquals(callInfo.ArgumentNames);
		}

		// Token: 0x04000B79 RID: 2937
		[CompilerGenerated]
		private readonly int <ArgumentCount>k__BackingField;

		// Token: 0x04000B7A RID: 2938
		[CompilerGenerated]
		private readonly ReadOnlyCollection<string> <ArgumentNames>k__BackingField;
	}
}
