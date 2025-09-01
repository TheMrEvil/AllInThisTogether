using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.UIElements
{
	// Token: 0x020002F5 RID: 757
	internal class VisualElementFactoryRegistry
	{
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x00066250 File Offset: 0x00064450
		internal static Dictionary<string, List<IUxmlFactory>> factories
		{
			get
			{
				bool flag = VisualElementFactoryRegistry.s_Factories == null;
				if (flag)
				{
					VisualElementFactoryRegistry.s_Factories = new Dictionary<string, List<IUxmlFactory>>();
					VisualElementFactoryRegistry.RegisterEngineFactories();
					VisualElementFactoryRegistry.RegisterUserFactories();
				}
				return VisualElementFactoryRegistry.s_Factories;
			}
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0006628C File Offset: 0x0006448C
		protected static void RegisterFactory(IUxmlFactory factory)
		{
			List<IUxmlFactory> list;
			bool flag = VisualElementFactoryRegistry.factories.TryGetValue(factory.uxmlQualifiedName, out list);
			if (flag)
			{
				foreach (IUxmlFactory uxmlFactory in list)
				{
					bool flag2 = uxmlFactory.GetType() == factory.GetType();
					if (flag2)
					{
						throw new ArgumentException("A factory for the type " + factory.GetType().FullName + " was already registered");
					}
				}
				list.Add(factory);
			}
			else
			{
				list = new List<IUxmlFactory>();
				list.Add(factory);
				VisualElementFactoryRegistry.factories.Add(factory.uxmlQualifiedName, list);
			}
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00066354 File Offset: 0x00064554
		internal static bool TryGetValue(string fullTypeName, out List<IUxmlFactory> factoryList)
		{
			factoryList = null;
			return VisualElementFactoryRegistry.factories.TryGetValue(fullTypeName, out factoryList);
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00066378 File Offset: 0x00064578
		internal static bool TryGetValue(Type type, out List<IUxmlFactory> factoryList)
		{
			foreach (List<IUxmlFactory> list in VisualElementFactoryRegistry.factories.Values)
			{
				IUxmlFactory uxmlFactory = list[0];
				IUxmlFactoryInternal uxmlFactoryInternal = uxmlFactory as IUxmlFactoryInternal;
				bool flag = uxmlFactoryInternal != null && uxmlFactoryInternal.uxmlType == type;
				if (flag)
				{
					factoryList = list;
					return true;
				}
			}
			factoryList = null;
			return false;
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00066404 File Offset: 0x00064604
		private static void RegisterEngineFactories()
		{
			IUxmlFactory[] array = new IUxmlFactory[]
			{
				new UxmlRootElementFactory(),
				new UxmlTemplateFactory(),
				new UxmlStyleFactory(),
				new UxmlAttributeOverridesFactory(),
				new Button.UxmlFactory(),
				new VisualElement.UxmlFactory(),
				new IMGUIContainer.UxmlFactory(),
				new Image.UxmlFactory(),
				new Label.UxmlFactory(),
				new RepeatButton.UxmlFactory(),
				new ScrollView.UxmlFactory(),
				new Scroller.UxmlFactory(),
				new Slider.UxmlFactory(),
				new SliderInt.UxmlFactory(),
				new MinMaxSlider.UxmlFactory(),
				new GroupBox.UxmlFactory(),
				new RadioButton.UxmlFactory(),
				new RadioButtonGroup.UxmlFactory(),
				new Toggle.UxmlFactory(),
				new TextField.UxmlFactory(),
				new TemplateContainer.UxmlFactory(),
				new Box.UxmlFactory(),
				new DropdownField.UxmlFactory(),
				new HelpBox.UxmlFactory(),
				new PopupWindow.UxmlFactory(),
				new ProgressBar.UxmlFactory(),
				new ListView.UxmlFactory(),
				new TwoPaneSplitView.UxmlFactory(),
				new InternalTreeView.UxmlFactory(),
				new TreeView.UxmlFactory(),
				new Foldout.UxmlFactory(),
				new BindableElement.UxmlFactory(),
				new TextElement.UxmlFactory(),
				new ButtonStripField.UxmlFactory()
			};
			foreach (IUxmlFactory factory in array)
			{
				VisualElementFactoryRegistry.RegisterFactory(factory);
			}
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00066564 File Offset: 0x00064764
		internal static void RegisterUserFactories()
		{
			HashSet<string> hashSet = new HashSet<string>(ScriptingRuntime.GetAllUserAssemblies());
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				bool flag = !hashSet.Contains(assembly.GetName().Name + ".dll") || assembly.GetName().Name == "UnityEngine.UIElementsModule";
				if (!flag)
				{
					Type[] types = assembly.GetTypes();
					foreach (Type type in types)
					{
						bool flag2 = !typeof(IUxmlFactory).IsAssignableFrom(type) || type.IsInterface || type.IsAbstract || type.IsGenericType;
						if (!flag2)
						{
							IUxmlFactory factory = (IUxmlFactory)Activator.CreateInstance(type);
							VisualElementFactoryRegistry.RegisterFactory(factory);
						}
					}
				}
			}
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x000020C2 File Offset: 0x000002C2
		public VisualElementFactoryRegistry()
		{
		}

		// Token: 0x04000AC6 RID: 2758
		private static Dictionary<string, List<IUxmlFactory>> s_Factories;
	}
}
