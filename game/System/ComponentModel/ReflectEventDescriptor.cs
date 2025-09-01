using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x020003E6 RID: 998
	internal sealed class ReflectEventDescriptor : EventDescriptor
	{
		// Token: 0x060020BD RID: 8381 RVA: 0x00071014 File Offset: 0x0006F214
		public ReflectEventDescriptor(Type componentClass, string name, Type type, Attribute[] attributes) : base(name, attributes)
		{
			if (componentClass == null)
			{
				throw new ArgumentException(SR.Format("Null is not a valid value for {0}.", "componentClass"));
			}
			if (type == null || !typeof(Delegate).IsAssignableFrom(type))
			{
				throw new ArgumentException(SR.Format("Invalid type for the {0} event.", name));
			}
			this._componentClass = componentClass;
			this._type = type;
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x00071082 File Offset: 0x0006F282
		public ReflectEventDescriptor(Type componentClass, EventInfo eventInfo) : base(eventInfo.Name, Array.Empty<Attribute>())
		{
			if (componentClass == null)
			{
				throw new ArgumentException(SR.Format("Null is not a valid value for {0}.", "componentClass"));
			}
			this._componentClass = componentClass;
			this._realEvent = eventInfo;
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x000710C4 File Offset: 0x0006F2C4
		public ReflectEventDescriptor(Type componentType, EventDescriptor oldReflectEventDescriptor, Attribute[] attributes) : base(oldReflectEventDescriptor, attributes)
		{
			this._componentClass = componentType;
			this._type = oldReflectEventDescriptor.EventType;
			ReflectEventDescriptor reflectEventDescriptor = oldReflectEventDescriptor as ReflectEventDescriptor;
			if (reflectEventDescriptor != null)
			{
				this._addMethod = reflectEventDescriptor._addMethod;
				this._removeMethod = reflectEventDescriptor._removeMethod;
				this._filledMethods = true;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060020C0 RID: 8384 RVA: 0x00071115 File Offset: 0x0006F315
		public override Type ComponentType
		{
			get
			{
				return this._componentClass;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x0007111D File Offset: 0x0006F31D
		public override Type EventType
		{
			get
			{
				this.FillMethods();
				return this._type;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060020C2 RID: 8386 RVA: 0x0007112B File Offset: 0x0006F32B
		public override bool IsMulticast
		{
			get
			{
				return typeof(MulticastDelegate).IsAssignableFrom(this.EventType);
			}
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x00071144 File Offset: 0x0006F344
		public override void AddEventHandler(object component, Delegate value)
		{
			this.FillMethods();
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					try
					{
						componentChangeService.OnComponentChanging(component, this);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
					componentChangeService.OnComponentChanging(component, this);
				}
				bool flag = false;
				if (site != null && site.DesignMode)
				{
					if (this.EventType != value.GetType())
					{
						throw new ArgumentException(SR.Format("Invalid event handler for the {0} event.", this.Name));
					}
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						Delegate @delegate = (Delegate)dictionaryService.GetValue(this);
						@delegate = Delegate.Combine(@delegate, value);
						dictionaryService.SetValue(this, @delegate);
						flag = true;
					}
				}
				if (!flag)
				{
					MethodBase addMethod = this._addMethod;
					object[] parameters = new Delegate[]
					{
						value
					};
					addMethod.Invoke(component, parameters);
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(component, this, null, value);
				}
			}
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x00071258 File Offset: 0x0006F458
		protected override void FillAttributes(IList attributes)
		{
			this.FillMethods();
			if (this._realEvent != null)
			{
				this.FillEventInfoAttribute(this._realEvent, attributes);
			}
			else
			{
				this.FillSingleMethodAttribute(this._removeMethod, attributes);
				this.FillSingleMethodAttribute(this._addMethod, attributes);
			}
			base.FillAttributes(attributes);
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000712AC File Offset: 0x0006F4AC
		private void FillEventInfoAttribute(EventInfo realEventInfo, IList attributes)
		{
			string name = realEventInfo.Name;
			BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
			Type type = realEventInfo.ReflectedType;
			int num = 0;
			while (type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = realEventInfo.ReflectedType;
				Attribute[][] array = new Attribute[num][];
				while (type != typeof(object))
				{
					MemberInfo @event = type.GetEvent(name, bindingAttr);
					if (@event != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(@event);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						foreach (Attribute value in array3)
						{
							attributes.Add(value);
						}
					}
				}
			}
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x00071388 File Offset: 0x0006F588
		private void FillMethods()
		{
			if (this._filledMethods)
			{
				return;
			}
			if (this._realEvent != null)
			{
				this._addMethod = this._realEvent.GetAddMethod();
				this._removeMethod = this._realEvent.GetRemoveMethod();
				EventInfo eventInfo = null;
				if (this._addMethod == null || this._removeMethod == null)
				{
					Type baseType = this._componentClass.BaseType;
					while (baseType != null && baseType != typeof(object))
					{
						BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
						EventInfo @event = baseType.GetEvent(this._realEvent.Name, bindingAttr);
						if (@event.GetAddMethod() != null)
						{
							eventInfo = @event;
							break;
						}
					}
				}
				if (eventInfo != null)
				{
					this._addMethod = eventInfo.GetAddMethod();
					this._removeMethod = eventInfo.GetRemoveMethod();
					this._type = eventInfo.EventHandlerType;
				}
				else
				{
					this._type = this._realEvent.EventHandlerType;
				}
			}
			else
			{
				this._realEvent = this._componentClass.GetEvent(this.Name);
				if (this._realEvent != null)
				{
					this.FillMethods();
					return;
				}
				Type[] args = new Type[]
				{
					this._type
				};
				this._addMethod = MemberDescriptor.FindMethod(this._componentClass, "AddOn" + this.Name, args, typeof(void));
				this._removeMethod = MemberDescriptor.FindMethod(this._componentClass, "RemoveOn" + this.Name, args, typeof(void));
				if (this._addMethod == null || this._removeMethod == null)
				{
					throw new ArgumentException(SR.Format("Accessor methods for the {0} event are missing.", this.Name));
				}
			}
			this._filledMethods = true;
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x00071558 File Offset: 0x0006F758
		private void FillSingleMethodAttribute(MethodInfo realMethodInfo, IList attributes)
		{
			string name = realMethodInfo.Name;
			BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
			Type type = realMethodInfo.ReflectedType;
			int num = 0;
			while (type != null && type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = realMethodInfo.ReflectedType;
				Attribute[][] array = new Attribute[num][];
				while (type != null && type != typeof(object))
				{
					MemberInfo method = type.GetMethod(name, bindingAttr);
					if (method != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(method);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						foreach (Attribute value in array3)
						{
							attributes.Add(value);
						}
					}
				}
			}
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x00071648 File Offset: 0x0006F848
		public override void RemoveEventHandler(object component, Delegate value)
		{
			this.FillMethods();
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					try
					{
						componentChangeService.OnComponentChanging(component, this);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
					componentChangeService.OnComponentChanging(component, this);
				}
				bool flag = false;
				if (site != null && site.DesignMode)
				{
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						Delegate @delegate = (Delegate)dictionaryService.GetValue(this);
						@delegate = Delegate.Remove(@delegate, value);
						dictionaryService.SetValue(this, @delegate);
						flag = true;
					}
				}
				if (!flag)
				{
					MethodBase removeMethod = this._removeMethod;
					object[] parameters = new Delegate[]
					{
						value
					};
					removeMethod.Invoke(component, parameters);
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(component, this, null, value);
				}
			}
		}

		// Token: 0x04000FDA RID: 4058
		private Type _type;

		// Token: 0x04000FDB RID: 4059
		private readonly Type _componentClass;

		// Token: 0x04000FDC RID: 4060
		private MethodInfo _addMethod;

		// Token: 0x04000FDD RID: 4061
		private MethodInfo _removeMethod;

		// Token: 0x04000FDE RID: 4062
		private EventInfo _realEvent;

		// Token: 0x04000FDF RID: 4063
		private bool _filledMethods;
	}
}
