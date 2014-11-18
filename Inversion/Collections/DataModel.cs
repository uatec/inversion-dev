﻿using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml;

using Newtonsoft.Json;

namespace Inversion.Collections {

	/// <summary>
	/// A <see cref="DynamicObject"/> implementing
	/// an <see cref="IDataDictionary{Data}"/> .
	/// </summary>
	/// <remarks>
	/// <para>
	/// This class is intended to help with exposing models to
	/// Razor templates, as it allows ad hoc properties
	/// to be used as dictionary keys, `model.UserDetails.Name` rather
	/// than `model["UserDetails"].Name`
	/// </para>
	/// <para>
	/// The initial idea was for  the `ControlState` to be
	/// one of these. When I start playing about with Razor
	/// a bit more I'll test it to see if there's any consequences.
	/// </para>
	/// </remarks>

	public class DataModel : DynamicObject, IDataDictionary<IData> {

		private readonly DataDictionary<IData> _backing = new DataDictionary<IData>();

		/// <summary>
		/// Implements trying to set a member of the data model ensuring that the value
		/// being assigned is both of type `IData` and not null.
		/// </summary>
		/// <param name="binder">The binder provided by the call site.</param>
		/// <param name="value">The value to set.</param>
		/// <returns>true if the operation is complete, false if the call site should determine behavior.</returns>
		public override bool TrySetMember(SetMemberBinder binder, object value) {
			IData data = value as IData;
			if (data != null) {
				_backing[binder.Name] = data;
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// Implements trying to get a member.
		/// </summary>
		/// <param name="binder">The binder provided by the call site.</param>
		/// <param name="result">The result of the get operation.</param>
		/// <returns>true if the operation is complete, false if the call site should determine behavior.</returns>
		public override bool TryGetMember(GetMemberBinder binder, out object result) {
			// is this method actually adding any value over the base implementation?
			if (_backing.ContainsKey(binder.Name)) {
				result = _backing[binder.Name];
				return true;
			} else {
				result = null;
				return false;
			}
		}

		/// <summary>
		/// Imports the key-value pairs from a provided dictionary into this one.
		/// </summary>
		/// <param name="other">The other dictionary to import into this one.</param>
		/// <returns>Returns the current instance of this dictionary.</returns>
		public IDataDictionary<IData> Import(IEnumerable<KeyValuePair<string, IData>> other) {
			foreach (KeyValuePair<string, IData> entry in other) {
				this.Add(entry);
			}
			return this;
		}


		#region IDictionary<string,IData> Members

		/// <summary>
		/// Adds the provided value to the model against the specified key.
		/// </summary>
		/// <param name="key">The key for the new element.</param>
		/// <param name="value">The value to be added/</param>
		public void Add(string key, IData value) {
			_backing.Add(key, value);
		}

		/// <summary>
		/// Determines whether or not the model contains anything stored against the provided key.
		/// </summary>
		/// <param name="key">The key to check.</param>
		/// <returns>
		/// Returns true if the model contains a key-value pair with a key corresponding to the
		/// specified key; otherwise returns false.
		/// </returns>
		public bool ContainsKey(string key) {
			return _backing.ContainsKey(key);
		}

		/// <summary>
		/// A collection of all the keys contained in the model.
		/// </summary>
		public ICollection<string> Keys {
			get { return _backing.Keys; }
		}

		/// <summary>
		/// Removes the key-value pair of the specified key.
		/// </summary>
		/// <param name="key">The key to remove from the model.</param>
		/// <returns>Rreturns true if the key was found and removed; otherwise returns false.</returns>
		public bool Remove(string key) {
			return _backing.Remove(key);
		}

		/// <summary>
		/// Tries to get the value of the key-value pair with the same
		/// key as the one provided.
		/// </summary>
		/// <param name="key">The key to lookup.</param>
		/// <param name="value">The value of the found key-value pair.</param>
		/// <returns>Returns true if the operation was successful; otherwise returns false.</returns>
		public bool TryGetValue(string key, out IData value) {
			return _backing.TryGetValue(key, out value);
		}

		/// <summary>
		/// A collection of all the values of the model.
		/// </summary>
		public ICollection<IData> Values {
			get { return _backing.Values; }
		}

		/// <summary>
		/// Obtains the value of the key-value pair with the same key as the one provided.
		/// </summary>
		/// <param name="key">The key to lookup.</param>
		/// <returns>Returns the value of the key-value pair found, if any.</returns>
		public IData this[string key] {
			get { return _backing[key]; }
			set { _backing[key] = value; }
		}

		#endregion

		#region ICollection<KeyValuePair<string,IData>> Members

		public void Add(KeyValuePair<string, IData> item) {
			((ICollection<KeyValuePair<string, IData>>)_backing).Add(item);
		}

		public void Clear() {
			_backing.Clear();
		}

		public bool Contains(KeyValuePair<string, IData> item) {
			return ((ICollection<KeyValuePair<string, IData>>)_backing).Contains(item);
		}

		public void CopyTo(KeyValuePair<string, IData>[] array, int arrayIndex) {
			((ICollection<KeyValuePair<string, IData>>)_backing).CopyTo(array, arrayIndex);
		}

		public int Count {
			get { return _backing.Count; }
		}

		public bool IsReadOnly {
			get { return ((ICollection<KeyValuePair<string, IData>>)_backing).IsReadOnly; }
		}

		public bool Remove(KeyValuePair<string, IData> item) {
			return ((ICollection<KeyValuePair<string, IData>>)_backing).Remove(item);
		}

		#endregion

		#region IEnumerable<KeyValuePair<string,IData>> Members

		public IEnumerator<KeyValuePair<string, IData>> GetEnumerator() {
			return ((IEnumerable<KeyValuePair<string, IData>>)_backing).GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator() {
			return ((IEnumerable)_backing).GetEnumerator();
		}

		#endregion

		#region IData Members

		public void ToXml(XmlWriter writer) {
			_backing.ToXml(writer);
		}

		public void ToJson(JsonWriter writer) {
			_backing.ToJson(writer);
		}

		#endregion

		#region ICloneable Members

		public object Clone() {
			return _backing.Clone();
		}

		#endregion
	}
}
