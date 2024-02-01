using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace KC_GRAMMAR
{
    public partial class Form1 : Form
    {
        string terminal, neterminal, simvol;
        int pravila;
        List<string> term, neterm, praviloleft, praviloright, result, resultfinal,result1;
        List<string> listNeterm;
        List<string> Peredat;
        bool fffff = true;

        private void button1_Click(object sender, EventArgs e)
        {
            if (terminal.IndexOf(textBox1.Text) == -1)
            {
                if (!(terminal.Length == 2))
                {
                    terminal = terminal.Insert(terminal.Length - 1, "," + textBox1.Text);
                }
                else
                {
                    terminal = terminal.Insert(terminal.Length - 1, textBox1.Text);
                }
            }
            else
                Info(1);
            richTextBox1.Text = terminal;
            textBox1.Clear();
        }
        private void Info(int choose)
        {
            switch (choose)
            {
                case 1:
                    {
                        MessageBox.Show(
                            "В списке терминалов уже находится вводимый символ.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
                        break;
                    }
                case 2:
                    {
                        MessageBox.Show(
                            "В списке нетерминалов уже находится вводимый символ.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
                        break;
                    }
                case 3:
                    {
                        MessageBox.Show(
                            "Правила содержат символы, несодержащиеся ни в терминалах, ни в нетерминалах. И не равные e.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
                        break;
                    }
                case 4:
                    {
                        MessageBox.Show(
                            "Это не КС-грамматика.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
                        fffff = false;
                        break;
                    }
                case 5:
                    {
                        MessageBox.Show(
                            "Отсутствует язык грамматики.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
                        fffff = false;
                        break;
                    }
                case 6:
                    {
                        MessageBox.Show(
                            "Колач Андрей 3221",
                            "Автор",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
                        break;
                    }
                case 7:
                    {
                        MessageBox.Show(
                            "1.Задаёте набор терминальных символов.\r\n" +
                            "2.Задаёте набор нетерминальных символов.\r\n" +
                            "3.Задаёте стартовый символ грамматики.\r\n" +
                            "4.Задаёте количество правил вывода. Нажимаете кнопку 'Добавить'.\r\n" +
                            "5.Вводите правила вывода.\r\n" +
                            "6.Нажимаете кнопку 'Старт'.\r\n" +
                            "7.Кнопка '6 вариант' записывает значения 6 варианта.\r\n" +
                            "8.Для возвращения в исходное состояние нажимаете кнопку 'Сброс'.\r\n" +
                            "9.Пустой символ добавляется через e. Если в языке используется этот символ, его необходимо заменить.",
                            "Помощь.",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
                        break;
                    }
                default:
                    break;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            simvol = textBox3.Text;
            richTextBox3.Text = simvol;
            textBox3.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (neterminal.IndexOf(textBox2.Text) == -1)
            {
                if (!(neterminal.Length == 2))
                {
                    neterminal = neterminal.Insert(neterminal.Length - 1, "," + textBox2.Text);
                }
                else
                {
                    neterminal = neterminal.Insert(neterminal.Length - 1, textBox2.Text);
                }
            }
            else
                Info(2);
            richTextBox2.Text = neterminal;
            textBox2.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            terminal = "{}";
            richTextBox1.Text = terminal;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            neterminal = "{}";
            richTextBox2.Text = neterminal;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            pravila = Convert.ToInt32(numericUpDown1.Value);
            for (int i = 0; i < pravila; i++)
            {
                panel1.Controls.Add(new TextBox() { Name = i.ToString(), Location = new Point(10, i * 30) });
                panel1.Controls.Add(new Label() { Name = (i + 100).ToString(), Location = new Point(120, i * 30), Text = "⟶", Width = 20 });
                panel1.Controls.Add(new TextBox() { Name = (i + 200).ToString(), Location = new Point(150, i * 30) });
            }
            button7.Show();
            button8.Show();
            button4.Hide();
        }
        private void Proverki()
        {
            term = new List<string>();
            neterm = new List<string>();
            terminal = terminal.Substring(1, terminal.Length - 2);
            string[] subs = terminal.Split(',');
            for (int i = 0; i < subs.Length; i++)
            {
                term.Add(subs[i]);
            }
            neterminal = neterminal.Substring(1, neterminal.Length - 2);
            string[] subs1 = neterminal.Split(',');
            for (int i = 0; i < subs1.Length; i++)
            {
                neterm.Add(subs1[i]);
            }
            praviloleft = new List<string>();
            praviloright = new List<string>();
            for (int i = 0; i < pravila; i++)
            {
                praviloleft.Add((panel1.Controls[i.ToString()] as TextBox).Text);
                praviloright.Add((panel1.Controls[(i + 200).ToString()] as TextBox).Text);
            }
            int count1 = 0;
            for (int i = 0; i < praviloleft.Count; i++)
            {
                for (int j = 0; j < praviloleft[i].Length; j++)
                {
                    if (neterm.Contains(praviloleft[i][j].ToString()) || term.Contains(praviloleft[i][j].ToString()) || praviloleft[i][j] == 'e')
                    {

                    }
                    else
                        count1++;
                }
                for (int j = 0; j < praviloright[i].Length; j++)
                {
                    if (neterm.Contains(praviloright[i][j].ToString()) || term.Contains(praviloright[i][j].ToString()) || praviloright[i][j] == 'e')
                    {

                    }
                    else
                        count1++;
                }
                if (count1 > 0)
                {
                    Info(3);
                    break;
                }
            }
            if (count1 != 0)
                Restart();
        }
        private void Restart()
        {
            button7.Hide();
            button8.Hide();
            terminal = "{}";
            neterminal = "{}";
            simvol = "S";
            richTextBox3.Text = simvol;
            richTextBox1.Text = terminal;
            richTextBox2.Text = neterminal;
            panel1.Controls.Clear();
            numericUpDown1.Value = 1;
            Peredat.Clear();
            button4.Show();
        }
        private void ExistLanguage()
        {
            bool bd = true;
            listNeterm = new List<string>();
            List<string> listNet1;
            int f = 0;
            for (int i = 0; i < praviloleft.Count; i++)
            {
                if (praviloleft[i].Length != 1)
                    f++;
            }
            if(f > 0)
            {
                Info(4);
            }
            else
            {
                int iter = 0;
                while (bd)
                {
                    listNet1 = new List<string>();
                    for (int i = 0; i < praviloleft.Count; i++)
                    {
                        if (!listNeterm.Contains(praviloleft[i]))
                        {
                            listNet1.Add(praviloleft[i]);
                        }
                    }
                    for(int v = 0; v < listNet1.Count; v++)
                    {
                        for (int i = 0; i < praviloright.Count; i++)
                        {
                            int l2 = 0;
                            for (int j = 0; j < praviloright[i].Length; j++)
                            {
                                if (praviloleft[i] == listNet1[v])
                                {
                                    if (term.Contains(praviloright[i][j].ToString()) || praviloright[i][j] == 'e' || listNeterm.Contains(praviloright[i][j].ToString()))
                                        l2++;
                                }
                            }
                            if ((l2 == praviloright[i].Length) && (!listNeterm.Contains(listNet1[v])))
                                listNeterm.Add(listNet1[v]);
                        }
                    }
                    iter++;
                    if(iter > 10)
                    {
                        if (listNeterm.Contains(simvol) || iter > 10)
                            bd = false;
                    }
                }
                if (!listNeterm.Contains(simvol))
                    Info(5);
                else
                    WriteAll(0);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            term = new List<string>() {"x", "y", "z", "k", "#", "$"};
            neterm = new List<string>() {"S","X","Y","Z","K"};
            praviloleft = new List<string>() {"S", "S", "S", "X", "X", "X", "Y",
            "Y","Y","Y","Z","K","K",};
            praviloright = new List<string>() { "X", "Y", "Z", "x#X", "x#Y",
                "e", "Yy$","Yz$", "$", "e", "Zz$", "Kk$", "k$"};
            simvol = "S";
            ExistLanguage();
            NonGenerative();
            Vivod();
            UdalenieE();
            Cep();
            Form2 frm = new Form2(Peredat);
            frm.ShowDialog();

        }
        private void UdalenieE()
        {
            List<string> pravilo3 = new List<string>();
            for (int i = 0; i < praviloright.Count;i++)
            {
                if (praviloright[i].Contains("e"))
                {
                    if(!pravilo3.Contains(praviloleft[i]))
                    {
                        pravilo3.Add(praviloleft[i]);
                    }
                }
            }
            bool bd = true;
            int iter = 0;
            while(bd)
            {
                for (int i = 0; i < pravilo3.Count; i++)
                {
                    for(int j = 0; j < praviloright.Count; j++)
                    {
                        if (praviloright[j].Contains(pravilo3[i]))
                        {
                            if(!pravilo3.Contains(praviloleft[j]))
                            {
                                pravilo3.Add(praviloleft[j]);
                            }
                        }
                    }
                }
                iter++;
                if (iter > 20)
                    bd = false;
            }
            List<string> pravilo1, pravilo2;
            pravilo1 = new List<string>();
            pravilo2 = new List<string>();
            for(int i = 0; i < praviloright.Count; i++)
            {
                for(int j = 0; j < praviloright[i].Length;j++)
                {
                    if (pravilo3.Contains(praviloright[i][j].ToString()))
                        {
                        string d = praviloright[i];
                        d = d.Remove(j, 1);
                        pravilo1.Add(praviloleft[i]);
                        pravilo2.Add(d);
                    }
                }
            }
            for(int i = 0; i < pravilo1.Count; i++)
            {
                if (!praviloright.Contains(pravilo2[i]))
                {
                    praviloleft.Add(pravilo1[i]);
                    praviloright.Add(pravilo2[i]);
                }
            }
            List<string> pravilo5, pravilo4;
            pravilo5 = new List<string>();
            pravilo4 = new List<string>();
            for (int v = 0; v< praviloleft.Count;v++)
            {
                if (praviloright[v] != "e" && praviloright[v] != "")
                {
                    pravilo5.Add(praviloleft[v]);
                    pravilo4.Add(praviloright[v]);
                }
            }
            praviloleft = pravilo5;
            praviloright = pravilo4;
            if (pravilo3.Contains(simvol))
            {
                int v = 0;
                for (int i = 0; i < praviloright.Count; i++)
                {
                    if (praviloright[i].Contains(simvol))
                    {
                        v++;
                        break;
                    }
                }
                if (v < 1)
                {
                    praviloleft.Add(simvol);
                    praviloright.Add("e");
                }
                else
                {
                    string lastsimvol = simvol;
                    simvol = "Z";
                    praviloleft.Add(simvol);
                    praviloleft.Add(simvol);
                    neterm.Add("Z");
                    praviloright.Add(lastsimvol);
                    praviloright.Add("e");
                }
            }
            WriteAll(3);
        }
        private void WriteAll(int a)
        {
            switch(a)
            {
                case 0:
                    Peredat.Add("Алгоритм 1.1.Проверка существования языка грамматики.");
                    Peredat.Add("Результат:");
                    Peredat.Add("Язык существует.");
                    break;
                case 1:
                    Peredat.Add("Алгоритм 1.2.Устранение нетерминалов, не порождающих терминальных строк.");
                    Peredat.Add("Результат:");
                    break;
                case 2:
                    Peredat.Add("Алгоритм 1.3.Устранение недостижимых символов.");
                    Peredat.Add("Результат:");
                    break;
                case 3:
                    Peredat.Add("Алгоритм 1.4.Устранение ε-правил.");
                    Peredat.Add("Результат:");
                    break;
                case 4:
                    Peredat.Add("Алгоритм 1.5.Устранение цепных правил.");
                    Peredat.Add("Результат:");
                    break;
                default:
                    break;
            }
            string net = "Список нетерминалов: ";
            string t = "Список терминалов: ";
            string pravila;
            for(int i = 0; i < neterm.Count; i++)
            {
                net += neterm[i].ToString() + " ";
            }
            for (int i = 0; i < term.Count; i++)
            {
                t += term[i].ToString() + " ";
            }
            Peredat.Add(net);
            Peredat.Add(t);
            Peredat.Add("Правила: ");
            for(int i = 0; i < praviloleft.Count; i++)
            {
                pravila = praviloleft[i] + " -> " + praviloright[i];
                Peredat.Add(pravila);
            }
            
        }
        private void Cep()
        {
            bool bd;
            List<List<string>> ddd = new List<List<string>>();

            for (int d = 0; d < neterm.Count; d++)
            {
                bd = true;
                listNeterm = new List<string>();
                List<string> listNet1;
                int iter = 0;
                listNet1 = new List<string>();
                listNeterm.Add(neterm[d]);
                while (bd)
                {
                    for (int i = 0; i < praviloright.Count; i++)
                    {
                        if (listNeterm.Contains(praviloleft[i]))
                        {
                            if (neterm.Contains(praviloright[i]))
                            {
                                if (!listNeterm.Contains(praviloright[i]))
                                {
                                    listNeterm.Add(praviloright[i]);
                                }
                            }
                        }
                    }
                    iter++;
                    if (iter > 10)
                    {
                        bd = false;
                    }
                }
                ddd.Add(listNeterm);
            }
            for(int i = 0; i < ddd.Count; i++)
            {
                List<string> df = new List<string>();
                for (int j = 0; j < praviloleft.Count; j++)
                {
                    if (praviloleft[j] == ddd[i][0])
                    {
                        if ((praviloright[j].Length == 1) && neterm.Contains(praviloright[j]))
                        {
                            df = new List<string>();
                            for(int v = 1; v < ddd[i].Count;v++)
                            {
                                for(int l = 0; l < praviloleft.Count; l++)
                                {
                                    if (ddd[i][v] == praviloleft[l])
                                    {
                                        bool ffff = false;
                                        for(int dz = 0; dz < praviloright[l].Length; dz++)
                                        {
                                            if (term.Contains(praviloright[l][dz].ToString()))
                                            {
                                                ffff = true;
                                                break;
                                            }
                                        }
                                        if (ffff)
                                            df.Add(praviloright[l]);
                                    }
                                }
                            }
                        }
                    }
                }
                for(int j = 0; j < df.Count;j++)
                {
                    praviloleft.Add(ddd[i][0]);
                    praviloright.Add(df[j]);
                }
            }
            List<string> pravilo5, pravilo4;
            pravilo5 = new List<string>();
            pravilo4 = new List<string>();
            for(int i = 0; i < praviloleft.Count; i++)
            {
                int ddf = -1;
                for(int j = 0; j <ddd.Count;j++)
                {
                    if (praviloleft[i] == ddd[j][0])
                        ddf = j;
                }
                if (ddd[ddf].Contains(praviloright[i]))
                {

                }
                else
                {
                    pravilo4.Add(praviloleft[i]);
                    pravilo5.Add(praviloright[i]);
                }

            }
            
            praviloleft = pravilo4;
            praviloright = pravilo5;
            WriteAll(4);
        }

        private void NonGenerative()
        {
            List<string> pravilo1, pravilo2;
            pravilo1 = new List<string>();
            pravilo2 = new List<string>();
            List<string> newN = new List<string>();
            for(int i = 0; i < neterm.Count; i++)
            {
                if (!listNeterm.Contains(neterm[i]))
                {
                    newN.Add(neterm[i]);
                }
            }
            for(int i = 0; i < praviloright.Count; i++)
            {
                int v = 0;
                for(int j = 0; j < praviloright[i].Length; j++)
                {
                    if (newN.Contains(praviloright[i][j].ToString()))
                    {
                        v++;
                        break;
                    }
                }
                if (newN.Contains(praviloleft[i]))
                    v++;
                if (v < 1)
                {
                    pravilo1.Add(praviloleft[i]);
                    pravilo2.Add(praviloright[i]);
                }

            }
            for(int i =0; i < newN.Count; i++)
            {
                neterm.Remove(newN[i]);
            }
            praviloleft = pravilo1;
            praviloright = pravilo2;
            WriteAll(1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Info(7);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Info(6);
        }

        public static string FindCommonPrefix(List<string> strings)
        {
            if (strings == null || strings.Count == 0)
            {
                return "";
            }

            int prefixLength = strings.Min(s => s.Length);
            StringBuilder commonPrefix = new StringBuilder();

            for (int i = 0; i < prefixLength; i++)
            {
                char currentChar = strings[0][i];

                if (strings.All(s => s[i] == currentChar))
                {
                    commonPrefix.Append(currentChar);
                }
                else
                {
                    break;
                }
            }

            return commonPrefix.ToString();
        }
        private void Vivod()
        {
            result = new List<string>();
            result1 = new List<string>();
            resultfinal = new List<string>();
            bool bd = true;
            result.Add(simvol);
            int iter = 0;
            while (bd)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    for (int v = 0; v < result[i].Length; v++)
                    {
                       for(int j = 0;j < praviloleft.Count; j++)
                        {
                            if (result[i][v].ToString() == praviloleft[j])
                            {
                                for(int k = 0; k < praviloright[j].Length;k++)
                                {
                                    if (neterm.Contains(praviloright[j][k].ToString()) && !result.Contains(praviloright[j][k].ToString()) && !result1.Contains(praviloright[j][k].ToString()))
                                    {
                                        result1.Add(praviloright[j][k].ToString());
                                    }
                                }
                            }
                        }
                    }
                }
                for(int v = 0; v < result1.Count; v++)
                {
                    result.Add(result1[v]);
                }
                result1.Clear();
                iter++;
                if (iter > 10)
                {
                    bd = false;
                    for(int i = 0;i<result.Count;i++)
                    {
                        resultfinal.Add(result[i]);
                    }
                }
            }
            List<string> pravilo1, pravilo2;
            pravilo1 = new List<string>();
            pravilo2 = new List<string>();
            for (int i = 0; i < praviloright.Count;i++)
            {
                int v = 0;
                for(int j = 0; j < praviloright[i].Length;j++)
                {
                    if (neterm.Contains(praviloright[i][j].ToString()) && !resultfinal.Contains(praviloright[i][j].ToString()))
                    {
                        v++;
                        break;
                    }
                }
                if (!resultfinal.Contains(praviloleft[i]))
                    v++;
                if(v < 1)
                {
                    pravilo1.Add(praviloleft[i]);
                    pravilo2.Add(praviloright[i]);
                }    
            }
            praviloleft = pravilo1;
            praviloright = pravilo2;
            List<string> newN = new List<string>();
            for (int i = 0; i < neterm.Count; i++)
            {
                if (!resultfinal.Contains(neterm[i]))
                {
                    newN.Add(neterm[i]);
                }
            }
            for (int i = 0; i < newN.Count; i++)
            {
                neterm.Remove(newN[i]);
            }
            RemoveTerm();
            WriteAll(2);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            Proverki();
            ExistLanguage();
            if(fffff)
            {
                NonGenerative();
                Vivod();
                UdalenieE();
                Cep();
                Form2 frm = new Form2(Peredat);
                frm.ShowDialog();
            }
        }
        private void RemoveTerm()
        {
            int[] numoft = new int[term.Count];
            for(int v = 0; v <term.Count;v++)
            {
                for (int i = 0; i < praviloright.Count; i++)
                {
                    for (int j = 0; j < praviloright[i].Length; j++)
                    {
                        if (term[v] == praviloright[i][j].ToString())
                            numoft[v]++;
                }
                }
            }
            List<string> toDel = new List<string>();
            for(int i = 0; i < numoft.Length;i++)
            {
                if (numoft[i] == 0)
                {
                    toDel.Add(term[i]);
                }
            }
            for(int i = 0; i < toDel.Count; i++)
            {
                term.Remove(toDel[i]);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Restart();
        }

        public Form1()
        {
            InitializeComponent();
            button7.Hide();
            button8.Hide();
            terminal = "{}";
            neterminal = "{}";
            simvol = "S";
            richTextBox3.Text = simvol;
            richTextBox1.Text = terminal;
            richTextBox2.Text = neterminal;
            panel1.AutoScroll = true;
            richTextBox1.ReadOnly = true;
            richTextBox2.ReadOnly = true;
            richTextBox3.ReadOnly = true;
            button11.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Peredat = new List<string>();
        }
    }
}
