﻿using System;
using System.Xml;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Inversion {

	/// <summary>
	/// Base interface for data models in Inversion.
	/// </summary>
	/// <remarks>
	/// <para>
	/// While this represents a point of extensibility for data models in Inversion this is pretty
	/// much limited to simple serialisation. `IData` had more relevance in Acumen
	/// pre-extension methods, now it's primary untility is simply to flag what is data-model
	/// and to be able to contrain based upon that.
	/// </para>
	/// <para>
	/// This approach to model serialisation is simple, but also fast, uncluttered, and explicit.
	/// The writers used obviously are  often used by a whole object graph being
	/// written out, and is part of a broader application concern. The writer
	/// should not be used in any way that yields side-effects beyond the
	/// serialisation at hand. <see cref="ToXml"/> and <see cref="ToJson"/>
	/// need to be fast and reliable. 
	/// </para>
	/// <para>
	/// Inversion favours an application where XML (or JSON for apps with data clients) is its primary external
	/// interface, which is transformed (normally with XSL) into a view stuitable for a particular
	/// class of user-agent. Other approaches may have a different regard for a data-model
	/// and `IData` serves as a point of extension in those cases perhaps.
	/// </para>
	/// <para>
	/// Extension methods are provided in <see cref="DataEx"/> to provide `IData.ToXml()` and `IData.ToJson()`.
	/// </para>
	/// </remarks>
	public interface IData : ICloneable {

		/// <summary>
		/// Provides an abstract representation
		/// of the objects data expressed as a JSON object.
		/// </summary>
		JObject Data { get; }

		/// <summary>
		/// Produces an xml representation of the model.
		/// </summary>
		/// <param name="writer">The writer to used to write the xml to. </param>
		void ToXml(XmlWriter writer);
		/// <summary>
		/// Produces a json respresentation of the model.
		/// </summary>
		/// <param name="writer">The writer to use for producing json.</param>
		void ToJson(JsonWriter writer);
	}
}
