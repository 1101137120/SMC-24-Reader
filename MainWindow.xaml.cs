using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Forms;



using ComPort_Communication;
using SMC_24_Reader.Source;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace SMC_24_Reader
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        ComPort rs232 = new ComPort();
        ComPort rs233 = new ComPort();
        ComPort rs234 = new ComPort();
        private Timer rs232Timer = new Timer();
        private Timer autoReadTimer = new Timer();

 
        private List<Tag> list_Tag = new List<Tag>();

        private byte index = 0;
        private bool isSendTagRW_Command = false;

        
        string IP = "10.1.1.77";

        int aaa = 0;
        int[,] reader;
        int[,] readerIDs;
        int[,] readerIDs2;
        int[,] readerIDs3;
        int readerIDindex = 0;
        int readerID2index = 0;
        string comport;
        string comport1;
        string comport2;
        string port ="";
       int readerID3index = 0;
        byte[] byteArraydata;
        byte[] byteArraydata2;
        byte[] byteArraydata3;

        bool isConnectComPort = false;

        public MainWindow()
        {
            InitializeComponent();
            LoadComport();
            LoadComport2();
            LoadComport3();
            rs232Timer.Interval = 3500;
            rs232Timer.Tick += new EventHandler(rs232Timer_Tick);
            rs232Timer.Enabled = true;

            rs232.ComPort_ReceiverData_Event += new ComPort.ReceiveDataEnent(ComPort_ReceiverData_Event);
            rs233.ComPort_ReceiverData_Event += new ComPort.ReceiveDataEnent(ComPort_ReceiverData_Event);
            rs234.ComPort_ReceiverData_Event += new ComPort.ReceiveDataEnent(ComPort_ReceiverData_Event);
            autoReadTimer.Interval = Convert.ToInt32(textBoxPollingRate.Text);
            autoReadTimer.Tick += new EventHandler(autoReadTagTimer_Tick);
            autoReadTimer.Enabled = true;// Start the timer

            reader = getReaders(99);
            readerIDs = getReaders(1);
            readerIDs2 =getReaders(2);
            readerIDs3 = getReaders(3);
            Console.WriteLine(readerIDs);
        }


