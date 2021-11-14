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


        private const string db = "Tournaments";

        /// <summary>
        /// Saves a new person to the database
        /// </summary>
        /// <param name="model">The person information</param>
        /// <returns>The person information plus the unique identifier.</returns>
        public PersonModel CreatePerson(PersonModel model)
        {
            using ( 
                IDbConnection connection = new System.Data.SqlClient.SqlConnection(
                    GlobalConfig.CnnString(db)
                    )
                )
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@EmailAddress", model.EmailAddress);
                p.Add("@CellPhoneNumber", model.CellphoneNumber);
                p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("[dbo].[spPeople_Insert]", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@Id");

                return model;
            }
        }



        /// <summary>
        /// Saves a new prize to the database.
        /// </summary>
        /// <param name="model">The prize information.</param>
        /// <returns></returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            using ( // "using" prevents memory leaks here by when program get to the last line it stops the connection
                IDbConnection connection = new System.Data.SqlClient.SqlConnection(
                    GlobalConfig.CnnString(db)
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

        public List<PersonModel> Getperson_All()
        {
            List<PersonModel> output;

            using (
                IDbConnection connection = new System.Data.SqlClient.SqlConnection(
                    GlobalConfig.CnnString(db)
                    )
                )
            {
                output = connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }
            return output;
        }
    }
}
