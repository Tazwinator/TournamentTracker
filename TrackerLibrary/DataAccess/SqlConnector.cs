using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    class SqlConnector : IDataConnection
    {

        // TODO - Maker the CreatePrize method actually save to the database

        /// <summary>
        /// Saves a new prize to the database.
        /// </summary>
        /// <param name="model">The prize information.</param>
        /// <returns></returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            using ( // "using" prevents memory leaks here by when program get to the last line it stops the connection
                IDbConnection connection = new System.Data.SqlClient.SqlConnection(
                    GlobalConfig.CnnString("Tournaments")
                    )
                )
            {
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure );

                model.Id = p.Get<int>("@id"); // Gets the id after the stored procedure has complete

                return model;

            }; // Stops when it gets here
        }

    }
}
