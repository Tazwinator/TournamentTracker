using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    class TextConnector : IDataConnection
    {

        private const string PrizesFile = "PrizeModels.csv";
        private const string PeopleFile = "PersonModels.csv";



        /// <summary>
        /// Saves a new prize to a text file.
        /// </summary>
        /// <param name="model">The prize information.</param>
        /// <returns></returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            // Load the text File and convert the etxt to List<Prizemodel>
            // Extension methods are used here if you get confused
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            // Find the max ID
            int currentId = 1;

            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            // Add the new record with the new ID (max + 1)
            prizes.Add(model);

            // Convert the prizes to List<string>
            // save the List<string> to the text file
            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }


        public PersonModel CreatePerson(PersonModel model)
        {
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;

            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            people.Add(model);

            people.SaveToPersonFile(PrizesFile);

            return model;
        }


    }
}
