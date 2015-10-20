using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AMS.Profile;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Net.Sockets;



using NNanomsg.Protocols;

namespace Client
{

    public partial class frmLogin : Form
    {
        public delegate void testDelegate(string str);
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public void btnConnect_Click(object sender, EventArgs e)
        {
            
          //  if (cbDownHistory.Checked==true)
          //  {

               // BeginInvoke((Action)delegate
              //  {
                    ///////////////////////  \\  // 
                   // Upload();
              //  });
          //  }
          
                Global.Instance.NNFPassword = txtPassword.Text;
                List<Task> tasks = new List<Task>();
                NNFInOut.Instance.SIGN_ON_REQUEST_IN();
                
    Thread.Sleep(5000);
 // Global.Instance.SignInStatus = true;
           // UDP_Reciever.Instance.UDPReciever(); 
            if (Global.Instance.SignInStatus)
                {
                    this.DialogResult = DialogResult.OK;


                    if (cbDownHistory.CheckState == CheckState.Checked)
                    {
                        //  StartDownload();

                        Thread t = new Thread(new ThreadStart(ListenData));
                        t.Start();


                        Upload();
                        
           
                        //  File.Copy(@"\\192.168.168.36\share\vSphere\vSphere.exe", "E:\\abc.xml", true);
                    }

                  //  Thread.Sleep(1000);
                    //t.Abort();
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                                      
                }

                

            
        }

        private void StartDownload(int Section = 0, string FileType = "")
        {

            pBarProgress.Value = 0;
            string remoteUri = "http://192.168.168.97:5252/files/";
            string fileName = "001.xml", myStringWebResource = null;
            // Create a new WebClient instance.
            WebClient myWebClient = new WebClient();
          //  myWebClient.DownloadFileCompleted += Completed;
            myWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            myWebClient.DownloadProgressChanged += ProgressChanged;
            myWebClient.DownloadFileCompleted += Completed;           
            myStringWebResource = remoteUri + fileName;
            pBarProgress.Value = 1;
            myWebClient.DownloadFile(myStringWebResource, fileName);
            pBarProgress.Value = 100;
         
            return;

            #region  comment
            //String FilePath = "";
            //if (Section == 0)
            //{
            //    FilePath = IniFIle.IniReadValue("DOWNLOAD", "SERVER").Trim();
            //}
            //else
            //{
            //    FilePath = IniFIle.IniReadValue("DOWNLOAD", "HISTORY").Trim() + "//" +
            //               DateTime.Today.ToString("yyyy//MM//dd//") + Section + "//" + Section + FileType + ".csv";
            //}

            //if (FilePath.Length > 0)
            //{
             //   var webClient = new WebClient();
            
                //frmdown = new frmDownloadProgress();
               // frmdown.lbl_ItemName.Text = "Contract files :";
                //webClient.DownloadFileCompleted += Completed;
                //  webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
              //  webClient.DownloadProgressChanged += ProgressChanged;
             ///   webClient.DownloadFileCompleted += Completed;
             ///   AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
             //   NetworkCredential myCredentials = new NetworkCredential("other", "123456");

             //   WindowsIdentity idnt = new WindowsIdentity("other", "123456");

               // WindowsImpersonationContext context = idnt.Impersonate();
            //    File.Copy(@"\\192.168.168.97\share e\contract.txt", "E:\\abc211.txt", true);




            // File.Copy("E:\\02022015\\001.xml", "E:\\abc.xml", true);
            #endregion

        }
          public void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            pBarProgress.Value = e.ProgressPercentage;
        }

