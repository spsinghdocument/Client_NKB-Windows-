using System;
using AMS.Profile;
using System.Diagnostics;
using System.Threading;
using System.Collections.Specialized;
using System.Configuration;
using System.Runtime.InteropServices;


namespace Download
{
	

	 class global
	{
		private global()
		{
			TapIp=ReadSetting("TapIp");
			TapPort=ReadSetting( "TapPort");
			SendClientConIp=ReadSetting("SendClientConIp");
			ClientPort_Send=ReadSetting( "SendClientPort");
			ClientPort_Rec=ReadSetting( "RecClientPort");
			RecClientConIp=ReadSetting("RecClientConIp");

			path=ReadSetting( "Path");
			Client1 = ReadSetting ("Client1");
			Client2 = ReadSetting ("Client2");
		}


		internal string TapIp;
		internal string TapPort;

		internal string SendClientConIp;
		internal string RecClientConIp;
		internal string ClientPort_Rec;
		internal string ClientPort_Send;
		internal string path;
		internal string Client1;
		internal string Client2;


		private static readonly global instance = new global ();
		public static global Instance
		{
			get{
				return instance;
			}
		}
		public string ReadSetting(string key)
		{
			string result = null;
			try
			{
				NameValueCollection appSettings =  ConfigurationManager.AppSettings;

				result = appSettings[key] ?? "Not Found";
				//Console.WriteLine(result);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error reading app settings");
			}
			return result;
		}
		
	}


}

