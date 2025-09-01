using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200011D RID: 285
	public class BaseFieldTraits<TValueType, TValueUxmlAttributeType> : BaseField<TValueType>.UxmlTraits where TValueUxmlAttributeType : TypedUxmlAttributeDescription<TValueType>, new()
	{
		// Token: 0x06000952 RID: 2386 RVA: 0x00025095 File Offset: 0x00023295
		public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
		{
			base.Init(ve, bag, cc);
			((INotifyValueChanged<TValueType>)ve).SetValueWithoutNotify(this.m_Value.GetValueFromBag(bag, cc));
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x000250C0 File Offset: 0x000232C0
		public BaseFieldTraits()
		{
			TValueUxmlAttributeType tvalueUxmlAttributeType = Activator.CreateInstance<TValueUxmlAttributeType>();
			tvalueUxmlAttributeType.name = "value";
			this.m_Value = tvalueUxmlAttributeType;
			base..ctor();
		}

		// Token: 0x040003E7 RID: 999
		private TValueUxmlAttributeType m_Value;
	}
}
