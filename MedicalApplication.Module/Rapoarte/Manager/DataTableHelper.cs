using MedicalApplication.Module.BusinessObjects;
using MedicalApplication.Module.BusinessObjects.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApplication.Module.Rapoarte.Manager
{
    public class DataTableHelper
    {
        public static DataSet ExecutaProceduraStocata(string numeProcedura, List<Parametru> parametrii, MedicalApplicationEFCoreDbContext dbContext)
        {
            using var command = dbContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = numeProcedura;
            command.CommandType = CommandType.StoredProcedure;
            foreach (var param in parametrii)
            {
                var sqlParam = new SqlParameter(param.Nume, param.Valoare);
                command.Parameters.Add(sqlParam);
            }

            using var adapter = new SqlDataAdapter(command as SqlCommand);
            var dataSet = new DataSet();
            adapter.Fill(dataSet);

            return dataSet;
        }
    }
}
