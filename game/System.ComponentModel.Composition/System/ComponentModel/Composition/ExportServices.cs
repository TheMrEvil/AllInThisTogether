using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Internal;
using Microsoft.Internal.Collections;

namespace System.ComponentModel.Composition
{
	// Token: 0x0200003B RID: 59
	internal static class ExportServices
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x000059A7 File Offset: 0x00003BA7
		internal static bool IsDefaultMetadataViewType(Type metadataViewType)
		{
			Assumes.NotNull<Type>(metadataViewType);
			return metadataViewType.IsAssignableFrom(ExportServices.DefaultMetadataViewType);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000059BA File Offset: 0x00003BBA
		internal static bool IsDictionaryConstructorViewType(Type metadataViewType)
		{
			Assumes.NotNull<Type>(metadataViewType);
			return metadataViewType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, Type.DefaultBinder, new Type[]
			{
				typeof(IDictionary<string, object>)
			}, new ParameterModifier[0]) != null;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000059F0 File Offset: 0x00003BF0
		internal static Func<Export, object> CreateStronglyTypedLazyFactory(Type exportType, Type metadataViewType)
		{
			MethodInfo methodInfo;
			if (metadataViewType != null)
			{
				methodInfo = ExportServices._createStronglyTypedLazyOfTM.MakeGenericMethod(new Type[]
				{
					exportType ?? ExportServices.DefaultExportedValueType,
					metadataViewType
				});
			}
			else
			{
				methodInfo = ExportServices._createStronglyTypedLazyOfT.MakeGenericMethod(new Type[]
				{
					exportType ?? ExportServices.DefaultExportedValueType
				});
			}
			Assumes.NotNull<MethodInfo>(methodInfo);
			return (Func<Export, object>)Delegate.CreateDelegate(typeof(Func<Export, object>), methodInfo);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005A68 File Offset: 0x00003C68
		internal static Func<Export, Lazy<object, object>> CreateSemiStronglyTypedLazyFactory(Type exportType, Type metadataViewType)
		{
			MethodInfo methodInfo = ExportServices._createSemiStronglyTypedLazy.MakeGenericMethod(new Type[]
			{
				exportType ?? ExportServices.DefaultExportedValueType,
				metadataViewType ?? ExportServices.DefaultMetadataViewType
			});
			Assumes.NotNull<MethodInfo>(methodInfo);
			return (Func<Export, Lazy<object, object>>)Delegate.CreateDelegate(typeof(Func<Export, Lazy<object, object>>), methodInfo);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00005ABC File Offset: 0x00003CBC
		internal static Lazy<T, M> CreateStronglyTypedLazyOfTM<T, M>(Export export)
		{
			IDisposable disposable = export as IDisposable;
			if (disposable != null)
			{
				return new ExportServices.DisposableLazy<T, M>(() => ExportServices.GetCastedExportedValue<T>(export), AttributedModelServices.GetMetadataView<M>(export.Metadata), disposable, LazyThreadSafetyMode.PublicationOnly);
			}
			return new Lazy<T, M>(() => ExportServices.GetCastedExportedValue<T>(export), AttributedModelServices.GetMetadataView<M>(export.Metadata), LazyThreadSafetyMode.PublicationOnly);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005B2C File Offset: 0x00003D2C
		internal static Lazy<T> CreateStronglyTypedLazyOfT<T>(Export export)
		{
			IDisposable disposable = export as IDisposable;
			if (disposable != null)
			{
				return new ExportServices.DisposableLazy<T>(() => ExportServices.GetCastedExportedValue<T>(export), disposable, LazyThreadSafetyMode.PublicationOnly);
			}
			return new Lazy<T>(() => ExportServices.GetCastedExportedValue<T>(export), LazyThreadSafetyMode.PublicationOnly);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005B7C File Offset: 0x00003D7C
		internal static Lazy<object, object> CreateSemiStronglyTypedLazy<T, M>(Export export)
		{
			IDisposable disposable = export as IDisposable;
			if (disposable != null)
			{
				return new ExportServices.DisposableLazy<object, object>(() => ExportServices.GetCastedExportedValue<T>(export), AttributedModelServices.GetMetadataView<M>(export.Metadata), disposable, LazyThreadSafetyMode.PublicationOnly);
			}
			return new Lazy<object, object>(() => ExportServices.GetCastedExportedValue<T>(export), AttributedModelServices.GetMetadataView<M>(export.Metadata), LazyThreadSafetyMode.PublicationOnly);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00005BF5 File Offset: 0x00003DF5
		internal static T GetCastedExportedValue<T>(Export export)
		{
			return ExportServices.CastExportedValue<T>(export.ToElement(), export.Value);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00005C08 File Offset: 0x00003E08
		internal static T CastExportedValue<T>(ICompositionElement element, object exportedValue)
		{
			object obj = null;
			if (!ContractServices.TryCast(typeof(T), exportedValue, out obj))
			{
				throw new CompositionContractMismatchException(string.Format(CultureInfo.CurrentCulture, Strings.ContractMismatch_ExportedValueCannotBeCastToT, element.DisplayName, typeof(T)));
			}
			return (T)((object)obj);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00005C56 File Offset: 0x00003E56
		internal static ExportCardinalityCheckResult CheckCardinality<T>(ImportDefinition definition, IEnumerable<T> enumerable)
		{
			return ExportServices.MatchCardinality((enumerable != null) ? enumerable.GetCardinality<T>() : EnumerableCardinality.Zero, definition.Cardinality);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005C6F File Offset: 0x00003E6F
		private static ExportCardinalityCheckResult MatchCardinality(EnumerableCardinality actualCardinality, ImportCardinality importCardinality)
		{
			if (actualCardinality != EnumerableCardinality.Zero)
			{
				if (actualCardinality != EnumerableCardinality.TwoOrMore)
				{
					Assumes.IsTrue(actualCardinality == EnumerableCardinality.One);
				}
				else if (importCardinality.IsAtMostOne())
				{
					return ExportCardinalityCheckResult.TooManyExports;
				}
			}
			else if (importCardinality == ImportCardinality.ExactlyOne)
			{
				return ExportCardinalityCheckResult.NoExports;
			}
			return ExportCardinalityCheckResult.Match;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005C94 File Offset: 0x00003E94
		// Note: this type is marked as 'beforefieldinit'.
		static ExportServices()
		{
		}

		// Token: 0x040000BB RID: 187
		private static readonly MethodInfo _createStronglyTypedLazyOfTM = typeof(ExportServices).GetMethod("CreateStronglyTypedLazyOfTM", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040000BC RID: 188
		private static readonly MethodInfo _createStronglyTypedLazyOfT = typeof(ExportServices).GetMethod("CreateStronglyTypedLazyOfT", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040000BD RID: 189
		private static readonly MethodInfo _createSemiStronglyTypedLazy = typeof(ExportServices).GetMethod("CreateSemiStronglyTypedLazy", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040000BE RID: 190
		internal static readonly Type DefaultMetadataViewType = typeof(IDictionary<string, object>);

		// Token: 0x040000BF RID: 191
		internal static readonly Type DefaultExportedValueType = typeof(object);

		// Token: 0x0200003C RID: 60
		private sealed class DisposableLazy<T, TMetadataView> : Lazy<T, TMetadataView>, IDisposable
		{
			// Token: 0x060001CE RID: 462 RVA: 0x00005D10 File Offset: 0x00003F10
			public DisposableLazy(Func<T> valueFactory, TMetadataView metadataView, IDisposable disposable, LazyThreadSafetyMode mode) : base(valueFactory, metadataView, mode)
			{
				Assumes.NotNull<IDisposable>(disposable);
				this._disposable = disposable;
			}

			// Token: 0x060001CF RID: 463 RVA: 0x00005D29 File Offset: 0x00003F29
			void IDisposable.Dispose()
			{
				this._disposable.Dispose();
			}

			// Token: 0x040000C0 RID: 192
			private IDisposable _disposable;
		}

		// Token: 0x0200003D RID: 61
		private sealed class DisposableLazy<T> : Lazy<T>, IDisposable
		{
			// Token: 0x060001D0 RID: 464 RVA: 0x00005D36 File Offset: 0x00003F36
			public DisposableLazy(Func<T> valueFactory, IDisposable disposable, LazyThreadSafetyMode mode) : base(valueFactory, mode)
			{
				Assumes.NotNull<IDisposable>(disposable);
				this._disposable = disposable;
			}

			// Token: 0x060001D1 RID: 465 RVA: 0x00005D4D File Offset: 0x00003F4D
			void IDisposable.Dispose()
			{
				this._disposable.Dispose();
			}

			// Token: 0x040000C1 RID: 193
			private IDisposable _disposable;
		}

		// Token: 0x0200003E RID: 62
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0<T, M>
		{
			// Token: 0x060001D2 RID: 466 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x060001D3 RID: 467 RVA: 0x00005D5A File Offset: 0x00003F5A
			internal T <CreateStronglyTypedLazyOfTM>b__0()
			{
				return ExportServices.GetCastedExportedValue<T>(this.export);
			}

			// Token: 0x060001D4 RID: 468 RVA: 0x00005D5A File Offset: 0x00003F5A
			internal T <CreateStronglyTypedLazyOfTM>b__1()
			{
				return ExportServices.GetCastedExportedValue<T>(this.export);
			}

			// Token: 0x040000C2 RID: 194
			public Export export;
		}

		// Token: 0x0200003F RID: 63
		[CompilerGenerated]
		private sealed class <>c__DisplayClass12_0<T>
		{
			// Token: 0x060001D5 RID: 469 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass12_0()
			{
			}

			// Token: 0x060001D6 RID: 470 RVA: 0x00005D67 File Offset: 0x00003F67
			internal T <CreateStronglyTypedLazyOfT>b__0()
			{
				return ExportServices.GetCastedExportedValue<T>(this.export);
			}

			// Token: 0x060001D7 RID: 471 RVA: 0x00005D67 File Offset: 0x00003F67
			internal T <CreateStronglyTypedLazyOfT>b__1()
			{
				return ExportServices.GetCastedExportedValue<T>(this.export);
			}

			// Token: 0x040000C3 RID: 195
			public Export export;
		}

		// Token: 0x02000040 RID: 64
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0<T, M>
		{
			// Token: 0x060001D8 RID: 472 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x060001D9 RID: 473 RVA: 0x00005D74 File Offset: 0x00003F74
			internal object <CreateSemiStronglyTypedLazy>b__0()
			{
				return ExportServices.GetCastedExportedValue<T>(this.export);
			}

			// Token: 0x060001DA RID: 474 RVA: 0x00005D74 File Offset: 0x00003F74
			internal object <CreateSemiStronglyTypedLazy>b__1()
			{
				return ExportServices.GetCastedExportedValue<T>(this.export);
			}

			// Token: 0x040000C4 RID: 196
			public Export export;
		}
	}
}
