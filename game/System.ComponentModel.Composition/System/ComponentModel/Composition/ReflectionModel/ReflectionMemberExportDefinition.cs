using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x0200007B RID: 123
	internal class ReflectionMemberExportDefinition : ExportDefinition, ICompositionElement
	{
		// Token: 0x06000336 RID: 822 RVA: 0x00009F1B File Offset: 0x0000811B
		public ReflectionMemberExportDefinition(LazyMemberInfo member, ExportDefinition exportDefinition, ICompositionElement origin)
		{
			Assumes.NotNull<ExportDefinition>(exportDefinition);
			this._member = member;
			this._exportDefinition = exportDefinition;
			this._origin = origin;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00009F3E File Offset: 0x0000813E
		public override string ContractName
		{
			get
			{
				return this._exportDefinition.ContractName;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00009F4B File Offset: 0x0000814B
		public LazyMemberInfo ExportingLazyMember
		{
			get
			{
				return this._member;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00009F53 File Offset: 0x00008153
		public override IDictionary<string, object> Metadata
		{
			get
			{
				if (this._metadata == null)
				{
					this._metadata = this._exportDefinition.Metadata.AsReadOnly();
				}
				return this._metadata;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00009F79 File Offset: 0x00008179
		string ICompositionElement.DisplayName
		{
			get
			{
				return this.GetDisplayName();
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00009F81 File Offset: 0x00008181
		ICompositionElement ICompositionElement.Origin
		{
			get
			{
				return this._origin;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00009F79 File Offset: 0x00008179
		public override string ToString()
		{
			return this.GetDisplayName();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00009F89 File Offset: 0x00008189
		public int GetIndex()
		{
			return this.ExportingLazyMember.ToReflectionMember().UnderlyingMember.MetadataToken;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00009FA0 File Offset: 0x000081A0
		public ExportingMember ToExportingMember()
		{
			return new ExportingMember(this, this.ToReflectionMember());
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00009FAE File Offset: 0x000081AE
		private ReflectionMember ToReflectionMember()
		{
			return this.ExportingLazyMember.ToReflectionMember();
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00009FBB File Offset: 0x000081BB
		private string GetDisplayName()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} (ContractName=\"{1}\")", this.ToReflectionMember().GetDisplayName(), this.ContractName);
		}

		// Token: 0x04000153 RID: 339
		private readonly LazyMemberInfo _member;

		// Token: 0x04000154 RID: 340
		private readonly ExportDefinition _exportDefinition;

		// Token: 0x04000155 RID: 341
		private readonly ICompositionElement _origin;

		// Token: 0x04000156 RID: 342
		private IDictionary<string, object> _metadata;
	}
}
