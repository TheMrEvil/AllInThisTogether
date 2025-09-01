using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using System.Reflection;
using Microsoft.Internal;
using Microsoft.Internal.Collections;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000069 RID: 105
	internal class ImportingMember : ImportingItem
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000826D File Offset: 0x0000646D
		public ImportingMember(ContractBasedImportDefinition definition, ReflectionWritableMember member, ImportType importType) : base(definition, importType)
		{
			Assumes.NotNull<ContractBasedImportDefinition, ReflectionWritableMember>(definition, member);
			this._member = member;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00008285 File Offset: 0x00006485
		public void SetExportedValue(object instance, object value)
		{
			if (this.RequiresCollectionNormalization())
			{
				this.SetCollectionMemberValue(instance, (IEnumerable)value);
				return;
			}
			this.SetSingleMemberValue(instance, value);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x000082A5 File Offset: 0x000064A5
		private bool RequiresCollectionNormalization()
		{
			return base.Definition.Cardinality == ImportCardinality.ZeroOrMore && (!this._member.CanWrite || !base.ImportType.IsAssignableCollectionType);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000082D4 File Offset: 0x000064D4
		private void SetSingleMemberValue(object instance, object value)
		{
			this.EnsureWritable();
			try
			{
				this._member.SetValue(instance, value);
			}
			catch (TargetInvocationException ex)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportThrewException, this._member.GetDisplayName()), base.Definition.ToElement(), ex.InnerException);
			}
			catch (TargetParameterCountException ex2)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ImportNotValidOnIndexers, this._member.GetDisplayName()), base.Definition.ToElement(), ex2.InnerException);
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00008378 File Offset: 0x00006578
		private void EnsureWritable()
		{
			if (!this._member.CanWrite)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportNotWritable, this._member.GetDisplayName()), base.Definition.ToElement());
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000083B4 File Offset: 0x000065B4
		private void SetCollectionMemberValue(object instance, IEnumerable values)
		{
			Assumes.NotNull<IEnumerable>(values);
			ICollection<object> collection = null;
			Type collectionElementType = CollectionServices.GetCollectionElementType(base.ImportType.ActualType);
			if (collectionElementType != null)
			{
				collection = this.GetNormalizedCollection(collectionElementType, instance);
			}
			this.EnsureCollectionIsWritable(collection);
			this.PopulateCollection(collection, values);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000083FC File Offset: 0x000065FC
		private ICollection<object> GetNormalizedCollection(Type itemType, object instance)
		{
			Assumes.NotNull<Type>(itemType);
			object obj = null;
			if (this._member.CanRead)
			{
				try
				{
					obj = this._member.GetValue(instance);
				}
				catch (TargetInvocationException ex)
				{
					throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportCollectionGetThrewException, this._member.GetDisplayName()), base.Definition.ToElement(), ex.InnerException);
				}
			}
			if (obj == null)
			{
				ConstructorInfo constructor = base.ImportType.ActualType.GetConstructor(Type.EmptyTypes);
				if (constructor != null)
				{
					try
					{
						obj = constructor.SafeInvoke(Array.Empty<object>());
					}
					catch (TargetInvocationException ex2)
					{
						throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportCollectionConstructionThrewException, this._member.GetDisplayName(), base.ImportType.ActualType.FullName), base.Definition.ToElement(), ex2.InnerException);
					}
					this.SetSingleMemberValue(instance, obj);
				}
			}
			if (obj == null)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportCollectionNull, this._member.GetDisplayName()), base.Definition.ToElement());
			}
			return CollectionServices.GetCollectionWrapper(itemType, obj);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000852C File Offset: 0x0000672C
		private void EnsureCollectionIsWritable(ICollection<object> collection)
		{
			bool flag = true;
			try
			{
				if (collection != null)
				{
					flag = collection.IsReadOnly;
				}
			}
			catch (Exception innerException)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportCollectionIsReadOnlyThrewException, this._member.GetDisplayName(), collection.GetType().FullName), base.Definition.ToElement(), innerException);
			}
			if (flag)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportCollectionNotWritable, this._member.GetDisplayName()), base.Definition.ToElement());
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000085C0 File Offset: 0x000067C0
		private void PopulateCollection(ICollection<object> collection, IEnumerable values)
		{
			Assumes.NotNull<ICollection<object>, IEnumerable>(collection, values);
			try
			{
				collection.Clear();
			}
			catch (Exception innerException)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportCollectionClearThrewException, this._member.GetDisplayName(), collection.GetType().FullName), base.Definition.ToElement(), innerException);
			}
			foreach (object item in values)
			{
				try
				{
					collection.Add(item);
				}
				catch (Exception innerException2)
				{
					throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportCollectionAddThrewException, this._member.GetDisplayName(), collection.GetType().FullName), base.Definition.ToElement(), innerException2);
				}
			}
		}

		// Token: 0x04000123 RID: 291
		private readonly ReflectionWritableMember _member;
	}
}
