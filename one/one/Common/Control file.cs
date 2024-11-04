using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using one.Model;
using System.IO;
using System.Xml.Serialization;

namespace one.Common
{
    public class Control_file
    {

        private Control_File_Model ReadConfigPara(string strPath)
        {
            string path = strPath;
            string file = path + "ConfigPara.xml";

            if (!File.Exists(file))
            {
                return null;
            }

            FileStream fs = new FileStream(file, FileMode.Open);
            XmlSerializer xml = new XmlSerializer(typeof(Control_File_Model));
            Control_File_Model cp = xml.Deserialize(fs) as Control_File_Model;
            fs.Close();
            return cp;
        }
    }
}
