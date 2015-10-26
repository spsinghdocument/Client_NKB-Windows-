using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.IO;
using NNanomsg.Protocols;
using NNanomsg;
using System.Threading;
using Download;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;



namespace server_2
{
	class Program
	{
		private static readonly Program instance = new Program ();
		public static Program Instance
		{
			get{
				return instance;
			}
		}


		static void Main(string[] args)
		{
			//Program p = new Program ();
			Thread th = new Thread (delegate(){Program.Instance.Download ();});
			th.Start ();
	      // Task.Factory.StartNew (() => Download ());
			Console.ReadLine();
			//========================================================================================================================================
			Console.WriteLine ("Download server start");
			//send();
		}


		 void Download()
		{
			ReplySocket sockClientListner;
			try
			{
				
				sockClientListner = new ReplySocket();
			
				sockClientListner.Bind("tcp://" +global.Instance.RecClientConIp+ ":" +global.Instance.ClientPort_Rec);

				//SubscribeSocket subscriber = new SubscribeSocket();
				//subscriber.Connect("tcp://" +global.Instance.ClientConIp+ ":" +global.Instance.ClientPort_Rec);
				Console.WriteLine("NANOMQ UDP_Reciever Start DataAddress: " + "tcp://" + global.Instance.RecClientConIp+ ":" +global.Instance.ClientPort_Rec);
				//subscriber.Subscribe(BitConverter.GetBytes(Convert.ToInt64(global.Instance.Client1)));
				//subscriber.Subscribe(BitConverter.GetBytes(Convert.ToInt64(global.Instance.Client2)));
				int cnt=0;
				while (true)
				{
					//Thread.Sleep(10000);
					//var buffer = subscriber.Receive();
					Console.WriteLine("cnt :\t"+cnt);

					var buffer = sockClientListner.Receive();
					cnt++;
					Console.WriteLine("Recieved :"+ Encoding.ASCII.GetString(buffer.Skip(8).Take(buffer.Length).ToArray()));

					string msg=Encoding.ASCII.GetString(buffer.Skip(8).Take(buffer.Length).ToArray());

					if("" + Encoding.ASCII.GetString(buffer.Skip(8).Take(buffer.Length).ToArray()) == "upload")
					{

						//Console.WriteLine("2" + Encoding.ASCII.GetString(buffer.Skip(8).Take(buffer.Length).ToArray()));
						Program.Instance.send(BitConverter.ToInt64(buffer,0),msg);

						 
					
					}
			
					if("" + Encoding.ASCII.GetString(buffer.Skip(8).Take(buffer.Length).ToArray()) == "recieved")
					{

						//Console.WriteLine("2" + Encoding.ASCII.GetString(buffer.Skip(8).Take(buffer.Length).ToArray()));
						Program.Instance.send(BitConverter.ToInt64(buffer,0),msg);


					}
			    }
				Console.ReadLine(); 

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
			}

		}


		void send(Int64 str,string msg)
		{
			FileStream fs=null;
			NetworkStream ns=null;
			TcpClient tc=null;
			
			//RequestSocket _req=null;
			
			int data = 0;
				string HostName = global.Instance.SendClientConIp;
				int prt = Convert.ToInt32 (global.Instance.ClientPort_Send);
			try{
				Console.WriteLine("before TcpClient ");
				using ( tc = new TcpClient (HostName, prt))
				{
					Console.WriteLine("after TcpClient ");
					using(   ns = tc.GetStream ())
					{
						Console.WriteLine("after ns.GetStream ");
						string logsDirectory = Path.Combine (global.Instance.path, Convert.ToString (str - 100) + ".xml");
						Console.WriteLine ("Send Data to Client:");
						using ( fs = File.Open (logsDirectory, FileMode.Open))
						{
							data = fs.ReadByte ();
						while (data!=-1)
						{
						ns.WriteByte ((byte)data);
								data = fs.ReadByte ();
						}
							fs.Close ();
						}
				//fs.Close ();
					
						ns.Close();
					}//ns.Close ();
				//	tc.GetStream().Close();

	tc.Close();



				}//tc.Close ();
				Console.WriteLine("lAST ====");
			}
			catch(SocketException err)
			{
				Console.WriteLine ("exception :\t"+err.Message);
			}
		

		}
	}
}
