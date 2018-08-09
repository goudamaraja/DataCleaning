using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DataMapping
{
    class DataMappingVM : INotifyPropertyChanged
    {
        const string STATUS_DELETE = "DELETE";

        static DataMappingVM instance = null;

        private OleDbConnection oleDbConnection;
        private DataView dataView;

        // Bound variables
        private string filePath;
        private DataView tableData;
        private ObservableCollection<ComparisonItem> comparisonTable;
        private string tableName;
        private ObservableCollection<string> tableList = new ObservableCollection<string>();
        private string errorMessage = "";
        private Object selectedRow;
        private string firstColumnSelected;
        private string secondColumnSelected;

        private ObservableCollection<string>  columnHeading = new ObservableCollection<string>();

        public ObservableCollection<string> ColumnHeading
        {
            get => columnHeading;
            set { columnHeading = value; propertyChanged(); }
        }
        
        private string newa;
        
        public Object SelectedRow
        {
            get => selectedRow;
            set { selectedRow = value; propertyChanged(); }
        }

        public string Newa
        {
            get => newa;
            set { newa = value; propertyChanged(); }
        }

        public string FilePath
        {
            get => filePath;
            set
            {
                filePath = value;
                readFile();
                propertyChanged();
            }
        }
       
        public ObservableCollection<ComparisonItem> ComparisonTable
        {
            get => comparisonTable;
            set { comparisonTable = value; propertyChanged(); }
        }

        public DataView TableData
        {
            get => tableData;
            set { tableData = value; propertyChanged(); }
        }

        public string TableName
        {
            get => tableName;
            set { tableName = value; propertyChanged(); }
        }

        public ObservableCollection<string> TableList
        {
            get => tableList;
            set { tableList = value; propertyChanged(); }
        }

        public string ErrorMessage
        {
            get => errorMessage;
            set { errorMessage = value; propertyChanged(); }
        }

        public string FirstColumnSelected
        {
            get => firstColumnSelected;
            set { firstColumnSelected = value; propertyChanged(); }
        }

        public string SecondColumnSelected
        {
            get => secondColumnSelected;
            set { secondColumnSelected = value; propertyChanged(); }
        }

        private DataMappingVM()
        {
        }

        public static DataMappingVM GetInstance()
        {
            if (instance == null)
            {
                instance = new DataMappingVM();
            }
            return instance;
        }

        public void ResetColumns()
        {
            FirstColumnSelected = "";
            SecondColumnSelected = "";
        }

        public bool AnalyzeData()
        {
            ErrorMessage = "";

            if (FirstColumnSelected == "" || SecondColumnSelected == "")
            {
                ErrorMessage = "Select both columns";
                return false;
            }

            Thread compareThread = new Thread(task_compare);
            compareThread.Start();
            
            return true;    
        }

        public bool CleanAndSave()
        {
            // Validation
            if (FilePath == null || FilePath == "")
            {
                ErrorMessage = "Error: Select a file first";
                return false;
            }
            if (dataView == null || dataView.Table.Rows.Count == 0)
            {
                ErrorMessage = "Error: No data retrieved";
                return false;
            }

            ObservableCollection<ComparisonItem> severeDuplicationsList = new ObservableCollection<ComparisonItem>(ComparisonTable.Where(p => p.Status == STATUS_DELETE));
            
            if (severeDuplicationsList.Count == 0)
            {
                ErrorMessage = "Error: No data to be cleaned";
                return false;
            }

            ErrorMessage = "";
            string path = FilePath.Insert(FilePath.LastIndexOf('.'), "_New");

            string columnString = "";

            for (int i = 0; i < dataView.Table.Columns.Count; i++)
            {
                columnString += "[" + dataView.Table.Columns[i].ToString() + "] VARCHAR";
                if (i != dataView.Table.Columns.Count - 1)
                    columnString += ",";
            }

            try
            {
                string con = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={path};Extended Properties=\"Excel 12.0 Xml; HDR = YES\";";
                oleDbConnection = new OleDbConnection(con);
                oleDbConnection.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = oleDbConnection;

                cmd.CommandText = "CREATE TABLE [DataCleaned] (" + columnString + ")";
                cmd.ExecuteNonQuery();

                //OleDbCommand command = new OleDbCommand($"select * from [DataCleaned$]", oleDbConnection);
                //OleDbDataAdapter oleda = new OleDbDataAdapter();
                //oleda.SelectCommand = command;

                //// Create a DataSet which will hold the data extracted from the worksheet.
                //DataSet ds = new DataSet();

                //// Fill the DataSet from the data extracted from the worksheet.
                //oleda.Fill(ds);
                //DataView dView = ds.Tables[0].DefaultView;

                // Add only rows not set to delete
                List<int> deleteIdsList = new List<int>(ComparisonTable.Where(p => p.Status == STATUS_DELETE).Select(p => p.SecondID));

                columnString = "";

                for (int i = 0; i < dataView.Table.Columns.Count; i++)
                {
                    columnString += "[" + dataView.Table.Columns[i].ToString() + "]";
                    if (i != dataView.Table.Columns.Count - 1)
                        columnString += ",";
                }

                for (int i = 0; i < dataView.Table.Rows.Count; i++)
                {
                    if (!deleteIdsList.Contains(i))
                    {
                        string valuesString = "";
                        for (int j = 0; j < dataView.Table.Columns.Count; j++)
                        {
                            valuesString += @"""" + dataView.Table.Rows[i][j].ToString() + @"""";
                            if (j != dataView.Table.Columns.Count - 1)
                                valuesString += ",";
                        }

                        cmd.CommandText = "INSERT INTO [DataCleaned](" + columnString + ") VALUES(" + valuesString + ");";
                        cmd.ExecuteNonQuery();
                        
                    }
                }

                oleDbConnection.Close();
            }
            catch (Exception exception)
            {
                ErrorMessage = "Error: Error creating new file";
                return false;
            }

            return true;
        }

        public void SetDelete(ComparisonItem item)
        {
            if (ComparisonTable[ComparisonTable.IndexOf(item)].Status == STATUS_DELETE)
                ComparisonTable[ComparisonTable.IndexOf(item)].Status = "";
            else
                ComparisonTable[ComparisonTable.IndexOf(item)].Status = STATUS_DELETE;

            ComparisonTable = new ObservableCollection<ComparisonItem>(ComparisonTable);
        }

        private void task_compare()
        {
            //ComparisonTable = new ObservableCollection<ComparisonItem>();
            ObservableCollection<ComparisonItem> comparisonTable = new ObservableCollection<ComparisonItem>();

            List<string> firstList = (from d in dataView.Table.AsEnumerable()
                                      select d.Field<string>(FirstColumnSelected)).ToList();

            List<string> secondList = (from d in dataView.Table.AsEnumerable()
                                      select d.Field<string>(SecondColumnSelected)).ToList();

            string firstItem;
            string secondItem;

            for (int i = 0;  i < firstList.Count(); i++)
            {
                firstItem = firstList[i];

                if (firstItem != null && firstItem != "NULL")
                {
                    for (int j = i + 1; j < secondList.Count(); j++)
                    {
                        secondItem = secondList[j];
                        if (secondItem != null && secondItem != "NULL")
                        {
                            double ret = FuzzyString.GetSimilarIndex(firstItem, secondItem);

                            if (ret > 0)
                            {
                                ComparisonItem item = new ComparisonItem()
                                {
                                    FirstID = i,
                                    FirstDescription = firstItem,
                                    PercentageMatching = Convert.ToInt32(ret),
                                    SecondID = j,
                                    SecondDescription = secondItem,
                                    Status = ""
                                };

                                if (item.PercentageMatching >= 80)
                                    item.Status = STATUS_DELETE;

                                comparisonTable.Add(item);
                            }
                        }
                    }
                }
            }

            ComparisonTable = new ObservableCollection<ComparisonItem>(comparisonTable.OrderByDescending(p => p.PercentageMatching));

            //foreach (var firstItem in firstList)
            //{
            //    if (firstItem.field != null && firstItem.field != "NULL")
            //    {
            //        foreach (var secondItem in secondList)
            //        {
            //            if (secondItem.field != null && secondItem.field != "NULL" && secondItem.ID != firstItem.ID)
            //            {
            //                double ret = FuzzyString.GetSimilarIndex(firstItem.field, secondItem.field);

            //                if (ret > 0)
            //                {
            //                    comparisonTable.Add(new ComparisonItem()
            //                    {
            //                        FirstID = firstItem.ID,
            //                        FirstDescription = firstItem.field,
            //                        PercentageMatching = Convert.ToInt32(ret),
            //                        SecondID = secondItem.ID,
            //                        SecondDescription = secondItem.field
            //                    });
            //                }
            //            }
            //        }
            //    }
            //}

            //ComparisonTable = new ObservableCollection<ComparisonItem>(
            //                                   (from d1 in dataView.Table.AsEnumerable()
            //                                    from d2 in dataView.Table.AsEnumerable()
            //                                    where d1.Field<string>(FirstColumnSelected) == d2.Field<string>(SecondColumnSelected) &&
            //                                          d1.Field<int>("ID") != d2.Field<int>("ID") &&
            //                                          d1.Field<string>(FirstColumnSelected) != null &&
            //                                          d1.Field<string>(FirstColumnSelected) != "NULL"
            //                                    select new ComparisonItem()
            //                                    {
            //                                        FirstID = d1.Field<int>("ID"),
            //                                        FirstDescription = d1.Field<string>(FirstColumnSelected),
            //                                        PercentageMatching = 100,
            //                                        SecondID = d2.Field<int>("ID"),
            //                                        SecondDescription = d2.Field<string>(SecondColumnSelected)
            //                                    }).ToList());
        }

        private void readFile()
        {
            string con = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties=\"Excel 12.0 Xml; HDR = YES\";";
            oleDbConnection = new OleDbConnection(con);
            oleDbConnection.Open();
            DataTable dt = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {
                return;
            }

            TableList.Clear();

            // Add the sheet name to the string array.
            foreach (DataRow row in dt.Rows)
            {
                TableList.Add(row["TABLE_NAME"].ToString());
            }
        }

        public bool ReadDataFromFile(string tableName)
        {
            ErrorMessage = "";
            List<string> listColumnInt = new List<string>();

            try
            {
                OleDbCommand command = new OleDbCommand($"select * from [{tableName}]", oleDbConnection);
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                oleda.SelectCommand = command;

                // Create a DataSet which will hold the data extracted from the worksheet.
                DataSet ds = new DataSet();

                // Fill the DataSet from the data extracted from the worksheet.
                oleda.Fill(ds);
                dataView = ds.Tables[0].DefaultView;

                //Deleting and Re-creating columns which has Integer value
                columnHeading = new ObservableCollection<string>();
                for (int i =0; i< dataView.Table.Columns.Count; i++)
                {
                    columnHeading.Add(dataView.Table.Columns[i].ToString());
                    if (dataView.Table.Rows[0].ItemArray[i].GetType() != typeof(string))
                    {
                        listColumnInt.Add(dataView.Table.Columns[i].ToString());
                    }
                }
                foreach (string listColumn in listColumnInt)
                {
                    List<string> columnString = new List<string>();
                    for (int j = 0; j < dataView.Table.Rows.Count; j++)
                    {
                        columnString.Add(dataView.Table.Rows[j][listColumn].ToString());
                    }
                   dataView.Table.Columns.Remove(dataView.Table.Columns[listColumn].ToString());
                   dataView.Table.Columns.Add(listColumn, typeof(string));
                   for (int i = 0; i < dataView.Table.Rows.Count; i++)
                    {
                        dataView.Table.Rows[i][listColumn] = columnString[i];
                    }
                }
                
                ComparisonTable = null;
                TableData = dataView;
                TableName = tableName;
            }
            catch (Exception exception)
            {
                ErrorMessage = "Error: Not possible to read table";
                return false;
            }

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void propertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
