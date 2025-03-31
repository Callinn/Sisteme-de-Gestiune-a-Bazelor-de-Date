using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABORATOR1.validator
{
    public class Validator
    {
        public bool ValidateInputs(string achievement_name, string description)
        {
            // Validate title
            if (string.IsNullOrWhiteSpace(achievement_name))
            {
                MessageBox.Show("Please enter an achievement name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

       
            // Validate description
            if (string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Please enter a description.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // All inputs are valid
            return true;
        }
    }
}
