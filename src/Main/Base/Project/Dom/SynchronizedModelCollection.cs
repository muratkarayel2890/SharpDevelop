﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using System.Threading;
using ICSharpCode.NRefactory.Utils;

namespace ICSharpCode.SharpDevelop.Dom
{
	/// <summary>
	/// Synchronizing wrapper around IMutableModelCollection.
	/// </summary>
	public class SynchronizedModelCollection<T> : IMutableModelCollection<T>
	{
		readonly IMutableModelCollection<T> underlyingCollection;
		readonly object syncRoot;
		
		public SynchronizedModelCollection(IMutableModelCollection<T> underlyingCollection)
			: this(underlyingCollection, new object())
		{
		}
		
		public SynchronizedModelCollection(IMutableModelCollection<T> underlyingCollection, object syncRoot)
		{
			if (underlyingCollection == null)
				throw new ArgumentNullException("underlyingCollection");
			if (syncRoot == null)
				throw new ArgumentNullException("syncRoot");
			this.underlyingCollection = underlyingCollection;
			this.syncRoot = syncRoot;
		}
		
		// Event registration is thread-safe on the underlying collection
		public event ModelCollectionChangedEventHandler<T> CollectionChanged {
			add { underlyingCollection.CollectionChanged += value; }
			remove { underlyingCollection.CollectionChanged -= value; }
		}

		#region IMutableModelCollection implementation

		public void Clear()
		{
			lock (syncRoot) {
				underlyingCollection.Clear();
			}
		}

		public void Add(T item)
		{
			lock (syncRoot) {
				underlyingCollection.Add(item);
			}
		}

		public void AddRange(IEnumerable<T> items)
		{
			lock (syncRoot) {
				underlyingCollection.AddRange(items);
			}
		}

		public bool Remove(T item)
		{
			lock (syncRoot) {
				return underlyingCollection.Remove(item);
			}
		}

		public int RemoveAll(Predicate<T> predicate)
		{
			lock (syncRoot) {
				return underlyingCollection.RemoveAll(predicate);
			}
		}

		public IDisposable BatchUpdate()
		{
			Monitor.Enter(syncRoot);
			IDisposable disposable = underlyingCollection.BatchUpdate();
			return new CallbackOnDispose(
				delegate {
					if (disposable != null)
						disposable.Dispose();
					Monitor.Exit(syncRoot);
				});
		}

		#endregion

		#region ICollection implementation

		public bool Contains(T item)
		{
			lock (syncRoot) {
				return underlyingCollection.Contains(item);
			}
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			lock (syncRoot) {
				underlyingCollection.CopyTo(array, arrayIndex);
			}
		}
		
		public int Count {
			get {
				lock (syncRoot) {
					return underlyingCollection.Count;
				}
			}
		}

		public bool IsReadOnly {
			get {
				lock (syncRoot) {
					return underlyingCollection.IsReadOnly;
				}
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			IEnumerable<T> snapshot;
			lock (syncRoot) {
				T[] array = new T[underlyingCollection.Count];
				underlyingCollection.CopyTo(array, 0);
				snapshot = array;
			}
			return snapshot.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion
	}
}
