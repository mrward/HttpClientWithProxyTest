//
// Program.cs
//
// Author:
//       Matt Ward <matt.ward@microsoft.com>
//
// Copyright (c) 2018 Microsoft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace HttpClientWithProxyTest
{
	class MainClass
	{
		// Change proxy uri as needed.

		static Uri proxyUri = new Uri ("http://localhost:8888");

		public static void Main (string[] args)
		{
			try {
				RunTest ().Wait ();
			} catch (Exception ex) {
				Console.WriteLine (ex.GetBaseException ());
			}
		}

		static async Task RunTest ()
		{
			//await RunTest ("https://www.google.com");

			// This works
			await RunTest ("https://go.microsoft.com/fwlink/?LinkID=288859");

			// This fails. The uri passed to the ICredentials.GetCredentials is the original request uri
			// not the proxy uri.
			await RunTest("http://go.microsoft.com/fwlink/?LinkID=288859");
		}

		static async Task RunTest (string url)
		{
			var uri = new Uri (url);

			// Change proxy address here
			var proxy = new WebProxy (proxyUri);

			// Assuming Fiddler proxy credentials.
			proxy.Credentials = new TestCredentials ("1", "1");

			var handler = new HttpClientHandler ();
			handler.Proxy = proxy;

			using (var client = new HttpClient (handler)) {
				Console.WriteLine ("Url: {0}", url);
				using (var response = await client.GetAsync (uri)) {
					if (response.StatusCode == HttpStatusCode.OK) {
						Console.WriteLine ("OK");
					} else if (response.StatusCode == HttpStatusCode.ProxyAuthenticationRequired) {
						// Should not happen.
						Console.WriteLine ("Failed: ProxyAuthenticationRequired");
					} else {
						Console.WriteLine ("Unexpected status code: {0}", response.StatusCode);
					}
				}
			}
			Console.WriteLine ();
		} 

		class TestCredentials : ICredentials
		{
			NetworkCredential credential;

			public TestCredentials (string username, string password)
			{
				credential = new NetworkCredential (username, password);
			}

			/// <summary>
			/// With the http source which has a 302 redirect the uri passed to GetCredential is the original request
			/// not the proxy uri. If the credentials are cached based on the proxy uri then this will result in the
			/// credential not being returned.
			/// </summary>
			public NetworkCredential GetCredential (Uri uri, string authType)
			{
				Console.WriteLine ($"GetCredential. AuthType={authType} Uri={uri}");

				if (proxyUri == uri) {
					return credential;
				}

				return null;
			}
		}
	}
}
