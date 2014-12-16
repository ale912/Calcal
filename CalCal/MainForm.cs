using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CalCal
{
    public partial class MainForm : Form
    {
        private readonly List<File1> _file1;
        private readonly List<Filter> _filters;

        private readonly List<Mouth> _mouths = new List<Mouth> { 
            new Mouth("Jan.", 1),
            new Mouth("Febr.", 2),
            new Mouth("Mars", 3),
            new Mouth("April", 4),
            new Mouth("May", 5),
            new Mouth("Juni", 6),
            new Mouth("Juli", 7),
            new Mouth("Aug.", 8),
            new Mouth("Sept.", 9),
            new Mouth("Oct.", 10),
            new Mouth("Nov.", 11),
            new Mouth("Dec.", 12)}; 

        public MainForm()
        {
            InitializeComponent();

            _file1 = new List<File1>();
            _filters = new List<Filter>();

            textBoxFile1.Text = 
                File.Exists(Environment.CurrentDirectory + "/LOVOZERO") ? 
                Environment.CurrentDirectory + @"/LOVOZERO" : @"Файл в корне прграммы отсутствует";

            textBoxFile2.Text = 
                File.Exists(Environment.CurrentDirectory + "/lovdat.txt") ? 
                Environment.CurrentDirectory + @"/lovdat.txt" : @"Файл в корне прграммы отсутствует";
        }

        private void Calculate()
        {
            if(_file1.Count == 0 || _filters.Count == 0) return;

            for (var i = 0; i < _filters.Count; i++)
            {
                int oldExp = 1;
                int oldGain = 1;

                using (
                    var sw =
                        new StreamWriter(
                            new FileStream(Environment.CurrentDirectory + "/cal" + _filters[i].Value + ".txt",
                                FileMode.Create, FileAccess.Write, FileShare.ReadWrite)))
                {
                    var file2 = _filters[i].File2;

                    for (var j = 0; j < _file1.Count - 1; j++)
                    {
                        var incAz = (_file1[j + 1].Az - _file1[j].Az) > 0;

                        int exp = 1;
                        int gain = 1;

                        double currentAz = _file1[j].Az;
                        switch (incAz)
                        {
                            case true:
                            {
                                if (file2[file2.Length - 1].Az > currentAz)
                                {
                                    exp = file2[file2.Length - 2].E;
                                    gain = file2[file2.Length - 2].G;
                                }
                                else
                                {
                                    if (file2[0].Az < currentAz)
                                    {
                                        exp = file2[0].E;
                                        gain = file2[0].G;
                                    }
                                    else
                                    {
                                        for (var k = file2.Length - 1; k > 0; k--)
                                        {
                                            if (currentAz > file2[k].Az && currentAz < file2[k - 1].Az)
                                            {
                                                exp = file2[k - 1].E;
                                                gain = file2[k - 1].G;

                                                break;
                                            }
                                        }
                                    }
                                    
                                }
                                break;
                            }
                            case false:
                            {
                                if (file2[file2.Length - 1].Az > currentAz)
                                {
                                    exp = file2[file2.Length - 1].E;
                                    gain = file2[file2.Length - 1].G;
                                }
                                else
                                {
                                    if (file2[0].Az < currentAz)
                                    {
                                        exp = file2[0].E;
                                        gain = file2[0].G;
                                    }
                                    else
                                    {
                                        for (var k = 0; k < file2.Length - 1; k++)
                                        {
                                            if (file2[k].Az > currentAz && file2[k + 1].Az < currentAz)
                                            {
                                                exp = file2[k].E;
                                                gain = file2[k].G;

                                                break;
                                            }
                                        }
                                    }
                                    
                                }
                                break;
                            }
                        }

                        if (exp == 1 && gain == 1) continue;
                        if (exp == oldExp && gain == oldGain) continue;

                        oldExp = exp;
                        oldGain = gain;

                        var currenTime = _file1[j].Time;
                        var line = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t",
                            currenTime.Month, currenTime.Day, currenTime.Hour, currenTime.Minute, exp, gain);

                        switch (_filters[i].Value)
                        {
                            case 4278:
                                {
                                    if (exp > 0)
                                        line += string.Format("{0}\t{1}", 5, 1);
                                    else
                                        line += string.Format("{0}\t{1}", 1, 0);

                                    break;
                                }
                            case 5577:
                                {
                                    if (exp > 0)
                                        line += string.Format("{0}\t{1}", 4, 1);
                                    else
                                        line += string.Format("{0}\t{1}", 1, 1);

                                    break;
                                }
                            case 4861:
                                {
                                    if (exp > 0)
                                        line += string.Format("{0}\t{1}", 3, 1);
                                    else
                                        line += string.Format("{0}\t{1}", 1, 0);

                                    break;
                                }
                            case 6300:
                                {
                                    line = line.Remove(line.Length - 2);
                                    if (exp > 0)
                                        line += string.Format("{0}\t{1}\t", 5, 1);
                                    else
                                        line += string.Format("{0}\t{1}\t", 1, 0);
                                    line += "0";

                                    break;
                                }
                            default:
                                {
                                    if (exp > 0)
                                        line += string.Format("{0}\t{1}", 1, 1);
                                    else
                                        line += string.Format("{0}\t{1}", 0, 1);

                                    break;
                                }
                        }

                        sw.WriteLine(line);
                    }
                }
            }
            
        }

        private void ReadFile2(string s)
        {
            var temp1 = new List<string>();

            using (var sr = new StreamReader(new FileStream(s, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                var line = sr.ReadLine();
                if (line == null) return;

                var mas = line.Split(' ');
                for (var i = 0; i < mas.Length; i++)
                    if(mas[i] != "")
                        temp1.Add(mas[i]);
            }

            var form = new Form
            {
                AutoSize = true,
                Text = @"Выбор фильтра"
            };

            var filters = new CheckBox[temp1.Count];
            int posY = 0;
            for (var i = 0; i < temp1.Count; i++)
            {
                posY = 20*i;
                filters[i] = new CheckBox
                {
                    Text = temp1[i],
                    Checked = true,
                    Location = new Point(20, posY)
                };
                form.Controls.Add(filters[i]);
            }

            var button = new Button
            {
                Text = @"Вычислить",
                Location = new Point(20, posY + 50)
            };

            button.Click += (sender, args) =>
            {
                for(var j = 0; j < filters.Length; j++)
                {
                    if(!filters[j].Checked) continue;

                    var file2 = new List<File2>();

                    using (var sr = new StreamReader(new FileStream(s, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                    {
                        sr.ReadLine();
                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            if (line == null) return;

                            var mas = line.Split(' ');
                            var temp2 = new List<string>();
                            for (var i = 0; i < mas.Length; i++)
                                if (mas[i] != "")
                                    temp2.Add(mas[i]);

                            if (temp2.Count == 0) continue;

                            double az = Convert.ToDouble(temp2[0].Replace('.', ','));
                            int e = Convert.ToInt32(temp2[j*2 + 1]);
                            int g = Convert.ToInt32(temp2[j*2 + 2]);

                            file2.Add(new File2(az, e, g));
                        }
                    }

                    _filters.Add(new Filter(Convert.ToInt32(filters[j].Text), file2.ToArray()));
                }
                
                form.Close();
            };

            form.Controls.Add(button);
            
            form.ShowDialog();
        }

        private void ReadFile1(string s)
        {
            using (var sr = new StreamReader(new FileStream(s, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                int year = -1;
                int mouth = -1;
                int day = -1;

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line == null) continue;

                    var mas = line.Split(' ');
                    var temp = new List<string>();
                    for (var i = 0; i < mas.Length; i++)
                        if(!mas[i].Equals(""))
                            temp.Add(mas[i]);

                    if (temp.Count == 0) continue;

                    if (temp[0] == "Year")
                    {

                        year = Convert.ToInt32(temp[2]);

                        if (IsMatchMouth(temp[3]))
                        {
                            mouth = GetMouth(temp[3]);
                            day = Convert.ToInt32(temp[4]);
                        }
                    }

                    if (year == -1 || mouth == -1) continue;

                    if (temp[0] == "Year") continue;
                    if (temp[0] == "Hour") continue;

                    if (temp.Count == 4)
                    {
                        int hour = Convert.ToInt32(temp[0]);
                        int min = Convert.ToInt32(temp[1]);
                        double az = Convert.ToDouble(temp[2].Replace('.', ','));

                        _file1.Add(new File1(new DateTime(year, mouth, day, hour, min, 0), az));
                    }

                    if (temp.Count == 5)
                    {
                        int hour = Convert.ToInt32(temp[0]);
                        int min = Convert.ToInt32(temp[1]);

                        double az = Convert.ToDouble(temp[2] + temp[3].Replace('.', ','));

                        _file1.Add(new File1(new DateTime(year, mouth, day, hour, min, 0), az));
                    }
                }
            }
        }

        private bool IsMatchMouth(string s)
        {
            var index = _mouths.FindIndex(m => s == m.Str);
            return index > -1;
        }

        private int GetMouth(string s)
        {
            return _mouths.Find(m => s == m.Str).Value;
        }

        private void buttonSet1_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() != DialogResult.OK) return;

            if (openFile.FileName == string.Empty) return;

            textBoxFile1.Text = openFile.FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() != DialogResult.OK) return;

            if (openFile.FileName == string.Empty) return;

            textBoxFile2.Text = openFile.FileName;
        }

        private void buttonCal_Click(object sender, EventArgs e)
        {
            _file1.Clear();
            _filters.Clear();

            ReadFile1(textBoxFile1.Text);
            ReadFile2(textBoxFile2.Text);
            Calculate();

            MessageBox.Show(@"Готово!");
        }
    }

    public struct File1
    {
        public DateTime Time;
        public double Az;

        public File1(DateTime time, double az)
        {
            Time = time;
            Az = az;
        }
    }

    public struct File2
    {
        public double Az;
        public int E;
        public int G;

        public File2(double az, int e, int g)
        {
            Az = az;
            E = e;
            G = g;
        }
    }

    public struct Filter
    {
        public File2[] File2;
        public int Value;

        public Filter(int value, File2[] file2)
        {
            File2 = file2;
            Value = value;
        }
    }

    public struct Mouth
    {
        public int Value;
        public string Str;

        public Mouth(string str, int value)
        {
            Value = value;
            Str = str;
        }
    }
}
