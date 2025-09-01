using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200041C RID: 1052
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class ReflectPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060021F5 RID: 8693 RVA: 0x000743D8 File Offset: 0x000725D8
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, Attribute[] attributes) : base(name, attributes)
		{
			try
			{
				if (type == null)
				{
					throw new ArgumentException(SR.GetString("Invalid type for the {0} property.", new object[]
					{
						name
					}));
				}
				if (componentClass == null)
				{
					throw new ArgumentException(SR.GetString("Null is not a valid value for {0}.", new object[]
					{
						"componentClass"
					}));
				}
				this.type = type;
				this.componentClass = componentClass;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x0007445C File Offset: 0x0007265C
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, PropertyInfo propInfo, MethodInfo getMethod, MethodInfo setMethod, Attribute[] attrs) : this(componentClass, name, type, attrs)
		{
			this.propInfo = propInfo;
			this.getMethod = getMethod;
			this.setMethod = setMethod;
			if (getMethod != null && propInfo != null && setMethod == null)
			{
				this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetOnDemand] = true;
				return;
			}
			this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetQueried] = true;
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000744D9 File Offset: 0x000726D9
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, Type receiverType, MethodInfo getMethod, MethodInfo setMethod, Attribute[] attrs) : this(componentClass, name, type, attrs)
		{
			this.receiverType = receiverType;
			this.getMethod = getMethod;
			this.setMethod = setMethod;
			this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetQueried] = true;
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x00074518 File Offset: 0x00072718
		public ReflectPropertyDescriptor(Type componentClass, PropertyDescriptor oldReflectPropertyDescriptor, Attribute[] attributes) : base(oldReflectPropertyDescriptor, attributes)
		{
			this.componentClass = componentClass;
			this.type = oldReflectPropertyDescriptor.PropertyType;
			if (componentClass == null)
			{
				throw new ArgumentException(SR.GetString("Null is not a valid value for {0}.", new object[]
				{
					"componentClass"
				}));
			}
			ReflectPropertyDescriptor reflectPropertyDescriptor = oldReflectPropertyDescriptor as ReflectPropertyDescriptor;
			if (reflectPropertyDescriptor != null)
			{
				if (reflectPropertyDescriptor.ComponentType == componentClass)
				{
					this.propInfo = reflectPropertyDescriptor.propInfo;
					this.getMethod = reflectPropertyDescriptor.getMethod;
					this.setMethod = reflectPropertyDescriptor.setMethod;
					this.shouldSerializeMethod = reflectPropertyDescriptor.shouldSerializeMethod;
					this.resetMethod = reflectPropertyDescriptor.resetMethod;
					this.defaultValue = reflectPropertyDescriptor.defaultValue;
					this.ambientValue = reflectPropertyDescriptor.ambientValue;
					this.state = reflectPropertyDescriptor.state;
				}
				if (attributes != null)
				{
					foreach (Attribute attribute in attributes)
					{
						DefaultValueAttribute defaultValueAttribute = attribute as DefaultValueAttribute;
						if (defaultValueAttribute != null)
						{
							this.defaultValue = defaultValueAttribute.Value;
							if (this.defaultValue != null && this.PropertyType.IsEnum && this.PropertyType.GetEnumUnderlyingType() == this.defaultValue.GetType())
							{
								this.defaultValue = Enum.ToObject(this.PropertyType, this.defaultValue);
							}
							this.state[ReflectPropertyDescriptor.BitDefaultValueQueried] = true;
						}
						else
						{
							AmbientValueAttribute ambientValueAttribute = attribute as AmbientValueAttribute;
							if (ambientValueAttribute != null)
							{
								this.ambientValue = ambientValueAttribute.Value;
								this.state[ReflectPropertyDescriptor.BitAmbientValueQueried] = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060021F9 RID: 8697 RVA: 0x000746A4 File Offset: 0x000728A4
		private object AmbientValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitAmbientValueQueried])
				{
					this.state[ReflectPropertyDescriptor.BitAmbientValueQueried] = true;
					Attribute attribute = this.Attributes[typeof(AmbientValueAttribute)];
					if (attribute != null)
					{
						this.ambientValue = ((AmbientValueAttribute)attribute).Value;
					}
					else
					{
						this.ambientValue = ReflectPropertyDescriptor.noValue;
					}
				}
				return this.ambientValue;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060021FA RID: 8698 RVA: 0x00074714 File Offset: 0x00072914
		private EventDescriptor ChangedEventValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitChangedQueried])
				{
					this.state[ReflectPropertyDescriptor.BitChangedQueried] = true;
					this.realChangedEvent = TypeDescriptor.GetEvents(this.ComponentType)[string.Format(CultureInfo.InvariantCulture, "{0}Changed", this.Name)];
				}
				return this.realChangedEvent;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060021FB RID: 8699 RVA: 0x00074778 File Offset: 0x00072978
		// (set) Token: 0x060021FC RID: 8700 RVA: 0x000747E4 File Offset: 0x000729E4
		private EventDescriptor IPropChangedEventValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitIPropChangedQueried])
				{
					this.state[ReflectPropertyDescriptor.BitIPropChangedQueried] = true;
					if (typeof(INotifyPropertyChanged).IsAssignableFrom(this.ComponentType))
					{
						this.realIPropChangedEvent = TypeDescriptor.GetEvents(typeof(INotifyPropertyChanged))["PropertyChanged"];
					}
				}
				return this.realIPropChangedEvent;
			}
			set
			{
				this.realIPropChangedEvent = value;
				this.state[ReflectPropertyDescriptor.BitIPropChangedQueried] = true;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x000747FE File Offset: 0x000729FE
		public override Type ComponentType
		{
			get
			{
				return this.componentClass;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x00074808 File Offset: 0x00072A08
		private object DefaultValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitDefaultValueQueried])
				{
					this.state[ReflectPropertyDescriptor.BitDefaultValueQueried] = true;
					Attribute attribute = this.Attributes[typeof(DefaultValueAttribute)];
					if (attribute != null)
					{
						this.defaultValue = ((DefaultValueAttribute)attribute).Value;
						if (this.defaultValue != null && this.PropertyType.IsEnum && this.PropertyType.GetEnumUnderlyingType() == this.defaultValue.GetType())
						{
							this.defaultValue = Enum.ToObject(this.PropertyType, this.defaultValue);
						}
					}
					else
					{
						this.defaultValue = ReflectPropertyDescriptor.noValue;
					}
				}
				return this.defaultValue;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x000748C4 File Offset: 0x00072AC4
		private MethodInfo GetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitGetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitGetQueried] = true;
					if (this.receiverType == null)
					{
						if (this.propInfo == null)
						{
							BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;
							this.propInfo = this.componentClass.GetProperty(this.Name, bindingAttr, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
						}
						if (this.propInfo != null)
						{
							this.getMethod = this.propInfo.GetGetMethod(true);
						}
						if (this.getMethod == null)
						{
							throw new InvalidOperationException(SR.GetString("Accessor methods for the {0} property are missing.", new object[]
							{
								this.componentClass.FullName + "." + this.Name
							}));
						}
					}
					else
					{
						this.getMethod = MemberDescriptor.FindMethod(this.componentClass, "Get" + this.Name, new Type[]
						{
							this.receiverType
						}, this.type);
						if (this.getMethod == null)
						{
							throw new ArgumentException(SR.GetString("Accessor methods for the {0} property are missing.", new object[]
							{
								this.Name
							}));
						}
					}
				}
				return this.getMethod;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x00074A15 File Offset: 0x00072C15
		private bool IsExtender
		{
			get
			{
				return this.receiverType != null;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x00074A23 File Offset: 0x00072C23
		public override bool IsReadOnly
		{
			get
			{
				return this.SetMethodValue == null || ((ReadOnlyAttribute)this.Attributes[typeof(ReadOnlyAttribute)]).IsReadOnly;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x00074A54 File Offset: 0x00072C54
		public override Type PropertyType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x00074A5C File Offset: 0x00072C5C
		private MethodInfo ResetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitResetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitResetQueried] = true;
					Type[] args;
					if (this.receiverType == null)
					{
						args = ReflectPropertyDescriptor.argsNone;
					}
					else
					{
						args = new Type[]
						{
							this.receiverType
						};
					}
					this.resetMethod = MemberDescriptor.FindMethod(this.componentClass, "Reset" + this.Name, args, typeof(void), false);
				}
				return this.resetMethod;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x00074AE8 File Offset: 0x00072CE8
		private MethodInfo SetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitSetQueried] && this.state[ReflectPropertyDescriptor.BitSetOnDemand])
				{
					this.state[ReflectPropertyDescriptor.BitSetQueried] = true;
					BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
					string name = this.propInfo.Name;
					if (this.setMethod == null)
					{
						Type baseType = this.ComponentType.BaseType;
						while (baseType != null && baseType != typeof(object) && !(baseType == null))
						{
							PropertyInfo property = baseType.GetProperty(name, bindingAttr, null, this.PropertyType, new Type[0], null);
							if (property != null)
							{
								this.setMethod = property.GetSetMethod();
								if (this.setMethod != null)
								{
									break;
								}
							}
							baseType = baseType.BaseType;
						}
					}
				}
				if (!this.state[ReflectPropertyDescriptor.BitSetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitSetQueried] = true;
					if (this.receiverType == null)
					{
						if (this.propInfo == null)
						{
							BindingFlags bindingAttr2 = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;
							this.propInfo = this.componentClass.GetProperty(this.Name, bindingAttr2, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
						}
						if (this.propInfo != null)
						{
							this.setMethod = this.propInfo.GetSetMethod(true);
						}
					}
					else
					{
						this.setMethod = MemberDescriptor.FindMethod(this.componentClass, "Set" + this.Name, new Type[]
						{
							this.receiverType,
							this.type
						}, typeof(void));
					}
				}
				return this.setMethod;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002205 RID: 8709 RVA: 0x00074CA0 File Offset: 0x00072EA0
		private MethodInfo ShouldSerializeMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitShouldSerializeQueried])
				{
					this.state[ReflectPropertyDescriptor.BitShouldSerializeQueried] = true;
					Type[] args;
					if (this.receiverType == null)
					{
						args = ReflectPropertyDescriptor.argsNone;
					}
					else
					{
						args = new Type[]
						{
							this.receiverType
						};
					}
					this.shouldSerializeMethod = MemberDescriptor.FindMethod(this.componentClass, "ShouldSerialize" + this.Name, args, typeof(bool), false);
				}
				return this.shouldSerializeMethod;
			}
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x00074D2C File Offset: 0x00072F2C
		public override void AddValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			EventDescriptor changedEventValue = this.ChangedEventValue;
			if (changedEventValue != null && changedEventValue.EventType.IsInstanceOfType(handler))
			{
				changedEventValue.AddEventHandler(component, handler);
				return;
			}
			if (base.GetValueChangedHandler(component) == null)
			{
				EventDescriptor ipropChangedEventValue = this.IPropChangedEventValue;
				if (ipropChangedEventValue != null)
				{
					ipropChangedEventValue.AddEventHandler(component, new PropertyChangedEventHandler(this.OnINotifyPropertyChanged));
				}
			}
			base.AddValueChanged(component, handler);
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x00074DA4 File Offset: 0x00072FA4
		internal bool ExtenderCanResetValue(IExtenderProvider provider, object component)
		{
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				return !object.Equals(this.ExtenderGetValue(provider, component), this.defaultValue);
			}
			if (this.ResetMethodValue != null)
			{
				MethodInfo shouldSerializeMethodValue = this.ShouldSerializeMethodValue;
				if (shouldSerializeMethodValue != null)
				{
					try
					{
						provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
						return (bool)shouldSerializeMethodValue.Invoke(provider, new object[]
						{
							component
						});
					}
					catch
					{
					}
					return true;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x00074E38 File Offset: 0x00073038
		internal Type ExtenderGetReceiverType()
		{
			return this.receiverType;
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x00074E40 File Offset: 0x00073040
		internal Type ExtenderGetType(IExtenderProvider provider)
		{
			return this.PropertyType;
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x00074E48 File Offset: 0x00073048
		internal object ExtenderGetValue(IExtenderProvider provider, object component)
		{
			if (provider != null)
			{
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				return this.GetMethodValue.Invoke(provider, new object[]
				{
					component
				});
			}
			return null;
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x00074E7C File Offset: 0x0007307C
		internal void ExtenderResetValue(IExtenderProvider provider, object component, PropertyDescriptor notifyDesc)
		{
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				this.ExtenderSetValue(provider, component, this.DefaultValue, notifyDesc);
				return;
			}
			if (this.AmbientValue != ReflectPropertyDescriptor.noValue)
			{
				this.ExtenderSetValue(provider, component, this.AmbientValue, notifyDesc);
				return;
			}
			if (this.ResetMethodValue != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object oldValue = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					oldValue = this.ExtenderGetValue(provider, component);
					try
					{
						componentChangeService.OnComponentChanging(component, notifyDesc);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
				}
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				if (this.ResetMethodValue != null)
				{
					this.ResetMethodValue.Invoke(provider, new object[]
					{
						component
					});
					if (componentChangeService != null)
					{
						object newValue = this.ExtenderGetValue(provider, component);
						componentChangeService.OnComponentChanged(component, notifyDesc, oldValue, newValue);
					}
				}
			}
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x00074F80 File Offset: 0x00073180
		internal void ExtenderSetValue(IExtenderProvider provider, object component, object value, PropertyDescriptor notifyDesc)
		{
			if (provider != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object oldValue = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					oldValue = this.ExtenderGetValue(provider, component);
					try
					{
						componentChangeService.OnComponentChanging(component, notifyDesc);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
				}
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				if (this.SetMethodValue != null)
				{
					this.SetMethodValue.Invoke(provider, new object[]
					{
						component,
						value
					});
					if (componentChangeService != null)
					{
						componentChangeService.OnComponentChanged(component, notifyDesc, oldValue, value);
					}
				}
			}
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x00075038 File Offset: 0x00073238
		internal bool ExtenderShouldSerializeValue(IExtenderProvider provider, object component)
		{
			provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
			if (this.IsReadOnly)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(provider, new object[]
						{
							component
						});
					}
					catch
					{
					}
				}
				return this.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			}
			if (this.DefaultValue == ReflectPropertyDescriptor.noValue)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(provider, new object[]
						{
							component
						});
					}
					catch
					{
					}
				}
				return true;
			}
			return !object.Equals(this.DefaultValue, this.ExtenderGetValue(provider, component));
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x00075114 File Offset: 0x00073314
		public override bool CanResetValue(object component)
		{
			if (this.IsExtender || this.IsReadOnly)
			{
				return false;
			}
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				return !object.Equals(this.GetValue(component), this.DefaultValue);
			}
			if (this.ResetMethodValue != null)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					component = this.GetInvocationTarget(this.componentClass, component);
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
					return true;
				}
				return true;
			}
			return this.AmbientValue != ReflectPropertyDescriptor.noValue && this.ShouldSerializeValue(component);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000751C4 File Offset: 0x000733C4
		protected override void FillAttributes(IList attributes)
		{
			foreach (object obj in TypeDescriptor.GetAttributes(this.PropertyType))
			{
				Attribute value = (Attribute)obj;
				attributes.Add(value);
			}
			BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			Type baseType = this.componentClass;
			int num = 0;
			while (baseType != null && baseType != typeof(object))
			{
				num++;
				baseType = baseType.BaseType;
			}
			if (num > 0)
			{
				baseType = this.componentClass;
				Attribute[][] array = new Attribute[num][];
				while (baseType != null && baseType != typeof(object))
				{
					MemberInfo memberInfo;
					if (this.IsExtender)
					{
						memberInfo = baseType.GetMethod("Get" + this.Name, bindingAttr, null, new Type[]
						{
							this.receiverType
						}, null);
					}
					else
					{
						memberInfo = baseType.GetProperty(this.Name, bindingAttr, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
					}
					if (memberInfo != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(memberInfo);
					}
					baseType = baseType.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						Attribute[] array4 = array3;
						for (int j = 0; j < array4.Length; j++)
						{
							AttributeProviderAttribute attributeProviderAttribute = array4[j] as AttributeProviderAttribute;
							if (attributeProviderAttribute != null)
							{
								Type type = Type.GetType(attributeProviderAttribute.TypeName);
								if (type != null)
								{
									Attribute[] array5 = null;
									if (!string.IsNullOrEmpty(attributeProviderAttribute.PropertyName))
									{
										MemberInfo[] member = type.GetMember(attributeProviderAttribute.PropertyName);
										if (member.Length != 0 && member[0] != null)
										{
											array5 = ReflectTypeDescriptionProvider.ReflectGetAttributes(member[0]);
										}
									}
									else
									{
										array5 = ReflectTypeDescriptionProvider.ReflectGetAttributes(type);
									}
									if (array5 != null)
									{
										foreach (Attribute value2 in array5)
										{
											attributes.Add(value2);
										}
									}
								}
							}
						}
					}
				}
				foreach (Attribute[] array7 in array)
				{
					if (array7 != null)
					{
						foreach (Attribute value3 in array7)
						{
							attributes.Add(value3);
						}
					}
				}
			}
			base.FillAttributes(attributes);
			if (this.SetMethodValue == null)
			{
				attributes.Add(ReadOnlyAttribute.Yes);
			}
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x0007545C File Offset: 0x0007365C
		public override object GetValue(object component)
		{
			if (this.IsExtender)
			{
				return null;
			}
			if (component != null)
			{
				component = this.GetInvocationTarget(this.componentClass, component);
				try
				{
					return SecurityUtils.MethodInfoInvoke(this.GetMethodValue, component, null);
				}
				catch (Exception innerException)
				{
					string text = null;
					IComponent component2 = component as IComponent;
					if (component2 != null)
					{
						ISite site = component2.Site;
						if (site != null && site.Name != null)
						{
							text = site.Name;
						}
					}
					if (text == null)
					{
						text = component.GetType().FullName;
					}
					if (innerException is TargetInvocationException)
					{
						innerException = innerException.InnerException;
					}
					string text2 = innerException.Message;
					if (text2 == null)
					{
						text2 = innerException.GetType().Name;
					}
					throw new TargetInvocationException(SR.GetString("Property accessor '{0}' on object '{1}' threw the following exception:'{2}'", new object[]
					{
						this.Name,
						text,
						text2
					}), innerException);
				}
			}
			return null;
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x00075538 File Offset: 0x00073738
		internal void OnINotifyPropertyChanged(object component, PropertyChangedEventArgs e)
		{
			if (string.IsNullOrEmpty(e.PropertyName) || string.Compare(e.PropertyName, this.Name, true, CultureInfo.InvariantCulture) == 0)
			{
				this.OnValueChanged(component, e);
			}
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x00075568 File Offset: 0x00073768
		protected override void OnValueChanged(object component, EventArgs e)
		{
			if (this.state[ReflectPropertyDescriptor.BitChangedQueried] && this.realChangedEvent == null)
			{
				base.OnValueChanged(component, e);
			}
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x0007558C File Offset: 0x0007378C
		public override void RemoveValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			EventDescriptor changedEventValue = this.ChangedEventValue;
			if (changedEventValue != null && changedEventValue.EventType.IsInstanceOfType(handler))
			{
				changedEventValue.RemoveEventHandler(component, handler);
				return;
			}
			base.RemoveValueChanged(component, handler);
			if (base.GetValueChangedHandler(component) == null)
			{
				EventDescriptor ipropChangedEventValue = this.IPropChangedEventValue;
				if (ipropChangedEventValue != null)
				{
					ipropChangedEventValue.RemoveEventHandler(component, new PropertyChangedEventHandler(this.OnINotifyPropertyChanged));
				}
			}
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x00075604 File Offset: 0x00073804
		public override void ResetValue(object component)
		{
			object invocationTarget = this.GetInvocationTarget(this.componentClass, component);
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				this.SetValue(component, this.DefaultValue);
				return;
			}
			if (this.AmbientValue != ReflectPropertyDescriptor.noValue)
			{
				this.SetValue(component, this.AmbientValue);
				return;
			}
			if (this.ResetMethodValue != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object oldValue = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					oldValue = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
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
				}
				if (this.ResetMethodValue != null)
				{
					SecurityUtils.MethodInfoInvoke(this.ResetMethodValue, invocationTarget, null);
					if (componentChangeService != null)
					{
						object newValue = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
						componentChangeService.OnComponentChanged(component, this, oldValue, newValue);
					}
				}
			}
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x00075700 File Offset: 0x00073900
		public override void SetValue(object component, object value)
		{
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				object invocationTarget = this.GetInvocationTarget(this.componentClass, component);
				if (!this.IsReadOnly)
				{
					if (site != null)
					{
						componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
					}
					if (componentChangeService != null)
					{
						obj = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
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
					}
					try
					{
						SecurityUtils.MethodInfoInvoke(this.SetMethodValue, invocationTarget, new object[]
						{
							value
						});
						this.OnValueChanged(invocationTarget, EventArgs.Empty);
					}
					catch (Exception ex2)
					{
						value = obj;
						if (ex2 is TargetInvocationException && ex2.InnerException != null)
						{
							throw ex2.InnerException;
						}
						throw ex2;
					}
					finally
					{
						if (componentChangeService != null)
						{
							componentChangeService.OnComponentChanged(component, this, obj, value);
						}
					}
				}
			}
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000757FC File Offset: 0x000739FC
		public override bool ShouldSerializeValue(object component)
		{
			component = this.GetInvocationTarget(this.componentClass, component);
			if (this.IsReadOnly)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
				}
				return this.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			}
			if (this.DefaultValue == ReflectPropertyDescriptor.noValue)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
				}
				return true;
			}
			return !object.Equals(this.DefaultValue, this.GetValue(component));
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06002217 RID: 8727 RVA: 0x000758C0 File Offset: 0x00073AC0
		public override bool SupportsChangeEvents
		{
			get
			{
				return this.IPropChangedEventValue != null || this.ChangedEventValue != null;
			}
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000758D8 File Offset: 0x00073AD8
		// Note: this type is marked as 'beforefieldinit'.
		static ReflectPropertyDescriptor()
		{
		}

		// Token: 0x04001039 RID: 4153
		private static readonly Type[] argsNone = new Type[0];

		// Token: 0x0400103A RID: 4154
		private static readonly object noValue = new object();

		// Token: 0x0400103B RID: 4155
		private static TraceSwitch PropDescCreateSwitch = new TraceSwitch("PropDescCreate", "ReflectPropertyDescriptor: Dump errors when creating property info");

		// Token: 0x0400103C RID: 4156
		private static TraceSwitch PropDescUsageSwitch = new TraceSwitch("PropDescUsage", "ReflectPropertyDescriptor: Debug propertydescriptor usage");

		// Token: 0x0400103D RID: 4157
		private static readonly int BitDefaultValueQueried = BitVector32.CreateMask();

		// Token: 0x0400103E RID: 4158
		private static readonly int BitGetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitDefaultValueQueried);

		// Token: 0x0400103F RID: 4159
		private static readonly int BitSetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitGetQueried);

		// Token: 0x04001040 RID: 4160
		private static readonly int BitShouldSerializeQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitSetQueried);

		// Token: 0x04001041 RID: 4161
		private static readonly int BitResetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitShouldSerializeQueried);

		// Token: 0x04001042 RID: 4162
		private static readonly int BitChangedQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitResetQueried);

		// Token: 0x04001043 RID: 4163
		private static readonly int BitIPropChangedQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitChangedQueried);

		// Token: 0x04001044 RID: 4164
		private static readonly int BitReadOnlyChecked = BitVector32.CreateMask(ReflectPropertyDescriptor.BitIPropChangedQueried);

		// Token: 0x04001045 RID: 4165
		private static readonly int BitAmbientValueQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitReadOnlyChecked);

		// Token: 0x04001046 RID: 4166
		private static readonly int BitSetOnDemand = BitVector32.CreateMask(ReflectPropertyDescriptor.BitAmbientValueQueried);

		// Token: 0x04001047 RID: 4167
		private BitVector32 state;

		// Token: 0x04001048 RID: 4168
		private Type componentClass;

		// Token: 0x04001049 RID: 4169
		private Type type;

		// Token: 0x0400104A RID: 4170
		private object defaultValue;

		// Token: 0x0400104B RID: 4171
		private object ambientValue;

		// Token: 0x0400104C RID: 4172
		private PropertyInfo propInfo;

		// Token: 0x0400104D RID: 4173
		private MethodInfo getMethod;

		// Token: 0x0400104E RID: 4174
		private MethodInfo setMethod;

		// Token: 0x0400104F RID: 4175
		private MethodInfo shouldSerializeMethod;

		// Token: 0x04001050 RID: 4176
		private MethodInfo resetMethod;

		// Token: 0x04001051 RID: 4177
		private EventDescriptor realChangedEvent;

		// Token: 0x04001052 RID: 4178
		private EventDescriptor realIPropChangedEvent;

		// Token: 0x04001053 RID: 4179
		private Type receiverType;
	}
}