#region Source
       
       //取得Comport
        private void LoadComport()
        {
            List<string> port = rs232.Load_AllComPortName();
            string s = cb_ComPort.Text;
            cb_ComPort.Items.Clear();
           

            int index = 0;
            for (int i = 0; i < port.Count; i++)
            {
                cb_ComPort.Items.Add(port[i]);
                if (port[i] == s) index = i;
            }

            cb_ComPort.SelectedIndex = index;
            comport = cb_ComPort.Text;
            System.Diagnostics.Debug.WriteLine(comport);
        }

        private void LoadComport2()
        {
            List<string> port = rs233.Load_AllComPortName();
            string s = cb_ComPort_Copy.Text;
            cb_ComPort_Copy.Items.Clear();
            

            int index = 0;
            for (int i = 0; i < port.Count; i++)
            {
                cb_ComPort_Copy.Items.Add(port[i]);
                if (port[i] == s) index = i;
            }

            cb_ComPort_Copy.SelectedIndex = index;
            comport1 = cb_ComPort_Copy.Text;
            System.Diagnostics.Debug.WriteLine(comport1);
        }
        private void LoadComport3()
        {
            List<string> port = rs234.Load_AllComPortName();
            string s = cb_ComPort_Copy1.Text;
            cb_ComPort_Copy1.Items.Clear();
            

            int index = 0;
            for (int i = 0; i < port.Count; i++)
            {
                cb_ComPort_Copy1.Items.Add(port[i]);
                if (port[i] == s) index = i;
            }

            cb_ComPort_Copy1.SelectedIndex = index;
            comport2 = cb_ComPort_Copy1.Text;
            System.Diagnostics.Debug.WriteLine(comport2);
        }



        private void Show_RW_Tag(string s)
        {
           
            /*if (s.IndexOf("Got", StringComparison.OrdinalIgnoreCase) >= 0)
             {*/
            string[] ss = tb_RW_Tag.Text.Split('\n');
                if (ss.Count() > 32) tb_RW_Tag.Text = "";
               

                tb_RW_Tag.Text += (s + "\n");
          // }
        }


        private void Show_RW_Tag_Test(string readID,int tagRSSI, string tagId,string time,string port)
        {

            /*if (s.IndexOf("Got", StringComparison.OrdinalIgnoreCase) >= 0)
             {*/
            string[] ss = tb_RW_Tag.Text.Split('\n');
            if (ss.Count() > 32) tb_RW_Tag.Text = "";


            tb_RW_Tag.Text += ("ID:"+ tagId + "reader:" + readID + "RSSI:" + tagRSSI+"Time:"+ time + "PORT:" + port + "\n");
            // }
        }

        private void LoadTagIdToListView(string id) {
            int time = (Convert.ToInt32(cb_TagLostTime.Text, 10)*20);
            foreach (Tag t in list_Tag)
            {
                if (t.id == id) {
                    t.time = time;
                    return;
                }
            }

            Tag tg = new Source.Tag();
            tg.id = id;
            tg.time = time;
            list_Tag.Add(tg);
            lv_ListTag.Items.Add(id);
        }
        //更新讀取到的tag列表
        private void CheckLostTag() {
            Tag t;
            for (int j = (list_Tag.Count - 1); j>-1;j-- )
            {
                t = list_Tag[j];
                t.time--;
                if (t.time == 0)
                {
                    for (int i = (lv_ListTag.Items.Count - 1); i > -1; i--)
                    {
                        if (lv_ListTag.Items[i].ToString() == t.id)
                        {
                            lv_ListTag.Items.RemoveAt(i);
                            break;
                        }
                    }
                    list_Tag.RemoveAt(j);
                }
            }
        }

#endregion

