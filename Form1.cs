using MySql.Data.MySqlClient;
using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
struct GesturalData
{
    public int box;
    public float x, y, z;
}
struct NAOData
{
    public int box;
    public ulong time;
}
namespace Database_Connectivity_Module
{
    public partial class Form1 : Form
    {
        bool connected = false;
        ArrayList gesturalData = new ArrayList();
        ArrayList naoData = new ArrayList();
        string midiData = null;
        MySqlConnection conn = null;
        MySqlCommand command = null;
        public Form1()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //connect to database
        private void button1_Click(object sender, EventArgs e)
        {
            if(connected)
            {
                MessageBox.Show("You are already connected to a database!");
                return;
            }
            string connectionString = "Server=devinraposo.com;database=devinrap_blackout;User ID=devinrap_devin;Password=lizardR64!;Port=3306;SslMode=Preferred;";
            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                command = conn.CreateCommand();
                command.CommandText = "INSERT INTO LOCATIONS(ID,State,City,Address,Start_Date,End_Date) VALUES"
                    + "(2500,'Florida','Cape Coral','217 SE 6th Ter.','2017-11-10','2017-11-12');";
                command.ExecuteNonQuery();
                command.CommandText = "SELECT * FROM LOCATIONS WHERE State = 'Florida';";
                MySqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        System.Diagnostics.Debug.Write(reader.GetString("State") + " ");
                        System.Diagnostics.Debug.Write(reader.GetString("City") + " ");
                        System.Diagnostics.Debug.Write(reader.GetString("Address") + " ");
                        System.Diagnostics.Debug.Write(reader.GetString("Start_Date") + " ");
                        System.Diagnostics.Debug.WriteLine(reader.GetString("End_Date"));
                    }
                }
                connected = true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                connected = false;
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show(ex.Message);
                connected = false;
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        //download data
        private void button2_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                MessageBox.Show("You must be connected to an SQL database to download data!");
                return;
            }
            if (uploadListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must choose data to download first!");
                return;
            }
            try
            {
                string curItem = uploadListBox.SelectedItem.ToString();
                command.CommandText = "SELECT BodyPart, Xcoord, Ycoord, Zcoord FROM Gestural "
                    + "WHERE UploadTime = '" + curItem + "';";
                MySqlDataReader reader = command.ExecuteReader();
                StringBuilder temp = new StringBuilder(Application.StartupPath + "/gestural0.txt");
                int i = 0;
                while (File.Exists(temp.ToString()))
                {
                    ++i;
                    temp = new StringBuilder(Application.StartupPath + "/gestural");
                    temp.Append(i.ToString());
                    temp.Append(".txt");
                }
                using (StreamWriter writeText = new StreamWriter(temp.ToString()))
                {
                    if (reader.HasRows)
                    {
                        int j = 0;
                        while (reader.Read())
                        {
                            int bodyPart = reader.GetInt32("BodyPart");
                            float x = reader.GetFloat("Xcoord");
                            float y = reader.GetFloat("Ycoord");
                            float z = reader.GetFloat("Zcoord");
                            if (j == 0)
                            {
                                writeText.Write("" + bodyPart.ToString() + " " + x.ToString() + " " + y.ToString() + " " + z.ToString());
                                ++j;
                            }
                            else writeText.Write("\n" + bodyPart.ToString() + " " + x.ToString() + " " + y.ToString() + " " + z.ToString());
                        }
                    }
                }
                i = 0;
                temp = new StringBuilder(Application.StartupPath + "/nao0.txt");
                while (File.Exists(temp.ToString()))
                {
                    ++i;
                    temp = new StringBuilder(Application.StartupPath + "/nao");
                    temp.Append(i.ToString());
                    temp.Append(".txt");
                }
                command.CommandText = "SELECT BoxIndex, Time FROM NAO WHERE UploadTime = '" + curItem + "';";
                reader.Close();
                reader = command.ExecuteReader();
                using (StreamWriter writeText = new StreamWriter(temp.ToString()))
                {
                    if (reader.HasRows)
                    {
                        int j = 0;
                        while (reader.Read())
                        {
                            int boxIndex = reader.GetInt32("BoxIndex");
                            ulong time = reader.GetUInt64("Time");
                            if (j == 0)
                            {
                                writeText.Write("" + boxIndex.ToString() + " " + time.ToString());
                                ++j;
                            }
                            else writeText.Write("\n" + boxIndex.ToString() + " " + time.ToString());
                        }
                    }
                }
                i = 0;
                temp = new StringBuilder(Application.StartupPath + "/test0.mid");
                while (File.Exists(temp.ToString()))
                {
                    ++i;
                    temp = new StringBuilder(Application.StartupPath + "/test");
                    temp.Append(i.ToString());
                    temp.Append(".mid");
                }
                reader.Close();
                command.CommandText = "SELECT Data FROM MIDI WHERE UploadTime = '" + curItem + "';";
                reader = command.ExecuteReader();
                using (StreamWriter writeText = new StreamWriter(temp.ToString()))
                {
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            StringBuilder temp2 = new StringBuilder(reader.GetString("Data"));
                            int j = 0;
                            while(j < temp2.Length)
                            {
                                if (temp2[j] == '\'' && temp2[j + 1] == '\'') temp2.Remove(j, 1);
                                else ++j;
                            }
                            writeText.Write(temp2.ToString());
                        }
                    }
                }
                reader.Close();
                MessageBox.Show("Successfully downloaded chosen data to files!");
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void loadFileButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(gesturalPath.Text))
                MessageBox.Show("Could not load gestural data file. Please enter a correct filepath.");
            else if (!File.Exists(naoPath.Text))
                MessageBox.Show("Could not load NAO data file. Please enter a correct filepath.");
            else if (!File.Exists(midiPath.Text))
                MessageBox.Show("Could not load MIDI data file. Please enter a correct filepath.");
            else
            {
                //Parse gestural data
                using (StreamReader sr = File.OpenText(gesturalPath.Text))
                {
                    gesturalData.Clear();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        GesturalData temp = new GesturalData();
                        StringBuilder temp2 = new StringBuilder();
                        int j = 0;
                        while (line[j] != ' ') temp2.Append(line[j++]);
                        ++j;
                        temp.box = int.Parse(temp2.ToString());
                        for (int k = 1; k <= 3; ++k)
                        {
                            temp2 = new StringBuilder();
                            while(j < line.Length && line[j] != ' ') temp2.Append(line[j++]);
                            ++j;
                            switch(k)
                            {
                                case 1:
                                    temp.x = float.Parse(temp2.ToString());
                                    break;
                                case 2:
                                    temp.y = float.Parse(temp2.ToString());
                                    break;
                                case 3:
                                    temp.z = float.Parse(temp2.ToString());
                                    break;
                            }
                        }
                        gesturalData.Add(temp);
                    }
                }
                //Parse NAO data
                using (StreamReader sr = File.OpenText(naoPath.Text))
                {
                    naoData.Clear();
                    string line;
                    while((line = sr.ReadLine()) != null)
                    {
                        NAOData temp = new NAOData();
                        temp.box = (int)char.GetNumericValue(line[0]);
                        int k = 2;
                        StringBuilder temp2 = new StringBuilder();
                        while (k < line.Length) temp2.Append(line[k++]);
                        temp.time = ulong.Parse(temp2.ToString());
                        naoData.Add(temp);
                    }
                }
                //Parse MIDI data
                StringBuilder temp3 = new StringBuilder(File.ReadAllText(midiPath.Text));
                int i = 0;
                while(i < temp3.Length)
                {
                    if (temp3[i] == '\'')
                    {
                        temp3.Insert(i, '\'');
                        ++i;
                    }
                    ++i;
                }
                midiData = temp3.ToString();
                MessageBox.Show("Successfully loaded all data!");
            }
        }
        private void uploadButton_Click(object sender, EventArgs e)
        {
            if(!connected)
            {
                MessageBox.Show("You must be connected to an SQL database to upload data!");
                return;
            }
            try
            {
                command = new MySqlCommand("SELECT NOW()", conn);
                System.DateTime uploadTime = (System.DateTime)command.ExecuteScalar();
                for (int i = 0; i < gesturalData.Count; ++i)
                {
                    GesturalData data = (GesturalData)gesturalData[i];
                    command.CommandText = "INSERT INTO Gestural (BodyPart, Xcoord, Ycoord, Zcoord, UploadTime) VALUES"
                        + "('" + data.box + "','" + data.x + "','" + data.y + "','" + data.z + "','"
                        + uploadTime.ToString() + "');";
                    command.ExecuteNonQuery();
                }
                for (int i = 0; i < naoData.Count; ++i)
                {
                    NAOData data = (NAOData)naoData[i];
                    command.CommandText = "INSERT INTO NAO (BoxIndex, Time, UploadTime) VALUES"
                        + "('" + data.box + "','" + data.time + "','"
                        + uploadTime.ToString() + "');";
                    command.ExecuteNonQuery();
                }
                command.CommandText = "INSERT INTO MIDI (Data, UploadTime) VALUES('" + midiData + "','" + uploadTime.ToString() + "');";
                command.ExecuteNonQuery();
                MessageBox.Show("Successfully uploaded all data!");
                command.CommandText = "SELECT DISTINCT UploadTime FROM MIDI";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (!uploadListBox.Items.Contains(reader.GetString("UploadTime"))) uploadListBox.Items.Add(reader.GetString("UploadTime"));
                }
                reader.Close();
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void portName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
