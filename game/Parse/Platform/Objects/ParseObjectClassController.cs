using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Platform.Objects;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Objects
{
	// Token: 0x02000030 RID: 48
	internal class ParseObjectClassController : IParseObjectClassController
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00009ABA File Offset: 0x00007CBA
		private static string ReservedParseObjectClassName
		{
			[CompilerGenerated]
			get
			{
				return ParseObjectClassController.<ReservedParseObjectClassName>k__BackingField;
			}
		} = "_ParseObject";

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00009AC1 File Offset: 0x00007CC1
		private ReaderWriterLockSlim Mutex
		{
			[CompilerGenerated]
			get
			{
				return this.<Mutex>k__BackingField;
			}
		} = new ReaderWriterLockSlim();

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00009AC9 File Offset: 0x00007CC9
		private IDictionary<string, ParseObjectClass> Classes
		{
			[CompilerGenerated]
			get
			{
				return this.<Classes>k__BackingField;
			}
		} = new Dictionary<string, ParseObjectClass>();

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00009AD1 File Offset: 0x00007CD1
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00009AD9 File Offset: 0x00007CD9
		private Dictionary<string, Action> RegisterActions
		{
			[CompilerGenerated]
			get
			{
				return this.<RegisterActions>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RegisterActions>k__BackingField = value;
			}
		} = new Dictionary<string, Action>();

		// Token: 0x0600025F RID: 607 RVA: 0x00009AE2 File Offset: 0x00007CE2
		public ParseObjectClassController()
		{
			this.AddValid(typeof(ParseObject));
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009B1B File Offset: 0x00007D1B
		public string GetClassName(Type type)
		{
			if (!(type == typeof(ParseObject)))
			{
				return type.GetParseClassName();
			}
			return ParseObjectClassController.ReservedParseObjectClassName;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00009B3C File Offset: 0x00007D3C
		public Type GetType(string className)
		{
			this.Mutex.EnterReadLock();
			ParseObjectClass parseObjectClass;
			this.Classes.TryGetValue(className, out parseObjectClass);
			this.Mutex.ExitReadLock();
			if (parseObjectClass == null)
			{
				return null;
			}
			return parseObjectClass.TypeInfo.AsType();
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00009B80 File Offset: 0x00007D80
		public bool GetClassMatch(string className, Type type)
		{
			this.Mutex.EnterReadLock();
			ParseObjectClass parseObjectClass;
			this.Classes.TryGetValue(className, out parseObjectClass);
			this.Mutex.ExitReadLock();
			if (parseObjectClass == null)
			{
				return type == typeof(ParseObject);
			}
			return parseObjectClass.TypeInfo == type.GetTypeInfo();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00009BD8 File Offset: 0x00007DD8
		public void AddValid(Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			if (!typeof(ParseObject).GetTypeInfo().IsAssignableFrom(typeInfo))
			{
				throw new ArgumentException("Cannot register a type that is not a subclass of ParseObject");
			}
			string className = this.GetClassName(type);
			try
			{
				this.Mutex.EnterWriteLock();
				ParseObjectClass parseObjectClass;
				if (this.Classes.TryGetValue(className, out parseObjectClass))
				{
					if (typeInfo.IsAssignableFrom(parseObjectClass.TypeInfo))
					{
						return;
					}
					if (!parseObjectClass.TypeInfo.IsAssignableFrom(typeInfo))
					{
						throw new ArgumentException(string.Concat(new string[]
						{
							"Tried to register both ",
							parseObjectClass.TypeInfo.FullName,
							" and ",
							typeInfo.FullName,
							" as the ParseObject subclass of ",
							className,
							". Cannot determine the right class to use because neither inherits from the other."
						}));
					}
				}
				ConstructorInfo constructorInfo;
				if ((constructorInfo = type.FindConstructor(Array.Empty<Type>())) == null)
				{
					constructorInfo = type.FindConstructor(new Type[]
					{
						typeof(string),
						typeof(IServiceHub)
					});
				}
				ConstructorInfo constructorInfo2 = constructorInfo;
				if (constructorInfo2 == null)
				{
					throw new ArgumentException("Cannot register a type that does not implement the default constructor!");
				}
				this.Classes[className] = new ParseObjectClass(type, constructorInfo2);
			}
			finally
			{
				this.Mutex.ExitWriteLock();
			}
			this.Mutex.EnterReadLock();
			Action action;
			this.RegisterActions.TryGetValue(className, out action);
			this.Mutex.ExitReadLock();
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00009D44 File Offset: 0x00007F44
		public void RemoveClass(Type type)
		{
			this.Mutex.EnterWriteLock();
			this.Classes.Remove(this.GetClassName(type));
			this.Mutex.ExitWriteLock();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009D6F File Offset: 0x00007F6F
		public void AddRegisterHook(Type type, Action action)
		{
			this.Mutex.EnterWriteLock();
			this.RegisterActions.Add(this.GetClassName(type), action);
			this.Mutex.ExitWriteLock();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00009D9C File Offset: 0x00007F9C
		public ParseObject Instantiate(string className, IServiceHub serviceHub)
		{
			this.Mutex.EnterReadLock();
			ParseObjectClass parseObjectClass;
			this.Classes.TryGetValue(className, out parseObjectClass);
			this.Mutex.ExitReadLock();
			if (parseObjectClass == null)
			{
				return new ParseObject(className, serviceHub);
			}
			return parseObjectClass.Instantiate().Bind(serviceHub);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009DE8 File Offset: 0x00007FE8
		public IDictionary<string, string> GetPropertyMappings(string className)
		{
			this.Mutex.EnterReadLock();
			ParseObjectClass parseObjectClass;
			this.Classes.TryGetValue(className, out parseObjectClass);
			if (parseObjectClass == null)
			{
				this.Classes.TryGetValue(ParseObjectClassController.ReservedParseObjectClassName, out parseObjectClass);
			}
			this.Mutex.ExitReadLock();
			return parseObjectClass.PropertyMappings;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00009E36 File Offset: 0x00008036
		// (set) Token: 0x06000269 RID: 617 RVA: 0x00009E3E File Offset: 0x0000803E
		private bool SDKClassesAdded
		{
			[CompilerGenerated]
			get
			{
				return this.<SDKClassesAdded>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SDKClassesAdded>k__BackingField = value;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00009E48 File Offset: 0x00008048
		public void AddIntrinsic()
		{
			if (!new ValueTuple<bool, bool>(this.SDKClassesAdded, this.SDKClassesAdded = true).Item1)
			{
				this.AddValid(typeof(ParseUser));
				this.AddValid(typeof(ParseRole));
				this.AddValid(typeof(ParseSession));
				this.AddValid(typeof(ParseInstallation));
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009EB1 File Offset: 0x000080B1
		// Note: this type is marked as 'beforefieldinit'.
		static ParseObjectClassController()
		{
		}

		// Token: 0x04000060 RID: 96
		[CompilerGenerated]
		private static readonly string <ReservedParseObjectClassName>k__BackingField;

		// Token: 0x04000061 RID: 97
		[CompilerGenerated]
		private readonly ReaderWriterLockSlim <Mutex>k__BackingField;

		// Token: 0x04000062 RID: 98
		[CompilerGenerated]
		private readonly IDictionary<string, ParseObjectClass> <Classes>k__BackingField;

		// Token: 0x04000063 RID: 99
		[CompilerGenerated]
		private Dictionary<string, Action> <RegisterActions>k__BackingField;

		// Token: 0x04000064 RID: 100
		[CompilerGenerated]
		private bool <SDKClassesAdded>k__BackingField;
	}
}
