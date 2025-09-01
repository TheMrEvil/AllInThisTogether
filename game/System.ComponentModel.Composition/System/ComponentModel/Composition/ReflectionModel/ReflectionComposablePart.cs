using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Internal;
using Microsoft.Internal.Collections;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000070 RID: 112
	internal class ReflectionComposablePart : ComposablePart, ICompositionElement
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x00008C54 File Offset: 0x00006E54
		public ReflectionComposablePart(ReflectionComposablePartDefinition definition)
		{
			Requires.NotNull<ReflectionComposablePartDefinition>(definition, "definition");
			this._definition = definition;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00008CAC File Offset: 0x00006EAC
		public ReflectionComposablePart(ReflectionComposablePartDefinition definition, object attributedPart)
		{
			Requires.NotNull<ReflectionComposablePartDefinition>(definition, "definition");
			Requires.NotNull<object>(attributedPart, "attributedPart");
			this._definition = definition;
			if (attributedPart is ValueType)
			{
				throw new ArgumentException(Strings.ArgumentValueType, "attributedPart");
			}
			this._cachedInstance = attributedPart;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000028FF File Offset: 0x00000AFF
		protected virtual void EnsureRunning()
		{
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00008D30 File Offset: 0x00006F30
		protected void RequiresRunning()
		{
			this.EnsureRunning();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000028FF File Offset: 0x00000AFF
		protected virtual void ReleaseInstanceIfNecessary(object instance)
		{
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00008D38 File Offset: 0x00006F38
		protected object CachedInstance
		{
			get
			{
				object @lock = this._lock;
				object cachedInstance;
				lock (@lock)
				{
					cachedInstance = this._cachedInstance;
				}
				return cachedInstance;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00008D7C File Offset: 0x00006F7C
		public ReflectionComposablePartDefinition Definition
		{
			get
			{
				this.RequiresRunning();
				return this._definition;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00008D8A File Offset: 0x00006F8A
		public override IDictionary<string, object> Metadata
		{
			get
			{
				this.RequiresRunning();
				return this.Definition.Metadata;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00008D9D File Offset: 0x00006F9D
		public sealed override IEnumerable<ImportDefinition> ImportDefinitions
		{
			get
			{
				this.RequiresRunning();
				return this.Definition.ImportDefinitions;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00008DB0 File Offset: 0x00006FB0
		public sealed override IEnumerable<ExportDefinition> ExportDefinitions
		{
			get
			{
				this.RequiresRunning();
				return this.Definition.ExportDefinitions;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00008DC3 File Offset: 0x00006FC3
		string ICompositionElement.DisplayName
		{
			get
			{
				return this.GetDisplayName();
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00008DCB File Offset: 0x00006FCB
		ICompositionElement ICompositionElement.Origin
		{
			get
			{
				return this.Definition;
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00008DD4 File Offset: 0x00006FD4
		public override object GetExportedValue(ExportDefinition definition)
		{
			this.RequiresRunning();
			Requires.NotNull<ExportDefinition>(definition, "definition");
			ExportingMember exportingMember = null;
			object @lock = this._lock;
			lock (@lock)
			{
				exportingMember = this.GetExportingMemberFromDefinition(definition);
				if (exportingMember == null)
				{
					throw ExceptionBuilder.CreateExportDefinitionNotOnThisComposablePart("definition");
				}
				this.EnsureGettable();
			}
			return this.GetExportedValue(exportingMember);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00008E44 File Offset: 0x00007044
		public override void SetImport(ImportDefinition definition, IEnumerable<Export> exports)
		{
			this.RequiresRunning();
			Requires.NotNull<ImportDefinition>(definition, "definition");
			Requires.NotNull<IEnumerable<Export>>(exports, "exports");
			ImportingItem importingItemFromDefinition = this.GetImportingItemFromDefinition(definition);
			if (importingItemFromDefinition == null)
			{
				throw ExceptionBuilder.CreateImportDefinitionNotOnThisComposablePart("definition");
			}
			this.EnsureSettable(definition);
			Export[] exports2 = exports.AsArray<Export>();
			ReflectionComposablePart.EnsureCardinality(definition, exports2);
			this.SetImport(importingItemFromDefinition, exports2);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00008EA0 File Offset: 0x000070A0
		public override void Activate()
		{
			this.RequiresRunning();
			this.SetNonPrerequisiteImports();
			this.NotifyImportSatisfied();
			object @lock = this._lock;
			lock (@lock)
			{
				this._initialCompositionComplete = true;
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00008DC3 File Offset: 0x00006FC3
		public override string ToString()
		{
			return this.GetDisplayName();
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00008EF4 File Offset: 0x000070F4
		private object GetExportedValue(ExportingMember member)
		{
			object instance = null;
			if (member.RequiresInstance)
			{
				instance = this.GetInstanceActivatingIfNeeded();
			}
			return member.GetExportedValue(instance, this._lock);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00008F20 File Offset: 0x00007120
		private void SetImport(ImportingItem item, Export[] exports)
		{
			object value = item.CastExportsToImportType(exports);
			object @lock = this._lock;
			lock (@lock)
			{
				this._invokeImportsSatisfied = true;
				this._importValues[item.Definition] = value;
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00008F7C File Offset: 0x0000717C
		private object GetInstanceActivatingIfNeeded()
		{
			if (this._cachedInstance != null)
			{
				return this._cachedInstance;
			}
			ConstructorInfo constructorInfo = null;
			object[] arguments = null;
			object @lock = this._lock;
			lock (@lock)
			{
				if (!this.RequiresActivation())
				{
					return null;
				}
				constructorInfo = this.Definition.GetConstructor();
				if (constructorInfo == null)
				{
					throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_PartConstructorMissing, this.Definition.GetPartType().FullName), this.Definition.ToElement());
				}
				arguments = this.GetConstructorArguments();
			}
			object obj = this.CreateInstance(constructorInfo, arguments);
			this.SetPrerequisiteImports();
			@lock = this._lock;
			lock (@lock)
			{
				if (this._cachedInstance == null)
				{
					this._cachedInstance = obj;
					obj = null;
				}
			}
			if (obj == null)
			{
				this.ReleaseInstanceIfNecessary(obj);
			}
			return this._cachedInstance;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00009090 File Offset: 0x00007290
		private object[] GetConstructorArguments()
		{
			ReflectionParameterImportDefinition[] array = this.ImportDefinitions.OfType<ReflectionParameterImportDefinition>().ToArray<ReflectionParameterImportDefinition>();
			object[] arguments = new object[array.Length];
			this.UseImportedValues<ReflectionParameterImportDefinition>(array, delegate(ImportingItem import, ReflectionParameterImportDefinition definition, object value)
			{
				if (definition.Cardinality == ImportCardinality.ZeroOrMore && !import.ImportType.IsAssignableCollectionType)
				{
					throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportManyOnParameterCanOnlyBeAssigned, this.Definition.GetPartType().FullName, definition.ImportingLazyParameter.Value.Name), this.Definition.ToElement());
				}
				arguments[definition.ImportingLazyParameter.Value.Position] = value;
			}, true);
			return arguments;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000090E3 File Offset: 0x000072E3
		private bool RequiresActivation()
		{
			return this.ImportDefinitions.Any<ImportDefinition>() || this.ExportDefinitions.Any((ExportDefinition definition) => this.GetExportingMemberFromDefinition(definition).RequiresInstance);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000910C File Offset: 0x0000730C
		private void EnsureGettable()
		{
			if (this._initialCompositionComplete)
			{
				return;
			}
			foreach (ImportDefinition importDefinition in from definition in this.ImportDefinitions
			where definition.IsPrerequisite
			select definition)
			{
				if (!this._importValues.ContainsKey(importDefinition))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.InvalidOperation_GetExportedValueBeforePrereqImportSet, importDefinition.ToElement().DisplayName));
				}
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000091B0 File Offset: 0x000073B0
		private void EnsureSettable(ImportDefinition definition)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._initialCompositionComplete && !definition.IsRecomposable)
				{
					throw new InvalidOperationException(Strings.InvalidOperation_DefinitionCannotBeRecomposed);
				}
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00009208 File Offset: 0x00007408
		private static void EnsureCardinality(ImportDefinition definition, Export[] exports)
		{
			Requires.NullOrNotNullElements<Export>(exports, "exports");
			ExportCardinalityCheckResult exportCardinalityCheckResult = ExportServices.CheckCardinality<Export>(definition, exports);
			if (exportCardinalityCheckResult == ExportCardinalityCheckResult.NoExports)
			{
				throw new ArgumentException(Strings.Argument_ExportsEmpty, "exports");
			}
			if (exportCardinalityCheckResult != ExportCardinalityCheckResult.TooManyExports)
			{
				Assumes.IsTrue(exportCardinalityCheckResult == ExportCardinalityCheckResult.Match);
				return;
			}
			throw new ArgumentException(Strings.Argument_ExportsTooMany, "exports");
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000925C File Offset: 0x0000745C
		private object CreateInstance(ConstructorInfo constructor, object[] arguments)
		{
			Exception ex = null;
			object result = null;
			try
			{
				result = constructor.SafeInvoke(arguments);
			}
			catch (TypeInitializationException ex)
			{
			}
			catch (TargetInvocationException ex2)
			{
				ex = ex2.InnerException;
			}
			if (ex != null)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_PartConstructorThrewException, this.Definition.GetPartType().FullName), this.Definition.ToElement(), ex);
			}
			return result;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000092D4 File Offset: 0x000074D4
		private void SetNonPrerequisiteImports()
		{
			IEnumerable<ImportDefinition> definitions = from import in this.ImportDefinitions
			where !import.IsPrerequisite
			select import;
			this.UseImportedValues<ImportDefinition>(definitions, new Action<ImportingItem, ImportDefinition, object>(this.SetExportedValueForImport), false);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009320 File Offset: 0x00007520
		private void SetPrerequisiteImports()
		{
			IEnumerable<ImportDefinition> definitions = from import in this.ImportDefinitions
			where import.IsPrerequisite
			select import;
			this.UseImportedValues<ImportDefinition>(definitions, new Action<ImportingItem, ImportDefinition, object>(this.SetExportedValueForImport), false);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000936C File Offset: 0x0000756C
		private void SetExportedValueForImport(ImportingItem import, ImportDefinition definition, object value)
		{
			ImportingMember importingMember = (ImportingMember)import;
			object instanceActivatingIfNeeded = this.GetInstanceActivatingIfNeeded();
			importingMember.SetExportedValue(instanceActivatingIfNeeded, value);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00009390 File Offset: 0x00007590
		private void UseImportedValues<TImportDefinition>(IEnumerable<TImportDefinition> definitions, Action<ImportingItem, TImportDefinition, object> useImportValue, bool errorIfMissing) where TImportDefinition : ImportDefinition
		{
			CompositionResult compositionResult = CompositionResult.SucceededResult;
			foreach (TImportDefinition timportDefinition in definitions)
			{
				ImportingItem importingItemFromDefinition = this.GetImportingItemFromDefinition(timportDefinition);
				object arg;
				if (!this.TryGetImportValue(timportDefinition, out arg))
				{
					if (!errorIfMissing)
					{
						continue;
					}
					if (timportDefinition.Cardinality == ImportCardinality.ExactlyOne)
					{
						CompositionError error = CompositionError.Create(CompositionErrorId.ImportNotSetOnPart, Strings.ImportNotSetOnPart, new object[]
						{
							this.Definition.GetPartType().FullName,
							timportDefinition.ToString()
						});
						compositionResult = compositionResult.MergeError(error);
						continue;
					}
					arg = importingItemFromDefinition.CastExportsToImportType(new Export[0]);
				}
				useImportValue(importingItemFromDefinition, timportDefinition, arg);
			}
			compositionResult.ThrowOnErrors();
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000946C File Offset: 0x0000766C
		private bool TryGetImportValue(ImportDefinition definition, out object value)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._importValues.TryGetValue(definition, out value))
				{
					this._importValues.Remove(definition);
					return true;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000094CC File Offset: 0x000076CC
		private void NotifyImportSatisfied()
		{
			if (this._invokeImportsSatisfied && !this._invokingImportsSatisfied)
			{
				IPartImportsSatisfiedNotification partImportsSatisfiedNotification = this.GetInstanceActivatingIfNeeded() as IPartImportsSatisfiedNotification;
				if (partImportsSatisfiedNotification != null)
				{
					try
					{
						this._invokingImportsSatisfied = true;
						partImportsSatisfiedNotification.OnImportsSatisfied();
					}
					catch (Exception innerException)
					{
						throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_PartOnImportsSatisfiedThrewException, this.Definition.GetPartType().FullName), this.Definition.ToElement(), innerException);
					}
					finally
					{
						this._invokingImportsSatisfied = false;
					}
					this._invokeImportsSatisfied = false;
				}
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00009564 File Offset: 0x00007764
		private ExportingMember GetExportingMemberFromDefinition(ExportDefinition definition)
		{
			ReflectionMemberExportDefinition reflectionMemberExportDefinition = definition as ReflectionMemberExportDefinition;
			if (reflectionMemberExportDefinition == null)
			{
				return null;
			}
			int index = reflectionMemberExportDefinition.GetIndex();
			ExportingMember exportingMember;
			if (!this._exportsCache.TryGetValue(index, out exportingMember))
			{
				exportingMember = ReflectionComposablePart.GetExportingMember(definition);
				if (exportingMember != null)
				{
					this._exportsCache[index] = exportingMember;
				}
			}
			return exportingMember;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000095AC File Offset: 0x000077AC
		private ImportingItem GetImportingItemFromDefinition(ImportDefinition definition)
		{
			ImportingItem importingItem;
			if (!this._importsCache.TryGetValue(definition, out importingItem))
			{
				importingItem = ReflectionComposablePart.GetImportingItem(definition);
				if (importingItem != null)
				{
					this._importsCache[definition] = importingItem;
				}
			}
			return importingItem;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000095E4 File Offset: 0x000077E4
		private static ImportingItem GetImportingItem(ImportDefinition definition)
		{
			ReflectionImportDefinition reflectionImportDefinition = definition as ReflectionImportDefinition;
			if (reflectionImportDefinition != null)
			{
				return reflectionImportDefinition.ToImportingItem();
			}
			return null;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00009604 File Offset: 0x00007804
		private static ExportingMember GetExportingMember(ExportDefinition definition)
		{
			ReflectionMemberExportDefinition reflectionMemberExportDefinition = definition as ReflectionMemberExportDefinition;
			if (reflectionMemberExportDefinition != null)
			{
				return reflectionMemberExportDefinition.ToExportingMember();
			}
			return null;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00009623 File Offset: 0x00007823
		private string GetDisplayName()
		{
			return this._definition.GetPartType().GetDisplayName();
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00009635 File Offset: 0x00007835
		[CompilerGenerated]
		private bool <RequiresActivation>b__36_0(ExportDefinition definition)
		{
			return this.GetExportingMemberFromDefinition(definition).RequiresInstance;
		}

		// Token: 0x0400012F RID: 303
		private readonly ReflectionComposablePartDefinition _definition;

		// Token: 0x04000130 RID: 304
		private readonly Dictionary<ImportDefinition, object> _importValues = new Dictionary<ImportDefinition, object>();

		// Token: 0x04000131 RID: 305
		private readonly Dictionary<ImportDefinition, ImportingItem> _importsCache = new Dictionary<ImportDefinition, ImportingItem>();

		// Token: 0x04000132 RID: 306
		private readonly Dictionary<int, ExportingMember> _exportsCache = new Dictionary<int, ExportingMember>();

		// Token: 0x04000133 RID: 307
		private bool _invokeImportsSatisfied = true;

		// Token: 0x04000134 RID: 308
		private bool _invokingImportsSatisfied;

		// Token: 0x04000135 RID: 309
		private bool _initialCompositionComplete;

		// Token: 0x04000136 RID: 310
		private volatile object _cachedInstance;

		// Token: 0x04000137 RID: 311
		private object _lock = new object();

		// Token: 0x02000071 RID: 113
		[CompilerGenerated]
		private sealed class <>c__DisplayClass35_0
		{
			// Token: 0x060002ED RID: 749 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass35_0()
			{
			}

			// Token: 0x060002EE RID: 750 RVA: 0x00009644 File Offset: 0x00007844
			internal void <GetConstructorArguments>b__0(ImportingItem import, ReflectionParameterImportDefinition definition, object value)
			{
				if (definition.Cardinality == ImportCardinality.ZeroOrMore && !import.ImportType.IsAssignableCollectionType)
				{
					throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportManyOnParameterCanOnlyBeAssigned, this.<>4__this.Definition.GetPartType().FullName, definition.ImportingLazyParameter.Value.Name), this.<>4__this.Definition.ToElement());
				}
				this.arguments[definition.ImportingLazyParameter.Value.Position] = value;
			}

			// Token: 0x04000138 RID: 312
			public ReflectionComposablePart <>4__this;

			// Token: 0x04000139 RID: 313
			public object[] arguments;
		}

		// Token: 0x02000072 RID: 114
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060002EF RID: 751 RVA: 0x000096C9 File Offset: 0x000078C9
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060002F0 RID: 752 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c()
			{
			}

			// Token: 0x060002F1 RID: 753 RVA: 0x000096D5 File Offset: 0x000078D5
			internal bool <EnsureGettable>b__37_0(ImportDefinition definition)
			{
				return definition.IsPrerequisite;
			}

			// Token: 0x060002F2 RID: 754 RVA: 0x000096DD File Offset: 0x000078DD
			internal bool <SetNonPrerequisiteImports>b__41_0(ImportDefinition import)
			{
				return !import.IsPrerequisite;
			}

			// Token: 0x060002F3 RID: 755 RVA: 0x000096D5 File Offset: 0x000078D5
			internal bool <SetPrerequisiteImports>b__42_0(ImportDefinition import)
			{
				return import.IsPrerequisite;
			}

			// Token: 0x0400013A RID: 314
			public static readonly ReflectionComposablePart.<>c <>9 = new ReflectionComposablePart.<>c();

			// Token: 0x0400013B RID: 315
			public static Func<ImportDefinition, bool> <>9__37_0;

			// Token: 0x0400013C RID: 316
			public static Func<ImportDefinition, bool> <>9__41_0;

			// Token: 0x0400013D RID: 317
			public static Func<ImportDefinition, bool> <>9__42_0;
		}
	}
}