        public void Completed(object sender, AsyncCompletedEventArgs e)
        {
            Close();
        }
    
/*
        private void frmLogin_Load(object sender, EventArgs e)
        {


     //       EditorFontData configData =  new EditorFontData();

            EditorFontData configData = (EditorFontData)ConfigurationManager.GetSection("EditorSettings");

            var v = configData.Style;

            configData.Name = "Arial";
            configData.Size = 20;
            configData.Style = 2;

            Configuration config =ConfigurationManager.OpenExeConfiguration( ConfigurationUserLevel.None);


            // You need to remove the old settings object before you can replace it
            config.Sections.Remove("EditorSettings");
            // with an updated one
            config.Sections.Add("EditorSettings", configData);
            // Write the new configuration data to the XML file
            config.Save();


            var v2 = configData.Style;


            // Get the application configuration file.
     //       System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Get the collection of the section groups.
       //     ConfigurationSectionGroupCollection sectionGroups = config.SectionGroups;

            // Show the configuration values
     //       ShowSectionGroupCollectionInfo(sectionGroups);






                foreach (string key in ConfigurationManager.AppSettings)
                {
                    string value = ConfigurationManager.AppSettings[key];
                    Console.WriteLine("Key: {0}, Value: {1}", key, value);
                }
                string fval = System.Configuration.ConfigurationManager.AppSettings["DataConIp"].ToString();
              
          


        }



        static void ShowSectionGroupCollectionInfo( ConfigurationSectionGroupCollection sectionGroups)
        {
            ClientSettingsSection clientSection;
            SettingValueElement value;

            foreach (ConfigurationSectionGroup group in sectionGroups)
            // Loop over all groups
            {
                if (!group.IsDeclared)
                    // Only the ones which are actually defined in app.config
                    continue;

                Console.WriteLine("Group {0}", group.Name);

                // get all sections inside group
                foreach (ConfigurationSection section in group.Sections)
                {
                    clientSection = section as ClientSettingsSection;
                    Console.WriteLine("\tSection: {0}", section);

                    if (clientSection == null)
                        continue;


                    foreach (SettingElement set in clientSection.Settings)
                    {
                        value = set.Value as SettingValueElement;
                        // print out value of each section
                        Console.WriteLine("\t\t{0}: {1}",
                        set.Name, value.ValueXml.InnerText);
                    }
                }
            }
        }
    */

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //Thread t = new Thread(new ThreadStart(ListenData));
            //t.Start();
           
            //if (Type_Client.Text.ToUpper() == "IOC".ToUpper())
            //{
            //    Global.Instance.Client_Type = true;
            //}
            //else
            //{
            //    Global.Instance.Client_Type = false;
            //}
            Thread myth = new System.Threading.Thread(delegate()
            {
                csv.CSV_Class.contract_fun();
            });
            myth.Start();
          
            var config = new Config { GroupName = null };
            tbServerIP.Text = config.GetValue("appSettings", "DataConIp", null);
            tbdataport.Text = config.GetValue("appSettings", "DataConSUBPort", null);

            tbOrderIp.Text = config.GetValue("appSettings", "NNFConIp", null);
            tbPort.Text = config.GetValue("appSettings", "NNFConSUBPort", null);

            txtUserId.Text = config.GetValue("appSettings", "ClientId", null);
            // txtPassword.Text = config.GetValue("Profile", "NNFPassword", null);
          
            Global.Instance.NNFPassword = txtPassword.Text;
            txtPassword.Focus();
            // if avoid  check soket telnet yhen please assign _bval ==  false  
            bool _bval = true;
          
            if (_bval == true)
                return;
            TcpClient tcpSocket = null;
            try
            {
                 tcpSocket = new TcpClient(tbOrderIp.Text, Convert.ToInt32(tbPort.Text));           
            }
            catch (SocketException SE)
            {
                MessageBox.Show("Message~~> " + SE.Message, "Server is Stopped" , MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Dispose();
                Environment.Exit(0);
            }
            finally
            {
               tcpSocket.Close();
            }
           


        }

            private void txtPassword_KeyDown(object sender, KeyEventArgs e)
            {
                 btnConnect.Enabled = true;
                 if (e.KeyCode == Keys.Enter)
                 {
                     btnConnect_Click(sender, e);
                 }
            }


            private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
            {
               //Global.Instance.warningvar = false;
               // if(Global.Instance.warningvar ==false)

            }

            private void label2_Click(object sender, EventArgs e)
            {

            }



            //==========================================================================Donl=============================================
            #region download
         
             
        


