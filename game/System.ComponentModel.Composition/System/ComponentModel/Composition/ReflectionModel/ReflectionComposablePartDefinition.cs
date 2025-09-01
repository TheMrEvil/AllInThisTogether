using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Internal;
using Microsoft.Internal.Collections;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000073 RID: 115
	internal class ReflectionComposablePartDefinition : ComposablePartDefinition, ICompositionElement
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x000096E8 File Offset: 0x000078E8
		public ReflectionComposablePartDefinition(IReflectionPartCreationInfo creationInfo)
		{
			Assumes.NotNull<IReflectionPartCreationInfo>(creationInfo);
			this._creationInfo = creationInfo;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00009708 File Offset: 0x00007908
		public Type GetPartType()
		{
			return this._creationInfo.GetPartType();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00009715 File Offset: 0x00007915
		public Lazy<Type> GetLazyPartType()
		{
			return this._creationInfo.GetLazyPartType();
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00009724 File Offset: 0x00007924
		public ConstructorInfo GetConstructor()
		{
			if (this._constructor == null)
			{
				ConstructorInfo constructor = this._creationInfo.GetConstructor();
				object @lock = this._lock;
				lock (@lock)
				{
					if (this._constructor == null)
					{
						this._constructor = constructor;
					}
				}
			}
			return this._constructor;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000979C File Offset: 0x0000799C
		public override IEnumerable<ExportDefinition> ExportDefinitions
		{
			get
			{
				if (this._exports == null)
				{
					ExportDefinition[] exports = this._creationInfo.GetExports().ToArray<ExportDefinition>();
					object @lock = this._lock;
					lock (@lock)
					{
						if (this._exports == null)
						{
							this._exports = exports;
						}
					}
				}
				return this._exports;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000980C File Offset: 0x00007A0C
		public override IEnumerable<ImportDefinition> ImportDefinitions
		{
			get
			{
				if (this._imports == null)
				{
					ImportDefinition[] imports = this._creationInfo.GetImports().ToArray<ImportDefinition>();
					object @lock = this._lock;
					lock (@lock)
					{
						if (this._imports == null)
						{
							this._imports = imports;
						}
					}
				}
				return this._imports;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000987C File Offset: 0x00007A7C
		public override IDictionary<string, object> Metadata
		{
			get
			{
				if (this._metadata == null)
				{
					IDictionary<string, object> metadata = this._creationInfo.GetMetadata().AsReadOnly();
					object @lock = this._lock;
					lock (@lock)
					{
						if (this._metadata == null)
						{
							this._metadata = metadata;
						}
					}
				}
				return this._metadata;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000098EC File Offset: 0x00007AEC
		internal bool IsDisposalRequired
		{
			get
			{
				return this._creationInfo.IsDisposalRequired;
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000098F9 File Offset: 0x00007AF9
		public override ComposablePart CreatePart()
		{
			if (this.IsDisposalRequired)
			{
				return new DisposableReflectionComposablePart(this);
			}
			return new ReflectionComposablePart(this);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00009910 File Offset: 0x00007B10
		internal override ComposablePartDefinition GetGenericPartDefinition()
		{
			GenericSpecializationPartCreationInfo genericSpecializationPartCreationInfo = this._creationInfo as GenericSpecializationPartCreationInfo;
			if (genericSpecializationPartCreationInfo != null)
			{
				return genericSpecializationPartCreationInfo.OriginalPart;
			}
			return null;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00009934 File Offset: 0x00007B34
		internal override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
		{
			if (this.IsGeneric())
			{
				List<Tuple<ComposablePartDefinition, ExportDefinition>> list = null;
				IEnumerable<object> enumerable = (definition.Metadata.Count > 0) ? definition.Metadata.GetValue("System.ComponentModel.Composition.GenericParameters") : null;
				if (enumerable != null)
				{
					Type[] genericParameters = null;
					if (ReflectionComposablePartDefinition.TryGetGenericTypeParameters(enumerable, out genericParameters))
					{
						foreach (Type[] genericTypeParameters in this.GetCandidateParameters(genericParameters))
						{
							ComposablePartDefinition composablePartDefinition = null;
							if (this.TryMakeGenericPartDefinition(genericTypeParameters, out composablePartDefinition))
							{
								IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> exports = composablePartDefinition.GetExports(definition);
								if (exports != ComposablePartDefinition._EmptyExports)
								{
									list = list.FastAppendToListAllowNulls(exports);
								}
							}
						}
					}
				}
				IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> enumerable2 = list;
				return enumerable2 ?? ComposablePartDefinition._EmptyExports;
			}
			return base.GetExports(definition);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000099FC File Offset: 0x00007BFC
		private IEnumerable<Type[]> GetCandidateParameters(Type[] genericParameters)
		{
			foreach (ExportDefinition exportDefinition in this.ExportDefinitions)
			{
				int[] value = exportDefinition.Metadata.GetValue("System.ComponentModel.Composition.GenericExportParametersOrderMetadataName");
				if (value != null && value.Length == genericParameters.Length)
				{
					yield return GenericServices.Reorder<Type>(genericParameters, value);
				}
			}
			IEnumerator<ExportDefinition> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00009A14 File Offset: 0x00007C14
		private static bool TryGetGenericTypeParameters(IEnumerable<object> genericParameters, out Type[] genericTypeParameters)
		{
			genericTypeParameters = (genericParameters as Type[]);
			if (genericTypeParameters == null)
			{
				object[] array = genericParameters.AsArray<object>();
				genericTypeParameters = new Type[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					genericTypeParameters[i] = (array[i] as Type);
					if (genericTypeParameters[i] == null)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00009A67 File Offset: 0x00007C67
		internal bool TryMakeGenericPartDefinition(Type[] genericTypeParameters, out ComposablePartDefinition genericPartDefinition)
		{
			genericPartDefinition = null;
			if (!GenericSpecializationPartCreationInfo.CanSpecialize(this.Metadata, genericTypeParameters))
			{
				return false;
			}
			genericPartDefinition = new ReflectionComposablePartDefinition(new GenericSpecializationPartCreationInfo(this._creationInfo, this, genericTypeParameters));
			return true;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00009A91 File Offset: 0x00007C91
		string ICompositionElement.DisplayName
		{
			get
			{
				return this._creationInfo.DisplayName;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00009A9E File Offset: 0x00007C9E
		ICompositionElement ICompositionElement.Origin
		{
			get
			{
				return this._creationInfo.Origin;
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00009A91 File Offset: 0x00007C91
		public override string ToString()
		{
			return this._creationInfo.DisplayName;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00009AAC File Offset: 0x00007CAC
		public override bool Equals(object obj)
		{
			ReflectionComposablePartDefinition reflectionComposablePartDefinition = obj as ReflectionComposablePartDefinition;
			return reflectionComposablePartDefinition != null && this._creationInfo.Equals(reflectionComposablePartDefinition._creationInfo);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00009AD6 File Offset: 0x00007CD6
		public override int GetHashCode()
		{
			return this._creationInfo.GetHashCode();
		}

		// Token: 0x0400013E RID: 318
		private readonly IReflectionPartCreationInfo _creationInfo;

		// Token: 0x0400013F RID: 319
		private volatile IEnumerable<ImportDefinition> _imports;

		// Token: 0x04000140 RID: 320
		private volatile IEnumerable<ExportDefinition> _exports;

		// Token: 0x04000141 RID: 321
		private volatile IDictionary<string, object> _metadata;

		// Token: 0x04000142 RID: 322
		private volatile ConstructorInfo _constructor;

		// Token: 0x04000143 RID: 323
		private object _lock = new object();

		// Token: 0x02000074 RID: 116
		[CompilerGenerated]
		private sealed class <GetCandidateParameters>d__21 : IEnumerable<Type[]>, IEnumerable, IEnumerator<Type[]>, IDisposable, IEnumerator
		{
			// Token: 0x06000307 RID: 775 RVA: 0x00009AE3 File Offset: 0x00007CE3
			[DebuggerHidden]
			public <GetCandidateParameters>d__21(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000308 RID: 776 RVA: 0x00009B00 File Offset: 0x00007D00
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

			// Token: 0x06000309 RID: 777 RVA: 0x00009B38 File Offset: 0x00007D38
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					ReflectionComposablePartDefinition reflectionComposablePartDefinition = this;
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
						enumerator = reflectionComposablePartDefinition.ExportDefinitions.GetEnumerator();
						this.<>1__state = -3;
					}
					while (enumerator.MoveNext())
					{
						int[] value = enumerator.Current.Metadata.GetValue("System.ComponentModel.Composition.GenericExportParametersOrderMetadataName");
						if (value != null && value.Length == genericParameters.Length)
						{
							this.<>2__current = GenericServices.Reorder<Type>(genericParameters, value);
							this.<>1__state = 1;
							return true;
						}
					}
					this.<>m__Finally1();
					enumerator = null;
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x0600030A RID: 778 RVA: 0x00009C0C File Offset: 0x00007E0C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x0600030B RID: 779 RVA: 0x00009C28 File Offset: 0x00007E28
			Type[] IEnumerator<Type[]>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600030C RID: 780 RVA: 0x00002C6B File Offset: 0x00000E6B
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x0600030D RID: 781 RVA: 0x00009C28 File Offset: 0x00007E28
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600030E RID: 782 RVA: 0x00009C30 File Offset: 0x00007E30
			[DebuggerHidden]
			IEnumerator<Type[]> IEnumerable<Type[]>.GetEnumerator()
			{
				ReflectionComposablePartDefinition.<GetCandidateParameters>d__21 <GetCandidateParameters>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetCandidateParameters>d__ = this;
				}
				else
				{
					<GetCandidateParameters>d__ = new ReflectionComposablePartDefinition.<GetCandidateParameters>d__21(0);
					<GetCandidateParameters>d__.<>4__this = this;
				}
				<GetCandidateParameters>d__.genericParameters = genericParameters;
				return <GetCandidateParameters>d__;
			}

			// Token: 0x0600030F RID: 783 RVA: 0x00009C7F File Offset: 0x00007E7F
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Type[]>.GetEnumerator();
			}

			// Token: 0x04000144 RID: 324
			private int <>1__state;

			// Token: 0x04000145 RID: 325
			private Type[] <>2__current;

			// Token: 0x04000146 RID: 326
			private int <>l__initialThreadId;

			// Token: 0x04000147 RID: 327
			public ReflectionComposablePartDefinition <>4__this;

			// Token: 0x04000148 RID: 328
			private Type[] genericParameters;

			// Token: 0x04000149 RID: 329
			public Type[] <>3__genericParameters;

			// Token: 0x0400014A RID: 330
			private IEnumerator<ExportDefinition> <>7__wrap1;
		}
	}
}
