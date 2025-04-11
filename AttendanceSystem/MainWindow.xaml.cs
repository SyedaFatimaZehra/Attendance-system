using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;
using System;

namespace AttendanceSystem
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Server=DESKTOP-38Q4IVF\\SA;Database=AttendanceDB;Integrated Security=True;TrustServerCertificate=True;";
        private ObservableCollection<AttendanceRecord> attendanceRecords;

        public MainWindow()
        {
            InitializeComponent();
            attendanceRecords = new ObservableCollection<AttendanceRecord>();
            dataGridAttendance.ItemsSource = attendanceRecords;
            LoadAttendanceRecords();
        }

        private void btnMarkTimeIn_Click(object sender, RoutedEventArgs e)
        {
            string staffName = txtName.Text;
            if (!string.IsNullOrEmpty(staffName))
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        // Check if the staff member already has an active "Time In" record
                        string checkQuery = "SELECT COUNT(*) FROM Attendance WHERE StaffName = @name AND TimeOut IS NULL";
                        SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                        checkCmd.Parameters.AddWithValue("@name", staffName);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("You have already marked Time In. Please mark Time Out before marking Time In again.");
                        }
                        else
                        {
                            string query = "INSERT INTO Attendance (StaffName, TimeIn) VALUES (@name, @timeIn)";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@name", staffName);
                            cmd.Parameters.AddWithValue("@timeIn", DateTime.Now);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Time In recorded successfully.");
                            LoadAttendanceRecords();
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("SQL Error: " + sqlEx.Message + "\n" + sqlEx.StackTrace);
                        Console.WriteLine("SQL Error: " + sqlEx.Message + "\n" + sqlEx.StackTrace);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message + "\n" + ex.StackTrace);
                        Console.WriteLine("Error: " + ex.Message + "\n" + ex.StackTrace);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter your name.");
            }
        }

        private void btnMarkTimeOut_Click(object sender, RoutedEventArgs e)
        {
            string staffName = txtName.Text;
            if (!string.IsNullOrEmpty(staffName))
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        string query = "UPDATE Attendance SET TimeOut = @timeOut WHERE StaffName = @name AND TimeOut IS NULL";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@name", staffName);
                        cmd.Parameters.AddWithValue("@timeOut", DateTime.Now);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Time Out recorded successfully.");
                        LoadAttendanceRecords();
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("SQL Error: " + sqlEx.Message + "\n" + sqlEx.StackTrace);
                        Console.WriteLine("SQL Error: " + sqlEx.Message + "\n" + sqlEx.StackTrace);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message + "\n" + ex.StackTrace);
                        Console.WriteLine("Error: " + ex.Message + "\n" + ex.StackTrace);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter your name.");
            }
        }

        private void btnOpenAdminDashboard_Click(object sender, RoutedEventArgs e)
        {
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.Show();
        }

        private void LoadAttendanceRecords()
        {
            attendanceRecords.Clear();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT StaffName, TimeIn, TimeOut FROM Attendance";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string staffName = reader["StaffName"]?.ToString() ?? "Unknown";
                        DateTime timeIn = reader["TimeIn"] != DBNull.Value ? Convert.ToDateTime(reader["TimeIn"]) : DateTime.MinValue;
                        DateTime? timeOut = reader["TimeOut"] != DBNull.Value ? Convert.ToDateTime(reader["TimeOut"]) : (DateTime?)null;
                        attendanceRecords.Add(new AttendanceRecord(staffName, timeIn, timeOut));
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("SQL Error: " + sqlEx.Message + "\n" + sqlEx.StackTrace);
                    Console.WriteLine("SQL Error: " + sqlEx.Message + "\n" + sqlEx.StackTrace);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message + "\n" + ex.StackTrace);
                    Console.WriteLine("Error: " + ex.Message + "\n" + ex.StackTrace);
                }
            }
        }
    }

    public class AttendanceRecord
    {
        public string StaffName { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }

        public AttendanceRecord(string staffName, DateTime timeIn, DateTime? timeOut = null)
        {
            StaffName = staffName ?? throw new ArgumentNullException(nameof(staffName));
            TimeIn = timeIn;
            TimeOut = timeOut;
        }
    }
}
