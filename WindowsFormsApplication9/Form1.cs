using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str;
            string CW_array, CW_natural_array;
            string cw_conv = null;
            ushort PNL_Z_T, PNL_Y_T, PNL_X_T, COM_T, BAT_T, BAT_L, BAT_H, COM_L, COM_H, PNL_X_L, PNL_X_H, PNL_Y_L, PNL_Y_H, PNL_Z_L, PNL_Z_H;
            ulong BAT_V;
            double BAT_sum = 0;
            double COM_sum = 0;
            double BAT_V_R = 0;
            double PNL_x_sum = 0;
            double PNL_y_sum = 0;
            double PNL_z_sum = 0;

            CW_natural_array = textBox1.Text; //z -> 0
            try
            {
                foreach (char c in CW_natural_array)
                {
                    if ("z".Equals(c.ToString()))
                    {
                        cw_conv = cw_conv + "0";
                    }
                    else
                    {
                        cw_conv = cw_conv + c.ToString();
                    }
                }
                textBox8.Text = cw_conv;
            }
            catch
            {
                textBox8.Text = "error";
            }

            CW_array = textBox8.Text;
            if (CW_array.Length >= 4)
            {
                try
                {
                    str = CW_array[0].ToString() + CW_array[1].ToString();
                    BAT_L = Convert.ToUInt16(str, 16);
                    str = CW_array[2].ToString() + CW_array[3].ToString();
                    BAT_H = Convert.ToUInt16(str, 16);

                    //-チェック
                    int flag_bat = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (((ushort)(BAT_H >> i + 3) & 0x0001) == 1)
                        {
                            flag_bat = 1;
                        }

                    }


                    BAT_T = CNV(flag_bat, BAT_H, BAT_L);

                    for (int i = -4; i < 7; i++)//一番右からだから
                    {
                        if (((ushort)(BAT_T >> i + 4) & 0x0001) == 1)
                        {
                            double pow = Math.Pow(2, i);
                            BAT_sum = BAT_sum + pow;
                        }
                    }

                    BAT_sum = M_judge(flag_bat, BAT_sum);

                    textBox2.Text = BAT_sum.ToString();//BAT温度
                }
                catch (Exception)
                {


                }

                try
                {
                    str = CW_array[4].ToString() + CW_array[5].ToString();
                    COM_L = Convert.ToUInt16(str, 16);
                    str = CW_array[6].ToString() + CW_array[7].ToString();
                    COM_H = Convert.ToUInt16(str, 16);

                    //-チェック
                    int flag_com = 0;
                    for (int i = 0; i < 5; i++)
                    {

                        if (((ushort)(COM_H >> i + 3) & 0x0001) == 1)
                        {
                            flag_com = 1;
                        }


                    }

                    COM_T = CNV(flag_com, COM_H, COM_L);


                    for (int i = -4; i < 7; i++)//一番右からだから
                    {
                        if (((ushort)(COM_T >> i + 4) & 0x0001) == 1)
                        {
                            double pow = Math.Pow(2, i);
                            COM_sum = COM_sum + pow;
                        }
                    }

                    COM_sum = M_judge(flag_com, COM_sum);


                    textBox3.Text = COM_sum.ToString();//COM温度
                }
                catch (Exception)
                {


                }

                try//BAT電圧
                {
                    str = CW_array[8].ToString() + CW_array[9].ToString() + CW_array[10].ToString() + CW_array[11].ToString();
                    BAT_V = Convert.ToUInt64(str, 16);

                    BAT_V_R = (2.5 * BAT_V) / 4096;

                    textBox4.Text = BAT_V_R.ToString();
                }
                catch (Exception)
                {


                }

                try
                {
                    str = CW_array[12].ToString() + CW_array[13].ToString();
                    PNL_X_L = Convert.ToUInt16(str, 16);
                    str = CW_array[14].ToString() + CW_array[15].ToString();
                    PNL_X_H = Convert.ToUInt16(str, 16);


                    //-チェック
                    int flag_PNL_X = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (((ushort)(PNL_X_H >> i + 3) & 0x0001) == 1)
                        {
                            flag_PNL_X = 1;
                        }

                    }


                    PNL_X_T = CNV(flag_PNL_X, PNL_X_H, PNL_X_L);

                    for (int i = -4; i < 7; i++)//一番右からだから
                    {
                        if (((ushort)(PNL_X_T >> i + 4) & 0x0001) == 1)
                        {
                            double pow = Math.Pow(2, i);
                            PNL_x_sum = PNL_x_sum + pow;
                        }
                    }

                    PNL_x_sum = M_judge(flag_PNL_X, PNL_x_sum);

                    textBox5.Text = PNL_x_sum.ToString();//PNL_X_温度
                }
                catch (Exception)
                {


                }

                try//PNL_Y_温度
                {
                    str = CW_array[16].ToString() + CW_array[17].ToString();
                    PNL_Y_L = Convert.ToUInt16(str, 16);
                    str = CW_array[18].ToString() + CW_array[19].ToString();
                    PNL_Y_H = Convert.ToUInt16(str, 16);

                    //-チェック
                    int flag_PNL_Y = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (((ushort)(PNL_Y_H >> i + 3) & 0x0001) == 1)
                        {
                            flag_PNL_Y = 1;
                        }

                    }


                    PNL_Y_T = CNV(flag_PNL_Y, PNL_Y_H, PNL_Y_L);

                    for (int i = -4; i < 7; i++)//一番右からだから
                    {
                        if (((ushort)(PNL_Y_T >> i + 4) & 0x0001) == 1)
                        {
                            double pow = Math.Pow(2, i);
                            PNL_y_sum = PNL_y_sum + pow;
                        }
                    }

                    PNL_y_sum = M_judge(flag_PNL_Y, PNL_y_sum);

                    textBox6.Text = PNL_y_sum.ToString();
                }
                catch (Exception)
                {


                }

                try
                {
                    str = CW_array[20].ToString() + CW_array[21].ToString();
                    PNL_Z_L = Convert.ToUInt16(str, 16);
                    str = CW_array[22].ToString() + CW_array[23].ToString();
                    PNL_Z_H = Convert.ToUInt16(str, 16);

                    //-チェック
                    int flag_PNL_Z = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (((ushort)(PNL_Z_H >> i + 3) & 0x0001) == 1)
                        {
                            flag_PNL_Z = 1;
                        }

                    }


                    PNL_Z_T = CNV(flag_PNL_Z, PNL_Z_H, PNL_Z_L);

                    for (int i = -4; i < 7; i++)//一番右からだから
                    {
                        if (((ushort)(PNL_Z_T >> i + 4) & 0x0001) == 1)
                        {
                            double pow = Math.Pow(2, i);
                            PNL_z_sum = PNL_z_sum + pow;
                        }
                    }

                    PNL_z_sum = M_judge(flag_PNL_Z, PNL_z_sum);

                    textBox7.Text = PNL_z_sum.ToString();//PNL_Z_温度
                }
                catch (Exception)
                {


                }
                try
                {
                    string data = "CW：" + textBox8.Text + "\r\n" + "バッテリー温度：" + textBox2.Text + "\r\n" + "通信機温度：" + textBox3.Text + "\r\n" + "バッテリー電圧：" + textBox4.Text + "\r\n" + "パネル温度\r\n" + "X面：" + textBox5.Text + "\r\n" + "Y面：" + textBox6.Text + "\r\n" + "Z面：" + textBox7.Text;
                    string time = DateTime.Now.ToLongTimeString();
                    string log = System.IO.File.ReadAllText(@"log.txt");
                    log = "\\-------------------\\" + "\r\n解析時刻：" + time + "\r\n" + data + "\r\n\r\n" + log;
                    System.IO.File.WriteAllText(@"log.txt", log);
                    status_label.Text = "正常に書き込みました。";
                }
                catch (Exception)
                {
                    status_label.Text = "ログの書き込みが出来ませんでした。";
                }
            }


        }

        public double M_judge(int flag, double SUM)
        {
            if (flag == 1)
            {
                return -SUM;

            }
            else
            {
                return SUM;
            }



        }
        public ushort CNV(int flag, ushort H_DATA, ushort L_DATA)
        {
            if (flag == 1)
            {
                //2の補数計算
                H_DATA <<= 8;
                return (ushort)(~H_DATA + ~L_DATA + 0x01);


            }
            else
            {
                H_DATA <<= 8;
                return (ushort)(H_DATA + L_DATA);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text;
            try
            {
                using (StreamReader sr = new StreamReader("log.txt"))
                {
                    text = sr.ReadToEnd();
                    logbox.Text = text;
                    status_label.Text = "更新成功";
                }
            }
            catch (Exception)
            {
                status_label.Text = "更新エラー";
            }
        }

        private void status_label_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            label10.Text = dt.ToLongTimeString();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
