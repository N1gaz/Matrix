using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Matrix
{
    [Serializable]
    class TimeList
    {
        List<TimeItem> timeItems;

        public TimeList()
        {
            timeItems = new List<TimeItem>();
        }

        public void Add(TimeItem add)
        {
            timeItems.Add(add);
        }

        public override string ToString()
        {
            string ret = "";

            foreach (TimeItem i in timeItems)
            {
                ret += i.ToString() + "\n";
            }

            return ret;
        }

        public bool Load(string filename)
        {
            bool check = false;

            using (FileStream fs = File.OpenRead(filename))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                fs.Position = 0;
                TimeList buff = (TimeList)formatter.Deserialize(fs);
                this.timeItems = buff.timeItems;
                check = true;
            }

            return check;
        }

        public bool Save(string filename)
        {
            bool check = false;

            using (FileStream fs = File.Create(filename))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, this);
                check = true;
            }

            return check;
        }
    }
}
