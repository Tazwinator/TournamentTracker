using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.DataAccess;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {

		private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
		private List<PersonModel> selectedTeamMembers = new List<PersonModel>();
		public CreateTeamForm()
		{
			InitializeComponent();

			//CreateSampleData();

			WireUpLists();
		}

		private void CreateSampleData()
		{
			availableTeamMembers.Add(new PersonModel { FirstName = "Tim", LastName = "Corey" });
			availableTeamMembers.Add(new PersonModel { FirstName = "Sue", LastName = "Storm" });

			selectedTeamMembers.Add(new PersonModel { FirstName = "Jane", LastName = "Smith" });
			selectedTeamMembers.Add(new PersonModel { FirstName = "Bill", LastName = "Jones" });
		}

		private void WireUpLists()
		{
			selectTeamMemberDropDown.DataSource = null;

			selectTeamMemberDropDown.DataSource = availableTeamMembers;
			selectTeamMemberDropDown.DisplayMember = "FullName";

			teamMembersListBox.DataSource = null;

			teamMembersListBox.DataSource = selectedTeamMembers;
			teamMembersListBox.DisplayMember = "FullName";
		}

		private void createMemberButton_Click(object sender, EventArgs e)
		{
			if (ValidateForm())
			{
				PersonModel p = new PersonModel();

				p.FirstName = firstNameValue.Text;
				p.LastName = lastNameValue.Text;
				p.EmailAddress = emailValue.Text;
				p.CellphoneNumber = cellphoneValue.Text;

				GlobalConfig.Connection.CreatePerson(p);


				firstNameValue.Text = string.Empty;
				lastNameValue.Text = string.Empty;
				emailValue.Text = string.Empty;
				cellphoneValue.Text = string.Empty;
			}
			else
			{
				MessageBox.Show("You need to fill in all of the fields.");
			}
		}

		private bool ValidateForm()
		{
			if (firstNameValue.Text.Length == 0)
			{
				return false;
			}

			if (lastNameValue.Text.Length == 0)
			{
				return false;
			}

			if (emailValue.Text.Length == 0)
			{
				return false;
			}

			if (cellphoneValue.Text.Length == 0)
			{
				return false;
			}

			return true;
		}

    }
}