            void Upload()
            {


                PublishSocket PubClient = new PublishSocket();
                PubClient.Bind("tcp://" + Global.Instance.LanIp + ":" + "8001");
                Thread.Sleep(10000);


                // label1.Text = "Come";
                string sp = "upload";
              //  Console.WriteLine(sp.Length);

                var intBytes = BitConverter.GetBytes(Convert.ToInt64(Global.Instance.ClientId));
                var buff = intBytes.Concat(Encoding.ASCII.GetBytes(sp)).ToArray();
               
                PubClient.Send(buff);
                PubClient.Dispose();
                //  Console.WriteLine(sp);



            }
            public void ListenData()
            {
                IPAddress ipad = IPAddress.Parse(Global.Instance.LanIp);
                Int32 prt = 2112;
                TcpListener tl = new TcpListener(ipad, prt);
                tl.Start();

                TcpClient tc = tl.AcceptTcpClient();

                NetworkStream ns = tc.GetStream();
                StreamReader sr = new StreamReader(ns);

                string result = sr.ReadToEnd();
                //Invoke(new testDelegate(test), new object[] { result });
               test(result);
                tc.Close();
                tl.Stop();

            }


            public void test(string str)
            {
                  try
                    {
                        StringReader theReader = new StringReader(str);
                        DataSet theDataSet = new DataSet();
                        theDataSet.ReadXml(theReader);

                        DataTable dt = theDataSet.Tables[0];

                        //  dataGridView1.DataSource = dt;

                        Global.Instance.OrdetTable = dt;

                        frmTradeBook.Instance.load_data();
                        frmTradeBook.Instance.lblnooftrade.Text = "No Of Trade  =" + frmTradeBook.Instance.DGV.Rows.Count;
                        frmTradeBook.Instance.lblb_V.Text = Global.Instance.OrdetTable.AsEnumerable().Where(r => r.Field<string>("Status") == "Traded" && r.Field<string>("Buy_SellIndicator") == "BUY").Sum(r => Convert.ToDouble(r.Field<string>("FillPrice")) * Convert.ToDouble(r.Field<string>("Volume"))).ToString();
                        frmTradeBook.Instance.lbls_v.Text = Global.Instance.OrdetTable.AsEnumerable().Where(r => r.Field<string>("Status") == "Traded" && r.Field<string>("Buy_SellIndicator") == "SELL").Sum(r => Convert.ToDouble(r.Field<string>("FillPrice")) * Convert.ToDouble(r.Field<string>("Volume"))).ToString();
                        frmTradeBook.Instance.lblb_q.Text = Global.Instance.OrdetTable.AsEnumerable().Where(r => r.Field<string>("Status") == "Traded" && r.Field<string>("Buy_SellIndicator") == "BUY").Sum(r => Convert.ToDouble(r.Field<string>("Volume"))).ToString();
                        frmTradeBook.Instance.lbls_q.Text = Global.Instance.OrdetTable.AsEnumerable().Where(r => r.Field<string>("Status") == "Traded" && r.Field<string>("Buy_SellIndicator") == "SELL").Sum(r => Convert.ToDouble(r.Field<string>("Volume"))).ToString();
                        frmTradeBook.Instance.lbln_v.Text = (Convert.ToDouble(frmTradeBook.Instance.lbls_v.Text) - Convert.ToDouble(frmTradeBook.Instance.lblb_V.Text)).ToString();
                        frmNetBook.Instance.netposion2(0, 0, 0, 0, 0);
                        frmTradeBook.Instance.DGV.Refresh();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Excel Not Loaded " + ex.Message);
                    }
            
                //  textBox1.Text = str;
            }
            #endregion

            private void cbDownHistory_CheckedChanged(object sender, EventArgs e)
            {
                if (cbDownHistory.CheckState == CheckState.Checked)
                {
                    //  StartDownload();
                 //   Upload();
                    //  File.Copy(@"\\192.168.168.36\share\vSphere\vSphere.exe", "E:\\abc.xml", true);
                }
            }



        //==========================================================

    }
}
