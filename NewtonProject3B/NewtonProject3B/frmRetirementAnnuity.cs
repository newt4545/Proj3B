using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ProjectPlutoCalculator
{
	public partial class frmRetirementAnnuity : Form
	{
		public frmRetirementAnnuity()
		{
			InitializeComponent();
		}

		private void frmRetirementAnnuity_Load(object sender, EventArgs e)
		{

		}

		private void btnCalcRetAnu_Click(object sender, EventArgs e)
		{
			try
			{
				if (retAnuPymtBox.Text == "" || retAnuRateBox.Text == "" || retAnuNumPerBox.Text == "")
				{
					//Will throw if any input fields are empty
					throw new MissingFieldException();
				}


				string pay = new string(retAnuPymtBox.Text.Where(c => char.IsDigit(c) || c == '.').ToArray());
				decimal payment = Convert.ToDecimal(pay);
				retAnuPymtBox.Text = payment.ToString();

				string rate = new string(retAnuRateBox.Text.Where(c => char.IsDigit(c) || c == '.').ToArray());
				decimal rateperperiod = Convert.ToDecimal(rate);
				retAnuRateBox.Text = rateperperiod.ToString();
				rateperperiod = rateperperiod / 100;


				string num = new string(retAnuNumPerBox.Text.Where(c => char.IsDigit(c) || c == '.').ToArray());
				decimal numperiods = Math.Round(Convert.ToDecimal(num));
				retAnuNumPerBox.Text = numperiods.ToString();

				//None of the below worked for me. Ended up referencing: Stack Overflow - Stripping Out Non-Numeric Characters in String
				//https://stackoverflow.com/questions/3977497/stripping-out-non-numeric-characters-in-string
				//decimal payment = 0m;
				//Decimal.TryParse(retAnuPymtBox.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out payment);
				//retAnuPymtBox.Text = payment.ToString();
				//decimal rateperperiod = 0m;
				//Decimal.TryParse(retAnuRateBox.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out rateperperiod); 
				//retAnuRateBox.Text = rateperperiod.ToString();
				//decimal numperiods = 0m;
				//Decimal.TryParse(retAnuNumPerBox.Text, NumberStyles.None, CultureInfo.CurrentCulture, out numperiods);
				//retAnuNumPerBox.Text = numperiods.ToString();

				decimal presentvalue = payment * ((1 - (decimal)Math.Pow((double)(1 + rateperperiod), (double)(numperiods * -1))) / rateperperiod);

				retAnuPVbox.Text = Math.Round(presentvalue, 2).ToString();
			}
			catch (FormatException)
			{
				// This will catch non-numeric value inputs
				MessageBox.Show("All entries must be numeric values!", "Error!");
			}
			catch (OverflowException ex)
			{
				//This will catch values that exceed memory capacity
				MessageBox.Show(ex.Message, ex.GetType().ToString());
			}
			catch (DivideByZeroException)
			{
				//This will catch if rate per period is 0
				MessageBox.Show("You cannot divide by zero!", "You know better!");
			}
			catch (MissingFieldException)
			{
				//This will catch missing inputs
				MessageBox.Show("All fields must be filled in!", "Error!");
			}
		}
		
	}
}
