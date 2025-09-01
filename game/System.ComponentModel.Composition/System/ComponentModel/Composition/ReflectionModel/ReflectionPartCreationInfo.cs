using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x0200007F RID: 127
	internal class ReflectionPartCreationInfo : IReflectionPartCreationInfo, ICompositionElement
	{
		// Token: 0x0600035E RID: 862 RVA: 0x0000A40E File Offset: 0x0000860E
		public ReflectionPartCreationInfo(Lazy<Type> partType, bool isDisposalRequired, Lazy<IEnumerable<ImportDefinition>> imports, Lazy<IEnumerable<ExportDefinition>> exports, Lazy<IDictionary<string, object>> metadata, ICompositionElement origin)
		{
			Assumes.NotNull<Lazy<Type>>(partType);
			this._partType = partType;
			this._isDisposalRequired = isDisposalRequired;
			this._imports = imports;
			this._exports = exports;
			this._metadata = metadata;
			this._origin = origin;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000A449 File Offset: 0x00008649
		public Type GetPartType()
		{
			return this._partType.GetNotNullValue("type");
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000A45B File Offset: 0x0000865B
		public Lazy<Type> GetLazyPartType()
		{
			return this._partType;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000A464 File Offset: 0x00008664
		public ConstructorInfo GetConstructor()
		{
			if (this._constructor == null)
			{
				ConstructorInfo[] array = (from parameterImport in this.GetImports().OfType<ReflectionParameterImportDefinition>()
				select parameterImport.ImportingLazyParameter.Value.Member).OfType<ConstructorInfo>().Distinct<ConstructorInfo>().ToArray<ConstructorInfo>();
				if (array.Length == 1)
				{
					this._constructor = array[0];
				}
				else if (array.Length == 0)
				{
					this._constructor = this.GetPartType().GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
				}
			}
			return this._constructor;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000A4F5 File Offset: 0x000086F5
		public bool IsDisposalRequired
		{
			get
			{
				return this._isDisposalRequired;
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000A4FD File Offset: 0x000086FD
		public IDictionary<string, object> GetMetadata()
		{
			if (this._metadata == null)
			{
				return null;
			}
			return this._metadata.Value;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000A514 File Offset: 0x00008714
		public IEnumerable<ExportDefinition> GetExports()
		{
			if (this._exports == null)
			{
				yield break;
			}
			IEnumerable<ExportDefinition> value = this._exports.Value;
			if (value == null)
			{
				yield break;
			}
			foreach (ExportDefinition exportDefinition in value)
			{
				ReflectionMemberExportDefinition reflectionMemberExportDefinition = exportDefinition as ReflectionMemberExportDefinition;
				if (reflectionMemberExportDefinition == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidExportDefinition, exportDefinition.GetType()));
				}
				yield return reflectionMemberExportDefinition;
			}
			IEnumerator<ExportDefinition> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000A524 File Offset: 0x00008724
		public IEnumerable<ImportDefinition> GetImports()
		{
			if (this._imports == null)
			{
				yield break;
			}
			IEnumerable<ImportDefinition> value = this._imports.Value;
			if (value == null)
			{
				yield break;
			}
			foreach (ImportDefinition importDefinition in value)
			{
				ReflectionImportDefinition reflectionImportDefinition = importDefinition as ReflectionImportDefinition;
				if (reflectionImportDefinition == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidMemberImportDefinition, importDefinition.GetType()));
				}
				yield return reflectionImportDefinition;
			}
			IEnumerator<ImportDefinition> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000A534 File Offset: 0x00008734
		public string DisplayName
		{
			get
			{
				return this.GetPartType().GetDisplayName();
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000A541 File Offset: 0x00008741
		public ICompositionElement Origin
		{
			get
			{
				return this._origin;
			}
		}

		// Token: 0x04000159 RID: 345
		private readonly Lazy<Type> _partType;

		// Token: 0x0400015A RID: 346
		private readonly Lazy<IEnumerable<ImportDefinition>> _imports;

		// Token: 0x0400015B RID: 347
		private readonly Lazy<IEnumerable<ExportDefinition>> _exports;

		// Token: 0x0400015C RID: 348
		private readonly Lazy<IDictionary<string, object>> _metadata;

		// Token: 0x0400015D RID: 349
		private readonly ICompositionElement _origin;

		// Token: 0x0400015E RID: 350
		private ConstructorInfo _constructor;

		// Token: 0x0400015F RID: 351
		private bool _isDisposalRequired;

		// Token: 0x02000080 RID: 128
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000368 RID: 872 RVA: 0x0000A549 File Offset: 0x00008749
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000369 RID: 873 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c()
			{
			}

			// Token: 0x0600036A RID: 874 RVA: 0x0000A555 File Offset: 0x00008755
			internal MemberInfo <GetConstructor>b__10_0(ReflectionParameterImportDefinition parameterImport)
			{
				return parameterImport.ImportingLazyParameter.Value.Member;
			}

			// Token: 0x04000160 RID: 352
			public static readonly ReflectionPartCreationInfo.<>c <>9 = new ReflectionPartCreationInfo.<>c();

			// Token: 0x04000161 RID: 353
			public static Func<ReflectionParameterImportDefinition, MemberInfo> <>9__10_0;
		}

		// Token: 0x02000081 RID: 129
		[CompilerGenerated]
		private sealed class <GetExports>d__14 : IEnumerable<ExportDefinition>, IEnumerable, IEnumerator<ExportDefinition>, IDisposable, IEnumerator
		{
			// Token: 0x0600036B RID: 875 RVA: 0x0000A567 File Offset: 0x00008767
			[DebuggerHidden]
			public <GetExports>d__14(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600036C RID: 876 RVA: 0x0000A584 File Offset: 0x00008784
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x0600036D RID: 877 RVA: 0x0000A5BC File Offset: 0x000087BC
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					ReflectionPartCreationInfo reflectionPartCreationInfo = this;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
					}
					else
					{
						this.<>1__state = -1;
						if (reflectionPartCreationInfo._exports == null)
						{
							return false;
						}
						IEnumerable<ExportDefinition> value = reflectionPartCreationInfo._exports.Value;
						if (value == null)
						{
							return false;
						}
						enumerator = value.GetEnumerator();
						this.<>1__state = -3;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						result = false;
					}
					else
					{
						ExportDefinition exportDefinition = enumerator.Current;
						ReflectionMemberExportDefinition reflectionMemberExportDefinition = exportDefinition as ReflectionMemberExportDefinition;
						if (reflectionMemberExportDefinition == null)
						{
							throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidExportDefinition, exportDefinition.GetType()));
						}
						this.<>2__current = reflectionMemberExportDefinition;
						this.<>1__state = 1;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x0600036E RID: 878 RVA: 0x0000A6B4 File Offset: 0x000088B4
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x0600036F RID: 879 RVA: 0x0000A6D0 File Offset: 0x000088D0
			ExportDefinition IEnumerator<ExportDefinition>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000370 RID: 880 RVA: 0x00002C6B File Offset: 0x00000E6B
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x06000371 RID: 881 RVA: 0x0000A6D0 File Offset: 0x000088D0
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000372 RID: 882 RVA: 0x0000A6D8 File Offset: 0x000088D8
			[DebuggerHidden]
			IEnumerator<ExportDefinition> IEnumerable<ExportDefinition>.GetEnumerator()
			{
				ReflectionPartCreationInfo.<GetExports>d__14 <GetExports>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetExports>d__ = this;
				}
				else
				{
					<GetExports>d__ = new ReflectionPartCreationInfo.<GetExports>d__14(0);
					<GetExports>d__.<>4__this = this;
				}
				return <GetExports>d__;
			}

			// Token: 0x06000373 RID: 883 RVA: 0x0000A71B File Offset: 0x0000891B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.ComponentModel.Composition.Primitives.ExportDefinition>.GetEnumerator();
			}

			// Token: 0x04000162 RID: 354
			private int <>1__state;

			// Token: 0x04000163 RID: 355
			private ExportDefinition <>2__current;

			// Token: 0x04000164 RID: 356
			private int <>l__initialThreadId;

			// Token: 0x04000165 RID: 357
			public ReflectionPartCreationInfo <>4__this;

			// Token: 0x04000166 RID: 358
			private IEnumerator<ExportDefinition> <>7__wrap1;
		}

		// Token: 0x02000082 RID: 130
		[CompilerGenerated]
		private sealed class <GetImports>d__15 : IEnumerable<ImportDefinition>, IEnumerable, IEnumerator<ImportDefinition>, IDisposable, IEnumerator
		{
			// Token: 0x06000374 RID: 884 RVA: 0x0000A723 File Offset: 0x00008923
			[DebuggerHidden]
			public <GetImports>d__15(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000375 RID: 885 RVA: 0x0000A740 File Offset: 0x00008940
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06000376 RID: 886 RVA: 0x0000A778 File Offset: 0x00008978
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					ReflectionPartCreationInfo reflectionPartCreationInfo = this;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
					}
					else
					{
						this.<>1__state = -1;
						if (reflectionPartCreationInfo._imports == null)
						{
							return false;
						}
						IEnumerable<ImportDefinition> value = reflectionPartCreationInfo._imports.Value;
						if (value == null)
						{
							return false;
						}
						enumerator = value.GetEnumerator();
						this.<>1__state = -3;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						result = false;
					}
					else
					{
						ImportDefinition importDefinition = enumerator.Current;
						ReflectionImportDefinition reflectionImportDefinition = importDefinition as ReflectionImportDefinition;
						if (reflectionImportDefinition == null)
						{
							throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidMemberImportDefinition, importDefinition.GetType()));
						}
						this.<>2__current = reflectionImportDefinition;
						this.<>1__state = 1;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06000377 RID: 887 RVA: 0x0000A870 File Offset: 0x00008A70
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x06000378 RID: 888 RVA: 0x0000A88C File Offset: 0x00008A8C
			ImportDefinition IEnumerator<ImportDefinition>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000379 RID: 889 RVA: 0x00002C6B File Offset: 0x00000E6B
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x0600037A RID: 890 RVA: 0x0000A88C File Offset: 0x00008A8C
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600037B RID: 891 RVA: 0x0000A894 File Offset: 0x00008A94
			[DebuggerHidden]
			IEnumerator<ImportDefinition> IEnumerable<ImportDefinition>.GetEnumerator()
			{
				ReflectionPartCreationInfo.<GetImports>d__15 <GetImports>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetImports>d__ = this;
				}
				else
				{
					<GetImports>d__ = new ReflectionPartCreationInfo.<GetImports>d__15(0);
					<GetImports>d__.<>4__this = this;
				}
				return <GetImports>d__;
			}

			// Token: 0x0600037C RID: 892 RVA: 0x0000A8D7 File Offset: 0x00008AD7
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.ComponentModel.Composition.Primitives.ImportDefinition>.GetEnumerator();
			}

			// Token: 0x04000167 RID: 359
			private int <>1__state;

			// Token: 0x04000168 RID: 360
			private ImportDefinition <>2__current;

			// Token: 0x04000169 RID: 361
			private int <>l__initialThreadId;

			// Token: 0x0400016A RID: 362
			public ReflectionPartCreationInfo <>4__this;

			// Token: 0x0400016B RID: 363
			private IEnumerator<ImportDefinition> <>7__wrap1;
		}
	}
}
