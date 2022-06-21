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
    public partial class UserTool : Form
    {
        User selectedUser = null;
        public UserTool()
        {
            InitializeComponent();
            // 유저 정보 출력 테이블
            dataGridView1.DataSource = DataManager.Users;
            dataGridView1.Columns["userId"].HeaderText = "회원번호";
            dataGridView1.Columns["name"].HeaderText = "회원명";
            dataGridView1.Columns["phone"].HeaderText = "연락처";
            dataGridView1.Columns["address"].HeaderText = "주소";
            dataGridView1.Columns["level"].HeaderText = "회원등급";
            dataGridView1.Columns["total"].HeaderText = "이용요금";

            textBox1.ReadOnly = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            User u = dataGridView1.CurrentRow.DataBoundItem as User;
            textBox1.Text = u.userId.ToString();
            this.selectedUser = u;
        }
        // 일반고객 등급 지정
        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedUser == null)
            {
                MessageBox.Show("등급을 지정할 고객을 먼저 선택해주세요.");
                return;
            }
            else if (selectedUser.level.Equals("일반고객"))
            {
                MessageBox.Show("현재 일반 등급 입니다.");
            }

            selectedUser.level = "일반고객";
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataManager.Users;
            DataManager.SaveUser();
        }
        // 기업 고객 등급 지정
        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedUser == null)
            {
                MessageBox.Show("등급을 지정할 고객을 먼저 선택해주세요.");
                return;
            }
            else if (selectedUser.level.Equals("기업고객"))
            {
                MessageBox.Show("현재 기업 등급 입니다.");
            }

            selectedUser.level = "기업고객";
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataManager.Users;
            DataManager.SaveUser();
        }
        // VIP 고객 등급 지정
        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedUser == null)
            {
                MessageBox.Show("등급을 지정할 고객을 먼저 선택해주세요.");
                return;
            }
            else if (selectedUser.level.Equals("VIP고객"))
            {
                MessageBox.Show("현재 VIP 등급 입니다.");
            }

            selectedUser.level = "VIP고객";
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataManager.Users;
            DataManager.SaveUser();
        }
        // 고객의 회원탈퇴 기능
        private void button4_Click(object sender, EventArgs e)
        {
            if (selectedUser == null)
            {
                MessageBox.Show("회원탈퇴를 진행할 고객을 먼저 선택해주세요.");
                return;
            }
            DialogResult dr = MessageBox.Show("정말 탈퇴하시겠습니까?", "취소", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (dr == DialogResult.OK)
            {
                DataManager.Users.Remove(selectedUser);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Users;
                DataManager.SaveUser();
                MessageBox.Show("회원탈퇴 되었습니다.");
            }
        }
    }
}
