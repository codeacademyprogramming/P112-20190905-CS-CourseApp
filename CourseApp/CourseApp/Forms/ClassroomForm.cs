using CourseApp.DAL;
using CourseApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseApp.Forms
{
    public partial class ClassroomForm : Form
    {
        ClassroomDTO dto;
        Classroom selectedClassroom;
        public ClassroomForm()
        {
            InitializeComponent();
            dto = new ClassroomDTO();
            selectedClassroom = new Classroom();
            
        }
        private void ClassroomForm_Load(object sender, EventArgs e)
        {
            FillClassrooms();
        }

        public void FillClassrooms()
        {
            dgvClassrooms.Rows.Clear();
            List<Classroom> classrooms = dto.GetAll();
            foreach (Classroom item in classrooms)
            {
                dgvClassrooms.Rows.Add(
                    item.Id,
                    item.Name,
                    item.Capacity
                    );
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            Classroom c = new Classroom();
            c.Name = txtClassroomName.Text;
            c.Capacity = (int)numericCapacity.Value;
            if (dto.Create(c))
            {
                MessageBox.Show("Successfully Created");
            } else
            {
                MessageBox.Show("NOT Created");
            }

        }

        private void dgvClassrooms_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = (int)dgvClassrooms.Rows[e.RowIndex].Cells[0].Value;
            selectedClassroom = dto.Get(id);

            txtClassroomName.Text = selectedClassroom.Name;
            numericCapacity.Value = selectedClassroom.Capacity;

            btnDelete.Visible = true;
            btnUpdate.Visible = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedClassroom != null)
            {
                dto.Delete(selectedClassroom.Id);
                FillClassrooms();
            }

        }
    }

}
