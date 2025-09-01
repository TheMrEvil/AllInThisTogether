using System;

namespace Mono.CSharp
{
	// Token: 0x020002E5 RID: 741
	public class ReferenceContainer : ElementTypeSpec
	{
		// Token: 0x06002365 RID: 9061 RVA: 0x000ACFC6 File Offset: 0x000AB1C6
		private ReferenceContainer(TypeSpec element) : base(MemberKind.Class, element, null)
		{
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x000ACFD5 File Offset: 0x000AB1D5
		public override Type GetMetaInfo()
		{
			if (this.info == null)
			{
				this.info = base.Element.GetMetaInfo().MakeByRefType();
			}
			return this.info;
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000ACFFC File Offset: 0x000AB1FC
		public static ReferenceContainer MakeType(ModuleContainer module, TypeSpec element)
		{
			ReferenceContainer referenceContainer;
			if (!module.ReferenceTypesCache.TryGetValue(element, out referenceContainer))
			{
				referenceContainer = new ReferenceContainer(element);
				module.ReferenceTypesCache.Add(element, referenceContainer);
			}
			return referenceContainer;
		}
	}
}