#region Event
        private void ComPort_ReceiverData_Event(List<byte> packet)
        {
            List<string> port = rs232.Load_AllComPortName();
            isSendTagRW_Command = false;
            ProtocolAck pa = new ProtocolAck();
            Show s = pa.GotReaderAck(packet);
            string pp = "";
            int aa=reader.GetUpperBound(0) + 1;
            System.Diagnostics.Debug.WriteLine(s.mode);
            for (var i = 0; i < aa; i++) {
                string read = reader[i,0].ToString();
                int readactive = reader[i, 1];
                if (read==s.readID) {
                    if (readactive == 1)
                    {
                        System.Diagnostics.Debug.WriteLine("111111");
                        pp = "第一區段";
                    }
                    else if(readactive == 2)
                    {
                        System.Diagnostics.Debug.WriteLine("222222");
                        pp = "第二區段";
                    }
                    else if (readactive == 3)
                    {
                        System.Diagnostics.Debug.WriteLine("333333");
                        pp = "第三區段";
                    }
                }
            }

            // Show_RW_Tag(s.show); //顯示讀取到的資料
            if (s.tagId != "") { 
                Show_RW_Tag_Test(s.readID,s.tagRSSI,s.tagId,DateTime.Now.ToString("HH:mm ss tt"), pp);
                }
            if (s.tagId != "")
           {
                LoadTagIdToListView(s.tagId);
                //s.readID = 讀頭的ID 為 string
                //s.tagIds = Tag上面的ID序號 為 string
                //s.tagRSSI= Tag的距離 為 int
           
                sendToChannel(s.readID, "should fetch from table", s.tagId, s.tagRSSI + "", "0");
               // sendTimeLines(s.readID, 1, s.tagRSSI+"", s.tagId);
                sendTimeLinesToday(s.readID, 1, s.tagRSSI + "", s.tagId);
           }
            else
            {
                //sendToChannel(s.readID, "沒有讀到卡片", "無", "無", "0");
               // sendTimeLines(s.readID, 1, "0", null);
                //sendTimeLinesToday(s.readID, 1, "0", null);
               Console.WriteLine("沒有讀到TAG之Reader ID:" + s.readID);
           }
        }
        //當re232有變化的時候這邊會接收到
        private void rs232Timer_Tick(object sender, EventArgs e)
        {

            Console.WriteLine("CHANGE");
            if (rs232.Check_ComPort_LinkStatus() == false)
            {


                if (rs232.Open_ComPort(cb_ComPort.Text, cb_Baudrate.Text) == true)
                {
                    lb_LinkStatus.Content = "Link OK !";
                    lb_LinkStatus.Foreground = Brushes.Green;
                    isConnectComPort = true;
                }
                else {
                    lb_LinkStatus.Content = "Link Fail !";
                    lb_LinkStatus.Foreground = Brushes.Red;
                    isConnectComPort = false;
                }     
            }
            if (rs233.Check_ComPort_LinkStatus() == false) {
                if (rs233.Open_ComPort(cb_ComPort_Copy.Text, cb_Baudrate.Text) == true)
                {
                    lb_LinkStatus_Copy.Content = "Link OK !";
                    lb_LinkStatus_Copy.Foreground = Brushes.Green;
                    isConnectComPort = true;

                }
                else {
                    lb_LinkStatus_Copy.Content = "Link Fail !";
                    lb_LinkStatus_Copy.Foreground = Brushes.Red;
                    isConnectComPort = false;
                }
            }

            if (rs234.Check_ComPort_LinkStatus() == false)
            {
                if (rs234.Open_ComPort(cb_ComPort_Copy1.Text, cb_Baudrate.Text) == true)
                {
                    lb_LinkStatus_Copy1.Content = "Link OK !";
                    lb_LinkStatus_Copy1.Foreground = Brushes.Green;
                    isConnectComPort = true;

                }
                else
                {
                    lb_LinkStatus_Copy1.Content = "Link Fail !";
                    lb_LinkStatus_Copy1.Foreground = Brushes.Red;
                    isConnectComPort = false;
                }
            }
        }
        private void autoReadTagTimer_Tick(object sender, EventArgs e)
        {
            if (isSendTagRW_Command == false) {
                if (chb_IsAutoReadTag.IsChecked == true) {
                    CheckLostTag();
                    Protocol protocol = new Protocol();
                    if (readerIDs != null) {
                       
                        Show sp = protocol.ReadTagIdBuffer((byte)readerIDs[readerIDindex,0]);  //設定從資料庫抓回來的reader id
                    
                           
                        // Show sp = protocol.ReadTagIdBuffer(list_ReaderId[cb_ReaderId.SelectedIndex].value);
                      if (readerIDindex != readerIDs.GetUpperBound(0) + 1 - 1) //判斷目前執行的id是不是跟抓回來的一樣多，一樣多的話就從頭開始
                    {
                        readerIDindex++;
                           
                        }
                    else
                    {
                        readerIDindex = 0;
                           
                        }
                        

                      //  Show_RW_Tag(sp.show+"KKKKK");
                     
                       // port = cb_ComPort.Text;
                        rs232.Send_Packet(sp.packet.ToArray());
                        }

                    if (readerIDs2 != null)
                        {
                            Show sp2 = protocol.ReadTagIdBuffer((byte)readerIDs2[readerID2index,0]);
                            if (readerID2index != readerIDs2.GetUpperBound(0) + 1 - 1) //判斷目前執行的id是不是跟抓回來的一樣多，一樣多的話就從頭開始
                            {
                                readerID2index++;

                            }
                            else
                            {
                                readerID2index = 0;

                            }
                            //Show_RW_Tag(sp2.show + "SSSSS" );
                            //port = cb_ComPort_Copy.Text;
                            rs233.Send_Packet(sp2.packet.ToArray());
                            }


                    if (readerIDs3 != null)
                            {
                                Show sp3 = protocol.ReadTagIdBuffer((byte)readerIDs3[readerID3index,0]);
                                if (readerID3index != readerIDs3.GetUpperBound(0) + 1 - 1) //判斷目前執行的id是不是跟抓回來的一樣多，一樣多的話就從頭開始
                                {
                                    readerID3index++;

                                }
                                else
                                {
                                    readerID3index = 0;

                                }
                               // Show_RW_Tag(sp3.show + "AAAAA");
                               // port = cb_ComPort_Copy1.Text;
                                rs234.Send_Packet(sp3.packet.ToArray());
                                
                            }


                   
                     
                        else
                    {
                        Show_RW_Tag("ID讀取失敗");
                    }
                }
            }
        }

        private void SelectComPortChanged(object sender, EventArgs e)
        {
            lb_LinkStatus.Content = "Start Link !";
            lb_LinkStatus.Foreground = Brushes.Red;

            rs232.ClosedRs232Port();
        }
        private void SelectComPortChanged_Copy(object sender, EventArgs e)
        {
            lb_LinkStatus_Copy.Content = "Start Link !";
            lb_LinkStatus_Copy.Foreground = Brushes.Red;

            rs233.ClosedRs232Port();
        }

        private void SelectComPortChanged_Copy1(object sender, EventArgs e)
        {
            lb_LinkStatus_Copy1.Content = "Start Link !";
            lb_LinkStatus_Copy1.Foreground = Brushes.Red;

            rs234.ClosedRs232Port();
        }

        private void SelectComPortStarted(object sender, EventArgs e)
        {
            LoadComport();
        }
        private void SelectComPortStarted_Copy(object sender, EventArgs e)
        {
             LoadComport2();
        }
        private void SelectComPortStarted_Copy1(object sender, EventArgs e)
        {
            LoadComport3();
        }

        private void SelectBaudrateChanged(object sender, EventArgs e)
        {
            lb_LinkStatus.Content = "Start Link !";
            lb_LinkStatus.Foreground = Brushes.Red;

            rs232.ClosedRs232Port();
            rs233.ClosedRs232Port();
            rs234.ClosedRs232Port();
        }

        //--------  按鈕
        private void bt_ReadTagIdBuffer_Click(object sender, RoutedEventArgs e)
        {


            Protocol protocol = new Protocol();
            Show sp = protocol.ReadTagIdBuffer(0); // 初始0

            foreach(byte a in sp.packet.ToArray())
            { 
            Console.WriteLine("sp.packet.ToArray()"+ a);
            }
            Show_RW_Tag(sp.show);
            rs232.Send_Packet(sp.packet.ToArray());
        }
       
        private void bt_Clear_RWTag_Click(object sender, RoutedEventArgs e)
        {
            tb_RW_Tag.Text = "";
        }
      
       
        private void bt_ClearListTag_Click(object sender, RoutedEventArgs e)
        {
            lv_ListTag.Items.Clear();
            list_Tag.Clear();
        }


        #endregion

        private void SelectTagChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lv_ListTag.Items.Count > 0)
                {
                    string t = lv_ListTag.SelectedValue.ToString();
                  //  lb_SelectTagId.Content = t;
                }
            }
            catch
            {

            }
        }



 #region DataBase
        //取得資料庫內所有的Reader ID
        int[,] getReaders(int num)
        {
            try
            {
                System.GC.Collect();

                string ServerUrl = "http://" + IP + ":9004/2.4/v1/readers";

                WebRequest request = WebRequest.Create(ServerUrl);
                request.Method = "GET";
                request.Timeout = 10000;

                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                var result = reader.ReadToEnd();
                
                dynamic readers = JsonConvert.DeserializeObject(result);
                //int[] readerIDs2 = new int[readers.response.Count];
                int[,] readerIDsaa = new int[readers.response.Count,2];
                int c = 0;
                int a = 0;
                for (int i = 0; i < readers.response.Count; i++)
                {
                    if (num == 99) {
                        readerIDsaa[a,0] =readers.response[i].reader_name;
                        readerIDsaa[a,1] = readers.response[i].active;
                        a = a + 1;
                    }
                   else  if (readers.response[i].active == num)
                    {
                        Console.WriteLine("ID:" + readers.response[i].reader_name + "F");
                        readerIDsaa[a,0] = readers.response[i].reader_name;
                        readerIDsaa[a,1] = readers.response[i].active;
                        a = a + 1;
                    }
                }

                request.Abort();
                response.Close();
                stream.Close();
                return readerIDsaa;
                
                   

            }
            catch (WebException Except)
            {
                Console.WriteLine("error");
                Console.WriteLine(Except);
                return null;
            }
        }




        //===================  傳給資料庫 ======================
        private void sendTimeLines(string reader_name, int is_read, string strength, string tag_uid)
        {
            try
            {
                string url = "http://" + IP + ":9004/2.4/v1/time_lines";

                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = 1000;

                //string postData = "group_code=" + group + "&run_code=" + run + "&reader_name=" + reader_name + "&is_read=" + is_read;
                string postData = "reader_name=" + reader_name + "&is_read=" + is_read + "&strength=" + strength + "&tag_uid=" + tag_uid;
                byteArraydata = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), request);
            }
            catch (WebException Except)
            {
                Console.WriteLine("error");
                Console.WriteLine(Except);
            }



        }
       
        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            request.Timeout = 1000;
            Stream postStream = request.EndGetRequestStream(asynchronousResult);
            postStream.Write(byteArraydata, 0, byteArraydata.Length);
            postStream.Close();
        }

        private void sendTimeLinesTodayGetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            request.Timeout = 1000;
            Stream postStream = request.EndGetRequestStream(asynchronousResult);
            postStream.Write(byteArraydata2, 0, byteArraydata2.Length);
            postStream.Close();
        }
        private void sendToChannelGetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            request.Timeout = 1000;
            Stream postStream = request.EndGetRequestStream(asynchronousResult);
            postStream.Write(byteArraydata3, 0, byteArraydata3.Length);
            postStream.Close();
        }

        private void sendTimeLinesToday(string reader_name, int is_read, string strength, string tag_uid)
        {
           try
            {
                string url = "http://" + IP + ":9004/2.4/v1/time_lines_today";

                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = 1000;

                //string postData = "group_code=" + group + "&run_code=" + run + "&reader_name=" + reader_name + "&is_read=" + is_read;
                string postData = "reader_name=" + reader_name + "&is_read=" + is_read + "&strength=" + strength + "&tag_uid=" + tag_uid;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                byteArraydata2 = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                request.BeginGetRequestStream(new AsyncCallback(sendTimeLinesTodayGetRequestStreamCallback), request);
            }
            catch (WebException Except)
            {
                Console.WriteLine("error");
                Console.WriteLine(Except);
            }



        }
        private void sendToChannel(string reader_name, string tag_name, string tag_uid, string strength, string wrong_packet)
        {
            try
            {
                
                string url = "http://" + IP + ":9004/2.4/v1/channel";
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = 1000;
                string postData = "reader_name=" + reader_name + "&tag_name=" + tag_name + "&tag_uid=" + tag_uid + "&strength=" + strength + "&wrong_packet=" + wrong_packet;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                byteArraydata3 = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                request.BeginGetRequestStream(new AsyncCallback(sendToChannelGetRequestStreamCallback), request);
            }
            catch (WebException Except)
            {
                Console.WriteLine("error");
                Console.WriteLine(Except);
            }



        }
        public string ConvertString(string value, int fromBase, int toBase)
        {
            int intValue = Convert.ToInt32(value, fromBase);
            return Convert.ToString(intValue, toBase);

        }
        //===================  傳給資料庫 end ======================
#endregion

        //更新輪尋的時間
        private void setPollingTime_Click(object sender, RoutedEventArgs e)
        {
            autoReadTimer.Enabled = false; //stop
            autoReadTimer.Interval = Convert.ToInt32(textBoxPollingRate.Text);
            autoReadTimer.Enabled = true; //start
        }
    }
}





