using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //配置文件信息
        private ConfigPara _cp = new ConfigPara();
        //日志文件路径矫正的 string path = _pathRoot + @"Config\";
     
        private string _pathRoot = AppDomain.CurrentDomain.BaseDirectory + @"Config\ConfigPara.json";
        private List<data> _Data = new List<data>();
        private List<string> _txt;
        private List<string> _Pasertxt;
        private int _TxtIndex;
        public Form1()
        {
            InitializeComponent();
        }

        public struct data
        {
            public string name;
            public string value;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            bool isSave = false;
            if (IP.SetNetworkAdapter(IPBox.Text, rdoAutoSetIP.Checked, SubTxt.Text))
            {
                if(!rdoAutoSetIP.Checked)
                {
                foreach (var item in _Data)
                {
                    if (item.name == NameBox.Text)
                    {
                        isSave=false;
                        break;
                    }
                    else
                    {
                        isSave = true;
                    }
                }
                if(_Data.Count==0)
                        isSave = true;

                    if (isSave)
                {
                    if (MessageBox.Show("设置成功！是否保存此ip信息？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        SaveTxt();
                        return;
                    }
                }
                }
                MessageBox.Show("设置成功");


            }
            else
            {
                MessageBox.Show("设置失败");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //test
            StringBuilder sb = new StringBuilder(
                "0" +
                " 1"

                );

            Refresh();
        }
        private void Refresh()
        {
            IPBox.Items.Clear();
            NameBox.Items.Clear();
            _Data.Clear();
            
            ReadFile(_pathRoot);

            if (_Data.Count == 0)
            {
                return;
            }
            foreach (data data in _Data)
            {
                IPBox.Items.Add(data.value);
                NameBox.Items.Add(data.name);
            }
        }
        private ConfigPara ReadConfig(string strPath)
        {
            string path = strPath + @"\Config\";
            string file = path + "ConfigPara.xml";

            if (!File.Exists(file))
            {
                return null;
            }

            FileStream fs = new FileStream(file, FileMode.Open);
            XmlSerializer xml = new XmlSerializer(typeof(ConfigPara));
            ConfigPara cp = xml.Deserialize(fs) as ConfigPara;
            fs.Close();
            return cp;
        }

        private void ReadFile(string strPath)
        {
           
            _txt = PaserFile(strPath);
            _Pasertxt = new List<string>();
            for (int i = 0; i < _txt.Count; i++)
            {
                if (_txt[i].Contains("{"))
                {
                    for (int j = i + 1; j < _txt.Count - i; j++)
                    {
                        if (_txt[j].Contains("}"))
                            break;
                        _Pasertxt.Add(_txt[j]);
                    }
                    _TxtIndex = i + 1;
                    break;
                }
            }
            
            if(_Pasertxt.Count==0)
            {
                return;
            }

            foreach (string p in _Pasertxt)
            {
                if (p.Trim() == "")
                    return;
                _Data.Add(paredata(p));
            }
        }
        public static List<string> PaserFile(string filename)
        {
            Encoding encoding = Encoding.Default;//指定编码类型
            using (StreamReader sr = new StreamReader(filename, Encoding.GetEncoding("utf-8")))
            {
                int index;
                string line;
                List<string> strs = new List<string>();
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "")
                        continue;
                    strs.Add(line);
                    //DatTxt.AppendLine(line);

                }

                return strs;

            }
        }

        public data paredata(string str)
        {
            
            int i = str.IndexOf(':');
            string name = str.Substring(0, i);
            string value = str.Substring(i + 1, str.Length - i - 1);
            name = name.Replace('"', ' ').Trim();
            value = value.Replace('"', ' ').Trim();
            if (value.Contains(','))
            {
                value = value.Replace(',', ' ').Trim();
            }
            return new data() { name = name, value = value };
        }

        private void IPBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in _Data)
            {
                if (item.value == IPBox.Text)
                {
                    NameBox.Text = item.name;
                }
            }
        }

        private void SaveTxt()
        {
            string str = _txt[_TxtIndex];
            if (_Data.Count != 0)
            {
                bool a = str.Contains(_Data[0].name);
                str = str.Replace(_Data[0].name, NameBox.Text);
                str = str.Replace(_Data[0].value, IPBox.Text);
                _txt[_TxtIndex] += "\r\n" + str;
            }
            else
            {
                str = $"\"{NameBox.Text}\": \"{IPBox.Text}\",";
                // str = str.Replace(_Data[0].value, IPBox.Text);
                _txt[_TxtIndex-1] += "\r\n" + str;
            }
          

            
           
            WriteDataToFile(_txt, _pathRoot);
            Refresh();
        }
        private void WriteDataToFile(List<string> data, string filePath)
        {
            // 打开一个文件流以写入数据  
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var line in data)
                {
                    // 将每个字符串写入文件，每行一个字符串  
                    writer.WriteLine(line);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (NameBox.Text != "" && IPBox.Text != "")
            {
                for (int i = 0; i < _txt.Count; i++)
                {
                    if(_txt[i].Contains(NameBox.Text))
                    {
                        _txt[i] = null;
                        WriteDataToFile(_txt, _pathRoot);
                        Refresh();
                        MessageBox.Show("删除成功");
                        NameBox.Text = "";
                        IPBox.Text = "";
                        return;
                    }
                }
                MessageBox.Show("未找到该数据");
            }
            else { MessageBox.Show("对于数据不完整"); }
        }

        private void NameBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in _Data)
            {
                if (item.name == NameBox.Text)
                {
                    IPBox.Text = item.value;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (IPBox.Text == "")
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click_1(sender,e);
            }
        }
    }
}
