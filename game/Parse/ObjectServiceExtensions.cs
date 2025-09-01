using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Control;
using Parse.Abstractions.Platform.Objects;
using Parse.Infrastructure.Data;
using Parse.Infrastructure.Utilities;

namespace Parse
{
	// Token: 0x0200001E RID: 30
	public static class ObjectServiceExtensions
	{
		// Token: 0x0600019B RID: 411 RVA: 0x000079D2 File Offset: 0x00005BD2
		public static void AddValidClass<T>(this IServiceHub serviceHub) where T : ParseObject, new()
		{
			serviceHub.ClassController.AddValid(typeof(T));
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000079E9 File Offset: 0x00005BE9
		public static void RegisterSubclass(this IServiceHub serviceHub, Type type)
		{
			if (typeof(ParseObject).IsAssignableFrom(type))
			{
				serviceHub.ClassController.AddValid(type);
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007A09 File Offset: 0x00005C09
		public static void RemoveClass<T>(this IServiceHub serviceHub) where T : ParseObject, new()
		{
			serviceHub.ClassController.RemoveClass(typeof(T));
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007A20 File Offset: 0x00005C20
		public static void RemoveClass(this IParseObjectClassController subclassingController, Type type)
		{
			if (typeof(ParseObject).IsAssignableFrom(type))
			{
				subclassingController.RemoveClass(type);
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007A3B File Offset: 0x00005C3B
		public static ParseObject CreateObject(this IServiceHub serviceHub, string className)
		{
			return serviceHub.ClassController.Instantiate(className, serviceHub);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00007A4A File Offset: 0x00005C4A
		public static T CreateObject<T>(this IServiceHub serviceHub) where T : ParseObject
		{
			return serviceHub.ClassController.CreateObject(serviceHub);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007A58 File Offset: 0x00005C58
		public static T CreateObject<T>(this IParseObjectClassController classController, IServiceHub serviceHub) where T : ParseObject
		{
			return (T)((object)classController.Instantiate(classController.GetClassName(typeof(T)), serviceHub));
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00007A76 File Offset: 0x00005C76
		public static ParseObject CreateObjectWithoutData(this IServiceHub serviceHub, string className, string objectId)
		{
			return serviceHub.ClassController.CreateObjectWithoutData(className, objectId, serviceHub);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007A88 File Offset: 0x00005C88
		public static ParseObject CreateObjectWithoutData(this IParseObjectClassController classController, string className, string objectId, IServiceHub serviceHub)
		{
			ParseObject.CreatingPointer.Value = true;
			ParseObject result;
			try
			{
				ParseObject parseObject = classController.Instantiate(className, serviceHub);
				parseObject.ObjectId = objectId;
				parseObject.IsDirty = false;
				if (parseObject.IsDirty)
				{
					throw new InvalidOperationException("A ParseObject subclass default constructor must not make changes to the object that cause it to be dirty.");
				}
				result = parseObject;
			}
			finally
			{
				ParseObject.CreatingPointer.Value = false;
			}
			return result;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00007AF0 File Offset: 0x00005CF0
		public static T CreateObjectWithoutData<T>(this IServiceHub serviceHub, string objectId) where T : ParseObject
		{
			return (T)((object)serviceHub.CreateObjectWithoutData(serviceHub.ClassController.GetClassName(typeof(T)), objectId));
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00007B13 File Offset: 0x00005D13
		public static Task DeleteObjectsAsync<T>(this IServiceHub serviceHub, IEnumerable<T> objects) where T : ParseObject
		{
			return serviceHub.DeleteObjectsAsync(objects, CancellationToken.None);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007B24 File Offset: 0x00005D24
		public static Task DeleteObjectsAsync<T>(this IServiceHub serviceHub, IEnumerable<T> objects, CancellationToken cancellationToken) where T : ParseObject
		{
			HashSet<ParseObject> unique = new HashSet<ParseObject>(objects.OfType<ParseObject>().ToList<ParseObject>(), new IdentityEqualityComparer<ParseObject>());
			Func<Task, Task> <>9__1;
			Func<Task, object> <>9__2;
			return ObjectServiceExtensions.EnqueueForAll<object>(unique, delegate(Task toAwait)
			{
				Func<Task, Task> continuation;
				Task task;
				if ((continuation = <>9__1) == null)
				{
					continuation = (<>9__1 = ((Task _) => Task.WhenAll(serviceHub.ObjectController.DeleteAllAsync((from task in unique
					select task.State).ToList<IObjectState>(), serviceHub.GetCurrentSessionToken(), cancellationToken))));
				}
				task = toAwait.OnSuccess(continuation).Unwrap();
				Func<Task, object> continuation2;
				if ((continuation2 = <>9__2) == null)
				{
					continuation2 = (<>9__2 = delegate(Task task)
					{
						foreach (ParseObject parseObject in unique)
						{
							parseObject.IsDirty = true;
						}
						return null;
					});
				}
				return task.OnSuccess(continuation2);
			}, cancellationToken);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007B7D File Offset: 0x00005D7D
		public static Task<IEnumerable<T>> FetchObjectsAsync<T>(this IServiceHub serviceHub, IEnumerable<T> objects) where T : ParseObject
		{
			return serviceHub.FetchObjectsAsync(objects, CancellationToken.None);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007B8C File Offset: 0x00005D8C
		public static Task<IEnumerable<T>> FetchObjectsAsync<T>(this IServiceHub serviceHub, IEnumerable<T> objects, CancellationToken cancellationToken) where T : ParseObject
		{
			return ObjectServiceExtensions.EnqueueForAll<IEnumerable<T>>(objects.Cast<ParseObject>(), (Task toAwait) => serviceHub.FetchAllInternalAsync(objects, true, toAwait, cancellationToken), cancellationToken);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007BD6 File Offset: 0x00005DD6
		public static Task<IEnumerable<T>> FetchObjectsIfNeededAsync<T>(this IServiceHub serviceHub, IEnumerable<T> objects) where T : ParseObject
		{
			return serviceHub.FetchObjectsIfNeededAsync(objects, CancellationToken.None);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007BE4 File Offset: 0x00005DE4
		public static Task<IEnumerable<T>> FetchObjectsIfNeededAsync<T>(this IServiceHub serviceHub, IEnumerable<T> objects, CancellationToken cancellationToken) where T : ParseObject
		{
			return ObjectServiceExtensions.EnqueueForAll<IEnumerable<T>>(objects.Cast<ParseObject>(), (Task toAwait) => serviceHub.FetchAllInternalAsync(objects, false, toAwait, cancellationToken), cancellationToken);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007C2E File Offset: 0x00005E2E
		public static ParseQuery<ParseObject> GetQuery(this IServiceHub serviceHub, string className)
		{
			if (serviceHub.ClassController.GetType(className) != null)
			{
				throw new ArgumentException("Use the class-specific query properties for class " + className, "className");
			}
			return new ParseQuery<ParseObject>(serviceHub, className);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007C5B File Offset: 0x00005E5B
		public static Task SaveObjectsAsync<T>(this IServiceHub serviceHub, IEnumerable<T> objects) where T : ParseObject
		{
			return serviceHub.SaveObjectsAsync(objects, CancellationToken.None);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007C69 File Offset: 0x00005E69
		public static Task SaveObjectsAsync<T>(this IServiceHub serviceHub, IEnumerable<T> objects, CancellationToken cancellationToken) where T : ParseObject
		{
			return serviceHub.DeepSaveAsync(objects.ToList<T>(), serviceHub.GetCurrentSessionToken(), cancellationToken);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007C80 File Offset: 0x00005E80
		internal static IEnumerable<object> TraverseObjectDeep(this IServiceHub serviceHub, object root, bool traverseParseObjects = false, bool yieldRoot = false)
		{
			IEnumerable<object> enumerable = serviceHub.DeepTraversalInternal(root, traverseParseObjects, new HashSet<object>(new IdentityEqualityComparer<object>()));
			if (!yieldRoot)
			{
				return enumerable;
			}
			return new object[]
			{
				root
			}.Concat(enumerable);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007CB5 File Offset: 0x00005EB5
		internal static T GenerateObjectFromState<T>(this IServiceHub serviceHub, IObjectState state, string defaultClassName) where T : ParseObject
		{
			return serviceHub.ClassController.GenerateObjectFromState(state, defaultClassName, serviceHub);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007CC5 File Offset: 0x00005EC5
		internal static T GenerateObjectFromState<T>(this IParseObjectClassController classController, IObjectState state, string defaultClassName, IServiceHub serviceHub) where T : ParseObject
		{
			T t = (T)((object)classController.CreateObjectWithoutData(state.ClassName ?? defaultClassName, state.ObjectId, serviceHub));
			t.HandleFetchResult(state);
			return t;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007CF0 File Offset: 0x00005EF0
		internal static IDictionary<string, object> GenerateJSONObjectForSaving(this IServiceHub serviceHub, IDictionary<string, IParseFieldOperation> operations)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (KeyValuePair<string, IParseFieldOperation> keyValuePair in operations)
			{
				dictionary[keyValuePair.Key] = PointerOrLocalIdEncoder.Instance.Encode(keyValuePair.Value, serviceHub);
			}
			return dictionary;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007D58 File Offset: 0x00005F58
		internal static bool CanBeSerializedAsValue(this IServiceHub serviceHub, object value)
		{
			return serviceHub.TraverseObjectDeep(value, false, true).OfType<ParseObject>().All((ParseObject entity) => entity.ObjectId != null);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007D8C File Offset: 0x00005F8C
		private static void CollectDirtyChildren(this IServiceHub serviceHub, object node, IList<ParseObject> dirtyChildren, ICollection<ParseObject> seen, ICollection<ParseObject> seenNew)
		{
			foreach (ParseObject parseObject in serviceHub.TraverseObjectDeep(node, false, false).OfType<ParseObject>())
			{
				ICollection<ParseObject> seenNew2;
				if (parseObject.ObjectId != null)
				{
					seenNew2 = new HashSet<ParseObject>(new IdentityEqualityComparer<ParseObject>());
				}
				else
				{
					if (seenNew.Contains(parseObject))
					{
						throw new InvalidOperationException("Found a circular dependency while saving");
					}
					seenNew2 = new HashSet<ParseObject>(seenNew, new IdentityEqualityComparer<ParseObject>())
					{
						parseObject
					};
				}
				if (seen.Contains(parseObject))
				{
					break;
				}
				seen.Add(parseObject);
				serviceHub.CollectDirtyChildren(parseObject.EstimatedData, dirtyChildren, seen, seenNew2);
				if (parseObject.CheckIsDirty(false))
				{
					dirtyChildren.Add(parseObject);
				}
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007E4C File Offset: 0x0000604C
		private static void CollectDirtyChildren(this IServiceHub serviceHub, object node, IList<ParseObject> dirtyChildren)
		{
			serviceHub.CollectDirtyChildren(node, dirtyChildren, new HashSet<ParseObject>(new IdentityEqualityComparer<ParseObject>()), new HashSet<ParseObject>(new IdentityEqualityComparer<ParseObject>()));
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007E6C File Offset: 0x0000606C
		internal static Task DeepSaveAsync(this IServiceHub serviceHub, object target, string sessionToken, CancellationToken cancellationToken)
		{
			List<ParseObject> list = new List<ParseObject>();
			serviceHub.CollectDirtyChildren(target, list);
			HashSet<ParseObject> uniqueObjects = new HashSet<ParseObject>(list, new IdentityEqualityComparer<ParseObject>());
			return Task.WhenAll((from file in serviceHub.TraverseObjectDeep(target, true, false).OfType<ParseFile>()
			where file.IsDirty
			select file.SaveAsync(serviceHub, cancellationToken)).ToList<Task>()).OnSuccess(delegate(Task _)
			{
				IEnumerable<ParseObject> remaining = new List<ParseObject>(uniqueObjects);
				return InternalExtensions.WhileAsync(() => Task.FromResult<bool>(remaining.Any<ParseObject>()), delegate
				{
					List<ParseObject> remaining;
					List<ParseObject> current = (from item in remaining
					where item.CanBeSerialized
					select item).ToList<ParseObject>();
					remaining = (from item in remaining
					where !item.CanBeSerialized
					select item).ToList<ParseObject>();
					remaining = remaining;
					if (current.Count == 0)
					{
						throw new InvalidOperationException("Unable to save a ParseObject with a relation to a cycle.");
					}
					Func<Task, Task<IObjectState[]>> <>9__8;
					return ObjectServiceExtensions.EnqueueForAll<object>(current, delegate(Task toAwait)
					{
						Func<Task, Task<IObjectState[]>> continuation;
						if ((continuation = <>9__8) == null)
						{
							continuation = (<>9__8 = delegate(Task __)
							{
								List<IObjectState> states = (from item in current
								select item.State).ToList<IObjectState>();
								List<IDictionary<string, IParseFieldOperation>> operationsList = (from item in current
								select item.StartSave()).ToList<IDictionary<string, IParseFieldOperation>>();
								return Task.WhenAll<IObjectState>(serviceHub.ObjectController.SaveAllAsync(states, operationsList, sessionToken, serviceHub, cancellationToken)).ContinueWith<Task<IObjectState[]>>(delegate(Task<IObjectState[]> task)
								{
									if (task.IsFaulted || task.IsCanceled)
									{
										using (IEnumerator<ValueTuple<ParseObject, IDictionary<string, IParseFieldOperation>>> enumerator = current.Zip(operationsList, (ParseObject item, IDictionary<string, IParseFieldOperation> ops) => new ValueTuple<ParseObject, IDictionary<string, IParseFieldOperation>>(item, ops)).GetEnumerator())
										{
											while (enumerator.MoveNext())
											{
												ValueTuple<ParseObject, IDictionary<string, IParseFieldOperation>> valueTuple = enumerator.Current;
												valueTuple.Item1.HandleFailedSave(valueTuple.Item2);
											}
											goto IL_E2;
										}
									}
									foreach (ValueTuple<ParseObject, IObjectState> valueTuple2 in current.Zip(task.Result, (ParseObject item, IObjectState state) => new ValueTuple<ParseObject, IObjectState>(item, state)))
									{
										valueTuple2.Item1.HandleSave(valueTuple2.Item2);
									}
									IL_E2:
									cancellationToken.ThrowIfCancellationRequested();
									return task;
								}).Unwrap<IObjectState[]>();
							});
						}
						return toAwait.OnSuccess(continuation).Unwrap<IObjectState[]>().OnSuccess((Task<IObjectState[]> t) => null);
					}, cancellationToken);
				});
			}).Unwrap();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007F20 File Offset: 0x00006120
		private static IEnumerable<object> DeepTraversalInternal(this IServiceHub serviceHub, object root, bool traverseParseObjects, ICollection<object> seen)
		{
			seen.Add(root);
			IEnumerable enumerable = ParseClient.IL2CPPCompiled ? null : null;
			IDictionary<string, object> dictionary = Conversion.As<IDictionary<string, object>>(root);
			if (dictionary != null)
			{
				enumerable = dictionary.Values;
			}
			else
			{
				IList<object> list = Conversion.As<IList<object>>(root);
				if (list != null)
				{
					enumerable = list;
				}
				else if (traverseParseObjects)
				{
					ParseObject entity = root as ParseObject;
					if (entity != null)
					{
						enumerable = from key in entity.Keys.ToList<string>()
						select entity[key];
					}
				}
			}
			if (enumerable != null)
			{
				foreach (object item in enumerable)
				{
					if (!seen.Contains(item))
					{
						yield return item;
						foreach (object obj in serviceHub.DeepTraversalInternal(item, traverseParseObjects, seen))
						{
							yield return obj;
						}
						IEnumerator<object> enumerator2 = null;
					}
					item = null;
				}
				IEnumerator enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007F48 File Offset: 0x00006148
		private static Task<T> EnqueueForAll<T>(IEnumerable<ParseObject> objects, Func<Task, Task<T>> taskStart, CancellationToken cancellationToken)
		{
			TaskCompletionSource<object> readyToStart = new TaskCompletionSource<object>();
			LockSet lockSet = new LockSet(from o in objects
			select o.TaskQueue.Mutex);
			lockSet.Enter();
			Task<T> fullTask2;
			try
			{
				Task<T> fullTask = taskStart(readyToStart.Task);
				List<Task> childTasks = new List<Task>();
				Func<Task, Task<T>> <>9__2;
				foreach (ParseObject parseObject in objects)
				{
					TaskQueue taskQueue = parseObject.TaskQueue;
					Func<Task, Task<T>> taskStart2;
					if ((taskStart2 = <>9__2) == null)
					{
						taskStart2 = (<>9__2 = delegate(Task task)
						{
							childTasks.Add(task);
							return fullTask;
						});
					}
					taskQueue.Enqueue<Task<T>>(taskStart2, cancellationToken);
				}
				Task.WhenAll(childTasks.ToArray()).ContinueWith(delegate(Task task)
				{
					readyToStart.SetResult(null);
				});
				fullTask2 = fullTask;
			}
			finally
			{
				lockSet.Exit();
			}
			return fullTask2;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008060 File Offset: 0x00006260
		private static Task<IEnumerable<T>> FetchAllInternalAsync<T>(this IServiceHub serviceHub, IEnumerable<T> objects, bool force, Task toAwait, CancellationToken cancellationToken) where T : ParseObject
		{
			ObjectServiceExtensions.<>c__DisplayClass29_0<T> CS$<>8__locals1 = new ObjectServiceExtensions.<>c__DisplayClass29_0<T>();
			CS$<>8__locals1.objects = objects;
			CS$<>8__locals1.force = force;
			CS$<>8__locals1.serviceHub = serviceHub;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			return toAwait.OnSuccess(delegate(Task _)
			{
				ObjectServiceExtensions.<>c__DisplayClass29_1<T> CS$<>8__locals2 = new ObjectServiceExtensions.<>c__DisplayClass29_1<T>();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				if (CS$<>8__locals1.objects.Any((T obj) => obj.State.ObjectId == null))
				{
					throw new InvalidOperationException("You cannot fetch objects that haven't already been saved.");
				}
				ObjectServiceExtensions.<>c__DisplayClass29_1<T> CS$<>8__locals3 = CS$<>8__locals2;
				IEnumerable<T> objects2 = CS$<>8__locals1.objects;
				Func<T, bool> predicate;
				if ((predicate = CS$<>8__locals1.<>9__2) == null)
				{
					predicate = (CS$<>8__locals1.<>9__2 = ((T obj) => CS$<>8__locals1.force || !obj.IsDataAvailable));
				}
				CS$<>8__locals3.objectsToFetch = objects2.Where(predicate).ToList<T>();
				if (CS$<>8__locals2.objectsToFetch.Count == 0)
				{
					return Task.FromResult<IEnumerable<T>>(CS$<>8__locals1.objects);
				}
				ObjectServiceExtensions.<>c__DisplayClass29_1<T> CS$<>8__locals4 = CS$<>8__locals2;
				IEnumerable<IGrouping<string, string>> source = from obj in CS$<>8__locals2.objectsToFetch
				group obj.ObjectId by obj.ClassName into classGroup
				where classGroup.Count<string>() > 0
				select classGroup;
				Func<IGrouping<string, string>, ValueTuple<string, Task<IEnumerable<ParseObject>>>> selector;
				if ((selector = CS$<>8__locals1.<>9__6) == null)
				{
					selector = (CS$<>8__locals1.<>9__6 = ((IGrouping<string, string> classGroup) => new ValueTuple<string, Task<IEnumerable<ParseObject>>>(classGroup.Key, new ParseQuery<ParseObject>(CS$<>8__locals1.serviceHub, classGroup.Key).WhereContainedIn<string>("objectId", classGroup).FindAsync(CS$<>8__locals1.cancellationToken))));
				}
				CS$<>8__locals4.findsByClass = source.Select(selector).ToDictionary(([TupleElementNames(new string[]
				{
					"ClassName",
					"FindTask"
				})] ValueTuple<string, Task<IEnumerable<ParseObject>>> pair) => pair.Item1, ([TupleElementNames(new string[]
				{
					"ClassName",
					"FindTask"
				})] ValueTuple<string, Task<IEnumerable<ParseObject>>> pair) => pair.Item2);
				return Task.WhenAll<IEnumerable<ParseObject>>(CS$<>8__locals2.findsByClass.Values.ToList<Task<IEnumerable<ParseObject>>>()).OnSuccess(delegate(Task<IEnumerable<ParseObject>[]> __)
				{
					if (CS$<>8__locals2.CS$<>8__locals1.cancellationToken.IsCancellationRequested)
					{
						return CS$<>8__locals2.CS$<>8__locals1.objects;
					}
					IEnumerable<T> objectsToFetch = CS$<>8__locals2.objectsToFetch;
					Func<T, IEnumerable<ParseObject>> collectionSelector;
					if ((collectionSelector = CS$<>8__locals2.<>9__10) == null)
					{
						collectionSelector = (CS$<>8__locals2.<>9__10 = ((T obj) => CS$<>8__locals2.findsByClass[obj.ClassName].Result));
					}
					foreach (ValueTuple<T, ParseObject> valueTuple in from <>h__TransparentIdentifier0 in objectsToFetch.SelectMany(collectionSelector, (T obj, ParseObject result) => new
					{
						obj,
						result
					})
					where <>h__TransparentIdentifier0.result.ObjectId == <>h__TransparentIdentifier0.obj.ObjectId
					select new ValueTuple<T, ParseObject>(<>h__TransparentIdentifier0.obj, <>h__TransparentIdentifier0.result))
					{
						T item = valueTuple.Item1;
						ParseObject item2 = valueTuple.Item2;
						item.MergeFromObject(item2);
						item.Fetched = true;
					}
					return CS$<>8__locals2.CS$<>8__locals1.objects;
				});
			}).Unwrap<IEnumerable<T>>();
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000080A8 File Offset: 0x000062A8
		internal static string GetFieldForPropertyName(this IServiceHub serviceHub, string className, string propertyName)
		{
			string result;
			if (!serviceHub.ClassController.GetPropertyMappings(className).TryGetValue(propertyName, out result))
			{
				return result;
			}
			return result;
		}

		// Token: 0x020000C7 RID: 199
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0<T> where T : ParseObject
		{
			// Token: 0x06000628 RID: 1576 RVA: 0x000136E4 File Offset: 0x000118E4
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x06000629 RID: 1577 RVA: 0x000136EC File Offset: 0x000118EC
			internal Task<object> <DeleteObjectsAsync>b__0(Task toAwait)
			{
				Func<Task, Task> continuation;
				if ((continuation = this.<>9__1) == null)
				{
					continuation = (this.<>9__1 = ((Task _) => Task.WhenAll(this.serviceHub.ObjectController.DeleteAllAsync(this.unique.Select(new Func<ParseObject, IObjectState>(ObjectServiceExtensions.<>c__11<T>.<>9.<DeleteObjectsAsync>b__11_3)).ToList<IObjectState>(), this.serviceHub.GetCurrentSessionToken(), this.cancellationToken))));
				}
				Task task2 = toAwait.OnSuccess(continuation).Unwrap();
				Func<Task, object> continuation2;
				if ((continuation2 = this.<>9__2) == null)
				{
					continuation2 = (this.<>9__2 = delegate(Task task)
					{
						foreach (ParseObject parseObject in this.unique)
						{
							parseObject.IsDirty = true;
						}
						return null;
					});
				}
				return task2.OnSuccess(continuation2);
			}

			// Token: 0x0600062A RID: 1578 RVA: 0x00013748 File Offset: 0x00011948
			internal Task <DeleteObjectsAsync>b__1(Task _)
			{
				return Task.WhenAll(this.serviceHub.ObjectController.DeleteAllAsync(this.unique.Select(new Func<ParseObject, IObjectState>(ObjectServiceExtensions.<>c__11<T>.<>9.<DeleteObjectsAsync>b__11_3)).ToList<IObjectState>(), this.serviceHub.GetCurrentSessionToken(), this.cancellationToken));
			}

			// Token: 0x0600062B RID: 1579 RVA: 0x000137AC File Offset: 0x000119AC
			internal object <DeleteObjectsAsync>b__2(Task task)
			{
				foreach (ParseObject parseObject in this.unique)
				{
					parseObject.IsDirty = true;
				}
				return null;
			}

			// Token: 0x0400016C RID: 364
			public IServiceHub serviceHub;

			// Token: 0x0400016D RID: 365
			public HashSet<ParseObject> unique;

			// Token: 0x0400016E RID: 366
			public CancellationToken cancellationToken;

			// Token: 0x0400016F RID: 367
			public Func<Task, Task> <>9__1;

			// Token: 0x04000170 RID: 368
			public Func<Task, object> <>9__2;
		}

		// Token: 0x020000C8 RID: 200
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__11<T> where T : ParseObject
		{
			// Token: 0x0600062C RID: 1580 RVA: 0x00013800 File Offset: 0x00011A00
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__11()
			{
			}

			// Token: 0x0600062D RID: 1581 RVA: 0x0001380C File Offset: 0x00011A0C
			public <>c__11()
			{
			}

			// Token: 0x0600062E RID: 1582 RVA: 0x00013814 File Offset: 0x00011A14
			internal IObjectState <DeleteObjectsAsync>b__11_3(ParseObject task)
			{
				return task.State;
			}

			// Token: 0x04000171 RID: 369
			public static readonly ObjectServiceExtensions.<>c__11<T> <>9 = new ObjectServiceExtensions.<>c__11<T>();

			// Token: 0x04000172 RID: 370
			public static Func<ParseObject, IObjectState> <>9__11_3;
		}

		// Token: 0x020000C9 RID: 201
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0<T> where T : ParseObject
		{
			// Token: 0x0600062F RID: 1583 RVA: 0x0001381C File Offset: 0x00011A1C
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x06000630 RID: 1584 RVA: 0x00013824 File Offset: 0x00011A24
			internal Task<IEnumerable<T>> <FetchObjectsAsync>b__0(Task toAwait)
			{
				return this.serviceHub.FetchAllInternalAsync(this.objects, true, toAwait, this.cancellationToken);
			}

			// Token: 0x04000173 RID: 371
			public IServiceHub serviceHub;

			// Token: 0x04000174 RID: 372
			public IEnumerable<T> objects;

			// Token: 0x04000175 RID: 373
			public CancellationToken cancellationToken;
		}

		// Token: 0x020000CA RID: 202
		[CompilerGenerated]
		private sealed class <>c__DisplayClass15_0<T> where T : ParseObject
		{
			// Token: 0x06000631 RID: 1585 RVA: 0x0001383F File Offset: 0x00011A3F
			public <>c__DisplayClass15_0()
			{
			}

			// Token: 0x06000632 RID: 1586 RVA: 0x00013847 File Offset: 0x00011A47
			internal Task<IEnumerable<T>> <FetchObjectsIfNeededAsync>b__0(Task toAwait)
			{
				return this.serviceHub.FetchAllInternalAsync(this.objects, false, toAwait, this.cancellationToken);
			}

			// Token: 0x04000176 RID: 374
			public IServiceHub serviceHub;

			// Token: 0x04000177 RID: 375
			public IEnumerable<T> objects;

			// Token: 0x04000178 RID: 376
			public CancellationToken cancellationToken;
		}

		// Token: 0x020000CB RID: 203
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000633 RID: 1587 RVA: 0x00013862 File Offset: 0x00011A62
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000634 RID: 1588 RVA: 0x0001386E File Offset: 0x00011A6E
			public <>c()
			{
			}

			// Token: 0x06000635 RID: 1589 RVA: 0x00013876 File Offset: 0x00011A76
			internal bool <CanBeSerializedAsValue>b__23_0(ParseObject entity)
			{
				return entity.ObjectId != null;
			}

			// Token: 0x06000636 RID: 1590 RVA: 0x00013881 File Offset: 0x00011A81
			internal bool <DeepSaveAsync>b__26_0(ParseFile file)
			{
				return file.IsDirty;
			}

			// Token: 0x06000637 RID: 1591 RVA: 0x00013889 File Offset: 0x00011A89
			internal bool <DeepSaveAsync>b__26_5(ParseObject item)
			{
				return item.CanBeSerialized;
			}

			// Token: 0x06000638 RID: 1592 RVA: 0x00013891 File Offset: 0x00011A91
			internal bool <DeepSaveAsync>b__26_6(ParseObject item)
			{
				return !item.CanBeSerialized;
			}

			// Token: 0x06000639 RID: 1593 RVA: 0x0001389C File Offset: 0x00011A9C
			internal IObjectState <DeepSaveAsync>b__26_10(ParseObject item)
			{
				return item.State;
			}

			// Token: 0x0600063A RID: 1594 RVA: 0x000138A4 File Offset: 0x00011AA4
			internal IDictionary<string, IParseFieldOperation> <DeepSaveAsync>b__26_11(ParseObject item)
			{
				return item.StartSave();
			}

			// Token: 0x0600063B RID: 1595 RVA: 0x000138AC File Offset: 0x00011AAC
			[return: TupleElementNames(new string[]
			{
				"item",
				"ops"
			})]
			internal ValueTuple<ParseObject, IDictionary<string, IParseFieldOperation>> <DeepSaveAsync>b__26_13(ParseObject item, IDictionary<string, IParseFieldOperation> ops)
			{
				return new ValueTuple<ParseObject, IDictionary<string, IParseFieldOperation>>(item, ops);
			}

			// Token: 0x0600063C RID: 1596 RVA: 0x000138B5 File Offset: 0x00011AB5
			[return: TupleElementNames(new string[]
			{
				"item",
				"state"
			})]
			internal ValueTuple<ParseObject, IObjectState> <DeepSaveAsync>b__26_14(ParseObject item, IObjectState state)
			{
				return new ValueTuple<ParseObject, IObjectState>(item, state);
			}

			// Token: 0x0600063D RID: 1597 RVA: 0x000138BE File Offset: 0x00011ABE
			internal object <DeepSaveAsync>b__26_9(Task<IObjectState[]> t)
			{
				return null;
			}

			// Token: 0x04000179 RID: 377
			public static readonly ObjectServiceExtensions.<>c <>9 = new ObjectServiceExtensions.<>c();

			// Token: 0x0400017A RID: 378
			public static Func<ParseObject, bool> <>9__23_0;

			// Token: 0x0400017B RID: 379
			public static Func<ParseFile, bool> <>9__26_0;

			// Token: 0x0400017C RID: 380
			public static Func<ParseObject, bool> <>9__26_5;

			// Token: 0x0400017D RID: 381
			public static Func<ParseObject, bool> <>9__26_6;

			// Token: 0x0400017E RID: 382
			public static Func<ParseObject, IObjectState> <>9__26_10;

			// Token: 0x0400017F RID: 383
			public static Func<ParseObject, IDictionary<string, IParseFieldOperation>> <>9__26_11;

			// Token: 0x04000180 RID: 384
			[TupleElementNames(new string[]
			{
				"item",
				"ops"
			})]
			public static Func<ParseObject, IDictionary<string, IParseFieldOperation>, ValueTuple<ParseObject, IDictionary<string, IParseFieldOperation>>> <>9__26_13;

			// Token: 0x04000181 RID: 385
			[TupleElementNames(new string[]
			{
				"item",
				"state"
			})]
			public static Func<ParseObject, IObjectState, ValueTuple<ParseObject, IObjectState>> <>9__26_14;

			// Token: 0x04000182 RID: 386
			public static Func<Task<IObjectState[]>, object> <>9__26_9;
		}

		// Token: 0x020000CC RID: 204
		[CompilerGenerated]
		private sealed class <>c__DisplayClass26_0
		{
			// Token: 0x0600063E RID: 1598 RVA: 0x000138C1 File Offset: 0x00011AC1
			public <>c__DisplayClass26_0()
			{
			}

			// Token: 0x0600063F RID: 1599 RVA: 0x000138C9 File Offset: 0x00011AC9
			internal Task <DeepSaveAsync>b__1(ParseFile file)
			{
				return file.SaveAsync(this.serviceHub, this.cancellationToken);
			}

			// Token: 0x06000640 RID: 1600 RVA: 0x000138E0 File Offset: 0x00011AE0
			internal Task <DeepSaveAsync>b__2(Task _)
			{
				ObjectServiceExtensions.<>c__DisplayClass26_1 CS$<>8__locals1 = new ObjectServiceExtensions.<>c__DisplayClass26_1();
				CS$<>8__locals1.CS$<>8__locals1 = this;
				CS$<>8__locals1.remaining = new List<ParseObject>(this.uniqueObjects);
				return InternalExtensions.WhileAsync(new Func<Task<bool>>(CS$<>8__locals1.<DeepSaveAsync>b__3), new Func<Task>(CS$<>8__locals1.<DeepSaveAsync>b__4));
			}

			// Token: 0x04000183 RID: 387
			public IServiceHub serviceHub;

			// Token: 0x04000184 RID: 388
			public CancellationToken cancellationToken;

			// Token: 0x04000185 RID: 389
			public HashSet<ParseObject> uniqueObjects;

			// Token: 0x04000186 RID: 390
			public string sessionToken;
		}

		// Token: 0x020000CD RID: 205
		[CompilerGenerated]
		private sealed class <>c__DisplayClass26_1
		{
			// Token: 0x06000641 RID: 1601 RVA: 0x00013928 File Offset: 0x00011B28
			public <>c__DisplayClass26_1()
			{
			}

			// Token: 0x06000642 RID: 1602 RVA: 0x00013930 File Offset: 0x00011B30
			internal Task<bool> <DeepSaveAsync>b__3()
			{
				return Task.FromResult<bool>(this.remaining.Any<ParseObject>());
			}

			// Token: 0x06000643 RID: 1603 RVA: 0x00013944 File Offset: 0x00011B44
			internal Task <DeepSaveAsync>b__4()
			{
				ObjectServiceExtensions.<>c__DisplayClass26_2 CS$<>8__locals1 = new ObjectServiceExtensions.<>c__DisplayClass26_2();
				CS$<>8__locals1.CS$<>8__locals2 = this;
				CS$<>8__locals1.current = this.remaining.Where(new Func<ParseObject, bool>(ObjectServiceExtensions.<>c.<>9.<DeepSaveAsync>b__26_5)).ToList<ParseObject>();
				List<ParseObject> list = this.remaining.Where(new Func<ParseObject, bool>(ObjectServiceExtensions.<>c.<>9.<DeepSaveAsync>b__26_6)).ToList<ParseObject>();
				this.remaining = list;
				if (CS$<>8__locals1.current.Count == 0)
				{
					throw new InvalidOperationException("Unable to save a ParseObject with a relation to a cycle.");
				}
				return ObjectServiceExtensions.EnqueueForAll<object>(CS$<>8__locals1.current, new Func<Task, Task<object>>(CS$<>8__locals1.<DeepSaveAsync>b__7), this.CS$<>8__locals1.cancellationToken);
			}

			// Token: 0x04000187 RID: 391
			public IEnumerable<ParseObject> remaining;

			// Token: 0x04000188 RID: 392
			public ObjectServiceExtensions.<>c__DisplayClass26_0 CS$<>8__locals1;
		}

		// Token: 0x020000CE RID: 206
		[CompilerGenerated]
		private sealed class <>c__DisplayClass26_2
		{
			// Token: 0x06000644 RID: 1604 RVA: 0x00013A04 File Offset: 0x00011C04
			public <>c__DisplayClass26_2()
			{
			}

			// Token: 0x06000645 RID: 1605 RVA: 0x00013A0C File Offset: 0x00011C0C
			internal Task<object> <DeepSaveAsync>b__7(Task toAwait)
			{
				Func<Task, Task<IObjectState[]>> continuation;
				if ((continuation = this.<>9__8) == null)
				{
					continuation = (this.<>9__8 = delegate(Task __)
					{
						ObjectServiceExtensions.<>c__DisplayClass26_3 CS$<>8__locals1 = new ObjectServiceExtensions.<>c__DisplayClass26_3();
						CS$<>8__locals1.CS$<>8__locals3 = this;
						List<IObjectState> states = this.current.Select(new Func<ParseObject, IObjectState>(ObjectServiceExtensions.<>c.<>9.<DeepSaveAsync>b__26_10)).ToList<IObjectState>();
						CS$<>8__locals1.operationsList = this.current.Select(new Func<ParseObject, IDictionary<string, IParseFieldOperation>>(ObjectServiceExtensions.<>c.<>9.<DeepSaveAsync>b__26_11)).ToList<IDictionary<string, IParseFieldOperation>>();
						return Task.WhenAll<IObjectState>(this.CS$<>8__locals2.CS$<>8__locals1.serviceHub.ObjectController.SaveAllAsync(states, CS$<>8__locals1.operationsList, this.CS$<>8__locals2.CS$<>8__locals1.sessionToken, this.CS$<>8__locals2.CS$<>8__locals1.serviceHub, this.CS$<>8__locals2.CS$<>8__locals1.cancellationToken)).ContinueWith<Task<IObjectState[]>>(new Func<Task<IObjectState[]>, Task<IObjectState[]>>(CS$<>8__locals1.<DeepSaveAsync>b__12)).Unwrap<IObjectState[]>();
					});
				}
				return toAwait.OnSuccess(continuation).Unwrap<IObjectState[]>().OnSuccess(new Func<Task<IObjectState[]>, object>(ObjectServiceExtensions.<>c.<>9.<DeepSaveAsync>b__26_9));
			}

			// Token: 0x06000646 RID: 1606 RVA: 0x00013A68 File Offset: 0x00011C68
			internal Task<IObjectState[]> <DeepSaveAsync>b__8(Task __)
			{
				ObjectServiceExtensions.<>c__DisplayClass26_3 CS$<>8__locals1 = new ObjectServiceExtensions.<>c__DisplayClass26_3();
				CS$<>8__locals1.CS$<>8__locals3 = this;
				List<IObjectState> states = this.current.Select(new Func<ParseObject, IObjectState>(ObjectServiceExtensions.<>c.<>9.<DeepSaveAsync>b__26_10)).ToList<IObjectState>();
				CS$<>8__locals1.operationsList = this.current.Select(new Func<ParseObject, IDictionary<string, IParseFieldOperation>>(ObjectServiceExtensions.<>c.<>9.<DeepSaveAsync>b__26_11)).ToList<IDictionary<string, IParseFieldOperation>>();
				return Task.WhenAll<IObjectState>(this.CS$<>8__locals2.CS$<>8__locals1.serviceHub.ObjectController.SaveAllAsync(states, CS$<>8__locals1.operationsList, this.CS$<>8__locals2.CS$<>8__locals1.sessionToken, this.CS$<>8__locals2.CS$<>8__locals1.serviceHub, this.CS$<>8__locals2.CS$<>8__locals1.cancellationToken)).ContinueWith<Task<IObjectState[]>>(new Func<Task<IObjectState[]>, Task<IObjectState[]>>(CS$<>8__locals1.<DeepSaveAsync>b__12)).Unwrap<IObjectState[]>();
			}

			// Token: 0x04000189 RID: 393
			public List<ParseObject> current;

			// Token: 0x0400018A RID: 394
			public ObjectServiceExtensions.<>c__DisplayClass26_1 CS$<>8__locals2;

			// Token: 0x0400018B RID: 395
			public Func<Task, Task<IObjectState[]>> <>9__8;
		}

		// Token: 0x020000CF RID: 207
		[CompilerGenerated]
		private sealed class <>c__DisplayClass26_3
		{
			// Token: 0x06000647 RID: 1607 RVA: 0x00013B53 File Offset: 0x00011D53
			public <>c__DisplayClass26_3()
			{
			}

			// Token: 0x06000648 RID: 1608 RVA: 0x00013B5C File Offset: 0x00011D5C
			internal Task<IObjectState[]> <DeepSaveAsync>b__12(Task<IObjectState[]> task)
			{
				if (task.IsFaulted || task.IsCanceled)
				{
					using (IEnumerator<ValueTuple<ParseObject, IDictionary<string, IParseFieldOperation>>> enumerator = this.CS$<>8__locals3.current.Zip(this.operationsList, new Func<ParseObject, IDictionary<string, IParseFieldOperation>, ValueTuple<ParseObject, IDictionary<string, IParseFieldOperation>>>(ObjectServiceExtensions.<>c.<>9.<DeepSaveAsync>b__26_13)).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ValueTuple<ParseObject, IDictionary<string, IParseFieldOperation>> valueTuple = enumerator.Current;
							valueTuple.Item1.HandleFailedSave(valueTuple.Item2);
						}
						goto IL_E2;
					}
				}
				foreach (ValueTuple<ParseObject, IObjectState> valueTuple2 in this.CS$<>8__locals3.current.Zip(task.Result, new Func<ParseObject, IObjectState, ValueTuple<ParseObject, IObjectState>>(ObjectServiceExtensions.<>c.<>9.<DeepSaveAsync>b__26_14)))
				{
					valueTuple2.Item1.HandleSave(valueTuple2.Item2);
				}
				IL_E2:
				this.CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.cancellationToken.ThrowIfCancellationRequested();
				return task;
			}

			// Token: 0x0400018C RID: 396
			public List<IDictionary<string, IParseFieldOperation>> operationsList;

			// Token: 0x0400018D RID: 397
			public ObjectServiceExtensions.<>c__DisplayClass26_2 CS$<>8__locals3;
		}

		// Token: 0x020000D0 RID: 208
		[CompilerGenerated]
		private sealed class <>c__DisplayClass27_0
		{
			// Token: 0x06000649 RID: 1609 RVA: 0x00013C84 File Offset: 0x00011E84
			public <>c__DisplayClass27_0()
			{
			}

			// Token: 0x0600064A RID: 1610 RVA: 0x00013C8C File Offset: 0x00011E8C
			internal object <DeepTraversalInternal>b__0(string key)
			{
				return this.entity[key];
			}

			// Token: 0x0400018E RID: 398
			public ParseObject entity;
		}

		// Token: 0x020000D1 RID: 209
		[CompilerGenerated]
		private sealed class <DeepTraversalInternal>d__27 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x0600064B RID: 1611 RVA: 0x00013C9A File Offset: 0x00011E9A
			[DebuggerHidden]
			public <DeepTraversalInternal>d__27(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600064C RID: 1612 RVA: 0x00013CB4 File Offset: 0x00011EB4
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num - -4 <= 1 || num - 1 <= 1)
				{
					try
					{
						if (num == -4 || num == 2)
						{
							try
							{
							}
							finally
							{
								this.<>m__Finally2();
							}
						}
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x0600064D RID: 1613 RVA: 0x00013D10 File Offset: 0x00011F10
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
					{
						this.<>1__state = -1;
						seen.Add(root);
						IEnumerable enumerable = ParseClient.IL2CPPCompiled ? null : null;
						IDictionary<string, object> dictionary = Conversion.As<IDictionary<string, object>>(root);
						if (dictionary != null)
						{
							enumerable = dictionary.Values;
						}
						else
						{
							IList<object> list = Conversion.As<IList<object>>(root);
							if (list != null)
							{
								enumerable = list;
							}
							else if (traverseParseObjects)
							{
								ObjectServiceExtensions.<>c__DisplayClass27_0 CS$<>8__locals1 = new ObjectServiceExtensions.<>c__DisplayClass27_0();
								CS$<>8__locals1.entity = (root as ParseObject);
								if (CS$<>8__locals1.entity != null)
								{
									enumerable = CS$<>8__locals1.entity.Keys.ToList<string>().Select(new Func<string, object>(CS$<>8__locals1.<DeepTraversalInternal>b__0));
								}
							}
						}
						if (enumerable != null)
						{
							enumerator = enumerable.GetEnumerator();
							this.<>1__state = -3;
							goto IL_1A3;
						}
						goto IL_1C0;
					}
					case 1:
						this.<>1__state = -3;
						enumerator2 = serviceHub.DeepTraversalInternal(item, traverseParseObjects, seen).GetEnumerator();
						this.<>1__state = -4;
						break;
					case 2:
						this.<>1__state = -4;
						break;
					default:
						return false;
					}
					if (enumerator2.MoveNext())
					{
						object obj = enumerator2.Current;
						this.<>2__current = obj;
						this.<>1__state = 2;
						return true;
					}
					this.<>m__Finally2();
					enumerator2 = null;
					IL_19C:
					item = null;
					IL_1A3:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
					}
					else
					{
						item = enumerator.Current;
						if (!seen.Contains(item))
						{
							this.<>2__current = item;
							this.<>1__state = 1;
							return true;
						}
						goto IL_19C;
					}
					IL_1C0:
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x0600064E RID: 1614 RVA: 0x00013F08 File Offset: 0x00012108
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x0600064F RID: 1615 RVA: 0x00013F31 File Offset: 0x00012131
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x06000650 RID: 1616 RVA: 0x00013F4E File Offset: 0x0001214E
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000651 RID: 1617 RVA: 0x00013F56 File Offset: 0x00012156
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x06000652 RID: 1618 RVA: 0x00013F5D File Offset: 0x0001215D
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000653 RID: 1619 RVA: 0x00013F68 File Offset: 0x00012168
			[DebuggerHidden]
			IEnumerator<object> IEnumerable<object>.GetEnumerator()
			{
				ObjectServiceExtensions.<DeepTraversalInternal>d__27 <DeepTraversalInternal>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<DeepTraversalInternal>d__ = this;
				}
				else
				{
					<DeepTraversalInternal>d__ = new ObjectServiceExtensions.<DeepTraversalInternal>d__27(0);
				}
				<DeepTraversalInternal>d__.serviceHub = serviceHub;
				<DeepTraversalInternal>d__.root = root;
				<DeepTraversalInternal>d__.traverseParseObjects = traverseParseObjects;
				<DeepTraversalInternal>d__.seen = seen;
				return <DeepTraversalInternal>d__;
			}

			// Token: 0x06000654 RID: 1620 RVA: 0x00013FCF File Offset: 0x000121CF
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();
			}

			// Token: 0x0400018F RID: 399
			private int <>1__state;

			// Token: 0x04000190 RID: 400
			private object <>2__current;

			// Token: 0x04000191 RID: 401
			private int <>l__initialThreadId;

			// Token: 0x04000192 RID: 402
			private ICollection<object> seen;

			// Token: 0x04000193 RID: 403
			public ICollection<object> <>3__seen;

			// Token: 0x04000194 RID: 404
			private object root;

			// Token: 0x04000195 RID: 405
			public object <>3__root;

			// Token: 0x04000196 RID: 406
			private bool traverseParseObjects;

			// Token: 0x04000197 RID: 407
			public bool <>3__traverseParseObjects;

			// Token: 0x04000198 RID: 408
			private IServiceHub serviceHub;

			// Token: 0x04000199 RID: 409
			public IServiceHub <>3__serviceHub;

			// Token: 0x0400019A RID: 410
			private IEnumerator <>7__wrap1;

			// Token: 0x0400019B RID: 411
			private object <item>5__3;

			// Token: 0x0400019C RID: 412
			private IEnumerator<object> <>7__wrap3;
		}

		// Token: 0x020000D2 RID: 210
		[CompilerGenerated]
		private sealed class <>c__DisplayClass28_0<T>
		{
			// Token: 0x06000655 RID: 1621 RVA: 0x00013FD7 File Offset: 0x000121D7
			public <>c__DisplayClass28_0()
			{
			}

			// Token: 0x06000656 RID: 1622 RVA: 0x00013FDF File Offset: 0x000121DF
			internal void <EnqueueForAll>b__1(Task task)
			{
				this.readyToStart.SetResult(null);
			}

			// Token: 0x0400019D RID: 413
			public TaskCompletionSource<object> readyToStart;
		}

		// Token: 0x020000D3 RID: 211
		[CompilerGenerated]
		private sealed class <>c__DisplayClass28_1<T>
		{
			// Token: 0x06000657 RID: 1623 RVA: 0x00013FED File Offset: 0x000121ED
			public <>c__DisplayClass28_1()
			{
			}

			// Token: 0x06000658 RID: 1624 RVA: 0x00013FF5 File Offset: 0x000121F5
			internal Task<T> <EnqueueForAll>b__2(Task task)
			{
				this.childTasks.Add(task);
				return this.fullTask;
			}

			// Token: 0x0400019E RID: 414
			public List<Task> childTasks;

			// Token: 0x0400019F RID: 415
			public Task<T> fullTask;

			// Token: 0x040001A0 RID: 416
			public Func<Task, Task<T>> <>9__2;
		}

		// Token: 0x020000D4 RID: 212
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__28<T>
		{
			// Token: 0x06000659 RID: 1625 RVA: 0x00014009 File Offset: 0x00012209
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__28()
			{
			}

			// Token: 0x0600065A RID: 1626 RVA: 0x00014015 File Offset: 0x00012215
			public <>c__28()
			{
			}

			// Token: 0x0600065B RID: 1627 RVA: 0x0001401D File Offset: 0x0001221D
			internal object <EnqueueForAll>b__28_0(ParseObject o)
			{
				return o.TaskQueue.Mutex;
			}

			// Token: 0x040001A1 RID: 417
			public static readonly ObjectServiceExtensions.<>c__28<T> <>9 = new ObjectServiceExtensions.<>c__28<T>();

			// Token: 0x040001A2 RID: 418
			public static Func<ParseObject, object> <>9__28_0;
		}

		// Token: 0x020000D5 RID: 213
		[CompilerGenerated]
		private sealed class <>c__DisplayClass29_0<T> where T : ParseObject
		{
			// Token: 0x0600065C RID: 1628 RVA: 0x0001402A File Offset: 0x0001222A
			public <>c__DisplayClass29_0()
			{
			}

			// Token: 0x0600065D RID: 1629 RVA: 0x00014034 File Offset: 0x00012234
			internal Task<IEnumerable<T>> <FetchAllInternalAsync>b__0(Task _)
			{
				ObjectServiceExtensions.<>c__DisplayClass29_1<T> CS$<>8__locals1 = new ObjectServiceExtensions.<>c__DisplayClass29_1<T>();
				CS$<>8__locals1.CS$<>8__locals1 = this;
				if (this.objects.Any(new Func<T, bool>(ObjectServiceExtensions.<>c__29<T>.<>9.<FetchAllInternalAsync>b__29_1)))
				{
					throw new InvalidOperationException("You cannot fetch objects that haven't already been saved.");
				}
				ObjectServiceExtensions.<>c__DisplayClass29_1<T> CS$<>8__locals2 = CS$<>8__locals1;
				IEnumerable<T> source = this.objects;
				Func<T, bool> predicate;
				if ((predicate = this.<>9__2) == null)
				{
					predicate = (this.<>9__2 = ((T obj) => this.force || !obj.IsDataAvailable));
				}
				CS$<>8__locals2.objectsToFetch = source.Where(predicate).ToList<T>();
				if (CS$<>8__locals1.objectsToFetch.Count == 0)
				{
					return Task.FromResult<IEnumerable<T>>(this.objects);
				}
				ObjectServiceExtensions.<>c__DisplayClass29_1<T> CS$<>8__locals3 = CS$<>8__locals1;
				IEnumerable<IGrouping<string, string>> source2 = CS$<>8__locals1.objectsToFetch.GroupBy(new Func<T, string>(ObjectServiceExtensions.<>c__29<T>.<>9.<FetchAllInternalAsync>b__29_3), new Func<T, string>(ObjectServiceExtensions.<>c__29<T>.<>9.<FetchAllInternalAsync>b__29_4)).Where(new Func<IGrouping<string, string>, bool>(ObjectServiceExtensions.<>c__29<T>.<>9.<FetchAllInternalAsync>b__29_5));
				Func<IGrouping<string, string>, ValueTuple<string, Task<IEnumerable<ParseObject>>>> selector;
				if ((selector = this.<>9__6) == null)
				{
					selector = (this.<>9__6 = ((IGrouping<string, string> classGroup) => new ValueTuple<string, Task<IEnumerable<ParseObject>>>(classGroup.Key, new ParseQuery<ParseObject>(this.serviceHub, classGroup.Key).WhereContainedIn<string>("objectId", classGroup).FindAsync(this.cancellationToken))));
				}
				CS$<>8__locals3.findsByClass = source2.Select(selector).ToDictionary(new Func<ValueTuple<string, Task<IEnumerable<ParseObject>>>, string>(ObjectServiceExtensions.<>c__29<T>.<>9.<FetchAllInternalAsync>b__29_7), new Func<ValueTuple<string, Task<IEnumerable<ParseObject>>>, Task<IEnumerable<ParseObject>>>(ObjectServiceExtensions.<>c__29<T>.<>9.<FetchAllInternalAsync>b__29_8));
				return Task.WhenAll<IEnumerable<ParseObject>>(CS$<>8__locals1.findsByClass.Values.ToList<Task<IEnumerable<ParseObject>>>()).OnSuccess(new Func<Task<IEnumerable<ParseObject>[]>, IEnumerable<T>>(CS$<>8__locals1.<FetchAllInternalAsync>b__9));
			}

			// Token: 0x0600065E RID: 1630 RVA: 0x000141D3 File Offset: 0x000123D3
			internal bool <FetchAllInternalAsync>b__2(T obj)
			{
				return this.force || !obj.IsDataAvailable;
			}

			// Token: 0x0600065F RID: 1631 RVA: 0x000141ED File Offset: 0x000123ED
			[return: TupleElementNames(new string[]
			{
				"ClassName",
				"FindTask"
			})]
			internal ValueTuple<string, Task<IEnumerable<ParseObject>>> <FetchAllInternalAsync>b__6(IGrouping<string, string> classGroup)
			{
				return new ValueTuple<string, Task<IEnumerable<ParseObject>>>(classGroup.Key, new ParseQuery<ParseObject>(this.serviceHub, classGroup.Key).WhereContainedIn<string>("objectId", classGroup).FindAsync(this.cancellationToken));
			}

			// Token: 0x040001A3 RID: 419
			public IEnumerable<T> objects;

			// Token: 0x040001A4 RID: 420
			public bool force;

			// Token: 0x040001A5 RID: 421
			public IServiceHub serviceHub;

			// Token: 0x040001A6 RID: 422
			public CancellationToken cancellationToken;

			// Token: 0x040001A7 RID: 423
			public Func<T, bool> <>9__2;

			// Token: 0x040001A8 RID: 424
			[TupleElementNames(new string[]
			{
				"ClassName",
				"FindTask"
			})]
			public Func<IGrouping<string, string>, ValueTuple<string, Task<IEnumerable<ParseObject>>>> <>9__6;
		}

		// Token: 0x020000D6 RID: 214
		[CompilerGenerated]
		private sealed class <>c__DisplayClass29_1<T> where T : ParseObject
		{
			// Token: 0x06000660 RID: 1632 RVA: 0x00014221 File Offset: 0x00012421
			public <>c__DisplayClass29_1()
			{
			}

			// Token: 0x06000661 RID: 1633 RVA: 0x0001422C File Offset: 0x0001242C
			internal IEnumerable<T> <FetchAllInternalAsync>b__9(Task<IEnumerable<ParseObject>[]> __)
			{
				if (this.CS$<>8__locals1.cancellationToken.IsCancellationRequested)
				{
					return this.CS$<>8__locals1.objects;
				}
				IEnumerable<T> source = this.objectsToFetch;
				Func<T, IEnumerable<ParseObject>> collectionSelector;
				if ((collectionSelector = this.<>9__10) == null)
				{
					collectionSelector = (this.<>9__10 = ((T obj) => this.findsByClass[obj.ClassName].Result));
				}
				foreach (ValueTuple<T, ParseObject> valueTuple in source.SelectMany(collectionSelector, new Func<T, ParseObject, <>f__AnonymousType2<T, ParseObject>>(ObjectServiceExtensions.<>c__29<T>.<>9.<FetchAllInternalAsync>b__29_11)).Where(new Func<<>f__AnonymousType2<T, ParseObject>, bool>(ObjectServiceExtensions.<>c__29<T>.<>9.<FetchAllInternalAsync>b__29_12)).Select(new Func<<>f__AnonymousType2<T, ParseObject>, ValueTuple<T, ParseObject>>(ObjectServiceExtensions.<>c__29<T>.<>9.<FetchAllInternalAsync>b__29_13)))
				{
					T item = valueTuple.Item1;
					ParseObject item2 = valueTuple.Item2;
					item.MergeFromObject(item2);
					item.Fetched = true;
				}
				return this.CS$<>8__locals1.objects;
			}

			// Token: 0x06000662 RID: 1634 RVA: 0x0001434C File Offset: 0x0001254C
			internal IEnumerable<ParseObject> <FetchAllInternalAsync>b__10(T obj)
			{
				return this.findsByClass[obj.ClassName].Result;
			}

			// Token: 0x040001A9 RID: 425
			public List<T> objectsToFetch;

			// Token: 0x040001AA RID: 426
			public Dictionary<string, Task<IEnumerable<ParseObject>>> findsByClass;

			// Token: 0x040001AB RID: 427
			public ObjectServiceExtensions.<>c__DisplayClass29_0<T> CS$<>8__locals1;

			// Token: 0x040001AC RID: 428
			public Func<T, IEnumerable<ParseObject>> <>9__10;
		}

		// Token: 0x020000D7 RID: 215
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__29<T> where T : ParseObject
		{
			// Token: 0x06000663 RID: 1635 RVA: 0x00014369 File Offset: 0x00012569
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__29()
			{
			}

			// Token: 0x06000664 RID: 1636 RVA: 0x00014375 File Offset: 0x00012575
			public <>c__29()
			{
			}

			// Token: 0x06000665 RID: 1637 RVA: 0x0001437D File Offset: 0x0001257D
			internal bool <FetchAllInternalAsync>b__29_1(T obj)
			{
				return obj.State.ObjectId == null;
			}

			// Token: 0x06000666 RID: 1638 RVA: 0x00014392 File Offset: 0x00012592
			internal string <FetchAllInternalAsync>b__29_3(T obj)
			{
				return obj.ClassName;
			}

			// Token: 0x06000667 RID: 1639 RVA: 0x0001439F File Offset: 0x0001259F
			internal string <FetchAllInternalAsync>b__29_4(T obj)
			{
				return obj.ObjectId;
			}

			// Token: 0x06000668 RID: 1640 RVA: 0x000143AC File Offset: 0x000125AC
			internal bool <FetchAllInternalAsync>b__29_5(IGrouping<string, string> classGroup)
			{
				return classGroup.Count<string>() > 0;
			}

			// Token: 0x06000669 RID: 1641 RVA: 0x000143B7 File Offset: 0x000125B7
			internal string <FetchAllInternalAsync>b__29_7([TupleElementNames(new string[]
			{
				"ClassName",
				"FindTask"
			})] ValueTuple<string, Task<IEnumerable<ParseObject>>> pair)
			{
				return pair.Item1;
			}

			// Token: 0x0600066A RID: 1642 RVA: 0x000143BF File Offset: 0x000125BF
			internal Task<IEnumerable<ParseObject>> <FetchAllInternalAsync>b__29_8([TupleElementNames(new string[]
			{
				"ClassName",
				"FindTask"
			})] ValueTuple<string, Task<IEnumerable<ParseObject>>> pair)
			{
				return pair.Item2;
			}

			// Token: 0x0600066B RID: 1643 RVA: 0x000143C7 File Offset: 0x000125C7
			internal <>f__AnonymousType2<T, ParseObject> <FetchAllInternalAsync>b__29_11(T obj, ParseObject result)
			{
				return new
				{
					obj,
					result
				};
			}

			// Token: 0x0600066C RID: 1644 RVA: 0x000143D0 File Offset: 0x000125D0
			internal bool <FetchAllInternalAsync>b__29_12(<>f__AnonymousType2<T, ParseObject> <>h__TransparentIdentifier0)
			{
				return <>h__TransparentIdentifier0.result.ObjectId == <>h__TransparentIdentifier0.obj.ObjectId;
			}

			// Token: 0x0600066D RID: 1645 RVA: 0x000143F2 File Offset: 0x000125F2
			[return: TupleElementNames(new string[]
			{
				"obj",
				"result"
			})]
			internal ValueTuple<T, ParseObject> <FetchAllInternalAsync>b__29_13(<>f__AnonymousType2<T, ParseObject> <>h__TransparentIdentifier0)
			{
				return new ValueTuple<T, ParseObject>(<>h__TransparentIdentifier0.obj, <>h__TransparentIdentifier0.result);
			}

			// Token: 0x040001AD RID: 429
			public static readonly ObjectServiceExtensions.<>c__29<T> <>9 = new ObjectServiceExtensions.<>c__29<T>();

			// Token: 0x040001AE RID: 430
			public static Func<T, bool> <>9__29_1;

			// Token: 0x040001AF RID: 431
			public static Func<T, string> <>9__29_3;

			// Token: 0x040001B0 RID: 432
			public static Func<T, string> <>9__29_4;

			// Token: 0x040001B1 RID: 433
			public static Func<IGrouping<string, string>, bool> <>9__29_5;

			// Token: 0x040001B2 RID: 434
			[TupleElementNames(new string[]
			{
				"ClassName",
				"FindTask"
			})]
			public static Func<ValueTuple<string, Task<IEnumerable<ParseObject>>>, string> <>9__29_7;

			// Token: 0x040001B3 RID: 435
			[TupleElementNames(new string[]
			{
				"ClassName",
				"FindTask"
			})]
			public static Func<ValueTuple<string, Task<IEnumerable<ParseObject>>>, Task<IEnumerable<ParseObject>>> <>9__29_8;

			// Token: 0x040001B4 RID: 436
			public static Func<T, ParseObject, <>f__AnonymousType2<T, ParseObject>> <>9__29_11;

			// Token: 0x040001B5 RID: 437
			public static Func<<>f__AnonymousType2<T, ParseObject>, bool> <>9__29_12;

			// Token: 0x040001B6 RID: 438
			[TupleElementNames(new string[]
			{
				"obj",
				"result"
			})]
			public static Func<<>f__AnonymousType2<T, ParseObject>, ValueTuple<T, ParseObject>> <>9__29_13;
		}
	}
}
