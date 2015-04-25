using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthWorkHelper.Classes
{
    public static class SmartEnding
    {
        /// <summary>
        /// Функция для получения правильной формы существительного, следующего за числительным
        /// </summary>
        /// <param name="numeral"> Числительное </param>
        /// <param name="noun1"> (одна) "секунда" </param>
        /// <param name="noun234"> (четыре) "секунды" </param>
        /// <param name="nounOther"> (семь) "секунд" </param>
        /// <returns> Правильная форма существительного, следующего за числительным </returns>
        public static string GetNounEnding(int numeral, string noun1, string noun234, string nounOther)
        {
            switch (numeral % 10)
	        {
                case 1:
                    return noun1;
                case 2:
                case 3:
                case 4:
                    return noun234;
		        default:
                    return nounOther;
	        }
        }
    }
}
