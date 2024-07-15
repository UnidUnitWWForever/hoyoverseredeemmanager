using System.Diagnostics;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;

namespace hoyoverseredeemmanager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //�����ڵ� �������� URL
        public string gsn_redeem = "https://genshin.hoyoverse.com/ko/gift?code="; //����
        public string hsr_redeem = "https://hsr.hoyoverse.com/gift?code="; //�ر�:��Ÿ����
        public string zzz_redeem = "https://zenless.hoyoverse.com/redemption?code="; //������ �� ����

        //�ε� �� �ʱ�ȭ
        private void Form1_Load(object sender, EventArgs e)
        {
            //����Ʈ�ڽ� �ʱ�ȭ
            cbx_games.Text = "����";

            //���� �̹��� �ҷ����� �õ��غ��� �����ϸ� ����
            try
            {
                pbx_launcher.Image = Image.FromFile(@".\launcher.bmp");
            }
            catch (Exception)
            {
                MessageBox.Show("���� �̹��� �ҷ����� ����.\n���� �����մϴ�.", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            //����� ������ ������ ������ �ҷ�����
            string rc_datafile = @"./rcodestore.dat";

            if (File.Exists(rc_datafile))
            {
                readfile();
            }

        }
        
        //�� ������ ����
        private void lvw_codes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            
        }

        //�ڵ� �߰�
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                add_code(cbx_games.Text, tb_codes.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("�۾� �� ġ������ ������ �߻��Ͽ����ϴ�:\n"+ex+"\n\n���α׷��� ����Ǹ�, �������� ���� ������ �սǵ˴ϴ�.", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            
        }

        //������ �ڵ� ���
        private void btn_getredeem_Click(object sender, EventArgs e)
        {
            try
            {
                get_redeem("use_code");
            }
            catch (Exception ex)
            {
                MessageBox.Show("�۾� �� ġ������ ������ �߻��Ͽ����ϴ�:\n" + ex + "\n\n���α׷��� ����Ǹ�, �������� ���� ������ �սǵ˴ϴ�.", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            
        }

        //URL ����
        private void btn_copyurl_Click(object sender, EventArgs e)
        {
            try
            {
                get_redeem("copy_clip");
            }
            catch (Exception ex)
            {
                MessageBox.Show("�۾� �� ġ������ ������ �߻��Ͽ����ϴ�:\n" + ex + "\n\n���α׷��� ����Ǹ�, �������� ���� ������ �սǵ˴ϴ�.", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            
        }

        //���� CSV ��������
        private void btn_savecsv_Click(object sender, EventArgs e)
        {
            try
            {
                save_csv_file();
            }
            catch (Exception ex)
            {
                MessageBox.Show("�۾� �� ġ������ ������ �߻��Ͽ����ϴ�:\n" + ex + "\n\n���α׷��� ����Ǹ�, �������� ���� ������ �սǵ˴ϴ�.", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            
        }

        //����
        private void btn_del_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("�۾� �� ġ������ ������ �߻��Ͽ����ϴ�:\n" + ex + "\n\n���α׷��� ����Ǹ�, �������� ���� ������ �սǵ˴ϴ�.", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            remove_code();
        }

        //����
        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        //���⼭���ʹ� �����Լ�

        //�ڵ� �߰�
        private void add_code(string hyb_games, string hyb_codes)
        {
            //�����ڸ� ���
            if (Regex.IsMatch(hyb_codes, @"^[a-zA-Z0-9]+$") == true)
            {
                //�빮�ڷ�
                string hyb_c_upper = hyb_codes.ToUpper();

                //����Ʈ�� �߰�
                string[] rcodes = { hyb_games, hyb_c_upper };
                ListViewItem rclview = new ListViewItem(rcodes);

                lvw_codes.Items.Add(rclview);

                //������ ���Ͽ� ����
                writefile();

                //�����ڵ� �Է�â ����
                tb_codes.Clear();

            }
            else if (hyb_codes == "")
            {
                MessageBox.Show("�����ڵ带 �Է��Ͻʽÿ�.", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("����, ���ڸ� ���˴ϴ�.", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //���� ����
        public void writefile()
        {
            FileStream fstream = File.Open(@".\rcodestore.dat", FileMode.Create);
            using (BinaryWriter bw = new BinaryWriter(fstream))
            {
                int listcnt = lvw_codes.Items.Count;
                for (int i = 0; i < listcnt; i++)
                {
                    bw.Write(lvw_codes.Items[i].SubItems[0].Text);
                    bw.Write(lvw_codes.Items[i].SubItems[1].Text);
                }
            }


        }

        //���� �б�
        public void readfile()
        {
            lvw_codes.Items.Clear();
            using (BinaryReader br = new BinaryReader(File.Open(@".\rcodestore.dat", FileMode.Open)))
            {

                while (br.BaseStream.Position != br.BaseStream.Length)
                {
                    //�� ���ڿ��� �о� �迭 �ϳ��� ����
                    string[] b_rcodes = { br.ReadString(), br.ReadString() };

                    //����Ʈ�� �߰�
                    ListViewItem fbrview = new ListViewItem(b_rcodes);
                    lvw_codes.Items.Add(fbrview);
                }
            }
        }


        //CSV���� ����
        public void save_csv_file()
        {
            if (save_csv.ShowDialog() == DialogResult.OK)
            {
                using(StreamWriter csvwrt = new StreamWriter(save_csv.FileName, false, Encoding.UTF8))
                {
                    //�� ����
                    csvwrt.WriteLine("����,�����ڵ�");

                    //�� ����
                    int listcnt = lvw_codes.Items.Count;
                    for (int i = 0; i < listcnt; i++)
                    {
                        csvwrt.WriteLine(lvw_codes.Items[i].SubItems[0].Text + "," + lvw_codes.Items[i].SubItems[1].Text);
                    }
                }
            }
        }

        //�����ڵ� ����ϱ�
        public void get_redeem(string exec_mode)
        {
            //������ ���� ��������
            if(lvw_codes.SelectedItems.Count != 0)
            {
                // URL�� ����
                string red_url = string.Empty;

                string s_games = lvw_codes.Items[lvw_codes.FocusedItem.Index].SubItems[0].Text; //������ ���� ����
                string s_code = lvw_codes.Items[lvw_codes.FocusedItem.Index].SubItems[1].Text; //������ ���� �����ڵ�

                //������ ������ ������ URL ����
                switch (s_games)
                {
                    case "����":
                        red_url = gsn_redeem + s_code;
                        break;

                    case "�ر�:��Ÿ����":
                        red_url = hsr_redeem + s_code;
                        break;

                    case "������ �� ����":
                        red_url = zzz_redeem + s_code;
                        break;
                }

                //������
                if(exec_mode == "use_code")
                {
                    //�������� ����(�⺻ �������� ������ �������� �����ڵ� ������ ����)
                    Process.Start(new ProcessStartInfo { FileName = red_url, UseShellExecute = true });
                }
                else if(exec_mode == "copy_clip")
                {
                    //Ŭ������ ����
                    Clipboard.SetText(red_url);
                    MessageBox.Show("�����ڵ� ��� URL�� Ŭ�����忡 �����Ͽ����ϴ�.\nģ���� �޽��� ��ȭâ �Ǵ� SNS�Խù��� �ٿ������ʽÿ�.", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            else
            {
                MessageBox.Show("������ �����ڵ尡 �����ϴ�.", "���", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //�ڵ� ����
        public void remove_code()
        {
            //������ ���� ��������
            if (lvw_codes.SelectedItems.Count != 0)
            {
                //���� �׸� ����
                int rem_code = lvw_codes.FocusedItem.Index;
                lvw_codes.Items.RemoveAt(rem_code);

                //���泻�� ������ ����
                writefile();

            }
            else
            {
                MessageBox.Show("������ �����ڵ尡 �����ϴ�.", "���", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
            
       
    }
}
