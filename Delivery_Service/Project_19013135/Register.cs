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
    public partial class Register : Form
    {
        private string category;
        private Map map;
        private Calc calc;
        private List<City> root;
        private string mod = "일반택배";
        public Register()
        {
            InitializeComponent();

            // 도시 종류 콤보박스
            foreach (City city in DataManager.Citys)
            {
                if (!comboBox2.Items.Contains(city.Name))
                    comboBox2.Items.Add(city.Name);
            }
            foreach (City city in DataManager.Citys)
            {
                if (!comboBox3.Items.Contains(city.Name))
                    comboBox3.Items.Add(city.Name);
            }

            // 물품 종류 콤보박스
            string[] category = { "선택 필요!", "의류", "서신/서류", "가전제품류", "과일류", "곡물류", "한약류", "식품류", "잡화", "서적" };

            comboBox1.Items.AddRange(category);
            comboBox1.SelectedIndex = 0;
            // 파손 주의 상품 체크박스

            /*
            // 데이터 그리드 설정
            DataTable dt = new DataTable();
            dt.Columns.Add("이름", typeof(string));
            dt.Columns.Add("전화번호", typeof(string));
            dt.Columns.Add("주소", typeof(string));
            foreach (User user in DataManager.Users)
            {
                dt.Rows.Add(user.name, user.phone, user.address);
            }*/

            dataGridView1.DataSource = DataManager.Users;
            dataGridView1.Columns["userId"].Visible = false;
            dataGridView1.Columns["name"].HeaderText = "회원명";
            dataGridView1.Columns["phone"].HeaderText = "연락처";
            dataGridView1.Columns["address"].HeaderText = "주소";
            dataGridView1.Columns["level"].HeaderText = "회원등급";
            dataGridView1.Columns["total"].HeaderText = "이용요금";

            map = new Map(DataManager.Citys);
            calc = new Calc(map.GetMap(), map.row, map.col);

            button4.Enabled = false;
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0)
                this.category = comboBox1.SelectedItem as string;
        }
        // 데이터 입력 확인
        private bool TextBoxCheck()
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
                MessageBox.Show("보내는 사람의 이름이 입력되지 않았습니다.");
            else if (String.IsNullOrWhiteSpace(textBox2.Text))
                MessageBox.Show("보내는 사람의 전화번호가 입력되지 않았습니다.");
            else if (String.IsNullOrWhiteSpace(textBox3.Text))
                MessageBox.Show("보내는 사람의 주소가 입력되지 않았습니다.");
            else if (String.IsNullOrWhiteSpace(textBox4.Text))
                MessageBox.Show("받는 사람의 이름이 입력되지 않았습니다.");
            else if (String.IsNullOrWhiteSpace(textBox5.Text))
                MessageBox.Show("받는 사람의 전화번호가 입력되지 않았습니다.");
            else if (String.IsNullOrWhiteSpace(textBox6.Text))
                MessageBox.Show("받는 사람의 주소가 입력되지 않았습니다.");
            else if (comboBox1.SelectedIndex == 0)
                MessageBox.Show("물품 종류를 선택해 주세요.");
            else if (comboBox2.SelectedIndex == -1)
                MessageBox.Show("보내는 사람 주소를 확인해 주세요.");
            else if (comboBox3.SelectedIndex == -1)
                MessageBox.Show("받는 사람 주소를 확인해 주세요.");
            else
                return true; // 모든 입력 완료
            return false;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            User u = dataGridView1.CurrentRow.DataBoundItem as User;

            if (radioButton1.Checked == true)
            {
                textBox1.Text = u.name;
                textBox2.Text = u.phone;
                textBox3.Text = u.address.Substring(3, u.address.Length - 3);
                for (int i = 0; i < comboBox2.Items.Count; i++)
                {
                    if (comboBox2.Items[i].ToString() == u.address.Substring(0, 2))
                    {
                        comboBox2.SelectedIndex = i;
                        break;
                    }
                }
            }
            else if (radioButton2.Checked == true)
            {
                textBox4.Text = u.name;
                textBox5.Text = u.phone;
                textBox6.Text = u.address.Substring(3, u.address.Length - 3);
                for (int i = 0; i < comboBox3.Items.Count; i++)
                {
                    if (comboBox3.Items[i].ToString() == u.address.Substring(0, 2))
                    {
                        comboBox3.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        // 일반, 기업, 급송 택배에 대하여 요금 가중치 계산
        private int CalcFee(User user, List<City> root, int mod = 1)
        {
            if (mod == 2)
                this.mod = "기업택배";
            else if (mod == 3)
                this.mod = "급속택배";

            int base_price = 2000;
            double weight = 1.0;
            if (user.level.Equals("기업고객") && mod == 2)
            {
                base_price = 1000;
                weight = 0.5;
            }
            else if (user.level.Equals("VIP고객"))
            {
                base_price = 1000;
                weight = 0.8;
            }

            int fee = 0;
            for (int i = 1; i < root.Count; i++)
            {
                if (root[i].Name != root[i - 1].Name)
                    fee += root[i].Cost;
            }

            if (mod == 2 && !user.level.Equals("기업고객"))
                return -1;
            else if (mod == 3 && !user.level.Equals("기업고객"))
            {
                base_price = 4000;
                if (user.level.Equals("일반고객"))
                    fee = (int)(weight * fee * 1.3) + base_price;
                else
                    fee = (int)(weight * fee) + base_price;
            }
            else if (mod == 3 && user.level.Equals("기업고객"))
                return -2;
            else
                fee = (int)(weight * fee) + base_price;
            return fee;
        }

        // 일반택배에 대하여 경로 계산 및 요금 산정
        private void button1_Click(object s, EventArgs e)
        {
            // 데이터 입력을 확인한다.
            if (!TextBoxCheck())
                return;

            string address = comboBox2.Text + " " + textBox3.Text;
            User sender = new User()
            {
                userId = DataManager.GetNewUserId(),
                name = textBox1.Text,
                phone = textBox2.Text,
                address = address,
                level = "일반고객",
                total = 0
            };

            address = comboBox3.Text + " " + textBox6.Text;
            User receiver = new User()
            {
                userId = DataManager.GetNewUserId() + 1,
                name = textBox4.Text,
                phone = textBox5.Text,
                address = address,
                level = "일반고객",
                total = 0
            };
            User u1 = DataManager.FindUser(sender.name, sender.phone);
            User u2 = DataManager.FindUser(receiver.name, receiver.phone);
            if (u1 != null)
                sender = u1;
            if (u2 != null)
                receiver = u2;


            City send_city = DataManager.FindCity(comboBox2.Text);
            City receive_city = DataManager.FindCity(comboBox3.Text);

            List<City> root = calc.CalcFeeForCost(send_city, receive_city);
            this.root = root;
            int fee = CalcFee(sender, root, 1);
            if (fee >= 0)
            {
                int day = (int)(root.Count / 3) + 1;
                textBox7.Text = fee.ToString();
                textBox8.Text = day.ToString() + " Days";
                button4.Enabled = true;
                button4.Visible = true;
            }
            else if (fee == -1)
            {
                MessageBox.Show("일반/VIP고객은 기업택배 이용불가");
                textBox7.Clear();
                textBox8.Clear();
            }
            else if (fee == -2)
            {
                MessageBox.Show("기업고객은 급송 택배 이용불가");
                textBox7.Clear();
                textBox8.Clear();
            }
        }

        // 기업택배에 대하여 경로 계산 및 요금 산정
        private void button2_Click(object s, EventArgs e)
        {
            // 데이터 입력을 확인한다.
            if (!TextBoxCheck())
                return;

            string address = comboBox2.Text + " " + textBox3.Text;
            User sender = new User()
            {
                userId = DataManager.GetNewUserId(),
                name = textBox1.Text,
                phone = textBox2.Text,
                address = address,
                level = "일반고객",
                total = 0
            };

            address = comboBox3.Text + " " + textBox6.Text;
            User receiver = new User()
            {
                userId = DataManager.GetNewUserId() + 1,
                name = textBox4.Text,
                phone = textBox5.Text,
                address = address,
                level = "일반고객",
                total = 0
            };
            User u1 = DataManager.FindUser(sender.name, sender.phone);
            User u2 = DataManager.FindUser(receiver.name, receiver.phone);
            if (u1 != null)
                sender = u1;
            if (u2 != null)
                receiver = u2;


            City send_city = DataManager.FindCity(comboBox2.Text);
            City receive_city = DataManager.FindCity(comboBox3.Text);

            List<City> root = calc.CalcFeeForCost(send_city, receive_city);
            this.root = root;
            int fee = CalcFee(sender, root, 2);
            if (fee >= 0)
            {
                int day = (int)(root.Count / 3) + 1;
                textBox7.Text = fee.ToString();
                textBox8.Text = day.ToString() + " Days";
                button4.Enabled = true;
                button4.Visible = true;
            }
            else if (fee == -1)
            {
                MessageBox.Show("일반/VIP고객은 기업택배 이용불가");
                textBox7.Clear();
                textBox8.Clear();
            }
            else if (fee == -2)
            {
                MessageBox.Show("기업고객은 급송 택배 이용불가");
                textBox7.Clear();
                textBox8.Clear();
            }
        }
        // 급송택배에 대하여 경로 계산 및 요금 산정
        private void button3_Click(object s, EventArgs e)
        {
            // 데이터 입력을 확인한다.
            if (!TextBoxCheck())
                return;

            string address = comboBox2.Text + " " + textBox3.Text;
            User sender = new User()
            {
                userId = DataManager.GetNewUserId(),
                name = textBox1.Text,
                phone = textBox2.Text,
                address = address,
                level = "일반고객",
                total = 0
            };

            address = comboBox3.Text + " " + textBox6.Text;
            User receiver = new User()
            {
                userId = DataManager.GetNewUserId() + 1,
                name = textBox4.Text,
                phone = textBox5.Text,
                address = address,
                level = "일반고객",
                total = 0
            };
            User u1 = DataManager.FindUser(sender.name, sender.phone);
            User u2 = DataManager.FindUser(receiver.name, receiver.phone);
            if (u1 != null)
                sender = u1;
            if (u2 != null)
                receiver = u2;


            City send_city = DataManager.FindCity(comboBox2.Text);
            City receive_city = DataManager.FindCity(comboBox3.Text);

            List<City> root = calc.CalcFeeForCost(send_city, receive_city);
            this.root = root;
            int fee = CalcFee(sender, root, 3);
            if (fee >= 0)
            {
                int day = (int)(root.Count / 5);
                textBox7.Text = fee.ToString();
                textBox8.Text = day.ToString() + " Days";
                button4.Enabled = true;
                button4.Visible = true;
            }
            else if (fee == -1)
            {
                MessageBox.Show("일반/VIP고객은 기업택배 이용불가");
                textBox7.Clear();
                textBox8.Clear();
            }
            else if (fee == -2)
            {
                MessageBox.Show("기업고객은 급송 택배 이용불가");
                textBox7.Clear();
                textBox8.Clear();
            }
        }
        // 취소버튼을 눌렀을때 화면 초기화
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            button4.Enabled = false;
            button4.Visible = false;
        }
        // 모든 값 및 요금 산정 이후에 유저및 물품 데이터 등록
        private void button4_Click(object s, EventArgs e)
        {
            string address = comboBox2.Text + " " + textBox3.Text;
            User sender = new User()
            {
                userId = DataManager.GetNewUserId(),
                name = textBox1.Text,
                phone = textBox2.Text,
                address = address
            };

            address = comboBox3.Text + " " + textBox6.Text;
            User receiver = new User()
            {
                userId = DataManager.GetNewUserId() + 1,
                name = textBox4.Text,
                phone = textBox5.Text,
                address = address,
            };
            User u1 = DataManager.FindUser(sender.name, sender.phone);
            User u2 = DataManager.FindUser(receiver.name, receiver.phone);
            if (u1 != null)
                sender = u1;
            if (u2 != null)
                receiver = u2;

            sender.total += int.Parse(textBox7.Text);
            if (sender.total >= 50000) // 50000원 이용 고객은 자동으로 VIP로 승급
                sender.level = "VIP고객";

            if (!DataManager.Users.Exists(x => x.userId == sender.userId))
            {
                DataManager.Users.Add(sender);
            }
            if (!DataManager.Users.Exists(x => x.userId == receiver.userId))
            {
                DataManager.Users.Add(receiver);
            }

            Package package = new Package()
            {
                packageId = DataManager.GetNewPackageId(),
                type = comboBox1.SelectedItem.ToString(),
                fee = int.Parse(textBox7.Text),
                start = sender.address,
                end = receiver.address,
                address = sender.address.Substring(0, 2),
                TransTime = DateTime.Now,
                date = int.Parse(textBox8.Text.Substring(0, 1)),
                dday = int.Parse(textBox8.Text.Substring(0, 1)),
                status = false,
                transUserId = sender.userId,
                receiveUserId = receiver.userId,
                root = this.root,
                mod = this.mod
            };

            DataManager.Packages.Add(package);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataManager.Users;
            DataManager.SavePackages();
            DataManager.SaveUser();

            button5_Click(s, e); // 취소 버튼 실행
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UserTool usertool = new UserTool();
            usertool.ShowDialog();
        }

        private void 사용자택배관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserPackages userPackages = new UserPackages();
            userPackages.ShowDialog();
        }

        private void 관리자관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manager manager = new Manager();
            manager.ShowDialog();
        }
    }
}
