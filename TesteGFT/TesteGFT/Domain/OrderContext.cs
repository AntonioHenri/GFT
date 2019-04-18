using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteGFT.Domain
{
    public class OrderContext
    {

        public List<(Period, int, string, bool)> Menu { get; set; } = new List<(Period, int, string, bool)>  { (Period.manha,1,"ovos",false), ( Period.manha,2, "torrada",false) ,
            ( Period.manha, 3,"cafe",true)
            ,( Period.manha, 4,"Não aplicavel",false),( Period.noite, 1,"bife",false),( Period.noite,2 ,"Batata",true),( Period.noite,2 ,"Vinho",false),( Period.noite,2 ,"Bolo",false)  };


        public IEnumerable<(int, string, bool)> GetMenuForPeriod(Period period)
        {
            return Menu.Where(u => u.Item1 == period).Select(t => (t.Item2, t.Item3, t.Item4));

        }


        public IEnumerable<string> ValidateMenuForId(List<(int, string, bool)> menuForPeriod, List<int> menuOptions)
        {

            var validateResponse = new List<string>();

            foreach (var item in menuOptions)
            {
                //check if exist
                var firstValidate = menuForPeriod.Where(u => u.Item1 == item);
                if (firstValidate.Any())
                {

                    var value = (firstValidate.First().Item2, firstValidate.First().Item3);

                    //check if exit on validateResponse
                    if (validateResponse.Contains(value.Item1) && !value.Item2)
                    {
                        //check if can repeat value
                        validateResponse.Add($"Não pode repetir este item. ({value.Item1})");
                        return validateResponse;
                    }

                    if (validateResponse.Contains(value.Item1) && value.Item2)
                    {
                        //check if can repeat value
                        var newvalue = value.Item1 + "X" + (validateResponse.Where(t => t == value.Item1).Count() + 1).ToString();
                        validateResponse.Remove(value.Item1);
                        validateResponse.Add(newvalue);
                        return validateResponse;
                    }

                    validateResponse.Add(value.Item1);
                }
                else
                {
                    validateResponse.Add("Opção não valida.");

                    return validateResponse;
                }



            }
            return validateResponse;

        }



        public string CheckOrder(string order)
        {

            //validate empty string 
            if (string.IsNullOrEmpty(order))
            {
                return "Order is not empty";
            }

            var values = order.Split(',');

            var options = values.Skip(1);

            //validate int type for options
            foreach (var item in options)
            {
                if (!int.TryParse(item, out var number))
                    return "valores para menu invalidos revisar cardapio";

            }

            if (string.Compare(values[0], Period.manha.ToString(), true) == 0)
            {
                var valuesToCheck = GetMenuForPeriod(Period.manha);
             
                var response = ValidateMenuForId(valuesToCheck.ToList(), options.Select(u=> int.Parse(u)).ToList());

                return String.Join(", ", response.ToArray());
            }
            else
            {
                if (string.Compare(values[0], Period.noite.ToString(), true) == 0)
                {
                    var valuesToCheck = GetMenuForPeriod(Period.noite);
                    
                    var response = ValidateMenuForId(valuesToCheck.ToList(), options.Select(u => int.Parse(u)).ToList());
                
                    String.Join(", ", response.ToArray());
                }
                else
                {
                    return "First value only contains [manha,noite]";

                }
            }

            return "xiiiiii";
        }

    
    }

    public enum Period
    {
        manha,
        noite

    }

}
