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
    public partial class CityPackage : Form
    {
        public CityPackage(List<Package> delivery)
        {
            InitializeComponent();

            // 도시에 대하여 현재 있는 물품 정보를 보여준다.
            dataGridView1.DataSource = delivery;
            dataGridView1.Columns["ReceiveTime"].Visible = false;
            dataGridView1.Columns["date"].Visible = false;
            dataGridView1.Columns["transUserId"].Visible = false;
            dataGridView1.Columns["receiveUserId"].Visible = false;
            dataGridView1.Columns["status"].Visible = false;

            dataGridView1.Columns["packageId"].HeaderText = "물품번호";
            dataGridView1.Columns["type"].HeaderText = "물품구분";
            dataGridView1.Columns["fee"].HeaderText = "운송료";
            dataGridView1.Columns["address"].HeaderText = "현위치";
            dataGridView1.Columns["TransTime"].HeaderText = "배송시작일";
            dataGridView1.Columns["dday"].HeaderText = "남은배송일";
            dataGridView1.Columns["start"].HeaderText = "배송시작점";
            dataGridView1.Columns["end"].HeaderText = "배송도착점";
            dataGridView1.Columns["mod"].HeaderText = "구분";
        }
    }
}
