using System;

namespace Mono.CSharp
{
	// Token: 0x020002E6 RID: 742
	public class PointerContainer : ElementTypeSpec
	{
		// Token: 0x06002368 RID: 9064 RVA: 0x000AD02E File Offset: 0x000AB22E
		private PointerContainer(TypeSpec element) : base(MemberKind.PointerType, element, null)
		{
			this.state &= ~MemberSpec.StateFlags.CLSCompliant_Undetected;
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000AD04C File Offset: 0x000AB24C
		public override Type GetMetaInfo()
		{
			if (this.info == null)
			{
				this.info = base.Element.GetMetaInfo().MakePointerType();
			}
			return this.info;
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x00011055 File Offset: 0x0000F255
		protected override string GetPostfixSignature()
		{
			return "*";
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x000AD074 File Offset: 0x000AB274
		public static PointerContainer MakeType(ModuleContainer module, TypeSpec element)
		{
			PointerContainer pointerContainer;
			if (!module.PointerTypesCache.TryGetValue(element, out pointerContainer))
			{
				pointerContainer = new PointerContainer(element);
				module.PointerTypesCache.Add(element, pointerContainer);
			}
			return pointerContainer;
		}
	}
}
