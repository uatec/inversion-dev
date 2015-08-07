﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inversion.Web.AspNet {
	public class AspNetRequestCookieCollection : IRequestCookieCollection {

		private readonly HttpCookieCollection _cookies;
		private IDictionary<string,string> _memo;

		protected IDictionary<string, string> Memo {
			get {
				if (_memo == null) {
					_memo = _cookies.AllKeys.Select(key => _cookies.Get(key)).ToDictionary(i => i.Name, i => i.Value);
				}
				return _memo;
			}
		}

		public string this[string key] {
			get { return this.Get(key); }
		}

		public AspNetRequestCookieCollection(HttpCookieCollection cookies) {
			_cookies = cookies;
		}

		public string Get(string key) {
			string value;
			this.TryGetValue(key, out value);
			return value;
		}

		public bool TryGetValue(string key, out string value) {
			return this.Memo.TryGetValue(key, out value);
		}

	    public IDictionary<string, string> GetValues(string key)
	    {
	        HttpCookie cookie = _cookies[key];
	        return cookie.Values.AllKeys.ToDictionary(name => name, name => _cookies[key].Values[name]);
	    }

	    public bool TryGetValues(string key, out IDictionary<string, string> values)
	    {
            values = null;
	        if (!_cookies.AllKeys.Contains(key))
	        {
                return false;
	        }
	        values = GetValues(key);
            return true;
	    }

	    public IEnumerator<KeyValuePair<string, string>> GetEnumerator() {
			return this.Memo.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}

	}
}
