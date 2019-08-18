namespace Kiper.Condominio.Core.Helpers.ValueObjects
{
    public class DocumentInfo
    {
        public string Number { get; private set; }

        public DocumentInfo() { }

        public DocumentInfo(string number)
        {
            Number = number;
        }

        public bool Valid(string number)
        {
            int[] mult1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempNumber;
            string digit;

            int sum;
            int rest;
            
            number = number.Trim();
            number = number.Replace(".", "").Replace("-", "");

            if (number.Length != 11)
            {
                return false;
            }
            tempNumber = number.Substring(0, 9);

            sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(tempNumber[i].ToString()) * (mult1[i]);
            }
            rest = sum % 11;

            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            digit = rest.ToString();
            tempNumber = tempNumber + digit;
            int sum2 = 0;

            for (int i = 0; i < 10; i++)
            {
                sum2 += int.Parse(tempNumber[i].ToString()) * mult2[i];
            }

            rest = sum2 % 11;

            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            digit = digit + rest.ToString();
            return number.EndsWith(digit);
        }
    }
}
