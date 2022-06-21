using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_19013135
{
    public partial class UserPackages : Form
    {

        public UserPackages()
        {
            InitializeComponent();
            // 고객 정보를 담은 테이블
            dataGridView1.DataSource = DataManager.Users;
            dataGridView1.Columns["userId"].HeaderText = "회원번호";
            dataGridView1.Columns["name"].HeaderText = "회원명";
            dataGridView1.Columns["phone"].HeaderText = "연락처";
            dataGridView1.Columns["address"].HeaderText = "주소";
            dataGridView1.Columns["level"].HeaderText = "회원등급";
            dataGridView1.Columns["total"].HeaderText = "이용요금";

            textBox1.ReadOnly = true;
            dataGridView2.DataSource = DataManager.Packages;
            dataGridView2.Columns["address"].Visible = false;
            dataGridView2.Columns["date"].Visible = false;
            dataGridView2.Columns["dday"].Visible = false;

            dataGridView2.Columns["packageId"].HeaderText = "물품번호";
            dataGridView2.Columns["type"].HeaderText = "물품구분";
            dataGridView2.Columns["fee"].HeaderText = "운송료";
            dataGridView2.Columns["start"].HeaderText = "배송시작점";
            dataGridView2.Columns["end"].HeaderText = "배송도착점";
            dataGridView2.Columns["TransTime"].HeaderText = "배송시작일";
            dataGridView2.Columns["ReceiveTime"].HeaderText = "배송종료일";
            dataGridView2.Columns["status"].HeaderText = "배송완료";
            dataGridView2.Columns["transUserId"].HeaderText = "배송유저ID";
            dataGridView2.Columns["receiveUserId"].HeaderText = "수신유저ID";
        }

        // 고객에 대하여 해당 고객이 이용한 택배 정보를 출력
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            User u = dataGridView1.CurrentRow.DataBoundItem as User;
            textBox1.Text = u.userId.ToString();

            List<Package> UserCity = new List<Package>();

            foreach (Package p in DataManager.Packages)
            {
                if (p.transUserId == u.userId)
                {
                    UserCity.Add(p);
                }
            }
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = UserCity;
            dataGridView2.Columns["address"].Visible = false;
            dataGridView2.Columns["date"].Visible = false;
            dataGridView2.Columns["dday"].Visible = false;

            dataGridView2.Columns["packageId"].HeaderText = "물품번호";
            dataGridView2.Columns["type"].HeaderText = "물품구분";
            dataGridView2.Columns["fee"].HeaderText = "운송료";
            dataGridView2.Columns["start"].HeaderText = "배송시작점";
            dataGridView2.Columns["end"].HeaderText = "배송도착점";
            dataGridView2.Columns["TransTime"].HeaderText = "배송시작일";
            dataGridView2.Columns["ReceiveTime"].HeaderText = "배송종료일";
            dataGridView2.Columns["status"].HeaderText = "배송완료";
            dataGridView2.Columns["transUserId"].HeaderText = "배송유저ID";
            dataGridView2.Columns["receiveUserId"].HeaderText = "수신유저ID";
        }
    }
}
