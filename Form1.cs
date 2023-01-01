using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Xml.Schema;
using ZedGraph;
using System.Security.Cryptography;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Security.Policy;
using System.Xml.Linq;
using System.Runtime.InteropServices;

namespace lora1
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            /* String file = @"C:\Users\Admin\Desktop\Output.csv";
             String separator = ",";
             StringBuilder output = new StringBuilder();
             String[] headings = { "StudentID", "Irradiance", "Datetime"};
             output.AppendLine(string.Join(separator, headings));
             File.AppendAllText(file, output.ToString());*/

            // KHAI BAO DO THI IRRADIANCE
            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.Title.Text = "Đồ thị bức xạ mặt trời theo thời gian thực";
            myPane.XAxis.Title.Text = "Thời gian";
            myPane.YAxis.Title.Text = "Bức xạ (W/m2)";

            RollingPointPairList list1 = new RollingPointPairList(60000);
            RollingPointPairList list2 = new RollingPointPairList(60000);
            RollingPointPairList list3 = new RollingPointPairList(60000);
            RollingPointPairList list4 = new RollingPointPairList(60000);
            RollingPointPairList list5 = new RollingPointPairList(60000);
            RollingPointPairList list6 = new RollingPointPairList(60000);


            LineItem irrNode1 = myPane.AddCurve("Node 1", list1, Color.Red, SymbolType.None); // 0
            LineItem irrNode2 = myPane.AddCurve("Node 2", list2, Color.Blue, SymbolType.None); // 1
            LineItem iscNode1 = myPane.AddCurve("Node 1", list3, Color.YellowGreen, SymbolType.None);//2
            LineItem iscNode2 = myPane.AddCurve("Node 2", list4, Color.Purple, SymbolType.None);//3
            LineItem temNode1 = myPane.AddCurve("Node 1", list5, Color.Green, SymbolType.None);//4
            LineItem temNode2 = myPane.AddCurve("Node 2", list6, Color.Orange, SymbolType.None);
            myPane.YAxis.Scale.Min = 0;
            myPane.YAxis.Scale.Max = 100;
            myPane.YAxis.Scale.MajorStep = 10;
            myPane.YAxis.Scale.MinorStep = 1;
            myPane.XAxis.Type = AxisType.Date;
            myPane.XAxis.Scale.Format = "HH:mm:ss";
            myPane.XAxis.Scale.FontSpec.Angle = 60;
            myPane.XAxis.Scale.FontSpec.Size = 12;
            myPane.XAxis.Scale.MajorUnit = DateUnit.Second;
            myPane.XAxis.Scale.MajorStep = 10;
            myPane.XAxis.Scale.MinorUnit = DateUnit.Second;
            myPane.XAxis.Scale.MinorStep = 1;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cbox.Items.AddRange(ports);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e) // BAM NUT "Open"
        {
            btnClose.Enabled = true;
            btnOpen.Enabled = false;
            try
            {
                // KHAI BAO THONG SO CHO CONG COM
                serialPort1.PortName = cbox.Text;
                serialPort1.BaudRate = 9600;
                serialPort1.Parity = Parity.None;
                serialPort1.DataBits = 8;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Open();

                // SET TIMEOUT
                //serialPort1.ReadTimeout = 10000;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Thread thrd = new Thread(threadReceive); // KHOI TAO LUONG ( THREAD ) NHAN DU LIEU
            Thread check = new Thread(threadChecklist); // KHOI TAO LUONG ( THREAD ) CHECK TRANG THAI 
            thrd.Start();
            check.Start();
            for (int i = 0; i < checkBox.Items.Count; i++)
            {
                checkBox.SetItemChecked(i, true);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) //  BAM NUT "Close"
        {
            btnClose.Enabled = false;
            btnOpen.Enabled = true;
            try
            {
                serialPort1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) // TAT UNG DUNG
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }

        private void txtRecieve_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void btnRecieve_Click(object sender, EventArgs e)
        {


        }

            
        public void threadReceive()
        {
            int ID;
            while (true)
            {
                Thread.Sleep(1000);
                // byte[] send1 = { 0x7A, 0x1, 0x7F }, send2 = { 0x7A, 0x02, 0x7F };
                // serialPort1.Write(send1,0,3);
                while (serialPort1.BytesToRead == 0); 
                if(serialPort1.BytesToRead > 0)
                {
                    byte[] data1 = new byte[serialPort1.BytesToRead];
                    //txtRecieve.Text = Convert.ToString(serialPort1.BytesToRead);
                    serialPort1.Read(data1, 0, data1.Length); // NHAN DU LIEU TU LORA 1
                    if (data1[0] == 0x1A && data1[16] == 0x1F && Convert.ToInt16(data1[1]) == 17)
                    {
                        // XU LY TIN HIEU
                        byte[] irr = new byte[4], tem = new byte[4], cur = new byte[4];
                        ID = Convert.ToInt16(data1[2]);
                        for (int i = 0; i <= 3; i++)
                        {
                            irr[i] = data1[i + 3];
                            cur[i] = data1[i + 7];
                            tem[i] = data1[i + 11];
                        }
                        float Irr = floatConversion(irr);
                        float Tem = floatConversion(tem);
                        float Isc = floatConversion(cur);
                        addCSV(ID, Irr, Isc, Tem);
                        Draw(ID, Irr, Isc, Tem); // THEM DIEM VAO DO THI 
                        txtRecieve.Text = Irr.ToString() + " | " + Isc.ToString() + " | " + Tem.ToString() + " | " + Convert.ToString(DateTime.Now);
                        /* while(serialPort1.BytesToRead != 0) ;
                         serialPort1.Write(send2,0,3); // GUI DU LIEU DEN LORA 2
                         while (serialPort1.BytesToRead == 0) ;
                         Thread.Sleep(1);
                         byte[] data2 = new byte[serialPort1.BytesToRead];
                         serialPort1.Read(data2,0, data2.Length); // NHAN DU LIEU TU LORA 2 */


                    }
                }
               

                // THEM DU LIEU VAO DO THI




            }
            // VE DO THI
        }

        public void threadChecklist()   // BAT TAT LINE DO THI
        {
            while (true)
            {

                if (typeGraph.Text == "Irradiance")
                {
                    LineItem node1, node2;
                    node1 = zedGraphControl1.GraphPane.CurveList[0] as LineItem;
                    node2 = zedGraphControl1.GraphPane.CurveList[1] as LineItem;
                    checknode(node1, 1);
                    checknode(node2, 2);
                }
                else if (typeGraph.Text == "Short Current")
                {
                    LineItem node1, node2;
                    node1 = zedGraphControl1.GraphPane.CurveList[2] as LineItem;
                    node2 = zedGraphControl1.GraphPane.CurveList[3] as LineItem;
                    checknode(node1, 1);
                    checknode(node2, 2);
                }
                else if (typeGraph.Text == "Temperature")
                {
                    LineItem node1, node2;
                    node1 = zedGraphControl1.GraphPane.CurveList[4] as LineItem;
                    node2 = zedGraphControl1.GraphPane.CurveList[5] as LineItem;
                    checknode(node1, 1);
                    checknode(node2, 2);
                }
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txtSend_TextChanged(object sender, EventArgs e)
        {

        }



        public static string ByteArrayToString(byte[] ba) // CHUYEN DOI BYTE SANG STRING
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
        public float floatConversion(byte[] bytes) // CHUYEN DOI BYTE SANG FLOAT
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes); // Convert big endian to big endian
            }
            float myFloat = BitConverter.ToSingle(bytes, 0);
            return myFloat;
        }
        public void Draw(int idNode, float irr, float isc, float tem) // THEM DIEM VAO DO THI a-irr | b-isc | c-tem
        {
            if (zedGraphControl1.GraphPane.CurveList.Count <= 0) return;
            LineItem ira = zedGraphControl1.GraphPane.CurveList[idNode - 1] as LineItem;
            LineItem curr = zedGraphControl1.GraphPane.CurveList[idNode + 1] as LineItem;
            LineItem temp = zedGraphControl1.GraphPane.CurveList[idNode + 3] as LineItem;
            if (ira == null) return;
            IPointListEdit list1 = ira.Points as IPointListEdit;
            IPointListEdit list3 = curr.Points as IPointListEdit;
            IPointListEdit list5 = temp.Points as IPointListEdit;

            if (list1 == null) return;
            list1.Add(new XDate(DateTime.Now), irr);
            list3.Add(new XDate(DateTime.Now), isc);
            list5.Add(new XDate(DateTime.Now), tem);

            //Tu dong scale theo thoi gian
            Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
            Scale yScale = zedGraphControl1.GraphPane.YAxis.Scale;

            xScale.Min = DateTime.Now.Subtract(new TimeSpan(0, 0, 1, 1, 0)).ToOADate();
            xScale.Max = DateTime.Now.ToOADate();


            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            zedGraphControl1.Refresh();
        }
        public void addCSV(int idNode, float irra, float isc, float tem) // THEM DU LIEU VAO CSV FILE
        {
            String separator = ",";
            StringBuilder output = new StringBuilder();
            String file = @"C:\Users\Admin\Desktop\Node1.csv";
            String[] newLine = { idNode.ToString(), Convert.ToString(irra), Convert.ToString(isc), Convert.ToString(tem), Convert.ToString(DateTime.Now) };
            output.AppendLine(string.Join(separator, newLine));
            File.AppendAllText(file, output.ToString());
        }

        public void checknode(LineItem node, int i) // i la vi tri cua node tren giao dien
        {
            if (checkBox.GetItemCheckState(i - 1) == 0)
            {

                node.IsVisible = false;
            zedGraphControl1.Invalidate();
            zedGraphControl1.AxisChange();
                zedGraphControl1.Refresh();
            }
            else
            {
                node.IsVisible = true;
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
                zedGraphControl1.Refresh();

            }
        }

        private void checkBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox.GetItemCheckState(0) == 0)
            {

            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e) // TUY CHON CAC LOAI DO THI
        {

            if (typeGraph.Text == "Irradiance")
            {
                zedGraphControl1.GraphPane.Title.Text = "Đồ thị bức xạ mặt trời theo thời gian thực";
                zedGraphControl1.GraphPane.XAxis.Title.Text = "Thời gian";
                zedGraphControl1.GraphPane.YAxis.Title.Text = "Bức xạ (W/m2)";
                zedGraphControl1.GraphPane.YAxis.Scale.Min = 0;
                zedGraphControl1.GraphPane.YAxis.Scale.Max = 100;
                zedGraphControl1.GraphPane.YAxis.Scale.MajorStep = 10;
                zedGraphControl1.GraphPane.YAxis.Scale.MinorStep = 1;

                // AN LINE CUA TEM VA ISC
                for (int i = 0; i < 6; i++)
                {
                    LineItem lr1 = zedGraphControl1.GraphPane.CurveList[i] as LineItem;
                    if (i == 0 || i == 1)
                    {
                        lr1.IsVisible = true;
                        zedGraphControl1.GraphPane.CurveList[i].Label.IsVisible = true;
                    }
                    else
                    {
                        lr1.IsVisible = false;
                        zedGraphControl1.GraphPane.CurveList[i].Label.IsVisible = false;
                    }
                }


                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
                zedGraphControl1.Refresh();

            }
            if (typeGraph.Text == "Short Current")
            {
                zedGraphControl1.GraphPane.Title.Text = "Đồ thị dòng điện ngắn mạch theo thời gian thực";
                zedGraphControl1.GraphPane.XAxis.Title.Text = "Thời gian";
                zedGraphControl1.GraphPane.YAxis.Title.Text = "Isc (A)";
                zedGraphControl1.GraphPane.YAxis.Scale.Min = 0;
                zedGraphControl1.GraphPane.YAxis.Scale.Max = 0.004;
                zedGraphControl1.GraphPane.YAxis.Scale.MajorStep = 0.001;
                zedGraphControl1.GraphPane.YAxis.Scale.MinorStep = 0.0002;
                //AN LINE CUA IRR VA TEM
                for (int i = 0; i < 6; i++)
                {
                    LineItem lr = zedGraphControl1.GraphPane.CurveList[i] as LineItem;
                    if (i == 2 || i == 3)
                    {
                        lr.IsVisible = true;
                        zedGraphControl1.GraphPane.CurveList[i].Label.IsVisible = true;
                    }
                    else
                    {
                        lr.IsVisible = false;
                        zedGraphControl1.GraphPane.CurveList[i].Label.IsVisible = false;
                    }
                }



                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
                zedGraphControl1.Refresh();

            }

            if (typeGraph.Text == "Temperature")
            {
                zedGraphControl1.GraphPane.Title.Text = "Đồ thị nhiệt độ theo thời gian thực";
                zedGraphControl1.GraphPane.XAxis.Title.Text = "Thời gian";
                zedGraphControl1.GraphPane.YAxis.Title.Text = "Nhiệt độ (C)";
                zedGraphControl1.GraphPane.YAxis.Scale.Min = 0;
                zedGraphControl1.GraphPane.YAxis.Scale.Max = 50;
                zedGraphControl1.GraphPane.YAxis.Scale.MajorStep = 5;
                zedGraphControl1.GraphPane.YAxis.Scale.MinorStep = 1;
                //AN LINE CUA IRR VA ISC
                for (int i = 0; i < 6; i++)
                {

                     LineItem lr = zedGraphControl1.GraphPane.CurveList[i] as LineItem;
                     if (i == 4 || i == 5)
                     {
                         lr.IsVisible = true;
                         zedGraphControl1.GraphPane.CurveList[i].Label.IsVisible = true;
                     }
                     else
                     {
                         lr.IsVisible = false;
                         zedGraphControl1.GraphPane.CurveList[i].Label.IsVisible = false;
                     }
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Invalidate();
                    zedGraphControl1.Refresh();

                }
            }
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }
    }
}


