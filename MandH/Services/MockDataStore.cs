using MandH.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MandH.Services
{
    public class MockDataStore : IDataStore<FoodString>
    {
        private List<FoodString> foodStrings;
        public DataTable abouttable;
        public MockDataStore()
        {
            GetRefresh();

        }
        public async Task<FoodString> GetItemAsync(string date)
        {
            return await Task.FromResult(foodStrings.FirstOrDefault(s => s.Date == date));
        }


        public async Task<IEnumerable<FoodString>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(foodStrings);
        }


        public async Task<bool> UpdateItemAsync(FoodString item)
        {
            var oldItem = foodStrings.Where((FoodString arg) => arg.Date == item.Date).FirstOrDefault();
            foodStrings.Remove(oldItem);
            foodStrings.Add(item);

            return await Task.FromResult(true);
        }
        
        public void GetRefresh()
        {
            string strConn = $"Server={Properties.Resources.server};Database={Properties.Resources.database};Uid={Properties.Resources.id};Pwd={Properties.Resources.pw};";
            foodStrings =  new List<FoodString>();
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("mh_datefood", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Input param
                MySqlParameter pstart = new MySqlParameter("@v_startdate", MySqlDbType.VarChar, 8);
                MySqlParameter pend = new MySqlParameter("@v_enddate", MySqlDbType.VarChar, 8);

                DateTime dateToday = DateTime.Today;
                DateTime mondayDate = dateToday.AddDays(Convert.ToInt32(DayOfWeek.Monday) - Convert.ToInt32(dateToday.DayOfWeek));
                DateTime fridayDate = dateToday.AddDays(Convert.ToInt32(DayOfWeek.Friday) - Convert.ToInt32(dateToday.DayOfWeek));

                pstart.Value = mondayDate.ToString("yyyyMMdd", new CultureInfo("ko-KR"));
                pend.Value = fridayDate.ToString("yyyyMMdd", new CultureInfo("ko-KR"));
                
                cmd.Parameters.Add(pstart);
                cmd.Parameters.Add(pend);

                DataSet ds = new DataSet();
                MySqlDataAdapter adpt = new MySqlDataAdapter(cmd);
                adpt.Fill(ds);
            abouttable = ds.Tables[1];

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    foodStrings.Add(new FoodString() { Date = ds.Tables[0].Rows[i]["date"].ToString(), Lunch = ds.Tables[0].Rows[i]["lunchstring"].ToString().Replace("\\n", System.Environment.NewLine), LunchBox = ds.Tables[0].Rows[i]["lunchboxstring"].ToString().Replace("\\n", System.Environment.NewLine), Dinner = ds.Tables[0].Rows[i]["dinnerstring"].ToString().Replace("\\n", System.Environment.NewLine) });
                }
            }
        }
        public DataTable AboutRefresh()
        {
            return abouttable;
        }
    }
}