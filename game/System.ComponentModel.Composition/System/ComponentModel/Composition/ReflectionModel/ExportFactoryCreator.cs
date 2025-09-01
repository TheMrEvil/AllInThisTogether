using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000054 RID: 84
	internal sealed class ExportFactoryCreator
	{
		// Token: 0x0600022E RID: 558 RVA: 0x00006A84 File Offset: 0x00004C84
		public ExportFactoryCreator(Type exportFactoryType)
		{
			Assumes.NotNull<Type>(exportFactoryType);
			this._exportFactoryType = exportFactoryType;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00006A9C File Offset: 0x00004C9C
		public Func<Export, object> CreateStronglyTypedExportFactoryFactory(Type exportType, Type metadataViewType)
		{
			MethodInfo methodInfo;
			if (metadataViewType == null)
			{
				methodInfo = ExportFactoryCreator._createStronglyTypedExportFactoryOfT.MakeGenericMethod(new Type[]
				{
					exportType
				});
			}
			else
			{
				methodInfo = ExportFactoryCreator._createStronglyTypedExportFactoryOfTM.MakeGenericMethod(new Type[]
				{
					exportType,
					metadataViewType
				});
			}
			Assumes.NotNull<MethodInfo>(methodInfo);
			Func<Export, object> exportFactoryFactory = (Func<Export, object>)Delegate.CreateDelegate(typeof(Func<Export, object>), this, methodInfo);
			return (Export e) => exportFactoryFactory(e);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00006B18 File Offset: 0x00004D18
		private object CreateStronglyTypedExportFactoryOfT<T>(Export export)
		{
			Type[] typeArguments = new Type[]
			{
				typeof(T)
			};
			Type type = this._exportFactoryType.MakeGenericType(typeArguments);
			ExportFactoryCreator.LifetimeContext lifetimeContext = new ExportFactoryCreator.LifetimeContext();
			Func<Tuple<T, Action>> func = () => lifetimeContext.GetExportLifetimeContextFromExport<T>(export);
			object[] args = new object[]
			{
				func
			};
			object obj = Activator.CreateInstance(type, args);
			lifetimeContext.SetInstance(obj);
			return obj;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00006B90 File Offset: 0x00004D90
		private object CreateStronglyTypedExportFactoryOfTM<T, M>(Export export)
		{
			Type[] typeArguments = new Type[]
			{
				typeof(T),
				typeof(M)
			};
			Type type = this._exportFactoryType.MakeGenericType(typeArguments);
			ExportFactoryCreator.LifetimeContext lifetimeContext = new ExportFactoryCreator.LifetimeContext();
			Func<Tuple<T, Action>> func = () => lifetimeContext.GetExportLifetimeContextFromExport<T>(export);
			M metadataView = AttributedModelServices.GetMetadataView<M>(export.Metadata);
			object[] args = new object[]
			{
				func,
				metadataView
			};
			object obj = Activator.CreateInstance(type, args);
			lifetimeContext.SetInstance(obj);
			return obj;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006C31 File Offset: 0x00004E31
		// Note: this type is marked as 'beforefieldinit'.
		static ExportFactoryCreator()
		{
		}

		// Token: 0x040000E9 RID: 233
		private static readonly MethodInfo _createStronglyTypedExportFactoryOfT = typeof(ExportFactoryCreator).GetMethod("CreateStronglyTypedExportFactoryOfT", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

		// Token: 0x040000EA RID: 234
		private static readonly MethodInfo _createStronglyTypedExportFactoryOfTM = typeof(ExportFactoryCreator).GetMethod("CreateStronglyTypedExportFactoryOfTM", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

		// Token: 0x040000EB RID: 235
		private Type _exportFactoryType;

		// Token: 0x02000055 RID: 85
		private class LifetimeContext
		{
			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x06000233 RID: 563 RVA: 0x00006C69 File Offset: 0x00004E69
			// (set) Token: 0x06000234 RID: 564 RVA: 0x00006C71 File Offset: 0x00004E71
			public Func<ComposablePartDefinition, bool> CatalogFilter
			{
				[CompilerGenerated]
				get
				{
					return this.<CatalogFilter>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<CatalogFilter>k__BackingField = value;
				}
			}

			// Token: 0x06000235 RID: 565 RVA: 0x00006C7C File Offset: 0x00004E7C
			public void SetInstance(object instance)
			{
				Assumes.NotNull<object>(instance);
				MethodInfo method = instance.GetType().GetMethod("IncludeInScopedCatalog", BindingFlags.Instance | BindingFlags.NonPublic, null, ExportFactoryCreator.LifetimeContext.types, null);
				this.CatalogFilter = (Func<ComposablePartDefinition, bool>)Delegate.CreateDelegate(typeof(Func<ComposablePartDefinition, bool>), instance, method);
			}

			// Token: 0x06000236 RID: 566 RVA: 0x00006CC8 File Offset: 0x00004EC8
			public Tuple<T, Action> GetExportLifetimeContextFromExport<T>(Export export)
			{
				IDisposable disposable = null;
				CatalogExportProvider.ScopeFactoryExport scopeFactoryExport = export as CatalogExportProvider.ScopeFactoryExport;
				T item;
				if (scopeFactoryExport != null)
				{
					Export export2 = scopeFactoryExport.CreateExportProduct(this.CatalogFilter);
					item = ExportServices.GetCastedExportedValue<T>(export2);
					disposable = (export2 as IDisposable);
				}
				else
				{
					CatalogExportProvider.FactoryExport factoryExport = export as CatalogExportProvider.FactoryExport;
					if (factoryExport != null)
					{
						Export export3 = factoryExport.CreateExportProduct();
						item = ExportServices.GetCastedExportedValue<T>(export3);
						disposable = (export3 as IDisposable);
					}
					else
					{
						ComposablePartDefinition castedExportedValue = ExportServices.GetCastedExportedValue<ComposablePartDefinition>(export);
						ComposablePart composablePart = castedExportedValue.CreatePart();
						ExportDefinition definition = castedExportedValue.ExportDefinitions.Single<ExportDefinition>();
						item = ExportServices.CastExportedValue<T>(composablePart.ToElement(), composablePart.GetExportedValue(definition));
						disposable = (composablePart as IDisposable);
					}
				}
				Action item2;
				if (disposable != null)
				{
					item2 = delegate()
					{
						disposable.Dispose();
					};
				}
				else
				{
					item2 = delegate()
					{
					};
				}
				return new Tuple<T, Action>(item, item2);
			}

			// Token: 0x06000237 RID: 567 RVA: 0x00002BAC File Offset: 0x00000DAC
			public LifetimeContext()
			{
			}

			// Token: 0x06000238 RID: 568 RVA: 0x00006DB8 File Offset: 0x00004FB8
			// Note: this type is marked as 'beforefieldinit'.
			static LifetimeContext()
			{
			}

			// Token: 0x040000EC RID: 236
			private static Type[] types = new Type[]
			{
				typeof(ComposablePartDefinition)
			};

			// Token: 0x040000ED RID: 237
			[CompilerGenerated]
			private Func<ComposablePartDefinition, bool> <CatalogFilter>k__BackingField;

			// Token: 0x02000056 RID: 86
			[CompilerGenerated]
			private sealed class <>c__DisplayClass6_0<T>
			{
				// Token: 0x06000239 RID: 569 RVA: 0x00002BAC File Offset: 0x00000DAC
				public <>c__DisplayClass6_0()
				{
				}

				// Token: 0x0600023A RID: 570 RVA: 0x00006DD2 File Offset: 0x00004FD2
				internal void <GetExportLifetimeContextFromExport>b__0()
				{
					this.disposable.Dispose();
				}

				// Token: 0x040000EE RID: 238
				public IDisposable disposable;
			}

			// Token: 0x02000057 RID: 87
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c__6<T>
			{
				// Token: 0x0600023B RID: 571 RVA: 0x00006DDF File Offset: 0x00004FDF
				// Note: this type is marked as 'beforefieldinit'.
				static <>c__6()
				{
				}

				// Token: 0x0600023C RID: 572 RVA: 0x00002BAC File Offset: 0x00000DAC
				public <>c__6()
				{
				}

				// Token: 0x0600023D RID: 573 RVA: 0x000028FF File Offset: 0x00000AFF
				internal void <GetExportLifetimeContextFromExport>b__6_1()
				{
				}

				// Token: 0x040000EF RID: 239
				public static readonly ExportFactoryCreator.LifetimeContext.<>c__6<T> <>9 = new ExportFactoryCreator.LifetimeContext.<>c__6<T>();

				// Token: 0x040000F0 RID: 240
				public static Action <>9__6_1;
			}
		}

		// Token: 0x02000058 RID: 88
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x0600023E RID: 574 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x0600023F RID: 575 RVA: 0x00006DEB File Offset: 0x00004FEB
			internal object <CreateStronglyTypedExportFactoryFactory>b__0(Export e)
			{
				return this.exportFactoryFactory(e);
			}

			// Token: 0x040000F1 RID: 241
			public Func<Export, object> exportFactoryFactory;
		}

		// Token: 0x02000059 RID: 89
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0<T>
		{
			// Token: 0x06000240 RID: 576 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06000241 RID: 577 RVA: 0x00006DF9 File Offset: 0x00004FF9
			internal Tuple<T, Action> <CreateStronglyTypedExportFactoryOfT>b__0()
			{
				return this.lifetimeContext.GetExportLifetimeContextFromExport<T>(this.export);
			}

			// Token: 0x040000F2 RID: 242
			public ExportFactoryCreator.LifetimeContext lifetimeContext;

			// Token: 0x040000F3 RID: 243
			public Export export;
		}

		// Token: 0x0200005A RID: 90
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0<T, M>
		{
			// Token: 0x06000242 RID: 578 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x06000243 RID: 579 RVA: 0x00006E0C File Offset: 0x0000500C
			internal Tuple<T, Action> <CreateStronglyTypedExportFactoryOfTM>b__0()
			{
				return this.lifetimeContext.GetExportLifetimeContextFromExport<T>(this.export);
			}

			// Token: 0x040000F4 RID: 244
			public ExportFactoryCreator.LifetimeContext lifetimeContext;

			// Token: 0x040000F5 RID: 245
			public Export export;
		}
	}
}
