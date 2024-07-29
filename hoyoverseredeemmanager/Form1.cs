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
                add_code(cbx_games.Text, tb_codes.Text, tb_desc.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("작업 중 치명적인 오류가 발생하였습니다:\n" + ex + "\n\n프로그램이 종료되며, 저장하지 않은 정보는 손실됩니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }


        }

        //코드 추가(엔터키)
        private void tb_codes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_add_Click(sender, e);
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

        //엑셀 CSV 가져오기
        private void btn_loadcsv_Click(object sender, EventArgs e)
        {
            try
            {
                load_csv_file();
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
        private void add_code(string hyb_games, string hyb_codes, string hyb_descs)
        {
            //영숫자만 허용
            if (Regex.IsMatch(hyb_codes, @"^[a-zA-Z0-9]+$") == true)
            {

                //대문자로
                string hyb_c_upper = hyb_codes.ToUpper();

                //중복입력 방지용
                ListViewItem found_data = lvw_codes.FindItemWithText(hyb_c_upper);//검색 데이터

                if (found_data != null)
                {
                    string found_game = found_data.SubItems[0].Text; //검색된 게임
                    string found_code = found_data.SubItems[1].Text; //검색된 코드

                    //등록할 게임의 코드가 게임명과 코드가 같을경우 경고메시지 내보내고 중단. 아닐 경우 등록
                    if (found_game == hyb_games && found_code == hyb_c_upper)
                    {
                        MessageBox.Show("추가하려는 \"" + found_game + "\"의 \"" + found_code + "\" 코드는 \n이미 추가된 리딤코드입니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        goto clearcodefield;
                    }
                    //내용에 게임명만 입력한경우 등록 불가
                    else if (hyb_descs == "원신" || hyb_descs == "붕괴:스타레일" || hyb_descs == "젠레스 존 제로")
                    {
                        MessageBox.Show("허용되지 않은 내용입니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        goto clearcodefield;
                    }
                    else
                    {
                        //목록추가로 점프
                        goto addlist;
                    }
                }
                else
                {
                    goto addlist;
                }

            //리스트에 추가
            addlist:
                string[] rcodes = [hyb_games, hyb_c_upper, hyb_descs];
                ListViewItem rclview = new ListViewItem(rcodes);

                lvw_codes.Items.Add(rclview);

                //데이터 파일에 저장
                writefile();

            //리딤코드 입력창 비우기
            clearcodefield:
                tb_codes.Clear();
                tb_desc.Clear();

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
                    bw.Write(lvw_codes.Items[i].SubItems[2].Text);
                }
            }


        }

        //파일 읽기
        public void readfile()
        {
            lvw_codes.Items.Clear();

            //이전 버전 데이터 확인용(첫째 행에서 열 3개 단위로 읽어본 후 3번째 열에 게임명이 나오는지 확인)
            FileStream frs_chk = File.Open(@".\rcodestore.dat", FileMode.Open);
            BinaryReader br = new BinaryReader(frs_chk);

            //열 3개까지 불러오기
            string[] chk_data = { br.ReadString(), br.ReadString(), br.ReadString() };

            //다시 불러오기 위해 파일 닫기
            br.Close();

            //데이터 파일 열어서 목록에 데이터 뿌리기
            FileStream frs_read = File.Open(@".\rcodestore.dat", FileMode.Open);
            using (BinaryReader br2 = new BinaryReader(frs_read))
            {
                //이전 버전 데이터인지 확인해서 버전에 맞게 불러오기
                if (chk_data[2] == "원신" || chk_data[2] == "붕괴:스타레일" || chk_data[2] == "젠레스 존 제로")
                {
                    MessageBox.Show("이전 버전의 데이터가 발견되었습니다.\n만일을 대비하여 데이터를 CSV로 내보내 백업하십시오.");

                    while (br2.BaseStream.Position != br2.BaseStream.Length)
                    {
                        //두 문자열씩 읽어 배열 하나로 묶기
                        string[] b_rcodes = { br2.ReadString(), br2.ReadString(), "" };

                        //기존 데이터형식 확인

                        //리스트에 추가
                        ListViewItem fbrview = new ListViewItem(b_rcodes);
                        lvw_codes.Items.Add(fbrview);
                    }

                }
                else
                {
                    while (br2.BaseStream.Position != br2.BaseStream.Length)
                    {
                        //두 문자열씩 읽어 배열 하나로 묶기
                        string[] b_rcodes = { br2.ReadString(), br2.ReadString(), br2.ReadString() };

                        //기존 데이터형식 확인

                        //리스트에 추가
                        ListViewItem fbrview = new ListViewItem(b_rcodes);
                        lvw_codes.Items.Add(fbrview);
                    }
                }


            }
        }


        //CSV 가져오기
        public void load_csv_file()
        {
            if (load_csv.ShowDialog() == DialogResult.OK)
            {
                //목록에 추가할지 덮어쓸지
                if(MessageBox.Show("데이터를 가져옵니다.\n현재 내용을 덮어쓰게 됩니다!\n\n계속하시겠습니까?","확인",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //목록 비우기
                    lvw_codes.Items.Clear();

                    //파일처리
                    string loadcsvpath = load_csv.FileName; //선택한 파일
                    string[] csv_loaded = File.ReadAllLines(loadcsvpath); //모든 줄 긁어오기

                    //목록에 추가(2번째 줄부터)
                    for (int i = 1; i < csv_loaded.Length; i++)
                    {
                        //쉼표 구분으로 쪼개기
                        string[] load_cont = csv_loaded[i].Split(',');

                        //목록에 추가
                        string[] list_csvitem = { load_cont[0], load_cont[1], load_cont[2] };
                        ListViewItem loadcsvitem = new ListViewItem(list_csvitem);
                        lvw_codes.Items.Add(loadcsvitem);

                    }

                    //데이터 파일 저장
                    writefile();
                }


                

            }
        }

        //CSV파일 저장
        public void save_csv_file()
        {
            if (save_csv.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter csvwrt = new StreamWriter(save_csv.FileName, false, Encoding.UTF8))
                {
                    //열 제목
                    csvwrt.WriteLine("게임,리딤코드,내용");

                    //열 내용
                    int listcnt = lvw_codes.Items.Count;
                    for (int i = 0; i < listcnt; i++)
                    {
                        csvwrt.WriteLine(lvw_codes.Items[i].SubItems[0].Text + "," + lvw_codes.Items[i].SubItems[1].Text + "," + lvw_codes.Items[i].SubItems[2].Text);
                    }
                }
            }
        }

        //리딤코드 사용하기
        public void get_redeem(string exec_mode)
        {
            //선택한 정보 가져오기
            if (lvw_codes.SelectedItems.Count != 0)
            {
                // URL용 변수
                string red_url = string.Empty;

                string s_games = lvw_codes.Items[lvw_codes.FocusedItem.Index].SubItems[0].Text; //선택한 열의 게임
                string s_code = lvw_codes.Items[lvw_codes.FocusedItem.Index].SubItems[1].Text; //선택한 열의 리딤코드
                string s_desc = "[" + s_games + "] 리딤코드 - " + lvw_codes.Items[lvw_codes.FocusedItem.Index].SubItems[2].Text + Environment.NewLine; //링크 위 설명

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
                if (exec_mode == "use_code")
                {
                    //웹페이지 띄우기(기본 브라우저로 설정한 브라우저로 리딤코드 페이지 열기)
                    Process.Start(new ProcessStartInfo { FileName = red_url, UseShellExecute = true });
                }
                else if (exec_mode == "copy_clip")
                {
                    //클립보드 복사
                    Clipboard.SetText(s_desc + red_url);
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
