using System;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using System.Reflection;
using System.Threading;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x0200005B RID: 91
	internal class ExportingMember
	{
		// Token: 0x06000244 RID: 580 RVA: 0x00006E1F File Offset: 0x0000501F
		public ExportingMember(ExportDefinition definition, ReflectionMember member)
		{
			Assumes.NotNull<ExportDefinition, ReflectionMember>(definition, member);
			this._definition = definition;
			this._member = member;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00006E3C File Offset: 0x0000503C
		public bool RequiresInstance
		{
			get
			{
				return this._member.RequiresInstance;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00006E49 File Offset: 0x00005049
		public ExportDefinition Definition
		{
			get
			{
				return this._definition;
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00006E54 File Offset: 0x00005054
		public object GetExportedValue(object instance, object @lock)
		{
			this.EnsureReadable();
			if (!this._isValueCached)
			{
				object value;
				try
				{
					value = this._member.GetValue(instance);
				}
				catch (TargetInvocationException ex)
				{
					throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ExportThrewException, this._member.GetDisplayName()), this.Definition.ToElement(), ex.InnerException);
				}
				catch (TargetParameterCountException ex2)
				{
					throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ExportNotValidOnIndexers, this._member.GetDisplayName()), this.Definition.ToElement(), ex2.InnerException);
				}
				lock (@lock)
				{
					if (!this._isValueCached)
					{
						this._cachedValue = value;
						Thread.MemoryBarrier();
						this._isValueCached = true;
					}
				}
			}
			return this._cachedValue;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00006F50 File Offset: 0x00005150
		private void EnsureReadable()
		{
			if (!this._member.CanRead)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ExportNotReadable, this._member.GetDisplayName()), this.Definition.ToElement());
			}
		}

		// Token: 0x040000F6 RID: 246
		private readonly ExportDefinition _definition;

		// Token: 0x040000F7 RID: 247
		private readonly ReflectionMember _member;

		// Token: 0x040000F8 RID: 248
		private object _cachedValue;

		// Token: 0x040000F9 RID: 249
		private volatile bool _isValueCached;
	}
}
