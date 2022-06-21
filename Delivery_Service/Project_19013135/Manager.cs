using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Data;

namespace Project_19013135
{
    public partial class Manager : Form
    {
        DateTime now;
        public Manager()
        {
            InitializeComponent();
            now = DateTime.Now;
            textBox1.Text = now.ToString();
            SetDataGrid();
        }
        // 도시 이름으로 도시당 물품의 정보를 구한다.
        public void SetDataGrid()
        {
            dataGridView1.Rows.Clear();
            string[] citys = new string[] { "서울", "경기", "강원", "충북", "충남", "대전", "경북", "경남", "대구",
                "울산", "부산", "전북", "광주", "전남", "제주" };
            for (int i=0; i<citys.Length; i++)
            {
                int cnt = 0;
                foreach(Package p in DataManager.Packages)
                {
                    if (p.status==false && p.address.Substring(0, 2).Equals(citys[i]))
                    {
                        cnt++;
                    }
                }
                dataGridView1.Rows.Add(citys[i], cnt);
            }
        }
        // 해당 도시의 배달물품 정보를 보여준다.
        public void ShowCityInfo(string name)
        {
            List<Package> delivery = new List<Package>();
            foreach (Package p in DataManager.Packages)
            {
                if (p.address.Substring(0, 2).Equals(name) && p.status==false)
                {
                    delivery.Add(p);
                }
            }

            CityPackage cityPackage = new CityPackage(delivery);
            cityPackage.ShowDialog();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            string name = ((Button)sender).Name;
            ShowCityInfo(name);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            ShowCityInfo(name);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            ShowCityInfo(name);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            ShowCityInfo(name);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            ShowCityInfo(name);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            ShowCityInfo(name);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            ShowCityInfo(name);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            ShowCityInfo(name);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            ShowCityInfo(name);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            ShowCityInfo(name);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            ShowCityInfo(name);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            ShowCityInfo(name);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            ShowCityInfo(name);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            ShowCityInfo(name);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            ShowCityInfo(name);
        }

        // 하루가 지났을때 실행
        private void button16_Click(object sender, EventArgs e)
        {
            now = now.AddDays(1);
            textBox1.Text = now.ToString();

            DataManager.One_Day_Pass(now);

            SetDataGrid();
        }
        // 3일이 지났을때 실행
        private void button1_Click_1(object sender, EventArgs e)
        {
            button16_Click(sender, e);
            button16_Click(sender, e);
            button16_Click(sender, e);
        }
    }
}
