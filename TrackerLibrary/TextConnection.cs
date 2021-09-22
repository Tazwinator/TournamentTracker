using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    class TextConnection : IDataConnection
    {
        // TODO - Maker the CreatePrize method actually save to a text file
        
        /// <summary>
        /// Saves a new prize to a text file.
        /// </summary>
        /// <param name="model">The prize information.</param>
        /// <returns></returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            model.Id = 1;

            return model;
        }

    }
}
