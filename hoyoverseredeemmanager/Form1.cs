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

        //리딤코드 웹페이지 URL
        public string gsn_redeem = "https://genshin.hoyoverse.com/ko/gift?code="; //원신
        public string hsr_redeem = "https://hsr.hoyoverse.com/gift?code="; //붕괴:스타레일
        public string zzz_redeem = "https://zenless.hoyoverse.com/redemption?code="; //젠레스 존 제로

        //로드 시 초기화
        private void Form1_Load(object sender, EventArgs e)
        {
            //리스트박스 초기화
            cbx_games.Text = "원신";

            //런쳐 이미지 불러오기 시도해보고 실패하면 종료
            try
            {
                pbx_launcher.Image = Image.FromFile(@".\launcher.bmp");
            }
            catch (Exception)
            {
                MessageBox.Show("런쳐 이미지 불러오기 실패.\n앱을 종료합니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            //저장된 데이터 파일이 있으면 불러오기
            string rc_datafile = @"./rcodestore.dat";

            if (File.Exists(rc_datafile))
            {
                readfile();
            }

        }
        
        //열 누르면 정렬
        private void lvw_codes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            
        }

        //코드 추가
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                add_code(cbx_games.Text, tb_codes.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("작업 중 치명적인 오류가 발생하였습니다:\n"+ex+"\n\n프로그램이 종료되며, 저장하지 않은 정보는 손실됩니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            
        }

        //선택한 코드 사용
        private void btn_getredeem_Click(object sender, EventArgs e)
        {
            try
            {
                get_redeem("use_code");
            }
            catch (Exception ex)
            {
                MessageBox.Show("작업 중 치명적인 오류가 발생하였습니다:\n" + ex + "\n\n프로그램이 종료되며, 저장하지 않은 정보는 손실됩니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            
        }

        //URL 복사
        private void btn_copyurl_Click(object sender, EventArgs e)
        {
            try
            {
                get_redeem("copy_clip");
            }
            catch (Exception ex)
            {
                MessageBox.Show("작업 중 치명적인 오류가 발생하였습니다:\n" + ex + "\n\n프로그램이 종료되며, 저장하지 않은 정보는 손실됩니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            
        }

        //엑셀 CSV 내보내기
        private void btn_savecsv_Click(object sender, EventArgs e)
        {
            try
            {
                save_csv_file();
            }
            catch (Exception ex)
            {
                MessageBox.Show("작업 중 치명적인 오류가 발생하였습니다:\n" + ex + "\n\n프로그램이 종료되며, 저장하지 않은 정보는 손실됩니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            
        }

        //삭제
        private void btn_del_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("작업 중 치명적인 오류가 발생하였습니다:\n" + ex + "\n\n프로그램이 종료되며, 저장하지 않은 정보는 손실됩니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            remove_code();
        }

        //종료
        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        //여기서부터는 범용함수

        //코드 추가
        private void add_code(string hyb_games, string hyb_codes)
        {
            //영숫자만 허용
            if (Regex.IsMatch(hyb_codes, @"^[a-zA-Z0-9]+$") == true)
            {
                //대문자로
                string hyb_c_upper = hyb_codes.ToUpper();

                //리스트에 추가
                string[] rcodes = { hyb_games, hyb_c_upper };
                ListViewItem rclview = new ListViewItem(rcodes);

                lvw_codes.Items.Add(rclview);

                //데이터 파일에 저장
                writefile();

                //리딤코드 입력창 비우기
                tb_codes.Clear();

            }
            else if (hyb_codes == "")
            {
                MessageBox.Show("리딤코드를 입력하십시오.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("영문, 숫자만 허용됩니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //파일 저장
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

        //파일 읽기
        public void readfile()
        {
            lvw_codes.Items.Clear();
            using (BinaryReader br = new BinaryReader(File.Open(@".\rcodestore.dat", FileMode.Open)))
            {

                while (br.BaseStream.Position != br.BaseStream.Length)
                {
                    //두 문자열씩 읽어 배열 하나로 묶기
                    string[] b_rcodes = { br.ReadString(), br.ReadString() };

                    //리스트에 추가
                    ListViewItem fbrview = new ListViewItem(b_rcodes);
                    lvw_codes.Items.Add(fbrview);
                }
            }
        }


        //CSV파일 저장
        public void save_csv_file()
        {
            if (save_csv.ShowDialog() == DialogResult.OK)
            {
                using(StreamWriter csvwrt = new StreamWriter(save_csv.FileName, false, Encoding.UTF8))
                {
                    //열 제목
                    csvwrt.WriteLine("게임,리딤코드");

                    //열 내용
                    int listcnt = lvw_codes.Items.Count;
                    for (int i = 0; i < listcnt; i++)
                    {
                        csvwrt.WriteLine(lvw_codes.Items[i].SubItems[0].Text + "," + lvw_codes.Items[i].SubItems[1].Text);
                    }
                }
            }
        }

        //리딤코드 사용하기
        public void get_redeem(string exec_mode)
        {
            //선택한 정보 가져오기
            if(lvw_codes.SelectedItems.Count != 0)
            {
                // URL용 변수
                string red_url = string.Empty;

                string s_games = lvw_codes.Items[lvw_codes.FocusedItem.Index].SubItems[0].Text; //선택한 열의 게임
                string s_code = lvw_codes.Items[lvw_codes.FocusedItem.Index].SubItems[1].Text; //선택한 열의 리딤코드

                //선택한 정보를 가지고 URL 생성
                switch (s_games)
                {
                    case "원신":
                        red_url = gsn_redeem + s_code;
                        break;

                    case "붕괴:스타레일":
                        red_url = hsr_redeem + s_code;
                        break;

                    case "젠레스 존 제로":
                        red_url = zzz_redeem + s_code;
                        break;
                }

                //실행모드
                if(exec_mode == "use_code")
                {
                    //웹페이지 띄우기(기본 브라우저로 설정한 브라우저로 리딤코드 페이지 열기)
                    Process.Start(new ProcessStartInfo { FileName = red_url, UseShellExecute = true });
                }
                else if(exec_mode == "copy_clip")
                {
                    //클립보드 복사
                    Clipboard.SetText(red_url);
                    MessageBox.Show("리딤코드 등록 URL을 클립보드에 복사하였습니다.\n친구의 메신저 대화창 또는 SNS게시물에 붙여넣으십시오.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            else
            {
                MessageBox.Show("선택한 리딤코드가 없습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //코드 삭제
        public void remove_code()
        {
            //선택한 정보 가져오기
            if (lvw_codes.SelectedItems.Count != 0)
            {
                //선택 항목 삭제
                int rem_code = lvw_codes.FocusedItem.Index;
                lvw_codes.Items.RemoveAt(rem_code);

                //변경내용 데이터 저장
                writefile();

            }
            else
            {
                MessageBox.Show("선택한 리딤코드가 없습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
            
       
    }
}
