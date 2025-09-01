using System;
using System.Collections.Generic;

namespace System.Reflection
{
	/// <summary>Provides methods that retrieve information about types at run time.</summary>
	// Token: 0x020008DC RID: 2268
	public static class RuntimeReflectionExtensions
	{
		/// <summary>Retrieves a collection that represents all the fields defined on a specified type.</summary>
		/// <param name="type">The type that contains the fields.</param>
		/// <returns>A collection of fields for the specified type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x06004B84 RID: 19332 RVA: 0x000F08E9 File Offset: 0x000EEAE9
		public static IEnumerable<FieldInfo> GetRuntimeFields(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Retrieves a collection that represents all methods defined on a specified type.</summary>
		/// <param name="type">The type that contains the methods.</param>
		/// <returns>A collection of methods for the specified type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x06004B85 RID: 19333 RVA: 0x000F0907 File Offset: 0x000EEB07
		public static IEnumerable<MethodInfo> GetRuntimeMethods(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Retrieves a collection that represents all the properties defined on a specified type.</summary>
		/// <param name="type">The type that contains the properties.</param>
		/// <returns>A collection of properties for the specified type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x06004B86 RID: 19334 RVA: 0x000F0925 File Offset: 0x000EEB25
		public static IEnumerable<PropertyInfo> GetRuntimeProperties(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Retrieves a collection that represents all the events defined on a specified type.</summary>
		/// <param name="type">The type that contains the events.</param>
		/// <returns>A collection of events for the specified type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x06004B87 RID: 19335 RVA: 0x000F0943 File Offset: 0x000EEB43
		public static IEnumerable<EventInfo> GetRuntimeEvents(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Retrieves an object that represents a specified field.</summary>
		/// <param name="type">The type that contains the field.</param>
		/// <param name="name">The name of the field.</param>
		/// <returns>An object that represents the specified field, or <see langword="null" /> if the field is not found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.
		/// -or-
		/// <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06004B88 RID: 19336 RVA: 0x000F0961 File Offset: 0x000EEB61
		public static FieldInfo GetRuntimeField(this Type type, string name)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetField(name);
		}

		/// <summary>Retrieves an object that represents a specified method.</summary>
		/// <param name="type">The type that contains the method.</param>
		/// <param name="name">The name of the method.</param>
		/// <param name="parameters">An array that contains the method's parameters.</param>
		/// <returns>An object that represents the specified method, or <see langword="null" /> if the method is not found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.
		/// -or-
		/// <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="">More than one method is found with the specified name.</exception>
		// Token: 0x06004B89 RID: 19337 RVA: 0x000F097E File Offset: 0x000EEB7E
		public static MethodInfo GetRuntimeMethod(this Type type, string name, Type[] parameters)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetMethod(name, parameters);
		}

		/// <summary>Retrieves an object that represents a specified property.</summary>
		/// <param name="type">The type that contains the property.</param>
		/// <param name="name">The name of the property.</param>
		/// <returns>An object that represents the specified property, or <see langword="null" /> if the property is not found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.
		/// -or-
		/// <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a <see langword="RuntimeType" />.</exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property with the requested name was found.</exception>
		// Token: 0x06004B8A RID: 19338 RVA: 0x000F099C File Offset: 0x000EEB9C
		public static PropertyInfo GetRuntimeProperty(this Type type, string name)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetProperty(name);
		}

		/// <summary>Retrieves an object that represents the specified event.</summary>
		/// <param name="type">The type that contains the event.</param>
		/// <param name="name">The name of the event.</param>
		/// <returns>An object that represents the specified event, or <see langword="null" /> if the event is not found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.
		/// -or-
		/// <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06004B8B RID: 19339 RVA: 0x000F09B9 File Offset: 0x000EEBB9
		public static EventInfo GetRuntimeEvent(this Type type, string name)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetEvent(name);
		}

		/// <summary>Retrieves an object that represents the specified method on the direct or indirect base class where the method was first declared.</summary>
		/// <param name="method">The method to retrieve information about.</param>
		/// <returns>An object that represents the specified method's initial declaration on a base class.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="method" /> is <see langword="null" />.</exception>
		// Token: 0x06004B8C RID: 19340 RVA: 0x000F09D6 File Offset: 0x000EEBD6
		public static MethodInfo GetRuntimeBaseDefinition(this MethodInfo method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			return method.GetBaseDefinition();
		}

		/// <summary>Returns an interface mapping for the specified type and the specified interface.</summary>
		/// <param name="typeInfo">The type to retrieve a mapping for.</param>
		/// <param name="interfaceType">The interface to retrieve a mapping for.</param>
		/// <returns>An object that represents the interface mapping for the specified interface and type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeInfo" /> is <see langword="null" />.
		/// -or-
		/// <paramref name="interfaceType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="interfaceType" /> is not implemented by <paramref name="typeInfo" />.
		/// -or-
		/// <paramref name="interfaceType" /> does not refer to an interface.
		/// -or-
		/// <paramref name="typeInfo" /> or <paramref name="interfaceType" /> is an open generic type.
		/// -or-
		/// <paramref name="interfaceType" /> is a generic interface, and <paramref name="typeInfo" /> is an array type.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="typeInfo" /> represents a generic type parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="typeInfo" /> is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> instance whose <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method has not yet been called.
		/// -or-
		/// The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		// Token: 0x06004B8D RID: 19341 RVA: 0x000F09F2 File Offset: 0x000EEBF2
		public static InterfaceMapping GetRuntimeInterfaceMap(this TypeInfo typeInfo, Type interfaceType)
		{
			if (typeInfo == null)
			{
				throw new ArgumentNullException("typeInfo");
			}
			return typeInfo.GetInterfaceMap(interfaceType);
		}

		/// <summary>Gets an object that represents the method represented by the specified delegate.</summary>
		/// <param name="del">The delegate to examine.</param>
		/// <returns>An object that represents the method.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="del" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private).</exception>
		// Token: 0x06004B8E RID: 19342 RVA: 0x000F0A0F File Offset: 0x000EEC0F
		public static MethodInfo GetMethodInfo(this Delegate del)
		{
			if (del == null)
			{
				throw new ArgumentNullException("del");
			}
			return del.Method;
		}

		// Token: 0x04002F66 RID: 12134
		private const BindingFlags Everything = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
	}
}
