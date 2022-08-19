using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace homework2
{
    public partial class BingoBingo : Form
    {
        List<Button> listbtn = new List<Button>();          //按鈕 List
        List<Label> listlbl = new List<Label>();            //label List
        List<string> listselect = new List<string>();       //選號星數對應
        List<int> autolotoNum = new List<int>();            //自動選號
        int modecheck = 0;                                  //自動或手動選號判定
        int lastcount;                                      //剩餘選號數
        int oecount;                                        //單雙中獎判定
        int supercheck = 0;                                 //特別獎號中獎判定
        int countmoney = 0;                                 //彩券金額
        int countaward = 0;                                 //獎金
        int[] lotoNum = new int[20];                        //開獎陣列
        int redlbl = 0;                                     //超級獎號label判定
        string finNum = "";                                 //選號顯示
        string autoStr1 = "";                               //自動選號顯示
        
        public BingoBingo()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //選號按鈕陣列
            Button[] btnarray = new Button[]
            {
                btn01,btn02,btn03,btn04,btn05,btn06,btn07,btn08,btn09,btn10,
                btn11,btn12,btn13,btn14,btn15,btn16,btn17,btn18,btn19,btn20,
                btn21,btn22,btn23,btn24,btn25,btn26,btn27,btn28,btn29,btn30,
                btn31,btn32,btn33,btn34,btn35,btn36,btn37,btn38,btn39,btn40,
                btn41,btn42,btn43,btn44,btn45,btn46,btn47,btn48,btn49,btn50,
                btn51,btn52,btn53,btn54,btn55,btn56,btn57,btn58,btn59,btn60,
                btn61,btn62,btn63,btn64,btn65,btn66,btn67,btn68,btn69,btn70,
                btn71,btn72,btn73,btn74,btn75,btn76,btn77,btn78,btn79,btn80,
            };
            listbtn.AddRange(btnarray);

            //玩法
            string[] picklist = new string[]
            {
                "零","一星","二星","三星","四星","五星",
                "六星","七星","八星","九星","十星"
            };
            cbx玩法.Items.AddRange(picklist);
            
            //倍數
            string[] pickpower = new string[]
            {
                "0","1","2","3","4","5",
                "6","8","10","12","20","50"
            };
            cbx選號倍數.Items.AddRange(pickpower);
            cbx大小倍數.Items.AddRange(pickpower);
            cbx單雙倍數.Items.AddRange(pickpower);
            
            //獎號顯示
            Label[] lblarray = new Label[]
            {
                lbl01,lbl02,lbl03,lbl04,lbl05,lbl06,lbl07,lbl08,lbl09,lbl10,
                lbl11,lbl12,lbl13,lbl14,lbl15,lbl16,lbl17,lbl18,lbl19,lbl20,
                lbl21,lbl22,lbl23
            };
            listlbl.AddRange(lblarray);

            //開獎號碼label改圓形
            for (int i = 0; i < lblarray.Length; i += 1)
            {
                lblarray[i].Size = new Size(50, 50);
                System.Drawing.Drawing2D.GraphicsPath aCircle = new System.Drawing.Drawing2D.GraphicsPath();
                aCircle.AddEllipse(new Rectangle(0, 0, 50, 50));
                lblarray[i].Region = new Region(aCircle);
            }

            MessageBox.Show("使用說明:\n\n選擇星數→選擇倍率→手動選號或自動選號→完成選號→確認金額→開獎→對獎", "歡迎使用");

            //btn完成選號.Enabled = false;
            btn確認金額.Enabled = false;
            btn對獎.Enabled = false;
            btn小到大.Enabled = false;
            btn大到小.Enabled = false;
        }

        private void btn開獎_Click(object sender, EventArgs e)
        {
            NumCreat();
        }

        private void btn小到大_Click(object sender, EventArgs e)
        {
            sort1();
        }

        private void btn大到小_Click(object sender, EventArgs e)
        {
            sort2();
        }

        private void cbx玩法_SelectedIndexChanged(object sender, EventArgs e)
        {
            //判定星數判定剩餘選號
            if(cbx玩法.SelectedIndex == 0)
            {
                btn隨機選號.Enabled = false;
            }
            else
            {
                btn隨機選號.Enabled = true;
            }
            lastcount = cbx玩法.SelectedIndex;
            lbl尚選號碼.Text = string.Format($"尚餘{lastcount}個未選");
        }

        private void btn隨機選號_Click(object sender, EventArgs e)
        {
            autoNumCreat();
        }

        private void btn完成選號_Click(object sender, EventArgs e)
        {
            numFinish();
        }

        private void btn清除選擇_Click(object sender, EventArgs e)
        {
            clearBtn1();
        }

        private void btn確認金額_Click(object sender, EventArgs e)
        {
            checkPrice();
        }

        private void btn對獎_Click(object sender, EventArgs e)
        {
            checkNumber();
        }

        //對獎清除
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearBtn1();
            lbl本期獎號.Text = "";
            lbl投注獎號.Text = "";
            lbl號碼對獎結果.Text = "";
            lbl本期單雙大小.Text = "";
            lbl猜數字結果.Text = "";
            lbl投注種類.Text = "";
            lbl獎金.Text = "";
            countaward = 0;     //獎金
        }

        private void btn01_Click(object sender, EventArgs e)
        {
            pickbtn(btn01);
        }

        private void btn02_Click(object sender, EventArgs e)
        {
            pickbtn(btn02);
        }

        private void btn03_Click(object sender, EventArgs e)
        {
            pickbtn(btn03);
        }

        private void btn04_Click(object sender, EventArgs e)
        {
            pickbtn(btn04);
        }

        private void btn05_Click(object sender, EventArgs e)
        {
            pickbtn(btn05);
        }

        private void btn06_Click(object sender, EventArgs e)
        {
            pickbtn(btn06);
        }

        private void btn07_Click(object sender, EventArgs e)
        {
            pickbtn(btn07);
        }

        private void btn08_Click(object sender, EventArgs e)
        {
            pickbtn(btn08);
        }

        private void btn09_Click(object sender, EventArgs e)
        {
            pickbtn(btn09);
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            pickbtn(btn10);
        }

        private void btn11_Click(object sender, EventArgs e)
        {
            pickbtn(btn11);
        }

        private void btn12_Click(object sender, EventArgs e)
        {
            pickbtn(btn12);
        }

        private void btn13_Click(object sender, EventArgs e)
        {
            pickbtn(btn13);
        }

        private void btn14_Click(object sender, EventArgs e)
        {
            pickbtn(btn14);
        }

        private void btn15_Click(object sender, EventArgs e)
        {
            pickbtn(btn15);
        }

        private void btn16_Click(object sender, EventArgs e)
        {
            pickbtn(btn16);
        }

        private void btn17_Click(object sender, EventArgs e)
        {
            pickbtn(btn17);
        }

        private void btn18_Click(object sender, EventArgs e)
        {
            pickbtn(btn18);
        }

        private void btn19_Click(object sender, EventArgs e)
        {
            pickbtn(btn19);
        }

        private void btn20_Click(object sender, EventArgs e)
        {
            pickbtn(btn20);
        }

        private void btn21_Click(object sender, EventArgs e)
        {
            pickbtn(btn21);
        }

        private void btn22_Click(object sender, EventArgs e)
        {
            pickbtn(btn22);
        }

        private void btn23_Click(object sender, EventArgs e)
        {
            pickbtn(btn23);
        }

        private void btn24_Click(object sender, EventArgs e)
        {
            pickbtn(btn24);
        }

        private void btn25_Click(object sender, EventArgs e)
        {
            pickbtn(btn25);
        }

        private void btn26_Click(object sender, EventArgs e)
        {
            pickbtn(btn26);
        }

        private void btn27_Click(object sender, EventArgs e)
        {
            pickbtn(btn27);
        }

        private void btn28_Click(object sender, EventArgs e)
        {
            pickbtn(btn28);
        }

        private void btn29_Click(object sender, EventArgs e)
        {
            pickbtn(btn29);
        }

        private void btn30_Click(object sender, EventArgs e)
        {
            pickbtn(btn30);
        }

        private void btn31_Click(object sender, EventArgs e)
        {
            pickbtn(btn31);
        }

        private void btn32_Click(object sender, EventArgs e)
        {
            pickbtn(btn32);
        }

        private void btn33_Click(object sender, EventArgs e)
        {
            pickbtn(btn33);
        }

        private void btn34_Click(object sender, EventArgs e)
        {
            pickbtn(btn34);
        }

        private void btn35_Click(object sender, EventArgs e)
        {
            pickbtn(btn35);
        }

        private void btn36_Click(object sender, EventArgs e)
        {
            pickbtn(btn36);
        }

        private void btn37_Click(object sender, EventArgs e)
        {
            pickbtn(btn37);
        }

        private void btn38_Click(object sender, EventArgs e)
        {
            pickbtn(btn38);
        }

        private void btn39_Click(object sender, EventArgs e)
        {
            pickbtn(btn39);
        }

        private void btn40_Click(object sender, EventArgs e)
        {
            pickbtn(btn40);
        }

        private void btn41_Click(object sender, EventArgs e)
        {
            pickbtn(btn41);
        }

        private void btn42_Click(object sender, EventArgs e)
        {
            pickbtn(btn42);
        }

        private void btn43_Click(object sender, EventArgs e)
        {
            pickbtn(btn43);
        }

        private void btn44_Click(object sender, EventArgs e)
        {
            pickbtn(btn44);
        }

        private void btn45_Click(object sender, EventArgs e)
        {
            pickbtn(btn45);
        }

        private void btn46_Click(object sender, EventArgs e)
        {
            pickbtn(btn46);
        }

        private void btn47_Click(object sender, EventArgs e)
        {
            pickbtn(btn47);
        }

        private void btn48_Click(object sender, EventArgs e)
        {
            pickbtn(btn48);
        }

        private void btn49_Click(object sender, EventArgs e)
        {
            pickbtn(btn49);
        }

        private void btn50_Click(object sender, EventArgs e)
        {
            pickbtn(btn50);
        }

        private void btn51_Click(object sender, EventArgs e)
        {
            pickbtn(btn51);
        }

        private void btn52_Click(object sender, EventArgs e)
        {
            pickbtn(btn52);
        }

        private void btn53_Click(object sender, EventArgs e)
        {
            pickbtn(btn53);
        }

        private void btn54_Click(object sender, EventArgs e)
        {
            pickbtn(btn54);
        }

        private void btn55_Click(object sender, EventArgs e)
        {
            pickbtn(btn55);
        }

        private void btn56_Click(object sender, EventArgs e)
        {
            pickbtn(btn56);
        }

        private void btn57_Click(object sender, EventArgs e)
        {
            pickbtn(btn57);
        }

        private void btn58_Click(object sender, EventArgs e)
        {
            pickbtn(btn58);
        }

        private void btn59_Click(object sender, EventArgs e)
        {
            pickbtn(btn59);
        }

        private void btn60_Click(object sender, EventArgs e)
        {
            pickbtn(btn60);
        }

        private void btn61_Click(object sender, EventArgs e)
        {
            pickbtn(btn61);
        }

        private void btn62_Click(object sender, EventArgs e)
        {
            pickbtn(btn62);
        }

        private void btn63_Click(object sender, EventArgs e)
        {
            pickbtn(btn63);
        }

        private void btn64_Click(object sender, EventArgs e)
        {
            pickbtn(btn64);
        }

        private void btn65_Click(object sender, EventArgs e)
        {
            pickbtn(btn65);
        }

        private void btn66_Click(object sender, EventArgs e)
        {
            pickbtn(btn66);
        }

        private void btn67_Click(object sender, EventArgs e)
        {
            pickbtn(btn67);
        }

        private void btn68_Click(object sender, EventArgs e)
        {
            pickbtn(btn68);
        }

        private void btn69_Click(object sender, EventArgs e)
        {
            pickbtn(btn69);
        }

        private void btn70_Click(object sender, EventArgs e)
        {
            pickbtn(btn70);
        }

        private void btn71_Click(object sender, EventArgs e)
        {
            pickbtn(btn71);
        }

        private void btn72_Click(object sender, EventArgs e)
        {
            pickbtn(btn72);
        }

        private void btn73_Click(object sender, EventArgs e)
        {
            pickbtn(btn73);
        }

        private void btn74_Click(object sender, EventArgs e)
        {
            pickbtn(btn74);
        }

        private void btn75_Click(object sender, EventArgs e)
        {
            pickbtn(btn75);
        }

        private void btn76_Click(object sender, EventArgs e)
        {
            pickbtn(btn76);
        }

        private void btn77_Click(object sender, EventArgs e)
        {
            pickbtn(btn77);
        }

        private void btn78_Click(object sender, EventArgs e)
        {
            pickbtn(btn78);
        }

        private void btn79_Click(object sender, EventArgs e)
        {
            pickbtn(btn79);
        }

        private void btn80_Click(object sender, EventArgs e)
        {
            pickbtn(btn80);
        }

        private void btn大_Click(object sender, EventArgs e)
        {
            btn大.BackColor = Color.Blue;
            btn小.BackColor = SystemColors.ButtonFace;
        }

        private void btn小_Click(object sender, EventArgs e)
        {
            btn小.BackColor = Color.Blue;
            btn大.BackColor = SystemColors.ButtonFace;
        }

        private void btn單_Click(object sender, EventArgs e)
        {
            btn單.BackColor = Color.Blue;
            oecount += 1;
        }

        private void btn雙_Click(object sender, EventArgs e)
        {
            btn雙.BackColor = Color.Blue;
            oecount += 1;
        }

        private void btn和_Click(object sender, EventArgs e)
        {
            btn和.BackColor = Color.Blue;
            oecount += 1;
        }

        private void btn小單_Click(object sender, EventArgs e)
        {
            btn小單.BackColor = Color.Blue;
            oecount += 1;
        }

        private void btn小雙_Click(object sender, EventArgs e)
        {
            btn小雙.BackColor = Color.Blue;
            oecount += 1;
        }



        //==============事件API=================

        //開獎按鈕
        void NumCreat()
        {
            //獎號產生
            System.Random lotoCreate = new Random();
            for (int i = 0; i < 20; i += 1)
            {
                lotoNum[i] = lotoCreate.Next(1, 81);
                for (int j = 0; j < i; j += 1)
                {
                    while (lotoNum[j] == lotoNum[i])
                    {
                        j = 0;
                        lotoNum[i] = lotoCreate.Next(1, 81);
                    }
                }
                listlbl[i].BackColor = Color.Yellow;
                listlbl[i].ForeColor = Color.Black;
            }

            //獎號顯示
            for (int i = 0; i < 20; i += 1)
            {
                listlbl[i].Text = string.Format("{0:00}", lotoNum[i]);
                if (i == 19)
                {
                    listlbl[i].BackColor = Color.Red;
                    listlbl[i].ForeColor = Color.White;
                    redlbl = lotoNum[19];
                }
            }

            //超級獎號(最後一個)
            listlbl[20].Text = string.Format("{0:00}", lotoNum[19]);

            //猜大小顯示
            int scount = 0;
            int bcount = 0;
            for (int i = 0; i <= lotoNum.GetUpperBound(0); i += 1)
            {
                if (lotoNum[i] < 41)
                {
                    scount += 1;
                }
                else
                {
                    bcount += 1;
                }

            }
            if (scount >= 13)
            {
                lbl22.Text = "小";
            }
            else if (bcount >= 13)
            {
                lbl22.Text = "大";
            }
            else
            {
                lbl22.Text = "-";
            }

            //猜單雙顯示
            int oddcount = 0;
            int evencount = 0;
            for (int i = 0; i <= lotoNum.GetUpperBound(0); i += 1)
            {
                if (lotoNum[i] % 2 == 0)
                {
                    evencount += 1;
                }
                else
                {
                    oddcount += 1;
                }
            }
            if (evencount >= 13)
            {
                lbl23.Text = "雙";
            }
            else if (oddcount >= 13)
            {
                lbl23.Text = "單";
            }
            else if (evencount >= 11)
            {
                lbl23.Text = "小雙";
            }
            else if (oddcount >= 11)
            {
                lbl23.Text = "小單";
            }
            else
            {
                lbl23.Text = "和";
            }

            btn小到大.Enabled = true;
            btn大到小.Enabled = true;
            btn對獎.Enabled = true;
        }

        //小到大排序按鈕
        void sort1()
        {
            Array.Sort(lotoNum);
            for (int i = 0; i < 20; i += 1)
            {
                listlbl[i].Text = string.Format("{0:00}", lotoNum[i]);
                if (lotoNum[i] == redlbl)
                {
                    listlbl[i].BackColor = Color.Red;
                    listlbl[i].ForeColor = Color.White;
                }
                else
                {
                    listlbl[i].BackColor = Color.Yellow;
                    listlbl[i].ForeColor = Color.Black;
                }
            }
        }

        //大到小排序按鈕
        void sort2()
        {
            Array.Sort(lotoNum);
            Array.Reverse(lotoNum);
            for (int i = 0; i < 20; i += 1)
            {
                listlbl[i].Text = string.Format("{0:00}", lotoNum[i]);
                if (lotoNum[i] == redlbl)
                {
                    listlbl[i].BackColor = Color.Red;
                    listlbl[i].ForeColor = Color.White;
                }
                else
                {
                    listlbl[i].BackColor = Color.Yellow;
                    listlbl[i].ForeColor = Color.Black;
                }
            }
        }

        //隨機選號按鈕
        void autoNumCreat()
        {
            if (cbx玩法.SelectedIndex != -1)
            {
                if (modecheck == 0)
                {
                    btn隨機選號.BackColor = Color.Blue;
                    System.Random autoNumCreate = new Random();
                    int[] autoNumArray = new int[lastcount];
                    List<int> autolotoNum = new List<int>();
                    for (int i = 0; i < lastcount; i += 1)
                    {
                        autoNumArray[i] = autoNumCreate.Next(1, 81);
                        for (int j = 0; j < i; j += 1)
                        {
                            while (autoNumArray[j] == autoNumArray[i])
                            {
                                j = 0;
                                autoNumArray[i] = autoNumCreate.Next(1, 81);
                            }
                        }
                        listbtn[autoNumArray[i] - 1].BackColor = Color.Blue;
                    }

                    autolotoNum.InsertRange(0, autoNumArray);
                    autolotoNum.Sort();

                    for (int i = 0; i < lastcount; i += 1)
                    {
                        autoStr1 += autolotoNum[i] + ",";
                        listselect.Add(Convert.ToString(autolotoNum[i]));
                    }
                    lbl尚選號碼.Text = string.Format("尚餘0個未選");
                    modecheck = 1;
                    //btn完成選號.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("請先選擇星數", "錯誤");
            }
        }

        //完成選號按鈕
        void numFinish()
        {
            //btn完成選號.BackColor = Color.Blue;
            for (int i = 0; i < listselect.Count; i += 1)
            {
                finNum += listselect[i] + ",";
            }
            if (modecheck == 1)
            {
                lbl所選號碼.Text = autoStr1;
            }

            if (modecheck == 2)
            {
                lbl所選號碼.Text = finNum;
            }
            btn確認金額.Enabled = true;
        }

        //清除選擇按鈕
        void clearBtn1()
        {
            lbl所選號碼.Text = "";                       //選擇號碼
            cbx玩法.SelectedIndex = 0;                  //星數  
            lastcount = 0;                              //剩餘選號數
            oecount = 0;                                //單雙計算
            countmoney = 0;                             //彩券金額
            lbl彩券總額顯示.Text = "";                   //彩券金額顯示
            listselect.Clear();                         //選擇獎號清除
            autoStr1 = "";                              //自動獎號顯示
            finNum = "";                                //選擇獎號顯示
            List<int> autolotoNum = new List<int>();    //自動獎號清除
            modecheck = 0;                              //自動或選擇兌獎判定

            //倍數歸0
            cbx單雙倍數.Text = "0";
            cbx選號倍數.Text = "0";
            cbx大小倍數.Text = "0";

            //btn復歸
            for (int i = 0; i < 80; i += 1)
            {
                listbtn[i].Enabled = true;
            }
            btn小.Enabled = true;
            btn大.Enabled = true;
            btn單.Enabled = true;
            btn雙.Enabled = true;
            btn和.Enabled = true;
            btn小單.Enabled = true;
            btn小雙.Enabled = true;
            btn完成選號.Enabled = true;
            btn隨機選號.Enabled = true;
            btn對獎.Enabled = true;
            cbx玩法.Enabled = true;
            cbx單雙倍數.Enabled = true;
            cbx大小倍數.Enabled = true;
            cbx選號倍數.Enabled = true;
            chb超級獎號.Enabled = true;
            btn確認金額.Enabled = true;

            //btn顏色復歸
            for (int i = 0; i < 80; i += 1)
            {
                listbtn[i].BackColor = SystemColors.ButtonFace;
            }
            btn大.BackColor = SystemColors.ButtonFace;
            btn小.BackColor = SystemColors.ButtonFace;
            btn單.BackColor = SystemColors.ButtonFace;
            btn雙.BackColor = SystemColors.ButtonFace;
            btn和.BackColor = SystemColors.ButtonFace;
            btn小單.BackColor = SystemColors.ButtonFace;
            btn小雙.BackColor = SystemColors.ButtonFace;
            btn確認金額.BackColor = SystemColors.ButtonFace;
            btn完成選號.BackColor = SystemColors.ButtonFace;
            btn隨機選號.BackColor = SystemColors.ButtonFace;
            MessageBox.Show("玩法及倍數歸零，請重新選擇", "提醒");
        }

        //確認金額按鈕
        void checkPrice()
        {
            btn確認金額.BackColor = Color.Blue;
            if (cbx玩法.SelectedIndex != 0)
            {
                countmoney += 25 * Convert.ToInt32(cbx選號倍數.Text);
            }

            if ((btn小.BackColor == Color.Blue) || (btn大.BackColor == Color.Blue))
            {
                countmoney += 25 * Convert.ToInt32(cbx大小倍數.Text);
            }

            if (chb超級獎號.Checked)
            {
                countmoney += 25;
            }

            if (oecount != 0)
            {
                countmoney += oecount * 25 * Convert.ToInt32(cbx單雙倍數.Text);
            }
            lbl彩券總額顯示.Text = countmoney + "元";

            for (int i = 0; i < 80; i += 1)
            {
                listbtn[i].Enabled = false;
            }
            btn小.Enabled = false;
            btn大.Enabled = false;
            btn單.Enabled = false;
            btn雙.Enabled = false;
            btn和.Enabled = false;
            btn小單.Enabled = false;
            btn小雙.Enabled = false;
            btn完成選號.Enabled = false;
            btn隨機選號.Enabled = false;
            cbx玩法.Enabled = false;
            cbx單雙倍數.Enabled = false;
            cbx大小倍數.Enabled = false;
            cbx選號倍數.Enabled = false;
            chb超級獎號.Enabled = false;
            btn確認金額.Enabled = false;
        }

        //對獎按鈕
        void checkNumber()
        {
            countaward = 0;
            //獎號顯示
            string pcNumStr = "";
            Array.Sort(lotoNum);
            for (int i = 0; i <= lotoNum.GetUpperBound(0); i += 1)
            {
                pcNumStr += $"{lotoNum[i]}, ";
            }
            lbl本期獎號.Text = pcNumStr;

            //猜數字顯示
            lbl本期單雙大小.Text = lbl22.Text + " ," + lbl23.Text;

            //投注獎號顯示
            string myNumStr = "";
            int[] myTempIntArray = new int[listselect.Count];
            for (int i = 0; i < listselect.Count; i++)
            {
                myTempIntArray[i] = Convert.ToInt32(listselect[i]);
            }
            Array.Sort(myTempIntArray);

            for (int i = 0; i < myTempIntArray.Length; i += 1)
            {
                myNumStr += myTempIntArray[i] + ",";
            }
            lbl投注獎號.Text = myNumStr;

            //猜數字顯示
            string myGuessStr = "";
            List<string> matchList = new List<string>();
            if (btn小.BackColor == Color.Blue)
            {
                myGuessStr += btn小.Text + ",";
                matchList.Add(btn小.Text);
            }
            if (btn大.BackColor == Color.Blue)
            {
                myGuessStr += btn大.Text + ",";
                matchList.Add(btn大.Text);
            }
            if (btn單.BackColor == Color.Blue)
            {
                myGuessStr += btn單.Text + ",";
                matchList.Add(btn單.Text);
            }
            if (btn雙.BackColor == Color.Blue)
            {
                myGuessStr += btn雙.Text + ",";
                matchList.Add(btn雙.Text);
            }
            if (btn和.BackColor == Color.Blue)
            {
                myGuessStr += btn和.Text + ",";
                matchList.Add(btn和.Text);
            }
            if (btn小單.BackColor == Color.Blue)
            {
                myGuessStr += btn小單.Text + ",";
                matchList.Add(btn小單.Text);
            }
            if (btn小雙.BackColor == Color.Blue)
            {
                myGuessStr += btn小雙.Text + ",";
                matchList.Add(btn小雙.Text);
            }
            lbl投注種類.Text = myGuessStr;

            //號碼對獎
            int count2 = 0;
            string checkNum = "";
            for (int i = 0; i < listselect.Count; i += 1)
            {
                for (int j = 0; j < 20; j += 1)
                {
                    if (Convert.ToInt32(listselect[i]) == lotoNum[j])
                    {
                        count2 += 1;
                        checkNum += listselect[i] + ",";
                    }
                }
            }
            if (chb超級獎號.Checked)
            {
                for (int i = 0; i < listselect.Count; i += 1)
                {
                    if (Convert.ToInt32(listselect[i]) == redlbl)
                    {
                        supercheck = 1;
                    }
                }
            }

            lbl號碼對獎結果.Text = "恭喜對中" + count2 + "個號碼，對中號碼為:" + checkNum;

            //猜數字對獎
            int count3 = 0;
            string checkGuess = "";
            for (int i = 0; i < matchList.Count; i += 1)
            {
                if (matchList[i] == lbl22.Text)
                {
                    count3 += 1;
                    checkGuess += matchList[i] + ",";

                }
                if (matchList[i] == lbl23.Text)
                {
                    count3 += 1;
                    checkGuess += matchList[i] + ",";
                }

            }
            lbl猜數字結果.Text = "恭喜對中" + count3 + "個，對中為種類:" + checkGuess;

            //號碼獎金


            if (supercheck == 1)
            {
                switch (cbx玩法.SelectedIndex)
                {
                    case 1:
                        if (count2 == 1)
                        {
                            countaward += 25 * 6 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 2:
                        if (count2 == 1)
                        {
                            countaward += 25 * 3 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 2)
                        {
                            countaward += 25 * 8 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 3:
                        if (count2 == 1)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 2)
                        {
                            countaward += 25 * 6 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 3)
                        {
                            countaward += 25 * 60 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 4:
                        if (count2 == 1)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 2)
                        {
                            countaward += 25 * 3 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 3)
                        {
                            countaward += 25 * 12 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 3)
                        {
                            countaward += 25 * 120 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 5:
                        if (count2 == 1)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 2)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 3)
                        {
                            countaward += 25 * 6 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 4)
                        {
                            countaward += 25 * 60 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 5)
                        {
                            countaward += 25 * 800 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 6:
                        if (count2 == 1)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 2)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 3)
                        {
                            countaward += 25 * 3 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 4)
                        {
                            countaward += 25 * 20 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 5)
                        {
                            countaward += 25 * 100 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 6)
                        {
                            countaward += 25 * 2500 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 7:
                        if (count2 == 1)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 2)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 3)
                        {
                            countaward += 25 * 3 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 4)
                        {
                            countaward += 25 * 5 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 5)
                        {
                            countaward += 25 * 30 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 6)
                        {
                            countaward += 25 * 300 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 7)
                        {
                            countaward += 25 * 8000 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 8:
                        if (count2 == 1)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 2)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 3)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 4)
                        {
                            countaward += 25 * 3 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 5)
                        {
                            countaward += 25 * 20 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 6)
                        {
                            countaward += 25 * 100 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 7)
                        {
                            countaward += 25 * 2000 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 8)
                        {
                            countaward += 25 * 50000 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 9:
                        if (count2 == 1)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 2)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 3)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 4)
                        {
                            countaward += 25 * 3 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 5)
                        {
                            countaward += 25 * 10 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 6)
                        {
                            countaward += 25 * 500 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 7)
                        {
                            countaward += 25 * 300 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 8)
                        {
                            countaward += 25 * 10000 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 9)
                        {
                            countaward += 25 * 100000 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 10:
                        if (count2 == 1)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 2)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 3)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 4)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 5)
                        {
                            countaward += 25 * 3 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 6)
                        {
                            countaward += 25 * 30 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 7)
                        {
                            countaward += 25 * 250 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 8)
                        {
                            countaward += 25 * 2500 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 9)
                        {
                            countaward += 25 * 25000 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 10)
                        {
                            countaward += 25 * 500000 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;
                }
            }

            //超級獎號
            if (supercheck == 0)
            {
                switch (cbx玩法.SelectedIndex)
                {
                    case 1:
                        if (count2 == 1)
                        {
                            countaward += 25 * 2 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 2:
                        if (count2 == 1)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 2)
                        {
                            countaward += 25 * 3 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 3:
                        if (count2 == 2)
                        {
                            countaward += 25 * 2 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 3)
                        {
                            countaward += 25 * 20 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 4:
                        if (count2 == 2)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 3)
                        {
                            countaward += 25 * 4 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 3)
                        {
                            countaward += 25 * 40 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 5:
                        if (count2 == 3)
                        {
                            countaward += 25 * 2 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 4)
                        {
                            countaward += 25 * 20 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 5)
                        {
                            countaward += 25 * 300 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 6:
                        if (count2 == 3)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 4)
                        {
                            countaward += 25 * 8 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 5)
                        {
                            countaward += 25 * 40 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 6)
                        {
                            countaward += 25 * 1000 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 7:
                        if (count2 == 3)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 4)
                        {
                            countaward += 25 * 2 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 5)
                        {
                            countaward += 25 * 12 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 6)
                        {
                            countaward += 25 * 120 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 7)
                        {
                            countaward += 25 * 3200 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 8:
                        if (count2 == 0)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 4)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 5)
                        {
                            countaward += 25 * 8 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 6)
                        {
                            countaward += 25 * 40 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 7)
                        {
                            countaward += 25 * 800 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 8)
                        {
                            countaward += 25 * 20000 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 9:
                        if (count2 == 0)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 4)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 5)
                        {
                            countaward += 25 * 4 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 6)
                        {
                            countaward += 25 * 20 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 7)
                        {
                            countaward += 25 * 120 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 8)
                        {
                            countaward += 25 * 4000 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 9)
                        {
                            countaward += 25 * 40000 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;

                    case 10:
                        if (count2 == 0)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 5)
                        {
                            countaward += 25 * 1 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 6)
                        {
                            countaward += 25 * 10 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 7)
                        {
                            countaward += 25 * 100 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 8)
                        {
                            countaward += 25 * 1000 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 9)
                        {
                            countaward += 25 * 10000 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        if (count2 == 10)
                        {
                            countaward += 25 * 200000 * Convert.ToInt32(cbx選號倍數.Text);
                        }
                        break;
                }
            }

            //猜數字獎金

            for (int i = 0; i < matchList.Count; i += 1)
            {
                if ((matchList[i] == lbl22.Text) && (lbl22.Text == "小"))
                {
                    countaward += 25 * 6 * Convert.ToInt32(cbx大小倍數.Text);
                }
                if ((matchList[i] == lbl22.Text) && (lbl22.Text == "大"))
                {
                    countaward += 25 * 6 * Convert.ToInt32(cbx大小倍數.Text);
                }
                if ((matchList[i] == lbl23.Text) && (lbl23.Text == "單"))
                {
                    countaward += 25 * 6 * Convert.ToInt32(cbx單雙倍數.Text);
                }
                if ((matchList[i] == lbl23.Text) && (lbl23.Text == "雙"))
                {
                    countaward += 25 * 6 * Convert.ToInt32(cbx單雙倍數.Text);
                }
                if ((matchList[i] == lbl23.Text) && (lbl23.Text == "和"))
                {
                    countaward += 70 * Convert.ToInt32(cbx單雙倍數.Text);
                }
                if ((matchList[i] == lbl23.Text) && (lbl23.Text == "小單"))
                {
                    countaward += 45 * Convert.ToInt32(cbx單雙倍數.Text);
                }
                if ((matchList[i] == lbl23.Text) && (lbl23.Text == "小雙"))
                {
                    countaward += 45 * Convert.ToInt32(cbx單雙倍數.Text);
                }
            }

            lbl獎金.Text = countaward + "元";
            btn對獎.Enabled = false;
        }

        //選號按鈕
        void pickbtn(Button button)
        {
            if (cbx玩法.SelectedIndex != -1)
            {
                if (listselect.Count == cbx玩法.SelectedIndex)
                {
                    MessageBox.Show("你已選擇完畢", "提醒");
                }
                else
                {
                    if (button.BackColor != Color.Blue)
                    {
                        listselect.Add(button.Text);
                        button.BackColor = Color.Blue;
                        lastcount -= 1;
                        lbl尚選號碼.Text = string.Format($"尚餘{lastcount}個未選");
                        modecheck = 2;
                    }
                }
            }
            else
            {
                MessageBox.Show("請先選擇星數", "錯誤");
            }
        }
    }
}
